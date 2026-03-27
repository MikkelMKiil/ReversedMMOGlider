using System.Collections.Generic;

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

        internal static void PopulateOffsetManager(OffsetManager manager, string descriptorType)
        {
            Dictionary<string, int> fields;

            switch (descriptorType)
            {
                case "Player":
                    fields = DescriptorFields.PlayerFields;
                    break;
                case "NPC":
                    fields = DescriptorFields.UnitFields;
                    break;
                case "Object":
                    fields = DescriptorFields.GameObjectFields;
                    break;
                case "Item":
                    fields = DescriptorFields.ItemFields;
                    break;
                case "Container":
                    fields = DescriptorFields.ContainerFields;
                    break;
                default:
                    Logger.LogMessage("GameMemoryConstants: Unknown descriptor type '" + descriptorType + "', using UnitFields");
                    fields = DescriptorFields.UnitFields;
                    break;
            }

            foreach (var kvp in fields)
                manager.AddOffset(kvp.Key, kvp.Value);
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

            // 3.3.5a (12340) unit base position in object movement struct.
            // Keep aligned with Heading/Pitch at 0x7A8/0x7AC.
            internal const uint PosX = 0x798;
            internal const uint PosY = 0x79C;
            internal const uint PosZ = 0x7A0;

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

        internal static class DescriptorFields
        {
            internal static readonly Dictionary<string, int> ObjectFields = new Dictionary<string, int>
            {
                { "OBJECT_FIELD_GUID",          0x00 },
                { "OBJECT_FIELD_TYPE",          0x08 },
                { "OBJECT_FIELD_ENTRY",         0x0C },
                { "OBJECT_FIELD_SCALE_X",       0x10 },
                { "OBJECT_FIELD_PADDING",       0x14 },
            };

            internal static readonly Dictionary<string, int> ItemFields = new Dictionary<string, int>
            {
                { "OBJECT_FIELD_GUID",          0x00 },
                { "OBJECT_FIELD_TYPE",          0x08 },
                { "OBJECT_FIELD_ENTRY",         0x0C },
                { "OBJECT_FIELD_SCALE_X",       0x10 },
                { "ITEM_FIELD_OWNER",           0x18 },
                { "ITEM_FIELD_CONTAINED",       0x20 },
                { "ITEM_FIELD_CREATOR",         0x28 },
                { "ITEM_FIELD_GIFTCREATOR",     0x30 },
                { "ITEM_FIELD_STACK_COUNT",     0x38 },
                { "ITEM_FIELD_DURATION",        0x3C },
                { "ITEM_FIELD_SPELL_CHARGES",   0x40 },
                { "ITEM_FIELD_FLAGS",           0x54 },
                { "ITEM_FIELD_ENCHANTMENT_1_1", 0x58 },
                { "ITEM_FIELD_PROPERTY_SEED",   0xC0 },
                { "ITEM_FIELD_RANDOM_PROPERTIES_ID", 0xC4 },
                { "ITEM_FIELD_DURABILITY",      0xC8 },
                { "ITEM_FIELD_MAXDURABILITY",   0xCC },
                { "ITEM_FIELD_CREATE_PLAYED_TIME", 0xD0 },
            };

            internal static readonly Dictionary<string, int> ContainerFields = new Dictionary<string, int>
            {
                { "OBJECT_FIELD_GUID",          0x00 },
                { "OBJECT_FIELD_TYPE",          0x08 },
                { "OBJECT_FIELD_ENTRY",         0x0C },
                { "OBJECT_FIELD_SCALE_X",       0x10 },
                { "ITEM_FIELD_OWNER",           0x18 },
                { "ITEM_FIELD_CONTAINED",       0x20 },
                { "ITEM_FIELD_STACK_COUNT",     0x38 },
                { "ITEM_FIELD_FLAGS",           0x54 },
                { "ITEM_FIELD_DURABILITY",      0xC8 },
                { "ITEM_FIELD_MAXDURABILITY",   0xCC },
                { "CONTAINER_FIELD_NUM_SLOTS",  0xD8 },
                { "CONTAINER_FIELD_ALIGN_PAD",  0xDC },
                { "CONTAINER_FIELD_SLOT_1",     0xE0 },
            };

            internal static readonly Dictionary<string, int> UnitFields = new Dictionary<string, int>
            {
                { "OBJECT_FIELD_GUID",              0x00 },
                { "OBJECT_FIELD_TYPE",              0x08 },
                { "OBJECT_FIELD_ENTRY",             0x0C },
                { "OBJECT_FIELD_SCALE_X",           0x10 },
                { "UNIT_FIELD_CHARM",               0x18 },
                { "UNIT_FIELD_SUMMON",              0x20 },
                { "UNIT_FIELD_CRITTER",             0x28 },
                { "UNIT_FIELD_CHARMEDBY",           0x30 },
                { "UNIT_FIELD_SUMMONEDBY",          0x38 },
                { "UNIT_FIELD_CREATEDBY",           0x40 },
                { "UNIT_FIELD_TARGET",              0x48 },
                { "UNIT_FIELD_CHANNEL_OBJECT",      0x50 },
                { "UNIT_CHANNEL_SPELL",             0x58 },
                { "UNIT_FIELD_BYTES_0",             0x5C },
                { "UNIT_FIELD_HEALTH",              0x60 },
                { "UNIT_FIELD_POWER1",              0x64 },
                { "UNIT_FIELD_POWER2",              0x68 },
                { "UNIT_FIELD_POWER3",              0x6C },
                { "UNIT_FIELD_POWER4",              0x70 },
                { "UNIT_FIELD_POWER5",              0x74 },
                { "UNIT_FIELD_POWER6",              0x78 },
                { "UNIT_FIELD_POWER7",              0x7C },
                { "UNIT_FIELD_MAXHEALTH",           0x80 },
                { "UNIT_FIELD_MAXPOWER1",           0x84 },
                { "UNIT_FIELD_MAXPOWER2",           0x88 },
                { "UNIT_FIELD_MAXPOWER3",           0x8C },
                { "UNIT_FIELD_MAXPOWER4",           0x90 },
                { "UNIT_FIELD_MAXPOWER5",           0x94 },
                { "UNIT_FIELD_MAXPOWER6",           0x98 },
                { "UNIT_FIELD_MAXPOWER7",           0x9C },
                { "UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER", 0xA0 },
                { "UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER", 0xBC },
                { "UNIT_FIELD_LEVEL",               0xD8 },
                { "UNIT_FIELD_FACTIONTEMPLATE",     0xDC },
                { "UNIT_VIRTUAL_ITEM_SLOT_ID",      0xE0 },
                { "UNIT_FIELD_FLAGS",               0xEC },
                { "UNIT_FIELD_FLAGS_2",             0xF0 },
                { "UNIT_FIELD_AURASTATE",           0xF4 },
                { "UNIT_FIELD_BASEATTACKTIME",      0xF8 },
                { "UNIT_FIELD_RANGEDATTACKTIME",    0x100 },
                { "UNIT_FIELD_BOUNDINGRADIUS",      0x104 },
                { "UNIT_FIELD_COMBATREACH",         0x108 },
                { "UNIT_FIELD_DISPLAYID",           0x10C },
                { "UNIT_FIELD_NATIVEDISPLAYID",     0x110 },
                { "UNIT_FIELD_MOUNTDISPLAYID",      0x114 },
                { "UNIT_FIELD_MINDAMAGE",           0x118 },
                { "UNIT_FIELD_MAXDAMAGE",           0x11C },
                { "UNIT_FIELD_MINOFFHANDDAMAGE",    0x120 },
                { "UNIT_FIELD_MAXOFFHANDDAMAGE",    0x124 },
                { "UNIT_FIELD_BYTES_1",             0x128 },
                { "UNIT_FIELD_PETNUMBER",           0x12C },
                { "UNIT_FIELD_PET_NAME_TIMESTAMP",  0x130 },
                { "UNIT_FIELD_PETEXPERIENCE",       0x134 },
                { "UNIT_FIELD_PETNEXTLEVELEXP",     0x138 },
                { "UNIT_DYNAMIC_FLAGS",             0x13C },
                { "UNIT_MOD_CAST_SPEED",            0x140 },
                { "UNIT_CREATED_BY_SPELL",          0x144 },
                { "UNIT_NPC_FLAGS",                 0x148 },
                { "UNIT_NPC_EMOTESTATE",            0x14C },
                { "UNIT_FIELD_STAT0",               0x150 },
                { "UNIT_FIELD_STAT1",               0x154 },
                { "UNIT_FIELD_STAT2",               0x158 },
                { "UNIT_FIELD_STAT3",               0x15C },
                { "UNIT_FIELD_STAT4",               0x160 },
                { "UNIT_FIELD_POSSTAT0",            0x164 },
                { "UNIT_FIELD_POSSTAT1",            0x168 },
                { "UNIT_FIELD_POSSTAT2",            0x16C },
                { "UNIT_FIELD_POSSTAT3",            0x170 },
                { "UNIT_FIELD_POSSTAT4",            0x174 },
                { "UNIT_FIELD_NEGSTAT0",            0x178 },
                { "UNIT_FIELD_NEGSTAT1",            0x17C },
                { "UNIT_FIELD_NEGSTAT2",            0x180 },
                { "UNIT_FIELD_NEGSTAT3",            0x184 },
                { "UNIT_FIELD_NEGSTAT4",            0x188 },
                { "UNIT_FIELD_RESISTANCES",         0x18C },
                { "UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE", 0x1A8 },
                { "UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE", 0x1C4 },
                { "UNIT_FIELD_BASE_MANA",           0x1E0 },
                { "UNIT_FIELD_BASE_HEALTH",         0x1E4 },
                { "UNIT_FIELD_BYTES_2",             0x1E8 },
                { "UNIT_FIELD_ATTACK_POWER",        0x1EC },
                { "UNIT_FIELD_ATTACK_POWER_MODS",   0x1F0 },
                { "UNIT_FIELD_ATTACK_POWER_MULTIPLIER", 0x1F4 },
                { "UNIT_FIELD_RANGED_ATTACK_POWER", 0x1F8 },
                { "UNIT_FIELD_RANGED_ATTACK_POWER_MODS", 0x1FC },
                { "UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER", 0x200 },
                { "UNIT_FIELD_MINRANGEDDAMAGE",     0x204 },
                { "UNIT_FIELD_MAXRANGEDDAMAGE",     0x208 },
                { "UNIT_FIELD_POWER_COST_MODIFIER", 0x20C },
                { "UNIT_FIELD_POWER_COST_MULTIPLIER", 0x228 },
                { "UNIT_FIELD_MAXHEALTHMODIFIER",   0x244 },
                { "UNIT_FIELD_HOVERHEIGHT",         0x248 },
            };

            internal static readonly Dictionary<string, int> PlayerFields = new Dictionary<string, int>
            {
                { "OBJECT_FIELD_GUID",              0x00 },
                { "OBJECT_FIELD_TYPE",              0x08 },
                { "OBJECT_FIELD_ENTRY",             0x0C },
                { "UNIT_FIELD_CHARM",               0x18 },
                { "UNIT_FIELD_SUMMON",              0x20 },
                { "UNIT_FIELD_CREATEDBY",           0x40 },
                { "UNIT_FIELD_TARGET",              0x48 },
                { "UNIT_FIELD_CHANNEL_OBJECT",      0x50 },
                { "UNIT_FIELD_BYTES_0",             0x5C },
                { "UNIT_FIELD_HEALTH",              0x60 },
                { "UNIT_FIELD_POWER1",              0x64 },
                { "UNIT_FIELD_POWER2",              0x68 },
                { "UNIT_FIELD_POWER3",              0x6C },
                { "UNIT_FIELD_POWER4",              0x70 },
                { "UNIT_FIELD_POWER5",              0x74 },
                { "UNIT_FIELD_POWER7",              0x7C },
                { "UNIT_FIELD_MAXHEALTH",           0x80 },
                { "UNIT_FIELD_MAXPOWER1",           0x84 },
                { "UNIT_FIELD_MAXPOWER4",           0x90 },
                { "UNIT_FIELD_LEVEL",               0xD8 },
                { "UNIT_FIELD_FACTIONTEMPLATE",     0xDC },
                { "UNIT_FIELD_FLAGS",               0xEC },
                { "UNIT_FIELD_BYTES_1",             0x128 },
                { "UNIT_DYNAMIC_FLAGS",             0x13C },
                { "PLAYER_FLAGS",                   0x258 },
                { "PLAYER_GUILDID",                 0x25C },
                { "PLAYER_GUILDRANK",               0x260 },
                { "PLAYER_BYTES",                   0x264 },
                { "PLAYER_BYTES_2",                 0x268 },
                { "PLAYER_BYTES_3",                 0x26C },
                { "PLAYER_DUEL_TEAM",               0x270 },
                { "PLAYER_GUILD_TIMESTAMP",         0x274 },
                { "PLAYER_QUEST_LOG_1_1",           0x278 },
                { "PLAYER_VISIBLE_ITEM_1_ENTRYID",  0x46C },
                { "PLAYER_FIELD_INV_SLOT_HEAD",     0x510 },
                { "PLAYER_FIELD_PACK_SLOT_1",       0x5C8 },
                { "PLAYER_FIELD_BANK_SLOT_1",       0x648 },
                { "PLAYER_FIELD_BANKBAG_SLOT_1",    0x728 },
                { "PLAYER_FIELD_VENDORBUYBACK_SLOT_1", 0x760 },
                { "PLAYER_FIELD_KEYRING_SLOT_1",    0x7C0 },
                { "PLAYER_FARSIGHT",                0x9C0 },
                { "PLAYER_FIELD_KNOWN_TITLES",      0x9C8 },
                { "PLAYER_FIELD_KNOWN_TITLES1",     0x9D0 },
                { "PLAYER_FIELD_KNOWN_CURRENCIES",  0x9E0 },
                { "PLAYER_XP",                      0x9E8 },
                { "PLAYER_NEXT_LEVEL_XP",           0x9EC },
                { "PLAYER_SKILL_INFO_1_1",          0x9F0 },
                { "PLAYER_CHARACTER_POINTS1",       0xFF0 },
                { "PLAYER_CHARACTER_POINTS2",       0xFF4 },
                { "PLAYER_TRACK_CREATURES",         0xFF8 },
                { "PLAYER_TRACK_RESOURCES",         0xFFC },
                { "PLAYER_BLOCK_PERCENTAGE",        0x1000 },
                { "PLAYER_DODGE_PERCENTAGE",        0x1004 },
                { "PLAYER_PARRY_PERCENTAGE",        0x1008 },
                { "PLAYER_EXPERTISE",               0x100C },
                { "PLAYER_OFFHAND_EXPERTISE",       0x1010 },
                { "PLAYER_CRIT_PERCENTAGE",         0x1014 },
                { "PLAYER_RANGED_CRIT_PERCENTAGE",  0x1018 },
                { "PLAYER_OFFHAND_CRIT_PERCENTAGE", 0x101C },
                { "PLAYER_SPELL_CRIT_PERCENTAGE1",  0x1020 },
                { "PLAYER_SHIELD_BLOCK",            0x103C },
                { "PLAYER_SHIELD_BLOCK_CRIT_PERCENTAGE", 0x1040 },
                { "PLAYER_EXPLORED_ZONES_1",        0x1044 },
                { "PLAYER_REST_STATE_EXPERIENCE",   0x1244 },
                { "PLAYER_FIELD_COINAGE",           0x1248 },
                { "PLAYER_FIELD_MOD_DAMAGE_DONE_POS", 0x124C },
                { "PLAYER_FIELD_MOD_DAMAGE_DONE_NEG", 0x1268 },
                { "PLAYER_FIELD_MOD_DAMAGE_DONE_PCT", 0x1284 },
                { "PLAYER_FIELD_MOD_HEALING_DONE_POS", 0x12A0 },
                { "PLAYER_FIELD_MOD_TARGET_RESISTANCE", 0x12AC },
                { "PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE", 0x12B0 },
                { "PLAYER_FIELD_BYTES",             0x12B4 },
                { "PLAYER_AMMO_ID",                 0x12B8 },
                { "PLAYER_SELF_RES_SPELL",          0x12BC },
                { "PLAYER_FIELD_PVP_MEDALS",        0x12C0 },
                { "PLAYER_FIELD_BUYBACK_PRICE_1",   0x12C4 },
                { "PLAYER_FIELD_BUYBACK_TIMESTAMP_1", 0x12F4 },
                { "PLAYER_FIELD_KILLS",             0x1324 },
                { "PLAYER_FIELD_TODAY_CONTRIBUTION", 0x1328 },
                { "PLAYER_FIELD_YESTERDAY_CONTRIBUTION", 0x132C },
                { "PLAYER_FIELD_LIFETIME_HONOURABLE_KILLS", 0x1330 },
                { "PLAYER_FIELD_GLYPH_SLOTS_1",     0x1480 },
                { "PLAYER_FIELD_GLYPHS_1",          0x1498 },
            };

            internal static readonly Dictionary<string, int> GameObjectFields = new Dictionary<string, int>
            {
                { "OBJECT_FIELD_GUID",          0x00 },
                { "OBJECT_FIELD_TYPE",          0x08 },
                { "OBJECT_FIELD_ENTRY",         0x0C },
                { "OBJECT_FIELD_SCALE_X",       0x10 },
                { "OBJECT_FIELD_CREATED_BY",    0x18 },
                { "GAMEOBJECT_DISPLAYID",       0x20 },
                { "GAMEOBJECT_FLAGS",           0x24 },
                { "GAMEOBJECT_PARENTROTATION",  0x28 },
                { "GAMEOBJECT_DYNAMIC",         0x38 },
                { "GAMEOBJECT_FACTION",         0x3C },
                { "GAMEOBJECT_LEVEL",           0x40 },
                { "GAMEOBJECT_BYTES_1",         0x44 },
            };

            internal static readonly Dictionary<string, int> DynamicObjectFields = new Dictionary<string, int>
            {
                { "OBJECT_FIELD_GUID",          0x00 },
                { "OBJECT_FIELD_TYPE",          0x08 },
                { "OBJECT_FIELD_ENTRY",         0x0C },
                { "DYNAMICOBJECT_CASTER",       0x18 },
                { "DYNAMICOBJECT_BYTES",        0x20 },
                { "DYNAMICOBJECT_SPELLID",      0x24 },
                { "DYNAMICOBJECT_RADIUS",       0x28 },
                { "DYNAMICOBJECT_CASTTIME",     0x2C },
            };

            internal static readonly Dictionary<string, int> CorpseFields = new Dictionary<string, int>
            {
                { "OBJECT_FIELD_GUID",          0x00 },
                { "OBJECT_FIELD_TYPE",          0x08 },
                { "OBJECT_FIELD_ENTRY",         0x0C },
                { "CORPSE_FIELD_OWNER",         0x18 },
                { "CORPSE_FIELD_PARTY",         0x20 },
                { "CORPSE_FIELD_DISPLAY_ID",    0x28 },
                { "CORPSE_FIELD_ITEM",          0x2C },
                { "CORPSE_FIELD_BYTES_1",       0x78 },
                { "CORPSE_FIELD_BYTES_2",       0x7C },
                { "CORPSE_FIELD_GUILD",         0x80 },
                { "CORPSE_FIELD_FLAGS",         0x84 },
                { "CORPSE_FIELD_DYNAMIC_FLAGS", 0x88 },
            };
        }
    }
}