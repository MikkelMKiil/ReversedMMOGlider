// Decompiled with JetBrains decompiler
// Type: GClass54
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections;
using System.Threading;
using Glider.Common;
using Glider.Common.Objects;

public class GClass54
{
    public static GClass54 gclass54_0;
    public bool bool_0;
    public bool bool_1;
    public bool bool_2;
    public bool bool_3;
    public bool bool_4;
    public GClass36[] gclass36_0;
    public GClass36[] gclass36_1;
    public GEnum7 genum7_0;
    public GPlayer[] gplayer_0;
    public GPlayer gplayer_1;
    public GPlayer[] gplayer_2;
    public HealMode healMode_0;
    public int int_0;
    public int int_1;
    public int int_2;
    public int int_3;
    public int int_4;
    public int int_5;
    public int int_6;
    public int int_7;
    public long[] long_0;
    public SortedList[] sortedList_0;
    public string[] string_0;
    public string string_1;

    public void method_0(GClass61 gclass61_0)
    {
        gclass54_0 = this;
        switch (GClass61.gclass61_0.method_2("PartyMode"))
        {
            case "Solo":
                genum7_0 = GEnum7.const_0;
                break;
            case "Leader":
                genum7_0 = GEnum7.const_1;
                break;
            case "Follower":
                genum7_0 = GEnum7.const_2;
                break;
        }

        switch (GClass61.gclass61_0.method_2("PartyHealMode"))
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
        int_4 = gclass61_0.method_3("PartyLeaderWait");
        int_5 = gclass61_0.method_3("PartyFollowerStart");
        int_6 = gclass61_0.method_3("PartyFollowerStop");
        string_1 = GClass61.gclass61_0.method_2("PartyLeaderName");
        bool_0 = gclass61_0.method_5("PartyAdds");
        bool_1 = gclass61_0.method_5("PartyHeal");
        bool_2 = gclass61_0.method_5("PartyBuff");
        bool_3 = gclass61_0.method_5("PartySlashFollow");
        var arrayList = new ArrayList();
        for (var index = 1; index <= 4; ++index)
            if (GClass61.gclass61_0.method_2("PartyMember" + index).Length > 0)
                arrayList.Add(GClass61.gclass61_0.method_2("PartyMember" + index));
        string_0 = (string[])arrayList.ToArray(typeof(string));
        gplayer_1 = null;
        gplayer_2 = null;
        method_9();
    }

    public static GEnum7 smethod_0()
    {
        return StartupClass.glideMode_0 != GlideMode.Auto ? GEnum7.const_0 : gclass54_0.genum7_0;
    }

