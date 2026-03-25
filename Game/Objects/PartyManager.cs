// Decompiled with JetBrains decompiler
// Type: PartyManager
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common;
using Glider.Common.Objects;
using System;
using System.Collections;
using System.Threading;

public class PartyManager
{
    public static PartyManager partyManager;
    public bool bool_0;
    public bool bool_1;
    public bool bool_2;
    public bool bool_3;
    public bool bool_4;
    public GameTimer[] licenseCheckTimer;
    public GameTimer[] resumeTimer;
    public PartyRole genum7_0;
    public GPlayer[] gplayer_0;
    public GPlayer gplayer_1;
    public GPlayer[] gplayer_2;
    public HealMode healMode_0;
    public int int_0;
    public int int_1;
    public int int_2;
    public int int_3;
    public int pgEditProfileCount;
    public int objectManagerBasePointer;
    public int initCount;
    public int knownVersion;
    public long[] playerGuid;
    public SortedList[] Offsets;
    public string[] string_0;
    public string string_1;

    public void method_0(ConfigManager gclass61_0)
    {
        partyManager = this;
        switch (ConfigManager.gclass61_0.method_2("PartyMode"))
        {
            case "Solo":
                genum7_0 = PartyRole.const_0;
                break;
            case "Leader":
                genum7_0 = PartyRole.const_1;
                break;
            case "Follower":
                genum7_0 = PartyRole.const_2;
                break;
        }

        switch (ConfigManager.gclass61_0.method_2("PartyHealMode"))
        {
            case "Dedicated":
                healMode_0 = HealMode.Dedicated;
                break;
            case "Normal":
                healMode_0 = HealMode.Normal;
                break;
            case "Never":
                healMode_0 = HealMode.Never;
                break;
        }

        int_0 = gclass61_0.method_3("PartyLooters");
        int_1 = gclass61_0.method_3("PartyLootPos") - 1;
        int_2 = gclass61_0.method_3("PartyAttackDelay");
        int_3 = gclass61_0.method_3("PartyLootDelay");
        pgEditProfileCount = gclass61_0.method_3("PartyLeaderWait");
        objectManagerBasePointer = gclass61_0.method_3("PartyFollowerStart");
        initCount = gclass61_0.method_3("PartyFollowerStop");
        string_1 = ConfigManager.gclass61_0.method_2("PartyLeaderName");
        bool_0 = gclass61_0.method_5("PartyAdds");
        bool_1 = gclass61_0.method_5("PartyHeal");
        bool_2 = gclass61_0.method_5("PartyBuff");
        bool_3 = gclass61_0.method_5("PartySlashFollow");
        var arrayList = new ArrayList();
        for (var index = 1; index <= 4; ++index)
            if (ConfigManager.gclass61_0.method_2("PartyMember" + index).Length > 0)
                arrayList.Add(ConfigManager.gclass61_0.method_2("PartyMember" + index));
        string_0 = (string[])arrayList.ToArray(typeof(string));
        gplayer_1 = null;
        gplayer_2 = null;
        method_9();
    }

    public static PartyRole smethod_0()
    {
        return StartupClass.glideMode_0 != GlideMode.Auto ? PartyRole.const_0 : partyManager.genum7_0;
    }

    public void method_1()
    {
        knownVersion = 0;
    }

    public GPlayer method_2(string string_2)
    {
        foreach (var player in GObjectList.GetPlayers())
            if (player.Name.ToLower() == string_2.ToLower())
                return player;
        return null;
    }

    public GUnit method_3()
    {
        if (genum7_0 == PartyRole.const_0)
            return null;
        GObjectList.GetMonsters();
        GObjectList.GetPlayers();
        if (genum7_0 == PartyRole.const_2)
        {
            var gplayer = method_2(string_1);
            if (gplayer != null)
            {
                var unitByTarget1 = GObjectList.FindUnitByTarget(gplayer.GUID);
                if (unitByTarget1 != null && !method_13(unitByTarget1.GUID))
                {
                    Logger.LoadProfile(MessageProvider.IsGroupProfile(315, unitByTarget1.Name));
                    return unitByTarget1;
                }

                if (gplayer.PetGUID != 0L)
                {
                    var unitByTarget2 = GObjectList.FindUnitByTarget(gplayer.PetGUID);
                    if (unitByTarget2 != null && !method_13(unitByTarget2.GUID))
                    {
                        Logger.LoadProfile(MessageProvider.IsGroupProfile(316, unitByTarget2.Name));
                        return unitByTarget2;
                    }
                }
            }
        }

        if (genum7_0 == PartyRole.const_1)
            foreach (var string_2 in string_0)
                if (string_2.Length > 2)
                {
                    var gplayer = method_2(string_2);
                    if (gplayer != null)
                    {
                        var unitByTarget3 = GObjectList.FindUnitByTarget(gplayer.GUID);
                        if (unitByTarget3 == null || method_13(unitByTarget3.GUID))
                        {
                            if (gplayer.PetGUID != 0L)
                            {
                                var unitByTarget4 = GObjectList.FindUnitByTarget(gplayer.PetGUID);
                                if (unitByTarget4 != null && !method_13(unitByTarget4.GUID))
                                {
                                    Logger.LoadProfile(MessageProvider.IsGroupProfile(318, unitByTarget4.Name));
                                    return unitByTarget4;
                                }
                            }
                        }
                        else
                        {
                            Logger.LoadProfile(MessageProvider.IsGroupProfile(317, unitByTarget3.Name));
                            return unitByTarget3;
                        }
                    }
                }

        return null;
    }

