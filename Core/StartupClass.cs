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
    public static int badTagCount;
    public static UIElement GameClass8Instance;
    public static ChatLogManager GameClass69Instance;
    public static bool IsLicenseValid;
    public static SortedList<long, LootableCorpseTracker> ProfileIdToProfileMap;
    public static AppMode ApplicationStartupMode;
    public static IntPtr MainApplicationHandle = IntPtr.Zero;
    public static string wowInstallPath;
    public static int wowProcessId;
    public static IntPtr AdditionalApplicationHandle;
    public static bool IsGameProcessAttached;
    public static GProfile ActiveGProfile;
    public static string currentProfilePath;
    public static bool IsOpenMemoryModel = false;
    public static CameraRotator cameraRotator;
    public static OffsetManager playerOffsetManager;
    public static OffsetManager npcOffsetManager;
    public static OffsetManager objectOffsetManager;
    public static OffsetManager itemOffsetManager;
    public static OffsetManager containerOffsetManager;
    public static int pgEditProfileCount;
    public static long playerGuid;
    public static int objectManagerBasePointer;
    public static SortedList corpseSortedList = new SortedList();
    public static bool IsLoading = true;
    public static int initCount = 1;
    public static DateTime sessionStartTime;
    public static string wowVersionLabel = "";
    public static bool IsProfileModified;
    public static bool IsBetaVersion = false;
    public static bool IsBetaAccessGranted;
    public static GameTimer licenseCheckTimer;
    public static GlideMode currentGlideMode;
    public static int knownVersion;
    public static int expectedVersion;
    public static int versionPatchLevel;
    public static bool IsVersionMismatch;
    public static Thread initializationThread;
    public static PartyManager partyManager;
    public static bool IsAutoLoginPending;
    public static bool IsFocusTimerActive;
    public static GameTimer resumeTimer;
    public static KeyboardHookManager keyboardHookManager;
    public static SecurityDescriptorHelper securityDescriptorHelper;
    public static bool isInitializationSuccessful;
    public static RemoteViewerServer remoteViewerServer;
    public static GameClass selectedGameClass;
    public static bool HasShownKeyAlert;
    public static bool isInputStringFourCharacters = true;
    public static DateTime expiryTime;
    public static bool isTimeAdded;
    public static bool legacyUnusedFlag = false;
    public static ILogger uiLogger;
    public static bool IsInitializationComplete;
    public static int lastAclProcessId;
    public static CombatController combatController;
    public static byte[] byte_0 = null;
    public static bool IsPendingStop = false;
    public static GlideMainThread glideMainThread;
    public static string barCharacters = "1234567890-=";
    public static int cachedGlideRate;
    public static WaypointType waypointMode = WaypointType.const_0;
    public static bool NeedsClassReload;
    public static EquipmentEnchantmentChecker enchantmentChecker;
    public static double autoAddDistance;
    public static DebuffDatabase DebuffsKnown_string;
    public static string[] friendWhitelist;
    public static ProfileGroupManager profileGroupManager;
    public static int attachPidOverride;
    public static Random rng;
    public static LootRouteParser lootRouteParser;
    public static GameTimer attachCooldownTimer = new GameTimer(6000);
    public static bool IsResumeMode;
    public static bool IsLoginTimerActive = false;
    public static GSpellTimer loginCooldownTimer = null;
    public static bool IsDetaching;
    public static string pendingAutoLoginName;
    public static bool IsDriverResident;
    public static bool IsDebugMode;
    public static bool HasLoggedNoProcessFound;
    public static bool ShouldReattachAfterStop;
    public static bool IsClassInitializing;
    public static bool NeedsClassInit;
    public static SpellbookManager spellbookManager;
    private static readonly object killActionLock = new object();
    private static int killActionNestingCount;
    public static IntPtr killEventHandle = IntPtr.Zero;
    public static bool IsKillEventArmed = true;
    public static uint infiniteWaitTimeout = uint.MaxValue;
    private static readonly GameTimer debuffUpdateTimer = new GameTimer(3500);
    private static GameTimer staleTimer = new GameTimer(6000);
    private static readonly GSpellTimer networkCheckInterval = new GSpellTimer(1080000, true);
    private static string deferredString = null;
    private static Size originalWindowSize;
    public static bool IsWindowHidden;
    public static bool IsWindowShrunk;

    public static void InitStartupMode(AppMode appMode_1)
    {
        ApplicationStartupMode = appMode_1;
        ProfileMapping = new SortedList<string, SpellActionData>();
        ProfileIdToProfileMap = new SortedList<long, LootableCorpseTracker>();
        if (appMode_1 == AppMode.PGEdit)
        {
            ConfigManager.gclass61_0 = new ConfigManager();
            MessageProvider.smethod_0(".\\");
            rng = new Random();
            ActiveGProfile = null;
            currentProfilePath = null;
            initializationThread = null;
            partyManager = new PartyManager();
            pgEditProfileCount = 1;
            if (ConfigManager.gclass61_0.method_2("LastProfile") != null)
            {
                LoadProfile(ConfigManager.gclass61_0.method_2("LastProfile"));
            }
            else
            {
                ActiveGProfile = new GProfile();
                currentProfilePath = MessageProvider.GetMessage(70);
            }

            var gcontext = new GContext();
            if (appMode_1 != AppMode.PGEdit)
                CodeCompiler.smethod_10();
            InputController.smethod_31(ConfigManager.gclass61_0);
            ApplyConfig();
            SpellcastingManager.gclass42_0 = new SpellcastingManager();
            SpellcastingManager.gclass42_0.method_12();
            ResolveWowVersion();
            if (appMode_1 == AppMode.PGEdit)
                return;
            if (ConfigManager.gclass61_0.method_2("AppKey") != "demo")
                CodeCompiler.smethod_14();
            SelectActiveGameClass();
        }
        else
        {
            Logger.LogMessage("Glider 1.8.0 starting up (Release)");
            ConfigManager.gclass61_0 = new ConfigManager();
            IsBetaAccessGranted = true;
            if (Environment.CommandLine.ToLower().IndexOf("/l1") != -1)
                IsDebugMode = true;
            if (Environment.CommandLine.ToLower().IndexOf("/mach") != -1)
            {
                IsAttached = true;
                Logger.LogMessage("Mach flag, using open memory model");
            }

            if (Environment.CommandLine.ToLower().IndexOf("/resume") != -1)
                IsResumeMode = true;
            MessageProvider.smethod_0(".\\");
            securityDescriptorHelper = new SecurityDescriptorHelper();
            securityDescriptorHelper.method_1();
            GameClass32Instance = null;
            rng = new Random();
            licenseCheckTimer = new GameTimer(10000);
            currentGlideMode = GlideMode.None;
            wowVersionLabel = "0.0";
            knownVersion = 0;
            expectedVersion = 0;
            versionPatchLevel = 0;
            ActiveGProfile = null;
            currentProfilePath = null;
            IsVersionMismatch = currentProfilePath != null;
            IsProfileModified = false;
            initializationThread = null;
            initCount = 1;
            licenseCheckTimer = new GameTimer(660000);
            IsFocusTimerActive = false;
            resumeTimer = new GameTimer(30000);
            IsGliderInitialized = false;
            cameraRotator = new CameraRotator();
            if (!IsAttached)
            {
                WarnIfForceVersionSet();
                WarnIfTripwireDisabled();
            }

            if (ConfigManager.gclass61_0.method_5("AllowNetCheck") && !IsAttached)
                new NetworkSafetyChecker().ValidateNetworkSafety(true);
            if (ConfigManager.gclass61_0.method_2("LastProfile") != null)
            {
                LoadProfile(ConfigManager.gclass61_0.method_2("LastProfile"));
            }
            else
            {
                ActiveGProfile = new GProfile();
                currentProfilePath = MessageProvider.GetMessage(70);
            }

            GliderUIManager = new WebNotificationService();
            var gcontext = new GContext();
            if (!IsAttached)
                CodeCompiler.smethod_10();
            InputController.smethod_31(ConfigManager.gclass61_0);
            ApplyConfig();
            partyManager = new PartyManager();
            partyManager.method_0(ConfigManager.gclass61_0);
            ApplyLnCommandLineArg();
            SpellcastingManager.gclass42_0 = new SpellcastingManager();
            SpellcastingManager.gclass42_0.method_12();
            keyboardHookManager = new KeyboardHookManager();
            licenseCheckTimer.method_4();
            if (!IsAttached)
                ResolveWowVersion();
            else
                wowVersionLabel = "EvoStub";
            if (!IsAttached)
                SelectActiveGameClass();
            IsVersionMismatch = knownVersion != expectedVersion;
            if (securityDescriptorHelper.string_0 != null)
            {
                Logger.LogMessage(MessageProvider.smethod_2(72, securityDescriptorHelper.string_0));
                Environment.Exit(1);
            }

            GameMemoryWriter = new ScriptExecutor();
            GameClass69Instance = new ChatLogManager();
            SetupKillEventListener();
            InitializeDriverAndPid();
            StartMainThread();
        }
    }

    public static bool LoadProfile(string profilePath)
    {
        if (IsGroupProfile(profilePath))
        {
            profileGroupManager = new ProfileGroupManager();
            return profileGroupManager.method_4(profilePath);
        }

        profileGroupManager = null;
        return LoadSingleProfile(profilePath);
    }

    private static bool IsGroupProfile(string profilePath)
    {
        return profilePath.ToLower().IndexOf("groups\\") != -1;
    }

    public static bool LoadSingleProfile(string profilePath)
    {
        ActiveGProfile = new GProfile();
        if (ActiveGProfile.Load(profilePath))
        {
            corpseSortedList.Clear();
            IsProfileModified = false;
            currentProfilePath = profilePath;
            Logger.LogMessage(MessageProvider.smethod_2(109, currentProfilePath));
            ConfigManager.gclass61_0.method_0("LastProfile", profilePath);
            if (partyManager != null && partyManager.Offsets != null)
            {
                partyManager.Offsets = null;
                Logger.LogMessage(MessageProvider.GetMessage(110));
            }

            if (IsInitializationComplete)
                uiLogger.imethod_0();
            return true;
        }

        Logger.LogMessage(MessageProvider.smethod_2(111, profilePath));
        if (IsInitializationComplete)
            uiLogger.imethod_0();
        return false;
    }

    public static string GetFileNameFromPath(string path)
    {
        return path.LastIndexOf('\\') == -1 ? path : path.Substring(path.LastIndexOf('\\') + 1);
    }

    public static void ApplyConfig()
    {
        if (IsAttached)
            return;
        barCharacters = ConfigManager.gclass61_0.method_2("BarCharacters");
        if (GContext.Main != null)
            GContext.Main.ApplyConfig();
        DebuffsKnown_string = new DebuffDatabase();
        if (NeedsClassReload)
        {
            NeedsClassReload = false;
            SelectActiveGameClass();
        }

        if (CurrentGameClass != null)
            CurrentGameClass.LoadConfig();
        RestStatusMonitor.double_2 = ParseDouble(ConfigManager.gclass61_0.method_2("MeleeDistance"));
        RestStatusMonitor.double_3 = ParseDouble(ConfigManager.gclass61_0.method_2("RangedDistance"));
        autoAddDistance = ParseDouble(ConfigManager.gclass61_0.method_2("AutoAddDistance"));
        SoundPlayer.bool_0 = ConfigManager.gclass61_0.method_5("Silent");
        if (ApplicationStartupMode == AppMode.PGEdit)
            return;
        if (!ConfigManager.gclass61_0.method_5("ListenEnabled"))
        {
            if (remoteViewerServer != null)
            {
                Logger.LogMessage(MessageProvider.GetMessage(141));
                remoteViewerServer.method_1();
                remoteViewerServer = null;
            }
        }
        else
        {
            if (remoteViewerServer != null && remoteViewerServer.int_0 != ConfigManager.gclass61_0.method_3("ListenPort"))
            {
                Logger.LogMessage(MessageProvider.smethod_2(142, remoteViewerServer.int_0));
                remoteViewerServer.method_1();
                remoteViewerServer = null;
            }

            if (remoteViewerServer == null)
            {
                remoteViewerServer = new RemoteViewerServer();
                Logger.LogMessage(MessageProvider.smethod_2(143, remoteViewerServer.int_0));
                remoteViewerServer.method_0();
            }
        }

        friendWhitelist = ConfigManager.gclass61_0.method_2("FriendWhitelist").Split(' ');
        lootRouteParser = new LootRouteParser(ConfigManager.gclass61_0.method_2("LootPattern"));
        GliderUIManager.method_2();
        if (ConfigManager.gclass61_0.method_5("UseHook") && !KeyboardHookManager.bool_0)
        {
            keyboardHookManager = new KeyboardHookManager();
        }
        else
        {
            if (keyboardHookManager == null || !KeyboardHookManager.bool_0 || ConfigManager.gclass61_0.method_5("UseHook"))
                return;
            keyboardHookManager.method_17();
        }
    }

    public static double ParseDouble(string value)
    {
        return double.Parse(value.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat);
    }

    private static void ResolveWowVersion()
    {
        if (ConfigManager.gclass61_0.method_2("ForceVersion") != null)
        {
            wowVersionLabel = ConfigManager.gclass61_0.method_2("ForceVersion");
            Logger.LogMessage(MessageProvider.smethod_2(81, wowVersionLabel));
        }

        string wowInstallPath;
        if (!TryResolveWowInstallPath(out wowInstallPath))
        {
            Logger.LogMessage(MessageProvider.GetMessage(83));
            return;
        }

        wowInstallPath = wowInstallPath;
        var fileName = Path.Combine(wowInstallPath, "WoW.exe");
        Logger.smethod_1(MessageProvider.smethod_2(84, fileName));
        var versionInfo = FileVersionInfo.GetVersionInfo(fileName);
        if (versionInfo == null)
        {
            Logger.LogMessage(MessageProvider.smethod_2(85, fileName));
            return;
        }

        if (ConfigManager.gclass61_0.method_2("ForceVersion") != null)
            return;
        wowVersionLabel = versionInfo.FileVersion;
        Logger.LogMessage(MessageProvider.smethod_2(86, wowVersionLabel));
    }

    private static bool TryResolveWowInstallPath(out string installPath)
    {
        installPath = null;
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
                        installPath = value.ToString();
                        if (!string.IsNullOrEmpty(installPath))
                            break;
                    }
                }
            }
            finally
            {
                registryKey.Close();
            }

        if (string.IsNullOrEmpty(installPath))
        {
            var string_13 = ConfigManager.gclass61_0.method_2("GamePath");
            if (!string.IsNullOrEmpty(string_13))
                installPath = string_13;
        }

        if (string.IsNullOrEmpty(installPath))
            try
            {
                var processesByName = Process.GetProcessesByName("Wow");
                if (processesByName.Length > 0 && processesByName[0].MainModule != null)
                    installPath = Path.GetDirectoryName(processesByName[0].MainModule.FileName);
            }
            catch (Exception ex)
            {
                Logger.smethod_1("Unable to query running WoW path: " + ex.Message);
            }

        if (string.IsNullOrEmpty(installPath))
            return false;
        installPath = installPath.Trim().Trim('"');
        if (!installPath.EndsWith("\\"))
            installPath += "\\";
        return Directory.Exists(installPath);
    }

    public static void SelectActiveGameClass()
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
                if (!IsClassInitializing)
                    Logger.LogMessage("Switching to: " + str);
            }
        }

        ProfileMapping[str].method_0();
        var object0 = (GGameClass)ProfileMapping[str].object_0;
        if (IsGameProcessAttached)
        {
            Logger.smethod_1("Calling OnAttach for new class");
            object0.OnAttach();
        }

        CurrentStance = GStance.Unknown;
        CurrentGameClass = object0;
        CurrentProfile = ProfileMapping[str];
    }

    public static void StartMainThread()
    {
        isInitializationSuccessful = false;
        MemoryOffsetTable.Instance = new MemoryOffsetTable();
        initializationThread = new Thread(RunMainThreadSafe);
        initializationThread.Start();
    }

    private static void RunMainThreadSafe()
    {
        try
        {
            RunInitializationFlow();
        }
        catch (Exception ex)
        {
            Logger.LogMessage(MessageProvider.smethod_2(73, ex.Message + ex.StackTrace));
        }
    }

    private static void RunInitializationFlow()
    {
        ApplicationInitializer.InitializeAndValidate(ConfigManager.gclass61_0.method_2("AppKey"), true);
        IsGameProcessAttached = false;
        if (isInputStringFourCharacters && !HasShownKeyAlert)
        {
            HasShownKeyAlert = true;
            Logger.LogMessage(MessageProvider.GetMessage(75));
            Sleep(1000);
        }

        initializationThread = null;
        if (isInitializationSuccessful)
            DebugPrivilegeElevator.smethod_0();
        if (!isInputStringFourCharacters)
        {
            Logger.LogMessage(MessageProvider.GetMessage(76));
            if (isTimeAdded)
                Logger.LogMessage(MessageProvider.smethod_2(77, expiryTime.ToString()));
            if (IsLicenseValid)
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

            IsVersionMismatch = false;
            if (ConfigManager.gclass61_0.method_5("AllowWW") && GliderManager != null && GameMemoryReader == null && !IsAttached)
            {
                Logger.LogMessage("Starting Tripwire");
                GameMemoryReader = new WardenMonitor(GliderManager, ConfigManager.gclass61_0.method_5("LogWW"),
                    MemoryOffsetTable.Instance.GetIntOffset("VAPeek"));
            }

            if (IsLicenseValid && !IsClassInitializing && !IsAttached)
                NeedsClassInit = true;
            if (GameMemoryWriter != null)
                GameMemoryWriter.method_2("OnGliderStart", false);
        }
        else
        {
            IsLicenseValid = false;
            if (IsBetaVersion)
            {
                IsBetaAccessGranted = true;
                Logger.LogMessage(MessageProvider.GetMessage(78));
                Sleep(2000);
                licenseCheckTimer.method_5();
            }
        }

        if (ApplicationStartupMode == AppMode.Normal)
            Logger.LogMessage(MessageProvider.GetMessage(79));
        IsInitializationComplete = true;
        uiLogger.imethod_0();
        IsLoading = false;
        if (IsSecCheckEnabled)
            return;
        CheckVersionUpdate();
    }

    public static bool IsAttachedToGame()
    {
        return IsGameProcessAttached;
    }

    public static void TryAttach()
    {
        GProcessMemoryManipulator.bool_2 = false;
        IsGameProcessAttached = false;
        if (!ProbeProcessAttach())
            return;
        ExecuteAttachOrDetach();
    }

    public static void ExecuteAttachOrDetach()
    {
        Logger.smethod_1("--- Attach code in");
        if (IsAttached)
        {
            SoundPlayer.smethod_0("Attach.wav");
            IsDetached = true;
            uiLogger.imethod_0();
        }
        else
        {
            IsForegroundEnabled = false;
            attachCooldownTimer.method_4();
            GProcessMemoryManipulator.bool_2 = false;
            playerOffsetManager = new OffsetManager("Player", MemoryOffsetTable.Instance.GetIntOffset("D_Player"));
            itemOffsetManager = new OffsetManager("Item", MemoryOffsetTable.Instance.GetIntOffset("D_Items"));
            npcOffsetManager = new OffsetManager("NPC", MemoryOffsetTable.Instance.GetIntOffset("D_NPC"));
            objectOffsetManager = new OffsetManager("Object", MemoryOffsetTable.Instance.GetIntOffset("D_Object"));
            containerOffsetManager = new OffsetManager("Container", MemoryOffsetTable.Instance.GetIntOffset("D_Container"));
            spellbookManager = new SpellbookManager();
            GContext.Main.OnAttach();
            if (CurrentGameClass != null)
                CurrentGameClass.OnAttach();
            UIElement.smethod_2("UIParent");
            NotifyStatusChange(1, MessageProvider.GetMessage(98));
            uiLogger.imethod_0();
            enchantmentChecker = new EquipmentEnchantmentChecker();
            enchantmentChecker.method_0();
            GameClass69Instance.method_0();
            DialogMonitor.smethod_0();
            GameClass8Instance = UIElement.smethod_2("GameMenuFrame");
            IsGliderPaused = false;
            if (profileGroupManager != null)
                profileGroupManager.method_6();
            IsGameProcessAttached = true;
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

            ShouldReattachAfterStop = false;
            Logger.smethod_1("--- Attach code out");
            if (!IsStopRequested)
                return;
            IsStopRequested = false;
            StartAutoGlide(false);
        }
    }

    public static void Detach()
    {
        if (!IsGameProcessAttached)
            return;
        IsAutoLoginPending = false;
        IsDetaching = true;
        Logger.smethod_1("AppContext.Detach invoked");
        if (attachPidOverride == 0 && !GProcessMemoryManipulator.HandleAutoLogin(wowProcessId))
        {
            GProcessMemoryManipulator.CloseProcessHandle(AdditionalApplicationHandle);
            AdditionalApplicationHandle = IntPtr.Zero;
            wowProcessId = 0;
        }

        IsGameProcessAttached = false;
        GameClass69Instance.method_3();
        GameClass8Instance = null;
        NotifyStatusChange(1, MessageProvider.GetMessage(99));
    }

    public static void ApplyAclForProcess(int pid)
    {
        if (lastAclProcessId == pid)
            return;
        lastAclProcessId = pid;
        if (!securityDescriptorHelper.method_2(pid))
        {
            Logger.LogMessage(MessageProvider.smethod_2(91, securityDescriptorHelper.string_0));
            Logger.LogMessage(MessageProvider.GetMessage(92));
        }
        else
        {
            Logger.LogMessage(MessageProvider.GetMessage(93));
            if (securityDescriptorHelper.string_1 == null)
                return;
            Logger.LogMessage(MessageProvider.smethod_2(94, securityDescriptorHelper.string_1));
        }
    }

    public static void NotifyStatusChange(int flags, string message)
    {
        if (remoteViewerServer != null)
            remoteViewerServer.method_5(flags, message);
        if (GliderUIManager == null)
            return;
        if ((flags & 32) > 0)
            GliderUIManager.method_1(message);
        if ((flags & 2) <= 0)
            return;
        GliderUIManager.method_0(message);
    }

    public static int ParseProcessIdFromCommandLine()
    {
        var num = Environment.CommandLine.IndexOf("/processid=");
        if (num != -1)
            return int.Parse(Environment.CommandLine.Substring(num + 11, 8), NumberStyles.HexNumber);
        Logger.LogMessage(MessageProvider.GetMessage(140));
        return 0;
    }

    public static bool IsNumericString(string value)
    {
        try
        {
            int.Parse(value);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static void SendInputString(string input)
    {
        for (var index = 0; index < input.Length; ++index)
        {
            var char_0 = input[index];
            var flag = true;
            if (char_0 == '#' && index < input.Length - 1)
            {
                if (input[index + 1] == '#')
                {
                    flag = true;
                    ++index;
                }
                else
                {
                    var num = input.IndexOf('#', index + 1);
                    if (num > -1)
                        try
                        {
                            var short_0 = (short)int.Parse(input.Substring(index + 1, num - index - 1));
                            flag = false;
                            InputController.StartMainThread(short_0);
                            index = num;
                        }
                        catch (Exception ex)
                        {
                            Logger.smethod_1(MessageProvider.smethod_2(144, input));
                        }
                }
            }

            if (char_0 == '|')
            {
                if (index < input.Length - 1 && input[index + 1] == '|')
                {
                    flag = true;
                    ++index;
                }
                else
                {
                    flag = false;
                    InputController.StartMainThread(13);
                    Thread.Sleep(700);
                }
            }

            if (flag)
                InputController.ParseDouble(char_0);
        }
    }

    public static bool StartManualGlide(bool flag)
    {
        if (!IsInitializationComplete)
            return false;
        if (IsVersionMismatch)
        {
            Logger.LogMessage(MessageProvider.GetMessage(113));
            return false;
        }

        if (currentGlideMode != GlideMode.None)
        {
            Logger.LogMessage(MessageProvider.GetMessage(114));
            return false;
        }

        if (!IsGameProcessAttached)
        {
            TryAttach();
            if (!IsGameProcessAttached)
                return false;
        }

        if (GPlayerSelf.Me.TargetGUID == 0L)
        {
            Logger.LogMessage(MessageProvider.GetMessage(115));
            return false;
        }

        if (!ConfigManager.gclass61_0.method_5("BackgroundEnable"))
            BringGameWindowToForeground();
        currentGlideMode = GlideMode.Manual;
        glideMainThread = new GlideMainThread();
        return true;
    }

    public static void BringGameWindowToForeground()
    {
        Sleep(500);
        GProcessMemoryManipulator.SetForegroundWindow(MainApplicationHandle);
        Sleep(500);
    }

    public static bool AddWaypoint()
    {
        if (!IsGameProcessAttached)
        {
            Logger.LogMessage(MessageProvider.GetMessage(107));
            return false;
        }

        if (ActiveGProfile == null)
        {
            Logger.LogMessage(MessageProvider.GetMessage(108));
            return false;
        }

        var flag = false;
        switch (waypointMode)
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

        if (waypointMode == WaypointType.const_3)
        {
            ActiveGProfile.VendorWaypoints.Add(GPlayerSelf.Me.Location);
            Logger.LogMessage(MessageProvider.smethod_2(870, ActiveGProfile.VendorWaypoints.Count));
        }
        else if (!flag)
        {
            ActiveGProfile.Waypoints.Add(GPlayerSelf.Me.Location);
            Logger.LogMessage(MessageProvider.smethod_2(658, ActiveGProfile.Waypoints.Count));
        }
        else
        {
            ActiveGProfile.GhostWaypoints.Add(GPlayerSelf.Me.Location);
            Logger.LogMessage(MessageProvider.smethod_2(659, ActiveGProfile.GhostWaypoints.Count));
        }

        IsProfileModified = true;
        return true;
    }

    public static bool StartAutoGlide(bool userInitiated)
    {
        if (!IsInitializationComplete)
            return false;
        if (isTimeAdded && DateTime.Now > expiryTime)
        {
            Logger.LogMessage(MessageProvider.GetMessage(116));
            return false;
        }

        if (ConfigManager.gclass61_0.method_5("AllowNetCheck") && !new NetworkSafetyChecker().ValidateNetworkSafety(true))
            return false;
        if (currentGlideMode != GlideMode.None)
        {
            Logger.LogMessage(MessageProvider.GetMessage(117));
            return false;
        }

        if (!IsGameProcessAttached && !IsDetached)
        {
            TryAttach();
            if (!IsGameProcessAttached)
                return false;
        }

        if (IsVersionMismatch)
        {
            Logger.LogMessage(MessageProvider.GetMessage(118));
            return false;
        }

        if (IsDetached)
            return StartDetachedGlide();
        if (profileGroupManager == null && (ActiveGProfile == null || (ActiveGProfile.Waypoints.Count < 2 && !ActiveGProfile.Fishing)))
        {
            Logger.LogMessage(MessageProvider.GetMessage(119));
            return false;
        }

        if (GPlayerSelf.Me.IsDead &&
            (ActiveGProfile.GhostWaypoints.Count == 0 || !ConfigManager.gclass61_0.method_5("Resurrect")))
        {
            Logger.LogMessage(MessageProvider.GetMessage(120));
            return false;
        }

        if (!IsGliderInitialized)
            BringGameWindowToForeground();
        currentGlideMode = GlideMode.Auto;
        combatController = new CombatController();
        if (combatController.method_1())
            return true;
        currentGlideMode = GlideMode.None;
        return false;
    }

    private static bool StartDetachedGlide()
    {
        GameProcessManager = new MachGlideRunner();
        if (!GameProcessManager.method_0())
        {
            currentGlideMode = GlideMode.None;
            return false;
        }

        currentGlideMode = GlideMode.Auto;
        uiLogger.imethod_0();
        return true;
    }

    public static bool ToggleFactionForTarget()
    {
        if (!IsGameProcessAttached)
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

        if (ActiveGProfile == null)
        {
            Logger.LogMessage(MessageProvider.GetMessage(133));
            return false;
        }

        if (ActiveGProfile.CheckFaction(GPlayerSelf.Me.Target.FactionID, true))
        {
            Logger.LogMessage(MessageProvider.GetMessage(128));
        }
        else
        {
            Logger.LogMessage(MessageProvider.GetMessage(129));
            ActiveGProfile.SetFactionsFromString(ActiveGProfile.GetFactionsAsString() + " " + GPlayerSelf.Me.Target.FactionID);
        }

        return true;
    }

    public static void StopGlide(bool detach, string reason)
    {
        if (currentGlideMode == GlideMode.None && !detach)
            return;
        var flag = false;
        try
        {
            ++killActionNestingCount;
            ExecuteStopGlide(detach, reason);
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
            --killActionNestingCount;
        }

        if (flag)
            throw new ThreadInterruptedException();
    }

    private static void ExecuteStopGlide(bool detach, string reason)
    {
        lock (killActionLock)
        {
            var flag = false;
            if (currentGlideMode != GlideMode.None || IsGameProcessAttached && detach)
            {
                RestoreGameWindow();
                cameraRotator.method_3(true);
                Logger.smethod_1(MessageProvider.smethod_2(652, detach, (int)currentGlideMode, reason));
                cameraRotator.method_3(true);
                InputController.StartManualGlide(false);
                if (currentGlideMode == GlideMode.Auto)
                {
                    if (detach)
                        ShouldReattachAfterStop = true;
                    if (CurrentGameClass != null)
                        CurrentGameClass.OnStopGlide();
                    NotifyStatusChange(1, MessageProvider.GetMessage(100));
                    if (IsAttached)
                    {
                        var gameProcessManager = GameProcessManager;
                        if (gameProcessManager != null && Thread.CurrentThread == gameProcessManager.thread_0)
                            flag = true;
                    }
                    else
                    {
                        var combatController = combatController;
                        if (combatController != null && Thread.CurrentThread == combatController.thread_0)
                            flag = true;
                    }

                    Logger.smethod_1(MessageProvider.GetMessage(100));
                    currentGlideMode = GlideMode.None;
                    if (IsAttached)
                    {
                        var gameProcessManager = GameProcessManager;
                        GameProcessManager = null;
                        if (gameProcessManager != null)
                            gameProcessManager.method_1();
                    }
                    else
                    {
                        var combatController = combatController;
                        combatController = null;
                        if (combatController != null)
                            combatController.method_2();
                    }
                }

                if (currentGlideMode == GlideMode.Manual)
                {
                    NotifyStatusChange(1, MessageProvider.GetMessage(101));
                    if (glideMainThread != null && Thread.CurrentThread == glideMainThread.thread_0)
                        flag = true;
                    Logger.smethod_1(MessageProvider.GetMessage(102));
                    currentGlideMode = GlideMode.None;
                    if (glideMainThread != null)
                        glideMainThread.method_0();
                    glideMainThread = null;
                }

                if (detach)
                    Detach();
                uiLogger.imethod_0();
                GContext.Main.ReleaseAllKeys();
                if (GliderManager != null)
                    GliderManager.method_33(false);
                if (flag)
                    throw new ThreadInterruptedException();
            }
        }
    }

    public static int GetGlideRate()
    {
        if (currentGlideMode != GlideMode.Auto)
            return cachedGlideRate;
        if (combatController == null)
            return 0;
        lock (combatController)
        {
            if (combatController.bool_9)
            {
                cachedGlideRate = (int)Math.Round(combatController.int_8 / (DateTime.Now - sessionStartTime).TotalSeconds * 3600.0, 0);
                combatController.bool_9 = false;
            }
        }

        return cachedGlideRate;
    }

    private static void SetupKillEventListener()
    {
        if (Environment.CommandLine.ToLower().IndexOf("/kill") == -1)
            return;
        new Thread(WaitForKillEvent).Start();
    }

    public static void Shutdown()
    {
        if (GameMemoryReader != null)
            GameMemoryReader.method_0();
        GameMemoryWriter.method_0();
        CodeCompiler.smethod_4();
        GliderUIManager.method_5();
    }

    private static void WaitForKillEvent()
    {
        var eventName = ParseCommandLineArg("/kill");
        killEventHandle = CreateEvent(IntPtr.Zero, false, false, eventName);
        if (killEventHandle == IntPtr.Zero)
        {
            Logger.LogMessage("Couldn't create named event");
        }
        else
        {
            WaitForSingleObject(killEventHandle, infiniteWaitTimeout);
            CloseHandle(killEventHandle);
            if (!IsKillEventArmed)
                return;
            ExecuteShutdown();
        }
    }

    private static void ExecuteShutdown()
    {
        SoundPlayer.smethod_1("GliderExit.wav");
        DebuffsKnown_string.method_10();
        uiLogger.imethod_4();
        if (GliderManager != null && !IsDriverResident)
            GliderManager.method_11();
        Shutdown();
        Environment.Exit(0);
    }

    public static void TriggerKillEvent()
    {
        if (killEventHandle == IntPtr.Zero)
            ExecuteShutdown();
        SetEvent(killEventHandle);
    }

    public static void CancelKillEvent()
    {
        if (killEventHandle == IntPtr.Zero)
            return;
        IsKillEventArmed = false;
        SetEvent(killEventHandle);
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr CreateEvent(
        IntPtr securityAttributes,
        bool manualReset,
        bool initialState,
        string eventName);

    [DllImport("kernel32", SetLastError = true)]
    internal static extern int WaitForSingleObject(IntPtr handle, uint milliseconds);

    [DllImport("Kernel32.dll", SetLastError = true)]
    private static extern void SetEvent(IntPtr handle);

    public static string ParseCommandLineArg(string argName)
    {
        var num1 = Environment.CommandLine.IndexOf(argName + "=");
        if (num1 == -1)
        {
            Logger.LogMessage(MessageProvider.GetMessage(759));
            return null;
        }

        var startIndex = num1 + argName.Length + 1;
        var num2 = Environment.CommandLine.IndexOf(' ', startIndex);
        if (num2 == -1)
            num2 = Environment.CommandLine.Length;
        return Environment.CommandLine.Substring(startIndex, num2 - startIndex);
    }

    [DllImport("kernel32")]
    private static extern bool CloseHandle(IntPtr handle);

    public static void HandleWardenCheckResult(WardenCheckStatus result)
    {
        Logger.LogMessage("StopOnTW invoked, result = " + (int)result);
        if (result == WardenCheckStatus.const_2)
            File.WriteAllText("TWfail.txt", "guh!");
        if (result == WardenCheckStatus.const_1)
            File.WriteAllText("TWunsafe.txt", "guh!");
        if (result == WardenCheckStatus.const_3)
            File.WriteAllText("DeadSession.txt", "guh!");
        IsExitRequested = true;
        if (GameMemoryReader != null)
            GameMemoryReader.method_0();
        if (!IsDriverResident && GliderManager != null)
            GliderManager.method_11();
        uiLogger.imethod_4();
    }

    public static void TickMainLoop()
    {
        if (IsExitRequested)
            return;
        TryAutoAttach();
        if (IsResumeMode && IsInitializationComplete)
        {
            GProcessMemoryManipulator.InitializeDriverAndPid();
            if (GameMemoryWriter != null && (ApplicationStartupMode == AppMode.Normal || ApplicationStartupMode == AppMode.Invisible))
                GameMemoryWriter.method_2("OnGliderStart", false);
        }

        if (networkCheckInterval.IsReady)
        {
            networkCheckInterval.Reset();
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

        if (NeedsClassInit && !IsAttached && !IsClassInitializing)
        {
            IsClassInitializing = true;
            CodeCompiler.smethod_14();
            SelectActiveGameClass();
        }

        if (!IsGameProcessAttached)
            return;

        GObjectList.GetObjects();
        var me = GPlayerSelf.Me;
        if (me == null)
            return;

        if (DebuffsKnown_string != null && debuffUpdateTimer.method_3())
        {
            debuffUpdateTimer.method_4();
            DebuffsKnown_string.method_8();
        }

        if (me.Stance != CurrentStance)
        {
            if (CurrentStance != GStance.Unknown)
                GContext.Main.Interface.UnFillAllKeys();
            CurrentStance = me.Stance;
        }

        GameClass69Instance.method_4();
        DialogMonitor.smethod_0();
        if (GameClass8Instance != null && GameClass8Instance.method_10() && currentGlideMode == GlideMode.Auto)
            InputController.StartMainThread(27);
        if (currentGlideMode == GlideMode.Auto && IsGliderInitialized && ConfigManager.gclass61_0.method_2("BackgroundDisplay") != "Normal" &&
            (DateTime.Now - sessionStartTime).TotalSeconds >= 8.0 && !IsGliderRunning)
        {
            IsGliderRunning = true;
            HandleBackgroundDisplay();
        }

        cameraRotator.method_7();
        InputController.StartManualGlide(true);
    }

    public static void Sleep(int milliseconds)
    {
        Thread.Sleep(milliseconds);
    }

    public static void SleepWithJitter()
    {
        Thread.Sleep(12);
        Thread.Sleep(47);
        Thread.Sleep(2382);
        Thread.Sleep(2);
    }

    public static bool HandleTargetVanish(GUnit gunit_0)
    {
        if (gunit_0 != null && gunit_0.IsValid)
        {
            if (GameClass69Instance == null || GameClass69Instance.method_10() >= 10)
                return false;
            Logger.LogMessage(MessageProvider.GetMessage(830));
            if (currentGlideMode == GlideMode.Auto)
                ActiveGProfile.ForceBlacklist(gunit_0.GUID);
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

    public static string GetFileNameFromBackslash(string path)
    {
        var num = path.LastIndexOf("\\");
        return num == -1 ? path : path.Substring(num + 1);
    }

    public static string GetDirectoryFromPath(string path)
    {
        var length = path.LastIndexOf("\\");
        return length == -1 ? path : path.Substring(0, length);
    }

    public static bool ProbeProcessAttach()
    {
        wowProcessId = GProcessMemoryManipulator.AttachToWowProcess();
        if (wowProcessId == 0)
        {
            if (!HasLoggedNoProcessFound)
                Logger.smethod_1("Attach attempt: no matching process found for AttachEXE");
            HasLoggedNoProcessFound = true;
            return false;
        }

        HasLoggedNoProcessFound = false;

        IsGliderAttached = true;
        if (AdditionalApplicationHandle == IntPtr.Zero && !IsOpenMemoryModel)
        {
            AdditionalApplicationHandle = GProcessMemoryManipulator.OpenProcessHandle(wowProcessId);
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
            if (GliderManager != null && !GliderManager.method_26(wowProcessId))
            {
                Logger.LogMessage(
                    "Some other Glider is already open on that game, maybe we'll attach to some other one");
                CloseHandle(AdditionalApplicationHandle);
                AdditionalApplicationHandle = IntPtr.Zero;
                GProcessMemoryManipulator.SetProcessId(wowProcessId);
                wowProcessId = 0;
                return false;
            }

            GProcessMemoryManipulator.GetProcessId();
            if (GameMemoryWriter != null)
                GameMemoryWriter.method_2("OnGameFirstSeen", false);
        }

        if (IsAttached)
            return true;
        var int_14 = MemoryOffsetTable.Instance.GetIntOffset("UIParent");
        if (int_14 > 0 && GProcessMemoryManipulator.ReadInt32(int_14, "probeuip") == 0 && !IsAutoLoginPending &&
            ((IsLoginTimerActive && loginCooldownTimer.IsReady) || IsForegroundEnabled))
        {
            var str = ConfigManager.gclass61_0.method_2("AutoLog");
            if (str != null && str.Length > 0 && IsLicenseValid && new AutoLoginManager().method_2())
            {
                IsAutoLoginPending = true;
                GameMemoryWriter.method_2("DoAutoLog", false);
            }

            Logger.smethod_1("Attach probe note: UIParent resolved to zero, continuing with TLS/static attach checks");
        }

        if (MemoryOffsetTable.Instance.HasOffset("TLSSlot") && MemoryOffsetTable.Instance.GetIntOffset("TLSSlot") > 0)
        {
            if (GProcessMemoryManipulator.ApplyLnCommandLineArg(out playerGuid, out objectManagerBasePointer) && playerGuid != 0L)
            {
                if (GObjectList.StealthCountGameObjects(playerGuid) > 0)
                    return true;
                Logger.smethod_1("TLS attach probe failed object validation, trying static offsets fallback");
            }
            else
            {
                Logger.smethod_1("TLS attach probe failed, trying static offsets fallback");
            }
        }
        playerGuid = 0L;
        var int_18 = GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("MainTable"), "MainTable");
        var int_19 = int_18;
        if (MemoryOffsetTable.Instance.HasOffset("MainTableProbe") && MemoryOffsetTable.Instance.GetIntOffset("MainTableProbe") > 0)
        {
            var int_20 = MemoryOffsetTable.Instance.GetIntOffset("MainTableProbe");
            var int_21 = GProcessMemoryManipulator.ReadInt32(int_18 + int_20, "MainTableProbe");
            var int_22 = int_18 + int_20;
            if (ValidateMainTablePointer(int_21))
                int_19 = int_21;
            else if (ValidateMainTablePointer(int_22))
                int_19 = int_22;
            else
                int_19 = int_21;
        }
        objectManagerBasePointer = int_19;
        if (objectManagerBasePointer == 0)
        {
            Logger.smethod_1("Attach probe failed: resolved MainTable pointer is zero");
            return false;
        }

        var manualReset = false;
        var initialState = false;
        var int_23 = MemoryOffsetTable.Instance.HasOffset("MainTableActivePlayer")
            ? MemoryOffsetTable.Instance.GetIntOffset("MainTableActivePlayer")
            : 24;
        if (int_23 > 0)
        {
            var int_24 = GProcessMemoryManipulator.ReadInt32(objectManagerBasePointer + int_23, "MainTableActivePlayer");
            if (isLikelyObjectAddress(int_24))
            {
                var int64_1 = GProcessMemoryManipulator.ReadInt64(int_24 + 48, "MainTableActivePlayerGuid");
                if (int64_1 != 0L)
                {
                    playerGuid = int64_1;
                    manualReset = true;
                    initialState = true;
                    Logger.smethod_1("Attach probe: using active player object GUID = 0x" + playerGuid.ToString("x"));
                }
            }
        }

        var int_25 = MemoryOffsetTable.Instance.HasOffset("MainTableLocalGuid")
            ? MemoryOffsetTable.Instance.GetIntOffset("MainTableLocalGuid")
            : 40;
        if (!manualReset && int_25 > 0)
        {
            var int64_2 = GProcessMemoryManipulator.ReadInt64(objectManagerBasePointer + int_25, "MainTableLocalGuid");
            if (int64_2 != 0L)
            {
                playerGuid = int64_2;
                initialState = true;
                Logger.smethod_1("Attach probe: using object manager local GUID = 0x" + playerGuid.ToString("x"));
            }
        }

        if (playerGuid == 0L)
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
                    playerGuid = playerGuid;
                    if (candidateAddress != configuredPlayerIdAddress)
                        Logger.smethod_1("Attach probe: using fallback PlayerIdAddr 0x" + candidateAddress.ToString("x"));
                    break;
                }
            }

            if (playerGuid == 0L)
            {
                Logger.smethod_1("Attach probe failed: Player GUID is zero across local and static sources");
                return false;
            }
        }

        var num = GObjectList.StealthCountGameObjects(playerGuid);
        if (num > 0)
            return true;
        long long_1;
        if (GObjectList.TryGetLikelyPlayerGuid(out long_1))
        {
            if (initialState && long_1 <= 4096L)
            {
                Logger.smethod_1("Attach probe note: ignoring low inferred GUID candidate because object manager GUID is already known");
                return true;
            }
            playerGuid = long_1;
            Logger.smethod_1("Attach probe: inferred player GUID from object list = 0x" + playerGuid.ToString("x"));
            num = GObjectList.StealthCountGameObjects(playerGuid);
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

    private static bool ValidateMainTablePointer(int address)
    {
        if (address == 0)
            return false;
        var firstObjectAddress = GProcessMemoryManipulator.ReadInt32(address + MemoryOffsetTable.Instance.GetIntOffset("InitialOffset"), "MainTableFirstProbe");
        if ((firstObjectAddress & 1) != 0 || firstObjectAddress == 0 || firstObjectAddress == 28 || firstObjectAddress < 65536)
            return false;
        var objectType = GProcessMemoryManipulator.ReadInt32(firstObjectAddress + 20, "MainTableFirstTypeProbe");
        return objectType >= 1 && objectType <= 7;
    }

    public static void TryAutoAttach()
    {
        if (IsGameProcessAttached || IsDetached || !isInitializationSuccessful || !ProbeProcessAttach())
            return;
        ExecuteAttachOrDetach();
    }

    public static void HandleBackgroundDisplay()
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
        GProcessMemoryManipulator.SetForegroundWindow(MainApplicationHandle);
        IsWindowHidden = true;
    }

    public static void ShrinkGameWindow()
    {
        if (IsWindowShrunk)
            return;
        double width = ConfigManager.gclass61_0.method_3("ShrinkWidth");
        GProcessMemoryManipulator.GetWindowSize(MainApplicationHandle, out originalWindowSize);
        var height = originalWindowSize.Height / (double)originalWindowSize.Width * width;
        GProcessMemoryManipulator.SetWindowSize(MainApplicationHandle, new Size((int)width, (int)height));
        IsWindowShrunk = true;
    }

    public static void RestoreHiddenWindow()
    {
        if (!IsWindowHidden)
            return;
        GProcessMemoryManipulator.ShowWindow(MainApplicationHandle);
        IsWindowHidden = false;
    }

    public static void RestoreShrunkWindow()
    {
        if (!IsWindowShrunk)
            return;
        GProcessMemoryManipulator.SetWindowSize(MainApplicationHandle, originalWindowSize);
        IsWindowShrunk = false;
    }

    public static void RestoreGameWindow()
    {
        RestoreHiddenWindow();
        RestoreShrunkWindow();
    }

    private static void ApplyLnCommandLineArg()
    {
        if (Environment.CommandLine.ToLower().IndexOf("/ln") == -1)
            return;
        var string_4 = ParseCommandLineArg("/ln");
        ConfigManager.gclass61_0.method_0("LName", string_4);
        ConfigManager.gclass61_0.method_8();
    }

    private static void InitializeDriverAndPid()
    {
        if (Environment.CommandLine.ToLower().IndexOf("/driver") != -1)
        {
            SoundPlayer.smethod_1("Kill.wav");
            GliderManager = new WardenProtocol(ParseCommandLineArg("/driver"));
            Logger.smethod_1("Sending data to shadow driver");
            if (!GliderManager.method_38())
            {
                if (MessageBox.Show(null, MessageProvider.GetMessage(862), "Glider", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Hand) == DialogResult.Yes)
                    Help.ShowHelp(null, "Glider.chm", HelpNavigator.Topic, "ShadowFailed.html");
                IsResumeMode = false;
            }
            else
            {
                Logger.LogMessage("Shadow confirmed, looks awake");
            }

            if (Environment.CommandLine.ToLower().IndexOf("/holddriver") != -1)
            {
                Logger.smethod_1("DriverName is static, will leave driver resident");
                IsDriverResident = true;
            }
        }
        else
        {
            Logger.LogMessage(MessageProvider.GetMessage(877));
        }

        Environment.CommandLine.ToLower().IndexOf("/shadowread");
        if (Environment.CommandLine.ToLower().IndexOf("/attachpid") != -1)
        {
            attachPidOverride = int.Parse(ParseCommandLineArg("/attachpid"));
            Logger.LogMessage("/attachpid specified, looking for: " + attachPidOverride);
        }

        if (!ConfigManager.gclass61_0.method_5("UnloadShadow") || GliderManager == null)
            return;
        GliderManager.method_11();
        GliderManager = null;
    }

    private static void WarnIfForceVersionSet()
    {
        var str = ConfigManager.gclass61_0.method_2("ForceVersion");
        if (str == null || str.Length <= 0 || MessageBox.Show(null,
                "ForceVersion detected in configuration.  Running with this option increases the risk of detection and may cause Glider to crash.  Are you sure you want to continue?",
                "Glider", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) !=
            DialogResult.No)
            return;
        if (GliderManager != null && !IsDriverResident)
            GliderManager.method_11();
        Environment.Exit(0);
    }

    private static void WarnIfTripwireDisabled()
    {
        if (ConfigManager.gclass61_0.method_5("AllowWW") || MessageBox.Show(null,
                "Tripwire is disabled in configuration.  Running with this option increases the risk of detection.  Are you sure you want to continue?",
                "Glider", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) !=
            DialogResult.No)
            return;
        if (GliderManager != null && !IsDriverResident)
            GliderManager.method_11();
        Environment.Exit(0);
    }

    public static void HandleAutoLogin()
    {
        IsStopRequested = false;
        if (!IsLicenseValid)
        {
            Logger.LogMessage(MessageProvider.GetMessage(868));
            CombatController.smethod_0(869);
        }
        else
        {
            var string_1 = ConfigManager.gclass61_0.method_2("AutoLog");
            if (!new AutoLoginManager().method_1(string_1))
                return;
            pendingAutoLoginName = string_1;
            Logger.smethod_1("Autolog is good!");
            IsStopRequested = true;
        }
    }

    public static bool IsDecryptedStreamEmpty(GDataEncryptionManager encryptionManager)
    {
        return encryptionManager.ReadIntFromDecryptedStream() == 0;
    }

    public static void ShowSecurityCheck()
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

    public static void CheckAutoSecCheck()
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
            PromptSecurityCheck();
        }
    }

    private static void PromptSecurityCheck()
    {
        if (MessageBox.Show(MainForm, MessageProvider.GetMessage(875), GProcessMemoryManipulator.GenerateRandomString(), MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation) != DialogResult.Yes)
            return;
        ShowSecurityCheck();
    }

    private static void CheckVersionUpdate()
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

    public static void SetupBackgroundMode()
    {
        if (!IsGliderInitialized && ConfigManager.gclass61_0.method_5("BackgroundEnable") && GliderManager != null && IsLicenseValid)
        {
            Logger.smethod_1("Setting up bg stuff");
            MainApplicationHandle = GProcessMemoryManipulator.OpenProcessWithAccess(wowProcessId);
            GliderManager.method_34(wowProcessId, MainApplicationHandle);
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
        var num2 = GProcessMemoryManipulator.ReadInt32(num1 + 36, "mbase");
        int int_29_1;
        for (var int_29_2 =
                 GProcessMemoryManipulator.ReadInt32(GProcessMemoryManipulator.ReadInt32(num1 + 28, "mbase2") + 12 * (macroId & num2) + 8, "mbase3");
             int_29_2 > 0 && (int_29_2 & 1) == 0;
             int_29_2 = GProcessMemoryManipulator.ReadInt32(int_29_2 + GProcessMemoryManipulator.ReadInt32(int_29_1, "mnext3") + 4, "mnext4"))
        {
            var num3 = GProcessMemoryManipulator.ReadInt32(int_29_2, "mstep");
            var str = GProcessMemoryManipulator.ReadString(int_29_2 + 36, 64, "mname");
            if (num3 == macroId)
                return str;
            int_29_1 = GProcessMemoryManipulator.ReadInt32(num1 + 28, "mnext1") +
                       12 * (GProcessMemoryManipulator.ReadInt32(num1 + 36, "mnext2") & macroId);
        }

        return "(could not find macro in list!)";
    }
}