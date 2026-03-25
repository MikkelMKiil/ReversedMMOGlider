#nullable disable

using System;
using System.Drawing;
using System.Windows.Forms;
using Glider.Common.Objects;

internal static class GameMemoryAccess
{
    internal static bool bool_2
    {
        get { return GProcessMemoryManipulator.bool_2; }
        set { GProcessMemoryManipulator.bool_2 = value; }
    }

    internal static bool bool_3
    {
        get { return GProcessMemoryManipulator.bool_3; }
        set { GProcessMemoryManipulator.bool_3 = value; }
    }

    internal static int int_27
    {
        get { return GProcessMemoryManipulator.int_27; }
    }

        // Used by: GObject.GObject(int BaseAddress, int FrameNumber)
        internal static int ReadObjectStorageAddress(int baseAddress)
        {
            return GProcessMemoryManipulator.ReadInt32(baseAddress + 8, "GameObjStorage");
        }

        // Used by: GObject.GObject(int BaseAddress, int FrameNumber)
        internal static long ReadObjectGuid(int baseAddress)
        {
            return GProcessMemoryManipulator.ReadInt64(baseAddress + 48, "NewObjGUID");
        }

        // Used by: GObject.IsCursorOnObject
        // Used by: GUnit.IsCursorOnUnit
        internal static long ReadUnderCursorGuid()
        {
            return GProcessMemoryManipulator.ReadInt64(MemoryOffsetTable.Instance.GetIntOffset("UnderCursor"), "UnderCursor");
        }

        // Used by: GObject.QuickGetType
        internal static int ReadQuickObjectType(int baseAddress)
        {
            return GProcessMemoryManipulator.ReadInt32(baseAddress + 20, "QuickType");
        }

        // Used by: GObject.Refresh(bool BypassTimer)
        internal static int ReadRefreshStorageAddress(int baseAddress)
        {
            return GProcessMemoryManipulator.ReadInt32(baseAddress + 8, "GameObjStorage.Refresh");
        }

        // Used by: GObject.GetStorageInt
        internal static int ReadStorageInt(int storageAddress, int descriptorOffset, string fieldName)
        {
            return GProcessMemoryManipulator.ReadIntFromOffset(storageAddress + descriptorOffset, "ReadSI." + fieldName);
        }

        // Used by: GObject.GetStorageLong
        internal static long ReadStorageLong(int storageAddress, int descriptorOffset, string fieldName)
        {
            return GProcessMemoryManipulator.ReadLongFromOffset(storageAddress + descriptorOffset, "ReadSL." + fieldName);
        }

        // Used by: GObject.GetStorageFloat
        internal static float ReadStorageFloat(int storageAddress, int descriptorOffset, string fieldName)
        {
            return GProcessMemoryManipulator.ReadFloatFromOffset(storageAddress + descriptorOffset, "ReadSF." + fieldName);
        }

        // Used by: GObject.GetBaseInt
        internal static int ReadBaseInt(int baseAddress, string offsetName)
        {
            return GProcessMemoryManipulator.ReadIntFromOffset(baseAddress + MemoryOffsetTable.Instance.GetIntOffset(offsetName), "ReadBI." + offsetName);
        }

        // Used by: GObject.GetBaseLong
        internal static long ReadBaseLong(int baseAddress, string offsetName)
        {
            return GProcessMemoryManipulator.ReadLongFromOffset(baseAddress + MemoryOffsetTable.Instance.GetIntOffset(offsetName), "ReadBL." + offsetName);
        }

        // Used by: GObject.GetBaseFloat
        internal static float ReadBaseFloat(int baseAddress, string offsetName)
        {
            return GProcessMemoryManipulator.ReadFloatFromOffset(baseAddress + MemoryOffsetTable.Instance.GetIntOffset(offsetName), "ReadBF." + offsetName);
        }

        // Used by: GMemory.WriteBytes
        internal static bool IsMemoryReadable(int startAddress)
        {
            return GProcessMemoryManipulator.IsMemoryReadable(startAddress);
        }

        // Used by: GMemory.WriteBytes
        internal static int ReadPointerChain(int startAddress, int lengthToRead, int maxDepth)
        {
            return GProcessMemoryManipulator.ReadPointerChain(startAddress, lengthToRead, maxDepth);
        }

        // Used by: GMemory.WriteBytes
        internal static int WriteBytes(int startAddress, byte[] dataToWrite, int lengthToWrite)
        {
            return GProcessMemoryManipulator.WriteBytes(startAddress, dataToWrite, lengthToWrite);
        }

