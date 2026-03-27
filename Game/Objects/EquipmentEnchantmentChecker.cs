// Decompiled with JetBrains decompiler
// Type: EquipmentEnchantmentChecker
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common.Objects;
using System.Collections;
using System.Collections.Generic;

public class EquipmentEnchantmentChecker
{
    private const int int_0 = 8;
    public bool bool_0;
    public OffsetManager gclass43_0;
    public OffsetManager gclass43_1;
    public int int_1;
    public SortedList Offsets;

    private static readonly string[] string_0 =
    {
        "HeadSlot",
        "NeckSlot",
        "ShoulderSlot",
        "ShirtSlot",
        "ChestSlot",
        "WaistSlot",
        "LegsSlot",
        "FeetSlot",
        "WristSlot",
        "HandsSlot",
        "Finger0Slot",
        "Finger1Slot",
        "Trinket0Slot",
        "Trinket1Slot",
        "BackSlot",
        "MainHandSlot",
        "SecondaryHandSlot",
        "RangedSlot",
        "TabardSlot"
    };

    public void method_0()
    {
        Offsets = new SortedList();
        if (MemoryOffsetTable.Instance.HasOffset("SlotNameCount") && MemoryOffsetTable.Instance.HasOffset("SlotName"))
        {
            var num1 = GameMemoryAccess.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("SlotNameCount"), "SlotNameCount");
            var num2 = GameMemoryAccess.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("SlotName"), "SlotName");
            if (num1 > 0 && num1 <= 64 && num2 > 65536)
            {
                for (var index = 0; index < num1; ++index)
                {
                    var str = GameMemoryAccess.ReadString(
                        GameMemoryAccess.ReadInt32(num2 + index * 16, "ItemBase"), 100, "ItemBaseName");
                    var num3 = GameMemoryAccess.ReadInt32(num2 + index * 16 + 8, "ItemBaseID");
                    if (str != null && str.Length > 0 && num3 > 0 && !Offsets.ContainsKey(str))
                        Offsets.Add(str, num3);
                }
            }
        }

        if (Offsets.Count == 0)
            for (var index = 0; index < string_0.Length; ++index)
                Offsets.Add(string_0[index], index + 1);

        bool_0 = Offsets.Count > 0;
    }

    public ulong method_1(string string_0)
    {
        if (!bool_0)
            return 0UL;
        if (!Offsets.ContainsKey(string_0))
        {
            Logger.LogMessage("Bogus slot name: " + string_0);
            return 0UL;
        }

        var num1 = (int)Offsets[string_0] - 1;
        var num2 = StartupClass.PlayerOffsetManager.GetOffsetValue("PLAYER_FIELD_INV_SLOT_HEAD");
        return GameMemoryAccess.ReadInt64(GPlayerSelf.Me.StorageAddress + num2 + num1 * 8, "Equipped/" + string_0);
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
            var num = StartupClass.ItemOffsetManager.GetOffsetValue("ITEM_FIELD_ENCHANTMENT") + index * 4 * 3;
            var int_2 = GameMemoryAccess.ReadInt32(gobject_0.StorageAddress + num, "EnchantID");
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
        var num1 = MemoryOffsetTable.Instance.GetIntOffset("EnchantRowJump");
        var num2 = MemoryOffsetTable.Instance.GetIntOffset("EnchantRowCount");
        var num3 = 0;
        if (MemoryOffsetTable.Instance.HasOffset("SpellNameRLE"))
            num3 = -1;
        var num4 = GameMemoryAccess.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("EnchantNames") + num1, "EnchantNamesBase");
        var num5 = GameMemoryAccess.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("EnchantNames") - num2, "EnchantNamesCount");
        Logger.smethod_1("Rows @ 0x" + (MemoryOffsetTable.Instance.GetIntOffset("EnchantNames") + num1).ToString("x") + " = 0x" +
                           num4.ToString("x"));
        Logger.smethod_1("Count @ 0x" + (MemoryOffsetTable.Instance.GetIntOffset("EnchantNames") - num2).ToString("x") +
                           " = 0x" + num5.ToString("x"));
        if (int_2 > num5)
            return "(enchantid out of range!)";
        Logger.smethod_1("The row @ " + (num4 + (int_2 + num3) * 4).ToString("x"));
        var num6 = GameMemoryAccess.ReadInt32(num4 + (int_2 + num3) * 4, "EnchantRow");
        if (num6 == 0)
            return "(no row for that enchantid: " + int_2 + ")";
        var int_29 = GameMemoryAccess.ReadInt32(num6 + MemoryOffsetTable.Instance.GetIntOffset("EnchantRowOffset"), "EnchantNamePtr");
        if (int_29 == 0)
            return "(null pointer to enchant name!)";
        var str = GameMemoryAccess.ReadString(int_29, 100, "EnchantName");
        Logger.smethod_1("EnchantName: \"" + str + "\"");
        return str;
    }

    public InventoryItem[] method_4()
    {
        var list_0 = new List<InventoryItem>();
        var int_4_1 = StartupClass.PlayerOffsetManager.GetOffsetValue("PLAYER_FIELD_PACK_SLOT_1");
        method_5(list_0, GPlayerSelf.Me.StorageAddress, 16, int_4_1);
        for (var index = 1; index < 5; ++index)
        {
            var num = StartupClass.PlayerOffsetManager.GetOffsetValue("PLAYER_FIELD_INV_SLOT_HEAD") + 144 + index * 8;
            var GUID = GameMemoryAccess.ReadInt64(GPlayerSelf.Me.StorageAddress + num, "BagGuid1");
            if (GUID != 0UL)
            {
                var gobject = GObjectList.FindObject(GUID);
                if (gobject == null)
                {
                    Logger.smethod_1("! Null bag: " + GUID.ToString("x"));
                }
                else
                {
                    var int_3 = GameMemoryAccess.ReadInt32(
                        gobject.StorageAddress + StartupClass.ContainerOffsetManager.GetOffsetValue("CONTAINER_FIELD_NUM_SLOTS"),
                        "NumSlots");
                    var int_4_2 = StartupClass.ContainerOffsetManager.GetOffsetValue("CONTAINER_FIELD_SLOT_1");
                    method_5(list_0, gobject.StorageAddress, int_3, int_4_2);
                }
            }
        }

        return list_0.ToArray();
    }

    private void method_5(List<InventoryItem> list_0, uint int_2, int int_3, int int_4)
    {
        for (var index = 0; index < int_3; ++index)
        {
            var num1 = int_4 + index * 8;
            var num2 = GameMemoryAccess.ReadInt64(int_2 + (uint)num1, "ItemGUID");
            if (num2 != 0UL)
            {
                var gobject = GObjectList.FindObject(num2);
                if (gobject == null)
                {
                    Logger.smethod_1("! Couldn't find item in list: " + num2.ToString("x"));
                }
                else
                {
                    var int_1 = GameMemoryAccess.ReadInt32(
                        gobject.StorageAddress + StartupClass.ItemOffsetManager.GetOffsetValue("OBJECT_FIELD_ENTRY"), "ItemEntry");
                    var string_1 = "(unknown)";
                    var gclass39 = new InventoryItem(num2, int_1, string_1);
                    list_0.Add(gclass39);
                }
            }
        }
    }
}

