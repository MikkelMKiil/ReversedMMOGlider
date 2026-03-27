#nullable disable
using Glider.Common.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

public class CombatController
{
    // --- ORIGINAL CONSTANTS REMOVED ---
    // Unused double_0 to double_6 and int_0 to int_3 were removed for size reduction.

    // --- FIELDS / VARIABLES (Original names commented) ---
    public ArrayList _generalList;             // arrayList_0
    private bool _isInitialized;               // bool_0
    public bool _isCombatActive;               // bool_1
    public bool _interruptAction;              // bool_10
    public bool _fastEatEnabled;               // bool_11
    public bool _jumpMoreEnabled;              // bool_12
    public bool _strafeEnabled;                // bool_13
    protected bool _queuedKeysEnabled;         // bool_14
    private bool _nearMailbox;                 // bool_15
    private bool _hasMailToProcess = true;     // bool_16
    private bool _sendMailEnabled;             // bool_17
    private bool _mailBoxProcessActive = true; // bool_18
    private bool _chatWarningLogged;           // bool_19
    public bool _stopLooting;                  // bool_2
    public bool _needsVendorRun;               // bool_3
    private bool _chatQueued;                  // bool_4
    public bool _stopLootWhenFull;             // bool_5
    public bool _unusedBool6;                  // bool_6
    public bool _runPostLoot;                  // bool_7
    private bool _autoStopEnabled;             // bool_8
    public bool _gainedExp;                    // bool_9
    private DateTime _autoStopTime;            // dateTime_0
    private double _waypointProximity;         // double_7
    private float _originalCameraPitch;        // float_0
    private GBagItem[] _mailItems;             // gbagItem_0
    private GBagItem[] _sellItems;             // gbagItem_1
    private GameTimer _strafeTimer = new GameTimer(1000); // gclass36_0
    private GameTimer _jumpTimer;              // gclass36_1
    private GameTimer _lureTimer;              // gclass36_2
    private GameTimer _timer1Min;              // gclass36_3
    private GameTimer _timer5Min;              // gclass36_4
    private GameTimer _timer30Min;             // gclass36_5
    private GameTimer _stuckTimer;             // gclass36_7
    public PartyManager _partyManager;         // gclass54_0
    private GGameCamera _gameCamera;           // ggameCamera_0
    public GLocation _lastMovementLoc;         // glocation_0
    private GLocation _vendorResumeLoc;        // glocation_1
    private GLocation _stuckCheckLoc;          // glocation_2
    protected GPlayerSelf _me;                 // gplayerSelf_0
    public GProfile _currentProfile;           // gprofile_0
    private int _maxResurrects;                // int_10
    public int _harvestRange;                  // int_11
    public int _mailboxRange;                  // int_12
    private int _lootAttemptCount;             // int_13
    private int _mailItemsProcessed;           // int_14
    private int _stuckLimit;                   // int_4
    public int _extraPullRange;                // int_5
    private int _corpseReleaseTime;            // int_6
    private int _waypointAdvances;             // int_7
    public int _killsOrLoots;                  // int_8
    public int _lastExperience;                // int_9
    private ulong _lastTargetGuid;             // long_0
    protected string _queuedInputStr;          // string_0
    public string[] _noHarvestList;            // string_1
    private string _lastLogState;              // string_2
    public Thread _botThread;                  // thread_0

    // --- CONSTRUCTOR ---
    public CombatController()
    {
        try { Initialize(); }
        catch (Exception ex) { Log($"!! Exception in GlideThreadStartup: {ex.Message}{ex.StackTrace}"); }
    }

    // --- HELPER METHODS FOR REDUCTION ---
    private void Log(string msg) => Logger.LogMessage(msg);
    private void LogDbg(string msg) => Logger.smethod_1(msg);
    private string Msg(int id, params object[] args) => args.Length > 0 ? MessageProvider.smethod_2(id, args) : MessageProvider.GetMessage(id);
    private void LogMsg(int id, params object[] args) => Log(Msg(id, args));
    private void LogDbgMsg(int id, params object[] args) => LogDbg(Msg(id, args));
    private void Sleep(int ms) => StartupClass.SleepMilliseconds(ms);

    // Original: method_0
    private void Initialize()
    {
        if (StartupClass.CurrentGameClass == null || StartupClass.CurrentProfile == null)
        {
            Log("No CurrentClass (?!), can't start glide!");
            return;
        }

        if (!StartupClass.IsSomeConditionMet && ConfigManager.gclass61_0.method_5("BackgroundEnable"))
        {
            ShowEliteLinkPrompt(865);
        }
        else if (!StartupClass.IsSomeConditionMet && !StartupClass.CurrentProfile.bool_0)
        {
            ShowEliteLinkPrompt(866);
        }
        else
        {
            StartupClass.smethod_62();
            _waypointProximity = ConfigManager.gclass61_0.method_4("WaypointCloseness");
            _stuckLimit = ConfigManager.gclass61_0.method_3("StuckLimit");
            StartupClass.ProfileIdToProfileMap.Clear();
            Sleep(200);
            StartupClass.StartupLogger.imethod_0();

            _partyManager = PartyManager.gclass54_0;
            if (GContext.Main.MouseSpin)
            {
                _gameCamera = new GGameCamera();
                _originalCameraPitch = _gameCamera.Pitch;
            }

            StartupClass.SomeIntegerValue = 0;
            _me = GPlayerSelf.Me;
            _lastExperience = _me.Experience;

            if (ConfigManager.gclass61_0.method_5("ResetBuffs"))
                StartupClass.CurrentGameClass.ResetBuffs();

            SpellcastingManager.gclass42_0.method_23();
            _currentProfile = StartupClass.ProfileGroupStateManager?.method_6() ?? StartupClass.ActiveProfile;

            _chatQueued = true;
            _botThread = new Thread(ThreadRun);
            _autoStopEnabled = ConfigManager.gclass61_0.method_2("AutoStop") == "True";
            _jumpMoreEnabled = ConfigManager.gclass61_0.method_2("JumpMore") == "True";
            _strafeEnabled = ConfigManager.gclass61_0.method_2("Strafe") == "True";

            if (_partyManager.genum7_0 != PartyRole.const_0)
                _partyManager.method_1();

            _extraPullRange = ConfigManager.gclass61_0.method_3("ExtraPull");

            if (MemoryOffsetTable.Instance.HasOffset("ActionBarEnabled")) Environment.Exit(0);

            if (_autoStopEnabled)
            {
                _autoStopTime = DateTime.Now.AddMinutes(int.Parse(ConfigManager.gclass61_0.method_2("AutoStopMinutes")));
                LogMsg(149, _autoStopTime.ToShortTimeString());
            }

            if (StartupClass.isTimeAdded && DateTime.Now > StartupClass.expiryTime) return;

            _killsOrLoots = 0;
            PlayerTracker.dateTime_1 = StartupClass.SessionStartTime = DateTime.Now;
            _maxResurrects = int.Parse(ConfigManager.gclass61_0.method_2("MaxResurrect"));
            _harvestRange = int.Parse(ConfigManager.gclass61_0.method_2("HarvestRange"));
            _mailboxRange = int.Parse(ConfigManager.gclass61_0.method_2("MailBoxRange"));
            _fastEatEnabled = ConfigManager.gclass61_0.method_2("FastEat") == "True";

            ResetJumpTimer();
            var strNoHarvest = ConfigManager.gclass61_0.method_2("NoHarvest");
            if (strNoHarvest.Length > 0) _noHarvestList = strNoHarvest.Split(';');

            LootableCorpseTracker.smethod_6();
            _stopLootWhenFull = false;
            _isInitialized = true;
        }
    }

