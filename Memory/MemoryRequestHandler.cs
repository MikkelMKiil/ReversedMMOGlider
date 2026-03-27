#nullable disable

using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Glider.Common.Objects
{
    internal static class MemoryRequestHandler
    {
        private static readonly SortedList RuntimeOffsets = new SortedList();
        private static readonly SortedList MissingOffsetsLogged = new SortedList();
        private static readonly SortedList DescriptorOffsetsByType = new SortedList();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint desiredAccess, bool inheritHandle, int processId);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, IntPtr lParam);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int processId);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder className, int maxCount);

        private const uint ProcessAllAccess = 2035711U;

        private static int ToAddress(long address)
        {
            return unchecked((int)address);
        }

        internal static SortedList GetOffsetsView()
        {
            return RuntimeOffsets;
        }

        internal static void ClearOffsets()
        {
            RuntimeOffsets.Clear();
            MissingOffsetsLogged.Clear();
        }

        internal static void SetStringOffset(string offsetName, string offsetValue)
        {
            if (RuntimeOffsets.ContainsKey(offsetName))
                RuntimeOffsets[offsetName] = offsetValue;
            else
                RuntimeOffsets.Add(offsetName, offsetValue);
        }

        internal static void SetIntOffset(string offsetName, int offsetAddress)
        {
            if (RuntimeOffsets.ContainsKey(offsetName))
                RuntimeOffsets[offsetName] = offsetAddress;
            else
                RuntimeOffsets.Add(offsetName, offsetAddress);
        }

        internal static string GetStringOffset(string offsetName)
        {
            if (RuntimeOffsets.ContainsKey(offsetName))
                return (string)RuntimeOffsets[offsetName];

            LogMissingOffsetOnce(offsetName);
            return string.Empty;
        }

        internal static int GetIntOffset(string offsetName)
        {
            if (RuntimeOffsets.ContainsKey(offsetName))
                return (int)RuntimeOffsets[offsetName];

            LogMissingOffsetOnce(offsetName);
            return 0;
        }

        internal static bool HasOffset(string offsetName)
        {
            return RuntimeOffsets.ContainsKey(offsetName);
        }

        internal static void ClearDescriptorOffsets(string descriptorName)
        {
            if (DescriptorOffsetsByType.ContainsKey(descriptorName))
                DescriptorOffsetsByType.Remove(descriptorName);

            DescriptorOffsetsByType.Add(descriptorName, new SortedList());
        }

        internal static void AddDescriptorOffset(string descriptorName, string key, int byteOffset)
        {
            if (!DescriptorOffsetsByType.ContainsKey(descriptorName))
                DescriptorOffsetsByType.Add(descriptorName, new SortedList());

            var descriptorOffsets = (SortedList)DescriptorOffsetsByType[descriptorName];
            if (!descriptorOffsets.ContainsKey(key))
                descriptorOffsets.Add(key, byteOffset);
        }

        internal static int GetDescriptorOffset(string descriptorName, string descriptorKey)
        {
            if (!DescriptorOffsetsByType.ContainsKey(descriptorName))
                return 0;

            var descriptorOffsets = (SortedList)DescriptorOffsetsByType[descriptorName];
            return descriptorOffsets.ContainsKey(descriptorKey) ? (int)descriptorOffsets[descriptorKey] : 0;
        }

        internal static int GetDescriptorOffsetCount(string descriptorName)
        {
            if (!DescriptorOffsetsByType.ContainsKey(descriptorName))
                return 0;

            return ((SortedList)DescriptorOffsetsByType[descriptorName]).Count;
        }

        private static void LogMissingOffsetOnce(string offsetName)
        {
            if (MissingOffsetsLogged.ContainsKey(offsetName))
                return;

            MissingOffsetsLogged.Add(offsetName, true);
            Logger.LogMessage(MessageProvider.smethod_2(314, offsetName));
        }

        private static string GetWindowTextSafe(IntPtr hWnd)
        {
            var builder = new StringBuilder(256);
            return GetWindowText(hWnd, builder, builder.Capacity) > 0 ? builder.ToString() : string.Empty;
        }

        private static string GetClassNameSafe(IntPtr hWnd)
        {
            var builder = new StringBuilder(256);
            return GetClassName(hWnd, builder, builder.Capacity) > 0 ? builder.ToString() : string.Empty;
        }

        private static IntPtr FindBestTopLevelWindowForProcess(int processId)
        {
            IntPtr best = IntPtr.Zero;
            var bestTitleLength = -1;

            EnumWindows((hWnd, lParam) =>
            {
                int pid;
                GetWindowThreadProcessId(hWnd, out pid);
                if (pid != processId || !IsWindowVisible(hWnd))
                    return true;

                var title = GetWindowTextSafe(hWnd);
                if (string.IsNullOrEmpty(title))
                    return true;

                if (title.Length > bestTitleLength)
                {
                    best = hWnd;
                    bestTitleLength = title.Length;
                }

                return true;
            }, IntPtr.Zero);

            return best;
        }

        internal static IntPtr GetWoWHandle()
        {
            var processes = Process.GetProcessesByName("WoW");
            if (processes.Length == 0)
                return IntPtr.Zero;

            var process = processes[0];

            try
            {
                var windowHandle = process.MainWindowHandle;
                if (windowHandle == IntPtr.Zero)
                    windowHandle = FindBestTopLevelWindowForProcess(process.Id);

                if (windowHandle != IntPtr.Zero)
                {
                    StartupClass.MainApplicationHandle = windowHandle;
                    Logger.LogMessage("[Input] WoW HWND selected: 0x" + windowHandle.ToInt64().ToString("x") +
                                      ", Title=\"" + GetWindowTextSafe(windowHandle) + "\", Class=\"" + GetClassNameSafe(windowHandle) + "\"");
                }
                else
                {
                    Logger.LogMessage("[Critical] WoW window handle (HWND) could not be resolved. Input to game window will fail (PostMessage requires HWND). Ensure WoW is running with a visible window.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage("[Critical] Exception resolving WoW window handle (HWND): " + ex.Message);
            }

            return OpenProcess(ProcessAllAccess, false, process.Id);
        }

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

        internal static uint ReadObjectStorageAddress(uint baseAddress)
        {
            if (baseAddress == 0)
                return 0;

            return GProcessMemoryManipulator.ReadUInt32(baseAddress + GameMemoryConstants.Wotlk.ObjStoragePointer, "GameObjStorage");
        }

        internal static ulong ReadObjectGuid(int baseAddress)
        {
            var storageAddress = ReadObjectStorageAddress((uint)baseAddress);
            if (storageAddress == 0)
                return 0UL;

            return ReadStorageULong(storageAddress, 0, "OBJECT_FIELD_GUID");
        }

        internal static ulong ReadObjectGuid(uint baseAddress)
        {
            var storageAddress = ReadObjectStorageAddress(baseAddress);
            if (storageAddress == 0)
                return 0UL;

            return ReadStorageULong(storageAddress, 0, "OBJECT_FIELD_GUID");
        }

        internal static ulong ReadUnderCursorGuid()
        {
            return unchecked((ulong)GProcessMemoryManipulator.ReadInt64(MemoryOffsetTable.Instance.GetIntOffset("UnderCursor"), "UnderCursor"));
        }

        internal static int ReadQuickObjectType(int baseAddress)
        {
            return GProcessMemoryManipulator.ReadInt32(baseAddress + (int)GameMemoryConstants.Wotlk.ObjType, "QuickType");
        }

        internal static int ReadQuickObjectType(uint baseAddress)
        {
            return ReadQuickObjectType(ToAddress(baseAddress));
        }

        internal static int ReadRefreshStorageAddress(int baseAddress)
        {
            return GProcessMemoryManipulator.ReadInt32(baseAddress + (int)GameMemoryConstants.Wotlk.ObjStoragePointer, "GameObjStorage.Refresh");
        }

        internal static uint ReadRefreshStorageAddress(uint baseAddress)
        {
            return unchecked((uint)ReadRefreshStorageAddress(ToAddress(baseAddress)));
        }

        internal static int ReadStorageInt(int storageAddress, int descriptorOffset, string fieldName)
        {
            return GProcessMemoryManipulator.ReadIntFromOffset(storageAddress + descriptorOffset, "ReadSI." + fieldName);
        }

        internal static int ReadStorageInt(uint storageAddress, int descriptorOffset, string fieldName)
        {
            return ReadStorageInt(ToAddress(storageAddress), descriptorOffset, fieldName);
        }

        internal static ulong ReadStorageULong(int storageAddress, int descriptorOffset, string fieldName)
        {
            return unchecked((ulong)GProcessMemoryManipulator.ReadULongFromOffset(storageAddress + descriptorOffset, "ReadSL." + fieldName));
        }

        internal static ulong ReadStorageULong(uint storageAddress, int descriptorOffset, string fieldName)
        {
            return ReadStorageULong(ToAddress(storageAddress), descriptorOffset, fieldName);
        }

        internal static float ReadStorageFloat(int storageAddress, int descriptorOffset, string fieldName)
        {
            return GProcessMemoryManipulator.ReadFloatFromOffset(storageAddress + descriptorOffset, "ReadSF." + fieldName);
        }

        internal static float ReadStorageFloat(uint storageAddress, int descriptorOffset, string fieldName)
        {
            return ReadStorageFloat(ToAddress(storageAddress), descriptorOffset, fieldName);
        }

        internal static int ReadBaseInt(int baseAddress, string offsetName)
        {
            return GProcessMemoryManipulator.ReadIntFromOffset(baseAddress + MemoryOffsetTable.Instance.GetIntOffset(offsetName), "ReadBI." + offsetName);
        }

        internal static int ReadBaseInt(uint baseAddress, string offsetName)
        {
            return ReadBaseInt(ToAddress(baseAddress), offsetName);
        }

        internal static ulong ReadBaseLong(int baseAddress, string offsetName)
        {
            return unchecked((ulong)GProcessMemoryManipulator.ReadULongFromOffset(baseAddress + MemoryOffsetTable.Instance.GetIntOffset(offsetName), "ReadBL." + offsetName));
        }

        internal static ulong ReadBaseLong(uint baseAddress, string offsetName)
        {
            return ReadBaseLong(ToAddress(baseAddress), offsetName);
        }

        internal static float ReadBaseFloat(int baseAddress, string offsetName)
        {
            return GProcessMemoryManipulator.ReadFloatFromOffset(baseAddress + MemoryOffsetTable.Instance.GetIntOffset(offsetName), "ReadBF." + offsetName);
        }

        internal static float ReadBaseFloat(uint baseAddress, string offsetName)
        {
            return ReadBaseFloat(ToAddress(baseAddress), offsetName);
        }

        internal static bool IsMemoryReadable(int startAddress) => GProcessMemoryManipulator.IsMemoryReadable(startAddress);
        internal static int ReadPointerChain(int startAddress, int lengthToRead, int maxDepth) => GProcessMemoryManipulator.ReadPointerChain(startAddress, lengthToRead, maxDepth);
        internal static int WriteBytes(int startAddress, byte[] dataToWrite, int lengthToWrite) => GProcessMemoryManipulator.WriteBytes(startAddress, dataToWrite, lengthToWrite);
        internal static byte[] ReadBytes(int startAddress, int lengthToRead, string debugClue) => GProcessMemoryManipulator.ReadBytes(startAddress, lengthToRead, debugClue);
        internal static byte[] ReadBytes(int startAddress, int lengthToRead, string debugClue, bool allowPartialRead) => GProcessMemoryManipulator.ReadBytes(startAddress, lengthToRead, debugClue, allowPartialRead);
        internal static byte[] ReadBytesRaw(int startAddress, int lengthToRead) => GProcessMemoryManipulator.ReadBytesRaw(startAddress, lengthToRead);
        internal static byte ReadByte(int startAddress, string debugClue) => GProcessMemoryManipulator.ReadByte(startAddress, debugClue);
        internal static int ReadInt32(int startAddress, string debugClue) => GProcessMemoryManipulator.ReadInt32(startAddress, debugClue);
        internal static int ReadInt32(uint startAddress, string debugClue) => GProcessMemoryManipulator.ReadInt32(ToAddress(startAddress), debugClue);
        internal static int ReadInt32(long startAddress, string debugClue) => GProcessMemoryManipulator.ReadInt32(ToAddress(startAddress), debugClue);
        internal static uint ReadUInt32(int startAddress, string debugClue) => GProcessMemoryManipulator.ReadUInt32(startAddress, debugClue);
        internal static uint ReadUInt32(uint startAddress, string debugClue) => GProcessMemoryManipulator.ReadUInt32(startAddress, debugClue);
        internal static uint ReadUInt32(long startAddress, string debugClue) => ReadUInt32(ToAddress(startAddress), debugClue);
        internal static ulong ReadInt64(int startAddress, string debugClue) => unchecked((ulong)GProcessMemoryManipulator.ReadInt64(startAddress, debugClue));
        internal static ulong ReadInt64(uint startAddress, string debugClue) => unchecked((ulong)GProcessMemoryManipulator.ReadInt64(startAddress, debugClue));
        internal static ulong ReadInt64(long startAddress, string debugClue) => unchecked((ulong)GProcessMemoryManipulator.ReadInt64(ToAddress(startAddress), debugClue));
        internal static double ReadDouble(int startAddress, string debugClue) => GProcessMemoryManipulator.ReadDouble(startAddress, debugClue);
        internal static float ReadFloat(int startAddress, string debugClue) => GProcessMemoryManipulator.ReadFloat(startAddress, debugClue);
        internal static float ReadFloatAlternate(int startAddress, string debugClue) => GProcessMemoryManipulator.ReadFloatAlternate(startAddress, debugClue);
        internal static int ReadIntFromOffset(int startAddress, string debugClue) => GProcessMemoryManipulator.ReadIntFromOffset(startAddress, debugClue);
        internal static int ReadIntFromOffset(uint startAddress, string debugClue) => GProcessMemoryManipulator.ReadIntFromOffset(ToAddress(startAddress), debugClue);
        internal static int ReadIntFromOffset(long startAddress, string debugClue) => GProcessMemoryManipulator.ReadIntFromOffset(ToAddress(startAddress), debugClue);
        internal static string ReadString(int startAddress, int maxLength, string debugClue) => GProcessMemoryManipulator.ReadString(startAddress, maxLength, debugClue);
        internal static string ReadStringInternal(int startAddress, int maxLength, string debugClue) => GProcessMemoryManipulator.ReadStringInternal(startAddress, maxLength, debugClue);

        internal static ulong ReadRaidTargetGuid(int raidTargetIconOffset, int index) => unchecked((ulong)GProcessMemoryManipulator.ReadInt64(raidTargetIconOffset + index * 8, "rti"));
        internal static int ReadPlayerCasting() => GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("PlayerCasting"), "PlayerCasting");
        internal static int ReadPlayerCastingAlt() => GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("PlayerCastingAlt"), "PlayerCastingAlt");
        internal static int ReadCreatureType(int monsterDefinition) => GProcessMemoryManipulator.ReadInt32(monsterDefinition + MemoryOffsetTable.Instance.GetIntOffset("CreatureType"), "rct");
        internal static int ReadMovementFlags1(int baseAddress) => GProcessMemoryManipulator.ReadInt32(baseAddress + MemoryOffsetTable.Instance.GetIntOffset("MoveFlags"), "movefl");
        internal static int ReadMovementFlags1(uint baseAddress) => ReadMovementFlags1(ToAddress(baseAddress));
        internal static int ReadMoveStruct2(int baseAddress) => GProcessMemoryManipulator.ReadInt32(baseAddress + MemoryOffsetTable.Instance.GetIntOffset("MoveStruct2"), "movest2");
        internal static int ReadMoveStruct2(uint baseAddress) => ReadMoveStruct2(ToAddress(baseAddress));
        internal static int ReadMovementFlags2(int moveStruct2Address) => GProcessMemoryManipulator.ReadInt32(moveStruct2Address + MemoryOffsetTable.Instance.GetIntOffset("MoveFlags2"), "movefl2");
        internal static int ReadFactionSub() => GProcessMemoryManipulator.ReadIntFromOffset(MemoryOffsetTable.Instance.GetIntOffset("FactionSub"), "facsub");
        internal static int ReadFactionOff1(int checkBaseAddress) => GProcessMemoryManipulator.ReadIntFromOffset(checkBaseAddress + MemoryOffsetTable.Instance.GetIntOffset("FactionOff1"), "fac1");
        internal static int ReadFactionOff1(uint checkBaseAddress) => ReadFactionOff1(ToAddress(checkBaseAddress));
        internal static int ReadFactionOff2(int factionOff1Address) => GProcessMemoryManipulator.ReadIntFromOffset(factionOff1Address + MemoryOffsetTable.Instance.GetIntOffset("FactionOff2"), "fac2");
        internal static int ReadFactionBase() => GProcessMemoryManipulator.ReadIntFromOffset(MemoryOffsetTable.Instance.GetIntOffset("FactionBase"), "fac3");
        internal static int ReadFactionLookup(int factionBaseAddress, int factionRowDelta) => GProcessMemoryManipulator.ReadIntFromOffset(factionBaseAddress + factionRowDelta * 4, "faclookup");
        internal static int ReadReactionValue(int address, string debugClue) => GProcessMemoryManipulator.ReadInt32(address, debugClue);
        internal static int ReadNewBuffBaseCount(int baseAddress) => GProcessMemoryManipulator.ReadInt32(baseAddress + MemoryOffsetTable.Instance.GetIntOffset("NB_BaseCount"), "ubuffcount");
        internal static int ReadNewBuffBaseCount(uint baseAddress) => ReadNewBuffBaseCount(ToAddress(baseAddress));
        internal static int ReadNewBuffExtCount(int baseAddress) => GProcessMemoryManipulator.ReadInt32(baseAddress + MemoryOffsetTable.Instance.GetIntOffset("NB_ExtCount"), "extbuffcount");
        internal static int ReadNewBuffExtCount(uint baseAddress) => ReadNewBuffExtCount(ToAddress(baseAddress));
        internal static int ReadNewBuffExtPointer(int baseAddress) => GProcessMemoryManipulator.ReadInt32(baseAddress + MemoryOffsetTable.Instance.GetIntOffset("NB_ExtListPtr"), "extbuffptr");
        internal static int ReadNewBuffExtPointer(uint baseAddress) => ReadNewBuffExtPointer(ToAddress(baseAddress));
        internal static int ReadNewBuffSpellId(int buffBaseAddress, int spellIdOffset) => GProcessMemoryManipulator.ReadInt32(buffBaseAddress + spellIdOffset, "buffsid");
        internal static byte ReadNewBuffCharges(int buffBaseAddress, int chargesOffset) => GProcessMemoryManipulator.ReadByte(buffBaseAddress + chargesOffset, "buffchgs");
        internal static byte ReadNewBuffFlags(int buffBaseAddress, int flagsOffset) => GProcessMemoryManipulator.ReadByte(buffBaseAddress + flagsOffset, "buffflgs");
        internal static int ReadOldBuffSpellId(int auraBaseAddress, int index) => GProcessMemoryManipulator.ReadInt32(auraBaseAddress + index * 4, "BuffSpell" + index);
        internal static int ReadOldBuffSpellId(long auraBaseAddress, int index) => ReadOldBuffSpellId(ToAddress(auraBaseAddress), index);

        internal static string GenerateRandomString() => GProcessMemoryManipulator.GenerateRandomString();
        internal static string smethod_0() => GProcessMemoryManipulator.smethod_0();
        internal static string smethod_10(int startAddress, int maxLength, string debugClue) => GProcessMemoryManipulator.smethod_10(startAddress, maxLength, debugClue);
        internal static int smethod_11(int startAddress, string debugClue) => GProcessMemoryManipulator.smethod_11(startAddress, debugClue);
        internal static long smethod_12(int startAddress, string debugClue) => GProcessMemoryManipulator.smethod_12(startAddress, debugClue);
        internal static double smethod_13(int startAddress, string debugClue) => GProcessMemoryManipulator.ReadDouble(startAddress, debugClue);
        internal static bool IsWowProcessRunning() => GProcessMemoryManipulator.IsWowProcessRunning();
        internal static int AttachToWowProcess() => GProcessMemoryManipulator.AttachToWowProcess();
        internal static void SetProcessId(int processId) => GProcessMemoryManipulator.SetProcessId(processId);
        internal static IntPtr OpenProcessHandle(int processId) => GProcessMemoryManipulator.OpenProcessHandle(processId);
        internal static IntPtr OpenProcessWithAccess(int processId) => GProcessMemoryManipulator.OpenProcessWithAccess(processId);
        internal static void CloseProcessHandle(IntPtr processHandle) => GProcessMemoryManipulator.CloseProcessHandle(processHandle);
        internal static void CloseCurrentProcessHandle() => GProcessMemoryManipulator.CloseCurrentProcessHandle();
        internal static IntPtr GetWindowHandle() => GProcessMemoryManipulator.GetWindowHandle();
        internal static IntPtr GetMainWindowHandle(int processId) => GProcessMemoryManipulator.GetMainWindowHandle(processId);
        internal static GProcessMemoryManipulator.GStruct22 GetCursorPosition() => GProcessMemoryManipulator.GetCursorPosition();
        internal static IntPtr GetForegroundWindow() => GProcessMemoryManipulator.GetForegroundWindow();
        internal static void GetForegroundWindow(IntPtr windowHandle, Size size, Point point) => GProcessMemoryManipulator.GetForegroundWindow(windowHandle, size, point);
        internal static int GetCurrentProcessId() => GProcessMemoryManipulator.GetCurrentProcessId();
        internal static int GetProcessId() => GProcessMemoryManipulator.GetProcessId();
        internal static int GetProcessIdFromWindow() => GProcessMemoryManipulator.GetProcessIdFromWindow();
        internal static string GetProcessExecutablePath() => GProcessMemoryManipulator.GetProcessExecutablePath();
        internal static void WorldToScreen(double x, double y, out int sx, out int sy) => GProcessMemoryManipulator.WorldToScreen(x, y, out sx, out sy);
        internal static void ScreenToWorld(out double x, out double y, int sx, int sy) => GProcessMemoryManipulator.ScreenToWorld(out x, out y, sx, sy);
        internal static void Sleep(uint milliseconds) => GProcessMemoryManipulator.Sleep(milliseconds);
        internal static bool SetForegroundWindow(IntPtr windowHandle) => GProcessMemoryManipulator.SetForegroundWindow(windowHandle);
        internal static void ShowWindow(IntPtr windowHandle) => GProcessMemoryManipulator.ShowWindow(windowHandle);
        internal static bool GetWindowPosition(IntPtr windowHandle, out Point point) => GProcessMemoryManipulator.GetWindowPosition(windowHandle, out point);
        internal static bool GetWindowSize(IntPtr windowHandle, out Size size) => GProcessMemoryManipulator.GetWindowSize(windowHandle, out size);
        internal static void SetWindowSize(IntPtr windowHandle, Size size) => GProcessMemoryManipulator.SetWindowSize(windowHandle, size);
        internal static void IsWindowVisible(Control control, string helpFile, HelpNavigator navigator, object parameter) => GProcessMemoryManipulator.IsWindowVisible(control, helpFile, navigator, parameter);
        internal static bool IsWindowMinimized() => GProcessMemoryManipulator.IsWindowMinimized();
        internal static byte[] smethod_17(int address, int size, string debugClue) => GProcessMemoryManipulator.smethod_17(address, size, debugClue);
        internal static byte[] smethod_20(int address, int size) => GProcessMemoryManipulator.smethod_20(address, size);
        internal static void smethod_48(Form form) => GProcessMemoryManipulator.smethod_48(form);
        internal static void smethod_51(HelpProvider helpProvider) => GProcessMemoryManipulator.smethod_51(helpProvider);
        internal static bool smethod_52(out long playerGuid, out int mainTable) => GProcessMemoryManipulator.smethod_52(out playerGuid, out mainTable);
        internal static bool smethod_52(out ulong playerGuid, out int mainTable)
        {
            long signedPlayerGuid;
            var result = GProcessMemoryManipulator.smethod_52(out signedPlayerGuid, out mainTable);
            playerGuid = unchecked((ulong)signedPlayerGuid);
            return result;
        }
        internal static void smethod_53() => GProcessMemoryManipulator.smethod_53();
        internal static void smethod_54() => GProcessMemoryManipulator.smethod_54();
        internal static void smethod_55(int processId) => GProcessMemoryManipulator.smethod_55(processId);
        internal static bool smethod_56(int processId) => GProcessMemoryManipulator.smethod_56(processId);
    }
}

