// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GItemDefinition
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
namespace Glider.Common.Objects
{
    public class GItemDefinition
    {
        private byte _conjureFlag;

        public GItemDefinition(int ItemID)
        {
            Quality = GItemQuality.Unknown;
            Name = "";
            this.ItemID = ItemID;
            StackSize = 0;
            _conjureFlag = 0;
            SpellID = 0;
            LoadFields();
        }

        public int ItemID { get; }

        public int SpellID { get; private set; }

        public GItemQuality Quality { get; private set; }

        public string Name { get; private set; }

        public int StackSize { get; private set; }

        public bool IsConjured => (_conjureFlag & 2) > 0;

        public override string ToString()
        {
            return "GItemDefinition(Quality=" + Quality + ")";
        }

        private void LoadFields()
        {
            var itemData = FindItemData();
            if (itemData == 0)
                return;
            Quality = (GItemQuality)GProcessMemoryManipulator.smethod_15(itemData + GClass18.gclass18_0.method_4("ItemDefQuality"),
                "itemquality");
            StackSize = GProcessMemoryManipulator.smethod_11(itemData + GClass18.gclass18_0.method_4("ItemDefStackSize"), "stacksize");
            SpellID = GProcessMemoryManipulator.smethod_11(itemData + GClass18.gclass18_0.method_4("ItemDefSpellID"), "spellid");
            var int_29 = GProcessMemoryManipulator.smethod_11(itemData + GClass18.gclass18_0.method_4("ItemDefName"), "itemname");
            if (int_29 != 0)
                Name = GProcessMemoryManipulator.smethod_10(int_29, 100, "itemdefname1");
            _conjureFlag = GProcessMemoryManipulator.smethod_15(itemData + GClass18.gclass18_0.method_4("ConjureFlag"), "conjureflag");
        }

        private int FindItemData()
        {
            var num1 = 10;
            var num2 = GProcessMemoryManipulator.smethod_11(
                GClass18.gclass18_0.method_4("ItemDBBase") + GClass18.gclass18_0.method_4("ItemDBMask"), "itemdbm");
            var num3 = GProcessMemoryManipulator.smethod_11(
                GClass18.gclass18_0.method_4("ItemDBBase") + GClass18.gclass18_0.method_4("ItemDBList"), "itemdbl");
            var num4 = (ItemID & num2) * 3;
            var num5 = GProcessMemoryManipulator.smethod_11(num3 + num4 * 4, "stepsize");
            var int_29 = GProcessMemoryManipulator.smethod_11(num3 + num4 * 4 + 4 + 4, "itemptr");
            while (true)
            {
                --num1;
                if (num1 != 0)
                {
                    if (int_29 != 0)
                    {
                        if ((int_29 & 1) == 0)
                        {
                            if (GProcessMemoryManipulator.smethod_11(int_29, "subitemptr") != ItemID)
                                int_29 = GProcessMemoryManipulator.smethod_11(int_29 + num5 + 4, "itemnext");
                            else
                                goto label_9;
                        }
                        else
                        {
                            goto label_8;
                        }
                    }
                    else
                    {
                        goto label_7;
                    }
                }
                else
                {
                    break;
                }
            }

            Logger.smethod_1("!! Spun out trying to find item: 0x" + ItemID.ToString("x"));
            return 0;
            label_7:
            Logger.smethod_1("! SubitemPointer is null for 0x" + ItemID.ToString("x"));
            return 0;
            label_8:
            Logger.smethod_1("! SubitemPointer is not right (0x" + int_29.ToString("x") + ") for 0x" +
                               ItemID.ToString("x"));
            return 0;
            label_9:
            return int_29;
        }
    }
}