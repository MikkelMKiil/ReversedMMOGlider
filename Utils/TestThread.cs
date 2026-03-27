// Decompile with JetBrains decompiler
// Type: TestThread
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Threading;

epublic class TestThread
{
    private const int ForegroundInitializationDelayMs = 2000;
    private const int PostRunDelayMs = 4000;
    private const int PollIntervalMs = 20;
    private const int LongPollIntervalMs = 100;
    private const string DefaultScenarioKey = "screen-target";
    private const int DefaultDiagnosticTimeoutMs = 30000;

    private static readonly object ExecutionLock = new object();
    private static readonly IDictionary<string, Action> ScenarioRegistry = CreateScenarioRegistry();
    private static readonly IDictionary<string, int> ScenarioTimeouts = CreateScenarioTimeouts();
    private static bool isRunning;
    private static GGameCamera cachedCamera;
    private static float cachedCameraPitch;
    private static DateTime? currentScenarioDeadlineUtc;
    private static string currentScenarioKey;

    public static void Start()
    {
        Start(DefaultScenarioKey);
    }

    public static void Start(string scenarioKey)
    {
        lock (ExecutionLock)
        {
            if (isRunning)
            {
                LogInfo("Can't start another TestThread, wait a minute!");
                return;
            }
        }

        var workerThread = new Thread(new ThreadStart(delegate { RunWorker(scenarioKey); }));
        workerThread.Start();
    }

    public static string[] GetScenarioKeys()
    {
        var keys = new string[ScenarioRegistry.Count];
        ScenarioRegistry.Keys.CopyTo(keys, 0);
        return keys;
    }

    private static void LogInfo(string message)
    {
        Logger.LogMessage("[TestThread] " + message);
    }

    private static GUnit GetCurrentTargetOrLog(string missingTargetMessage)
    {
        var target = GPlayerSelf.Me.Target;
        if (target == null)
        {
            Logger.LogMessage(missingTargetMessage);
            return null;
        }

        return target;
    }

    private static void RunWorker(string scenarioKey)
    {
        try
        {
            lock (ExecutionLock)
            {
                if (isRunning)
                {
                    LogInfo("Can't start another TestThread, wait a minute!");
                    return;
                }

                isRunning = true;
            }

            LogInfo("worker starting");

            if (!StartupClass.IsGliderInitialized && ConfigManager.gclass61_0.method_5("BackgroundEnable"))
            {
                StartupClass.InitializeBackgroundModeIfNeeded();
                if (StartupClass.IsGliderInitialized)
                    LogInfo("Setting up for background mode!");
            }
            else
            {
                StartupClass.BringGameToForeground();
                Thread.Sleep(ForegroundInitializationDelayMs);
            }

            RunScenarioByKey(scenarioKey);
            Thread.Sleep(PostRunDelayMs);
            LogInfo("worker done");
        }
        catch (Exception ex)
        {
            LogInfo("! TestThread exception: " + ex.Message + "\r\n" + ex.StackTrace);
        }
        finally
        {
            lock (ExecutionLock)
            {
                isRunning = false;
            }
        }
    }

    private static void RunScenarioByKey(string scenarioKey)
    {
        Action scenario;
        var normalizedScenarioKey = NormalizeScenarioKey(scenarioKey);
        if (!ScenarioRegistry.TryGetValue(normalizedScenarioKey, out scenario))
        {
            LogInfo("Unknown scenario key: \"" + scenarioKey + "\", running default scenario");
            normalizedScenarioKey = DefaultScenarioKey;
            scenario = ScenarioRegistry[DefaultScenarioKey];
        }

        var timeoutMs = GetScenarioTimeoutMs(normalizedScenarioKey);
        currentScenarioKey = normalizedScenarioKey;
        currentScenarioDeadlineUtc = timeoutMs > 0
            ? DateTime.UtcNow.AddMilliseconds(timeoutMs)
            : (DateTime?)null;

        if (timeoutMs > 0)
            LogInfo("Running scenario: " + normalizedScenarioKey + " (timeout " + timeoutMs + "ms)");
        else
            LogInfo("Running scenario: " + normalizedScenarioKey);

        try
        {
            scenario();
        }
        finally
        {
            currentScenarioKey = null;
            currentScenarioDeadlineUtc = null;
        }
    }

    private static string NormalizeScenarioKey(string scenarioKey)
    {
        if (string.IsNullOrEmpty(scenarioKey))
            return DefaultScenarioKey;

        return scenarioKey.Trim().ToLowerInvariant();
    }

    private static int GetScenarioTimeoutMs(string normalizedScenarioKey)
    {
        int timeoutMs;
        return ScenarioTimeouts.TryGetValue(normalizedScenarioKey, out timeoutMs) ? timeoutMs : 0;
    }

    private static bool HasScenarioTimedOut()
    {
        return currentScenarioDeadlineUtc.HasValue && DateTime.UtcNow >= currentScenarioDeadlineUtc.Value;
    }

    private static bool StopIfScenarioTimedOut()
    {
        if (!HasScenarioTimedOut())
            return false;

        LogInfo("Scenario timed out: " + currentScenarioKey);
        return true;
    }

    private static void RunCompatibilitySelfTest()
    {
        LogInfo("Compatibility self-test started");
        LogInfo("OS Version: " + Environment.OSVersion.VersionString);
        LogInfo("CLR Version: " + Environment.Version);
        LogInfo("Process bitness: " + (IntPtr.Size * 8));
        LogInfo("Current directory: " + Environment.CurrentDirectory);

        try
        {
            var windowsIdentity = WindowsIdentity.GetCurrent();
            var windowsPrincipal = new WindowsPrincipal(windowsIdentity);
            LogInfo("Running as administrator: " + windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator));
        }
        catch (Exception ex)
        {
            LogInfo("Unable to determine elevation status: " + ex.Message);
        }

        try
        {
            var wowHandle = GameMemoryAccess.GetWindowHandle();
            LogInfo("Game window handle: 0x" + wowHandle.ToInt64().ToString("x"));
            if (wowHandle == IntPtr.Zero)
                LogInfo("WoW window handle is unresolved; input delivery may fail on Windows 11.");
        }
        catch (Exception ex)
        {
            LogInfo("Unable to query game window handle: " + ex.Message);
        }

        LogInfo("Registered scenarios: " + ScenarioRegistry.Count);
        LogInfo("Compatibility self-test finished");
    }

    private static void ProjectTargetToScreenAndMoveCursor()
    {
        var target = GetCurrentTargetOrLog("Target something first");
        if (target == null)
            return;

        double screenX;
        double screenY;
        if (WorldToScreenProjector.smethod_0(target.Location, 0.0, out screenX, out screenY))
        {
            Logger.LogMessage("Conversion good, positioning cursor");
            //InputController.smethod_18(screenX, screenY);
            Thread.Sleep(1000);
            //InputController.smethod_23(true);
        }
        else
        {
            Logger.LogMessage("Conversion no good, check log");
        }
    }

    private static void WaitForTargetAndReportSpin()
    {
        var target = GetCurrentTargetOrLog("Target something first, dummy");
        if (target == null)
            return;

        if (!StartupClass.IsGliderInitialized)
            Thread.Sleep(3000);

        var timeoutTimer = new GSpellTimer(5000, false);
        while (!timeoutTimer.IsReady)
            Thread.Sleep(PollIntervalMs);

        if (timeoutTimer.IsReady)
            LogInfo("Futility in test mouse spin");
    }

    private static void ReportIsSitting()
    {
        Logger.LogMessage("GPlayerSelf.Me.IsSitting: " + GPlayerSelf.Me.IsSitting);
    }

    private static void CaptureCameraSnapshotOrRestorePitch()
    {
        if (cachedCamera == null)
        {
            cachedCamera = new GGameCamera();
            LogInfo("New camera snapshot: " + cachedCamera);
            cachedCameraPitch = cachedCamera.Pitch;
        }
        else
        {
            LogInfo("Moving camera to target pitch of: " + Math.Round(cachedCameraPitch, 3));
            //StartupClass.CameraController.method_16(cachedCamera, cachedCameraPitch);
        }
    }

    private static void LogPlayerAndTargetLocations()
    {
        var target = GetCurrentTargetOrLog("no target, no test!");
        if (target == null)
            return;

        Logger.LogMessage("My location: " + GPlayerSelf.Me.Location.ToString3D() + ", heading = " +
                           GPlayerSelf.Me.Heading);
        Logger.LogMessage("Target location: " + target.Location.ToString3D());
        Logger.LogMessage("Heading to target: " +
                           GPlayerSelf.Me.Location.GetHeadingTo(target.Location));
    }

    private static void LogHandEquipment()
    {
        LogEquippedItemInSlot("MainHandSlot");
        LogEquippedItemInSlot("SecondaryHandSlot");
    }

    private static void LogEquippedItemInSlot(string slotName)
    {
        var equippedItemGuid = GContext.Main.Items.GetEquippedGUID(slotName);
        LogInfo("Item in \"" + slotName + "\": 0x" + equippedItemGuid.ToString("x"));
        if (equippedItemGuid == 0UL)
            return;

        GContext.Main.Items.DebugItem(equippedItemGuid);
    }

    private static void TrackRiposteStateDuringCombat()
    {
        var previousRiposteState = false;
        while (GPlayerSelf.Me.IsInCombat)
        {
            var currentRiposteState = GContext.Main.Interface.IsKeyEnabled("Rogue.Riposte");
            if (currentRiposteState != previousRiposteState)
            {
                LogInfo("New riposte enable state: " + currentRiposteState);
                previousRiposteState = currentRiposteState;
            }

            Thread.Sleep(PollIntervalMs);
        }

        LogInfo("Player left combat, bailing out of test loop");
    }

    private static void LogParentAndPopupState()
    {
        var uiParent = UIElement.smethod_2("UIParent");
        var staticPopup = UIElement.smethod_2("StaticPopup1", false);
        Logger.LogMessage("parent: " + uiParent.method_2());
        Logger.LogMessage("sp1: " + staticPopup.method_2());
    }

    private static void LogStealthStatus()
    {
        Logger.LogMessage("Stealthed: " + GPlayerSelf.Me.HasWellKnownBuff("Stealth"));
    }

    private static void CastPriestTestSpells()
    {
        GContext.Main.CastSpell("Priest.Shield");
        GContext.Main.CastSpell("Priest.MindBlast");
    }

    private static void MonitorBstMemoryValue()
    {
        var lastObservedValue = -1;
        while (true)
        {
            if (StopIfScenarioTimedOut())
                break;

            int currentObservedValue;
            do
            {
                if (StopIfScenarioTimedOut())
                    return;

                Thread.Sleep(LongPollIntervalMs);
                currentObservedValue = MemoryRequestHandler.ReadInt32(11257960, "bst");
            } while (currentObservedValue == lastObservedValue);

            LogInfo("new value for bst: " + currentObservedValue);
            lastObservedValue = currentObservedValue;
        }
    }

    private static void LogObjectAndBagContents()
    {
        LogInventorySnapshot(false);
    }

    private static void LogAmmoStatus()
    {
        if (GPlayerSelf.Me.HasAmmo)
            Logger.LogMessage("I have ammo!");
        else
            Logger.LogMessage("Out of Ammo--Help!");
    }

    private static void TrackTargetFacingDirection()
    {
        while (true)
        {
            if (StopIfScenarioTimedOut())
                break;

            var target = GetCurrentTargetOrLog("Target something first");
            if (target == null)
                break;

            Logger.LogMessage("Mob Heading: " + target.Bearing);
            if (target.Bearing >= Math.PI / 2.0 || target.Bearing <= -1.0 * Math.PI / 2.0)
                Logger.LogMessage("BACK");
            else
                Logger.LogMessage("FRONT");

            Thread.Sleep(PollIntervalMs);
        }
    }

    private static void LogPotionKeyState()
    {
        if (GContext.Main.Interface.IsKeyEnabled("Common.Potion"))
            Logger.LogMessage("Key Enabled");
        else
            Logger.LogMessage("Key Disabled");
    }

    private static void TestVendorInteraction()
    {
        var innkeeperUnit = GObjectList.FindUnit("Innkeeper Heather");
        if (innkeeperUnit == null)
        {
            GContext.Main.Log("Never found vendor in test code, oh well");
        }
        else
        {
            innkeeperUnit.Approach(3.0);
            innkeeperUnit.Interact();
            if (GPlayerSelf.Me.Target != innkeeperUnit)
            {
                GContext.Main.Log("Never managed to click on vendor in test code, oh well");
            }
            else
            {
                var merchantWindow = new GMerchant();
                if (!merchantWindow.IsVisible)
                    return;
                if (merchantWindow.IsRepairEnabled)
                    merchantWindow.ClickRepairButton();
                if (!merchantWindow.BuyOnAnyPage("Longjaw Mud Snapper"))
                    GContext.Main.Log("!! Unable to buy from vendor");
                else
                    GContext.Main.Log("Bought test item  successfully");
                merchantWindow.Close();
            }
        }
    }

    private static void MonitorWandKeyState()
    {
        while (GPlayerSelf.Me.Target != null)
        {
            if (StopIfScenarioTimedOut())
                break;

            Thread.Sleep(1000);
            GContext.Main.Log("Populated/Firing/Ready: " + GContext.Main.Interface.IsKeyPopulated("Warlock.Wand") +
                              "/" + GContext.Main.Interface.IsKeyFiring("Warlock.Wand") + "/" +
                              GContext.Main.Interface.IsKeyReady("Warlock.Wand"));
        }

        GContext.Main.Log("Wand test all done");
    }

    private static void MonitorKickStateDuringTargetCasting()
    {
        while (GPlayerSelf.Me.Target != null)
        {
            if (StopIfScenarioTimedOut())
                break;

            var targetUnit = GPlayerSelf.Me.Target;
            Thread.Sleep(200);
            GContext.Main.Log("Casting/Energy/Ready: " + targetUnit.IsCasting + "/" + GPlayerSelf.Me.Energy + "/" +
                              GContext.Main.Interface.IsKeyReady("Rogue.Kick"));
        }
    }

    private static void LogGlueDialogText()
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

    private static void LogFoodInterfaceObject()
    {
        var byKeyName = GContext.Main.Interface.GetByKeyName("Common.Eat");
        if (byKeyName == null)
            return;
        GContext.Main.Log("Food interface object: " + byKeyName);
    }

    private static void LogEquippedItemsAndBags()
    {
        LogInventorySnapshot(true);
    }

    private static void LogCursorTypeFromMemory()
    {
        Logger.LogMessage("Put cursor on item!");
        Thread.Sleep(3000);
        Logger.LogMessage("Cursor Type: " +
                           GameMemoryAccess.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("CursorType"), "CursorType"));
    }

    private static void LogCreatureAndDeathState()
    {
        Logger.LogMessage("Testing Creature Type:");
        Logger.LogMessage(GPlayerSelf.Me.Target.CreatureType.ToString());
        Logger.LogMessage("Testing Player Type:");
        Logger.LogMessage(GPlayerSelf.Me.CreatureType.ToString());
        Logger.LogMessage("IsDead :" + GPlayerSelf.Me.IsDead);
    }

    private static void LogTargetBuffs()
    {
        var target = GetCurrentTargetOrLog("Target something first!");
        if (target == null)
        {
            return;
        }

        var buffSnapshot = target.GetBuffSnapshot(false);
        Logger.LogMessage("Retrieved " + buffSnapshot.Length + " buffs for target");
        foreach (object obj in buffSnapshot)
            Logger.LogMessage(obj.ToString());
    }

    private static void LogRaidTargetIcon()
    {
        var target = GetCurrentTargetOrLog("Target something first!");
        if (target == null)
            return;

        Logger.LogMessage("Raid target icon is: " + target.RaidTargetIcon);
    }

    private static void LogSelfBuffs()
    {
        var target = GetCurrentTargetOrLog("Target something first!");
        if (target == null)
            return;

        var buffSnapshot = target.GetBuffSnapshot(false);
        Logger.LogMessage("Retrieved " + buffSnapshot.Length + " buffs for self");
        foreach (object obj in buffSnapshot)
            Logger.LogMessage(obj.ToString());
    }

    private static void LogProfileHasZCoordinates()
    {
        Logger.LogMessage("Profile has Z stuff: " + GContext.Main.Profile.AllCoordsHaveZ());
    }

    private static void LogObjectListNames()
    {
        Logger.LogMessage("Starting Object Test 3..2..1");
        var objects = GObjectList.GetObjects();
        Logger.LogMessage("Node Count: " + objects.Length);
        foreach (var gobject in objects)
            Logger.LogMessage("object Type: " + gobject.Type + " | Name: " + gobject.Name);
    }

    private static void DumpQuestFrameDetails()
    {
        var questFrame = GContext.Main.Interface.GetByName("QuestFrame");
        var questTitleText = GContext.Main.Interface.GetByName("QuestDetailScrollChildFrame")
            .GetChildObject("QuestTitleText");
        Thread.Sleep(777);
        if (!questFrame.IsVisible)
        {
            Logger.LogMessage("No quest frame, skipping gossip check");
        }
        else
        {
            Logger.LogMessage("Quest frame visible, skipping gossip check");
            Logger.LogMessage(questTitleText.LabelText);
            File.WriteAllLines("mybiglist.txt", GInterfaceHelper.GetAllInterfaceObjectNames());
        }
    }

    private static void LogShortcutBindings()
    {
        for (var slotNumber = 1; slotNumber <= 130; ++slotNumber)
        {
            var shortcut = new GShortcut(slotNumber);
            switch (shortcut.ShortcutType)
            {
                case GShortcutType.Spell:
                    Logger.LogMessage("Slot #" + slotNumber + ": Spell, definition = 0x" +
                                       shortcut.ShortcutValue.ToString("x") + ", details = " +
                                       GSpells.GetSpellName(shortcut.ShortcutValue));
                    break;
                case GShortcutType.Item:
                    Logger.LogMessage("Slot #" + slotNumber + ": Item, definition = 0x" +
                                       shortcut.ShortcutValue.ToString("x") + ", details = " +
                                       new GItemDefinition(shortcut.ShortcutValue).Name);
                    break;
                case GShortcutType.Macro:
                    Logger.LogMessage("Slot #" + slotNumber + ": Macro, number = 0x" +
                                       shortcut.ShortcutValue.ToString("x"));
                    break;
            }
        }
    }

    private static void FillSinisterStrikeKey()
    {
        SpellcastingManager.gclass42_0.Offsets["Rogue.Sinister"].FilloutKey();
    }

    private static void MonitorStealthCooldownState()
    {
        var flag1 = GContext.Main.Interface.IsKeyReady("Rogue.Stealth");
        var gspellTimer = new GSpellTimer(30000, false);
        Logger.LogMessage("Starting state: " + flag1);
        while (!gspellTimer.IsReadySlow)
        {
            if (StopIfScenarioTimedOut())
                break;

            bool flag2;
            if ((flag2 = GContext.Main.Interface.IsKeyReady("Rogue.Stealth")) != flag1)
            {
                Logger.LogMessage("New cd state: " + flag2);
                flag1 = flag2;
            }
        }
    }

    private static void LogCurrentStance()
    {
        Logger.LogMessage("My stance is: " + GPlayerSelf.Me.Stance);
    }

    private static void RevealSinisterStrikeButton()
    {
        var visibleInterfaceObject = SpellcastingManager.gclass42_0.Offsets["Rogue.Sinister"].FindVisibleInterfaceObject();
        if (visibleInterfaceObject == null)
        {
            Logger.LogMessage("Could not find visible object for sinister strike");
        }
        else
        {
            Logger.LogMessage("Found it: \"" + visibleInterfaceObject + "\"");
            UIElement.smethod_2(visibleInterfaceObject).method_16(false);
        }
    }

    private static void RefreshPotionKeyState()
    {
        SpellcastingManager.gclass42_0.method_23();
        SpellcastingManager.gclass42_0.Offsets["Common.Potion"].FilloutKey();
        Logger.LogMessage("Potion ready flag: " + GContext.Main.Interface.IsKeyReady("Common.Potion"));
    }

    private static void RefreshCommonAndClassKeys()
    {
        SpellcastingManager.gclass42_0.method_23();
        foreach (var gkey in SpellcastingManager.gclass42_0.Offsets.Values)
            if (gkey.KeyName.StartsWith("Common") || gkey.KeyName.StartsWith(GPlayerSelf.Me.PlayerClass.ToString()))
                gkey.FilloutKey();
    }

    private static void LogTargetNpcTitle()
    {
        var target = GetCurrentTargetOrLog("no target, no test!");
        if (target == null)
            return;

        Logger.LogMessage("NPC Title: " + target.Title);
    }

    private static void LogCommonTestActionCount()
    {
        Logger.LogMessage("The count is: " + GContext.Main.Interface.GetActionInventory("Common.Test"));
    }

    private static void InteractWithCurrentTarget()
    {
        var target = GetCurrentTargetOrLog("Target something first");
        if (target == null)
            return;

        target.Interact();
    }

    private static void SendWhisperTestMessage()
    {
        //InputController.smethod_28("/w dirtymeat woo! YeAhhh!  Money$$!!??");
    }

    private static void LogQuestFlags()
    {
        foreach (var quest in GPlayerSelf.Me.Quests)
            Logger.LogMessage("Quest: 0x" + quest.ToString("x"));
        Logger.LogMessage("Cutting teeth quest: " + GPlayerSelf.Me.IsOnQuest(788));
    }

    private static void ReadAndWriteMemoryTestByte()
    {
        const int testAddress = 7146512;
        var originalByte = MemoryRequestHandler.ReadByte(testAddress, "memtestread");
        LogInfo("Byte at test addr: " + originalByte.ToString("x"));

        var testValue = new byte[1] { 204 };
        MemoryRequestHandler.WriteBytes(testAddress, testValue, 1);

        var updatedByte = MemoryRequestHandler.ReadByte(testAddress, "memtestread2");
        LogInfo("Now byte is: " + updatedByte.ToString("x"));
    }

    private static void LogMovementFlags()
    {
        Logger.LogMessage("My move flags1: 0x" + GPlayerSelf.Me.MovementFlags1.ToString("x"));
        Logger.LogMessage("My move flags2: 0x" + GPlayerSelf.Me.MovementFlags2.ToString("x"));
    }

    private static void DumpUiDebugInfo()
    {
        GInterfaceHelper.DumpUIDebug(true);
    }

    private static void LogCharacterSelectFrameChildren()
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

    private static void HoverVisibleInterfaceObject(GInterfaceObject interfaceObject)
    {
        if (interfaceObject == null || !interfaceObject.IsVisible)
            return;
        GContext.Main.EnableCursorHook();
        interfaceObject.Hover();
        GContext.Main.DisableCursorHook();
    }

    private static void InspectTaxiButtons()
    {
        for (var index = 1; index < 65; ++index)
        {
            var byName = GContext.Main.Interface.GetByName("TaxiButton" + index);
            if (byName != null && byName.IsVisible)
            {
                HoverVisibleInterfaceObject(byName);
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

    private static void HoverActionButton12()
    {
        GContext.Main.Interface.GetByName("ActionButton12").Hover();
    }

    private static void LogVisibleGossipTitleButtons()
    {
        for (var index = 1; index <= 32; ++index)
        {
            var byName = GContext.Main.Interface.GetByName("GossipTitleButton" + index);
            if (byName != null && byName.IsVisible)
                GContext.Main.Log("GossipTitleButton #" + index + " => " + byName.LabelText);
        }
    }

    private static void LogInventorySnapshot(bool logOnlyEquippedItems)
    {
        foreach (var gameObject in GObjectList.GetObjects())
        {
            if (logOnlyEquippedItems && (gameObject.Type != GObjectType.Item || !((GItem)gameObject).IsEquipped))
                continue;

            Logger.LogMessage(gameObject.ToString());
        }

        LogBackpackContents();

        foreach (var bagGuid in GPlayerSelf.Me.Bags)
            LogBagContents(bagGuid);

        Logger.LogMessage("ObjectList test done");
    }

    private static void LogBackpackContents()
    {
        var backpackContents = GPlayerSelf.Me.BagContents;
        for (var slotIndex = 0; slotIndex < backpackContents.Length; ++slotIndex)
            Logger.LogMessage("Backpack/" + slotIndex + " = 0x" + backpackContents[slotIndex].ToString("x"));
    }

    private static void LogBagContents(ulong bagGuid)
    {
        var container = (GContainer)GObjectList.FindObject(bagGuid);
        if (container == null)
        {
            Logger.LogMessage("No bag for this ID: 0x" + bagGuid.ToString("x"));
            return;
        }

        Logger.LogMessage(container.ToString());
        var bagContents = container.BagContents;
        for (var slotIndex = 0; slotIndex < bagContents.Length; ++slotIndex)
        {
            var itemGuid = bagContents[slotIndex];
            var itemText = "0x" + itemGuid.ToString("x");
            if (itemGuid != 0UL)
            {
                var item = GObjectList.FindObject(itemGuid) as GItem;
                if (item != null)
                    itemText = item.ToString();
            }

            Logger.LogMessage("Bag " + bagGuid.ToString("x") + "/" + slotIndex + " = " + itemText);
        }
    }

    private static IDictionary<string, Action> CreateScenarioRegistry()
    {
        var scenarios = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase);

        scenarios[DefaultScenarioKey] = ProjectTargetToScreenAndMoveCursor;
        scenarios["compat-self-test"] = RunCompatibilitySelfTest;
        scenarios["target-spin"] = WaitForTargetAndReportSpin;
        scenarios["is-sitting"] = ReportIsSitting;
        scenarios["camera-snapshot"] = CaptureCameraSnapshotOrRestorePitch;
        scenarios["target-locations"] = LogPlayerAndTargetLocations;
        scenarios["hand-equipment"] = LogHandEquipment;
        scenarios["riposte-state"] = TrackRiposteStateDuringCombat;
        scenarios["parent-popup"] = LogParentAndPopupState;
        scenarios["stealth-status"] = LogStealthStatus;
        scenarios["cast-priest-spells"] = CastPriestTestSpells;
        scenarios["bst-memory"] = MonitorBstMemoryValue;
        scenarios["object-bags"] = LogObjectAndBagContents;
        scenarios["ammo-status"] = LogAmmoStatus;
        scenarios["target-facing"] = TrackTargetFacingDirection;
        scenarios["potion-key"] = LogPotionKeyState;
        scenarios["vendor"] = TestVendorInteraction;
        scenarios["wand-state"] = MonitorWandKeyState;
        scenarios["kick-state"] = MonitorKickStateDuringTargetCasting;
        scenarios["glue-dialog"] = LogGlueDialogText;
        scenarios["food-interface"] = LogFoodInterfaceObject;
        scenarios["equipped-items"] = LogEquippedItemsAndBags;
        scenarios["cursor-type"] = LogCursorTypeFromMemory;
        scenarios["creature-state"] = LogCreatureAndDeathState;
        scenarios["target-buffs"] = LogTargetBuffs;
        scenarios["raid-target-icon"] = LogRaidTargetIcon;
        scenarios["self-buffs"] = LogSelfBuffs;
        scenarios["profile-z"] = LogProfileHasZCoordinates;
        scenarios["object-names"] = LogObjectListNames;
        scenarios["quest-frame"] = DumpQuestFrameDetails;
        scenarios["shortcuts"] = LogShortcutBindings;
        scenarios["fill-sinister"] = FillSinisterStrikeKey;
        scenarios["stealth-cooldown"] = MonitorStealthCooldownState;
        scenarios["stance"] = LogCurrentStance;
        scenarios["reveal-sinister"] = RevealSinisterStrikeButton;
        scenarios["refresh-potion"] = RefreshPotionKeyState;
        scenarios["refresh-common-class-keys"] = RefreshCommonAndClassKeys;
        scenarios["target-title"] = LogTargetNpcTitle;
        scenarios["common-test-count"] = LogCommonTestActionCount;
        scenarios["interact-target"] = InteractWithCurrentTarget;
        scenarios["whisper-test"] = SendWhisperTestMessage;
        scenarios["quest-flags"] = LogQuestFlags;
        scenarios["memory-byte"] = ReadAndWriteMemoryTestByte;
        scenarios["movement-flags"] = LogMovementFlags;
        scenarios["ui-debug"] = DumpUiDebugInfo;
        scenarios["character-select-children"] = LogCharacterSelectFrameChildren;
        scenarios["taxi-buttons"] = InspectTaxiButtons;
        scenarios["hover-action12"] = HoverActionButton12;
        scenarios["gossip-buttons"] = LogVisibleGossipTitleButtons;

        return scenarios;
    }

    private static IDictionary<string, int> CreateScenarioTimeouts()
    {
        var timeouts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        timeouts["compat-self-test"] = 5000;
        timeouts["bst-memory"] = DefaultDiagnosticTimeoutMs;
        timeouts["target-facing"] = DefaultDiagnosticTimeoutMs;
        timeouts["wand-state"] = DefaultDiagnosticTimeoutMs;
        timeouts["kick-state"] = DefaultDiagnosticTimeoutMs;
        timeouts["stealth-cooldown"] = DefaultDiagnosticTimeoutMs;
        timeouts["riposte-state"] = DefaultDiagnosticTimeoutMs;

        return timeouts;
    }

}

