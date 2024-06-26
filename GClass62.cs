﻿// Decompiled with JetBrains decompiler
// Type: GClass62
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.IO;
using System.Threading;
using Glider.Common.Objects;

public class GClass62
{
    private static bool bool_0;
    private static GGameCamera ggameCamera_0;
    private static float float_0;

    public static void smethod_0()
    {
        if (bool_0)
            Logger.LogMessage("Can't start another TestoThread, wait a minute!");
        else
            new Thread(smethod_1).Start();
    }

    private static void smethod_1()
    {
        try
        {
            bool_0 = true;
            Logger.LogMessage("TestoThread starting");
            if (!StartupClass.IsGliderInitialized && GClass61.gclass61_0.method_5("BackgroundEnable") &&
                StartupClass.GliderManager != null)
            {
                StartupClass.MainApplicationHandle = GProcessMemoryManipulator.smethod_29(StartupClass.AnotherIntegerValue);
                StartupClass.GliderManager.method_34(StartupClass.AnotherIntegerValue, StartupClass.MainApplicationHandle);
                StartupClass.IsGliderInitialized = true;
                Logger.LogMessage("Setting up for background mode!");
            }
            else
            {
                StartupClass.smethod_22();
                Thread.Sleep(2000);
            }

            if (StartupClass.GliderManager != null)
                StartupClass.GliderManager.method_33(true);
            smethod_52();
            Thread.Sleep(4000);
            StartupClass.gclass68_0.method_7();
            GClass55.smethod_21(false);
            if (StartupClass.GliderManager != null)
                StartupClass.GliderManager.method_33(false);
            Logger.LogMessage("TestoThread done");
        }
        catch (Exception ex)
        {
            Logger.LogMessage("! TestoThread exception: " + ex.Message + "\r\n" + ex.StackTrace);
        }
        finally
        {
            bool_0 = false;
        }
    }

    private static void smethod_2()
    {
        if (GContext.Main.Me.Target != null)
        {
            if (!StartupClass.IsGliderInitialized)
                Thread.Sleep(3000);
            var gspellTimer = new GSpellTimer(5000, false);
            StartupClass.gclass68_0.method_4(GContext.Main.Me.GetHeadingTo(GContext.Main.Me.Target));
            while (!gspellTimer.IsReady)
            {
                Thread.Sleep(20);
                if (StartupClass.gclass68_0.method_8(true))
                    break;
            }

            if (gspellTimer.IsReady)
                Logger.LogMessage("Futility in test mouse spin");
            StartupClass.gclass68_0.method_3(false);
        }
        else
        {
            Logger.LogMessage("Target something first, dummy");
        }
    }

    private static void smethod_3()
    {
        Logger.LogMessage("GPlayerSelf.Me.IsSitting: " + GPlayerSelf.Me.IsSitting);
    }

    private static void smethod_4()
    {
        if (ggameCamera_0 == null)
        {
            ggameCamera_0 = new GGameCamera();
            Logger.LogMessage("New gamera: " + ggameCamera_0);
            float_0 = ggameCamera_0.Pitch;
        }
        else
        {
            Logger.LogMessage("Moving camera to target pitch of: " + Math.Round(float_0, 3));
            StartupClass.gclass68_0.method_16(ggameCamera_0, float_0);
        }
    }

    private static void smethod_5()
    {
        if (GPlayerSelf.Me.Target == null)
        {
            Logger.LogMessage("no target, no test!");
        }
        else
        {
            Logger.LogMessage("My location: " + GPlayerSelf.Me.Location.ToString3D() + ", heading = " +
                               GPlayerSelf.Me.Heading);
            Logger.LogMessage("Target location: " + GPlayerSelf.Me.Target.Location.ToString3D());
            Logger.LogMessage("Heading to target: " +
                               GPlayerSelf.Me.Location.GetHeadingTo(GPlayerSelf.Me.Target.Location));
        }
    }

    private static void smethod_6()
    {
        smethod_7("MainHandSlot");
        smethod_7("SecondaryHandSlot");
    }

    private static void smethod_7(string string_0)
    {
        var equippedGuid = GContext.Main.Items.GetEquippedGUID(string_0);
        Logger.LogMessage("Item in \"" + string_0 + "\": 0x" + equippedGuid.ToString("x"));
        if (equippedGuid == 0L)
            return;
        GContext.Main.Items.DebugItem(equippedGuid);
    }

