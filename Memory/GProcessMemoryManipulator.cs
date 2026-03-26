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
                // Don't throw here - callers expect an empty buffer on read failure.
                return allowPartialRead ? buffer : new byte[0];
            }

            var nativeAddress = new IntPtr(address);
            if (!ReadProcessMemory(_currentProcessHandle, nativeAddress, buffer, lengthToRead, out int bytesRead))
            {
                var lastError = Marshal.GetLastWin32Error();
                if (lastError == ERROR_PARTIAL_COPY)
                    return allowPartialRead ? buffer : new byte[0];
                return allowPartialRead ? buffer : new byte[0];
            }

            if (bytesRead < lengthToRead && !allowPartialRead) return new byte[0];
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
                // Don't throw here - callers expect an empty buffer on read failure.
                return allowPartialRead ? buffer : new byte[0];
            }

            if (!ReadProcessMemory(_currentProcessHandle, new IntPtr(address), buffer, lengthToRead, out int bytesRead))
            {
                var lastError = Marshal.GetLastWin32Error();
                if (lastError == ERROR_PARTIAL_COPY)
                    return allowPartialRead ? buffer : new byte[0];
                return allowPartialRead ? buffer : new byte[0];
            }

            if (bytesRead < lengthToRead && !allowPartialRead) return new byte[0];
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