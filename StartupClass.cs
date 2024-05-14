// Decompiled with JetBrains decompiler
// Type: StartupClass
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Glider.Common;
using Glider.Common.Objects;
using Microsoft.Win32;

public class StartupClass
{
    public const string releaseDate = "January 21, 2009";
    public const string versionNumber = "1.8.0";
    public const string releaseType = "Release";
    public const int releaseId = 6703;
    public static bool IsInitialized;
    public static bool IsSecCheckEnabled;
    public static SecCheck SecurityCheckInstance;
    public static int InitializationCount = 0;

    public static string[] ClassesString = new string[10]
    {
        "Deathknight",
        "Druid",
        "Hunter",
        "Mage",
        "Paladin",
        "Priest",
        "Rogue",
        "Shaman",
        "Warlock",
        "Warrior"
    };

    public static bool IsAttached;
    public static bool IsDetached;
    public static GClass49 GameProcessManager;
    public static GStance CurrentStance = GStance.Unknown;
    public static bool IsForegroundEnabled = true;
    public static bool IsBackgroundEnabled = false;
    public static IWin32Window MainWindowHandle = null;
    public static bool IsStopRequested;
    public static GClass16 GameMemoryReader;
    public static GClass47 GameMemoryWriter;
    public static bool IsExitRequested;
    public static GClass71 GliderManager;
    public static SortedList<string, GClass22> ProfileMapping;
    public static GGameClass CurrentGameClass;
    public static GClass22 CurrentProfile;
    public static GClass9 GliderUIManager;
    private static readonly GSpellTimer SpellCooldownTimer = new GSpellTimer(37000, false);
    public static bool IsGliderAttached;
    public static Form MainForm = null;
    public static bool IsGliderRunning;
    public static bool IsGliderPaused;
    public static bool IsGliderInitialized;
    public static GClass32 GameClass32Instance;
    public static int SomeIntegerValue;
    public static GClass8 GameClass8Instance;
    public static GClass69 GameClass69Instance;
    public static bool IsSomeConditionMet;
    public static SortedList<long, GClass5> ProfileIdToProfileMap;
    public static AppMode ApplicationStartupMode;
    public static IntPtr MainApplicationHandle = IntPtr.Zero;
    public static string SomeStringData;
    public static int AnotherIntegerValue;
    public static IntPtr AdditionalApplicationHandle;
    public static bool bool_13;
    public static GProfile gprofile_0;
    public static string string_5;
    public static bool bool_14 = false;
    public static GClass68 gclass68_0;
    public static GClass43 gclass43_0;
    public static GClass43 gclass43_1;
    public static GClass43 gclass43_2;
    public static GClass43 gclass43_3;
    public static GClass43 gclass43_4;
    public static int int_4;
    public static long long_0;
    public static int int_5;
    public static SortedList sortedList_2 = new SortedList();
    public static bool bool_15 = true;
    public static int int_6 = 1;
    public static DateTime dateTime_0;
    public static string WowVersionLabel_string = "";
    public static bool bool_16;
    public static bool bool_17 = false;
    public static bool bool_18;
    public static GClass36 gclass36_0;
    public static GlideMode glideMode_0;
    public static int int_7;
    public static int int_8;
    public static int int_9;
    public static bool bool_19;
    public static Thread thread_0;
    public static GClass54 gclass54_0;
    public static bool bool_20;
    public static bool bool_21;
    public static GClass36 gclass36_1;
    public static GClass24 gclass24_0;
    public static GClass11 gclass11_0;
    public static bool bool_22;
    public static GClass79 gclass79_0;
    public static GameClass gameClass_0;
    public static bool bool_23;
    public static bool bool_24 = true;
    public static DateTime dateTime_1;
    public static bool bool_25;
    public static bool bool_26 = false;
    public static GInterface0 ginterface0_0;
    public static bool bool_27;
    public static int int_10;
    public static GClass73 gclass73_0;
    public static byte[] byte_0 = null;
    public static bool bool_28 = false;
    public static GClass60 gclass60_0;
    public static string numbers_string = "1234567890-=";
    public static int int_11;
    public static GEnum2 genum2_0 = GEnum2.const_0;
    public static bool bool_29;
    public static GClass38 gclass38_0;
    public static double double_0;
    public static GClass44 DebuffsKnown_string;
    public static string[] string_8;
    public static GClass48 gclass48_0;
    public static int int_12;
    public static Random random_0;
    public static GClass33 gclass33_0;
    public static GClass36 gclass36_2 = new GClass36(6000);
    public static bool bool_30;
    public static bool bool_31 = false;
    public static GSpellTimer gspellTimer_1 = null;
    public static bool bool_32;
    public static string string_9;
    public static bool bool_33;
    public static bool bool_34;
    public static bool bool_35;
    public static bool bool_36;
    public static bool bool_37;
    public static bool bool_38;
    public static GClass63 gclass63_0;
    private static int int_13;
    public static IntPtr intptr_2 = IntPtr.Zero;
    public static bool bool_39 = true;
    public static uint uint_0 = uint.MaxValue;
    private static readonly GClass36 gclass36_3 = new GClass36(3500);
    private static GClass36 gclass36_4 = new GClass36(6000);
    private static readonly GSpellTimer gspellTimer_2 = new GSpellTimer(1080000, true);
    private static string string_10 = null;
    private static Size size_0;
    public static bool bool_40;
    public static bool bool_41;

