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
    private const string LogCategoryStartup = "[Startup] ";
    private const string LogCategoryAttach = "[Attach] ";
    private const string LogCategoryDetach = "[Detach] ";
    private const string CommandLineFlagDiagnosticLogging = "/l1";
    private const string CommandLineFlagMach = "/mach";
    private const string CommandLineFlagResume = "/resume";

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
    public static SortedList<ulong, LootableCorpseTracker> ProfileIdToProfileMap;
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
    public static ulong CurrentPlayerGuid;
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
    public static Logger StartupLogger;
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
    private static string PendingErrorMessage = null;
    private static Size OriginalWindowSize;
    public static bool IsWindowHidden;
    public static bool IsWindowShrunk;

    public static void InitStartupMode(AppMode startupMode)
    {
        ApplicationStartupMode = startupMode;
        ProfileMapping = new SortedList<string, SpellActionData>();
        ProfileIdToProfileMap = new SortedList<ulong, LootableCorpseTracker>();

        if (startupMode == AppMode.PGEdit)
        {
            InitializeProfileEditorMode();
            return;
        }

        InitializeRuntimeMode();
    }

    private static void InitializeProfileEditorMode()
    {
        ConfigManager.gclass61_0 = new ConfigManager();
        MessageProvider.smethod_0(".\\");
        RandomGenerator = new Random();
        ActiveProfile = null;
        ActiveProfilePath = null;
        InitializationThread = null;
        PartyStateManager = new PartyManager();
        ProfileEditorVersion = 1;

        LoadLastProfileOrCreateDefault();

        new GContext();
        InputController.smethod_31(ConfigManager.gclass61_0);
        ApplyRuntimeConfiguration();
        SpellcastingManager.gclass42_0 = new SpellcastingManager();
        SpellcastingManager.gclass42_0.method_12();
        ResolveAndLogWowVersion();
    }

    private static void InitializeRuntimeMode()
    {
        try
        {
            Logger.LogMessage("Glider 1.8.0 starting up (Release)");
            ConfigManager.gclass61_0 = new ConfigManager();
            IsBetaAccessGranted = true;
            ProcessStartupCommandLineFlags();
            MessageProvider.smethod_0(".\\");
            SecurityDescriptor = new SecurityDescriptorHelper();
            SecurityDescriptor.method_1();
            InitializeRuntimeState();
            LoadLastProfileOrCreateDefault();
            GliderUIManager = new WebNotificationService();
            new GContext();
            if (!IsAttached)
                CodeCompiler.smethod_10();
            InputController.smethod_31(ConfigManager.gclass61_0);
            ApplyRuntimeConfiguration();
            PartyStateManager = new PartyManager();
            PartyStateManager.method_0(ConfigManager.gclass61_0);
            ApplyLaunchNameOverride();
            SpellcastingManager.gclass42_0 = new SpellcastingManager();
            SpellcastingManager.gclass42_0.method_12();
            KeyboardHook = new KeyboardHookManager();
            LicenseCheckTimer.method_4();
            if (!IsAttached)
                ResolveAndLogWowVersion();
            else
                WowVersionLabel = "EvoStub";

            if (!IsAttached)
                ReloadSelectedGameClass();

            HasClassLoadMismatch = DynamicClassCount != CompiledClassCount;
            EnsureSecurityDescriptorIsValid();

            GameMemoryWriter = new ScriptExecutor();
            GameClass69Instance = new ChatLogManager();
            StartKillEventListenerIfRequested();
            ProcessDriverAndAttachPidFlags();
            BeginBackgroundInitialization();
        }
        catch (Exception ex)
        {
            Logger.LogMessage(LogCategoryStartup + "Fatal runtime initialization failure: " + ex.Message);
            Logger.LogDebug(LogCategoryStartup + ex.StackTrace);
            throw;
        }
    }

    private static void ProcessStartupCommandLineFlags()
    {
        var commandLine = Environment.CommandLine;

        if (HasCommandLineFlag(commandLine, CommandLineFlagDiagnosticLogging))
            IsDiagnosticLoggingEnabled = true;

        if (HasCommandLineFlag(commandLine, CommandLineFlagMach))
        {
            IsAttached = true;
            Logger.LogMessage(LogCategoryStartup + "Mach flag detected, using open memory model");
        }

        if (HasCommandLineFlag(commandLine, CommandLineFlagResume))
            IsResumeRequested = true;
    }

    private static bool HasCommandLineFlag(string commandLine, string flag)
    {
        return !string.IsNullOrEmpty(commandLine) && commandLine.IndexOf(flag, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    private static void InitializeRuntimeState()
    {
        RandomGenerator = new Random();
        CurrentGlideMode = GlideMode.None;
        WowVersionLabel = "0.0";
        DynamicClassCount = 0;
        CompiledClassCount = 0;
        InternalClassCount = 0;
        ActiveProfile = null;
        ActiveProfilePath = null;
        HasClassLoadMismatch = false;
        IsProfileDirty = false;
        InitializationThread = null;
        StartupAttemptCount = 1;
        LicenseCheckTimer = new GameTimer(660000);
        HasSessionWarning = false;
        SessionHeartbeatTimer = new GameTimer(30000);
        IsGliderInitialized = false;
        CameraController = new CameraRotator();
    }

    private static void LoadLastProfileOrCreateDefault()
    {
        var lastProfile = ConfigManager.gclass61_0.method_2("LastProfile");
        if (!string.IsNullOrEmpty(lastProfile))
        {
            if (TryLoadProfileOrProfileGroup(lastProfile))
                return;

            Logger.LogMessage(LogCategoryStartup + "Failed to load last profile, falling back to default profile: " + lastProfile);
        }

        ActiveProfile = new GProfile();
        ActiveProfilePath = MessageProvider.GetMessage(70);
        Logger.LogMessage(LogCategoryStartup + "Initialized with default empty profile");
    }

    private static void EnsureSecurityDescriptorIsValid()
    {
        if (SecurityDescriptor == null)
        {
            Logger.LogMessage(LogCategoryStartup + "Security descriptor was not initialized");
            Environment.Exit(1);
            return;
        }

        if (SecurityDescriptor.string_0 == null)
            return;

        Logger.LogMessage(MessageProvider.smethod_2(72, SecurityDescriptor.string_0));
        Environment.Exit(1);
    }

    public static bool TryLoadProfileOrProfileGroup(string profilePath)
    {
        if (IsProfileGroupPath(profilePath))
        {
            ProfileGroupStateManager = new ProfileGroupManager();
            return ProfileGroupStateManager.method_4(profilePath);
        }

        ProfileGroupStateManager = null;
        return TryLoadProfile(profilePath);
    }

    private static bool IsProfileGroupPath(string profilePath)
    {
        return !string.IsNullOrEmpty(profilePath) && profilePath.IndexOf("groups\\", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    public static bool TryLoadProfile(string profilePath)
    {
        if (string.IsNullOrEmpty(profilePath))
        {
            Logger.LogMessage(LogCategoryStartup + "Attempted to load an empty profile path");
            return false;
        }

        ActiveProfile = new GProfile();
        if (ActiveProfile.Load(profilePath))
        {
            RuntimeProfileCache.Clear();
            IsProfileDirty = false;
            ActiveProfilePath = profilePath;
            Logger.LogMessage(MessageProvider.smethod_2(109, ActiveProfilePath));
            ConfigManager.gclass61_0.method_0("LastProfile", profilePath);
            if (PartyStateManager != null && PartyStateManager.Offsets != null)
            {
                PartyStateManager.Offsets = null;
                Logger.LogMessage(MessageProvider.GetMessage(110));
            }

            RefreshStartupUiIfReady();
            return true;
        }

        Logger.LogMessage(MessageProvider.smethod_2(111, profilePath));
        RefreshStartupUiIfReady();
        return false;
    }

    private static void RefreshStartupUiIfReady()
    {
        if (IsInitializationComplete && StartupLogger != null)
            StartupLogger.imethod_0();
    }

    public static void ApplyRuntimeConfiguration()
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
            ReloadSelectedGameClass();
        }

        if (CurrentGameClass != null)
            CurrentGameClass.LoadConfig();
        RestStatusMonitor.double_2 = ParseInvariantDouble(ConfigManager.gclass61_0.method_2("MeleeDistance"));
        RestStatusMonitor.double_3 = ParseInvariantDouble(ConfigManager.gclass61_0.method_2("RangedDistance"));
        AutoAddDistance = ParseInvariantDouble(ConfigManager.gclass61_0.method_2("AutoAddDistance"));
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

    public static double ParseInvariantDouble(string value)
    {
        return double.Parse(value.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat);
    }

    private static void ResolveAndLogWowVersion()
    {
        var forcedVersion = ConfigManager.gclass61_0.method_2("ForceVersion");
        if (forcedVersion != null)
        {
            WowVersionLabel = forcedVersion;
            Logger.LogMessage(MessageProvider.smethod_2(81, WowVersionLabel));
        }

        string wowInstallPath;
        if (!TryResolveWowInstallPath(out wowInstallPath))
        {
            Logger.LogMessage(MessageProvider.GetMessage(83));
            return;
        }

        SomeStringData = wowInstallPath;
        var fileName = Path.Combine(SomeStringData, "WoW.exe");
        if (!File.Exists(fileName))
        {
            Logger.LogMessage(LogCategoryStartup + "WoW executable was not found at resolved path: " + fileName);
            return;
        }

        Logger.smethod_1(MessageProvider.smethod_2(84, fileName));
        var versionInfo = FileVersionInfo.GetVersionInfo(fileName);
        if (versionInfo == null)
        {
            Logger.LogMessage(MessageProvider.smethod_2(85, fileName));
            return;
        }

        if (forcedVersion != null)
            return;
        WowVersionLabel = versionInfo.FileVersion;
        Logger.LogMessage(MessageProvider.smethod_2(86, WowVersionLabel));
    }

    private static bool TryResolveWowInstallPath(out string wowInstallPath)
    {
        wowInstallPath = null;
        var registryPath = MessageProvider.GetMessage(649);
        RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryPath) ?? Registry.CurrentUser.OpenSubKey(registryPath);
        if (registryKey != null)
            try
            {
                foreach (var str in new string[3] { "InstallPath", "Path", "GamePath" })
                {
                    var value = registryKey.GetValue(str);
                    if (value != null)
                    {
                        wowInstallPath = value.ToString();
                        if (!string.IsNullOrEmpty(wowInstallPath))
                            break;
                    }
                }
            }
            finally
            {
                registryKey.Close();
            }

        if (string.IsNullOrEmpty(wowInstallPath))
        {
            var configuredGamePath = ConfigManager.gclass61_0.method_2("GamePath");
            if (!string.IsNullOrEmpty(configuredGamePath))
                wowInstallPath = configuredGamePath;
        }

        if (string.IsNullOrEmpty(wowInstallPath))
            try
            {
                var processesByName = Process.GetProcessesByName("Wow");
                if (processesByName.Length > 0 && processesByName[0].MainModule != null)
                    wowInstallPath = Path.GetDirectoryName(processesByName[0].MainModule.FileName);
            }
            catch (Exception ex)
            {
                Logger.smethod_1("Unable to query running WoW path: " + ex.Message);
            }

        if (string.IsNullOrEmpty(wowInstallPath))
        {
            Logger.LogMessage(LogCategoryStartup + "Unable to resolve WoW install path from registry, config, or active process");
            return false;
        }

        wowInstallPath = wowInstallPath.Trim().Trim('"');
        if (!wowInstallPath.EndsWith("\\"))
            wowInstallPath += "\\";

        if (Directory.Exists(wowInstallPath))
            return true;

        Logger.LogMessage(LogCategoryStartup + "Resolved WoW path does not exist: " + wowInstallPath);
        return false;
    }

    public static void ReloadSelectedGameClass()
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

    public static void BeginBackgroundInitialization()
    {
        isInitializationSuccessful = false;
        MemoryOffsetTable.Instance = new MemoryOffsetTable();
        InitializationThread = new Thread(RunBackgroundInitializationSafe);
        InitializationThread.Start();
    }

    private static void RunBackgroundInitializationSafe()
    {
        try
        {
            ExecuteBackgroundInitialization();
        }
        catch (Exception ex)
        {
            Logger.LogMessage(MessageProvider.smethod_2(73, ex.Message + ex.StackTrace));
        }
    }

    private static void ExecuteBackgroundInitialization()
    {
        ApplicationInitializer.InitializeAndValidate(ConfigManager.gclass61_0.method_2("AppKey"), true);
        IsRuntimeAttached = false;
        if (isInputStringFourCharacters && !HasShownStartupNotice)
        {
            HasShownStartupNotice = true;
            Logger.LogMessage(MessageProvider.GetMessage(75));
            SleepMilliseconds(1000);
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
                SleepMilliseconds(2000);
                LicenseCheckTimer.method_5();
            }
        }

        if (ApplicationStartupMode == AppMode.Normal)
            Logger.LogMessage(MessageProvider.GetMessage(79));
        IsInitializationComplete = true;
        StartupLogger.imethod_0();
        IsStartupPending = false;
    }

    public static void CompleteAttachSequence()
    {
        Logger.smethod_1("--- Attach code in");
        if (IsAttached)
        {
            SoundPlayer.smethod_0("Attach.wav");
            IsDetached = true;
            RefreshStartupUiIfReady();
            Logger.smethod_1(LogCategoryAttach + "Using detached/open-memory model");
            return;
        }

        IsForegroundEnabled = false;
        AttachRefreshTimer.method_4();
        GameMemoryAccess.bool_2 = false;
        InitializeAttachOffsetManagers();
        SpellbookStateManager = new SpellbookManager();

        if (GContext.Main != null)
            GContext.Main.OnAttach();
        else
            Logger.LogMessage(LogCategoryAttach + "GContext.Main was null during attach");

        if (CurrentGameClass != null)
            CurrentGameClass.OnAttach();

        PublishRuntimeMessage(1, MessageProvider.GetMessage(98));
        RefreshStartupUiIfReady();
        EquipmentEnchantmentChecker = new EquipmentEnchantmentChecker();
        EquipmentEnchantmentChecker.method_0();
        if (GameClass69Instance != null)
            GameClass69Instance.method_0();
        else
            Logger.LogMessage(LogCategoryAttach + "ChatLogManager was null during attach");

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
        StartAutoGlide(false);
    }

    private static void InitializeAttachOffsetManagers()
    {
        PlayerOffsetManager = CreateOffsetManager("Player", "D_Player");
        ItemOffsetManager = CreateOffsetManager("Item", "D_Items");
        NpcOffsetManager = CreateOffsetManager("NPC", "D_NPC");
        ObjectOffsetManager = CreateOffsetManager("Object", "D_Object");
        ContainerOffsetManager = CreateOffsetManager("Container", "D_Container");
    }

    private static OffsetManager CreateOffsetManager(string name, string offsetKey)
    {
        var manager = new OffsetManager(name, MemoryOffsetTable.Instance.GetIntOffset(offsetKey));
        manager.PopulateOffsetList();
        return manager;
    }

    public static void DetachRuntime()
    {
        if (!IsRuntimeAttached)
        {
            Logger.smethod_1(LogCategoryDetach + "Detach requested while runtime is already detached");
            return;
        }

        IsAutoLoginTriggered = false;
        IsDetachInProgress = true;
        Logger.smethod_1(LogCategoryDetach + "Detach invoked");
        if (AttachAttemptCount == 0 && !GameMemoryAccess.smethod_56(AnotherIntegerValue))
        {
            GameMemoryAccess.CloseProcessHandle(AdditionalApplicationHandle);
            AdditionalApplicationHandle = IntPtr.Zero;
            AnotherIntegerValue = 0;
            Logger.smethod_1(LogCategoryDetach + "Closed secondary process handle during detach");
        }

        IsRuntimeAttached = false;
        if (GameClass69Instance != null)
            GameClass69Instance.method_3();
        else
            Logger.LogMessage(LogCategoryDetach + "ChatLogManager was null during detach");

        IsWorldUiReady = false;
        GameClass8Instance = null;
        PublishRuntimeMessage(1, MessageProvider.GetMessage(99));
    }

    public static void ValidateSecurityDescriptorCode(int securityCode)
    {
        if (LastSecurityCode == securityCode)
            return;
        LastSecurityCode = securityCode;
        if (!SecurityDescriptor.method_2(securityCode))
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

    public static void PublishRuntimeMessage(int messageFlags, string message)
    {
        if (RemoteViewer != null)
            RemoteViewer.method_5(messageFlags, message);
        if (GliderUIManager == null)
            return;
        if ((messageFlags & 32) > 0)
            GliderUIManager.method_1(message);
        if ((messageFlags & 2) <= 0)
            return;
        GliderUIManager.method_0(message);
    }

    public static int GetInjectedProcessIdFromCommandLine()
    {
        var num = Environment.CommandLine.IndexOf("/processid=");
        if (num != -1)
            return int.Parse(Environment.CommandLine.Substring(num + 11, 8), NumberStyles.HexNumber);
        Logger.LogMessage(MessageProvider.GetMessage(140));
        return 0;
    }

    public static bool IsIntegerInput(string input)
    {
        try
        {
            int.Parse(input);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static void SendInputSequence(string inputSequence)
    {
        for (var index = 0; index < inputSequence.Length; ++index)
        {
            var char_0 = inputSequence[index];
            var flag = true;
            if (char_0 == '#' && index < inputSequence.Length - 1)
            {
                if (inputSequence[index + 1] == '#')
                {
                    flag = true;
                    ++index;
                }
                else
                {
                    var num = inputSequence.IndexOf('#', index + 1);
                    if (num > -1)
                        try
                        {
                            var short_0 = (short)int.Parse(inputSequence.Substring(index + 1, num - index - 1));
                            flag = false;
                            InputController.TapKey(short_0);
                            index = num;
                        }
                        catch (Exception ex)
                        {
                            Logger.smethod_1(MessageProvider.smethod_2(144, inputSequence));
                        }
                }
            }

            if (char_0 == '|')
            {
                if (index < inputSequence.Length - 1 && inputSequence[index + 1] == '|')
                {
                    flag = true;
                    ++index;
                }
                else
                {
                    flag = false;
                    InputController.TapKey(13);
                    Thread.Sleep(700);
                }
            }

            if (flag)
                InputController.smethod_6(char_0);
        }
    }

    public static bool StartManualMode(bool isHotkeyTriggered)
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
            TryAttachAndInitializeRuntime();
            if (!IsRuntimeAttached)
                return false;
        }

        if (GPlayerSelf.Me.TargetGUID == 0L)
        {
            Logger.LogMessage(MessageProvider.GetMessage(115));
            return false;
        }

        if (!ConfigManager.gclass61_0.method_5("BackgroundEnable"))
            BringGameToForeground();
        CurrentGlideMode = GlideMode.Manual;
        ManualGlideController = new GlideMainThread();
        return true;
    }

    public static void BringGameToForeground()
    {
        SleepMilliseconds(500);
        GameMemoryAccess.SetForegroundWindow(MainApplicationHandle);
        SleepMilliseconds(500);
    }

    public static bool AddCurrentWaypoint()
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

    public static bool StartAutoGlide(bool isHotkeyTriggered)
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
            TryAttachAndInitializeRuntime();
            if (!IsRuntimeAttached)
                return false;
        }

        if (HasClassLoadMismatch)
        {
            Logger.LogMessage(MessageProvider.GetMessage(118));
            return false;
        }

        if (IsDetached)
            return TryStartDetachedMachGlide();
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
            BringGameToForeground();
        CurrentGlideMode = GlideMode.Auto;
        ActiveCombatController = new CombatController();
        if (ActiveCombatController.method_1())
            return true;
        CurrentGlideMode = GlideMode.None;
        return false;
    }

    public static bool TryAttachAndInitializeRuntime()
    {
        GameMemoryAccess.bool_2 = false;
        IsRuntimeAttached = false;
        if (!TryAttachToProcessAndResolveState())
        {
            Logger.smethod_1(LogCategoryAttach + "Attach probe did not resolve a valid runtime context");
            return false;
        }

        CompleteAttachSequence();
        return IsRuntimeAttached;
    }

    private static bool TryStartDetachedMachGlide()
    {
        GameProcessManager = new MachGlideRunner();
        if (!GameProcessManager.method_0())
        {
            CurrentGlideMode = GlideMode.None;
            return false;
        }

        return true;
    }

    public static bool TryAddCurrentTargetFaction()
    {
        if (!IsRuntimeAttached)
            return false;

        if (ActiveProfile == null)
            return false;

        var target = GPlayerSelf.Me.Target;
        if (target == null || !target.IsValid)
        {
            Logger.smethod_1(LogCategoryStartup + "Unable to add faction: no valid target selected");
            return false;
        }

        var factionId = target.FactionID;
        if (factionId == 0)
        {
            Logger.smethod_1(LogCategoryStartup + "Unable to add faction: target faction id is zero");
            return false;
        }

        if (HasFaction(ActiveProfile.Factions, factionId))
        {
            Logger.smethod_1(LogCategoryStartup + "Faction already present in profile: " + factionId);
            return true;
        }

        ActiveProfile.AddFaction(factionId);
        IsProfileDirty = true;
        Logger.LogMessage(MessageProvider.GetMessage(129));
        return true;
    }

    private static bool HasFaction(int[] factions, int factionId)
    {
        if (factions == null)
            return false;

        foreach (var faction in factions)
            if (faction == factionId)
                return true;

        return false;
    }

    public static void StopGlide(bool detachAfterStop, string reason)
    {
        if (CurrentGlideMode == GlideMode.None && !detachAfterStop)
            return;
        var flag = false;
        try
        {
            ++KillActionDepth;
            ExecuteStopGlideCore(detachAfterStop, reason);
        }
        catch (ThreadInterruptedException)
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

    private static void ExecuteStopGlideCore(bool detachAfterStop, string reason)
    {
        lock (killActionLock)
        {
            var flag = false;
            if (CurrentGlideMode != GlideMode.None || IsRuntimeAttached && detachAfterStop)
            {
                ResetWindowDisplayState();
                CameraController.method_3(true);
                Logger.smethod_1(MessageProvider.smethod_2(652, detachAfterStop, (int)CurrentGlideMode, reason));
                CameraController.method_3(true);
                InputController.smethod_21(false);
                if (CurrentGlideMode == GlideMode.Auto)
                {
                    if (detachAfterStop)
                        DetachAfterStopRequested = true;
                    if (CurrentGameClass != null)
                        CurrentGameClass.OnStopGlide();
                    PublishRuntimeMessage(1, MessageProvider.GetMessage(100));
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
                    PublishRuntimeMessage(1, MessageProvider.GetMessage(101));
                    if (ManualGlideController != null && Thread.CurrentThread == ManualGlideController.thread_0)
                        flag = true;
                    Logger.smethod_1(MessageProvider.GetMessage(102));
                    CurrentGlideMode = GlideMode.None;
                    if (ManualGlideController != null)
                        ManualGlideController.method_0();
                    ManualGlideController = null;
                }

                if (detachAfterStop)
                    DetachRuntime();
                StartupLogger.imethod_0();
                GContext.Main.ReleaseAllKeys();
                InputController.smethod_21(false);
                if (flag)
                    throw new ThreadInterruptedException();
            }
        }
    }

    public static int GetExperiencePerHour()
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

    private static void StartKillEventListenerIfRequested()
    {
        if (Environment.CommandLine.ToLower().IndexOf("/kill") == -1)
            return;

        new Thread(WaitForKillEventAndShutdown).Start();
    }

    public static void ShutdownRuntimeServices()
    {
        GameMemoryWriter.method_0();
        CodeCompiler.smethod_4();
        GliderUIManager.method_5();
    }

    private static void WaitForKillEventAndShutdown()
    {
        var killEventName = GetCommandLineValue("/kill");
        KillEventHandle = CreateEvent(IntPtr.Zero, false, false, killEventName);
        if (KillEventHandle == IntPtr.Zero)
        {
            Logger.LogMessage("Couldn't create named event");
            return;
        }

        WaitForSingleObject(KillEventHandle, KillEventWaitTimeout);
        CloseHandle(KillEventHandle);
        if (!ShouldProcessKillEvent)
            return;

        ExecuteImmediateShutdown();
    }

    private static void ExecuteImmediateShutdown()
    {
        SoundPlayer.smethod_1("GliderExit.wav");

        if (KnownDebuffs != null)
            KnownDebuffs.method_10();

        StartupLogger.imethod_4();
        ShutdownRuntimeServices();
        Environment.Exit(0);
    }

    public static void SignalKillEventOrExit()
    {
        if (KillEventHandle == IntPtr.Zero)
            ExecuteImmediateShutdown();
        SetEvent(KillEventHandle);
    }

    public static void SuppressKillEventAndSignal()
    {
        if (KillEventHandle == IntPtr.Zero)
            return;
        ShouldProcessKillEvent = false;
        SetEvent(KillEventHandle);
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr CreateEvent(
        IntPtr attributes,
        bool manualReset,
        bool initialState,
        string eventName);

    [DllImport("kernel32", SetLastError = true)]
    internal static extern int WaitForSingleObject(IntPtr handle, uint timeoutMilliseconds);

    [DllImport("Kernel32.dll", SetLastError = true)]
    private static extern void SetEvent(IntPtr handle);

    public static string GetCommandLineValue(string argumentName)
    {
        var num1 = Environment.CommandLine.IndexOf(argumentName + "=");
        if (num1 == -1)
        {
            Logger.LogMessage(MessageProvider.GetMessage(759));
            return null;
        }

        var startIndex = num1 + argumentName.Length + 1;
        var num2 = Environment.CommandLine.IndexOf(' ', startIndex);
        if (num2 == -1)
            num2 = Environment.CommandLine.Length;
        return Environment.CommandLine.Substring(startIndex, num2 - startIndex);
    }

    [DllImport("kernel32")]
    private static extern bool CloseHandle(IntPtr handle);

    public static void RunMainLoopTick()
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

            ExecuteMainLoopTick();
        }
        finally
        {
            Interlocked.Exchange(ref MainLoopTickGuard, 0);
        }
    }

    private static void ExecuteMainLoopTick()
    {
        LogMainLoopStep("Tick start");

        if (IsExitRequested)
        {
            LogMainLoopStep("Exit requested, skipping tick");
            return;
        }

        TryAutoAttachIfReady();
        LogMainLoopStep("Attach/refresh check completed");

        RunResumeHandshakeIfNeeded();
        RunProcessProbeIfNeeded();
        RunDeferredCompilationIfNeeded();

        if (!IsRuntimeAttached)
        {
            LogMainLoopStep("Not attached, tick complete");
            return;
        }

        if (!TryRefreshObjectState(out GPlayerSelf me))
            return;

        RefreshDebuffStateIfNeeded();
        UpdatePlayerStanceIfNeeded(me);
        EnsureWorldUiReady(me);

        RunUiUpdateTick();
        ApplyBackgroundModeIfNeeded();
        RunInputMaintenance();

        LogMainLoopStep("Tick complete");
    }

    private static void RunResumeHandshakeIfNeeded()
    {
        if (!IsResumeRequested || !IsInitializationComplete)
            return;

        LogMainLoopStep("Security handshake check");
        GameMemoryAccess.smethod_53();
        if (GameMemoryWriter != null && (ApplicationStartupMode == AppMode.Normal || ApplicationStartupMode == AppMode.Invisible))
            GameMemoryWriter.method_2("OnGliderStart", false);
    }

    private static void RunProcessProbeIfNeeded()
    {
        if (!SpellCooldownTimer.IsReady)
            return;

        LogMainLoopStep("Process probe timer fired");
        SpellCooldownTimer.Reset();
        GameMemoryAccess.bool_3 = GameMemoryAccess.IsWowProcessRunning();
        GameMemoryAccess.GetProcessId();
    }

    private static void RunDeferredCompilationIfNeeded()
    {
        if (!NeedsDeferredCompile || IsAttached || HasDeferredCompileRun)
            return;

        LogMainLoopStep("Deferred startup compilation");
        HasDeferredCompileRun = true;
        CodeCompiler.smethod_14();
        ReloadSelectedGameClass();
    }

    private static bool TryRefreshObjectState(out GPlayerSelf player)
    {
        player = null;

        LogMainLoopStep("Refreshing object list");
        GObjectList.GetObjects();
        player = GPlayerSelf.Me;
        if (player != null)
            return true;

        LogMainLoopStep("Player object unavailable, tick complete");
        return false;
    }

    private static void RefreshDebuffStateIfNeeded()
    {
        if (KnownDebuffs == null || !DebuffRefreshTimer.method_3())
            return;

        LogMainLoopStep("Debuff cache refresh");
        DebuffRefreshTimer.method_4();
        KnownDebuffs.method_8();
    }

    private static void UpdatePlayerStanceIfNeeded(GPlayerSelf player)
    {
        if (player.Stance == CurrentStance)
            return;

        LogMainLoopStep("Stance changed");
        if (CurrentStance != GStance.Unknown)
            GContext.Main.Interface.UnFillAllKeys();

        CurrentStance = player.Stance;
    }

    private static void RunUiUpdateTick()
    {
        LogMainLoopStep("Running chat/dialog updates");
        GameClass69Instance.method_4();
        DialogMonitor.smethod_1();
        if (GameClass8Instance == null || !GameClass8Instance.method_10() || CurrentGlideMode != GlideMode.Auto)
            return;

        LogMainLoopStep("Auto mode popup dismiss");
        InputController.TapKey(27);
    }

    private static void ApplyBackgroundModeIfNeeded()
    {
        if (CurrentGlideMode != GlideMode.Auto || !IsGliderInitialized || IsGliderRunning)
            return;

        if (ConfigManager.gclass61_0.method_2("BackgroundDisplay") == "Normal")
            return;

        if ((DateTime.Now - SessionStartTime).TotalSeconds < 8.0)
            return;

        LogMainLoopStep("Applying background display state");
        IsGliderRunning = true;
        ApplyBackgroundDisplayMode();
    }

    private static void RunInputMaintenance()
    {
        LogMainLoopStep("Running camera/input maintenance");
        CameraController.method_7();
        InputController.smethod_21(true);
    }

    private static bool IsVerboseMainLoopLoggingEnabled()
    {
        return ConfigManager.gclass61_0 != null && ConfigManager.gclass61_0.method_5("VerboseMainLoopLogging");
    }

    private static void LogMainLoopStep(string step)
    {
        if (!IsVerboseMainLoopLoggingEnabled() || StartupLogger == null)
            return;

        StartupLogger.imethod_2("[Debug] [MainLoop] " + step);
    }

    private static void EnsureWorldUiReady(GPlayerSelf player)
    {
        if (IsWorldUiReady || player == null || CurrentPlayerGuid == 0UL || player.BaseAddress == 0)
            return;

        if (GContext.Main == null || GContext.Main.Interface == null || GContext.Main.Interface.IsPreWorldVisible)
            return;

        DialogMonitor.smethod_0();
        GameClass8Instance = UIElement.smethod_2("GameMenuFrame");
        IsWorldUiReady = true;
    }

    public static void SleepMilliseconds(int milliseconds)
    {
        Thread.Sleep(milliseconds);
    }

    public static void PauseAfterCombatVanishDetection()
    {
        Thread.Sleep(12);
        Thread.Sleep(47);
        Thread.Sleep(2382);
        Thread.Sleep(2);
    }

    public static bool HandleTargetVanishDuringCombat(GUnit targetUnit)
    {
        if (targetUnit != null && targetUnit.IsValid)
        {
            if (GameClass69Instance == null || GameClass69Instance.method_10() >= 10)
                return false;
            Logger.LogMessage(MessageProvider.GetMessage(830));
            if (CurrentGlideMode == GlideMode.Auto)
                ActiveProfile.ForceBlacklist(targetUnit.GUID);
            CombatController.smethod_1();
            return true;
        }

        Logger.LogMessage(MessageProvider.GetMessage(517));
        if (ConfigManager.gclass61_0.method_5("StopOnVanish"))
        {
            GContext.Main.Movement.LookConfused();
            SoundPlayer.smethod_0("GMWhisper.wav");
            StopGlide(true, "TargetVanishedInCombat");
        }

        return true;
    }

    public static string GetFileNameFromPath(string path)
    {
        var separatorIndex = path.LastIndexOf("\\");
        return separatorIndex == -1 ? path : path.Substring(separatorIndex + 1);
    }

    public static string GetDirectoryFromPath(string path)
    {
        var separatorIndex = path.LastIndexOf("\\");
        return separatorIndex == -1 ? path : path.Substring(0, separatorIndex);
    }

    public static bool TryAttachToProcessAndResolveState()
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

        if (!EnsureAttachProcessHandle())
            return false;

        if (IsAttached)
            return true;

        TryAutoLoginFromUiParentProbe();

        if (TryDirectAttachProbe())
            return true;

        CurrentPlayerGuid = 0UL;
        if (!TryResolveObjectManagerPointer())
            return false;

        var isObjectManagerGuidKnown = TryResolvePlayerGuidFromObjectManager();
        if (CurrentPlayerGuid == 0UL)
        {
            ulong wotlkGuid;
            if (TryResolvePlayerGuidFromGuaranteedWotlkOffsets(out wotlkGuid))
            {
                CurrentPlayerGuid = wotlkGuid;
                isObjectManagerGuidKnown = true;
            }
        }

        if (CurrentPlayerGuid == 0UL && !TryResolvePlayerGuidFromPlayerIdAddresses())
        {
            Logger.smethod_1("Attach probe failed: Player GUID is zero across local and static sources");
            return false;
        }

        return ValidateAttachedPlayerGuid(isObjectManagerGuidKnown);
    }

    public static bool TryAttachRuntimeProbe()
    {
        return TryAttachToProcessAndResolveState();
    }

    private static bool EnsureAttachProcessHandle()
    {
        if (AdditionalApplicationHandle != IntPtr.Zero || SkipHandleOpen)
            return true;

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

        return true;
    }

    private static void TryAutoLoginFromUiParentProbe()
    {
        var uiParentAddress = MemoryOffsetTable.Instance.GetIntOffset("UIParent");
        if (uiParentAddress <= 0)
            return;

        if (GameMemoryAccess.ReadInt32(uiParentAddress, "probeuip") != 0)
            return;

        if (IsAutoLoginTriggered)
            return;

        if (!((IsAutoLoginArmed && AutoLoginTimer.IsReady) || IsForegroundEnabled))
            return;

        var autoLogSetting = ConfigManager.gclass61_0.method_2("AutoLog");
        if (autoLogSetting != null && autoLogSetting.Length > 0 && IsSomeConditionMet && new AutoLoginManager().method_2())
        {
            IsAutoLoginTriggered = true;
            GameMemoryWriter.method_2("DoAutoLog", false);
        }

        Logger.smethod_1("Attach probe note: UIParent resolved to zero, continuing with TLS/static attach checks");
    }

    private static bool TryDirectAttachProbe()
    {
        if (!GameMemoryAccess.smethod_52(out CurrentPlayerGuid, out ResolvedMainTableAddress) || CurrentPlayerGuid == 0UL)
        {
            Logger.smethod_1("Direct attach probe failed, trying static offsets fallback");
            return false;
        }

        if (!IsLikelyPlayerGuid(CurrentPlayerGuid))
        {
            Logger.smethod_1("Direct attach probe rejected implausible GUID: 0x" + CurrentPlayerGuid.ToString("x"));
            CurrentPlayerGuid = 0UL;
            return false;
        }

        if (GObjectList.StealthCountGameObjects(CurrentPlayerGuid) > 0)
            return true;

        Logger.smethod_1("Direct attach probe failed object validation, trying static offsets fallback");
        return false;
    }

    private static bool TryResolveObjectManagerPointer()
    {
        var baseMainTable = GameMemoryAccess.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("MainTable"), "MainTable");
        var resolvedMainTable = baseMainTable;

        if (MemoryOffsetTable.Instance.HasOffset("MainTableProbe") && MemoryOffsetTable.Instance.GetIntOffset("MainTableProbe") > 0)
        {
            var probeOffset = MemoryOffsetTable.Instance.GetIntOffset("MainTableProbe");
            var candidatePointer = GameMemoryAccess.ReadInt32(baseMainTable + probeOffset, "MainTableProbe");
            var candidateAddress = baseMainTable + probeOffset;

            if (IsValidMainTablePointer(candidatePointer))
                resolvedMainTable = candidatePointer;
            else if (IsValidMainTablePointer(candidateAddress))
                resolvedMainTable = candidateAddress;
            else
                resolvedMainTable = candidatePointer;
        }

        ResolvedMainTableAddress = resolvedMainTable;
        if (ResolvedMainTableAddress != 0)
            return true;

        Logger.smethod_1("Attach probe failed: resolved MainTable pointer is zero");
        return false;
    }

    private static bool TryResolvePlayerGuidFromObjectManager()
    {
        var usedActivePlayerPointer = false;
        var hasKnownObjectManagerGuid = false;

        var activePlayerOffset = MemoryOffsetTable.Instance.HasOffset("MainTableActivePlayer")
            ? MemoryOffsetTable.Instance.GetIntOffset("MainTableActivePlayer")
            : 24;

        if (activePlayerOffset > 0)
        {
            var activePlayerAddress = GameMemoryAccess.ReadInt32(ResolvedMainTableAddress + activePlayerOffset, "MainTableActivePlayer");
            if (IsLikelyObjectAddress(activePlayerAddress))
            {
                var activePlayerGuid = GameMemoryAccess.ReadInt64(activePlayerAddress + 48, "MainTableActivePlayerGuid");
                if (activePlayerGuid != 0UL && IsLikelyPlayerGuid(activePlayerGuid))
                {
                    CurrentPlayerGuid = activePlayerGuid;
                    usedActivePlayerPointer = true;
                    hasKnownObjectManagerGuid = true;
                    Logger.smethod_1("Attach probe: using active player object GUID = 0x" + CurrentPlayerGuid.ToString("x"));
                }
            }
        }

        var localGuidOffset = MemoryOffsetTable.Instance.HasOffset("MainTableLocalGuid")
            ? MemoryOffsetTable.Instance.GetIntOffset("MainTableLocalGuid")
            : 40;

        if (!usedActivePlayerPointer && localGuidOffset > 0)
        {
            var localGuid = GameMemoryAccess.ReadInt64(ResolvedMainTableAddress + localGuidOffset, "MainTableLocalGuid");
            if (localGuid != 0UL && IsLikelyPlayerGuid(localGuid))
            {
                CurrentPlayerGuid = localGuid;
                hasKnownObjectManagerGuid = true;
                Logger.smethod_1("Attach probe: using object manager local GUID = 0x" + CurrentPlayerGuid.ToString("x"));
            }
            else if (localGuid != 0UL)
            {
                Logger.smethod_1("Attach probe note: rejected implausible object manager local GUID = 0x" + localGuid.ToString("x"));
            }
        }

        return hasKnownObjectManagerGuid;
    }

    private static bool TryResolvePlayerGuidFromPlayerIdAddresses()
    {
        var configuredPlayerIdAddress = MemoryOffsetTable.Instance.GetIntOffset("PlayerIdAddr");
        var playerIdCandidates = new int[] { configuredPlayerIdAddress, 0x00CD87A8, 0x00BD07A8 };

        for (var candidateIndex = 0; candidateIndex < playerIdCandidates.Length; ++candidateIndex)
        {
            var candidateAddress = playerIdCandidates[candidateIndex];
            if (candidateAddress == 0)
                continue;

            var playerGuidBytes = GameMemoryAccess.ReadBytesRaw(candidateAddress, 8);
            if (playerGuidBytes != null)
            {
                var playerGuid = BitConverter.ToUInt64(playerGuidBytes, 0);
                if (playerGuid != 0UL && IsLikelyPlayerGuid(playerGuid))
                {
                    CurrentPlayerGuid = playerGuid;
                    if (candidateAddress != configuredPlayerIdAddress)
                        Logger.smethod_1("Attach probe: using fallback PlayerIdAddr 0x" + candidateAddress.ToString("x"));

                    return true;
                }
            }

            var playerObjectPointer = GameMemoryAccess.ReadInt32(candidateAddress, "PlayerIdAddrPointer");
            if (!IsLikelyObjectAddress(playerObjectPointer))
                continue;

            var playerObjectGuid = GameMemoryAccess.ReadInt64(playerObjectPointer + 48, "PlayerIdAddrPointerGuid");
            if (playerObjectGuid == 0UL || !IsLikelyPlayerGuid(playerObjectGuid))
                continue;

            CurrentPlayerGuid = playerObjectGuid;
            Logger.smethod_1("Attach probe: using PlayerIdAddr object pointer path at 0x" + candidateAddress.ToString("x") +
                             " -> object 0x" + playerObjectPointer.ToString("x") + " -> GUID 0x" + CurrentPlayerGuid.ToString("x"));
            return true;
        }

        return false;
    }

    private static bool ValidateAttachedPlayerGuid(bool objectManagerGuidKnown)
    {
        var validationCount = GObjectList.StealthCountGameObjects(CurrentPlayerGuid);
        if (validationCount > 0)
            return true;

        ulong inferredGuid;
        if (GObjectList.TryGetLikelyPlayerGuid(out inferredGuid))
        {
            if (objectManagerGuidKnown && inferredGuid <= 4096UL)
            {
                Logger.smethod_1("Attach probe note: ignoring low inferred GUID candidate because object manager GUID is already known");
                return true;
            }

            CurrentPlayerGuid = inferredGuid;
            Logger.smethod_1("Attach probe: inferred player GUID from object list = 0x" + CurrentPlayerGuid.ToString("x"));
            validationCount = GObjectList.StealthCountGameObjects(CurrentPlayerGuid);
            if (validationCount > 1)
                return true;
        }

        Logger.smethod_1("Attach probe failed: object validation count too low = " + validationCount);
        return false;
    }

    private static bool IsLikelyObjectAddress(int objectAddress)
    {
        if ((objectAddress & 1) != 0 || objectAddress == 0 || objectAddress == 28 || objectAddress < 65536)
            return false;
        var objectTypeBytes = GameMemoryAccess.ReadBytesRaw(objectAddress + 20, 4);
        if (objectTypeBytes == null || objectTypeBytes.Length < 4)
            return false;
        var objectType = BitConverter.ToInt32(objectTypeBytes, 0);
        return objectType >= 1 && objectType <= 7;
    }

    private static bool IsLikelyPlayerGuid(ulong playerGuid)
    {
        return playerGuid != 0UL;
    }

    private static bool IsLikelyMemoryPointer(int pointer)
    {
        return (pointer & 1) == 0 && pointer != 0 && pointer != 28 && pointer >= 65536;
    }

    private static bool TryResolvePlayerGuidFromGuaranteedWotlkOffsets(out ulong playerGuid)
    {
        playerGuid = 0UL;
        var clientConnection = GameMemoryAccess.ReadInt32(GameMemoryConstants.Wotlk.ClientConnection, "WotlkClientConnection");
        if (IsLikelyMemoryPointer(clientConnection))
        {
            var objectManager = GameMemoryAccess.ReadInt32(clientConnection + GameMemoryConstants.Wotlk.CurMgrOffset, "WotlkObjectManager");
            if (IsLikelyMemoryPointer(objectManager))
            {
                var objectManagerGuid = GameMemoryAccess.ReadInt64(objectManager + GameMemoryConstants.Wotlk.LocalGuid,
                    "WotlkObjectManagerLocalGuid");
                if (IsLikelyPlayerGuid(objectManagerGuid))
                {
                    playerGuid = objectManagerGuid;
                    Logger.smethod_1("Attach probe: using WotLK ClientConnection/ObjectManager GUID path = 0x" + playerGuid.ToString("x"));
                    return true;
                }

                if (objectManagerGuid != 0UL)
                    Logger.smethod_1("Attach probe note: rejected WotLK ObjectManager GUID = 0x" + objectManagerGuid.ToString("x"));
            }
        }

        var staticGuid = GameMemoryAccess.ReadInt64(GameMemoryConstants.Wotlk.PlayerIdAddr, "WotlkStaticPlayerGuid");
        if (IsLikelyPlayerGuid(staticGuid))
        {
            playerGuid = staticGuid;
            Logger.smethod_1("Attach probe: using WotLK static PlayerGUID address 0x" + GameMemoryConstants.Wotlk.PlayerIdAddr.ToString("x") +
                             " = 0x" + playerGuid.ToString("x"));
            return true;
        }

        if (staticGuid != 0UL)
            Logger.smethod_1("Attach probe note: rejected WotLK static PlayerGUID value = 0x" + staticGuid.ToString("x"));

        return false;
    }

    private static bool IsValidMainTablePointer(int candidateAddress)
    {
        if (candidateAddress == 0)
            return false;
        var firstObjectAddress = GameMemoryAccess.ReadInt32(candidateAddress + MemoryOffsetTable.Instance.GetIntOffset("InitialOffset"), "MainTableFirstProbe");
        if ((firstObjectAddress & 1) != 0 || firstObjectAddress == 0 || firstObjectAddress == 28 || firstObjectAddress < 65536)
            return false;
        var objectType = GameMemoryAccess.ReadInt32(firstObjectAddress + 20, "MainTableFirstTypeProbe");
        return objectType >= 1 && objectType <= 7;
    }

    public static void TryAutoAttachIfReady()
    {
        if (IsRuntimeAttached || IsDetached || !isInitializationSuccessful || !TryAttachToProcessAndResolveState())
            return;
        CompleteAttachSequence();
    }

    public static void ApplyBackgroundDisplayMode()
    {
        switch (ConfigManager.gclass61_0.method_2("BackgroundDisplay"))
        {
            case "Hide":
                HideGameWindow();
                break;
            case "Shrink":
                ShrinkGameWindow();
                break;
        }
    }

    public static void HideGameWindow()
    {
        if (IsWindowHidden)
            return;
        GameMemoryAccess.SetForegroundWindow(MainApplicationHandle);
        IsWindowHidden = true;
    }

    public static void ShrinkGameWindow()
    {
        if (IsWindowShrunk)
            return;
        double width = ConfigManager.gclass61_0.method_3("ShrinkWidth");
        GameMemoryAccess.GetWindowSize(MainApplicationHandle, out OriginalWindowSize);
        var height = OriginalWindowSize.Height / (double)OriginalWindowSize.Width * width;
        GameMemoryAccess.SetWindowSize(MainApplicationHandle, new Size((int)width, (int)height));
        IsWindowShrunk = true;
    }

    public static void ShowGameWindow()
    {
        if (!IsWindowHidden)
            return;
        GameMemoryAccess.ShowWindow(MainApplicationHandle);
        IsWindowHidden = false;
    }

    public static void RestoreGameWindowSize()
    {
        if (!IsWindowShrunk)
            return;
        GameMemoryAccess.SetWindowSize(MainApplicationHandle, OriginalWindowSize);
        IsWindowShrunk = false;
    }

    public static void ResetWindowDisplayState()
    {
        ShowGameWindow();
        RestoreGameWindowSize();
    }

    private static void ApplyLaunchNameOverride()
    {
        if (Environment.CommandLine.ToLower().IndexOf("/ln") == -1)
            return;
        var launchName = GetCommandLineValue("/ln");
        ConfigManager.gclass61_0.method_0("LName", launchName);
        ConfigManager.gclass61_0.method_8();
    }

    private static void ProcessDriverAndAttachPidFlags()
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
            AttachAttemptCount = int.Parse(GetCommandLineValue("/attachpid"));
            Logger.LogMessage("/attachpid specified, looking for: " + AttachAttemptCount);
        }

        if (!ConfigManager.gclass61_0.method_5("UnloadShadow"))
            return;
    }



    public static void PrepareAutoLoginFromConfig()
    {
        IsStopRequested = false;
        if (!IsSomeConditionMet)
        {
            Logger.LogMessage(MessageProvider.GetMessage(868));
            CombatController.smethod_0(869);
        }
        else
        {
            var autoLogValue = ConfigManager.gclass61_0.method_2("AutoLog");
            if (!new AutoLoginManager().method_1(autoLogValue))
                return;
            AutoLoginSetting = autoLogValue;
            Logger.smethod_1("Autolog is good!");
            IsStopRequested = true;
        }
    }

    public static bool IsDecryptedStreamEmpty(GDataEncryptionManager encryptedDataManager)
    {
        return encryptedDataManager.ReadIntFromDecryptedStream() == 0;
    }
    public static void InitializeBackgroundModeIfNeeded()
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

    public static string GetMacroNameById(int macroId)
    {
        var num1 = MemoryOffsetTable.Instance.GetIntOffset("MacroBase");
        var num2 = GameMemoryAccess.ReadInt32(num1 + 36, "mbase");
        int int_29_1;
        for (var int_29_2 =
                 GameMemoryAccess.ReadInt32(GameMemoryAccess.ReadInt32(num1 + 28, "mbase2") + 12 * (macroId & num2) + 8, "mbase3");
             int_29_2 > 0 && (int_29_2 & 1) == 0;
             int_29_2 = GameMemoryAccess.ReadInt32(int_29_2 + GameMemoryAccess.ReadInt32(int_29_1, "mnext3") + 4, "mnext4"))
        {
            var num3 = GameMemoryAccess.ReadInt32(int_29_2, "mstep");
            var str = GameMemoryAccess.ReadString(int_29_2 + 36, 64, "mname");
            if (num3 == macroId)
                return str;
            int_29_1 = GameMemoryAccess.ReadInt32(num1 + 28, "mnext1") +
                       12 * (GameMemoryAccess.ReadInt32(num1 + 36, "mnext2") & macroId);
        }

        return "(could not find macro in list!)";
    }
}

