// Decompiled with JetBrains decompiler
// Type: GClass73
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Glider.Common.Objects;

public class GClass73
{
    private const double double_0 = 15.0;
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
    public bool bool_13;
    protected bool bool_14;
    private bool bool_15;
    private bool bool_16 = true;
    private bool bool_17;
    private bool bool_18 = true;
    private bool bool_19;
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
    private GClass36 gclass36_0 = new GClass36(1000);
    private GClass36 gclass36_1;
    private GClass36 gclass36_2;
    private GClass36 gclass36_3;
    private GClass36 gclass36_4;
    private GClass36 gclass36_5;
    private GClass36 gclass36_6 = new GClass36(3500);
    private GClass36 gclass36_7;
    public GClass54 gclass54_0;
    private GGameCamera ggameCamera_0;
    public GLocation glocation_0;
    private GLocation glocation_1;
    private GLocation glocation_2;
    protected GPlayerSelf gplayerSelf_0;
    public GProfile gprofile_0;
    private int int_10;
    public int int_11;
    public int int_12;
    private int int_13;
    private int int_14;
    private int int_4;
    public int int_5;
    private int int_6;
    private int int_7;
    public int int_8;
    public int int_9;
    private long long_0;
    protected string string_0;
    public string[] string_1;
    public Thread thread_0;

    public GClass73()
    {
        try
        {
            method_0();
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("!! Exception in GlideThreadStartup: " + ex.Message + ex.StackTrace);
        }
    }

    private void method_0()
    {
        switch (GClass61.gclass61_0.method_2("AppKey")[0])
        {
            case 'C':
            case 'c':
                if (StartupClass.GliderManager != null) StartupClass.GliderManager.method_22();
                break;
        }

        if (StartupClass.CurrentGameClass != null && StartupClass.CurrentProfile != null)
        {
            if (!StartupClass.IsSomeConditionMet && GClass61.gclass61_0.method_5("BackgroundEnable"))
            {
                smethod_0(865);
            }
            else if (!StartupClass.IsSomeConditionMet && !StartupClass.CurrentProfile.bool_0)
            {
                smethod_0(866);
            }
            else
            {
                StartupClass.smethod_62();
                double_7 = GClass61.gclass61_0.method_4("WaypointCloseness");
                int_4 = GClass61.gclass61_0.method_3("StuckLimit");
                StartupClass.ProfileIdToProfileMap.Clear();
                StartupClass.smethod_39(200);
                StartupClass.ginterface0_0.imethod_0();
                gclass54_0 = GClass54.gclass54_0;
                if (GContext.Main.MouseSpin)
                {
                    ggameCamera_0 = new GGameCamera();
                    float_0 = ggameCamera_0.Pitch;
                }

                StartupClass.SomeIntegerValue = 0;
                gplayerSelf_0 = GPlayerSelf.Me;
                int_9 = gplayerSelf_0.Experience;
                if (GClass61.gclass61_0.method_5("ResetBuffs"))
                    StartupClass.CurrentGameClass.ResetBuffs();
                GClass42.gclass42_0.method_23();
                gprofile_0 = StartupClass.gclass48_0 == null
                    ? StartupClass.gprofile_0
                    : StartupClass.gclass48_0.method_6();
                bool_4 = true;
                thread_0 = null;
                thread_0 = new Thread(method_3);
                bool_8 = false;
                bool_12 = GClass61.gclass61_0.method_2("JumpMore") == "True";
                bool_13 = GClass61.gclass61_0.method_2("Strafe") == "True";
                if (gclass54_0.genum7_0 != GEnum7.const_0)
                    gclass54_0.method_1();
                int_5 = GClass61.gclass61_0.method_3("ExtraPull");
                if (GClass18.gclass18_0.method_5("ActionBarEnabled"))
                {
                    if (StartupClass.GliderManager != null)
                        StartupClass.GliderManager.method_28(0);
                    Environment.Exit(0);
                }

                if (GClass61.gclass61_0.method_2("AutoStop") == "True")
                {
                    bool_8 = true;
                    dateTime_0 = DateTime.Now.AddMinutes(int.Parse(GClass61.gclass61_0.method_2("AutoStopMinutes")));
                    GClass37.smethod_0(GClass30.smethod_2(149, dateTime_0.ToShortTimeString()));
                }

                if (StartupClass.bool_25 && DateTime.Now > StartupClass.dateTime_1)
                    return;
                int_8 = 0;
                StartupClass.dateTime_0 = DateTime.Now;
                GClass21.dateTime_1 = StartupClass.dateTime_0;
                int_10 = int.Parse(GClass61.gclass61_0.method_2("MaxResurrect"));
                int_11 = int.Parse(GClass61.gclass61_0.method_2("HarvestRange"));
                int_12 = int.Parse(GClass61.gclass61_0.method_2("MailBoxRange"));
                bool_11 = GClass61.gclass61_0.method_2("FastEat") == "True";
                method_27();
                var str = GClass61.gclass61_0.method_2("NoHarvest");
                if (str.Length > 0)
                    string_1 = str.Split(';');
                GClass5.smethod_6();
                bool_5 = false;
                bool_0 = true;
            }
        }
        else
        {
            GClass37.smethod_0("No CurrentClass (?!), can't start glide!");
        }
    }

