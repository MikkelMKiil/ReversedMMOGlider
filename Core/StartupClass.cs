// Decompiled with JetBrains decompiler
// Type: StartupClass
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common;
using Glider.Common.Objects;
using Microsoft.Win32;
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
    public static MachGlideRunner GameProcessManager;
    public static GStance CurrentStance = GStance.Unknown;
    public static bool IsForegroundEnabled = true;
    public static bool IsBackgroundEnabled = false;
    public static IWin32Window MainWindowHandle = null;
    public static bool IsStopRequested;
    public static WardenMonitor GameMemoryReader;
    public static ScriptExecutor GameMemoryWriter;
    public static bool IsExitRequested;
    public static WardenProtocol GliderManager;
    public static SortedList<string, SpellActionData> ProfileMapping;
    public static GGameClass CurrentGameClass;
    public static SpellActionData CurrentProfile;
    public static WebNotificationService GliderUIManager;
    private static readonly GSpellTimer SpellCooldownTimer = new GSpellTimer(37000, false);
    public static bool IsGliderAttached;
    public static Form MainForm = null;
    public static bool IsGliderRunning;
    public static bool IsGliderPaused;
    public static bool IsGliderInitialized;
    public static ISXWardenIntegration GameClass32Instance;
    public static int SomeIntegerValue;
    public static UIElement GameClass8Instance;
    public static ChatLogManager GameClass69Instance;
    public static bool IsSomeConditionMet;
    public static SortedList<long, LootableCorpseTracker> ProfileIdToProfileMap;
    public static AppMode ApplicationStartupMode;
    public static IntPtr MainApplicationHandle = IntPtr.Zero;
    public static string SomeStringData;
    public static int AnotherIntegerValue;
    public static IntPtr AdditionalApplicationHandle;
    public static bool bool_13;
    public static GProfile gprofile_0;
    public static string string_5;
    public static bool bool_14 = false;
    public static CameraRotator gclass68_0;
    public static OffsetManager gclass43_0;
    public static OffsetManager gclass43_1;
    public static OffsetManager gclass43_2;
    public static OffsetManager gclass43_3;
    public static OffsetManager gclass43_4;
    public static int int_4;
    public static long long_0;
    public static int int_5;
    public static SortedList sortedList_2 = new SortedList();
    public static bool bool_15 = true;
    public static int int_6 = 1;
    public static DateTime dateTime_0;
    public static string WowVersionLabel_string = "";
    public static bool bool_16;
    public static bool IsBetaVersion = false;
    public static bool IsBetaAccessGranted;
    public static GameTimer gclass36_0;
    public static GlideMode glideMode_0;
    public static int int_7;
    public static int int_8;
    public static int int_9;
    public static bool bool_19;
    public static Thread thread_0;
    public static PartyManager gclass54_0;
    public static bool bool_20;
    public static bool bool_21;
    public static GameTimer gclass36_1;
    public static KeyboardHookManager gclass24_0;
    public static SecurityDescriptorHelper gclass11_0;
    public static bool isInitializationSuccessful;
    public static RemoteViewerServer gclass79_0;
    public static GameClass gameClass_0;
    public static bool bool_23;
    public static bool isInputStringFourCharacters = true;
    public static DateTime expiryTime;
    public static bool isTimeAdded;
    public static bool bool_26 = false;
    public static ILogger ginterface0_0;
    public static bool bool_27;
    public static int int_10;
    public static CombatController gclass73_0;
    public static byte[] byte_0 = null;
    public static bool bool_28 = false;
    public static GlideMainThread gclass60_0;
    public static string numbers_string = "1234567890-=";
    public static int int_11;
    public static WaypointType genum2_0 = WaypointType.const_0;
    public static bool bool_29;
    public static EquipmentEnchantmentChecker gclass38_0;
    public static double double_0;
    public static DebuffDatabase DebuffsKnown_string;
    public static string[] string_8;
    public static ProfileGroupManager gclass48_0;
    public static int int_12;
    public static Random random_0;
    public static LootRouteParser gclass33_0;
    public static GameTimer gclass36_2 = new GameTimer(6000);
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
    public static SpellbookManager gclass63_0;
    private static readonly object killActionLock = new object();
    private static int int_13;
    public static IntPtr intptr_2 = IntPtr.Zero;
    public static bool bool_39 = true;
    public static uint uint_0 = uint.MaxValue;
    private static readonly GameTimer gclass36_3 = new GameTimer(3500);
    private static GameTimer gclass36_4 = new GameTimer(6000);
    private static readonly GSpellTimer gspellTimer_2 = new GSpellTimer(1080000, true);
    private static string string_10 = null;
    private static Size size_0;
    public static bool bool_40;
    public static bool bool_41;

    public static void InitStartupMode(AppMode appMode_1)
    {
        ApplicationStartupMode = appMode_1;
        ProfileMapping = new SortedList<string, SpellActionData>();
        ProfileIdToProfileMap = new SortedList<long, LootableCorpseTracker>();
        if (appMode_1 == AppMode.PGEdit)
        {
            ConfigManager.gclass61_0 = new ConfigManager();
            MessageProvider.smethod_0(".\\");
            random_0 = new Random();
            gprofile_0 = null;
            string_5 = null;
            thread_0 = null;
            gclass54_0 = new PartyManager();
            int_4 = 1;
            if (ConfigManager.gclass61_0.method_2("LastProfile") != null)
            {
                smethod_1(ConfigManager.gclass61_0.method_2("LastProfile"));
            }
            else
            {
                gprofile_0 = new GProfile();
                string_5 = MessageProvider.GetMessage(70);
            }

            var gcontext = new GContext();
            if (appMode_1 != AppMode.PGEdit)
                CodeCompiler.smethod_10();
            InputController.smethod_31(ConfigManager.gclass61_0);
            smethod_5();
            SpellcastingManager.gclass42_0 = new SpellcastingManager();
            SpellcastingManager.gclass42_0.method_12();
            smethod_7();
            if (appMode_1 == AppMode.PGEdit)
                return;
            if (ConfigManager.gclass61_0.method_2("AppKey") != "demo")
                CodeCompiler.smethod_14();
            smethod_8();
        }
        else
        {
            Logger.LogMessage("Glider 1.8.0 starting up (Release)");
            ConfigManager.gclass61_0 = new ConfigManager();
            IsBetaAccessGranted = true;
            if (Environment.CommandLine.ToLower().IndexOf("/l1") != -1)
                bool_34 = true;
            if (Environment.CommandLine.ToLower().IndexOf("/mach") != -1)
            {
                IsAttached = true;
                Logger.LogMessage("Mach flag, using open memory model");
            }

            if (Environment.CommandLine.ToLower().IndexOf("/resume") != -1)
                bool_30 = true;
            MessageProvider.smethod_0(".\\");
            gclass11_0 = new SecurityDescriptorHelper();
            gclass11_0.method_1();
            GameClass32Instance = null;
            random_0 = new Random();
            gclass36_0 = new GameTimer(10000);
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
            gclass36_0 = new GameTimer(660000);
            bool_21 = false;
            gclass36_1 = new GameTimer(30000);
            IsGliderInitialized = false;
            gclass68_0 = new CameraRotator();
            if (!IsAttached)
            {
                smethod_54();
                smethod_55();
            }

            if (ConfigManager.gclass61_0.method_5("AllowNetCheck") && !IsAttached)
                new NetworkSafetyChecker().ValidateNetworkSafety(true);
            if (ConfigManager.gclass61_0.method_2("LastProfile") != null)
            {
                smethod_1(ConfigManager.gclass61_0.method_2("LastProfile"));
            }
            else
            {
                gprofile_0 = new GProfile();
                string_5 = MessageProvider.GetMessage(70);
            }

            GliderUIManager = new WebNotificationService();
            var gcontext = new GContext();
            if (!IsAttached)
                CodeCompiler.smethod_10();
            InputController.smethod_31(ConfigManager.gclass61_0);
            smethod_5();
            gclass54_0 = new PartyManager();
            gclass54_0.method_0(ConfigManager.gclass61_0);
            smethod_52();
            SpellcastingManager.gclass42_0 = new SpellcastingManager();
            SpellcastingManager.gclass42_0.method_12();
            gclass24_0 = new KeyboardHookManager();
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
                Logger.LogMessage(MessageProvider.smethod_2(72, gclass11_0.string_0));
                Environment.Exit(1);
            }

            GameMemoryWriter = new ScriptExecutor();
            GameClass69Instance = new ChatLogManager();
            smethod_30();
            smethod_53();
            smethod_9();
        }
    }

    public static bool smethod_1(string string_11)
    {
        if (smethod_2(string_11))
        {
            gclass48_0 = new ProfileGroupManager();
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
            Logger.LogMessage(MessageProvider.smethod_2(109, string_5));
            ConfigManager.gclass61_0.method_0("LastProfile", string_11);
            if (gclass54_0 != null && gclass54_0.Offsets != null)
            {
                gclass54_0.Offsets = null;
                Logger.LogMessage(MessageProvider.GetMessage(110));
            }

            if (bool_27)
                ginterface0_0.imethod_0();
            return true;
        }

        Logger.LogMessage(MessageProvider.smethod_2(111, string_11));
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
        numbers_string = ConfigManager.gclass61_0.method_2("BarCharacters");
        if (GContext.Main != null)
            GContext.Main.ApplyConfig();
        DebuffsKnown_string = new DebuffDatabase();
        if (bool_29)
        {
            bool_29 = false;
            smethod_8();
        }

        if (CurrentGameClass != null)
            CurrentGameClass.LoadConfig();
        RestStatusMonitor.double_2 = smethod_6(ConfigManager.gclass61_0.method_2("MeleeDistance"));
        RestStatusMonitor.double_3 = smethod_6(ConfigManager.gclass61_0.method_2("RangedDistance"));
        double_0 = smethod_6(ConfigManager.gclass61_0.method_2("AutoAddDistance"));
        SoundPlayer.bool_0 = ConfigManager.gclass61_0.method_5("Silent");
        if (ApplicationStartupMode == AppMode.PGEdit)
            return;
        if (!ConfigManager.gclass61_0.method_5("ListenEnabled"))
        {
            if (gclass79_0 != null)
            {
                Logger.LogMessage(MessageProvider.GetMessage(141));
                gclass79_0.method_1();
                gclass79_0 = null;
            }
        }
        else
        {
            if (gclass79_0 != null && gclass79_0.int_0 != ConfigManager.gclass61_0.method_3("ListenPort"))
            {
                Logger.LogMessage(MessageProvider.smethod_2(142, gclass79_0.int_0));
                gclass79_0.method_1();
                gclass79_0 = null;
            }

            if (gclass79_0 == null)
            {
                gclass79_0 = new RemoteViewerServer();
                Logger.LogMessage(MessageProvider.smethod_2(143, gclass79_0.int_0));
                gclass79_0.method_0();
            }
        }

        string_8 = ConfigManager.gclass61_0.method_2("FriendWhitelist").Split(' ');
        gclass33_0 = new LootRouteParser(ConfigManager.gclass61_0.method_2("LootPattern"));
        GliderUIManager.method_2();
        if (ConfigManager.gclass61_0.method_5("UseHook") && !KeyboardHookManager.bool_0)
        {
            gclass24_0 = new KeyboardHookManager();
        }
        else
        {
            if (gclass24_0 == null || !KeyboardHookManager.bool_0 || ConfigManager.gclass61_0.method_5("UseHook"))
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
        if (ConfigManager.gclass61_0.method_2("ForceVersion") != null)
        {
            WowVersionLabel_string = ConfigManager.gclass61_0.method_2("ForceVersion");
            Logger.LogMessage(MessageProvider.smethod_2(81, WowVersionLabel_string));
        }

        string string_11;
        if (!TryResolveWowInstallPath(out string_11))
        {
            Logger.LogMessage(MessageProvider.GetMessage(83));
            return;
        }

        SomeStringData = string_11;
        var fileName = Path.Combine(SomeStringData, "WoW.exe");
        Logger.smethod_1(MessageProvider.smethod_2(84, fileName));
        var versionInfo = FileVersionInfo.GetVersionInfo(fileName);
        if (versionInfo == null)
        {
            Logger.LogMessage(MessageProvider.smethod_2(85, fileName));
            return;
        }

        if (ConfigManager.gclass61_0.method_2("ForceVersion") != null)
            return;
        WowVersionLabel_string = versionInfo.FileVersion;
        Logger.LogMessage(MessageProvider.smethod_2(86, WowVersionLabel_string));
    }

    private static bool TryResolveWowInstallPath(out string string_11)
    {
        string_11 = null;
        var string_12 = MessageProvider.GetMessage(649);
        RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string_12) ?? Registry.CurrentUser.OpenSubKey(string_12);
        if (registryKey != null)
            try
            {
                foreach (var str in new string[3] { "InstallPath", "Path", "GamePath" })
                {
                    var value = registryKey.GetValue(str);
                    if (value != null)
                    {
                        string_11 = value.ToString();
                        if (!string.IsNullOrEmpty(string_11))
                            break;
                    }
                }
            }
            finally
            {
                registryKey.Close();
            }

        if (string.IsNullOrEmpty(string_11))
        {
            var string_13 = ConfigManager.gclass61_0.method_2("GamePath");
            if (!string.IsNullOrEmpty(string_13))
                string_11 = string_13;
        }

        if (string.IsNullOrEmpty(string_11))
            try
            {
                var processesByName = Process.GetProcessesByName("Wow");
                if (processesByName.Length > 0 && processesByName[0].MainModule != null)
                    string_11 = Path.GetDirectoryName(processesByName[0].MainModule.FileName);
            }
            catch (Exception ex)
            {
                Logger.smethod_1("Unable to query running WoW path: " + ex.Message);
            }

        if (string.IsNullOrEmpty(string_11))
            return false;
        string_11 = string_11.Trim().Trim('"');
        if (!string_11.EndsWith("\\"))
            string_11 += "\\";
        return Directory.Exists(string_11);
    }

    public static void smethod_8()
    {
        var str = ConfigManager.gclass61_0.method_2("CustomClassName");
        if (str.Length == 0)
        {
            var gameClass = (GameClass)ConfigManager.gclass61_0.method_3("Class");
            str = gameClass + ".cs (internal)";
            if (!ProfileMapping.ContainsKey(str))
            {
                Logger.smethod_1("No dynamic class: \"" + str + "\"");
                str = gameClass.ToString();
            }

            Logger.smethod_1("Promoting name to: \"" + str + "\"");
            ConfigManager.gclass61_0.method_0("CustomClassName", str);
        }

        if (!ProfileMapping.ContainsKey(str))
        {
            Logger.LogMessage("!! No class defined for: \"" + str + "\"");
            if (ProfileMapping.ContainsKey(str + ".cs (internal)"))
            {
                str += ".cs (internal)";
                Logger.LogMessage("Promoted to real internal class: " + str);
                ConfigManager.gclass61_0.method_0("CustomClassName", str);
            }
            else
            {
                str = ProfileMapping.Keys[0];
                if (!bool_37)
                    Logger.LogMessage("Switching to: " + str);
            }
        }

        ProfileMapping[str].method_0();
        var object0 = (GGameClass)ProfileMapping[str].object_0;
        if (bool_13)
        {
            Logger.smethod_1("Calling OnAttach for new class");
            object0.OnAttach();
        }

        CurrentStance = GStance.Unknown;
        CurrentGameClass = object0;
        CurrentProfile = ProfileMapping[str];
    }

    public static void smethod_9()
    {
        isInitializationSuccessful = false;
        MemoryOffsetTable.Instance = new MemoryOffsetTable();
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
            Logger.LogMessage(MessageProvider.smethod_2(73, ex.Message + ex.StackTrace));
        }
    }

    private static void smethod_11()
    {
        ApplicationInitializer.InitializeAndValidate(ConfigManager.gclass61_0.method_2("AppKey"), true);
        bool_13 = false;
        if (isInputStringFourCharacters && !bool_23)
        {
            bool_23 = true;
            Logger.LogMessage(MessageProvider.GetMessage(75));
            smethod_39(1000);
        }

        thread_0 = null;
        if (isInitializationSuccessful)
            DebugPrivilegeElevator.smethod_0();
        if (!isInputStringFourCharacters)
        {
            Logger.LogMessage(MessageProvider.GetMessage(76));
            if (isTimeAdded)
                Logger.LogMessage(MessageProvider.smethod_2(77, expiryTime.ToString()));
            if (IsSomeConditionMet)
            {
                CodeCompiler.smethod_12();
                if (GameMemoryWriter != null)
                    GameMemoryWriter.method_7();
                switch (ApplicationInitializer.InitializationCount)
                {
                    case 0:
                        Logger.LogMessage(MessageProvider.GetMessage(846));
                        break;
                    case 1:
                        Logger.LogMessage(MessageProvider.smethod_2(880, ApplicationInitializer.InitializationTime.ToShortDateString()));
                        break;
                    case 2:
                        Logger.LogMessage(MessageProvider.GetMessage(881));
                        break;
                    case 3:
                        Logger.LogMessage(MessageProvider.GetMessage(882));
                        break;
                    case 4:
                        Logger.LogMessage(MessageProvider.GetMessage(883));
                        break;
                }
            }

            bool_19 = false;
            if (ConfigManager.gclass61_0.method_5("AllowWW") && GliderManager != null && GameMemoryReader == null && !IsAttached)
            {
                Logger.LogMessage("Starting Tripwire");
                GameMemoryReader = new WardenMonitor(GliderManager, ConfigManager.gclass61_0.method_5("LogWW"),
                    MemoryOffsetTable.Instance.GetIntOffset("VAPeek"));
            }

            if (IsSomeConditionMet && !bool_37 && !IsAttached)
                bool_38 = true;
            if (GameMemoryWriter != null)
                GameMemoryWriter.method_2("OnGliderStart", false);
        }
        else
        {
            IsSomeConditionMet = false;
            if (IsBetaVersion)
            {
                IsBetaAccessGranted = true;
                Logger.LogMessage(MessageProvider.GetMessage(78));
                smethod_39(2000);
                gclass36_0.method_5();
            }
        }

        if (ApplicationStartupMode == AppMode.Normal)
            Logger.LogMessage(MessageProvider.GetMessage(79));
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
        Logger.smethod_1("--- Attach code in");
        if (IsAttached)
        {
            SoundPlayer.smethod_0("Attach.wav");
            IsDetached = true;
            ginterface0_0.imethod_0();
        }
        else
        {
            IsForegroundEnabled = false;
            gclass36_2.method_4();
            GProcessMemoryManipulator.bool_2 = false;
            gclass43_0 = new OffsetManager("Player", MemoryOffsetTable.Instance.GetIntOffset("D_Player"));
            gclass43_3 = new OffsetManager("Item", MemoryOffsetTable.Instance.GetIntOffset("D_Items"));
            gclass43_1 = new OffsetManager("NPC", MemoryOffsetTable.Instance.GetIntOffset("D_NPC"));
            gclass43_2 = new OffsetManager("Object", MemoryOffsetTable.Instance.GetIntOffset("D_Object"));
            gclass43_4 = new OffsetManager("Container", MemoryOffsetTable.Instance.GetIntOffset("D_Container"));
            gclass63_0 = new SpellbookManager();
            GContext.Main.OnAttach();
            if (CurrentGameClass != null)
                CurrentGameClass.OnAttach();
            UIElement.smethod_2("UIParent");
            smethod_17(1, MessageProvider.GetMessage(98));
            ginterface0_0.imethod_0();
            gclass38_0 = new EquipmentEnchantmentChecker();
            gclass38_0.method_0();
            GameClass69Instance.method_0();
            DialogMonitor.smethod_0();
            GameClass8Instance = UIElement.smethod_2("GameMenuFrame");
            IsGliderPaused = false;
            if (gclass48_0 != null)
                gclass48_0.method_6();
            bool_13 = true;
            SoundPlayer.smethod_0("Attach.wav");
            if (GameMemoryReader != null)
            {
                GameMemoryReader.method_5();
                GameMemoryReader.method_4();
            }
            else
            {
                Logger.smethod_1("No WH present at attach");
            }

            bool_36 = false;
            Logger.smethod_1("--- Attach code out");
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
        Logger.smethod_1("AppContext.Detach invoked");
        if (int_12 == 0 && !GProcessMemoryManipulator.smethod_56(AnotherIntegerValue))
        {
            GProcessMemoryManipulator.CloseProcessHandle(AdditionalApplicationHandle);
            AdditionalApplicationHandle = IntPtr.Zero;
            AnotherIntegerValue = 0;
        }

        bool_13 = false;
        GameClass69Instance.method_3();
        GameClass8Instance = null;
        smethod_17(1, MessageProvider.GetMessage(99));
    }

    public static void smethod_16(int int_14)
    {
        if (int_10 == int_14)
            return;
        int_10 = int_14;
        if (!gclass11_0.method_2(int_14))
        {
            Logger.LogMessage(MessageProvider.smethod_2(91, gclass11_0.string_0));
            Logger.LogMessage(MessageProvider.GetMessage(92));
        }
        else
        {
            Logger.LogMessage(MessageProvider.GetMessage(93));
            if (gclass11_0.string_1 == null)
                return;
            Logger.LogMessage(MessageProvider.smethod_2(94, gclass11_0.string_1));
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
        Logger.LogMessage(MessageProvider.GetMessage(140));
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
                            InputController.smethod_9(short_0);
                            index = num;
                        }
                        catch (Exception ex)
                        {
                            Logger.smethod_1(MessageProvider.smethod_2(144, string_11));
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
                    InputController.smethod_9(13);
                    Thread.Sleep(700);
                }
            }

            if (flag)
                InputController.smethod_6(char_0);
        }
    }

    public static bool smethod_21(bool bool_42)
    {
        if (!bool_27)
            return false;
        if (bool_19)
        {
            Logger.LogMessage(MessageProvider.GetMessage(113));
            return false;
        }

        if (glideMode_0 != GlideMode.None)
        {
            Logger.LogMessage(MessageProvider.GetMessage(114));
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
            Logger.LogMessage(MessageProvider.GetMessage(115));
            return false;
        }

        if (!ConfigManager.gclass61_0.method_5("BackgroundEnable"))
            smethod_22();
        glideMode_0 = GlideMode.Manual;
        gclass60_0 = new GlideMainThread();
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
            Logger.LogMessage(MessageProvider.GetMessage(107));
            return false;
        }

        if (gprofile_0 == null)
        {
            Logger.LogMessage(MessageProvider.GetMessage(108));
            return false;
        }

        var flag = false;
        switch (genum2_0)
        {
            case WaypointType.const_0:
                flag = GPlayerSelf.Me.IsDead;
                break;
            case WaypointType.const_1:
                flag = false;
                break;
            case WaypointType.const_2:
                flag = true;
                break;
        }

        if (genum2_0 == WaypointType.const_3)
        {
            gprofile_0.VendorWaypoints.Add(GPlayerSelf.Me.Location);
            Logger.LogMessage(MessageProvider.smethod_2(870, gprofile_0.VendorWaypoints.Count));
        }
        else if (!flag)
        {
            gprofile_0.Waypoints.Add(GPlayerSelf.Me.Location);
            Logger.LogMessage(MessageProvider.smethod_2(658, gprofile_0.Waypoints.Count));
        }
        else
        {
            gprofile_0.GhostWaypoints.Add(GPlayerSelf.Me.Location);
            Logger.LogMessage(MessageProvider.smethod_2(659, gprofile_0.GhostWaypoints.Count));
        }

        bool_16 = true;
        return true;
    }

    public static bool smethod_24(bool bool_42)
    {
        if (!bool_27)
            return false;
        if (isTimeAdded && DateTime.Now > expiryTime)
        {
            Logger.LogMessage(MessageProvider.GetMessage(116));
            return false;
        }

        if (ConfigManager.gclass61_0.method_5("AllowNetCheck") && !new NetworkSafetyChecker().ValidateNetworkSafety(true))
            return false;
        if (glideMode_0 != GlideMode.None)
        {
            Logger.LogMessage(MessageProvider.GetMessage(117));
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
            Logger.LogMessage(MessageProvider.GetMessage(118));
            return false;
        }

        if (IsDetached)
            return smethod_25();
        if (gclass48_0 == null && (gprofile_0 == null || (gprofile_0.Waypoints.Count < 2 && !gprofile_0.Fishing)))
        {
            Logger.LogMessage(MessageProvider.GetMessage(119));
            return false;
        }

        if (GPlayerSelf.Me.IsDead &&
            (gprofile_0.GhostWaypoints.Count == 0 || !ConfigManager.gclass61_0.method_5("Resurrect")))
        {
            Logger.LogMessage(MessageProvider.GetMessage(120));
            return false;
        }

        if (!IsGliderInitialized)
            smethod_22();
        glideMode_0 = GlideMode.Auto;
        gclass73_0 = new CombatController();
        if (gclass73_0.method_1())
            return true;
        glideMode_0 = GlideMode.None;
        return false;
    }

    private static bool smethod_25()
    {
        GameProcessManager = new MachGlideRunner();
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
            Logger.LogMessage(MessageProvider.GetMessage(130));
            return false;
        }

        if (GPlayerSelf.Me.Target == null)
        {
            Logger.LogMessage(MessageProvider.GetMessage(131));
            return false;
        }

        if (GPlayerSelf.Me.Target.IsPlayer)
        {
            Logger.LogMessage(MessageProvider.GetMessage(132));
            return false;
        }

        if (gprofile_0 == null)
        {
            Logger.LogMessage(MessageProvider.GetMessage(133));
            return false;
        }

        if (gprofile_0.CheckFaction(GPlayerSelf.Me.Target.FactionID, true))
        {
            Logger.LogMessage(MessageProvider.GetMessage(128));
        }
        else
        {
            Logger.LogMessage(MessageProvider.GetMessage(129));
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
            Logger.LogMessage("! Exception in KillAction: " + ex.Message + ex.StackTrace);
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
        lock (killActionLock)
        {
            var flag = false;
            if (glideMode_0 != GlideMode.None || bool_13 && bool_42)
            {
                smethod_51();
                gclass68_0.method_3(true);
                Logger.smethod_1(MessageProvider.smethod_2(652, bool_42, (int)glideMode_0, string_11));
                gclass68_0.method_3(true);
                InputController.smethod_21(false);
                if (glideMode_0 == GlideMode.Auto)
                {
                    if (bool_42)
                        bool_36 = true;
                    if (CurrentGameClass != null)
                        CurrentGameClass.OnStopGlide();
                    smethod_17(1, MessageProvider.GetMessage(100));
                    if (IsAttached)
                    {
                        var gameProcessManager = GameProcessManager;
                        if (gameProcessManager != null && Thread.CurrentThread == gameProcessManager.thread_0)
                            flag = true;
                    }
                    else
                    {
                        var combatController = gclass73_0;
                        if (combatController != null && Thread.CurrentThread == combatController.thread_0)
                            flag = true;
                    }

                    Logger.smethod_1(MessageProvider.GetMessage(100));
                    glideMode_0 = GlideMode.None;
                    if (IsAttached)
                    {
                        var gameProcessManager = GameProcessManager;
                        GameProcessManager = null;
                        if (gameProcessManager != null)
                            gameProcessManager.method_1();
                    }
                    else
                    {
                        var combatController = gclass73_0;
                        gclass73_0 = null;
                        if (combatController != null)
                            combatController.method_2();
                    }
                }

                if (glideMode_0 == GlideMode.Manual)
                {
                    smethod_17(1, MessageProvider.GetMessage(101));
                    if (gclass60_0 != null && Thread.CurrentThread == gclass60_0.thread_0)
                        flag = true;
                    Logger.smethod_1(MessageProvider.GetMessage(102));
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
        CodeCompiler.smethod_4();
        GliderUIManager.method_5();
    }

    private static void smethod_32()
    {
        var string_11 = smethod_36("/kill");
        intptr_2 = CreateEvent(IntPtr.Zero, false, false, string_11);
        if (intptr_2 == IntPtr.Zero)
        {
            Logger.LogMessage("Couldn't create named event");
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
        SoundPlayer.smethod_1("GliderExit.wav");
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
            Logger.LogMessage(MessageProvider.GetMessage(759));
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

    public static void smethod_37(WardenCheckStatus genum0_0)
    {
        Logger.LogMessage("StopOnTW invoked, result = " + (int)genum0_0);
        if (genum0_0 == WardenCheckStatus.const_2)
            File.WriteAllText("TWfail.txt", "guh!");
        if (genum0_0 == WardenCheckStatus.const_1)
            File.WriteAllText("TWunsafe.txt", "guh!");
        if (genum0_0 == WardenCheckStatus.const_3)
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
            var gclass3 = new NetworkSafetyChecker();
            if (ConfigManager.gclass61_0.method_5("AllowNetCheck"))
                gclass3.ValidateNetworkSafety(false);
        }

        if (SpellCooldownTimer.IsReady)
        {
            SpellCooldownTimer.Reset();
            GProcessMemoryManipulator.bool_3 = GProcessMemoryManipulator.IsWowProcessRunning();
            GProcessMemoryManipulator.GetProcessId();
        }

        if (bool_38 && !IsAttached && !bool_37)
        {
            bool_37 = true;
            CodeCompiler.smethod_14();
            smethod_8();
        }

        if (!bool_13)
            return;

        GObjectList.GetObjects();
        var me = GPlayerSelf.Me;
        if (me == null)
            return;

        if (DebuffsKnown_string != null && gclass36_3.method_3())
        {
            gclass36_3.method_4();
            DebuffsKnown_string.method_8();
        }

        if (me.Stance != CurrentStance)
        {
            if (CurrentStance != GStance.Unknown)
                GContext.Main.Interface.UnFillAllKeys();
            CurrentStance = me.Stance;
        }

        GameClass69Instance.method_4();
        DialogMonitor.smethod_1();
        if (GameClass8Instance != null && GameClass8Instance.method_10() && glideMode_0 == GlideMode.Auto)
            InputController.smethod_9(27);
        if (glideMode_0 == GlideMode.Auto && IsGliderInitialized && ConfigManager.gclass61_0.method_2("BackgroundDisplay") != "Normal" &&
            (DateTime.Now - dateTime_0).TotalSeconds >= 8.0 && !IsGliderRunning)
        {
            IsGliderRunning = true;
            smethod_46();
        }

        gclass68_0.method_7();
        InputController.smethod_21(true);
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
            Logger.LogMessage(MessageProvider.GetMessage(830));
            if (glideMode_0 == GlideMode.Auto)
                gprofile_0.ForceBlacklist(gunit_0.GUID);
            CombatController.smethod_1();
            return true;
        }

        Logger.LogMessage(MessageProvider.GetMessage(517));
        if (ConfigManager.gclass61_0.method_5("StopOnVanish"))
        {
            GContext.Main.Movement.LookConfused();
            SoundPlayer.smethod_0("GMWhisper.wav");
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
        AnotherIntegerValue = GProcessMemoryManipulator.AttachToWowProcess();
        if (AnotherIntegerValue == 0)
        {
            if (!bool_35)
                Logger.smethod_1("Attach attempt: no matching process found for AttachEXE");
            bool_35 = true;
            return false;
        }

        bool_35 = false;

        IsGliderAttached = true;
        if (AdditionalApplicationHandle == IntPtr.Zero && !bool_14)
        {
            AdditionalApplicationHandle = GProcessMemoryManipulator.OpenProcessHandle(AnotherIntegerValue);
            if (AdditionalApplicationHandle == IntPtr.Zero)
            {
                if (!IsGliderPaused)
                {
                    IsGliderPaused = true;
                    Logger.LogMessage(MessageProvider.smethod_2(96, Marshal.GetLastWin32Error()));
                }

                return false;
            }

            GProcessMemoryManipulator.bool_3 = GProcessMemoryManipulator.IsWowProcessRunning();
            if (GliderManager != null && !GliderManager.method_26(AnotherIntegerValue))
            {
                Logger.LogMessage(
                    "Some other Glider is already open on that game, maybe we'll attach to some other one");
                CloseHandle(AdditionalApplicationHandle);
                AdditionalApplicationHandle = IntPtr.Zero;
                GProcessMemoryManipulator.SetProcessId(AnotherIntegerValue);
                AnotherIntegerValue = 0;
                return false;
            }

            GProcessMemoryManipulator.GetProcessId();
            if (GameMemoryWriter != null)
                GameMemoryWriter.method_2("OnGameFirstSeen", false);
        }

        if (IsAttached)
            return true;
        var int_14 = MemoryOffsetTable.Instance.GetIntOffset("UIParent");
        if (int_14 > 0 && GProcessMemoryManipulator.ReadInt32(int_14, "probeuip") == 0 && !bool_20 &&
            ((bool_31 && gspellTimer_1.IsReady) || IsForegroundEnabled))
        {
            var str = ConfigManager.gclass61_0.method_2("AutoLog");
            if (str != null && str.Length > 0 && IsSomeConditionMet && new AutoLoginManager().method_2())
            {
                bool_20 = true;
                GameMemoryWriter.method_2("DoAutoLog", false);
            }

            Logger.smethod_1("Attach probe note: UIParent resolved to zero, continuing with TLS/static attach checks");
        }

        if (MemoryOffsetTable.Instance.HasOffset("TLSSlot") && MemoryOffsetTable.Instance.GetIntOffset("TLSSlot") > 0)
        {
            if (GProcessMemoryManipulator.smethod_52(out long_0, out int_5) && long_0 != 0L)
            {
                if (GObjectList.StealthCountGameObjects(long_0) > 0)
                    return true;
                Logger.smethod_1("TLS attach probe failed object validation, trying static offsets fallback");
            }
            else
            {
                Logger.smethod_1("TLS attach probe failed, trying static offsets fallback");
            }
        }
        long_0 = 0L;
        var int_18 = GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("MainTable"), "MainTable");
        var int_19 = int_18;
        if (MemoryOffsetTable.Instance.HasOffset("MainTableProbe") && MemoryOffsetTable.Instance.GetIntOffset("MainTableProbe") > 0)
        {
            var int_20 = MemoryOffsetTable.Instance.GetIntOffset("MainTableProbe");
            var int_21 = GProcessMemoryManipulator.ReadInt32(int_18 + int_20, "MainTableProbe");
            var int_22 = int_18 + int_20;
            if (smethod_62(int_21))
                int_19 = int_21;
            else if (smethod_62(int_22))
                int_19 = int_22;
            else
                int_19 = int_21;
        }
        int_5 = int_19;
        if (int_5 == 0)
        {
            Logger.smethod_1("Attach probe failed: resolved MainTable pointer is zero");
            return false;
        }

        var bool_42 = false;
        var bool_43 = false;
        var int_23 = MemoryOffsetTable.Instance.HasOffset("MainTableActivePlayer")
            ? MemoryOffsetTable.Instance.GetIntOffset("MainTableActivePlayer")
            : 24;
        if (int_23 > 0)
        {
            var int_24 = GProcessMemoryManipulator.ReadInt32(int_5 + int_23, "MainTableActivePlayer");
            if (isLikelyObjectAddress(int_24))
            {
                var int64_1 = GProcessMemoryManipulator.ReadInt64(int_24 + 48, "MainTableActivePlayerGuid");
                if (int64_1 != 0L)
                {
                    long_0 = int64_1;
                    bool_42 = true;
                    bool_43 = true;
                    Logger.smethod_1("Attach probe: using active player object GUID = 0x" + long_0.ToString("x"));
                }
            }
        }

        var int_25 = MemoryOffsetTable.Instance.HasOffset("MainTableLocalGuid")
            ? MemoryOffsetTable.Instance.GetIntOffset("MainTableLocalGuid")
            : 40;
        if (!bool_42 && int_25 > 0)
        {
            var int64_2 = GProcessMemoryManipulator.ReadInt64(int_5 + int_25, "MainTableLocalGuid");
            if (int64_2 != 0L)
            {
                long_0 = int64_2;
                bool_43 = true;
                Logger.smethod_1("Attach probe: using object manager local GUID = 0x" + long_0.ToString("x"));
            }
        }

        if (long_0 == 0L)
        {
            var configuredPlayerIdAddress = MemoryOffsetTable.Instance.GetIntOffset("PlayerIdAddr");
            var playerIdCandidates = new int[]
            {
                configuredPlayerIdAddress,
                0x00CD87A8,
                0x00BD07A8
            };
            for (var candidateIndex = 0; candidateIndex < playerIdCandidates.Length; ++candidateIndex)
            {
                var candidateAddress = playerIdCandidates[candidateIndex];
                if (candidateAddress == 0)
                    continue;

                var playerGuidBytes = GProcessMemoryManipulator.ReadBytesRaw(candidateAddress, 8);
                if (playerGuidBytes == null)
                    continue;

                var playerGuid = BitConverter.ToInt64(playerGuidBytes, 0);
                if (playerGuid != 0L)
                {
                    long_0 = playerGuid;
                    if (candidateAddress != configuredPlayerIdAddress)
                        Logger.smethod_1("Attach probe: using fallback PlayerIdAddr 0x" + candidateAddress.ToString("x"));
                    break;
                }
            }

            if (long_0 == 0L)
            {
                Logger.smethod_1("Attach probe failed: Player GUID is zero across local and static sources");
                return false;
            }
        }

        var num = GObjectList.StealthCountGameObjects(long_0);
        if (num > 0)
            return true;
        long long_1;
        if (GObjectList.TryGetLikelyPlayerGuid(out long_1))
        {
            if (bool_43 && long_1 <= 4096L)
            {
                Logger.smethod_1("Attach probe note: ignoring low inferred GUID candidate because object manager GUID is already known");
                return true;
            }
            long_0 = long_1;
            Logger.smethod_1("Attach probe: inferred player GUID from object list = 0x" + long_0.ToString("x"));
            num = GObjectList.StealthCountGameObjects(long_0);
            if (num > 1)
                return true;
        }

        Logger.smethod_1("Attach probe failed: object validation count too low = " + num);
        return false;
    }

    private static bool isLikelyObjectAddress(int objectAddress)
    {
        if ((objectAddress & 1) != 0 || objectAddress == 0 || objectAddress == 28 || objectAddress < 65536)
            return false;
        var objectTypeBytes = GProcessMemoryManipulator.ReadBytesRaw(objectAddress + 20, 4);
        if (objectTypeBytes == null || objectTypeBytes.Length < 4)
            return false;
        var objectType = BitConverter.ToInt32(objectTypeBytes, 0);
        return objectType >= 1 && objectType <= 7;
    }

    private static bool smethod_62(int int_14)
    {
        if (int_14 == 0)
            return false;
        var firstObjectAddress = GProcessMemoryManipulator.ReadInt32(int_14 + MemoryOffsetTable.Instance.GetIntOffset("InitialOffset"), "MainTableFirstProbe");
        if ((firstObjectAddress & 1) != 0 || firstObjectAddress == 0 || firstObjectAddress == 28 || firstObjectAddress < 65536)
            return false;
        var objectType = GProcessMemoryManipulator.ReadInt32(firstObjectAddress + 20, "MainTableFirstTypeProbe");
        return objectType >= 1 && objectType <= 7;
    }

    public static void smethod_45()
    {
        if (bool_13 || IsDetached || !isInitializationSuccessful || !smethod_44())
            return;
        smethod_14();
    }

    public static void smethod_46()
    {
        switch (ConfigManager.gclass61_0.method_2("BackgroundDisplay"))
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
        GProcessMemoryManipulator.SetForegroundWindow(MainApplicationHandle);
        bool_40 = true;
    }

    public static void smethod_48()
    {
        if (bool_41)
            return;
        double width = ConfigManager.gclass61_0.method_3("ShrinkWidth");
        GProcessMemoryManipulator.GetWindowSize(MainApplicationHandle, out size_0);
        var height = size_0.Height / (double)size_0.Width * width;
        GProcessMemoryManipulator.SetWindowSize(MainApplicationHandle, new Size((int)width, (int)height));
        bool_41 = true;
    }

    public static void smethod_49()
    {
        if (!bool_40)
            return;
        GProcessMemoryManipulator.ShowWindow(MainApplicationHandle);
        bool_40 = false;
    }

    public static void smethod_50()
    {
        if (!bool_41)
            return;
        GProcessMemoryManipulator.SetWindowSize(MainApplicationHandle, size_0);
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
        ConfigManager.gclass61_0.method_0("LName", string_4);
        ConfigManager.gclass61_0.method_8();
    }

    private static void smethod_53()
    {
        if (Environment.CommandLine.ToLower().IndexOf("/driver") != -1)
        {
            SoundPlayer.smethod_1("Kill.wav");
            GliderManager = new WardenProtocol(smethod_36("/driver"));
            Logger.smethod_1("Sending data to shadow driver");
            if (!GliderManager.method_38())
            {
                if (MessageBox.Show(null, MessageProvider.GetMessage(862), "Glider", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Hand) == DialogResult.Yes)
                    Help.ShowHelp(null, "Glider.chm", HelpNavigator.Topic, "ShadowFailed.html");
                bool_30 = false;
            }
            else
            {
                Logger.LogMessage("Shadow confirmed, looks awake");
            }

            if (Environment.CommandLine.ToLower().IndexOf("/holddriver") != -1)
            {
                Logger.smethod_1("DriverName is static, will leave driver resident");
                bool_33 = true;
            }
        }
        else
        {
            Logger.LogMessage(MessageProvider.GetMessage(877));
        }

        Environment.CommandLine.ToLower().IndexOf("/shadowread");
        if (Environment.CommandLine.ToLower().IndexOf("/attachpid") != -1)
        {
            int_12 = int.Parse(smethod_36("/attachpid"));
            Logger.LogMessage("/attachpid specified, looking for: " + int_12);
        }

        if (!ConfigManager.gclass61_0.method_5("UnloadShadow") || GliderManager == null)
            return;
        GliderManager.method_11();
        GliderManager = null;
    }

    private static void smethod_54()
    {
        var str = ConfigManager.gclass61_0.method_2("ForceVersion");
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
        if (ConfigManager.gclass61_0.method_5("AllowWW") || MessageBox.Show(null,
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
            Logger.LogMessage(MessageProvider.GetMessage(868));
            CombatController.smethod_0(869);
        }
        else
        {
            var string_1 = ConfigManager.gclass61_0.method_2("AutoLog");
            if (!new AutoLoginManager().method_1(string_1))
                return;
            string_9 = string_1;
            Logger.smethod_1("Autolog is good!");
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
            ConfigManager.gclass61_0.method_0("LastSecCheck", DateTime.Now.ToShortDateString());
            ConfigManager.gclass61_0.method_8();
        }
    }

    public static void smethod_59()
    {
        if (!ConfigManager.gclass61_0.method_5("AllowAutoSecCheck"))
            return;
        if (ConfigManager.gclass61_0.method_2("LastSecCheck") == null)
        {
            ConfigManager.gclass61_0.method_0("LastSecCheck", DateTime.Now.ToShortDateString());
        }
        else
        {
            if ((DateTime.Now - DateTime.Parse(ConfigManager.gclass61_0.method_2("LastSecCheck"))).TotalDays < 7.0)
                return;
            smethod_60();
        }
    }

    private static void smethod_60()
    {
        if (MessageBox.Show(MainForm, MessageProvider.GetMessage(875), GProcessMemoryManipulator.GenerateRandomString(), MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation) != DialogResult.Yes)
            return;
        smethod_58();
    }

    private static void smethod_61()
    {
        IsSecCheckEnabled = true;
        if (int.Parse("1.8.0".Replace(".", "")) >= InitializationCount)
            return;
        if (ConfigManager.gclass61_0.method_5("NoVersionPop"))
        {
            Logger.LogMessage("A new version of Glider is available for download.");
        }
        else
        {
            if (MessageBox.Show(MainForm, MessageProvider.GetMessage(876), GProcessMemoryManipulator.GenerateRandomString(), MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;
            Process.Start("http://www.mmoglider.com/Download.aspx?Update=True");
        }
    }

    public static void smethod_62()
    {
        if (!IsGliderInitialized && ConfigManager.gclass61_0.method_5("BackgroundEnable") && GliderManager != null && IsSomeConditionMet)
        {
            Logger.smethod_1("Setting up bg stuff");
            MainApplicationHandle = GProcessMemoryManipulator.OpenProcessWithAccess(AnotherIntegerValue);
            GliderManager.method_34(AnotherIntegerValue, MainApplicationHandle);
            IsGliderInitialized = true;
        }
        else
        {
            Logger.smethod_1("No bg stuff setup");
        }
    }

    public static string smethod_63(int int_14)
    {
        var num1 = MemoryOffsetTable.Instance.GetIntOffset("MacroBase");
        var num2 = GProcessMemoryManipulator.ReadInt32(num1 + 36, "mbase");
        int int_29_1;
        for (var int_29_2 =
                 GProcessMemoryManipulator.ReadInt32(GProcessMemoryManipulator.ReadInt32(num1 + 28, "mbase2") + 12 * (int_14 & num2) + 8, "mbase3");
             int_29_2 > 0 && (int_29_2 & 1) == 0;
             int_29_2 = GProcessMemoryManipulator.ReadInt32(int_29_2 + GProcessMemoryManipulator.ReadInt32(int_29_1, "mnext3") + 4, "mnext4"))
        {
            var num3 = GProcessMemoryManipulator.ReadInt32(int_29_2, "mstep");
            var str = GProcessMemoryManipulator.ReadString(int_29_2 + 36, 64, "mname");
            if (num3 == int_14)
                return str;
            int_29_1 = GProcessMemoryManipulator.ReadInt32(num1 + 28, "mnext1") +
                       12 * (GProcessMemoryManipulator.ReadInt32(num1 + 36, "mnext2") & int_14);
        }

        return "(could not find macro in list!)";
    }
}