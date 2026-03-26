using Glider.Common.Objects;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

public class InputController // Original: InputController
{
    // Original: GEnum8
    public enum VirtualKeyCode : ushort
    {
        Backspace = 8,       // const_4
        Tab = 9,             // const_5
        Enter = 13,          // const_6
        Shift = 16,          // const_0
        Control = 17,        // const_1
        Alt = 18,            // const_2
        Escape = 27,         // const_3
        PageUp = 33,         // const_7
        PageDown = 34,       // const_8
        End = 35,            // const_9
        Home = 36,           // const_10
        Left = 37,           // const_11
        Up = 38,             // const_12
        Right = 39,          // const_13
        Down = 40,           // const_14
        Insert = 45,         // const_19
        Delete = 46,         // const_20
        // (Abridged standard VK codes for brevity, match the rest to standard Virtual Keys)
    }

    // Windows Message Constants (Original: uint_0, int_0, etc.)
    private const uint WM_KEYDOWN = 0x0100;    // uint_0
    private const uint WM_KEYUP = 0x0101;      // uint_1
    private const uint WM_CHAR = 0x0102;       // uint_2
    private const uint WM_SYSKEYDOWN = 0x0104;
    private const uint WM_SYSKEYUP = 0x0105;

    private const int WM_MOUSEMOVE = 0x0200;   // int_0
    private const int WM_LBUTTONDOWN = 0x0201; // int_1
    private const int WM_LBUTTONUP = 0x0202;   // int_2
    private const int WM_RBUTTONDOWN = 0x0204; // int_3
    private const int WM_RBUTTONUP = 0x0205;   // int_4

    private const uint MAPVK_VK_TO_VSC = 0;

    // State Variables
    public static int TapSpinDelay;            // Original: int_19
    public static int KeyDelay;                // Original: int_20
    public static bool UseClipboard;           // Original: bool_0
    public static bool IsCursorHooked = true;  // Original: bool_1
    public static bool IsGrabbingCursor = true;// Original: bool_2
    // Stored normalized cursor position (0..1)
    public static double double_0;
    public static double double_1;

    private static readonly GSpellTimer ActionTimer = new GSpellTimer(1000); // Original: gspellTimer_0
    private static Dictionary<short, short> ActiveKeys = new Dictionary<short, short>();                      // Original: dictionary_0
    private static readonly GameTimer AntiAfkTimer = new GameTimer(11000);   // Original: gclass36_0

    private static int LastMouseX; // Original: int_21
    private static int LastMouseY; // Original: int_22

    // P/Invoke Imports
    [DllImport("user32.dll")]
    private static extern bool ScreenToClient(IntPtr hWnd, ref Win32Point lpPoint); // Original: gstruct12_0

    [DllImport("kernel32.dll")]
    private static extern int GetCurrentThreadId();

