// Decompiled with JetBrains decompiler
// Type: GClass42
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Glider.Common.Objects;

public class GClass42
{
    public static GClass42 gclass42_0;
    private bool bool_0;
    public SortedList<string, GKey> sortedList_0;
    public SortedList<string, GKey> sortedList_1;
    private string string_0 = "??";

    public GClass42()
    {
        sortedList_1 = new SortedList<string, GKey>();
    }

    public void method_0(string string_1)
    {
        if (!sortedList_0.ContainsKey(string_1))
        {
            GClass37.smethod_0(GClass30.smethod_2(58, string_1));
            GClass37.smethod_1(Environment.StackTrace);
        }
        else
        {
            lock (this)
            {
                sortedList_0[string_1].Send();
            }
        }
    }

    public void method_1(string string_1)
    {
        if (!sortedList_0.ContainsKey(string_1))
        {
            GClass37.smethod_0(GClass30.smethod_2(58, string_1));
            GClass37.smethod_1(Environment.StackTrace);
        }
        else
        {
            lock (this)
            {
                sortedList_0[string_1].Press();
            }
        }
    }

    public void method_2(string string_1)
    {
        if (!sortedList_0.ContainsKey(string_1))
        {
            GClass37.smethod_0(GClass30.smethod_2(58, string_1));
            GClass37.smethod_1(Environment.StackTrace);
        }
        else
        {
            lock (this)
            {
                sortedList_0[string_1].Release();
            }
        }
    }

    public void method_3()
    {
        lock (this)
        {
            foreach (var gkey in sortedList_0.Values)
                gkey.UnFill();
        }
    }

    public void method_4(string string_1)
    {
        method_10(new GKey(string_1)
        {
            KType = GKeyType.SpellID,
            SIM = 0,
            AutoUpdate = true
        });
    }

    public void method_5(
        string string_1,
        GBarState gbarState_0,
        int int_0,
        char char_0,
        short short_0)
    {
        if (sortedList_0.ContainsKey(string_1))
            return;
        method_9(string_1, gbarState_0, int_0, char_0, short_0);
    }

    public void method_6(string string_1, int int_0)
    {
        method_10(new GKey(string_1)
        {
            KType = GKeyType.SpellID,
            SIM = int_0,
            AutoUpdate = true
        });
    }

    public void method_7(string string_1, int int_0)
    {
        method_10(new GKey(string_1)
        {
            KType = GKeyType.ItemDefID,
            SIM = int_0,
            AutoUpdate = true
        });
    }

    public void method_8(string string_1, int int_0, int int_1)
    {
        method_10(new GKey(string_1)
        {
            KType = GKeyType.SpellID,
            SIM = int_0,
            AutoUpdate = true,
            ShiftState = int_1
        });
    }

    protected void method_9(
        string string_1,
        GBarState gbarState_0,
        int int_0,
        char char_0,
        short short_0)
    {
        if (string_1 == "Combat" || string_1 == "Rest")
            gbarState_0 = GBarState.Indifferent;
        var gkey_0 = new GKey(string_1);
        if (char_0 != char.MinValue)
        {
            gkey_0.KType = GKeyType.Char;
            gkey_0.CharCode = char_0;
        }
        else
        {
            gkey_0.KType = GKeyType.VChar;
            gkey_0.VK = short_0;
        }

        gkey_0.ShiftState = int_0;
        gkey_0.BarState = gbarState_0;
        gkey_0.AutoUpdate = true;
        method_10(gkey_0);
    }

    protected void method_10(GKey gkey_0)
    {
        if (!sortedList_0.ContainsKey(gkey_0.KeyName))
            sortedList_0.Add(gkey_0.KeyName, gkey_0);
        if (sortedList_1.ContainsKey(gkey_0.KeyName))
            return;
        sortedList_1.Add(gkey_0.KeyName, gkey_0);
    }

