// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GItem
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;

namespace Glider.Common.Objects
{
    public class GItem : GObject
    {
        private long _contained;
        private int _durability;
        private int _durabilityMax;
        private int _flags;
        private int _stackSize;

        public GItem(int BaseAddress, int FrameNumber)
            : base(BaseAddress, FrameNumber)
        {
            SetType(GObjectType.Item);
            Definition = null;
            ItemDefID = 0;
            _durability = 0;
            _durabilityMax = 0;
            _stackSize = 0;
            _contained = 0L;
            _flags = 0;
        }

        public override string Name => Definition != null && Definition.Name != null
            ? Definition.Name
            : "GItem @ 0x" + BaseAddress.ToString("x");

        public int ItemDefID { get; private set; }

        public GItemDefinition Definition { get; private set; }

        public int DurabilityPoints
        {
            get
            {
                Refresh();
                return _durability;
            }
        }

        public int DurabilityMax
        {
            get
            {
                Refresh();
                return _durabilityMax;
            }
        }

        public int StackSize
        {
            get
            {
                Refresh();
                return _stackSize;
            }
        }

        public double Durability
        {
            get
            {
                Refresh();
                return _durabilityMax == 0 ? 1.0 : _durability / (double)_durabilityMax;
            }
        }

        public bool IsSoulbound
        {
            get
            {
                Refresh();
                return (_flags & 1) > 0;
            }
        }

        public long ContainerID
        {
            get
            {
                Refresh();
                return _contained;
            }
        }

        public GObject Container
        {
            get
            {
                Refresh();
                return _contained == 0L ? null : GObjectList.FindObject(_contained);
            }
        }

        public bool IsEquipped
        {
            get
            {
                foreach (var equippedItem in GPlayerSelf.Me.EquippedItems)
                    if (equippedItem == GUID)
                        return true;
                return false;
            }
        }

        public bool IsMailable
        {
            get
            {
                if (!GClass61.gclass61_0.method_5("SendMail") || IsProtected || IsSoulbound || Definition.IsConjured)
                    return false;
                var gitemQuality = (GItemQuality)Enum.Parse(typeof(GItemQuality),
                    GClass61.gclass61_0.method_2("VendType"), true);
                var str1 = GClass61.gclass61_0.method_2("VendMailList");
                var chArray = new char[1] { ',' };
                foreach (var str2 in str1.Split(chArray))
                    if (Name.ToLower() == str2.ToLower())
                        return true;
                return Definition.Quality > GItemQuality.Uncommon ||
                       ((gitemQuality != GItemQuality.Uncommon || Definition.Quality > GItemQuality.Uncommon) &&
                        Definition.Quality >= GItemQuality.Uncommon);
            }
        }

        public bool IsSellable => !IsMailable && !IsProtected && !IsSoulbound && !Definition.IsConjured &&
                                  Definition.Quality <= (GItemQuality)Enum.Parse(typeof(GItemQuality),
                                      GClass61.gclass61_0.method_2("VendType"), true);

        public bool IsProtected
        {
            get
            {
                var str1 = GClass61.gclass61_0.method_2("VendWhiteList");
                var chArray = new char[1] { ',' };
                foreach (var str2 in str1.Split(chArray))
                    if (Name.ToLower() == str2.ToLower())
                    {
                        GClass37.smethod_1("Protected: " + str2);
                        return true;
                    }

                return false;
            }
        }

        protected override void LoadFields()
        {
            base.LoadFields();
            ItemDefID = GetStorageInt("OBJECT_FIELD_ENTRY");
            _durability = GetStorageInt("ITEM_FIELD_DURABILITY");
            _durabilityMax = GetStorageInt("ITEM_FIELD_MAXDURABILITY");
            Definition = new GItemDefinition(ItemDefID);
            _stackSize = GetStorageInt("ITEM_FIELD_STACK_COUNT");
            _contained = GetStorageLong("ITEM_FIELD_CONTAINED");
            _flags = GetStorageInt("ITEM_FIELD_FLAGS");
        }

        public override string ToString()
        {
            var strArray1 = new string[5]
            {
                base.ToString(),
                ", ItemDefID=0x",
                ItemDefID.ToString("x"),
                ", ItemDef=",
                null
            };
            var strArray2 = strArray1;
            string str;
            if (Definition != null)
                str = Definition + ", Durability=" + Durability + ", StackSize=" + StackSize + ", Contained=0x" +
                      _contained.ToString("x") + ", Equipped=" + IsEquipped;
            else
                str = "(null!)";
            strArray2[4] = str;
            return string.Concat(strArray1);
        }

        public static GItem[] SearchForItemDef(int ItemDefID)
        {
            var gitemList = new List<GItem>();
            foreach (var gitem in GObjectList.GetItems())
                if (gitem.ItemDefID == ItemDefID)
                    gitemList.Add(gitem);
            return gitemList.ToArray();
        }
    }
}