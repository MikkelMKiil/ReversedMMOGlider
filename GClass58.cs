// Decompiled with JetBrains decompiler
// Type: GClass58
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Runtime.InteropServices;

public class GClass58
{
    public const int int_0 = 13369376;

    [DllImport("gdi32.dll")]
    public static extern bool BitBlt(
        IntPtr intptr_0,
        int int_1,
        int int_2,
        int int_3,
        int int_4,
        IntPtr intptr_1,
        int int_5,
        int int_6,
        int int_7);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleBitmap(IntPtr intptr_0, int int_1, int int_2);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleDC(IntPtr intptr_0);

    [DllImport("gdi32.dll")]
    public static extern bool DeleteDC(IntPtr intptr_0);

    [DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr intptr_0);

    [DllImport("gdi32.dll")]
    public static extern IntPtr SelectObject(IntPtr intptr_0, IntPtr intptr_1);
}