    public void method_1()
    {
        int_7 = 0;
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
        if (genum7_0 == GEnum7.const_0)
            return null;
        GObjectList.GetMonsters();
        GObjectList.GetPlayers();
        if (genum7_0 == GEnum7.const_2)
        {
            var gplayer = method_2(string_1);
            if (gplayer != null)
            {
                var unitByTarget1 = GObjectList.FindUnitByTarget(gplayer.GUID);
                if (unitByTarget1 != null && !method_13(unitByTarget1.GUID))
                {
                    GClass37.smethod_1(GClass30.smethod_2(315, unitByTarget1.Name));
                    return unitByTarget1;
                }

                if (gplayer.PetGUID != 0L)
                {
                    var unitByTarget2 = GObjectList.FindUnitByTarget(gplayer.PetGUID);
                    if (unitByTarget2 != null && !method_13(unitByTarget2.GUID))
                    {
                        GClass37.smethod_1(GClass30.smethod_2(316, unitByTarget2.Name));
                        return unitByTarget2;
                    }
                }
            }
        }

        if (genum7_0 == GEnum7.const_1)
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
                                    GClass37.smethod_1(GClass30.smethod_2(318, unitByTarget4.Name));
                                    return unitByTarget4;
                                }
                            }
                        }
                        else
                        {
                            GClass37.smethod_1(GClass30.smethod_2(317, unitByTarget3.Name));
                            return unitByTarget3;
                        }
                    }
                }

        return null;
    }

    public GPlayer method_4()
    {
        var gclass36 = new GClass36(3000, 37000);
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
                        GClass37.smethod_0(GClass30.smethod_2(704, string_1));
                    }

                    if (GPlayerSelf.Me.TargetGUID != 0L)
                    {
                        GClass37.smethod_0(GClass30.smethod_1(319));
                        StartupClass.gclass73_0.method_12(true);
                    }

                    StartupClass.smethod_39(1000);
                }
                else
                {
                    goto label_8;
                }
            } while (!gclass36.method_3());

            gclass36 = new GClass36(3000, 37000);
            method_8();
        }

        label_8:
        if (gplayer.DistanceToSelf > (double)int_5)
        {
            if (!gplayer.Approach(int_6, false))
            {
                GClass37.smethod_0(GClass30.smethod_1(320));
                return null;
            }

            if (bool_3)
                GClass55.smethod_28(GClass30.smethod_2(705, string_1));
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
        var gclass36 = new GClass36(3000, 37000);
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
                            GClass37.smethod_0(GClass30.smethod_2(706, string_0[index]));
                        flag2 = false;
                        GContext.Main.ReleaseSpinRun();
                    }
                    else if (gplayer_2[index].DistanceToSelf > (double)int_4 || gplayer_2[index].IsSitting)
                    {
                        if (!flag1)
                            GClass37.smethod_0(GClass30.smethod_2(707, string_0[index],
                                Math.Round(gplayer_2[index].DistanceToSelf, 2)));
                        flag2 = false;
                        GContext.Main.ReleaseSpinRun();
                    }

                flag1 = true;
                if (!flag2)
                {
                    StartupClass.smethod_39(1300);
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

            gclass36 = new GClass36(3000, 37000);
            method_8();
        }

        label_15:
        return;
        label_19:
        GClass37.smethod_0(GClass30.smethod_1(321));
    }

    public bool method_7(GUnit gunit_0)
    {
        GClass37.smethod_0(GClass30.smethod_2(202, Math.Round(gunit_0.DistanceToSelf, 2)));
        GContext.Main.ReleaseSpinRun();
        gunit_0.Face();
        gunit_0.SetAsTarget(false);
        if (GPlayerSelf.Me.TargetGUID == gunit_0.GUID)
        {
            StartupClass.gclass73_0.method_12(true);
            return true;
        }

        StartupClass.gprofile_0.ForceBlacklist(gunit_0.GUID);
        return false;
    }

    private void method_8()
    {
        switch (StartupClass.random_0.Next() % 2)
        {
            case 0:
                GClass42.gclass42_0.method_0("Common.Jump");
                break;
            case 1:
                GContext.Main.Movement.SetHeading(StartupClass.random_0.NextDouble() * 2.0 * Math.PI);
                break;
        }
    }

    public void method_9()
    {
        long_0 = null;
    }

    public void method_10()
    {
        if (long_0 != null)
            return;
        method_11();
    }

    public void method_11()
    {
        var arrayList = new ArrayList();
        long_0 = null;
        gplayer_0 = null;
        for (var index = 1; index <= 4; ++index)
        {
            GClass73.smethod_1();
            StartupClass.smethod_39(200);
            GClass42.gclass42_0.method_0("Common.TargetParty" + index);
            StartupClass.smethod_39(500);
            if (GPlayerSelf.Me.TargetGUID != 0L)
            {
                var unit = (GPlayer)GObjectList.FindUnit(GPlayerSelf.Me.TargetGUID);
                if (unit != null)
                    GClass37.smethod_0(GClass30.smethod_2(708, unit.Name));
                else
                    GClass37.smethod_0(GClass30.smethod_1(326));
                arrayList.Add(GPlayerSelf.Me.TargetGUID);
                if (genum7_0 == GEnum7.const_2 && unit.Name.ToLower() == string_1.ToLower())
                {
                    int_7 = index;
                    GClass37.smethod_1("Found leader in slot: " + index);
                }
            }
            else
            {
                break;
            }
        }

        if (arrayList.Count <= 0)
            return;
        long_0 = (long[])arrayList.ToArray(typeof(long));
        gplayer_0 = new GPlayer[long_0.Length];
        gclass36_0 = new GClass36[long_0.Length];
        gclass36_1 = new GClass36[long_0.Length];
        if (sortedList_0 != null)
            return;
        sortedList_0 = new SortedList[long_0.Length];
    }

    public void method_12()
    {
        if (long_0 == null)
            return;
        for (var index = 0; index < long_0.Length; ++index)
            gplayer_0[index] = (GPlayer)GObjectList.FindUnit(long_0[index]);
    }

    public bool method_13(long long_1)
    {
        if (genum7_0 == GEnum7.const_0 || long_0 == null)
            return false;
        foreach (var num in long_0)
            if (num == long_1)
                return true;
        return long_1 == GPlayerSelf.Me.GUID;
    }

    public void method_14(int int_8, string string_2, GUnit gunit_0)
    {
        GClass37.smethod_1(GClass30.smethod_2(709, string_2, int_8 + 1));
        GClass42.gclass42_0.method_0("Common.TargetParty" + (int_8 + 1));
        StartupClass.smethod_39(300);
        if (gplayer_0[int_8].DistanceToSelf > 29.0)
            gplayer_0[int_8].Approach(29.0);
        GContext.Main.CastSpell(string_2);
        if (gunit_0 != null)
        {
            gunit_0.SetAsTarget(true);
            if (GPlayerSelf.Me.TargetGUID == gunit_0.GUID)
                return;
            GClass37.smethod_0(GClass30.smethod_1(327));
            gunit_0.SetAsTarget(true);
        }
        else
        {
            GClass73.smethod_1();
        }
    }

    public bool method_15(int int_8, string string_2, int int_9)
    {
        if (sortedList_0 == null || !bool_2 || GPlayerSelf.Me.Mana < 0.2)
            return false;
        if (sortedList_0[int_8] == null)
            sortedList_0[int_8] = new SortedList();
        if (sortedList_0[int_8].ContainsKey(string_2))
        {
            var gclass12 = (GClass12)sortedList_0[int_8][string_2];
            if (!gclass12.gclass36_0.method_3())
                return false;
            method_14(int_8, string_2, null);
            gclass12.gclass36_0.method_4();
            return true;
        }

        var gclass12_1 = new GClass12(string_2, int_9);
        method_14(int_8, string_2, null);
        sortedList_0[int_8].Add(string_2, gclass12_1);
        return true;
    }

    public bool method_16(int int_8, string string_2)
    {
        if (sortedList_0 == null || !bool_2 || GPlayerSelf.Me.Mana < 0.2)
            return false;
        if (sortedList_0[int_8] == null)
            sortedList_0[int_8] = new SortedList();
        return !sortedList_0[int_8].ContainsKey(string_2) ||
               ((GClass12)sortedList_0[int_8][string_2]).gclass36_0.method_3();
    }

    public bool method_17(GUnit gunit_0)
    {
        GClass37.smethod_1("Assisting on target: " + gunit_0);
        if (int_7 != 0)
        {
            GClass37.smethod_1("Using assist key to pick up target");
            GClass42.gclass42_0.method_0("Common.TargetParty" + int_7);
            Thread.Sleep(100);
            GClass42.gclass42_0.method_0("Common.Assist");
            var gclass36 = new GClass36(2000);
            gclass36.method_4();
            while (!gclass36.method_3())
            {
                Thread.Sleep(100);
                if (GPlayerSelf.Me.TargetGUID == gunit_0.GUID)
                    return true;
            }

            GClass37.smethod_1("Never got target with assist key, using tab");
            return gunit_0.SetAsTarget(false);
        }

        GClass37.smethod_1("LeaderSlot is empty, attempting to pick up target with tab");
        return gunit_0.SetAsTarget(false);
    }
}