# World of Warcraft 3.3.5a (12340) Memory Offsets

This document contains commonly known memory offsets for World of Warcraft client version 3.3.5a (build 12340), the final patch of the Wrath of the Lich King expansion.

## Client Information
- **Version**: 3.3.5a
- **Build**: 12340
- **Release Date**: June 29, 2010
- **Base Address**: 0x00400000 (for WoW.exe)

## Core Static Addresses

### Player & Object Manager
```
PlayerGUID                 = 0x00CD87A8  // Current player GUID
TargetGUID                 = 0x00CD87B8  // Current target GUID
MouseoverGUID              = 0x00CD87C8  // Mouseover GUID
FocusGUID                  = 0x00CD87D8  // Focus target GUID

ObjectManager_Base         = 0x00CE87A0  // Object manager pointer
ObjectManager_ActivePlayer = 0x00CE87B8  // Active player object
ObjectManager_LocalGUID    = 0x00CE87C8  // Local player GUID
```

### Camera & World
```
CameraPointer              = 0x00D09850  // Camera structure pointer
WorldFrame                 = 0x00CE87E0  // World frame pointer
```

### Spell & Cooldown
```
SpellCooldown_Base         = 0x00CF3640  // Spell cooldown array base
```

### Lua & Script
```
LuaDoString                = 0x00819210  // Execute Lua string
LuaGetLocalizedText        = 0x007045F0  // Get localized text
```

### Functions (Code Pointers)
```
ClntObjMgrGetActivePlayer  = 0x00468550  // Get active player object
ClntObjMgrEnumVisibleObjects = 0x00468380  // Enumerate visible objects
ClntObjMgrObjectPtr        = 0x00468360  // Get object by GUID
```

### Casting & Combat
```
CastSpell                  = 0x0080BE50  // Cast spell by ID
CastSpellByName            = 0x0080C1F0  // Cast spell by name
StopCasting                = 0x006E4CF0  // Stop casting
UseItem                    = 0x0075C8E0  // Use item by bag/slot
```

### Movement & CTM (Click-to-Move)
```
CTM_Base                   = 0x00CA11B8  // Click-to-move base
CTM_X                      = 0x00CA11BC  // CTM X coordinate
CTM_Y                      = 0x00CA11C0  // CTM Y coordinate
CTM_Z                      = 0x00CA11C4  // CTM Z coordinate
CTM_Action                 = 0x00CA11C8  // CTM action type (0x4=walk, 0x8=interact)
CTM_Distance               = 0x00CA11CC  // CTM distance
CTM_GUID                   = 0x00CA11D0  // CTM target GUID
```

### UI & Interface
```
ComboPoints                = 0x00C5EC10  // Current combo points
RuneState                  = 0x00C5EC18  // Death Knight rune state
```

## Object Offsets (Add to base object pointer)

### Base Object Fields
```
ObjectType                 = 0x14   // Type (player, unit, item, etc)
GUID                       = 0x30   // Object GUID (8 bytes)
```

### Unit Fields (Descriptors)
```
UnitField_Health           = 0x58   // Current health
UnitField_MaxHealth        = 0x70   // Maximum health
UnitField_Power1           = 0x5C   // Mana (power type 0)
UnitField_MaxPower1        = 0x74   // Maximum mana
UnitField_Power2           = 0x60   // Rage (power type 1)
UnitField_Power3           = 0x64   // Focus (power type 2)
UnitField_Power4           = 0x68   // Energy (power type 3)
UnitField_Power7           = 0x6C   // Runic power (power type 6)

UnitField_Level            = 0x88   // Unit level
UnitField_FactionTemplate  = 0x8C   // Faction template ID
UnitField_Flags            = 0x90   // Unit flags (combat, dead, etc)
UnitField_DynamicFlags     = 0x94   // Dynamic flags (lootable, etc)

UnitField_Target           = 0x40   // Current target GUID (8 bytes)
UnitField_SummonedBy       = 0x48   // Summoner GUID (8 bytes)
UnitField_CreatedBy        = 0x50   // Creator GUID (8 bytes)

UnitField_DisplayID        = 0x98   // Display ID
UnitField_MountDisplayID   = 0x9C   // Mount display ID
UnitField_Bytes0           = 0xA0   // Race, class, gender, power type
UnitField_BaseHealth       = 0xA8   // Base health (before buffs)
UnitField_BaseMana         = 0xAC   // Base mana
```

### Player Fields (Additional offsets for player objects)
```
PlayerField_Combo          = 0x118  // Combo points
PlayerField_XP             = 0x120  // Current XP
PlayerField_NextLevelXP    = 0x124  // XP needed for next level
```

### Object Position (in object structure, not descriptor)
```
Object_X                   = 0x798  // X coordinate
Object_Y                   = 0x79C  // Y coordinate
Object_Z                   = 0x7A0  // Z coordinate
Object_Rotation            = 0x7A4  // Rotation (facing)
Object_Type                = 0x14   // Object type ID
```