    protected void method_11()
    {
        sortedList_0 = new SortedList<string, GKey>();
        method_9("Common.Mount", GBarState.Indifferent, 0, '`', 0);
        method_9("Common.Backpack", GBarState.Indifferent, 0, 'B', 0);
        method_9("Common.BackpackAll", GBarState.Indifferent, 1, 'B', 0);
        method_9("Common.Character", GBarState.Indifferent, 0, 'C', 0);
        method_9("Common.Escape", GBarState.Indifferent, 0, char.MinValue, 27);
        method_9("Common.Bar1", GBarState.Indifferent, 1, '1', 0);
        method_9("Common.Bar2", GBarState.Indifferent, 1, '2', 0);
        method_9("Common.Bar3", GBarState.Indifferent, 1, '3', 0);
        method_9("Common.Bar4", GBarState.Indifferent, 1, '4', 0);
        method_9("Common.Bar5", GBarState.Indifferent, 1, '5', 0);
        method_9("Common.Bar6", GBarState.Indifferent, 1, '6', 0);
        method_4("Common.Bandage");
        method_9("Common.Back", GBarState.Indifferent, 0, char.MinValue, 40);
        method_4("Common.Eat");
        method_4("Common.Drink");
        method_4("Common.Fish");
        method_9("Common.Forward", GBarState.Indifferent, 0, char.MinValue, 38);
        method_4("Common.Hearth");
        method_9("Common.Jump", GBarState.Indifferent, 0, ' ', 0);
        method_4("Common.LureSlot");
        method_9("Common.PetAttack", GBarState.Indifferent, 2, '1', 0);
        method_9("Common.PetFollow", GBarState.Indifferent, 2, '2', 0);
        method_4("Common.Potion");
        method_9("Common.RotateLeft", GBarState.Indifferent, 0, char.MinValue, 37);
        method_9("Common.RotateRight", GBarState.Indifferent, 0, char.MinValue, 39);
        method_9("Common.Sit", GBarState.Indifferent, 0, 'X', 0);
        method_9("Common.StrafeLeft", GBarState.Indifferent, 0, 'Q', 0);
        method_9("Common.StrafeRight", GBarState.Indifferent, 0, 'E', 0);
        method_9("Common.Assist", GBarState.Indifferent, 0, 'F', 0);
        method_9("Common.Target", GBarState.Indifferent, 0, char.MinValue, 9);
        method_9("Common.TargetLastHostile", GBarState.Indifferent, 0, 'G', 0);
        method_9("Common.TargetSelf", GBarState.Indifferent, 0, char.MinValue, 112);
        method_9("Common.ToggleCombat", GBarState.Indifferent, 0, 'T', 0);
        method_9("Common.TargetParty1", GBarState.Indifferent, 0, char.MinValue, 113);
        method_9("Common.TargetParty2", GBarState.Indifferent, 0, char.MinValue, 114);
        method_9("Common.TargetParty3", GBarState.Indifferent, 0, char.MinValue, 115);
        method_9("Common.TargetParty4", GBarState.Indifferent, 0, char.MinValue, 116);
        method_9("Common.PreMount", GBarState.Indifferent, 0, char.MinValue, 0);
        method_9("Common.PostDismount", GBarState.Indifferent, 0, char.MinValue, 0);
        method_9("Common.PreCombat", GBarState.Indifferent, 0, char.MinValue, 0);
        method_9("Common.PostCombat", GBarState.Indifferent, 0, char.MinValue, 0);
        method_9("Common.PostLoot", GBarState.Indifferent, 0, char.MinValue, 0);
        method_9("Common.Time1", GBarState.Indifferent, 0, char.MinValue, 0);
        method_9("Common.Time5", GBarState.Indifferent, 0, char.MinValue, 0);
        method_9("Common.Time30", GBarState.Indifferent, 0, char.MinValue, 0);
        method_6("Druid.Starfire", 2912);
        method_6("Druid.Moonfire", 8921);
        method_6("Druid.Faerie", 770);
        method_6("Druid.NS", 17116);
        method_6("Druid.Barkskin", 22812);
        method_6("Druid.Mangle", 33917);
        method_6("Druid.BearForm", 5487);
        method_6("Druid.Bash", 5211);
        method_6("Druid.DemoRoar", 99);
        method_6("Druid.Maul", 6807);
        method_6("Druid.Charge", 16979);
        method_6("Druid.Enrage", 5229);
        method_6("Druid.Swipe", 779);
        method_6("Druid.CatForm", 768);
        method_6("Druid.Bite", 22568);
        method_6("Druid.Claw", 1082);
        method_6("Druid.Fury", 5217);
        method_6("Druid.Prowl", 5215);
        method_6("Druid.Ravage", 6785);
        method_6("Druid.Pounce", 9005);
        method_6("Druid.Rip", 1079);
        method_6("Druid.Cower", 8998);
        method_8("Druid.Cure", 8946, 4);
        method_8("Druid.RemoveCurse", 2782, 4);
        method_8("Druid.HealingTouch", 5185, 4);
        method_8("Druid.Mark", 1126, 4);
        method_8("Druid.Rejuvenation", 774, 4);
        method_8("Druid.Regrowth", 8936, 4);
        method_8("Druid.Thorns", 467, 4);
        method_6("Druid.MarkOther", 1126);
        method_6("Druid.RejuvenationOther", 774);
        method_6("Druid.HealingTouchOther", 5185);
        method_6("Druid.ThornsOther", 467);
        method_6("Hunter.Mark", 1130);
        method_6("Hunter.FirstShot", 1978);
        method_6("Hunter.SecondShot", 5116);
        method_6("Hunter.RepeatShot", 3044);
        method_6("Hunter.RaptorStrike", 2973);
        method_6("Hunter.Shoot", 75);
        method_6("Hunter.Trap", 13795);
        method_6("Hunter.Intimidation", 19577);
        method_6("Hunter.Wrath", 19574);
        method_6("Hunter.Feign", 5384);
        method_6("Hunter.Aspect", 13165);
        method_6("Hunter.RevivePet", 982);
        method_6("Hunter.MendPet", 136);
        method_6("Hunter.FeedPet", 6991);
        method_6("Hunter.CallPet", 883);
        method_6("Hunter.TrapAdd", 1499);
        method_6("Hunter.Viper", 34074);
        method_9("Hunter.FeedMacro", GBarState.Bar1, 0, '1', 0);
        method_6("Mage.Frostbolt", 116);
        method_6("Mage.ArcaneMissiles", 5143);
        method_6("Mage.Fireball", 133);
        method_6("Mage.Fireblast", 2136);
        method_6("Mage.FrostNova", 122);
        method_6("Mage.Poly", 118);
        method_6("Mage.Scorch", 2948);
        method_6("Mage.MeleeSpell", 120);
        method_6("Mage.Counterspell", 2139);
        method_7("Mage.UseManastone", 5514);
        method_6("Mage.Wand", 5019);
        method_6("Mage.ConjureFood", 587);
        method_6("Mage.ConjureWater", 5504);
        method_8("Mage.ArcaneIntellect", 1459, 4);
        method_8("Mage.ArcaneIntellectOther", 1459, 4);
        method_6("Mage.FrostArmor", 168);
        method_6("Mage.DampenMagic", 604);
        method_6("Mage.RemoveCurse", 475);
        method_6("Mage.Combustion", 11129);
        method_6("Mage.IceBarrier", 11426);
        method_6("Mage.CreateManastone", 759);
        method_6("Mage.Evocation", 12051);
        method_6("Paladin.Judge", 20271);
        method_6("Paladin.HammerJustice", 853);
        method_6("Paladin.SealCrusader", 21082);
        method_6("Paladin.SealCommand", 20375);
        method_6("Paladin.DivineShield", 642);
        method_6("Paladin.DivineFavor", 20216);
        method_6("Paladin.LayHands", 633);
        method_6("Paladin.HammerWrath", 24275);
        method_6("Paladin.CrusaderStrike", 35395);
        method_8("Paladin.Cleanse", 4987, 4);
        method_8("Paladin.BOP", 1022, 4);
        method_8("Paladin.Heal", 635, 4);
        method_8("Paladin.FastHeal", 19750, 4);
        method_8("Paladin.Blessing", 19740, 4);
        method_6("Paladin.BOPOther", 1022);
        method_6("Paladin.HealOther", 635);
        method_6("Paladin.FastHealOther", 19750);
        method_6("Paladin.Aura", 465);
        method_6("Paladin.BlessingOther", 19740);
        method_6("Paladin.BlessingWisdomOther", 19742);
        method_6("Priest.SWPain", 589);
        method_6("Priest.MindFlay", 15407);
        method_6("Priest.MindBlast", 8092);
        method_6("Priest.Wand", 5019);
        method_8("Priest.Shield", 17, 4);
        method_8("Priest.Renew", 139, 4);
        method_8("Priest.FlashHeal", 2061, 4);
        method_8("Priest.PWFort", 1243, 4);
        method_8("Priest.RestHeal", 2060, 4);
        method_6("Priest.ShieldOther", 17);
        method_6("Priest.RenewOther", 139);
        method_6("Priest.FlashHealOther", 2061);
        method_6("Priest.PWFortOther", 1243);
        method_6("Priest.RestHealOther", 2060);
        method_6("Priest.Fade", 586);
        method_6("Priest.Shadowform", 15473);
        method_6("Priest.Vampiric", 15286);
        method_6("Priest.InnerFire", 588);
        method_8("Priest.Dispel", 527, 4);
        method_8("Priest.CureDisease", 528, 4);
        method_6("Shaman.LightningBolt", 403);
        method_6("Shaman.HealTotem", 5394);
        method_6("Shaman.EarthShock", 8042);
        method_6("Shaman.LightningShield", 324);
        method_6("Shaman.StartTotem", 8227);
        method_6("Shaman.Earthbind", 2484);
        method_6("Shaman.Stormstrike", 17364);
        method_6("Shaman.Rage", 30823);
        method_6("Shaman.TotemicCall", 36936);
        method_6("Shaman.Rockbiter", 8017);
        method_8("Shaman.Heal", 331, 4);
        method_6("Shaman.HealOther", 331);
        method_6("Shaman.NS", 16188);
        method_8("Shaman.CurePoison", 526, 4);
        method_8("Shaman.CureDisease", 2870, 4);
        method_6("Shaman.AltWeaponEnchant", 8232);
        method_6("Shaman.StoneclawTotem", 5730);
        method_6("Rogue.Ranged", 2764);
        method_6("Rogue.Sinister", 1752);
        method_6("Rogue.Eviscerate", 2098);
        method_6("Rogue.Gouge", 1776);
        method_6("Rogue.Backstab", 53);
        method_6("Rogue.ColdBlood", 14177);
        method_6("Rogue.CheapShot", 1833);
        method_6("Rogue.Evasion", 5277);
        method_6("Rogue.Kick", 1766);
        method_6("Rogue.Vanish", 1856);
        method_6("Rogue.Stealth", 1784);
        method_7("Rogue.Poison1", 2892);
        method_7("Rogue.Poison2", 2892);
        method_6("Rogue.Riposte", 14251);
        method_6("Rogue.BladeFlurry", 13877);
        method_6("Rogue.KidneyShot", 408);
        method_6("Rogue.Feint", 1966);
        method_6("Rogue.AdrenalineRush", 13750);
        method_6("Rogue.GhostlyStrike", 14278);
        method_6("Warlock.Lifetap", 1454);
        method_6("Warlock.Curse", 980);
        method_6("Warlock.Corruption", 172);
        method_6("Warlock.Immolate", 348);
        method_6("Warlock.DrainLife", 689);
        method_6("Warlock.DrainSoul", 1120);
        method_6("Warlock.Reckless", 704);
        method_6("Warlock.Fear", 5782);
        method_6("Warlock.Shadowbolt", 686);
        method_7("Warlock.UseHealthstone", 19005);
        method_6("Warlock.Wand", 5019);
        method_6("Warlock.DarkPact", 18220);
        method_6("Warlock.SoulLink", 19028);
        method_6("Warlock.DemonArmor", 706);
        method_6("Warlock.SummonDemon", 688);
        method_6("Warlock.HealthFunnel", 755);
        method_6("Warlock.Deathcoil", 6789);
        method_6("Warlock.CreateHealthstone", 6201);
        method_9("Warlock.ConsumeShadows", GBarState.Indifferent, 2, '6', 0);
        method_9("Warlock.VoidieSacrifice", GBarState.Indifferent, 2, '5', 0);
        method_9("Warlock.Suffering", GBarState.Indifferent, 2, '7', 0);
        method_9("Warlock.SpellLock", GBarState.Indifferent, 2, '6', 0);
        method_6("Warrior.Ranged", 3018);
        method_6("Warrior.Rend", 25208);
        method_6("Warrior.BattleShout", 2048);
        method_6("Warrior.Charge", 11578);
        method_6("Warrior.Concussion", 12809);
        method_6("Warrior.HeroicStrike", 29707);
        method_6("Warrior.SunderArmor", 25225);
        method_6("Warrior.Execute", 25236);
        method_6("Warrior.MortalStrike", 30330);
        method_6("Warrior.Bloodrage", 2687);
        method_6("Warrior.DemoShout", 25203);
        method_6("Warrior.Thunderclap", 25264);
        method_6("Warrior.Cleave", 25231);
        method_6("Warrior.Overpower", 11585);
        method_6("Warrior.ShieldBash", 29704);
        method_6("Warrior.Hamstring", 25212);
        method_6("Deathknight.IcyTouch", 45477);
        method_6("Deathknight.DeathGrip", 49576);
        method_6("Deathknight.BoneShield", 49222);
        method_6("Deathknight.PlagueStrike", 45462);
        method_6("Deathknight.FrostStrike", 49143);
        method_6("Deathknight.DeathStrike", 49998);
        method_6("Deathknight.ScourgeStrike", 55090);
        method_6("Deathknight.BloodStrike", 49926);
        method_6("Deathknight.DeathCoil", 47541);
        method_6("Deathknight.MindFreeze", 47528);
        method_6("Deathknight.RuneTap", 48982);
        method_6("Deathknight.HeartStrike", 55050);
        method_6("Deathknight.RaiseDead", 46584);
        method_6("Deathknight.HornOfWinter", 57330);
        method_9("Deathknight.Gnaw", GBarState.Indifferent, 2, '5', 0);
        bool_0 = false;
    }

