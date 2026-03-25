// Decompiled with JetBrains decompiler
// Type: InputController
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common.Objects;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

public class InputController
{
    public enum GEnum8 : ushort
    {
        const_4 = 8,
        const_5 = 9,
        const_6 = 13, // 0x000D
        const_0 = 16, // 0x0010
        const_1 = 17, // 0x0011
        const_2 = 18, // 0x0012
        const_3 = 27, // 0x001B
        const_7 = 33, // 0x0021
        const_8 = 34, // 0x0022
        const_9 = 35, // 0x0023
        const_10 = 36, // 0x0024
        const_11 = 37, // 0x0025
        const_12 = 38, // 0x0026
        const_13 = 39, // 0x0027
        const_14 = 40, // 0x0028
        const_15 = 41, // 0x0029
        const_16 = 42, // 0x002A
        const_17 = 43, // 0x002B
        const_18 = 44, // 0x002C
        const_19 = 45, // 0x002D
        const_20 = 46, // 0x002E
        const_21 = 47, // 0x002F
        const_61 = 91, // 0x005B
        const_62 = 92, // 0x005C
        const_22 = 96, // 0x0060
        const_23 = 97, // 0x0061
        const_24 = 98, // 0x0062
        const_25 = 99, // 0x0063
        const_26 = 100, // 0x0064
        const_27 = 101, // 0x0065
        const_28 = 102, // 0x0066
        const_29 = 103, // 0x0067
        const_30 = 104, // 0x0068
        const_31 = 105, // 0x0069
        const_32 = 106, // 0x006A
        const_33 = 107, // 0x006B
        const_34 = 108, // 0x006C
        const_35 = 109, // 0x006D
        const_36 = 110, // 0x006E
        const_37 = 111, // 0x006F
        const_38 = 112, // 0x0070
        const_39 = 113, // 0x0071
        const_40 = 114, // 0x0072
        const_41 = 115, // 0x0073
        const_42 = 116, // 0x0074
        const_43 = 117, // 0x0075
        const_44 = 118, // 0x0076
        const_45 = 119, // 0x0077
        const_46 = 120, // 0x0078
        const_47 = 121, // 0x0079
        const_48 = 122, // 0x007A
        const_49 = 123, // 0x007B
        const_57 = 176, // 0x00B0
        const_58 = 177, // 0x00B1
        const_59 = 178, // 0x00B2
        const_60 = 179, // 0x00B3
        const_50 = 186, // 0x00BA
        const_51 = 187, // 0x00BB
        const_52 = 188, // 0x00BC
        const_53 = 189, // 0x00BD
        const_54 = 190, // 0x00BE
        const_55 = 191, // 0x00BF
        const_56 = 192 // 0x00C0
    }

    private const uint infiniteWaitTimeout = 256;
    private const uint uint_1 = 257;
    private const uint uint_2 = 258;
    private const int int_0 = 512;
    private const int int_1 = 513;
    private const int int_2 = 514;
    private const int int_3 = 516;
    private const int pgEditProfileCount = 517;
    private const int objectManagerBasePointer = 1;
    private const int initCount = 2;
    public const int knownVersion = 0;
    public const int expectedVersion = 1;
    public const int versionPatchLevel = 2;
    public const int lastAclProcessId = 1;
    public const int cachedGlideRate = 2;
    public const int attachPidOverride = 8;
    public const int killActionNestingCount = 1;
    public const int int_14 = 32768;
    public const int int_15 = 2;
    public const int int_16 = 4;
    public const int int_17 = 8;
    public const int int_18 = 16;
    private const uint uint_3 = 1;
    private const uint uint_4 = 3221225473;
    public static int int_19;
    public static int int_20;
    public static bool bool_0;
    public static bool bool_1 = true;
    public static bool bool_2 = true;
    private static readonly GSpellTimer gspellTimer_0 = new GSpellTimer(1000);
    private static Dictionary<short, short> dictionary_0;
    private static readonly GameTimer licenseCheckTimer = new GameTimer(11000);
    private static int int_21;
    private static int int_22;
    public static string string_0 = "69.44.61.101";
    public static double autoAddDistance;
    public static double double_1;
    public static int int_23;
    public static int int_24;