    public GPlayer method_4()
    {
        var gclass36 = new GameTimer(3000, 37000);
        var flag = false;
        GPlayer gplayer;
        while (true)
        {
            do
            {
                gplayer = method_2(string_1);
                if (gplayer == null)
                {
                    if (!flag)
                    {
                        flag = true;
                        Logger.LogMessage(MessageProvider.IsGroupProfile(704, string_1));
                    }

                    if (GPlayerSelf.Me.TargetGUID != 0L)
                    {
                        Logger.LogMessage(MessageProvider.GetMessage(319));
                        StartupClass.combatController.method_12(true);
                    }

                    StartupClass.Sleep(1000);
                }
                else
                {
                    goto label_8;
                }
            } while (!gclass36.method_3());

            gclass36 = new GameTimer(3000, 37000);
            method_8();
        }

    label_8:
        if (gplayer.DistanceToSelf > (double)objectManagerBasePointer)
        {
            if (!gplayer.Approach(initCount, false))
            {
                Logger.LogMessage(MessageProvider.GetMessage(320));
                return null;
            }

            if (bool_3)
                InputController.ExecuteStopGlide(MessageProvider.IsGroupProfile(705, string_1));
        }

        return gplayer;
    }

    public void method_5()
    {
        gplayer_2 = new GPlayer[string_0.Length];
        for (var index = 0; index < string_0.Length; ++index)
            gplayer_2[index] = (GPlayer)GObjectList.FindUnit(string_0[index]);
    }

    public void method_6()
    {
        var gclass36 = new GameTimer(3000, 37000);
        var flag1 = false;
        while (true)
        {
            do
            {
                method_5();
                var flag2 = true;
                for (var index = 0; index < gplayer_2.Length; ++index)
                    if (gplayer_2[index] == null)
                    {
                        if (!flag1)
                            Logger.LogMessage(MessageProvider.IsGroupProfile(706, string_0[index]));
                        flag2 = false;
                        GContext.Main.ReleaseSpinRun();
                    }
                    else if (gplayer_2[index].DistanceToSelf > (double)pgEditProfileCount || gplayer_2[index].IsSitting)
                    {
                        if (!flag1)
                            Logger.LogMessage(MessageProvider.IsGroupProfile(707, string_0[index],
                                Math.Round(gplayer_2[index].DistanceToSelf, 2)));
                        flag2 = false;
                        GContext.Main.ReleaseSpinRun();
                    }

                flag1 = true;
                if (!flag2)
                {
                    StartupClass.Sleep(1300);
                    var gunit_0 = method_3();
                    if (gunit_0 != null)
                        method_7(gunit_0);
                    if (GPlayerSelf.Me.TargetGUID != 0L)
                        goto label_19;
                }
                else
                {
                    goto label_15;
                }
            } while (!gclass36.method_3());

            gclass36 = new GameTimer(3000, 37000);
            method_8();
        }

    label_15:
        return;
    label_19:
        Logger.LogMessage(MessageProvider.GetMessage(321));
    }

    public bool method_7(GUnit gunit_0)
    {
        Logger.LogMessage(MessageProvider.IsGroupProfile(202, Math.Round(gunit_0.DistanceToSelf, 2)));
        GContext.Main.ReleaseSpinRun();
        gunit_0.Face();
        gunit_0.SetAsTarget(false);
        if (GPlayerSelf.Me.TargetGUID == gunit_0.GUID)
        {
            StartupClass.combatController.method_12(true);
            return true;
        }

        StartupClass.ActiveGProfile.ForceBlacklist(gunit_0.GUID);
        return false;
    }

    private void method_8()
    {
        switch (StartupClass.random_0.Next() % 2)
        {
            case 0:
                SpellcastingManager.gclass42_0.method_0("Common.Jump");
                break;
            case 1:
                GContext.Main.Movement.SetHeading(StartupClass.random_0.NextDouble() * 2.0 * Math.PI);
                break;
        }
    }

    public void method_9()
    {
        playerGuid = null;
    }

    public void method_10()
    {
        if (playerGuid != null)
            return;
        method_11();
    }