    public static void InitStartupMode(AppMode appMode_1)
    {
        ApplicationStartupMode = appMode_1;
        ProfileMapping = new SortedList<string, GClass22>();
        ProfileIdToProfileMap = new SortedList<long, GClass5>();
        if (appMode_1 == AppMode.PGEdit)
        {
            GClass61.gclass61_0 = new GClass61();
            GClass30.smethod_0(".\\");
            random_0 = new Random();
            gprofile_0 = null;
            string_5 = null;
            thread_0 = null;
            gclass54_0 = new GClass54();
            int_4 = 1;
            if (GClass61.gclass61_0.method_2("LastProfile") != null)
            {
                smethod_1(GClass61.gclass61_0.method_2("LastProfile"));
            }
            else
            {
                gprofile_0 = new GProfile();
                string_5 = GClass30.smethod_1(70);
            }

            var gcontext = new GContext();
            if (appMode_1 != AppMode.PGEdit)
                GClass74.smethod_10();
            GClass55.smethod_31(GClass61.gclass61_0);
            smethod_5();
            GClass42.gclass42_0 = new GClass42();
            GClass42.gclass42_0.method_12();
            smethod_7();
            if (appMode_1 == AppMode.PGEdit)
                return;
            if (GClass61.gclass61_0.method_2("AppKey") != "demo")
                GClass74.smethod_14();
            smethod_8();
        }
        else
        {
            GClass37.smethod_0("Glider 1.8.0 starting up (Release)");
            GClass61.gclass61_0 = new GClass61();
            bool_18 = true;
            if (Environment.CommandLine.ToLower().IndexOf("/l1") != -1)
                bool_34 = true;
            if (Environment.CommandLine.ToLower().IndexOf("/mach") != -1)
            {
                IsAttached = true;
                GClass37.smethod_0("Mach flag, using open memory model");
            }

            if (Environment.CommandLine.ToLower().IndexOf("/resume") != -1)
                bool_30 = true;
            GClass30.smethod_0(".\\");
            gclass11_0 = new GClass11();
            gclass11_0.method_1();
            GameClass32Instance = null;
            random_0 = new Random();
            gclass36_0 = new GClass36(10000);
            glideMode_0 = GlideMode.None;
            WowVersionLabel_string = "0.0";
            int_7 = 0;
            int_8 = 0;
            int_9 = 0;
            gprofile_0 = null;
            string_5 = null;
            bool_19 = string_5 != null;
            bool_16 = false;
            thread_0 = null;
            int_6 = 1;
            gclass36_0 = new GClass36(660000);
            bool_21 = false;
            gclass36_1 = new GClass36(30000);
            IsGliderInitialized = false;
            gclass68_0 = new GClass68();
            if (!IsAttached)
            {
                smethod_54();
                smethod_55();
            }

            if (GClass61.gclass61_0.method_5("AllowNetCheck") && !IsAttached)
                new GClass3().ValidateNetworkSafety(true);
            if (GClass61.gclass61_0.method_2("LastProfile") != null)
            {
                smethod_1(GClass61.gclass61_0.method_2("LastProfile"));
            }
            else
            {
                gprofile_0 = new GProfile();
                string_5 = GClass30.smethod_1(70);
            }

            GliderUIManager = new GClass9();
            var gcontext = new GContext();
            if (!IsAttached)
                GClass74.smethod_10();
            GClass55.smethod_31(GClass61.gclass61_0);
            smethod_5();
            gclass54_0 = new GClass54();
            gclass54_0.method_0(GClass61.gclass61_0);
            smethod_52();
            GClass42.gclass42_0 = new GClass42();
            GClass42.gclass42_0.method_12();
            gclass24_0 = new GClass24();
            gclass36_0.method_4();
            if (!IsAttached)
                smethod_7();
            else
                WowVersionLabel_string = "EvoStub";
            if (!IsAttached)
                smethod_8();
            bool_19 = int_7 != int_8;
            if (gclass11_0.string_0 != null)
            {
                GClass37.smethod_0(GClass30.smethod_2(72, gclass11_0.string_0));
                Environment.Exit(1);
            }

            GameMemoryWriter = new GClass47();
            GameClass69Instance = new GClass69();
            smethod_30();
            smethod_53();
            smethod_9();
        }
    }

    public static bool smethod_1(string string_11)
    {
        if (smethod_2(string_11))
        {
            gclass48_0 = new GClass48();
            return gclass48_0.method_4(string_11);
        }

        gclass48_0 = null;
        return smethod_3(string_11);
    }

    private static bool smethod_2(string string_11)
    {
        return string_11.ToLower().IndexOf("groups\\") != -1;
    }

    public static bool smethod_3(string string_11)
    {
        gprofile_0 = new GProfile();
        if (gprofile_0.Load(string_11))
        {
            sortedList_2.Clear();
            bool_16 = false;
            string_5 = string_11;
            GClass37.smethod_0(GClass30.smethod_2(109, string_5));
            GClass61.gclass61_0.method_0("LastProfile", string_11);
            if (gclass54_0 != null && gclass54_0.sortedList_0 != null)
            {
                gclass54_0.sortedList_0 = null;
                GClass37.smethod_0(GClass30.smethod_1(110));
            }

            if (bool_27)
                ginterface0_0.imethod_0();
            return true;
        }

        GClass37.smethod_0(GClass30.smethod_2(111, string_11));
        if (bool_27)
            ginterface0_0.imethod_0();
        return false;
    }

    public static string smethod_4(string string_11)
    {
        return string_11.LastIndexOf('\\') == -1 ? string_11 : string_11.Substring(string_11.LastIndexOf('\\') + 1);
    }

    public static void smethod_5()
    {
        if (IsAttached)
            return;
        numbers_string = GClass61.gclass61_0.method_2("BarCharacters");
        if (GContext.Main != null)
            GContext.Main.ApplyConfig();
        DebuffsKnown_string = new GClass44();
        if (bool_29)
        {
            bool_29 = false;
            smethod_8();
        }

        if (CurrentGameClass != null)
            CurrentGameClass.LoadConfig();
        GClass50.double_2 = smethod_6(GClass61.gclass61_0.method_2("MeleeDistance"));
        GClass50.double_3 = smethod_6(GClass61.gclass61_0.method_2("RangedDistance"));
        double_0 = smethod_6(GClass61.gclass61_0.method_2("AutoAddDistance"));
        GClass20.bool_0 = GClass61.gclass61_0.method_5("Silent");
        if (ApplicationStartupMode == AppMode.PGEdit)
            return;
        if (!GClass61.gclass61_0.method_5("ListenEnabled"))
        {
            if (gclass79_0 != null)
            {
                GClass37.smethod_0(GClass30.smethod_1(141));
                gclass79_0.method_1();
                gclass79_0 = null;
            }
        }
        else
        {
            if (gclass79_0 != null && gclass79_0.int_0 != GClass61.gclass61_0.method_3("ListenPort"))
            {
                GClass37.smethod_0(GClass30.smethod_2(142, gclass79_0.int_0));
                gclass79_0.method_1();
                gclass79_0 = null;
            }

            if (gclass79_0 == null)
            {
                gclass79_0 = new GClass79();
                GClass37.smethod_0(GClass30.smethod_2(143, gclass79_0.int_0));
                gclass79_0.method_0();
            }
        }

        string_8 = GClass61.gclass61_0.method_2("FriendWhitelist").Split(' ');
        gclass33_0 = new GClass33(GClass61.gclass61_0.method_2("LootPattern"));
        GliderUIManager.method_2();
        if (GClass61.gclass61_0.method_5("UseHook") && !GClass24.bool_0)
        {
            gclass24_0 = new GClass24();
        }
        else
        {
            if (gclass24_0 == null || !GClass24.bool_0 || GClass61.gclass61_0.method_5("UseHook"))
                return;
            gclass24_0.method_17();
        }
    }

