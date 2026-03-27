#nullable disable
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Glider.Common.Objects
{
    public enum ShortcutMatchState
    {
        Missing,
        Present,
        Visible,
        Usable
    }

    public class ShortcutSnapshotEntry
    {
        public int SlotNumber;
        public string SlotBand;
        public GShortcutType ShortcutType;
        public int ShortcutValue;
        public bool IsPlayerActionSlot;
        public bool IsPresent;
        public bool IsOnVisiblePage;
        public bool IsVisible;
        public bool IsUsable;
        public int BarIndex;
        public GBarState BarState;
        public char CharCode;
        public string ButtonName;
        public string DisplayName;
        public string RankText;
        public int SpellRankValue;
    }

    public class ShortcutSnapshot
    {
        public string Source;
        public GPlayerClass PlayerClass;
        public GStance Stance;
        public int ActionBarPage;
        public int VisibleMainStartSlot;
        public string ActionBarCharacters;
        public SortedList<int, ShortcutSnapshotEntry> Entries = new SortedList<int, ShortcutSnapshotEntry>();

        public ShortcutSnapshotEntry GetEntry(int slotNumber)
        {
            if (!Entries.ContainsKey(slotNumber))
                return null;

            return Entries[slotNumber];
        }
    }

    public class ShortcutMatchResult
    {
        public ShortcutMatchState State = ShortcutMatchState.Missing;
        public ShortcutSnapshotEntry BestEntry;
        public ShortcutSnapshotEntry BestVisibleEntry;
        public bool IsAmbiguous;
        public int CandidateCount;
        public int VisibleCandidateCount;
        public int UsableCandidateCount;
        public string Details = "";
    }

    public static class ShortcutSnapshotService
    {
        public static bool IsVerboseShortcutDetectionEnabled()
        {
            return ConfigManager.gclass61_0 != null && ConfigManager.gclass61_0.method_5("VerboseShortcutDetection");
        }

        public static ShortcutSnapshot CaptureSnapshot(string source)
        {
            return CaptureSnapshot(source, true);
        }

        public static ShortcutSnapshot CaptureSnapshot(string source, bool includePossessSlots)
        {
            var snapshot = new ShortcutSnapshot();
            snapshot.Source = source;
            snapshot.ActionBarCharacters = StartupClass.ActionBarCharacters;

            var me = GPlayerSelf.Me;
            snapshot.PlayerClass = me != null ? me.PlayerClass : GPlayerClass.Unknown;
            snapshot.Stance = me != null ? me.Stance : GStance.Unknown;

            var actionBarCurrent = 0;
            if (MemoryOffsetTable.Instance.HasOffset("ActionBarCurrent"))
            {
                try
                {
                    actionBarCurrent = GameMemoryAccess.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("ActionBarCurrent"), "curbar");
                }
                catch
                {
                    actionBarCurrent = 0;
                }
            }

            snapshot.ActionBarPage = actionBarCurrent + 1;
            snapshot.VisibleMainStartSlot = actionBarCurrent == 0
                ? ShortcutLayout335a.GetPrimaryBarStartSlot(snapshot.PlayerClass, snapshot.Stance)
                : 1 + actionBarCurrent * 12;

            var maxSlot = includePossessSlots ? ShortcutLayout335a.MaxPossessSlot : ShortcutLayout335a.MaxActionSlot;
            for (var slotNumber = ShortcutLayout335a.MinSlot; slotNumber <= maxSlot; ++slotNumber)
            {
                var shortcut = new GShortcut(slotNumber);
                var entry = new ShortcutSnapshotEntry();
                entry.SlotNumber = slotNumber;
                entry.SlotBand = ShortcutLayout335a.GetSlotBandLabel(slotNumber);
                entry.ShortcutType = shortcut.ShortcutType;
                entry.ShortcutValue = shortcut.ShortcutValue;
                entry.IsPlayerActionSlot = ShortcutLayout335a.IsPlayerActionSlot(slotNumber);
                entry.IsPresent = shortcut.ShortcutType != GShortcutType.Empty && shortcut.ShortcutValue != 0;

                entry.ButtonName = ResolveButtonNameForSlot(slotNumber, snapshot.VisibleMainStartSlot);
                entry.IsOnVisiblePage = entry.ButtonName != null;
                entry.IsVisible = IsButtonVisible(entry.ButtonName);

                entry.BarIndex = -1;
                entry.BarState = GBarState.Indifferent;
                entry.CharCode = char.MinValue;
                int barIndex;
                if (ShortcutLayout335a.TryMapSlotToBarIndex(slotNumber, snapshot.PlayerClass, snapshot.Stance, out barIndex))
                {
                    entry.BarIndex = barIndex;
                    entry.BarState = (GBarState)(3 + barIndex);
                    var index = (slotNumber - 1) % 12;
                    if (index >= 0 && index < snapshot.ActionBarCharacters.Length)
                        entry.CharCode = snapshot.ActionBarCharacters[index];
                }

                entry.IsUsable = entry.IsPresent && entry.BarIndex >= 0 && entry.CharCode != char.MinValue;
                ResolveDisplayData(entry);
                snapshot.Entries.Add(slotNumber, entry);
            }

            return snapshot;
        }

        private static string ResolveButtonNameForSlot(int slotNumber, int visibleMainStartSlot)
        {
            if (slotNumber >= visibleMainStartSlot && slotNumber < visibleMainStartSlot + 12)
            {
                var prefix = visibleMainStartSlot >= 73 ? "BonusAction" : "Action";
                return prefix + "Button" + (slotNumber - visibleMainStartSlot + 1);
            }

            if (slotNumber >= 61 && slotNumber <= 72)
                return "MultiBarBottomLeftButton" + (slotNumber - 60);

            if (slotNumber >= 49 && slotNumber <= 60)
                return "MultiBarBottomRightButton" + (slotNumber - 48);

            if (slotNumber >= 25 && slotNumber <= 36)
                return "MultiBarRightButton" + (slotNumber - 24);

            if (slotNumber >= 37 && slotNumber <= 48)
                return "MultiBarLeftButton" + (slotNumber - 36);

            return null;
        }

        private static bool IsButtonVisible(string buttonName)
        {
            if (string.IsNullOrEmpty(buttonName))
                return false;

            try
            {
                var uiElement = UIElement.smethod_2(buttonName, false);
                return uiElement != null && uiElement.method_10();
            }
            catch
            {
                return false;
            }
        }

        private static void ResolveDisplayData(ShortcutSnapshotEntry entry)
        {
            entry.DisplayName = "";
            entry.RankText = "";
            entry.SpellRankValue = 0;

            if (!entry.IsPresent)
                return;

            if (entry.ShortcutType == GShortcutType.Spell)
            {
                var manager = StartupClass.SpellbookStateManager;
                if (manager != null)
                {
                    try
                    {
                        manager.method_9(entry.ShortcutValue);
                        if (manager.Offsets.ContainsKey(entry.ShortcutValue))
                        {
                            var spellEntry = manager.Offsets[entry.ShortcutValue];
                            if (spellEntry != null)
                            {
                                entry.DisplayName = spellEntry.string_0 ?? "(no name)";
                                entry.SpellRankValue = spellEntry.int_2;
                                if (entry.SpellRankValue > 0)
                                    entry.RankText = "Rank " + entry.SpellRankValue;
                            }
                        }

                        if (entry.DisplayName == "")
                            entry.DisplayName = manager.method_11(entry.ShortcutValue);
                    }
                    catch (Exception ex)
                    {
                        entry.DisplayName = "(spell lookup failed: " + ex.Message + ")";
                    }
                }
                else
                {
                    entry.DisplayName = "(spellbook unavailable)";
                }

                return;
            }

            if (entry.ShortcutType == GShortcutType.Item)
            {
                try
                {
                    var itemDefinition = new GItemDefinition(entry.ShortcutValue);
                    entry.DisplayName = itemDefinition != null && !string.IsNullOrEmpty(itemDefinition.Name)
                        ? itemDefinition.Name
                        : "(unknown item)";
                }
                catch (Exception ex)
                {
                    entry.DisplayName = "(item lookup failed: " + ex.Message + ")";
                }

                return;
            }

            if (entry.ShortcutType == GShortcutType.Macro)
            {
                entry.DisplayName = "Macro #" + entry.ShortcutValue;
                return;
            }

            entry.DisplayName = entry.ShortcutType.ToString();
        }

        private static bool IsCandidateForKey(GKey key, ShortcutSnapshotEntry entry, int[] spellFamily)
        {
            if (!entry.IsPresent || !entry.IsPlayerActionSlot)
                return false;

            if (key.KType == GKeyType.Char)
            {
                if (entry.CharCode != key.CharCode)
                    return false;

                if (key.BarState != GBarState.Indifferent && entry.BarState != key.BarState)
                    return false;

                return true;
            }

            if (key.KType == GKeyType.Macro)
                return entry.ShortcutType == GShortcutType.Macro && entry.ShortcutValue == key.SIM;

            if (key.KType == GKeyType.ItemDefID)
                return entry.ShortcutType == GShortcutType.Item && entry.ShortcutValue == key.SIM;

            if (key.KType != GKeyType.SpellID || entry.ShortcutType != GShortcutType.Spell)
                return false;

            if (spellFamily == null || spellFamily.Length == 0)
                return entry.ShortcutValue == key.SIM;

            for (var index = 0; index < spellFamily.Length; ++index)
                if (spellFamily[index] == entry.ShortcutValue)
                    return true;

            return false;
        }

        private static int GetCandidateScore(GKey key, ShortcutSnapshotEntry entry)
        {
            var score = 0;

            if (entry.IsUsable)
                score += 100000;
            if (entry.IsVisible)
                score += 50000;
            if (entry.IsOnVisiblePage)
                score += 20000;
            if (key.BarState != GBarState.Indifferent && entry.BarState == key.BarState)
                score += 10000;
            if (key.KType == GKeyType.SpellID)
                score += entry.SpellRankValue * 100;

            // Keep lower slots deterministic as a final stable preference.
            score += ShortcutLayout335a.MaxActionSlot - entry.SlotNumber;
            return score;
        }

        public static ShortcutMatchResult MatchKey(GKey key, ShortcutSnapshot snapshot)
        {
            var result = new ShortcutMatchResult();
            if (key == null)
            {
                result.Details = "key is null";
                return result;
            }

            var useSnapshot = snapshot ?? CaptureSnapshot("ShortcutSnapshotService.MatchKey", false);
            int[] spellFamily = null;
            if (key.KType == GKeyType.SpellID && StartupClass.SpellbookStateManager != null && key.SIM != 0)
            {
                try
                {
                    spellFamily = StartupClass.SpellbookStateManager.method_13(key.SIM);
                }
                catch
                {
                    spellFamily = null;
                }
            }

            var candidates = new List<ShortcutSnapshotEntry>();
            foreach (var entry in useSnapshot.Entries.Values)
            {
                if (!IsCandidateForKey(key, entry, spellFamily))
                    continue;

                candidates.Add(entry);
                if (entry.IsVisible)
                    result.VisibleCandidateCount++;
                if (entry.IsUsable)
                    result.UsableCandidateCount++;
            }

            result.CandidateCount = candidates.Count;
            if (result.CandidateCount == 0)
            {
                result.State = ShortcutMatchState.Missing;
                result.Details = "no matching slots";
                return result;
            }

            if (result.UsableCandidateCount > 0)
                result.State = ShortcutMatchState.Usable;
            else if (result.VisibleCandidateCount > 0)
                result.State = ShortcutMatchState.Visible;
            else
                result.State = ShortcutMatchState.Present;

            var bestScore = int.MinValue;
            var secondBestScore = int.MinValue;
            var bestVisibleScore = int.MinValue;

            foreach (var entry in candidates)
            {
                var score = GetCandidateScore(key, entry);
                if (score > bestScore || score == bestScore && result.BestEntry != null && entry.SlotNumber < result.BestEntry.SlotNumber)
                {
                    secondBestScore = bestScore;
                    bestScore = score;
                    result.BestEntry = entry;
                }
                else if (score > secondBestScore)
                {
                    secondBestScore = score;
                }

                if (!entry.IsVisible)
                    continue;

                if (score > bestVisibleScore || score == bestVisibleScore && result.BestVisibleEntry != null && entry.SlotNumber < result.BestVisibleEntry.SlotNumber)
                {
                    bestVisibleScore = score;
                    result.BestVisibleEntry = entry;
                }
            }

            if (result.CandidateCount > 1 && secondBestScore == bestScore)
                result.IsAmbiguous = true;

            result.Details = "state=" + result.State +
                             ", candidates=" + result.CandidateCount +
                             ", visible=" + result.VisibleCandidateCount +
                             ", usable=" + result.UsableCandidateCount +
                             ", ambiguous=" + result.IsAmbiguous;
            return result;
        }

        public static bool ApplyBestMatchToKey(GKey key, ShortcutMatchResult matchResult, bool failQuietly)
        {
            if (key == null || matchResult == null || matchResult.BestEntry == null)
            {
                if (!failQuietly && key != null)
                    Logger.smethod_1("! Could not find this spell on any action bar: " + key);
                return false;
            }

            var bestEntry = matchResult.BestEntry;
            key.BarState = bestEntry.BarState;
            key.CharCode = bestEntry.CharCode;

            if (key.KType == GKeyType.SpellID)
            {
                key.LiveSIM = bestEntry.ShortcutValue;
                return true;
            }

            if (key.KType == GKeyType.ItemDefID)
            {
                try
                {
                    key.LiveSIM = new GItemDefinition(key.SIM).SpellID;
                    if (StartupClass.SpellbookStateManager != null && key.LiveSIM != 0)
                        StartupClass.SpellbookStateManager.method_9(key.LiveSIM);
                }
                catch
                {
                    key.LiveSIM = bestEntry.ShortcutValue;
                }

                return true;
            }

            if (key.KType == GKeyType.Macro)
            {
                key.LiveSIM = bestEntry.ShortcutValue;
                return true;
            }

            if (key.KType == GKeyType.Char)
            {
                key.LiveSIM = bestEntry.ShortcutValue;
                return true;
            }

            return false;
        }

        public static string GetEntryStateLabel(ShortcutSnapshotEntry entry)
        {
            if (entry == null || !entry.IsPresent)
                return "missing";

            if (entry.IsUsable)
                return "usable";

            if (entry.IsVisible)
                return "visible";

            return "present";
        }

        public static string FormatEntryForLog(ShortcutSnapshotEntry entry)
        {
            var line = "slot=" + entry.SlotNumber +
                       ", band=" + entry.SlotBand +
                       ", type=" + entry.ShortcutType +
                       ", present=" + entry.IsPresent.ToString().ToLowerInvariant() +
                       ", visible=" + entry.IsVisible.ToString().ToLowerInvariant() +
                       ", usable=" + entry.IsUsable.ToString().ToLowerInvariant();

            if (!entry.IsPresent)
                return line;

            line += ", id=0x" + entry.ShortcutValue.ToString("x", CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(entry.DisplayName))
                line += ", name=\"" + entry.DisplayName + "\"";
            if (!string.IsNullOrEmpty(entry.RankText))
                line += ", rank=\"" + entry.RankText + "\"";

            line += ", button=" + (entry.ButtonName ?? "(none)");
            line += ", bar=" + (entry.IsUsable ? entry.BarState.ToString() : "(none)");
            line += ", char=" + (entry.CharCode == char.MinValue ? "(none)" : entry.CharCode.ToString());
            return line;
        }

        public static void LogStartupShortcutHealth(string source, SortedList<string, GKey> keyMap)
        {
            if (keyMap == null)
                return;

            var snapshot = CaptureSnapshot(source, false);
            var total = 0;
            var missing = 0;
            var present = 0;
            var visible = 0;
            var usable = 0;
            var ambiguous = 0;

            Logger.LogMessage("[VerboseShortcutDetection] startup-health source=" + source +
                              ", class=" + snapshot.PlayerClass +
                              ", stance=" + snapshot.Stance +
                              ", page=" + snapshot.ActionBarPage +
                              ", chars=\"" + snapshot.ActionBarCharacters + "\"");

            foreach (var keyName in keyMap.Keys)
            {
                var key = keyMap[keyName];
                if (key == null)
                    continue;

                if (key.KType != GKeyType.SpellID && key.KType != GKeyType.ItemDefID && key.KType != GKeyType.Macro)
                    continue;

                total++;
                var match = MatchKey(key, snapshot);
                if (match.State == ShortcutMatchState.Missing)
                    missing++;
                else if (match.State == ShortcutMatchState.Present)
                    present++;
                else if (match.State == ShortcutMatchState.Visible)
                    visible++;
                else if (match.State == ShortcutMatchState.Usable)
                    usable++;

                if (match.IsAmbiguous)
                    ambiguous++;

                if (match.State == ShortcutMatchState.Usable && !match.IsAmbiguous)
                    continue;

                var best = match.BestEntry;
                var bestSlot = best != null ? best.SlotNumber.ToString(CultureInfo.InvariantCulture) : "n/a";
                var bestType = best != null ? best.ShortcutType.ToString() : "n/a";
                var bestName = best != null ? best.DisplayName : "n/a";
                var bestRank = best != null ? best.RankText : "";

                Logger.LogMessage("[VerboseShortcutDetection] startup-health key=" + keyName +
                                  ", ktype=" + key.KType +
                                  ", sim=0x" + key.SIM.ToString("x", CultureInfo.InvariantCulture) +
                                  ", state=" + match.State +
                                  ", candidates=" + match.CandidateCount +
                                  ", visible=" + match.VisibleCandidateCount +
                                  ", usable=" + match.UsableCandidateCount +
                                  ", ambiguous=" + match.IsAmbiguous +
                                  ", bestSlot=" + bestSlot +
                                  ", bestType=" + bestType +
                                  ", bestName=\"" + bestName + "\"" +
                                  ", bestRank=\"" + bestRank + "\"");
            }

            Logger.LogMessage("[VerboseShortcutDetection] startup-health summary total=" + total +
                              ", usable=" + usable +
                              ", visible=" + visible +
                              ", present=" + present +
                              ", missing=" + missing +
                              ", ambiguous=" + ambiguous);
        }
    }
}