    private static void smethod_8()
    {
        var flag1 = false;
        while (GPlayerSelf.Me.IsInCombat)
        {
            bool flag2;
            if ((flag2 = GContext.Main.Interface.IsKeyEnabled("Rogue.Riposte")) != flag1)
            {
                Logger.LogMessage("New riposte enable state: " + flag2);
                flag1 = flag2;
            }
        }

        Logger.LogMessage("Player left combat, bailing out of test loop");
    }

    private static void smethod_9()
    {
        var gclass8_1 = GClass8.smethod_2("UIParent");
        var gclass8_2 = GClass8.smethod_2("StaticPopup1");
        Logger.LogMessage("parent: " + gclass8_1.method_2());
        Logger.LogMessage("sp1: " + gclass8_2.method_2());
    }

    private static void smethod_10()
    {
        Logger.LogMessage("Stealthed: " + GPlayerSelf.Me.HasWellKnownBuff("Stealth"));
    }

    private static void smethod_11()
    {
        GContext.Main.CastSpell("Priest.Shield");
        GContext.Main.CastSpell("Priest.MindBlast");
    }

    private static void smethod_12()
    {
        var num1 = -1;
        while (true)
        {
            int num2;
            do
            {
                Thread.Sleep(100);
                num2 = GProcessMemoryManipulator.smethod_11(11257960, "bst");
            } while (num2 == num1);

            Logger.LogMessage("new value for bst: " + num2);
            num1 = num2;
        }
    }

    private static void smethod_13()
    {
        foreach (object obj in GObjectList.GetObjects())
            Logger.LogMessage(obj.ToString());
        var bagContents1 = GPlayerSelf.Me.BagContents;
        for (var index = 0; index < bagContents1.Length; ++index)
            Logger.LogMessage("Backpack/" + index + " = 0x" + bagContents1[index].ToString("x"));
        foreach (var bag in GPlayerSelf.Me.Bags)
        {
            var gcontainer = (GContainer)GObjectList.FindObject(bag);
            if (gcontainer != null)
            {
                Logger.LogMessage(gcontainer.ToString());
                var bagContents2 = gcontainer.BagContents;
                for (var index = 0; index < bagContents2.Length; ++index)
                {
                    var str = "0x" + bagContents2[index].ToString("x");
                    if (bagContents2[index] != 0L)
                        str = ((GItem)GObjectList.FindObject(bagContents2[index])).ToString();
                    Logger.LogMessage("Bag " + bag.ToString("x") + "/" + index + " = " + str);
                }
            }
            else
            {
                Logger.LogMessage("No bag for this ID: 0x" + bag.ToString("x"));
            }
        }

        Logger.LogMessage("ObjectList test done");
    }

    private static void smethod_14()
    {
        if (GPlayerSelf.Me.HasAmmo)
            Logger.LogMessage("I have ammo!");
        else
            Logger.LogMessage("Out of Ammo--Help!");
    }

    private static void smethod_15()
    {
        while (true)
        {
            Logger.LogMessage("Mob Heading: " + GPlayerSelf.Me.Target.Bearing);
            if (GPlayerSelf.Me.Target.Bearing >= Math.PI / 2.0 || GPlayerSelf.Me.Target.Bearing <= -1.0 * Math.PI / 2.0)
                Logger.LogMessage("BACK");
            else
                Logger.LogMessage("FRONT");
        }
    }

    private static void smethod_16()
    {
        if (GContext.Main.Interface.IsKeyEnabled("Common.Potion"))
            Logger.LogMessage("Key Enabled");
        else
            Logger.LogMessage("Key Disabled");
    }

    private static void smethod_17()
    {
        var unit = GObjectList.FindUnit("Innkeeper Heather");
        if (unit == null)
        {
            GContext.Main.Log("Never found vendor in test code, oh well");
        }
        else
        {
            unit.Approach(3.0);
            unit.Interact();
            if (GPlayerSelf.Me.Target != unit)
            {
                GContext.Main.Log("Never managed to click on vendor in test code, oh well");
            }
            else
            {
                var gmerchant = new GMerchant();
                if (!gmerchant.IsVisible)
                    return;
                if (gmerchant.IsRepairEnabled)
                    gmerchant.ClickRepairButton();
                if (!gmerchant.BuyOnAnyPage("Longjaw Mud Snapper"))
                    GContext.Main.Log("!! Unable to buy from vendor");
                else
                    GContext.Main.Log("Bought test item  successfully");
                gmerchant.Close();
            }
        }
    }

