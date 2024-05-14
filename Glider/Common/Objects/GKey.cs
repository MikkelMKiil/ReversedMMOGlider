// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GKey
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
namespace Glider.Common.Objects
{
    public class GKey
    {
        public const int SS_NONE = 0;
        public const int SS_SHIFT = 1;
        public const int SS_CTRL = 2;
        public const int SS_ALT = 4;
        public const char CHAR_NONE = '\0';
        public const short VK_NONE = 0;
        public bool AutoUpdate;
        public GBarState BarState;
        public char CharCode;
        private bool ComplainedVisible;
        private bool Filled;
        public string KeyName;
        public GKeyType KType;
        public int LiveSIM;
        public bool NeedAutoUpdate;
        private int[] SameSpells;
        public int ShiftState;
        public int SIM;
        public short VK;

        public GKey(string KeyName)
        {
            this.KeyName = KeyName;
            CharCode = char.MinValue;
            VK = 0;
            ShiftState = 0;
            BarState = GBarState.Indifferent;
            SIM = 0;
            LiveSIM = 0;
            AutoUpdate = false;
        }

        public bool IsUndefined
        {
            get
            {
                switch (KType)
                {
                    case GKeyType.Char:
                        return CharCode == char.MinValue;
                    case GKeyType.VChar:
                        return VK == 0;
                    case GKeyType.SpellID:
                        return LiveSIM == 0;
                    case GKeyType.ItemDefID:
                        return LiveSIM == 0;
                    case GKeyType.Macro:
                        return LiveSIM == 0;
                    default:
                        return true;
                }
            }
        }

        public override string ToString()
        {
            return "(GKey, name=" + KeyName + ", type=" + KType + ", SIM=0x" + SIM.ToString("x") + ", LiveSIM=0x" +
                   LiveSIM.ToString("x") + ", CharCode=" + CharCode + ")";
        }

        public GKey Clone()
        {
            var NewGuy = new GKey(KeyName);
            CopyTo(NewGuy);
            return NewGuy;
        }

        public void CopyTo(GKey NewGuy)
        {
            NewGuy.KType = KType;
            NewGuy.CharCode = CharCode;
            NewGuy.VK = VK;
            NewGuy.CharCode = CharCode;
            NewGuy.ShiftState = ShiftState;
            NewGuy.BarState = BarState;
            NewGuy.SIM = SIM;
            NewGuy.LiveSIM = LiveSIM;
            NewGuy.AutoUpdate = AutoUpdate;
        }

        protected void ShiftThisKey(bool KeyDown)
        {
            if ((ShiftState & 1) != 0)
                GClass55.smethod_0(16, KeyDown);
            if ((ShiftState & 2) != 0)
                GClass55.smethod_0(17, KeyDown);
            if ((ShiftState & 4) == 0)
                return;
            GClass55.smethod_0(18, KeyDown);
        }

        public void EnsureBar()
        {
            var gbarState =
                (GBarState)(GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("ActionBarCurrent"), "curbar") + 3);
            if (BarState == GBarState.Indifferent || BarState == gbarState)
                return;
            if (BarState == GBarState.Combat)
                GClass42.gclass42_0.method_0("Common.BarCombat");
            if (BarState == GBarState.Rest)
                GClass42.gclass42_0.method_0("Common.BarRest");
            if (BarState != GBarState.Bar1 && BarState != GBarState.Bar2 && BarState != GBarState.Bar3 &&
                BarState != GBarState.Bar4 && BarState != GBarState.Bar5 && BarState != GBarState.Bar6)
                return;
            GClass42.gclass42_0.method_0("Common." + BarState);
        }

        public void Send()
        {
            if (CharCode == char.MinValue && VK == 0)
            {
                Logger.smethod_1(MessageProvider.smethod_2(57, KeyName));
            }
            else
            {
                EnsureBar();
                ShiftThisKey(true);
                if (CharCode == char.MinValue)
                    GClass55.smethod_9(VK);
                else
                    GClass55.smethod_6(char.ToLower(CharCode));
                ShiftThisKey(false);
            }
        }

