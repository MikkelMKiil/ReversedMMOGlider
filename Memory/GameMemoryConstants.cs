namespace Glider.Common.Objects
{
    internal static class GameMemoryConstants
    {
        internal static int LoadStaticOffsets(MemoryOffsetTable table)
        {
            table.Clear();

            var count = 0;

            AddInt(table, ref count, "MainTable", Wotlk.ClientConnection);
            AddInt(table, ref count, "MainTableProbe", Wotlk.CurMgrOffset);
            AddInt(table, ref count, "InitialOffset", Wotlk.FirstObject);
            AddInt(table, ref count, "MainTableLocalGuid", Wotlk.LocalGuid);
            AddInt(table, ref count, "MainTableActivePlayer", StaticOffsets.MainTableActivePlayer);
            AddInt(table, ref count, "GameObjNext", Wotlk.NextObject);

            AddInt(table, ref count, "PlayerIdAddr", Wotlk.PlayerIdAddr);
            AddInt(table, ref count, "TargetId", Wotlk.TargetGuid);
            AddInt(table, ref count, "UnderCursor", StaticOffsets.UnderCursor);

            AddInt(table, ref count, "D_Player", StaticOffsets.D_Player);
            AddInt(table, ref count, "D_Items", StaticOffsets.D_Items);
            AddInt(table, ref count, "D_NPC", StaticOffsets.D_NPC);
            AddInt(table, ref count, "D_Object", StaticOffsets.D_Object);
            AddInt(table, ref count, "D_Container", StaticOffsets.D_Container);

            AddInt(table, ref count, "X", Wotlk.PosX);
            AddInt(table, ref count, "Y", Wotlk.PosY);
            AddInt(table, ref count, "Z", Wotlk.PosZ);
            AddInt(table, ref count, "Heading", StaticOffsets.Heading);
            AddInt(table, ref count, "Pitch", StaticOffsets.Pitch);

            AddInt(table, ref count, "MoveFlags", StaticOffsets.MoveFlags);
            AddInt(table, ref count, "MoveStruct2", StaticOffsets.MoveStruct2);
            AddInt(table, ref count, "MoveFlags2", StaticOffsets.MoveFlags2);

            AddInt(table, ref count, "MonsterDefinition", StaticOffsets.MonsterDefinition);
            AddInt(table, ref count, "UnitNameSecond", StaticOffsets.UnitNameSecond);
            AddInt(table, ref count, "UnitTitle", StaticOffsets.UnitTitle);
            AddInt(table, ref count, "CreatureType", StaticOffsets.CreatureType);
            AddInt(table, ref count, "HarvestType", StaticOffsets.HarvestType);

            AddInt(table, ref count, "NodeName", StaticOffsets.NodeName);
            AddInt(table, ref count, "NodeNameSecond", StaticOffsets.NodeNameSecond);

            AddInt(table, ref count, "PlayerCasting", StaticOffsets.PlayerCasting);
            AddInt(table, ref count, "PlayerCastingAlt", StaticOffsets.PlayerCastingAlt);
            AddInt(table, ref count, "ComboPointsAddr", StaticOffsets.ComboPointsAddr);
            AddInt(table, ref count, "Combat", StaticOffsets.Combat);

            AddInt(table, ref count, "UIParent", StaticOffsets.UIParent);
            AddInt(table, ref count, "GameState", StaticOffsets.GameState);
            AddInt(table, ref count, "UIGlue1", StaticOffsets.UIGlue1);
            AddInt(table, ref count, "UIGlue2", StaticOffsets.UIGlue2);
            AddInt(table, ref count, "UIGlueStep", StaticOffsets.UIGlueStep);
            AddInt(table, ref count, "UIName", StaticOffsets.UIName);
            AddInt(table, ref count, "UINext", StaticOffsets.UINext);
            AddInt(table, ref count, "UIParentOffset", StaticOffsets.UIParentOffset);
            AddInt(table, ref count, "UIChildren", StaticOffsets.UIChildren);
            AddInt(table, ref count, "UIChildStep", StaticOffsets.UIChildStep);
            AddInt(table, ref count, "UIChildNext", StaticOffsets.UIChildNext);
            AddInt(table, ref count, "UIChildrenOneShot", StaticOffsets.UIChildrenOneShot);
            AddInt(table, ref count, "UIMenuVisible", StaticOffsets.UIMenuVisible);
            AddInt(table, ref count, "UILabelText", StaticOffsets.UILabelText);
            AddInt(table, ref count, "UIFontString", StaticOffsets.UIFontString);
            AddInt(table, ref count, "UITypeLabel", StaticOffsets.UITypeLabel);
            AddInt(table, ref count, "UITypeLabel1", StaticOffsets.UITypeLabel1);
            AddInt(table, ref count, "UITypeLabel2", StaticOffsets.UITypeLabel2);

            AddInt(table, ref count, "ChatFrameBase", StaticOffsets.ChatFrameBase);
            AddInt(table, ref count, "ChatFrameSize", StaticOffsets.ChatFrameSize);
            AddInt(table, ref count, "PlayerNames", Wotlk.PlayerNameStore);

            AddInt(table, ref count, "MySpells", Spellbook.MySpells);
            AddInt(table, ref count, "ActionBarShortcuts", StaticOffsets.ActionBarShortcuts);
            AddInt(table, ref count, "ActionBarCurrent", StaticOffsets.ActionBarCurrent);
            AddInt(table, ref count, "CameraBase", StaticOffsets.CameraBase);
            AddInt(table, ref count, "WorldMap", StaticOffsets.WorldMap);
            AddInt(table, ref count, "MacroBase", StaticOffsets.MacroBase);
            AddInt(table, ref count, "CorpseLocation", StaticOffsets.CorpseLocation);
            AddInt(table, ref count, "ObjectInfo", StaticOffsets.ObjectInfo);
            AddInt(table, ref count, "ObjectInfoSub", StaticOffsets.ObjectInfoSub);
            AddInt(table, ref count, "RaidTargetIcon", StaticOffsets.RaidTargetIcon);
            AddInt(table, ref count, "ZoneText", StaticOffsets.ZoneText);
            AddInt(table, ref count, "SubZoneText", StaticOffsets.SubZoneText);
            AddInt(table, ref count, "RedMessage", StaticOffsets.RedMessage);
            AddInt(table, ref count, "Bobber", StaticOffsets.Bobber);

            AddInt(table, ref count, "NB_BaseCount", StaticOffsets.NB_BaseCount);
            AddInt(table, ref count, "NB_BaseList", StaticOffsets.NB_BaseList);
            AddInt(table, ref count, "NB_ExtCount", StaticOffsets.NB_ExtCount);
            AddInt(table, ref count, "NB_ExtListPtr", StaticOffsets.NB_ExtListPtr);
            AddInt(table, ref count, "UNIT_FIELD_AURA", StaticOffsets.UnitFieldAura);

            AddInt(table, ref count, "FactionSub", StaticOffsets.FactionSub);
            AddInt(table, ref count, "FactionOff1", StaticOffsets.FactionOff1);
            AddInt(table, ref count, "FactionOff2", StaticOffsets.FactionOff2);
            AddInt(table, ref count, "FactionBase", StaticOffsets.FactionBase);

            AddInt(table, ref count, "ClassPtrOffset", StaticOffsets.ClassPtrOffset);
            AddInt(table, ref count, "ClassIdOffset", StaticOffsets.ClassIdOffset);
            AddInt(table, ref count, "RaceIdOffset", StaticOffsets.RaceIdOffset);
            AddInt(table, ref count, "TLSSlot", StaticOffsets.TLSSlot);
            AddInt(table, ref count, "TLSPlayerID", StaticOffsets.TLSPlayerID);
            AddInt(table, ref count, "TLSMainTable", StaticOffsets.TLSMainTable);
            AddInt(table, ref count, "VAPeek", StaticOffsets.VAPeek);
            AddInt(table, ref count, "FLPeek", StaticOffsets.FLPeek);

            AddString(table, ref count, "Buff_Stealth", BuffLists.BuffStealth);
            AddString(table, ref count, "Buff_Ghost", BuffLists.BuffGhost);

            return count;
        }

        internal static bool ValidateRequiredOffsets(MemoryOffsetTable table)
        {
            return table.HasOffset("MainTable") &&
                   table.HasOffset("InitialOffset") &&
                   table.HasOffset("PlayerIdAddr") &&
                   table.HasOffset("D_Player") &&
                   table.HasOffset("D_Object");
        }

        private static void AddInt(MemoryOffsetTable table, ref int count, string key, int value)
        {
            table.AddIntOffset(key, value);
            ++count;
        }

        private static void AddInt(MemoryOffsetTable table, ref int count, string key, uint value)
        {
            table.AddIntOffset(key, unchecked((int)value));
            ++count;
        }

        private static void AddString(MemoryOffsetTable table, ref int count, string key, string value)
        {
            table.AddStringOffset(key, value);
            ++count;
        }

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

        internal static class StaticOffsets
        {
            internal const int MainTableActivePlayer = 0x18;
            internal const int UnderCursor = 0x00BD07A0;

            internal const int D_Player = 0x0;
            internal const int D_Items = 0x0;
            internal const int D_NPC = 0x0;
            internal const int D_Object = 0x0;
            internal const int D_Container = 0x0;

            internal const int Heading = 0x7A8;
            internal const int Pitch = 0x7AC;
            internal const int MoveFlags = 0xD8;
            internal const int MoveStruct2 = 0x0;
            internal const int MoveFlags2 = 0x0;

            internal const int MonsterDefinition = 0x964;
            internal const int UnitNameSecond = 0x0;
            internal const int UnitTitle = 0x4;
            internal const int CreatureType = 0x10;
            internal const int HarvestType = 0x14;

            internal const int NodeName = 0x1A4;
            internal const int NodeNameSecond = 0x90;

            internal const int PlayerCasting = 0x00CECAAC;
            internal const int PlayerCastingAlt = 0x00CECAB4;
            internal const int ComboPointsAddr = 0x00BD084D;
            internal const int Combat = 0xE4;

            internal const int UIParent = 0x00B7436C;
            internal const int GameState = 0x00B674B0;
            internal const int UIGlue1 = 0x00CFA354;
            internal const int UIGlue2 = 0x00CDA330;
            internal const int UIGlueStep = 0x0;
            internal const int UIName = 0x1C;
            internal const int UINext = 0x9C;
            internal const int UIParentOffset = 0x94;
            internal const int UIChildren = 0x98;
            internal const int UIChildStep = 0x0;
            internal const int UIChildNext = 0x9C;
            internal const int UIChildrenOneShot = 0x0;
            internal const int UIMenuVisible = 0x64;
            internal const int UILabelText = 0xE4;
            internal const int UIFontString = 0xE4;
            internal const int UITypeLabel = 0x14;
            internal const int UITypeLabel1 = 0x11;
            internal const int UITypeLabel2 = 0x11;

            internal const int ChatFrameBase = 0x00B75A60;
            internal const int ChatFrameSize = 0x17C0;
            internal const int PlayerNames = 0x00C79D18;

            internal const int ActionBarShortcuts = 0x81E358;
            internal const int ActionBarCurrent = 0x00BDFC34;
            internal const int CameraBase = 0x00B7436C;
            internal const int WorldMap = 0x00BD0F40;
            internal const int MacroBase = 0x00BE1150;
            internal const int CorpseLocation = 0x00BD0A68;
            internal const int ObjectInfo = 0x00C7CA58;
            internal const int ObjectInfoSub = 0x00C81270;
            internal const int RaidTargetIcon = 0x00BD0790;
            internal const int ZoneText = 0x00BD0790;
            internal const int SubZoneText = 0x00BD07C8;
            internal const int RedMessage = 0x00D2B7A0;
            internal const int Bobber = 0x110;

            internal const int NB_BaseCount = 0xDD0;
            internal const int NB_BaseList = 0xC50;
            internal const int NB_ExtCount = 0xDD4;
            internal const int NB_ExtListPtr = 0xC54;
            internal const int UnitFieldAura = 0x148;

            internal const int FactionSub = 0x00A47DD8;
            internal const int FactionOff1 = 0xD4;
            internal const int FactionOff2 = 0x0;
            internal const int FactionBase = 0x00A47DCC;

            internal const int ClassPtrOffset = 0x0;
            internal const int ClassIdOffset = 0x0;
            internal const int RaceIdOffset = 0x0;

            internal const int TLSSlot = 0x0;
            internal const int TLSPlayerID = 0x0;
            internal const int TLSMainTable = 0x0;

            internal const int VAPeek = 0x10;
            internal const int FLPeek = 0x14;
        }

        internal static class BuffLists
        {
            internal const string BuffStealth = "1784 5215 1785 1786 1787 11305 11306 11307 11308 11309 31526 58984";
            internal const string BuffGhost = "8326 20584";
        }
    }
}