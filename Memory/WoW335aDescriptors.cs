// WoW 3.3.5a Build 12340 — Static Descriptor Field Offsets
// All values are BYTE offsets into the descriptor/storage array (StorageAddress + offset).
// Source: wowdev.wiki, OwnedCore, UnKnoWnCheaTs — community-verified for build 12340.

#nullable disable
using System.Collections.Generic;

/// <summary>
/// Contains all verified WoW 3.3.5a (build 12340) descriptor field byte offsets.
/// Replaces the old dynamic descriptor scanning that no longer works.
/// </summary>
public static class WoW335aDescriptors
{
    // =========================================================================
    //  OBJECT FIELDS (eObjectFields) — base for all object types
    //  Descriptor index range: 0x0000 – 0x0005 (6 fields)
    //  OBJECT_END = 0x6 * 4 = 0x18
    // =========================================================================
    public static readonly Dictionary<string, int> ObjectFields = new Dictionary<string, int>
    {
        { "OBJECT_FIELD_GUID",          0x00 },  // uint64 (2 DWORDs)
        { "OBJECT_FIELD_TYPE",          0x08 },  // uint32 + padding
        { "OBJECT_FIELD_ENTRY",         0x0C },  // uint32 — template/entry ID
        { "OBJECT_FIELD_SCALE_X",       0x10 },  // float
        { "OBJECT_FIELD_PADDING",       0x14 },  // uint32
    };

    // =========================================================================
    //  ITEM FIELDS (eItemFields) — starts after OBJECT_END = 0x18
    //  ITEM_END = 0x3C (index) * 4 = 0xF0 (but relative to descriptor base)
    // =========================================================================
    public static readonly Dictionary<string, int> ItemFields = new Dictionary<string, int>
    {
        // Object fields first (inherited)
        { "OBJECT_FIELD_GUID",          0x00 },
        { "OBJECT_FIELD_TYPE",          0x08 },
        { "OBJECT_FIELD_ENTRY",         0x0C },
        { "OBJECT_FIELD_SCALE_X",       0x10 },

        // Item-specific fields (start at OBJECT_END = 0x18)
        { "ITEM_FIELD_OWNER",           0x18 },  // uint64
        { "ITEM_FIELD_CONTAINED",       0x20 },  // uint64
        { "ITEM_FIELD_CREATOR",         0x28 },  // uint64
        { "ITEM_FIELD_GIFTCREATOR",     0x30 },  // uint64
        { "ITEM_FIELD_STACK_COUNT",     0x38 },  // uint32
        { "ITEM_FIELD_DURATION",        0x3C },  // uint32
        { "ITEM_FIELD_SPELL_CHARGES",   0x40 },  // 5x uint32 (0x40-0x53)
        { "ITEM_FIELD_FLAGS",           0x54 },  // uint32
        { "ITEM_FIELD_ENCHANTMENT_1_1", 0x58 },  // enchantment array start
        { "ITEM_FIELD_PROPERTY_SEED",   0xC0 },  // uint32
        { "ITEM_FIELD_RANDOM_PROPERTIES_ID", 0xC4 }, // uint32
        { "ITEM_FIELD_DURABILITY",      0xC8 },  // uint32
        { "ITEM_FIELD_MAXDURABILITY",   0xCC },  // uint32
        { "ITEM_FIELD_CREATE_PLAYED_TIME", 0xD0 }, // uint32
    };

    // =========================================================================
    //  CONTAINER FIELDS (eContainerFields) — starts after ITEM_END
    //  Containers extend Items
    // =========================================================================
    public static readonly Dictionary<string, int> ContainerFields = new Dictionary<string, int>
    {
        // Object fields
        { "OBJECT_FIELD_GUID",          0x00 },
        { "OBJECT_FIELD_TYPE",          0x08 },
        { "OBJECT_FIELD_ENTRY",         0x0C },
        { "OBJECT_FIELD_SCALE_X",       0x10 },

        // Item fields (inherited)
        { "ITEM_FIELD_OWNER",           0x18 },
        { "ITEM_FIELD_CONTAINED",       0x20 },
        { "ITEM_FIELD_STACK_COUNT",     0x38 },
        { "ITEM_FIELD_FLAGS",           0x54 },
        { "ITEM_FIELD_DURABILITY",      0xC8 },
        { "ITEM_FIELD_MAXDURABILITY",   0xCC },

        // Container-specific fields (start at ITEM_END = 0xD4)
        { "CONTAINER_FIELD_NUM_SLOTS",  0xD8 },  // uint32
        { "CONTAINER_FIELD_ALIGN_PAD",  0xDC },  // uint32
        { "CONTAINER_FIELD_SLOT_1",     0xE0 },  // 36 x uint64 slots
    };

