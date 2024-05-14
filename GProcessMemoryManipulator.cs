// Decompiled with JetBrains decompiler
// Type: GClass78
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

public class GProcessMemoryManipulator
{
    public delegate bool GDelegate1(IntPtr intptr_0, IntPtr intptr_1);

    private const uint uint_0 = 2035711;
    private const uint uint_1 = 131088;
    private const uint uint_2 = 16;
    private const uint uint_3 = 32;
    private const uint uint_4 = 8;
    private const uint uint_5 = 1024;
    private const uint uint_6 = 1;
    private const uint uint_7 = 4096;
    private const uint uint_8 = 32768;
    private const int int_0 = 4;
    private const int int_1 = 16;
    public const int int_2 = 1;
    public const int int_3 = 2;
    public const int int_4 = 8;
    public const int int_5 = 32;
    public const int int_6 = 64;
    public const int int_7 = 128;
    public const int int_8 = 256;
    public const int int_9 = 512;
    public const int int_10 = 1024;
    private const uint uint_9 = 1;
    private const uint uint_10 = 2;
    private const uint uint_11 = 4;
    private const uint uint_12 = 8;
    private const uint uint_13 = 16;
    private const uint uint_14 = 32;
    private const uint uint_15 = 64;
    private const uint uint_16 = 128;
    private const uint uint_17 = 256;
    private const uint uint_18 = 512;
    private const uint uint_19 = 1024;
    private const int int_11 = 0;
    private const int int_12 = 0;
    private const int int_13 = 1;
    private const int int_14 = 1;
    private const int int_15 = 2;
    private const int int_16 = 3;
    private const int int_17 = 3;
    private const int int_18 = 4;
    private const int int_19 = 5;
    private const int int_20 = 6;
    private const int int_21 = 7;
    private const int int_22 = 8;
    private const int int_23 = 9;
    private const int int_24 = 10;
    private const int int_25 = 11;
    private const int int_26 = 11;
    private const uint uint_20 = 0;
    private const uint uint_21 = 64;
    private const uint uint_22 = 2;
    public static bool bool_0 = false;
    private static readonly bool bool_1 = true;
    public static bool bool_2;
    public static bool bool_3 = true;
    public static int int_27;
    private static readonly SortedList<int, string> sortedList_0 = new SortedList<int, string>();
    private static IntPtr intptr_0;
    private static int int_28;
    private static IntPtr intptr_1;

    public static string smethod_0()
    {
        var num = StartupClass.random_0.Next() % 10 + 8;
        var stringBuilder = new StringBuilder();
        while (stringBuilder.Length < num)
            stringBuilder.Append((char)(StartupClass.random_0.Next() % 26 + 97));
        return stringBuilder.ToString();
    }

    public static int smethod_1()
    {
        var gclass65 = new GClass65();
        gclass65.method_0();
        if (StartupClass.int_12 != 0)
        {
            if (gclass65.method_2(StartupClass.int_12) == 0)
                return 0;
            StartupClass.int_3 = StartupClass.int_12;
            StartupClass.intptr_0 = smethod_29(StartupClass.int_3);
            return StartupClass.int_3;
        }

        if (StartupClass.intptr_1 != IntPtr.Zero)
        {
            if (smethod_56(StartupClass.int_3))
                return StartupClass.int_3;
            CloseHandle(StartupClass.intptr_1);
            StartupClass.intptr_1 = IntPtr.Zero;
            StartupClass.int_3 = 0;
            StartupClass.bool_4 = true;
            StartupClass.bool_11 = false;
        }

        var str = GClass61.gclass61_0.method_2("AttachEXE");
        if (StartupClass.bool_2)
            str = "Solitaire.exe";
        if (gclass65.method_1(str) == 0)
            return 0;
        var num = 0;
        foreach (var gclass66 in gclass65.gclass66_0)
            if (string.Compare(gclass66.string_0, str, true) == 0 && !sortedList_0.ContainsKey(gclass66.int_0))
            {
                num = gclass66.int_0;
                break;
            }

        if (num == 0)
            return 0;
        StartupClass.int_3 = num;
        StartupClass.intptr_0 = smethod_29(StartupClass.int_3);
        return StartupClass.int_3;
    }

