// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GShortcut
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections.Generic;
using System.Globalization;

namespace Glider.Common.Objects
{
    public class GShortcut
    {
        public string Description;
        public GShortcutType ShortcutType;
        public int ShortcutValue;
        public int SlotNumber;

        public GShortcut(int SlotNumber)
        {
            this.SlotNumber = SlotNumber;
            var num = (uint)GProcessMemoryManipulator.smethod_11(
                GClass18.gclass18_0.method_4("ActionBarShortcuts") + 4 * (SlotNumber - 1), "shortcut1");
            if (num == 0U)
            {
                ShortcutType = GShortcutType.Empty;
                ShortcutValue = 0;
            }
            else if ((num & 2147483648U) > 0U)
            {
                ShortcutType = GShortcutType.Item;
                ShortcutValue = (int)num & 268435455;
            }
            else if ((num & 1073741824U) > 0U)
            {
                ShortcutType = GShortcutType.Macro;
                ShortcutValue = (int)num & 268435455;
            }
            else
            {
                ShortcutValue = (int)num;
                ShortcutType = GShortcutType.Spell;
            }
        }

        public static GShortcut[] GetAllShortcuts()
        {
            var gshortcutList = new List<GShortcut>();
            for (var SlotNumber = 1; SlotNumber <= 108; ++SlotNumber)
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

            GClass37.smethod_1("! Never found anything to match: \"" + PossibleIDs + "\"");
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
            foreach (var allShortcut in GetAllShortcuts())
                if (allShortcut.ShortcutType == TypeSought)
                    foreach (var possibleId in PossibleIDs)
                        if (possibleId == allShortcut.ShortcutValue)
                            return allShortcut;
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
    }
}