// Decompiled with JetBrains decompiler
// Type: GClass38
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections;
using System.Collections.Generic;
using Glider.Common.Objects;

public class GClass38
{
    private const int int_0 = 8;
    public bool bool_0;
    public OffsetManager gclass43_0;
    public OffsetManager gclass43_1;
    public int int_1;
    public SortedList sortedList_0;

    public void method_0()
    {
        sortedList_0 = new SortedList();
        var num1 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("SlotNameCount"), "SlotNameCount");
        var num2 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("SlotName"), "SlotName");
        for (var index = 0; index < num1; ++index)
            sortedList_0.Add(
                GProcessMemoryManipulator.smethod_9(GProcessMemoryManipulator.smethod_11(num2 + index * 16, "ItemBase"), 100, "ItemBaseName"),
                GProcessMemoryManipulator.smethod_11(num2 + index * 16 + 8, "ItemBaseID"));
        bool_0 = sortedList_0.Count > 0;
    }

    public long method_1(string string_0)
    {
        if (!bool_0)
            return 0;
        if (!sortedList_0.ContainsKey(string_0))
        {
            Logger.LogMessage("Bogus slot name: " + string_0);
            return 0;
        }

        var num1 = (int)sortedList_0[string_0] - 1;
        var num2 = StartupClass.gclass43_0.GetOffsetValue("PLAYER_FIELD_INV_SLOT_HEAD");
        return GProcessMemoryManipulator.smethod_12(GPlayerSelf.Me.StorageAddress + num2 + num1 * 8, "Equipped/" + string_0);
    }

    public bool method_2(GObject gobject_0, string string_0)
    {
        if (!bool_0)
        {
            Logger.smethod_1("Can't check for buff, setup is not good!");
            return true;
        }

        Logger.smethod_1("Check item " + gobject_0.GUID.ToString("x") + " for buffs");
        for (var index = 0; index < 8; ++index)
        {
            var num = StartupClass.gclass43_3.GetOffsetValue("ITEM_FIELD_ENCHANTMENT") + index * 4 * 3;
            var int_2 = GProcessMemoryManipulator.smethod_11(gobject_0.StorageAddress + num, "EnchantID");
            if (int_2 > 0)
            {
                Logger.smethod_1("EnchantID: " + int_2);
                var str = method_3(int_2);
                Logger.smethod_1("Enchant name: " + str);
                if (str.ToLower().IndexOf(string_0) > -1)
                    return true;
            }
        }

        return false;
    }

    public string method_3(int int_2)
    {
        Logger.smethod_1("GetEnchantmentName: " + int_2.ToString("x"));
        var num1 = GClass18.gclass18_0.method_4("EnchantRowJump");
        var num2 = GClass18.gclass18_0.method_4("EnchantRowCount");
        var num3 = 0;
        if (GClass18.gclass18_0.method_5("SpellNameRLE"))
            num3 = -1;
        var num4 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("EnchantNames") + num1, "EnchantNamesBase");
        var num5 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("EnchantNames") - num2, "EnchantNamesCount");
        Logger.smethod_1("Rows @ 0x" + (GClass18.gclass18_0.method_4("EnchantNames") + num1).ToString("x") + " = 0x" +
                           num4.ToString("x"));
        Logger.smethod_1("Count @ 0x" + (GClass18.gclass18_0.method_4("EnchantNames") - num2).ToString("x") +
                           " = 0x" + num5.ToString("x"));
        if (int_2 > num5)
            return "(enchantid out of range!)";
        Logger.smethod_1("The row @ " + (num4 + (int_2 + num3) * 4).ToString("x"));
        var num6 = GProcessMemoryManipulator.smethod_11(num4 + (int_2 + num3) * 4, "EnchantRow");
        if (num6 == 0)
            return "(no row for that enchantid: " + int_2 + ")";
        var int_29 = GProcessMemoryManipulator.smethod_11(num6 + GClass18.gclass18_0.method_4("EnchantRowOffset"), "EnchantNamePtr");
        if (int_29 == 0)
            return "(null pointer to enchant name!)";
        var str = GProcessMemoryManipulator.smethod_9(int_29, 100, "EnchantName");
        Logger.smethod_1("EnchantName: \"" + str + "\"");
        return str;
    }

    public GClass39[] method_4()
    {
        var list_0 = new List<GClass39>();
        var int_4_1 = StartupClass.gclass43_0.GetOffsetValue("PLAYER_FIELD_PACK_SLOT_1");
        method_5(list_0, GPlayerSelf.Me.StorageAddress, 16, int_4_1);
        for (var index = 1; index < 5; ++index)
        {
            var num = StartupClass.gclass43_0.GetOffsetValue("PLAYER_FIELD_INV_SLOT_HEAD") + 144 + index * 8;
            var GUID = GProcessMemoryManipulator.smethod_12(GPlayerSelf.Me.StorageAddress + num, "BagGuid1");
            if (GUID != 0L)
            {
                var gobject = GObjectList.FindObject(GUID);
                if (gobject == null)
                {
                    Logger.smethod_1("! Null bag: " + GUID.ToString("x"));
                }
                else
                {
                    var int_3 = GProcessMemoryManipulator.smethod_11(
                        gobject.StorageAddress + StartupClass.gclass43_4.GetOffsetValue("CONTAINER_FIELD_NUM_SLOTS"),
                        "NumSlots");
                    var int_4_2 = StartupClass.gclass43_4.GetOffsetValue("CONTAINER_FIELD_SLOT_1");
                    method_5(list_0, gobject.StorageAddress, int_3, int_4_2);
                }
            }
        }

        return list_0.ToArray();
    }

    private void method_5(List<GClass39> list_0, int int_2, int int_3, int int_4)
    {
        for (var index = 0; index < int_3; ++index)
        {
            var num1 = int_4 + index * 8;
            var num2 = GProcessMemoryManipulator.smethod_12(int_2 + num1, "ItemGUID");
            if (num2 != 0L)
            {
                var gobject = GObjectList.FindObject(num2);
                if (gobject == null)
                {
                    Logger.smethod_1("! Couldn't find item in list: " + num2.ToString("x"));
                }
                else
                {
                    var int_1 = GProcessMemoryManipulator.smethod_11(
                        gobject.StorageAddress + StartupClass.gclass43_3.GetOffsetValue("OBJECT_FIELD_ENTRY"), "ItemEntry");
                    var string_1 = "(unknown)";
                    var gclass39 = new GClass39(num2, int_1, string_1);
                    list_0.Add(gclass39);
                }
            }
        }
    }
}