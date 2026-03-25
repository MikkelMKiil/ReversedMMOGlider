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
        internal const uint ClientConnection = 0x00C79CE0;
        internal const uint CurMgrOffset = 0x2ED0;
        internal const uint FirstObject = 0xAC;
        internal const uint LocalGuid = 0xC0;
        internal const uint PlayerGuid = LocalGuid;
        internal const uint TargetGuid = 0x00BD07B0;

        internal const uint ObjStoragePointer = 0x8;
        internal const uint ObjType = 0x14;
        internal const uint ObjGuid = 0x30;
        internal const uint NextObject = 0x3C;

        internal const uint PosX = 0x9B8;
        internal const uint PosY = 0x9BC;
        internal const uint PosZ = 0x9C0;

        internal const uint DescriptorBase = 0x8;
        internal const uint UnitFieldHealth = 0x6C;
        internal const uint UnitFieldMaxHealth = 0x74;
        internal const uint UnitFieldPower1 = 0x70;
        internal const uint UnitFieldMaxPower1 = 0x78;
        internal const uint UnitFieldPower2 = 0x74;
        internal const uint UnitFieldPower4 = 0x7C;
        internal const uint UnitFieldFactionTemplate = 0x90;

        internal const uint PlayerNameStore = 0x00C79D18;
        internal const uint MapId = 0x00AB63BC;
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
            public int Width;
            public int Height;
            public int Left;
            public int Top;

            /// <summary>
            /// Returns the window height.
            /// </summary>
            public int method_0() => Height;

            /// <summary>
            /// Returns the window width.
            /// </summary>
            public int method_1() => Width;

            /// <summary>
            /// Checks if the given coordinates are within the screen bounds.
            /// </summary>
            public bool method_5(int x, int y)
            {
                return x >= Left && x < Left + Width && y >= Top && y < Top + Height;
            }

            public int int_0 => Left;
            public int int_1 => Top;
        }

        private static IntPtr _currentProcessHandle = IntPtr.Zero;
        private static int _currentProcessId = 0;

        // Properties for delegated access
        internal static bool bool_2 { get; set; }
        internal static bool bool_3 { get; set; }
        internal static int int_27 { get; set; }

        private static bool IsMemoryLoggingEnabled()
        {
            return ConfigManager.gclass61_0 != null && ConfigManager.gclass61_0.method_5("Log_Memory");
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
        /// Reads a 32-bit unsigned integer from process memory at the specified address.
        /// </summary>
        internal static uint ReadUInt32(int address, string debugClue)
        {
            var bytes = ReadBytes(address, 4, debugClue);
            if (bytes.Length < 4)
                return 0U;
            return BitConverter.ToUInt32(bytes, 0);
        }

        internal static uint ReadUInt32(uint address, string debugClue)
        {
            var bytes = ReadBytes(address, 4, debugClue);
            if (bytes.Length < 4)
                return 0U;
            return BitConverter.ToUInt32(bytes, 0);
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

        internal static long ReadInt64(uint address, string debugClue)
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

        internal static byte[] ReadBytes(uint startAddress, int lengthToRead, string debugClue)
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
            LogMemoryAccess("READ request addr=" + FormatAddressHex(startAddress) + ", len=" + lengthToRead + ", clue=" + (debugClue ?? "(none)") + ", allowPartial=" + allowPartialRead);

            if (_currentProcessHandle == IntPtr.Zero)
            {
                if (!string.IsNullOrEmpty(debugClue))
                    GContext.Main.Log($"[CRITICAL] ReadBytes failed: No process handle (clue = {debugClue})");
                LogMemoryAccess("READ failed addr=" + FormatAddressHex(startAddress) + ", reason=no process handle");
                return buffer;
            }

            if (!ReadProcessMemory(_currentProcessHandle, new IntPtr(startAddress), buffer, lengthToRead, out int bytesRead))
            {
                var lastError = Marshal.GetLastWin32Error();
                if (!string.IsNullOrEmpty(debugClue))
                    GContext.Main.Log($"[CRITICAL] ReadBytes from 0x{startAddress:x} failed, error: {lastError} (clue = {debugClue})");
                LogMemoryAccess("READ failed addr=" + FormatAddressHex(startAddress) + ", error=" + lastError + ", clue=" + (debugClue ?? "(none)"));
                return allowPartialRead ? buffer : new byte[0];
            }

            if (bytesRead < lengthToRead && !allowPartialRead)
            {
                if (!string.IsNullOrEmpty(debugClue))
                    GContext.Main.Log($"[CRITICAL] ReadBytes from 0x{startAddress:x}: Expected {lengthToRead} bytes, got {bytesRead} (clue = {debugClue})");
                LogMemoryAccess("READ short addr=" + FormatAddressHex(startAddress) + ", expected=" + lengthToRead + ", got=" + bytesRead + ", clue=" + (debugClue ?? "(none)"));
                return new byte[0];
            }

            LogMemoryAccess("READ result addr=" + FormatAddressHex(startAddress) + ", bytesRead=" + bytesRead + ", data=" + FormatBytesForLog(buffer, bytesRead));

            return buffer;
        }

        private static byte[] ReadBytesInternal(uint startAddress, int lengthToRead, string debugClue, bool allowPartialRead)
        {
            var buffer = new byte[lengthToRead];
            LogMemoryAccess("READ request addr=" + FormatAddressHex(startAddress) + ", len=" + lengthToRead + ", clue=" + (debugClue ?? "(none)") + ", allowPartial=" + allowPartialRead);

            if (_currentProcessHandle == IntPtr.Zero)
            {
                if (!string.IsNullOrEmpty(debugClue))
                    GContext.Main.Log($"[CRITICAL] ReadBytes failed: No process handle (clue = {debugClue})");
                LogMemoryAccess("READ failed addr=" + FormatAddressHex(startAddress) + ", reason=no process handle");
                return buffer;
            }

            if (!ReadProcessMemory(_currentProcessHandle, new IntPtr((long)startAddress), buffer, lengthToRead, out int bytesRead))
            {
                var lastError = Marshal.GetLastWin32Error();
                if (!string.IsNullOrEmpty(debugClue))
                    GContext.Main.Log($"[CRITICAL] ReadBytes from 0x{startAddress:x} failed, error: {lastError} (clue = {debugClue})");
                LogMemoryAccess("READ failed addr=" + FormatAddressHex(startAddress) + ", error=" + lastError + ", clue=" + (debugClue ?? "(none)"));
                return allowPartialRead ? buffer : new byte[0];
            }

            if (bytesRead < lengthToRead && !allowPartialRead)
            {
                if (!string.IsNullOrEmpty(debugClue))
                    GContext.Main.Log($"[CRITICAL] ReadBytes from 0x{startAddress:x}: Expected {lengthToRead} bytes, got {bytesRead} (clue = {debugClue})");
                LogMemoryAccess("READ short addr=" + FormatAddressHex(startAddress) + ", expected=" + lengthToRead + ", got=" + bytesRead + ", clue=" + (debugClue ?? "(none)"));
                return new byte[0];
            }

            LogMemoryAccess("READ result addr=" + FormatAddressHex(startAddress) + ", bytesRead=" + bytesRead + ", data=" + FormatBytesForLog(buffer, bytesRead));

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
        internal static long ReadULongFromOffset(int address, string debugClue)
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
            LogMemoryAccess("WRITE request addr=" + FormatAddressHex(startAddress) + ", len=" + lengthToWrite + ", data=" + FormatBytesForLog(dataToWrite, lengthToWrite));

            if (_currentProcessHandle == IntPtr.Zero)
            {
                GContext.Main.Log($"[CRITICAL] WriteBytes failed: No process handle");
                LogMemoryAccess("WRITE failed addr=" + FormatAddressHex(startAddress) + ", reason=no process handle");
                return 0;
            }

            if (!WriteProcessMemory(_currentProcessHandle, new IntPtr(startAddress), dataToWrite, lengthToWrite, out int bytesWritten))
            {
                var lastError = Marshal.GetLastWin32Error();
                GContext.Main.Log($"[CRITICAL] WriteBytes to 0x{startAddress:x} failed, error: {lastError}");
                LogMemoryAccess("WRITE failed addr=" + FormatAddressHex(startAddress) + ", error=" + lastError);
                return 0;
            }

            LogMemoryAccess("WRITE result addr=" + FormatAddressHex(startAddress) + ", bytesWritten=" + bytesWritten);

            return bytesWritten;
        }

        /// <summary>
        /// Checks if a memory address is readable without raising exceptions.
        /// </summary>
        internal static bool IsMemoryReadable(int startAddress)
        {
            if (_currentProcessHandle == IntPtr.Zero)
            {
                LogMemoryAccess("READABLE check addr=" + FormatAddressHex(startAddress) + " => false (no process handle)");
                return false;
            }

            var testBuffer = new byte[1];
            var readable = ReadProcessMemory(_currentProcessHandle, new IntPtr(startAddress), testBuffer, 1, out _);
            LogMemoryAccess("READABLE check addr=" + FormatAddressHex(startAddress) + " => " + readable);
            return readable;
        }

        /// <summary>
        /// Reads a pointer chain to dereference multi-level pointers.
        /// </summary>
        internal static int ReadPointerChain(int startAddress, int lengthToRead, int maxDepth)
        {
            int currentAddress = startAddress;
            int depth = 0;
            LogMemoryAccess("PTRCHAIN begin addr=" + FormatAddressHex(startAddress) + ", requestedLen=" + lengthToRead + ", maxDepth=" + maxDepth);

            while (depth < maxDepth)
            {
                var pointerBytes = ReadBytesRaw(currentAddress, 4);
                if (pointerBytes.Length < 4)
                {
                    LogMemoryAccess("PTRCHAIN stop depth=" + depth + ", addr=" + FormatAddressHex(currentAddress) + ", reason=short read");
                    return currentAddress;
                }

                var nextAddress = BitConverter.ToInt32(pointerBytes, 0);
                LogMemoryAccess("PTRCHAIN step depth=" + depth + ", from=" + FormatAddressHex(currentAddress) + ", to=" + FormatAddressHex(nextAddress) + ", raw=" + FormatBytesForLog(pointerBytes, pointerBytes.Length));
                currentAddress = BitConverter.ToInt32(pointerBytes, 0);
                depth++;
            }

            LogMemoryAccess("PTRCHAIN end finalAddr=" + FormatAddressHex(currentAddress) + ", depth=" + depth);

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
            RECT rect;
            IntPtr windowHandle = StartupClass.MainApplicationHandle;
            if (windowHandle == IntPtr.Zero)
                windowHandle = GetForegroundWindowNative();

            if (windowHandle != IntPtr.Zero && GetWindowRect(windowHandle, out rect))
            {
                var width = rect.Right - rect.Left;
                var height = rect.Bottom - rect.Top;
                if (width > 0 && height > 0)
                {
                    return new GStruct22
                    {
                        Left = rect.Left,
                        Top = rect.Top,
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
            var window = GetCursorPosition();
            sx = window.int_0 + (int)(window.method_1() * x);
            sy = window.int_1 + (int)(window.method_0() * y);
            if (ConfigManager.gclass61_0 != null && ConfigManager.gclass61_0.method_5("VerboseMainLoopLogging"))
                GContext.Main.Log("[VerboseMainLoop] [WorldToScreen/GProcess] input=(" + x + "," + y +
                                  "), window=(" + window.int_0 + "," + window.int_1 + "," + window.method_1() +
                                  "," + window.method_0() + "), output=(" + sx + "," + sy + ")");
        }

        internal static void ScreenToWorld(out double x, out double y, int sx, int sy)
        {
            var window = GetCursorPosition();
            x = window.method_1() <= 0 ? 0.0 : (sx - window.int_0) / (double)window.method_1();
            y = window.method_0() <= 0 ? 0.0 : (sy - window.int_1) / (double)window.method_0();
            if (ConfigManager.gclass61_0 != null && ConfigManager.gclass61_0.method_5("VerboseMainLoopLogging"))
                GContext.Main.Log("[VerboseMainLoop] [ScreenToWorld/GProcess] input=(" + sx + "," + sy +
                                  "), window=(" + window.int_0 + "," + window.int_1 + "," + window.method_1() +
                                  "," + window.method_0() + "), output=(" + x + "," + y + ")");
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

            var clientConnection = ReadUInt32(WotlkOffsets.ClientConnection, "ClientConnection");
            if (!IsLikelyMemoryPointer(clientConnection))
            {
                GContext.Main.Log("[CRITICAL] Attach probe failed: ClientConnection pointer is invalid");
                return false;
            }

            var objectManager = ReadUInt32(clientConnection + WotlkOffsets.CurMgrOffset, "CurMgrOffset");
            if (!IsLikelyMemoryPointer(objectManager))
            {
                GContext.Main.Log("[CRITICAL] Attach probe failed: ObjectManager pointer is invalid");
                return false;
            }

            var firstObject = ReadUInt32(objectManager + WotlkOffsets.FirstObject, "FirstObject");
            if (!IsLikelyMemoryPointer(firstObject))
            {
                GContext.Main.Log("[CRITICAL] Attach probe failed: first object pointer is invalid");
                return false;
            }

            playerGuid = ReadInt64(objectManager + WotlkOffsets.LocalGuid, "LocalGUID");
            if (playerGuid == 0L)
            {
                GContext.Main.Log("[CRITICAL] Attach probe failed: local player GUID is zero");
                return false;
            }

            mainTable = unchecked((int)objectManager);
            return true;
        }

        private static uint GetWowBaseAddress()
        {
            if (_currentProcessId == 0)
                return 0U;

            try
            {
                var process = Process.GetProcessById(_currentProcessId);
                if (process == null || process.HasExited || process.MainModule == null)
                    return 0U;

                return unchecked((uint)process.MainModule.BaseAddress.ToInt32());
            }
            catch (InvalidOperationException)
            {
                return 0U;
            }
            catch (Win32Exception)
            {
                return 0U;
            }
            catch (NotSupportedException)
            {
                return 0U;
            }
        }

        private static bool IsLikelyMemoryPointer(uint pointer)
        {
            return (pointer & 1U) == 0U && pointer != 0U && pointer != 28U && pointer >= 65536U;
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
