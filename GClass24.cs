// Decompiled with JetBrains decompiler
// Type: GClass24
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Glider.Common;
using Glider.Common.Objects;

public class GClass24
{
    public delegate int GDelegate0(int int_0, int int_1, IntPtr intptr_0);

    public const int int_0 = 14;
    public const int int_1 = 13;
    private const int int_2 = 512;
    private const int int_3 = 513;
    private const int int_4 = 516;
    private const int int_5 = 519;
    private const int int_6 = 514;
    private const int int_7 = 517;
    private const int int_8 = 520;
    private const int int_9 = 515;
    private const int int_10 = 518;
    private const int int_11 = 521;
    private const int int_12 = 256;
    private const int int_13 = 257;
    private const int int_14 = 260;
    private const int int_15 = 261;
    public static bool bool_0 = true;
    public static IntPtr intptr_0 = IntPtr.Zero;
    private static int int_19;
    private static int int_20;
    public bool bool_1;
    public bool bool_2;
    private bool bool_3;
    private bool bool_4;
    public bool bool_5;
    public bool bool_6;
    private bool bool_7;
    public bool bool_8;
    private GDelegate0 gdelegate0_0;
    private GDelegate0 gdelegate0_1;
    public int int_16;
    public int int_17;
    protected int int_18;
    private KeyEventHandler keyEventHandler_0;
    private KeyEventHandler keyEventHandler_1;
    private KeyPressEventHandler keyPressEventHandler_0;
    private MouseEventHandler mouseEventHandler_0;
    public string string_0;

    public GClass24()
    {
        if (!GClass61.gclass61_0.method_5("UseHook"))
        {
            GClass37.smethod_0("Keyboard hook disabled");
            bool_0 = false;
        }
        else
        {
            method_0();
            method_16();
            keyEventHandler_0 += method_2;
            keyEventHandler_1 += method_1;
            keyPressEventHandler_0 += method_6;
        }
    }

    public void method_0()
    {
        bool_1 = GClass61.gclass61_0.method_2("UseMediaKeys") == "True";
        bool_2 = GClass61.gclass61_0.method_2("ControlKeys") == "True";
        int_16 = GClass61.gclass61_0.method_3("CommandKey1");
        int_17 = GClass61.gclass61_0.method_3("CommandKey2");
    }

    public void method_1(object sender, KeyEventArgs e)
    {
        if (bool_4)
        {
            bool_4 = false;
            e.Handled = true;
        }

        if (bool_2)
        {
            if ((e.KeyCode == (Keys)int_16 && int_18 == 0) || (e.KeyCode == (Keys)int_17 && int_18 == 1))
            {
                ++int_18;
                if (int_18 == 2)
                {
                    GClass20.smethod_0("Control.wav");
                    bool_3 = true;
                    int_18 = 0;
                }
            }
            else
            {
                int_18 = 0;
            }
        }

        if (bool_5 && !bool_6)
        {
            e.Handled = true;
            if (e.KeyCode == Keys.Return && string_0.Length > 0)
            {
                method_5();
                method_4();
            }
        }

        if (e.KeyCode != Keys.F1)
            return;
        GProcessMemoryManipulator.smethod_45();
    }

