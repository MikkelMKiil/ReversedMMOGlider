# Experimental Branch Progress Report

## Branch: claude/experiment-compile-offsets-functions

Created: 2026-03-22
Purpose: Create experimental branch for WoW 3.3.5a compatibility with deobfuscation and simplified server

---

## Completed Work

### 1. WoW 3.3.5a Offset Documentation ✓
**File**: `offsets/wotlk_3.3.5a_offsets.md`

Created comprehensive documentation of commonly known memory offsets for WoW 3.3.5a (build 12340):
- Core static addresses (PlayerGUID, TargetGUID, ObjectManager, etc.)
- Object field offsets (health, mana, level, position, etc.)
- Function pointers (CastSpell, LuaDoString, etc.)
- Buff/Aura structures
- CTM (Click-to-Move) offsets
- Complete spell ID lists for all 10 classes
- Combat and interaction flags
- Object types and power types

**Impact**: Provides reference for updating client to use 3.3.5a instead of 3.0.x/3.1.x

---

### 2. Deobfuscation Analysis & Planning ✓
**Files**:
- `docs/DEOBFUSCATION_PLAN.md`
- `docs/GProcessMemoryManipulator_MAPPING.md`

Analyzed entire codebase obfuscation:
- **5,646** occurrences of `method_X` patterns
- **2,117** occurrences of `smethod_X` patterns
- **2,321** occurrences of `gclassX_X` patterns
- **204** C# source files total

Created comprehensive deobfuscation plan with priorities and mappings.

**Impact**: Provides roadmap for systematic deobfuscation work

---

### 3. MemoryOffsetTable Deobfuscation ✓
**File**: `src/Memory/MemoryOffsetTable.cs` + 31 dependent files

**Renamed:**
- `gclass18_0` → `Instance` (singleton)
- `sortedList_0` → `Offsets` (storage)
- `method_0()` → `Clear()`
- `method_1()` → `AddStringOffset()`
- `method_2()` → `AddIntOffset()`
- `method_3()` → `GetStringOffset()`
- `method_4()` → `GetIntOffset()`
- `method_5()` → `HasOffset()`

Added comprehensive XML documentation.

**Files Updated**: 31 files across entire codebase

**Impact**: Core offset management is now fully readable and documented

---

### 4. GProcessMemoryManipulator Deobfuscation ✓
**File**: `src/Memory/GProcessMemoryManipulator.cs` + 68 dependent files

**Renamed 40+ Critical Methods:**

*Memory Read Operations:*
- `smethod_11` → `ReadInt32` (MOST USED)
- `smethod_12` → `ReadInt64`
- `smethod_13` → `ReadFloat`
- `smethod_14` → `ReadDouble`
- `smethod_15` → `ReadByte`
- `smethod_9` → `ReadString`
- `smethod_10` → `ReadStringInternal`
- `smethod_17` → `ReadBytes`
- `smethod_19` → `ReadBytesInternal`
- `smethod_20` → `ReadBytesRaw`

*Memory Write Operations:*
- `smethod_16` → `WriteBytes`

*Process Management:*
- `smethod_1` → `AttachToWowProcess`
- `smethod_29` → `OpenProcessWithAccess`
- `smethod_31` → `GetProcessId`
- `smethod_32` → `GetProcessExecutablePath`
- And 20+ more...

**Files Updated**: 68 files including all game objects, combat system, UI, and utilities

**Impact**: Core memory manipulation is now fully readable. Critical for understanding how the bot interacts with WoW memory.

---

### 5. Simple Non-Encrypted Offset Server ✓
**Files**:
- `server-emulation/simple_server.py`
- `server-emulation/README_SIMPLE.md`

Created new Python-based HTTP server:
- **No encryption** - plain JSON API
- **No authentication** - development/testing friendly
- **Includes default 3.3.5a offsets**
- RESTful endpoints (`/offsets`, `/health`)
- Comprehensive documentation
- Easy to extend and modify

**API Example:**
```bash
curl http://localhost:8080/offsets
```

