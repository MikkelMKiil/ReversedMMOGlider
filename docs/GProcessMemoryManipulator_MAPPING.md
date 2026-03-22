# GProcessMemoryManipulator Method Mapping

## Overview
This file contains core memory manipulation functions for interacting with the WoW process.
940 lines, ~60+ static methods.

## Method Name Mappings (Critical Methods First)

### Memory Read Operations
- `smethod_9(address, maxLen, context)` → `ReadString(address, maxLen, context)` - Read string (alias for smethod_10)
- `smethod_10(address, maxLen, context)` → `ReadStringInternal(address, maxLen, context)` - Read UTF8 string from memory
- `smethod_11(address, context)` → `ReadInt32(address, context)` - Read 32-bit integer (MOST USED)
- `smethod_12(address, context)` → `ReadInt64(address, context)` - Read 64-bit integer / long
- `smethod_13(address, context)` → `ReadFloat(address, context)` - Read float (32-bit)
- `smethod_14(address, context)` → `ReadDouble(address, context)` - Read double (64-bit)
- `smethod_15(address, context)` → `ReadByte(address, context)` - Read single byte
- `smethod_17(address, size, context)` → `ReadBytes(address, size, context)` - Read byte array
- `smethod_19(address, size, context, allowPartial)` → `ReadBytesInternal(address, size, context, allowPartial)` - Read bytes with partial read support
- `smethod_20(address, size)` → `ReadBytesRaw(address, size)` - Read bytes without context string

### Memory Write Operations
- `smethod_16(address, data, size)` → `WriteBytes(address, data, size)` - Write byte array to memory

### Memory Read Helper Functions (Less Common)
- `smethod_21(address, context)` → `ReadIntFromOffset(address, context)` - Read int from offset table key
- `smethod_22(address, context)` → `ReadFloatFromOffset(address, context)` - Read float from offset table key
- `smethod_23(address, context)` → `ReadFloatAlternate(address, context)` - Read float alternate implementation
- `smethod_24(address, context)` → `ReadLongFromOffset(address, context)` - Read long from offset table key

### Process & Window Management
- `smethod_0()` → `GenerateRandomString()` - Generate random lowercase string (8-18 chars)
- `smethod_1()` → `AttachToWowProcess()` - Find and attach to WoW process
- `smethod_2()` → `GetWindowRectangle()` - Get window rectangle
- `smethod_3()` → `GetWindowHandle()` - Get window handle
- `smethod_4()` → `GetCursorPosition()` - Get cursor position
- `smethod_5(processId)` → `SetProcessId(processId)` - Set target process ID
- `smethod_6(processId)` → `OpenProcessHandle(processId)` - Open process handle
- `smethod_7(handle)` → `CloseProcessHandle(handle)` - Close process handle

### Coordinate/Screen Conversion
- `smethod_25(worldX, worldY, out screenX, out screenY)` → `WorldToScreen(worldX, worldY, out screenX, out screenY)` - Convert world to screen coordinates
- `smethod_26(out worldX, out worldY, screenX, screenY)` → `ScreenToWorld(out worldX, out worldY, screenX, screenY)` - Convert screen to world coordinates

### Process Information
- `smethod_27(processId)` → `GetMainWindowHandle(processId)` - Get main window handle for process
- `smethod_28()` → `IsWowProcessRunning()` - Check if WoW process is running
- `smethod_29(processId)` → `OpenProcessWithAccess(processId)` - Open process with full access rights
- `smethod_31()` → `GetProcessId()` - Get current target process ID
- `smethod_32()` → `GetProcessExecutablePath()` - Get executable path
- `smethod_33()` → `CloseCurrentProcessHandle()` - Close current process handle
- `smethod_34()` → `GetProcessIdFromWindow()` - Get process ID from window handle

### Memory Validation
- `smethod_35(address)` → `IsMemoryReadable(address)` - Check if memory address is readable
- `smethod_36(base, offset1, offset2)` → `ReadPointerChain(base, offset1, offset2)` - Read multi-level pointer

