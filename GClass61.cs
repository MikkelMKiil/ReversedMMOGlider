// Decompiled with JetBrains decompiler
// Type: GClass61
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class GClass61
{
    public static string string_0 =
        "(50 48) (40 40) (40 44) (40 48) (40 52) (40 56) (44 40) (44 44) (44 48) (44 52) (44 56) (48 40) (48 44) (48 48) (48 52) (48 56) (52 40) (52 44) (52 48) (52 52) (52 56) (56 40) (56 44) (56 48) (56 52) (56 56)";

    public static GClass61 gclass61_0;
    public bool bool_0;
    protected List<string> list_0;
    public SortedList sortedList_0;
    private readonly string string_1;
    private readonly string string_2;

    public GClass61()
    {
        gclass61_0 = this;
        string_1 = "Glider.config.xml";
        string_2 = "GliderConfig";
        bool_0 = false;
        sortedList_0 = new SortedList();
        method_6();
        list_0 = new List<string>();
        method_7(true);
    }

    public GClass61(string string_3, string string_4)
    {
        sortedList_0 = new SortedList();
        string_1 = string_3;
        string_2 = string_4;
        method_7(false);
    }

    public void method_0(string string_3, string string_4)
    {
        if (sortedList_0.ContainsKey(string_3))
            sortedList_0[string_3] = string_4;
        else
            sortedList_0.Add(string_3, string_4);
    }

    public void method_1(string string_3, string string_4)
    {
        if (sortedList_0.ContainsKey(string_3))
            return;
        sortedList_0.Add(string_3, string_4);
    }

    public string method_2(string string_3)
    {
        return sortedList_0.ContainsKey(string_3) ? (string)sortedList_0[string_3] : null;
    }

    public int method_3(string string_3)
    {
        return sortedList_0.ContainsKey(string_3) ? int.Parse((string)sortedList_0[string_3]) : 0;
    }

    public double method_4(string string_3)
    {
        return sortedList_0.ContainsKey(string_3) ? StartupClass.smethod_6((string)sortedList_0[string_3]) : 0.0;
    }

    public bool method_5(string string_3)
    {
        return sortedList_0.ContainsKey(string_3) && (string)sortedList_0[string_3] == "True";
    }

    public void method_6()
    {
        method_0("TargetWithMouse", "False");
        method_0("Silent", "False");
        method_0("AllowAutoSecCheck", "True");
        method_0("HandleChatTxt", "False");
        method_0("VendorDurabilityMin", ".10");
        method_0("VendorOnDurability", "True");
        method_0("VendorOnFoodWater", "True");
        method_0("VendorJunk", "True");
        method_0("AmmoAmount", "200");
        method_0("FoodAmount", "40");
        method_0("WaterAmount", "40");
        method_0("BarCharacters", "1234567890-=");
        method_0("RelogEnabled", "False");
        method_0("ProfilesDir", "Profiles\\");
        method_0("UseProfileWizard", "True");
        method_0("ScriptsFolder", "Scripts");
        method_0("MaxStartDistance", "50");
        method_0("TurboLoot", "True");
        method_0("SpellLeadDelay", "333");
        method_0("LogWW", "True");
        method_0("AllowWW", "True");
        method_0("AllowNetCheck", "True");
        method_0("AlertOnPopup", "True");
        method_0("CorpseShortCircuit", "30");
        method_0("MouseHoldMS", "1400");
        method_0("MouseSpin", "False");
        method_0("MaxPopups", "2");
        method_0("StuckLimit", "3");
        method_0("WaypointCloseness", "5.0");
        method_0("LootCheckDistance", "15");
        method_0("LootCheckHostiles", "True");
        method_0("ManageGamePos", "True");
        method_0("CustomClassName", "");
        method_0("AltLayout", "False");
        method_0("PawSpeed", "100");
        method_0("LogNotifyQuery", "False");
        method_0("LogNotifyResponse", "False");
        method_0("NoCursorHook", "False");
        method_0("WebNotifyEnabled", "False");
        method_0("WebNotifyURL", "");
        method_0("WebNotifyCredentials", "");
        method_0("ShrinkWidth", "500");
        method_0("BackgroundDisplay", "Normal");
        method_0("SoundKill", "True");
        method_0("UseTray", "True");
        method_0("ForceSkin", "False");
        method_0("BackgroundEnable", "False");
        method_0("ShiftLoot", "True");
        method_0("UseHook", "True");
        method_0("WardenLinkTest", "False");
        method_0("StopOnVanish", "True");
        method_0("LogMonsterChecks", "False");
        method_0("UnloadShadow", "False");
        method_0("ExitOnWardenChangeReal", "False");
        method_0("ExitOnWardenChange", "True");
        method_0("BypassTagCheck", "False");
        method_0("BypassTagCheck", "False");
        method_0("EscapeClear", "True");
        method_0("ChatLogFrame", "1");
        method_0("CombatLogFrame", "2");
        method_0("BadTagLimit", "3");
        method_0("NinjaSkin", "False");
        method_0("FriendWhitelist", "");
        method_0("RemoveDebuffs", "True");
        method_0("AutoAddDistance", "10");
        method_0("CommandKey1", "162");
        method_0("CommandKey2", "162");
        method_0("CaptureDelay", "2000");
        method_0("RunPostLoot", "False");
        method_0("NoHarvest", "");
        method_0("LootHoverDelay", "500");
        method_0("LootHoverDelay2", "500");
        method_0("PlaySay", "True");
        method_0("TapSpin", "120");
        method_0("BypassLootSanity", "False");
        method_0("StrafeObstacles", "True");
        method_0("AppKey", "demo");
        method_0("AlwaysOnTop", "False");
        method_0("Class", "7");
        method_0("Dev", "False");
        method_0("UseMediaKeys", "True");
        method_0("AutoSkin", "True");
        method_0("WalkLoot", "True");
        method_0("ResetBuffs", "True");
        method_0("MaxResurrect", "10");
        method_0("Resurrect", "False");
        method_0("AutoStop", "True");
        method_0("AutoStopMinutes", "240");
        method_0("RestMana", "70");
        method_0("RestHealth", "70");
        method_0("IgnoreTags", "False");
        method_0("SitWhenBored", "True");
        method_0("ExtraPull", "0");
        method_0("ChatEnabled", "True");
        method_0("ChatDelete", "True");
        method_0("ChatWhisper", "True");
        method_0("ChatAutoReply", "False");
        method_0("ChatAutoReplyText", "");
        method_0("KeyDelay", "20");
        method_0("CombatBarKey", "5");
        method_0("RestBarKey", "6");
        method_0("ControlKeys", "True");
        method_0("TargetSwitchStop", "False");
        method_0("TeleportStop", "False");
        method_0("TeleportLogout", "False");
        method_0("TitleBarRename", "True");
        method_0("TitleBarRandom", "True");
        method_0("TitleBarName", "");
        method_0("ProcessRename", "False");
        method_0("ProcessRandom", "True");
        method_0("ProcessName", "");
        method_0("SkipLoot", "False");
        method_0("HarvestRange", "0");
        method_0("PickupJunk", "False");
        method_0("UseBandages", "False");
        method_0("BandageHealth", "20");
        method_0("FastEat", "False");
        method_0("SkinDelay", "2");
        method_0("UseClipboard", "True");
        method_0("FriendAlert", "3");
        method_0("FriendLogout", "5");
        method_0("Strafe", "False");
        method_0("JumpMore", "False");
        method_0("MeleeDistance", "4.80");
        method_0("RangedDistance", "14");
        method_0("PartyBuff", "True");
        method_0("PartyHealMode", "Normal");
        method_0("PartyMode", "Solo");
        method_0("PartyAdds", "False");
        method_0("PartyLooters", "2");
        method_0("PartyLootPos", "0");
        method_0("PartyProductKey", "");
        method_0("PartyLeaderName", "");
        method_0("PartyMember1", "");
        method_0("PartyMember2", "");
        method_0("PartyMember3", "");
        method_0("PartyMember4", "");
        method_0("PartyKey1", "");
        method_0("PartyKey2", "");
        method_0("PartyKey3", "");
        method_0("PartyKey4", "");
        method_0("PartyAttackDelay", "0");
        method_0("PartyLootDelay", "2");
        method_0("PartyLeaderWait", "20");
        method_0("PartyFollowerStart", "9");
        method_0("PartyFollowerStop", "6");
        method_0("PartySlashFollow", "False");
        method_0("MouseSpin", "False");
        method_0("FightPlayers", "False");
        method_0("SkinSkill", "2a10,21a9,21aa,21a5");
        method_0("HerbSkill", "94f");
        method_0("MineSkill", "a14");
        method_0("ListenPort", "3200");
        method_0("ListenEnabled", "False");
        method_0("ListenPassword", "");
        method_0("StopWhenFull", "True");
        method_0("StopLootingWhenFull", "True");
        method_0("AttachEXE", "wow.exe");
        method_0("LootPattern", string_0);
        method_0("MailBoxRange", "3");
        method_0("AllowWriteBytes", "False");
    }

    public void method_7(bool bool_1)
    {
        var xmlDocument_0 = new XmlDocument();
        try
        {
            xmlDocument_0.Load(string_1);
            foreach (XmlNode selectNode in xmlDocument_0.SelectNodes("/" + string_2 + "/*"))
                if (selectNode.ChildNodes.Count == 1)
                    method_0(selectNode.Name, selectNode.InnerText.Trim());
            if (bool_1)
                method_9(xmlDocument_0, list_0, "/GliderConfig/LoadClasses/*");
            bool_0 = true;
        }
        catch (FileNotFoundException ex)
        {
        }
    }

    public void method_8()
    {
        method_15("Glider.config.xml");
    }

    private void method_9(XmlDocument xmlDocument_0, List<string> list_1, string string_3)
    {
        foreach (XmlNode selectNode in xmlDocument_0.SelectNodes(string_3))
        {
            var str = selectNode.InnerText.Trim();
            list_1.Add(str);
        }
    }

    public string[] method_10(string string_3)
    {
        switch (string_3)
        {
            case "CustomClasses":
                return list_0 != null ? list_0.ToArray() : null;
            default:
                return null;
        }
    }

    public bool method_11(string string_3, string string_4)
    {
        List<string> stringList = null;
        if (string_3 == "CustomClasses")
            stringList = list_0;
        if (stringList == null)
        {
            GClass37.smethod_0("!! Attempt to lookupstring in unknown stringlist: \"" + string_3 + "\"");
            return false;
        }

        for (var index = 0; index < stringList.Count; ++index)
            if (stringList[index].ToLower() == string_4.ToLower())
                return true;
        return false;
    }

    public void method_12(string string_3, string string_4)
    {
        List<string> stringList = null;
        if (string_3 == "CustomClasses")
            stringList = list_0;
        if (stringList == null)
            GClass37.smethod_0("!! Attempt to add to unknown stringlist: \"" + string_3 + "\"");
        else
            stringList.Add(string_4);
    }

    public bool method_13(string string_3, string string_4)
    {
        List<string> stringList = null;
        if (string_3 == "CustomClasses")
            stringList = list_0;
        if (stringList == null)
        {
            GClass37.smethod_0("!! Attempt to remove from unknown stringlist: \"" + string_3 + "\"");
            return false;
        }

        for (var index = 0; index < stringList.Count; ++index)
            if (stringList[index].ToLower() == string_4.ToLower())
            {
                stringList.RemoveAt(index);
                return true;
            }

        return false;
    }

    private void method_14(
        XmlDocument xmlDocument_0,
        List<string> list_1,
        string string_3,
        string string_4)
    {
        if (list_1 == null)
            return;
        var element1 = xmlDocument_0.CreateElement(string_3);
        xmlDocument_0.DocumentElement.AppendChild(element1);
        foreach (var str in list_1)
        {
            var element2 = xmlDocument_0.CreateElement(string_4);
            element2.InnerText = str;
            element1.AppendChild(element2);
        }
    }

    public void method_15(string string_3)
    {
        var xmlDocument_0 = new XmlDocument();
        xmlDocument_0.AppendChild(xmlDocument_0.CreateXmlDeclaration("1.0", null, null));
        xmlDocument_0.AppendChild(xmlDocument_0.CreateElement(string_2));
        foreach (string key in sortedList_0.Keys)
            method_17(xmlDocument_0, key, (string)sortedList_0[key]);
        method_14(xmlDocument_0, list_0, "LoadClasses", "SourceFile");
        xmlDocument_0.Save(string_3);
    }

    public void method_16(XmlDocument xmlDocument_0, string string_3, ref string string_4)
    {
        var xmlNode = xmlDocument_0.SelectSingleNode("/" + string_2 + "/" + string_3);
        if (xmlNode == null)
            return;
        string_4 = xmlNode.InnerText.Trim();
    }

    public void method_17(XmlDocument xmlDocument_0, string string_3, string string_4)
    {
        if (string_4 == null)
            return;
        var element = xmlDocument_0.CreateElement(string_3);
        element.InnerText = string_4;
        xmlDocument_0.DocumentElement.AppendChild(element);
    }

    public void method_18(string string_3)
    {
        if (!sortedList_0.ContainsKey(string_3))
            return;
        sortedList_0.Remove(string_3);
    }
}