    public static double smethod_6(string string_11)
    {
        return double.Parse(string_11.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat);
    }

    private static void smethod_7()
    {
        if (GClass61.gclass61_0.method_2("ForceVersion") != null)
        {
            WowVersionLabel_string = GClass61.gclass61_0.method_2("ForceVersion");
            GClass37.smethod_0(GClass30.smethod_2(81, WowVersionLabel_string));
        }

        var registryKey = Registry.LocalMachine.OpenSubKey(GClass30.smethod_1(649));
        if (registryKey == null)
        {
            GClass37.smethod_0(GClass30.smethod_1(82));
        }
        else
        {
            var obj = registryKey.GetValue("InstallPath");
            if (obj == null)
            {
                registryKey.Close();
                GClass37.smethod_0(GClass30.smethod_1(83));
            }
            else
            {
                SomeStringData = obj.ToString();
                var fileName = SomeStringData + "WoW.exe";
                GClass37.smethod_1(GClass30.smethod_2(84, fileName));
                registryKey.Close();
                var versionInfo = FileVersionInfo.GetVersionInfo(fileName);
                if (versionInfo == null)
                {
                    GClass37.smethod_0(GClass30.smethod_2(85, fileName));
                }
                else
                {
                    if (GClass61.gclass61_0.method_2("ForceVersion") != null)
                        return;
                    WowVersionLabel_string = versionInfo.FileVersion;
                    GClass37.smethod_0(GClass30.smethod_2(86, WowVersionLabel_string));
                }
            }
        }
    }

    public static void smethod_8()
    {
        var str = GClass61.gclass61_0.method_2("CustomClassName");
        if (str.Length == 0)
        {
            var gameClass = (GameClass)GClass61.gclass61_0.method_3("Class");
            str = gameClass + ".cs (internal)";
            if (!ProfileMapping.ContainsKey(str))
            {
                GClass37.smethod_1("No dynamic class: \"" + str + "\"");
                str = gameClass.ToString();
            }

            GClass37.smethod_1("Promoting name to: \"" + str + "\"");
            GClass61.gclass61_0.method_0("CustomClassName", str);
        }

        if (!ProfileMapping.ContainsKey(str))
        {
            GClass37.smethod_0("!! No class defined for: \"" + str + "\"");
            if (ProfileMapping.ContainsKey(str + ".cs (internal)"))
            {
                str += ".cs (internal)";
                GClass37.smethod_0("Promoted to real internal class: " + str);
                GClass61.gclass61_0.method_0("CustomClassName", str);
            }
            else
            {
                str = ProfileMapping.Keys[0];
                if (!bool_37)
                    GClass37.smethod_0("Switching to: " + str);
            }
        }

        ProfileMapping[str].method_0();
        var object0 = (GGameClass)ProfileMapping[str].object_0;
        if (bool_13)
        {
            GClass37.smethod_1("Calling OnAttach for new class");
            object0.OnAttach();
        }

        CurrentStance = GStance.Unknown;
        CurrentGameClass = object0;
        CurrentProfile = ProfileMapping[str];
    }

    public static void smethod_9()
    {
        bool_22 = false;
        GClass18.gclass18_0 = new GClass18();
        thread_0 = new Thread(smethod_10);
        thread_0.Start();
    }

    private static void smethod_10()
    {
        try
        {
            smethod_11();
        }
        catch (Exception ex)
        {
            GClass37.smethod_0(GClass30.smethod_2(73, ex.Message + ex.StackTrace));
        }
    }

    private static void smethod_11()
    {
        GClass52.smethod_0(GClass61.gclass61_0.method_2("AppKey"), true);
        bool_13 = false;
        if (bool_24 && !bool_23)
        {
            bool_23 = true;
            GClass37.smethod_0(GClass30.smethod_1(75));
            smethod_39(1000);
        }

        thread_0 = null;
        if (bool_22)
            GClass17.smethod_0();
        if (!bool_24)
        {
            GClass37.smethod_0(GClass30.smethod_1(76));
            if (bool_25)
                GClass37.smethod_0(GClass30.smethod_2(77, dateTime_1.ToString()));
            if (IsSomeConditionMet)
            {
                GClass74.smethod_12();
                if (GameMemoryWriter != null)
                    GameMemoryWriter.method_7();
                switch (GClass52.int_0)
                {
                    case 0:
                        GClass37.smethod_0(GClass30.smethod_1(846));
                        break;
                    case 1:
                        GClass37.smethod_0(GClass30.smethod_2(880, GClass52.dateTime_0.ToShortDateString()));
                        break;
                    case 2:
                        GClass37.smethod_0(GClass30.smethod_1(881));
                        break;
                    case 3:
                        GClass37.smethod_0(GClass30.smethod_1(882));
                        break;
                    case 4:
                        GClass37.smethod_0(GClass30.smethod_1(883));
                        break;
                }
            }

            bool_19 = false;
            if (GClass61.gclass61_0.method_5("AllowWW") && GliderManager != null && GameMemoryReader == null && !IsAttached)
            {
                GClass37.smethod_0("Starting Tripwire");
                GameMemoryReader = new GClass16(GliderManager, GClass61.gclass61_0.method_5("LogWW"),
                    GClass18.gclass18_0.method_4("VAPeek"));
            }

            if (IsSomeConditionMet && !bool_37 && !IsAttached)
                bool_38 = true;
            if (GameMemoryWriter != null)
                GameMemoryWriter.method_2("OnGliderStart", false);
        }
        else
        {
            IsSomeConditionMet = false;
            if (bool_17)
            {
                bool_18 = true;
                GClass37.smethod_0(GClass30.smethod_1(78));
                smethod_39(2000);
                gclass36_0.method_5();
            }
        }

        if (ApplicationStartupMode == AppMode.Normal)
            GClass37.smethod_0(GClass30.smethod_1(79));
        bool_27 = true;
        ginterface0_0.imethod_0();
        bool_15 = false;
        if (IsSecCheckEnabled)
            return;
        smethod_61();
    }