    public void method_12()
    {
        method_11();
        try
        {
            var str = "Keys.xml";
            if (File.Exists(str))
            {
                if (!File.Exists("Keys.backup.xml"))
                {
                    GClass37.smethod_0("Saving old Keys.xml file to Keys.backup.xml");
                    File.Copy("Keys.xml", "Keys.backup.xml");
                }

                method_17(str);
            }

            method_22();
            bool_0 = false;
        }
        catch (Exception ex)
        {
            GClass37.smethod_0(GClass30.smethod_2(59, string_0, ex.Message));
        }
    }

    public void method_13()
    {
        if (!bool_0)
            return;
        method_14();
    }

    public void method_14()
    {
        try
        {
            method_16("Keys.xml");
            bool_0 = false;
        }
        catch (Exception ex)
        {
            GClass37.smethod_0(GClass30.smethod_2(60, ex.Message));
        }
    }

    public bool method_15(string string_1)
    {
        if (!sortedList_0.ContainsKey(string_1))
            return true;
        var gkey = sortedList_0[string_1];
        return gkey.CharCode == char.MinValue && gkey.VK == 0;
    }

    protected void method_16(string string_1)
    {
        var xmlDocument_0 = new XmlDocument();
        xmlDocument_0.AppendChild(xmlDocument_0.CreateXmlDeclaration("1.0", null, null));
        xmlDocument_0.AppendChild(xmlDocument_0.CreateElement("Keys"));
        foreach (var gkey_0 in sortedList_0.Values)
            method_18(xmlDocument_0, gkey_0);
        if (File.Exists(string_1))
            File.Delete(string_1);
        xmlDocument_0.Save(string_1);
        GClass37.smethod_0(GClass30.smethod_1(61));
    }

