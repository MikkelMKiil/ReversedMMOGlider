// Decompiled with JetBrains decompiler
// Type: GClass68
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Glider.Common.Objects;

public class GClass68
{
    private const double double_0 = 0.15;
    private const double double_1 = 120.0;
    private const double double_2 = 0.087266462599716474;
    private bool bool_0;
    private bool bool_1;
    private bool bool_2;
    private double double_3 = byte.MaxValue;
    private double double_4;
    private GSpellTimer gspellTimer_0;
    private int int_0;
    private int int_1;
    private int int_2;
    private int int_3;

    private void method_0(string string_0)
    {
        Logger.smethod_1(string_0);
    }

    public void method_1()
    {
        gspellTimer_0 = new GSpellTimer(GClass61.gclass61_0.method_3("MouseHoldMS"), true);
        bool_0 = false;
    }

    [SpecialName]
    public bool method_2()
    {
        lock (this)
        {
            return !bool_0;
        }
    }

    public void method_3(bool bool_3)
    {
        lock (this)
        {
            if (bool_2)
            {
                method_0("Stopping mouse spin, force = " + bool_3);
                bool_1 = true;
                bool_2 = false;
                GClass55.smethod_21(false);
            }

            method_6(bool_3);
        }
    }

    public void method_4(double double_5)
    {
        lock (this)
        {
            if (bool_2)
            {
                method_0("New heading received by mousespin helper, updating field");
                double_4 = double_5;
            }
            else
            {
                if (GContext.Main.Me.IsSitting)
                {
                    GContext.Main.Log("Mouse spin invoked while sitting, standing up first!");
                    GContext.Main.SendKey("Common.Sit");
                    Thread.Sleep(1211);
                }

                method_0("Starting mouse spin, new heading = " + double_5 + ", my heading = " + GPlayerSelf.Me.Heading);
                int_1 = GContext.Main.Me.GetHeadingAddress();
                method_11();
                bool_2 = true;
                bool_1 = false;
                double_4 = double_5;
                method_5();
            }
        }
    }

    private void method_5()
    {
        lock (this)
        {
            if (!bool_0)
            {
                bool_0 = true;
                method_0("Pushing down right button");
                GClass55.smethod_24(true);
                gspellTimer_0.Reset();
                Thread.Sleep(199);
            }
            else
            {
                method_0("Skipping button push, already down");
            }
        }
    }

    private void method_6(bool bool_3)
    {
        lock (this)
        {
            var flag = false;
            if (!bool_0)
                return;
            if (bool_3)
            {
                method_0("Waiting out timer on force call");
                flag = gspellTimer_0.WaitNoInterrupt();
            }

            if (gspellTimer_0.IsReady)
            {
                method_0("Releasing right button");
                bool_0 = false;
                GClass55.smethod_25(true);
            }

            if (flag)
                throw new ThreadInterruptedException();
        }
    }

    public void method_7()
    {
        lock (this)
        {
            if (!bool_0 || !bool_1 || !gspellTimer_0.IsReady)
                return;
            method_6(false);
            method_0("Releasing cursor in ConsiderReleaseButton, also");
            GClass55.smethod_21(false);
        }
    }

    public bool method_8(bool bool_3)
    {
        lock (this)
        {
            double num1 = GProcessMemoryManipulator.smethod_13(int_1, "Quickheading");
            var num2 = GContext.Main.Movement.CompareHeadings(num1, double_4);
            if (Math.Abs(num2) < 0.15)
            {
                method_3(false);
                return true;
            }

            var int_4 = method_10(Math.Abs(num2), bool_3) * double_3;
            if (int_4 > 120.0)
                int_4 = 120.0;
            if (num2 < 0.0)
                int_4 *= -1.0;
            var gspellTimer = new GSpellTimer(2000, false);
            method_0("Pulsing cursor: " + int_4 + " pixels");
            method_13((int)int_4, 0);
            var H1 = 0.0;
            while (!gspellTimer.IsReady)
            {
                H1 = GProcessMemoryManipulator.smethod_13(int_1, "QuickHeadingCheck");
                if (H1 == num1)
                    Thread.Sleep(6);
                else
                    break;
            }

            if (H1 == num1)
            {
                ++int_0;
                if (int_0 == 3)
                    throw new Exception("never able to change heading in mouse spin!");
                method_0("Restarting spin, heading did not change");
                method_3(false);
                method_4(double_4);
                return method_8(bool_3);
            }

            int_0 = 0;
            var num3 = 1.0 / Math.Abs(GContext.Main.Movement.CompareHeadings(H1, num1) / int_4);
            if (num3 > 50.0)
                double_3 = num3;
            var num4 = GContext.Main.Movement.CompareHeadings(H1, double_4);
            if ((num4 <= 0.0 || num2 >= 0.0) && (num4 >= 0.0 || num2 <= 0.0))
                if (Math.Abs(num4) >= 0.15)
                    goto label_21;
            method_3(false);
            return true;
        }

        label_21:
        return false;
    }