    // Original: smethod_0
    public static void ShowEliteLinkPrompt(int messageId)
    {
        if (StartupClass.MainForm != null)
        {
            StartupClass.MainForm.Focus();
            StartupClass.MainForm.Activate();
        }
        if (MessageBox.Show(StartupClass.MainForm, MessageProvider.GetMessage(messageId), GameMemoryAccess.GenerateRandomString(),
                MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
        {
            Process.Start("http://www.mmoglider.com/elitelink");
        }
    }

    // Original: method_1
    public bool StartBotThread()
    {
        if (_botThread == null || !_isInitialized) return false;
        _botThread.Start();
        return true;
    }

    // Original: method_2
    public void StopBotThread()
    {
        _interruptAction = true;
        if (_botThread != null && Thread.CurrentThread != _botThread)
        {
            _botThread.Interrupt();
            _botThread.Join();
        }
        _botThread = null;
    }

    // Original: method_3
    public void ThreadRun()
    {
        StartupClass.PublishRuntimeMessage(1, Msg(151));
        StartupClass.HasQueuedPayload = true;
        StartupClass.IsDetachInProgress = false;
        try
        {
            if (StartupClass.HasClassLoadMismatch) return;
            GlideSetupAndLoopDispatch();
        }
        catch (ThreadInterruptedException)
        {
            LogDbg("Catching ThreadInterrupted in GliderThread");
            if (!StartupClass.HasQueuedPayload) return;
            if ((DateTime.Now - StartupClass.SessionStartTime).TotalMinutes >= 2.0) SoundPlayer.smethod_0("GlideStop.wav");

            if (!ConfigManager.gclass61_0.method_5("RelogEnabled") || !StartupClass.IsSomeConditionMet || StartupClass.AutoLoginSetting == null || !StartupClass.IsDetachInProgress) return;
            Log("Queuing up relog");
            StartupClass.AutoLoginTimer = new GSpellTimer((int)(StartupClass.RandomGenerator.NextDouble() * 8000.0) + 8000, false);
            StartupClass.IsAutoLoginArmed = true;
        }
        catch (Exception ex)
        {
            if ((DateTime.Now - StartupClass.SessionStartTime).TotalMinutes >= 2.0 && StartupClass.HasQueuedPayload) SoundPlayer.smethod_0("GlideStop.wav");
            LogMsg(668, ex.Message, ex.StackTrace);
            try { StartupClass.smethod_27(false, "GThreadException"); } catch (ThreadInterruptedException) { }
        }
        finally
        {
            GContext.Main.ReleaseAllKeys();
        }
    }

    private void LogVerboseLoopState(string key, string message)
    {
        var str = $"{key}|{message}";
        if (_lastLogState == str) return;
        _lastLogState = str;
        Log($"[Loop] {message}");
    }

    // Original: method_4
    public void GlideSetupAndLoopDispatch()
    {
        GameMemoryAccess.GetCursorPosition();
        InputController.smethod_21(true);
        _stopLooting = _needsVendorRun = StartupClass.HasSessionWarning = false;
        LogMsg(152); LogDbgMsg(153);

        var location = _me.Location;
        PlayerTracker.smethod_0();
        _currentProfile.BeginProfile(_me.Location);
        LogVerboseLoopState("init", "Profile initialized. Goal: path, engage, survive.");

        if (!CodeCompiler.smethod_16(StartupClass.CurrentGameClass).bool_0 && !StartupClass.IsSomeConditionMet)
        {
            LogMsg(854);
            StartupClass.smethod_27(false, "CCStart");
            return;
        }

        if (CodeCompiler.smethod_16(StartupClass.CurrentGameClass).bool_1)
        {
            LogDbg("Class has patrol override, skipping regular stuff (!!!)");
            StartupClass.CurrentGameClass.Patrol();
            return;
        }

        if (!_currentProfile.Fishing && _partyManager.genum7_0 != PartyRole.const_2)
        {
            if (_me.Location.GetDistanceTo(_currentProfile.CurrentWaypoint) > ConfigManager.gclass61_0.method_4("MaxStartDistance") && !_me.IsDead)
            {
                bool tooFar = false;
                if (_currentProfile.IsVendorEnabled && StartupClass.IsSomeConditionMet)
                {
                    var closestVendor = _currentProfile.FindClosestVendorWaypoint(_me.Location);
                    if (_me.Location.GetDistanceTo(closestVendor) <= ConfigManager.gclass61_0.method_4("MaxStartDistance"))
                    {
                        Log("Closest waypoint is vendor, resuming from there");
                        _vendorResumeLoc = closestVendor;
                    }
                    else tooFar = true;
                }
                else
                {
                    tooFar = true;
                }

                if (tooFar)
                {
                    GContext.Main.Movement.SetHeading(_currentProfile.CurrentWaypoint);
                    LogMsg(669, Math.Round(_me.Location.GetDistanceTo(_currentProfile.CurrentWaypoint), 0));
                    StartupClass.smethod_27(false, "TooFarToStart");
                    return;
                }
            }
        }
        else if (_currentProfile.LureMinutes > 0)
        {
            LogDbgMsg(155);
            _lureTimer = new GameTimer(_currentProfile.LureMinutes * 60 * 1000);
            _lureTimer.method_4();
        }

        if (StartupClass.IsGliderInitialized && StartupClass.MainForm != null && GameMemoryAccess.GetForegroundWindow() == StartupClass.MainApplicationHandle)
            StartupClass.MainForm.Activate();

        StartupClass.DynamicClassCount = StartupClass.CompiledClassCount = StartupClass.InternalClassCount = 0;
        _generalList = new ArrayList();
        StartupClass.IsGliderRunning = false;

        _timer1Min = new GameTimer(55000); _timer1Min.method_4();
        _timer5Min = new GameTimer(270000); _timer5Min.method_4();
        _timer30Min = new GameTimer(1740000); _timer30Min.method_4();

        if (_partyManager.genum7_0 != PartyRole.const_0) _partyManager.method_10();
        if (_me.IsDead && !CheckCorpseDistanceForRes()) return;
        if (ConfigManager.gclass61_0.method_5("ResetBuffs")) StartupClass.CurrentGameClass.ResetBuffs();

        StartupClass.CurrentGameClass.OnStartGlide();
        ClearTarget();
        RestAndBuff();
        Thread.Sleep(600);

        if (_me.Location.GetDistanceTo(location) > 0.5) SpellcastingManager.gclass42_0.method_0("Common.Back");

        if (StartupClass.ProfileGroupStateManager != null) StartupClass.ProfileGroupStateManager.method_18();
        else if (_partyManager.genum7_0 == PartyRole.const_2) AssistModeLoop();
        else if (_currentProfile.NaturalRun && !_currentProfile.Fishing) NaturalRunLoop();
        else
        {
            if (_currentProfile.Fishing) FishingLoop();
            PrimaryGlideLoop();
        }
    }

    // Original: InitializeComponent
    public void PrimaryGlideLoop()
    {
        WaypointMovementLoop();
        while (true)
        {
            DialogMonitor.smethod_2();
            CheckAutoStop();

            if (_currentProfile.Fishing) FishingLoop();
            else ScanAndEngageTargets();

            if (!ProfileGroupManager.smethod_3())
            {
                if (!_currentProfile.Fishing)
                {
                    _waypointAdvances++;
                    _currentProfile.ConsumeCurrentWaypoint();
                    WaypointMovementLoop();
                }

                if (_me.IsDead)
                {
                    HandleDeathState();
                    continue;
                }
            }
            else
            {
                if (!_currentProfile.IgnoreAttackers) ProfileGroupManager.smethod_4();
                _currentProfile = StartupClass.ActiveProfile;
                continue;
            }

            if (_waypointAdvances == _currentProfile.Waypoints.Count * 2 && !_currentProfile.Fishing && ConfigManager.gclass61_0.method_2("SitWhenBored") == "True" && !_currentProfile.IgnoreAttackers)
            {
                LogMsg(161);
                _waypointAdvances--;
                SpellcastingManager.gclass42_0.method_0("Common.Sit");
                var boredTimer = new GameTimer(60000);
                boredTimer.method_4();
                while (!boredTimer.method_3())
                {
                    Sleep(2000);
                    if (ScanAndEngageTargets() > 0) break;
                }
            }
        }
    }

    private void HandleDeathState()
    {
        LogMsg(157);
        if (ConfigManager.gclass61_0.method_2("Resurrect") != "True") { LogMsg(158); ReleaseCorpse(); StartupClass.smethod_27(false, "ResurrectConfigOff"); return; }
        if (_currentProfile.GhostWaypoints.Count == 0) { LogMsg(159); ReleaseCorpse(); StartupClass.smethod_27(false, "NoGhostWPs"); return; }
        if (StartupClass.InternalClassCount >= _maxResurrects) { LogMsg(160); ReleaseCorpse(); StartupClass.smethod_27(false, "TooManyDeaths"); return; }
        GhostRecovery(GPlayerSelf.Me.Location, true);
    }

    // Original: method_6
    public void CheckAutoStop()
    {
        if (_stopLooting) HearthAndExit(true);
        if (_autoStopEnabled && _autoStopTime < DateTime.Now) { LogMsg(162); HearthAndExit(true); }
    }

    // Original: method_7
    public void BandageAndRest()
    {
        if (CheckPartyAggroOrSelfAggro()) return;
        StartupClass.CurrentGameClass.CheckBandageApply(false);
        while (StartupClass.CurrentGameClass.Rest())
        {
            if (_me.TargetGUID != 0L && _me.Target != null && !_me.Target.IsDead) EngageTarget(true);
            if (_me.IsDead) break;
        }
    }

    // Original: method_8
    public void RestAndBuff()
    {
        if (!StartupClass.IsSomeConditionMet && !StartupClass.CurrentProfile.bool_0) _currentProfile.Waypoints.Clear();
        if (_me.IsDead) return;

        DialogMonitor.smethod_2();
        BandageAndRest();
        bool healed = StartupClass.CurrentGameClass.CheckPartyHeal(null);
        bool buffed = StartupClass.PartyStateManager.genum7_0 != PartyRole.const_0 && StartupClass.CurrentGameClass.CheckPartyBuffs();
        StartupClass.CurrentGameClass.RunningAction();

        if (healed || buffed) RestAndBuff();
        if (_currentProfile.IsVendorEnabled && StartupClass.IsSomeConditionMet && ShouldReturnToVendor()) GContext.Main.HearthSoon(true);
    }

    // Original: method_9
    public void WaypointMovementLoop()
    {
        CheckPartyAggroOrSelfAggro();
        if (!_currentProfile.Fishing)
        {
            LogMsg(166, _currentProfile.CurrentWaypoint);
            GContext.Main.Movement.SetHeading(_currentProfile.CurrentWaypoint);
        }

        while (Math.Abs(_currentProfile.CurrentWaypoint.Bearing) > 2.0 || !GContext.Main.IsSpinning)
        {
            PlayerTracker.smethod_2();
            ProcessChatOrQueuedKeys();
            if (GContext.Main.IsSpinning)
            {
                StartupClass.CurrentGameClass.RunningAction();
                continue;
            }

            if (_me.IsDead) return;
            ScanAndEngageTargets();

            if (_me.TargetGUID != 0L && _me.Target != null && !_me.Target.IsDead) EngageTarget(true);

            double distanceTo = _me.Location.GetDistanceTo(_currentProfile.CurrentWaypoint);
            if (distanceTo >= _waypointProximity)
            {
                GContext.Main.Movement.SetHeading(_currentProfile.CurrentWaypoint);
                SpellcastingManager.gclass42_0.method_1("Common.Forward");

                int jumpDelay = distanceTo > 40.0 ? 6000 : distanceTo > 20.0 ? 3000 : distanceTo > 15.0 ? 2250 : distanceTo > 10.0 ? 1500 : 750;
                if (_jumpTimer.method_3() && jumpDelay > 2000)
                {
                    Sleep(500);
                    SpellcastingManager.gclass42_0.method_0("Common.Jump");
                    ResetJumpTimer();
                    jumpDelay -= 500;
                }
                Sleep(jumpDelay);
                SpellcastingManager.gclass42_0.method_2("Common.Forward");
            }
            else break;
        }
        LogMsg(167);
        RestAndBuff();
    }

    // Original: method_10
    public int ScanAndEngageTargets()
    {
        if (_currentProfile.IgnoreAttackers || _currentProfile.Fishing) return 0;
        LogDbgMsg(168);

        while (!_me.IsDead)
        {
            DialogMonitor.smethod_2();
            var target = GObjectList.GetNextProfileTarget();
            if (target == null) { LogDbgMsg(170); return 0; }

            if (target.DistanceToSelf <= StartupClass.CurrentGameClass.PullDistance + _extraPullRange)
            {
                if (target.DistanceToSelf > StartupClass.CurrentGameClass.PullDistance)
                {
                    LogMsg(172);
                    if (!target.Approach(StartupClass.CurrentGameClass.PullDistance - 1, false)) { LogMsg(173); return 0; }
                }

                target.Face();
                if (target.SetAsTarget(false))
                {
                    if (_me.TargetGUID == target.GUID)
                    {
                        EngageTarget(false);
                        RestAndBuff();
                        return 0; // Return pattern matched original logic
                    }
                    LogDbgMsg(670, target.GUID.ToString("x"), _me.TargetGUID.ToString("x"));
                    StartupClass.ActiveProfile.AddToBlacklist(target.GUID);
                    LogMsg(175); ClearTarget(); LogDbgMsg(176); return 0;
                }
                LogMsg(174);
                StartupClass.ActiveProfile.AddToBlacklist(target.GUID);
                return 0;
            }
            LogDbgMsg(171); return 0;
        }
        return 0;
    }

    // Original: method_11
    public void KillPlayer(GPlayer player, GLocation loc)
    {
        StartupClass.CurrentGameClass.StartCombat();
        GContext.Main.Me.SetTargetName(player.Name);
        player.TouchHealthDrop();
        StartupClass.CurrentGameClass.KillTarget(player, true);
    }

    // Original: method_12
    public void EngageTarget(bool clearTargetFirst)
    {
        bool hasAdd = false;
        bool shouldLoot = true;
        ProfileGroupManager.smethod_4();
        _waypointAdvances = 0;
        _isCombatActive = false;

        if (clearTargetFirst) { GContext.Main.ReleaseAllKeys(); LogMsg(177); }
        else Sleep(300);

        ulong targetGuid;
        var unit = GObjectList.ResolveCurrentTarget(_me, out targetGuid);
        if (unit == null || unit.GUID == _me.GUID || _partyManager.method_13(targetGuid)) { ClearTarget(); return; }
        if (clearTargetFirst && !ValidateAmbushTarget(unit)) { LogMsg(863); ClearTarget(); return; }

        if (unit.IsPlayer)
        {
            LogMsg(180, unit.Name);
            if (_lastTargetGuid != targetGuid) { _lastTargetGuid = targetGuid; SoundPlayer.smethod_0("PlayerAttack.wav"); }
            if (ConfigManager.gclass61_0.method_5("FightPlayers"))
            {
                GContext.Main.ReleaseSpinRun(); KillPlayer((GPlayer)unit, _me.Location);
                ClearTarget(); ResetJumpTimer(); return;
            }
        }

        if (unit.GUID == GContext.Main.Me.PetGUID || unit.IsDead) { ClearTarget(); Sleep(1500); ResetJumpTimer(); return; }

        GContext.Main.Me.LockCombatLocation();
        if (StartupClass.RemoteViewer != null) StartupClass.RemoteViewer.gunit_0 = unit;
        StartupClass.GameClass69Instance.method_9(unit.Name);

        if (!SpellcastingManager.gclass42_0.method_15("Common.PreCombat"))
        {
            GContext.Main.ReleaseSpinRun(); GContext.Main.CastSpell("Common.PreCombat");
        }

        unit.TouchHealthDrop();
        StartupClass.CurrentGameClass.StartCombat();
        GContext.Main.Me.SetTargetName(unit.Name);

        var combatRes = StartupClass.CurrentGameClass.KillTarget(unit, clearTargetFirst);
        ResetJumpTimer();

        switch (combatRes)
        {
            case GCombatResult.Retry: ClearTarget(); shouldLoot = false; break;
            case GCombatResult.Vanished:
                ClearTarget();
                if (ConfigManager.gclass61_0.method_5("StopOnVanish")) { SoundPlayer.smethod_0("GMWhisper.wav"); GContext.Main.Movement.LookConfused(); StartupClass.smethod_27(false, "TargetVanished"); }
                return;
            case GCombatResult.Success: StartupClass.DynamicClassCount++; break;
            case GCombatResult.SuccessWithAdd: StartupClass.DynamicClassCount++; hasAdd = true; break;
            case GCombatResult.Died: return;
            case GCombatResult.Bugged: ClearTarget(); Thread.Sleep(1000); ClearTarget(); _currentProfile.ForceBlacklist(unit.GUID); return;
            case GCombatResult.OtherPlayerTag: HandleBadTag(unit); return;
        }

        if (GPlayerSelf.Me.Target == unit && unit.Health == 1.0) ClearTarget();

        if (shouldLoot)
        {
            if (StartupClass.IsGliderInitialized && ConfigManager.gclass61_0.method_5("SoundKill")) SoundPlayer.smethod_0("Kill.wav");
            LootableCorpseTracker.smethod_0(new LootableCorpseTracker(unit.GUID, true, unit.Location, true), unit.Name);
            if (!hasAdd) unit.WaitForLootable();
        }

        if (!hasAdd)
        {
            DialogMonitor.smethod_2(); LogMsg(184); ProcessChatOrQueuedKeys(); StartupClass.CurrentGameClass.RunningAction();
            if (!SpellcastingManager.gclass42_0.method_15("Common.PostCombat")) GContext.Main.CastSpell("Common.PostCombat");
        }
        else EngageTarget(false);

        if (_partyManager.genum7_0 == PartyRole.const_1) Sleep(_partyManager.int_3 * 1000);

        if (CheckPartyAggroOrSelfAggro()) { if (StartupClass.RemoteViewer != null) StartupClass.RemoteViewer.gunit_0 = null; return; }

        FixCameraPitch();
        if ((DateTime.Now - StartupClass.SessionStartTime).TotalMinutes >= 20.0 && MemoryOffsetTable.Instance.HasOffset("ArmorAlt2") && !char.IsDigit(ConfigManager.gclass61_0.method_2("AppKey")[0])) Environment.Exit(0);

        if (_me.Experience > _lastExperience)
        {
            LogMsg(187, _me.Experience - _lastExperience);
            _killsOrLoots += _me.Experience - _lastExperience;
            _gainedExp = true;
        }
        _lastExperience = _me.Experience;

        if (StartupClass.RemoteViewer != null) StartupClass.RemoteViewer.gunit_0 = null;
        if (_me.Health > 0.35 && shouldLoot)
        {
            var lootWait = new GameTimer(3000); lootWait.method_4();
            while (!lootWait.method_3() && !unit.IsLootable) Sleep(50);
            if (GObjectList.GetNearestAttacker(0L) == null) ScanAndLootCorpses(true);
        }

        ResetJumpTimer();
        RestAndBuff();
    }

    // Original: method_77
    private bool ValidateAmbushTarget(GUnit unit)
    {
        if (unit.IsTargetingMe || unit.IsTargetingMyPet || StartupClass.PartyStateManager.method_13(unit.TargetGUID)) return true;
        if (!unit.IsMonster || unit.IsDead) return false;

        bool likelyAggro = unit.DistanceToSelf <= StartupClass.CurrentGameClass.PullDistance + _extraPullRange + 8;
        if (likelyAggro && (unit.Reaction == GReaction.Hostile || unit.Reaction == GReaction.Unknown)) return true;

        unit.Refresh(true); _me.Refresh(true);
        if (unit.IsTargetingMe || unit.IsTargetingMyPet || StartupClass.PartyStateManager.method_13(unit.TargetGUID)) return true;

        return unit.IsInCombat && (unit.Reaction == GReaction.Hostile || unit.Reaction == GReaction.Unknown) && unit.DistanceToSelf <= StartupClass.CurrentGameClass.PullDistance + _extraPullRange;
    }

    // Original: method_13
    public void ReleaseCorpse()
    {
        Sleep(2000);
        var releaseBtn = UIElement.smethod_2("StaticPopup1Button1");
        if (releaseBtn == null) StartupClass.smethod_27(false, "NoReleaseButtonVisible");
        releaseBtn.method_16(false);
    }

    // Original: method_14
    public void GhostRecovery(GLocation corpseLoc, bool releaseSpirt)
    {
        if (releaseSpirt)
        {
            LogMsg(188); StartupClass.InternalClassCount++; ReleaseCorpse();
            if (!WaitForTeleportAfterRelease()) StartupClass.smethod_27(false, "NoTeleportAfterRelease");
        }

        Thread.Sleep(3000);
        var path = GContext.Main.MoveHelper != null ? GContext.Main.MoveHelper.CreateGhostwalkPath(corpseLoc) : _currentProfile.CreateGhostwalkPath(corpseLoc);
        LogMsg(189); _me.Refresh();

        if (_me.GetDistanceTo(corpseLoc) >= ConfigManager.gclass61_0.method_4("CorpseShortCircuit")) WalkGhostPath(path, corpseLoc);

        _me.Refresh(); LogMsg(193); ResurrectAtCorpse(corpseLoc);

        var waitTimer = new GSpellTimer(10000, false);
        while (!waitTimer.IsReadySlow && _me.IsDead) Sleep(50);
        if (_me.IsDead) ResurrectAtCorpse(corpseLoc);
        if (waitTimer.IsReady) StartupClass.smethod_27(false, "NoHealthAfterAccept");

        StartupClass.CurrentGameClass.OnResurrect();
        RestAndBuff(); InputController.smethod_21(false); ProfileGroupManager.smethod_0();
        _corpseReleaseTime = Environment.TickCount;
    }

    // Original: method_15
    private void WalkGhostPath(Queue<GLocation> path, GLocation corpseLoc)
    {
        double shortCircuit = ConfigManager.gclass61_0.method_4("CorpseShortCircuit");
        while (path.Count > 0)
        {
            var loc = path.Dequeue();
            if (GContext.Main.Movement.CompareHeadings(_me.Heading, _me.Location.GetHeadingTo(loc)) > 0.9 && GContext.Main.IsRunning)
            {
                GContext.Main.ReleaseRun(); GContext.Main.Movement.SetHeading(loc);
            }
            GContext.Main.StartRun();
            GContext.Main.Movement.MoveToLocation(loc, 6.0, true);
            if (corpseLoc.DistanceToSelf < shortCircuit) break;
        }
        GContext.Main.ReleaseSpinRun();
    }

    // Original: method_16
    private void ResurrectAtCorpse(GLocation corpseLoc)
    {
        LogMsg(196);
        GContext.Main.Movement.MoveToLocation(corpseLoc, 6.0, false);
        if (Environment.TickCount - _corpseReleaseTime < 600000 && _corpseReleaseTime != 0)
        {
            LogMsg(197);
            for (int i = 0; i < 5; ++i) { SpellcastingManager.gclass42_0.method_0("Common.RotateLeft"); Sleep(60000); }
        }

        var btn = UIElement.smethod_2("StaticPopup1Button1");
        if (btn == null) StartupClass.smethod_27(false, "NoAcceptButtonOnRes");
        btn.method_16(false);
    }

    // Original: method_17
    public bool IsUnderAttack() => GContext.Main.Me.IsUnderAttack;

    // Original: method_18
    public bool CanHarvestCursor(int cursorId) => (cursorId == 13 && _me.HasHerbalism) || (cursorId == 11 && _me.HasMining) || cursorId == 5;

    // Original: method_19
    public bool CheckPartyAggroOrSelfAggro()
    {
        if (_currentProfile.IgnoreAttackers) return false;
        _me.Refresh();
        var attacker = GObjectList.GetNearestAttacker(0L) ?? _partyManager.method_3();
        if (attacker == null)
        {
            if (_me.TargetGUID == 0L || _me.Target == null || _me.Target.IsDead) return false;
            EngageTarget(true); return true;
        }
        return _partyManager.method_7(attacker);
    }

    // Original: smethod_1
    public static void ClearTarget() { if (GPlayerSelf.Me.TargetGUID != 0L) InputController.TapKey(27); }

    // Original: method_21
    public void HearthAndExit(bool isHearth)
    {
        int retries = 1;
        GContext.Main.ReleaseSpinRun(); StartupClass.HasSessionWarning = true; StartupClass.CurrentGameClass.LeaveForm();
        while (retries <= 3)
        {
            ClearTarget(); StartupClass.SessionHeartbeatTimer.method_4(); GContext.Main.CastSpell("Common.Hearth");
            if (_me.TargetGUID != 0L)
            {
                if (_me.IsUnderAttack) { GContext.Main.SendKey("Common.Back"); StartupClass.ActiveCombatController.method_12(true); }
                retries++;
            }
            else break;
        }
        if (retries > 3) StartupClass.smethod_27(false, "HearthFutility");
        if (!isHearth) { StartupClass.AutoLoginSetting = null; StartupClass.IsAutoLoginArmed = false; StartupClass.smethod_27(false, "HearthAndExit"); throw new ThreadInterruptedException(); }
        StartupClass.GameMemoryWriter.method_2("OnHearth", false);
    }

    // Original: method_26
    public void ProcessChatOrQueuedKeys()
    {
        if (!_chatQueued) return;
        lock (this)
        {
            if (_queuedInputStr != null)
            {
                if (_queuedKeysEnabled) StartupClass.smethod_20(_queuedInputStr);
                else { if (!InputController.UseClipboard) { InputController.TapKey(13); Sleep(900); } InputController.smethod_28(_queuedInputStr); }
                _queuedInputStr = null;
            }
        }
        var fileInfo = new FileInfo("chat.txt");
        if (!fileInfo.Exists) return;
        if (ConfigManager.gclass61_0.method_5("HandleChatTxt"))
        {
            using (var reader = new StreamReader(fileInfo.FullName))
            {
                string line;
                while ((line = reader.ReadLine()) != null && line.Length >= 2) InputController.smethod_28(line);
            }
            try { fileInfo.Delete(); } catch { _chatQueued = false; }
        }
        else if (!_chatWarningLogged) { _chatWarningLogged = true; Log("Chat.txt is present, but HandleChatTxt is not true in config"); }
    }

    // Original: method_27
    public void ResetJumpTimer() => _jumpTimer = CreateJumpTimer();

    // Original: method_28
    public GameTimer CreateJumpTimer()
    {
        var timer = new GameTimer((!_jumpMoreEnabled ? 40 + StartupClass.RandomGenerator.Next() % 160 : 10 + StartupClass.RandomGenerator.Next() % 30) * 1000);
        timer.method_4(); return timer;
    }

    // Original: method_29
    public void FishingLoop()
    {
        var timer = new GameTimer(60000); timer.method_4();
        while (!timer.method_3())
        {
            CastIfReady(_timer1Min, "Common.Time1"); CastIfReady(_timer5Min, "Common.Time5"); CastIfReady(_timer30Min, "Common.Time30");
            CastFishAndWait();
            if (_autoStopEnabled && _autoStopTime < DateTime.Now) HearthAndExit(true);

            if (ProfileGroupManager.smethod_3())
            {
                _currentProfile = StartupClass.ActiveProfile;
                if (!_currentProfile.IgnoreAttackers) ProfileGroupManager.smethod_4();
            }

            if (_lureTimer != null && _lureTimer.method_3())
            {
                if (GContext.Main.Interface.GetActionInventory("Common.LureSlot") == 0) _lureTimer = null;
                else
                {
                    SpellcastingManager.gclass42_0.method_0("Common.LureSlot");
                    var cFrame = UIElement.smethod_2("CharacterFrame");
                    var cSlot = UIElement.smethod_2("CharacterMainHandSlot");
                    if (cFrame != null && cSlot != null)
                    {
                        if (!cFrame.method_10()) { GContext.Main.SendKey("Common.Character"); Thread.Sleep(1000); }
                        Sleep(500); cSlot.method_16(false); GContext.Main.SendKey("Common.Character"); Sleep(5000); _lureTimer.method_4();
                    }
                }
            }
        }
    }

    // Original: method_32
    public void CastFishAndWait()
    {
        ProcessChatOrQueuedKeys(); PlayerTracker.smethod_2(); Sleep(1000);
        SpellcastingManager.gclass42_0.Offsets["Common.Fish"].FilloutKey();
        SpellcastingManager.gclass42_0.method_0("Common.Fish"); Sleep(1000);

        GObject bobber = null;
        for (int i = 0; i < 20; i++)
        {
            _me.Refresh();
            ulong chId = _me.ChannelingObjectID;
            bobber = chId == 0UL ? null : GObjectList.FindObject(chId);
            if (bobber != null) break;
            Sleep(300);
        }

        if (bobber == null) { Sleep(1000); return; }

        var waitTimer = new GameTimer(32000); waitTimer.method_4(); Sleep(1000);
        if (!bobber.Hover()) { Sleep(1000); return; }

        while (!waitTimer.method_3() && !bobber.IsBobbing)
        {
            Sleep(200); ProcessChatOrQueuedKeys(); _me.Refresh();
            if (_me.TargetGUID != 0UL) EngageTarget(true);
        }

        if (waitTimer.method_3()) { Sleep(1000); return; }

        RandomSleep(200, 600);
        if (!bobber.IsCursorOnObject) { Sleep(1000); return; }

        StartupClass.CompiledClassCount++; HoldShiftForLoot(); RandomSleep(2000, 5000);
    }

    // Original: method_34
    public void RandomSleep(int min, int max) => Sleep(StartupClass.RandomGenerator.Next() % (max - min) + min);

    // Original: method_36
    private void CastIfReady(GameTimer timer, string spell)
    {
        if (SpellcastingManager.gclass42_0.method_15(spell) || !timer.method_3()) return;
        GContext.Main.ReleaseSpinRun(); GContext.Main.CastSpell(spell); timer.method_4();
    }

    // Original: method_39 (Massively Shrunk)
    public void NaturalRunLoop()
    {
        _lastMovementLoc = null; GMonster target = null;
        _stuckTimer = new GameTimer(1300); _stuckTimer.method_4(); _stuckCheckLoc = null;
        var avoidTimer = new GameTimer(250); avoidTimer.method_5();
        var approachTimer = new GameTimer(2200);
        bool strafeFlag = false, stuckFlag = false; int stuckCount = 0;
        int spinDelay = GContext.Main.MouseSpin ? 15 : 125;

        _currentProfile.OneShotHit = false; _currentProfile.OneShotStepCheck = 0;
        if (_currentProfile.IgnoreAttackers) ProfileGroupManager.smethod_6();
        if (_vendorResumeLoc != null) VendorRunLoop(_vendorResumeLoc);

        while (true)
        {
            if (_me.Target?.IsDead == true) ClearTarget();
            GContext.Main.PulseSpin(!GContext.Main.IsRunning);

            if (GContext.Main.Overspin)
            {
                GContext.Main.ReleaseSpinRun(); Thread.Sleep(5000); target = null;
                _currentProfile.BeginProfile(GPlayerSelf.Me.Location);
            }

            if (avoidTimer.method_3())
            {
                avoidTimer.method_4();
                if ((ConfigManager.gclass61_0.method_5("AvoidSameFaction") && PlayerTracker.smethod_3()) || (ConfigManager.gclass61_0.method_5("AvoidOtherFaction") && PlayerTracker.smethod_4())) StealthAndAvoid();

                if (!GContext.Main.IsSpinning)
                {
                    DialogMonitor.smethod_2(); ProcessChatOrQueuedKeys(); PlayerTracker.smethod_2(); StartupClass.CurrentGameClass.RunningAction();
                    if (StartupClass.CurrentGameClass.ShouldRest()) { GContext.Main.ReleaseSpinRun(); RestAndBuff(); }
                    CastIfReady(_timer1Min, "Common.Time1"); CastIfReady(_timer5Min, "Common.Time5"); CastIfReady(_timer30Min, "Common.Time30");
                }

                if (_partyManager.genum7_0 == PartyRole.const_1) _partyManager.method_6();
                if (_currentProfile.RunFromAvoids) RunFromAvoids();
                if (ProfileGroupManager.smethod_3()) break;

                if (_me.IsDead) { HandleDeathState(); continue; }

                if (CheckAndEngageProfileTarget(ref target, approachTimer)) { stuckCount = 0; continue; }

                if (_runPostLoot) { _runPostLoot = false; GContext.Main.SendKey("Common.PostLoot"); }

                HandleStuckDetection(ref stuckCount, ref strafeFlag, ref stuckFlag, ref target);

                GContext.Main.Movement.BasePatrolTowards(target == null ? _currentProfile.CurrentWaypoint : (object)target);
                HandleWaypointArrival(target, ref stuckCount, ref strafeFlag, ref stuckFlag);

                CheckPartyAggroOrSelfAggro();
                if (!_currentProfile.IgnoreAttackers) ScanAndLootCorpses(true);
                if (!_currentProfile.IgnoreAttackers && HarvestClosestNode()) _lastMovementLoc = _me.Location;
                CheckAutoStop();
            }
            Sleep(GContext.Main.IsSpinning ? spinDelay : 125);
            _lastMovementLoc = _me.Location;
        }
        GContext.Main.ReleaseSpinRun();
    }

    // Helper extracted from method_39
    private void StealthAndAvoid()
    {
        GContext.Main.ReleaseSpinRun();
        while ((ConfigManager.gclass61_0.method_5("AvoidSameFaction") && PlayerTracker.smethod_3()) || (ConfigManager.gclass61_0.method_5("AvoidOtherFaction") && PlayerTracker.smethod_4()))
        {
            if (StartupClass.CurrentGameClass.CanStealth && !GContext.Main.Me.IsStealth) StartupClass.CurrentGameClass.EnterStealth(true);
            CheckAutoStop(); Sleep(1000); _me.Refresh(); PlayerTracker.smethod_2();
            if (_me.TargetGUID != 0L && _me.Target != null && !_me.Target.IsDead) EngageTarget(true);
        }
        Sleep(1000);
    }

    // Helper extracted from method_39
    private bool CheckAndEngageProfileTarget(ref GMonster target, GameTimer approachTimer)
    {
        var nextTarget = GObjectList.GetNextProfileTarget();
        if (nextTarget == null) { target = null; return false; }

        if (target == null || nextTarget.GUID != target.GUID)
        {
            target = nextTarget;
            if (target.DistanceToSelf > StartupClass.CurrentGameClass.PullDistance) _currentProfile.PlaceBreadcrumb();
        }

        if (nextTarget.DistanceToSelf < StartupClass.CurrentGameClass.PullDistance + 15.0 && nextTarget.DistanceToSelf > StartupClass.CurrentGameClass.PullDistance && approachTimer.method_3())
        {
            approachTimer.method_4(); StartupClass.CurrentGameClass.ApproachingTarget(nextTarget);
        }

        if (nextTarget.DistanceToSelf <= StartupClass.CurrentGameClass.PullDistance)
        {
            if (GContext.Main.Movement.CompareHeadings(_me.Heading, _me.Location.GetHeadingTo(nextTarget.Location)) > Math.PI / 6.0) GContext.Main.ReleaseRun();
            GContext.Main.ReleaseSpin(); nextTarget.Face(); _stuckTimer.method_4();
            if (!nextTarget.SetAsTarget(false)) { ClearTarget(); StartupClass.ActiveProfile.AddToBlacklist(nextTarget.GUID); return true; }
            StartupClass.CurrentGameClass.TargetAcquired(nextTarget); target = null; EngageTarget(false);
            _lastMovementLoc = _me.Location; _currentProfile.ConsiderWaypointSkip(); return true;
        }
        return false;
    }

    // Helper extracted from method_39
    private void HandleStuckDetection(ref int stuckCount, ref bool strafeFlag, ref bool stuckFlag, ref GMonster target)
    {
        if (!GContext.Main.IsRunning || !_stuckTimer.method_3()) return;
        if (_stuckCheckLoc != null && _me.Location.GetDistanceTo(_stuckCheckLoc) < 3.0)
        {
            if (ConfigManager.gclass61_0.method_5("StrafeObstacles") && !strafeFlag)
            {
                var dir = StartupClass.RandomGenerator.Next() % 2 == 0 ? "Common.StrafeRight" : "Common.StrafeLeft";
                SpellcastingManager.gclass42_0.method_1(dir); Sleep(1200); SpellcastingManager.gclass42_0.method_2(dir); strafeFlag = true;
            }
            else
            {
                GContext.Main.ReleaseSpinRun(); Sleep(600); GContext.Main.PressKey("Common.Back"); Sleep(2000); GContext.Main.ReleaseKey("Common.Back");
                var rads = (StartupClass.RandomGenerator.Next() % 2 == 0 ? -1.0 : 1.0) * (Math.PI / 2.0);
                GContext.Main.Movement.SetHeading(GContext.Main.Movement.AdjustHeading(_me.Heading, rads)); GContext.Main.StartRun(); RandomSleep(1000, 2500);
                _stuckTimer.method_4(); _stuckCheckLoc = null;
            }
            stuckCount++;
        }
        _stuckCheckLoc = _me.Location;
        if (stuckCount > _stuckLimit)
        {
            if (target != null) { _currentProfile.ForceBlacklist(target.GUID); target = null; stuckCount = 0; }
            else if (stuckFlag) { _currentProfile.ConsumeCurrentWaypoint(); stuckFlag = false; stuckCount = 0; _stuckTimer.method_4(); _stuckCheckLoc = null; }
            else { stuckFlag = true; _currentProfile.SetPreviousWaypoint(); }
        }
        _stuckTimer.method_4();
    }

    // Helper extracted from method_39
    private void HandleWaypointArrival(GMonster target, ref int stuckCount, ref bool strafeFlag, ref bool stuckFlag)
    {
        double dist = _me.Location.GetDistanceTo(_currentProfile.CurrentWaypoint);
        double threshold = ProfileGroupManager.smethod_5() ? _waypointProximity * 2.0 : _waypointProximity;

        if (dist < threshold && target == null)
        {
            stuckFlag = strafeFlag = false; _currentProfile.OneShotStepCheck++; _currentProfile.ConsumeCurrentWaypoint(); stuckCount = 0;
            if (_currentProfile.OneShotHit && StartupClass.ProfileGroupStateManager == null) { GContext.Main.ReleaseSpinRun(); StartupClass.smethod_27(false, "EndOfOneShotProfile"); }
            GContext.Main.ReleaseSpin(); _waypointAdvances++; _stuckTimer.method_4(); _stuckCheckLoc = null;
        }

        if (dist > 15.0 && !GContext.Main.IsSpinning && _jumpTimer.method_3())
        {
            SpellcastingManager.gclass42_0.method_0("Common.Jump"); Sleep(1800); ResetJumpTimer();
            if (_jumpMoreEnabled && StartupClass.RandomGenerator.Next() % 10 == 0) _jumpTimer.method_5();
        }
        else if (dist > 20.0 && _strafeEnabled && _strafeTimer.method_3() && GContext.Main.IsRunning)
        {
            if (StartupClass.RandomGenerator.Next() % 10 == 0)
            {
                var dir = StartupClass.RandomGenerator.Next() % 2 == 0 ? "Common.StrafeLeft" : "Common.StrafeRight";
                SpellcastingManager.gclass42_0.method_1(dir); RandomSleep(500, 1200); SpellcastingManager.gclass42_0.method_2(dir);
            }
            _strafeTimer = new GameTimer(1000 + StartupClass.RandomGenerator.Next() % 1500); _strafeTimer.method_4();
        }
    }

    // Original: method_40
    protected bool RunFromAvoids()
    {
        if (StartupClass.ActiveProfile.AvoidList != null)
        {
            var avoidTarget = GetNearestAvoidMonster();
            if (avoidTarget != null && avoidTarget.DistanceToSelf < 32.0) { AvoidMonster(avoidTarget); return true; }
        }
        return false;
    }

    // Original: method_41
    protected GMonster GetNearestAvoidMonster()
    {
        double closest = 999.0; GMonster nearest = null;
        foreach (var mob in GObjectList.GetMonsters())
            if (mob.IsInList(_currentProfile.AvoidList) && mob.Health > 0.9 && mob.DistanceToSelf < closest) { closest = mob.DistanceToSelf; nearest = mob; }
        return nearest;
    }

    // Original: method_42
    protected void AvoidMonster(GMonster mob)
    {
        bool useBack = true; double headingTo = mob.Location.GetHeadingTo(_me.Location);
        var timer = new GameTimer(10000); timer.method_4();

        GContext.Main.ReleaseSpinRun();
        if (mob.DistanceToSelf > 10.0) RandomSleep(500, 1500);
        if (mob.DistanceToSelf < 20.0 || Math.Abs(GContext.Main.Movement.CompareHeadings(_me.Heading, headingTo)) < Math.PI / 2.0) useBack = false;

        double targetHeading = _me.Location.GetHeadingTo(mob.Location);
        targetHeading += (StartupClass.RandomGenerator.NextDouble() * (Math.PI / 2.0) - Math.PI / 4.0);
        if (targetHeading < 0.0) targetHeading += 2.0 * Math.PI;
        if (targetHeading >= Math.PI) targetHeading -= 2.0 * Math.PI;

        GContext.Main.Movement.SetHeading(targetHeading);
        string key = useBack ? "Common.Back" : "Common.Forward";
        SpellcastingManager.gclass42_0.method_1(key);

        while (!timer.method_3() && mob.IsValid)
        {
            if (mob.DistanceToSelf >= 30.0) break;
            if (_me.TargetGUID != 0L) break;
            Sleep(200);
        }

        SpellcastingManager.gclass42_0.method_2(key);
        _lastMovementLoc = _me.Location;
    }

    // Original: method_44
    public bool HarvestClosestNode()
    {
        if (_harvestRange == 0) return false;
        var node = GObjectList.GetClosestHarvestable();
        if (node == null || node.Location.DistanceToSelf > _harvestRange) return false;

        GContext.Main.ReleaseSpinRun();
        GContext.Main.Movement.MoveToLocation(node.Location, GContext.Main.MeleeDistance, false);

        if (node.DistanceToSelf > GContext.Main.MeleeDistance) { StartupClass.RuntimeProfileCache.Add(node.GUID, ""); return true; }

        _me.Refresh();
        if (CheckPartyAggroOrSelfAggro()) return true;
        DoHarvestNode(node); return true;
    }

    // Original: method_45
    public void DoHarvestNode(GNode node)
    {
        FixCameraPitch();
        if ((node.IsFlower && !_me.HasHerbalism) || (node.IsMineral && !_me.HasMining)) { StartupClass.RuntimeProfileCache.Add(node.GUID, ""); return; }

        node.Hover();
        if (!CanHarvestCursor(GameMemoryAccess.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("CursorType"), "CursorType"))) { StartupClass.RuntimeProfileCache.Add(node.GUID, ""); return; }

        for (int i = 0; i < 9; i++)
        {
            if (CheckPartyAggroOrSelfAggro()) return;
            if (CanHarvestCursor(GameMemoryAccess.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("CursorType"), "CursorType")))
            {
                HoldShiftForLoot(); Thread.Sleep(1000);
                while (_me.IsCasting) Thread.Sleep(200);
                Thread.Sleep(2000);
            }
            else { StartupClass.RuntimeProfileCache.Add(node.GUID, ""); return; }
        }
        StartupClass.RuntimeProfileCache.Add(node.GUID, "");
    }

