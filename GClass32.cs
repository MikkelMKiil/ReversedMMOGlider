// Decompiled with JetBrains decompiler
// Type: GClass32
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Runtime.InteropServices;

public class GClass32
{
    [DllImport("ISXWardenLink.dll")]
    private static extern bool ISXWarden_IsPresent(int int_0);

    [DllImport("ISXWardenLink.dll")]
    private static extern bool ISXWarden_ProtectProcess(int int_0, string string_0);

    [DllImport("ISXWardenLink.dll")]
    private static extern bool ISXWarden_ProtectWindow(IntPtr intptr_0, string string_0);

    public bool method_0(IntPtr intptr_0)
    {
        return false;
    }
}