### Object Manager Traversal
```
NextObject                 = 0x3C   // Pointer to next object in linked list
FirstObject                = ObjectManager_Base + 0xAC  // First object in list
```

## Buff/Aura Offsets
```
FirstAuraPointer           = 0xBC8  // First aura in aura list
NextAuraPointer            = 0x04   // Next aura in linked list (relative to aura)
AuraCount                  = 0xBD0  // Number of active auras
AuraID                     = 0x00   // Aura spell ID (relative to aura struct)
AuraFlags                  = 0x08   // Aura flags
AuraLevel                  = 0x0C   // Aura caster level
AuraStackCount             = 0x10   // Number of stacks
AuraCreator                = 0x14   // Aura creator GUID
AuraTimeLeft               = 0x20   // Time remaining (milliseconds)
AuraDuration               = 0x24   // Total duration
```

## Item & Inventory
```
BackpackStart              = 0x850  // First backpack slot
BackpackSlots              = 16     // Number of backpack slots
Bag1Slot                   = 0x890  // First bag slot
BagSlots                   = 4      // Number of bag slots
ItemGUID                   = 0x00   // Item GUID (8 bytes)
ItemEntry                  = 0x0C   // Item entry ID
ItemStackCount             = 0x38   // Item stack count
ItemDurability             = 0x40   // Item current durability
ItemMaxDurability          = 0x44   // Item maximum durability
```

## Spell Book
```
SpellBookBase              = 0x00C5D1A8  // Spell book data base
NumSpells                  = 0x00C5D1AC  // Number of known spells
```

## CGPlayer Structure Offsets (from player object base)
```
Player_CorpseX             = 0xD8   // Corpse X position
Player_CorpseY             = 0xDC   // Corpse Y position
Player_CorpseZ             = 0xE0   // Corpse Z position
Player_RunSpeed            = 0x9E0  // Current run speed
Player_SwimSpeed           = 0x9E4  // Current swim speed
Player_FlightSpeed         = 0x9E8  // Current flight speed
Player_IsCasting           = 0xC50  // Casting state flag
Player_ChannelSpell        = 0xC54  // Channel spell ID
Player_CastingSpell        = 0xC58  // Current casting spell ID
Player_CastStart           = 0xC5C  // Cast start time
Player_CastEnd             = 0xC60  // Cast end time
```

## Known Spell IDs (WotLK)
```
// Death Knight
DeathGrip                  = 49576
IcyTouch                   = 49909
Obliterate                 = 49020
BloodStrike                = 49930
PlagueStrike               = 49921
DeathCoil                  = 47541
RuneStrike                 = 56815
IceboundFortitude          = 48792
ArmyOfTheDead              = 42650
DancingRuneWeapon          = 49028

// Druid
Wrath                      = 48461
Moonfire                   = 48463
Starfire                   = 48465
Rejuvenation               = 48441
Regrowth                   = 48443
HealingTouch               = 48378
Tranquility                = 48447
Rebirth                    = 48477
MangleBear                 = 48564
MangleCat                  = 48566
Swipe                      = 48562
FerociousBite              = 48577
Rip                        = 49800
Rake                       = 48574

// Hunter
ArcaneShot                 = 49045
SteadyShot                 = 49052
AimedShot                  = 49050
MultiShot                  = 49048
ExplosiveShot              = 60053
KillShot                   = 61006
SerpentSting               = 49001
HuntersMark                = 53338
AspectOfTheHawk            = 27044
AspectOfTheViper           = 34074
AspectOfTheDragonhawk      = 61847
RapidFire                  = 3045

// Mage
Fireball                   = 42833
Frostbolt                  = 42842
ArcaneBlast                = 42897
ArcaneMissiles             = 42846
Pyroblast                  = 42891
FrostfireBlbolt            = 47610
Blizzard                   = 42940
Flamestrike                = 42926
IceLance                   = 42914
FireBlast                  = 42873
ConeOfCold                 = 42931
IceBarrier                 = 43039
ManaShield                 = 43020
Evocation                  = 12051
IceBlock                   = 45438
Blink                      = 1953

// Paladin
HolyLight                  = 48782
FlashOfLight               = 48785
HolyShock                  = 48825
CrusaderStrike             = 35395
JudgementOfWisdom          = 53408
JudgementOfLight           = 20271
HammerOfWrath              = 48806
Exorcism                   = 48801
Consecration               = 48819
HolyWrath                  = 48817
DivineStorm                = 53385
ShieldOfRighteousness      = 61411
HammerOfTheRighteous       = 53595
AvengersShield             = 48827
DivineShield               = 642
LayOnHands                 = 48788

// Priest
FlashHeal                  = 48071
GreaterHeal                = 48063
Renew                      = 48068
PrayerOfHealing            = 48072
CircleOfHealing            = 48089
PrayerOfMending            = 48113
PowerWordShield            = 48066
Smite                      = 48123
HolyFire                   = 48135
MindBlast                  = 48127
ShadowWordPain             = 48125
ShadowWordDeath            = 48158
MindFlay                   = 48156
Dispersion                 = 47585
VampiricTouch              = 48160
DevouringPlague            = 48300
Penance                    = 53007

// Rogue
Sinister Strike            = 48638
Hemorrhage                 = 48660
Backstab                   = 48657
Eviscerate                 = 48668
Envenom                    = 57993
SliceAndDice               = 6774
Rupture                    = 48672
KidneyShot                 = 8643
CheapShot                  = 1833
Garrote                    = 48676
Ambush                     = 48691
FanOfKnives                = 51723
TricksOfTheTrade           = 57934
Evasion                    = 26669
CloakOfShadows             = 31224
Vanish                     = 26889

// Shaman
LightningBolt              = 49238
ChainLightning             = 49271
LavaBurst                  = 60043
FlameShock                 = 49233
FrostShock                 = 49236
EarthShock                 = 49231
HealingWave                = 49273
LesserHealingWave          = 49276
ChainHeal                  = 55459
Riptide                    = 61301
EarthShield                = 49284
Stormstrike                = 17364
LavaLash                   = 60103
FeralSpirit                = 51533
FireNova                   = 61657
Hex                        = 51514

// Warlock
ShadowBolt                 = 47809
Incinerate                 = 47838
Immolate                   = 47811
Conflagrate                = 17962
ChaosBolt                  = 59172
Corruption                 = 47813
CurseOfAgony               = 47864
CurseOfDoom                = 47867
UnstableAffliction         = 47843
Haunt                      = 59164
SearingPain                = 47815
SoulFire                   = 47825
Seed Of Corruption         = 47836
DrainLife                  = 47857
DrainSoul                  = 47855
DeathCoil                  = 47860
Hellfire                   = 47823

// Warrior
Bloodthirst                = 23881
MortalStrike               = 47486
Whirlwind                  = 1680
Slam                       = 47475
HeroicStrike               = 47450
Execute                    = 47471
Rend                       = 47465
ThunderClap                = 47502
Shockwave                  = 46968
Cleave                     = 47520
Revenge                    = 57823
ShieldSlam                 = 47488
Devastate                  = 47498
ShieldBlock                = 2565
ShieldWall                 = 871
ChargingShout              = 47436
BattleShout                = 47436
DemoralizingShout          = 47437
```