        public void Press()
        {
            SendAsPress(true);
        }

        public void Release()
        {
            SendAsPress(false);
        }

        protected void SendAsPress(bool Pressing)
        {
            if (CharCode == char.MinValue && VK == 0)
            {
                Logger.smethod_1(MessageProvider.smethod_2(57, KeyName));
            }
            else
            {
                EnsureBar();
                ShiftThisKey(true);
                if (CharCode == char.MinValue)
                    GClass55.smethod_0(VK, Pressing);
                else
                    GClass55.smethod_4(CharCode, Pressing);
                ShiftThisKey(false);
            }
        }

        public void FilloutKey()
        {
            FilloutKey(false);
        }

        public void FilloutKey(bool FailQuietly)
        {
            if (Filled)
                return;
            if (!StartupClass.bool_13)
            {
                UnFill();
            }
            else
            {
                LiveSIM = 0;
                if (KType == GKeyType.VChar)
                {
                    Filled = true;
                }
                else if (KType == GKeyType.Char)
                {
                    var num1 = 0;
                    var num2 = 1;
                    if (BarState == GBarState.Combat || BarState == GBarState.Indifferent || BarState == GBarState.Rest)
                        return;
                    var num3 = StartupClass.numbers_string.IndexOf(CharCode);
                    if (num3 == -1)
                        return;
                    var num4 = (int)(BarState - 3);
                    if (BarState == GBarState.Bar1)
                    {
                        var stance = GPlayerSelf.Me.Stance;
                        if (stance == GStance.Battle || stance == GStance.Stealth || stance == GStance.Shadow ||
                            stance == GStance.Cat)
                            num1 = 72;
                        if (stance == GStance.Defensive)
                            num1 = 84;
                        if (stance == GStance.Berserker || stance == GStance.Bear)
                            num1 = 96;
                    }

                    var gshortcut = new GShortcut(num2 + num1 + num4 * 12 + num3);
                    if (gshortcut.ShortcutType != GShortcutType.Macro)
                        LiveSIM = gshortcut.ShortcutValue;
                    Filled = true;
                }
                else
                {
                    if (KType != GKeyType.ItemDefID && KType != GKeyType.Macro && KType != GKeyType.SpellID)
                        return;
                    LiveSIM = 0;
                    for (var SlotNumber = 1; SlotNumber < 109; ++SlotNumber)
                    {
                        var gshortcut = new GShortcut(SlotNumber);
                        if (KType != GKeyType.Macro || gshortcut.ShortcutType != GShortcutType.Macro ||
                            SIM != gshortcut.ShortcutValue || !MapSlotToBars(SlotNumber))
                        {
                            if (KType != GKeyType.ItemDefID || gshortcut.ShortcutType != GShortcutType.Item ||
                                SIM != gshortcut.ShortcutValue || !MapSlotToBars(SlotNumber))
                            {
                                if (KType == GKeyType.SpellID && gshortcut.ShortcutType == GShortcutType.Spell)
                                {
                                    var flag = false;
                                    if (SameSpells == null)
                                        SameSpells = StartupClass.gclass63_0.method_13(SIM);
                                    if (SameSpells == null)
                                        return;
                                    foreach (var sameSpell in SameSpells)
                                        if (sameSpell == gshortcut.ShortcutValue)
                                            flag = true;
                                    if (flag && MapSlotToBars(SlotNumber))
                                    {
                                        LiveSIM = gshortcut.ShortcutValue;
                                        Filled = true;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                LiveSIM = new GItemDefinition(SIM).SpellID;
                                StartupClass.gclass63_0.method_9(LiveSIM);
                                Filled = true;
                                return;
                            }
                        }
                        else
                        {
                            Filled = true;
                            return;
                        }
                    }

                    if (!FailQuietly)
                        Logger.smethod_1("! Could not find this spell on any action bar: " + ToString());
                    Filled = true;
                }
            }
        }

        public void UnFill()
        {
            Filled = false;
            SameSpells = null;
        }

        protected bool MapSlotToBars(int SlotNumber)
        {
            var num = -1;
            var index = (SlotNumber - 1) % 12;
            if (SlotNumber >= 13 && SlotNumber <= 72)
            {
                num = (SlotNumber - 1) / 12;
            }
            else
            {
                var flag = false;
                var stance = GPlayerSelf.Me.Stance;
                if (stance == GStance.Battle || stance == GStance.Stealth || stance == GStance.Shadow ||
                    stance == GStance.Cat)
                {
                    flag = true;
                    if (SlotNumber >= 73 && SlotNumber <= 84)
                        num = 0;
                }

                if (stance == GStance.Berserker || stance == GStance.Bear)
                {
                    flag = true;
                    if (SlotNumber >= 97 && SlotNumber <= 108)
                        num = 0;
                }

                if (stance == GStance.Defensive)
                {
                    flag = true;
                    if (SlotNumber >= 85 && SlotNumber <= 96)
                        num = 0;
                }

                if (SlotNumber >= 1 && SlotNumber <= 12 && !flag)
                    num = 0;
                if (num == -1)
                    return false;
            }

            BarState = (GBarState)(3 + num);
            CharCode = StartupClass.numbers_string[index];
            return true;
        }

        public string FindVisibleInterfaceObject()
        {
            FilloutKey();
            if (SIM == 0)
                return null;
            var StartSlotIndex = 1;
            var num = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("ActionBarCurrent"), "abcurrent");
            if (num == 0)
            {
                var stance = GPlayerSelf.Me.Stance;
                if (stance == GStance.Battle || stance == GStance.Stealth || stance == GStance.Shadow ||
                    stance == GStance.Cat)
                    StartSlotIndex += 72;
                if (stance == GStance.Defensive)
                    StartSlotIndex += 84;
                if (stance == GStance.Bear || stance == GStance.Battle)
                    StartSlotIndex += 96;
            }
            else
            {
                StartSlotIndex += 12 * num;
            }

            string HitName = null;
            if (ScanForShortcut(StartSlotIndex >= 72 ? "BonusAction" : "Action", StartSlotIndex, out HitName) ||
                ScanForShortcut("MultiBarBottomLeft", 61, out HitName) ||
                ScanForShortcut("MultiBarBottomRight", 49, out HitName) ||
                ScanForShortcut("MultiBarRight", 25, out HitName) || ScanForShortcut("MultiBarLeft", 37, out HitName))
                return HitName;
            if (!ComplainedVisible)
            {
                ComplainedVisible = true;
                Logger.smethod_1("Could not find interface object for " + KeyName +
                                   ", make sure it's visible somewhere!");
            }

            return null;
        }

        private bool ScanForShortcut(string BaseObjectName, int StartSlotIndex, out string HitName)
        {
            HitName = null;
            for (var index = 0; index < 12; ++index)
                if (MatchesShortcut(new GShortcut(StartSlotIndex + index)))
                {
                    var string_1 = BaseObjectName + "Button" + (index + 1);
                    var gclass8 = GClass8.smethod_2(string_1);
                    if (gclass8 != null && gclass8.method_10())
                    {
                        HitName = string_1;
                        return true;
                    }
                }

            return false;
        }

        public bool MatchesShortcut(GShortcut gshortcut_0)
        {
            return (gshortcut_0.ShortcutValue == SIM &&
                    ((gshortcut_0.ShortcutType == GShortcutType.Macro && KType == GKeyType.Macro) ||
                     (gshortcut_0.ShortcutType == GShortcutType.Item && KType == GKeyType.ItemDefID))) ||
                   (gshortcut_0.ShortcutValue == LiveSIM && gshortcut_0.ShortcutType == GShortcutType.Spell &&
                    KType == GKeyType.SpellID);
        }
    }
}