    public void method_2(object sender, KeyEventArgs e)
    {
        if (bool_1)
        {
            if (e.KeyCode == Keys.MediaStop && StartupClass.glideMode_0 != GlideMode.None)
            {
                StartupClass.bool_28 = false;
                StartupClass.smethod_27(false, "StopKeyFromHook");
            }

            if (e.KeyCode == Keys.MediaNextTrack && StartupClass.glideMode_0 == GlideMode.None)
                StartupClass.smethod_21(true);
            if (e.KeyCode == Keys.MediaPreviousTrack && StartupClass.glideMode_0 == GlideMode.None)
                StartupClass.smethod_23();
            if (e.KeyCode == Keys.MediaPlayPause && StartupClass.glideMode_0 == GlideMode.None)
                StartupClass.smethod_24(true);
        }

        if (bool_5 && !bool_6)
        {
            e.Handled = true;
            if (e.KeyCode == Keys.Escape)
            {
                method_4();
                return;
            }

            if (e.KeyCode == Keys.Back && string_0.Length > 0)
            {
                string_0 = string_0.Substring(0, string_0.Length - 1);
                method_7();
            }
        }

        if (bool_2)
        {
            if (bool_3)
            {
                bool_3 = false;
                if (e.KeyCode == (Keys)int_17)
                {
                    StartupClass.bool_28 = false;
                    StartupClass.smethod_27(false, "StopKeyFromHook2");
                }

                switch (e.KeyCode)
                {
                    case Keys.Return:
                        if (StartupClass.smethod_12())
                        {
                            method_3();
                            GClass37.smethod_1(GClass30.smethod_1(287));
                            GClass20.smethod_0("Key.wav");
                        }

                        break;
                    case Keys.D0:
                        method_20("LastProfile");
                        break;
                    case Keys.D1:
                        method_20("Profile1");
                        break;
                    case Keys.D2:
                        method_20("Profile2");
                        break;
                    case Keys.D3:
                        method_20("Profile3");
                        break;
                    case Keys.B:
                        if (StartupClass.smethod_12())
                        {
                            GClass37.smethod_0("Logging debuffs");
                            StartupClass.DebuffsKnown_string.method_11();
                            GClass20.smethod_0("Key.wav");
                        }

                        break;
                    case Keys.C:
                        method_21();
                        break;
                    case Keys.F:
                        if (StartupClass.smethod_12() && StartupClass.smethod_26()) GClass20.smethod_0("Key.wav");
                        break;
                    case Keys.G:
                        if (StartupClass.smethod_12() && StartupClass.smethod_24(true)) GClass20.smethod_0("Key.wav");
                        break;
                    case Keys.K:
                        if (StartupClass.smethod_12() && StartupClass.smethod_21(true)) GClass20.smethod_0("Key.wav");
                        break;
                    case Keys.M:
                        double double_2;
                        double double_3;
                        GClass55.smethod_22(out double_2, out double_3);
                        GClass37.smethod_0(GClass30.smethod_2(772, Math.Round(double_2, 3), Math.Round(double_3, 3)));
                        GClass20.smethod_0("Key.wav");
                        break;
                    case Keys.N:
                        if (StartupClass.smethod_12())
                        {
                            StartupClass.gprofile_0 = new GProfile();
                            StartupClass.string_5 = "Profiles\\NewProfile.xml";
                            StartupClass.ginterface0_0.imethod_0();
                            GClass20.smethod_0("Key.wav");
                        }

                        break;
                    case Keys.P:
                        if (StartupClass.smethod_12() && StartupClass.gprofile_0 != null)
                        {
                            StartupClass.gprofile_0.Factions = null;
                            GClass20.smethod_0("Key.wav");
                        }

                        break;
                    case Keys.Q:
                        if (StartupClass.smethod_12())
                        {
                            StartupClass.ginterface0_0.imethod_1();
                            GClass20.smethod_0("Key.wav");
                        }

                        break;
                    case Keys.S:
                        if (StartupClass.smethod_12() && StartupClass.gprofile_0 != null)
                        {
                            StartupClass.gprofile_0.Save(StartupClass.string_5);
                            GClass20.smethod_0("Key.wav");
                        }

                        break;
                    case Keys.W:
                        if (StartupClass.smethod_12() && StartupClass.smethod_23()) GClass20.smethod_0("Key.wav");
                        break;
                    case Keys.X:
                        if (bool_7)
                        {
                            StartupClass.smethod_34();
                            break;
                        }

                        GClass20.smethod_0("Key.wav");
                        bool_7 = true;
                        break;
                    case Keys.OemMinus:
                        if (StartupClass.smethod_12())
                        {
                            if (!StartupClass.smethod_12())
                            {
                                GClass37.smethod_0(GClass30.smethod_1(769));
                                return;
                            }

                            if (StartupClass.gprofile_0 != null)
                            {
                                if (GPlayerSelf.Me.IsDead)
                                {
                                    if (StartupClass.gprofile_0.GhostWaypoints.Count > 0)
                                    {
                                        GClass37.smethod_0(GClass30.smethod_2(767,
                                            StartupClass.gprofile_0.GhostWaypoints.Count - 1));
                                        StartupClass.gprofile_0.GhostWaypoints.RemoveAt(
                                            StartupClass.gprofile_0.GhostWaypoints.Count - 1);
                                        GClass20.smethod_0("Key.wav");
                                    }

                                    break;
                                }

                                if (StartupClass.gprofile_0.Waypoints.Count > 0)
                                {
                                    GClass37.smethod_0(GClass30.smethod_2(766,
                                        StartupClass.gprofile_0.Waypoints.Count - 1));
                                    StartupClass.gprofile_0.Waypoints.RemoveAt(StartupClass.gprofile_0.Waypoints.Count -
                                                                               1);
                                    GClass20.smethod_0("Key.wav");
                                }
                            }
                        }

                        break;
                    default:
                        GClass37.smethod_1("** Unknown command key: " + e.KeyCode);
                        break;
                }

                if (e.KeyCode != Keys.X)
                    bool_7 = false;
                e.Handled = true;
                bool_4 = true;
                return;
            }

            if (e.KeyCode != (Keys)int_16 && e.KeyCode != (Keys)int_17)
                int_18 = 0;
        }

        if (e.KeyCode != Keys.Escape)
            return;
        var foregroundWindow = GProcessMemoryManipulator.GetForegroundWindow();
        StartupClass.bool_21 = false;
        if (StartupClass.glideMode_0 != GlideMode.None && (foregroundWindow == intptr_0 ||
                                                           foregroundWindow == StartupClass.MainApplicationHandle ||
                                                           !StartupClass.IsGliderInitialized))
        {
            GClass37.smethod_1("Escape key picked up in hook, shutting action down");
            StartupClass.bool_28 = false;
            StartupClass.smethod_27(false, "EscapeFromHook");
        }

        if (!(foregroundWindow == intptr_0) || !StartupClass.GameMemoryWriter.method_1())
            return;
        GClass37.smethod_1("Killing background script from Escape key");
        StartupClass.GameMemoryWriter.method_0();
    }

