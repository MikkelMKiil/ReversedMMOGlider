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
        public ShortcutMatchState LastShortcutMatchState;
        public string LastShortcutMatchDetails;
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
            LastShortcutMatchState = ShortcutMatchState.Missing;
            LastShortcutMatchDetails = "";
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
                InputController.SendKey(16, KeyDown);
            if ((ShiftState & 2) != 0)
                InputController.SendKey(17, KeyDown);
            if ((ShiftState & 4) == 0)
                return;
            InputController.SendKey(18, KeyDown);
        }

        public void EnsureBar()
        {
            var gbarState =
                (GBarState)(GameMemoryAccess.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("ActionBarCurrent"), "curbar") + 3);
            if (BarState == GBarState.Indifferent || BarState == gbarState)
                return;
            if (BarState == GBarState.Combat)
                SpellcastingManager.gclass42_0.method_0("Common.BarCombat");
            if (BarState == GBarState.Rest)
                SpellcastingManager.gclass42_0.method_0("Common.BarRest");
            if (BarState != GBarState.Bar1 && BarState != GBarState.Bar2 && BarState != GBarState.Bar3 &&
                BarState != GBarState.Bar4 && BarState != GBarState.Bar5 && BarState != GBarState.Bar6)
                return;
            SpellcastingManager.gclass42_0.method_0("Common." + BarState);
        }

        public void Send()
        {
            if (CharCode == char.MinValue && VK == 0)
            {
                if (KeyName == "Common.ToggleCombat")
                    Logger.LogMessage("[Critical] [Input][AutoAttack] " + KeyName + " is not bound (CharCode/VK are empty). Configure your WoW Attack/StartAttack bind or set this key in Edit Keys.");
                Logger.smethod_1(MessageProvider.smethod_2(57, KeyName));
            }
            else
            {
                EnsureBar();
                ShiftThisKey(true);
                if (CharCode == char.MinValue)
                    InputController.TapKey(VK);
                else
                    InputController.smethod_6(char.ToLower(CharCode));
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
                    InputController.SendKey(VK, Pressing);
                else
                    InputController.smethod_4(CharCode, Pressing);
                ShiftThisKey(false);
            }
        }

        public void FilloutKey()
        {
            FilloutKey(false);
        }

        public void FilloutKey(bool FailQuietly)
        {
            FilloutKey(FailQuietly, null);
        }

        public void FilloutKey(bool FailQuietly, ShortcutSnapshot snapshot)
        {
            if (Filled)
                return;
            if (!StartupClass.IsRuntimeAttached)
            {
                UnFill();
            }
            else
            {
                LiveSIM = 0;
                LastShortcutMatchState = ShortcutMatchState.Missing;
                LastShortcutMatchDetails = "";
                if (KType == GKeyType.VChar)
                {
                    Filled = true;
                    LastShortcutMatchState = ShortcutMatchState.Usable;
                    LastShortcutMatchDetails = "virtual key";
                }
                else if (KType == GKeyType.Char)
                {
                    var me = GPlayerSelf.Me;
                    if (me == null)
                        return;

                    if (BarState == GBarState.Combat || BarState == GBarState.Indifferent || BarState == GBarState.Rest)
                        return;
                    var num3 = StartupClass.ActionBarCharacters.IndexOf(CharCode);
                    if (num3 == -1)
                        return;
                    var num4 = (int)(BarState - 3);
                    var primaryBarStart = ShortcutLayout335a.GetPrimaryBarStartSlot(me.PlayerClass, me.Stance);
                    var slotNumber = primaryBarStart + num4 * 12 + num3;

                    var activeSnapshot = snapshot ?? ShortcutSnapshotService.CaptureSnapshot("GKey.FilloutKey.Char", false);
                    var entry = activeSnapshot.GetEntry(slotNumber);
                    if (entry != null && entry.ShortcutType != GShortcutType.Macro)
                        LiveSIM = entry.ShortcutValue;

                    Filled = true;
                    LastShortcutMatchState = ShortcutMatchState.Usable;
                    LastShortcutMatchDetails = "char slot=" + slotNumber;
                }
                else
                {
                    if (KType != GKeyType.ItemDefID && KType != GKeyType.Macro && KType != GKeyType.SpellID)
                        return;

                    var activeSnapshot = snapshot ?? ShortcutSnapshotService.CaptureSnapshot("GKey.FilloutKey", false);
                    var matchResult = ShortcutSnapshotService.MatchKey(this, activeSnapshot);
                    ShortcutSnapshotService.ApplyBestMatchToKey(this, matchResult, FailQuietly);
                    LastShortcutMatchState = matchResult.State;
                    LastShortcutMatchDetails = matchResult.Details;
                    Filled = true;
                }
            }
        }

        public void UnFill()
        {
            Filled = false;
            SameSpells = null;
            LastShortcutMatchState = ShortcutMatchState.Missing;
            LastShortcutMatchDetails = "";
        }

        protected bool MapSlotToBars(int SlotNumber)
        {
            var index = (SlotNumber - 1) % 12;
            var me = GPlayerSelf.Me;
            if (me == null)
                return false;

            int num;
            if (!ShortcutLayout335a.TryMapSlotToBarIndex(SlotNumber, me.PlayerClass, me.Stance, out num))
                return false;

            if (index < 0 || index >= StartupClass.ActionBarCharacters.Length)
                return false;

            BarState = (GBarState)(3 + num);
            CharCode = StartupClass.ActionBarCharacters[index];
            return true;
        }

        public string FindVisibleInterfaceObject()
        {
            FilloutKey();
            if (SIM == 0)
                return null;

            var snapshot = ShortcutSnapshotService.CaptureSnapshot("GKey.FindVisibleInterfaceObject", false);
            var match = ShortcutSnapshotService.MatchKey(this, snapshot);
            var visibleEntry = match.BestVisibleEntry;
            if (visibleEntry != null && visibleEntry.IsVisible && visibleEntry.ButtonName != null)
                return visibleEntry.ButtonName;

            if (!ComplainedVisible)
            {
                ComplainedVisible = true;
                Logger.smethod_1("Could not find interface object for " + KeyName +
                                   ", make sure it's visible somewhere!");
            }

            return null;
        }
    }
}

