// Decompiled with JetBrains decompiler
// Type: TestThread
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common.Objects;
using System;
using System.IO;
using System.Threading;

public class TestThread
{
    private static bool bool_0;
    private static GGameCamera ggameCamera_0;
    private static float float_0;

    public static void smethod_0()
    {
        if (bool_0)
            Logger.LogMessage("Can't start another TestoThread, wait a minute!");
        else
            new Thread(LoadProfile).Start();
    }

    private static void LoadProfile()
    {
        try
        {
            bool_0 = true;
            Logger.LogMessage("TestoThread starting");
            if (!StartupClass.IsGliderInitialized && ConfigManager.gclass61_0.method_5("BackgroundEnable") &&
                StartupClass.GliderManager != null)
            {
                StartupClass.MainApplicationHandle = GProcessMemoryManipulator.OpenProcessWithAccess(StartupClass.AnotherIntegerValue);
                StartupClass.GliderManager.method_34(StartupClass.AnotherIntegerValue, StartupClass.MainApplicationHandle);
                StartupClass.IsGliderInitialized = true;
                Logger.LogMessage("Setting up for background mode!");
            }
            else
            {
                StartupClass.BringGameWindowToForeground();
                Thread.Sleep(2000);
            }

            if (StartupClass.GliderManager != null)
                StartupClass.GliderManager.method_33(true);
            ApplyLnCommandLineArg();
            Thread.Sleep(4000);
            StartupClass.cameraRotator.method_7();
            InputController.StartManualGlide(false);
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

    private static void IsGroupProfile()
    {
        if (GContext.Main.Me.Target != null)
        {
            if (!StartupClass.IsGliderInitialized)
                Thread.Sleep(3000);
            var gspellTimer = new GSpellTimer(5000, false);
            StartupClass.cameraRotator.method_4(GContext.Main.Me.GetHeadingTo(GContext.Main.Me.Target));
            while (!gspellTimer.IsReady)
            {
                Thread.Sleep(20);
                if (StartupClass.cameraRotator.method_8(true))
                    break;
            }

            if (gspellTimer.IsReady)
                Logger.LogMessage("Futility in test mouse spin");
            StartupClass.cameraRotator.method_3(false);
        }
        else
        {
            Logger.LogMessage("Target something first, dummy");
        }
    }

    private static void LoadSingleProfile()
    {
        Logger.LogMessage("GPlayerSelf.Me.IsSitting: " + GPlayerSelf.Me.IsSitting);
    }

    private static void GetFileNameFromPath()
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
            StartupClass.cameraRotator.method_16(ggameCamera_0, float_0);
        }
    }

    private static void ApplyConfig()
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

    private static void ParseDouble()
    {
        ResolveWowVersion("MainHandSlot");
        ResolveWowVersion("SecondaryHandSlot");
    }

    private static void ResolveWowVersion(string string_0)
    {
        var equippedGuid = GContext.Main.Items.GetEquippedGUID(string_0);
        Logger.LogMessage("Item in \"" + string_0 + "\": 0x" + equippedGuid.ToString("x"));
        if (equippedGuid == 0L)
            return;
        GContext.Main.Items.DebugItem(equippedGuid);
    }

    private static void SelectActiveGameClass()
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

    private static void StartMainThread()
    {
        var gclass8_1 = UIElement.IsGroupProfile("UIParent");
        var gclass8_2 = UIElement.IsGroupProfile("StaticPopup1");
        Logger.LogMessage("parent: " + gclass8_1.method_2());
        Logger.LogMessage("sp1: " + gclass8_2.method_2());
    }

    private static void RunMainThreadSafe()
    {
        Logger.LogMessage("Stealthed: " + GPlayerSelf.Me.HasWellKnownBuff("Stealth"));
    }

    private static void RunInitializationFlow()
    {
        GContext.Main.CastSpell("Priest.Shield");
        GContext.Main.CastSpell("Priest.MindBlast");
    }

    private static void IsAttachedToGame()
    {
        var num1 = -1;
        while (true)
        {
            int num2;
            do
            {
                Thread.Sleep(100);
                num2 = GProcessMemoryManipulator.ReadInt32(11257960, "bst");
            } while (num2 == num1);

            Logger.LogMessage("new value for bst: " + num2);
            num1 = num2;
        }
    }

