#nullable disable

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Glider.Common.Objects
{
    internal static class WotlkOffsets
    {
        internal const int CurMgrPointer = 0x00CF0B90;
        internal const int CurMgrOffset = 0x3568;
        internal const int FirstObject = 0xAC;
        internal const int PlayerGuid = 0x00CA1238;
        internal const int TargetGuid = 0x00BD07A0;

        internal const int ObjStoragePointer = 0x8;
        internal const int ObjType = 0x14;
        internal const int ObjGuid = 0x30;
        internal const int NextObject = 0x3C;

        internal const int PosX = 0x9B8;
        internal const int PosY = 0x9BC;
        internal const int PosZ = 0x9C0;

        internal const int DescriptorBase = 0x8;
        internal const int UnitFieldHealth = 0x6C;
        internal const int UnitFieldMaxHealth = 0x74;
        internal const int UnitFieldPower1 = 0x70;
        internal const int UnitFieldMaxPower1 = 0x78;
        internal const int UnitFieldPower2 = 0x74;
        internal const int UnitFieldPower4 = 0x7C;
        internal const int UnitFieldFactionTemplate = 0x90;
    }

    /// <summary>
    /// Central process memory manipulation handler for WoW (WOTLK 3.3.5a).
    /// Provides low-level read/write operations to external process memory.
    /// All memory access should flow through this class for consistent error handling and logging.
    /// </summary>
    internal static class GProcessMemoryManipulator
    {
        // Windows API imports for process memory access
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

        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

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
            public int X;
            public int Y;

            /// <summary>
            /// Returns the X coordinate (width).
            /// </summary>
            public int method_0() => X;

            /// <summary>
            /// Returns the Y coordinate (height).
            /// </summary>
            public int method_1() => Y;

            /// <summary>
            /// Checks if the given coordinates are within the screen bounds.
            /// </summary>
            public bool method_5(int x, int y)
            {
                return x >= 0 && x < X && y >= 0 && y < Y;
            }

            public int int_0 => X;
            public int int_1 => Y;
        }

        private static IntPtr _currentProcessHandle = IntPtr.Zero;
        private static int _currentProcessId = 0;

        // Properties for delegated access
        internal static bool bool_2 { get; set; }
        internal static bool bool_3 { get; set; }
        internal static int int_27 { get; set; }

        /// <summary>
        /// Reads a 32-bit signed integer from process memory at the specified address.
        /// </summary>
        internal static int ReadInt32(int address, string debugClue)
        {
            var bytes = ReadBytes(address, 4, debugClue);
            if (bytes.Length < 4)
                return 0;
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// Reads a 64-bit signed integer from process memory at the specified address.
        /// </summary>
        internal static long ReadInt64(int address, string debugClue)
        {
            var bytes = ReadBytes(address, 8, debugClue);
            if (bytes.Length < 8)
                return 0;
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// Reads a 32-bit floating-point value from process memory at the specified address.
        /// </summary>
        internal static float ReadFloat(int address, string debugClue)
        {
            var bytes = ReadBytes(address, 4, debugClue);
            if (bytes.Length < 4)
                return 0f;
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// Reads a single byte from process memory at the specified address.
        /// </summary>
        internal static byte ReadByte(int address, string debugClue)
        {
            var bytes = ReadBytes(address, 1, debugClue);
            return bytes.Length > 0 ? bytes[0] : (byte)0;
        }

        /// <summary>
        /// Reads raw bytes from process memory. This is the core memory read operation.
        /// </summary>
        internal static byte[] ReadBytes(int startAddress, int lengthToRead, string debugClue)
        {
            return ReadBytesInternal(startAddress, lengthToRead, debugClue, false);
        }

        /// <summary>
        /// Reads raw bytes from process memory with optional partial read support.
        /// </summary>
        internal static byte[] ReadBytes(int startAddress, int lengthToRead, string debugClue, bool allowPartialRead)
        {
            return ReadBytesInternal(startAddress, lengthToRead, debugClue, allowPartialRead);
        }

        /// <summary>
        /// Reads raw bytes without debug logging.
        /// </summary>
        internal static byte[] ReadBytesRaw(int startAddress, int lengthToRead)
        {
            return ReadBytesInternal(startAddress, lengthToRead, null, true);
        }

        /// <summary>
        /// Core memory read implementation. All byte reads flow through here.
        /// </summary>
        private static byte[] ReadBytesInternal(int startAddress, int lengthToRead, string debugClue, bool allowPartialRead)
        {
            var buffer = new byte[lengthToRead];

            if (_currentProcessHandle == IntPtr.Zero)
            {
                if (!string.IsNullOrEmpty(debugClue))
                    GContext.Main.Log($"[CRITICAL] ReadBytes failed: No process handle (clue = {debugClue})");
                return buffer;
            }

            if (!ReadProcessMemory(_currentProcessHandle, new IntPtr(startAddress), buffer, lengthToRead, out int bytesRead))
            {
                var lastError = Marshal.GetLastWin32Error();
                if (!string.IsNullOrEmpty(debugClue))
                    GContext.Main.Log($"[CRITICAL] ReadBytes from 0x{startAddress:x} failed, error: {lastError} (clue = {debugClue})");
                return allowPartialRead ? buffer : new byte[0];
            }

            if (bytesRead < lengthToRead && !allowPartialRead)
            {
                if (!string.IsNullOrEmpty(debugClue))
                    GContext.Main.Log($"[CRITICAL] ReadBytes from 0x{startAddress:x}: Expected {lengthToRead} bytes, got {bytesRead} (clue = {debugClue})");
                return new byte[0];
            }

            return buffer;
        }

        /// <summary>
        /// Reads a 32-bit integer directly from memory with offset calculation.
        /// </summary>
        internal static int ReadIntFromOffset(int address, string debugClue)
        {
            return ReadInt32(address, debugClue);
        }

        /// <summary>
        /// Reads a 64-bit integer directly from memory with offset calculation.
        /// </summary>
        internal static long ReadLongFromOffset(int address, string debugClue)
        {
            return ReadInt64(address, debugClue);
        }

        /// <summary>
        /// Reads a 32-bit floating-point value with offset.
        /// </summary>
        internal static float ReadFloatFromOffset(int address, string debugClue)
        {
            return ReadFloat(address, debugClue);
        }

        /// <summary>
        /// Reads a double-precision floating-point value from process memory.
        /// </summary>
        internal static double ReadDouble(int address, string debugClue)
        {
            var bytes = ReadBytes(address, 8, debugClue);
            if (bytes.Length < 8)
                return 0.0;
            return BitConverter.ToDouble(bytes, 0);
        }

        /// <summary>
        /// Reads an alternate floating-point format (for special cases).
        /// </summary>
        internal static float ReadFloatAlternate(int address, string debugClue)
        {
            return ReadFloat(address, debugClue);
        }

        /// <summary>
        /// Reads a null-terminated string from process memory.
        /// </summary>
        internal static string ReadString(int startAddress, int maxLength, string debugClue)
        {
            return ReadStringInternal(startAddress, maxLength, debugClue);
        }

        /// <summary>
        /// Reads a string with internal handling.
        /// </summary>
        internal static string ReadStringInternal(int startAddress, int maxLength, string debugClue)
        {
            var bytes = ReadBytesRaw(startAddress, maxLength);
            var nullIndex = Array.IndexOf(bytes, (byte)0);
            var length = nullIndex >= 0 ? nullIndex : bytes.Length;
            return Encoding.ASCII.GetString(bytes, 0, length);
        }

        /// <summary>
        /// Writes bytes to process memory. This is the core memory write operation.
        /// </summary>
        internal static int WriteBytes(int startAddress, byte[] dataToWrite, int lengthToWrite)
        {
            if (_currentProcessHandle == IntPtr.Zero)
            {
                GContext.Main.Log($"[CRITICAL] WriteBytes failed: No process handle");
                return 0;
            }

            if (!WriteProcessMemory(_currentProcessHandle, new IntPtr(startAddress), dataToWrite, lengthToWrite, out int bytesWritten))
            {
                var lastError = Marshal.GetLastWin32Error();
                GContext.Main.Log($"[CRITICAL] WriteBytes to 0x{startAddress:x} failed, error: {lastError}");
                return 0;
            }

            return bytesWritten;
        }

        /// <summary>
        /// Checks if a memory address is readable without raising exceptions.
        /// </summary>
        internal static bool IsMemoryReadable(int startAddress)
        {
            if (_currentProcessHandle == IntPtr.Zero)
                return false;

            var testBuffer = new byte[1];
            return ReadProcessMemory(_currentProcessHandle, new IntPtr(startAddress), testBuffer, 1, out _);
        }

        /// <summary>
        /// Reads a pointer chain to dereference multi-level pointers.
        /// </summary>
        internal static int ReadPointerChain(int startAddress, int lengthToRead, int maxDepth)
        {
            int currentAddress = startAddress;
            int depth = 0;

            while (depth < maxDepth)
            {
                var pointerBytes = ReadBytesRaw(currentAddress, 4);
                if (pointerBytes.Length < 4)
                    return currentAddress;

                currentAddress = BitConverter.ToInt32(pointerBytes, 0);
                depth++;
            }

            return currentAddress;
        }

        /// <summary>
        /// Generates a random string for internal use.
        /// </summary>
        internal static string GenerateRandomString()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8);
        }

        /// <summary>
        /// Utility method smethod_0.
        /// </summary>
        internal static string smethod_0()
        {
            return GenerateRandomString();
        }

        /// <summary>
        /// Utility method smethod_10 - reads a string from memory.
        /// </summary>
        internal static string smethod_10(int startAddress, int maxLength, string debugClue)
        {
            return ReadString(startAddress, maxLength, debugClue);
        }

        /// <summary>
        /// Utility method smethod_11 - reads an int from memory.
        /// </summary>
        internal static int smethod_11(int startAddress, string debugClue)
        {
            return ReadInt32(startAddress, debugClue);
        }

        /// <summary>
        /// Utility method smethod_12 - reads a long from memory.
        /// </summary>
        internal static long smethod_12(int startAddress, string debugClue)
        {
            return ReadInt64(startAddress, debugClue);
        }

        /// <summary>
        /// Utility method smethod_13 - reads a double from memory.
        /// </summary>
        internal static double smethod_13(int startAddress, string debugClue)
        {
            return ReadDouble(startAddress, debugClue);
        }

        // Process management methods

        internal static bool IsWowProcessRunning()
        {
            var processes = Process.GetProcessesByName("WoW");
            return processes.Length > 0;
        }

        internal static int AttachToWowProcess()
        {
            var processes = Process.GetProcessesByName("WoW");
            if (processes.Length == 0)
                return 0;

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

        internal static IntPtr OpenProcessHandle(int processId)
        {
            return OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, processId);
        }

        internal static IntPtr OpenProcessWithAccess(int processId)
        {
            return OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION | PROCESS_QUERY_INFORMATION, false, processId);
        }

        internal static void CloseProcessHandle(IntPtr processHandle)
        {
            if (processHandle != IntPtr.Zero)
                CloseHandle(processHandle);
        }

        internal static void CloseCurrentProcessHandle()
        {
            if (_currentProcessHandle != IntPtr.Zero)
            {
                CloseHandle(_currentProcessHandle);
                _currentProcessHandle = IntPtr.Zero;
            }
        }

        internal static IntPtr GetWindowHandle()
        {
            return GetForegroundWindow();
        }

        internal static IntPtr GetMainWindowHandle(int processId)
        {
            var process = Process.GetProcessById(processId);
            return process?.MainWindowHandle ?? IntPtr.Zero;
        }

        internal static GStruct22 GetCursorPosition()
        {
            GetCursorPos(out POINT point);
            return new GStruct22 { X = point.X, Y = point.Y };
        }

        internal static IntPtr GetForegroundWindow()
        {
            return GetForegroundWindowNative();
        }

        internal static void GetForegroundWindow(IntPtr windowHandle, Size size, Point point)
        {
            // This appears to be for setting window state, but the signature suggests getting.
            // Left as placeholder for compatibility.
        }

        internal static int GetCurrentProcessId()
        {
            return Process.GetCurrentProcess().Id;
        }

        internal static int GetProcessId()
        {
            return _currentProcessId;
        }

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
            catch
            {
                return string.Empty;
            }
        }

        internal static void WorldToScreen(double x, double y, out int sx, out int sy)
        {
            // This would require the game's camera matrix - placeholder implementation
            sx = (int)x;
            sy = (int)y;
        }

        internal static void ScreenToWorld(out double x, out double y, int sx, int sy)
        {
            // This would require the game's camera matrix inverse - placeholder implementation
            x = sx;
            y = sy;
        }

        internal static void Sleep(uint milliseconds)
        {
            SleepNative(milliseconds);
        }

        internal static bool SetForegroundWindow(IntPtr windowHandle)
        {
            return SetForegroundWindowNative(windowHandle);
        }

        internal static void ShowWindow(IntPtr windowHandle)
        {
            ShowWindowNative(windowHandle, 5); // SW_SHOW = 5
        }

        internal static bool GetWindowPosition(IntPtr windowHandle, out Point point)
        {
            if (GetWindowRect(windowHandle, out RECT rect))
            {
                point = new Point(rect.Left, rect.Top);
                return true;
            }
            point = Point.Empty;
            return false;
        }

        internal static bool GetWindowSize(IntPtr windowHandle, out Size size)
        {
            if (GetWindowRect(windowHandle, out RECT rect))
            {
                size = new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
                return true;
            }
            size = Size.Empty;
            return false;
        }

        internal static void SetWindowSize(IntPtr windowHandle, Size size)
        {
            // Implementation would use SetWindowPos - placeholder
        }

        internal static void IsWindowVisible(Control control, string helpFile, HelpNavigator navigator, object parameter)
        {
            // Placeholder for UI help integration
        }

        internal static void IsWindowMinimized()
        {
            // Placeholder implementation
        }

        internal static byte[] smethod_17(int address, int size, string debugClue)
        {
            return ReadBytesRaw(address, size);
        }

        internal static byte[] smethod_20(int address, int size)
        {
            return ReadBytesRaw(address, size);
        }

        internal static void smethod_48(Form form)
        {
            // Placeholder for form-related operation
        }

        internal static void smethod_51(HelpProvider helpProvider)
        {
            // Placeholder for help provider operation
        }

        internal static bool smethod_52(out long playerGuid, out int mainTable)
        {
            playerGuid = 0L;
            mainTable = 0;

            var wowBaseAddress = GetWowBaseAddress();
            if (!IsLikelyMemoryPointer(wowBaseAddress))
            {
                GContext.Main.Log("[CRITICAL] Attach probe failed: unable to resolve WoW base address");
                return false;
            }

            var curMgrPointer = ReadInt32(wowBaseAddress + WotlkOffsets.CurMgrPointer, "CurMgrPointer");
            if (!IsLikelyMemoryPointer(curMgrPointer))
            {
                GContext.Main.Log("[CRITICAL] Attach probe failed: CurMgr pointer is invalid");
                return false;
            }

            var objectManager = ReadInt32(curMgrPointer + WotlkOffsets.CurMgrOffset, "CurMgrOffset");
            if (!IsLikelyMemoryPointer(objectManager))
            {
                GContext.Main.Log("[CRITICAL] Attach probe failed: object manager pointer is invalid");
                return false;
            }

            var firstObject = ReadInt32(objectManager + WotlkOffsets.FirstObject, "FirstObject");
            if (!IsLikelyMemoryPointer(firstObject))
            {
                GContext.Main.Log("[CRITICAL] Attach probe failed: first object pointer is invalid");
                return false;
            }

            playerGuid = ReadInt64(wowBaseAddress + WotlkOffsets.PlayerGuid, "PlayerGuid");
            if (playerGuid == 0L)
            {
                GContext.Main.Log("[CRITICAL] Attach probe failed: local player GUID is zero");
                return false;
            }

            mainTable = objectManager;
            return true;
        }

        private static int GetWowBaseAddress()
        {
            if (_currentProcessId == 0)
                return 0;

            try
            {
                var process = Process.GetProcessById(_currentProcessId);
                if (process == null || process.HasExited || process.MainModule == null)
                    return 0;

                return process.MainModule.BaseAddress.ToInt32();
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
            catch (Win32Exception)
            {
                return 0;
            }
            catch (NotSupportedException)
            {
                return 0;
            }
        }

        private static bool IsLikelyMemoryPointer(int pointer)
        {
            return (pointer & 1) == 0 && pointer != 0 && pointer != 28 && pointer >= 65536;
        }

        internal static void smethod_53()
        {
            // Placeholder
        }

        internal static void smethod_54()
        {
            // Placeholder
        }

        internal static void smethod_55(int processId)
        {
            SetProcessId(processId);
        }

        internal static bool smethod_56(int processId)
        {
            // Placeholder for process validation
            try
            {
                var process = Process.GetProcessById(processId);
                return !process.HasExited;
            }
            catch
            {
                return false;
            }
        }
    }
}