public class MemoryOffsetTable
{
    public static MemoryOffsetTable Instance;

    public System.Collections.SortedList Offsets;

    public MemoryOffsetTable()
    {
        Instance = this;
        Offsets = Glider.Common.Objects.MemoryRequestHandler.GetOffsetsView();
    }

    public void Clear()
    {
        Glider.Common.Objects.MemoryRequestHandler.ClearOffsets();
    }

    public void AddStringOffset(string offsetName, string offsetValue)
    {
        Glider.Common.Objects.MemoryRequestHandler.SetStringOffset(offsetName, offsetValue);
    }

    public void AddIntOffset(string offsetName, int offsetAddress)
    {
        Glider.Common.Objects.MemoryRequestHandler.SetIntOffset(offsetName, offsetAddress);
    }

    public string GetStringOffset(string offsetName)
    {
        return Glider.Common.Objects.MemoryRequestHandler.GetStringOffset(offsetName);
    }

    public int GetIntOffset(string offsetName)
    {
        return Glider.Common.Objects.MemoryRequestHandler.GetIntOffset(offsetName);
    }

    public bool HasOffset(string offsetName)
    {
        return Glider.Common.Objects.MemoryRequestHandler.HasOffset(offsetName);
    }
}

public class OffsetManager
{
    private readonly string descriptorName;