        // Used by: GMemory.ReadBytes
        internal static byte[] ReadBytes(int startAddress, int lengthToRead, string debugClue)
        {
            return GProcessMemoryManipulator.ReadBytes(startAddress, lengthToRead, debugClue);
        }

        internal static byte[] ReadBytes(int startAddress, int lengthToRead, string debugClue, bool allowPartialRead)
        {
            return GProcessMemoryManipulator.ReadBytes(startAddress, lengthToRead, debugClue, allowPartialRead);
        }

        internal static byte[] ReadBytesRaw(int startAddress, int lengthToRead)
        {
            return GProcessMemoryManipulator.ReadBytesRaw(startAddress, lengthToRead);
        }

        // Used by: GMemory.ReadByte
        internal static byte ReadByte(int startAddress, string debugClue)
        {
            return GProcessMemoryManipulator.ReadByte(startAddress, debugClue);
        }

        // Used by: GMemory.ReadInt
        internal static int ReadInt32(int startAddress, string debugClue)
        {
            return GProcessMemoryManipulator.ReadInt32(startAddress, debugClue);
        }

        // Used by: GMemory.ReadLong
        internal static long ReadInt64(int startAddress, string debugClue)
        {
            return GProcessMemoryManipulator.ReadInt64(startAddress, debugClue);
        }

        // Used by: GMemory.ReadFloat
        internal static float ReadFloat(int startAddress, string debugClue)
        {
            return GProcessMemoryManipulator.ReadFloat(startAddress, debugClue);
        }

        internal static float ReadFloatAlternate(int startAddress, string debugClue)
        {
            return GProcessMemoryManipulator.ReadFloatAlternate(startAddress, debugClue);
        }

        internal static int ReadIntFromOffset(int startAddress, string debugClue)
        {
            return GProcessMemoryManipulator.ReadIntFromOffset(startAddress, debugClue);
        }

        // Used by: GMemory.ReadString
        internal static string ReadString(int startAddress, int maxLength, string debugClue)
        {
            return GProcessMemoryManipulator.ReadString(startAddress, maxLength, debugClue);
        }

        internal static string ReadStringInternal(int startAddress, int maxLength, string debugClue)
        {
            return GProcessMemoryManipulator.ReadStringInternal(startAddress, maxLength, debugClue);
        }

        // Used by: GUnit.RaidTargetIcon
        internal static long ReadRaidTargetGuid(int raidTargetIconOffset, int index)
        {
            return GProcessMemoryManipulator.ReadInt64(raidTargetIconOffset + index * 8, "rti");
        }