    private static void smethod_18()
    {
        while (GPlayerSelf.Me.Target != null)
        {
            Thread.Sleep(1000);
            GContext.Main.Log("Populated/Firing/Ready: " + GContext.Main.Interface.IsKeyPopulated("Warlock.Wand") +
                              "/" + GContext.Main.Interface.IsKeyFiring("Warlock.Wand") + "/" +
                              GContext.Main.Interface.IsKeyReady("Warlock.Wand"));
        }

        GContext.Main.Log("Wand test all done");
    }

    private static void smethod_19()
    {
        while (GPlayerSelf.Me.Target != null)
        {
            var target = GPlayerSelf.Me.Target;
            Thread.Sleep(200);
            GContext.Main.Log("Casting/Energy/Ready: " + target.IsCasting + "/" + GPlayerSelf.Me.Energy + "/" +
                              GContext.Main.Interface.IsKeyReady("Rogue.Kick"));
        }
    }

    private static void smethod_20()
    {
        var byNamePreWorld = GContext.Main.Interface.GetByNamePreWorld("GlueDialog");
        if (byNamePreWorld == null)
        {
            GContext.Main.Log("Could not find GlueDialog!  Are you sitting at login screen?");
        }
        else if (!byNamePreWorld.IsVisible)
        {
            GContext.Main.Log("GlueDialog is not visible");
        }
        else
        {
            var labelText = GContext.Main.Interface.GetByNamePreWorld("GlueDialogBackground")
                .GetChildObject("GlueDialogText").LabelText;
            GContext.Main.Log("Label text: \"" + labelText + "\"");
        }
    }

    private static void smethod_21()
    {
        var byKeyName = GContext.Main.Interface.GetByKeyName("Common.Eat");
        if (byKeyName == null)
            return;
        GContext.Main.Log("Food interface object: " + byKeyName);
    }

    private static void smethod_22()
    {
        foreach (var gobject in GObjectList.GetObjects())
            if (gobject.Type == GObjectType.Item && ((GItem)gobject).IsEquipped)
                Logger.LogMessage(gobject.ToString());
        var bagContents1 = GPlayerSelf.Me.BagContents;
        for (var index = 0; index < bagContents1.Length; ++index)
            Logger.LogMessage("Backpack/" + index + " = 0x" + bagContents1[index].ToString("x"));
        foreach (var bag in GPlayerSelf.Me.Bags)
        {
            var gcontainer = (GContainer)GObjectList.FindObject(bag);
            if (gcontainer != null)
            {
                Logger.LogMessage(gcontainer.ToString());
                var bagContents2 = gcontainer.BagContents;
                for (var index = 0; index < bagContents2.Length; ++index)
                    Logger.LogMessage("Bag" + bag.ToString("x") + "/" + index + " = 0x" +
                                       bagContents2[index].ToString("x"));
            }
            else
            {
                Logger.LogMessage("No bag for this ID: 0x" + bag.ToString("x"));
            }
        }

        Logger.LogMessage("ObjectList test done");
    }

