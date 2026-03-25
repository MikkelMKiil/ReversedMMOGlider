# Object List Population Debug Guide

## Overview
Strategic logging has been added to `GObjectList.cs` to efficiently track where enemies are being lost during object enumeration. The logging is **throttled** to avoid spam while providing critical visibility.

## Logging Levels

### 1. **PERIODIC SUMMARY** (Every 5 seconds)
```
[DEBUG_OBJLIST] Total=42, Enemies=5, Players=1, Nodes=3, Other=33
```
**What it shows:**
- Total objects in the current snapshot
- Breakdown by type
- Run this in-game and check if enemy count stays flat or grows

**Action:** If `Enemies=0` consistently, enemies aren't being added at all. If it grows then drops, they're being culled.

---

### 2. **TRAVERSAL STATISTICS** (During object enumeration)
```
[TRACE] Skipped sentinel/invalid nodes: 100
[TRACE] Added monster: 0x123abc (Goblin Warrior)
[TRACE] Monsters: Total=8, Valid=7, Invalid=1
```

**What it shows:**
- How many invalid pointer nodes were skipped (normal behavior)
- Which monsters were added to the snapshot
- Validity status of enumerated monsters

**Action:** 
- High skip count? Memory structure changed
- Monsters added but marked invalid? Check `IsValid` property
- 0 monsters added? Object manager isn't returning enemy data

---

### 3. **CRITICAL ERRORS** (When something breaks)
```
[CRITICAL] GetAll: FirstObjectPointer returned 0
[CRITICAL] Object list traversal aborted: detected cycle in object links at 0x3abc1234
[CRITICAL] TryMaterializeUnitByGuid: Iteration cap hit for GUID=0x123abc456def
[CRITICAL] ResolveUnitByGuid GUID mismatch while materializing...
```

**What it shows:**
- Object manager not initialized
- Circular reference in linked list
- Memory corruption or offset mismatch
- GUID resolution failures

**Action:** These are root causes—screenshot and post to logs

---

## Finding Bugs: Step-by-Step Checklist

### **Scenario 1: No Enemies Detected**
1. Check periodic summary: `[DEBUG_OBJLIST] Enemies=0`?
   - ✓ **Next step:** Go to step 2
   - ✗ **Enemies exist:** Skip to Scenario 2

2. Check traversal output for monsters being added:
   - Look for: `[TRACE] Added monster:`
   - ✓ **Monsters logged:** Enemies are in snapshot but `GetMonsters()` filters them
   - ✗ **No monsters added:** ObjectManager not returning enemies

3. Check memory errors:
   - Look for: `[CRITICAL]` lines
   - ✓ **Found errors:** Memory corruption or offset wrong
   - ✗ **No errors:** Check ObjectManager pointer in memory

---

### **Scenario 2: Enemies Added, Then Disappear**
1. Check periodic summary: Does enemy count go up then down?
   - ✓ **Yes:** Objects are being culled (likely stale frame detection)
   - ✗ **No:** Check GetMonsters() filter

2. Enable frame culling debug by checking object validity:
   - Look for: `[TRACE] Monsters: Valid=X, Invalid=Y`
   - ✓ **Invalid > 0:** Objects marked invalid during refresh
   - ✗ **All valid:** Check culling logic in GetAll()

3. Add breakpoint in object culling section:
   ```csharp
   if (LogObjects)
       Logger.smethod_1("+ Culling old object: " + gobject);
   ```

---

### **Scenario 3: Wrong Enemy Count**
1. Compare periodic summary count vs expected:
   - Example: 2 spawned, 5 logged = +3 false positives
   
2. Check `[TRACE] Added monster:` log:
   - Count logged additions
   - Compare with final count
   - Delta = culled during same frame

3. Check storage address updates:
   - Some creatures update BaseAddress constantly
   - This shouldn't reduce count, but may affect filtering

---

## Output Example: Good Health

```
[DEBUG_OBJLIST] Total=45, Enemies=7, Players=1, Nodes=2, Other=35
[TRACE] Added monster: 0x123abc (Goblin Warrior)
[TRACE] Added monster: 0x456def (Forest Spider)
[TRACE] Monsters: Total=7, Valid=7, Invalid=0
[DEBUG_OBJLIST] Total=46, Enemies=8, Players=1, Nodes=2, Other=35
```

**Interpretation:** 7-8 enemies in memory, all valid, properly added to snapshot ✓

---

## Output Example: Problem Case

```
[CRITICAL] GetAll: FirstObjectPointer returned 0
[CRITICAL] Object list traversal aborted: exceeded safety iteration cap
[TRACE] TryMaterializeUnitByGuid: GUID=0x789ghi not found. Checked=50, ReadFailures=12
[DEBUG_OBJLIST] Total=3, Enemies=0, Players=1, Nodes=2, Other=0
```

**Interpretation:** 
- Traversal failed (memory corruption)
- Cannot find enemies even after materialization attempt
- 12 read failures = offset mismatch ✗

---

## How to Use in Debug Session

### **Enable Detailed Logging**
Edit `GObjectList.cs` near `LogObjectListDebugInfo()`:

```csharp
private const int LogIntervalMs = 5000; // Change to 1000 for more frequent logging
```

Decrease interval (e.g., 1000ms = 1 second) for faster feedback.

### **Disable When Satisfied**
Change condition in `LogObjectListDebugInfo()`:

```csharp
if (currentTick - _lastLoggedTick < LogIntervalMs)
    return; // Add: return; before this line to skip debug logs
```

### **Copy Logs**
Check your output window in Visual Studio:
- **Debug** tab → **Output** pane
- Right-click → **Copy All**
- Paste into text editor for analysis

---

## Key Metrics to Track

| Metric | Healthy Range | Problem |
|---|---|---|
| `Total` objects | 30-100+ | < 10 = object manager broken |
| `Enemies` count | >0, grows over time | = 0 = not added; stays same = culled |
| `Valid` monsters | = total | < total = invalid/stale |
| Read failures | 0-2 | > 5 = memory corruption |
| Iteration count | 10-100 | > 100 = traversal inefficiency |
| Sentinel skips | variable | 0 = none expected, high = structure changed |

---

## Memory Offset Validation

If you see read failures, validate offset is correct:

```csharp
// Current: 0x3C (60 bytes)
GameMemoryAccess.ReadInt32(currentObjectAddress + GameObjNextOffset, "GameObjNext");
```

Cross-check in memory dump tool (CheatEngine) with actual object pointer offsets.

---

## Next Steps

1. Run with debug logging enabled
2. Copy 30-60 seconds of output
3. Check if enemies appear in `[TRACE] Added monster:` lines
4. Compare periodic summaries for trends
5. Report findings with full log excerpt

This will pinpoint: **memory issue, initialization issue, or filter issue** → narrowing down the root cause significantly.