        // Used by: GUnit.LoadFields
        internal static int ReadPlayerCasting()
        {
            return GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("PlayerCasting"), "PlayerCasting");
        }

        // Used by: GUnit.LoadFields
        internal static int ReadPlayerCastingAlt()
        {
            return GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("PlayerCastingAlt"), "PlayerCastingAlt");
        }

        // Used by: GUnit.LoadFields
        internal static int ReadCreatureType(int monsterDefinition)
        {
            return GProcessMemoryManipulator.ReadInt32(monsterDefinition + MemoryOffsetTable.Instance.GetIntOffset("CreatureType"), "rct");
        }

        // Used by: GUnit.LoadFields
        internal static int ReadMovementFlags1(int baseAddress)
        {
            return GProcessMemoryManipulator.ReadInt32(baseAddress + MemoryOffsetTable.Instance.GetIntOffset("MoveFlags"), "movefl");
        }

        // Used by: GUnit.LoadFields
        internal static int ReadMoveStruct2(int baseAddress)
        {
            return GProcessMemoryManipulator.ReadInt32(baseAddress + MemoryOffsetTable.Instance.GetIntOffset("MoveStruct2"), "movest2");
        }

        // Used by: GUnit.LoadFields
        internal static int ReadMovementFlags2(int moveStruct2Address)
        {
            return GProcessMemoryManipulator.ReadInt32(moveStruct2Address + MemoryOffsetTable.Instance.GetIntOffset("MoveFlags2"), "movefl2");
        }

        // Used by: GUnit.GetFactionGroupRow
        internal static int ReadFactionSub()
        {
            return GProcessMemoryManipulator.ReadIntFromOffset(MemoryOffsetTable.Instance.GetIntOffset("FactionSub"), "facsub");
        }

        // Used by: GUnit.GetFactionGroupRow
        internal static int ReadFactionOff1(int checkBaseAddress)
        {
            return GProcessMemoryManipulator.ReadIntFromOffset(checkBaseAddress + MemoryOffsetTable.Instance.GetIntOffset("FactionOff1"), "fac1");
        }

        // Used by: GUnit.GetFactionGroupRow
        internal static int ReadFactionOff2(int factionOff1Address)
        {
            return GProcessMemoryManipulator.ReadIntFromOffset(factionOff1Address + MemoryOffsetTable.Instance.GetIntOffset("FactionOff2"), "fac2");
        }

        // Used by: GUnit.GetFactionGroupRow
        internal static int ReadFactionBase()
        {
            return GProcessMemoryManipulator.ReadIntFromOffset(MemoryOffsetTable.Instance.GetIntOffset("FactionBase"), "fac3");
        }

        // Used by: GUnit.GetFactionGroupRow
        internal static int ReadFactionLookup(int factionBaseAddress, int factionRowDelta)
        {
            return GProcessMemoryManipulator.ReadIntFromOffset(factionBaseAddress + factionRowDelta * 4, "faclookup");
        }

        // Used by: GUnit.GetReaction
        internal static int ReadReactionValue(int address, string debugClue)
        {
            return GProcessMemoryManipulator.ReadInt32(address, debugClue);
        }

        // Used by: GUnit.LoadBuffList
        internal static int ReadNewBuffBaseCount(int baseAddress)
        {
            return GProcessMemoryManipulator.ReadInt32(baseAddress + MemoryOffsetTable.Instance.GetIntOffset("NB_BaseCount"), "ubuffcount");
        }

        // Used by: GUnit.LoadBuffList
        internal static int ReadNewBuffExtCount(int baseAddress)
        {
            return GProcessMemoryManipulator.ReadInt32(baseAddress + MemoryOffsetTable.Instance.GetIntOffset("NB_ExtCount"), "extbuffcount");
        }

        // Used by: GUnit.LoadBuffList
        internal static int ReadNewBuffExtPointer(int baseAddress)
        {
            return GProcessMemoryManipulator.ReadInt32(baseAddress + MemoryOffsetTable.Instance.GetIntOffset("NB_ExtListPtr"), "extbuffptr");
        }

        // Used by: GUnit.LoadBuffList
        internal static int ReadNewBuffSpellId(int buffBaseAddress, int spellIdOffset)
        {
            return GProcessMemoryManipulator.ReadInt32(buffBaseAddress + spellIdOffset, "buffsid");
        }

        // Used by: GUnit.LoadBuffList
        internal static byte ReadNewBuffCharges(int buffBaseAddress, int chargesOffset)
        {
            return GProcessMemoryManipulator.ReadByte(buffBaseAddress + chargesOffset, "buffchgs");
        }

        // Used by: GUnit.LoadBuffList
        internal static byte ReadNewBuffFlags(int buffBaseAddress, int flagsOffset)
        {
            return GProcessMemoryManipulator.ReadByte(buffBaseAddress + flagsOffset, "buffflgs");
        }

        // Used by: GUnit.LoadBuffListOld
        internal static int ReadOldBuffSpellId(int auraBaseAddress, int index)
        {
            return GProcessMemoryManipulator.ReadInt32(auraBaseAddress + index * 4, "BuffSpell" + index);
        }

        internal static string GenerateRandomString()
        {
            return GProcessMemoryManipulator.GenerateRandomString();
        }

        internal static string smethod_0()
        {
            return GProcessMemoryManipulator.smethod_0();
        }

        internal static string smethod_10(int startAddress, int maxLength, string debugClue)
        {
            return GProcessMemoryManipulator.smethod_10(startAddress, maxLength, debugClue);
        }

        internal static int smethod_11(int startAddress, string debugClue)
        {
            return GProcessMemoryManipulator.smethod_11(startAddress, debugClue);
        }

        internal static long smethod_12(int startAddress, string debugClue)
        {
            return GProcessMemoryManipulator.smethod_12(startAddress, debugClue);
        }

        internal static double smethod_13(int startAddress, string debugClue)
        {
            return GProcessMemoryManipulator.ReadDouble(startAddress, debugClue);
        }

        internal static bool IsWowProcessRunning()
        {
            return GProcessMemoryManipulator.IsWowProcessRunning();
        }

        internal static int AttachToWowProcess()
        {
            return GProcessMemoryManipulator.AttachToWowProcess();
        }

        internal static void SetProcessId(int processId)
        {
            GProcessMemoryManipulator.SetProcessId(processId);
        }

        internal static IntPtr OpenProcessHandle(int processId)
        {
            return GProcessMemoryManipulator.OpenProcessHandle(processId);
        }

        internal static IntPtr OpenProcessWithAccess(int processId)
        {
            return GProcessMemoryManipulator.OpenProcessWithAccess(processId);
        }

        internal static void CloseProcessHandle(IntPtr processHandle)
        {
            GProcessMemoryManipulator.CloseProcessHandle(processHandle);
        }

        internal static void CloseCurrentProcessHandle()
        {
            GProcessMemoryManipulator.CloseCurrentProcessHandle();
        }

        internal static IntPtr GetWindowHandle()
        {
            return GProcessMemoryManipulator.GetWindowHandle();
        }

        internal static IntPtr GetMainWindowHandle(int processId)
        {
            return GProcessMemoryManipulator.GetMainWindowHandle(processId);
        }

        internal static GProcessMemoryManipulator.GStruct22 GetCursorPosition()
        {
            return GProcessMemoryManipulator.GetCursorPosition();
        }

        internal static IntPtr GetForegroundWindow()
        {
            return GProcessMemoryManipulator.GetForegroundWindow();
        }

        internal static void GetForegroundWindow(IntPtr windowHandle, Size size, Point point)
        {
            GProcessMemoryManipulator.GetForegroundWindow(windowHandle, size, point);
        }

        internal static int GetCurrentProcessId()
        {
            return GProcessMemoryManipulator.GetCurrentProcessId();
        }

        internal static int GetProcessId()
        {
            return GProcessMemoryManipulator.GetProcessId();
        }

        internal static int GetProcessIdFromWindow()
        {
            return GProcessMemoryManipulator.GetProcessIdFromWindow();
        }

        internal static string GetProcessExecutablePath()
        {
            return GProcessMemoryManipulator.GetProcessExecutablePath();
        }

        internal static void WorldToScreen(double x, double y, out int sx, out int sy)
        {
            GProcessMemoryManipulator.WorldToScreen(x, y, out sx, out sy);
        }

        internal static void ScreenToWorld(out double x, out double y, int sx, int sy)
        {
            GProcessMemoryManipulator.ScreenToWorld(out x, out y, sx, sy);
        }

        internal static void Sleep(uint milliseconds)
        {
            GProcessMemoryManipulator.Sleep(milliseconds);
        }

        internal static bool SetForegroundWindow(IntPtr windowHandle)
        {
            return GProcessMemoryManipulator.SetForegroundWindow(windowHandle);
        }

        internal static void ShowWindow(IntPtr windowHandle)
        {
            GProcessMemoryManipulator.ShowWindow(windowHandle);
        }

        internal static bool GetWindowPosition(IntPtr windowHandle, out Point point)
        {
            return GProcessMemoryManipulator.GetWindowPosition(windowHandle, out point);
        }

        internal static bool GetWindowSize(IntPtr windowHandle, out Size size)
        {
            return GProcessMemoryManipulator.GetWindowSize(windowHandle, out size);
        }

        internal static void SetWindowSize(IntPtr windowHandle, Size size)
        {
            GProcessMemoryManipulator.SetWindowSize(windowHandle, size);
        }

        internal static void IsWindowVisible(Control control, string helpFile, HelpNavigator navigator, object parameter)
        {
            GProcessMemoryManipulator.IsWindowVisible(control, helpFile, navigator, parameter);
        }

        internal static void IsWindowMinimized()
        {
            GProcessMemoryManipulator.IsWindowMinimized();
        }

        internal static byte[] smethod_17(int address, int size, string debugClue)
        {
            return GProcessMemoryManipulator.smethod_17(address, size, debugClue);
        }

        internal static byte[] smethod_20(int address, int size)
        {
            return GProcessMemoryManipulator.smethod_20(address, size);
        }

        internal static void smethod_48(Form form)
        {
            GProcessMemoryManipulator.smethod_48(form);
        }

        internal static void smethod_51(HelpProvider helpProvider)
        {
            GProcessMemoryManipulator.smethod_51(helpProvider);
        }

        internal static bool smethod_52(out long playerGuid, out int mainTable)
        {
            return GProcessMemoryManipulator.smethod_52(out playerGuid, out mainTable);
        }

        internal static void smethod_53()
        {
            GProcessMemoryManipulator.smethod_53();
        }

        internal static void smethod_54()
        {
            GProcessMemoryManipulator.smethod_54();
        }

        internal static void smethod_55(int processId)
        {
            GProcessMemoryManipulator.smethod_55(processId);
        }

        internal static bool smethod_56(int processId)
        {
            return GProcessMemoryManipulator.smethod_56(processId);
        }
    }