    private void method_3()
    {
        bool_5 = true;
        string_0 = "";
        method_7();
    }

    private void method_4()
    {
        bool_5 = false;
    }

    private void method_5()
    {
        bool_5 = false;
        if (StartupClass.glideMode_0 == GlideMode.Auto)
            StartupClass.gclass73_0.method_23(string_0, false);
        StartupClass.ginterface0_0.imethod_0();
    }

    private void method_6(object sender, KeyPressEventArgs e)
    {
        if (!bool_5 || e.KeyChar < '\u001F')
            return;
        string_0 += (string)(object)e.KeyChar;
        e.Handled = true;
        method_7();
    }

    private void method_7()
    {
        StartupClass.ginterface0_0.imethod_0();
    }

    [SpecialName]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void method_8(MouseEventHandler mouseEventHandler_1)
    {
        mouseEventHandler_0 += mouseEventHandler_1;
    }

    [SpecialName]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void method_9(MouseEventHandler mouseEventHandler_1)
    {
        mouseEventHandler_0 -= mouseEventHandler_1;
    }

    [SpecialName]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void method_10(KeyEventHandler keyEventHandler_2)
    {
        keyEventHandler_0 += keyEventHandler_2;
    }

    [SpecialName]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void method_11(KeyEventHandler keyEventHandler_2)
    {
        keyEventHandler_0 -= keyEventHandler_2;
    }

    [SpecialName]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void method_12(KeyPressEventHandler keyPressEventHandler_1)
    {
        keyPressEventHandler_0 += keyPressEventHandler_1;
    }

    [SpecialName]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void method_13(KeyPressEventHandler keyPressEventHandler_1)
    {
        keyPressEventHandler_0 -= keyPressEventHandler_1;
    }

    [SpecialName]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void method_14(KeyEventHandler keyEventHandler_2)
    {
        keyEventHandler_1 += keyEventHandler_2;
    }