    private static void TryAttach()
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

    private static void ExecuteAttachOrDetach()
    {
        if (GPlayerSelf.Me.HasAmmo)
            Logger.LogMessage("I have ammo!");
        else
            Logger.LogMessage("Out of Ammo--Help!");
    }

    private static void Detach()
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

    private static void ApplyAclForProcess()
    {
        if (GContext.Main.Interface.IsKeyEnabled("Common.Potion"))
            Logger.LogMessage("Key Enabled");
        else
            Logger.LogMessage("Key Disabled");
    }

    private static void NotifyStatusChange()
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

    private static void ParseProcessIdFromCommandLine()
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

    private static void IsNumericString()
    {
        while (GPlayerSelf.Me.Target != null)
        {
            var target = GPlayerSelf.Me.Target;
            Thread.Sleep(200);
            GContext.Main.Log("Casting/Energy/Ready: " + target.IsCasting + "/" + GPlayerSelf.Me.Energy + "/" +
                              GContext.Main.Interface.IsKeyReady("Rogue.Kick"));
        }
    }

    private static void SendInputString()
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

    private static void StartManualGlide()
    {
        var byKeyName = GContext.Main.Interface.GetByKeyName("Common.Eat");
        if (byKeyName == null)
            return;
        GContext.Main.Log("Food interface object: " + byKeyName);
    }

    private static void BringGameWindowToForeground()
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