    protected void method_17(string string_1)
    {
        var xmlDocument = new XmlDocument();
        xmlDocument.Load(string_1);
        foreach (XmlNode selectNode in xmlDocument.SelectNodes("/Keys/*"))
            method_20(selectNode, true);
        GClass37.smethod_0(GClass30.smethod_1(62));
    }

    protected void method_18(XmlDocument xmlDocument_0, GKey gkey_0)
    {
        var element = xmlDocument_0.CreateElement("Key");
        xmlDocument_0.DocumentElement.AppendChild(element);
        method_19(element, "KeyName", gkey_0.KeyName);
        string_0 = gkey_0.KeyName;
        GClass37.smethod_1(string_0);
        var stringBuilder = new StringBuilder();
        if (gkey_0.ShiftState == 0)
        {
            stringBuilder.Append("None");
        }
        else
        {
            if ((gkey_0.ShiftState & 1) != 0)
                stringBuilder.Append("Shift,");
            if ((gkey_0.ShiftState & 2) != 0)
                stringBuilder.Append("Ctrl,");
            if ((gkey_0.ShiftState & 4) != 0)
                stringBuilder.Append("Alt,");
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
        }

        method_19(element, "ShiftState", stringBuilder.ToString());
        method_19(element, "KeyType", gkey_0.KType.ToString());
        switch (gkey_0.KType)
        {
            case GKeyType.Char:
                method_19(element, "BarState", gkey_0.BarState.ToString());
                method_19(element, "Char", "" + char.ToUpper(gkey_0.CharCode));
                break;
            case GKeyType.VChar:
                method_19(element, "BarState", gkey_0.BarState.ToString());
                method_19(element, "VK", smethod_0(gkey_0.VK));
                break;
            case GKeyType.SpellID:
                method_19(element, "SIM", "0x" + gkey_0.SIM.ToString("x"));
                break;
            case GKeyType.ItemDefID:
                method_19(element, "SIM", "0x" + gkey_0.SIM.ToString("x"));
                break;
            case GKeyType.Macro:
                method_19(element, "SIM", "0x" + gkey_0.SIM.ToString("x"));
                break;
        }

        if (!gkey_0.AutoUpdate)
            return;
        method_19(element, "Auto", gkey_0.AutoUpdate.ToString());
    }

