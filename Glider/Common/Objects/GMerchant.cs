// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GMerchant
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections.Generic;
using System.Threading;

namespace Glider.Common.Objects
{
    public class GMerchant
    {
        private GInterfaceObject _nextButton;
        private GInterfaceObject _prevButton;
        private readonly int ForceGossipIndex = -1;
        private GInterfaceObject Frame;
        private string LastItemBought;

        public GMerchant()
        {
            StartupStuff();
        }

        public GMerchant(int ForceGossipIndex)
        {
            this.ForceGossipIndex = ForceGossipIndex;
            StartupStuff();
        }

        public bool IsRepairVisible
        {
            get
            {
                var byName = GContext.Main.Interface.GetByName("MerchantRepairAllButton");
                return byName != null && byName.IsVisible;
            }
        }

        public bool IsRepairEnabled
        {
            get
            {
                var byName = GContext.Main.Interface.GetByName("MerchantRepairAllButton");
                return byName != null && byName.IsVisible && byName.IsEnabledInFrame;
            }
        }

        public bool IsVisible => Frame != null && Frame.IsVisible;

        public bool IsPrevVisible => _prevButton.IsVisible;

        public bool IsNextVisible => _nextButton.IsVisible;

        public bool IsPrevEnabled => _prevButton.IsEnabledInFrame;

        public bool IsNextEnabled => _nextButton.IsEnabledInFrame;

        private void StartupStuff()
        {
            Frame = GContext.Main.Interface.GetByName("MerchantFrame");
            if (Frame == null)
                GContext.Main.Log("Can't find MerchantFrame... !?");
            _prevButton = GContext.Main.Interface.GetByName("MerchantPrevPageButton");
            _nextButton = GContext.Main.Interface.GetByName("MerchantNextPageButton");
            BypassGossip();
        }

        private void BypassGossip()
        {
            Thread.Sleep(777);
            if (IsVisible)
            {
                GClass37.smethod_1("Merchant frame visible, skipping gossip check");
            }
            else if (!GContext.Main.Interface.GetByName("GossipFrame").IsVisible)
            {
                GClass37.smethod_0("No gossip frame, skipping gossip check");
            }
            else
            {
                var num = 0;
                if (ForceGossipIndex == -1)
                {
                    GClass30.smethod_1(878).ToLower();
                    for (var index = 1; index < 5; ++index)
                    {
                        var ObjectName = "GossipTitleButton" + index;
                        var byName = GContext.Main.Interface.GetByName(ObjectName);
                        if (byName != null)
                        {
                            if (byName.IsVisible)
                                num = index;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (num == 0)
                    {
                        GClass37.smethod_0("No gossip buttons visible, skipping gossip check");
                        return;
                    }
                }
                else
                {
                    num = ForceGossipIndex;
                }

                var ObjectName1 = "GossipTitleButton" + num;
                var byName1 = GContext.Main.Interface.GetByName(ObjectName1);
                if (byName1 == null)
                    return;
                byName1.ClickMouse(false);
                Thread.Sleep(1777);
            }
        }

        public void ClickRepairButton()
        {
            var byName = GContext.Main.Interface.GetByName("MerchantRepairAllButton");
            if (byName == null || !byName.IsVisible || !byName.IsEnabledInFrame)
                return;
            byName.ClickMouse(false);
        }

        public string[] GetListedItems()
        {
            var stringList = new List<string>();
            if (!IsVisible)
                return stringList.ToArray();
            for (var index = 1; index < 12; ++index)
            {
                var ObjectName = "MerchantItem" + index;
                var byName = GContext.Main.Interface.GetByName(ObjectName);
                if (GContext.Main.Interface.GetByName(ObjectName + "ItemButton").IsVisible && byName != null &&
                    byName.IsVisible)
                {
                    var childObject = byName.GetChildObject("MerchantItem" + index + "Name");
                    if (childObject != null)
                        stringList.Add(childObject.LabelText);
                    else
                        break;
                }
                else
                {
                    break;
                }
            }

            return stringList.ToArray();
        }

        public void ClickNext()
        {
            _nextButton.ClickMouse(false);
            Thread.Sleep(888);
        }

        public void ClickPrev()
        {
            _prevButton.ClickMouse(false);
            Thread.Sleep(888);
        }

        public void Close()
        {
            if (IsVisible)
                GClass42.gclass42_0.method_0("Common.Escape");
            Thread.Sleep(777);
            GClass73.smethod_1();
        }

        public bool BuyOnAnyPage(string ItemName)
        {
            if (LastItemBought == null || ItemName != LastItemBought)
                while (IsPrevVisible && IsPrevEnabled)
                    ClickPrev();
            while (!BuyOnThisPage(ItemName))
                if (IsNextVisible && IsNextEnabled)
                {
                    ClickNext();
                }
                else
                {
                    GContext.Main.Log("Never found item at merchant: \"" + ItemName + "\"");
                    return false;
                }

            LastItemBought = ItemName;
            return true;
        }

        public bool BuyOnThisPage(string ItemName)
        {
            var listedItems = GetListedItems();
            for (var index = 0; index < listedItems.Length; ++index)
                if (listedItems[index].ToLower() == ItemName.ToLower())
                {
                    var ObjectName = "MerchantItem" + (index + 1) + "ItemButton";
                    var byName = GContext.Main.Interface.GetByName(ObjectName);
                    if (byName == null)
                    {
                        GContext.Main.Log("!! Missing merchant item button: \"" + ObjectName + "\"");
                        return false;
                    }

                    byName.ClickMouse(true);
                    return true;
                }

            return false;
        }
    }
}