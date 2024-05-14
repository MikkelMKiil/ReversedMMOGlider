// Decompiled with JetBrains decompiler
// Type: GClass21
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Glider.Common;
using Glider.Common.Objects;

public class GClass21
{
    private static readonly List<GClass21> list_0 = new List<GClass21>();
    public static DateTime dateTime_1 = DateTime.Now;
    private static int int_0;
    private static int int_1;
    public bool bool_0;
    public bool bool_1;
    public DateTime dateTime_0;
    public GPlayer gplayer_0;

    public GClass21(GPlayer gplayer_1)
    {
        bool_0 = false;
        dateTime_0 = DateTime.Now;
        gplayer_0 = gplayer_1;
    }

    public static void smethod_0()
    {
        int_0 = (int)(StartupClass.smethod_6(GClass61.gclass61_0.method_2("FriendAlert")) * 60.0);
        int_1 = (int)(StartupClass.smethod_6(GClass61.gclass61_0.method_2("FriendLogout")) * 60.0);
        list_0.Clear();
    }

    public static GClass21[] smethod_1()
    {
        lock (list_0)
        {
            return list_0.ToArray();
        }
    }

    public static void smethod_2()
    {
        lock (list_0)
        {
            foreach (var gclass21 in list_0)
                gclass21.bool_1 = true;
            var players = GObjectList.GetPlayers();
            if ((DateTime.Now - StartupClass.dateTime_0).TotalMinutes >= 20.0 &&
                GClass61.gclass61_0.method_2("AppKey").Length < 8)
                StartupClass.gprofile_0.Waypoints.Clear();
            foreach (var gplayer_1 in players)
            {
                double distanceToSelf = gplayer_1.DistanceToSelf;
                if (gplayer_1.GUID != StartupClass.long_0 && !smethod_7(gplayer_1.Name))
                {
                    var gclass21 = smethod_5(gplayer_1.GUID);
                    if (gclass21 == null)
                    {
                        GClass37.smethod_0(GClass30.smethod_2(641, gplayer_1.Name, gplayer_1.PlayerRace.ToString(),
                            gplayer_1.PlayerClass.ToString()));
                        list_0.Add(new GClass21(gplayer_1));
                    }
                    else
                    {
                        gclass21.bool_1 = false;
                        if (int_0 > 0 && (DateTime.Now - gclass21.dateTime_0).TotalSeconds >= int_0 && !gclass21.bool_0)
                        {
                            gclass21.bool_0 = true;
                            GClass37.smethod_0(GClass30.smethod_2(642, int_0, gplayer_1.Name,
                                gplayer_1.GUID.ToString("x")));
                            GClass20.smethod_0("PlayerNear.wav");
                            GClass48.smethod_1();
                        }

                        if (int_1 > 0 && (DateTime.Now - gclass21.dateTime_0).TotalSeconds >= int_1)
                        {
                            GClass37.smethod_0(GClass30.smethod_2(643, int_1, gplayer_1.Name,
                                gplayer_1.GUID.ToString("x")));
                            GClass20.smethod_0("PlayerNear.wav");
                            StartupClass.gclass73_0.method_21(true);
                        }
                    }
                }
            }

            do
            {
                ;
            } while (smethod_6());
        }
    }

    [SpecialName]
    public static bool smethod_3()
    {
        foreach (var gclass21 in list_0)
            if (gclass21.gplayer_0.IsValid && gclass21.gplayer_0.IsSameFaction)
                return true;
        return false;
    }

    [SpecialName]
    public static bool smethod_4()
    {
        foreach (var gclass21 in list_0)
            if (gclass21.gplayer_0.IsValid && !gclass21.gplayer_0.IsSameFaction)
                return true;
        return false;
    }

    private static GClass21 smethod_5(long long_0)
    {
        foreach (var gclass21 in list_0)
            if (gclass21.gplayer_0.GUID == long_0)
                return gclass21;
        return null;
    }

    private static bool smethod_6()
    {
        for (var index = 0; index < list_0.Count; ++index)
        {
            var gclass21 = list_0[index];
            if (gclass21.bool_1)
            {
                GClass37.smethod_0(GClass30.smethod_2(55, gclass21.gplayer_0.Name));
                list_0.RemoveAt(index);
                return true;
            }
        }

        return false;
    }

    private static bool smethod_7(string string_0)
    {
        switch (StartupClass.gclass54_0.genum7_0)
        {
            case GEnum7.const_1:
                if (string_0.ToLower() == GClass61.gclass61_0.method_2("PartyMember1").ToLower() ||
                    string_0.ToLower() == GClass61.gclass61_0.method_2("PartyMember2").ToLower() ||
                    string_0.ToLower() == GClass61.gclass61_0.method_2("PartyMember3").ToLower() ||
                    string_0.ToLower() == GClass61.gclass61_0.method_2("PartyMember4").ToLower())
                    return true;
                break;
            case GEnum7.const_2:
                if (string_0.ToLower() == GClass61.gclass61_0.method_2("PartyLeaderName").ToLower())
                    return true;
                break;
        }

        if (StartupClass.string_8 != null && StartupClass.string_8.Length > 0)
            for (var index = 0; index < StartupClass.string_8.Length; ++index)
                if (StartupClass.string_8[index].ToUpper() == string_0.ToUpper())
                    return true;
        return false;
    }

    public static bool smethod_8(GUnit gunit_0, string string_0, bool bool_2)
    {
        var gmonster = (GMonster)gunit_0;
        if (!gmonster.IsTagged || gmonster.IsMine || StartupClass.glideMode_0 != GlideMode.Auto ||
            GClass61.gclass61_0.method_5("BypassTagCheck") || StartupClass.gclass73_0.bool_1)
            return true;
        StartupClass.gclass73_0.bool_1 = true;
        GClass20.smethod_0("BadTag.wav");
        ++StartupClass.SomeIntegerValue;
        GClass42.gclass42_0.method_0("Common.PetFollow");
        if (StartupClass.SomeIntegerValue >= GClass61.gclass61_0.method_3("BadTagLimit"))
        {
            StartupClass.gclass73_0.bool_2 = true;
            GClass37.smethod_0(GClass30.smethod_1(808));
        }

        if (!gmonster.IsTargetingMe && !GClass61.gclass61_0.method_5("IgnoreTags"))
        {
            GClass37.smethod_0(GClass30.smethod_2(805, gmonster.Name));
            GClass73.smethod_1();
            return false;
        }

        GClass37.smethod_0(GClass30.smethod_2(806, gmonster.Name));
        if (bool_2)
            GClass42.gclass42_0.method_0("Common.Back");
        if (string_0 != null)
            GContext.Main.CastSpell(string_0);
        var gspellTimer1 = new GSpellTimer(3000, false);
        var gspellTimer2 = new GSpellTimer(1200, false);
        while (!gspellTimer1.IsReadySlow)
            if (gmonster.IsTargetingMe)
            {
                if (gspellTimer2.IsReady)
                {
                    GClass42.gclass42_0.method_1("Common.Back");
                    Thread.Sleep(400);
                    GClass42.gclass42_0.method_2("Common.Back");
                    gspellTimer2.Reset();
                }
            }
            else
            {
                GClass37.smethod_0(GClass30.smethod_2(806, gmonster.Name));
                GClass73.smethod_1();
                return false;
            }

        GClass37.smethod_0(GClass30.smethod_2(807, gmonster.Name));
        return true;
    }
}