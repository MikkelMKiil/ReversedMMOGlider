using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;


public static class MemoryMaster
{
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint uint_0, bool bool_0, int int_0);

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    private static string GetWindowTextSafe(IntPtr hWnd)
    {
        var sb = new StringBuilder(256);
        return GetWindowText(hWnd, sb, sb.Capacity) > 0 ? sb.ToString() : string.Empty;
    }

    private static string GetClassNameSafe(IntPtr hWnd)
    {
        var sb = new StringBuilder(256);
        return GetClassName(hWnd, sb, sb.Capacity) > 0 ? sb.ToString() : string.Empty;
    }

    private static IntPtr FindBestTopLevelWindowForProcess(int processId)
    {
        IntPtr best = IntPtr.Zero;
        var bestTitleLen = -1;

        EnumWindows((hWnd, lParam) =>
        {
            int pid;
            GetWindowThreadProcessId(hWnd, out pid);
            if (pid != processId)
                return true;

            if (!IsWindowVisible(hWnd))
                return true;

            var title = GetWindowTextSafe(hWnd);
            if (string.IsNullOrEmpty(title))
                return true;

            // Prefer the window with the longest title (usually the main frame).
            if (title.Length > bestTitleLen)
            {
                best = hWnd;
                bestTitleLen = title.Length;
            }

            return true;
        }, IntPtr.Zero);

        return best;
    }


    public static IntPtr GetWoWHandle()
    {
        var array = Process.GetProcessesByName("WoW");
        if (array.Length == 0)
            return IntPtr.Zero;

        var process = array[0];

        try
        {
            var hwnd = process.MainWindowHandle;
            if (hwnd == IntPtr.Zero)
                hwnd = FindBestTopLevelWindowForProcess(process.Id);

            if (hwnd != IntPtr.Zero)
            {
                StartupClass.MainApplicationHandle = hwnd;
                Logger.LogMessage("[Input] WoW HWND selected: 0x" + hwnd.ToInt64().ToString("x") +
                                  ", Title=\"" + GetWindowTextSafe(hwnd) + "\", Class=\"" + GetClassNameSafe(hwnd) + "\"");
            }
            else
            {
                Logger.LogMessage("[Critical] WoW window handle (HWND) could not be resolved. Input to game window will fail (PostMessage requires HWND). Ensure WoW is running with a visible window.");
            }
        }
        catch (Exception ex)
        {
            Logger.LogMessage("[Critical] Exception resolving WoW window handle (HWND): " + ex.Message);
        }

        return OpenProcess(2035711U, false, process.Id);
    }
}