    protected void method_19(XmlElement xmlElement_0, string string_1, string string_2)
    {
        var attribute = xmlElement_0.OwnerDocument.CreateAttribute(string_1);
        attribute.Value = string_2;
        xmlElement_0.Attributes.Append(attribute);
    }

    public static string smethod_0(short short_0)
    {
        if (short_0 >= 112 && short_0 <= 123)
            return "F" + (short_0 - 112 + 1);
        switch (short_0)
        {
            case 9:
                return "Tab";
            case 37:
                return "Left";
            case 38:
                return "Up";
            case 39:
                return "Right";
            case 40:
                return "Down";
            default:
                return short_0.ToString();
        }
    }

    public static short smethod_1(string string_1)
    {
        switch (string_1)
        {
            case "Tab":
                return 9;
            case "Up":
                return 38;
            case "Down":
                return 40;
            case "Left":
                return 37;
            case "Right":
                return 39;
            default:
                return char.ToUpper(string_1[0]) == 'F'
                    ? (short)(112 + (int.Parse(string_1.Substring(1)) - 1))
                    : (short)int.Parse(string_1);
        }
    }

    public void method_20(XmlNode xmlNode_0, bool bool_1)
    {
        string str1 = null;
        var str2 = xmlNode_0.Attributes["KeyName"].Value;
        try
        {
            if (sortedList_0.ContainsKey(str2) && !bool_1)
                return;
            var gkey = new GKey(str2);
            if (xmlNode_0.Attributes["KeyType"] != null)
                str1 = xmlNode_0.Attributes["KeyType"].Value.Trim();
            if (str1 == null || str1 == "Char" || str1 == "VChar")
                switch (xmlNode_0.Attributes["BarState"].Value)
                {
                    case "Indifferent":
                        gkey.BarState = GBarState.Indifferent;
                        break;
                    case "Combat":
                        gkey.BarState = GBarState.Combat;
                        break;
                    case "Rest":
                        gkey.BarState = GBarState.Rest;
                        break;
                    case "Bar1":
                        gkey.BarState = GBarState.Bar1;
                        break;
                    case "Bar2":
                        gkey.BarState = GBarState.Bar2;
                        break;
                    case "Bar3":
                        gkey.BarState = GBarState.Bar3;
                        break;
                    case "Bar4":
                        gkey.BarState = GBarState.Bar4;
                        break;
                    case "Bar5":
                        gkey.BarState = GBarState.Bar5;
                        break;
                    case "Bar6":
                        gkey.BarState = GBarState.Bar6;
                        break;
                    default:
                        throw new Exception(GClass30.smethod_2(645, str2));
                }

            if (xmlNode_0.Attributes["Char"] != null && (str1 == null || str1 == "Char"))
            {
                gkey.KType = GKeyType.Char;
                gkey.CharCode =
                    xmlNode_0.Attributes["Char"].Value.ToLower() == "undefined" ||
                    xmlNode_0.Attributes["Char"].Value.Length == 0
                        ? char.MinValue
                        : xmlNode_0.Attributes["Char"].Value[0];
            }
            else if (xmlNode_0.Attributes["VK"] != null && (str1 == null || str1 == "VChar"))
            {
                gkey.KType = GKeyType.VChar;
                gkey.VK = xmlNode_0.Attributes["VK"].Value.Length != 0
                    ? smethod_1(xmlNode_0.Attributes["VK"].Value)
                    : (short)0;
            }

            if (xmlNode_0.Attributes["ShiftState"] != null)
            {
                var str3 = xmlNode_0.Attributes["ShiftState"].Value;
                if (str3.IndexOf("Shift") > -1)
                    gkey.ShiftState |= 1;
                if (str3.IndexOf("Ctrl") > -1)
                    gkey.ShiftState |= 2;
                if (str3.IndexOf("Alt") > -1)
                    gkey.ShiftState |= 4;
            }

            if (str1 == "SpellID")
            {
                gkey.KType = GKeyType.SpellID;
                gkey.SIM = method_21(xmlNode_0);
            }

            if (str1 == "ItemDefID")
            {
                gkey.KType = GKeyType.ItemDefID;
                gkey.SIM = method_21(xmlNode_0);
            }

            if (str1 == "Macro")
            {
                gkey.KType = GKeyType.Macro;
                gkey.SIM = method_21(xmlNode_0);
            }

            if (xmlNode_0.Attributes["Auto"] != null && xmlNode_0.Attributes["Auto"].Value == "True")
                gkey.AutoUpdate = true;
            if (sortedList_0.ContainsKey(str2))
                sortedList_0[str2] = gkey;
            else
                sortedList_0.Add(str2, gkey);
        }
        catch (Exception ex)
        {
            GClass37.smethod_0(GClass30.smethod_2(646, str2, ex.Message));
        }
    }