    // =========================================================================
    //  UNIT FIELDS (eUnitFields) — starts after OBJECT_END = 0x18
    //  UNIT_END = 0xA6 (index) * 4 = 0x298 (but byte offset from descriptor base)
    // =========================================================================
    public static readonly Dictionary<string, int> UnitFields = new Dictionary<string, int>
    {
        // Object fields (inherited)
        { "OBJECT_FIELD_GUID",              0x00 },
        { "OBJECT_FIELD_TYPE",              0x08 },
        { "OBJECT_FIELD_ENTRY",             0x0C },
        { "OBJECT_FIELD_SCALE_X",           0x10 },

        // Unit fields (start at OBJECT_END = 0x18)
        { "UNIT_FIELD_CHARM",               0x18 },  // uint64
        { "UNIT_FIELD_SUMMON",              0x20 },  // uint64
        { "UNIT_FIELD_CRITTER",             0x28 },  // uint64
        { "UNIT_FIELD_CHARMEDBY",           0x30 },  // uint64
        { "UNIT_FIELD_SUMMONEDBY",          0x38 },  // uint64
        { "UNIT_FIELD_CREATEDBY",           0x40 },  // uint64
        { "UNIT_FIELD_TARGET",              0x48 },  // uint64
        { "UNIT_FIELD_CHANNEL_OBJECT",      0x50 },  // uint64
        { "UNIT_FIELD_HEALTH",              0x58 },  // uint32
        { "UNIT_FIELD_POWER1",              0x5C },  // uint32 (mana)
        { "UNIT_FIELD_POWER2",              0x60 },  // uint32 (rage)
        { "UNIT_FIELD_POWER3",              0x64 },  // uint32 (focus)
        { "UNIT_FIELD_POWER4",              0x68 },  // uint32 (energy)
        { "UNIT_FIELD_POWER5",              0x6C },  // uint32 (happiness)
        { "UNIT_FIELD_POWER6",              0x70 },  // uint32
        { "UNIT_FIELD_POWER7",              0x74 },  // uint32 (runic power)
        { "UNIT_FIELD_MAXHEALTH",           0x78 },  // uint32
        { "UNIT_FIELD_MAXPOWER1",           0x7C },  // uint32
        { "UNIT_FIELD_MAXPOWER2",           0x80 },  // uint32
        { "UNIT_FIELD_MAXPOWER3",           0x84 },  // uint32
        { "UNIT_FIELD_MAXPOWER4",           0x88 },  // uint32
        { "UNIT_FIELD_MAXPOWER5",           0x8C },  // uint32
        { "UNIT_FIELD_MAXPOWER6",           0x90 },  // uint32
        { "UNIT_FIELD_MAXPOWER7",           0x94 },  // uint32
        { "UNIT_FIELD_POWER_REGEN_FLAT_MODIFIER", 0x98 },   // 7x float
        { "UNIT_FIELD_POWER_REGEN_INTERRUPTED_FLAT_MODIFIER", 0xB4 }, // 7x float
        { "UNIT_FIELD_LEVEL",               0xD0 },  // uint32
        { "UNIT_FIELD_FACTIONTEMPLATE",     0xD4 },  // uint32
        { "UNIT_VIRTUAL_ITEM_SLOT_ID",      0xD8 },  // 3x uint32
        { "UNIT_FIELD_FLAGS",               0xE4 },  // uint32
        { "UNIT_FIELD_FLAGS_2",             0xE8 },  // uint32
        { "UNIT_FIELD_AURASTATE",           0xEC },  // uint32
        { "UNIT_FIELD_BASEATTACKTIME",      0xF0 },  // 2x uint32
        { "UNIT_FIELD_RANGEDATTACKTIME",    0xF8 },  // uint32
        { "UNIT_FIELD_BOUNDINGRADIUS",      0xFC },  // float
        { "UNIT_FIELD_COMBATREACH",         0x100 }, // float
        { "UNIT_FIELD_DISPLAYID",           0x104 }, // uint32
        { "UNIT_FIELD_NATIVEDISPLAYID",     0x108 }, // uint32
        { "UNIT_FIELD_MOUNTDISPLAYID",      0x10C }, // uint32
        { "UNIT_FIELD_MINDAMAGE",           0x110 }, // float
        { "UNIT_FIELD_MAXDAMAGE",           0x114 }, // float
        { "UNIT_FIELD_MINOFFHANDDAMAGE",    0x118 }, // float
        { "UNIT_FIELD_MAXOFFHANDDAMAGE",    0x11C }, // float
        { "UNIT_FIELD_BYTES_1",             0x120 }, // uint32 (standstate, pet loyalty, shapeshift, stealth)
        { "UNIT_FIELD_PETNUMBER",           0x124 }, // uint32
        { "UNIT_FIELD_PET_NAME_TIMESTAMP",  0x128 }, // uint32
        { "UNIT_FIELD_PETEXPERIENCE",       0x12C }, // uint32
        { "UNIT_FIELD_PETNEXTLEVELEXP",     0x130 }, // uint32
        { "UNIT_DYNAMIC_FLAGS",             0x134 }, // uint32
        { "UNIT_CHANNEL_SPELL",             0x138 }, // uint32
        { "UNIT_MOD_CAST_SPEED",            0x13C }, // float
        { "UNIT_CREATED_BY_SPELL",          0x140 }, // uint32
        { "UNIT_NPC_FLAGS",                 0x144 }, // uint32
        { "UNIT_NPC_EMOTESTATE",            0x148 }, // uint32
        { "UNIT_FIELD_STAT0",               0x14C }, // uint32 (strength)
        { "UNIT_FIELD_STAT1",               0x150 }, // uint32 (agility)
        { "UNIT_FIELD_STAT2",               0x154 }, // uint32 (stamina)
        { "UNIT_FIELD_STAT3",               0x158 }, // uint32 (intellect)
        { "UNIT_FIELD_STAT4",               0x15C }, // uint32 (spirit)
        { "UNIT_FIELD_POSSTAT0",            0x160 }, // uint32
        { "UNIT_FIELD_POSSTAT1",            0x164 }, // uint32
        { "UNIT_FIELD_POSSTAT2",            0x168 }, // uint32
        { "UNIT_FIELD_POSSTAT3",            0x16C }, // uint32
        { "UNIT_FIELD_POSSTAT4",            0x170 }, // uint32
        { "UNIT_FIELD_NEGSTAT0",            0x174 }, // uint32
        { "UNIT_FIELD_NEGSTAT1",            0x178 }, // uint32
        { "UNIT_FIELD_NEGSTAT2",            0x17C }, // uint32
        { "UNIT_FIELD_NEGSTAT3",            0x180 }, // uint32
        { "UNIT_FIELD_NEGSTAT4",            0x184 }, // uint32
        { "UNIT_FIELD_RESISTANCES",         0x188 }, // 7x uint32 (physical, holy, fire, nature, frost, shadow, arcane)
        { "UNIT_FIELD_RESISTANCEBUFFMODSPOSITIVE", 0x1A4 }, // 7x uint32
        { "UNIT_FIELD_RESISTANCEBUFFMODSNEGATIVE", 0x1C0 }, // 7x uint32
        { "UNIT_FIELD_BASE_MANA",           0x1DC }, // uint32
        { "UNIT_FIELD_BASE_HEALTH",         0x1E0 }, // uint32
        { "UNIT_FIELD_BYTES_2",             0x1E4 }, // uint32 (sheath state etc.)
        { "UNIT_FIELD_ATTACK_POWER",        0x1E8 }, // uint32
        { "UNIT_FIELD_ATTACK_POWER_MODS",   0x1EC }, // int32
        { "UNIT_FIELD_ATTACK_POWER_MULTIPLIER", 0x1F0 }, // float
        { "UNIT_FIELD_RANGED_ATTACK_POWER", 0x1F4 }, // uint32
        { "UNIT_FIELD_RANGED_ATTACK_POWER_MODS", 0x1F8 }, // int32
        { "UNIT_FIELD_RANGED_ATTACK_POWER_MULTIPLIER", 0x1FC }, // float
        { "UNIT_FIELD_MINRANGEDDAMAGE",     0x200 }, // float
        { "UNIT_FIELD_MAXRANGEDDAMAGE",     0x204 }, // float
        { "UNIT_FIELD_POWER_COST_MODIFIER", 0x208 }, // 7x uint32
        { "UNIT_FIELD_POWER_COST_MULTIPLIER", 0x224 }, // 7x float
        { "UNIT_FIELD_MAXHEALTHMODIFIER",   0x240 }, // float
        { "UNIT_FIELD_HOVERHEIGHT",         0x244 }, // float

        // UNIT_FIELD_BYTES_0: race/class/gender/power type
        { "UNIT_FIELD_BYTES_0",             0x248 },
    };

