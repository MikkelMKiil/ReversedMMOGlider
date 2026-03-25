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
    public static ScriptExecutor GameMemoryWriter;
    public static bool IsExitRequested;
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
    public static bool IsRuntimeAttached;
    public static GProfile ActiveProfile;
    public static string ActiveProfilePath;
    public static bool SkipHandleOpen = false;
    public static CameraRotator CameraController;
    public static OffsetManager PlayerOffsetManager;
    public static OffsetManager NpcOffsetManager;
    public static OffsetManager ObjectOffsetManager;
    public static OffsetManager ItemOffsetManager;
    public static OffsetManager ContainerOffsetManager;
    public static int ProfileEditorVersion;
    public static long CurrentPlayerGuid;
    public static int ResolvedMainTableAddress;
    public static SortedList RuntimeProfileCache = new SortedList();
    public static bool IsStartupPending = true;
    public static int StartupAttemptCount = 1;
    public static DateTime SessionStartTime;
    public static string WowVersionLabel = "";
    public static bool IsProfileDirty;
    public static bool IsBetaVersion = false;
    public static bool IsBetaAccessGranted;
    public static GameTimer LicenseCheckTimer;
    public static GlideMode CurrentGlideMode;
    public static int DynamicClassCount;
    public static int CompiledClassCount;
    public static int InternalClassCount;
    public static bool HasClassLoadMismatch;
    public static Thread InitializationThread;
    public static PartyManager PartyStateManager;
    public static bool IsAutoLoginTriggered;
    public static bool HasSessionWarning;
    public static GameTimer SessionHeartbeatTimer;
    public static KeyboardHookManager KeyboardHook;
    public static SecurityDescriptorHelper SecurityDescriptor;
    public static bool isInitializationSuccessful;
    public static RemoteViewerServer RemoteViewer;
    public static GameClass SelectedGameClass;
    public static bool HasShownStartupNotice;
    public static bool isInputStringFourCharacters = true;
    public static DateTime expiryTime;
    public static bool isTimeAdded;
    public static bool HasAppliedTimeExtension = false;
    public static ILogger StartupLogger;
    public static bool IsInitializationComplete;
    public static int LastSecurityCode;
    public static CombatController ActiveCombatController;
    public static byte[] RuntimePayloadBuffer = null;
    public static bool HasQueuedPayload = false;
    public static GlideMainThread ManualGlideController;
    public static string ActionBarCharacters = "1234567890-=";
    public static int CachedRatePerHour;
    public static WaypointType SelectedWaypointType = WaypointType.const_0;
    public static bool RequiresConfigReload;
    public static EquipmentEnchantmentChecker EquipmentEnchantmentChecker;
    public static double AutoAddDistance;
    public static DebuffDatabase KnownDebuffs;
    public static string[] FriendWhitelist;
    public static ProfileGroupManager ProfileGroupStateManager;
    public static int AttachAttemptCount;
    public static Random RandomGenerator;
    public static LootRouteParser LootRoutePattern;
    public static GameTimer AttachRefreshTimer = new GameTimer(6000);
    public static bool IsResumeRequested;
    public static bool IsAutoLoginArmed = false;
    public static GSpellTimer AutoLoginTimer = null;
    public static bool IsDetachInProgress;
    public static string AutoLoginSetting;
    public static bool HasManualPause;
    public static bool IsDiagnosticLoggingEnabled;
    public static bool HasLoggedMissingProcess;
    public static bool DetachAfterStopRequested;
    public static bool HasDeferredCompileRun;
    public static bool NeedsDeferredCompile;
    public static SpellbookManager SpellbookStateManager;
    private static readonly object killActionLock = new object();
    private static int KillActionDepth;
    public static IntPtr KillEventHandle = IntPtr.Zero;
    public static bool ShouldProcessKillEvent = true;
    public static uint KillEventWaitTimeout = uint.MaxValue;
    private static readonly GameTimer DebuffRefreshTimer = new GameTimer(3500);
    private static GameTimer ForegroundRefreshTimer = new GameTimer(6000);
    private static readonly GSpellTimer PeriodicSafetyTimer = new GSpellTimer(1080000, true);
    private static int MainLoopTickGuard;
    private static int LastMainLoopTickTime;
    private static bool IsWorldUiReady;
    private const int MinMainLoopTickIntervalMs = 75;
    private const int WotlkClientConnectionAddress = 0x00C79CE0;
    private const int WotlkObjectManagerOffset = 0x2ED0;
    private const int WotlkObjectManagerLocalGuidOffset = 0xC0;
    private const int WotlkStaticPlayerGuidAddress = 0x00CA1238;
    private static string PendingErrorMessage = null;
    private static Size OriginalWindowSize;
    public static bool IsWindowHidden;
    public static bool IsWindowShrunk;

    public static bool bool_13 { get { return IsRuntimeAttached; } set { IsRuntimeAttached = value; } }
    public static GProfile gprofile_0 { get { return ActiveProfile; } set { ActiveProfile = value; } }
    public static string string_5 { get { return ActiveProfilePath; } set { ActiveProfilePath = value; } }
    public static bool bool_14 { get { return SkipHandleOpen; } set { SkipHandleOpen = value; } }
    public static CameraRotator gclass68_0 { get { return CameraController; } set { CameraController = value; } }
    public static OffsetManager gclass43_0 { get { return PlayerOffsetManager; } set { PlayerOffsetManager = value; } }
    public static OffsetManager gclass43_1 { get { return NpcOffsetManager; } set { NpcOffsetManager = value; } }
    public static OffsetManager gclass43_2 { get { return ObjectOffsetManager; } set { ObjectOffsetManager = value; } }
    public static OffsetManager gclass43_3 { get { return ItemOffsetManager; } set { ItemOffsetManager = value; } }
    public static OffsetManager gclass43_4 { get { return ContainerOffsetManager; } set { ContainerOffsetManager = value; } }
    public static int int_4 { get { return ProfileEditorVersion; } set { ProfileEditorVersion = value; } }
    public static long long_0 { get { return CurrentPlayerGuid; } set { CurrentPlayerGuid = value; } }
    public static int int_5 { get { return ResolvedMainTableAddress; } set { ResolvedMainTableAddress = value; } }
    public static SortedList sortedList_2 { get { return RuntimeProfileCache; } set { RuntimeProfileCache = value; } }
    public static bool bool_15 { get { return IsStartupPending; } set { IsStartupPending = value; } }
    public static int int_6 { get { return StartupAttemptCount; } set { StartupAttemptCount = value; } }
    public static DateTime dateTime_0 { get { return SessionStartTime; } set { SessionStartTime = value; } }
    public static string WowVersionLabel_string { get { return WowVersionLabel; } set { WowVersionLabel = value; } }
    public static bool bool_16 { get { return IsProfileDirty; } set { IsProfileDirty = value; } }
    public static GameTimer gclass36_0 { get { return LicenseCheckTimer; } set { LicenseCheckTimer = value; } }
    public static GlideMode glideMode_0 { get { return CurrentGlideMode; } set { CurrentGlideMode = value; } }
    public static int int_7 { get { return DynamicClassCount; } set { DynamicClassCount = value; } }
    public static int int_8 { get { return CompiledClassCount; } set { CompiledClassCount = value; } }
    public static int int_9 { get { return InternalClassCount; } set { InternalClassCount = value; } }
    public static bool bool_19 { get { return HasClassLoadMismatch; } set { HasClassLoadMismatch = value; } }
    public static Thread thread_0 { get { return InitializationThread; } set { InitializationThread = value; } }
    public static PartyManager gclass54_0 { get { return PartyStateManager; } set { PartyStateManager = value; } }
    public static bool bool_20 { get { return IsAutoLoginTriggered; } set { IsAutoLoginTriggered = value; } }
    public static bool bool_21 { get { return HasSessionWarning; } set { HasSessionWarning = value; } }
    public static GameTimer gclass36_1 { get { return SessionHeartbeatTimer; } set { SessionHeartbeatTimer = value; } }
    public static KeyboardHookManager gclass24_0 { get { return KeyboardHook; } set { KeyboardHook = value; } }
    public static SecurityDescriptorHelper gclass11_0 { get { return SecurityDescriptor; } set { SecurityDescriptor = value; } }
    public static RemoteViewerServer gclass79_0 { get { return RemoteViewer; } set { RemoteViewer = value; } }
    public static GameClass gameClass_0 { get { return SelectedGameClass; } set { SelectedGameClass = value; } }
    public static bool bool_23 { get { return HasShownStartupNotice; } set { HasShownStartupNotice = value; } }
    public static bool bool_26 { get { return HasAppliedTimeExtension; } set { HasAppliedTimeExtension = value; } }
    public static ILogger ginterface0_0 { get { return StartupLogger; } set { StartupLogger = value; } }
    public static bool bool_27 { get { return IsInitializationComplete; } set { IsInitializationComplete = value; } }
    public static int int_10 { get { return LastSecurityCode; } set { LastSecurityCode = value; } }
    public static CombatController gclass73_0 { get { return ActiveCombatController; } set { ActiveCombatController = value; } }
    public static byte[] byte_0 { get { return RuntimePayloadBuffer; } set { RuntimePayloadBuffer = value; } }
    public static bool bool_28 { get { return HasQueuedPayload; } set { HasQueuedPayload = value; } }
    public static GlideMainThread gclass60_0 { get { return ManualGlideController; } set { ManualGlideController = value; } }
    public static string numbers_string { get { return ActionBarCharacters; } set { ActionBarCharacters = value; } }
    public static int int_11 { get { return CachedRatePerHour; } set { CachedRatePerHour = value; } }
    public static WaypointType genum2_0 { get { return SelectedWaypointType; } set { SelectedWaypointType = value; } }
    public static bool bool_29 { get { return RequiresConfigReload; } set { RequiresConfigReload = value; } }
    public static EquipmentEnchantmentChecker gclass38_0 { get { return EquipmentEnchantmentChecker; } set { EquipmentEnchantmentChecker = value; } }
    public static double double_0 { get { return AutoAddDistance; } set { AutoAddDistance = value; } }
    public static DebuffDatabase DebuffsKnown_string { get { return KnownDebuffs; } set { KnownDebuffs = value; } }
    public static string[] string_8 { get { return FriendWhitelist; } set { FriendWhitelist = value; } }
    public static ProfileGroupManager gclass48_0 { get { return ProfileGroupStateManager; } set { ProfileGroupStateManager = value; } }
    public static int int_12 { get { return AttachAttemptCount; } set { AttachAttemptCount = value; } }
    public static Random random_0 { get { return RandomGenerator; } set { RandomGenerator = value; } }
    public static LootRouteParser gclass33_0 { get { return LootRoutePattern; } set { LootRoutePattern = value; } }
    public static GameTimer gclass36_2 { get { return AttachRefreshTimer; } set { AttachRefreshTimer = value; } }
    public static bool bool_30 { get { return IsResumeRequested; } set { IsResumeRequested = value; } }
    public static bool bool_31 { get { return IsAutoLoginArmed; } set { IsAutoLoginArmed = value; } }
    public static GSpellTimer gspellTimer_1 { get { return AutoLoginTimer; } set { AutoLoginTimer = value; } }
    public static bool bool_32 { get { return IsDetachInProgress; } set { IsDetachInProgress = value; } }
    public static string string_9 { get { return AutoLoginSetting; } set { AutoLoginSetting = value; } }
    public static bool bool_33 { get { return HasManualPause; } set { HasManualPause = value; } }
    public static bool bool_34 { get { return IsDiagnosticLoggingEnabled; } set { IsDiagnosticLoggingEnabled = value; } }
    public static bool bool_35 { get { return HasLoggedMissingProcess; } set { HasLoggedMissingProcess = value; } }
    public static bool bool_36 { get { return DetachAfterStopRequested; } set { DetachAfterStopRequested = value; } }
    public static bool bool_37 { get { return HasDeferredCompileRun; } set { HasDeferredCompileRun = value; } }
    public static bool bool_38 { get { return NeedsDeferredCompile; } set { NeedsDeferredCompile = value; } }
    public static SpellbookManager gclass63_0 { get { return SpellbookStateManager; } set { SpellbookStateManager = value; } }
    public static IntPtr intptr_2 { get { return KillEventHandle; } set { KillEventHandle = value; } }
    public static bool bool_39 { get { return ShouldProcessKillEvent; } set { ShouldProcessKillEvent = value; } }
    public static uint uint_0 { get { return KillEventWaitTimeout; } set { KillEventWaitTimeout = value; } }
    public static bool bool_40 { get { return IsWindowHidden; } set { IsWindowHidden = value; } }
    public static bool bool_41 { get { return IsWindowShrunk; } set { IsWindowShrunk = value; } }

    public static void InitStartupMode(AppMode appMode_1)
    {
        ApplicationStartupMode = appMode_1;
        ProfileMapping = new SortedList<string, SpellActionData>();
        ProfileIdToProfileMap = new SortedList<long, LootableCorpseTracker>();
        if (appMode_1 == AppMode.PGEdit)
        {
            ConfigManager.gclass61_0 = new ConfigManager();
            MessageProvider.smethod_0(".\\");
            RandomGenerator = new Random();
            ActiveProfile = null;
            ActiveProfilePath = null;
            InitializationThread = null;
            PartyStateManager = new PartyManager();
            ProfileEditorVersion = 1;
            if (ConfigManager.gclass61_0.method_2("LastProfile") != null)
            {
                smethod_1(ConfigManager.gclass61_0.method_2("LastProfile"));
            }
            else
            {
                ActiveProfile = new GProfile();
                ActiveProfilePath = MessageProvider.GetMessage(70);
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
                IsDiagnosticLoggingEnabled = true;
            if (Environment.CommandLine.ToLower().IndexOf("/mach") != -1)
            {
                IsAttached = true;
                Logger.LogMessage("Mach flag, using open memory model");
            }

            if (Environment.CommandLine.ToLower().IndexOf("/resume") != -1)
                IsResumeRequested = true;
            MessageProvider.smethod_0(".\\");
            SecurityDescriptor = new SecurityDescriptorHelper();
            SecurityDescriptor.method_1();
            RandomGenerator = new Random();
            LicenseCheckTimer = new GameTimer(10000);
            CurrentGlideMode = GlideMode.None;
            WowVersionLabel = "0.0";
            DynamicClassCount = 0;
            CompiledClassCount = 0;
            InternalClassCount = 0;
            ActiveProfile = null;
            ActiveProfilePath = null;
            HasClassLoadMismatch = ActiveProfilePath != null;
            IsProfileDirty = false;
            InitializationThread = null;
            StartupAttemptCount = 1;
            LicenseCheckTimer = new GameTimer(660000);
            HasSessionWarning = false;
            SessionHeartbeatTimer = new GameTimer(30000);
            IsGliderInitialized = false;
            CameraController = new CameraRotator();
            if (!IsAttached)
            {
                smethod_54();
            }

            if (ConfigManager.gclass61_0.method_2("LastProfile") != null)
            {
                smethod_1(ConfigManager.gclass61_0.method_2("LastProfile"));
            }
            else
            {
                ActiveProfile = new GProfile();
                ActiveProfilePath = MessageProvider.GetMessage(70);
            }

            GliderUIManager = new WebNotificationService();
            var gcontext = new GContext();
            if (!IsAttached)
                CodeCompiler.smethod_10();
            InputController.smethod_31(ConfigManager.gclass61_0);
            smethod_5();
            PartyStateManager = new PartyManager();
            PartyStateManager.method_0(ConfigManager.gclass61_0);
            smethod_52();
            SpellcastingManager.gclass42_0 = new SpellcastingManager();
            SpellcastingManager.gclass42_0.method_12();
            KeyboardHook = new KeyboardHookManager();
            LicenseCheckTimer.method_4();
            if (!IsAttached)
                smethod_7();
            else
                WowVersionLabel = "EvoStub";
            if (!IsAttached)
                smethod_8();
            HasClassLoadMismatch = DynamicClassCount != CompiledClassCount;
            if (SecurityDescriptor.string_0 != null)
            {
                Logger.LogMessage(MessageProvider.smethod_2(72, SecurityDescriptor.string_0));
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
            ProfileGroupStateManager = new ProfileGroupManager();
            return ProfileGroupStateManager.method_4(string_11);
        }

        ProfileGroupStateManager = null;
        return smethod_3(string_11);
    }

    private static bool smethod_2(string string_11)
    {
        return string_11.ToLower().IndexOf("groups\\") != -1;
    }

    public static bool smethod_3(string string_11)
    {
        ActiveProfile = new GProfile();
        if (ActiveProfile.Load(string_11))
        {
            RuntimeProfileCache.Clear();
            IsProfileDirty = false;
            ActiveProfilePath = string_11;
            Logger.LogMessage(MessageProvider.smethod_2(109, ActiveProfilePath));
            ConfigManager.gclass61_0.method_0("LastProfile", string_11);
            if (PartyStateManager != null && PartyStateManager.Offsets != null)
            {
                PartyStateManager.Offsets = null;
                Logger.LogMessage(MessageProvider.GetMessage(110));
            }

            if (IsInitializationComplete)
                StartupLogger.imethod_0();
            return true;
        }

        Logger.LogMessage(MessageProvider.smethod_2(111, string_11));
        if (IsInitializationComplete)
            StartupLogger.imethod_0();
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
        ActionBarCharacters = ConfigManager.gclass61_0.method_2("BarCharacters");
        if (GContext.Main != null)
            GContext.Main.ApplyConfig();
        KnownDebuffs = new DebuffDatabase();
        if (RequiresConfigReload)
        {
            RequiresConfigReload = false;
            smethod_8();
        }

        if (CurrentGameClass != null)
            CurrentGameClass.LoadConfig();
        RestStatusMonitor.double_2 = smethod_6(ConfigManager.gclass61_0.method_2("MeleeDistance"));
        RestStatusMonitor.double_3 = smethod_6(ConfigManager.gclass61_0.method_2("RangedDistance"));
        AutoAddDistance = smethod_6(ConfigManager.gclass61_0.method_2("AutoAddDistance"));
        SoundPlayer.bool_0 = ConfigManager.gclass61_0.method_5("Silent");
        if (ApplicationStartupMode == AppMode.PGEdit)
            return;
        if (!ConfigManager.gclass61_0.method_5("ListenEnabled"))
        {
            if (RemoteViewer != null)
            {
                Logger.LogMessage(MessageProvider.GetMessage(141));
                RemoteViewer.method_1();
                RemoteViewer = null;
            }
        }
        else
        {
            if (RemoteViewer != null && RemoteViewer.int_0 != ConfigManager.gclass61_0.method_3("ListenPort"))
            {
                Logger.LogMessage(MessageProvider.smethod_2(142, RemoteViewer.int_0));
                RemoteViewer.method_1();
                RemoteViewer = null;
            }

            if (RemoteViewer == null)
            {
                RemoteViewer = new RemoteViewerServer();
                Logger.LogMessage(MessageProvider.smethod_2(143, RemoteViewer.int_0));
                RemoteViewer.method_0();
            }
        }

        FriendWhitelist = ConfigManager.gclass61_0.method_2("FriendWhitelist").Split(' ');
        LootRoutePattern = new LootRouteParser(ConfigManager.gclass61_0.method_2("LootPattern"));
        GliderUIManager.method_2();
        if (ConfigManager.gclass61_0.method_5("UseHook") && !KeyboardHookManager.bool_0)
        {
            KeyboardHook = new KeyboardHookManager();
        }
        else
        {
            if (KeyboardHook == null || !KeyboardHookManager.bool_0 || ConfigManager.gclass61_0.method_5("UseHook"))
                return;
            KeyboardHook.method_17();
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
            WowVersionLabel = ConfigManager.gclass61_0.method_2("ForceVersion");
            Logger.LogMessage(MessageProvider.smethod_2(81, WowVersionLabel));
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
        WowVersionLabel = versionInfo.FileVersion;
        Logger.LogMessage(MessageProvider.smethod_2(86, WowVersionLabel));
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
                if (!HasDeferredCompileRun)
                    Logger.LogMessage("Switching to: " + str);
            }
        }

        ProfileMapping[str].method_0();
        var object0 = (GGameClass)ProfileMapping[str].object_0;
        if (IsRuntimeAttached)
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
        InitializationThread = new Thread(smethod_10);
        InitializationThread.Start();
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
        IsRuntimeAttached = false;
        if (isInputStringFourCharacters && !HasShownStartupNotice)
        {
            HasShownStartupNotice = true;
            Logger.LogMessage(MessageProvider.GetMessage(75));
            smethod_39(1000);
        }

        InitializationThread = null;
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

            HasClassLoadMismatch = false;

            if (IsSomeConditionMet && !HasDeferredCompileRun && !IsAttached)
                NeedsDeferredCompile = true;
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
                LicenseCheckTimer.method_5();
            }
        }

        if (ApplicationStartupMode == AppMode.Normal)
            Logger.LogMessage(MessageProvider.GetMessage(79));
        IsInitializationComplete = true;
        StartupLogger.imethod_0();
        IsStartupPending = false;
        if (IsSecCheckEnabled)
            return;
        smethod_61();
    }

    public static bool smethod_12()
    {
        return IsRuntimeAttached;
    }

    public static void smethod_13()
    {
        GameMemoryAccess.bool_2 = false;
        IsRuntimeAttached = false;
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
            StartupLogger.imethod_0();
        }
        else
        {
            IsForegroundEnabled = false;
            AttachRefreshTimer.method_4();
            GameMemoryAccess.bool_2 = false;
            PlayerOffsetManager = new OffsetManager("Player", MemoryOffsetTable.Instance.GetIntOffset("D_Player"));
            PlayerOffsetManager.PopulateOffsetList();
            ItemOffsetManager = new OffsetManager("Item", MemoryOffsetTable.Instance.GetIntOffset("D_Items"));
            ItemOffsetManager.PopulateOffsetList();
            NpcOffsetManager = new OffsetManager("NPC", MemoryOffsetTable.Instance.GetIntOffset("D_NPC"));
            NpcOffsetManager.PopulateOffsetList();
            ObjectOffsetManager = new OffsetManager("Object", MemoryOffsetTable.Instance.GetIntOffset("D_Object"));
            ObjectOffsetManager.PopulateOffsetList();
            ContainerOffsetManager = new OffsetManager("Container", MemoryOffsetTable.Instance.GetIntOffset("D_Container"));
            ContainerOffsetManager.PopulateOffsetList();
            SpellbookStateManager = new SpellbookManager();
            GContext.Main.OnAttach();
            if (CurrentGameClass != null)
                CurrentGameClass.OnAttach();
            smethod_17(1, MessageProvider.GetMessage(98));
            StartupLogger.imethod_0();
            EquipmentEnchantmentChecker = new EquipmentEnchantmentChecker();
            EquipmentEnchantmentChecker.method_0();
            GameClass69Instance.method_0();
            IsWorldUiReady = false;
            GameClass8Instance = null;
            IsGliderPaused = false;
            if (ProfileGroupStateManager != null)
                ProfileGroupStateManager.method_6();
            IsRuntimeAttached = true;
            SoundPlayer.smethod_0("Attach.wav");
            DetachAfterStopRequested = false;
            Logger.smethod_1("--- Attach code out");
            if (!IsStopRequested)
                return;
            IsStopRequested = false;
            smethod_24(false);
        }
    }

    public static void smethod_15()
    {
        if (!IsRuntimeAttached)
            return;
        IsAutoLoginTriggered = false;
        IsDetachInProgress = true;
        Logger.smethod_1("AppContext.Detach invoked");
        if (AttachAttemptCount == 0 && !GameMemoryAccess.smethod_56(AnotherIntegerValue))
        {
            GameMemoryAccess.CloseProcessHandle(AdditionalApplicationHandle);
            AdditionalApplicationHandle = IntPtr.Zero;
            AnotherIntegerValue = 0;
        }

        IsRuntimeAttached = false;
        GameClass69Instance.method_3();
        IsWorldUiReady = false;
        GameClass8Instance = null;
        smethod_17(1, MessageProvider.GetMessage(99));
    }

    public static void smethod_16(int int_14)
    {
        if (LastSecurityCode == int_14)
            return;
        LastSecurityCode = int_14;
        if (!SecurityDescriptor.method_2(int_14))
        {
            Logger.LogMessage(MessageProvider.smethod_2(91, SecurityDescriptor.string_0));
            Logger.LogMessage(MessageProvider.GetMessage(92));
        }
        else
        {
            Logger.LogMessage(MessageProvider.GetMessage(93));
            if (SecurityDescriptor.string_1 == null)
                return;
            Logger.LogMessage(MessageProvider.smethod_2(94, SecurityDescriptor.string_1));
        }
    }

    public static void smethod_17(int int_14, string string_11)
    {
        if (RemoteViewer != null)
            RemoteViewer.method_5(int_14, string_11);
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
        if (!IsInitializationComplete)
            return false;
        if (HasClassLoadMismatch)
        {
            Logger.LogMessage(MessageProvider.GetMessage(113));
            return false;
        }

        if (CurrentGlideMode != GlideMode.None)
        {
            Logger.LogMessage(MessageProvider.GetMessage(114));
            return false;
        }

        if (!IsRuntimeAttached)
        {
            smethod_13();
            if (!IsRuntimeAttached)
                return false;
        }

        if (GPlayerSelf.Me.TargetGUID == 0L)
        {
            Logger.LogMessage(MessageProvider.GetMessage(115));
            return false;
        }

        if (!ConfigManager.gclass61_0.method_5("BackgroundEnable"))
            smethod_22();
        CurrentGlideMode = GlideMode.Manual;
        ManualGlideController = new GlideMainThread();
        return true;
    }

    public static void smethod_22()
    {
        smethod_39(500);
        GameMemoryAccess.SetForegroundWindow(MainApplicationHandle);
        smethod_39(500);
    }

    public static bool smethod_23()
    {
        if (!IsRuntimeAttached)
        {
            Logger.LogMessage(MessageProvider.GetMessage(107));
            return false;
        }

        if (ActiveProfile == null)
        {
            Logger.LogMessage(MessageProvider.GetMessage(108));
            return false;
        }

        var flag = false;
        switch (SelectedWaypointType)
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

        if (SelectedWaypointType == WaypointType.const_3)
        {
            ActiveProfile.VendorWaypoints.Add(GPlayerSelf.Me.Location);
            Logger.LogMessage(MessageProvider.smethod_2(870, ActiveProfile.VendorWaypoints.Count));
        }
        else if (!flag)
        {
            ActiveProfile.Waypoints.Add(GPlayerSelf.Me.Location);
            Logger.LogMessage(MessageProvider.smethod_2(658, ActiveProfile.Waypoints.Count));
        }
        else
        {
            ActiveProfile.GhostWaypoints.Add(GPlayerSelf.Me.Location);
            Logger.LogMessage(MessageProvider.smethod_2(659, ActiveProfile.GhostWaypoints.Count));
        }

        IsProfileDirty = true;
        return true;
    }

    public static bool smethod_24(bool bool_42)
    {
        if (!IsInitializationComplete)
            return false;
        if (isTimeAdded && DateTime.Now > expiryTime)
        {
            Logger.LogMessage(MessageProvider.GetMessage(116));
            return false;
        }

        if (CurrentGlideMode != GlideMode.None)
        {
            Logger.LogMessage(MessageProvider.GetMessage(117));
            return false;
        }

        if (!IsRuntimeAttached && !IsDetached)
        {
            smethod_13();
            if (!IsRuntimeAttached)
                return false;
        }

        if (HasClassLoadMismatch)
        {
            Logger.LogMessage(MessageProvider.GetMessage(118));
            return false;
        }

        if (IsDetached)
            return smethod_25();
        if (ProfileGroupStateManager == null && (ActiveProfile == null || (ActiveProfile.Waypoints.Count < 2 && !ActiveProfile.Fishing)))
        {
            Logger.LogMessage(MessageProvider.GetMessage(119));
            return false;
        }

        if (GPlayerSelf.Me.IsDead &&
            (ActiveProfile.GhostWaypoints.Count == 0 || !ConfigManager.gclass61_0.method_5("Resurrect")))
        {
            Logger.LogMessage(MessageProvider.GetMessage(120));
            return false;
        }

        if (!IsGliderInitialized)
            smethod_22();
        CurrentGlideMode = GlideMode.Auto;
        ActiveCombatController = new CombatController();
        if (ActiveCombatController.method_1())
            return true;
        CurrentGlideMode = GlideMode.None;
        return false;
    }

    private static bool smethod_25()
    {
        GameProcessManager = new MachGlideRunner();
        if (!GameProcessManager.method_0())
        {
            CurrentGlideMode = GlideMode.None;
            return false;
        }

        CurrentGlideMode = GlideMode.Auto;
        StartupLogger.imethod_0();
        return true;
    }

    public static bool smethod_26()
    {
        if (!IsRuntimeAttached)
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

        if (ActiveProfile == null)
        {
            Logger.LogMessage(MessageProvider.GetMessage(133));
            return false;
        }

        if (ActiveProfile.CheckFaction(GPlayerSelf.Me.Target.FactionID, true))
        {
            Logger.LogMessage(MessageProvider.GetMessage(128));
        }
        else
        {
            Logger.LogMessage(MessageProvider.GetMessage(129));
            ActiveProfile.SetFactionsFromString(ActiveProfile.GetFactionsAsString() + " " + GPlayerSelf.Me.Target.FactionID);
        }

        return true;
    }

    public static void smethod_27(bool bool_42, string string_11)
    {
        if (CurrentGlideMode == GlideMode.None && !bool_42)
            return;
        var flag = false;
        try
        {
            ++KillActionDepth;
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
            --KillActionDepth;
        }

        if (flag)
            throw new ThreadInterruptedException();
    }

    private static void smethod_28(bool bool_42, string string_11)
    {
        lock (killActionLock)
        {
            var flag = false;
            if (CurrentGlideMode != GlideMode.None || IsRuntimeAttached && bool_42)
            {
                smethod_51();
                CameraController.method_3(true);
                Logger.smethod_1(MessageProvider.smethod_2(652, bool_42, (int)CurrentGlideMode, string_11));
                CameraController.method_3(true);
                InputController.smethod_21(false);
                if (CurrentGlideMode == GlideMode.Auto)
                {
                    if (bool_42)
                        DetachAfterStopRequested = true;
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
                        var combatController = ActiveCombatController;
                        if (combatController != null && Thread.CurrentThread == combatController.thread_0)
                            flag = true;
                    }

                    Logger.smethod_1(MessageProvider.GetMessage(100));
                    CurrentGlideMode = GlideMode.None;
                    if (IsAttached)
                    {
                        var gameProcessManager = GameProcessManager;
                        GameProcessManager = null;
                        if (gameProcessManager != null)
                            gameProcessManager.method_1();
                    }
                    else
                    {
                        var combatController = ActiveCombatController;
                        ActiveCombatController = null;
                        if (combatController != null)
                            combatController.method_2();
                    }
                }

                if (CurrentGlideMode == GlideMode.Manual)
                {
                    smethod_17(1, MessageProvider.GetMessage(101));
                    if (ManualGlideController != null && Thread.CurrentThread == ManualGlideController.thread_0)
                        flag = true;
                    Logger.smethod_1(MessageProvider.GetMessage(102));
                    CurrentGlideMode = GlideMode.None;
                    if (ManualGlideController != null)
                        ManualGlideController.method_0();
                    ManualGlideController = null;
                }

                if (bool_42)
                    smethod_15();
                StartupLogger.imethod_0();
                GContext.Main.ReleaseAllKeys();
                InputController.smethod_21(false);
                if (flag)
                    throw new ThreadInterruptedException();
            }
        }
    }

    public static int smethod_29()
    {
        if (CurrentGlideMode != GlideMode.Auto)
            return CachedRatePerHour;
        if (ActiveCombatController == null)
            return 0;
        lock (ActiveCombatController)
        {
            if (ActiveCombatController.bool_9)
            {
                CachedRatePerHour = (int)Math.Round(ActiveCombatController.int_8 / (DateTime.Now - SessionStartTime).TotalSeconds * 3600.0, 0);
                ActiveCombatController.bool_9 = false;
            }
        }

        return CachedRatePerHour;
    }

    private static void smethod_30()
    {
        if (Environment.CommandLine.ToLower().IndexOf("/kill") == -1)
            return;
        new Thread(smethod_32).Start();
    }

    public static void smethod_31()
    {
        GameMemoryWriter.method_0();
        CodeCompiler.smethod_4();
        GliderUIManager.method_5();
    }

    private static void smethod_32()
    {
        var string_11 = smethod_36("/kill");
        KillEventHandle = CreateEvent(IntPtr.Zero, false, false, string_11);
        if (KillEventHandle == IntPtr.Zero)
        {
            Logger.LogMessage("Couldn't create named event");
        }
        else
        {
            WaitForSingleObject(KillEventHandle, KillEventWaitTimeout);
            CloseHandle(KillEventHandle);
            if (!ShouldProcessKillEvent)
                return;
            smethod_33();
        }
    }

    private static void smethod_33()
    {
        SoundPlayer.smethod_1("GliderExit.wav");
        KnownDebuffs.method_10();
        StartupLogger.imethod_4();
        smethod_31();
        Environment.Exit(0);
    }

    public static void smethod_34()
    {
        if (KillEventHandle == IntPtr.Zero)
            smethod_33();
        SetEvent(KillEventHandle);
    }

    public static void smethod_35()
    {
        if (KillEventHandle == IntPtr.Zero)
            return;
        ShouldProcessKillEvent = false;
        SetEvent(KillEventHandle);
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

    public static void smethod_38()
    {
        if (Interlocked.Exchange(ref MainLoopTickGuard, 1) == 1)
            return;

        try
        {
            var tickNow = Environment.TickCount;
            var elapsed = unchecked(tickNow - LastMainLoopTickTime);
            if (LastMainLoopTickTime != 0 && elapsed >= 0 && elapsed < MinMainLoopTickIntervalMs)
                return;

            LastMainLoopTickTime = tickNow;

        smethod_63("Tick start");
        if (IsExitRequested)
        {
            smethod_63("Exit requested, skipping tick");
            return;
        }

        smethod_45();
        smethod_63("Attach/refresh check completed");
        if (IsResumeRequested && IsInitializationComplete)
        {
            smethod_63("Security handshake check");
            GameMemoryAccess.smethod_53();
            if (GameMemoryWriter != null && (ApplicationStartupMode == AppMode.Normal || ApplicationStartupMode == AppMode.Invisible))
                GameMemoryWriter.method_2("OnGliderStart", false);
        }

        if (SpellCooldownTimer.IsReady)
        {
            smethod_63("Process probe timer fired");
            SpellCooldownTimer.Reset();
            GameMemoryAccess.bool_3 = GameMemoryAccess.IsWowProcessRunning();
            GameMemoryAccess.GetProcessId();
        }

        if (NeedsDeferredCompile && !IsAttached && !HasDeferredCompileRun)
        {
            smethod_63("Deferred startup compilation");
            HasDeferredCompileRun = true;
            CodeCompiler.smethod_14();
            smethod_8();
        }

        if (!IsRuntimeAttached)
        {
            smethod_63("Not attached, tick complete");
            return;
        }

        smethod_63("Refreshing object list");
        GObjectList.GetObjects();
        var me = GPlayerSelf.Me;
        if (me == null)
        {
            smethod_63("Player object unavailable, tick complete");
            return;
        }

        if (KnownDebuffs != null && DebuffRefreshTimer.method_3())
        {
            smethod_63("Debuff cache refresh");
            DebuffRefreshTimer.method_4();
            KnownDebuffs.method_8();
        }

        if (me.Stance != CurrentStance)
        {
            smethod_63("Stance changed");
            if (CurrentStance != GStance.Unknown)
                GContext.Main.Interface.UnFillAllKeys();
            CurrentStance = me.Stance;
        }

        EnsureWorldUiReady(me);
        smethod_63("Running chat/dialog updates");
        GameClass69Instance.method_4();
        DialogMonitor.smethod_1();
        if (GameClass8Instance != null && GameClass8Instance.method_10() && CurrentGlideMode == GlideMode.Auto)
        {
            smethod_63("Auto mode popup dismiss");
            InputController.smethod_9(27);
        }

        if (CurrentGlideMode == GlideMode.Auto && IsGliderInitialized && ConfigManager.gclass61_0.method_2("BackgroundDisplay") != "Normal" &&
            (DateTime.Now - SessionStartTime).TotalSeconds >= 8.0 && !IsGliderRunning)
        {
            smethod_63("Applying background display state");
            IsGliderRunning = true;
            smethod_46();
        }

        smethod_63("Running camera/input maintenance");
        CameraController.method_7();
        InputController.smethod_21(true);
        smethod_63("Tick complete");
        }
        finally
        {
            Interlocked.Exchange(ref MainLoopTickGuard, 0);
        }
    }

    private static bool smethod_64()
    {
        return ConfigManager.gclass61_0 != null && ConfigManager.gclass61_0.method_5("VerboseMainLoopLogging");
    }

    private static void smethod_63(string string_11)
    {
        if (!smethod_64() || StartupLogger == null)
            return;
        StartupLogger.imethod_2("[Debug] [MainLoop] " + string_11);
    }

    private static void EnsureWorldUiReady(GPlayerSelf player)
    {
        if (IsWorldUiReady || player == null || long_0 <= 0L || player.BaseAddress == 0)
            return;

        if (GContext.Main == null || GContext.Main.Interface == null || GContext.Main.Interface.IsPreWorldVisible)
            return;

        DialogMonitor.smethod_0();
        GameClass8Instance = UIElement.smethod_2("GameMenuFrame");
        IsWorldUiReady = true;
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
            if (CurrentGlideMode == GlideMode.Auto)
                ActiveProfile.ForceBlacklist(gunit_0.GUID);
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
        AnotherIntegerValue = GameMemoryAccess.AttachToWowProcess();
        if (AnotherIntegerValue == 0)
        {
            if (!HasLoggedMissingProcess)
                Logger.smethod_1("Attach attempt: no matching process found for AttachEXE");
            HasLoggedMissingProcess = true;
            return false;
        }

        HasLoggedMissingProcess = false;

        IsGliderAttached = true;
        if (AdditionalApplicationHandle == IntPtr.Zero && !SkipHandleOpen)
        {
            AdditionalApplicationHandle = GameMemoryAccess.OpenProcessHandle(AnotherIntegerValue);
            if (AdditionalApplicationHandle == IntPtr.Zero)
            {
                if (!IsGliderPaused)
                {
                    IsGliderPaused = true;
                    Logger.LogMessage(MessageProvider.smethod_2(96, Marshal.GetLastWin32Error()));
                }

                return false;
            }

            GameMemoryAccess.bool_3 = GameMemoryAccess.IsWowProcessRunning();

            GameMemoryAccess.GetProcessId();
            if (GameMemoryWriter != null)
                GameMemoryWriter.method_2("OnGameFirstSeen", false);
        }

        if (IsAttached)
            return true;
        var int_14 = MemoryOffsetTable.Instance.GetIntOffset("UIParent");
        if (int_14 > 0 && GameMemoryAccess.ReadInt32(int_14, "probeuip") == 0 && !IsAutoLoginTriggered &&
            ((IsAutoLoginArmed && AutoLoginTimer.IsReady) || IsForegroundEnabled))
        {
            var str = ConfigManager.gclass61_0.method_2("AutoLog");
            if (str != null && str.Length > 0 && IsSomeConditionMet && new AutoLoginManager().method_2())
            {
                IsAutoLoginTriggered = true;
                GameMemoryWriter.method_2("DoAutoLog", false);
            }

            Logger.smethod_1("Attach probe note: UIParent resolved to zero, continuing with TLS/static attach checks");
        }

        if (GameMemoryAccess.smethod_52(out CurrentPlayerGuid, out ResolvedMainTableAddress) && CurrentPlayerGuid != 0L)
        {
            if (!isLikelyPlayerGuid(CurrentPlayerGuid))
            {
                Logger.smethod_1("Direct attach probe rejected implausible GUID: 0x" + CurrentPlayerGuid.ToString("x"));
                CurrentPlayerGuid = 0L;
            }
            else
            {
                if (GObjectList.StealthCountGameObjects(CurrentPlayerGuid) > 0)
                    return true;
                Logger.smethod_1("Direct attach probe failed object validation, trying static offsets fallback");
            }
        }
        else
        {
            Logger.smethod_1("Direct attach probe failed, trying static offsets fallback");
        }
        CurrentPlayerGuid = 0L;
        var int_18 = GameMemoryAccess.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("MainTable"), "MainTable");
        var int_19 = int_18;
        if (MemoryOffsetTable.Instance.HasOffset("MainTableProbe") && MemoryOffsetTable.Instance.GetIntOffset("MainTableProbe") > 0)
        {
            var int_20 = MemoryOffsetTable.Instance.GetIntOffset("MainTableProbe");
            var int_21 = GameMemoryAccess.ReadInt32(int_18 + int_20, "MainTableProbe");
            var int_22 = int_18 + int_20;
            if (smethod_62(int_21))
                int_19 = int_21;
            else if (smethod_62(int_22))
                int_19 = int_22;
            else
                int_19 = int_21;
        }
        ResolvedMainTableAddress = int_19;
        if (ResolvedMainTableAddress == 0)
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
            var int_24 = GameMemoryAccess.ReadInt32(ResolvedMainTableAddress + int_23, "MainTableActivePlayer");
            if (isLikelyObjectAddress(int_24))
            {
                var int64_1 = GameMemoryAccess.ReadInt64(int_24 + 48, "MainTableActivePlayerGuid");
                if (int64_1 != 0L && isLikelyPlayerGuid(int64_1))
                {
                    CurrentPlayerGuid = int64_1;
                    bool_42 = true;
                    bool_43 = true;
                    Logger.smethod_1("Attach probe: using active player object GUID = 0x" + CurrentPlayerGuid.ToString("x"));
                }
            }
        }

        var int_25 = MemoryOffsetTable.Instance.HasOffset("MainTableLocalGuid")
            ? MemoryOffsetTable.Instance.GetIntOffset("MainTableLocalGuid")
            : 40;
        if (!bool_42 && int_25 > 0)
        {
            var int64_2 = GameMemoryAccess.ReadInt64(ResolvedMainTableAddress + int_25, "MainTableLocalGuid");
            if (int64_2 != 0L && isLikelyPlayerGuid(int64_2))
            {
                CurrentPlayerGuid = int64_2;
                bool_43 = true;
                Logger.smethod_1("Attach probe: using object manager local GUID = 0x" + CurrentPlayerGuid.ToString("x"));
            }
            else if (int64_2 != 0L)
            {
                Logger.smethod_1("Attach probe note: rejected implausible object manager local GUID = 0x" + int64_2.ToString("x"));
            }
        }

        long long_2;
        if (CurrentPlayerGuid == 0L && TryResolvePlayerGuidFromGuaranteedWotlkOffsets(out long_2))
        {
            CurrentPlayerGuid = long_2;
            bool_43 = true;
        }

        if (CurrentPlayerGuid == 0L)
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

                var playerGuidBytes = GameMemoryAccess.ReadBytesRaw(candidateAddress, 8);
                if (playerGuidBytes == null)
                    continue;

                var playerGuid = BitConverter.ToInt64(playerGuidBytes, 0);
                if (playerGuid != 0L && isLikelyPlayerGuid(playerGuid))
                {
                    CurrentPlayerGuid = playerGuid;
                    bool_43 = true;
                    if (candidateAddress != configuredPlayerIdAddress)
                        Logger.smethod_1("Attach probe: using fallback PlayerIdAddr 0x" + candidateAddress.ToString("x"));
                    break;
                }

                var playerObjectPointer = GameMemoryAccess.ReadInt32(candidateAddress, "PlayerIdAddrPointer");
                if (!isLikelyObjectAddress(playerObjectPointer))
                    continue;

                var playerObjectGuid = GameMemoryAccess.ReadInt64(playerObjectPointer + 48, "PlayerIdAddrPointerGuid");
                if (playerObjectGuid == 0L || !isLikelyPlayerGuid(playerObjectGuid))
                    continue;

                CurrentPlayerGuid = playerObjectGuid;
                bool_43 = true;
                Logger.smethod_1("Attach probe: using PlayerIdAddr object pointer path at 0x" + candidateAddress.ToString("x") +
                                 " -> object 0x" + playerObjectPointer.ToString("x") + " -> GUID 0x" + CurrentPlayerGuid.ToString("x"));
                break;
            }

            if (CurrentPlayerGuid == 0L)
            {
                Logger.smethod_1("Attach probe failed: Player GUID is zero across local and static sources");
                return false;
            }
        }

        var num = GObjectList.StealthCountGameObjects(CurrentPlayerGuid);
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
            CurrentPlayerGuid = long_1;
            Logger.smethod_1("Attach probe: inferred player GUID from object list = 0x" + CurrentPlayerGuid.ToString("x"));
            num = GObjectList.StealthCountGameObjects(CurrentPlayerGuid);
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
        var objectTypeBytes = GameMemoryAccess.ReadBytesRaw(objectAddress + 20, 4);
        if (objectTypeBytes == null || objectTypeBytes.Length < 4)
            return false;
        var objectType = BitConverter.ToInt32(objectTypeBytes, 0);
        return objectType >= 1 && objectType <= 7;
    }

    private static bool isLikelyPlayerGuid(long playerGuid)
    {
        return playerGuid != 0L;
    }

    private static bool isLikelyMemoryPointer(int pointer)
    {
        return (pointer & 1) == 0 && pointer != 0 && pointer != 28 && pointer >= 65536;
    }

    private static bool TryResolvePlayerGuidFromGuaranteedWotlkOffsets(out long playerGuid)
    {
        playerGuid = 0L;
        var clientConnection = GameMemoryAccess.ReadInt32(WotlkClientConnectionAddress, "WotlkClientConnection");
        if (isLikelyMemoryPointer(clientConnection))
        {
            var objectManager = GameMemoryAccess.ReadInt32(clientConnection + WotlkObjectManagerOffset, "WotlkObjectManager");
            if (isLikelyMemoryPointer(objectManager))
            {
                var objectManagerGuid = GameMemoryAccess.ReadInt64(objectManager + WotlkObjectManagerLocalGuidOffset,
                    "WotlkObjectManagerLocalGuid");
                if (isLikelyPlayerGuid(objectManagerGuid))
                {
                    playerGuid = objectManagerGuid;
                    Logger.smethod_1("Attach probe: using WotLK ClientConnection/ObjectManager GUID path = 0x" + playerGuid.ToString("x"));
                    return true;
                }

                if (objectManagerGuid != 0L)
                    Logger.smethod_1("Attach probe note: rejected WotLK ObjectManager GUID = 0x" + objectManagerGuid.ToString("x"));
            }
        }

        var staticGuid = GameMemoryAccess.ReadInt64(WotlkStaticPlayerGuidAddress, "WotlkStaticPlayerGuid");
        if (isLikelyPlayerGuid(staticGuid))
        {
            playerGuid = staticGuid;
            Logger.smethod_1("Attach probe: using WotLK static PlayerGUID address 0x" + WotlkStaticPlayerGuidAddress.ToString("x") +
                             " = 0x" + playerGuid.ToString("x"));
            return true;
        }

        if (staticGuid != 0L)
            Logger.smethod_1("Attach probe note: rejected WotLK static PlayerGUID value = 0x" + staticGuid.ToString("x"));

        return false;
    }

    private static bool smethod_62(int int_14)
    {
        if (int_14 == 0)
            return false;
        var firstObjectAddress = GameMemoryAccess.ReadInt32(int_14 + MemoryOffsetTable.Instance.GetIntOffset("InitialOffset"), "MainTableFirstProbe");
        if ((firstObjectAddress & 1) != 0 || firstObjectAddress == 0 || firstObjectAddress == 28 || firstObjectAddress < 65536)
            return false;
        var objectType = GameMemoryAccess.ReadInt32(firstObjectAddress + 20, "MainTableFirstTypeProbe");
        return objectType >= 1 && objectType <= 7;
    }

    public static void smethod_45()
    {
        if (IsRuntimeAttached || IsDetached || !isInitializationSuccessful || !smethod_44())
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
        if (IsWindowHidden)
            return;
        GameMemoryAccess.SetForegroundWindow(MainApplicationHandle);
        IsWindowHidden = true;
    }

    public static void smethod_48()
    {
        if (IsWindowShrunk)
            return;
        double width = ConfigManager.gclass61_0.method_3("ShrinkWidth");
        GameMemoryAccess.GetWindowSize(MainApplicationHandle, out OriginalWindowSize);
        var height = OriginalWindowSize.Height / (double)OriginalWindowSize.Width * width;
        GameMemoryAccess.SetWindowSize(MainApplicationHandle, new Size((int)width, (int)height));
        IsWindowShrunk = true;
    }

    public static void smethod_49()
    {
        if (!IsWindowHidden)
            return;
        GameMemoryAccess.ShowWindow(MainApplicationHandle);
        IsWindowHidden = false;
    }

    public static void smethod_50()
    {
        if (!IsWindowShrunk)
            return;
        GameMemoryAccess.SetWindowSize(MainApplicationHandle, OriginalWindowSize);
        IsWindowShrunk = false;
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
            Logger.LogMessage("Shadow confirmed, looks awake");

            if (Environment.CommandLine.ToLower().IndexOf("/holddriver") != -1)
            {
                Logger.smethod_1("DriverName is static, will leave driver resident");
                HasManualPause = true;
            }
        }
        else
        {
            Logger.LogMessage(MessageProvider.GetMessage(877));
        }

        Environment.CommandLine.ToLower().IndexOf("/shadowread");
        if (Environment.CommandLine.ToLower().IndexOf("/attachpid") != -1)
        {
            AttachAttemptCount = int.Parse(smethod_36("/attachpid"));
            Logger.LogMessage("/attachpid specified, looking for: " + AttachAttemptCount);
        }

        if (!ConfigManager.gclass61_0.method_5("UnloadShadow"))
            return;
    }

    private static void smethod_54()
    {
        var str = ConfigManager.gclass61_0.method_2("ForceVersion");
        if (str == null || str.Length <= 0 || MessageBox.Show(null,
                "ForceVersion detected in configuration.  Running with this option increases the risk of detection and may cause Glider to crash.  Are you sure you want to continue?",
                "Glider", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) !=
            DialogResult.No)
            return;
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
            AutoLoginSetting = string_1;
            Logger.smethod_1("Autolog is good!");
            IsStopRequested = true;
        }
    }

    public static bool IsDecryptedStreamEmpty(GDataEncryptionManager gclass56_0)
    {
        return gclass56_0.ReadIntFromDecryptedStream() == 0;
    }


    private static void smethod_60()
    {
        if (MessageBox.Show(MainForm, MessageProvider.GetMessage(875), GameMemoryAccess.GenerateRandomString(), MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation) != DialogResult.Yes)
            return;
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
            if (MessageBox.Show(MainForm, MessageProvider.GetMessage(876), GameMemoryAccess.GenerateRandomString(), MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;
            Process.Start("http://www.mmoglider.com/Download.aspx?Update=True");
        }
    }

    public static void smethod_62()
    {
        if (!IsGliderInitialized && ConfigManager.gclass61_0.method_5("BackgroundEnable") && IsSomeConditionMet)
        {
            Logger.smethod_1("Setting up bg stuff");
            MainApplicationHandle = GameMemoryAccess.OpenProcessWithAccess(AnotherIntegerValue);
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
        var num2 = GameMemoryAccess.ReadInt32(num1 + 36, "mbase");
        int int_29_1;
        for (var int_29_2 =
                 GameMemoryAccess.ReadInt32(GameMemoryAccess.ReadInt32(num1 + 28, "mbase2") + 12 * (int_14 & num2) + 8, "mbase3");
             int_29_2 > 0 && (int_29_2 & 1) == 0;
             int_29_2 = GameMemoryAccess.ReadInt32(int_29_2 + GameMemoryAccess.ReadInt32(int_29_1, "mnext3") + 4, "mnext4"))
        {
            var num3 = GameMemoryAccess.ReadInt32(int_29_2, "mstep");
            var str = GameMemoryAccess.ReadString(int_29_2 + 36, 64, "mname");
            if (num3 == int_14)
                return str;
            int_29_1 = GameMemoryAccess.ReadInt32(num1 + 28, "mnext1") +
                       12 * (GameMemoryAccess.ReadInt32(num1 + 36, "mnext2") & int_14);
        }

        return "(could not find macro in list!)";
    }
}

