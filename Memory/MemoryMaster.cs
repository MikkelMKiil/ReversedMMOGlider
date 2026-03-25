using System;
using System.Diagnostics;
using System.Runtime.InteropServices;


public static class MemoryMaster
{
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint uint_0, bool bool_0, int int_0);

    public static IntPtr GetWoWHandle()
    {
        var array = Process.GetProcessesByName("WoW");
        return array.Length == 0 ? IntPtr.Zero : OpenProcess(2035711U, false, array[0].Id);
    }
}

