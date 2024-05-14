// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GTendency
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections.Generic;

namespace Glider.Common.Objects
{
    public class GTendency
    {
        private readonly SortedList<string, int> TData;

        public GTendency()
        {
            TData = new SortedList<string, int>();
        }

        public int GetCount(string MonsterName)
        {
            return TData.ContainsKey(MonsterName) ? TData[MonsterName] : 0;
        }

        public void Increment(string MonsterName)
        {
            if (TData.ContainsKey(MonsterName))
                TData[MonsterName] = TData[MonsterName] + 1;
            else
                TData[MonsterName] = 1;
        }

        public void Decrement(string MonsterName)
        {
            if (TData.ContainsKey(MonsterName))
                TData[MonsterName] = TData[MonsterName] - 1;
            else
                TData[MonsterName] = -1;
        }

        public void Reset()
        {
            TData.Clear();
        }
    }
}