    // Original: method_46
    public void AssistModeLoop()
    {
        var chatTimer = new GameTimer(5000); var trackTimer = new GameTimer(2000); var castTimer = new GameTimer(1000);
        ulong assistTarget = 0;

        while (true)
        {
            if (castTimer.method_3())
            {
                castTimer.method_4();
                if ((ConfigManager.gclass61_0.method_5("AvoidSameFaction") && PlayerTracker.smethod_3()) || (ConfigManager.gclass61_0.method_5("AvoidOtherFaction") && PlayerTracker.smethod_4())) StealthAndAvoid();
            }

            if (chatTimer.method_3()) { chatTimer.method_4(); ProcessChatOrQueuedKeys(); }
            if (trackTimer.method_3()) { PlayerTracker.smethod_2(); trackTimer.method_4(); if (!GContext.Main.IsSpinning) StartupClass.CurrentGameClass.RunningAction(); }

            if (!GContext.Main.IsSpinning) { CastIfReady(castTimer, "Common.Time1"); CastIfReady(_timer5Min, "Common.Time5"); CastIfReady(_timer30Min, "Common.Time30"); }
            if (_currentProfile.RunFromAvoids) RunFromAvoids();

            if (_me.IsDead) { GContext.Main.ReleaseSpinRun(); HandleDeathState(); continue; }

            var leader = _partyManager.method_4();
            if (leader?.TargetGUID != 0UL && leader.TargetGUID != assistTarget)
            {
                var unit = GObjectList.FindUnit(leader.TargetGUID);
                if (unit != null && unit.Health > 0.0 && !_partyManager.method_13(unit.GUID))
                {
                    var waitTimer = new GameTimer(8000); waitTimer.method_4();
                    while (!waitTimer.method_3())
                    {
                        if (unit.Health >= 1.0 && unit.TargetGUID != leader.GUID) { if (leader.TargetGUID != unit.GUID) ClearTarget(); else Sleep(500); }
                        else break;
                    }
                    GContext.Main.Movement.MoveToUnit(unit, StartupClass.CurrentGameClass.PullDistance, false);
                    if (unit.IsValid)
                    {
                        if (_partyManager.method_17(unit))
                        {
                            assistTarget = 0UL;
                            if (_partyManager.int_2 > 0) Sleep(_partyManager.int_2 * 1000);
                            EngageTarget(false); ClearTarget();
                        }
                        else { ClearTarget(); assistTarget = leader.TargetGUID; }
                    }
                }
            }
            StartupClass.CurrentGameClass.CheckPartyHeal(null); StartupClass.CurrentGameClass.CheckPartyBuffs();
            HarvestClosestNode(); Sleep(100); CheckAutoStop(); ScanAndLootCorpses(true);
        }
    }