    private static void smethod_23()
    {
        Logger.LogMessage("Put cursor on item!");
        Thread.Sleep(3000);
        Logger.LogMessage("Cursor Type: " +
                           GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("CursorType"), "CursorType"));
    }

    private static void smethod_24()
    {
        Logger.LogMessage("Testing Creature Type:");
        Logger.LogMessage(GPlayerSelf.Me.Target.CreatureType.ToString());
        Logger.LogMessage("Testing Player Type:");
        Logger.LogMessage(GPlayerSelf.Me.CreatureType.ToString());
        Logger.LogMessage("IsDead :" + GPlayerSelf.Me.IsDead);
    }

    private static void smethod_25()
    {
        var target = GPlayerSelf.Me.Target;
        if (target == null)
        {
            Logger.LogMessage("Target something first!");
        }
        else
        {
            var buffSnapshot = target.GetBuffSnapshot(false);
            Logger.LogMessage("Retrieved " + buffSnapshot.Length + " buffs for target");
            foreach (object obj in buffSnapshot)
                Logger.LogMessage(obj.ToString());
        }
    }

    private static void smethod_26()
    {
        var target = GPlayerSelf.Me.Target;
        if (target == null)
            Logger.LogMessage("Target something first!");
        else
            Logger.LogMessage("Raid target icon is: " + target.RaidTargetIcon);
    }

    private static void smethod_27()
    {
        var buffSnapshot = GPlayerSelf.Me.Target.GetBuffSnapshot(false);
        Logger.LogMessage("Retrieved " + buffSnapshot.Length + " buffs for self");
        foreach (object obj in buffSnapshot)
            Logger.LogMessage(obj.ToString());
    }

    private static void smethod_28()
    {
        Logger.LogMessage("Profile has Z stuff: " + GContext.Main.Profile.AllCoordsHaveZ());
    }

    private static void smethod_29()
    {
        Logger.LogMessage("Starting Object Test 3..2..1");
        var objects = GObjectList.GetObjects();
        Logger.LogMessage("Node Count: " + objects.Length);
        foreach (var gobject in objects)
            Logger.LogMessage("object Type: " + gobject.Type + " | Name: " + gobject.Name);
    }

    private static void smethod_30()
    {
        var byName = GContext.Main.Interface.GetByName("QuestFrame");
        var childObject = GContext.Main.Interface.GetByName("QuestDetailScrollChildFrame")
            .GetChildObject("QuestTitleText");
        Thread.Sleep(777);
        if (!byName.IsVisible)
        {
            Logger.LogMessage("No quest frame, skipping gossip check");
        }
        else
        {
            if (byName.IsVisible)
                Logger.LogMessage("Quest frame visible, skipping gossip check");
            Logger.LogMessage(childObject.LabelText);
            File.WriteAllLines("mybiglist.txt", GInterfaceHelper.GetAllInterfaceObjectNames());
        }
    }

    private static void smethod_31()
    {
        for (var SlotNumber = 1; SlotNumber <= 130; ++SlotNumber)
        {
            var gshortcut = new GShortcut(SlotNumber);
            switch (gshortcut.ShortcutType)
            {
                case GShortcutType.Spell:
                    Logger.LogMessage("Slot #" + SlotNumber + ": Spell, definition = 0x" +
                                       gshortcut.ShortcutValue.ToString("x") + ", details = " +
                                       GSpells.GetSpellName(gshortcut.ShortcutValue));
                    break;
                case GShortcutType.Item:
                    Logger.LogMessage("Slot #" + SlotNumber + ": Item, definition = 0x" +
                                       gshortcut.ShortcutValue.ToString("x") + ", details = " +
                                       new GItemDefinition(gshortcut.ShortcutValue).Name);
                    break;
                case GShortcutType.Macro:
                    Logger.LogMessage("Slot #" + SlotNumber + ": Macro, number = 0x" +
                                       gshortcut.ShortcutValue.ToString("x"));
                    break;
            }
        }
    }

    private static void smethod_32()
    {
        GClass42.gclass42_0.sortedList_0["Rogue.Sinister"].FilloutKey();
    }

    private static void smethod_33()
    {
        var flag1 = GContext.Main.Interface.IsKeyReady("Rogue.Stealth");
        var gspellTimer = new GSpellTimer(30000, false);
        Logger.LogMessage("Starting state: " + flag1);
        while (!gspellTimer.IsReadySlow)
        {
            bool flag2;
            if ((flag2 = GContext.Main.Interface.IsKeyReady("Rogue.Stealth")) != flag1)
            {
                Logger.LogMessage("New cd state: " + flag2);
                flag1 = flag2;
            }
        }
    }

    private static void smethod_34()
    {
        Logger.LogMessage("My stance is: " + GPlayerSelf.Me.Stance);
    }

    private static void smethod_35()
    {
        var visibleInterfaceObject = GClass42.gclass42_0.sortedList_0["Rogue.Sinister"].FindVisibleInterfaceObject();
        if (visibleInterfaceObject == null)
        {
            Logger.LogMessage("Could not find visible object for sinister strike");
        }
        else
        {
            Logger.LogMessage("Found it: \"" + visibleInterfaceObject + "\"");
            GClass8.smethod_2(visibleInterfaceObject).method_16(false);
        }
    }

    private static void smethod_36()
    {
        GClass42.gclass42_0.method_23();
        GClass42.gclass42_0.sortedList_0["Common.Potion"].FilloutKey();
        Logger.LogMessage("Potion ready flag: " + GContext.Main.Interface.IsKeyReady("Common.Potion"));
    }

    private static void smethod_37()
    {
        GClass42.gclass42_0.method_23();
        foreach (var gkey in GClass42.gclass42_0.sortedList_0.Values)
            if (gkey.KeyName.StartsWith("Common") || gkey.KeyName.StartsWith(GPlayerSelf.Me.PlayerClass.ToString()))
                gkey.FilloutKey();
    }

    private static void smethod_38()
    {
        if (GPlayerSelf.Me.Target == null)
        {
            Logger.LogMessage("no target, no test!");
        }
        else
        {
            var target = GPlayerSelf.Me.Target;
            if (target == null)
                GContext.Main.Log("Never found NPC");
            else
                Logger.LogMessage("NPC Title: " + target.Title);
        }
    }

    private static void smethod_39()
    {
        Logger.LogMessage("The count is: " + GContext.Main.Interface.GetActionInventory("Common.Test"));
    }

    private static void smethod_40()
    {
        if (GPlayerSelf.Me.Target == null)
        {
            Logger.LogMessage("Target something first");
        }
        else
        {
            double double_1;
            double double_2;
            if (GClass6.smethod_0(GPlayerSelf.Me.Target.Location, 0.0, out double_1, out double_2))
            {
                Logger.LogMessage("Conversion good, positioning cursor");
                GClass55.smethod_18(double_1, double_2);
                Thread.Sleep(1000);
                GClass55.smethod_23(true);
            }
            else
            {
                Logger.LogMessage("Conversion no good, check log");
            }
        }
    }

    private static void smethod_41()
    {
        if (GPlayerSelf.Me.Target == null)
            Logger.LogMessage("Target something first");
        else
            GPlayerSelf.Me.Target.Interact();
    }

    private static void smethod_42()
    {
        GClass55.smethod_28("/w dirtymeat woo! YeAhhh!  Money$$!!??");
    }

    private static void smethod_43()
    {
        foreach (var quest in GPlayerSelf.Me.Quests)
            Logger.LogMessage("Quest: 0x" + quest.ToString("x"));
        Logger.LogMessage("Cutting teeth quest: " + GPlayerSelf.Me.IsOnQuest(788));
    }

    private static void smethod_44()
    {
        var memory = GContext.Main.Memory;
        Logger.LogMessage("Byte at test addr: " + memory.ReadByte(7146512, "memtestread").ToString("x"));
        var DataToWrite = new byte[1] { 204 };
        memory.WriteBytes(7146512, DataToWrite, 1, "memtestwrite");
        Logger.LogMessage("Now byte is: " + memory.ReadByte(7146512, "memtestread2").ToString("x"));
    }

    private static void smethod_45()
    {
        Logger.LogMessage("My move flags1: 0x" + GPlayerSelf.Me.MovementFlags1.ToString("x"));
        Logger.LogMessage("My move flags2: 0x" + GPlayerSelf.Me.MovementFlags2.ToString("x"));
    }

    private static void smethod_46()
    {
        GInterfaceHelper.DumpUIDebug(true);
    }

    private static void smethod_47()
    {
        var byNamePreWorld = GContext.Main.Interface.GetByNamePreWorld("CharacterSelectCharacterFrame");
        if (byNamePreWorld != null && byNamePreWorld.IsVisible)
        {
            Logger.LogMessage(byNamePreWorld + ", child count: " + byNamePreWorld.Children.Length);
            for (var index = 0; index < byNamePreWorld.Children.Length; ++index)
                Logger.LogMessage(index + ": " + byNamePreWorld.Children[index]);
        }
        else
        {
            Logger.LogMessage("No CharacterSelectCharacterFrame visible!");
        }
    }

    public static void smethod_48(GInterfaceObject ginterfaceObject_0)
    {
        if (ginterfaceObject_0 == null || !ginterfaceObject_0.IsVisible)
            return;
        GContext.Main.EnableCursorHook();
        ginterfaceObject_0.Hover();
        GContext.Main.DisableCursorHook();
    }

    private static void smethod_49()
    {
        for (var index = 1; index < 65; ++index)
        {
            var byName = GContext.Main.Interface.GetByName("TaxiButton" + index);
            if (byName != null && byName.IsVisible)
            {
                smethod_48(byName);
                var childObject = GContext.Main.Interface.GetByName("GameTooltip")
                    .GetChildObject("GameTooltipTextLeft1");
                Logger.LogMessage("Tooltip for " + index + " sez \"" + childObject.LabelText + "\"");
            }
            else if (byName == null)
            {
                Logger.LogMessage("Stopping because button " + index + " was not found");
                break;
            }
        }
    }

    private static void smethod_50()
    {
        GContext.Main.Interface.GetByName("ActionButton12").Hover();
    }

    private static void smethod_51()
    {
        for (var index = 1; index <= 32; ++index)
        {
            var byName = GContext.Main.Interface.GetByName("GossipTitleButton" + index);
            if (byName != null && byName.IsVisible)
                GContext.Main.Log("GossipTitleButton #" + index + " => " + byName.LabelText);
        }
    }

    private static void smethod_52()
    {
        smethod_40();
    }
}