    public OffsetManager(string descriptorName, int initialOffset)
    {
        this.descriptorName = descriptorName;
        Glider.Common.Objects.MemoryRequestHandler.ClearDescriptorOffsets(descriptorName);
    }

    public void PopulateOffsetList()
    {
        Glider.Common.Objects.MemoryRequestHandler.ClearDescriptorOffsets(descriptorName);
        Glider.Common.Objects.GameMemoryConstants.PopulateOffsetManager(this, descriptorName);
        Logger.smethod_1("OffsetManager: Loaded " + Glider.Common.Objects.MemoryRequestHandler.GetDescriptorOffsetCount(descriptorName) + " static descriptors for '" + descriptorName + "'");
    }

    public void AddOffset(string key, int byteOffset)
    {
        Glider.Common.Objects.MemoryRequestHandler.AddDescriptorOffset(descriptorName, key, byteOffset);
    }

    public int GetOffsetValue(string descriptorKey)
    {
        return Glider.Common.Objects.MemoryRequestHandler.GetDescriptorOffset(descriptorName, descriptorKey);
    }
}
namespace Glider.Common.Objects
{
    internal static class GProcessMemoryManipulator
    {
        private const int ERROR_PARTIAL_COPY = 299;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        private static extern IntPtr GetForegroundWindowNative();