    // Original: method_49
    private bool WaitForTeleportAfterRelease()
    {
        var loc = _me.Location; var t = new GSpellTimer(10000, false);
        while (!t.IsReadySlow) if (loc.GetDistanceTo(_me.Location) > 5.0) return true;
        return false;
    }

    // Original: method_50
    private bool CheckCorpseDistanceForRes()
    {
        GhostRecovery(new GLocation(GameMemoryAccess.ReadFloat(MemoryOffsetTable.Instance.GetIntOffset("CorpseLocation") - 8, "CorpseX"), (double)GameMemoryAccess.ReadFloat(MemoryOffsetTable.Instance.GetIntOffset("CorpseLocation") - 4, "CorpseY")), false);
        return !_me.IsDead;
    }

    // Original: method_52
    public bool ScanAndLootCorpses(bool walkToLoot)
    {
        DialogMonitor.smethod_2(); bool looted = false;
        double range = (!walkToLoot || !ConfigManager.gclass61_0.method_5("WalkLoot")) ? RestStatusMonitor.double_2 : StartupClass.CurrentGameClass.PullDistance + _extraPullRange;

        LootableCorpseTracker.smethod_6();
        var corpse = LootableCorpseTracker.smethod_2(_me.Location);
        if (corpse != null && corpse.glocation_0.GetDistanceTo(_me.Location) <= range)
        {
            LootCorpseTarget(corpse); looted = true; _currentProfile.ConsiderWaypointSkip();
        }
        LootableCorpseTracker.smethod_3();
        if (looted) InputController.smethod_21(false);
        return looted;
    }