    [SpecialName]
    public bool method_9()
    {
        return bool_2;
    }

    private double method_10(double double_5, bool bool_3)
    {
        double num1;
        if (double_5 > Math.PI / 2.0)
            num1 = Math.PI / 4.0;
        if (double_5 > Math.PI / 4.0)
            num1 = Math.PI / 8.0;
        var num2 = Math.PI / 16.0;
        if (bool_3 && num2 > Math.PI / 16.0)
            num2 *= 0.66;
        if (!bool_3)
            num2 *= 0.44;
        return num2;
    }

    private void method_11()
    {
        method_12();
    }

    private void method_12()
    {
        int int_4;
        int int_5;
        double double_2;
        double double_3;
        if (!StartupClass.IsGliderInitialized)
        {
            method_15(out int_4, out int_5);
            if (int_4 == int_2 && int_5 == int_3)
            {
                method_0("Cursor hasn't moved since last time we moved it, should be over the game ok");
                return;
            }

            method_0("Cursor seems to have moved, our last coords are: " + int_2 + "," + int_3);
            GClass55.smethod_22(out double_2, out double_3);
            if (double_2 >= 0.0 && double_2 < 1.0 && double_3 >= 0.0 && double_3 < 1.0)
            {
                method_0("Cursor is already over the game window at " + double_2 + "/" + double_3 +
                         " relative, should be ok");
                int_2 = int_4;
                int_3 = int_5;
                return;
            }
        }
        else
        {
            GClass55.smethod_22(out double_2, out double_3);
        }

        method_0("Moving cursor to center of game window (Relative pos was: " + double_2 + "/" + double_3 + ")");
        GProcessMemoryManipulator.smethod_25(0.5, 0.5, out int_4, out int_5);
        method_14(int_4, int_5);
        Thread.Sleep(201);
    }

    private void method_13(int int_4, int int_5)
    {
        if (StartupClass.IsGliderInitialized)
        {
            GClass55.smethod_16(int_4, int_5);
        }
        else
        {
            method_15(out int_2, out int_3);
            method_14(int_2 + int_4, int_3 + int_5);
        }
    }

    private void method_14(int int_4, int int_5)
    {
        method_0("SetMousePosition: " + int_4 + "," + int_5);
        if (StartupClass.IsGliderInitialized)
            GClass55.smethod_17(int_4, int_5);
        else
            SetCursorPos(int_4, int_5);
        int_2 = int_4;
        int_3 = int_5;
    }

    private void method_15(out int int_4, out int int_5)
    {
        GStruct17 gstruct17_0;
        GetCursorPos(out gstruct17_0);
        int_4 = gstruct17_0.int_0;
        int_5 = gstruct17_0.int_1;
    }

    public void method_16(GGameCamera ggameCamera_0, float float_0)
    {
        var num1 = ggameCamera_0.Pitch - (double)float_0;
        method_12();
        var gspellTimer1 = new GSpellTimer(GClass61.gclass61_0.method_3("MouseHoldMS"), false);
        GClass55.smethod_24(false);
        Thread.Sleep(122);
        while (!gspellTimer1.IsReady)
        {
            var pitch = ggameCamera_0.Pitch;
            var num2 = pitch - (double)float_0;
            if ((num2 >= 0.0 || num1 <= 0.0) && (num2 <= 0.0 || num1 >= 0.0) && Math.Abs(num2) >= Math.PI / 36.0)
            {
                Logger.smethod_1("Delta on pulse: " + num2 + ", oldpitch: " + pitch);
                var int_5 = method_10(Math.Abs(num2), false) * double_3;
                if (int_5 > 120.0)
                    int_5 = 120.0;
                if (num2 > 0.0)
                    int_5 *= -1.0;
                Logger.smethod_1("Pulsing cursor: " + int_5);
                method_13(0, (int)int_5);
                var gspellTimer2 = new GSpellTimer(1000, false);
                while (!gspellTimer2.IsReady && ggameCamera_0.Pitch == (double)pitch)
                    Thread.Sleep(9);
                if (gspellTimer2.IsReady)
                    throw new Exception("Camera pitch never changed in SetCameraPitch!");
            }
            else
            {
                break;
            }
        }

        gspellTimer1.Wait();
        GClass55.smethod_25(false);
        Logger.LogMessage("SetCameraPitch all done!");
    }

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out GStruct17 gstruct17_0);

    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int int_4, int int_5);

    public struct GStruct17
    {
        public int int_0;
        public int int_1;

        public GStruct17(int int_2, int int_3)
        {
            int_0 = int_2;
            int_1 = int_3;
        }
    }
}