// WOTLK 3.3.5a (12340) SpellbookEx Offsets
// Centralized offsets for SpellbookEx memory reading.
// Fill remaining 0x0 placeholders with reversed offsets for your client build.

internal static class SpellbookOffsets
{
    // --- Spell List ---
    public const uint MySpells = 0x00B700F0;
    public const uint SpellListBase = 0x24;
    public const uint SpellListRowCount = 0x28;
    public const uint SpellListSub = 0x0;

    // RLE / Arrays
    public const uint SpellNameRLE = 0x0;
    public const uint BuffTypeRLE = 0x0;
    public const uint SpellGroupRLE = 0x0;
    public const uint SpellRankRLE = 0x0;

    // --- Cooldown Tracking ---
    public const uint CooldownStart = 0x29C;
    public const uint CooldownStep = 0x10;

    // CD Struct Fields (Relative to CooldownStep)
    public const uint CD_TicksAtCast = 0x4;
    public const uint CD_DurationGCD = 0x0;
    public const uint CD_SpellID = 0x8;
    public const uint CD_GroupID = 0xC;
    public const uint CD_ActiveDuration = 0x0;
    public const uint CD_DurationSpell = 0x0;
    public const uint CD_DurationGroup = 0x0;
}