**Impact**: Provides simple alternative to complex encrypted authentication server. Perfect for local development and testing.

---

## Remaining Work

### Phase 1: Complete Offset System Migration

#### 1. Remove Encryption from Client ⚠️ CRITICAL
**File**: `src/Core/ApplicationInitializer.cs`

**Required Changes:**
- Replace `GDataEncryptionManager` usage with simple HTTP client
- Parse JSON response from simple server
- Remove RSA/AES decryption logic
- Update to use `/offsets` endpoint

**Estimated Effort**: 2-4 hours
**Priority**: HIGH - Required for client to work with simple server

#### 2. Create SimpleHttpClient Class
**File**: `src/Core/SimpleHttpOffsetClient.cs` (new)

**Requirements:**
- HTTP GET request to offset server
- JSON parsing (deserialize to dictionary)
- Error handling
- Timeout support

**Estimated Effort**: 1-2 hours
**Priority**: HIGH

#### 3. Update Offset Values to 3.3.5a
**Files**: Various throughout codebase

**Required Changes:**
- Update hardcoded offsets to 3.3.5a values
- Test with WoW 3.3.5a client
- Validate memory reads work correctly

**Estimated Effort**: 4-8 hours (requires testing with actual WoW client)
**Priority**: MEDIUM - Can use server-provided offsets initially

---

### Phase 2: Compilation & Build System

#### 4. Retarget to Modern .NET Framework ⚠️ CRITICAL
**File**: `Glider_fix-cleaned.csproj`

**Current**: .NET Framework 3.5 (2007)
**Target**: .NET Framework 4.7.2 or 4.8 (2018/2019)

**Required Changes:**
- Update `<TargetFrameworkVersion>` in .csproj
- Test for breaking API changes
- Update any deprecated API usages
- Verify Windows Forms compatibility

**Estimated Effort**: 2-4 hours
**Priority**: HIGH - Required for modern IDE compilation

**Challenges:**
- .NET Framework 3.5 not available on most modern systems
- Need to test for compatibility issues
- Some P/Invoke signatures may need updates

#### 5. Fix Compilation Errors
**Files**: Various

**Known Issues:**
- .NET 3.5 reference assemblies not found
- Possible namespace conflicts after renaming
- Deprecated API usages

**Estimated Effort**: 4-8 hours
**Priority**: HIGH

---

### Phase 3: Extended Deobfuscation (Optional)

#### 6. Deobfuscate Game Object Layer
**Files**: `src/Game/Objects/Glider/Common/Objects/G*.cs` (50+ files)

Still contains many `method_X` patterns in:
- GPlayer
- GMonster
- GUnit
- GObject
- GSpells
- etc.

**Estimated Effort**: 20-40 hours
**Priority**: LOW - Can function with current names

#### 7. Deobfuscate UI Layer
**Files**: `src/UI/Forms/*.cs`, `src/UI/Components/*.cs`

Still contains many `method_X` patterns.

**Estimated Effort**: 10-20 hours
**Priority**: LOW

#### 8. Deobfuscate Automation Layer
**Files**: `src/Automation/*.cs`, `src/Game/Combat/*.cs`

**Estimated Effort**: 10-20 hours
**Priority**: LOW

---

## Architecture Summary

### Current System Flow

