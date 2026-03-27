using System;
using System.Runtime.InteropServices;

#nullable disable

public class WindowContextHelper
{
    [DllImport("user32.dll")]
    public static extern IntPtr GetDesktopWindow();

    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowDC(IntPtr intptr_0);

    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr intptr_0);

    [DllImport("user32.dll")]
    public static extern IntPtr ReleaseDC(IntPtr intptr_0, IntPtr intptr_1);

    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowRect(IntPtr intptr_0, ref GStruct16 gstruct16_0);

    [DllImport("user32.dll")]
    public static extern IntPtr GetClientRect(IntPtr intptr_0, ref GStruct16 gstruct16_0);

    public struct GStruct16
    {
        public int int_0;
        public int int_1;
        public int int_2;
        public int int_3;
    }
}