// Decompiled with JetBrains decompiler
// Type: CombatController
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

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
    private const double autoAddDistance = 15.0;
    private const int int_0 = 600000;
    private const int int_1 = 60000;
    private const int int_2 = 4;
    private const int int_3 = 6000;
    private const double double_1 = 25.0;
    private const double double_2 = 30.0;
    private const double double_3 = 32.0;
    private const double double_4 = 30.0;
    private const double double_5 = 20.0;
    private const double double_6 = 10.0;
    public ArrayList arrayList_0;
    private bool bool_0;
    public bool bool_1;
    public bool bool_10;
    public bool bool_11;
    public bool bool_12;
    public bool IsGameProcessAttached;
    protected bool IsOpenMemoryModel;
    private bool IsLoading;
    private bool IsProfileModified = true;
    private bool bool_17;
    private bool bool_18 = true;
    private bool IsVersionMismatch;
    public bool bool_2;
    public bool bool_3;
    private bool bool_4;
    public bool bool_5;
    public bool bool_6;
    public bool bool_7;
    private bool bool_8;
    public bool bool_9;
    private DateTime dateTime_0;
    private DateTime dateTime_1 = new DateTime(2000, 1, 1);
    private DateTime dateTime_2 = new DateTime(2000, 1, 1);
    private double double_7;
    private float float_0;
    private GBagItem[] gbagItem_0;
    private GBagItem[] gbagItem_1;
    private GameTimer licenseCheckTimer = new GameTimer(1000);
    private GameTimer resumeTimer;
    private GameTimer attachCooldownTimer;
    private GameTimer debuffUpdateTimer;
    private GameTimer staleTimer;
    private GameTimer gclass36_5;
    private GameTimer gclass36_6 = new GameTimer(3500);
    private GameTimer gclass36_7;
    public PartyManager partyManager;
    private GGameCamera ggameCamera_0;
    public GLocation glocation_0;
    private GLocation glocation_1;
    private GLocation glocation_2;
    protected GPlayerSelf gplayerSelf_0;
    public GProfile ActiveGProfile;
    private int lastAclProcessId;
    public int cachedGlideRate;
    public int attachPidOverride;
    private int killActionNestingCount;
    private int int_14;
    private int pgEditProfileCount;
    public int objectManagerBasePointer;
    private int initCount;
    private int knownVersion;
    public int expectedVersion;
    public int versionPatchLevel;
    private long playerGuid;
    protected string string_0;
    public string[] string_1;
    public Thread thread_0;

    public CombatController()
    {
        try
        {
            method_0();
        }
        catch (Exception ex)
        {
            Logger.LogMessage("!! Exception in GlideThreadStartup: " + ex.Message + ex.StackTrace);
        }
    }

    private void method_0()
    {
        switch (ConfigManager.gclass61_0.method_2("AppKey")[0])
        {
            case 'C':
            case 'c':
                if (StartupClass.GliderManager != null) StartupClass.GliderManager.method_22();
                break;
        }

        if (StartupClass.CurrentGameClass != null && StartupClass.CurrentProfile != null)
        {
            if (!StartupClass.IsSomeConditionMet && ConfigManager.gclass61_0.method_5("BackgroundEnable"))
            {
                smethod_0(865);
            }
            else if (!StartupClass.IsSomeConditionMet && !StartupClass.CurrentProfile.bool_0)
            {
                smethod_0(866);
            }
            else
            {
                StartupClass.SetupBackgroundMode();
                double_7 = ConfigManager.gclass61_0.method_4("WaypointCloseness");
                pgEditProfileCount = ConfigManager.gclass61_0.method_3("StuckLimit");
                StartupClass.ProfileIdToProfileMap.Clear();
                StartupClass.Sleep(200);
                StartupClass.ginterface0_0.imethod_0();
                partyManager = PartyManager.partyManager;
                if (GContext.Main.MouseSpin)
                {
                    ggameCamera_0 = new GGameCamera();
                    float_0 = ggameCamera_0.Pitch;
                }

                StartupClass.SomeIntegerValue = 0;
                gplayerSelf_0 = GPlayerSelf.Me;
                versionPatchLevel = gplayerSelf_0.Experience;
                if (ConfigManager.gclass61_0.method_5("ResetBuffs"))
                    StartupClass.CurrentGameClass.ResetBuffs();
                SpellcastingManager.gclass42_0.method_23();
                ActiveGProfile = StartupClass.profileGroupManager == null
                    ? StartupClass.ActiveGProfile
                    : StartupClass.profileGroupManager.method_6();
                bool_4 = true;
                thread_0 = null;
                thread_0 = new Thread(method_3);
                bool_8 = false;
                bool_12 = ConfigManager.gclass61_0.method_2("JumpMore") == "True";
                IsGameProcessAttached = ConfigManager.gclass61_0.method_2("Strafe") == "True";
                if (partyManager.genum7_0 != PartyRole.const_0)
                    partyManager.method_1();
                objectManagerBasePointer = ConfigManager.gclass61_0.method_3("ExtraPull");
                if (MemoryOffsetTable.Instance.HasOffset("ActionBarEnabled"))
                {
                    if (StartupClass.GliderManager != null)
                        StartupClass.GliderManager.method_28(0);
                    Environment.Exit(0);
                }

                if (ConfigManager.gclass61_0.method_2("AutoStop") == "True")
                {
                    bool_8 = true;
                    dateTime_0 = DateTime.Now.AddMinutes(int.Parse(ConfigManager.gclass61_0.method_2("AutoStopMinutes")));
                    Logger.LogMessage(MessageProvider.IsGroupProfile(149, dateTime_0.ToShortTimeString()));
                }

                if (StartupClass.isTimeAdded && DateTime.Now > StartupClass.expiryTime)
                    return;
                expectedVersion = 0;
                StartupClass.dateTime_0 = DateTime.Now;
                PlayerTracker.dateTime_1 = StartupClass.dateTime_0;
                lastAclProcessId = int.Parse(ConfigManager.gclass61_0.method_2("MaxResurrect"));
                cachedGlideRate = int.Parse(ConfigManager.gclass61_0.method_2("HarvestRange"));
                attachPidOverride = int.Parse(ConfigManager.gclass61_0.method_2("MailBoxRange"));
                bool_11 = ConfigManager.gclass61_0.method_2("FastEat") == "True";
                method_27();
                var str = ConfigManager.gclass61_0.method_2("NoHarvest");
                if (str.Length > 0)
                    string_1 = str.Split(';');
                LootableCorpseTracker.ParseDouble();
                bool_5 = false;
                bool_0 = true;
            }
        }
        else
        {
            Logger.LogMessage("No CurrentClass (?!), can't start glide!");
        }
    }

    public static void smethod_0(int int_15)
    {
        if (StartupClass.MainForm != null)
        {
            StartupClass.MainForm.Focus();
            StartupClass.MainForm.Activate();
        }

        if (MessageBox.Show(StartupClass.MainForm, MessageProvider.GetMessage(int_15), GProcessMemoryManipulator.GenerateRandomString(),
                MessageBoxButtons.YesNo, MessageBoxIcon.Hand) != DialogResult.Yes)
            return;
        Process.Start("http://www.mmoglider.com/elitelink");
    }

    public bool method_1()
    {
        if (thread_0 == null || !bool_0)
            return false;
        thread_0.Start();
        return true;
    }

    public void method_2()
    {
        bool_10 = true;
        if (thread_0 != null && Thread.CurrentThread != thread_0)
        {
            thread_0.Interrupt();
            thread_0.Join();
        }

        thread_0 = null;
    }

    public void method_3()
    {
        StartupClass.NotifyStatusChange(1, MessageProvider.GetMessage(151));
        StartupClass.IsPendingStop = true;
        StartupClass.IsDetaching = false;
        try
        {
            if (StartupClass.IsVersionMismatch)
                return;
            method_4();
        }
        catch (ThreadInterruptedException ex)
        {
            Logger.LoadProfile("Catching ThreadInterrupted in GliderThread");
            if (!StartupClass.IsPendingStop)
                return;
            if ((DateTime.Now - StartupClass.dateTime_0).TotalMinutes >= 2.0)
                SoundPlayer.smethod_0("GlideStop.wav");
            Logger.LoadProfile("Considering relog, enabled: " + ConfigManager.gclass61_0.method_5("RelogEnabled") +
                               ", elite: " + StartupClass.IsSomeConditionMet + ", AutoLogNickname: " +
                               (StartupClass.pendingAutoLoginName == null ? "(null)" : (object)StartupClass.pendingAutoLoginName) +
                               ", consider: " + StartupClass.IsDetaching);
            if (!ConfigManager.gclass61_0.method_5("RelogEnabled") || !StartupClass.IsSomeConditionMet ||
                StartupClass.pendingAutoLoginName == null || !StartupClass.IsDetaching)
                return;
            Logger.LogMessage("Queuing up relog");
            StartupClass.loginCooldownTimer =
                new GSpellTimer((int)(StartupClass.random_0.NextDouble() * 8000.0) + 8000, false);
            StartupClass.IsLoginTimerActive = true;
        }
        catch (Exception ex1)
        {
            if ((DateTime.Now - StartupClass.dateTime_0).TotalMinutes >= 2.0 && StartupClass.IsPendingStop)
                SoundPlayer.smethod_0("GlideStop.wav");
            Logger.LogMessage(MessageProvider.IsGroupProfile(668, ex1.Message, ex1.StackTrace));
            try
            {
                StartupClass.StopGlide(false, "GThreadException");
            }
            catch (ThreadInterruptedException ex2)
            {
            }
        }
        finally
        {
            GContext.Main.ReleaseAllKeys();
        }
    }

    public void method_4()
    {
        GProcessMemoryManipulator.GetCursorPosition();
        if (StartupClass.GliderManager != null)
            StartupClass.GliderManager.method_33(true);
        bool_2 = false;
        bool_3 = false;
        StartupClass.IsFocusTimerActive = false;
        Logger.LogMessage(MessageProvider.GetMessage(152));
        Logger.LoadProfile(MessageProvider.GetMessage(153));
        var location = gplayerSelf_0.Location;
        PlayerTracker.smethod_0();
        ActiveGProfile.BeginProfile(GPlayerSelf.Me.Location);
        if (!CodeCompiler.ApplyAclForProcess(StartupClass.CurrentGameClass).bool_0 && !StartupClass.IsSomeConditionMet)
        {
            Logger.LogMessage(MessageProvider.GetMessage(854));
            StartupClass.StopGlide(false, "CCStart");
        }
        else if (CodeCompiler.ApplyAclForProcess(StartupClass.CurrentGameClass).bool_1)
        {
            Logger.LoadProfile("Class has patrol override, skipping regular stuff (!!!)");
            StartupClass.CurrentGameClass.Patrol();
        }
        else
        {
            if (!ActiveGProfile.Fishing && partyManager.genum7_0 != PartyRole.const_2)
            {
                if (gplayerSelf_0.Location.GetDistanceTo(ActiveGProfile.CurrentWaypoint) >
                    ConfigManager.gclass61_0.method_4("MaxStartDistance") && !gplayerSelf_0.IsDead)
                {
                    var flag = false;
                    if (ActiveGProfile.IsVendorEnabled && StartupClass.IsSomeConditionMet)
                    {
                        var closestVendorWaypoint = ActiveGProfile.FindClosestVendorWaypoint(gplayerSelf_0.Location);
                        if (gplayerSelf_0.Location.GetDistanceTo(closestVendorWaypoint) <=
                            ConfigManager.gclass61_0.method_4("MaxStartDistance") && !gplayerSelf_0.IsDead)
                        {
                            flag = true;
                            Logger.LogMessage("Closest waypoint is vendor, resuming from there");
                            glocation_1 = closestVendorWaypoint;
                        }
                    }

                    if (!flag)
                    {
                        GContext.Main.Movement.SetHeading(ActiveGProfile.CurrentWaypoint);
                        Logger.LogMessage(MessageProvider.IsGroupProfile(669,
                            Math.Round(gplayerSelf_0.Location.GetDistanceTo(ActiveGProfile.CurrentWaypoint), 0)));
                        StartupClass.StopGlide(false, "TooFarToStart");
                        return;
                    }
                }
            }
            else if (ActiveGProfile.LureMinutes > 0)
            {
                Logger.LoadProfile(MessageProvider.GetMessage(155));
                attachCooldownTimer = new GameTimer(ActiveGProfile.LureMinutes * 60 * 1000);
                attachCooldownTimer.method_4();
            }
            else
            {
                Logger.LoadProfile(MessageProvider.GetMessage(156));
            }

            if (StartupClass.IsGliderInitialized && StartupClass.MainForm != null &&
                GProcessMemoryManipulator.GetForegroundWindow() == StartupClass.MainApplicationHandle)
                StartupClass.MainForm.Activate();
            StartupClass.knownVersion = 0;
            StartupClass.expectedVersion = 0;
            StartupClass.versionPatchLevel = 0;
            arrayList_0 = new ArrayList();
            StartupClass.IsGliderRunning = false;
            debuffUpdateTimer = new GameTimer(55000);
            staleTimer = new GameTimer(270000);
            gclass36_5 = new GameTimer(1740000);
            debuffUpdateTimer.method_4();
            staleTimer.method_4();
            gclass36_5.method_4();
            if (partyManager.genum7_0 != PartyRole.const_0)
                partyManager.method_10();
            if (gplayerSelf_0.IsDead && !method_50())
                return;
            if (ConfigManager.gclass61_0.method_5("ResetBuffs"))
                StartupClass.CurrentGameClass.ResetBuffs();
            StartupClass.CurrentGameClass.OnStartGlide();
            Logger.LoadProfile("First clear target");
            LoadProfile();
            Logger.LoadProfile("First rest");
            method_8();
            Thread.Sleep(600);
            Logger.LoadProfile("Second refresh, post-rest");
            if (gplayerSelf_0.Location.GetDistanceTo(location) > 0.5)
                SpellcastingManager.gclass42_0.method_0("Common.Back");
            if (StartupClass.profileGroupManager != null)
            {
                Logger.LoadProfile("Pass off to main loop");
                StartupClass.profileGroupManager.method_18();
            }
            else if (partyManager.genum7_0 == PartyRole.const_2)
            {
                method_46();
            }
            else if (ActiveGProfile.NaturalRun && !ActiveGProfile.Fishing)
            {
                method_39();
            }
            else
            {
                if (ActiveGProfile.Fishing)
                    method_29();
                method_5();
            }
        }
    }

    public void method_5()
    {
        method_9();
    label_17:
        while (true)
        {
            do
            {
                do
                {
                    DialogMonitor.IsGroupProfile();
                    method_6();
                    if (!ActiveGProfile.Fishing)
                        method_10();
                    else
                        goto label_16;
                label_2:
                    if (!ProfileGroupManager.LoadSingleProfile())
                    {
                        if (!ActiveGProfile.Fishing)
                        {
                            ++knownVersion;
                            ActiveGProfile.ConsumeCurrentWaypoint();
                            method_9();
                        }

                        if (gplayerSelf_0.IsDead)
                        {
                            Logger.LogMessage(MessageProvider.GetMessage(157));
                            if (!(ConfigManager.gclass61_0.method_2("Resurrect") != "True"))
                            {
                                if (ActiveGProfile.GhostWaypoints.Count != 0)
                                {
                                    if (StartupClass.versionPatchLevel < lastAclProcessId)
                                        method_14(GPlayerSelf.Me.Location, true);
                                    else
                                        goto label_20;
                                }
                                else
                                {
                                    goto label_19;
                                }
                            }
                            else
                            {
                                goto label_18;
                            }
                        }

                        continue;
                    }

                    goto label_3;
                label_16:
                    method_29();
                    goto label_2;
                } while (knownVersion != ActiveGProfile.Waypoints.Count * 2 || ActiveGProfile.Fishing ||
                         !(ConfigManager.gclass61_0.method_2("SitWhenBored") == "True") || ActiveGProfile.IgnoreAttackers);

                goto label_13;
            label_3:
                ActiveGProfile = StartupClass.ActiveGProfile;
            } while (ActiveGProfile.IgnoreAttackers);

            ProfileGroupManager.GetFileNameFromPath();
        }

    label_13:
        Logger.LogMessage(MessageProvider.GetMessage(161));
        --knownVersion;
        SpellcastingManager.gclass42_0.method_0("Common.Sit");
        var gclass36 = new GameTimer(60000);
        gclass36.method_4();
        while (!gclass36.method_3())
        {
            StartupClass.Sleep(2000);
            if (method_10() > 0)
                break;
        }

        goto label_17;
    label_18:
        Logger.LogMessage(MessageProvider.GetMessage(158));
        method_13();
        StartupClass.StopGlide(false, "ResurrectConfigOff");
        return;
    label_19:
        Logger.LogMessage(MessageProvider.GetMessage(159));
        method_13();
        StartupClass.StopGlide(false, "NoGhostWPs");
        return;
    label_20:
        Logger.LogMessage(MessageProvider.GetMessage(160));
        method_13();
        StartupClass.StopGlide(false, "TooManyDeaths");
    }

    public void method_6()
    {
        if (bool_2)
            method_21(true);
        if (!bool_8 || !(dateTime_0 < DateTime.Now))
            return;
        Logger.LogMessage(MessageProvider.GetMessage(162));
        method_21(true);
    }

    public void method_7()
    {
        if (method_19())
            return;
        StartupClass.CurrentGameClass.CheckBandageApply(false);
        while (StartupClass.CurrentGameClass.Rest())
        {
            if (gplayerSelf_0.TargetGUID != 0L && gplayerSelf_0.Target != null && !gplayerSelf_0.Target.IsDead)
                method_12(true);
            if (gplayerSelf_0.IsDead)
                break;
        }
    }

    public void method_8()
    {
        if (!StartupClass.IsSomeConditionMet && !StartupClass.CurrentProfile.bool_0)
            ActiveGProfile.Waypoints.Clear();
        if (gplayerSelf_0.IsDead)
        {
            Logger.LoadProfile("Skipping rest, we're dead");
        }
        else
        {
            DialogMonitor.IsGroupProfile();
            method_7();
            var flag1 = StartupClass.CurrentGameClass.CheckPartyHeal(null);
            var flag2 = StartupClass.partyManager.genum7_0 != PartyRole.const_0 &&
                        StartupClass.CurrentGameClass.CheckPartyBuffs();
            StartupClass.CurrentGameClass.RunningAction();
            if (flag1 || flag2)
                method_8();
            if (!ActiveGProfile.IsVendorEnabled || !StartupClass.IsSomeConditionMet || !method_61())
                return;
            GContext.Main.HearthSoon(true);
        }
    }

    public void method_9()
    {
        method_19();
        if (!ActiveGProfile.Fishing)
        {
            Logger.LogMessage(MessageProvider.IsGroupProfile(166, ActiveGProfile.CurrentWaypoint));
            GContext.Main.Movement.SetHeading(ActiveGProfile.CurrentWaypoint);
        }

        do
        {
            PlayerTracker.IsGroupProfile();
            method_26();
            if (!GContext.Main.IsSpinning)
                goto label_17;
        label_2:
            if (!gplayerSelf_0.IsDead)
            {
                method_10();
                if (gplayerSelf_0.TargetGUID != 0L && gplayerSelf_0.Target != null && !gplayerSelf_0.Target.IsDead)
                    method_12(true);
                double distanceTo = gplayerSelf_0.Location.GetDistanceTo(ActiveGProfile.CurrentWaypoint);
                if (distanceTo >= double_7)
                {
                    GUnit nextProfileTarget = GObjectList.GetNextProfileTarget();
                    var num = nextProfileTarget == null
                        ? 9999.0
                        : gplayerSelf_0.Location.GetDistanceTo(nextProfileTarget.Location);
                    var int_14 = 750;
                    if (num > 40.0 && distanceTo > 10.0)
                        int_14 = 1500;
                    if (num > 60.0 && distanceTo > 15.0)
                        int_14 = 2250;
                    if (num > 80.0 && distanceTo > 20.0)
                        int_14 = 3000;
                    if (num > 160.0 && distanceTo > 40.0)
                        int_14 = 6000;
                    GContext.Main.Movement.SetHeading(ActiveGProfile.CurrentWaypoint);
                    SpellcastingManager.gclass42_0.method_1("Common.Forward");
                    if (resumeTimer.method_3() && int_14 > 2000)
                    {
                        StartupClass.Sleep(500);
                        SpellcastingManager.gclass42_0.method_0("Common.Jump");
                        method_27();
                        int_14 -= 500;
                    }

                    StartupClass.Sleep(int_14);
                    SpellcastingManager.gclass42_0.method_2("Common.Forward");
                    continue;
                }

                break;
            }

            goto label_19;
        label_17:
            StartupClass.CurrentGameClass.RunningAction();
            goto label_2;
        } while (Math.Abs(ActiveGProfile.CurrentWaypoint.Bearing) <= 2.0);

        goto label_20;
    label_19:
        return;
    label_20:
        Logger.LogMessage(MessageProvider.GetMessage(167));
        method_8();
    }

    public int method_10()
    {
        if (ActiveGProfile.IgnoreAttackers || ActiveGProfile.Fishing)
            return 0;
        var num = 0;
        Logger.LoadProfile(MessageProvider.GetMessage(168));
        GMonster nextProfileTarget;
        while (true)
        {
            DialogMonitor.IsGroupProfile();
            if (!gplayerSelf_0.IsDead)
            {
                nextProfileTarget = GObjectList.GetNextProfileTarget();
                if (nextProfileTarget != null)
                {
                    double distanceToSelf = nextProfileTarget.DistanceToSelf;
                    if (distanceToSelf <= StartupClass.CurrentGameClass.PullDistance + objectManagerBasePointer)
                    {
                        if (distanceToSelf > StartupClass.CurrentGameClass.PullDistance)
                        {
                            Logger.LogMessage(MessageProvider.GetMessage(172));
                            if (!nextProfileTarget.Approach(StartupClass.CurrentGameClass.PullDistance - 1, false))
                                goto label_13;
                        }

                        nextProfileTarget.Face();
                        if (nextProfileTarget.SetAsTarget(false))
                        {
                            if (gplayerSelf_0.TargetGUID == nextProfileTarget.GUID)
                            {
                                Logger.LoadProfile("Target.Location: " + nextProfileTarget.Location +
                                                   ", Me.Location: " + gplayerSelf_0.Location);
                                method_12(false);
                                method_8();
                            }
                            else
                            {
                                goto label_15;
                            }
                        }
                        else
                        {
                            goto label_14;
                        }
                    }
                    else
                    {
                        goto label_12;
                    }
                }
                else
                {
                    goto label_11;
                }
            }
            else
            {
                break;
            }
        }

        Logger.LoadProfile(MessageProvider.GetMessage(169));
        return num;
    label_11:
        Logger.LoadProfile(MessageProvider.GetMessage(170));
        return num;
    label_12:
        Logger.LoadProfile(MessageProvider.GetMessage(171));
        return num;
    label_13:
        Logger.LogMessage(MessageProvider.GetMessage(173));
        return num;
    label_14:
        Logger.LogMessage(MessageProvider.GetMessage(174));
        StartupClass.ActiveGProfile.AddToBlacklist(nextProfileTarget.GUID);
        return num;
    label_15:
        Logger.LoadProfile(MessageProvider.IsGroupProfile(670, nextProfileTarget.GUID.ToString("x"),
            gplayerSelf_0.TargetGUID.ToString("x")));
        StartupClass.ActiveGProfile.AddToBlacklist(nextProfileTarget.GUID);
        Logger.LogMessage(MessageProvider.GetMessage(175));
        LoadProfile();
        Logger.LoadProfile(MessageProvider.GetMessage(176));
        return num;
    }

    public void method_11(GPlayer gplayer_0, GLocation glocation_3)
    {
        StartupClass.CurrentGameClass.StartCombat();
        GContext.Main.Me.SetTargetName(gplayer_0.Name);
        gplayer_0.TouchHealthDrop();
        var num = (int)StartupClass.CurrentGameClass.KillTarget(gplayer_0, true);
    }

    public void method_12(bool IsAutoLoginPending)
    {
        var flag1 = false;
        var flag2 = true;
        ProfileGroupManager.GetFileNameFromPath();
        knownVersion = 0;
        bool_1 = false;
        if (IsAutoLoginPending)
        {
            GContext.Main.ReleaseAllKeys();
            Logger.LogMessage(MessageProvider.GetMessage(177));
        }
        else
        {
            StartupClass.Sleep(300);
        }

        var unit = GObjectList.FindUnit(gplayerSelf_0.TargetGUID);
        if (unit == null)
        {
            Logger.LogMessage("Could not find target: 0x" + gplayerSelf_0.TargetGUID.ToString("x") +
                               " in object list... ?!");
            LoadProfile();
        }
        else if (unit.GUID == gplayerSelf_0.GUID)
        {
            Logger.LogMessage(MessageProvider.GetMessage(178));
            LoadProfile();
        }
        else if (IsAutoLoginPending && !unit.IsTargetingMe && !unit.IsTargetingMyPet &&
                 !StartupClass.partyManager.method_13(unit.TargetGUID))
        {
            Logger.LogMessage(MessageProvider.GetMessage(863));
            LoadProfile();
        }
        else if (partyManager.method_13(gplayerSelf_0.TargetGUID))
        {
            Logger.LogMessage(MessageProvider.GetMessage(179));
            LoadProfile();
        }
        else
        {
            if (unit.IsPlayer)
            {
                Logger.LogMessage(MessageProvider.IsGroupProfile(180, unit.Name));
                if (playerGuid != gplayerSelf_0.TargetGUID)
                {
                    playerGuid = gplayerSelf_0.TargetGUID;
                    SoundPlayer.smethod_0("PlayerAttack.wav");
                }

                if (ConfigManager.gclass61_0.method_5("FightPlayers"))
                {
                    GContext.Main.ReleaseSpinRun();
                    method_11((GPlayer)unit, gplayerSelf_0.Location);
                    LoadProfile();
                    method_27();
                    return;
                }
            }

            if (unit.GUID == GContext.Main.Me.PetGUID)
            {
                Logger.LogMessage(MessageProvider.GetMessage(182));
                LoadProfile();
                StartupClass.Sleep(1500);
                method_27();
            }
            else if (unit.IsDead)
            {
                Logger.LogMessage(MessageProvider.GetMessage(183));
                LoadProfile();
                StartupClass.Sleep(1500);
                method_27();
            }
            else
            {
                GContext.Main.Me.LockCombatLocation();
                if (StartupClass.remoteViewerServer != null)
                    StartupClass.remoteViewerServer.gunit_0 = unit;
                StartupClass.GameClass69Instance.method_9(unit.Name);
                if (!SpellcastingManager.gclass42_0.method_15("Common.PreCombat"))
                {
                    GContext.Main.ReleaseSpinRun();
                    GContext.Main.CastSpell("Common.PreCombat");
                }

                unit.TouchHealthDrop();
                StartupClass.CurrentGameClass.StartCombat();
                GContext.Main.Me.SetTargetName(unit.Name);
                var gcombatResult = StartupClass.CurrentGameClass.KillTarget(unit, IsAutoLoginPending);
                Logger.LoadProfile("Combat result: " + gcombatResult);
                method_27();
                switch (gcombatResult)
                {
                    case GCombatResult.Unknown:
                        throw new NotImplementedException(
                            "Custom class returned GCombatResult.Unknown - should never happen!");
                    case GCombatResult.Retry:
                        LoadProfile();
                        flag2 = false;
                        break;
                    case GCombatResult.RunAway:
                        throw new NotImplementedException("can't run away yet, not implemented in main code");
                    case GCombatResult.Vanished:
                        LoadProfile();
                        if (ConfigManager.gclass61_0.method_5("StopOnVanish"))
                        {
                            SoundPlayer.smethod_0("GMWhisper.wav");
                            GContext.Main.Movement.LookConfused();
                            StartupClass.StopGlide(false, "TargetVanishedInCombat");
                        }

                        method_27();
                        return;
                    case GCombatResult.Success:
                        ++StartupClass.knownVersion;
                        break;
                    case GCombatResult.SuccessWithAdd:
                        ++StartupClass.knownVersion;
                        flag1 = true;
                        break;
                    case GCombatResult.Died:
                        return;
                    case GCombatResult.Bugged:
                        LoadProfile();
                        Thread.Sleep(1000);
                        LoadProfile();
                        ActiveGProfile.ForceBlacklist(unit.GUID);
                        return;
                    case GCombatResult.OtherPlayerTag:
                        method_59(unit);
                        return;
                }

                if (GPlayerSelf.Me.Target == unit && unit.Health == 1.0)
                {
                    Logger.LogMessage("Still targeting full-health mob after combat, clearing it out");
                    LoadProfile();
                }

                if (flag2)
                {
                    if (StartupClass.IsGliderInitialized && ConfigManager.gclass61_0.method_5("SoundKill"))
                        SoundPlayer.smethod_0("Kill.wav");
                    LootableCorpseTracker.smethod_0(new LootableCorpseTracker(unit.GUID, true, unit.Location, true), unit.Name);
                    if (!flag1)
                        unit.WaitForLootable();
                }

                if (!flag1)
                {
                    DialogMonitor.IsGroupProfile();
                    Logger.LogMessage(MessageProvider.GetMessage(184));
                    method_26();
                    StartupClass.CurrentGameClass.RunningAction();
                    if (!SpellcastingManager.gclass42_0.method_15("Common.PostCombat"))
                        GContext.Main.CastSpell("Common.PostCombat");
                }
                else
                {
                    Logger.LogMessage("Combat done with add, dealing with extra monster");
                    method_12(false);
                }

                var flag3 = true;
                if (partyManager.genum7_0 != PartyRole.const_0)
                {
                    flag3 = false;
                    if (partyManager.int_0 != 0)
                    {
                        var num = (int)Math.Abs(unit.GUID % partyManager.int_0);
                        Logger.LoadProfile(MessageProvider.IsGroupProfile(671, num, partyManager.int_1));
                        if (num == partyManager.int_1)
                        {
                            Logger.LogMessage(MessageProvider.GetMessage(185));
                            flag3 = true;
                        }
                    }
                }

                if (!flag3 && partyManager.genum7_0 == PartyRole.const_1)
                {
                    Logger.LoadProfile(MessageProvider.GetMessage(186));
                    StartupClass.Sleep(partyManager.int_3 * 1000);
                }

                if (method_19())
                {
                    if (StartupClass.remoteViewerServer == null)
                        return;
                    StartupClass.remoteViewerServer.gunit_0 = null;
                }
                else
                {
                    method_60();
                    if ((DateTime.Now - StartupClass.dateTime_0).TotalMinutes >= 20.0 &&
                        MemoryOffsetTable.Instance.HasOffset("ArmorAlt2") &&
                        !char.IsDigit(ConfigManager.gclass61_0.method_2("AppKey")[0]))
                    {
                        if (StartupClass.GliderManager != null)
                            StartupClass.GliderManager.method_28(0);
                        Environment.Exit(0);
                    }

                    if (gplayerSelf_0.Experience != versionPatchLevel)
                        lock (this)
                        {
                            if (gplayerSelf_0.Experience > versionPatchLevel)
                            {
                                Logger.LogMessage(MessageProvider.IsGroupProfile(187, gplayerSelf_0.Experience - versionPatchLevel));
                                expectedVersion += gplayerSelf_0.Experience - versionPatchLevel;
                                bool_9 = true;
                            }

                            versionPatchLevel = gplayerSelf_0.Experience;
                        }

                    if (StartupClass.remoteViewerServer != null)
                        StartupClass.remoteViewerServer.gunit_0 = null;
                    if (gplayerSelf_0.Health > 0.35 && flag2)
                    {
                        var gclass36 = new GameTimer(3000);
                        gclass36.method_4();
                        while (!gclass36.method_3() && !unit.IsLootable)
                            StartupClass.Sleep(50);
                        if (GObjectList.GetNearestAttacker(0L) == null)
                            method_52(true);
                    }

                    method_27();
                    method_8();
                }
            }
        }
    }

    public void method_13()
    {
        StartupClass.Sleep(2000);
        var gclass8 = UIElement.IsGroupProfile("StaticPopup1Button1");
        if (gclass8 == null)
            StartupClass.StopGlide(false, "NoReleaseButtonVisible");
        gclass8.method_16(false);
    }

    public void method_14(GLocation glocation_3, bool IsAutoLoginPending)
    {
        if (IsAutoLoginPending)
        {
            Logger.LogMessage(MessageProvider.GetMessage(188));
            ++StartupClass.versionPatchLevel;
            method_13();
            if (!method_49())
                StartupClass.StopGlide(false, "NoTeleportAfterRelease");
        }

        Thread.Sleep(3000);
        var queue_0 = GContext.Main.MoveHelper != null
            ? GContext.Main.MoveHelper.CreateGhostwalkPath(glocation_3)
            : ActiveGProfile.CreateGhostwalkPath(glocation_3);
        Logger.LogMessage(MessageProvider.GetMessage(189));
        gplayerSelf_0.Refresh();
        var num = ConfigManager.gclass61_0.method_4("CorpseShortCircuit");
        if (gplayerSelf_0.GetDistanceTo(glocation_3) < num)
            Logger.LogMessage(MessageProvider.GetMessage(879));
        else
            method_15(queue_0, glocation_3);
        gplayerSelf_0.Refresh();
        Logger.LogMessage(MessageProvider.GetMessage(193));
        method_16(glocation_3);
        var gspellTimer = new GSpellTimer(10000, false);
        do
        {
            ;
        } while (!gspellTimer.IsReadySlow && gplayerSelf_0.IsDead);

        if (gplayerSelf_0.IsDead)
        {
            Logger.LogMessage("Still dead - try again");
            method_16(glocation_3);
        }

        if (gspellTimer.IsReady)
            StartupClass.StopGlide(false, "NoHealthAfterAccept");
        StartupClass.CurrentGameClass.OnResurrect();
        method_8();
        InputController.StartManualGlide(false);
        ProfileGroupManager.smethod_0();
        initCount = Environment.TickCount;
    }

    private void method_15(Queue<GLocation> queue_0, GLocation glocation_3)
    {
        Logger.LoadProfile("# Walking GhostWalkPath, queue contains: " + queue_0.Count + " items");
        var num = ConfigManager.gclass61_0.method_4("CorpseShortCircuit");
        while (queue_0.Count > 0)
        {
            var glocation = queue_0.Dequeue();
            Logger.LoadProfile("# Dequeued loc: " + glocation);
            if (GContext.Main.Movement.CompareHeadings(gplayerSelf_0.Heading,
                    gplayerSelf_0.Location.GetHeadingTo(glocation)) > 0.9 && GContext.Main.IsRunning)
            {
                GContext.Main.ReleaseRun();
                GContext.Main.Movement.SetHeading(glocation);
            }

            GContext.Main.StartRun();
            GContext.Main.Movement.MoveToLocation(glocation, 6.0, true);
            if (glocation_3.DistanceToSelf < num)
            {
                Logger.LoadProfile("# Corpse is close stopping pathing, gwp's left: " + queue_0.Count);
                break;
            }
        }

        GContext.Main.ReleaseSpinRun();
        Logger.LoadProfile("# Done with GhostWalkPath");
    }

    private void method_16(GLocation glocation_3)
    {
        var num1 = 4;
        Logger.LogMessage(MessageProvider.GetMessage(196));
        var gclass8_1 = UIElement.IsGroupProfile("StaticPopup1");
        GContext.Main.Movement.MoveToLocation(glocation_3, 6.0, false);
        if (Environment.TickCount - initCount < 600000 && initCount != 0)
        {
            Logger.LogMessage(MessageProvider.GetMessage(197));
            for (var index = 0; index < 5; ++index)
            {
                SpellcastingManager.gclass42_0.method_0("Common.RotateLeft");
                StartupClass.Sleep(60000);
            }
        }

        Logger.LogMessage(MessageProvider.GetMessage(198));
        while (true)
        {
            GContext.Main.ReleaseSpinRun();
            var nearestHostile1 = GObjectList.GetNearestHostile();
            if (nearestHostile1 != null &&
                (nearestHostile1.DistanceToSelf < 25.0 || nearestHostile1.DistanceToSelf > 30.0))
            {
                Logger.LogMessage(MessageProvider.GetMessage(200));
                nearestHostile1.Face();
                var num2 = 200;
                GContext.Main.PressKey("Common.Back");
                for (;
                     nearestHostile1.DistanceToSelf < 25.0 && num2 > 0 &&
                     gplayerSelf_0.Location.GetDistanceTo(glocation_3) < 30.0;
                     --num2)
                {
                    nearestHostile1 = GObjectList.GetNearestHostile();
                    if (nearestHostile1 != null || !gclass8_1.method_10())
                    {
                        nearestHostile1.Face();
                        StartupClass.Sleep(500);
                    }
                    else
                    {
                        break;
                    }
                }

                GContext.Main.ReleaseKey("Common.Back");
                var nearestHostile2 = GObjectList.GetNearestHostile();
                if ((nearestHostile2 != null && nearestHostile2.DistanceToSelf < 25.0) ||
                    gplayerSelf_0.Location.GetDistanceTo(glocation_3) >= 30.0 || !gclass8_1.method_10())
                {
                    Logger.LogMessage(MessageProvider.IsGroupProfile(201, nearestHostile2.DistanceToSelf.ToString()));
                    --num1;
                    if (num1 > 0 || !gclass8_1.method_10())
                    {
                        GContext.Main.Movement.MoveToLocation(glocation_3, 6.0, true);
                        StartupClass.Sleep(6000);
                    }
                    else
                    {
                        goto label_15;
                    }
                }
                else
                {
                    break;
                }
            }
            else
            {
                goto label_14;
            }
        }

        Logger.LogMessage(MessageProvider.GetMessage(199));
        goto label_15;
    label_14:
        Logger.LogMessage(MessageProvider.GetMessage(199));
    label_15:
        var gclass8_2 = UIElement.IsGroupProfile("StaticPopup1Button1");
        if (gclass8_2 == null)
            StartupClass.StopGlide(false, "NoAcceptButtonOnRes");
        gclass8_2.method_16(false);
    }

    public bool method_17()
    {
        if (!GContext.Main.Me.IsUnderAttack)
            return false;
        Logger.LoadProfile("- GotAttacker returning true:");
        foreach (object attacker in GObjectList.GetAttackers())
            Logger.LoadProfile("- " + attacker);
        return true;
    }

    public bool method_18(int int_15)
    {
        if ((int_15 == 13 && gplayerSelf_0.HasHerbalism) || (int_15 == 11 && gplayerSelf_0.HasMining) || int_15 == 5)
            return true;
        Logger.LogMessage("Cannot Harvest Item:" + int_15);
        return false;
    }

    public bool method_19()
    {
        if (ActiveGProfile.IgnoreAttackers)
            return false;
        gplayerSelf_0.Refresh();
        var gunit_0 = GObjectList.GetNearestAttacker(0L);
        if (gunit_0 == null)
        {
            gunit_0 = partyManager.method_3();
            if (gunit_0 == null)
            {
                if (gplayerSelf_0.TargetGUID == 0L || gplayerSelf_0.Target == null || gplayerSelf_0.Target.IsDead)
                    return false;
                method_12(true);
                return true;
            }
        }

        return partyManager.method_7(gunit_0);
    }

    public static void LoadProfile()
    {
        if (GPlayerSelf.Me.TargetGUID == 0L)
            return;
        Logger.LoadProfile("Sending Esc to clear target");
        InputController.StartMainThread(27);
    }

    public void method_20()
    {
        var gclass8 = UIElement.IsGroupProfile("StaticPopup1Button1");
        if (gclass8 != null)
        {
            gclass8.method_16(false);
            Thread.Sleep(1000);
        }
        else
        {
            double double_3;
            for (double_3 = 0.23; double_3 < 0.286; double_3 += 0.008)
            {
                InputController.ParseProcessIdFromCommandLine(0.505, double_3);
                StartupClass.Sleep(300);
                InputController.AddWaypoint(false);
                StartupClass.Sleep(500);
            }

            Logger.LogMessage(MessageProvider.IsGroupProfile(203, double_3));
        }
    }

    public void method_21(bool IsAutoLoginPending)
    {
        var num = 1;
        GContext.Main.ReleaseSpinRun();
        StartupClass.IsFocusTimerActive = true;
        StartupClass.CurrentGameClass.LeaveForm();
        while (true)
        {
            do
            {
                LoadProfile();
                StartupClass.resumeTimer.method_4();
                GContext.Main.CastSpell("Common.Hearth");
                if (gplayerSelf_0.TargetGUID != 0L)
                {
                    if (gplayerSelf_0.IsUnderAttack)
                    {
                        Logger.LogMessage(MessageProvider.GetMessage(204));
                        GContext.Main.SendKey("Common.Back");
                        StartupClass.combatController.method_12(true);
                    }

                    ++num;
                }
                else
                {
                    goto label_6;
                }
            } while (num <= 3);

            Logger.LogMessage(MessageProvider.GetMessage(745));
            StartupClass.StopGlide(false, "HearthFutility");
        }

    label_6:
        if (!IsAutoLoginPending)
        {
            StartupClass.pendingAutoLoginName = null;
            StartupClass.IsLoginTimerActive = false;
            StartupClass.StopGlide(false, "HearthAndExit");
            throw new ThreadInterruptedException();
        }

        StartupClass.GameMemoryWriter.method_2("OnHearth", false);
    }

    public void method_22()
    {
        GContext.Main.ReleaseSpinRun();
        StartupClass.IsFocusTimerActive = true;
        StartupClass.resumeTimer.method_5();
        StartupClass.CurrentGameClass.LeaveForm();
        StartupClass.StopGlide(true, "StopAndExit");
        throw new ThreadInterruptedException();
    }

    public void method_23(string string_2, bool IsAutoLoginPending)
    {
        lock (this)
        {
            string_0 = string_2;
            IsOpenMemoryModel = IsAutoLoginPending;
        }
    }

    public void method_24()
    {
        lock (this)
        {
            string_0 = null;
        }
    }

    [SpecialName]
    public bool method_25()
    {
        return string_0 != null;
    }

    public void method_26()
    {
        if (!bool_4)
            return;
        lock (this)
        {
            if (string_0 != null)
            {
                if (IsOpenMemoryModel)
                {
                    Logger.LogMessage("Sending queued keys");
                    StartupClass.SendInputString(string_0);
                }
                else
                {
                    if (!InputController.bool_0)
                    {
                        InputController.StartMainThread(13);
                        StartupClass.Sleep(900);
                    }

                    Logger.LogMessage("Sending queued chat message");
                    InputController.ExecuteStopGlide(string_0);
                }

                Logger.LogMessage("Queued keys sent.");
                string_0 = null;
            }
        }

        var fileInfo = new FileInfo("chat.txt");
        if (!fileInfo.Exists)
            return;
        if (ConfigManager.gclass61_0.method_5("HandleChatTxt"))
        {
            Logger.LogMessage(MessageProvider.GetMessage(205));
            var streamReader = new StreamReader(fileInfo.FullName);
            while (true)
            {
                var string_1 = streamReader.ReadLine();
                if (string_1 != null && string_1.Length >= 2)
                    InputController.ExecuteStopGlide(string_1);
                else
                    break;
            }

            streamReader.Close();
            try
            {
                fileInfo.Delete();
            }
            catch (Exception ex)
            {
                Logger.LogMessage(MessageProvider.IsGroupProfile(675, ex.Message));
                bool_4 = false;
            }
        }
        else
        {
            if (IsVersionMismatch)
                return;
            IsVersionMismatch = true;
            Logger.LogMessage("Chat.txt is present, but HandleChatTxt is not true in config");
        }
    }

    public void method_27()
    {
        resumeTimer = method_28();
    }

    public GameTimer method_28()
    {
        var gclass36 =
            new GameTimer((!bool_12 ? 40 + StartupClass.random_0.Next() % 160 : 10 + StartupClass.random_0.Next() % 30) *
                         1000);
        gclass36.method_4();
        return gclass36;
    }

    public void method_29()
    {
        var num = StartupClass.random_0.Next() % 15 + 5;
        var gclass36 = new GameTimer(60000);
        gclass36.method_4();
        while (!gclass36.method_3())
        {
            method_36(debuffUpdateTimer, "Common.Time1");
            method_36(staleTimer, "Common.Time5");
            method_36(gclass36_5, "Common.Time30");
            method_32();
            if (bool_8 && dateTime_0 < DateTime.Now)
            {
                Logger.LogMessage(MessageProvider.GetMessage(162));
                method_21(true);
            }

            if (ProfileGroupManager.LoadSingleProfile())
            {
                ActiveGProfile = StartupClass.ActiveGProfile;
                if (!ActiveGProfile.IgnoreAttackers)
                    ProfileGroupManager.GetFileNameFromPath();
            }

            if (attachCooldownTimer != null && attachCooldownTimer.method_3())
            {
                Logger.LogMessage(MessageProvider.GetMessage(233));
                if (GContext.Main.Interface.GetActionInventory("Common.LureSlot") == 0)
                {
                    Logger.LogMessage(MessageProvider.GetMessage(234));
                    attachCooldownTimer = null;
                }
                else
                {
                    SpellcastingManager.gclass42_0.method_0("Common.LureSlot");
                    var gclass8_1 = UIElement.IsGroupProfile("CharacterFrame");
                    var gclass8_2 = UIElement.IsGroupProfile("CharacterMainHandSlot");
                    if (gclass8_1 != null && gclass8_2 != null)
                    {
                        if (!gclass8_1.method_10())
                        {
                            GContext.Main.SendKey("Common.Character");
                            Thread.Sleep(1000);
                            if (!gclass8_1.method_10())
                            {
                                Logger.LoadProfile("CharFrame never became visible after keystroke!");
                                break;
                            }
                        }

                        StartupClass.Sleep(500);
                        gclass8_2.method_16(false);
                        GContext.Main.SendKey("Common.Character");
                        StartupClass.Sleep(5000);
                        attachCooldownTimer.method_4();
                    }
                    else
                    {
                        Logger.LoadProfile("Couldn't get CharacterFrame or CharacterMainHandSlot");
                        break;
                    }
                }
            }
        }
    }

    public bool method_30(long long_1, double double_8, double double_9, double double_10)
    {
        var int_29 = MemoryOffsetTable.Instance.GetIntOffset("UnderCursor");
        var flag = false;
        Logger.LoadProfile("Position on bobber: " + double_8 + " -> " + double_9 + ", inc = " + double_10);
        for (var double_3 = 0.08; double_3 < 0.6 && !flag; double_3 += double_10)
            for (var double_2 = double_8; double_2 < double_9; double_2 += double_10)
            {
                InputController.ParseProcessIdFromCommandLine(double_2, double_3);
                GProcessMemoryManipulator.Sleep(20U);
                if (GProcessMemoryManipulator.ReadInt64(int_29, "UnderCursor3") != long_1)
                {
                    if (bool_10)
                        return false;
                }
                else
                {
                    flag = true;
                    break;
                }
            }

        if (flag)
        {
            StartupClass.Sleep(500);
            flag = GProcessMemoryManipulator.ReadInt64(int_29, "UnderCursor4") == long_1;
        }

        Logger.LoadProfile(MessageProvider.IsGroupProfile(235, flag));
        return flag;
    }

    public GObject method_31()
    {
        gplayerSelf_0.Refresh();
        var channelingObjectId = gplayerSelf_0.ChannelingObjectID;
        return channelingObjectId == 0L ? null : GObjectList.FindObject(channelingObjectId);
    }

    public void method_32()
    {
        Logger.LoadProfile(MessageProvider.GetMessage(239));
        GObject gobject = null;
        var num = 20;
        method_26();
        PlayerTracker.IsGroupProfile();
        StartupClass.Sleep(1000);
        Logger.LoadProfile(MessageProvider.GetMessage(240));
        SpellcastingManager.gclass42_0.Offsets["Common.Fish"].FilloutKey();
        SpellcastingManager.gclass42_0.method_0("Common.Fish");
        StartupClass.Sleep(1000);
        Logger.LoadProfile(MessageProvider.GetMessage(241));
        for (; num > 0; --num)
        {
            gobject = method_31();
            if (gobject == null)
                StartupClass.Sleep(300);
            else
                break;
        }

        if (gobject == null)
        {
            Logger.LogMessage(MessageProvider.GetMessage(243));
            method_33();
        }
        else
        {
            Logger.LoadProfile(MessageProvider.GetMessage(244));
            var gclass36 = new GameTimer(32000);
            gclass36.method_4();
            StartupClass.Sleep(1000);
            if (!gobject.Hover())
            {
                Logger.LogMessage(MessageProvider.GetMessage(245));
                method_33();
            }
            else
            {
                while (!gclass36.method_3() && !gobject.IsBobbing)
                {
                    StartupClass.Sleep(200);
                    method_26();
                    gplayerSelf_0.Refresh();
                    if (gplayerSelf_0.TargetGUID != 0L)
                    {
                        Logger.LogMessage(MessageProvider.GetMessage(246));
                        method_12(true);
                    }
                }

                if (gclass36.method_3())
                {
                    Logger.LogMessage(MessageProvider.GetMessage(247));
                    method_33();
                }
                else
                {
                    method_34(200, 600);
                    if (!gobject.IsCursorOnObject)
                    {
                        Logger.LogMessage(MessageProvider.GetMessage(248));
                        method_33();
                    }
                    else
                    {
                        Logger.LogMessage(MessageProvider.GetMessage(249));
                        ++StartupClass.expectedVersion;
                        method_56();
                        method_34(2000, 5000);
                    }
                }
            }
        }
    }

    public void method_33()
    {
        StartupClass.Sleep(1000);
    }

    public void method_34(int int_15, int int_16)
    {
        StartupClass.Sleep(StartupClass.random_0.Next() % (int_16 - int_15) + int_15);
    }

    public void method_35()
    {
        gclass36_7.method_4();
        glocation_2 = null;
    }

    private void method_36(GameTimer gclass36_8, string string_2)
    {
        if (SpellcastingManager.gclass42_0.method_15(string_2) || !gclass36_8.method_3())
            return;
        GContext.Main.ReleaseSpinRun();
        GContext.Main.CastSpell(string_2);
        gclass36_8.method_4();
    }

    private bool method_37()
    {
        return (ConfigManager.gclass61_0.method_5("AvoidSameFaction") && PlayerTracker.LoadSingleProfile()) ||
               (ConfigManager.gclass61_0.method_5("AvoidOtherFaction") && PlayerTracker.GetFileNameFromPath());
    }

    private void method_38()
    {
        GContext.Main.ReleaseSpinRun();
        while (method_37())
        {
            if (StartupClass.CurrentGameClass.CanStealth && !GContext.Main.Me.IsStealth)
                StartupClass.CurrentGameClass.EnterStealth(true);
            method_6();
            StartupClass.Sleep(1000);
            gplayerSelf_0.Refresh();
            PlayerTracker.IsGroupProfile();
            if (gplayerSelf_0.TargetGUID != 0L && gplayerSelf_0.Target != null && !gplayerSelf_0.Target.IsDead)
            {
                Logger.LogMessage(MessageProvider.GetMessage(254));
                method_12(true);
            }
        }

        StartupClass.Sleep(1000);
    }

    public void method_39()
    {
        glocation_0 = null;
        GMonster gmonster = null;
        gclass36_7 = new GameTimer(1300);
        method_35();
        var resumeTimer = new GameTimer(250);
        var attachCooldownTimer = new GameTimer(2200);
        var flag1 = false;
        var debuffUpdateTimer = new GameTimer(12000);
        var num1 = 0;
        var flag2 = false;
        var num2 = 25;
        var num3 = 125;
        var gspellTimer = new GSpellTimer(2000, false);
        if (GContext.Main.MouseSpin)
            num2 = 15;
        ActiveGProfile.OneShotHit = false;
        ActiveGProfile.OneShotStepCheck = 0;
        if (ActiveGProfile.IgnoreAttackers)
            ProfileGroupManager.ParseDouble();
        resumeTimer.method_5();
        if (glocation_1 != null)
            method_64(glocation_1);
        while (true)
        {
            GUnit target = gplayerSelf_0.Target;
            if (target != null && target.IsDead)
                goto label_91;
        label_6:
            GContext.Main.PulseSpin(!GContext.Main.IsRunning);
            if (GContext.Main.Overspin)
            {
                GContext.Main.ReleaseSpinRun();
                Logger.LogMessage("Spinning for too long, letting go of key and re-syncing");
                Thread.Sleep(5000);
                gmonster = null;
                ActiveGProfile.BeginProfile(GPlayerSelf.Me.Location);
            }

            if (resumeTimer.method_3())
            {
                resumeTimer.method_4();
                if (method_37())
                {
                    Logger.LogMessage(MessageProvider.GetMessage(byte.MaxValue));
                    method_38();
                }

                if (!GContext.Main.IsSpinning)
                {
                    DialogMonitor.IsGroupProfile();
                    method_26();
                    PlayerTracker.IsGroupProfile();
                    StartupClass.CurrentGameClass.RunningAction();
                    if (StartupClass.CurrentGameClass.ShouldRest())
                    {
                        GContext.Main.ReleaseSpinRun();
                        method_8();
                    }

                    method_36(this.debuffUpdateTimer, "Common.Time1");
                    method_36(staleTimer, "Common.Time5");
                    method_36(gclass36_5, "Common.Time30");
                }

                if (partyManager.genum7_0 == PartyRole.const_1)
                    partyManager.method_6();
                if (ActiveGProfile.RunFromAvoids)
                    method_40();
                if (!ProfileGroupManager.LoadSingleProfile())
                {
                    if (gplayerSelf_0.IsDead)
                    {
                        Logger.LoadProfile("# IsDead = true in main loop");
                        gclass36_7.method_4();
                        GContext.Main.ReleaseSpinRun();
                        Logger.LogMessage(MessageProvider.GetMessage(157));
                        glocation_0 = null;
                        if (!(ConfigManager.gclass61_0.method_2("Resurrect") != "True"))
                        {
                            if (ActiveGProfile.GhostWaypoints.Count != 0)
                            {
                                if (StartupClass.versionPatchLevel < lastAclProcessId)
                                    method_14(GPlayerSelf.Me.Location, true);
                                else
                                    goto label_96;
                            }
                            else
                            {
                                goto label_95;
                            }
                        }
                        else
                        {
                            goto label_94;
                        }
                    }

                    var nextProfileTarget = GObjectList.GetNextProfileTarget();
                    if (nextProfileTarget != null)
                    {
                        if (gmonster == null || nextProfileTarget.GUID != gmonster.GUID)
                        {
                            Logger.LoadProfile("## New target = \"" + nextProfileTarget.Name + "\", GUID = 0x" +
                                               nextProfileTarget.GUID.ToString("x16"));
                            Logger.LoadProfile("## Profile.Wander = " + ActiveGProfile.Wander + ", Profile distance = " +
                                               ActiveGProfile.GetDistanceTo(nextProfileTarget.Location));
                            if (gmonster != null)
                                Logger.LoadProfile(MessageProvider.GetMessage(258));
                            gmonster = nextProfileTarget;
                            if (gmonster.DistanceToSelf > (double)StartupClass.CurrentGameClass.PullDistance)
                                ActiveGProfile.PlaceBreadcrumb();
                        }

                        if (nextProfileTarget.DistanceToSelf < StartupClass.CurrentGameClass.PullDistance + 15.0 &&
                            nextProfileTarget.DistanceToSelf > (double)StartupClass.CurrentGameClass.PullDistance &&
                            attachCooldownTimer.method_3())
                        {
                            attachCooldownTimer.method_4();
                            StartupClass.CurrentGameClass.ApproachingTarget(nextProfileTarget);
                        }

                        if (nextProfileTarget.DistanceToSelf <= (double)StartupClass.CurrentGameClass.PullDistance)
                        {
                            var heading = gplayerSelf_0.Heading;
                            Logger.LoadProfile(MessageProvider.IsGroupProfile(683, nextProfileTarget.Name,
                                nextProfileTarget.GUID.ToString("x")));
                            if (GContext.Main.Movement.CompareHeadings(gplayerSelf_0.Heading,
                                    gplayerSelf_0.Location.GetHeadingTo(nextProfileTarget.Location)) > Math.PI / 6.0)
                                GContext.Main.ReleaseRun();
                            GContext.Main.ReleaseSpin();
                            Logger.LoadProfile("Facing enemy");
                            nextProfileTarget.Face();
                            gclass36_7.method_4();
                            if (!nextProfileTarget.SetAsTarget(false))
                            {
                                LoadProfile();
                                Logger.LoadProfile(MessageProvider.GetMessage(259));
                                StartupClass.ActiveGProfile.AddToBlacklist(nextProfileTarget.GUID);
                                continue;
                            }

                            StartupClass.CurrentGameClass.TargetAcquired(nextProfileTarget);
                            gmonster = null;
                            method_12(false);
                            glocation_0 = gplayerSelf_0.Location;
                            num1 = 0;
                            ActiveGProfile.ConsiderWaypointSkip();
                            continue;
                        }
                    }
                    else
                    {
                        if (gmonster != null)
                            Logger.LoadProfile("### Old target no longer valid (\"" + gmonster.Name +
                                               "\", distance = " + gmonster.DistanceToSelf + "), skipreason = " +
                                               gmonster.SkipReason);
                        gmonster = null;
                    }

                    if (bool_7)
                    {
                        bool_7 = false;
                        GContext.Main.SendKey("Common.PostLoot");
                    }

                    if (GContext.Main.IsRunning && gclass36_7.method_3())
                    {
                        if (glocation_2 == null)
                        {
                            glocation_2 = gplayerSelf_0.Location;
                        }
                        else
                        {
                            if (gplayerSelf_0.Location.GetDistanceTo(glocation_2) < 3.0)
                            {
                                if (ConfigManager.gclass61_0.method_5("StrafeObstacles") && !flag1)
                                {
                                    Logger.LogMessage(MessageProvider.GetMessage(742));
                                    var string_1 = "Common.StrafeLeft";
                                    if (StartupClass.random_0.Next() % 2 == 0)
                                        string_1 = "Common.StrafeRight";
                                    SpellcastingManager.gclass42_0.method_1(string_1);
                                    StartupClass.Sleep(1200);
                                    SpellcastingManager.gclass42_0.method_2(string_1);
                                    flag1 = true;
                                }
                                else
                                {
                                    Logger.LogMessage(MessageProvider.GetMessage(256));
                                    GContext.Main.ReleaseSpinRun();
                                    StartupClass.Sleep(600);
                                    GContext.Main.PressKey("Common.Back");
                                    StartupClass.Sleep(2000);
                                    GContext.Main.ReleaseKey("Common.Back");
                                    var DeltaRads = Math.PI / 2.0;
                                    if (StartupClass.random_0.Next() % 2 == 0)
                                        DeltaRads *= -1.0;
                                    var NewHeading =
                                        GContext.Main.Movement.AdjustHeading(gplayerSelf_0.Heading, DeltaRads);
                                    GContext.Main.Movement.SetHeading(NewHeading);
                                    GContext.Main.StartRun();
                                    method_34(1000, 2500);
                                    method_35();
                                }

                                ++num1;
                            }

                            glocation_2 = gplayerSelf_0.Location;
                            if (num1 > pgEditProfileCount)
                            {
                                Logger.LogMessage("Stuck too many times");
                                if (gmonster != null)
                                {
                                    Logger.LogMessage("Blacklisting this target");
                                    ActiveGProfile.ForceBlacklist(gmonster.GUID);
                                    gmonster = null;
                                    num1 = 0;
                                }
                                else
                                {
                                    if (flag2)
                                    {
                                        Logger.LogMessage("Still stuck after previous-waypoint recovery, skipping current waypoint");
                                        ActiveGProfile.ConsumeCurrentWaypoint();
                                        flag2 = false;
                                        num1 = 0;
                                        method_35();
                                        continue;
                                    }
                                    Logger.LogMessage("Stuck too much, trying previous waypoint");
                                    flag2 = true;
                                    ActiveGProfile.SetPreviousWaypoint();
                                }
                            }
                        }

                        gclass36_7.method_4();
                    }

                    if (GContext.Main.MoveHelper != null)
                        GContext.Main.MoveHelper.PatrolTowards(gmonster == null
                            ? ActiveGProfile.CurrentWaypoint
                            : (object)gmonster);
                    else
                        GContext.Main.Movement.BasePatrolTowards(gmonster == null
                            ? ActiveGProfile.CurrentWaypoint
                            : (object)gmonster);
                    double distanceTo1 = gplayerSelf_0.Location.GetDistanceTo(ActiveGProfile.CurrentWaypoint);
                    var double7 = double_7;
                    if (ProfileGroupManager.ApplyConfig())
                        double7 *= 2.0;
                    if (distanceTo1 < double7 && gmonster == null)
                    {
                        flag2 = false;
                        Logger.LoadProfile("## Reached current waypoint, it is: " + ActiveGProfile.DebugCurrentWaypoint());
                        flag1 = false;
                        ++ActiveGProfile.OneShotStepCheck;
                        ActiveGProfile.ConsumeCurrentWaypoint();
                        num1 = 0;
                        if (ActiveGProfile.OneShotHit && StartupClass.profileGroupManager == null)
                        {
                            Logger.LogMessage(MessageProvider.GetMessage(257));
                            GContext.Main.ReleaseSpinRun();
                            StartupClass.StopGlide(false, "EndOfOneShotProfile");
                        }

                        GContext.Main.ReleaseSpin();
                        ++knownVersion;
                        method_35();
                    }

                    if (distanceTo1 > 15.0 && !GContext.Main.IsSpinning && this.resumeTimer.method_3())
                    {
                        SpellcastingManager.gclass42_0.method_0("Common.Jump");
                        StartupClass.Sleep(1800);
                        method_27();
                        if (bool_12 && StartupClass.random_0.Next() % 10 == 0)
                            this.resumeTimer.method_5();
                    }
                    else if (distanceTo1 > 20.0 && IsGameProcessAttached && licenseCheckTimer.method_3() && GContext.Main.IsRunning)
                    {
                        if (StartupClass.random_0.Next() % 10 == 0)
                        {
                            var string_1 = StartupClass.random_0.Next() % 2 == 0
                                ? "Common.StrafeLeft"
                                : "Common.StrafeRight";
                            SpellcastingManager.gclass42_0.method_1(string_1);
                            method_34(500, 1200);
                            SpellcastingManager.gclass42_0.method_2(string_1);
                        }

                        licenseCheckTimer = new GameTimer(1000 + StartupClass.random_0.Next() % 1500);
                        licenseCheckTimer.method_4();
                    }

                    method_19();
                    if (!ActiveGProfile.IgnoreAttackers)
                        method_52(true);
                    if (knownVersion == ActiveGProfile.Waypoints.Count * 2 && ConfigManager.gclass61_0.method_5("SitWhenBored") &&
                        !GContext.Main.IsSpinning)
                    {
                        Logger.LogMessage(MessageProvider.GetMessage(161));
                        --knownVersion;
                        GContext.Main.ReleaseSpinRun();
                        StartupClass.Sleep(1000);
                        SpellcastingManager.gclass42_0.method_0("Common.Sit");
                        var staleTimer = new GameTimer(60000);
                        staleTimer.method_4();
                        while (!staleTimer.method_3())
                        {
                            StartupClass.Sleep(2000);
                            if (method_10() > 0)
                                break;
                        }
                    }

                    if (!ActiveGProfile.IgnoreAttackers && method_44())
                        glocation_0 = gplayerSelf_0.Location;
                    method_6();
                    if (glocation_0 != null)
                    {
                        double distanceTo2 = gplayerSelf_0.Location.GetDistanceTo(glocation_0);
                        if (distanceTo2 > 50.0)
                        {
                            Logger.LogMessage(MessageProvider.IsGroupProfile(684, distanceTo2));
                            Logger.LogMessage(MessageProvider.IsGroupProfile(685, glocation_0.ToString(),
                                gplayerSelf_0.Location.ToString()));
                            method_58();
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            StartupClass.Sleep(GContext.Main.IsSpinning ? num2 : num3);
            glocation_0 = gplayerSelf_0.Location;
            continue;
        label_91:
            LoadProfile();
            goto label_6;
        }

        GContext.Main.ReleaseSpinRun();
        return;
    label_94:
        Logger.LogMessage(MessageProvider.GetMessage(158));
        StartupClass.StopGlide(false, "DeadResurrectOff");
        return;
    label_95:
        Logger.LogMessage(MessageProvider.GetMessage(159));
        StartupClass.StopGlide(false, "DeadNoGhostWP");
        return;
    label_96:
        Logger.LogMessage(MessageProvider.GetMessage(160));
        StartupClass.StopGlide(false, "DeadTooMany");
    }

    protected bool method_40()
    {
        if (StartupClass.ActiveGProfile.AvoidList != null)
        {
            var gmonster_0 = method_41();
            if (gmonster_0 != null && gmonster_0.DistanceToSelf < 32.0)
            {
                method_42(gmonster_0);
                return true;
            }
        }

        return false;
    }

    protected GMonster method_41()
    {
        var num = 999.0;
        var monsters = GObjectList.GetMonsters();
        GMonster gmonster1 = null;
        foreach (var gmonster2 in monsters)
            if (gmonster2.IsInList(ActiveGProfile.AvoidList) && gmonster2.Health > 0.9 && gmonster2.DistanceToSelf < num)
            {
                num = gmonster2.DistanceToSelf;
                gmonster1 = gmonster2;
            }

        return gmonster1;
    }

    protected void method_42(GMonster gmonster_0)
    {
        var flag1 = false;
        var flag2 = true;
        double headingTo = gmonster_0.Location.GetHeadingTo(gplayerSelf_0.Location);
        var gclass36 = new GameTimer(10000);
        gclass36.method_4();
        Logger.LogMessage(MessageProvider.IsGroupProfile(262, gmonster_0.Name));
        GContext.Main.ReleaseSpinRun();
        if (gmonster_0.DistanceToSelf > 10.0)
            method_34(500, 1500);
        if (gmonster_0.DistanceToSelf < 20.0)
            flag2 = false;
        if (Math.Abs(GContext.Main.Movement.CompareHeadings(gplayerSelf_0.Heading, headingTo)) < Math.PI / 2.0)
            flag2 = false;
        double NewHeading;
        string string_1;
        if (flag2)
        {
            Logger.LoadProfile(MessageProvider.GetMessage(263));
            NewHeading = method_43(gplayerSelf_0.Location.GetHeadingTo(gmonster_0.Location));
            string_1 = "Common.Back";
        }
        else
        {
            Logger.LoadProfile(MessageProvider.GetMessage(264));
            NewHeading = method_43(headingTo);
            string_1 = "Common.Forward";
        }

        GContext.Main.Movement.SetHeading(NewHeading);
        SpellcastingManager.gclass42_0.method_1(string_1);
        while (!gclass36.method_3() && gmonster_0.IsValid)
            if (gmonster_0.DistanceToSelf < 30.0)
            {
                if (gplayerSelf_0.TargetGUID == 0L)
                {
                    StartupClass.Sleep(200);
                }
                else
                {
                    Logger.LogMessage(MessageProvider.GetMessage(265));
                    break;
                }
            }
            else
            {
                flag1 = true;
                break;
            }

        SpellcastingManager.gclass42_0.method_2(string_1);
        if (gclass36.method_3())
            Logger.LogMessage(MessageProvider.GetMessage(266));
        if (flag1)
        {
            Logger.LogMessage(MessageProvider.GetMessage(267));
            method_34(2000, 4500);
        }

        glocation_0 = gplayerSelf_0.Location;
    }

    protected double method_43(double double_8)
    {
        var num = StartupClass.random_0.NextDouble() * (Math.PI / 2.0) - Math.PI / 4.0;
        double_8 += num;
        if (double_8 < 0.0)
            double_8 += 2.0 * Math.PI;
        if (double_8 >= Math.PI)
            double_8 -= 2.0 * Math.PI;
        return double_8;
    }

    public bool method_44()
    {
        if (cachedGlideRate == 0)
            return false;
        var closestHarvestable = GObjectList.GetClosestHarvestable();
        if (closestHarvestable == null)
            return false;
        double distanceToSelf = closestHarvestable.Location.DistanceToSelf;
        if (distanceToSelf > cachedGlideRate)
            return false;
        Logger.LogMessage(MessageProvider.IsGroupProfile(686, Math.Round(distanceToSelf, 2)));
        GContext.Main.ReleaseSpinRun();
        GContext.Main.Movement.MoveToLocation(closestHarvestable.Location, GContext.Main.MeleeDistance, false);
        if (closestHarvestable.DistanceToSelf > GContext.Main.MeleeDistance)
        {
            Logger.LogMessage(MessageProvider.GetMessage(268));
            StartupClass.corpseSortedList.Add(closestHarvestable.GUID, "");
            return true;
        }

        gplayerSelf_0.Refresh();
        if (method_19())
        {
            Logger.LogMessage(MessageProvider.GetMessage(269));
            return true;
        }

        method_45(closestHarvestable);
        return true;
    }

    public void method_45(GNode gnode_0)
    {
        method_60();
        if (gnode_0.IsFlower && !gplayerSelf_0.HasHerbalism)
        {
            StartupClass.corpseSortedList.Add(gnode_0.GUID, "");
        }
        else if (gnode_0.IsMineral && !gplayerSelf_0.HasMining)
        {
            StartupClass.corpseSortedList.Add(gnode_0.GUID, "");
        }
        else
        {
            Logger.LogMessage(MessageProvider.IsGroupProfile(786, gnode_0.Name));
            var num = 0;
            gnode_0.Hover();
            if (!method_18(GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("CursorType"), "CursorType")))
            {
                Logger.LogMessage("Can't harvest this, cursor never turned into something acceptable");
                StartupClass.corpseSortedList.Add(gnode_0.GUID, "");
            }
            else
            {
                for (; num < 9; ++num)
                    if (!method_19())
                    {
                        var int_15 = GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("CursorType"), "CursorType");
                        if (method_18(int_15))
                        {
                            method_56();
                            Thread.Sleep(1000);
                            if (gplayerSelf_0.IsCasting)
                                while (gplayerSelf_0.IsCasting)
                                    Thread.Sleep(200);
                            Thread.Sleep(2000);
                            Logger.LogMessage(MessageProvider.GetMessage(280));
                        }
                        else
                        {
                            StartupClass.corpseSortedList.Add(gnode_0.GUID, "");
                            Logger.LogMessage("Item not Havestable: " + int_15);
                            return;
                        }
                    }
                    else
                    {
                        Logger.LogMessage(MessageProvider.GetMessage(271));
                        return;
                    }

                Logger.LogMessage(MessageProvider.GetMessage(281));
                StartupClass.corpseSortedList.Add(gnode_0.GUID, "");
            }
        }
    }

    public void method_46()
    {
        Logger.LogMessage(MessageProvider.GetMessage(282));
        var resumeTimer = new GameTimer(5000);
        var attachCooldownTimer = new GameTimer(2000);
        var debuffUpdateTimer = new GameTimer(1000);
        long num = 0;
        while (true)
        {
            GPlayer gplayer;
            do
            {
                if (debuffUpdateTimer.method_3())
                    goto label_33;
            label_1:
                if (resumeTimer.method_3())
                {
                    resumeTimer.method_4();
                    method_26();
                }

                if (attachCooldownTimer.method_3())
                {
                    PlayerTracker.IsGroupProfile();
                    attachCooldownTimer.method_4();
                    if (!GContext.Main.IsSpinning)
                        StartupClass.CurrentGameClass.RunningAction();
                }

                if (!GContext.Main.IsSpinning)
                {
                    method_36(this.debuffUpdateTimer, "Common.Time1");
                    method_36(staleTimer, "Common.Time5");
                    method_36(gclass36_5, "Common.Time30");
                }

                if (ActiveGProfile.RunFromAvoids)
                    method_40();
                if (gplayerSelf_0.IsDead)
                {
                    GContext.Main.ReleaseSpinRun();
                    Logger.LogMessage(MessageProvider.GetMessage(157));
                    if (!(ConfigManager.gclass61_0.method_2("Resurrect") != "True"))
                    {
                        if (ActiveGProfile.GhostWaypoints.Count != 0)
                        {
                            if (StartupClass.versionPatchLevel < lastAclProcessId)
                                method_14(GPlayerSelf.Me.Location, true);
                            else
                                goto label_38;
                        }
                        else
                        {
                            goto label_37;
                        }
                    }
                    else
                    {
                        goto label_36;
                    }
                }

                gplayer = partyManager.method_4();
                continue;
            label_33:
                debuffUpdateTimer.method_4();
                if (method_37())
                {
                    Logger.LogMessage(MessageProvider.GetMessage(byte.MaxValue));
                    method_38();
                }

                goto label_1;
            } while (gplayer == null);

            if (gplayer.TargetGUID != 0L && gplayer.TargetGUID != num)
            {
                Logger.LoadProfile(MessageProvider.IsGroupProfile(687, gplayer.TargetGUID.ToString("x")));
                var unit = GObjectList.FindUnit(gplayer.TargetGUID);
                if (unit != null && unit.Health > 0.0 && !partyManager.method_13(unit.GUID))
                {
                    Logger.LoadProfile(MessageProvider.GetMessage(283));
                    var staleTimer = new GameTimer(8000);
                    staleTimer.method_4();
                    while (!staleTimer.method_3())
                        if (unit.Health >= 1.0 && unit.TargetGUID != gplayer.GUID)
                        {
                            if (gplayer.TargetGUID != unit.GUID)
                                LoadProfile();
                            else
                                StartupClass.Sleep(500);
                        }
                        else
                        {
                            Logger.LoadProfile(MessageProvider.GetMessage(284));
                            break;
                        }

                    GContext.Main.Movement.MoveToUnit(unit, StartupClass.CurrentGameClass.PullDistance, false);
                    if (unit.IsValid)
                    {
                        if (partyManager.method_17(unit))
                        {
                            num = 0L;
                            if (partyManager.int_2 > 0)
                                StartupClass.Sleep(partyManager.int_2 * 1000);
                            method_12(false);
                            LoadProfile();
                        }
                        else
                        {
                            LoadProfile();
                            Logger.LogMessage(MessageProvider.IsGroupProfile(688, unit.Name));
                            num = gplayer.TargetGUID;
                        }
                    }
                    else
                    {
                        Logger.LogMessage(MessageProvider.GetMessage(286));
                    }
                }
            }

            StartupClass.CurrentGameClass.CheckPartyHeal(null);
            StartupClass.CurrentGameClass.CheckPartyBuffs();
            method_44();
            StartupClass.Sleep(100);
            method_6();
            method_52(true);
        }

    label_36:
        Logger.LogMessage(MessageProvider.GetMessage(158));
        StartupClass.StopGlide(false, "PDeadNoResurrect");
        return;
    label_37:
        Logger.LogMessage(MessageProvider.GetMessage(159));
        StartupClass.StopGlide(false, "PDeadNoGhostWP");
        return;
    label_38:
        Logger.LogMessage(MessageProvider.GetMessage(160));
        StartupClass.StopGlide(false, "PDeadMaxDeaths");
    }

    private void method_47(GUnit gunit_0, int int_15)
    {
        var resumeTimer = new GameTimer(2000);
        resumeTimer.method_4();
        var attachCooldownTimer = new GameTimer(500);
        attachCooldownTimer.method_4();
        while (!resumeTimer.method_3())
        {
            gplayerSelf_0.Refresh();
            if (gplayerSelf_0.TargetGUID == gunit_0.GUID)
                break;
            if (gplayerSelf_0.TargetGUID == 0L)
            {
                gunit_0.Refresh(true);
                if (!gunit_0.IsValid)
                    break;
                if (gunit_0.DistanceToSelf >= (double)int_15)
                {
                    if (attachCooldownTimer.method_3())
                    {
                        GContext.Main.ReleaseSpinRun();
                        attachCooldownTimer.method_4();
                    }

                    StartupClass.Sleep(100);
                }
                else
                {
                    GContext.Main.ReleaseSpinRun();
                    break;
                }
            }
            else
            {
                GContext.Main.ReleaseSpinRun();
                LoadProfile();
                break;
            }
        }
    }

    private void method_48(GameTimer gclass36_8, GUnit gunit_0, int int_15)
    {
        while (!gclass36_8.method_3())
        {
            gunit_0.Refresh(true);
            if (gunit_0.IsValid)
            {
                gplayerSelf_0.Refresh();
                if (gunit_0.DistanceToSelf >= (double)int_15)
                {
                    StartupClass.Sleep(100);
                }
                else
                {
                    GContext.Main.ReleaseSpinRun();
                    break;
                }
            }
            else
            {
                break;
            }
        }

        gclass36_8.method_2();
    }

    private bool method_49()
    {
        var location = gplayerSelf_0.Location;
        var gspellTimer = new GSpellTimer(10000, false);
        Logger.LoadProfile("Waiting for teleport after releasing spirit...");
        while (!gspellTimer.IsReadySlow)
            if (location.GetDistanceTo(gplayerSelf_0.Location) > 5.0)
                return true;
        return false;
    }

    private bool method_50()
    {
        method_14(
            new GLocation(GProcessMemoryManipulator.ReadFloat(MemoryOffsetTable.Instance.GetIntOffset("CorpseLocation") - 8, "CorpseX"),
                (double)GProcessMemoryManipulator.ReadFloat(MemoryOffsetTable.Instance.GetIntOffset("CorpseLocation") - 4, "CorpseY")), false);
        return !gplayerSelf_0.IsDead;
    }

    public bool method_51()
    {
        double num = StartupClass.CurrentGameClass.PullDistance + objectManagerBasePointer;
        LootableCorpseTracker.ParseDouble();
        var gclass5 = LootableCorpseTracker.IsGroupProfile(GPlayerSelf.Me.Location);
        return gclass5 != null && gclass5.glocation_0.GetDistanceTo(gplayerSelf_0.Location) <= num;
    }

    public bool method_52(bool IsAutoLoginPending)
    {
        DialogMonitor.IsGroupProfile();
        var flag = false;
        var num = !IsAutoLoginPending || !ConfigManager.gclass61_0.method_5("WalkLoot")
            ? RestStatusMonitor.double_2
            : StartupClass.CurrentGameClass.PullDistance + objectManagerBasePointer;
        LootableCorpseTracker.ParseDouble();
        var gclass5_0 = LootableCorpseTracker.IsGroupProfile(gplayerSelf_0.Location);
        if (gclass5_0 != null && gclass5_0.glocation_0.GetDistanceTo(gplayerSelf_0.Location) <= num)
        {
            method_53(gclass5_0);
            flag = true;
            ActiveGProfile.ConsiderWaypointSkip();
        }

        LootableCorpseTracker.LoadSingleProfile();
        if (flag)
            InputController.StartManualGlide(false);
        return flag;
    }

    private void method_53(LootableCorpseTracker gclass5_0)
    {
        var unit = GObjectList.FindUnit(gclass5_0.playerGuid);
        GContext.Main.ReleaseSpinRun();
        if (gclass5_0.glocation_0.DistanceToSelf > RestStatusMonitor.double_2 - 1.0 &&
            !unit.Approach(RestStatusMonitor.double_2 - 1.0, false))
        {
            if (!method_17())
                gclass5_0.method_2();
            Logger.LogMessage(MessageProvider.GetMessage(212));
        }
        else
        {
            glocation_0 = gplayerSelf_0.Location;
            if (gclass5_0.bool_2)
            {
                if (gclass5_0.method_4(unit))
                {
                    gclass5_0.method_1();
                    return;
                }

                Logger.LogMessage("TurboLoot didn't work, doing regular loot");
            }

            var flag = false;
            if (!unit.IsSkinnable && gplayerSelf_0.HasSkinning && ConfigManager.gclass61_0.method_5("AutoSkin"))
                flag = true;
            method_57(gclass5_0, unit, unit.IsSkinnable);
            if (method_17() || !flag)
                return;
            var gclass36 = new GameTimer(ConfigManager.gclass61_0.method_3("SkinDelay") * 1000);
            gclass36.method_4();
            while (!gclass36.method_3() && !unit.IsSkinnable)
            {
                StartupClass.Sleep(100);
                if (method_17())
                    return;
            }

            if (!unit.IsSkinnable)
                return;
            method_57(gclass5_0, unit, true);
        }
    }

    private bool method_54(GUnit gunit_0)
    {
        var gclass36 = new GameTimer(3000);
        gclass36.method_4();
        while (!gclass36.method_3())
        {
            if (gunit_0.IsLootable)
                return true;
            StartupClass.Sleep(60);
        }

        return false;
    }

    private bool method_55(GUnit gunit_0, bool IsAutoLoginPending, bool IsFocusTimerActive)
    {
        if (IsAutoLoginPending)
        {
            var gclass36 = new GameTimer(2000);
            gclass36.method_4();
            while (!gclass36.method_3() && !GPlayerSelf.Me.IsCasting)
            {
                if (method_17())
                    return false;
                StartupClass.Sleep(100);
            }

            if (gclass36.method_3())
            {
                Logger.LogMessage(MessageProvider.IsGroupProfile(804, gunit_0.GUID.ToString("x")));
                return false;
            }
        }

        var flag = false;
        var resumeTimer = new GameTimer(4500);
        var attachCooldownTimer = new GameTimer(1000);
        resumeTimer.method_4();
        attachCooldownTimer.method_4();
        while (!resumeTimer.method_3())
            if (gunit_0.IsValid)
            {
                if ((gunit_0.IsLootable || IsAutoLoginPending) && (gunit_0.IsSkinnable || !IsAutoLoginPending))
                {
                    if (method_17())
                        return false;
                    if (!IsFocusTimerActive || !attachCooldownTimer.method_3() || GContext.Main.Me.PetGUID == 0L ||
                        GContext.Main.Me.TargetGUID != GContext.Main.Me.PetGUID)
                    {
                        var num = GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("LootWindow"), "LootWindow");
                        if (num != 0 || !flag)
                        {
                            if (num != 0)
                            {
                                if (!flag)
                                    Logger.LoadProfile(MessageProvider.GetMessage(754));
                                flag = true;
                            }

                            if (flag)
                            {
                                DialogMonitor.LoadProfile();
                                if (DialogMonitor.bool_1)
                                {
                                    if (DialogMonitor.string_0.ToLower().IndexOf(MessageProvider.GetMessage(871)) != -1)
                                    {
                                        Logger.LogMessage("Bind-on-pickup dialog is visible, accepting");
                                        Thread.Sleep(600);
                                        UIElement.IsGroupProfile("StaticPopup1Button1").method_16(false);
                                        Thread.Sleep(600);
                                    }
                                    else
                                    {
                                        Logger.LogMessage("Unknown dialog visible during loot: \"" +
                                                           DialogMonitor.string_0 + "\", dismissing");
                                        DialogMonitor.IsGroupProfile();
                                    }
                                }
                            }

                            StartupClass.Sleep(50);
                        }
                        else
                        {
                            Logger.LoadProfile(MessageProvider.GetMessage(755));
                            break;
                        }
                    }
                    else
                    {
                        Logger.LogMessage("Clicked on pet trying to loot, oops!");
                        LoadProfile();
                        Thread.Sleep(500);
                        method_56();
                        return method_55(gunit_0, IsAutoLoginPending, false);
                    }
                }
                else
                {
                    break;
                }
            }
            else
            {
                Logger.LoadProfile(MessageProvider.GetMessage(756));
                break;
            }

        var str = GProcessMemoryManipulator.ReadString(MemoryOffsetTable.Instance.GetIntOffset("RedMessage"), 128, "RedMessage");
        Logger.LoadProfile("Red message after loot: [" + str + "]");
        if (str.ToLower().IndexOf(MessageProvider.GetMessage(230)) > -1)
        {
            if (ConfigManager.gclass61_0.method_5("StopWhenFull"))
            {
                Logger.LogMessage(MessageProvider.GetMessage(231));
                bool_2 = true;
                bool_3 = true;
                LoadProfile();
            }
            else if (ConfigManager.gclass61_0.method_5("StopLootingWhenFull"))
            {
                Logger.LogMessage(MessageProvider.GetMessage(792));
                bool_5 = true;
            }
            else
            {
                Logger.LogMessage(MessageProvider.GetMessage(857));
            }
        }

        return (!flag || !resumeTimer.method_3()) && gunit_0 != null &&
               (flag || (!gunit_0.IsLootable && !IsAutoLoginPending) || (!gunit_0.IsSkinnable && IsAutoLoginPending));
    }

    public void method_56()
    {
        if (ConfigManager.gclass61_0.method_5("ShiftLoot"))
        {
            InputController.smethod_0(16, true);
            StartupClass.Sleep(100);
        }

        InputController.AddWaypoint(true);
        if (!ConfigManager.gclass61_0.method_5("ShiftLoot"))
            return;
        InputController.smethod_0(16, false);
    }

    public void method_57(LootableCorpseTracker gclass5_0, GUnit gunit_0, bool IsAutoLoginPending)
    {
        Logger.LoadProfile("Starting DoLootCorpse on: 0x" + gclass5_0.playerGuid.ToString("x"));
        method_60();
        if (!gunit_0.Hover())
        {
            if (GContext.Main.Me.IsUnderAttack)
                return;
            Logger.LoadProfile("First pass failed, trying harder by approaching closer");
            if (!gunit_0.Approach(2.0, false))
            {
                if (!method_17())
                    gclass5_0.method_2();
                Logger.LogMessage(MessageProvider.GetMessage(212));
                return;
            }

            if (!gunit_0.Hover())
            {
                Logger.LogMessage(MessageProvider.IsGroupProfile(801, gunit_0.GUID.ToString("x")));
                if (method_17())
                    return;
                gclass5_0.method_2();
                return;
            }
        }

        method_56();
        if (!method_55(gunit_0, IsAutoLoginPending, true))
        {
            Logger.LogMessage(MessageProvider.IsGroupProfile(803, gunit_0.GUID.ToString("x")));
            if (IsAutoLoginPending)
            {
                if (killActionNestingCount < 4)
                {
                    ++killActionNestingCount;
                    Logger.LogMessage(MessageProvider.GetMessage(823));
                    method_57(gclass5_0, gunit_0, IsAutoLoginPending);
                    return;
                }

                Logger.LogMessage(MessageProvider.GetMessage(824));
            }

            if (method_17())
                return;
            gclass5_0.method_2();
        }
        else if (!IsAutoLoginPending)
        {
            Logger.LogMessage(MessageProvider.GetMessage(208));
            killActionNestingCount = 0;
            if (!SpellcastingManager.gclass42_0.method_15("Common.PostLoot"))
            {
                if (ConfigManager.gclass61_0.method_5("RunPostLoot"))
                    bool_7 = true;
                else
                    GContext.Main.CastSpell("Common.PostLoot");
            }

            ++StartupClass.expectedVersion;
            if (StartupClass.SomeIntegerValue > 0)
                --StartupClass.SomeIntegerValue;
            if (gplayerSelf_0.HasSkinning && ConfigManager.gclass61_0.method_5("AutoSkin"))
                return;
            gclass5_0.method_1();
        }
        else
        {
            gclass5_0.method_1();
        }
    }

    public void method_58()
    {
        SoundPlayer.smethod_0("GMWhisper.wav");
        if (!(ConfigManager.gclass61_0.method_2("TeleportStop") == "True"))
            return;
        GContext.Main.Movement.LookConfused();
        InputController.StartMainThread(27);
        StartupClass.StopGlide(true, "TeleportWarning");
    }

    private void method_59(GUnit gunit_0)
    {
        SoundPlayer.smethod_0("BadTag.wav");
        ++StartupClass.SomeIntegerValue;
        if (GContext.Main.Me.Pet != null)
            SpellcastingManager.gclass42_0.method_0("Common.PetFollow");
        if (StartupClass.SomeIntegerValue >= ConfigManager.gclass61_0.method_3("BadTagLimit"))
        {
            StartupClass.combatController.bool_2 = true;
            Logger.LogMessage(MessageProvider.GetMessage(808));
        }

        if (!gunit_0.IsTargetingMe && !ConfigManager.gclass61_0.method_5("IgnoreTags"))
        {
            Logger.LogMessage(MessageProvider.IsGroupProfile(805, gunit_0.Name));
            LoadProfile();
        }
        else
        {
            Logger.LogMessage(MessageProvider.IsGroupProfile(806, gunit_0.Name));
            StartupClass.CurrentGameClass.Disengage(gunit_0);
            var resumeTimer = new GameTimer(3000);
            var attachCooldownTimer = new GameTimer(1200);
            resumeTimer.method_4();
            attachCooldownTimer.method_4();
            LoadProfile();
            while (!resumeTimer.method_3())
                if (gunit_0.IsTargetingMe)
                {
                    StartupClass.Sleep(200);
                    if (attachCooldownTimer.method_3())
                    {
                        SpellcastingManager.gclass42_0.method_1("Common.Back");
                        Thread.Sleep(400);
                        SpellcastingManager.gclass42_0.method_2("Common.Back");
                        attachCooldownTimer.method_4();
                    }
                }
                else
                {
                    Logger.LogMessage(MessageProvider.IsGroupProfile(806, gunit_0.Name));
                    LoadProfile();
                    return;
                }

            Logger.LogMessage(MessageProvider.IsGroupProfile(807, gunit_0.Name));
        }
    }

    private void method_60()
    {
        if (!GContext.Main.MouseSpin || ggameCamera_0 == null || float_0 == 0.0 ||
            Math.Abs(ggameCamera_0.Pitch - float_0) <= Math.PI / 36.0)
            return;
        Logger.LoadProfile("Current camera: " + ggameCamera_0);
        Logger.LogMessage("Camera pitch is messed up, fixing");
        GContext.Main.ReleaseSpinRun();
        StartupClass.cameraRotator.method_16(ggameCamera_0, float_0);
    }

    private bool method_61()
    {
        if (ConfigManager.gclass61_0.method_5("VendorOnFoodWater") && GPlayerSelf.Me.PlayerClass != GPlayerClass.Mage)
        {
            var actionInventory1 = GContext.Main.Interface.GetActionInventory("Common.Eat");
            var actionInventory2 = GContext.Main.Interface.GetActionInventory("Common.Drink");
            if ((actionInventory1 == 0 && StartupClass.CurrentGameClass.ShouldBuyFood) ||
                (actionInventory2 == 0 && StartupClass.CurrentGameClass.ShouldBuyWater))
            {
                Logger.LogMessage("Food or water is zero, going back for more");
                return true;
            }
        }

        if (ConfigManager.gclass61_0.method_5("VendorOnDurability"))
        {
            var equippedItems = GObjectList.GetEquippedItems();
            var num = ConfigManager.gclass61_0.method_4("VendorDurabilityMin");
            var flag = false;
            foreach (var gitem in equippedItems)
                if (gitem.Durability < num)
                {
                    Logger.LogMessage("Item is in need of repair: \"" + gitem.Name + "\"");
                    flag = true;
                }

            if (flag)
                return true;
        }

        return false;
    }

    public void method_62()
    {
        try
        {
            method_63();
        }
        catch (ThreadInterruptedException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            Logger.LogMessage("** OnHearth inner exception: " + ex.Message + "\r\n" + ex.StackTrace);
            throw ex;
        }
    }

    private void method_63()
    {
        if (ActiveGProfile.IsVendorEnabled && bool_3 && StartupClass.IsSomeConditionMet)
        {
            StartupClass.IsFocusTimerActive = false;
            Logger.LogMessage("Starting up vendor stuff");
            Thread.Sleep(6000);
            if (ActiveGProfile.VendorWaypoints[0].GetDistanceTo(gplayerSelf_0.Location) > 20.0)
            {
                Logger.LogMessage("First vendor waypoint is too far away, skipping resume");
                StartupClass.IsFocusTimerActive = true;
            }
            else
            {
                method_64(null);
            }

            glocation_0 = null;
        }
        else
        {
            StartupClass.pendingAutoLoginName = null;
            StartupClass.IsLoginTimerActive = false;
            StartupClass.StopGlide(false, "HearthAndExit");
            throw new ThreadInterruptedException();
        }
    }

    private void method_64(GLocation glocation_3)
    {
        var flag1 = false;
        if (ConfigManager.gclass61_0.method_5("SendMail"))
            bool_17 = true;
        var flag2 = false;
        var num = 3;
        var flag3 = false;
        var Tolerance = Math.PI / 6.0;
        var vendorPath = ActiveGProfile.CreateVendorPath(glocation_3);
        if (!StartupClass.CurrentGameClass.ShouldBuyFood && !StartupClass.CurrentGameClass.ShouldBuyWater)
            flag1 = true;
        while (vendorPath.Count > 0)
        {
            if (!flag1)
            {
                var unit = GObjectList.FindUnit(ActiveGProfile.VendorFW, true);
                if (unit != null && unit.DistanceToSelf <= 10.0)
                {
                    flag2 = method_70(unit);
                    flag1 = true;
                }
            }

            if (!flag2)
            {
                var unit = GObjectList.FindUnit(ActiveGProfile.VendorAR, true);
                if (unit != null && unit.DistanceToSelf < 10.0)
                    flag2 = method_71(unit);
            }

            if (!flag2 && ActiveGProfile.VendorRepair != null)
            {
                var unit = GObjectList.FindUnit(ActiveGProfile.VendorRepair, true);
                if (unit != null && unit.DistanceToSelf < 10.0)
                    flag2 = method_72(unit);
            }

            bool_18 = true;
            int_14 = 0;
            while (IsProfileModified & bool_17 & bool_18)
                method_75();
            var Target = vendorPath.Dequeue();
            Logger.LoadProfile("$ Dequeued loc: " + Target);
            if (Math.Abs(GContext.Main.Movement.CompareHeadings(gplayerSelf_0.Heading,
                    gplayerSelf_0.Location.GetHeadingTo(Target))) > Tolerance && GContext.Main.IsRunning)
            {
                Logger.LogMessage("Lifting off to set heading exact");
                GContext.Main.ReleaseRun();
                double headingTo = gplayerSelf_0.Location.GetHeadingTo(Target);
                GContext.Main.Movement.SetHeading(headingTo, Tolerance);
            }

            if (flag1 && flag2 && !flag3)
            {
                --num;
                if (num == 0)
                {
                    Logger.LogMessage("Got food and repair, switching to faster running");
                    flag3 = true;
                    Tolerance = Math.PI / 3.0;
                }
            }

            if (GContext.Main.Movement.MoveToLocation(Target, flag3 ? 10.0 : 3.0, true))
            {
                gplayerSelf_0.Refresh();
                method_7();
            }
            else
            {
                Logger.LogMessage("Never able to reach next vendor wp, giving up");
                StartupClass.StopGlide(false, "VendorWPStuck");
                return;
            }
        }

        Logger.LogMessage("Vendor waypoints all done, resuming profile");
        bool_2 = false;
        bool_3 = false;
        ActiveGProfile.BeginProfile(GPlayerSelf.Me.Location);
    }

    private GMerchant method_65(GUnit gunit_0)
    {
        GContext.Main.ReleaseSpinRun();
        gunit_0.Approach();
        for (var index = 0; index < 3; ++index)
        {
            gunit_0.Interact();
            var gmerchant = new GMerchant();
            if (gmerchant.IsVisible)
                return gmerchant;
            Logger.LogMessage("Merchant window didn't open, trying again");
            Thread.Sleep(3000);
        }

        Logger.LogMessage("Too many failures, giving up");
        StartupClass.StopGlide(false, "VendorInteractFailed");
        return null;
    }

    private void method_66(GMerchant gmerchant_0, int int_15)
    {
        Logger.LogMessage("Loading up on ammo: \"" + GPlayerSelf.Me.AmmoName + "\"");
        for (var int_15_1 = GPlayerSelf.Me.AmmoCount; int_15_1 < int_15; int_15_1 = method_69(int_15_1))
            gmerchant_0.BuyOnAnyPage(GPlayerSelf.Me.AmmoName);
    }

    private void method_67(GMerchant gmerchant_0, string string_2, int int_15, string string_3)
    {
        var actionInventory = GContext.Main.Interface.GetActionInventory(string_3);
        var int_15_1 = actionInventory;
        if (actionInventory >= int_15)
            Logger.LogMessage("Already got enough, don't need to buy any");
        else
            for (; int_15_1 < int_15; int_15_1 = method_68(string_3, int_15_1))
                gmerchant_0.BuyOnAnyPage(string_2);
    }

    private int method_68(string string_2, int int_15)
    {
        var gspellTimer = new GSpellTimer(5000, false);
        while (!gspellTimer.IsReadySlow)
        {
            var actionInventory = GContext.Main.Interface.GetActionInventory(string_2);
            if (actionInventory > int_15)
                return actionInventory;
        }

        Logger.LogMessage("Inventory doesn't seem to be going up for " + string_2 + " when buying");
        StartupClass.StopGlide(false, "StockupFailed");
        return 0;
    }

    private int method_69(int int_15)
    {
        var gspellTimer = new GSpellTimer(5000, false);
        while (!gspellTimer.IsReadySlow)
        {
            var ammoCount = GPlayerSelf.Me.AmmoCount;
            if (ammoCount > int_15)
                return ammoCount;
        }

        Logger.LogMessage("Ammo count doesn't seem to be going up!");
        StartupClass.StopGlide(false, "StockupFailed");
        return 0;
    }

    private bool method_70(GUnit gunit_0)
    {
        Logger.LogMessage("Doing food/water with \"" + gunit_0.Name + "\"");
        var gmerchant_0 = method_65(gunit_0);
        var flag = false;
        if (gmerchant_0.IsRepairVisible && gmerchant_0.IsRepairEnabled)
        {
            flag = true;
            gmerchant_0.ClickRepairButton();
        }

        if (ConfigManager.gclass61_0.method_5("VendorJunk"))
        {
            GContext.Main.SendKey("Common.BackpackAll");
            method_74();
            Thread.Sleep(1000);
        }

        if (StartupClass.CurrentGameClass.ShouldBuyFood)
        {
            var string_2 = method_73("Common.Eat");
            Logger.LogMessage("Loading up on food: \"" + string_2 + "\"");
            method_67(gmerchant_0, string_2, ConfigManager.gclass61_0.method_3("FoodAmount"), "Common.Eat");
        }

        if (StartupClass.CurrentGameClass.ShouldBuyWater)
        {
            var string_2 = method_73("Common.Drink");
            Logger.LogMessage("Loading up on water: \"" + string_2 + "\"");
            method_67(gmerchant_0, string_2, ConfigManager.gclass61_0.method_3("WaterAmount"), "Common.Drink");
        }

        Thread.Sleep(671);
        LoadProfile();
        Thread.Sleep(671);
        LoadProfile();
        Thread.Sleep(671);
        return flag;
    }

    private bool method_71(GUnit gunit_0)
    {
        Logger.LogMessage("Doing ammo/repair with \"" + gunit_0.Name + "\"");
        var gmerchant_0 = method_65(gunit_0);
        var flag = false;
        if (gmerchant_0.IsRepairVisible && gmerchant_0.IsRepairEnabled)
        {
            flag = true;
            gmerchant_0.ClickRepairButton();
        }

        if (gmerchant_0.IsRepairVisible && !gmerchant_0.IsRepairEnabled)
            flag = true;
        if (GPlayerSelf.Me.PlayerClass == GPlayerClass.Hunter)
        {
            var int_15 = ConfigManager.gclass61_0.method_3("AmmoAmount");
            Logger.LogMessage("My current ammo: " + GPlayerSelf.Me.AmmoCount + ", required: " + int_15);
            if (GPlayerSelf.Me.AmmoCount < int_15)
                method_66(gmerchant_0, int_15);
        }

        if (ConfigManager.gclass61_0.method_5("VendorJunk"))
        {
            GContext.Main.SendKey("Common.BackpackAll");
            method_74();
            Thread.Sleep(1000);
        }

        Thread.Sleep(671);
        LoadProfile();
        Thread.Sleep(671);
        LoadProfile();
        Thread.Sleep(671);
        return flag;
    }

    private bool method_72(GUnit gunit_0)
    {
        Logger.LogMessage("Doing alt repair with \"" + gunit_0.Name + "\"");
        var gmerchant = method_65(gunit_0);
        if (gmerchant.IsRepairVisible && gmerchant.IsRepairEnabled)
            gmerchant.ClickRepairButton();
        Thread.Sleep(671);
        LoadProfile();
        Thread.Sleep(671);
        LoadProfile();
        Thread.Sleep(671);
        return true;
    }

    private string method_73(string string_2)
    {
        var byKeyName = GContext.Main.Interface.GetByKeyName(string_2);
        if (byKeyName == null)
        {
            Logger.LogMessage("Couldn't guess item name, no UI object for: " + string_2);
            StartupClass.StopGlide(false, "BadItemN");
            return null;
        }

        byKeyName.Hover();
        Thread.Sleep(888);
        var byName = GContext.Main.Interface.GetByName("GameTooltip");
        if (byName != null && byName.IsVisible)
            return byName.GetChildObject("GameTooltipTextLeft1").LabelText;
        Logger.LogMessage("Couldn't find tool tip or tooltip not visible after hovering over: " + string_2);
        StartupClass.StopGlide(false, "BadToolTip");
        return null;
    }

    private void method_74()
    {
        gbagItem_1 = GPlayerSelf.Me.GetBagCollection(GItemBagAction.Sell);
        if (gbagItem_1.Length > 0)
            foreach (var gbagItem in gbagItem_1)
            {
                Logger.LoadProfile("Sell: " + gbagItem.Item.Name);
                Thread.Sleep(500 + StartupClass.random_0.Next() % 1000);
                gbagItem.Click(true);
            }
        else
            Logger.LogMessage("No items to sell.");
    }

    private void method_75()
    {
        foreach (var node in GObjectList.GetNodes())
            if (node.IsMailBox && node.DistanceToSelf <= (double)attachPidOverride)
            {
                Logger.LogMessage("We are near a mailbox!");
                IsLoading = true;
                GContext.Main.ReleaseSpinRun();
                gbagItem_0 = GPlayerSelf.Me.GetBagCollection(GItemBagAction.Mail);
                int_14 = 0;
                Logger.LoadProfile("Bag Count: " + gbagItem_0.Length);
                if (gbagItem_0.Length >= 1)
                {
                    if (node.DistanceToSelf > GContext.Main.MeleeDistance)
                    {
                        Logger.LogMessage("Approaching Mailbox");
                        GContext.Main.ReleaseSpinRun();
                        GContext.Main.Movement.MoveToLocation(node.Location, GContext.Main.MeleeDistance, false);
                        if (node.DistanceToSelf > GContext.Main.MeleeDistance)
                        {
                            Logger.LoadProfile("cant get to the box...something blocking?");
                            IsProfileModified = false;
                            break;
                        }
                    }

                    GContext.Main.SendKey("Common.BackpackAll");
                    Thread.Sleep(1000);
                    node.Interact();
                    Thread.Sleep(1000);
                    GContext.Main.Interface.GetByName("MailFrameTab2").ClickMouse(false);
                    Thread.Sleep(200);
                    if (method_76())
                    {
                        Thread.Sleep(1000);
                        if (gbagItem_0.Length > 12)
                        {
                            IsProfileModified = true;
                            bool_18 = true;
                        }
                        else
                        {
                            IsProfileModified = false;
                        }
                    }
                    else
                    {
                        Logger.LogMessage("No items to mail.");
                        IsProfileModified = false;
                    }

                    Thread.Sleep(200);
                    GContext.Main.Interface.GetByName("InboxCloseButton").ClickMouse(false);
                }
                else
                {
                    Logger.LogMessage("We have no mail");
                    IsProfileModified = false;
                    break;
                }
            }

        if (IsLoading)
            return;
        bool_18 = false;
    }

    private bool method_76()
    {
        GContext.Main.Interface.GetByName("SendMailNameEditBox").ClickMouse(false);
        Thread.Sleep(200);
        var What1 = ConfigManager.gclass61_0.method_2("MailToText");
        GContext.Main.Interface.SendString(What1);
        Thread.Sleep(200);
        GContext.Main.Interface.GetByName("SendMailSubjectEditBox").ClickMouse(false);
        var What2 = ConfigManager.gclass61_0.method_2("SubjectText");
        GContext.Main.Interface.SendString(What2);
        Thread.Sleep(500);
        GContext.Main.SendKey("Common.Escape");
        foreach (var gbagItem in gbagItem_0)
            if (int_14 <= 11)
            {
                gbagItem.Click(true);
                Thread.Sleep(500);
                ++int_14;
            }
            else
            {
                Logger.LoadProfile("max item limit reached (12 items)");
                break;
            }

        if (int_14 > 0)
        {
            GContext.Main.Interface.GetByName("SendMailMailButton").ClickMouse(false);
            Thread.Sleep(500);
            GContext.Main.SendKey("Common.Escape");
            Thread.Sleep(500);
            GContext.Main.SendKey("Common.BackpackAll");
            Thread.Sleep(200);
            return true;
        }

        Logger.LogMessage("No mail to send.");
        Thread.Sleep(500);
        GContext.Main.SendKey("Common.BackpackAll");
        return false;
    }
}