```
┌─────────────────────────────────────────────────────────┐
│                   MMOGlider Client                      │
│                                                         │
│  ┌────────────────────────────────────────────────┐   │
│  │  ApplicationInitializer                        │   │
│  │  - Uses GDataEncryptionManager                 │   │
│  │  - Connects to encrypted server                │   │
│  │  - Decrypts offsets                           │   │
│  │  - Populates MemoryOffsetTable                │   │
│  └─────────────┬──────────────────────────────────┘   │
│                │                                        │
│                ▼                                        │
│  ┌────────────────────────────────────────────────┐   │
│  │  MemoryOffsetTable [DEOBFUSCATED ✓]           │   │
│  │  - Stores all memory offsets                   │   │
│  │  - Instance.GetIntOffset()                     │   │
│  │  - Instance.GetStringOffset()                  │   │
│  └─────────────┬──────────────────────────────────┘   │
│                │                                        │
│                ▼                                        │
│  ┌────────────────────────────────────────────────┐   │
│  │  GProcessMemoryManipulator [DEOBFUSCATED ✓]   │   │
│  │  - ReadInt32(), WriteBytes()                   │   │
│  │  - ReadString(), ReadFloat()                   │   │
│  │  - Uses offsets to read WoW memory             │   │
│  └─────────────┬──────────────────────────────────┘   │
│                │                                        │
│                ▼                                        │
│  ┌────────────────────────────────────────────────┐   │
│  │  Game Objects (GPlayer, GUnit, etc.)           │   │
│  │  - Use memory manipulation for game state      │   │
│  └────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────┘
                 │
                 │ (HTTPS + RSA + AES)
                 │
                 ▼
┌─────────────────────────────────────────────────────────┐
│         Original Encrypted Server (defunct)              │
│         - mmoglider.com (offline)                        │
└─────────────────────────────────────────────────────────┘
```

### Proposed System Flow

```
┌─────────────────────────────────────────────────────────┐
│                   MMOGlider Client                      │
│                                                         │
│  ┌────────────────────────────────────────────────┐   │
│  │  ApplicationInitializer [TO BE SIMPLIFIED]     │   │
│  │  - Use simple HTTP GET                         │   │
│  │  - Parse JSON response                         │   │
│  │  - No encryption needed                        │   │
│  │  - Populates MemoryOffsetTable                │   │
│  └─────────────┬──────────────────────────────────┘   │
│                │                                        │
│                ▼                                        │
│  ┌────────────────────────────────────────────────┐   │
│  │  MemoryOffsetTable [DEOBFUSCATED ✓]           │   │
│  │  - Stores all memory offsets                   │   │
│  └─────────────┬──────────────────────────────────┘   │
│                │                                        │
│                ▼                                        │
│  ┌────────────────────────────────────────────────┐   │
│  │  GProcessMemoryManipulator [DEOBFUSCATED ✓]   │   │
│  │  - ReadInt32(), WriteBytes()                   │   │
│  └─────────────┬──────────────────────────────────┘   │
│                │                                        │
│                ▼                                        │
│  ┌────────────────────────────────────────────────┐   │
│  │  Game Objects                                  │   │
│  └────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────┘
                 │
                 │ (Plain HTTP + JSON)
                 │
                 ▼
┌─────────────────────────────────────────────────────────┐
│      Simple Offset Server [CREATED ✓]                   │
│      - Python HTTP server                                │
│      - Serves JSON with offsets                         │
│      - No encryption, no authentication                  │
│      - Easy to modify and extend                        │
└─────────────────────────────────────────────────────────┘
```

---

## Testing Strategy

### Once Client Modifications Complete:

1. **Unit Testing**: Test offset loading from simple server
   ```bash
   # Start server
   python3 server-emulation/simple_server.py

   # Run client
   ./Glider.exe
   ```

2. **Memory Read Testing**: Verify offsets work with WoW 3.3.5a
   - Attach to WoW process
   - Read PlayerGUID
   - Read player position
   - Read target info

3. **Integration Testing**: Test full bot functionality
   - Combat rotation
   - Movement
   - Looting
   - Quest handling

---

## Known Issues & Considerations

### 1. WoW Client Version Compatibility
- **Current**: Targets WoW 3.0.x/3.1.x (early WotLK)
- **Target**: WoW 3.3.5a (final WotLK patch)
- **Issue**: Memory structures changed between versions
- **Solution**: Use 3.3.5a offsets from documentation

### 2. .NET Framework Version
- **Current**: .NET Framework 3.5 (2007)
- **Issue**: Not available on most modern systems
- **Solution**: Retarget to 4.7.2 or 4.8

### 3. Warden Anti-Cheat
- **Issue**: Code contains Warden detection/evasion
- **Status**: Not relevant for private servers
- **Action**: Could be removed/disabled for simplicity