        // ADDED: Replaces GetWindowRect to ignore Windows 11 invisible borders
        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        // ADDED: Translates the Client area to Screen coordinates for InputController compatibility
        [DllImport("user32.dll")]
        private static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        private static extern bool SetForegroundWindowNative(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        private static extern bool ShowWindowNative(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("kernel32.dll", EntryPoint = "Sleep")]
        private static extern void SleepNative(uint dwMilliseconds);



        // ADDED: For checking minimized state
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsIconic(IntPtr hWnd);

        private const uint PROCESS_VM_READ = 0x0010;
        private const uint PROCESS_VM_WRITE = 0x0020;
        private const uint PROCESS_VM_OPERATION = 0x0008;
        private const uint PROCESS_QUERY_INFORMATION = 0x0400;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        public struct GStruct22
        {
            public int Width;
            public int Height;
            public int Left;
            public int Top;

            public int method_0() => Height;
            public int method_1() => Width;

            public bool method_5(int x, int y)
            {
                return x >= Left && x < Left + Width && y >= Top && y < Top + Height;
            }

            public int int_0 => Left;
            public int int_1 => Top;
        }

        private static IntPtr _currentProcessHandle = IntPtr.Zero;
        private static int _currentProcessId = 0;

        internal static bool bool_2 { get; set; }
        internal static bool bool_3 { get; set; }
        internal static int int_27 { get; set; }

        private static bool IsMemoryLoggingEnabled()
        {
            return ConfigManager.gclass61_0 != null && ConfigManager.gclass61_0.method_5("Log_Memory");
        }

        private static bool IsVerboseMemoryReadFailuresEnabled()
        {
            return ConfigManager.gclass61_0 != null && ConfigManager.gclass61_0.method_5("VerboseMemoryReadFailures");
        }

        private static string FormatAddressHex(int address)
        {
            return "0x" + unchecked((uint)address).ToString("x8");
        }

        private static string FormatAddressHex(uint address)
        {
            return "0x" + address.ToString("x8");
        }

        private static string FormatBytesForLog(byte[] bytes, int length)
        {
            if (bytes == null || bytes.Length == 0 || length <= 0)
                return "<empty>";

            var count = Math.Min(Math.Min(bytes.Length, length), 64);
            var builder = new StringBuilder(count * 3);
            for (var index = 0; index < count; ++index)
            {
                if (index > 0)
                    builder.Append(' ');
                builder.Append(bytes[index].ToString("x2"));
            }

            if (length > count)
                builder.Append(" ...");

            return builder.ToString();
        }

        private static void LogMemoryAccess(string message)
        {
            if (!IsMemoryLoggingEnabled())
                return;

            GContext.Main.Log("[Log_Memory] " + message);
        }

        private static void LogReadFailure(long address, int lengthToRead, int bytesRead, int lastError, bool allowPartialRead, string debugClue)
        {
            if (!IsVerboseMemoryReadFailuresEnabled())
                return;

            try
            {
                Logger.LogMessage(string.Format(
                    "[VerboseMemoryReadFailures] addr=0x{0:X8}, len={1}, bytesRead={2}, lastError={3}, allowPartial={4}, clue={5}",
                    address,
                    lengthToRead,
                    bytesRead,
                    lastError,
                    allowPartialRead,
                    debugClue ?? "<null>"));
            }
            catch
            {
                // Read-failure diagnostics must never throw.
            }
        }

        // Validate 32-bit address space for attached WoW process
        private static bool IsValid32BitAddress(long address)
        {
            // In 32-bit user-mode processes, valid addresses are typically below 0x80000000 (2GB).
            return address >= 0 && address <= 0x7FFFFFFF;
        }

        // Log invalid address attempts with caller information
        private static void LogInvalidAddress(long address, string debugClue, string operation)
        {
            try
            {
                var st = new StackTrace(true);
                string caller = "<unknown>";
                var frames = st.GetFrames();
                if (frames != null)
                {
                    foreach (var f in frames)
                    {
                        var m = f.GetMethod();
                        if (m == null) continue;
                        var decl = m.DeclaringType;
                        if (decl == null) continue;
                        if (decl != typeof(GProcessMemoryManipulator))
                        {
                            caller = decl.FullName + "." + m.Name;
                            break;
                        }
                    }
                }

                string message = string.Format("[Memory] Invalid address 0x{0:X8} in {1} (debugClue: '{2}'), caller: {3}", address, operation, debugClue ?? "<null>", caller);
                Logger.LogMessage(message);
            }
            catch
            {
                // Avoid throwing from logging path
            }
        }

        internal static int ReadInt32(int address, string debugClue)
        {
            var bytes = ReadBytes(address, 4, debugClue);
            if (bytes.Length < 4) return 0;
            return BitConverter.ToInt32(bytes, 0);
        }

        internal static uint ReadUInt32(int address, string debugClue)
        {
            var bytes = ReadBytes(address, 4, debugClue);
            if (bytes.Length < 4) return 0U;
            return BitConverter.ToUInt32(bytes, 0);
        }

        internal static uint ReadUInt32(uint address, string debugClue)
        {
            var bytes = ReadBytes(address, 4, debugClue);
            if (bytes.Length < 4) return 0U;
            return BitConverter.ToUInt32(bytes, 0);
        }

        internal static long ReadInt64(int address, string debugClue)
        {
            var bytes = ReadBytes(address, 8, debugClue);
            if (bytes.Length < 8) return 0;
            return BitConverter.ToInt64(bytes, 0);
        }

        internal static long ReadInt64(uint address, string debugClue)
        {
            var bytes = ReadBytes(address, 8, debugClue);
            if (bytes.Length < 8) return 0;
            return BitConverter.ToInt64(bytes, 0);
        }

        internal static float ReadFloat(int address, string debugClue)
        {
            var bytes = ReadBytes(address, 4, debugClue);
            if (bytes.Length < 4) return 0f;
            return BitConverter.ToSingle(bytes, 0);
        }

        internal static byte ReadByte(int address, string debugClue)
        {
            var bytes = ReadBytes(address, 1, debugClue);
            return bytes.Length > 0 ? bytes[0] : (byte)0;
        }

        internal static byte[] ReadBytes(int startAddress, int lengthToRead, string debugClue)
        {
            return ReadBytesInternal(startAddress, lengthToRead, debugClue, false);
        }

        internal static byte[] ReadBytes(uint startAddress, int lengthToRead, string debugClue)
        {
            return ReadBytesInternal(startAddress, lengthToRead, debugClue, false);
        }

        internal static byte[] ReadBytes(int startAddress, int lengthToRead, string debugClue, bool allowPartialRead)
        {
            return ReadBytesInternal(startAddress, lengthToRead, debugClue, allowPartialRead);
        }

        internal static byte[] ReadBytesRaw(int startAddress, int lengthToRead)
        {
            return ReadBytesInternal(startAddress, lengthToRead, null, true);
        }

        private static byte[] ReadBytesInternal(int startAddress, int lengthToRead, string debugClue, bool allowPartialRead)
        {
            var buffer = new byte[lengthToRead];

            if (_currentProcessHandle == IntPtr.Zero) return buffer;

            long address = unchecked((uint)startAddress);
            if (!IsValid32BitAddress(address))
            {
                LogInvalidAddress(address, debugClue, nameof(ReadBytesInternal));
                LogReadFailure(address, lengthToRead, 0, 0, allowPartialRead, debugClue);
                // Don't throw here - callers expect an empty buffer on read failure.
                return allowPartialRead ? buffer : new byte[0];
            }

            var nativeAddress = new IntPtr(address);
            if (!ReadProcessMemory(_currentProcessHandle, nativeAddress, buffer, lengthToRead, out int bytesRead))
            {
                var lastError = Marshal.GetLastWin32Error();
                LogReadFailure(address, lengthToRead, bytesRead, lastError, allowPartialRead, debugClue);
                if (lastError == ERROR_PARTIAL_COPY)
                    return allowPartialRead ? buffer : new byte[0];
                return allowPartialRead ? buffer : new byte[0];
            }

            if (bytesRead < lengthToRead && !allowPartialRead)
            {
                LogReadFailure(address, lengthToRead, bytesRead, ERROR_PARTIAL_COPY, allowPartialRead, debugClue);
                return new byte[0];
            }
            return buffer;
        }

        private static byte[] ReadBytesInternal(uint startAddress, int lengthToRead, string debugClue, bool allowPartialRead)
        {
            var buffer = new byte[lengthToRead];
            if (_currentProcessHandle == IntPtr.Zero) return buffer;

            long address = startAddress;
            if (!IsValid32BitAddress(address))
            {
                LogInvalidAddress(address, debugClue, nameof(ReadBytesInternal));
                LogReadFailure(address, lengthToRead, 0, 0, allowPartialRead, debugClue);
                // Don't throw here - callers expect an empty buffer on read failure.
                return allowPartialRead ? buffer : new byte[0];
            }

            if (!ReadProcessMemory(_currentProcessHandle, new IntPtr(address), buffer, lengthToRead, out int bytesRead))
            {
                var lastError = Marshal.GetLastWin32Error();
                LogReadFailure(address, lengthToRead, bytesRead, lastError, allowPartialRead, debugClue);
                if (lastError == ERROR_PARTIAL_COPY)
                    return allowPartialRead ? buffer : new byte[0];
                return allowPartialRead ? buffer : new byte[0];
            }

            if (bytesRead < lengthToRead && !allowPartialRead)
            {
                LogReadFailure(address, lengthToRead, bytesRead, ERROR_PARTIAL_COPY, allowPartialRead, debugClue);
                return new byte[0];
            }
            return buffer;
        }

        internal static int ReadIntFromOffset(int address, string debugClue) => ReadInt32(address, debugClue);
        internal static long ReadULongFromOffset(int address, string debugClue) => ReadInt64(address, debugClue);
        internal static float ReadFloatFromOffset(int address, string debugClue) => ReadFloat(address, debugClue);

        internal static double ReadDouble(int address, string debugClue)
        {
            var bytes = ReadBytes(address, 8, debugClue);
            if (bytes.Length < 8) return 0.0;
            return BitConverter.ToDouble(bytes, 0);
        }

        internal static float ReadFloatAlternate(int address, string debugClue) => ReadFloat(address, debugClue);

        internal static string ReadString(int startAddress, int maxLength, string debugClue)
        {
            return ReadStringInternal(startAddress, maxLength, debugClue);
        }

        internal static string ReadStringInternal(int startAddress, int maxLength, string debugClue)
        {
            var bytes = ReadBytesRaw(startAddress, maxLength);
            var nullIndex = Array.IndexOf(bytes, (byte)0);
            var length = nullIndex >= 0 ? nullIndex : bytes.Length;
            return Encoding.ASCII.GetString(bytes, 0, length);
        }

        internal static int WriteBytes(int startAddress, byte[] dataToWrite, int lengthToWrite)
        {
            if (_currentProcessHandle == IntPtr.Zero) return 0;

            long address = unchecked((uint)startAddress);
            if (!IsValid32BitAddress(address))
            {
                LogInvalidAddress(address, null, nameof(WriteBytes));
                throw new ArgumentOutOfRangeException(nameof(startAddress), $"Address 0x{address:X8} is not valid for 32-bit process.");
            }

            var nativeAddress = new IntPtr(address);
            if (!WriteProcessMemory(_currentProcessHandle, nativeAddress, dataToWrite, lengthToWrite, out int bytesWritten))
                return 0;

            return bytesWritten;
        }

        internal static bool IsMemoryReadable(int startAddress)
        {
            if (_currentProcessHandle == IntPtr.Zero) return false;

            long address = unchecked((uint)startAddress);
            if (!IsValid32BitAddress(address))
            {
                LogInvalidAddress(address, null, nameof(IsMemoryReadable));
                return false;
            }

            var testBuffer = new byte[1];
            var nativeAddress = new IntPtr(address);
            return ReadProcessMemory(_currentProcessHandle, nativeAddress, testBuffer, 1, out _);
        }

        internal static int ReadPointerChain(int startAddress, int lengthToRead, int maxDepth)
        {
            int currentAddress = startAddress;
            int depth = 0;

            while (depth < maxDepth)
            {
                var pointerBytes = ReadBytesRaw(currentAddress, 4);
                if (pointerBytes.Length < 4) return currentAddress;
                currentAddress = BitConverter.ToInt32(pointerBytes, 0);
                depth++;
            }
            return currentAddress;
        }

        internal static string GenerateRandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);
        internal static string smethod_0() => GenerateRandomString();
        internal static string smethod_10(int startAddress, int maxLength, string debugClue) => ReadString(startAddress, maxLength, debugClue);
        internal static int smethod_11(int startAddress, string debugClue) => ReadInt32(startAddress, debugClue);
        internal static long smethod_12(int startAddress, string debugClue) => ReadInt64(startAddress, debugClue);
        internal static double smethod_13(int startAddress, string debugClue) => ReadDouble(startAddress, debugClue);