    // =========================================================================
    //  PLAYER FIELDS (ePlayerFields) — starts after UNIT_END
    //  UNIT_END index = 0xA6, so byte offset = 0x298
    //  But player descriptor offsets are absolute from descriptor base
    // =========================================================================
    public static readonly Dictionary<string, int> PlayerFields = new Dictionary<string, int>
    {
        // Object fields (inherited)
        { "OBJECT_FIELD_GUID",              0x00 },
        { "OBJECT_FIELD_TYPE",              0x08 },
        { "OBJECT_FIELD_ENTRY",             0x0C },

        // Unit fields (inherited — key ones used by code)
        { "UNIT_FIELD_CHARM",               0x18 },
        { "UNIT_FIELD_SUMMON",              0x20 },
        { "UNIT_FIELD_CREATEDBY",           0x40 },
        { "UNIT_FIELD_TARGET",              0x48 },
        { "UNIT_FIELD_CHANNEL_OBJECT",      0x50 },
        { "UNIT_FIELD_HEALTH",              0x58 },
        { "UNIT_FIELD_POWER1",              0x5C },
        { "UNIT_FIELD_POWER2",              0x60 },
        { "UNIT_FIELD_POWER3",              0x64 },
        { "UNIT_FIELD_POWER4",              0x68 },
        { "UNIT_FIELD_POWER5",              0x6C },
        { "UNIT_FIELD_POWER7",              0x74 },
        { "UNIT_FIELD_MAXHEALTH",           0x78 },
        { "UNIT_FIELD_MAXPOWER1",           0x7C },
        { "UNIT_FIELD_MAXPOWER4",           0x88 },
        { "UNIT_FIELD_LEVEL",               0xD0 },
        { "UNIT_FIELD_FACTIONTEMPLATE",     0xD4 },
        { "UNIT_FIELD_FLAGS",               0xE4 },
        { "UNIT_FIELD_BYTES_0",             0x248 },
        { "UNIT_FIELD_BYTES_1",             0x120 },
        { "UNIT_DYNAMIC_FLAGS",             0x134 },

        // Player-specific fields 
        // PLAYER_DUEL_ARBITER = UNIT_END = 0x298 area
        { "PLAYER_FLAGS",                   0x2A0 },
        { "PLAYER_GUILDID",                 0x2A4 },
        { "PLAYER_GUILDRANK",               0x2A8 },
        { "PLAYER_BYTES",                   0x2AC },
        { "PLAYER_BYTES_2",                 0x2B0 },
        { "PLAYER_BYTES_3",                 0x2B4 },
        { "PLAYER_DUEL_TEAM",               0x2B8 },
        { "PLAYER_GUILD_TIMESTAMP",         0x2BC },

        // Quest log — 25 quests, each with 5 fields (QuestID, State, Counts, Time, padding)
        // PLAYER_QUEST_LOG_1_1 starts at descriptor index 0xB0 => byte offset 0x2C0
        { "PLAYER_QUEST_LOG_1_1",           0x2C0 },

        // Visible equipment slots (19 slots, 2 fields each — entryID + enchant)
        // Starts after quest log region
        { "PLAYER_VISIBLE_ITEM_1_ENTRYID",  0x4F8 },

        // Inventory/equipment slots
        { "PLAYER_FIELD_INV_SLOT_HEAD",     0x5AC },  // 23 x uint64 = 0xB8 bytes (equipment 0-22)
        { "PLAYER_FIELD_PACK_SLOT_1",       0x664 },  // 16 x uint64 = 0x80 bytes (backpack main slots)
        { "PLAYER_FIELD_BANK_SLOT_1",       0x6E4 },  // 28 x uint64

        // Key ring and other slots
        { "PLAYER_FIELD_BANKBAG_SLOT_1",    0x7C4 },  // 7 x uint64
        { "PLAYER_FIELD_VENDORBUYBACK_SLOT_1", 0x7FC }, // 12 x uint64
        { "PLAYER_FIELD_KEYRING_SLOT_1",    0x85C },  // 32 x uint64

        // Frag bag
        { "PLAYER_FARSIGHT",               0x99C },
        { "PLAYER_FIELD_KNOWN_TITLES",     0x9A4 },
        { "PLAYER_FIELD_KNOWN_CURRENCIES", 0x9BC },

        // XP
        { "PLAYER_XP",                     0x9C4 },
        { "PLAYER_NEXT_LEVEL_XP",          0x9C8 },

        // Skill info — 384 x uint32 block
        { "PLAYER_SKILL_INFO_1_1",         0x9CC },

        // Character points
        { "PLAYER_CHARACTER_POINTS1",      0xFCC },  // talent points
        { "PLAYER_CHARACTER_POINTS2",      0xFD0 },  // professions (unused in 3.3.5)

        // Track creatures/resources
        { "PLAYER_TRACK_CREATURES",        0xFD4 },
        { "PLAYER_TRACK_RESOURCES",        0xFD8 },

        // Block/dodge/parry pct
        { "PLAYER_BLOCK_PERCENTAGE",       0xFDC },
        { "PLAYER_DODGE_PERCENTAGE",       0xFE0 },
        { "PLAYER_PARRY_PERCENTAGE",       0xFE4 },

        // Expertise
        { "PLAYER_EXPERTISE",              0xFE8 },
        { "PLAYER_OFFHAND_EXPERTISE",      0xFEC },

        // Critical hit percentages
        { "PLAYER_CRIT_PERCENTAGE",        0xFF0 },
        { "PLAYER_RANGED_CRIT_PERCENTAGE", 0xFF4 },
        { "PLAYER_OFFHAND_CRIT_PERCENTAGE",0xFF8 },

        // Spell crit (7 schools)
        { "PLAYER_SPELL_CRIT_PERCENTAGE1", 0xFFC },

        // Shield block
        { "PLAYER_SHIELD_BLOCK",           0x1018 },
        { "PLAYER_SHIELD_BLOCK_CRIT_PERCENTAGE", 0x101C },

        // Explored zones (128 uint32s)
        { "PLAYER_EXPLORED_ZONES_1",       0x1020 },

        // Rest state XP
        { "PLAYER_REST_STATE_EXPERIENCE",  0x1220 },

        // Coinage
        { "PLAYER_FIELD_COINAGE",          0x1224 },

        // Mod damage done (7 schools)
        { "PLAYER_FIELD_MOD_DAMAGE_DONE_POS", 0x1228 },
        { "PLAYER_FIELD_MOD_DAMAGE_DONE_NEG", 0x1244 },
        { "PLAYER_FIELD_MOD_DAMAGE_DONE_PCT", 0x1260 },

        // Healing done
        { "PLAYER_FIELD_MOD_HEALING_DONE_POS", 0x127C },
        { "PLAYER_FIELD_MOD_TARGET_RESISTANCE", 0x1280 },
        { "PLAYER_FIELD_MOD_TARGET_PHYSICAL_RESISTANCE", 0x1284 },

        // Player field bytes
        { "PLAYER_FIELD_BYTES",            0x1288 },

        // Ammo ID
        { "PLAYER_AMMO_ID",               0x128C },

        // Self resurrect spell
        { "PLAYER_SELF_RES_SPELL",         0x1290 },

        // PVP medals
        { "PLAYER_FIELD_PVP_MEDALS",       0x1294 },

        // Buyback prices + timestamps (12 each)
        { "PLAYER_FIELD_BUYBACK_PRICE_1",  0x1298 },
        { "PLAYER_FIELD_BUYBACK_TIMESTAMP_1", 0x12C8 },

        // Kills
        { "PLAYER_FIELD_KILLS",            0x12F8 },
        { "PLAYER_FIELD_TODAY_CONTRIBUTION", 0x12FC },
        { "PLAYER_FIELD_YESTERDAY_CONTRIBUTION", 0x1300 },
        { "PLAYER_FIELD_LIFETIME_HONOURABLE_KILLS", 0x1304 },

        // Glyph slots & glyphs (6 each)
        { "PLAYER_FIELD_GLYPH_SLOTS_1",   0x131C },
        { "PLAYER_FIELD_GLYPHS_1",        0x1334 },

        // Known titles 3 (arena stuff)
        { "PLAYER_FIELD_KNOWN_TITLES1",    0x134C },
    };