### 4. Remaining Obfuscation
- **Issue**: Still 10,000+ obfuscated names remain
- **Status**: Core functionality deobfuscated
- **Action**: Continue incrementally as needed

---

## Files Modified Summary

### Created Files (6):
1. `offsets/wotlk_3.3.5a_offsets.md` - Offset documentation
2. `docs/DEOBFUSCATION_PLAN.md` - Deobfuscation roadmap
3. `docs/GProcessMemoryManipulator_MAPPING.md` - Method mappings
4. `server-emulation/simple_server.py` - Simple HTTP server
5. `server-emulation/README_SIMPLE.md` - Server documentation
6. `docs/PROGRESS_REPORT.md` - This file

### Modified Files (100+):
- `src/Memory/MemoryOffsetTable.cs` - Fully deobfuscated
- `src/Memory/GProcessMemoryManipulator.cs` - Methods renamed
- 68 files updated for GProcessMemoryManipulator changes
- 31 files updated for MemoryOffsetTable changes

### Total Lines Changed: ~2,500+

---

## Next Steps (Prioritized)

1. **[HIGH]** Modify `ApplicationInitializer.cs` to use simple server
2. **[HIGH]** Create `SimpleHttpOffsetClient.cs` class
3. **[HIGH]** Retarget project to .NET Framework 4.7.2+
4. **[HIGH]** Fix compilation errors
5. **[MEDIUM]** Test with actual WoW 3.3.5a client
6. **[MEDIUM]** Update hardcoded offsets to 3.3.5a
7. **[LOW]** Continue deobfuscation of game objects
8. **[LOW]** Continue deobfuscation of UI layer

---

## Conclusion

Significant progress has been made:
- ✓ **Core memory system deobfuscated** (MemoryOffsetTable, GProcessMemoryManipulator)
- ✓ **3.3.5a offsets documented**
- ✓ **Simple server created**
- ✓ **Development infrastructure in place**

Critical remaining work:
- ⚠️ **Remove encryption from client**
- ⚠️ **Update build system for modern compilation**
- ⚠️ **Test with actual WoW client**

The experimental branch is well-positioned for:
1. Modern IDE compilation (after .NET retargeting)
2. Understanding codebase (core deobfuscation complete)
3. WoW 3.3.5a compatibility (offset documentation ready)
4. Local development (simple server available)

**Estimated Time to Fully Working State**: 15-30 hours additional work

---

## Repository Structure After Changes

```
ReversedMMOGlider/
├── src/
│   ├── Memory/
│   │   ├── MemoryOffsetTable.cs          [✓ DEOBFUSCATED]
│   │   ├── GProcessMemoryManipulator.cs  [✓ DEOBFUSCATED]
│   │   └── OffsetManager.cs              [✓ PARTIALLY DEOBFUSCATED]
│   ├── Core/
│   │   ├── ApplicationInitializer.cs     [⚠️ NEEDS WORK]
│   │   └── StartupClass.cs
│   └── [Other directories...]
├── offsets/
│   └── wotlk_3.3.5a_offsets.md          [✓ NEW]
├── server-emulation/
│   ├── simple_server.py                  [✓ NEW]
│   ├── README_SIMPLE.md                  [✓ NEW]
│   ├── server.py                         [EXISTS]
│   └── README.md                         [EXISTS]
├── docs/
│   ├── DEOBFUSCATION_PLAN.md            [✓ NEW]
│   ├── GProcessMemoryManipulator_MAPPING.md [✓ NEW]
│   ├── PROGRESS_REPORT.md               [✓ NEW - THIS FILE]
│   ├── ANALYSIS.md                      [EXISTS]
│   └── PRESERVATION_SUMMARY.md          [EXISTS]
└── Glider_fix-cleaned.csproj            [⚠️ NEEDS .NET VERSION UPDATE]
```

---

**Last Updated**: 2026-03-22
**Branch**: claude/experiment-compile-offsets-functions
**Commits**: 3 major commits
**Status**: Foundation complete, integration work remaining
