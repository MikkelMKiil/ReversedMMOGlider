// Decompiled with JetBrains decompiler
// Type: LootableCorpseTracker
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common.Objects;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

public class LootableCorpseTracker
{
    public bool bool_0;
    public bool bool_1;
    public bool bool_2;
    public DateTime dateTime_0;
    public GameTimer licenseCheckTimer;
    public GLocation glocation_0;
    protected int int_0;
    public long playerGuid;

    public LootableCorpseTracker(long long_1, bool bool_3, GLocation glocation_1, bool bool_4)
    {
        playerGuid = long_1;
        licenseCheckTimer = new GameTimer(60000);
        licenseCheckTimer.method_5();
        dateTime_0 = DateTime.Now;
        bool_0 = bool_3;
        glocation_0 = glocation_1;
        bool_1 = false;
        bool_2 = bool_4 && ConfigManager.gclass61_0.method_5("TurboLoot");
        int_0 = 0;
    }

    [SpecialName]
    public bool method_0()
    {
        return !bool_1 && int_0 < 3 && licenseCheckTimer.method_3();
    }

    public void method_1()
    {
        bool_1 = true;
    }

    public void method_2()
    {
        ++int_0;
        licenseCheckTimer.method_4();
    }

    public static void smethod_0(LootableCorpseTracker gclass5_0, string string_0)
    {
        if (!StartupClass.ProfileIdToProfileMap.ContainsKey(gclass5_0.playerGuid))
        {
            Logger.LoadProfile("Queueing new lootable corpse: 0x" + gclass5_0.playerGuid.ToString("x") + ", IsMine=" +
                               gclass5_0.bool_0 + ", name = \"" + string_0 + "\"");
            StartupClass.ProfileIdToProfileMap.Add(gclass5_0.playerGuid, gclass5_0);
        }
        else
        {
            StartupClass.ProfileIdToProfileMap[gclass5_0.playerGuid].glocation_0 = gclass5_0.glocation_0;
        }
    }

    public static bool LoadProfile()
    {
        var gclass5 = IsGroupProfile(GPlayerSelf.Me.Location);
        return gclass5 != null && gclass5.glocation_0.DistanceToSelf <
            (double)(StartupClass.CurrentGameClass.PullDistance + StartupClass.combatController.objectManagerBasePointer);
    }