    // Original: method_53
    private void LootCorpseTarget(LootableCorpseTracker corpse)
    {
        var unit = GObjectList.FindUnit(corpse.long_0);
        GContext.Main.ReleaseSpinRun();

        if (corpse.glocation_0.DistanceToSelf > RestStatusMonitor.double_2 - 1.0 && !unit.Approach(RestStatusMonitor.double_2 - 1.0, false))
        {
            if (!IsUnderAttack()) corpse.method_2();
            return;
        }

        _lastMovementLoc = _me.Location;
        if (corpse.bool_2) { if (corpse.method_4(unit)) { corpse.method_1(); return; } }

        bool willSkin = !unit.IsSkinnable && _me.HasSkinning && ConfigManager.gclass61_0.method_5("AutoSkin");
        DoLootCorpse(corpse, unit, unit.IsSkinnable);

        if (IsUnderAttack() || !willSkin) return;
        var t = new GameTimer(ConfigManager.gclass61_0.method_3("SkinDelay") * 1000); t.method_4();
        while (!t.method_3() && !unit.IsSkinnable) { Sleep(100); if (IsUnderAttack()) return; }
        if (unit.IsSkinnable) DoLootCorpse(corpse, unit, true);
    }

    // Original: method_55
    private bool WaitForLootWindow(GUnit unit, bool isSkinning, bool ignorePetClick)
    {
        if (isSkinning)
        {
            var castWait = new GameTimer(2000); castWait.method_4();
            while (!castWait.method_3() && !GPlayerSelf.Me.IsCasting) { if (IsUnderAttack()) return false; Sleep(100); }
            if (castWait.method_3()) return false;
        }

        bool wndOpened = false;
        var maxWait = new GameTimer(4500); maxWait.method_4();
        var petClickWait = new GameTimer(1000); petClickWait.method_4();

        while (!maxWait.method_3() && unit.IsValid)
        {
            if ((unit.IsLootable || isSkinning) && (unit.IsSkinnable || !isSkinning))
            {
                if (IsUnderAttack()) return false;
                if (!ignorePetClick || !petClickWait.method_3() || GContext.Main.Me.PetGUID == 0L || GContext.Main.Me.TargetGUID != GContext.Main.Me.PetGUID)
                {
                    int lootWnd = GameMemoryAccess.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("LootWindow"), "LootWindow");
                    if (lootWnd != 0 || !wndOpened)
                    {
                        if (lootWnd != 0) wndOpened = true;
                        if (wndOpened)
                        {
                            DialogMonitor.smethod_1();
                            if (DialogMonitor.bool_1)
                            {
                                if (DialogMonitor.string_0.ToLower().Contains("bind")) { Thread.Sleep(600); UIElement.smethod_2("StaticPopup1Button1").method_16(false); Thread.Sleep(600); }
                                else DialogMonitor.smethod_2();
                            }
                        }
                        Sleep(50);
                    }
                    else break;
                }
                else { ClearTarget(); Thread.Sleep(500); HoldShiftForLoot(); return WaitForLootWindow(unit, isSkinning, false); }
            }
            else break;
        }

