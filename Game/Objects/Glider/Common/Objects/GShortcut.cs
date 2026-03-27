// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GShortcut
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Glider.Common.Objects
{
    public class GShortcut
    {
        private static readonly object SnapshotLock = new object();
        private static readonly SortedList<int, string> LastSnapshotBySlot = new SortedList<int, string>();
        private static readonly object DecodeDiagnosticLock = new object();
        private static readonly SortedList<string, bool> LoggedDecodeFailures = new SortedList<string, bool>();

        public string Description;
        public GShortcutType ShortcutType;
        public int ShortcutValue;
        public int SlotNumber;

        public GShortcut(int SlotNumber)
        {
            this.SlotNumber = SlotNumber;

            if (!ShortcutLayout335a.IsReadableSlot(SlotNumber))
            {
                ShortcutType = GShortcutType.Empty;
                ShortcutValue = 0;
                return;
            }

            if (!MemoryOffsetTable.Instance.HasOffset("ActionBarShortcuts"))
            {
                ShortcutType = GShortcutType.Empty;
                ShortcutValue = 0;
                return;
            }

            var actionBarShortcutsBase = MemoryOffsetTable.Instance.GetIntOffset("ActionBarShortcuts");
            if (actionBarShortcutsBase < 65536)
            {
                ShortcutType = GShortcutType.Empty;
                ShortcutValue = 0;
                return;
            }

            uint num;
            try
            {
                var shortcutAddress = ShortcutLayout335a.GetSlotAddress(actionBarShortcutsBase, SlotNumber);
                num = (uint)GameMemoryAccess.ReadInt32(shortcutAddress, "shortcut1");
            }
            catch (Exception ex)
            {
                Logger.smethod_1("GShortcut read failed for slot " + SlotNumber + ": " + ex.Message);
                ShortcutType = GShortcutType.Empty;
                ShortcutValue = 0;
                return;
            }

            if (!ShortcutLayout335a.TryDecodeShortcut(num, out ShortcutType, out ShortcutValue))
            {
                LogDecodeFailure(SlotNumber, num);
                ShortcutType = GShortcutType.Empty;
                ShortcutValue = 0;
            }
        }

        private static void LogDecodeFailure(int slotNumber, uint rawShortcut)
        {
            if (!IsVerboseShortcutDetectionEnabled())
                return;

            var key = slotNumber.ToString(CultureInfo.InvariantCulture) + ":" + rawShortcut.ToString("x", CultureInfo.InvariantCulture);
            lock (DecodeDiagnosticLock)
            {
                if (LoggedDecodeFailures.ContainsKey(key))
                    return;

                if (LoggedDecodeFailures.Count > 200)
                    LoggedDecodeFailures.Clear();

                LoggedDecodeFailures.Add(key, true);
            }

            Logger.LogMessage("[VerboseShortcutDetection] decode-fail slot=" + slotNumber +
                              ", raw=0x" + rawShortcut.ToString("x", CultureInfo.InvariantCulture));
        }

        private static string FormatPossibleIds(int[] possibleIds)
        {
            if (possibleIds == null || possibleIds.Length == 0)
                return "";

            var parts = new string[possibleIds.Length];
            for (var index = 0; index < possibleIds.Length; ++index)
                parts[index] = "0x" + possibleIds[index].ToString("x", CultureInfo.InvariantCulture);

            return string.Join(" ", parts);
        }

        public static GShortcut[] GetAllShortcuts()
        {
            var gshortcutList = new List<GShortcut>();
            for (var SlotNumber = ShortcutLayout335a.MinSlot; SlotNumber <= ShortcutLayout335a.MaxActionSlot; ++SlotNumber)
            {
                var gshortcut = new GShortcut(SlotNumber);
                if (gshortcut.ShortcutValue != 0)
                    gshortcutList.Add(gshortcut);
            }

            return gshortcutList.ToArray();
        }

        public static GShortcut FindMatchingSpellGroup(string PossibleIDs)
        {
            var strArray = PossibleIDs.Split(' ');
            var numArray = new int[strArray.Length];
            for (var index = 0; index < strArray.Length; ++index)
                numArray[index] = !strArray[index].StartsWith("0x")
                    ? int.Parse(strArray[index])
                    : int.Parse(strArray[index].Substring(2), NumberStyles.HexNumber);
            foreach (var SpellID in numArray)
            {
                var matchingShortcut = FindMatchingShortcut(GShortcutType.Spell, GSpells.CrossReferenceSpell(SpellID));
                if (matchingShortcut != null)
                    return matchingShortcut;
            }

            Logger.smethod_1("! Never found anything to match: \"" + PossibleIDs + "\"");
            return null;
        }

        public static GShortcut FindMatchingShortcut(GShortcutType TypeSought, string PossibleIDs)
        {
            var strArray = PossibleIDs.Split(' ');
            var PossibleIDs1 = new int[strArray.Length];
            for (var index = 0; index < strArray.Length; ++index)
                PossibleIDs1[index] = !strArray[index].StartsWith("0x")
                    ? int.Parse(strArray[index])
                    : int.Parse(strArray[index].Substring(2), NumberStyles.HexNumber);
            return FindMatchingShortcut(TypeSought, PossibleIDs1);
        }

        public static GShortcut FindMatchingShortcut(GShortcutType TypeSought, int[] PossibleIDs)
        {
            var scanned = 0;
            var scannedType = 0;
            var sample = new List<string>();
            foreach (var allShortcut in GetAllShortcuts())
            {
                scanned++;
                if (allShortcut.ShortcutType == TypeSought)
                {
                    scannedType++;
                    if (sample.Count < 8)
                    {
                        sample.Add("slot=" + allShortcut.SlotNumber +
                                   ":0x" + allShortcut.ShortcutValue.ToString("x", CultureInfo.InvariantCulture));
                    }

                    foreach (var possibleId in PossibleIDs)
                        if (possibleId == allShortcut.ShortcutValue)
                            return allShortcut;
                }
            }

            if (IsVerboseShortcutDetectionEnabled())
            {
                var sampleText = sample.Count > 0 ? string.Join(" ", sample.ToArray()) : "(none)";
                Logger.LogMessage("[VerboseShortcutDetection] match-fail type=" + TypeSought +
                                  ", ids=\"" + FormatPossibleIds(PossibleIDs) + "\"" +
                                  ", scanned=" + scanned +
                                  ", scannedType=" + scannedType +
                                  ", sample=\"" + sampleText + "\"");
            }

            return null;
        }

        public static GShortcut FindMatchingShortcut(GShortcutType TypeSought, int ExactSpellID)
        {
            foreach (var allShortcut in GetAllShortcuts())
                if (allShortcut.ShortcutType == TypeSought && ExactSpellID == allShortcut.ShortcutValue)
                    return allShortcut;
            return null;
        }

        public override string ToString()
        {
            return "(GShortcut, Slot=" + SlotNumber + ", Type=" + ShortcutType + ", Value=0x" +
                   ShortcutValue.ToString("x") + ")";
        }

        private static bool IsVerboseShortcutDetectionEnabled()
        {
            return ShortcutSnapshotService.IsVerboseShortcutDetectionEnabled();
        }

        public static void LogShortcutDetectionSnapshot(string source, bool onlyChanges)
        {
            if (!IsVerboseShortcutDetectionEnabled())
                return;

            try
            {
                var snapshotData = ShortcutSnapshotService.CaptureSnapshot(source, true);
                Logger.LogMessage("[VerboseShortcutDetection] source=" + source +
                                  ", class=" + snapshotData.PlayerClass +
                                  ", stance=" + snapshotData.Stance +
                                  ", page=" + snapshotData.ActionBarPage +
                                  ", chars=\"" + snapshotData.ActionBarCharacters + "\"" +
                                  ", uiReady=" + snapshotData.IsWorldUiReady.ToString().ToLowerInvariant() +
                                  ", classPlausible=" + snapshotData.IsPlayerClassPlausible.ToString().ToLowerInvariant() +
                                  ", actionBarProbe=" + snapshotData.IsActionBarDataPlausible.ToString().ToLowerInvariant() +
                                  ", actionBarProbeDetails=\"" + (snapshotData.ActionBarProbeDetails ?? "") + "\"");

                var snapshot = new SortedList<int, string>();
                foreach (var slotNumber in snapshotData.Entries.Keys)
                {
                    snapshot.Add(slotNumber, ShortcutSnapshotService.FormatEntryForLog(snapshotData.Entries[slotNumber]));
                }

                var loggedCount = 0;
                lock (SnapshotLock)
                {
                    foreach (var slot in snapshot.Keys)
                    {
                        var state = snapshot[slot];
                        var changed = !LastSnapshotBySlot.ContainsKey(slot) || LastSnapshotBySlot[slot] != state;
                        if (!onlyChanges || changed)
                        {
                            Logger.LogMessage("[VerboseShortcutDetection] " + state);
                            loggedCount++;
                        }
                    }

                    LastSnapshotBySlot.Clear();
                    foreach (var slot in snapshot.Keys)
                        LastSnapshotBySlot.Add(slot, snapshot[slot]);
                }

                if (onlyChanges && loggedCount == 0)
                    Logger.LogMessage("[VerboseShortcutDetection] source=" + source + ", no slot changes detected");
            }
            catch (Exception ex)
            {
                Logger.LogMessage("[VerboseShortcutDetection] snapshot failed: " + ex.Message);
            }
        }
    }
}