    public static void smethod_0(int int_15)
    {
        if (StartupClass.MainForm != null)
        {
            StartupClass.MainForm.Focus();
            StartupClass.MainForm.Activate();
        }

        if (MessageBox.Show(StartupClass.MainForm, GClass30.smethod_1(int_15), GProcessMemoryManipulator.smethod_0(),
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
        StartupClass.smethod_17(1, GClass30.smethod_1(151));
        StartupClass.bool_28 = true;
        StartupClass.bool_32 = false;
        try
        {
            if (StartupClass.bool_19)
                return;
            method_4();
        }
        catch (ThreadInterruptedException ex)
        {
            GClass37.smethod_1("Catching ThreadInterrupted in GliderThread");
            if (!StartupClass.bool_28)
                return;
            if ((DateTime.Now - StartupClass.dateTime_0).TotalMinutes >= 2.0)
                GClass20.smethod_0("GlideStop.wav");
            GClass37.smethod_1("Considering relog, enabled: " + GClass61.gclass61_0.method_5("RelogEnabled") +
                               ", elite: " + StartupClass.IsSomeConditionMet + ", AutoLogNickname: " +
                               (StartupClass.string_9 == null ? "(null)" : (object)StartupClass.string_9) +
                               ", consider: " + StartupClass.bool_32);
            if (!GClass61.gclass61_0.method_5("RelogEnabled") || !StartupClass.IsSomeConditionMet ||
                StartupClass.string_9 == null || !StartupClass.bool_32)
                return;
            GClass37.smethod_0("Queuing up relog");
            StartupClass.gspellTimer_1 =
                new GSpellTimer((int)(StartupClass.random_0.NextDouble() * 8000.0) + 8000, false);
            StartupClass.bool_31 = true;
        }
        catch (Exception ex1)
        {
            if ((DateTime.Now - StartupClass.dateTime_0).TotalMinutes >= 2.0 && StartupClass.bool_28)
                GClass20.smethod_0("GlideStop.wav");
            GClass37.smethod_0(GClass30.smethod_2(668, ex1.Message, ex1.StackTrace));
            try
            {
                StartupClass.smethod_27(false, "GThreadException");
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
        GProcessMemoryManipulator.smethod_4();
        if (StartupClass.GliderManager != null)
            StartupClass.GliderManager.method_33(true);
        bool_2 = false;
        bool_3 = false;
        StartupClass.bool_21 = false;
        GClass37.smethod_0(GClass30.smethod_1(152));
        GClass37.smethod_1(GClass30.smethod_1(153));
        var location = gplayerSelf_0.Location;
        GClass21.smethod_0();
        gprofile_0.BeginProfile(GPlayerSelf.Me.Location);
        if (!GClass74.smethod_16(StartupClass.CurrentGameClass).bool_0 && !StartupClass.IsSomeConditionMet)
        {
            GClass37.smethod_0(GClass30.smethod_1(854));
            StartupClass.smethod_27(false, "CCStart");
        }
        else if (GClass74.smethod_16(StartupClass.CurrentGameClass).bool_1)
        {
            GClass37.smethod_1("Class has patrol override, skipping regular stuff (!!!)");
            StartupClass.CurrentGameClass.Patrol();
        }
        else
        {
            if (!gprofile_0.Fishing && gclass54_0.genum7_0 != GEnum7.const_2)
            {
                if (gplayerSelf_0.Location.GetDistanceTo(gprofile_0.CurrentWaypoint) >
                    GClass61.gclass61_0.method_4("MaxStartDistance") && !gplayerSelf_0.IsDead)
                {
                    var flag = false;
                    if (gprofile_0.IsVendorEnabled && StartupClass.IsSomeConditionMet)
                    {
                        var closestVendorWaypoint = gprofile_0.FindClosestVendorWaypoint(gplayerSelf_0.Location);
                        if (gplayerSelf_0.Location.GetDistanceTo(closestVendorWaypoint) <=
                            GClass61.gclass61_0.method_4("MaxStartDistance") && !gplayerSelf_0.IsDead)
                        {
                            flag = true;
                            GClass37.smethod_0("Closest waypoint is vendor, resuming from there");
                            glocation_1 = closestVendorWaypoint;
                        }
                    }

                    if (!flag)
                    {
                        GContext.Main.Movement.SetHeading(gprofile_0.CurrentWaypoint);
                        GClass37.smethod_0(GClass30.smethod_2(669,
                            Math.Round(gplayerSelf_0.Location.GetDistanceTo(gprofile_0.CurrentWaypoint), 0)));
                        StartupClass.smethod_27(false, "TooFarToStart");
                        return;
                    }
                }
            }
            else if (gprofile_0.LureMinutes > 0)
            {
                GClass37.smethod_1(GClass30.smethod_1(155));
                gclass36_2 = new GClass36(gprofile_0.LureMinutes * 60 * 1000);
                gclass36_2.method_4();
            }
            else
            {
                GClass37.smethod_1(GClass30.smethod_1(156));
            }

            if (StartupClass.IsGliderInitialized && StartupClass.MainForm != null &&
                GProcessMemoryManipulator.GetForegroundWindow() == StartupClass.MainApplicationHandle)
                StartupClass.MainForm.Activate();
            StartupClass.int_7 = 0;
            StartupClass.int_8 = 0;
            StartupClass.int_9 = 0;
            arrayList_0 = new ArrayList();
            StartupClass.IsGliderRunning = false;
            gclass36_3 = new GClass36(55000);
            gclass36_4 = new GClass36(270000);
            gclass36_5 = new GClass36(1740000);
            gclass36_3.method_4();
            gclass36_4.method_4();
            gclass36_5.method_4();
            if (gclass54_0.genum7_0 != GEnum7.const_0)
                gclass54_0.method_10();
            if (gplayerSelf_0.IsDead && !method_50())
                return;
            if (GClass61.gclass61_0.method_5("ResetBuffs"))
                StartupClass.CurrentGameClass.ResetBuffs();
            StartupClass.CurrentGameClass.OnStartGlide();
            GClass37.smethod_1("First clear target");
            smethod_1();
            GClass37.smethod_1("First rest");
            method_8();
            Thread.Sleep(600);
            GClass37.smethod_1("Second refresh, post-rest");
            if (gplayerSelf_0.Location.GetDistanceTo(location) > 0.5)
                GClass42.gclass42_0.method_0("Common.Back");
            if (StartupClass.gclass48_0 != null)
            {
                GClass37.smethod_1("Pass off to main loop");
                StartupClass.gclass48_0.method_18();
            }
            else if (gclass54_0.genum7_0 == GEnum7.const_2)
            {
                method_46();
            }
            else if (gprofile_0.NaturalRun && !gprofile_0.Fishing)
            {
                method_39();
            }
            else
            {
                if (gprofile_0.Fishing)
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
                    GClass67.smethod_2();
                    method_6();
                    if (!gprofile_0.Fishing)
                        method_10();
                    else
                        goto label_16;
                    label_2:
                    if (!GClass48.smethod_3())
                    {
                        if (!gprofile_0.Fishing)
                        {
                            ++int_7;
                            gprofile_0.ConsumeCurrentWaypoint();
                            method_9();
                        }

                        if (gplayerSelf_0.IsDead)
                        {
                            GClass37.smethod_0(GClass30.smethod_1(157));
                            if (!(GClass61.gclass61_0.method_2("Resurrect") != "True"))
                            {
                                if (gprofile_0.GhostWaypoints.Count != 0)
                                {
                                    if (StartupClass.int_9 < int_10)
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
                } while (int_7 != gprofile_0.Waypoints.Count * 2 || gprofile_0.Fishing ||
                         !(GClass61.gclass61_0.method_2("SitWhenBored") == "True") || gprofile_0.IgnoreAttackers);

                goto label_13;
                label_3:
                gprofile_0 = StartupClass.gprofile_0;
            } while (gprofile_0.IgnoreAttackers);

            GClass48.smethod_4();
        }

        label_13:
        GClass37.smethod_0(GClass30.smethod_1(161));
        --int_7;
        GClass42.gclass42_0.method_0("Common.Sit");
        var gclass36 = new GClass36(60000);
        gclass36.method_4();
        while (!gclass36.method_3())
        {
            StartupClass.smethod_39(2000);
            if (method_10() > 0)
                break;
        }

        goto label_17;
        label_18:
        GClass37.smethod_0(GClass30.smethod_1(158));
        method_13();
        StartupClass.smethod_27(false, "ResurrectConfigOff");
        return;
        label_19:
        GClass37.smethod_0(GClass30.smethod_1(159));
        method_13();
        StartupClass.smethod_27(false, "NoGhostWPs");
        return;
        label_20:
        GClass37.smethod_0(GClass30.smethod_1(160));
        method_13();
        StartupClass.smethod_27(false, "TooManyDeaths");
    }

    public void method_6()
    {
        if (bool_2)
            method_21(true);
        if (!bool_8 || !(dateTime_0 < DateTime.Now))
            return;
        GClass37.smethod_0(GClass30.smethod_1(162));
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
            gprofile_0.Waypoints.Clear();
        if (gplayerSelf_0.IsDead)
        {
            GClass37.smethod_1("Skipping rest, we're dead");
        }
        else
        {
            GClass67.smethod_2();
            method_7();
            var flag1 = StartupClass.CurrentGameClass.CheckPartyHeal(null);
            var flag2 = StartupClass.gclass54_0.genum7_0 != GEnum7.const_0 &&
                        StartupClass.CurrentGameClass.CheckPartyBuffs();
            StartupClass.CurrentGameClass.RunningAction();
            if (flag1 || flag2)
                method_8();
            if (!gprofile_0.IsVendorEnabled || !StartupClass.IsSomeConditionMet || !method_61())
                return;
            GContext.Main.HearthSoon(true);
        }
    }

    public void method_9()
    {
        method_19();
        if (!gprofile_0.Fishing)
        {
            GClass37.smethod_0(GClass30.smethod_2(166, gprofile_0.CurrentWaypoint));
            GContext.Main.Movement.SetHeading(gprofile_0.CurrentWaypoint);
        }

        do
        {
            GClass21.smethod_2();
            method_26();
            if (!GContext.Main.IsSpinning)
                goto label_17;
            label_2:
            if (!gplayerSelf_0.IsDead)
            {
                method_10();
                if (gplayerSelf_0.TargetGUID != 0L && gplayerSelf_0.Target != null && !gplayerSelf_0.Target.IsDead)
                    method_12(true);
                double distanceTo = gplayerSelf_0.Location.GetDistanceTo(gprofile_0.CurrentWaypoint);
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
                    GContext.Main.Movement.SetHeading(gprofile_0.CurrentWaypoint);
                    GClass42.gclass42_0.method_1("Common.Forward");
                    if (gclass36_1.method_3() && int_14 > 2000)
                    {
                        StartupClass.smethod_39(500);
                        GClass42.gclass42_0.method_0("Common.Jump");
                        method_27();
                        int_14 -= 500;
                    }

                    StartupClass.smethod_39(int_14);
                    GClass42.gclass42_0.method_2("Common.Forward");
                    continue;
                }

                break;
            }

            goto label_19;
            label_17:
            StartupClass.CurrentGameClass.RunningAction();
            goto label_2;
        } while (Math.Abs(gprofile_0.CurrentWaypoint.Bearing) <= 2.0);

        goto label_20;
        label_19:
        return;
        label_20:
        GClass37.smethod_0(GClass30.smethod_1(167));
        method_8();
    }

    public int method_10()
    {
        if (gprofile_0.IgnoreAttackers || gprofile_0.Fishing)
            return 0;
        var num = 0;
        GClass37.smethod_1(GClass30.smethod_1(168));
        GMonster nextProfileTarget;
        while (true)
        {
            GClass67.smethod_2();
            if (!gplayerSelf_0.IsDead)
            {
                nextProfileTarget = GObjectList.GetNextProfileTarget();
                if (nextProfileTarget != null)
                {
                    double distanceToSelf = nextProfileTarget.DistanceToSelf;
                    if (distanceToSelf <= StartupClass.CurrentGameClass.PullDistance + int_5)
                    {
                        if (distanceToSelf > StartupClass.CurrentGameClass.PullDistance)
                        {
                            GClass37.smethod_0(GClass30.smethod_1(172));
                            if (!nextProfileTarget.Approach(StartupClass.CurrentGameClass.PullDistance - 1, false))
                                goto label_13;
                        }

                        nextProfileTarget.Face();
                        if (nextProfileTarget.SetAsTarget(false))
                        {
                            if (gplayerSelf_0.TargetGUID == nextProfileTarget.GUID)
                            {
                                GClass37.smethod_1("Target.Location: " + nextProfileTarget.Location +
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

        GClass37.smethod_1(GClass30.smethod_1(169));
        return num;
        label_11:
        GClass37.smethod_1(GClass30.smethod_1(170));
        return num;
        label_12:
        GClass37.smethod_1(GClass30.smethod_1(171));
        return num;
        label_13:
        GClass37.smethod_0(GClass30.smethod_1(173));
        return num;
        label_14:
        GClass37.smethod_0(GClass30.smethod_1(174));
        StartupClass.gprofile_0.AddToBlacklist(nextProfileTarget.GUID);
        return num;
        label_15:
        GClass37.smethod_1(GClass30.smethod_2(670, nextProfileTarget.GUID.ToString("x"),
            gplayerSelf_0.TargetGUID.ToString("x")));
        StartupClass.gprofile_0.AddToBlacklist(nextProfileTarget.GUID);
        GClass37.smethod_0(GClass30.smethod_1(175));
        smethod_1();
        GClass37.smethod_1(GClass30.smethod_1(176));
        return num;
    }

    public void method_11(GPlayer gplayer_0, GLocation glocation_3)
    {
        StartupClass.CurrentGameClass.StartCombat();
        GContext.Main.Me.SetTargetName(gplayer_0.Name);
        gplayer_0.TouchHealthDrop();
        var num = (int)StartupClass.CurrentGameClass.KillTarget(gplayer_0, true);
    }

    public void method_12(bool bool_20)
    {
        var flag1 = false;
        var flag2 = true;
        GClass48.smethod_4();
        int_7 = 0;
        bool_1 = false;
        if (bool_20)
        {
            GContext.Main.ReleaseAllKeys();
            GClass37.smethod_0(GClass30.smethod_1(177));
        }
        else
        {
            StartupClass.smethod_39(300);
        }

        var unit = GObjectList.FindUnit(gplayerSelf_0.TargetGUID);
        if (unit == null)
        {
            GClass37.smethod_0("Could not find target: 0x" + gplayerSelf_0.TargetGUID.ToString("x") +
                               " in object list... ?!");
            smethod_1();
        }
        else if (unit.GUID == gplayerSelf_0.GUID)
        {
            GClass37.smethod_0(GClass30.smethod_1(178));
            smethod_1();
        }
        else if (bool_20 && !unit.IsTargetingMe && !unit.IsTargetingMyPet &&
                 !StartupClass.gclass54_0.method_13(unit.TargetGUID))
        {
            GClass37.smethod_0(GClass30.smethod_1(863));
            smethod_1();
        }
        else if (gclass54_0.method_13(gplayerSelf_0.TargetGUID))
        {
            GClass37.smethod_0(GClass30.smethod_1(179));
            smethod_1();
        }
        else
        {
            if (unit.IsPlayer)
            {
                GClass37.smethod_0(GClass30.smethod_2(180, unit.Name));
                if (long_0 != gplayerSelf_0.TargetGUID)
                {
                    long_0 = gplayerSelf_0.TargetGUID;
                    GClass20.smethod_0("PlayerAttack.wav");
                }

                if (GClass61.gclass61_0.method_5("FightPlayers"))
                {
                    GContext.Main.ReleaseSpinRun();
                    method_11((GPlayer)unit, gplayerSelf_0.Location);
                    smethod_1();
                    method_27();
                    return;
                }
            }

            if (unit.GUID == GContext.Main.Me.PetGUID)
            {
                GClass37.smethod_0(GClass30.smethod_1(182));
                smethod_1();
                StartupClass.smethod_39(1500);
                method_27();
            }
            else if (unit.IsDead)
            {
                GClass37.smethod_0(GClass30.smethod_1(183));
                smethod_1();
                StartupClass.smethod_39(1500);
                method_27();
            }
            else
            {
                GContext.Main.Me.LockCombatLocation();
                if (StartupClass.gclass79_0 != null)
                    StartupClass.gclass79_0.gunit_0 = unit;
                StartupClass.GameClass69Instance.method_9(unit.Name);
                if (!GClass42.gclass42_0.method_15("Common.PreCombat"))
                {
                    GContext.Main.ReleaseSpinRun();
                    GContext.Main.CastSpell("Common.PreCombat");
                }

                unit.TouchHealthDrop();
                StartupClass.CurrentGameClass.StartCombat();
                GContext.Main.Me.SetTargetName(unit.Name);
                var gcombatResult = StartupClass.CurrentGameClass.KillTarget(unit, bool_20);
                GClass37.smethod_1("Combat result: " + gcombatResult);
                method_27();
                switch (gcombatResult)
                {
                    case GCombatResult.Unknown:
                        throw new NotImplementedException(
                            "Custom class returned GCombatResult.Unknown - should never happen!");
                    case GCombatResult.Retry:
                        smethod_1();
                        flag2 = false;
                        break;
                    case GCombatResult.RunAway:
                        throw new NotImplementedException("can't run away yet, not implemented in main code");
                    case GCombatResult.Vanished:
                        smethod_1();
                        if (GClass61.gclass61_0.method_5("StopOnVanish"))
                        {
                            GClass20.smethod_0("GMWhisper.wav");
                            GContext.Main.Movement.LookConfused();
                            StartupClass.smethod_27(false, "TargetVanishedInCombat");
                        }

                        method_27();
                        return;
                    case GCombatResult.Success:
                        ++StartupClass.int_7;
                        break;
                    case GCombatResult.SuccessWithAdd:
                        ++StartupClass.int_7;
                        flag1 = true;
                        break;
                    case GCombatResult.Died:
                        return;
                    case GCombatResult.Bugged:
                        smethod_1();
                        Thread.Sleep(1000);
                        smethod_1();
                        gprofile_0.ForceBlacklist(unit.GUID);
                        return;
                    case GCombatResult.OtherPlayerTag:
                        method_59(unit);
                        return;
                }

                if (GPlayerSelf.Me.Target == unit && unit.Health == 1.0)
                {
                    GClass37.smethod_0("Still targeting full-health mob after combat, clearing it out");
                    smethod_1();
                }

                if (flag2)
                {
                    if (StartupClass.IsGliderInitialized && GClass61.gclass61_0.method_5("SoundKill"))
                        GClass20.smethod_0("Kill.wav");
                    GClass5.smethod_0(new GClass5(unit.GUID, true, unit.Location, true), unit.Name);
                    if (!flag1)
                        unit.WaitForLootable();
                }

                if (!flag1)
                {
                    GClass67.smethod_2();
                    GClass37.smethod_0(GClass30.smethod_1(184));
                    method_26();
                    StartupClass.CurrentGameClass.RunningAction();
                    if (!GClass42.gclass42_0.method_15("Common.PostCombat"))
                        GContext.Main.CastSpell("Common.PostCombat");
                }
                else
                {
                    GClass37.smethod_0("Combat done with add, dealing with extra monster");
                    method_12(false);
                }

                var flag3 = true;
                if (gclass54_0.genum7_0 != GEnum7.const_0)
                {
                    flag3 = false;
                    if (gclass54_0.int_0 != 0)
                    {
                        var num = (int)Math.Abs(unit.GUID % gclass54_0.int_0);
                        GClass37.smethod_1(GClass30.smethod_2(671, num, gclass54_0.int_1));
                        if (num == gclass54_0.int_1)
                        {
                            GClass37.smethod_0(GClass30.smethod_1(185));
                            flag3 = true;
                        }
                    }
                }

                if (!flag3 && gclass54_0.genum7_0 == GEnum7.const_1)
                {
                    GClass37.smethod_1(GClass30.smethod_1(186));
                    StartupClass.smethod_39(gclass54_0.int_3 * 1000);
                }

                if (method_19())
                {
                    if (StartupClass.gclass79_0 == null)
                        return;
                    StartupClass.gclass79_0.gunit_0 = null;
                }
                else
                {
                    method_60();
                    if ((DateTime.Now - StartupClass.dateTime_0).TotalMinutes >= 20.0 &&
                        GClass18.gclass18_0.method_5("ArmorAlt2") &&
                        !char.IsDigit(GClass61.gclass61_0.method_2("AppKey")[0]))
                    {
                        if (StartupClass.GliderManager != null)
                            StartupClass.GliderManager.method_28(0);
                        Environment.Exit(0);
                    }

                    if (gplayerSelf_0.Experience != int_9)
                        lock (this)
                        {
                            if (gplayerSelf_0.Experience > int_9)
                            {
                                GClass37.smethod_0(GClass30.smethod_2(187, gplayerSelf_0.Experience - int_9));
                                int_8 += gplayerSelf_0.Experience - int_9;
                                bool_9 = true;
                            }

                            int_9 = gplayerSelf_0.Experience;
                        }

                    if (StartupClass.gclass79_0 != null)
                        StartupClass.gclass79_0.gunit_0 = null;
                    if (gplayerSelf_0.Health > 0.35 && flag2)
                    {
                        var gclass36 = new GClass36(3000);
                        gclass36.method_4();
                        while (!gclass36.method_3() && !unit.IsLootable)
                            StartupClass.smethod_39(50);
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
        StartupClass.smethod_39(2000);
        var gclass8 = GClass8.smethod_2("StaticPopup1Button1");
        if (gclass8 == null)
            StartupClass.smethod_27(false, "NoReleaseButtonVisible");
        gclass8.method_16(false);
    }

    public void method_14(GLocation glocation_3, bool bool_20)
    {
        if (bool_20)
        {
            GClass37.smethod_0(GClass30.smethod_1(188));
            ++StartupClass.int_9;
            method_13();
            if (!method_49())
                StartupClass.smethod_27(false, "NoTeleportAfterRelease");
        }

        Thread.Sleep(3000);
        var queue_0 = GContext.Main.MoveHelper != null
            ? GContext.Main.MoveHelper.CreateGhostwalkPath(glocation_3)
            : gprofile_0.CreateGhostwalkPath(glocation_3);
        GClass37.smethod_0(GClass30.smethod_1(189));
        gplayerSelf_0.Refresh();
        var num = GClass61.gclass61_0.method_4("CorpseShortCircuit");
        if (gplayerSelf_0.GetDistanceTo(glocation_3) < num)
            GClass37.smethod_0(GClass30.smethod_1(879));
        else
            method_15(queue_0, glocation_3);
        gplayerSelf_0.Refresh();
        GClass37.smethod_0(GClass30.smethod_1(193));
        method_16(glocation_3);
        var gspellTimer = new GSpellTimer(10000, false);
        do
        {
            ;
        } while (!gspellTimer.IsReadySlow && gplayerSelf_0.IsDead);

        if (gplayerSelf_0.IsDead)
        {
            GClass37.smethod_0("Still dead - try again");
            method_16(glocation_3);
        }

        if (gspellTimer.IsReady)
            StartupClass.smethod_27(false, "NoHealthAfterAccept");
        StartupClass.CurrentGameClass.OnResurrect();
        method_8();
        GClass55.smethod_21(false);
        GClass48.smethod_0();
        int_6 = Environment.TickCount;
    }

    private void method_15(Queue<GLocation> queue_0, GLocation glocation_3)
    {
        GClass37.smethod_1("# Walking GhostWalkPath, queue contains: " + queue_0.Count + " items");
        var num = GClass61.gclass61_0.method_4("CorpseShortCircuit");
        while (queue_0.Count > 0)
        {
            var glocation = queue_0.Dequeue();
            GClass37.smethod_1("# Dequeued loc: " + glocation);
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
                GClass37.smethod_1("# Corpse is close stopping pathing, gwp's left: " + queue_0.Count);
                break;
            }
        }

        GContext.Main.ReleaseSpinRun();
        GClass37.smethod_1("# Done with GhostWalkPath");
    }

    private void method_16(GLocation glocation_3)
    {
        var num1 = 4;
        GClass37.smethod_0(GClass30.smethod_1(196));
        var gclass8_1 = GClass8.smethod_2("StaticPopup1");
        GContext.Main.Movement.MoveToLocation(glocation_3, 6.0, false);
        if (Environment.TickCount - int_6 < 600000 && int_6 != 0)
        {
            GClass37.smethod_0(GClass30.smethod_1(197));
            for (var index = 0; index < 5; ++index)
            {
                GClass42.gclass42_0.method_0("Common.RotateLeft");
                StartupClass.smethod_39(60000);
            }
        }

        GClass37.smethod_0(GClass30.smethod_1(198));
        while (true)
        {
            GContext.Main.ReleaseSpinRun();
            var nearestHostile1 = GObjectList.GetNearestHostile();
            if (nearestHostile1 != null &&
                (nearestHostile1.DistanceToSelf < 25.0 || nearestHostile1.DistanceToSelf > 30.0))
            {
                GClass37.smethod_0(GClass30.smethod_1(200));
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
                        StartupClass.smethod_39(500);
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
                    GClass37.smethod_0(GClass30.smethod_2(201, nearestHostile2.DistanceToSelf.ToString()));
                    --num1;
                    if (num1 > 0 || !gclass8_1.method_10())
                    {
                        GContext.Main.Movement.MoveToLocation(glocation_3, 6.0, true);
                        StartupClass.smethod_39(6000);
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

        GClass37.smethod_0(GClass30.smethod_1(199));
        goto label_15;
        label_14:
        GClass37.smethod_0(GClass30.smethod_1(199));
        label_15:
        var gclass8_2 = GClass8.smethod_2("StaticPopup1Button1");
        if (gclass8_2 == null)
            StartupClass.smethod_27(false, "NoAcceptButtonOnRes");
        gclass8_2.method_16(false);
    }

    public bool method_17()
    {
        if (!GContext.Main.Me.IsUnderAttack)
            return false;
        GClass37.smethod_1("- GotAttacker returning true:");
        foreach (object attacker in GObjectList.GetAttackers())
            GClass37.smethod_1("- " + attacker);
        return true;
    }

    public bool method_18(int int_15)
    {
        if ((int_15 == 13 && gplayerSelf_0.HasHerbalism) || (int_15 == 11 && gplayerSelf_0.HasMining) || int_15 == 5)
            return true;
        GClass37.smethod_0("Cannot Harvest Item:" + int_15);
        return false;
    }

    public bool method_19()
    {
        if (gprofile_0.IgnoreAttackers)
            return false;
        gplayerSelf_0.Refresh();
        var gunit_0 = GObjectList.GetNearestAttacker(0L);
        if (gunit_0 == null)
        {
            gunit_0 = gclass54_0.method_3();
            if (gunit_0 == null)
            {
                if (gplayerSelf_0.TargetGUID == 0L || gplayerSelf_0.Target == null || gplayerSelf_0.Target.IsDead)
                    return false;
                method_12(true);
                return true;
            }
        }

        return gclass54_0.method_7(gunit_0);
    }

    public static void smethod_1()
    {
        if (GPlayerSelf.Me.TargetGUID == 0L)
            return;
        GClass37.smethod_1("Sending Esc to clear target");
        GClass55.smethod_9(27);
    }

    public void method_20()
    {
        var gclass8 = GClass8.smethod_2("StaticPopup1Button1");
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
                GClass55.smethod_18(0.505, double_3);
                StartupClass.smethod_39(300);
                GClass55.smethod_23(false);
                StartupClass.smethod_39(500);
            }

            GClass37.smethod_0(GClass30.smethod_2(203, double_3));
        }
    }

    public void method_21(bool bool_20)
    {
        var num = 1;
        GContext.Main.ReleaseSpinRun();
        StartupClass.bool_21 = true;
        StartupClass.CurrentGameClass.LeaveForm();
        while (true)
        {
            do
            {
                smethod_1();
                StartupClass.gclass36_1.method_4();
                GContext.Main.CastSpell("Common.Hearth");
                if (gplayerSelf_0.TargetGUID != 0L)
                {
                    if (gplayerSelf_0.IsUnderAttack)
                    {
                        GClass37.smethod_0(GClass30.smethod_1(204));
                        GContext.Main.SendKey("Common.Back");
                        StartupClass.gclass73_0.method_12(true);
                    }

                    ++num;
                }
                else
                {
                    goto label_6;
                }
            } while (num <= 3);

            GClass37.smethod_0(GClass30.smethod_1(745));
            StartupClass.smethod_27(false, "HearthFutility");
        }

        label_6:
        if (!bool_20)
        {
            StartupClass.string_9 = null;
            StartupClass.bool_31 = false;
            StartupClass.smethod_27(false, "HearthAndExit");
            throw new ThreadInterruptedException();
        }

        StartupClass.GameMemoryWriter.method_2("OnHearth", false);
    }

    public void method_22()
    {
        GContext.Main.ReleaseSpinRun();
        StartupClass.bool_21 = true;
        StartupClass.gclass36_1.method_5();
        StartupClass.CurrentGameClass.LeaveForm();
        StartupClass.smethod_27(true, "StopAndExit");
        throw new ThreadInterruptedException();
    }

    public void method_23(string string_2, bool bool_20)
    {
        lock (this)
        {
            string_0 = string_2;
            bool_14 = bool_20;
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
                if (bool_14)
                {
                    GClass37.smethod_0("Sending queued keys");
                    StartupClass.smethod_20(string_0);
                }
                else
                {
                    if (!GClass55.bool_0)
                    {
                        GClass55.smethod_9(13);
                        StartupClass.smethod_39(900);
                    }

                    GClass37.smethod_0("Sending queued chat message");
                    GClass55.smethod_28(string_0);
                }

                GClass37.smethod_0("Queued keys sent.");
                string_0 = null;
            }
        }

        var fileInfo = new FileInfo("chat.txt");
        if (!fileInfo.Exists)
            return;
        if (GClass61.gclass61_0.method_5("HandleChatTxt"))
        {
            GClass37.smethod_0(GClass30.smethod_1(205));
            var streamReader = new StreamReader(fileInfo.FullName);
            while (true)
            {
                var string_1 = streamReader.ReadLine();
                if (string_1 != null && string_1.Length >= 2)
                    GClass55.smethod_28(string_1);
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
                GClass37.smethod_0(GClass30.smethod_2(675, ex.Message));
                bool_4 = false;
            }
        }
        else
        {
            if (bool_19)
                return;
            bool_19 = true;
            GClass37.smethod_0("Chat.txt is present, but HandleChatTxt is not true in config");
        }
    }

    public void method_27()
    {
        gclass36_1 = method_28();
    }

    public GClass36 method_28()
    {
        var gclass36 =
            new GClass36((!bool_12 ? 40 + StartupClass.random_0.Next() % 160 : 10 + StartupClass.random_0.Next() % 30) *
                         1000);
        gclass36.method_4();
        return gclass36;
    }

    public void method_29()
    {
        var num = StartupClass.random_0.Next() % 15 + 5;
        var gclass36 = new GClass36(60000);
        gclass36.method_4();
        while (!gclass36.method_3())
        {
            method_36(gclass36_3, "Common.Time1");
            method_36(gclass36_4, "Common.Time5");
            method_36(gclass36_5, "Common.Time30");
            method_32();
            if (bool_8 && dateTime_0 < DateTime.Now)
            {
                GClass37.smethod_0(GClass30.smethod_1(162));
                method_21(true);
            }

            if (GClass48.smethod_3())
            {
                gprofile_0 = StartupClass.gprofile_0;
                if (!gprofile_0.IgnoreAttackers)
                    GClass48.smethod_4();
            }

            if (gclass36_2 != null && gclass36_2.method_3())
            {
                GClass37.smethod_0(GClass30.smethod_1(233));
                if (GContext.Main.Interface.GetActionInventory("Common.LureSlot") == 0)
                {
                    GClass37.smethod_0(GClass30.smethod_1(234));
                    gclass36_2 = null;
                }
                else
                {
                    GClass42.gclass42_0.method_0("Common.LureSlot");
                    var gclass8_1 = GClass8.smethod_2("CharacterFrame");
                    var gclass8_2 = GClass8.smethod_2("CharacterMainHandSlot");
                    if (gclass8_1 != null && gclass8_2 != null)
                    {
                        if (!gclass8_1.method_10())
                        {
                            GContext.Main.SendKey("Common.Character");
                            Thread.Sleep(1000);
                            if (!gclass8_1.method_10())
                            {
                                GClass37.smethod_1("CharFrame never became visible after keystroke!");
                                break;
                            }
                        }

                        StartupClass.smethod_39(500);
                        gclass8_2.method_16(false);
                        GContext.Main.SendKey("Common.Character");
                        StartupClass.smethod_39(5000);
                        gclass36_2.method_4();
                    }
                    else
                    {
                        GClass37.smethod_1("Couldn't get CharacterFrame or CharacterMainHandSlot");
                        break;
                    }
                }
            }
        }
    }

    public bool method_30(long long_1, double double_8, double double_9, double double_10)
    {
        var int_29 = GClass18.gclass18_0.method_4("UnderCursor");
        var flag = false;
        GClass37.smethod_1("Position on bobber: " + double_8 + " -> " + double_9 + ", inc = " + double_10);
        for (var double_3 = 0.08; double_3 < 0.6 && !flag; double_3 += double_10)
        for (var double_2 = double_8; double_2 < double_9; double_2 += double_10)
        {
            GClass55.smethod_18(double_2, double_3);
            GProcessMemoryManipulator.Sleep(20U);
            if (GProcessMemoryManipulator.smethod_12(int_29, "UnderCursor3") != long_1)
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
            StartupClass.smethod_39(500);
            flag = GProcessMemoryManipulator.smethod_12(int_29, "UnderCursor4") == long_1;
        }

        GClass37.smethod_1(GClass30.smethod_2(235, flag));
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
        GClass37.smethod_1(GClass30.smethod_1(239));
        GObject gobject = null;
        var num = 20;
        method_26();
        GClass21.smethod_2();
        StartupClass.smethod_39(1000);
        GClass37.smethod_1(GClass30.smethod_1(240));
        GClass42.gclass42_0.sortedList_0["Common.Fish"].FilloutKey();
        GClass42.gclass42_0.method_0("Common.Fish");
        StartupClass.smethod_39(1000);
        GClass37.smethod_1(GClass30.smethod_1(241));
        for (; num > 0; --num)
        {
            gobject = method_31();
            if (gobject == null)
                StartupClass.smethod_39(300);
            else
                break;
        }

        if (gobject == null)
        {
            GClass37.smethod_0(GClass30.smethod_1(243));
            method_33();
        }
        else
        {
            GClass37.smethod_1(GClass30.smethod_1(244));
            var gclass36 = new GClass36(32000);
            gclass36.method_4();
            StartupClass.smethod_39(1000);
            if (!gobject.Hover())
            {
                GClass37.smethod_0(GClass30.smethod_1(245));
                method_33();
            }
            else
            {
                while (!gclass36.method_3() && !gobject.IsBobbing)
                {
                    StartupClass.smethod_39(200);
                    method_26();
                    gplayerSelf_0.Refresh();
                    if (gplayerSelf_0.TargetGUID != 0L)
                    {
                        GClass37.smethod_0(GClass30.smethod_1(246));
                        method_12(true);
                    }
                }

                if (gclass36.method_3())
                {
                    GClass37.smethod_0(GClass30.smethod_1(247));
                    method_33();
                }
                else
                {
                    method_34(200, 600);
                    if (!gobject.IsCursorOnObject)
                    {
                        GClass37.smethod_0(GClass30.smethod_1(248));
                        method_33();
                    }
                    else
                    {
                        GClass37.smethod_0(GClass30.smethod_1(249));
                        ++StartupClass.int_8;
                        method_56();
                        method_34(2000, 5000);
                    }
                }
            }
        }
    }

    public void method_33()
    {
        StartupClass.smethod_39(1000);
    }

    public void method_34(int int_15, int int_16)
    {
        StartupClass.smethod_39(StartupClass.random_0.Next() % (int_16 - int_15) + int_15);
    }

    public void method_35()
    {
        gclass36_7.method_4();
        glocation_2 = null;
    }

    private void method_36(GClass36 gclass36_8, string string_2)
    {
        if (GClass42.gclass42_0.method_15(string_2) || !gclass36_8.method_3())
            return;
        GContext.Main.ReleaseSpinRun();
        GContext.Main.CastSpell(string_2);
        gclass36_8.method_4();
    }

    private bool method_37()
    {
        return (GClass61.gclass61_0.method_5("AvoidSameFaction") && GClass21.smethod_3()) ||
               (GClass61.gclass61_0.method_5("AvoidOtherFaction") && GClass21.smethod_4());
    }

    private void method_38()
    {
        GContext.Main.ReleaseSpinRun();
        while (method_37())
        {
            if (StartupClass.CurrentGameClass.CanStealth && !GContext.Main.Me.IsStealth)
                StartupClass.CurrentGameClass.EnterStealth(true);
            method_6();
            StartupClass.smethod_39(1000);
            gplayerSelf_0.Refresh();
            GClass21.smethod_2();
            if (gplayerSelf_0.TargetGUID != 0L && gplayerSelf_0.Target != null && !gplayerSelf_0.Target.IsDead)
            {
                GClass37.smethod_0(GClass30.smethod_1(254));
                method_12(true);
            }
        }

        StartupClass.smethod_39(1000);
    }

    public void method_39()
    {
        glocation_0 = null;
        GMonster gmonster = null;
        gclass36_7 = new GClass36(1300);
        method_35();
        var gclass36_1 = new GClass36(250);
        var gclass36_2 = new GClass36(2200);
        var flag1 = false;
        var gclass36_3 = new GClass36(12000);
        var num1 = 0;
        var flag2 = false;
        var num2 = 25;
        var num3 = 125;
        var gspellTimer = new GSpellTimer(2000, false);
        if (GContext.Main.MouseSpin)
            num2 = 15;
        gprofile_0.OneShotHit = false;
        gprofile_0.OneShotStepCheck = 0;
        if (gprofile_0.IgnoreAttackers)
            GClass48.smethod_6();
        gclass36_1.method_5();
        if (glocation_1 != null)
            method_64(glocation_1);
        while (true)
        {
            if (gplayerSelf_0.TargetGUID != 0L && gplayerSelf_0.Target.IsDead)
                goto label_91;
            label_6:
            GContext.Main.PulseSpin(!GContext.Main.IsRunning);
            if (GContext.Main.Overspin)
            {
                GContext.Main.ReleaseSpinRun();
                GClass37.smethod_0("Spinning for too long, letting go of key and re-syncing");
                Thread.Sleep(5000);
                gmonster = null;
                gprofile_0.BeginProfile(GPlayerSelf.Me.Location);
            }

            if (gclass36_1.method_3())
            {
                gclass36_1.method_4();
                if (method_37())
                {
                    GClass37.smethod_0(GClass30.smethod_1(byte.MaxValue));
                    method_38();
                }

                if (!GContext.Main.IsSpinning)
                {
                    GClass67.smethod_2();
                    method_26();
                    GClass21.smethod_2();
                    StartupClass.CurrentGameClass.RunningAction();
                    if (StartupClass.CurrentGameClass.ShouldRest())
                    {
                        GContext.Main.ReleaseSpinRun();
                        method_8();
                    }

                    method_36(this.gclass36_3, "Common.Time1");
                    method_36(gclass36_4, "Common.Time5");
                    method_36(gclass36_5, "Common.Time30");
                }

                if (gclass54_0.genum7_0 == GEnum7.const_1)
                    gclass54_0.method_6();
                if (gprofile_0.RunFromAvoids)
                    method_40();
                if (!GClass48.smethod_3())
                {
                    if (gplayerSelf_0.IsDead)
                    {
                        GClass37.smethod_1("# IsDead = true in main loop");
                        gclass36_7.method_4();
                        GContext.Main.ReleaseSpinRun();
                        GClass37.smethod_0(GClass30.smethod_1(157));
                        glocation_0 = null;
                        if (!(GClass61.gclass61_0.method_2("Resurrect") != "True"))
                        {
                            if (gprofile_0.GhostWaypoints.Count != 0)
                            {
                                if (StartupClass.int_9 < int_10)
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
                            GClass37.smethod_1("## New target = \"" + nextProfileTarget.Name + "\", GUID = 0x" +
                                               nextProfileTarget.GUID.ToString("x16"));
                            GClass37.smethod_1("## Profile.Wander = " + gprofile_0.Wander + ", Profile distance = " +
                                               gprofile_0.GetDistanceTo(nextProfileTarget.Location));
                            if (gmonster != null)
                                GClass37.smethod_1(GClass30.smethod_1(258));
                            gmonster = nextProfileTarget;
                            if (gmonster.DistanceToSelf > (double)StartupClass.CurrentGameClass.PullDistance)
                                gprofile_0.PlaceBreadcrumb();
                        }

                        if (nextProfileTarget.DistanceToSelf < StartupClass.CurrentGameClass.PullDistance + 15.0 &&
                            nextProfileTarget.DistanceToSelf > (double)StartupClass.CurrentGameClass.PullDistance &&
                            gclass36_2.method_3())
                        {
                            gclass36_2.method_4();
                            StartupClass.CurrentGameClass.ApproachingTarget(nextProfileTarget);
                        }

                        if (nextProfileTarget.DistanceToSelf <= (double)StartupClass.CurrentGameClass.PullDistance)
                        {
                            var heading = gplayerSelf_0.Heading;
                            GClass37.smethod_1(GClass30.smethod_2(683, nextProfileTarget.Name,
                                nextProfileTarget.GUID.ToString("x")));
                            if (GContext.Main.Movement.CompareHeadings(gplayerSelf_0.Heading,
                                    gplayerSelf_0.Location.GetHeadingTo(nextProfileTarget.Location)) > Math.PI / 6.0)
                                GContext.Main.ReleaseRun();
                            GContext.Main.ReleaseSpin();
                            GClass37.smethod_1("Facing enemy");
                            nextProfileTarget.Face();
                            gclass36_7.method_4();
                            if (!nextProfileTarget.SetAsTarget(false))
                            {
                                smethod_1();
                                GClass37.smethod_1(GClass30.smethod_1(259));
                                StartupClass.gprofile_0.AddToBlacklist(nextProfileTarget.GUID);
                                continue;
                            }

                            StartupClass.CurrentGameClass.TargetAcquired(nextProfileTarget);
                            gmonster = null;
                            method_12(false);
                            glocation_0 = gplayerSelf_0.Location;
                            num1 = 0;
                            gprofile_0.ConsiderWaypointSkip();
                            continue;
                        }
                    }
                    else
                    {
                        if (gmonster != null)
                            GClass37.smethod_1("### Old target no longer valid (\"" + gmonster.Name +
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
                                if (GClass61.gclass61_0.method_5("StrafeObstacles") && !flag1)
                                {
                                    GClass37.smethod_0(GClass30.smethod_1(742));
                                    var string_1 = "Common.StrafeLeft";
                                    if (StartupClass.random_0.Next() % 2 == 0)
                                        string_1 = "Common.StrafeRight";
                                    GClass42.gclass42_0.method_1(string_1);
                                    StartupClass.smethod_39(1200);
                                    GClass42.gclass42_0.method_2(string_1);
                                    flag1 = true;
                                }
                                else
                                {
                                    GClass37.smethod_0(GClass30.smethod_1(256));
                                    GContext.Main.ReleaseSpinRun();
                                    StartupClass.smethod_39(600);
                                    GContext.Main.PressKey("Common.Back");
                                    StartupClass.smethod_39(2000);
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
                            if (num1 > int_4)
                            {
                                GClass37.smethod_0("Stuck too many times");
                                if (gmonster != null)
                                {
                                    GClass37.smethod_0("Blacklisting this target");
                                    gprofile_0.ForceBlacklist(gmonster.GUID);
                                    gmonster = null;
                                    num1 = 0;
                                }
                                else
                                {
                                    if (flag2)
                                        StartupClass.smethod_27(false, "StuckTooMuchOnWP");
                                    GClass37.smethod_0("Stuck too much, trying previous waypoint");
                                    flag2 = true;
                                    gprofile_0.SetPreviousWaypoint();
                                }
                            }
                        }

                        gclass36_7.method_4();
                    }

                    if (GContext.Main.MoveHelper != null)
                        GContext.Main.MoveHelper.PatrolTowards(gmonster == null
                            ? gprofile_0.CurrentWaypoint
                            : (object)gmonster);
                    else
                        GContext.Main.Movement.BasePatrolTowards(gmonster == null
                            ? gprofile_0.CurrentWaypoint
                            : (object)gmonster);
                    double distanceTo1 = gplayerSelf_0.Location.GetDistanceTo(gprofile_0.CurrentWaypoint);
                    var double7 = double_7;
                    if (GClass48.smethod_5())
                        double7 *= 2.0;
                    if (distanceTo1 < double7 && gmonster == null)
                    {
                        flag2 = false;
                        GClass37.smethod_1("## Reached current waypoint, it is: " + gprofile_0.DebugCurrentWaypoint());
                        flag1 = false;
                        ++gprofile_0.OneShotStepCheck;
                        gprofile_0.ConsumeCurrentWaypoint();
                        num1 = 0;
                        if (gprofile_0.OneShotHit && StartupClass.gclass48_0 == null)
                        {
                            GClass37.smethod_0(GClass30.smethod_1(257));
                            GContext.Main.ReleaseSpinRun();
                            StartupClass.smethod_27(false, "EndOfOneShotProfile");
                        }

                        GContext.Main.ReleaseSpin();
                        ++int_7;
                        method_35();
                    }

                    if (distanceTo1 > 15.0 && !GContext.Main.IsSpinning && this.gclass36_1.method_3())
                    {
                        GClass42.gclass42_0.method_0("Common.Jump");
                        StartupClass.smethod_39(1800);
                        method_27();
                        if (bool_12 && StartupClass.random_0.Next() % 10 == 0)
                            this.gclass36_1.method_5();
                    }
                    else if (distanceTo1 > 20.0 && bool_13 && gclass36_0.method_3() && GContext.Main.IsRunning)
                    {
                        if (StartupClass.random_0.Next() % 10 == 0)
                        {
                            var string_1 = StartupClass.random_0.Next() % 2 == 0
                                ? "Common.StrafeLeft"
                                : "Common.StrafeRight";
                            GClass42.gclass42_0.method_1(string_1);
                            method_34(500, 1200);
                            GClass42.gclass42_0.method_2(string_1);
                        }

                        gclass36_0 = new GClass36(1000 + StartupClass.random_0.Next() % 1500);
                        gclass36_0.method_4();
                    }

                    method_19();
                    if (!gprofile_0.IgnoreAttackers)
                        method_52(true);
                    if (int_7 == gprofile_0.Waypoints.Count * 2 && GClass61.gclass61_0.method_5("SitWhenBored") &&
                        !GContext.Main.IsSpinning)
                    {
                        GClass37.smethod_0(GClass30.smethod_1(161));
                        --int_7;
                        GContext.Main.ReleaseSpinRun();
                        StartupClass.smethod_39(1000);
                        GClass42.gclass42_0.method_0("Common.Sit");
                        var gclass36_4 = new GClass36(60000);
                        gclass36_4.method_4();
                        while (!gclass36_4.method_3())
                        {
                            StartupClass.smethod_39(2000);
                            if (method_10() > 0)
                                break;
                        }
                    }

                    if (!gprofile_0.IgnoreAttackers && method_44())
                        glocation_0 = gplayerSelf_0.Location;
                    method_6();
                    if (glocation_0 != null)
                    {
                        double distanceTo2 = gplayerSelf_0.Location.GetDistanceTo(glocation_0);
                        if (distanceTo2 > 50.0)
                        {
                            GClass37.smethod_0(GClass30.smethod_2(684, distanceTo2));
                            GClass37.smethod_0(GClass30.smethod_2(685, glocation_0.ToString(),
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

            StartupClass.smethod_39(GContext.Main.IsSpinning ? num2 : num3);
            glocation_0 = gplayerSelf_0.Location;
            continue;
            label_91:
            smethod_1();
            goto label_6;
        }

        GContext.Main.ReleaseSpinRun();
        return;
        label_94:
        GClass37.smethod_0(GClass30.smethod_1(158));
        StartupClass.smethod_27(false, "DeadResurrectOff");
        return;
        label_95:
        GClass37.smethod_0(GClass30.smethod_1(159));
        StartupClass.smethod_27(false, "DeadNoGhostWP");
        return;
        label_96:
        GClass37.smethod_0(GClass30.smethod_1(160));
        StartupClass.smethod_27(false, "DeadTooMany");
    }

    protected bool method_40()
    {
        if (StartupClass.gprofile_0.AvoidList != null)
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
            if (gmonster2.IsInList(gprofile_0.AvoidList) && gmonster2.Health > 0.9 && gmonster2.DistanceToSelf < num)
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
        var gclass36 = new GClass36(10000);
        gclass36.method_4();
        GClass37.smethod_0(GClass30.smethod_2(262, gmonster_0.Name));
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
            GClass37.smethod_1(GClass30.smethod_1(263));
            NewHeading = method_43(gplayerSelf_0.Location.GetHeadingTo(gmonster_0.Location));
            string_1 = "Common.Back";
        }
        else
        {
            GClass37.smethod_1(GClass30.smethod_1(264));
            NewHeading = method_43(headingTo);
            string_1 = "Common.Forward";
        }

        GContext.Main.Movement.SetHeading(NewHeading);
        GClass42.gclass42_0.method_1(string_1);
        while (!gclass36.method_3() && gmonster_0.IsValid)
            if (gmonster_0.DistanceToSelf < 30.0)
            {
                if (gplayerSelf_0.TargetGUID == 0L)
                {
                    StartupClass.smethod_39(200);
                }
                else
                {
                    GClass37.smethod_0(GClass30.smethod_1(265));
                    break;
                }
            }
            else
            {
                flag1 = true;
                break;
            }

        GClass42.gclass42_0.method_2(string_1);
        if (gclass36.method_3())
            GClass37.smethod_0(GClass30.smethod_1(266));
        if (flag1)
        {
            GClass37.smethod_0(GClass30.smethod_1(267));
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
        if (int_11 == 0)
            return false;
        var closestHarvestable = GObjectList.GetClosestHarvestable();
        if (closestHarvestable == null)
            return false;
        double distanceToSelf = closestHarvestable.Location.DistanceToSelf;
        if (distanceToSelf > int_11)
            return false;
        GClass37.smethod_0(GClass30.smethod_2(686, Math.Round(distanceToSelf, 2)));
        GContext.Main.ReleaseSpinRun();
        GContext.Main.Movement.MoveToLocation(closestHarvestable.Location, GContext.Main.MeleeDistance, false);
        if (closestHarvestable.DistanceToSelf > GContext.Main.MeleeDistance)
        {
            GClass37.smethod_0(GClass30.smethod_1(268));
            StartupClass.sortedList_2.Add(closestHarvestable.GUID, "");
            return true;
        }

        gplayerSelf_0.Refresh();
        if (method_19())
        {
            GClass37.smethod_0(GClass30.smethod_1(269));
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
            StartupClass.sortedList_2.Add(gnode_0.GUID, "");
        }
        else if (gnode_0.IsMineral && !gplayerSelf_0.HasMining)
        {
            StartupClass.sortedList_2.Add(gnode_0.GUID, "");
        }
        else
        {
            GClass37.smethod_0(GClass30.smethod_2(786, gnode_0.Name));
            var num = 0;
            gnode_0.Hover();
            if (!method_18(GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("CursorType"), "CursorType")))
            {
                GClass37.smethod_0("Can't harvest this, cursor never turned into something acceptable");
                StartupClass.sortedList_2.Add(gnode_0.GUID, "");
            }
            else
            {
                for (; num < 9; ++num)
                    if (!method_19())
                    {
                        var int_15 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("CursorType"), "CursorType");
                        if (method_18(int_15))
                        {
                            method_56();
                            Thread.Sleep(1000);
                            if (gplayerSelf_0.IsCasting)
                                while (gplayerSelf_0.IsCasting)
                                    Thread.Sleep(200);
                            Thread.Sleep(2000);
                            GClass37.smethod_0(GClass30.smethod_1(280));
                        }
                        else
                        {
                            StartupClass.sortedList_2.Add(gnode_0.GUID, "");
                            GClass37.smethod_0("Item not Havestable: " + int_15);
                            return;
                        }
                    }
                    else
                    {
                        GClass37.smethod_0(GClass30.smethod_1(271));
                        return;
                    }

                GClass37.smethod_0(GClass30.smethod_1(281));
                StartupClass.sortedList_2.Add(gnode_0.GUID, "");
            }
        }
    }

    public void method_46()
    {
        GClass37.smethod_0(GClass30.smethod_1(282));
        var gclass36_1 = new GClass36(5000);
        var gclass36_2 = new GClass36(2000);
        var gclass36_3 = new GClass36(1000);
        long num = 0;
        while (true)
        {
            GPlayer gplayer;
            do
            {
                if (gclass36_3.method_3())
                    goto label_33;
                label_1:
                if (gclass36_1.method_3())
                {
                    gclass36_1.method_4();
                    method_26();
                }

                if (gclass36_2.method_3())
                {
                    GClass21.smethod_2();
                    gclass36_2.method_4();
                    if (!GContext.Main.IsSpinning)
                        StartupClass.CurrentGameClass.RunningAction();
                }

                if (!GContext.Main.IsSpinning)
                {
                    method_36(this.gclass36_3, "Common.Time1");
                    method_36(gclass36_4, "Common.Time5");
                    method_36(gclass36_5, "Common.Time30");
                }

                if (gprofile_0.RunFromAvoids)
                    method_40();
                if (gplayerSelf_0.IsDead)
                {
                    GContext.Main.ReleaseSpinRun();
                    GClass37.smethod_0(GClass30.smethod_1(157));
                    if (!(GClass61.gclass61_0.method_2("Resurrect") != "True"))
                    {
                        if (gprofile_0.GhostWaypoints.Count != 0)
                        {
                            if (StartupClass.int_9 < int_10)
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

                gplayer = gclass54_0.method_4();
                continue;
                label_33:
                gclass36_3.method_4();
                if (method_37())
                {
                    GClass37.smethod_0(GClass30.smethod_1(byte.MaxValue));
                    method_38();
                }

                goto label_1;
            } while (gplayer == null);

            if (gplayer.TargetGUID != 0L && gplayer.TargetGUID != num)
            {
                GClass37.smethod_1(GClass30.smethod_2(687, gplayer.TargetGUID.ToString("x")));
                var unit = GObjectList.FindUnit(gplayer.TargetGUID);
                if (unit != null && unit.Health > 0.0 && !gclass54_0.method_13(unit.GUID))
                {
                    GClass37.smethod_1(GClass30.smethod_1(283));
                    var gclass36_4 = new GClass36(8000);
                    gclass36_4.method_4();
                    while (!gclass36_4.method_3())
                        if (unit.Health >= 1.0 && unit.TargetGUID != gplayer.GUID)
                        {
                            if (gplayer.TargetGUID != unit.GUID)
                                smethod_1();
                            else
                                StartupClass.smethod_39(500);
                        }
                        else
                        {
                            GClass37.smethod_1(GClass30.smethod_1(284));
                            break;
                        }

                    GContext.Main.Movement.MoveToUnit(unit, StartupClass.CurrentGameClass.PullDistance, false);
                    if (unit.IsValid)
                    {
                        if (gclass54_0.method_17(unit))
                        {
                            num = 0L;
                            if (gclass54_0.int_2 > 0)
                                StartupClass.smethod_39(gclass54_0.int_2 * 1000);
                            method_12(false);
                            smethod_1();
                        }
                        else
                        {
                            smethod_1();
                            GClass37.smethod_0(GClass30.smethod_2(688, unit.Name));
                            num = gplayer.TargetGUID;
                        }
                    }
                    else
                    {
                        GClass37.smethod_0(GClass30.smethod_1(286));
                    }
                }
            }

            StartupClass.CurrentGameClass.CheckPartyHeal(null);
            StartupClass.CurrentGameClass.CheckPartyBuffs();
            method_44();
            StartupClass.smethod_39(100);
            method_6();
            method_52(true);
        }

        label_36:
        GClass37.smethod_0(GClass30.smethod_1(158));
        StartupClass.smethod_27(false, "PDeadNoResurrect");
        return;
        label_37:
        GClass37.smethod_0(GClass30.smethod_1(159));
        StartupClass.smethod_27(false, "PDeadNoGhostWP");
        return;
        label_38:
        GClass37.smethod_0(GClass30.smethod_1(160));
        StartupClass.smethod_27(false, "PDeadMaxDeaths");
    }

    private void method_47(GUnit gunit_0, int int_15)
    {
        var gclass36_1 = new GClass36(2000);
        gclass36_1.method_4();
        var gclass36_2 = new GClass36(500);
        gclass36_2.method_4();
        while (!gclass36_1.method_3())
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
                    if (gclass36_2.method_3())
                    {
                        GContext.Main.ReleaseSpinRun();
                        gclass36_2.method_4();
                    }

                    StartupClass.smethod_39(100);
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
                smethod_1();
                break;
            }
        }
    }

    private void method_48(GClass36 gclass36_8, GUnit gunit_0, int int_15)
    {
        while (!gclass36_8.method_3())
        {
            gunit_0.Refresh(true);
            if (gunit_0.IsValid)
            {
                gplayerSelf_0.Refresh();
                if (gunit_0.DistanceToSelf >= (double)int_15)
                {
                    StartupClass.smethod_39(100);
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
        GClass37.smethod_1("Waiting for teleport after releasing spirit...");
        while (!gspellTimer.IsReadySlow)
            if (location.GetDistanceTo(gplayerSelf_0.Location) > 5.0)
                return true;
        return false;
    }

    private bool method_50()
    {
        method_14(
            new GLocation(GProcessMemoryManipulator.smethod_13(GClass18.gclass18_0.method_4("CorpseLocation") - 8, "CorpseX"),
                (double)GProcessMemoryManipulator.smethod_13(GClass18.gclass18_0.method_4("CorpseLocation") - 4, "CorpseY")), false);
        return !gplayerSelf_0.IsDead;
    }

    public bool method_51()
    {
        double num = StartupClass.CurrentGameClass.PullDistance + int_5;
        GClass5.smethod_6();
        var gclass5 = GClass5.smethod_2(GPlayerSelf.Me.Location);
        return gclass5 != null && gclass5.glocation_0.GetDistanceTo(gplayerSelf_0.Location) <= num;
    }

    public bool method_52(bool bool_20)
    {
        GClass67.smethod_2();
        var flag = false;
        var num = !bool_20 || !GClass61.gclass61_0.method_5("WalkLoot")
            ? GClass50.double_2
            : StartupClass.CurrentGameClass.PullDistance + int_5;
        GClass5.smethod_6();
        var gclass5_0 = GClass5.smethod_2(gplayerSelf_0.Location);
        if (gclass5_0 != null && gclass5_0.glocation_0.GetDistanceTo(gplayerSelf_0.Location) <= num)
        {
            method_53(gclass5_0);
            flag = true;
            gprofile_0.ConsiderWaypointSkip();
        }

        GClass5.smethod_3();
        if (flag)
            GClass55.smethod_21(false);
        return flag;
    }

    private void method_53(GClass5 gclass5_0)
    {
        var unit = GObjectList.FindUnit(gclass5_0.long_0);
        GContext.Main.ReleaseSpinRun();
        if (gclass5_0.glocation_0.DistanceToSelf > GClass50.double_2 - 1.0 &&
            !unit.Approach(GClass50.double_2 - 1.0, false))
        {
            if (!method_17())
                gclass5_0.method_2();
            GClass37.smethod_0(GClass30.smethod_1(212));
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

                GClass37.smethod_0("TurboLoot didn't work, doing regular loot");
            }

            var flag = false;
            if (!unit.IsSkinnable && gplayerSelf_0.HasSkinning && GClass61.gclass61_0.method_5("AutoSkin"))
                flag = true;
            method_57(gclass5_0, unit, unit.IsSkinnable);
            if (method_17() || !flag)
                return;
            var gclass36 = new GClass36(GClass61.gclass61_0.method_3("SkinDelay") * 1000);
            gclass36.method_4();
            while (!gclass36.method_3() && !unit.IsSkinnable)
            {
                StartupClass.smethod_39(100);
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
        var gclass36 = new GClass36(3000);
        gclass36.method_4();
        while (!gclass36.method_3())
        {
            if (gunit_0.IsLootable)
                return true;
            StartupClass.smethod_39(60);
        }

        return false;
    }

    private bool method_55(GUnit gunit_0, bool bool_20, bool bool_21)
    {
        if (bool_20)
        {
            var gclass36 = new GClass36(2000);
            gclass36.method_4();
            while (!gclass36.method_3() && !GPlayerSelf.Me.IsCasting)
            {
                if (method_17())
                    return false;
                StartupClass.smethod_39(100);
            }

            if (gclass36.method_3())
            {
                GClass37.smethod_0(GClass30.smethod_2(804, gunit_0.GUID.ToString("x")));
                return false;
            }
        }

        var flag = false;
        var gclass36_1 = new GClass36(4500);
        var gclass36_2 = new GClass36(1000);
        gclass36_1.method_4();
        gclass36_2.method_4();
        while (!gclass36_1.method_3())
            if (gunit_0.IsValid)
            {
                if ((gunit_0.IsLootable || bool_20) && (gunit_0.IsSkinnable || !bool_20))
                {
                    if (method_17())
                        return false;
                    if (!bool_21 || !gclass36_2.method_3() || GContext.Main.Me.PetGUID == 0L ||
                        GContext.Main.Me.TargetGUID != GContext.Main.Me.PetGUID)
                    {
                        var num = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("LootWindow"), "LootWindow");
                        if (num != 0 || !flag)
                        {
                            if (num != 0)
                            {
                                if (!flag)
                                    GClass37.smethod_1(GClass30.smethod_1(754));
                                flag = true;
                            }

                            if (flag)
                            {
                                GClass67.smethod_1();
                                if (GClass67.bool_1)
                                {
                                    if (GClass67.string_0.ToLower().IndexOf(GClass30.smethod_1(871)) != -1)
                                    {
                                        GClass37.smethod_0("Bind-on-pickup dialog is visible, accepting");
                                        Thread.Sleep(600);
                                        GClass8.smethod_2("StaticPopup1Button1").method_16(false);
                                        Thread.Sleep(600);
                                    }
                                    else
                                    {
                                        GClass37.smethod_0("Unknown dialog visible during loot: \"" +
                                                           GClass67.string_0 + "\", dismissing");
                                        GClass67.smethod_2();
                                    }
                                }
                            }

                            StartupClass.smethod_39(50);
                        }
                        else
                        {
                            GClass37.smethod_1(GClass30.smethod_1(755));
                            break;
                        }
                    }
                    else
                    {
                        GClass37.smethod_0("Clicked on pet trying to loot, oops!");
                        smethod_1();
                        Thread.Sleep(500);
                        method_56();
                        return method_55(gunit_0, bool_20, false);
                    }
                }
                else
                {
                    break;
                }
            }
            else
            {
                GClass37.smethod_1(GClass30.smethod_1(756));
                break;
            }

        var str = GProcessMemoryManipulator.smethod_9(GClass18.gclass18_0.method_4("RedMessage"), 128, "RedMessage");
        GClass37.smethod_1("Red message after loot: [" + str + "]");
        if (str.ToLower().IndexOf(GClass30.smethod_1(230)) > -1)
        {
            if (GClass61.gclass61_0.method_5("StopWhenFull"))
            {
                GClass37.smethod_0(GClass30.smethod_1(231));
                bool_2 = true;
                bool_3 = true;
                smethod_1();
            }
            else if (GClass61.gclass61_0.method_5("StopLootingWhenFull"))
            {
                GClass37.smethod_0(GClass30.smethod_1(792));
                bool_5 = true;
            }
            else
            {
                GClass37.smethod_0(GClass30.smethod_1(857));
            }
        }

        return (!flag || !gclass36_1.method_3()) && gunit_0 != null &&
               (flag || (!gunit_0.IsLootable && !bool_20) || (!gunit_0.IsSkinnable && bool_20));
    }

    public void method_56()
    {
        if (GClass61.gclass61_0.method_5("ShiftLoot"))
        {
            GClass55.smethod_0(16, true);
            StartupClass.smethod_39(100);
        }

        GClass55.smethod_23(true);
        if (!GClass61.gclass61_0.method_5("ShiftLoot"))
            return;
        GClass55.smethod_0(16, false);
    }

    public void method_57(GClass5 gclass5_0, GUnit gunit_0, bool bool_20)
    {
        GClass37.smethod_1("Starting DoLootCorpse on: 0x" + gclass5_0.long_0.ToString("x"));
        method_60();
        if (!gunit_0.Hover())
        {
            if (GContext.Main.Me.IsUnderAttack)
                return;
            GClass37.smethod_1("First pass failed, trying harder by approaching closer");
            if (!gunit_0.Approach(2.0, false))
            {
                if (!method_17())
                    gclass5_0.method_2();
                GClass37.smethod_0(GClass30.smethod_1(212));
                return;
            }

            if (!gunit_0.Hover())
            {
                GClass37.smethod_0(GClass30.smethod_2(801, gunit_0.GUID.ToString("x")));
                if (method_17())
                    return;
                gclass5_0.method_2();
                return;
            }
        }

        method_56();
        if (!method_55(gunit_0, bool_20, true))
        {
            GClass37.smethod_0(GClass30.smethod_2(803, gunit_0.GUID.ToString("x")));
            if (bool_20)
            {
                if (int_13 < 4)
                {
                    ++int_13;
                    GClass37.smethod_0(GClass30.smethod_1(823));
                    method_57(gclass5_0, gunit_0, bool_20);
                    return;
                }

                GClass37.smethod_0(GClass30.smethod_1(824));
            }

            if (method_17())
                return;
            gclass5_0.method_2();
        }
        else if (!bool_20)
        {
            GClass37.smethod_0(GClass30.smethod_1(208));
            int_13 = 0;
            if (!GClass42.gclass42_0.method_15("Common.PostLoot"))
            {
                if (GClass61.gclass61_0.method_5("RunPostLoot"))
                    bool_7 = true;
                else
                    GContext.Main.CastSpell("Common.PostLoot");
            }

            ++StartupClass.int_8;
            if (StartupClass.SomeIntegerValue > 0)
                --StartupClass.SomeIntegerValue;
            if (gplayerSelf_0.HasSkinning && GClass61.gclass61_0.method_5("AutoSkin"))
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
        GClass20.smethod_0("GMWhisper.wav");
        if (!(GClass61.gclass61_0.method_2("TeleportStop") == "True"))
            return;
        GContext.Main.Movement.LookConfused();
        GClass55.smethod_9(27);
        StartupClass.smethod_27(true, "TeleportWarning");
    }

    private void method_59(GUnit gunit_0)
    {
        GClass20.smethod_0("BadTag.wav");
        ++StartupClass.SomeIntegerValue;
        if (GContext.Main.Me.Pet != null)
            GClass42.gclass42_0.method_0("Common.PetFollow");
        if (StartupClass.SomeIntegerValue >= GClass61.gclass61_0.method_3("BadTagLimit"))
        {
            StartupClass.gclass73_0.bool_2 = true;
            GClass37.smethod_0(GClass30.smethod_1(808));
        }

        if (!gunit_0.IsTargetingMe && !GClass61.gclass61_0.method_5("IgnoreTags"))
        {
            GClass37.smethod_0(GClass30.smethod_2(805, gunit_0.Name));
            smethod_1();
        }
        else
        {
            GClass37.smethod_0(GClass30.smethod_2(806, gunit_0.Name));
            StartupClass.CurrentGameClass.Disengage(gunit_0);
            var gclass36_1 = new GClass36(3000);
            var gclass36_2 = new GClass36(1200);
            gclass36_1.method_4();
            gclass36_2.method_4();
            smethod_1();
            while (!gclass36_1.method_3())
                if (gunit_0.IsTargetingMe)
                {
                    StartupClass.smethod_39(200);
                    if (gclass36_2.method_3())
                    {
                        GClass42.gclass42_0.method_1("Common.Back");
                        Thread.Sleep(400);
                        GClass42.gclass42_0.method_2("Common.Back");
                        gclass36_2.method_4();
                    }
                }
                else
                {
                    GClass37.smethod_0(GClass30.smethod_2(806, gunit_0.Name));
                    smethod_1();
                    return;
                }

            GClass37.smethod_0(GClass30.smethod_2(807, gunit_0.Name));
        }
    }

    private void method_60()
    {
        if (!GContext.Main.MouseSpin || ggameCamera_0 == null || float_0 == 0.0 ||
            Math.Abs(ggameCamera_0.Pitch - float_0) <= Math.PI / 36.0)
            return;
        GClass37.smethod_1("Current camera: " + ggameCamera_0);
        GClass37.smethod_0("Camera pitch is messed up, fixing");
        GContext.Main.ReleaseSpinRun();
        StartupClass.gclass68_0.method_16(ggameCamera_0, float_0);
    }

    private bool method_61()
    {
        if (GClass61.gclass61_0.method_5("VendorOnFoodWater") && GPlayerSelf.Me.PlayerClass != GPlayerClass.Mage)
        {
            var actionInventory1 = GContext.Main.Interface.GetActionInventory("Common.Eat");
            var actionInventory2 = GContext.Main.Interface.GetActionInventory("Common.Drink");
            if ((actionInventory1 == 0 && StartupClass.CurrentGameClass.ShouldBuyFood) ||
                (actionInventory2 == 0 && StartupClass.CurrentGameClass.ShouldBuyWater))
            {
                GClass37.smethod_0("Food or water is zero, going back for more");
                return true;
            }
        }

        if (GClass61.gclass61_0.method_5("VendorOnDurability"))
        {
            var equippedItems = GObjectList.GetEquippedItems();
            var num = GClass61.gclass61_0.method_4("VendorDurabilityMin");
            var flag = false;
            foreach (var gitem in equippedItems)
                if (gitem.Durability < num)
                {
                    GClass37.smethod_0("Item is in need of repair: \"" + gitem.Name + "\"");
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
            GClass37.smethod_0("** OnHearth inner exception: " + ex.Message + "\r\n" + ex.StackTrace);
            throw ex;
        }
    }

    private void method_63()
    {
        if (gprofile_0.IsVendorEnabled && bool_3 && StartupClass.IsSomeConditionMet)
        {
            StartupClass.bool_21 = false;
            GClass37.smethod_0("Starting up vendor stuff");
            Thread.Sleep(6000);
            if (gprofile_0.VendorWaypoints[0].GetDistanceTo(gplayerSelf_0.Location) > 20.0)
            {
                GClass37.smethod_0("First vendor waypoint is too far away, skipping resume");
                StartupClass.bool_21 = true;
            }
            else
            {
                method_64(null);
            }

            glocation_0 = null;
        }
        else
        {
            StartupClass.string_9 = null;
            StartupClass.bool_31 = false;
            StartupClass.smethod_27(false, "HearthAndExit");
            throw new ThreadInterruptedException();
        }
    }

    private void method_64(GLocation glocation_3)
    {
        var flag1 = false;
        if (GClass61.gclass61_0.method_5("SendMail"))
            bool_17 = true;
        var flag2 = false;
        var num = 3;
        var flag3 = false;
        var Tolerance = Math.PI / 6.0;
        var vendorPath = gprofile_0.CreateVendorPath(glocation_3);
        if (!StartupClass.CurrentGameClass.ShouldBuyFood && !StartupClass.CurrentGameClass.ShouldBuyWater)
            flag1 = true;
        while (vendorPath.Count > 0)
        {
            if (!flag1)
            {
                var unit = GObjectList.FindUnit(gprofile_0.VendorFW, true);
                if (unit != null && unit.DistanceToSelf <= 10.0)
                {
                    flag2 = method_70(unit);
                    flag1 = true;
                }
            }

            if (!flag2)
            {
                var unit = GObjectList.FindUnit(gprofile_0.VendorAR, true);
                if (unit != null && unit.DistanceToSelf < 10.0)
                    flag2 = method_71(unit);
            }

            if (!flag2 && gprofile_0.VendorRepair != null)
            {
                var unit = GObjectList.FindUnit(gprofile_0.VendorRepair, true);
                if (unit != null && unit.DistanceToSelf < 10.0)
                    flag2 = method_72(unit);
            }

            bool_18 = true;
            int_14 = 0;
            while (bool_16 & bool_17 & bool_18)
                method_75();
            var Target = vendorPath.Dequeue();
            GClass37.smethod_1("$ Dequeued loc: " + Target);
            if (Math.Abs(GContext.Main.Movement.CompareHeadings(gplayerSelf_0.Heading,
                    gplayerSelf_0.Location.GetHeadingTo(Target))) > Tolerance && GContext.Main.IsRunning)
            {
                GClass37.smethod_0("Lifting off to set heading exact");
                GContext.Main.ReleaseRun();
                double headingTo = gplayerSelf_0.Location.GetHeadingTo(Target);
                GContext.Main.Movement.SetHeading(headingTo, Tolerance);
            }

            if (flag1 && flag2 && !flag3)
            {
                --num;
                if (num == 0)
                {
                    GClass37.smethod_0("Got food and repair, switching to faster running");
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
                GClass37.smethod_0("Never able to reach next vendor wp, giving up");
                StartupClass.smethod_27(false, "VendorWPStuck");
                return;
            }
        }

        GClass37.smethod_0("Vendor waypoints all done, resuming profile");
        bool_2 = false;
        bool_3 = false;
        gprofile_0.BeginProfile(GPlayerSelf.Me.Location);
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
            GClass37.smethod_0("Merchant window didn't open, trying again");
            Thread.Sleep(3000);
        }

        GClass37.smethod_0("Too many failures, giving up");
        StartupClass.smethod_27(false, "VendorInteractFailed");
        return null;
    }

    private void method_66(GMerchant gmerchant_0, int int_15)
    {
        GClass37.smethod_0("Loading up on ammo: \"" + GPlayerSelf.Me.AmmoName + "\"");
        for (var int_15_1 = GPlayerSelf.Me.AmmoCount; int_15_1 < int_15; int_15_1 = method_69(int_15_1))
            gmerchant_0.BuyOnAnyPage(GPlayerSelf.Me.AmmoName);
    }

    private void method_67(GMerchant gmerchant_0, string string_2, int int_15, string string_3)
    {
        var actionInventory = GContext.Main.Interface.GetActionInventory(string_3);
        var int_15_1 = actionInventory;
        if (actionInventory >= int_15)
            GClass37.smethod_0("Already got enough, don't need to buy any");
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

        GClass37.smethod_0("Inventory doesn't seem to be going up for " + string_2 + " when buying");
        StartupClass.smethod_27(false, "StockupFailed");
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

        GClass37.smethod_0("Ammo count doesn't seem to be going up!");
        StartupClass.smethod_27(false, "StockupFailed");
        return 0;
    }

    private bool method_70(GUnit gunit_0)
    {
        GClass37.smethod_0("Doing food/water with \"" + gunit_0.Name + "\"");
        var gmerchant_0 = method_65(gunit_0);
        var flag = false;
        if (gmerchant_0.IsRepairVisible && gmerchant_0.IsRepairEnabled)
        {
            flag = true;
            gmerchant_0.ClickRepairButton();
        }

        if (GClass61.gclass61_0.method_5("VendorJunk"))
        {
            GContext.Main.SendKey("Common.BackpackAll");
            method_74();
            Thread.Sleep(1000);
        }

        if (StartupClass.CurrentGameClass.ShouldBuyFood)
        {
            var string_2 = method_73("Common.Eat");
            GClass37.smethod_0("Loading up on food: \"" + string_2 + "\"");
            method_67(gmerchant_0, string_2, GClass61.gclass61_0.method_3("FoodAmount"), "Common.Eat");
        }

        if (StartupClass.CurrentGameClass.ShouldBuyWater)
        {
            var string_2 = method_73("Common.Drink");
            GClass37.smethod_0("Loading up on water: \"" + string_2 + "\"");
            method_67(gmerchant_0, string_2, GClass61.gclass61_0.method_3("WaterAmount"), "Common.Drink");
        }

        Thread.Sleep(671);
        smethod_1();
        Thread.Sleep(671);
        smethod_1();
        Thread.Sleep(671);
        return flag;
    }

    private bool method_71(GUnit gunit_0)
    {
        GClass37.smethod_0("Doing ammo/repair with \"" + gunit_0.Name + "\"");
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
            var int_15 = GClass61.gclass61_0.method_3("AmmoAmount");
            GClass37.smethod_0("My current ammo: " + GPlayerSelf.Me.AmmoCount + ", required: " + int_15);
            if (GPlayerSelf.Me.AmmoCount < int_15)
                method_66(gmerchant_0, int_15);
        }

        if (GClass61.gclass61_0.method_5("VendorJunk"))
        {
            GContext.Main.SendKey("Common.BackpackAll");
            method_74();
            Thread.Sleep(1000);
        }

        Thread.Sleep(671);
        smethod_1();
        Thread.Sleep(671);
        smethod_1();
        Thread.Sleep(671);
        return flag;
    }

    private bool method_72(GUnit gunit_0)
    {
        GClass37.smethod_0("Doing alt repair with \"" + gunit_0.Name + "\"");
        var gmerchant = method_65(gunit_0);
        if (gmerchant.IsRepairVisible && gmerchant.IsRepairEnabled)
            gmerchant.ClickRepairButton();
        Thread.Sleep(671);
        smethod_1();
        Thread.Sleep(671);
        smethod_1();
        Thread.Sleep(671);
        return true;
    }

    private string method_73(string string_2)
    {
        var byKeyName = GContext.Main.Interface.GetByKeyName(string_2);
        if (byKeyName == null)
        {
            GClass37.smethod_0("Couldn't guess item name, no UI object for: " + string_2);
            StartupClass.smethod_27(false, "BadItemN");
            return null;
        }

        byKeyName.Hover();
        Thread.Sleep(888);
        var byName = GContext.Main.Interface.GetByName("GameTooltip");
        if (byName != null && byName.IsVisible)
            return byName.GetChildObject("GameTooltipTextLeft1").LabelText;
        GClass37.smethod_0("Couldn't find tool tip or tooltip not visible after hovering over: " + string_2);
        StartupClass.smethod_27(false, "BadToolTip");
        return null;
    }

    private void method_74()
    {
        gbagItem_1 = GPlayerSelf.Me.GetBagCollection(GItemBagAction.Sell);
        if (gbagItem_1.Length > 0)
            foreach (var gbagItem in gbagItem_1)
            {
                GClass37.smethod_1("Sell: " + gbagItem.Item.Name);
                Thread.Sleep(500 + StartupClass.random_0.Next() % 1000);
                gbagItem.Click(true);
            }
        else
            GClass37.smethod_0("No items to sell.");
    }

    private void method_75()
    {
        foreach (var node in GObjectList.GetNodes())
            if (node.IsMailBox && node.DistanceToSelf <= (double)int_12)
            {
                GClass37.smethod_0("We are near a mailbox!");
                bool_15 = true;
                GContext.Main.ReleaseSpinRun();
                gbagItem_0 = GPlayerSelf.Me.GetBagCollection(GItemBagAction.Mail);
                int_14 = 0;
                GClass37.smethod_1("Bag Count: " + gbagItem_0.Length);
                if (gbagItem_0.Length >= 1)
                {
                    if (node.DistanceToSelf > GContext.Main.MeleeDistance)
                    {
                        GClass37.smethod_0("Approaching Mailbox");
                        GContext.Main.ReleaseSpinRun();
                        GContext.Main.Movement.MoveToLocation(node.Location, GContext.Main.MeleeDistance, false);
                        if (node.DistanceToSelf > GContext.Main.MeleeDistance)
                        {
                            GClass37.smethod_1("cant get to the box...something blocking?");
                            bool_16 = false;
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
                            bool_16 = true;
                            bool_18 = true;
                        }
                        else
                        {
                            bool_16 = false;
                        }
                    }
                    else
                    {
                        GClass37.smethod_0("No items to mail.");
                        bool_16 = false;
                    }

                    Thread.Sleep(200);
                    GContext.Main.Interface.GetByName("InboxCloseButton").ClickMouse(false);
                }
                else
                {
                    GClass37.smethod_0("We have no mail");
                    bool_16 = false;
                    break;
                }
            }

        if (bool_15)
            return;
        bool_18 = false;
    }

    private bool method_76()
    {
        GContext.Main.Interface.GetByName("SendMailNameEditBox").ClickMouse(false);
        Thread.Sleep(200);
        var What1 = GClass61.gclass61_0.method_2("MailToText");
        GContext.Main.Interface.SendString(What1);
        Thread.Sleep(200);
        GContext.Main.Interface.GetByName("SendMailSubjectEditBox").ClickMouse(false);
        var What2 = GClass61.gclass61_0.method_2("SubjectText");
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
                GClass37.smethod_1("max item limit reached (12 items)");
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

        GClass37.smethod_0("No mail to send.");
        Thread.Sleep(500);
        GContext.Main.SendKey("Common.BackpackAll");
        return false;
    }
}