    public static Rectangle smethod_2()
    {
        var gstruct22_0 = new GStruct22();
        GetWindowRect(smethod_29(StartupClass.int_3), out gstruct22_0);
        return new Rectangle(gstruct22_0.int_0, gstruct22_0.int_1, gstruct22_0.int_2 - gstruct22_0.int_0,
            gstruct22_0.int_3 - gstruct22_0.int_1);
    }

    public static IntPtr smethod_3()
    {
        return smethod_29(StartupClass.int_3);
    }

    public static GStruct22 smethod_4()
    {
        var intptr_2 = smethod_29(StartupClass.int_3);
        var gstruct22_0 = new GStruct22(0, 0, 0, 0);
        if (!GetClientRect(intptr_2, out gstruct22_0))
        {
            GClass37.smethod_0("GetClientRect failed, last error: " + Marshal.GetLastWin32Error());
            StartupClass.smethod_27(false, "GetClientRectBurp");
        }

        var point_0_1 = new Point(gstruct22_0.int_0, gstruct22_0.int_1);
        var point_0_2 = new Point(gstruct22_0.int_2, gstruct22_0.int_3);
        ClientToScreen(intptr_2, ref point_0_1);
        ClientToScreen(intptr_2, ref point_0_2);
        gstruct22_0 = new GStruct22(point_0_1.X, point_0_1.Y, point_0_2.X, point_0_2.Y);
        return gstruct22_0;
    }

    public static void smethod_5(int int_29)
    {
        GClass37.smethod_1("Forgetting app: " + int_29);
        sortedList_0.Add(int_29, "");
    }

    public static IntPtr smethod_6(int int_29)
    {
        var num = !StartupClass.bool_2
            ? !GClass61.gclass61_0.method_5("AllowWriteBytes")
                ? OpenProcess(24U, false, int_29)
                : OpenProcess(1080U, false, int_29)
            : OpenProcess(1048U, false, int_29);
        if (!(num == IntPtr.Zero))
            return num;
        return StartupClass.gclass71_0 != null ? StartupClass.gclass71_0.method_20(int_29) : IntPtr.Zero;
    }

    public static void smethod_7(IntPtr intptr_2)
    {
        CloseHandle(intptr_2);
    }

    public static string smethod_8(byte[] byte_0)
    {
        var stringBuilder = new StringBuilder();
        foreach (var num in byte_0)
            if (num < 16)
                stringBuilder.AppendFormat("0{0:x} ", num);
            else
                stringBuilder.AppendFormat("{0:x} ", num);
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        return stringBuilder.ToString();
    }

    public static string smethod_9(int int_29, int int_30, string string_0)
    {
        return smethod_10(int_29, int_30, string_0);
    }

    public static string smethod_10(int int_29, int int_30, string string_0)
    {
        GStruct21 gstruct21_0;
        if (VirtualQueryEx(StartupClass.intptr_1, int_29, out gstruct21_0, 28) > 0)
        {
            var num = gstruct21_0.int_2 - (int_29 - gstruct21_0.int_0);
            if (num < int_30)
            {
                int_30 = num;
                GClass37.smethod_1("Cutting down maximum read on region end: 0x" + int_30.ToString("x"));
            }
        }

        var bytes = smethod_20(int_29, int_30);
        if (bytes == null)
            return "(read failed)";
        var count = 0;
        while (count < bytes.Length && bytes[count] != 0)
            ++count;
        if (count == bytes.Length)
            count = bytes.Length - 1;
        return new UTF8Encoding().GetString(bytes, 0, count);
    }

    public static int smethod_11(int int_29, string string_0)
    {
        var numArray = smethod_17(int_29, 4, string_0);
        return numArray == null ? 0 : BitConverter.ToInt32(numArray, 0);
    }

    public static long smethod_12(int int_29, string string_0)
    {
        var numArray = smethod_17(int_29, 8, string_0);
        return numArray == null ? 0L : BitConverter.ToInt64(numArray, 0);
    }

    public static float smethod_13(int int_29, string string_0)
    {
        var numArray = smethod_17(int_29, 4, string_0);
        return numArray == null ? 0.0f : BitConverter.ToSingle(numArray, 0);
    }

    public static double smethod_14(int int_29, string string_0)
    {
        var numArray = smethod_17(int_29, 8, string_0);
        return numArray == null ? 0.0 : BitConverter.ToDouble(numArray, 0);
    }

