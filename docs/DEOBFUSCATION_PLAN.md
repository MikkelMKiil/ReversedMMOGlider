# Deobfuscation Plan - MMOGlider Codebase

## Overview
The MMOGlider codebase has been decompiled from a .NET binary and contains extensive obfuscation:
- **5,646** occurrences of `method_X` patterns (instance methods)
- **2,117** occurrences of `smethod_X` patterns (static methods)
- **2,321** occurrences of `gclassX_X` patterns (class-level variables/fields)
- **204** C# source files total

## Obfuscation Patterns

### Instance Methods (`method_X`)
- Pattern: `method_0`, `method_1`, ... `method_N`
- These are instance (non-static) methods
- Numbers go up to at least method_53+ based on initial scan
- Multiple classes can have the same `method_X` name (context-dependent)

### Static Methods (`smethod_X`)
- Pattern: `smethod_0`, `smethod_1`, ... `smethod_N`
- These are static methods
- Numbers go up to at least smethod_53+ based on initial scan
- Commonly used for utility functions

### Class Fields/Variables (`gclassX_Y`)
- Pattern: `gclass0_0`, `gclass18_0`, etc.
- Format: `gclass[TypeID]_[InstanceID]`
- Often used for singleton instances
- Examples found:
  - `gclass18_0` (MemoryOffsetTable singleton)
  - `gclass61_0` (ConfigManager singleton)
  - `gclass0_0`, `gclass2_0`, etc. (various types)

## Priority Deobfuscation Order

Based on the problem statement's phased approach:

### Phase 1: Core Infrastructure (Offsets, Server, Compilation)

#### 1.1 Memory & Offset Management (HIGHEST PRIORITY)
These are the foundation for everything else:

**Files to deobfuscate:**
- `src/Memory/GProcessMemoryManipulator.cs` - Core memory read/write functions
- `src/Memory/MemoryOffsetTable.cs` - Offset storage and retrieval
- `src/Memory/OffsetManager.cs` - Dynamic offset discovery
- `src/Memory/ProcessEnumerator.cs` - Process enumeration
- `src/Memory/ProcessInfo.cs` - Process information
- `src/Memory/PEImageParser.cs` - PE parsing
- `src/Memory/DebugPrivilegeElevator.cs` - Privilege management

**Key methods to rename:**
- `smethod_11()` → `ReadInt32()` or `ReadMemoryInt()`
- `smethod_16()` → `WriteBytes()` or `WriteMemory()`
- `smethod_9()` → `ReadString()` or `ReadMemoryString()`
- `method_1()` → `SetStringOffset()` (in MemoryOffsetTable)
- `method_2()` → `SetIntOffset()` (in MemoryOffsetTable)
- `method_3()` → `GetStringOffset()` (in MemoryOffsetTable)
- `method_4()` → `GetIntOffset()` (in MemoryOffsetTable)
- `method_5()` → `HasOffset()` (in MemoryOffsetTable)

**Singleton fields:**
- `gclass18_0` → `Instance` or `Singleton` (in MemoryOffsetTable)

#### 1.2 Authentication & Encryption (SECOND PRIORITY)
Must be modified to remove encryption:

**Files to deobfuscate:**
- `src/Authentication/GDataEncryptionManager.cs` - RSA+AES encryption
- `src/Authentication/SimpleHttpClient.cs` - HTTP client
- `src/Authentication/EncryptedDataTransport.cs` - Data transport
- `src/Core/ApplicationInitializer.cs` - Server auth flow

**Key methods to rename and simplify:**
- `PrepareEncryptionData()` → will be removed (encryption elimination)
- `SendAndReceiveData()` → `SendUnencryptedData()` or similar
- HTTP client methods should have clear REST-like names

#### 1.3 Core Application & Configuration
**Files to deobfuscate:**
- `src/Core/StartupClass.cs` - Main entry point
- `src/Core/ConfigManager.cs` - Configuration management
- `src/Core/ApplicationLogger.cs` - Logging
- `src/Core/GlideMainThread.cs` - Main thread

**Singleton fields:**
- `gclass61_0` → `Instance` (in ConfigManager)

**Key config methods:**
- `method_2()` → `GetString()` (in ConfigManager)
- `method_3()` → `GetInt()` (in ConfigManager)
- `method_4()` → `GetDouble()` (in ConfigManager)
- `method_5()` → `GetBool()` (in ConfigManager)
- `method_0()` → `SetValue()` (in ConfigManager)

### Phase 2: Game Objects & Combat System

#### 2.1 Game Object Core
**Files to deobfuscate:**
- `src/Game/Objects/Glider/Common/Objects/GMemory.cs`
- `src/Game/Objects/Glider/Common/Objects/GPlayer.cs`
- `src/Game/Objects/Glider/Common/Objects/GMonster.cs`
- `src/Game/Objects/Glider/Common/Objects/GItem.cs`
- All other G*.cs object files

