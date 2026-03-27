#nullable disable
using System;
using System.Collections.Generic;
using System.Globalization;

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
        public const uint ClickTypeMask = 0x01000000U;
        public const uint EquipmentSetTypeMask = 0x20000000U;
        public const int ActionIdMask = 0x0FFFFFFF;
        public const int MaxPlausibleSpellId = 0x0007FFFF;
        private static readonly object ActionBarBaseLock = new object();
        private static int CachedActionBarShortcutsOffset;
        private static int CachedResolvedActionBarShortcutsBase;
        private static int CachedResolveTick;

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

        private static bool IsPlausibleActionBarAddress(int address)
        {
            return address >= 65536 && (address & 3) == 0;
        }

        private static int ComputeActionBarCandidateScore(int nonZero, int plausible, int invalid)
        {
            var score = plausible * 4 - invalid * 3 + Math.Min(nonZero, 24);
            if (nonZero > 0 && plausible >= 2 && invalid * 2 < nonZero)
                score += 100;

            return score;
        }

        private static bool TryEvaluateActionBarCandidate(int actionBarShortcutsBase, string debugCluePrefix,
            out int nonZero,
            out int plausible,
            out int invalid,
            out string error)
        {
            nonZero = 0;
            plausible = 0;
            invalid = 0;
            error = "";

            if (!IsPlausibleActionBarAddress(actionBarShortcutsBase))
            {
                error = "candidate implausible: 0x" + unchecked((uint)actionBarShortcutsBase).ToString("x", CultureInfo.InvariantCulture);
                return false;
            }

            var maxProbeSlot = Math.Min(MaxActionSlot, MinSlot + 23);
            for (var slotNumber = MinSlot; slotNumber <= maxProbeSlot; ++slotNumber)
            {
                uint rawShortcut;
                try
                {
                    var shortcutAddress = GetSlotAddress(actionBarShortcutsBase, slotNumber);
                    rawShortcut = (uint)GameMemoryAccess.ReadInt32(shortcutAddress, debugCluePrefix + "-" + slotNumber.ToString(CultureInfo.InvariantCulture));
                }
                catch (Exception ex)
                {
                    error = "read failed at slot " + slotNumber + ": " + ex.Message;
                    return false;
                }

                if (rawShortcut == 0U)
                    continue;

                nonZero++;
                if (IsRawShortcutPlausible(rawShortcut))
                    plausible++;
                else
                    invalid++;
            }

            return true;
        }

        private static void AddOffsetCandidate(List<int> candidates, int candidate)
        {
            if (candidate == 0)
                return;

            if (!candidates.Contains(candidate))
                candidates.Add(candidate);
        }

        public static bool TryResolveActionBarShortcutsBase(out int actionBarShortcutsBase, out string details, bool forceRefresh)
        {
            actionBarShortcutsBase = 0;
            details = "";

            if (!MemoryOffsetTable.Instance.HasOffset("ActionBarShortcuts"))
            {
                details = "ActionBarShortcuts offset missing";
                return false;
            }

            var configuredOffset = MemoryOffsetTable.Instance.GetIntOffset("ActionBarShortcuts");
            if (!IsPlausibleActionBarAddress(configuredOffset))
            {
                details = "ActionBarShortcuts offset implausible: 0x" + unchecked((uint)configuredOffset).ToString("x", CultureInfo.InvariantCulture);
                return false;
            }

            var nowTick = Environment.TickCount;
            lock (ActionBarBaseLock)
            {
                if (!forceRefresh && CachedResolvedActionBarShortcutsBase != 0 && CachedActionBarShortcutsOffset == configuredOffset && unchecked(nowTick - CachedResolveTick) < 1000)
                {
                    actionBarShortcutsBase = CachedResolvedActionBarShortcutsBase;
                    details = "source=cached, base=0x" + unchecked((uint)actionBarShortcutsBase).ToString("x", CultureInfo.InvariantCulture);
                    return true;
                }
            }

            var wowBase = (int)GameMemoryAccess.GetWowBaseAddress();
            var offsetCandidates = new List<int>();
            AddOffsetCandidate(offsetCandidates, configuredOffset);
            if (configuredOffset > 0 && configuredOffset < 0x01000000 && IsPlausibleActionBarAddress(wowBase))
                AddOffsetCandidate(offsetCandidates, wowBase + configuredOffset);

            var bestBase = 0;
            var bestLabel = "none";
            var bestNonZero = 0;
            var bestPlausible = 0;
            var bestInvalid = 0;
            var bestScore = int.MinValue;

            var bestError = "";
            foreach (var offsetCandidate in offsetCandidates)
            {
                int directNonZero;
                int directPlausible;
                int directInvalid;
                string directError;
                if (TryEvaluateActionBarCandidate(offsetCandidate, "abshortcuts-direct", out directNonZero, out directPlausible, out directInvalid, out directError))
                {
                    var directScore = ComputeActionBarCandidateScore(directNonZero, directPlausible, directInvalid);
                    if (directScore > bestScore)
                    {
                        bestBase = offsetCandidate;
                        bestLabel = offsetCandidate == configuredOffset ? "direct" : "direct-module-adjusted";
                        bestNonZero = directNonZero;
                        bestPlausible = directPlausible;
                        bestInvalid = directInvalid;
                        bestScore = directScore;
                    }
                }

                try
                {
                    var indirectBase = GameMemoryAccess.ReadInt32(offsetCandidate, "abshortcuts-indirect");
                    int indirectNonZero;
                    int indirectPlausible;
                    int indirectInvalid;
                    string indirectError = "";
                    if (indirectBase != offsetCandidate &&
                        TryEvaluateActionBarCandidate(indirectBase, "abshortcuts-indirect-base", out indirectNonZero, out indirectPlausible, out indirectInvalid, out indirectError))
                    {
                        var indirectScore = ComputeActionBarCandidateScore(indirectNonZero, indirectPlausible, indirectInvalid);
                        if (indirectScore > bestScore)
                        {
                            bestBase = indirectBase;
                            bestLabel = offsetCandidate == configuredOffset ? "indirect" : "indirect-module-adjusted";
                            bestNonZero = indirectNonZero;
                            bestPlausible = indirectPlausible;
                            bestInvalid = indirectInvalid;
                            bestScore = indirectScore;
                        }
                    }
                    else if (!string.IsNullOrEmpty(indirectError))
                    {
                        bestError = indirectError;
                    }
                }
                catch (Exception ex)
                {
                    bestError = ex.Message;
                }

                if (!string.IsNullOrEmpty(directError))
                    bestError = directError;
            }

            if (!IsPlausibleActionBarAddress(bestBase) || bestNonZero == 0)
            {
                details = "unable to resolve ActionBarShortcuts base from 0x" + unchecked((uint)configuredOffset).ToString("x", CultureInfo.InvariantCulture) +
                          ", wowBase=0x" + unchecked((uint)wowBase).ToString("x", CultureInfo.InvariantCulture) +
                          ", resolveError=" + (bestError ?? "") +
                          ", bestLabel=" + bestLabel;
                return false;
            }

            lock (ActionBarBaseLock)
            {
                CachedActionBarShortcutsOffset = configuredOffset;
                CachedResolvedActionBarShortcutsBase = bestBase;
                CachedResolveTick = nowTick;
            }

            actionBarShortcutsBase = bestBase;
            details = "source=" + bestLabel +
                      ", offset=0x" + unchecked((uint)configuredOffset).ToString("x", CultureInfo.InvariantCulture) +
                      ", wowBase=0x" + unchecked((uint)wowBase).ToString("x", CultureInfo.InvariantCulture) +
                      ", base=0x" + unchecked((uint)bestBase).ToString("x", CultureInfo.InvariantCulture) +
                      ", nonZero=" + bestNonZero +
                      ", plausible=" + bestPlausible +
                      ", invalid=" + bestInvalid;
            return true;
        }

        public static bool TryResolveActionBarShortcutsBase(out int actionBarShortcutsBase, out string details)
        {
            return TryResolveActionBarShortcutsBase(out actionBarShortcutsBase, out details, false);
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
            var actionType = rawShortcut & 0xFF000000U;
            if (actionType == ClickTypeMask || actionType == EquipmentSetTypeMask)
            {
                shortcutType = GShortcutType.Empty;
                shortcutValue = 0;
                return true;
            }

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
