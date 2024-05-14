// Decompiled with JetBrains decompiler
// Type: GClass5
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Glider.Common.Objects;

public class GClass5
{
    public bool bool_0;
    public bool bool_1;
    public bool bool_2;
    public DateTime dateTime_0;
    public GClass36 gclass36_0;
    public GLocation glocation_0;
    protected int int_0;
    public long long_0;

    public GClass5(long long_1, bool bool_3, GLocation glocation_1, bool bool_4)
    {
        long_0 = long_1;
        gclass36_0 = new GClass36(60000);
        gclass36_0.method_5();
        dateTime_0 = DateTime.Now;
        bool_0 = bool_3;
        glocation_0 = glocation_1;
        bool_1 = false;
        bool_2 = bool_4 && GClass61.gclass61_0.method_5("TurboLoot");
        int_0 = 0;
    }

    [SpecialName]
    public bool method_0()
    {
        return !bool_1 && int_0 < 3 && gclass36_0.method_3();
    }

    public void method_1()
    {
        bool_1 = true;
    }

    public void method_2()
    {
        ++int_0;
        gclass36_0.method_4();
    }

    public static void smethod_0(GClass5 gclass5_0, string string_0)
    {
        if (!StartupClass.ProfileMap.ContainsKey(gclass5_0.long_0))
        {
            GClass37.smethod_1("Queueing new lootable corpse: 0x" + gclass5_0.long_0.ToString("x") + ", IsMine=" +
                               gclass5_0.bool_0 + ", name = \"" + string_0 + "\"");
            StartupClass.ProfileMap.Add(gclass5_0.long_0, gclass5_0);
        }
        else
        {
            StartupClass.ProfileMap[gclass5_0.long_0].glocation_0 = gclass5_0.glocation_0;
        }
    }

    public static bool smethod_1()
    {
        var gclass5 = smethod_2(GPlayerSelf.Me.Location);
        return gclass5 != null && gclass5.glocation_0.DistanceToSelf <
            (double)(StartupClass.ggameClass_0.PullDistance + StartupClass.gclass73_0.int_5);
    }

    public static GClass5 smethod_2(GLocation glocation_1)
    {
        if (GClass61.gclass61_0.method_5("SkipLoot") || StartupClass.gclass73_0.bool_5)
            return null;
        GClass5 gclass5 = null;
        var num = 9999.0;
        foreach (var gclass5_0 in StartupClass.ProfileMap.Values)
            if (gclass5_0.method_0())
            {
                var unit = GObjectList.FindUnit(gclass5_0.long_0);
                if (unit != null && unit.IsValid)
                {
                    double distanceTo = unit.Location.GetDistanceTo(glocation_1);
                    if (unit.IsLootable || smethod_4(unit, gclass5_0))
                    {
                        GContext.Main.IsHostileNear(unit.Location);
                        if (!GContext.Main.IsHostileNear(unit.Location) && (gclass5 == null || distanceTo < num))
                        {
                            gclass5 = gclass5_0;
                            num = distanceTo;
                        }
                    }
                }
                else
                {
                    gclass5_0.method_2();
                }
            }

        return gclass5;
    }

    public static void smethod_3()
    {
        GClass5 gclass5_1 = null;
        foreach (var gclass5_2 in StartupClass.ProfileMap.Values)
            if ((DateTime.Now - gclass5_2.dateTime_0).TotalMinutes > 20.0)
            {
                gclass5_1 = gclass5_2;
                break;
            }

        if (gclass5_1 == null)
            return;
        StartupClass.ProfileMap.Remove(gclass5_1.long_0);
    }

    private static bool smethod_4(GUnit gunit_0, GClass5 gclass5_0)
    {
        return GClass61.gclass61_0.method_5("AutoSkin") && gunit_0.IsSkinnable && GPlayerSelf.Me.HasSkinning &&
               (gclass5_0.bool_0 ||
                (GClass61.gclass61_0.method_5("NinjaSkin") && smethod_5(gclass5_0.glocation_0) > 15.0));
    }

    private static double smethod_5(GLocation glocation_1)
    {
        var num = 999.0;
        foreach (var player in GObjectList.GetPlayers())
            if (player.GUID != GPlayerSelf.Me.GUID && !GClass54.gclass54_0.method_13(player.GUID))
            {
                double distanceTo = player.Location.GetDistanceTo(glocation_1);
                if (distanceTo < num)
                    num = distanceTo;
            }

        return num;
    }

