// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GItemHelper
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections.Generic;

namespace Glider.Common.Objects
{
    public class GItemHelper
    {
        public void OnAttach()
        {
        }

        public long GetEquippedGUID(string SlotName)
        {
            return StartupClass.gclass38_0.method_1(SlotName);
        }

        public int[] GetItemEnchants(long ItemGUID)
        {
            var gobject = GObjectList.FindObject(ItemGUID);
            var intList = new List<int>();
            if (gobject == null)
            {
                GContext.Main.Debug("GetItemEnchants called on bogus GUID: 0x" + ItemGUID.ToString("x"));
                return intList.ToArray();
            }

            for (var index = 0; index < 12; ++index)
            {
                var num1 = StartupClass.gclass43_3.GetOffsetValue("ITEM_FIELD_ENCHANTMENT_" + (index + 1) + "_1");
                var num2 = GProcessMemoryManipulator.smethod_11(gobject.StorageAddress + num1, "EnchantID");
                if (num2 > 0)
                    intList.Add(num2);
            }

            return intList.ToArray();
        }

        public string GetEnchantName(int EnchantID)
        {
            return StartupClass.gclass38_0.method_3(EnchantID);
        }

        public void DebugItem(long GUID)
        {
            var itemEnchants = GetItemEnchants(GUID);
            GContext.Main.Log("Enchant count on 0x" + GUID.ToString("x") + ": " + itemEnchants.Length);
            foreach (var EnchantID in itemEnchants)
                GContext.Main.Log("0x" + EnchantID + " --> \"" + GetEnchantName(EnchantID) + "\"");
        }
    }
}