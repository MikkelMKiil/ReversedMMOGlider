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

    private const uint infiniteWaitTimeout = 2035711;
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
    public const int pgEditProfileCount = 8;
    public const int objectManagerBasePointer = 32;
    public const int initCount = 64;
    public const int knownVersion = 128;
    public const int expectedVersion = 256;
    public const int versionPatchLevel = 512;
    public const int lastAclProcessId = 1024;
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
    private const int cachedGlideRate = 0;
    private const int attachPidOverride = 0;
    private const int killActionNestingCount = 1;
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
    private static readonly SortedList<int, string> Offsets = new SortedList<int, string>();
    private static IntPtr intptr_0;
    private static int int_28;
    private static IntPtr intptr_1;

    public static string GenerateRandomString()
    {
        var num = StartupClass.random_0.Next() % 10 + 8;
        var stringBuilder = new StringBuilder();
        while (stringBuilder.Length < num)
            stringBuilder.Append((char)(StartupClass.random_0.Next() % 26 + 97));
        return stringBuilder.ToString();
    }

    public static string smethod_0()
    {
        return GenerateRandomString();
    }

    public static int LoadProfile()
    {
        return StartupClass.ParseProcessIdFromCommandLine();
    }

    public static GStruct22 GetFileNameFromPath()
    {
        return GetCursorPosition();
    }

    public static int AttachToWowProcess()
    {
        var gclass65 = new ProcessEnumerator();
        gclass65.method_0();
        if (StartupClass.attachPidOverride != 0)
        {
            if (gclass65.method_2(StartupClass.attachPidOverride) == 0)
                return 0;
            StartupClass.AnotherIntegerValue = StartupClass.attachPidOverride;
            StartupClass.MainApplicationHandle = GetGlideRate(StartupClass.AnotherIntegerValue);
            return StartupClass.AnotherIntegerValue;
        }

        if (StartupClass.AdditionalApplicationHandle != IntPtr.Zero)
        {
            if (HandleAutoLogin(StartupClass.AnotherIntegerValue))
                return StartupClass.AnotherIntegerValue;
            CloseHandle(StartupClass.AdditionalApplicationHandle);
            StartupClass.AdditionalApplicationHandle = IntPtr.Zero;
            StartupClass.AnotherIntegerValue = 0;
            StartupClass.IsForegroundEnabled = true;
            StartupClass.IsGliderInitialized = false;
        }

        var str = ConfigManager.gclass61_0.method_2("AttachEXE");
        if (StartupClass.IsAttached)
            str = "Solitaire.exe";
        if (gclass65.method_1(str) == 0)
            return 0;
        var num = 0;
        foreach (var gclass66 in gclass65.gclass66_0)
            if (string.Compare(gclass66.string_0, str, true) == 0 && !Offsets.ContainsKey(gclass66.int_0))
            {
                num = gclass66.int_0;
                break;
            }

        if (num == 0)
            return 0;
        StartupClass.AnotherIntegerValue = num;
        StartupClass.MainApplicationHandle = GetGlideRate(StartupClass.AnotherIntegerValue);
        return StartupClass.AnotherIntegerValue;
    }

    public static Rectangle GetWindowRectangle()
    {
        var gstruct22_0 = new GStruct22();
        GetWindowRect(GetGlideRate(StartupClass.AnotherIntegerValue), out gstruct22_0);
        return new Rectangle(gstruct22_0.int_0, gstruct22_0.int_1, gstruct22_0.int_2 - gstruct22_0.int_0,
            gstruct22_0.int_3 - gstruct22_0.int_1);
    }

    public static IntPtr GetWindowHandle()
    {
        return GetGlideRate(StartupClass.AnotherIntegerValue);
    }

    public static GStruct22 GetCursorPosition()
    {
        var killEventHandle = GetGlideRate(StartupClass.AnotherIntegerValue);
        var gstruct22_0 = new GStruct22(0, 0, 0, 0);
        if (!GetClientRect(killEventHandle, out gstruct22_0))
        {
            Logger.LogMessage("GetClientRect failed, last error: " + Marshal.GetLastWin32Error());
            StartupClass.StopGlide(false, "GetClientRectBurp");
        }

        var point_0_1 = new Point(gstruct22_0.int_0, gstruct22_0.int_1);
        var point_0_2 = new Point(gstruct22_0.int_2, gstruct22_0.int_3);
        ClientToScreen(killEventHandle, ref point_0_1);
        ClientToScreen(killEventHandle, ref point_0_2);
        gstruct22_0 = new GStruct22(point_0_1.X, point_0_1.Y, point_0_2.X, point_0_2.Y);
        return gstruct22_0;
    }

    public static void SetProcessId(int int_29)
    {
        Logger.LoadProfile("Forgetting app: " + int_29);
        Offsets.Add(int_29, "");
    }

    public static IntPtr OpenProcessHandle(int int_29)
    {
        var num = !StartupClass.IsAttached
            ? !ConfigManager.gclass61_0.method_5("AllowWriteBytes")
                ? OpenProcess(24U, false, int_29)
                : OpenProcess(1080U, false, int_29)
            : OpenProcess(1048U, false, int_29);
        if (!(num == IntPtr.Zero))
            return num;
        return StartupClass.GliderManager != null ? StartupClass.GliderManager.method_20(int_29) : IntPtr.Zero;
    }

    public static void CloseProcessHandle(IntPtr killEventHandle)
    {
        CloseHandle(killEventHandle);
    }

    public static string BytesToHexString(byte[] byte_0)
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

    public static string ReadString(int int_29, int int_30, string string_0)
    {
        return RunMainThreadSafe(int_29, int_30, string_0);
    }

    public static string RunMainThreadSafe(int int_29, int int_30, string string_0)
    {
        return ReadStringInternal(int_29, int_30, string_0);
    }

    public static string ReadStringInternal(int int_29, int int_30, string string_0)
    {
        GStruct21 gstruct21_0;
        if (VirtualQueryEx(StartupClass.AdditionalApplicationHandle, int_29, out gstruct21_0, 28) > 0)
        {
            var num = gstruct21_0.int_2 - (int_29 - gstruct21_0.int_0);
            if (num < int_30)
            {
                int_30 = num;
                Logger.LoadProfile("Cutting down maximum read on region end: 0x" + int_30.ToString("x"));
            }
        }

        var bytes = SendInputString(int_29, int_30);
        if (bytes == null)
            return "(read failed)";
        var count = 0;
        while (count < bytes.Length && bytes[count] != 0)
            ++count;
        if (count == bytes.Length)
            count = bytes.Length - 1;
        return new UTF8Encoding().GetString(bytes, 0, count);
    }

    public static int ReadInt32(int int_29, string string_0)
    {
        var numArray = NotifyStatusChange(int_29, 4, string_0);
        return numArray == null ? 0 : BitConverter.ToInt32(numArray, 0);
    }

    public static int RunInitializationFlow(int int_29, string string_0)
    {
        return ReadInt32(int_29, string_0);
    }

    public static long ReadInt64(int int_29, string string_0)
    {
        var numArray = NotifyStatusChange(int_29, 8, string_0);
        return numArray == null ? 0L : BitConverter.ToInt64(numArray, 0);
    }

    public static long IsAttachedToGame(int int_29, string string_0)
    {
        return ReadInt64(int_29, string_0);
    }

    public static float ReadFloat(int int_29, string string_0)
    {
        var numArray = NotifyStatusChange(int_29, 4, string_0);
        return numArray == null ? 0.0f : BitConverter.ToSingle(numArray, 0);
    }

    public static double ReadDouble(int int_29, string string_0)
    {
        var numArray = NotifyStatusChange(int_29, 8, string_0);
        return numArray == null ? 0.0 : BitConverter.ToDouble(numArray, 0);
    }

    public static double ExecuteAttachOrDetach(int int_29, string string_0)
    {
        return ReadDouble(int_29, string_0);
    }

    public static byte ReadByte(int int_29, string string_0)
    {
        var numArray = NotifyStatusChange(int_29, 1, string_0);
        return numArray == null ? (byte)0 : numArray[0];
    }

    public static int WriteBytes(int int_29, byte[] byte_0, int int_30)
    {
        int int_31;
        return WriteProcessMemory(StartupClass.AdditionalApplicationHandle, int_29, byte_0, int_30, out int_31) != 0 ? int_31 : 0;
    }

    public static byte[] NotifyStatusChange(int int_29, int int_30, string string_0)
    {
        return IsNumericString(int_29, int_30, string_0, false);
    }

    public static byte[] ReadBytes(int int_29, int int_30, string string_0)
    {
        return NotifyStatusChange(int_29, int_30, string_0);
    }

    public static byte[] ReadBytes(int int_29, int int_30, string string_0, bool bool_4)
    {
        return IsNumericString(int_29, int_30, string_0, bool_4);
    }

    private static int ReadProcessMemoryInternal(int int_29, byte[] byte_0, int int_30, out int int_31)
    {
        if (StartupClass.IsOpenMemoryModel && StartupClass.GliderManager != null)
        {
            var num = StartupClass.GliderManager.method_41(int_29, byte_0, int_30, out int_31);
            int_27 = num < int_30 ? 299 : 0;
            return num;
        }

        var num1 = ReadProcessMemory(StartupClass.AdditionalApplicationHandle, int_29, byte_0, int_30, out int_31);
        if (num1 != 0)
            int_27 = Marshal.GetLastWin32Error();
        return num1;
    }

    private static int ParseProcessIdFromCommandLine(int int_29, byte[] byte_0, int int_30, out int int_31)
    {
        return ReadProcessMemoryInternal(int_29, byte_0, int_30, out int_31);
    }

    public static byte[] IsNumericString(int int_29, int int_30, string string_0, bool bool_4)
    {
        var byte_0 = new byte[int_30];
        int int_31;
        if (ParseProcessIdFromCommandLine(int_29, byte_0, int_30, out int_31) == 0)
        {
            if (int_27 == 299 && bool_4)
            {
                Logger.LogMessage("! Partial read @ " + int_29.ToString("x") + " for " + string_0 +
                                   ": expected bytes = " + int_30 + ", got bytes = " + int_31);
                byte_0[int_31] = 0;
            }
            else
            {
                if (!bool_2)
                {
                    bool_2 = true;
                    Logger.LoadProfile(MessageProvider.IsGroupProfile(712, int_29.ToString("x"), string_0, int_27));
                }

                if (bool_1)
                {
                    if (StartupClass.IsGameProcessAttached)
                        Logger.LogMessage(string.Format(MessageProvider.GetMessage(341), int_29, string_0));
                    StartupClass.StopGlide(true, "ReadBytesFail");
                }

                return null;
            }
        }

        return byte_0;
    }

    public static byte[] SendInputString(int int_29, int int_30)
    {
        var byte_0 = new byte[int_30];
        return ParseProcessIdFromCommandLine(int_29, byte_0, int_30, out var _) == 0 ? null : byte_0;
    }

    public static byte[] ReadBytesRaw(int int_29, int int_30)
    {
        return SendInputString(int_29, int_30);
    }

    public static int ReadIntFromOffset(int int_29, string string_0)
    {
        var bytes = SendInputString(int_29, 4);
        return bytes == null ? 0 : BitConverter.ToInt32(bytes, 0);
    }

    public static float ReadFloatFromOffset(int int_29, string string_0)
    {
        var bytes = SendInputString(int_29, 4);
        return bytes == null ? 0f : BitConverter.ToSingle(bytes, 0);
    }

    public static float ReadFloatAlternate(int int_29, string string_0)
    {
        var bytes = SendInputString(int_29, 1);
        return bytes == null ? 0f : (float)bytes[0];
    }

    public static long ReadLongFromOffset(int int_29, string string_0)
    {
        var bytes = SendInputString(int_29, 8);
        return bytes == null ? 0L : BitConverter.ToInt64(bytes, 0);
    }

    [DllImport("kernel32", SetLastError = true)]
    private static extern int ResumeThread(IntPtr killEventHandle);

    [DllImport("kernel32", SetLastError = true)]
    private static extern int GetProcAddress(int int_29, string string_0);

    [DllImport("kernel32", SetLastError = true)]
    private static extern int GetModuleHandle(string string_0);

    [DllImport("user32.dll")]
    private static extern bool SetWindowText(IntPtr killEventHandle, string string_0);

    [DllImport("user32.dll")]
    private static extern uint RealGetWindowClass(
        IntPtr killEventHandle,
        StringBuilder stringBuilder_0,
        int int_29);

    [DllImport("User32.dll")]
    public static extern IntPtr GetDesktopWindow();

    [DllImport("User32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("User32.dll")]
    public static extern bool EnumChildWindows(IntPtr killEventHandle, Delegate delegate_0, IntPtr intptr_3);

    [DllImport("User32.dll")]
    public static extern int GetWindowText(
        IntPtr killEventHandle,
        StringBuilder stringBuilder_0,
        int int_29);

    [DllImport("User32.dll")]
    public static extern bool ClientToScreen(IntPtr killEventHandle, ref Point point_0);

    [DllImport("kernel32.dll")]
    public static extern int VirtualQueryEx(
        IntPtr killEventHandle,
        int int_29,
        out GStruct21 gstruct21_0,
        int int_30);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint uint_23, bool bool_4, int int_29);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool TerminateProcess(IntPtr killEventHandle, uint uint_23);

    [DllImport("kernel32")]
    private static extern bool CloseHandle(IntPtr killEventHandle);

    [DllImport("kernel32", SetLastError = true)]
    public static extern int ReadProcessMemory(
        IntPtr killEventHandle,
        int int_29,
        byte[] byte_0,
        int int_30,
        out int int_31);

    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr killEventHandle);

    [DllImport("kernel32", SetLastError = true)]
    private static extern int VirtualProtectEx(
        IntPtr killEventHandle,
        int int_29,
        int int_30,
        int int_31,
        out int int_32);

    [DllImport("kernel32", SetLastError = true)]
    private static extern int WriteProcessMemory(
        IntPtr killEventHandle,
        int int_29,
        byte[] byte_0,
        int int_30,
        out int int_31);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr killEventHandle, out GStruct22 gstruct22_0);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetClientRect(IntPtr killEventHandle, out GStruct22 gstruct22_0);

    public static void WorldToScreen(double autoAddDistance, double double_1, out int int_29, out int int_30)
    {
        var gstruct22 = GetFileNameFromPath();
        int_29 = gstruct22.int_0 + (int)(autoAddDistance * gstruct22.method_1());
        int_30 = gstruct22.int_1 + (int)(double_1 * gstruct22.method_0());
    }

    public static void ScreenToWorld(out double autoAddDistance, out double double_1, int int_29, int int_30)
    {
        var gstruct22 = GetFileNameFromPath();
        autoAddDistance = (int_29 - gstruct22.int_0) / (double)gstruct22.method_1();
        double_1 = (int_30 - gstruct22.int_1) / (double)gstruct22.method_0();
    }

    [DllImport("user32")]
    public static extern int EnumWindows(GDelegate1 gdelegate1_0, IntPtr killEventHandle);

    [DllImport("kernel32.dll")]
    public static extern int GetCurrentProcessId();

    [DllImport("user32")]
    public static extern int GetWindowThreadProcessId(IntPtr killEventHandle, out int int_29);

    public static IntPtr GetMainWindowHandle(int int_29)
    {
        var gclass36 = new GameTimer(20000);
        gclass36.method_4();
        while (!gclass36.method_3())
        {
            var num = GetGlideRate(int_29);
            if (num != IntPtr.Zero)
                return num;
            Thread.Sleep(500);
        }

        return new IntPtr(0);
    }

    public static IntPtr GetGlideRate(int int_29)
    {
        return OpenProcessWithAccess(int_29);
    }

    public static bool IsWowProcessRunning()
    {
        var procAddress = GetProcAddress(GetModuleHandle("kernel32.dll"), "ReadProcessMemory");
        if (procAddress == 0)
            return true;
        var num = (uint)Marshal.ReadInt32(new IntPtr(procAddress));
        return num == 2337669003U || num == 2381089621U;
    }

    public static IntPtr OpenProcessWithAccess(int int_29)
    {
        if (int_29 == 0)
            int_29 = LoadProfile();
        if (int_29 == 0)
            return IntPtr.Zero;
        intptr_0 = IntPtr.Zero;
        int_28 = int_29;
        EnumWindows(SetupKillEventListener, IntPtr.Zero);
        return intptr_0;
    }

    private static bool SetupKillEventListener(IntPtr killEventHandle, IntPtr intptr_3)
    {
        int int_29;
        GetWindowThreadProcessId(killEventHandle, out int_29);
        if (int_29 != int_28)
            return true;
        intptr_0 = killEventHandle;
        return false;
    }

    public static int GetProcessId()
    {
        if (!MemoryOffsetTable.Instance.HasOffset("Julie") || StartupClass.AdditionalApplicationHandle == IntPtr.Zero || !bool_3)
            return 0;
        var num1 = MemoryOffsetTable.Instance.GetIntOffset("DS1");
        var num2 = MemoryOffsetTable.Instance.GetIntOffset("DS2");
        var num3 = MemoryOffsetTable.Instance.GetIntOffset("Julie");
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
            ReadProcessMemory(StartupClass.AdditionalApplicationHandle, int_29, byte_0, 4096, out int_31);
            if (4096 == int_31 && int_29 <= num3 && int_29 + int_31 > num3)
            {
                var startIndex = num3 - int_29;
                if (BitConverter.ToInt32(byte_0, startIndex) != 0)
                    num4 = 1;
            }
        }

        if (num4 > 0 && StartupClass.GameMemoryReader != null && MemoryOffsetTable.Instance.HasOffset("AllowFS"))
            StartupClass.GameMemoryReader.method_6(MemoryOffsetTable.Instance.GetIntOffset("JulieDrop"),
                MemoryOffsetTable.Instance.GetIntOffset("JulieSize"));
        if (num4 > 0)
            StartupClass.HandleWardenCheckResult(WardenCheckStatus.const_1);
        return num4;
    }

    public static string GetProcessExecutablePath()
    {
        var path = StartupClass.SomeStringData + "wtf\\config.wtf";
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
        catch (Exception)
        {
        }

        return str1;
    }

    public static void CloseCurrentProcessHandle()
    {
        SetWindowPos(StartupClass.MainApplicationHandle, new IntPtr(0), 0, 0, 0, 0, 259U);
    }

    public static int GetProcessIdFromWindow()
    {
        var int_29 = RunInitializationFlow(MemoryOffsetTable.Instance.GetIntOffset("GameTimeType"), "gt1");
        var num1 = RunInitializationFlow(int_29 + MemoryOffsetTable.Instance.GetIntOffset("GameTimeTypeF1"), "gt2");
        long playerGuid = 0;
        if (num1 >= 2)
        {
            int num2 = QueryPerformanceCounter(ref playerGuid);
        }
        else
        {
            playerGuid = Environment.TickCount;
        }

        var num3 = ExecuteAttachOrDetach(int_29, "gt0");
        var num4 = ExecuteAttachOrDetach(int_29 + MemoryOffsetTable.Instance.GetIntOffset("GameTimeTypeF2"), "gt3");
        return (int)(playerGuid * num3 + num4);
    }

    public static bool IsMemoryReadable(int int_29)
    {
        GStruct21 gstruct21_0;
        if (VirtualQueryEx(StartupClass.AdditionalApplicationHandle, int_29, out gstruct21_0, 28) != 28)
        {
            Logger.LoadProfile("! VirtualQueryEx failed at 0x" + int_29.ToString("x"));
            return false;
        }

        return gstruct21_0.infiniteWaitTimeout == 4U || gstruct21_0.infiniteWaitTimeout == 64U;
    }

    public static int ReadPointerChain(int int_29, int int_30, int int_31)
    {
        int int_32;
        VirtualProtectEx(StartupClass.AdditionalApplicationHandle, int_29, int_30, int_31, out int_32);
        return int_32;
    }

    [DllImport("kernel32.dll")]
    private static extern short QueryPerformanceCounter(ref long playerGuid);

    [DllImport("kernel32.dll")]
    public static extern void Sleep(uint uint_23);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(
        IntPtr killEventHandle,
        IntPtr intptr_3,
        int int_29,
        int int_30,
        int int_31,
        int int_32,
        uint uint_23);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr killEventHandle, int int_29);

    public static void ShowWindow(IntPtr killEventHandle)
    {
        ShowWindow(killEventHandle, 5);
    }

    public static void HideWindow(IntPtr killEventHandle)
    {
        ShowWindow(killEventHandle, 0);
    }

    //public static void SetForegroundWindow(IntPtr killEventHandle)
    //{
    //    ShowWindow(killEventHandle, 0);
    //}

    public static bool GetWindowPosition(IntPtr killEventHandle, out Point point_0)
    {
        point_0 = new Point();
        point_0.X = 0;
        point_0.Y = 0;
        GStruct22 gstruct22_0;
        if (!GetWindowRect(killEventHandle, out gstruct22_0))
            return false;
        point_0.X = gstruct22_0.int_0;
        point_0.Y = gstruct22_0.int_1;
        return true;
    }

    public static bool GetWindowSize(IntPtr killEventHandle, out Size originalWindowSize)
    {
        originalWindowSize = new Size();
        originalWindowSize.Width = 0;
        originalWindowSize.Height = 0;
        GStruct22 gstruct22_0;
        if (!GetWindowRect(killEventHandle, out gstruct22_0))
            return false;
        originalWindowSize.Width = gstruct22_0.method_1() - 1;
        originalWindowSize.Height = gstruct22_0.method_0() - 1;
        return true;
    }

    public static void SetWindowPosition(IntPtr killEventHandle, Point point_0)
    {
        SetWindowPos(killEventHandle, IntPtr.Zero, point_0.X, point_0.Y, 0, 0, 277U);
    }

    public static void SetWindowSize(IntPtr killEventHandle, Size originalWindowSize)
    {
        SetWindowPos(killEventHandle, IntPtr.Zero, 0, 0, originalWindowSize.Width, originalWindowSize.Height, 278U);
    }

    public static void GetForegroundWindow(IntPtr killEventHandle, Size originalWindowSize, Point point_0)
    {
        SetWindowPos(killEventHandle, IntPtr.Zero, point_0.X, point_0.Y, originalWindowSize.Width, originalWindowSize.Height, 276U);
    }

    public static void IsWindowVisible(
        Control control_0,
        string string_0,
        HelpNavigator helpNavigator_0,
        object object_0)
    {
        var url = string_0;
        Help.ShowHelp(control_0, url, helpNavigator_0, object_0);
        TryAutoAttach();
    }

    private static void TryAutoAttach()
    {
        StartupClass.TryAutoAttach();
    }

    public static void IsWindowMinimized()
    {
        new Thread(HandleBackgroundDisplay).Start();
    }

    public static void HandleBackgroundDisplay()
    {
        try
        {
            for (var index = 50; index > 0 && !HideGameWindow(); --index)
                StartupClass.Sleep(100);
        }
        catch (Exception ex)
        {
            Logger.LoadProfile(MessageProvider.IsGroupProfile(347, ex.Message));
        }
    }

    public static bool HideGameWindow()
    {
        var killEventHandle = RestoreHiddenWindow();
        if (!(killEventHandle != IntPtr.Zero))
            return false;
        if (ConfigManager.gclass61_0.method_2("TitleBarRename") == "True")
        {
            if (ConfigManager.gclass61_0.method_2("TitleBarRandom") == "True")
                SetWindowText(killEventHandle, MessageProvider.IsGroupProfile(348, smethod_0()));
            else
                SetWindowText(killEventHandle, MessageProvider.IsGroupProfile(713, "TitleBarName"));
        }

        return true;
    }

    public static void ShrinkGameWindow(Form form_0)
    {
        if (!(ConfigManager.gclass61_0.method_2("TitleBarRename") == "True") ||
            !(ConfigManager.gclass61_0.method_2("TitleBarRandom") == "True"))
            return;
        form_0.Text = smethod_0();
    }

    private static IntPtr RestoreHiddenWindow()
    {
        intptr_1 = IntPtr.Zero;
        EnumWindows(RestoreShrunkWindow, IntPtr.Zero);
        return intptr_1;
    }

    private static bool RestoreShrunkWindow(IntPtr killEventHandle, IntPtr intptr_3)
    {
        var stringBuilder_0_1 = new StringBuilder(256);
        GetWindowText(killEventHandle, stringBuilder_0_1, stringBuilder_0_1.Capacity - 1);
        if (stringBuilder_0_1.Length == 0 || stringBuilder_0_1.ToString().ToLower().IndexOf("glider") <= -1)
            return true;
        var stringBuilder_0_2 = new StringBuilder(256);
        var windowClass = (int)RealGetWindowClass(killEventHandle, stringBuilder_0_2, stringBuilder_0_2.Capacity - 1);
        if (!(stringBuilder_0_2.ToString() == "HH Parent"))
            return true;
        intptr_1 = killEventHandle;
        return false;
    }

    public static void RestoreGameWindow(HelpProvider helpProvider_0)
    {
    }

    [DllImport("kernel32", SetLastError = true)]
    private static extern IntPtr OpenThread(uint uint_23, bool bool_4, uint uint_24);

    [DllImport("ntdll.dll", SetLastError = true)]
    private static extern int NtQueryInformationThread(
        IntPtr killEventHandle,
        uint uint_23,
        IntPtr intptr_3,
        uint uint_24,
        out uint uint_25);

    public static bool ApplyLnCommandLineArg(out long playerGuid, out int int_29)
    {
        playerGuid = 0L;
        int_29 = 0;
        var gclass65 = new ProcessEnumerator();
        gclass65.method_0();
        var numArray = gclass65.method_4(StartupClass.AnotherIntegerValue);
        if (numArray.Length == 0)
            return false;
        var num1 = RunInitializationFlow(MemoryOffsetTable.Instance.GetIntOffset("TLSSlot"), "TLSSlot");
        foreach (var uint_24 in numArray)
        {
            var killEventHandle = OpenThread(64U, false, uint_24);
            if (killEventHandle.ToInt32() > 0)
            {
                var structure = new Class3();
                var num2 = Marshal.AllocHGlobal(80);
                var num3 = NtQueryInformationThread(killEventHandle, 0U, num2, (uint)Marshal.SizeOf(structure), out var _);
                Marshal.PtrToStructure(num2, structure);
                Marshal.FreeHGlobal(num2);
                CloseHandle(killEventHandle);
                if (num3 == 0)
                {
                    var num4 = RunInitializationFlow(RunInitializationFlow(structure.int_1 + 44, "TLSOffset") + 4 * num1, "TargetTLSSlot");
                    var num5 = IsAttachedToGame(num4 + MemoryOffsetTable.Instance.GetIntOffset("TLSPlayerID"), "TLSPlayerID");
                    var num6 = RunInitializationFlow(num4 + MemoryOffsetTable.Instance.GetIntOffset("TLSMainTable"), "TLSMainTable");
                    if (num5 > 0L)
                    {
                        playerGuid = num5;
                        int_29 = num6;
                        break;
                    }
                }
            }
            else
            {
                Logger.LoadProfile("OpenThread failed, last error = " + Marshal.GetLastWin32Error());
                return false;
            }
        }

        return playerGuid != 0L;
    }

    public static void InitializeDriverAndPid()
    {
        if (StartupClass.AnotherIntegerValue == 0)
            return;
        StartupClass.IsResumeMode = false;
        var gclass65 = new ProcessEnumerator();
        gclass65.method_0();
        var numArray = gclass65.method_4(StartupClass.AnotherIntegerValue);
        var killEventHandle = numArray.Length == 1
            ? OpenThread(2U, false, numArray[0])
            : throw new Exception("!! Unexpected number of threads in game: " + numArray.Length);
        if (killEventHandle.ToInt32() <= 0)
            throw new Exception("!! Unable to open main thread in game: " + Marshal.GetLastWin32Error());
        ResumeThread(killEventHandle);
        CloseHandle(killEventHandle);
    }

    public static void WarnIfForceVersionSet()
    {
        WarnIfTripwireDisabled(StartupClass.AnotherIntegerValue);
    }

    public static void WarnIfTripwireDisabled(int int_29)
    {
        var killEventHandle = OpenProcess(1U, false, int_29);
        if (killEventHandle.ToInt32() <= 0)
            return;
        TerminateProcess(killEventHandle, 0U);
        CloseHandle(killEventHandle);
    }

    public static bool HandleAutoLogin(int int_29)
    {
        var gclass65 = new ProcessEnumerator();
        gclass65.method_0();
        return gclass65.method_2(int_29) > 0;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct GStruct21
    {
        [FieldOffset(0)] public int int_0;
        [FieldOffset(4)] public int int_1;
        [FieldOffset(8)] public uint infiniteWaitTimeout;
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

        public GStruct22(int pgEditProfileCount, int objectManagerBasePointer, int initCount, int knownVersion)
        {
            int_0 = pgEditProfileCount;
            int_1 = objectManagerBasePointer;
            int_2 = initCount;
            int_3 = knownVersion;
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

        public static GStruct22 GenerateRandomString(Rectangle rectangle_0)
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
        public bool method_5(int pgEditProfileCount, int objectManagerBasePointer)
        {
            return pgEditProfileCount >= int_0 && pgEditProfileCount < int_2 && objectManagerBasePointer >= int_1 && objectManagerBasePointer < int_3;
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
        public int pgEditProfileCount;
        public int objectManagerBasePointer;
        public int initCount;
    }

}