    public static LootableCorpseTracker IsGroupProfile(GLocation glocation_1)
    {
        if (ConfigManager.gclass61_0.method_5("SkipLoot") || StartupClass.combatController.bool_5)
            return null;
        LootableCorpseTracker gclass5 = null;
        var num = 9999.0;
        foreach (var gclass5_0 in StartupClass.ProfileIdToProfileMap.Values)
            if (gclass5_0.method_0())
            {
                var unit = GObjectList.FindUnit(gclass5_0.playerGuid);
                if (unit != null && unit.IsValid)
                {
                    double distanceTo = unit.Location.GetDistanceTo(glocation_1);
                    if (unit.IsLootable || GetFileNameFromPath(unit, gclass5_0))
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

    public static void LoadSingleProfile()
    {
        LootableCorpseTracker gclass5_1 = null;
        foreach (var gclass5_2 in StartupClass.ProfileIdToProfileMap.Values)
            if ((DateTime.Now - gclass5_2.dateTime_0).TotalMinutes > 20.0)
            {
                gclass5_1 = gclass5_2;
                break;
            }

        if (gclass5_1 == null)
            return;
        StartupClass.ProfileIdToProfileMap.Remove(gclass5_1.playerGuid);
    }

    private static bool GetFileNameFromPath(GUnit gunit_0, LootableCorpseTracker gclass5_0)
    {
        return ConfigManager.gclass61_0.method_5("AutoSkin") && gunit_0.IsSkinnable && GPlayerSelf.Me.HasSkinning &&
               (gclass5_0.bool_0 ||
                (ConfigManager.gclass61_0.method_5("NinjaSkin") && ApplyConfig(gclass5_0.glocation_0) > 15.0));
    }

    private static double ApplyConfig(GLocation glocation_1)
    {
        var num = 999.0;
        foreach (var player in GObjectList.GetPlayers())
            if (player.GUID != GPlayerSelf.Me.GUID && !PartyManager.partyManager.method_13(player.GUID))
            {
                double distanceTo = player.Location.GetDistanceTo(glocation_1);
                if (distanceTo < num)
                    num = distanceTo;
            }

        return num;
    }

    public static void ParseDouble()
    {
        foreach (var monster in GObjectList.GetMonsters())
            if (monster.IsDead && (monster.IsLootable || monster.IsSkinnable))
                smethod_0(new LootableCorpseTracker(monster.GUID, false, monster.Location, false), monster.Name);
    }

    private bool method_3()
    {
        foreach (var unit in GObjectList.GetUnits())
            if (unit.DistanceToSelf < 10.0 && !unit.IsDead && unit.GUID != GContext.Main.Me.PetGUID &&
                unit.GUID != GPlayerSelf.Me.GUID && !PartyManager.partyManager.method_13(unit.GUID))
            {
                Logger.LoadProfile("Skipping TurboLoot, this guy is too close: " + unit);
                return false;
            }

        return true;
    }

    public bool method_4(GUnit gunit_0)
    {
        Logger.LogMessage("Turbo loot!");
        if (!method_3())
            return false;
        var gspellTimer1 = new GSpellTimer(4000, false);
        var gspellTimer2 = new GSpellTimer(700, false);
        var flag1 = false;
        var flag2 = false;
        while (!gspellTimer1.IsReady)
        {
            Logger.LoadProfile("LootFute loop top");
            if (GContext.Main.Me.IsUnderAttack || !gunit_0.Hover())
                return false;
            StartupClass.combatController.method_56();
            gspellTimer2.Reset();
            while (!gspellTimer2.IsReadySlow)
                if (gunit_0.WasLootable && !gunit_0.IsLootable)
                {
                    flag1 = true;
                    Logger.LogMessage("Lootable is gone, looks good");
                    break;
                }

            if (!flag1)
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
                        Logger.LogMessage("Unknown dialog visible during loot: \"" + DialogMonitor.string_0 +
                                           "\", dismissing");
                        DialogMonitor.IsGroupProfile();
                    }
                }
            }
            else
            {
                Logger.LoadProfile("Breaking out of LootFute");
                break;
            }
        }

        if (!SpellcastingManager.gclass42_0.method_15("Common.PostLoot"))
        {
            if (ConfigManager.gclass61_0.method_5("RunPostLoot"))
                StartupClass.combatController.bool_7 = true;
            else
                GContext.Main.CastSpell("Common.PostLoot");
        }

        ++StartupClass.expectedVersion;
        if (!ConfigManager.gclass61_0.method_5("AutoSkin"))
        {
            Logger.LoadProfile("Skinning disabled, won't try it");
            return true;
        }

        if (GContext.Main.T_Skinnable.GetCount(gunit_0.Name) < -2)
        {
            Logger.LoadProfile("This guy is never skinnable, won't try it");
            return true;
        }

        Logger.LoadProfile("Going to try skinning it, too!");
        gspellTimer1.Reset();
        var gspellTimer3 = new GSpellTimer(2000, false);
        while (!gspellTimer1.IsReady)
            if (!gspellTimer3.IsReady || gunit_0.IsSkinnable)
            {
                if (GContext.Main.Me.IsUnderAttack || !gunit_0.Hover())
                    return false;
                StartupClass.combatController.method_56();
                gspellTimer2.Reset();
                while (!gspellTimer2.IsReadySlow)
                    if (GContext.Main.Me.IsCasting)
                    {
                        flag2 = true;
                        Logger.LoadProfile("Channeling detected in skin paw");
                        break;
                    }

                if (flag2)
                    break;
            }
            else
            {
                Logger.LoadProfile("Been too long and monster is still not skinnable");
                break;
            }

        if (!flag2)
        {
            CombatController.LoadProfile();
            GContext.Main.T_Skinnable.Decrement(gunit_0.Name);
            return true;
        }

        GContext.Main.T_Skinnable.Increment(gunit_0.Name);
        var gspellTimer4 = new GSpellTimer(5000, false);
        Logger.LoadProfile("Waiting for skinning flag to clear");
        var flag3 = false;
        while (!gspellTimer4.IsReadySlow)
        {
            var num = GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("LootWindow"), "lwprobe");
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
                        Logger.LoadProfile("I stopped casting, skinning must be good (gulp!)");
                        Thread.Sleep(1371);
                        break;
                    }
                }
                else
                {
                    Logger.LoadProfile("Skinnable flag gone!");
                    break;
                }
            }
            else
            {
                Logger.LoadProfile("Loot window gone!");
                break;
            }
        }

        CombatController.LoadProfile();
        return true;
    }
}