    private static void AddWaypoint()
    {
        Logger.LogMessage("Put cursor on item!");
        Thread.Sleep(3000);
        Logger.LogMessage("Cursor Type: " +
                           GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("CursorType"), "CursorType"));
    }

    private static void StartAutoGlide()
    {
        Logger.LogMessage("Testing Creature Type:");
        Logger.LogMessage(GPlayerSelf.Me.Target.CreatureType.ToString());
        Logger.LogMessage("Testing Player Type:");
        Logger.LogMessage(GPlayerSelf.Me.CreatureType.ToString());
        Logger.LogMessage("IsDead :" + GPlayerSelf.Me.IsDead);
    }

    private static void StartDetachedGlide()
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

    private static void ToggleFactionForTarget()
    {
        var target = GPlayerSelf.Me.Target;
        if (target == null)
            Logger.LogMessage("Target something first!");
        else
            Logger.LogMessage("Raid target icon is: " + target.RaidTargetIcon);
    }

    private static void StopGlide()
    {
        var buffSnapshot = GPlayerSelf.Me.Target.GetBuffSnapshot(false);
        Logger.LogMessage("Retrieved " + buffSnapshot.Length + " buffs for self");
        foreach (object obj in buffSnapshot)
            Logger.LogMessage(obj.ToString());
    }

    private static void ExecuteStopGlide()
    {
        Logger.LogMessage("Profile has Z stuff: " + GContext.Main.Profile.AllCoordsHaveZ());
    }

    private static void GetGlideRate()
    {
        Logger.LogMessage("Starting Object Test 3..2..1");
        var objects = GObjectList.GetObjects();
        Logger.LogMessage("Node Count: " + objects.Length);
        foreach (var gobject in objects)
            Logger.LogMessage("object Type: " + gobject.Type + " | Name: " + gobject.Name);
    }

    private static void SetupKillEventListener()
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

    private static void Shutdown()
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

    private static void WaitForKillEvent()
    {
        SpellcastingManager.gclass42_0.Offsets["Rogue.Sinister"].FilloutKey();
    }

    private static void ExecuteShutdown()
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

    private static void TriggerKillEvent()
    {
        Logger.LogMessage("My stance is: " + GPlayerSelf.Me.Stance);
    }

    private static void CancelKillEvent()
    {
        var visibleInterfaceObject = SpellcastingManager.gclass42_0.Offsets["Rogue.Sinister"].FindVisibleInterfaceObject();
        if (visibleInterfaceObject == null)
        {
            Logger.LogMessage("Could not find visible object for sinister strike");
        }
        else
        {
            Logger.LogMessage("Found it: \"" + visibleInterfaceObject + "\"");
            UIElement.IsGroupProfile(visibleInterfaceObject).method_16(false);
        }
    }

    private static void ParseCommandLineArg()
    {
        SpellcastingManager.gclass42_0.method_23();
        SpellcastingManager.gclass42_0.Offsets["Common.Potion"].FilloutKey();
        Logger.LogMessage("Potion ready flag: " + GContext.Main.Interface.IsKeyReady("Common.Potion"));
    }

    private static void HandleWardenCheckResult()
    {
        SpellcastingManager.gclass42_0.method_23();
        foreach (var gkey in SpellcastingManager.gclass42_0.Offsets.Values)
            if (gkey.KeyName.StartsWith("Common") || gkey.KeyName.StartsWith(GPlayerSelf.Me.PlayerClass.ToString()))
                gkey.FilloutKey();
    }

    private static void TickMainLoop()
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

    private static void Sleep()
    {
        Logger.LogMessage("The count is: " + GContext.Main.Interface.GetActionInventory("Common.Test"));
    }

    private static void SleepWithJitter()
    {
        if (GPlayerSelf.Me.Target == null)
        {
            Logger.LogMessage("Target something first");
        }
        else
        {
            double double_1;
            double double_2;
            if (WorldToScreenProjector.smethod_0(GPlayerSelf.Me.Target.Location, 0.0, out double_1, out double_2))
            {
                Logger.LogMessage("Conversion good, positioning cursor");
                InputController.ParseProcessIdFromCommandLine(double_1, double_2);
                Thread.Sleep(1000);
                InputController.AddWaypoint(true);
            }
            else
            {
                Logger.LogMessage("Conversion no good, check log");
            }
        }
    }

    private static void HandleTargetVanish()
    {
        if (GPlayerSelf.Me.Target == null)
            Logger.LogMessage("Target something first");
        else
            GPlayerSelf.Me.Target.Interact();
    }

    private static void GetFileNameFromBackslash()
    {
        InputController.ExecuteStopGlide("/w dirtymeat woo! YeAhhh!  Money$$!!??");
    }

    private static void GetDirectoryFromPath()
    {
        foreach (var quest in GPlayerSelf.Me.Quests)
            Logger.LogMessage("Quest: 0x" + quest.ToString("x"));
        Logger.LogMessage("Cutting teeth quest: " + GPlayerSelf.Me.IsOnQuest(788));
    }

    private static void ProbeProcessAttach()
    {
        var memory = GContext.Main.Memory;
        Logger.LogMessage("Byte at test addr: " + memory.ReadByte(7146512, "memtestread").ToString("x"));
        var DataToWrite = new byte[1] { 204 };
        memory.WriteBytes(7146512, DataToWrite, 1, "memtestwrite");
        Logger.LogMessage("Now byte is: " + memory.ReadByte(7146512, "memtestread2").ToString("x"));
    }

    private static void TryAutoAttach()
    {
        Logger.LogMessage("My move flags1: 0x" + GPlayerSelf.Me.MovementFlags1.ToString("x"));
        Logger.LogMessage("My move flags2: 0x" + GPlayerSelf.Me.MovementFlags2.ToString("x"));
    }

    private static void HandleBackgroundDisplay()
    {
        GInterfaceHelper.DumpUIDebug(true);
    }

    private static void HideGameWindow()
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

    public static void ShrinkGameWindow(GInterfaceObject ginterfaceObject_0)
    {
        if (ginterfaceObject_0 == null || !ginterfaceObject_0.IsVisible)
            return;
        GContext.Main.EnableCursorHook();
        ginterfaceObject_0.Hover();
        GContext.Main.DisableCursorHook();
    }

    private static void RestoreHiddenWindow()
    {
        for (var index = 1; index < 65; ++index)
        {
            var byName = GContext.Main.Interface.GetByName("TaxiButton" + index);
            if (byName != null && byName.IsVisible)
            {
                ShrinkGameWindow(byName);
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

    private static void RestoreShrunkWindow()
    {
        GContext.Main.Interface.GetByName("ActionButton12").Hover();
    }

    private static void RestoreGameWindow()
    {
        for (var index = 1; index <= 32; ++index)
        {
            var byName = GContext.Main.Interface.GetByName("GossipTitleButton" + index);
            if (byName != null && byName.IsVisible)
                GContext.Main.Log("GossipTitleButton #" + index + " => " + byName.LabelText);
        }
    }

    private static void ApplyLnCommandLineArg()
    {
        SleepWithJitter();
    }
}