    public void method_11()
    {
        var arrayList = new ArrayList();
        playerGuid = null;
        gplayer_0 = null;
        for (var index = 1; index <= 4; ++index)
        {
            CombatController.LoadProfile();
            StartupClass.Sleep(200);
            SpellcastingManager.gclass42_0.method_0("Common.TargetParty" + index);
            StartupClass.Sleep(500);
            if (GPlayerSelf.Me.TargetGUID != 0L)
            {
                var unit = (GPlayer)GObjectList.FindUnit(GPlayerSelf.Me.TargetGUID);
                if (unit != null)
                    Logger.LogMessage(MessageProvider.IsGroupProfile(708, unit.Name));
                else
                    Logger.LogMessage(MessageProvider.GetMessage(326));
                arrayList.Add(GPlayerSelf.Me.TargetGUID);
                if (genum7_0 == PartyRole.const_2 && unit.Name.ToLower() == string_1.ToLower())
                {
                    knownVersion = index;
                    Logger.LoadProfile("Found leader in slot: " + index);
                }
            }
            else
            {
                break;
            }
        }

        if (arrayList.Count <= 0)
            return;
        playerGuid = (long[])arrayList.ToArray(typeof(long));
        gplayer_0 = new GPlayer[playerGuid.Length];
        licenseCheckTimer = new GameTimer[playerGuid.Length];
        resumeTimer = new GameTimer[playerGuid.Length];
        if (Offsets != null)
            return;
        Offsets = new SortedList[playerGuid.Length];
    }

    public void method_12()
    {
        if (playerGuid == null)
            return;
        for (var index = 0; index < playerGuid.Length; ++index)
            gplayer_0[index] = (GPlayer)GObjectList.FindUnit(playerGuid[index]);
    }

    public bool method_13(long long_1)
    {
        if (genum7_0 == PartyRole.const_0 || playerGuid == null)
            return false;
        foreach (var num in playerGuid)
            if (num == long_1)
                return true;
        return long_1 == GPlayerSelf.Me.GUID;
    }

    public void method_14(int expectedVersion, string string_2, GUnit gunit_0)
    {
        Logger.LoadProfile(MessageProvider.IsGroupProfile(709, string_2, expectedVersion + 1));
        SpellcastingManager.gclass42_0.method_0("Common.TargetParty" + (expectedVersion + 1));
        StartupClass.Sleep(300);
        if (gplayer_0[expectedVersion].DistanceToSelf > 29.0)
            gplayer_0[expectedVersion].Approach(29.0);
        GContext.Main.CastSpell(string_2);
        if (gunit_0 != null)
        {
            gunit_0.SetAsTarget(true);
            if (GPlayerSelf.Me.TargetGUID == gunit_0.GUID)
                return;
            Logger.LogMessage(MessageProvider.GetMessage(327));
            gunit_0.SetAsTarget(true);
        }
        else
        {
            CombatController.LoadProfile();
        }
    }

    public bool method_15(int expectedVersion, string string_2, int versionPatchLevel)
    {
        if (Offsets == null || !bool_2 || GPlayerSelf.Me.Mana < 0.2)
            return false;
        if (Offsets[expectedVersion] == null)
            Offsets[expectedVersion] = new SortedList();
        if (Offsets[expectedVersion].ContainsKey(string_2))
        {
            var gclass12 = (TimedStringEntry)Offsets[expectedVersion][string_2];
            if (!gclass12.licenseCheckTimer.method_3())
                return false;
            method_14(expectedVersion, string_2, null);
            gclass12.licenseCheckTimer.method_4();
            return true;
        }

        var gclass12_1 = new TimedStringEntry(string_2, versionPatchLevel);
        method_14(expectedVersion, string_2, null);
        Offsets[expectedVersion].Add(string_2, gclass12_1);
        return true;
    }

    public bool method_16(int expectedVersion, string string_2)
    {
        if (Offsets == null || !bool_2 || GPlayerSelf.Me.Mana < 0.2)
            return false;
        if (Offsets[expectedVersion] == null)
            Offsets[expectedVersion] = new SortedList();
        return !Offsets[expectedVersion].ContainsKey(string_2) ||
               ((TimedStringEntry)Offsets[expectedVersion][string_2]).licenseCheckTimer.method_3();
    }

    public bool method_17(GUnit gunit_0)
    {
        Logger.LoadProfile("Assisting on target: " + gunit_0);
        if (knownVersion != 0)
        {
            Logger.LoadProfile("Using assist key to pick up target");
            SpellcastingManager.gclass42_0.method_0("Common.TargetParty" + knownVersion);
            Thread.Sleep(100);
            SpellcastingManager.gclass42_0.method_0("Common.Assist");
            var gclass36 = new GameTimer(2000);
            gclass36.method_4();
            while (!gclass36.method_3())
            {
                Thread.Sleep(100);
                if (GPlayerSelf.Me.TargetGUID == gunit_0.GUID)
                    return true;
            }

            Logger.LoadProfile("Never got target with assist key, using tab");
            return gunit_0.SetAsTarget(false);
        }

        Logger.LoadProfile("LeaderSlot is empty, attempting to pick up target with tab");
        return gunit_0.SetAsTarget(false);
    }
}