        internal static bool IsWowProcessRunning() => Process.GetProcessesByName("WoW").Length > 0;

        internal static int AttachToWowProcess()
        {
            var processes = Process.GetProcessesByName("WoW");
            if (processes.Length == 0) return 0;

            SetProcessId(processes[0].Id);
            return processes[0].Id;
        }

        internal static void SetProcessId(int processId)
        {
            _currentProcessId = processId;
            CloseCurrentProcessHandle();
            _currentProcessHandle = OpenProcess(
                PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION | PROCESS_QUERY_INFORMATION,
                false,
                processId);
        }

        internal static IntPtr OpenProcessHandle(int processId) => OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, processId);
        internal static IntPtr OpenProcessWithAccess(int processId) => OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION | PROCESS_QUERY_INFORMATION, false, processId);

        internal static void CloseProcessHandle(IntPtr processHandle)
        {
            if (processHandle != IntPtr.Zero) CloseHandle(processHandle);
        }

        internal static void CloseCurrentProcessHandle()
        {
            if (_currentProcessHandle != IntPtr.Zero)
            {
                CloseHandle(_currentProcessHandle);
                _currentProcessHandle = IntPtr.Zero;
            }
        }

        internal static IntPtr GetWindowHandle() => GetForegroundWindowNative();

        internal static IntPtr GetMainWindowHandle(int processId)
        {
            var process = Process.GetProcessById(processId);
            return process?.MainWindowHandle ?? IntPtr.Zero;
        }

        internal static GStruct22 GetCursorPosition()
        {
            RECT rect;
            POINT pt = new POINT { X = 0, Y = 0 };
            IntPtr windowHandle = StartupClass.MainApplicationHandle;

            if (windowHandle == IntPtr.Zero)
                windowHandle = GetForegroundWindowNative();

            // FIX: Using GetClientRect + ClientToScreen ensures we don't include Windows 11 drop-shadow borders
            if (windowHandle != IntPtr.Zero && GetClientRect(windowHandle, out rect))
            {
                ClientToScreen(windowHandle, ref pt);
                var width = rect.Right - rect.Left;
                var height = rect.Bottom - rect.Top;

                if (width > 0 && height > 0)
                {
                    return new GStruct22
                    {
                        Left = pt.X,
                        Top = pt.Y,
                        Width = width,
                        Height = height
                    };
                }
            }

            var bounds = Screen.PrimaryScreen.Bounds;
            return new GStruct22
            {
                Left = bounds.Left,
                Top = bounds.Top,
                Width = bounds.Width,
                Height = bounds.Height
            };
        }

        internal static IntPtr GetForegroundWindow() => GetForegroundWindowNative();

        internal static void GetForegroundWindow(IntPtr windowHandle, Size size, Point point) { }

        internal static int GetCurrentProcessId() => Process.GetCurrentProcess().Id;
        internal static int GetProcessId() => _currentProcessId;

        internal static int GetProcessIdFromWindow()
        {
            var fgWindow = GetForegroundWindow();
            GetWindowThreadProcessId(fgWindow, out int processId);
            return processId;
        }

        internal static string GetProcessExecutablePath()
        {
            try
            {
                var process = Process.GetProcessById(_currentProcessId);
                return process.MainModule?.FileName ?? string.Empty;
            }
            catch { return string.Empty; }
        }

        internal static void WorldToScreen(double x, double y, out int sx, out int sy)
        {
            var window = GetCursorPosition();
            sx = window.int_0 + (int)(window.method_1() * x);
            sy = window.int_1 + (int)(window.method_0() * y);
        }

        internal static void ScreenToWorld(out double x, out double y, int sx, int sy)
        {
            var window = GetCursorPosition();
            x = window.method_1() <= 0 ? 0.0 : (sx - window.int_0) / (double)window.method_1();
            y = window.method_0() <= 0 ? 0.0 : (sy - window.int_1) / (double)window.method_0();
        }

        internal static void Sleep(uint milliseconds) => SleepNative(milliseconds);

        internal static bool SetForegroundWindow(IntPtr windowHandle) => SetForegroundWindowNative(windowHandle);

        internal static void ShowWindow(IntPtr windowHandle) => ShowWindowNative(windowHandle, 5); // SW_SHOW = 5

        internal static bool GetWindowPosition(IntPtr windowHandle, out Point point)
        {
            POINT pt = new POINT { X = 0, Y = 0 };
            if (ClientToScreen(windowHandle, ref pt))
            {
                point = new Point(pt.X, pt.Y);
                return true;
            }
            point = Point.Empty;
            return false;
        }

        internal static bool GetWindowSize(IntPtr windowHandle, out Size size)
        {
            if (GetClientRect(windowHandle, out RECT rect))
            {
                size = new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
                return true;
            }
            size = Size.Empty;
            return false;
        }

        internal static void SetWindowSize(IntPtr windowHandle, Size size) { }

        internal static void IsWindowVisible(Control control, string helpFile, HelpNavigator navigator, object parameter) { }

        // FULLY IMPLEMENTED: Checks if WoW is minimized to allow background rotation loops to pause or adjust
        internal static bool IsWindowMinimized()
        {
            IntPtr windowHandle = StartupClass.MainApplicationHandle;
            if (windowHandle == IntPtr.Zero)
                return false;

            return IsIconic(windowHandle);
        }

        internal static byte[] smethod_17(int address, int size, string debugClue) => ReadBytesRaw(address, size);
        internal static byte[] smethod_20(int address, int size) => ReadBytesRaw(address, size);
        internal static void smethod_48(Form form) { }
        internal static void smethod_51(HelpProvider helpProvider) { }

        internal static bool smethod_52(out long playerGuid, out int mainTable)
        {
            playerGuid = 0L;
            mainTable = 0;

            var clientConnection = ReadUInt32(GameMemoryConstants.Wotlk.ClientConnection, "ClientConnection");
            if (!IsLikelyMemoryPointer(clientConnection)) return false;

            var objectManager = ReadUInt32(clientConnection + GameMemoryConstants.Wotlk.CurMgrOffset, "CurMgrOffset");
            if (!IsLikelyMemoryPointer(objectManager)) return false;

            var firstObject = ReadUInt32(objectManager + GameMemoryConstants.Wotlk.FirstObject, "FirstObject");
            if (!IsLikelyMemoryPointer(firstObject)) return false;

            playerGuid = ReadInt64(objectManager + GameMemoryConstants.Wotlk.LocalGuid, "LocalGUID");
            if (playerGuid == 0L) return false;

            mainTable = unchecked((int)objectManager);
            return true;
        }

        private static uint GetWowBaseAddress()
        {
            if (_currentProcessId == 0) return 0U;
            try
            {
                var process = Process.GetProcessById(_currentProcessId);
                if (process == null || process.HasExited || process.MainModule == null) return 0U;
                return unchecked((uint)process.MainModule.BaseAddress.ToInt32());
            }
            catch { return 0U; }
        }

        private static bool IsLikelyMemoryPointer(uint pointer)
        {
            return (pointer & 1U) == 0U && pointer != 0U && pointer != 28U && pointer >= 65536U;
        }

        internal static void smethod_53() { }
        internal static void smethod_54() { }
        internal static void smethod_55(int processId) => SetProcessId(processId);

        internal static bool smethod_56(int processId)
        {
            try
            {
                var process = Process.GetProcessById(processId);
                return !process.HasExited;
            }
            catch { return false; }
        }
    }
}