### Window Management (Win32 API Wrappers)
- `smethod_37(hwnd)` → `ShowWindow(hwnd)` - Show window
- `smethod_38(hwnd)` → `SetForegroundWindow(hwnd)` - Bring window to foreground
- `smethod_39(hwnd, out point)` → `GetWindowPosition(hwnd, out point)` - Get window position
- `smethod_40(hwnd, out size)` → `GetWindowSize(hwnd, out size)` - Get window size
- `smethod_41(hwnd, point)` → `SetWindowPosition(hwnd, point)` - Set window position
- `smethod_42(hwnd, size)` → `SetWindowSize(hwnd, size)` - Set window size
- `smethod_43()` → `GetForegroundWindow()` - Get foreground window handle
- `smethod_44(hwnd)` → `IsWindowVisible(hwnd)` - Check if window is visible
- `smethod_45(hwnd)` → `IsWindowMinimized(hwnd)` - Check if window is minimized

### Input Simulation
- `smethod_46(key)` → `SendKeyDown(key)` - Send key down event
- `smethod_47(key)` → `SendKeyUp(key)` - Send key up event
- `smethod_48(key)` → `PressKey(key)` - Press key (down + up)

### Memory Pattern Scanning
- `smethod_49(pattern, mask)` → `FindPattern(pattern, mask)` - Find byte pattern in memory
- `smethod_50(startAddr, endAddr, pattern, mask)` → `FindPatternInRange(startAddr, endAddr, pattern, mask)` - Find pattern in address range

### Other Utilities
- `smethod_8(bytes)` → `BytesToHexString(bytes)` - Convert byte array to hex string
- `smethod_18(address, buffer, size, out bytesRead)` → `ReadProcessMemoryInternal(address, buffer, size, out bytesRead)` - Internal read with driver support

## Usage Priority (for renaming order)

**Critical (Rename First):**
1. `smethod_11` → `ReadInt32` (most frequently used)
2. `smethod_16` → `WriteBytes` (critical for memory writes)
3. `smethod_9/10` → `ReadString` (frequently used)
4. `smethod_17` → `ReadBytes` (frequently used)
5. `smethod_1` → `AttachToWowProcess` (initialization)

**Important (Rename Second):**
6. `smethod_12` → `ReadInt64` (GUID reading)
7. `smethod_13` → `ReadFloat` (coordinates, stats)
8. `smethod_29` → `OpenProcessWithAccess` (process access)
9. `smethod_35` → `IsMemoryReadable` (validation)

**Nice to Have (Rename Last):**
- Window management methods (smethod_37 through smethod_45)
- Input simulation (smethod_46 through smethod_48)
- Utilities and helpers

## Field/Variable Mappings

### Static Fields
- `Offsets` - Already renamed (SortedList<int, string>)
- `intptr_0` → `_currentProcessHandle`
- `int_28` → `_currentProcessId`
- `intptr_1` → `_mainWindowHandle`
- `bool_0` → `UseMemoryDriver`
- `bool_2` → `MemoryReadingEnabled`
- `bool_3` → `LogMemoryOperations`
- `int_27` → `LastErrorCode`

### Constants (uint_X and int_X)
Most of these are Windows API constants. Should be renamed to descriptive names:
- `uint_0 = 2035711` → `PROCESS_ALL_ACCESS`
- Various memory protection flags (uint_1 through uint_22)
- Various other flags (int_2 through int_26)

## P/Invoke Declarations

The file also contains many P/Invoke declarations for Windows API functions:
- `ReadProcessMemory`
- `WriteProcessMemory`
- `OpenProcess`
- `VirtualQueryEx`
- `GetWindowRect`
- `SetForegroundWindow`
- etc.

These are standard Windows APIs and should keep their names.

## Implementation Notes

- Many methods have a `string_0` parameter which is a "context" string for logging/debugging
- The class uses both direct Windows API calls and an optional driver (Shadow.sys)
- Error handling uses `int_27` (LastErrorCode) to store Win32 error codes
- Some methods use the offset table (MemoryOffsetTable.Instance) for lookups
