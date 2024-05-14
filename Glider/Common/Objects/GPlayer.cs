// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GPlayer
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections.Generic;

namespace Glider.Common.Objects
{
    public class GPlayer : GUnit
    {
        protected int _energy;
        protected int _energyMax;
        protected long _petGuid;
        protected GPlayerClass _playerClass;
        protected GPlayerRace _playerRace;
        protected int _rage;
        protected int _runicPower;
        protected bool _sitting;
        private int[] _stealthAuras;

        public GPlayer(int BaseAddress, int FrameNumber)
            : base(BaseAddress, FrameNumber)
        {
            SetType(GObjectType.Player);
        }

        public int[] Quests
        {
            get
            {
                var intList = new List<int>();
                for (var index = 1; index <= 25; ++index)
                {
                    var storageInt = GetStorageInt("PLAYER_QUEST_LOG_" + index + "_1");
                    if (storageInt != 0)
                        intList.Add(storageInt);
                }

                return intList.ToArray();
            }
        }

        public bool IsPVP
        {
            get
            {
                Refresh();
                return (_flags & 4096) > 0;
            }
        }

        public GPlayerClass PlayerClass
        {
            get
            {
                Refresh();
                return _playerClass;
            }
        }

        public GPlayerRace PlayerRace
        {
            get
            {
                Refresh();
                return _playerRace;
            }
        }

        public GPlayerFaction PlayerFaction
        {
            get
            {
                switch (PlayerRace)
                {
                    case GPlayerRace.Human:
                        return GPlayerFaction.Alliance;
                    case GPlayerRace.Orc:
                        return GPlayerFaction.Horde;
                    case GPlayerRace.Dwarf:
                        return GPlayerFaction.Alliance;
                    case GPlayerRace.NightElf:
                        return GPlayerFaction.Alliance;
                    case GPlayerRace.Undead:
                        return GPlayerFaction.Horde;
                    case GPlayerRace.Tauren:
                        return GPlayerFaction.Horde;
                    case GPlayerRace.Gnome:
                        return GPlayerFaction.Alliance;
                    case GPlayerRace.Troll:
                        return GPlayerFaction.Horde;
                    case GPlayerRace.BloodElf:
                        return GPlayerFaction.Horde;
                    case GPlayerRace.Draenei:
                        return GPlayerFaction.Alliance;
                    default:
                        return GPlayerFaction.Unknown;
                }
            }
        }

        public bool HasLivePet => Pet != null && !Pet.IsDead;

        public long PetGUID
        {
            get
            {
                Refresh();
                return _petGuid;
            }
        }

        public GUnit Pet => PetGUID == 0L ? null : GObjectList.FindUnit(PetGUID);

        public bool IsSameFaction => PlayerFaction == GContext.Main.Me.PlayerFaction;

        public int Energy
        {
            get
            {
                Refresh();
                return _energy;
            }
        }

        public int EnergyMax
        {
            get
            {
                Refresh();
                return _energyMax;
            }
        }

        public int Rage
        {
            get
            {
                Refresh();
                return _rage;
            }
        }

        public int RunicPower
        {
            get
            {
                Refresh();
                return _runicPower;
            }
        }

        public bool IsSitting
        {
            get
            {
                Refresh();
                return _sitting;
            }
        }

        public override GCreatureType CreatureType => GCreatureType.Humanoid;

        public GStance Stance
        {
            get
            {
                var buffSnapshot = GetBuffSnapshot();
                if (HasAura(buffSnapshot, 2457))
                    return GStance.Battle;
                if (HasAura(buffSnapshot, 71))
                    return GStance.Defensive;
                if (HasAura(buffSnapshot, 2458))
                    return GStance.Berserker;
                if (HasAura(buffSnapshot, 15473))
                    return GStance.Shadow;
                if (HasAura(buffSnapshot, 9634) || HasAura(buffSnapshot, 5487))
                    return GStance.Bear;
                if (HasAura(buffSnapshot, 768))
                    return GStance.Cat;
                if (_stealthAuras == null)
                    _stealthAuras = StartupClass.gclass63_0.method_13(1784);
                return HasAura(buffSnapshot, _stealthAuras) ? GStance.Stealth : GStance.None;
            }
        }

        protected override void LoadFields()
        {
            base.LoadFields();
            _playerClass =
                (GPlayerClass)(GProcessMemoryManipulator.smethod_21(
                    GProcessMemoryManipulator.smethod_21(BaseAddress + GClass18.gclass18_0.method_4("ClassPtrOffset"), "ClassPtr") +
                    GClass18.gclass18_0.method_4("ClassIdOffset"), "ClassId") & byte.MaxValue);
            var num = GProcessMemoryManipulator.smethod_21(BaseAddress + GClass18.gclass18_0.method_4("ClassPtrOffset"), "RacePtr");
            if (num != 0)
                _playerRace =
                    (GPlayerRace)(GProcessMemoryManipulator.smethod_21(num + GClass18.gclass18_0.method_4("RaceIdOffset"), "Race") &
                                  byte.MaxValue);
            _petGuid = GetStorageLong("UNIT_FIELD_SUMMON");
            if (_petGuid == 0L)
                _petGuid = GetStorageLong("UNIT_FIELD_CHARM");
            _energy = GetStorageInt("UNIT_FIELD_POWER4");
            _energyMax = GetStorageInt("UNIT_FIELD_MAXPOWER4");
            _rage = GetStorageInt("UNIT_FIELD_POWER2") / 10;
            _runicPower = GetStorageInt("UNIT_FIELD_POWER7") / 10;
            _sitting = (GetStorageInt("UNIT_FIELD_BYTES_1") & byte.MaxValue) != 0;
        }

        protected override void SetName()
        {
            var num1 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("PlayerNames") + 12, "PlayerNamesBase");
            while ((num1 & 1) == 0 && num1 != 0 && num1 != 28)
            {
                var num2 = GProcessMemoryManipulator.smethod_12(num1 + 24, "PlayerNamesGUID");
                var str = GProcessMemoryManipulator.smethod_9(num1 + 32, 32, "PlayerName");
                if (num2 != GUID)
                {
                    var num3 = num1;
                    num1 = GProcessMemoryManipulator.smethod_11(num1 + 16, "PlayerNext");
                    if (num1 == num3)
                        break;
                }
                else
                {
                    _name = str.Length > 0 ? str : "(unknown)";
                    return;
                }
            }

            _name = "(unknown)";
        }

        public bool IsOnQuest(int QuestID)
        {
            foreach (var quest in Quests)
                if (quest == QuestID)
                    return true;
            return false;
        }

        protected bool HasAura(GBuff[] Buffs, int SpellID)
        {
            foreach (var buff in Buffs)
                if (buff.SpellID == SpellID)
                    return true;
            return false;
        }

        protected bool HasAura(GBuff[] Buffs, int[] SpellIDs)
        {
            foreach (var buff in Buffs)
            foreach (var spellId in SpellIDs)
                if (buff.SpellID == spellId)
                    return true;
            return false;
        }
    }
}