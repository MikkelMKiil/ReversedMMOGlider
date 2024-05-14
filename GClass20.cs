// Decompiled with JetBrains decompiler
// Type: GClass20
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Runtime.InteropServices;
using System.Threading;

public class GClass20
{
    public static bool bool_0 = false;
    public static int int_0 = 64;
    public static int int_1 = 1;
    public static int int_2 = 0;
    public static int int_3 = 2;
    public static int int_4 = 4;
    public static int int_5 = 8;
    public static int int_6 = 16;
    public static int int_7 = 8192;
    public static int int_8 = 65536;
    public static int int_9 = 1114112;
    public static int int_10 = 131072;
    public static int int_11 = 262148;

    [DllImport("WinMM.dll", SetLastError = true)]
    public static extern bool PlaySound(string string_0, int int_12, int int_13);

    public static void smethod_0(string string_0)
    {
        if (bool_0)
            return;
        Logger.smethod_1("Sounds.Play: \"" + string_0 + "\" (last error now is " + Marshal.GetLastWin32Error() + ")");
        for (var index = 0; index < 5 && !PlaySound(".\\" + string_0, 0, int_1 | int_10 | int_7); ++index)
        {
            Logger.smethod_1("PlaySound failed, last error = " + Marshal.GetLastWin32Error() + ", current dir = " +
                               Environment.CurrentDirectory);
            Thread.Sleep(700);
        }
    }

    public static void smethod_1(string string_0)
    {
        Logger.smethod_1("Sounds.PlayWait: \"" + string_0 + "\"");
        PlaySound(string_0, 0, int_2 | int_10 | int_7);
    }

    public static void smethod_2(string string_0)
    {
        Logger.smethod_1("Sounds.PlayAlias: \"" + string_0 + "\"");
        PlaySound(string_0, 0, int_1 | int_8 | int_7);
    }

    public static void smethod_3()
    {
        PlaySound(null, 0, int_0);
    }
}