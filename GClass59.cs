// Decompiled with JetBrains decompiler
// Type: GClass59
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Runtime.InteropServices;

public class GClass59
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