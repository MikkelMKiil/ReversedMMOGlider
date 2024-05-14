// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GBagItem
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
namespace Glider.Common.Objects
{
    public class GBagItem
    {
        public GBagItem(GItem Item, string ContainerFrame, int Slot, int SlotCount)
        {
            this.Item = Item;
            this.ContainerFrame = ContainerFrame;
            this.Slot = Slot;
            this.SlotCount = SlotCount;
        }

        public GItem Item { get; }

        public string ContainerFrame { get; }

        public int Slot { get; }

        public int SlotCount { get; }

        public void Click(bool UseRight)
        {
            var ObjectName = ContainerFrame + "Item" + (SlotCount - Slot);
            var byName = GContext.Main.Interface.GetByName(ObjectName);
            if (byName == null)
            {
                GClass37.smethod_0("! Could not find interface object: " + ObjectName);
                GContext.Main.KillAction("NoMail1", false);
            }
            else
            {
                if (!byName.IsVisible)
                {
                    GContext.Main.SendKey("Common.BackpackAll");
                    GClass37.smethod_0("Only one bag - hit key again!");
                }

                if (!byName.IsVisible)
                {
                    GClass37.smethod_0("! Interface object not visible (default backpack key wrong?): " + byName);
                    GContext.Main.KillAction("NoMail2", false);
                }
                else
                {
                    byName.ClickMouse(UseRight);
                }
            }
        }
    }
}