// Decompiled with JetBrains decompiler
// Type: GClass37
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common;
using Glider.Common.Objects;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

public sealed class Logger
{
    public interface IUiSink
    {
        void OnLogMessage(string string_0);

        void OnLoggerRefreshRequested();

        void OnLoggerToggleRequested();

        void OnLoggerShutdownRequested();
    }

    public static readonly Logger Instance = new Logger();

    private readonly object syncRoot = new object();
    private readonly string logFilePath = "Glider.log";
    private IUiSink uiSink;
    private StreamWriter writer;
    private Thread workThread;
    private bool bool_0;
    private GLocation glocation_0;

    public void AttachUiSink(IUiSink iUiSink_0)
    {
        uiSink = iUiSink_0;
    }

    public void DetachUiSink(IUiSink iUiSink_0)
    {
        if (uiSink == iUiSink_0)
            uiSink = null;
    }

    public static void LogMessage(string string_0)
    {
        Instance.imethod_2(string_0);
    }

    public static void LogDebug(string string_0)
    {
        Instance.imethod_3(string_0);
    }

    public static void smethod_1(string string_0)
    {
        LogDebug(string_0);
    }

    public static void smethod_0()
    {
        try
        {
            Instance.method_0();
        }
        catch (Exception ex)
        {
            int num = (int)MessageBox.Show("** Last chance exception from inviso: " + ex.Message + "\r\n\r\n" + ex.StackTrace);
        }
    }

    public void imethod_0()
    {
        if (uiSink != null)
            uiSink.OnLoggerRefreshRequested();
    }

    public void imethod_1()
    {
        bool_0 = !bool_0;
        if (uiSink != null)
            uiSink.OnLoggerToggleRequested();
    }

    public void imethod_2(string string_0)
    {
        lock (syncRoot)
        {
            WriteLine(FormatCompactEntry(DateTime.Now, string_0));
        }

        if (string_0.StartsWith("[Debug]"))
            return;

        if (uiSink != null)
            uiSink.OnLogMessage(string_0);
        else
            StartupClass.smethod_17(2, string_0);
    }

    public void imethod_3(string string_0)
    {
        if (!StartupClass.IsBetaAccessGranted)
            return;
        imethod_2("[Debug] " + string_0);
    }

    public void imethod_4()
    {
        if (uiSink != null)
            uiSink.OnLoggerShutdownRequested();
        if (workThread != null)
            workThread.Interrupt();
        Environment.Exit(0);
    }

    public void Reset()
    {
        lock (syncRoot)
        {
            CloseWriter();
            if (File.Exists(logFilePath))
                File.Delete(logFilePath);
            using (File.Create(logFilePath))
            {
            }
        }
    }

    public void method_0()
    {
        Reset();
        StartupClass.StartupLogger = this;
        StartupClass.InitStartupMode(AppMode.Normal);
        StartupClass.SelectedWaypointType = WaypointType.const_0;
        workThread = new Thread(method_3);
        workThread.Start();
    }

    public void method_3()
    {
        try
        {
            method_4();
        }
        catch (ThreadInterruptedException)
        {
            imethod_2("Work thread interrupted, shutting down now");
        }
        catch (Exception ex)
        {
            imethod_2("!! Exception in workthread: " + ex.Message + ex.StackTrace);
        }
    }

    private void method_4()
    {
        while (true)
        {
            Thread.Sleep(200);
            if (bool_0 && StartupClass.IsRuntimeAttached)
                goto label_6;
label_1:
            if (!StartupClass.isInputStringFourCharacters)
                StartupClass.LicenseCheckTimer = null;
            if (StartupClass.LicenseCheckTimer != null && StartupClass.LicenseCheckTimer.method_3())
            {
                StartupClass.LicenseCheckTimer = null;
                StartupClass.HasClassLoadMismatch = true;
                Logger.LogMessage(MessageProvider.GetMessage(103));
                StartupClass.smethod_27(false, "Timer2Up");
            }

            StartupClass.smethod_38();
            continue;
label_6:
            if (glocation_0 == null)
            {
                glocation_0 = GPlayerSelf.Me.Location;
            }
            else if (GPlayerSelf.Me.Location.GetDistanceTo(glocation_0) > StartupClass.AutoAddDistance)
            {
                StartupClass.smethod_23();
                glocation_0 = GPlayerSelf.Me.Location;
                SoundPlayer.smethod_0("Key.wav");
                goto label_1;
            }
            else
            {
                goto label_1;
            }
        }
    }

    private void WriteLine(string string_0)
    {
        try
        {
            EnsureWriter();
            writer.WriteLine(string_0);
            writer.Flush();
        }
        catch (IOException ex)
        {
            Console.WriteLine(MessageProvider.smethod_2(90, ex.Message));
        }
    }

    private void EnsureWriter()
    {
        if (writer != null)
            return;
        writer = File.AppendText(logFilePath);
    }

    private void CloseWriter()
    {
        if (writer == null)
            return;
        try
        {
            writer.Flush();
            writer.Close();
        }
        catch
        {
        }
        finally
        {
            writer = null;
        }
    }

    private static string FormatCompactEntry(DateTime dateTime_0, string string_0)
    {
        return dateTime_0.ToString("HH:mm:ss.ffff ") + string_0;
    }
}

