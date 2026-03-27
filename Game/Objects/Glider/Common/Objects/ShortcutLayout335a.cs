#nullable disable
namespace Glider.Common.Objects
{
    // WoW 3.3.5a (12340) action slot layout and shortcut value encoding.
    public static class ShortcutLayout335a
    {
        public const int MinSlot = 1;
        public const int MaxActionSlot = 120;
        public const int MaxPossessSlot = 132;
        public const int SlotStrideBytes = 4;

        public const uint ItemTypeMask = 0x80000000U;
        public const uint MacroTypeMask = 0x40000000U;
        public const int ActionIdMask = 0x0FFFFFFF;
        public const int MaxPlausibleSpellId = 0x0007FFFF;

        public static bool IsReadableSlot(int slotNumber)
        {
            return slotNumber >= MinSlot && slotNumber <= MaxPossessSlot;
        }

        public static bool IsPlayerActionSlot(int slotNumber)
        {
            return slotNumber >= MinSlot && slotNumber <= MaxActionSlot;
        }

        public static int GetSlotAddress(int actionBarShortcutsBase, int slotNumber)
        {
            return actionBarShortcutsBase + SlotStrideBytes * (slotNumber - 1);
        }

        public static void DecodeShortcut(uint rawShortcut, out GShortcutType shortcutType, out int shortcutValue)
        {
            if (rawShortcut == 0U)
            {
                shortcutType = GShortcutType.Empty;
                shortcutValue = 0;
                return;
            }

            if ((rawShortcut & ItemTypeMask) != 0U)
            {
                shortcutType = GShortcutType.Item;
                shortcutValue = (int)rawShortcut & ActionIdMask;
                return;
            }

            if ((rawShortcut & MacroTypeMask) != 0U)
            {
                shortcutType = GShortcutType.Macro;
                shortcutValue = (int)rawShortcut & ActionIdMask;
                return;
            }

            shortcutType = GShortcutType.Spell;
            shortcutValue = (int)rawShortcut;
        }

        public static bool IsDecodedShortcutPlausible(GShortcutType shortcutType, int shortcutValue)
        {
            if (shortcutType == GShortcutType.Empty)
                return shortcutValue == 0;

            if (shortcutValue <= 0)
                return false;

            if (shortcutType == GShortcutType.Spell)
                return shortcutValue <= MaxPlausibleSpellId;

            if (shortcutType == GShortcutType.Item || shortcutType == GShortcutType.Macro)
                return shortcutValue <= ActionIdMask;

            return false;
        }

        public static bool TryDecodeShortcut(uint rawShortcut, out GShortcutType shortcutType, out int shortcutValue)
        {
            DecodeShortcut(rawShortcut, out shortcutType, out shortcutValue);
            if (rawShortcut == 0U)
                return true;

            var hasItemMask = (rawShortcut & ItemTypeMask) != 0U;
            var hasMacroMask = (rawShortcut & MacroTypeMask) != 0U;
            if (hasItemMask && hasMacroMask)
                return false;

            return IsDecodedShortcutPlausible(shortcutType, shortcutValue);
        }

        public static bool IsRawShortcutPlausible(uint rawShortcut)
        {
            GShortcutType shortcutType;
            int shortcutValue;
            return TryDecodeShortcut(rawShortcut, out shortcutType, out shortcutValue);
        }

        public static int GetPrimaryBarStartSlot(GPlayerClass playerClass, GStance stance)
        {
            if (stance == GStance.Battle || stance == GStance.Stealth || stance == GStance.Shadow || stance == GStance.Cat)
                return 73;

            if (stance == GStance.Defensive)
                return 85;

            if (stance == GStance.Berserker || stance == GStance.Bear)
                return 97;

            // Moonkin/Tree forms are not represented in GStance; keep default at bar 1.
            return 1;
        }

        public static bool TryMapSlotToBarIndex(int slotNumber, GPlayerClass playerClass, GStance stance, out int barIndex)
        {
            barIndex = -1;

            if (slotNumber >= 13 && slotNumber <= 72)
            {
                barIndex = (slotNumber - 1) / 12;
                return true;
            }

            if (slotNumber >= 1 && slotNumber <= 12)
            {
                if (UsesBonusBar(stance))
                    return false;

                barIndex = 0;
                return true;
            }

            if (slotNumber >= 73 && slotNumber <= 84 &&
                (stance == GStance.Battle || stance == GStance.Stealth || stance == GStance.Shadow || stance == GStance.Cat))
            {
                barIndex = 0;
                return true;
            }

            if (slotNumber >= 85 && slotNumber <= 96 && stance == GStance.Defensive)
            {
                barIndex = 0;
                return true;
            }

            if (slotNumber >= 97 && slotNumber <= 108 && (stance == GStance.Berserker || stance == GStance.Bear))
            {
                barIndex = 0;
                return true;
            }

            return false;
        }

        public static bool UsesBonusBar(GStance stance)
        {
            return stance == GStance.Battle || stance == GStance.Defensive || stance == GStance.Berserker ||
                   stance == GStance.Stealth || stance == GStance.Shadow || stance == GStance.Bear || stance == GStance.Cat;
        }

        public static string GetSlotBandLabel(int slotNumber)
        {
            if (slotNumber >= 1 && slotNumber <= 12)
                return "main";
            if (slotNumber >= 13 && slotNumber <= 24)
                return "bar2";
            if (slotNumber >= 25 && slotNumber <= 36)
                return "bar3-right";
            if (slotNumber >= 37 && slotNumber <= 48)
                return "bar4-left";
            if (slotNumber >= 49 && slotNumber <= 60)
                return "bar5-bottom-right";
            if (slotNumber >= 61 && slotNumber <= 72)
                return "bar6-bottom-left";
            if (slotNumber >= 73 && slotNumber <= 120)
                return "bonus";
            if (slotNumber >= 121 && slotNumber <= 132)
                return "possess";
            return "invalid";
        }
    }
}