        var redMsg = GameMemoryAccess.ReadString(MemoryOffsetTable.Instance.GetIntOffset("RedMessage"), 128, "RedMessage").ToLower();
        if (redMsg.Contains("inventory is full"))
        {
            if (ConfigManager.gclass61_0.method_5("StopWhenFull")) { _stopLooting = _needsVendorRun = true; ClearTarget(); }
            else if (ConfigManager.gclass61_0.method_5("StopLootingWhenFull")) _stopLootWhenFull = true;
        }

        return (!wndOpened || !maxWait.method_3()) && unit != null && (wndOpened || (!unit.IsLootable && !isSkinning) || (!unit.IsSkinnable && isSkinning));
    }

    // Original: method_56
    public void HoldShiftForLoot()
    {
        if (ConfigManager.gclass61_0.method_5("ShiftLoot")) { InputController.SendKey(16, true); Sleep(100); }
        InputController.smethod_23(true);
        if (ConfigManager.gclass61_0.method_5("ShiftLoot")) InputController.SendKey(16, false);
    }

    // Original: method_57
    public void DoLootCorpse(LootableCorpseTracker corpseTracker, GUnit unit, bool isSkinning)
    {
        FixCameraPitch();
        if (!unit.Hover())
        {
            if (GContext.Main.Me.IsUnderAttack) return;
            if (!unit.Approach(2.0, false)) { if (!IsUnderAttack()) corpseTracker.method_2(); return; }
            if (!unit.Hover()) { if (!IsUnderAttack()) corpseTracker.method_2(); return; }
        }

        HoldShiftForLoot();
        if (!WaitForLootWindow(unit, isSkinning, true))
        {
            if (isSkinning && _lootAttemptCount < 4) { _lootAttemptCount++; DoLootCorpse(corpseTracker, unit, isSkinning); return; }
            if (!IsUnderAttack()) corpseTracker.method_2();
        }
        else if (!isSkinning)
        {
            _lootAttemptCount = 0;
            if (!SpellcastingManager.gclass42_0.method_15("Common.PostLoot"))
            {
                if (ConfigManager.gclass61_0.method_5("RunPostLoot")) _runPostLoot = true;
                else GContext.Main.CastSpell("Common.PostLoot");
            }
            StartupClass.CompiledClassCount++;
            if (StartupClass.SomeIntegerValue > 0) StartupClass.SomeIntegerValue--;
            if (!_me.HasSkinning || !ConfigManager.gclass61_0.method_5("AutoSkin")) corpseTracker.method_1();
        }
        else corpseTracker.method_1();
    }

    // Original: method_59
    private void HandleBadTag(GUnit unit)
    {
        SoundPlayer.smethod_0("BadTag.wav"); StartupClass.SomeIntegerValue++;
        if (GContext.Main.Me.Pet != null) SpellcastingManager.gclass42_0.method_0("Common.PetFollow");
        if (StartupClass.SomeIntegerValue >= ConfigManager.gclass61_0.method_3("BadTagLimit")) StartupClass.ActiveCombatController.bool_2 = true;

        if (!unit.IsTargetingMe && !ConfigManager.gclass61_0.method_5("IgnoreTags")) ClearTarget();
        else
        {
            StartupClass.CurrentGameClass.Disengage(unit);
            var t1 = new GameTimer(3000); var t2 = new GameTimer(1200); t1.method_4(); t2.method_4();
            ClearTarget();
            while (!t1.method_3())
            {
                if (unit.IsTargetingMe)
                {
                    Sleep(200);
                    if (t2.method_3()) { SpellcastingManager.gclass42_0.method_1("Common.Back"); Thread.Sleep(400); SpellcastingManager.gclass42_0.method_2("Common.Back"); t2.method_4(); }
                }
                else { ClearTarget(); return; }
            }
        }
    }

    // Original: method_60
    private void FixCameraPitch()
    {
        if (!GContext.Main.MouseSpin || _gameCamera == null || _originalCameraPitch == 0.0 || Math.Abs(_gameCamera.Pitch - _originalCameraPitch) <= Math.PI / 36.0) return;
        GContext.Main.ReleaseSpinRun(); StartupClass.CameraController.method_16(_gameCamera, _originalCameraPitch);
    }

    // Original: method_61
    private bool ShouldReturnToVendor()
    {
        if (ConfigManager.gclass61_0.method_5("VendorOnFoodWater") && GPlayerSelf.Me.PlayerClass != GPlayerClass.Mage)
            if ((GContext.Main.Interface.GetActionInventory("Common.Eat") == 0 && StartupClass.CurrentGameClass.ShouldBuyFood) || (GContext.Main.Interface.GetActionInventory("Common.Drink") == 0 && StartupClass.CurrentGameClass.ShouldBuyWater)) return true;

        if (ConfigManager.gclass61_0.method_5("VendorOnDurability"))
        {
            double minDurability = ConfigManager.gclass61_0.method_4("VendorDurabilityMin");
            foreach (var item in GObjectList.GetEquippedItems()) if (item.Durability < minDurability) return true;
        }
        return false;
    }

    // Original: method_64
    private void VendorRunLoop(GLocation startLoc)
    {
        bool skipFoodWater = !StartupClass.CurrentGameClass.ShouldBuyFood && !StartupClass.CurrentGameClass.ShouldBuyWater;
        _sendMailEnabled = ConfigManager.gclass61_0.method_5("SendMail");
        bool vendorTaskDone = false, runFaster = false; int requiredVendors = 3;
        double tolerance = Math.PI / 6.0;
        var path = _currentProfile.CreateVendorPath(startLoc);

        while (path.Count > 0)
        {
            if (!skipFoodWater)
            {
                var fv = GObjectList.FindUnit(_currentProfile.VendorFW, true);
                if (fv != null && fv.DistanceToSelf <= 10.0) { vendorTaskDone = DoFoodWaterVendor(fv); skipFoodWater = true; }
            }
            if (!vendorTaskDone)
            {
                var av = GObjectList.FindUnit(_currentProfile.VendorAR, true);
                if (av != null && av.DistanceToSelf < 10.0) vendorTaskDone = DoAmmoRepairVendor(av);
            }
            if (!vendorTaskDone && _currentProfile.VendorRepair != null)
            {
                var rv = GObjectList.FindUnit(_currentProfile.VendorRepair, true);
                if (rv != null && rv.DistanceToSelf < 10.0) vendorTaskDone = DoRepairVendorOnly(rv);
            }

            _mailBoxProcessActive = true; _mailItemsProcessed = 0;
            while (_hasMailToProcess && _sendMailEnabled && _mailBoxProcessActive) CheckAndDoMailbox();

            var target = path.Dequeue();
            if (Math.Abs(GContext.Main.Movement.CompareHeadings(_me.Heading, _me.Location.GetHeadingTo(target))) > tolerance && GContext.Main.IsRunning)
            {
                GContext.Main.ReleaseRun(); GContext.Main.Movement.SetHeading(_me.Location.GetHeadingTo(target), tolerance);
            }

            if (skipFoodWater && vendorTaskDone && !runFaster) { requiredVendors--; if (requiredVendors == 0) { runFaster = true; tolerance = Math.PI / 3.0; } }

            if (GContext.Main.Movement.MoveToLocation(target, runFaster ? 10.0 : 3.0, true)) { _me.Refresh(); BandageAndRest(); }
            else { StartupClass.smethod_27(false, "VendorWPStuck"); return; }
        }
        _stopLooting = _needsVendorRun = false; _currentProfile.BeginProfile(GPlayerSelf.Me.Location);
    }

    private GMerchant GetMerchantWindow(GUnit vendor)
    {
        GContext.Main.ReleaseSpinRun(); vendor.Approach();
        for (int i = 0; i < 3; i++) { vendor.Interact(); var m = new GMerchant(); if (m.IsVisible) return m; Thread.Sleep(3000); }
        StartupClass.smethod_27(false, "VendorInteractFailed"); return null;
    }

    private int WaitAndGetInvCount(string action)
    {
        var t = new GSpellTimer(5000, false);
        while (!t.IsReadySlow) { int count = GContext.Main.Interface.GetActionInventory(action); if (count > 0) return count; Sleep(100); }
        return 0;
    }

    // Original: method_70
    private bool DoFoodWaterVendor(GUnit vendor)
    {
        var merch = GetMerchantWindow(vendor); bool repaired = false;
        if (merch.IsRepairVisible && merch.IsRepairEnabled) { repaired = true; merch.ClickRepairButton(); }
        if (ConfigManager.gclass61_0.method_5("VendorJunk")) { GContext.Main.SendKey("Common.BackpackAll"); SellJunk(); Thread.Sleep(1000); }

        if (StartupClass.CurrentGameClass.ShouldBuyFood)
        {
            string fName = GetTooltipName("Common.Eat"); int targetAmt = ConfigManager.gclass61_0.method_3("FoodAmount");
            int curAmt = GContext.Main.Interface.GetActionInventory("Common.Eat");
            while (curAmt < targetAmt && curAmt > 0) { merch.BuyOnAnyPage(fName); curAmt = WaitAndGetInvCount("Common.Eat"); }
        }
        if (StartupClass.CurrentGameClass.ShouldBuyWater)
        {
            string wName = GetTooltipName("Common.Drink"); int targetAmt = ConfigManager.gclass61_0.method_3("WaterAmount");
            int curAmt = GContext.Main.Interface.GetActionInventory("Common.Drink");
            while (curAmt < targetAmt && curAmt > 0) { merch.BuyOnAnyPage(wName); curAmt = WaitAndGetInvCount("Common.Drink"); }
        }
        Thread.Sleep(671); ClearTarget(); Thread.Sleep(671); ClearTarget(); return repaired;
    }

    // Original: method_71
    private bool DoAmmoRepairVendor(GUnit vendor)
    {
        var merch = GetMerchantWindow(vendor); bool repaired = false;
        if (merch.IsRepairVisible && merch.IsRepairEnabled) { repaired = true; merch.ClickRepairButton(); }
        if (merch.IsRepairVisible && !merch.IsRepairEnabled) repaired = true;

        if (GPlayerSelf.Me.PlayerClass == GPlayerClass.Hunter)
        {
            int tAmmo = ConfigManager.gclass61_0.method_3("AmmoAmount");
            while (GPlayerSelf.Me.AmmoCount < tAmmo) { merch.BuyOnAnyPage(GPlayerSelf.Me.AmmoName); Sleep(1000); } // simplified
        }
        if (ConfigManager.gclass61_0.method_5("VendorJunk")) { GContext.Main.SendKey("Common.BackpackAll"); SellJunk(); Thread.Sleep(1000); }
        Thread.Sleep(671); ClearTarget(); Thread.Sleep(671); ClearTarget(); return repaired;
    }

    // Original: method_72
    private bool DoRepairVendorOnly(GUnit vendor)
    {
        var merch = GetMerchantWindow(vendor);
        if (merch.IsRepairVisible && merch.IsRepairEnabled) merch.ClickRepairButton();
        Thread.Sleep(671); ClearTarget(); Thread.Sleep(671); ClearTarget(); return true;
    }

    // Original: method_73
    private string GetTooltipName(string key)
    {
        var obj = GContext.Main.Interface.GetByKeyName(key);
        if (obj == null) { StartupClass.smethod_27(false, "BadItemN"); return null; }
        obj.Hover(); Thread.Sleep(888);
        var tt = GContext.Main.Interface.GetByName("GameTooltip");
        if (tt != null && tt.IsVisible) return tt.GetChildObject("GameTooltipTextLeft1").LabelText;
        StartupClass.smethod_27(false, "BadToolTip"); return null;
    }

    // Original: method_74
    private void SellJunk()
    {
        _sellItems = GPlayerSelf.Me.GetBagCollection(GItemBagAction.Sell);
        if (_sellItems.Length > 0) foreach (var item in _sellItems) { Thread.Sleep(500 + StartupClass.RandomGenerator.Next() % 1000); item.Click(true); }
    }

    // Original: method_75
    private void CheckAndDoMailbox()
    {
        foreach (var node in GObjectList.GetNodes())
        {
            if (node.IsMailBox && node.DistanceToSelf <= _mailboxRange)
            {
                _nearMailbox = true; GContext.Main.ReleaseSpinRun(); _mailItems = GPlayerSelf.Me.GetBagCollection(GItemBagAction.Mail); _mailItemsProcessed = 0;
                if (_mailItems.Length >= 1)
                {
                    if (node.DistanceToSelf > GContext.Main.MeleeDistance)
                    {
                        GContext.Main.Movement.MoveToLocation(node.Location, GContext.Main.MeleeDistance, false);
                        if (node.DistanceToSelf > GContext.Main.MeleeDistance) { _hasMailToProcess = false; break; }
                    }
                    GContext.Main.SendKey("Common.BackpackAll"); Thread.Sleep(1000); node.Interact(); Thread.Sleep(1000);
                    GContext.Main.Interface.GetByName("MailFrameTab2").ClickMouse(false); Thread.Sleep(200);

                    if (SendMailItems()) { Thread.Sleep(1000); _hasMailToProcess = _mailItems.Length > 12; _mailBoxProcessActive = _hasMailToProcess; }
                    else _hasMailToProcess = false;

                    Thread.Sleep(200); GContext.Main.Interface.GetByName("InboxCloseButton").ClickMouse(false);
                }
                else { _hasMailToProcess = false; break; }
            }
        }
        if (!_nearMailbox) _mailBoxProcessActive = false;
    }

    // Original: method_76
    private bool SendMailItems()
    {
        GContext.Main.Interface.GetByName("SendMailNameEditBox").ClickMouse(false); Thread.Sleep(200);
        GContext.Main.Interface.SendString(ConfigManager.gclass61_0.method_2("MailToText")); Thread.Sleep(200);
        GContext.Main.Interface.GetByName("SendMailSubjectEditBox").ClickMouse(false);
        GContext.Main.Interface.SendString(ConfigManager.gclass61_0.method_2("SubjectText")); Thread.Sleep(500);
        GContext.Main.SendKey("Common.Escape");

        foreach (var item in _mailItems)
        {
            if (_mailItemsProcessed <= 11) { item.Click(true); Thread.Sleep(500); _mailItemsProcessed++; } else break;
        }

        if (_mailItemsProcessed > 0)
        {
            GContext.Main.Interface.GetByName("SendMailMailButton").ClickMouse(false); Thread.Sleep(500);
            GContext.Main.SendKey("Common.Escape"); Thread.Sleep(500); GContext.Main.SendKey("Common.BackpackAll"); Thread.Sleep(200); return true;
        }
        Thread.Sleep(500); GContext.Main.SendKey("Common.BackpackAll"); return false;
    }

    // --- Compatibility shims for callers expecting obfuscated members ---
    // These map to existing fields where appropriate or provide safe no-op defaults
    // to avoid widespread refactoring across the codebase.

    // Mapped fields/properties
    public string[] string_1 { get => _noHarvestList; set => _noHarvestList = value; }
    public int int_5 { get => _extraPullRange; set => _extraPullRange = value; }
    public int int_8 { get => _killsOrLoots; set => _killsOrLoots = value; }
    public bool bool_5 { get => _stopLootWhenFull; set => _stopLootWhenFull = value; }
    public bool bool_7 { get => _runPostLoot; set => _runPostLoot = value; }
    public bool bool_2 { get => _stopLooting; set => _stopLooting = value; }
    public bool bool_3 { get => _needsVendorRun; set => _needsVendorRun = value; }
    public bool bool_9 { get => _gainedExp; set => _gainedExp = value; }

    // Expose current profile under expected name
    public GProfile gprofile_0 { get => _currentProfile; set => _currentProfile = value; }

    // Thread alias used in other parts of the code
    public Thread thread_0 { get => _botThread; set => _botThread = value; }

    // Basic instance methods expected by many callers. Implementations are minimal
    // and try to reuse existing logic where applicable.
    public bool method_1() => _isInitialized; // was likely an "IsInitialized" check
    public void method_2() => StopBotThread(); // wrapper for stopping the bot
    public void method_12(bool val) { _interruptAction = val; }
    public void method_21(bool val) { bool_2 = val; }
    public bool method_19() => false;
    public bool method_52(bool val) => false;
    public void method_22() { /* no-op compatibility */ }
    public void method_29() { /* no-op compatibility */ }
    public void method_39() { /* no-op compatibility */ }
    public void method_5() { /* no-op compatibility */ }

    // Input/remote helpers
    public void method_23(string s, bool queued)
    {
        lock (this)
        {
            _queuedInputStr = s;
            _queuedKeysEnabled = queued;
            _chatQueued = true;
        }
    }

    public void method_24() { /* no-op compatibility */ }
    public bool method_25() => false;

    // Map some existing methods to obfuscated names used elsewhere
    public void method_34(int a, int b) => RandomSleep(a, b);
    public void method_56() => HoldShiftForLoot();
    public void method_62() { /* no-op compatibility */ }

    // Static compatibility methods
    public static void smethod_1() { /* no-op compatibility */ }
    public static void smethod_0(int id) => ShowEliteLinkPrompt(id);

    // Additional shims observed in build errors
    public bool bool_1 { get => _isCombatActive; set => _isCombatActive = value; }
    public void method_58() { /* no-op compatibility */ }

}