    public static bool smethod_12()
    {
        return bool_13;
    }

    public static void smethod_13()
    {
        GProcessMemoryManipulator.bool_2 = false;
        bool_13 = false;
        if (!smethod_44())
            return;
        smethod_14();
    }

    public static void smethod_14()
    {
        GClass37.smethod_1("--- Attach code in");
        if (IsAttached)
        {
            GClass20.smethod_0("Attach.wav");
            IsDetached = true;
            ginterface0_0.imethod_0();
        }
        else
        {
            IsForegroundEnabled = false;
            gclass36_2.method_4();
            GProcessMemoryManipulator.bool_2 = false;
            gclass43_0 = new GClass43("Player", GClass18.gclass18_0.method_4("D_Player"));
            gclass43_3 = new GClass43("Item", GClass18.gclass18_0.method_4("D_Items"));
            gclass43_1 = new GClass43("NPC", GClass18.gclass18_0.method_4("D_NPC"));
            gclass43_2 = new GClass43("Object", GClass18.gclass18_0.method_4("D_Object"));
            gclass43_4 = new GClass43("Container", GClass18.gclass18_0.method_4("D_Container"));
            gclass63_0 = new GClass63();
            GContext.Main.OnAttach();
            if (CurrentGameClass != null)
                CurrentGameClass.OnAttach();
            GClass8.smethod_2("UIParent");
            smethod_17(1, GClass30.smethod_1(98));
            ginterface0_0.imethod_0();
            gclass38_0 = new GClass38();
            gclass38_0.method_0();
            GameClass69Instance.method_0();
            GClass67.smethod_0();
            GameClass8Instance = GClass8.smethod_2("GameMenuFrame");
            IsGliderPaused = false;
            if (gclass48_0 != null)
                gclass48_0.method_6();
            bool_13 = true;
            GClass20.smethod_0("Attach.wav");
            if (GameMemoryReader != null)
            {
                GameMemoryReader.method_5();
                GameMemoryReader.method_4();
            }
            else
            {
                GClass37.smethod_1("No WH present at attach");
            }

            bool_36 = false;
            GClass37.smethod_1("--- Attach code out");
            if (!IsStopRequested)
                return;
            IsStopRequested = false;
            smethod_24(false);
        }
    }

    public static void smethod_15()
    {
        if (!bool_13)
            return;
        bool_20 = false;
        bool_32 = true;
        GClass37.smethod_1("AppContext.Detach invoked");
        if (int_12 == 0 && !GProcessMemoryManipulator.smethod_56(AnotherIntegerValue))
        {
            GProcessMemoryManipulator.smethod_7(AdditionalApplicationHandle);
            AdditionalApplicationHandle = IntPtr.Zero;
            AnotherIntegerValue = 0;
        }

        bool_13 = false;
        GameClass69Instance.method_3();
        GameClass8Instance = null;
        smethod_17(1, GClass30.smethod_1(99));
    }

    public static void smethod_16(int int_14)
    {
        if (int_10 == int_14)
            return;
        int_10 = int_14;
        if (!gclass11_0.method_2(int_14))
        {
            GClass37.smethod_0(GClass30.smethod_2(91, gclass11_0.string_0));
            GClass37.smethod_0(GClass30.smethod_1(92));
        }
        else
        {
            GClass37.smethod_0(GClass30.smethod_1(93));
            if (gclass11_0.string_1 == null)
                return;
            GClass37.smethod_0(GClass30.smethod_2(94, gclass11_0.string_1));
        }
    }

    public static void smethod_17(int int_14, string string_11)
    {
        if (gclass79_0 != null)
            gclass79_0.method_5(int_14, string_11);
        if (GliderUIManager == null)
            return;
        if ((int_14 & 32) > 0)
            GliderUIManager.method_1(string_11);
        if ((int_14 & 2) <= 0)
            return;
        GliderUIManager.method_0(string_11);
    }

    public static int smethod_18()
    {
        var num = Environment.CommandLine.IndexOf("/processid=");
        if (num != -1)
            return int.Parse(Environment.CommandLine.Substring(num + 11, 8), NumberStyles.HexNumber);
        GClass37.smethod_0(GClass30.smethod_1(140));
        return 0;
    }

