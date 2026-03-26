using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#nullable disable
public static class CrashHandler
{
    // Register global exception handlers. Call early in startup (before Application.Run).
    public static void RegisterExceptionHandlers()
    {
        try
        {
            Application.ThreadException += new ThreadExceptionEventHandler(OnThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);
            Logger.LogMessage("CrashHandler: global exception handlers registered.");
        }
        catch (Exception ex)
        {
            try { Logger.LogMessage("CrashHandler: failed to register handlers: " + ex.Message); } catch { }
        }
    }

    public static void OnThreadException(object sender, ThreadExceptionEventArgs e)
    {
        try
        {
            LogException(e.Exception, "Application.ThreadException", false);
        }
        catch { }

        try
        {
            MessageBox.Show("An unhandled UI exception occurred and was logged. The application may need to close. See crash log for details.", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch { }
    }

    public static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        try
        {
            var ex = e.ExceptionObject as Exception;
            LogException(ex, "AppDomain.CurrentDomain.UnhandledException", e.IsTerminating);
            try { WriteMiniDump(); } catch { }
        }
        catch { }

        try
        {
            MessageBox.Show("A fatal error occurred and has been logged. The application may need to close.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch { }
    }

    private static void LogException(Exception ex, string source, bool isTerminating)
    {
        try
        {
            var sb = new StringBuilder();
            sb.AppendLine("===== Crash Report =====");
            sb.AppendLine("Timestamp: " + DateTime.Now.ToString("o"));
            sb.AppendLine("Source: " + source);
            sb.AppendLine("IsTerminating: " + isTerminating);
            sb.AppendLine("Thread: " + Thread.CurrentThread.ManagedThreadId);
            sb.AppendLine("Process: " + Process.GetCurrentProcess().ProcessName + " (PID=" + Process.GetCurrentProcess().Id + ")");
            sb.AppendLine("CLR Version: " + Environment.Version);
            sb.AppendLine("OS Version: " + Environment.OSVersion);
            sb.AppendLine("WorkingSet: " + Process.GetCurrentProcess().WorkingSet64);
            sb.AppendLine();

            if (ex == null)
            {
                sb.AppendLine("Exception object was null (non-Exception thrown)");
            }
            else
            {
                AppendExceptionDetails(sb, ex);
            }

            sb.AppendLine();
            sb.AppendLine("Loaded assemblies:");
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    sb.AppendLine(string.Format("  {0} - {1}", asm.GetName().Name, asm.Location ?? "<dynamic/unknown>"));
                }
                catch { }
            }

            var fileName = string.Format("crash_{0:yyyyMMdd_HHmmss}.log", DateTime.Now);
            try
            {
                File.AppendAllText(fileName, sb.ToString());
            }
            catch { }

            try
            {
                File.AppendAllText("Glider.log", DateTime.Now.ToString("HH:mm:ss.ffff ") + "FATAL: " + sb.ToString() + Environment.NewLine);
            }
            catch { }

            try { Logger.LogMessage("Unhandled exception: " + (ex != null ? ex.Message : "<null>")); } catch { }
        }
        catch { }
    }

    private static void AppendExceptionDetails(StringBuilder sb, Exception ex)
    {
        var depth = 0;
        while (ex != null && depth < 20)
        {
            sb.AppendLine(string.Format("--- Exception level {0} ---", depth));
            sb.AppendLine("Type: " + ex.GetType().FullName);
            sb.AppendLine("Message: " + ex.Message);
            sb.AppendLine("StackTrace:");
            sb.AppendLine(ex.StackTrace ?? "<no stacktrace>");
            sb.AppendLine();
            ex = ex.InnerException;
            depth++;
        }
    }

    private static void WriteMiniDump()
    {
        try
        {
            var proc = Process.GetCurrentProcess();
            var dumpName = string.Format("crash_{0:yyyyMMdd_HHmmss}.dmp", DateTime.Now);
            using (var fs = new FileStream(dumpName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                IntPtr hFile = IntPtr.Zero;
                // Try to get SafeFileHandle.DangerousGetHandle via reflection for broad compatibility
                try
                {
                    var safeHandleProp = typeof(FileStream).GetProperty("SafeFileHandle");
                    if (safeHandleProp != null)
                    {
                        var safeHandle = safeHandleProp.GetValue(fs, null);
                        if (safeHandle != null)
                        {
                            var dangerous = safeHandle.GetType().GetMethod("DangerousGetHandle");
                            if (dangerous != null)
                                hFile = (IntPtr)dangerous.Invoke(safeHandle, null);
                        }
                    }
                }
                catch { }

                if (hFile == IntPtr.Zero)
                {
                    try
                    {
                        var handleProp = typeof(FileStream).GetProperty("Handle");
                        if (handleProp != null)
                        {
                            var handleVal = handleProp.GetValue(fs, null);
                            if (handleVal is IntPtr)
                                hFile = (IntPtr)handleVal;
                            else if (handleVal is int)
                                hFile = new IntPtr((int)handleVal);
                        }
                    }
                    catch { }
                }

                var success = false;
                if (hFile != IntPtr.Zero)
                {
                    success = MiniDumpWriteDump(proc.Handle, (uint)proc.Id, hFile, MiniDumpWithFullMemoryFlags, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
                }
                else
                {
                    success = MiniDumpWriteDump(proc.Handle, (uint)proc.Id, IntPtr.Zero, MiniDumpWithFullMemoryFlags, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
                }

                try { Logger.LogMessage("MiniDumpWriteDump success=" + success); } catch { }
            }
        }
        catch (Exception ex)
        {
            try { Logger.LogMessage("WriteMiniDump failed: " + ex.Message); } catch { }
        }
    }

    [DllImport("dbghelp.dll", SetLastError = true)]
    private static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, IntPtr hFile, uint dumpType, IntPtr expParam, IntPtr userStreamParam, IntPtr callbackParam);

    private const uint MiniDumpNormal = 0x00000000;
    private const uint MiniDumpWithDataSegs = 0x00000001;
    private const uint MiniDumpWithFullMemory = 0x00000002;
    private const uint MiniDumpWithHandleData = 0x00000004;
    private const uint MiniDumpWithFullMemoryInfo = 0x00000800;

    private static readonly uint MiniDumpWithFullMemoryFlags = MiniDumpWithFullMemory | MiniDumpWithDataSegs | MiniDumpWithHandleData | MiniDumpWithFullMemoryInfo;
}