    private int method_21(XmlNode xmlNode_0)
    {
        var s = xmlNode_0.Attributes["SIM"].Value.Trim();
        return s.StartsWith("0x") ? int.Parse(s.Substring(2), NumberStyles.HexNumber) : int.Parse(s);
    }

    private void method_22()
    {
        if (!sortedList_0.ContainsKey("Common.BarCombat"))
            return;
        var num1 = StartupClass.numbers_string.IndexOf(sortedList_0["Common.BarCombat"].CharCode);
        var num2 = StartupClass.numbers_string.IndexOf(sortedList_0["Common.BarRest"].CharCode);
        foreach (var gkey in sortedList_0.Values)
        {
            var num3 = 0;
            if (gkey.BarState == GBarState.Combat)
                num3 = 3 + num1;
            if (gkey.BarState == GBarState.Rest)
                num3 = 3 + num2;
            if (num3 != 0)
                gkey.BarState = (GBarState)num3;
        }

        sortedList_0.Remove("Common.BarCombat");
        sortedList_0.Remove("Common.BarRest");
        sortedList_0.Remove("Common.CooldownProbe");
        GClass37.smethod_0("Updated Keys.xml to new style, saving to disk.");
        method_14();
    }

    public void method_23()
    {
        var gkeyList = new List<GKey>();
        foreach (var gkey in sortedList_0.Values)
            if (gkey.AutoUpdate)
            {
                gkey.NeedAutoUpdate = true;
                gkeyList.Add(gkey);
            }

        var array = gkeyList.ToArray();
        StartupClass.ggameClass_0.UpdateKeys(array);
        method_24(array);
        method_3();
    }