    public static bool smethod_19(string string_11)
    {
        try
        {
            int.Parse(string_11);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static void smethod_20(string string_11)
    {
        for (var index = 0; index < string_11.Length; ++index)
        {
            var char_0 = string_11[index];
            var flag = true;
            if (char_0 == '#' && index < string_11.Length - 1)
            {
                if (string_11[index + 1] == '#')
                {
                    flag = true;
                    ++index;
                }
                else
                {
                    var num = string_11.IndexOf('#', index + 1);
                    if (num > -1)
                        try
                        {
                            var short_0 = (short)int.Parse(string_11.Substring(index + 1, num - index - 1));
                            flag = false;
                            GClass55.smethod_9(short_0);
                            index = num;
                        }
                        catch (Exception ex)
                        {
                            GClass37.smethod_1(GClass30.smethod_2(144, string_11));
                        }
                }
            }

            if (char_0 == '|')
            {
                if (index < string_11.Length - 1 && string_11[index + 1] == '|')
                {
                    flag = true;
                    ++index;
                }
                else
                {
                    flag = false;
                    GClass55.smethod_9(13);
                    Thread.Sleep(700);
                }
            }

            if (flag)
                GClass55.smethod_6(char_0);
        }
    }

    public static bool smethod_21(bool bool_42)
    {
        if (!bool_27)
            return false;
        if (bool_19)
        {
            GClass37.smethod_0(GClass30.smethod_1(113));
            return false;
        }

        if (glideMode_0 != GlideMode.None)
        {
            GClass37.smethod_0(GClass30.smethod_1(114));
            return false;
        }

        if (!bool_13)
        {
            smethod_13();
            if (!bool_13)
                return false;
        }

        if (GPlayerSelf.Me.TargetGUID == 0L)
        {
            GClass37.smethod_0(GClass30.smethod_1(115));
            return false;
        }

        if (!GClass61.gclass61_0.method_5("BackgroundEnable"))
            smethod_22();
        glideMode_0 = GlideMode.Manual;
        gclass60_0 = new GClass60();
        return true;
    }

    public static void smethod_22()
    {
        smethod_39(500);
        GProcessMemoryManipulator.SetForegroundWindow(MainApplicationHandle);
        smethod_39(500);
    }

    public static bool smethod_23()
    {
        if (!bool_13)
        {
            GClass37.smethod_0(GClass30.smethod_1(107));
            return false;
        }

        if (gprofile_0 == null)
        {
            GClass37.smethod_0(GClass30.smethod_1(108));
            return false;
        }

        var flag = false;
        switch (genum2_0)
        {
            case GEnum2.const_0:
                flag = GPlayerSelf.Me.IsDead;
                break;
            case GEnum2.const_1:
                flag = false;
                break;
            case GEnum2.const_2:
                flag = true;
                break;
        }

        if (genum2_0 == GEnum2.const_3)
        {
            gprofile_0.VendorWaypoints.Add(GPlayerSelf.Me.Location);
            GClass37.smethod_0(GClass30.smethod_2(870, gprofile_0.VendorWaypoints.Count));
        }
        else if (!flag)
        {
            gprofile_0.Waypoints.Add(GPlayerSelf.Me.Location);
            GClass37.smethod_0(GClass30.smethod_2(658, gprofile_0.Waypoints.Count));
        }
        else
        {
            gprofile_0.GhostWaypoints.Add(GPlayerSelf.Me.Location);
            GClass37.smethod_0(GClass30.smethod_2(659, gprofile_0.GhostWaypoints.Count));
        }

        bool_16 = true;
        return true;
    }

    public static bool smethod_24(bool bool_42)
    {
        if (!bool_27)
            return false;
        if (bool_25 && DateTime.Now > dateTime_1)
        {
            GClass37.smethod_0(GClass30.smethod_1(116));
            return false;
        }

        if (GClass61.gclass61_0.method_5("AllowNetCheck") && !new GClass3().ValidateNetworkSafety(true))
            return false;
        if (glideMode_0 != GlideMode.None)
        {
            GClass37.smethod_0(GClass30.smethod_1(117));
            return false;
        }

        if (!bool_13 && !IsDetached)
        {
            smethod_13();
            if (!bool_13)
                return false;
        }

        if (bool_19)
        {
            GClass37.smethod_0(GClass30.smethod_1(118));
            return false;
        }

        if (IsDetached)
            return smethod_25();
        if (gclass48_0 == null && (gprofile_0 == null || (gprofile_0.Waypoints.Count < 2 && !gprofile_0.Fishing)))
        {
            GClass37.smethod_0(GClass30.smethod_1(119));
            return false;
        }

        if (GPlayerSelf.Me.IsDead &&
            (gprofile_0.GhostWaypoints.Count == 0 || !GClass61.gclass61_0.method_5("Resurrect")))
        {
            GClass37.smethod_0(GClass30.smethod_1(120));
            return false;
        }

        if (!IsGliderInitialized)
            smethod_22();
        glideMode_0 = GlideMode.Auto;
        gclass73_0 = new GClass73();
        if (gclass73_0.method_1())
            return true;
        glideMode_0 = GlideMode.None;
        return false;
    }

    private static bool smethod_25()
    {
        GameProcessManager = new GClass49();
        if (!GameProcessManager.method_0())
        {
            glideMode_0 = GlideMode.None;
            return false;
        }

        glideMode_0 = GlideMode.Auto;
        ginterface0_0.imethod_0();
        return true;
    }

    public static bool smethod_26()
    {
        if (!bool_13)
        {
            GClass37.smethod_0(GClass30.smethod_1(130));
            return false;
        }

        if (GPlayerSelf.Me.Target == null)
        {
            GClass37.smethod_0(GClass30.smethod_1(131));
            return false;
        }

        if (GPlayerSelf.Me.Target.IsPlayer)
        {
            GClass37.smethod_0(GClass30.smethod_1(132));
            return false;
        }

        if (gprofile_0 == null)
        {
            GClass37.smethod_0(GClass30.smethod_1(133));
            return false;
        }

        if (gprofile_0.CheckFaction(GPlayerSelf.Me.Target.FactionID, true))
        {
            GClass37.smethod_0(GClass30.smethod_1(128));
        }
        else
        {
            GClass37.smethod_0(GClass30.smethod_1(129));
            gprofile_0.SetFactionsFromString(gprofile_0.GetFactionsAsString() + " " + GPlayerSelf.Me.Target.FactionID);
        }

        return true;
    }

    public static void smethod_27(bool bool_42, string string_11)
    {
        if (glideMode_0 == GlideMode.None && !bool_42)
            return;
        var flag = false;
        try
        {
            ++int_13;
            smethod_28(bool_42, string_11);
        }
        catch (ThreadInterruptedException ex)
        {
            flag = true;
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("! Exception in KillAction: " + ex.Message + ex.StackTrace);
        }
        finally
        {
            --int_13;
        }

        if (flag)
            throw new ThreadInterruptedException();
    }

    private static void smethod_28(bool bool_42, string string_11)
    {
        var flag = false;
        if (glideMode_0 == GlideMode.None && (!bool_13 || !bool_42))
        {
            --int_13;
        }
        else
        {
            smethod_51();
            gclass68_0.method_3(true);
            GClass37.smethod_1(GClass30.smethod_2(652, bool_42, (int)glideMode_0, string_11));
            gclass68_0.method_3(true);
            GClass55.smethod_21(false);
            if (glideMode_0 == GlideMode.Auto)
            {
                if (bool_42)
                    bool_36 = true;
                if (CurrentGameClass != null)
                    CurrentGameClass.OnStopGlide();
                smethod_17(1, GClass30.smethod_1(100));
                if (IsAttached)
                {
                    if (Thread.CurrentThread == GameProcessManager.thread_0)
                        flag = true;
                }
                else if (Thread.CurrentThread == gclass73_0.thread_0)
                {
                    flag = true;
                }

                GClass37.smethod_1(GClass30.smethod_1(100));
                glideMode_0 = GlideMode.None;
                if (IsAttached)
                {
                    GameProcessManager.method_1();
                    GameProcessManager = null;
                }
                else
                {
                    gclass73_0.method_2();
                    gclass73_0 = null;
                }
            }

            if (glideMode_0 == GlideMode.Manual)
            {
                smethod_17(1, GClass30.smethod_1(101));
                if (gclass60_0 != null && Thread.CurrentThread == gclass60_0.thread_0)
                    flag = true;
                GClass37.smethod_1(GClass30.smethod_1(102));
                glideMode_0 = GlideMode.None;
                if (gclass60_0 != null)
                    gclass60_0.method_0();
                gclass60_0 = null;
            }

            if (bool_42)
                smethod_15();
            ginterface0_0.imethod_0();
            GContext.Main.ReleaseAllKeys();
            if (GliderManager != null)
                GliderManager.method_33(false);
            if (flag)
                throw new ThreadInterruptedException();
        }
    }

    public static int smethod_29()
    {
        if (glideMode_0 != GlideMode.Auto)
            return int_11;
        if (gclass73_0 == null)
            return 0;
        lock (gclass73_0)
        {
            if (gclass73_0.bool_9)
            {
                int_11 = (int)Math.Round(gclass73_0.int_8 / (DateTime.Now - dateTime_0).TotalSeconds * 3600.0, 0);
                gclass73_0.bool_9 = false;
            }
        }

        return int_11;
    }

    private static void smethod_30()
    {
        if (Environment.CommandLine.ToLower().IndexOf("/kill") == -1)
            return;
        new Thread(smethod_32).Start();
    }

    public static void smethod_31()
    {
        if (GameMemoryReader != null)
            GameMemoryReader.method_0();
        GameMemoryWriter.method_0();
        GClass74.smethod_4();
        GliderUIManager.method_5();
    }

    private static void smethod_32()
    {
        var string_11 = smethod_36("/kill");
        intptr_2 = CreateEvent(IntPtr.Zero, false, false, string_11);
        if (intptr_2 == IntPtr.Zero)
        {
            GClass37.smethod_0("Couldn't create named event");
        }
        else
        {
            WaitForSingleObject(intptr_2, uint_0);
            CloseHandle(intptr_2);
            if (!bool_39)
                return;
            smethod_33();
        }
    }

    private static void smethod_33()
    {
        GClass20.smethod_1("GliderExit.wav");
        DebuffsKnown_string.method_10();
        ginterface0_0.imethod_4();
        if (GliderManager != null && !bool_33)
            GliderManager.method_11();
        smethod_31();
        Environment.Exit(0);
    }

    public static void smethod_34()
    {
        if (intptr_2 == IntPtr.Zero)
            smethod_33();
        SetEvent(intptr_2);
    }

    public static void smethod_35()
    {
        if (intptr_2 == IntPtr.Zero)
            return;
        bool_39 = false;
        SetEvent(intptr_2);
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr CreateEvent(
        IntPtr intptr_3,
        bool bool_42,
        bool bool_43,
        string string_11);

    [DllImport("kernel32", SetLastError = true)]
    internal static extern int WaitForSingleObject(IntPtr intptr_3, uint uint_1);

    [DllImport("Kernel32.dll", SetLastError = true)]
    private static extern void SetEvent(IntPtr intptr_3);

    public static string smethod_36(string string_11)
    {
        var num1 = Environment.CommandLine.IndexOf(string_11 + "=");
        if (num1 == -1)
        {
            GClass37.smethod_0(GClass30.smethod_1(759));
            return null;
        }

        var startIndex = num1 + string_11.Length + 1;
        var num2 = Environment.CommandLine.IndexOf(' ', startIndex);
        if (num2 == -1)
            num2 = Environment.CommandLine.Length;
        return Environment.CommandLine.Substring(startIndex, num2 - startIndex);
    }

    [DllImport("kernel32")]
    private static extern bool CloseHandle(IntPtr intptr_3);

    public static void smethod_37(GEnum0 genum0_0)
    {
        GClass37.smethod_0("StopOnTW invoked, result = " + (int)genum0_0);
        if (genum0_0 == GEnum0.const_2)
            File.WriteAllText("TWfail.txt", "guh!");
        if (genum0_0 == GEnum0.const_1)
            File.WriteAllText("TWunsafe.txt", "guh!");
        if (genum0_0 == GEnum0.const_3)
            File.WriteAllText("DeadSession.txt", "guh!");
        IsExitRequested = true;
        if (GameMemoryReader != null)
            GameMemoryReader.method_0();
        if (!bool_33 && GliderManager != null)
            GliderManager.method_11();
        ginterface0_0.imethod_4();
    }

    public static void smethod_38()
    {
        if (IsExitRequested)
            return;
        smethod_45();
        if (bool_30 && bool_27)
        {
            GProcessMemoryManipulator.smethod_53();
            if (GameMemoryWriter != null && (ApplicationStartupMode == AppMode.Normal || ApplicationStartupMode == AppMode.Invisible))
                GameMemoryWriter.method_2("OnGliderStart", false);
        }

        if (gspellTimer_2.IsReady)
        {
            gspellTimer_2.Reset();
            var gclass3 = new GClass3();
            if (GClass61.gclass61_0.method_5("AllowNetCheck"))
                gclass3.ValidateNetworkSafety(false);
        }

        if (SpellCooldownTimer.IsReady)
        {
            SpellCooldownTimer.Reset();
            GProcessMemoryManipulator.bool_3 = GProcessMemoryManipulator.smethod_28();
            GProcessMemoryManipulator.smethod_31();
        }

        if (bool_38 && !IsAttached && !bool_37)
        {
            bool_37 = true;
            GClass74.smethod_14();
            smethod_8();
        }

        if (!bool_13)
            return;
        if (DebuffsKnown_string != null && gclass36_3.method_3())
        {
            gclass36_3.method_4();
            DebuffsKnown_string.method_8();
        }

        if (GPlayerSelf.Me.Stance != CurrentStance)
        {
            if (CurrentStance != GStance.Unknown)
                GContext.Main.Interface.UnFillAllKeys();
            CurrentStance = GPlayerSelf.Me.Stance;
        }

        GameClass69Instance.method_4();
        GClass67.smethod_1();
        if (GameClass8Instance != null && GameClass8Instance.method_10() && glideMode_0 == GlideMode.Auto)
            GClass55.smethod_9(27);
        if (glideMode_0 == GlideMode.Auto && IsGliderInitialized && GClass61.gclass61_0.method_2("BackgroundDisplay") != "Normal" &&
            (DateTime.Now - dateTime_0).TotalSeconds >= 8.0 && !IsGliderRunning)
        {
            IsGliderRunning = true;
            smethod_46();
        }

        GObjectList.GetObjects();
        gclass68_0.method_7();
        GClass55.smethod_21(true);
    }

    public static void smethod_39(int int_14)
    {
        Thread.Sleep(int_14);
    }

    public static void smethod_40()
    {
        Thread.Sleep(12);
        Thread.Sleep(47);
        Thread.Sleep(2382);
        Thread.Sleep(2);
    }

    public static bool smethod_41(GUnit gunit_0)
    {
        if (gunit_0 != null && gunit_0.IsValid)
        {
            if (GameClass69Instance == null || GameClass69Instance.method_10() >= 10)
                return false;
            GClass37.smethod_0(GClass30.smethod_1(830));
            if (glideMode_0 == GlideMode.Auto)
                gprofile_0.ForceBlacklist(gunit_0.GUID);
            GClass73.smethod_1();
            return true;
        }

        GClass37.smethod_0(GClass30.smethod_1(517));
        if (GClass61.gclass61_0.method_5("StopOnVanish"))
        {
            GContext.Main.Movement.LookConfused();
            GClass20.smethod_0("GMWhisper.wav");
            smethod_27(true, "TargetVanishedInCombat");
        }

        return true;
    }

    public static string smethod_42(string string_11)
    {
        var num = string_11.LastIndexOf("\\");
        return num == -1 ? string_11 : string_11.Substring(num + 1);
    }

    public static string smethod_43(string string_11)
    {
        var length = string_11.LastIndexOf("\\");
        return length == -1 ? string_11 : string_11.Substring(0, length);
    }

    public static bool smethod_44()
    {
        AnotherIntegerValue = GProcessMemoryManipulator.smethod_1();
        if (AnotherIntegerValue == 0)
        {
            bool_35 = true;
            return false;
        }

        IsGliderAttached = true;
        if (AdditionalApplicationHandle == IntPtr.Zero && !bool_14)
        {
            AdditionalApplicationHandle = GProcessMemoryManipulator.smethod_6(AnotherIntegerValue);
            if (AdditionalApplicationHandle == IntPtr.Zero)
            {
                if (!IsGliderPaused)
                {
                    IsGliderPaused = true;
                    GClass37.smethod_0(GClass30.smethod_2(96, Marshal.GetLastWin32Error()));
                }

                return false;
            }

            GProcessMemoryManipulator.bool_3 = GProcessMemoryManipulator.smethod_28();
            if (GliderManager != null && !GliderManager.method_26(AnotherIntegerValue))
            {
                GClass37.smethod_0(
                    "Some other Glider is already open on that game, maybe we'll attach to some other one");
                CloseHandle(AdditionalApplicationHandle);
                AdditionalApplicationHandle = IntPtr.Zero;
                GProcessMemoryManipulator.smethod_5(AnotherIntegerValue);
                AnotherIntegerValue = 0;
                return false;
            }

            GProcessMemoryManipulator.smethod_31();
            if (GameMemoryWriter != null)
                GameMemoryWriter.method_2("OnGameFirstSeen", false);
        }

        if (IsAttached)
            return true;
        if (GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("UIParent"), "probeuip") == 0 && !bool_20 &&
            ((bool_31 && gspellTimer_1.IsReady) || IsForegroundEnabled))
        {
            var str = GClass61.gclass61_0.method_2("AutoLog");
            if (str != null && str.Length > 0 && IsSomeConditionMet && new GClass31().method_2())
            {
                bool_20 = true;
                GameMemoryWriter.method_2("DoAutoLog", false);
            }

            return false;
        }

        if (GClass18.gclass18_0.method_5("TLSSlot"))
            return GProcessMemoryManipulator.smethod_52(out long_0, out int_5) && GObjectList.StealthCountGameObjects(long_0) > 0 &&
                   long_0 != 0L;
        var numArray = GProcessMemoryManipulator.smethod_20(GClass18.gclass18_0.method_4("PlayerIdAddr"), 8);
        if (numArray == null)
        {
            if (AdditionalApplicationHandle != IntPtr.Zero)
                CloseHandle(AdditionalApplicationHandle);
            AdditionalApplicationHandle = IntPtr.Zero;
            return false;
        }

        long_0 = BitConverter.ToInt64(numArray, 0);
        if (long_0 == 0L)
            return false;
        int_5 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("MainTable"), "MainTableProbe");
        return GObjectList.StealthCountGameObjects(long_0) > 1;
    }

    public static void smethod_45()
    {
        if (bool_13 || IsDetached || !bool_22 || !smethod_44())
            return;
        smethod_14();
    }

    public static void smethod_46()
    {
        switch (GClass61.gclass61_0.method_2("BackgroundDisplay"))
        {
            case "Hide":
                smethod_47();
                break;
            case "Shrink":
                smethod_48();
                break;
        }
    }

    public static void smethod_47()
    {
        if (bool_40)
            return;
        GProcessMemoryManipulator.smethod_38(MainApplicationHandle);
        bool_40 = true;
    }

    public static void smethod_48()
    {
        if (bool_41)
            return;
        double width = GClass61.gclass61_0.method_3("ShrinkWidth");
        GProcessMemoryManipulator.smethod_40(MainApplicationHandle, out size_0);
        var height = size_0.Height / (double)size_0.Width * width;
        GProcessMemoryManipulator.smethod_42(MainApplicationHandle, new Size((int)width, (int)height));
        bool_41 = true;
    }

    public static void smethod_49()
    {
        if (!bool_40)
            return;
        GProcessMemoryManipulator.smethod_37(MainApplicationHandle);
        bool_40 = false;
    }

    public static void smethod_50()
    {
        if (!bool_41)
            return;
        GProcessMemoryManipulator.smethod_42(MainApplicationHandle, size_0);
        bool_41 = false;
    }

    public static void smethod_51()
    {
        smethod_49();
        smethod_50();
    }

    private static void smethod_52()
    {
        if (Environment.CommandLine.ToLower().IndexOf("/ln") == -1)
            return;
        var string_4 = smethod_36("/ln");
        GClass61.gclass61_0.method_0("LName", string_4);
        GClass61.gclass61_0.method_8();
    }

    private static void smethod_53()
    {
        if (Environment.CommandLine.ToLower().IndexOf("/driver") != -1)
        {
            GClass20.smethod_1("Kill.wav");
            GliderManager = new GClass71(smethod_36("/driver"));
            GClass37.smethod_1("Sending data to shadow driver");
            if (!GliderManager.method_38())
            {
                if (MessageBox.Show(null, GClass30.smethod_1(862), "Glider", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Hand) == DialogResult.Yes)
                    Help.ShowHelp(null, "Glider.chm", HelpNavigator.Topic, "ShadowFailed.html");
                bool_30 = false;
            }
            else
            {
                GClass37.smethod_0("Shadow confirmed, looks awake");
            }

            if (Environment.CommandLine.ToLower().IndexOf("/holddriver") != -1)
            {
                GClass37.smethod_1("DriverName is static, will leave driver resident");
                bool_33 = true;
            }
        }
        else
        {
            GClass37.smethod_0(GClass30.smethod_1(877));
        }

        Environment.CommandLine.ToLower().IndexOf("/shadowread");
        if (Environment.CommandLine.ToLower().IndexOf("/attachpid") != -1)
        {
            int_12 = int.Parse(smethod_36("/attachpid"));
            GClass37.smethod_0("/attachpid specified, looking for: " + int_12);
        }

        if (!GClass61.gclass61_0.method_5("UnloadShadow") || GliderManager == null)
            return;
        GliderManager.method_11();
        GliderManager = null;
    }

    private static void smethod_54()
    {
        var str = GClass61.gclass61_0.method_2("ForceVersion");
        if (str == null || str.Length <= 0 || MessageBox.Show(null,
                "ForceVersion detected in configuration.  Running with this option increases the risk of detection and may cause Glider to crash.  Are you sure you want to continue?",
                "Glider", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) !=
            DialogResult.No)
            return;
        if (GliderManager != null && !bool_33)
            GliderManager.method_11();
        Environment.Exit(0);
    }

    private static void smethod_55()
    {
        if (GClass61.gclass61_0.method_5("AllowWW") || MessageBox.Show(null,
                "Tripwire is disabled in configuration.  Running with this option increases the risk of detection.  Are you sure you want to continue?",
                "Glider", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) !=
            DialogResult.No)
            return;
        if (GliderManager != null && !bool_33)
            GliderManager.method_11();
        Environment.Exit(0);
    }

    public static void smethod_56()
    {
        IsStopRequested = false;
        if (!IsSomeConditionMet)
        {
            GClass37.smethod_0(GClass30.smethod_1(868));
            GClass73.smethod_0(869);
        }
        else
        {
            var string_1 = GClass61.gclass61_0.method_2("AutoLog");
            if (!new GClass31().method_1(string_1))
                return;
            string_9 = string_1;
            GClass37.smethod_1("Autolog is good!");
            IsStopRequested = true;
        }
    }

    public static bool IsDecryptedStreamEmpty(GDataEncryptionManager gclass56_0)
    {
        return gclass56_0.ReadIntFromDecryptedStream() == 0;
    }

    public static void smethod_58()
    {
        if (IsInitialized)
        {
            SecurityCheckInstance.Focus();
            SecurityCheckInstance.method_0();
        }
        else
        {
            IsInitialized = true;
            SecurityCheckInstance = new SecCheck();
            SecurityCheckInstance.Show();
            GClass61.gclass61_0.method_0("LastSecCheck", DateTime.Now.ToShortDateString());
            GClass61.gclass61_0.method_8();
        }
    }

    public static void smethod_59()
    {
        if (!GClass61.gclass61_0.method_5("AllowAutoSecCheck"))
            return;
        if (GClass61.gclass61_0.method_2("LastSecCheck") == null)
        {
            GClass61.gclass61_0.method_0("LastSecCheck", DateTime.Now.ToShortDateString());
        }
        else
        {
            if ((DateTime.Now - DateTime.Parse(GClass61.gclass61_0.method_2("LastSecCheck"))).TotalDays < 7.0)
                return;
            smethod_60();
        }
    }

    private static void smethod_60()
    {
        if (MessageBox.Show(MainForm, GClass30.smethod_1(875), GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation) != DialogResult.Yes)
            return;
        smethod_58();
    }

    private static void smethod_61()
    {
        IsSecCheckEnabled = true;
        if (int.Parse("1.8.0".Replace(".", "")) >= InitializationCount)
            return;
        if (GClass61.gclass61_0.method_5("NoVersionPop"))
        {
            GClass37.smethod_0("A new version of Glider is available for download.");
        }
        else
        {
            if (MessageBox.Show(MainForm, GClass30.smethod_1(876), GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;
            Process.Start("http://www.mmoglider.com/Download.aspx?Update=True");
        }
    }

    public static void smethod_62()
    {
        if (!IsGliderInitialized && GClass61.gclass61_0.method_5("BackgroundEnable") && GliderManager != null && IsSomeConditionMet)
        {
            GClass37.smethod_1("Setting up bg stuff");
            MainApplicationHandle = GProcessMemoryManipulator.smethod_29(AnotherIntegerValue);
            GliderManager.method_34(AnotherIntegerValue, MainApplicationHandle);
            IsGliderInitialized = true;
        }
        else
        {
            GClass37.smethod_1("No bg stuff setup");
        }
    }

    public static string smethod_63(int int_14)
    {
        var num1 = GClass18.gclass18_0.method_4("MacroBase");
        var num2 = GProcessMemoryManipulator.smethod_11(num1 + 36, "mbase");
        int int_29_1;
        for (var int_29_2 =
                 GProcessMemoryManipulator.smethod_11(GProcessMemoryManipulator.smethod_11(num1 + 28, "mbase2") + 12 * (int_14 & num2) + 8, "mbase3");
             int_29_2 > 0 && (int_29_2 & 1) == 0;
             int_29_2 = GProcessMemoryManipulator.smethod_11(int_29_2 + GProcessMemoryManipulator.smethod_11(int_29_1, "mnext3") + 4, "mnext4"))
        {
            var num3 = GProcessMemoryManipulator.smethod_11(int_29_2, "mstep");
            var str = GProcessMemoryManipulator.smethod_9(int_29_2 + 36, 64, "mname");
            if (num3 == int_14)
                return str;
            int_29_1 = GProcessMemoryManipulator.smethod_11(num1 + 28, "mnext1") +
                       12 * (GProcessMemoryManipulator.smethod_11(num1 + 36, "mnext2") & int_14);
        }

        return "(could not find macro in list!)";
    }
}