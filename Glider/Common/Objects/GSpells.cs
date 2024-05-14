// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GSpells
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections.Generic;

namespace Glider.Common.Objects
{
    public class GSpells
    {
        private static readonly SortedList<int, string> SpellNameCache = new SortedList<int, string>();
        private static readonly SortedList<int, GBuffType> BuffTypeCache = new SortedList<int, GBuffType>();

        public static string GetSpellName(int SpellID)
        {
            if (SpellNameCache.ContainsKey(SpellID))
                return SpellNameCache[SpellID];
            StartupClass.gclass63_0.method_9(SpellID);
            SpellNameCache.Add(SpellID, StartupClass.gclass63_0.sortedList_0[SpellID].string_0);
            BuffTypeCache.Add(SpellID, StartupClass.gclass63_0.sortedList_0[SpellID].gbuffType_0);
            return StartupClass.gclass63_0.sortedList_0[SpellID].string_0;
        }

        public static string GetSpellNameAndBuffType(int SpellID, out GBuffType BuffType)
        {
            BuffType = GBuffType.Unknown;
            if (SpellNameCache.ContainsKey(SpellID))
            {
                BuffType = BuffTypeCache[SpellID];
                return SpellNameCache[SpellID];
            }

            StartupClass.gclass63_0.method_9(SpellID);
            SpellNameCache.Add(SpellID, StartupClass.gclass63_0.sortedList_0[SpellID].string_0);
            BuffTypeCache.Add(SpellID, StartupClass.gclass63_0.sortedList_0[SpellID].gbuffType_0);
            return StartupClass.gclass63_0.sortedList_0[SpellID].string_0;
        }

        public static int[] CrossReferenceSpell(int SpellID)
        {
            return StartupClass.gclass63_0.method_13(SpellID);
        }
    }
}