#### 2.2 Combat System
**Files to deobfuscate:**
- `src/Game/Combat/CombatController.cs`
- `src/Game/Combat/SpellbookManager.cs`
- `src/Game/Combat/SpellcastingManager.cs`
- All class-specific AI files (DeathknightConfig.cs, etc.)

### Phase 3: UI & Automation

#### 3.1 UI Components
**Files to deobfuscate:**
- All files in `src/UI/Forms/`
- All files in `src/UI/Components/`

#### 3.2 Automation & Profiles
**Files to deobfuscate:**
- `src/Automation/MachGlideRunner.cs`
- `src/Automation/ProfileGroupManager.cs`
- All other automation files

### Phase 4: Security & Utils

#### 4.1 Security (Lower Priority for This Project)
**Files to deobfuscate:**
- Files in `src/Security/` (Warden-related)

#### 4.2 Utilities
**Files to deobfuscate:**
- Files in `src/Utils/`

## Deobfuscation Strategy

### Approach 1: Semi-Automated Pattern-Based Renaming
For each file:
1. Read the file and understand context
2. Analyze method signatures, parameters, return types
3. Analyze method bodies to understand purpose
4. Create a mapping of obfuscated → clear names
5. Use Edit tool to rename methods one by one
6. Test compilation after each file

### Approach 2: Create Reference Documentation First
Before renaming, create a comprehensive mapping document:
1. Analyze all critical files
2. Document what each `method_X` does in that file's context
3. Create a master renaming map
4. Apply renames in dependency order (bottom-up)

### Approach 3: Incremental with Git Commits
1. Deobfuscate one file at a time
2. Commit after each file with descriptive message
3. Ensures we can track changes and revert if needed
4. Allows for iterative testing

## Recommended Approach for This Project

Given the scope (10,000+ obfuscated names) and project goals:

**Phase 1 - Start Small and Critical:**
1. Focus ONLY on Phase 1 files (offsets, server, compilation)
2. Manually deobfuscate the ~10 critical files
3. Get compilation working
4. Get simplified server working
5. Test basic functionality

**Phase 2 - Expand if Needed:**
- Only proceed if Phase 1 is successful and stable
- Deobfuscate game object layer
- Deobfuscate combat system

**Phase 3 - Complete Deobfuscation:**
- Long-term goal
- Could be done iteratively over time
- Lower priority than getting core functionality working

## Naming Conventions

When renaming, follow C# conventions:
- **Methods**: PascalCase, verb-based names
  - `ReadInt32()`, `WriteBytes()`, `GetOffset()`, `SetValue()`
- **Properties**: PascalCase, noun-based names
  - `Instance`, `PlayerBase`, `TargetGUID`
- **Private fields**: camelCase or _camelCase
  - `_memoryOffsets`, `offsetTable`, `_singleton`
- **Static fields**: PascalCase (for public) or s_camelCase (for private)
  - `Instance`, `s_instance`
- **Parameters**: camelCase
  - `offsetName`, `value`, `address`
- **Local variables**: camelCase
  - `result`, `buffer`, `processHandle`

## Documentation Requirements

As we deobfuscate, we should:
1. Add XML documentation comments for public methods
2. Add inline comments for complex logic
3. Update this document with mappings found
4. Store discoveries in repository memory tool for future sessions

## Testing Strategy

After each deobfuscation change:
1. Attempt to build with `msbuild`
2. Fix any compilation errors immediately
3. Document any breaking changes
4. Update dependent files if needed

## Progress Tracking

Track progress in this document by marking completed files:
- [ ] Phase 1.1 - Memory & Offset Management
  - [ ] GProcessMemoryManipulator.cs
  - [ ] MemoryOffsetTable.cs
  - [ ] OffsetManager.cs
  - [ ] ProcessEnumerator.cs
  - [ ] ProcessInfo.cs
  - [ ] PEImageParser.cs
  - [ ] DebugPrivilegeElevator.cs
- [ ] Phase 1.2 - Authentication & Encryption
  - [ ] GDataEncryptionManager.cs
  - [ ] SimpleHttpClient.cs
  - [ ] EncryptedDataTransport.cs
  - [ ] ApplicationInitializer.cs
- [ ] Phase 1.3 - Core Application
  - [ ] StartupClass.cs
  - [ ] ConfigManager.cs
  - [ ] ApplicationLogger.cs
  - [ ] GlideMainThread.cs

## Next Steps

1. Start with `MemoryOffsetTable.cs` - smallest and most critical
2. Move to `GProcessMemoryManipulator.cs` - core memory operations
3. Continue through Phase 1.1 files
4. Test compilation frequently
5. Proceed to Phase 1.2 only after Phase 1.1 compiles successfully

## Notes

- This is a preservation and educational project
- The goal is to make the code understandable and compilable
- Perfect deobfuscation is not required - clarity is the goal
- We should prioritize getting it working over complete deobfuscation
