// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.PartyBuff
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
namespace Glider.Common.Objects
{
    internal class PartyBuff
    {
        public string Name;
        public GSpellTimer Timer;

        public PartyBuff(string Name, int DurationMS)
        {
            Timer = new GSpellTimer(DurationMS, true);
            this.Name = Name;
        }
    }
}