    [DllImport("user32.dll")]
    private static extern bool ScreenToClient(IntPtr intptr_0, ref GStruct12 gstruct12_0);

    [DllImport("kernel32.dll")]
    private static extern int GetCurrentThreadId();

    [DllImport("user32.dll")]
    private static extern bool AttachThreadInput(int int_25, int int_26, bool bool_3);

    [DllImport("user32.dll")]
    private static extern IntPtr PostMessage(IntPtr intptr_0, uint uint_5, uint uint_6, uint uint_7);

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr intptr_0, uint uint_5, uint uint_6, uint uint_7);

    [DllImport("user32.dll")]
    public static extern int GetWindowThreadProcessId(IntPtr intptr_0, out int int_25);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out GStruct12 gstruct12_0);

    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int int_25, int int_26);

    [DllImport("user32.dll")]
    public static extern int SendInput(uint uint_5, ref GStruct13 gstruct13_0, int int_25);

    [DllImport("user32.dll")]
    public static extern short VkKeyScan(char char_0);

    public static int smethod_0(short short_0, bool bool_3)
    {
        return LoadProfile(short_0, bool_3, true);
    }

    private static int LoadProfile(short short_0, bool bool_3, bool bool_4)
    {
        if (bool_4)
            lock (dictionary_0)
            {
                if (bool_3 && !dictionary_0.ContainsKey(short_0))
                    dictionary_0.Add(short_0, short_0);
                else
                    dictionary_0.Remove(short_0);
            }

        if (StartupClass.IsGliderInitialized)
        {
            var uint_5 = bool_3 ? 256U : 257U;
            var uint_7 = bool_3 ? 1U : 3221225473U;
            PostMessage(StartupClass.MainApplicationHandle, uint_5, (uint)short_0, uint_7);
            if (licenseCheckTimer.method_3())
            {
                IsGroupProfile();
                licenseCheckTimer.method_4();
            }

            return 1;
        }

        var gstruct13_0 = new GStruct13();
        gstruct13_0.int_0 = 1;
        gstruct13_0.gstruct14_0.short_1 = 0;
        gstruct13_0.gstruct14_0.int_1 = 0;
        gstruct13_0.gstruct14_0.int_0 = 0;
        gstruct13_0.gstruct14_0.int_2 = !KeyboardHookManager.bool_0 ? 0 : 102;
        gstruct13_0.gstruct14_0.short_0 = short_0;
        if (!bool_3)
            gstruct13_0.gstruct14_0.int_0 |= 2;
        var num = SendInput(1U, ref gstruct13_0, Marshal.SizeOf(gstruct13_0));
        if (int_20 > 0)
            StartupClass.Sleep(int_20);
        return num;
    }

    private static void IsGroupProfile()
    {
        var gstruct13_0 = new GStruct13();
        gstruct13_0.int_0 = 1;
        gstruct13_0.gstruct14_0.short_1 = 0;
        gstruct13_0.gstruct14_0.int_1 = 0;
        gstruct13_0.gstruct14_0.int_0 = 0;
        gstruct13_0.gstruct14_0.short_0 = 0;
        SendInput(1U, ref gstruct13_0, Marshal.SizeOf(gstruct13_0));
        Thread.Sleep(200);
        gstruct13_0.gstruct14_0.int_0 |= 2;
        SendInput(1U, ref gstruct13_0, Marshal.SizeOf(gstruct13_0));
    }

    public static void LoadSingleProfile(char char_0)
    {
        smethod_0(17, true);
        ParseDouble(char_0);
        smethod_0(17, false);
    }

    public static void GetFileNameFromPath(char char_0, bool bool_3)
    {
        smethod_0((byte)((uint)VkKeyScan(char_0) & byte.MaxValue), bool_3);
    }

    public static void ApplyConfig(out int int_25, out int int_26)
    {
        GStruct12 gstruct12_0;
        GetCursorPos(out gstruct12_0);
        int_25 = gstruct12_0.int_0;
        int_26 = gstruct12_0.int_1;
    }

    public static void ParseDouble(char char_0)
    {
        var num = VkKeyScan(char_0);
        var short_0 = (byte)((uint)num & byte.MaxValue);
        if (((byte)((num & 65280) >> 8) & 1) == 1)
            IsAttachedToGame(short_0);
        else
            StartMainThread(short_0);
    }

    public static void ResolveWowVersion(char char_0)
    {
        var num = VkKeyScan(char_0);
        var short_0 = (byte)((uint)num & byte.MaxValue);
        if (((byte)((num & 65280) >> 8) & 1) == 1)
            RunInitializationFlow(short_0, char_0);
        else
            StartMainThread(short_0);
    }

    public static void SelectActiveGameClass(char char_0)
    {
        IsAttachedToGame((byte)((uint)VkKeyScan(char_0) & byte.MaxValue));
    }

    public static void StartMainThread(short short_0)
    {
        smethod_0(short_0, true);
        StartupClass.Sleep(20);
        smethod_0(short_0, false);
    }

    public static void RunMainThreadSafe(short short_0)
    {
        smethod_0(17, true);
        StartMainThread(short_0);
        smethod_0(17, false);
    }

    public static void RunInitializationFlow(short short_0, char char_0)
    {
        if (StartupClass.IsGliderInitialized)
        {
            smethod_0(16, true);
            PostMessage(StartupClass.MainApplicationHandle, 258U, char_0, 0U);
            smethod_0(16, false);
        }
        else
        {
            smethod_0(16, true);
            StartMainThread(short_0);
            smethod_0(16, false);
        }
    }

    public static void IsAttachedToGame(short short_0)
    {
        smethod_0(16, true);
        StartMainThread(short_0);
        smethod_0(16, false);
    }

    public static void TryAttach(short short_0)
    {
        smethod_0(18, true);
        StartMainThread(short_0);
        smethod_0(18, false);
    }

    public static void ExecuteAttachOrDetach(char char_0)
    {
        smethod_0(18, true);
        ParseDouble(char_0);
        smethod_0(18, false);
    }

    public static void Detach(out int int_25, out int int_26)
    {
        StartupClass.GliderManager.method_36(out int_21, out int_22);
        int_25 = int_21;
        int_26 = int_22;
    }

    public static void ApplyAclForProcess(int int_25, int int_26)
    {
        lock (gspellTimer_0)
        {
            var tickCount1 = Environment.TickCount;
            int int_25_1;
            int int_26_1;
            Detach(out int_25_1, out int_26_1);
            var int_25_2 = int_25_1 + int_25;
            int_26_1 += int_26;
            int_23 += Environment.TickCount - tickCount1;
            var tickCount2 = Environment.TickCount;
            NotifyStatusChange(int_25_2, int_26_1);
            int_24 += Environment.TickCount - tickCount2;
            SendInputString();
        }
    }

    public static void NotifyStatusChange(int int_25, int int_26)
    {
        lock (gspellTimer_0)
        {
            if (!StartupClass.IsAttached)
                SetCursorPos(int_25 + 5000, int_26 + 5000);
            int_21 = int_25;
            int_22 = int_26;
            GStruct12 gstruct12_0;
            gstruct12_0.int_0 = int_25;
            gstruct12_0.int_1 = int_26;
            ScreenToClient(StartupClass.MainApplicationHandle, ref gstruct12_0);
            var windowThreadProcessId = GetWindowThreadProcessId(StartupClass.MainApplicationHandle, out var _);
            var currentThreadId = GetCurrentThreadId();
            AttachThreadInput(currentThreadId, windowThreadProcessId, true);
            SendMessage(StartupClass.MainApplicationHandle, 512U, 0U, WaitForKillEvent((uint)gstruct12_0.int_0, (uint)gstruct12_0.int_1));
            AttachThreadInput(currentThreadId, windowThreadProcessId, false);
            SendInputString();
            if (!StartupClass.IsAttached)
                return;
            Thread.Sleep(100);
        }
    }

    public static void ParseProcessIdFromCommandLine(double double_2, double double_3)
    {
        int int_29;
        int int_30;
        GProcessMemoryManipulator.WorldToScreen(double_2, double_3, out int_29, out int_30);
        if (StartupClass.IsGliderInitialized && !ConfigManager.gclass61_0.method_5("NoCursorHook"))
        {
            autoAddDistance = double_2;
            double_1 = double_3;
        }

        IsNumericString(int_29, int_30);
    }

    public static void IsNumericString(int int_25, int int_26)
    {
        if (StartupClass.IsGliderInitialized && !ConfigManager.gclass61_0.method_5("NoCursorHook"))
            NotifyStatusChange(int_25, int_26);
        else
            SetCursorPos(int_25, int_26);
    }

    private static void SendInputString()
    {
        lock (gspellTimer_0)
        {
            Logger.LoadProfile("#- Grabbing cursor!");
            bool_2 = false;
            bool_1 = false;
            gspellTimer_0.Reset();
        }
    }

    public static void StartManualGlide(bool bool_3)
    {
        lock (gspellTimer_0)
        {
            if (bool_1 || (bool_3 && !bool_2))
                return;
            bool_2 = true;
            if (!StartupClass.IsGliderInitialized || ConfigManager.gclass61_0.method_5("NoCursorHook") ||
                !StartupClass.cameraRotator.method_2() || (!gspellTimer_0.IsReady && !StartupClass.IsAttached))
                return;
            bool_1 = true;
            SetCursorPos(5000, 5000);
        }
    }

    public static void BringGameWindowToForeground(out double double_2, out double double_3)
    {
        GStruct12 gstruct12_0;
        GetCursorPos(out gstruct12_0);
        double autoAddDistance;
        double double_1;
        GProcessMemoryManipulator.ScreenToWorld(out autoAddDistance, out double_1, gstruct12_0.int_0, gstruct12_0.int_1);
        double_2 = autoAddDistance;
        double_3 = double_1;
    }

    public static void AddWaypoint(bool bool_3)
    {
        StartAutoGlide(bool_3);
        Thread.Sleep(StartupClass.IsAttached ? 100 : 10);
        StartDetachedGlide(bool_3);
    }

    public static void StartAutoGlide(bool bool_3)
    {
        ToggleFactionForTarget(bool_3 ? 8 : 2);
    }

    public static void StartDetachedGlide(bool bool_3)
    {
        ToggleFactionForTarget(bool_3 ? 16 : 4);
    }

    public static int ToggleFactionForTarget(int int_25)
    {
        if (StartupClass.IsGliderInitialized)
        {
            uint uint_5;
            uint uint_6;
            switch (int_25)
            {
                case 2:
                    uint_5 = 513U;
                    uint_6 = 1U;
                    break;
                case 4:
                    uint_5 = 514U;
                    uint_6 = 0U;
                    break;
                case 8:
                    uint_5 = 516U;
                    uint_6 = 2U;
                    break;
                case 16:
                    uint_5 = 517U;
                    uint_6 = 0U;
                    break;
                default:
                    throw new Exception("Can't background send mouse flag: 0x" + int_25.ToString("x"));
            }

            GStruct12 gstruct12_0;
            gstruct12_0.int_0 = int_21;
            gstruct12_0.int_1 = int_22;
            ScreenToClient(StartupClass.MainApplicationHandle, ref gstruct12_0);
            SendMessage(StartupClass.MainApplicationHandle, uint_5, uint_6,
                WaitForKillEvent((uint)gstruct12_0.int_0, (uint)gstruct12_0.int_1));
            gspellTimer_0.Reset();
            return 1;
        }

        var gstruct13_0 = new GStruct13();
        gstruct13_0.int_0 = 0;
        gstruct13_0.gstruct15_0.int_0 = 0;
        gstruct13_0.gstruct15_0.int_1 = 0;
        gstruct13_0.gstruct15_0.int_2 = 0;
        gstruct13_0.gstruct15_0.int_3 = int_25;
        var num = SendInput(1U, ref gstruct13_0, Marshal.SizeOf(gstruct13_0));
        if (int_20 > 0)
            StartupClass.Sleep(int_20);
        return num;
    }

    public static void StopGlide()
    {
        lock (dictionary_0)
        {
            foreach (var key in dictionary_0.Keys)
                LoadProfile(key, false, false);
            dictionary_0.Clear();
        }
    }

    public static void ExecuteStopGlide(string string_1)
    {
        string_1 = string_1.Replace("\r", "");
        string_1 = string_1.Replace("\n", "");
        if (bool_0 && !StartupClass.IsGliderInitialized)
        {
            Logger.LoadProfile(MessageProvider.IsGroupProfile(689, string_1));
            ClipboardHelper.smethod_0(string_1);
            StartMainThread(13);
            StartupClass.Sleep(300 + StartupClass.random_0.Next() % 300);
            LoadSingleProfile('v');
        }
        else
        {
            StartMainThread(13);
            StartupClass.Sleep(300 + StartupClass.random_0.Next() % 300);
            foreach (var char_0 in string_1)
            {
                ResolveWowVersion(char_0);
                StartupClass.Sleep(5);
            }
        }

        StartupClass.Sleep(300 + StartupClass.random_0.Next() % 300);
        StartMainThread(13);
    }

    public static void GetGlideRate(string string_1)
    {
        foreach (var char_0 in string_1)
        {
            ResolveWowVersion(char_0);
            StartupClass.Sleep(20);
        }

        StartupClass.Sleep(300 + StartupClass.random_0.Next() % 300);
    }

    public static void SetupKillEventListener(string string_1)
    {
        if (bool_0 && !StartupClass.IsGliderInitialized)
        {
            ExecuteStopGlide(string_1);
        }
        else
        {
            foreach (var char_0 in string_1)
            {
                ParseDouble(char_0);
                StartupClass.Sleep(66);
            }

            ParseDouble('\r');
        }
    }

    public static void Shutdown(ConfigManager gclass61_0)
    {
        dictionary_0 = new Dictionary<short, short>();
        int_19 = gclass61_0.method_3("TapSpin");
        int_20 = gclass61_0.method_3("KeyDelay");
        bool_0 = gclass61_0.method_2("UseClipboard") == "True";
    }

    private static uint WaitForKillEvent(uint uint_5, uint uint_6)
    {
        return (uint_6 << 16) | uint_5;
    }

    public struct GStruct12
    {
        public int int_0;
        public int int_1;

        public GStruct12(int int_2, int int_3)
        {
            int_0 = int_2;
            int_1 = int_3;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 28)]
    public struct GStruct13
    {
        [FieldOffset(0)] public int int_0;
        [FieldOffset(4)] public GStruct14 gstruct14_0;
        [FieldOffset(4)] public GStruct15 gstruct15_0;
    }

    public struct GStruct14
    {
        public short short_0;
        public short short_1;
        public int int_0;
        public int int_1;
        public int int_2;
    }

    public struct GStruct15
    {
        public int int_0;
        public int int_1;
        public int int_2;
        public int int_3;
        public int pgEditProfileCount;
    }
}