    public static void smethod_6()
    {
        foreach (var monster in GObjectList.GetMonsters())
            if (monster.IsDead && (monster.IsLootable || monster.IsSkinnable))
                smethod_0(new GClass5(monster.GUID, false, monster.Location, false), monster.Name);
    }

    private bool method_3()
    {
        foreach (var unit in GObjectList.GetUnits())
            if (unit.DistanceToSelf < 10.0 && !unit.IsDead && unit.GUID != GContext.Main.Me.PetGUID &&
                unit.GUID != GPlayerSelf.Me.GUID && !GClass54.gclass54_0.method_13(unit.GUID))
            {
                GClass37.smethod_1("Skipping TurboLoot, this guy is too close: " + unit);
                return false;
            }

        return true;
    }

    public bool method_4(GUnit gunit_0)
    {
        GClass37.smethod_0("Turbo loot!");
        if (!method_3())
            return false;
        var gspellTimer1 = new GSpellTimer(4000, false);
        var gspellTimer2 = new GSpellTimer(700, false);
        var flag1 = false;
        var flag2 = false;
        while (!gspellTimer1.IsReady)
        {
            GClass37.smethod_1("LootFute loop top");
            if (GContext.Main.Me.IsUnderAttack || !gunit_0.Hover())
                return false;
            StartupClass.gclass73_0.method_56();
            gspellTimer2.Reset();
            while (!gspellTimer2.IsReadySlow)
                if (gunit_0.WasLootable && !gunit_0.IsLootable)
                {
                    flag1 = true;
                    GClass37.smethod_0("Lootable is gone, looks good");
                    break;
                }

            if (!flag1)
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
                        GClass37.smethod_0("Unknown dialog visible during loot: \"" + GClass67.string_0 +
                                           "\", dismissing");
                        GClass67.smethod_2();
                    }
                }
            }
            else
            {
                GClass37.smethod_1("Breaking out of LootFute");
                break;
            }
        }

        if (!GClass42.gclass42_0.method_15("Common.PostLoot"))
        {
            if (GClass61.gclass61_0.method_5("RunPostLoot"))
                StartupClass.gclass73_0.bool_7 = true;
            else
                GContext.Main.CastSpell("Common.PostLoot");
        }

        ++StartupClass.int_8;
        if (!GClass61.gclass61_0.method_5("AutoSkin"))
        {
            GClass37.smethod_1("Skinning disabled, won't try it");
            return true;
        }

        if (GContext.Main.T_Skinnable.GetCount(gunit_0.Name) < -2)
        {
            GClass37.smethod_1("This guy is never skinnable, won't try it");
            return true;
        }

        GClass37.smethod_1("Going to try skinning it, too!");
        gspellTimer1.Reset();
        var gspellTimer3 = new GSpellTimer(2000, false);
        while (!gspellTimer1.IsReady)
            if (!gspellTimer3.IsReady || gunit_0.IsSkinnable)
            {
                if (GContext.Main.Me.IsUnderAttack || !gunit_0.Hover())
                    return false;
                StartupClass.gclass73_0.method_56();
                gspellTimer2.Reset();
                while (!gspellTimer2.IsReadySlow)
                    if (GContext.Main.Me.IsCasting)
                    {
                        flag2 = true;
                        GClass37.smethod_1("Channeling detected in skin paw");
                        break;
                    }

                if (flag2)
                    break;
            }
            else
            {
                GClass37.smethod_1("Been too long and monster is still not skinnable");
                break;
            }

        if (!flag2)
        {
            GClass73.smethod_1();
            GContext.Main.T_Skinnable.Decrement(gunit_0.Name);
            return true;
        }

        GContext.Main.T_Skinnable.Increment(gunit_0.Name);
        var gspellTimer4 = new GSpellTimer(5000, false);
        GClass37.smethod_1("Waiting for skinning flag to clear");
        var flag3 = false;
        while (!gspellTimer4.IsReadySlow)
        {
            var num = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("LootWindow"), "lwprobe");
            if (num != 0 || !flag3)
            {
                if (num != 0)
                    flag3 = true;
                if (gunit_0.IsSkinnable)
                {
                    if (GContext.Main.Me.IsUnderAttack)
                        return false;
                    if (!GPlayerSelf.Me.IsCasting)
                    {
                        GClass37.smethod_1("I stopped casting, skinning must be good (gulp!)");
                        Thread.Sleep(1371);
                        break;
                    }
                }
                else
                {
                    GClass37.smethod_1("Skinnable flag gone!");
                    break;
                }
            }
            else
            {
                GClass37.smethod_1("Loot window gone!");
                break;
            }
        }

        GClass73.smethod_1();
        return true;
    }
}