    [SpecialName]
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void method_15(KeyEventHandler keyEventHandler_2)
    {
        keyEventHandler_1 -= keyEventHandler_2;
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern int SetWindowsHookEx(
        int int_21,
        GDelegate0 gdelegate0_2,
        IntPtr intptr_1,
        int int_22);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern bool UnhookWindowsHookEx(int int_21);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern int CallNextHookEx(int int_21, int int_22, int int_23, IntPtr intptr_1);

    public void method_16()
    {
        if (int_19 == 0)
        {
            gdelegate0_0 = method_18;
            int_19 = SetWindowsHookEx(14, gdelegate0_0,
                Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
            if (int_19 == 0)
            {
                method_17();
                throw new Exception("SetWindowsHookEx failed.");
            }
        }

        if (int_20 != 0)
            return;
        gdelegate0_1 = method_19;
        int_20 = SetWindowsHookEx(13, gdelegate0_1,
            Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
        if (int_20 == 0)
        {
            method_17();
            throw new Exception("SetWindowsHookEx ist failed.");
        }
    }

    public void method_17()
    {
        if (int_19 != 0)
        {
            UnhookWindowsHookEx(int_19);
            int_19 = 0;
        }

        if (int_20 == 0)
            return;
        UnhookWindowsHookEx(int_20);
        int_20 = 0;
    }

    private int method_18(int int_21, int int_22, IntPtr intptr_1)
    {
        if (int_21 >= 0 && mouseEventHandler_0 != null)
        {
            var button = MouseButtons.None;
            switch (int_22)
            {
                case 513:
                    button = MouseButtons.Left;
                    break;
                case 516:
                    button = MouseButtons.Right;
                    break;
            }

            var clicks = 0;
            if (button != MouseButtons.None)
                clicks = int_22 == 515 || int_22 == 518 ? 2 : 1;
            var structure = (GClass26)Marshal.PtrToStructure(intptr_1, typeof(GClass26));
            mouseEventHandler_0(this,
                new MouseEventArgs(button, clicks, structure.gclass25_0.int_0, structure.gclass25_0.int_1, 0));
        }

        return CallNextHookEx(int_19, int_21, int_22, intptr_1);
    }

    [DllImport("user32")]
    public static extern int ToAscii(
        int int_21,
        int int_22,
        byte[] byte_0,
        byte[] byte_1,
        int int_23);

    [DllImport("user32")]
    public static extern int GetKeyboardState(byte[] byte_0);

    private int method_19(int int_21, int int_22, IntPtr intptr_1)
    {
        var flag = false;
        if (int_21 >= 0 && (keyEventHandler_0 != null || keyEventHandler_1 != null || keyPressEventHandler_0 != null))
        {
            var structure = (GClass27)Marshal.PtrToStructure(intptr_1, typeof(GClass27));
            if (structure.int_4 != 102)
            {
                if (keyEventHandler_0 != null && (int_22 == 256 || int_22 == 260))
                {
                    var int0 = (Keys)structure.int_0;
                    switch (int0)
                    {
                        case Keys.LShiftKey:
                        case Keys.RShiftKey:
                            bool_8 = true;
                            break;
                    }

                    var e = new KeyEventArgs(int0);
                    keyEventHandler_0(this, e);
                    if (e.Handled)
                        flag = true;
                }

                if (keyPressEventHandler_0 != null && int_22 == 256)
                {
                    var byte_0 = new byte[256];
                    GetKeyboardState(byte_0);
                    byte_0[16] = !bool_8 ? (byte)0 : (byte)128;
                    var byte_1 = new byte[2];
                    if (ToAscii(structure.int_0, structure.int_1, byte_0, byte_1, structure.int_2) == 1)
                    {
                        var e = new KeyPressEventArgs((char)byte_1[0]);
                        keyPressEventHandler_0(this, e);
                        if (e.Handled)
                            flag = true;
                    }
                }

                if (keyEventHandler_1 != null && (int_22 == 257 || int_22 == 261))
                {
                    var int0 = (Keys)structure.int_0;
                    switch (int0)
                    {
                        case Keys.LShiftKey:
                        case Keys.RShiftKey:
                            bool_8 = false;
                            break;
                    }

                    var e = new KeyEventArgs(int0);
                    keyEventHandler_1(this, e);
                    if (e.Handled)
                        flag = true;
                }
            }
            else
            {
                structure.int_4 = 0;
            }
        }

        return flag ? -1 : CallNextHookEx(int_20, int_21, int_22, intptr_1);
    }

    private void method_20(string string_1)
    {
        var string_11 = GClass61.gclass61_0.method_2(string_1);
        if (string_11 == null || !StartupClass.smethod_1(string_11))
            return;
        GClass37.smethod_0("Loaded profile: " + StartupClass.string_5);
        GClass20.smethod_0("Key.wav");
        StartupClass.ginterface0_0.imethod_0();
    }

    private void method_21()
    {
        var str = GClass61.gclass61_0.method_2("AppKey");
        GClass61.gclass61_0.method_3("Class");
        GClass61.gclass61_0.method_7(true);
        StartupClass.bool_29 = true;
        StartupClass.gclass24_0.method_0();
        GClass42.gclass42_0.method_12();
        GClass55.smethod_31(GClass61.gclass61_0);
        StartupClass.smethod_5();
        StartupClass.gclass54_0.method_0(GClass61.gclass61_0);
        if (str != GClass61.gclass61_0.method_2("AppKey") || StartupClass.gclass54_0.bool_4 || !StartupClass.bool_22)
        {
            StartupClass.gclass54_0.bool_4 = false;
            StartupClass.smethod_15();
            StartupClass.smethod_9();
        }

        GClass20.smethod_0("Key.wav");
    }

    [StructLayout(LayoutKind.Sequential)]
    public class GClass25
    {
        public int int_0;
        public int int_1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class GClass26
    {
        public GClass25 gclass25_0;
        public int int_0;
        public int int_1;
        public int int_2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class GClass27
    {
        public int int_0;
        public int int_1;
        public int int_2;
        public int int_3;
        public int int_4;
    }
}