    [DllImport("user32.dll")]
    private static extern bool AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);

    [DllImport("user32.dll")]
    private static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

    [DllImport("user32.dll")]
    private static extern uint MapVirtualKey(uint uCode, uint uMapType);

    [DllImport("user32.dll")]
    public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId); // Original: int_25

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out Win32Point lpPoint); // Original: gstruct12_0

    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    public static extern int SendInput(uint nInputs, ref HardwareInput pInputs, int cbSize); // Original: gstruct13_0

    [DllImport("user32.dll")]
    public static extern short VkKeyScan(char ch);

    // Safe wrapper for other code that expects an accessible VkKeyScan
    public static short VkKeyScanSafe(char ch) => VkKeyScan(ch);

    // Compatibility shims for decompiled callers
    public static void smethod_21(bool enable)
    {
        IsCursorHooked = enable;
    }

    public static void smethod_18(double nx, double ny)
    {
        double_0 = nx;
        double_1 = ny;
        try
        {
            var w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            var h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            var x = (int)(nx * w);
            var y = (int)(ny * h);
            SetCursorPos(x, y);
            LastMouseX = x;
            LastMouseY = y;
        }
        catch (Exception ex)
        {
            Logger.LogMessage("smethod_18 failed: " + ex.Message);
        }
    }

    public static void smethod_23(bool right)
    {
        try
        {
            if (right)
            {
                SendMouseEvent(8);
                Thread.Sleep(20);
                SendMouseEvent(16);
            }
            else
            {
                SendMouseEvent(2);
                Thread.Sleep(20);
                SendMouseEvent(4);
            }
        }
        catch (Exception ex)
        {
            Logger.LogMessage("smethod_23 failed: " + ex.Message);
        }
    }

    public static void smethod_28(string text)
    {
        // forward to shim which will send characters
        InputControllerShim.SendText(text);
    }

    public static void smethod_31(object cfg)
    {
        // no-op compatibility shim
    }

    public static void smethod_22(out double nx, out double ny)
    {
        try
        {
            System.Drawing.Point p;
            GetCursorPos(out var pt);
            p = new System.Drawing.Point(pt.X, pt.Y);
            var w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            var h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            nx = (double)p.X / w;
            ny = (double)p.Y / h;
        }
        catch
        {
            nx = 0.0;
            ny = 0.0;
        }
    }

    public static void smethod_6(char c)
    {
        try
        {
            short vk = VkKeyScanSafe(c);
            TapKey(vk);
        }
        catch (Exception ex)
        {
            Logger.LogMessage("smethod_6 failed: " + ex.Message);
        }
    }

    public static void smethod_4(char c, bool pressing)
    {
        try
        {
            short vk = VkKeyScanSafe(c);
            SendKey(vk, pressing);
        }
        catch (Exception ex)
        {
            Logger.LogMessage("smethod_4 failed: " + ex.Message);
        }
    }

    public static void smethod_27()
    {
        try
        {
            ReleaseAllModifiers();
        }
        catch (Exception ex)
        {
            Logger.LogMessage("smethod_27 failed: " + ex.Message);
        }
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    // Original: smethod_0
    public static int SendKey(short virtualKey, bool isKeyDown)
    {
        return SendKeyCore(virtualKey, isKeyDown, true);
    }

    // Original: smethod_1
    private static int SendKeyCore(short virtualKey, bool isKeyDown, bool trackKeyState)
    {
        if (trackKeyState)
        {
            lock (ActiveKeys)
            {
                if (isKeyDown && !ActiveKeys.ContainsKey(virtualKey))
                    ActiveKeys.Add(virtualKey, virtualKey);
                else if (!isKeyDown)
                    ActiveKeys.Remove(virtualKey);
            }
        }

        // Always attempt background PostMessage if initialized, regardless of focus.
        if (StartupClass.IsGliderInitialized && StartupClass.MainApplicationHandle != IntPtr.Zero)
        {
            uint msg = GetKeyMessage(virtualKey, isKeyDown);
            uint lParam = BuildKeyLParam(virtualKey, isKeyDown);

            bool ok = PostMessage(StartupClass.MainApplicationHandle, msg, (uint)(ushort)virtualKey, lParam);

            if (!ok)
            {
                Logger.LogMessage("[Input] PostMessage failed for VK=0x" + virtualKey.ToString("x"));
            }

            if (AntiAfkTimer.method_3()) // Keeps the bot from timing out
            {
                ReleaseAllModifiers(); // Original: smethod_2
                AntiAfkTimer.method_4();
            }
            return 1;
        }

        // Fallback for when game handle isn't attached (Hardware level)
        var input = new HardwareInput(); // Original: gstruct13_0
        input.Type = 1; // Keyboard
        input.Keyboard.Vk = virtualKey;
        if (!isKeyDown)
            input.Keyboard.Flags |= 2; // KEYEVENTF_KEYUP

        var result = SendInput(1U, ref input, Marshal.SizeOf(input));
        if (KeyDelay > 0)
            StartupClass.smethod_39(KeyDelay);
        return result;
    }

    // Original: smethod_2
    private static void ReleaseAllModifiers()
    {
        var input = new HardwareInput { Type = 1 };
        SendInput(1U, ref input, Marshal.SizeOf(input));
        Thread.Sleep(200);
        input.Keyboard.Flags |= 2;
        SendInput(1U, ref input, Marshal.SizeOf(input));
    }

    // Original: smethod_9
    public static void TapKey(short virtualKey)
    {
        SendKey(virtualKey, true);
        StartupClass.smethod_39(20); // Hold delay
        SendKey(virtualKey, false);
    }

    // Original: smethod_26
    public static int SendMouseEvent(int mouseFlag)
    {
        // Background clicking implementation
        if (StartupClass.IsGliderInitialized)
        {
            uint msg;
            uint wParam;
            switch (mouseFlag)
            {
                case 2: // WM_LBUTTONDOWN
                    msg = WM_LBUTTONDOWN; wParam = 1U; break;
                case 4: // WM_LBUTTONUP
                    msg = WM_LBUTTONUP; wParam = 0U; break;
                case 8: // WM_RBUTTONDOWN
                    msg = WM_RBUTTONDOWN; wParam = 2U; break;
                case 16: // WM_RBUTTONUP
                    msg = WM_RBUTTONUP; wParam = 0U; break;
                default:
                    throw new Exception("Unknown mouse flag.");
            }

            Win32Point pt = new Win32Point(LastMouseX, LastMouseY);
            ScreenToClient(StartupClass.MainApplicationHandle, ref pt);

            SendMessage(StartupClass.MainApplicationHandle, msg, wParam, BuildLParam((uint)pt.X, (uint)pt.Y));
            ActionTimer.Reset();
            return 1;
        }

        // Fallback Hardware Input
        var input = new HardwareInput();
        input.Type = 0; // Mouse
        input.Mouse.Flags = mouseFlag;
        return SendInput(1U, ref input, Marshal.SizeOf(input));
    }

    // Original: smethod_32
    private static uint BuildLParam(uint lowWord, uint highWord)
    {
        return (highWord << 16) | lowWord;
    }

    private static uint BuildKeyLParam(short vk, bool isKeyDown)
    {
        uint scanCode = MapVirtualKey((uint)(ushort)vk, MAPVK_VK_TO_VSC) & 0xFFu;
        uint lParam = 1u | (scanCode << 16);
        if (!isKeyDown)
            lParam |= 0xC0000000u;
        return lParam;
    }

    private static uint GetKeyMessage(short vk, bool isKeyDown)
    {
        bool altDown;
        lock (ActiveKeys) { altDown = ActiveKeys.ContainsKey(18); }
        if (!altDown) return isKeyDown ? WM_KEYDOWN : WM_KEYUP;
        return isKeyDown ? WM_SYSKEYDOWN : WM_SYSKEYUP;
    }

    // Original: GStruct12
    public struct Win32Point
    {
        public int X; // Original: int_0
        public int Y; // Original: int_1

        public Win32Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    // Original: GStruct13
    [StructLayout(LayoutKind.Explicit, Size = 28)]
    public struct HardwareInput
    {
        [FieldOffset(0)] public int Type; // Original: int_0
        [FieldOffset(4)] public KeyboardInput Keyboard; // Original: gstruct14_0
        [FieldOffset(4)] public MouseInput Mouse; // Original: gstruct15_0
    }

    // Original: GStruct14
    public struct KeyboardInput
    {
        public short Vk; // Original: short_0
        public short Scan; // Original: short_1
        public int Flags; // Original: int_0
        public int Time; // Original: int_1
        public int ExtraInfo; // Original: int_2
    }

    // Original: GStruct15
    public struct MouseInput
    {
        public int Dx; // Original: int_0
        public int Dy; // Original: int_1
        public int MouseData; // Original: int_2
        public int Flags; // Original: int_3
        public int Time; // Original: int_4
    }
}