    public static byte smethod_15(int int_29, string string_0)
    {
        var numArray = smethod_17(int_29, 1, string_0);
        return numArray == null ? (byte)0 : numArray[0];
    }

    public static int smethod_16(int int_29, byte[] byte_0, int int_30)
    {
        int int_31;
        return WriteProcessMemory(StartupClass.intptr_1, int_29, byte_0, int_30, out int_31) != 0 ? int_31 : 0;
    }

    public static byte[] smethod_17(int int_29, int int_30, string string_0)
    {
        return smethod_19(int_29, int_30, string_0, false);
    }

    private static int smethod_18(int int_29, byte[] byte_0, int int_30, out int int_31)
    {
        if (StartupClass.bool_14 && StartupClass.gclass71_0 != null)
        {
            var num = StartupClass.gclass71_0.method_41(int_29, byte_0, int_30, out int_31);
            int_27 = num < int_30 ? 299 : 0;
            return num;
        }

        var num1 = ReadProcessMemory(StartupClass.intptr_1, int_29, byte_0, int_30, out int_31);
        if (num1 != 0)
            int_27 = Marshal.GetLastWin32Error();
        return num1;
    }

    public static byte[] smethod_19(int int_29, int int_30, string string_0, bool bool_4)
    {
        var byte_0 = new byte[int_30];
        int int_31;
        if (smethod_18(int_29, byte_0, int_30, out int_31) == 0)
        {
            if (int_27 == 299 && bool_4)
            {
                GClass37.smethod_0("! Partial read @ " + int_29.ToString("x") + " for " + string_0 +
                                   ": expected bytes = " + int_30 + ", got bytes = " + int_31);
                byte_0[int_31] = 0;
            }
            else
            {
                if (!bool_2)
                {
                    bool_2 = true;
                    GClass37.smethod_1(GClass30.smethod_2(712, int_29.ToString("x"), string_0, int_27));
                }

                if (bool_1)
                {
                    if (StartupClass.bool_13)
                        GClass37.smethod_0(string.Format(GClass30.smethod_1(341), int_29, string_0));
                    StartupClass.smethod_27(true, "ReadBytesFail");
                }

                return null;
            }
        }

        return byte_0;
    }

    public static byte[] smethod_20(int int_29, int int_30)
    {
        var byte_0 = new byte[int_30];
        return smethod_18(int_29, byte_0, int_30, out var _) == 0 ? null : byte_0;
    }

    public static int smethod_21(int int_29, string string_0)
    {
        return BitConverter.ToInt32(smethod_20(int_29, 4) ?? throw new GException1(int_29, 4, string_0), 0);
    }

    public static float smethod_22(int int_29, string string_0)
    {
        return BitConverter.ToSingle(smethod_20(int_29, 4) ?? throw new GException1(int_29, 4, string_0), 0);
    }

    public static float smethod_23(int int_29, string string_0)
    {
        return (smethod_20(int_29, 1) ?? throw new GException1(int_29, 1, string_0))[0];
    }

    public static long smethod_24(int int_29, string string_0)
    {
        return BitConverter.ToInt64(smethod_20(int_29, 8) ?? throw new GException1(int_29, 8, string_0), 0);
    }

    [DllImport("kernel32", SetLastError = true)]
    private static extern int ResumeThread(IntPtr intptr_2);

    [DllImport("kernel32", SetLastError = true)]
    private static extern int GetProcAddress(int int_29, string string_0);

    [DllImport("kernel32", SetLastError = true)]
    private static extern int GetModuleHandle(string string_0);

    [DllImport("user32.dll")]
    private static extern bool SetWindowText(IntPtr intptr_2, string string_0);

    [DllImport("user32.dll")]
    private static extern uint RealGetWindowClass(
        IntPtr intptr_2,
        StringBuilder stringBuilder_0,
        int int_29);

    [DllImport("User32.dll")]
    public static extern IntPtr GetDesktopWindow();

