using System;
using System.Runtime.InteropServices;

#nullable disable

internal class ClipboardHelper
{
    private const uint uint_0 = 13;

    [DllImport("kernel32.dll")]
    private static extern IntPtr GlobalAlloc(uint uint_1, uint uint_2);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GlobalLock(IntPtr intptr_0);

    [DllImport("kernel32.dll")]
    private static extern bool GlobalUnlock(IntPtr intptr_0);

    [DllImport("user32.dll")]
    private static extern bool OpenClipboard(IntPtr intptr_0);

    [DllImport("user32.dll")]
    private static extern bool EmptyClipboard();

    [DllImport("user32.dll")]
    private static extern bool CloseClipboard();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetClipboardData(uint uint_1, IntPtr intptr_0);

    public static unsafe bool smethod_0(string string_0)
    {
        var intptr_0 = GlobalAlloc(0U, (uint)((string_0.Length + 1) * 2));
        var pointer = (char*)GlobalLock(intptr_0).ToPointer();
        var index = 0;
        while (index < string_0.Length)
        {
            *pointer = string_0[index];
            ++index;
            ++pointer;
        }

        pointer[1] = char.MinValue;
        GlobalUnlock(intptr_0);
        if (!OpenClipboard(IntPtr.Zero))
        {
            Logger.LogMessage("Unable to open clipboard!");
            return false;
        }

        if (!EmptyClipboard())
        {
            Logger.LogMessage("Unable to empty clipboard!");
            return false;
        }

        if (SetClipboardData(13U, intptr_0) == IntPtr.Zero)
            Logger.LogMessage("SetClipboardData failed, last error: " + Marshal.GetLastWin32Error());
        CloseClipboard();
        return true;
    }
}