    // =========================================================================
    //  GAMEOBJECT FIELDS (eGameObjectFields) — starts after OBJECT_END = 0x18
    // =========================================================================
    public static readonly Dictionary<string, int> GameObjectFields = new Dictionary<string, int>
    {
        // Object fields (inherited)
        { "OBJECT_FIELD_GUID",          0x00 },
        { "OBJECT_FIELD_TYPE",          0x08 },
        { "OBJECT_FIELD_ENTRY",         0x0C },
        { "OBJECT_FIELD_SCALE_X",       0x10 },

        // GameObject-specific (start at OBJECT_END = 0x18)
        { "OBJECT_FIELD_CREATED_BY",    0x18 },  // uint64
        { "GAMEOBJECT_DISPLAYID",       0x20 },  // uint32
        { "GAMEOBJECT_FLAGS",           0x24 },  // uint32
        { "GAMEOBJECT_PARENTROTATION",  0x28 },  // 4x float (quaternion)
        { "GAMEOBJECT_DYNAMIC",         0x38 },  // uint16 + uint16
        { "GAMEOBJECT_FACTION",         0x3C },  // uint32
        { "GAMEOBJECT_LEVEL",           0x40 },  // uint32
        { "GAMEOBJECT_BYTES_1",         0x44 },  // uint32 (state, type, artkit, animprogress)
    };