    private void method_24(GKey[] gkey_0)
    {
        foreach (var gkey_0_1 in gkey_0)
            switch (gkey_0_1.KeyName)
            {
                case "Common.Eat":
                    method_26(gkey_0_1,
                        "33254 19301 8932 20062 20063 20064 13935 4457 27636 16166 2888 18635 3726 27657 27663", false);
                    method_26(gkey_0_1,
                        "27661 29449 13810 3220 13546 20516 5525 6290 4593 33867 27651 21031 11584 7807 20390", false);
                    method_26(gkey_0_1,
                        "17344 20389 12213 35563 2679 7808 30155 5526 16971 8683 29451 1113 22895 22019 34062", false);
                    method_26(gkey_0_1,
                        "5349 1487 1114 8075 8076 2682 13927 23756 2684 2683 12224 5479 3664 3662 19306 31673", false);
                    method_26(gkey_0_1,
                        "22645 4599 3665 414 13888 19223 2070 21030 19225 8953 17119 20222 20223 20224 4607 6522",
                        false);
                    method_26(gkey_0_1,
                        "29393 5478 21023 12217 24009 8948 24008 34063 2687 422 17198 13724 32722 20031 27662 21537",
                        false);
                    method_26(gkey_0_1,
                        "13930 5476 3927 33052 5066 4604 4541 23160 6807 33246 27857 6038 35285 17197 5527 27666",
                        false);
                    method_26(gkey_0_1,
                        "4539 3666 724 17407 21215 1401 9681 27664 30355 13928 11444 19995 19696 19996 19994 2287",
                        false);
                    method_26(gkey_0_1,
                        "961 16168 20074 12215 29292 24338 6888 20225 20226 20227 17406 8950 20857 34410 33053 3727",
                        false);
                    method_26(gkey_0_1,
                        "13929 13851 29412 35565 12212 5472 33874 13893 5480 12209 7097 13933 6316 7806 20388 4592",
                        false);
                    method_26(gkey_0_1,
                        "21711 27635 29394 27855 29448 24539 13934 32686 8364 11415 4542 31672 12218 4602 18632",
                        false);
                    method_26(gkey_0_1,
                        "28486 4544 3663 3770 12214 34780 13931 32685 6458 30358 30357 30361 30359 19305 33024 27665",
                        false);
                    method_26(gkey_0_1,
                        "13932 21033 5095 27655 28501 13932 21033 5095 27655 28501 4608 6291 6308 13754 21153 6317",
                        false);
                    method_26(gkey_0_1,
                        "6289 8365 13759 6361 13758 6362 21071 6303 8959 4603 13756 13760 13889 19224 4605 1082 5057",
                        false);
                    method_26(gkey_0_1,
                        "12210 2681 27658 5474 24105 8952 4594 18255 18254 21217 24072 1326 6657 33023 5473 1017 3448",
                        false);
                    method_26(gkey_0_1,
                        "16171 4536 6299 27856 33825 32721 787 4656 6890 30610 20452 21072 27854 4538 4601 3729 11109",
                        false);
                    method_26(gkey_0_1, "30816 19304 12216 34065 2680 17408 27667 33872 33025", false);
                    method_26(gkey_0_1,
                        "8957 4606 29453 27656 6887 23495 16170 33048 33866 1707 5477 21552 30458 18633 2685", false);
                    method_26(gkey_0_1,
                        "34064 27858 27660 3728 4537 29450 18045 33043 33026 7228 4540 117 16766 28112 16167", false);
                    method_26(gkey_0_1,
                        "35794 44749 38428 40358 30359 29394 33053 34410 33452 40356 30355 37252 40359 44609 43646",
                        false);
                    method_26(gkey_0_1,
                        "33052 33451 37452 44608 33449 22019 29451 33254 34750 34757 43001 34755 39691 42993 34768",
                        false);
                    method_26(gkey_0_1,
                        "34756 43005 42996 34763 34759 43652 34749 34125 43647 43571 34761 34751 42994 34752", false);
                    method_26(gkey_0_1,
                        "34766 34764 34765 34747 34758 34754 34748 43572 34769 42995 34762 34760 34767 43000", false);
                    method_26(gkey_0_1,
                        "42430 43268 42998 42433 42428 42997 42999 42432 42942 43518 35950 41729 42779 35947", false);
                    method_26(gkey_0_1,
                        "44071 40202 35948 44072 42429 35951 35953 42434 44049 42431 42778 43087 35952 38706", false);
                    method_26(gkey_0_1, "44722 44607 43523", false);
                    method_25(gkey_0_1, "8526 27659 19060 19062 19061 733 3771 16169 11950 22324 13755");
                    break;
                case "Common.Drink":
                    method_26(gkey_0_1,
                        "34140 4696 23586 33030 33029 33028 32667 33042 17404 13813 19300 34021 33929 9451 4600",
                        false);
                    method_26(gkey_0_1,
                        "8079 2288 22018 8077 30703 2136 8078 3772 5350 32668 4791 29395 28399 19299 33236 24007",
                        false);
                    method_26(gkey_0_1,
                        "23161 30457 10841 33034 17405 24006 19997 34411 18300 1179 33234 34020 1262 34018 1205",
                        false);
                    method_26(gkey_0_1,
                        "1645 8766 33036 33035 34019 27860 5342 159 29454 34017 34412 29401 32455 32453 4952 34022",
                        false);
                    method_26(gkey_0_1,
                        "44750 34411 40357 30457 37253 29395 32668 38431 38698 41731 43236 39520 33445 42777", false);
                    method_25(gkey_0_1, "23585 1708 33031 33032 33033 4953 21241 33042 39738 33444 43086");
                    break;
                case "Common.Hearth":
                    method_25(gkey_0_1, "6948");
                    break;
                case "Common.Potion":
                    method_25(gkey_0_1, "118 858 929 1710 3928 13446 22829");
                    break;
                case "Common.Bandage":
                    method_25(gkey_0_1, "21991 21990 14530 14529 8545 8544 6451 6450 3531 3530 2581 1251");
                    break;
                case "Common.LureSlot":
                    method_25(gkey_0_1, "6529 6530 6533 6532");
                    break;
                case "Common.Fish":
                    var matchingSpellGroup = GShortcut.FindMatchingSpellGroup("0x1dc4 7733 7734 18249 33100 19890");
                    if (matchingSpellGroup != null) gkey_0_1.SIM = matchingSpellGroup.ShortcutValue;
                    break;
            }
    }

    private void method_25(GKey gkey_0, string string_1)
    {
        method_26(gkey_0, string_1, false);
    }

    private void method_26(GKey gkey_0, string string_1, bool bool_1)
    {
        if (!gkey_0.NeedAutoUpdate)
            return;
        gkey_0.KType = GKeyType.ItemDefID;
        gkey_0.SIM = 0;
        gkey_0.ShiftState = 0;
        var matchingShortcut = GShortcut.FindMatchingShortcut(GShortcutType.Item, string_1);
        if (matchingShortcut == null)
        {
            if (!bool_1)
                return;
            GClass37.smethod_0(GClass30.smethod_2(884, gkey_0.KeyName));
        }
        else
        {
            gkey_0.SIM = matchingShortcut.ShortcutValue;
            gkey_0.NeedAutoUpdate = false;
        }
    }
}