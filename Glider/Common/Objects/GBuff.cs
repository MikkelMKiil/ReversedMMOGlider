// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GBuff
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
namespace Glider.Common.Objects
{
    public class GBuff
    {
        private string _spellName;

        public GBuff(int SpellID, int ChargesLeft, bool IsHarmful)
        {
            this.SpellID = SpellID;
            this.ChargesLeft = ChargesLeft;
            this.IsHarmful = IsHarmful;
            var gclass64 = StartupClass.gclass63_0.method_7(SpellID);
            if (gclass64 != null)
            {
                _spellName = gclass64.string_0;
                BuffType = gclass64.gbuffType_0;
            }
            else
            {
                _spellName = "(unknown spell)";
                BuffType = GBuffType.Unknown;
            }
        }

        public bool IsHarmful { get; }

        public int SpellID { get; }

        public int ChargesLeft { get; set; }

        public string SpellName
        {
            get
            {
                if (_spellName == null || _spellName.StartsWith("("))
                    _spellName = GSpells.GetSpellName(SpellID);
                return _spellName;
            }
        }

        public GBuffType BuffType { get; }

        public override string ToString()
        {
            return "GBuff: SpellID=0x" + SpellID.ToString("x") + ",Charges=" + ChargesLeft + ",BuffType=" + BuffType +
                   ",SpellName=\"" + SpellName + "\", IsHarmful=" + IsHarmful;
        }
    }
}