    // =========================================================================
    //  DYNAMIC OBJECT FIELDS
    // =========================================================================
    public static readonly Dictionary<string, int> DynamicObjectFields = new Dictionary<string, int>
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

    // =========================================================================
    //  CORPSE FIELDS
    // =========================================================================
    public static readonly Dictionary<string, int> CorpseFields = new Dictionary<string, int>
    {
        { "OBJECT_FIELD_GUID",          0x00 },
        { "OBJECT_FIELD_TYPE",          0x08 },
        { "OBJECT_FIELD_ENTRY",         0x0C },
        { "CORPSE_FIELD_OWNER",         0x18 },
        { "CORPSE_FIELD_PARTY",         0x20 },
        { "CORPSE_FIELD_DISPLAY_ID",    0x28 },
        { "CORPSE_FIELD_ITEM",          0x2C },  // 19 x uint32
        { "CORPSE_FIELD_BYTES_1",       0x78 },
        { "CORPSE_FIELD_BYTES_2",       0x7C },
        { "CORPSE_FIELD_GUILD",         0x80 },
        { "CORPSE_FIELD_FLAGS",         0x84 },
        { "CORPSE_FIELD_DYNAMIC_FLAGS", 0x88 },
    };

    /// <summary>
    /// Populates an OffsetManager with the correct static descriptor offsets for the given type.
    /// This replaces the old dynamic PopulateOffsetList() approach.
    /// </summary>
    /// <param name="manager">The OffsetManager to populate</param>
    /// <param name="descriptorType">One of: "Player", "NPC", "Object", "Item", "Container"</param>
    public static void PopulateOffsetManager(OffsetManager manager, string descriptorType)
    {
        Dictionary<string, int> fields;

        switch (descriptorType)
        {
            case "Player":
                fields = PlayerFields;
                break;
            case "NPC":
                fields = UnitFields;
                break;
            case "Object":
                fields = GameObjectFields;
                break;
            case "Item":
                fields = ItemFields;
                break;
            case "Container":
                fields = ContainerFields;
                break;
            default:
                Logger.LogMessage("WoW335aDescriptors: Unknown descriptor type '" + descriptorType + "', using UnitFields");
                fields = UnitFields;
                break;
        }

        foreach (var kvp in fields)
        {
            manager.AddOffset(kvp.Key, kvp.Value);
        }
    }
}
