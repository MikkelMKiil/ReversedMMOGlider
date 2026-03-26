namespace Glider.Common.Objects
{
    internal static class GameMemoryConstants
    {
        internal static class Wotlk
        {
            internal const uint ClientConnection = 0x00C79CE0;
            internal const uint CurMgrOffset = 0x2ED0;
            internal const uint FirstObject = 0xAC;
            internal const uint LocalGuid = 0xC0;
            internal const uint PlayerGuid = LocalGuid;
            internal const uint PlayerIdAddr = 0x00CA1238;
            internal const uint TargetGuid = 0x00BD07B0;

            internal const uint ObjStoragePointer = 0x8;
            internal const uint ObjType = 0x14;
            internal const uint ObjGuid = 0x30;
            internal const uint NextObject = 0x3C;

            internal const uint PosX = 0x9B8;
            internal const uint PosY = 0x9BC;
            internal const uint PosZ = 0x9C0;

            internal const uint DescriptorBase = 0x8;
            internal const uint UnitFieldHealth = 0x6C;
            internal const uint UnitFieldMaxHealth = 0x74;
            internal const uint UnitFieldPower1 = 0x70;
            internal const uint UnitFieldMaxPower1 = 0x78;
            internal const uint UnitFieldPower2 = 0x74;
            internal const uint UnitFieldPower4 = 0x7C;
            internal const uint UnitFieldFactionTemplate = 0x90;

            internal const uint PlayerNameStore = 0x00C79D18;
            internal const uint MapId = 0x00AB63BC;
        }

        internal static class Spellbook
        {
            internal const uint MySpells = 0x00B700F0;
            internal const uint SpellListBase = 0x24;
            internal const uint SpellListRowCount = 0x28;
            internal const uint SpellListSub = 0x0;

            internal const uint SpellNameRLE = 0x0;
            internal const uint BuffTypeRLE = 0x0;
            internal const uint SpellGroupRLE = 0x0;
            internal const uint SpellRankRLE = 0x0;

            internal const uint CooldownStart = 0x29C;
            internal const uint CooldownStep = 0x10;

            internal const uint CD_TicksAtCast = 0x4;
            internal const uint CD_DurationGCD = 0x0;
            internal const uint CD_SpellID = 0x8;
            internal const uint CD_GroupID = 0xC;
            internal const uint CD_ActiveDuration = 0x0;
            internal const uint CD_DurationSpell = 0x0;
            internal const uint CD_DurationGroup = 0x0;
        }

        internal static class ClassIds
        {
            internal static class Deathknight
            {
                internal const int BoneShieldBuff = 0xC046;
                internal const int DeathTranceBuff = 0xC522;
                internal const int FrostFeverDebuff = 0xD737;
                internal const int BloodPlagueDebuff = 0xD726;
                internal const int CorpseDustItemId = 0x9151;
            }

            internal static class Mage
            {
                internal const int CombustionBuff = 0x700A;
            }

            internal static class Warlock
            {
                internal const int SoulShardItemId = 0x1879;
            }

            internal static class Hunter
            {
                internal const int MinimumAmmo = 30;
                internal const int ViperBuff = 34074;
            }
        }
    }
}