## Combat & Interaction Flags
```
// Unit Flags (bitfield at UnitField_Flags)
FLAG_NOT_SELECTABLE        = 0x02000000
FLAG_SKINNABLE             = 0x04000000
FLAG_MOUNT                 = 0x00200000
FLAG_DEAD                  = 0x00000001
FLAG_LOOTING               = 0x00000400
FLAG_COMBAT                = 0x00080000
FLAG_PET_IN_COMBAT         = 0x00100000
FLAG_SWIMMING              = 0x00200000
FLAG_STUNNED               = 0x00040000

// Dynamic Flags (bitfield at UnitField_DynamicFlags)
DFLAG_LOOTABLE             = 0x00000001
DFLAG_TRACK_UNIT           = 0x00000002
DFLAG_TAPPED               = 0x00000004
DFLAG_TAPPED_BY_PLAYER     = 0x00000008
DFLAG_SPECIALINFO          = 0x00000010
DFLAG_DEAD                 = 0x00000020

// CTM Action Types
CTM_FACE                   = 0x01
CTM_WALK                   = 0x04
CTM_STOP                   = 0x02
CTM_INTERACT               = 0x08
CTM_LOOT                   = 0x09
CTM_FACE_TARGET            = 0x0C
```

## Object Types
```
TYPE_OBJECT                = 0
TYPE_ITEM                  = 1
TYPE_CONTAINER             = 2
TYPE_UNIT                  = 3
TYPE_PLAYER                = 4
TYPE_GAMEOBJECT            = 5
TYPE_DYNAMICOBJECT         = 6
TYPE_CORPSE                = 7
```

## Power Types
```
POWER_MANA                 = 0
POWER_RAGE                 = 1
POWER_FOCUS                = 2
POWER_ENERGY               = 3
POWER_HAPPINESS            = 4
POWER_RUNES                = 5
POWER_RUNIC_POWER          = 6
```

## Notes
- All offsets are for the **32-bit WoW client** (WoW.exe)
- Offsets may vary slightly between different language clients
- Some offsets are static addresses, others are relative to object pointers
- GUID values are 64-bit (8 bytes)
- Coordinates (X, Y, Z) are 32-bit floats
- Health, mana, and other stats use descriptors (mirror system)

## Sources
These offsets are compiled from publicly available information from WoW emulation communities, memory editing forums, and open-source bot projects from the 3.3.5a era (2010-2014).

## Important Notice
This information is provided for educational and preservation purposes only. Using memory manipulation tools with World of Warcraft violates Blizzard Entertainment's Terms of Service and can result in account suspension or permanent ban. This documentation is intended for:
- Historical preservation of game client architecture
- Educational understanding of game memory structures
- Research into anti-cheat detection mechanisms
- Development of private server emulators