    [DllImport("User32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("User32.dll")]
    public static extern bool EnumChildWindows(IntPtr intptr_2, Delegate delegate_0, IntPtr intptr_3);

    [DllImport("User32.dll")]
    public static extern int GetWindowText(
        IntPtr intptr_2,
        StringBuilder stringBuilder_0,
        int int_29);

    [DllImport("User32.dll")]
    public static extern bool ClientToScreen(IntPtr intptr_2, ref Point point_0);

    [DllImport("kernel32.dll")]
    public static extern int VirtualQueryEx(
        IntPtr intptr_2,
        int int_29,
        out GStruct21 gstruct21_0,
        int int_30);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint uint_23, bool bool_4, int int_29);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool TerminateProcess(IntPtr intptr_2, uint uint_23);

    [DllImport("kernel32")]
    private static extern bool CloseHandle(IntPtr intptr_2);

    [DllImport("kernel32", SetLastError = true)]
    public static extern int ReadProcessMemory(
        IntPtr intptr_2,
        int int_29,
        byte[] byte_0,
        int int_30,
        out int int_31);

    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr intptr_2);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern int VirtualProtectEx(
        IntPtr intptr_2,
        int int_29,
        int int_30,
        int int_31,
        out int int_32);

    [DllImport("kernel32", SetLastError = true)]
    private static extern int WriteProcessMemory(
        IntPtr intptr_2,
        int int_29,
        byte[] byte_0,
        int int_30,
        out int int_31);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr intptr_2, out GStruct22 gstruct22_0);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetClientRect(IntPtr intptr_2, out GStruct22 gstruct22_0);

    public static void smethod_25(double double_0, double double_1, out int int_29, out int int_30)
    {
        var gstruct22 = smethod_4();
        int_29 = gstruct22.int_0 + (int)(double_0 * gstruct22.method_1());
        int_30 = gstruct22.int_1 + (int)(double_1 * gstruct22.method_0());
    }

    public static void smethod_26(out double double_0, out double double_1, int int_29, int int_30)
    {
        var gstruct22 = smethod_4();
        double_0 = (int_29 - gstruct22.int_0) / (double)gstruct22.method_1();
        double_1 = (int_30 - gstruct22.int_1) / (double)gstruct22.method_0();
    }

    [DllImport("user32")]
    public static extern int EnumWindows(GDelegate1 gdelegate1_0, IntPtr intptr_2);

    [DllImport("kernel32.dll")]
    public static extern int GetCurrentProcessId();

    [DllImport("user32")]
    public static extern int GetWindowThreadProcessId(IntPtr intptr_2, out int int_29);

    public static IntPtr smethod_27(int int_29)
    {
        var gclass36 = new GClass36(20000);
        gclass36.method_4();
        while (!gclass36.method_3())
        {
            var num = smethod_29(int_29);
            if (num != IntPtr.Zero)
                return num;
            Thread.Sleep(500);
        }

        return new IntPtr(0);
    }

    public static bool smethod_28()
    {
        var procAddress = GetProcAddress(GetModuleHandle("kernel32.dll"), "ReadProcessMemory");
        if (procAddress == 0)
            return true;
        var num = (uint)Marshal.ReadInt32(new IntPtr(procAddress));
        return num == 2337669003U || num == 2381089621U;
    }

    public static IntPtr smethod_29(int int_29)
    {
        if (int_29 == 0)
            int_29 = smethod_1();
        if (int_29 == 0)
            return IntPtr.Zero;
        intptr_0 = IntPtr.Zero;
        int_28 = int_29;
        EnumWindows(smethod_30, IntPtr.Zero);
        return intptr_0;
    }

    private static bool smethod_30(IntPtr intptr_2, IntPtr intptr_3)
    {
        int int_29;
        GetWindowThreadProcessId(intptr_2, out int_29);
        if (int_29 != int_28)
            return true;
        intptr_0 = intptr_2;
        return false;
    }

    public static int smethod_31()
    {
        if (!GClass18.gclass18_0.method_5("Julie") || StartupClass.intptr_1 == IntPtr.Zero || !bool_3)
            return 0;
        var num1 = GClass18.gclass18_0.method_4("DS1");
        var num2 = GClass18.gclass18_0.method_4("DS2");
        var num3 = GClass18.gclass18_0.method_4("Julie");
        var num4 = 0;
        var int_29 = num1 - 4096;
        var tickCount = Environment.TickCount;
        var byte_0 = new byte[4096];
        var num5 = 0;
        while (int_29 < num2)
        {
            ++num5;
            int_29 += 4096;
            int int_31;
            ReadProcessMemory(StartupClass.intptr_1, int_29, byte_0, 4096, out int_31);
            if (4096 == int_31 && int_29 <= num3 && int_29 + int_31 > num3)
            {
                var startIndex = num3 - int_29;
                if (BitConverter.ToInt32(byte_0, startIndex) != 0)
                    num4 = 1;
            }
        }

        if (num4 > 0 && StartupClass.gclass16_0 != null && GClass18.gclass18_0.method_5("AllowFS"))
            StartupClass.gclass16_0.method_6(GClass18.gclass18_0.method_4("JulieDrop"),
                GClass18.gclass18_0.method_4("JulieSize"));
        if (num4 > 0)
            StartupClass.smethod_37(GEnum0.const_1);
        return num4;
    }

    public static string smethod_32()
    {
        var path = StartupClass.string_4 + "wtf\\config.wtf";
        var str1 = "(unknown)";
        try
        {
            var streamReader = File.OpenText(path);
            while (true)
            {
                string str2;
                do
                {
                    str2 = streamReader.ReadLine();
                    if (str2 == null)
                        goto label_5;
                } while (str2.IndexOf("realmName") <= 0);

                var num1 = str2.IndexOf('"');
                var num2 = str2.LastIndexOf('"');
                str1 = str2.Substring(num1 + 1, num2 - num1 - 1);
            }

            label_5:
            streamReader.Close();
        }
        catch (Exception ex)
        {
        }

        return str1;
    }

    public static void smethod_33()
    {
        SetWindowPos(StartupClass.intptr_0, new IntPtr(0), 0, 0, 0, 0, 259U);
    }

    public static int smethod_34()
    {
        var int_29 = smethod_11(GClass18.gclass18_0.method_4("GameTimeType"), "gt1");
        var num1 = smethod_11(int_29 + GClass18.gclass18_0.method_4("GameTimeTypeF1"), "gt2");
        long long_0 = 0;
        if (num1 >= 2)
        {
            int num2 = QueryPerformanceCounter(ref long_0);
        }
        else
        {
            long_0 = Environment.TickCount;
        }

        var num3 = smethod_14(int_29, "gt0");
        var num4 = smethod_14(int_29 + GClass18.gclass18_0.method_4("GameTimeTypeF2"), "gt3");
        return (int)(long_0 * num3 + num4);
    }

    public static bool smethod_35(int int_29)
    {
        GStruct21 gstruct21_0;
        if (VirtualQueryEx(StartupClass.intptr_1, int_29, out gstruct21_0, 28) != 28)
        {
            GClass37.smethod_1("! VirtualQueryEx failed at 0x" + int_29.ToString("x"));
            return false;
        }

        return gstruct21_0.uint_0 == 4U || gstruct21_0.uint_0 == 64U;
    }

    public static int smethod_36(int int_29, int int_30, int int_31)
    {
        int int_32;
        VirtualProtectEx(StartupClass.intptr_1, int_29, int_30, int_31, out int_32);
        return int_32;
    }

    [DllImport("kernel32.dll")]
    private static extern short QueryPerformanceCounter(ref long long_0);

    [DllImport("kernel32.dll")]
    public static extern void Sleep(uint uint_23);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(
        IntPtr intptr_2,
        IntPtr intptr_3,
        int int_29,
        int int_30,
        int int_31,
        int int_32,
        uint uint_23);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr intptr_2, int int_29);

    public static void smethod_37(IntPtr intptr_2)
    {
        ShowWindow(intptr_2, 5);
    }

    public static void smethod_38(IntPtr intptr_2)
    {
        ShowWindow(intptr_2, 0);
    }

    public static bool smethod_39(IntPtr intptr_2, out Point point_0)
    {
        point_0 = new Point();
        point_0.X = 0;
        point_0.Y = 0;
        GStruct22 gstruct22_0;
        if (!GetWindowRect(intptr_2, out gstruct22_0))
            return false;
        point_0.X = gstruct22_0.int_0;
        point_0.Y = gstruct22_0.int_1;
        return true;
    }

    public static bool smethod_40(IntPtr intptr_2, out Size size_0)
    {
        size_0 = new Size();
        size_0.Width = 0;
        size_0.Height = 0;
        GStruct22 gstruct22_0;
        if (!GetWindowRect(intptr_2, out gstruct22_0))
            return false;
        size_0.Width = gstruct22_0.method_1() - 1;
        size_0.Height = gstruct22_0.method_0() - 1;
        return true;
    }

    public static void smethod_41(IntPtr intptr_2, Point point_0)
    {
        SetWindowPos(intptr_2, IntPtr.Zero, point_0.X, point_0.Y, 0, 0, 277U);
    }

    public static void smethod_42(IntPtr intptr_2, Size size_0)
    {
        SetWindowPos(intptr_2, IntPtr.Zero, 0, 0, size_0.Width, size_0.Height, 278U);
    }

    public static void smethod_43(IntPtr intptr_2, Size size_0, Point point_0)
    {
        SetWindowPos(intptr_2, IntPtr.Zero, point_0.X, point_0.Y, size_0.Width, size_0.Height, 276U);
    }

    public static void smethod_44(
        Control control_0,
        string string_0,
        HelpNavigator helpNavigator_0,
        object object_0)
    {
        var url = string_0;
        Help.ShowHelp(control_0, url, helpNavigator_0, object_0);
        smethod_45();
    }

    public static void smethod_45()
    {
        new Thread(smethod_46).Start();
    }

    public static void smethod_46()
    {
        try
        {
            for (var index = 50; index > 0 && !smethod_47(); --index)
                StartupClass.smethod_39(100);
        }
        catch (Exception ex)
        {
            GClass37.smethod_1(GClass30.smethod_2(347, ex.Message));
        }
    }

    public static bool smethod_47()
    {
        var intptr_2 = smethod_49();
        if (!(intptr_2 != IntPtr.Zero))
            return false;
        if (GClass61.gclass61_0.method_2("TitleBarRename") == "True")
        {
            if (GClass61.gclass61_0.method_2("TitleBarRandom") == "True")
                SetWindowText(intptr_2, GClass30.smethod_2(348, smethod_0()));
            else
                SetWindowText(intptr_2, GClass30.smethod_2(713, "TitleBarName"));
        }

        return true;
    }

    public static void smethod_48(Form form_0)
    {
        if (!(GClass61.gclass61_0.method_2("TitleBarRename") == "True") ||
            !(GClass61.gclass61_0.method_2("TitleBarRandom") == "True"))
            return;
        form_0.Text = smethod_0();
    }

    private static IntPtr smethod_49()
    {
        intptr_1 = IntPtr.Zero;
        EnumWindows(smethod_50, IntPtr.Zero);
        return intptr_1;
    }

    private static bool smethod_50(IntPtr intptr_2, IntPtr intptr_3)
    {
        var stringBuilder_0_1 = new StringBuilder(256);
        GetWindowText(intptr_2, stringBuilder_0_1, stringBuilder_0_1.Capacity - 1);
        if (stringBuilder_0_1.Length == 0 || stringBuilder_0_1.ToString().ToLower().IndexOf("glider") <= -1)
            return true;
        var stringBuilder_0_2 = new StringBuilder(256);
        var windowClass = (int)RealGetWindowClass(intptr_2, stringBuilder_0_2, stringBuilder_0_2.Capacity - 1);
        if (!(stringBuilder_0_2.ToString() == "HH Parent"))
            return true;
        intptr_1 = intptr_2;
        return false;
    }

    public static void smethod_51(HelpProvider helpProvider_0)
    {
    }

    [DllImport("kernel32", SetLastError = true)]
    private static extern IntPtr OpenThread(uint uint_23, bool bool_4, uint uint_24);

    [DllImport("ntdll.dll", SetLastError = true)]
    private static extern int NtQueryInformationThread(
        IntPtr intptr_2,
        uint uint_23,
        IntPtr intptr_3,
        uint uint_24,
        out uint uint_25);

    public static bool smethod_52(out long long_0, out int int_29)
    {
        long_0 = 0L;
        int_29 = 0;
        var gclass65 = new GClass65();
        gclass65.method_0();
        var numArray = gclass65.method_4(StartupClass.int_3);
        if (numArray.Length == 0)
            return false;
        var num1 = smethod_11(GClass18.gclass18_0.method_4("TLSSlot"), "TLSSlot");
        foreach (var uint_24 in numArray)
        {
            var intptr_2 = OpenThread(64U, false, uint_24);
            if (intptr_2.ToInt32() > 0)
            {
                var structure = new Class3();
                var num2 = Marshal.AllocHGlobal(80);
                var num3 = NtQueryInformationThread(intptr_2, 0U, num2, (uint)Marshal.SizeOf(structure), out var _);
                Marshal.PtrToStructure(num2, structure);
                Marshal.FreeHGlobal(num2);
                CloseHandle(intptr_2);
                if (num3 == 0)
                {
                    var num4 = smethod_11(smethod_11(structure.int_1 + 44, "TLSOffset") + 4 * num1, "TargetTLSSlot");
                    var num5 = smethod_12(num4 + GClass18.gclass18_0.method_4("TLSPlayerID"), "TLSPlayerID");
                    var num6 = smethod_11(num4 + GClass18.gclass18_0.method_4("TLSMainTable"), "TLSMainTable");
                    if (num5 > 0L)
                    {
                        long_0 = num5;
                        int_29 = num6;
                        break;
                    }
                }
            }
            else
            {
                GClass37.smethod_1("OpenThread failed, last error = " + Marshal.GetLastWin32Error());
                return false;
            }
        }

        return long_0 != 0L;
    }

    public static void smethod_53()
    {
        if (StartupClass.int_3 == 0)
            return;
        StartupClass.bool_30 = false;
        var gclass65 = new GClass65();
        gclass65.method_0();
        var numArray = gclass65.method_4(StartupClass.int_3);
        var intptr_2 = numArray.Length == 1
            ? OpenThread(2U, false, numArray[0])
            : throw new Exception("!! Unexpected number of threads in game: " + numArray.Length);
        if (intptr_2.ToInt32() <= 0)
            throw new Exception("!! Unable to open main thread in game: " + Marshal.GetLastWin32Error());
        ResumeThread(intptr_2);
        CloseHandle(intptr_2);
    }

    public static void smethod_54()
    {
        smethod_55(StartupClass.int_3);
    }

    public static void smethod_55(int int_29)
    {
        var intptr_2 = OpenProcess(1U, false, int_29);
        if (intptr_2.ToInt32() <= 0)
            return;
        TerminateProcess(intptr_2, 0U);
        CloseHandle(intptr_2);
    }

    public static bool smethod_56(int int_29)
    {
        var gclass65 = new GClass65();
        gclass65.method_0();
        return gclass65.method_2(int_29) > 0;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct GStruct21
    {
        [FieldOffset(0)] public int int_0;
        [FieldOffset(4)] public int int_1;
        [FieldOffset(8)] public uint uint_0;
        [FieldOffset(12)] public int int_2;
        [FieldOffset(16)] public uint uint_1;
        [FieldOffset(20)] public uint uint_2;
        [FieldOffset(24)] public uint uint_3;
    }

    [Serializable]
    public struct GStruct22
    {
        public int int_0;
        public int int_1;
        public int int_2;
        public int int_3;

        public GStruct22(int int_4, int int_5, int int_6, int int_7)
        {
            int_0 = int_4;
            int_1 = int_5;
            int_2 = int_6;
            int_3 = int_7;
        }

        [SpecialName]
        public int method_0()
        {
            return int_3 - int_1 + 1;
        }

        [SpecialName]
        public int method_1()
        {
            return int_2 - int_0 + 1;
        }

        [SpecialName]
        public Size method_2()
        {
            return new Size(method_1(), method_0());
        }

        [SpecialName]
        public Point method_3()
        {
            return new Point(int_0, int_1);
        }

        public Rectangle method_4()
        {
            return Rectangle.FromLTRB(int_0, int_1, int_2, int_3);
        }

        public static GStruct22 smethod_0(Rectangle rectangle_0)
        {
            return new GStruct22(rectangle_0.Left, rectangle_0.Top, rectangle_0.Right, rectangle_0.Bottom);
        }

        public override int GetHashCode()
        {
            return int_0 ^ ((int_1 << 13) | (int_1 >> 19)) ^ ((method_1() << 26) | (method_1() >> 6)) ^
                   ((method_0() << 7) | (method_0() >> 25));
        }

        public override string ToString()
        {
            return "L/R=" + int_0 + "/" + int_2 + ", T/B=" + int_1 + "/" + int_3;
        }
        public bool method_5(int int_4, int int_5)
        {
            return int_4 >= int_0 && int_4 < int_2 && int_5 >= int_1 && int_5 < int_3;
        }
    }

    private delegate int Delegate1(IntPtr intptr_0, IntPtr intptr_1);

    [StructLayout(LayoutKind.Sequential)]
    private class Class3
    {
        public int int_0;
        public int int_1;
        public int int_2;
        public int int_3;
        public int int_4;
        public int int_5;
        public int int_6;
    }
}