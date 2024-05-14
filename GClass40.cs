// Decompiled with JetBrains decompiler
// Type: GClass40
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

public class GClass40
{
    private int int_0;

    public void method_0()
    {
        int_0 = 100;
        StartupClass.smethod_22();
        Thread.Sleep(500);
        var num = smethod_0(StartupClass.AdditionalApplicationHandle, "Solitaire.exe");
        if (num == 0)
            StartupClass.smethod_27(false, "NoSolitaireModuleInSolitaire");
        var int_1 = GProcessMemoryManipulator.smethod_11(num + GClass41.int_0, "md0");
        if (int_1 == 0)
            StartupClass.smethod_27(false, "NoGameInSolitaire");
        var gclass34_0 = new GClass34(int_1);
        while (method_3(gclass34_0))
        {
            Thread.Sleep(int_0);
            gclass34_0.method_2();
            if (gclass34_0.method_4())
            {
                method_1("Won game, woo!");
                break;
            }
        }
    }

    private void method_1(string string_0)
    {
        GClass37.smethod_0(string_0);
    }

    private void method_2(string string_0)
    {
        GClass37.smethod_1(string_0);
    }

    private bool method_3(GClass34 gclass34_0)
    {
        GClass37.smethod_1("--- DoGameMove invoked");
        if (method_4(gclass34_0) || method_5(gclass34_0) || method_8(gclass34_0) || method_6(gclass34_0) ||
            method_7(gclass34_0) || method_9(gclass34_0))
            return true;
        if (!gclass34_0.method_0())
        {
            method_1("Haven't made any successful plays on this run through, giving up");
            return false;
        }

        gclass34_0.gclass80_0.method_3();
        Thread.Sleep(1000);
        gclass34_0.method_2();
        if (gclass34_0.gclass80_0.gclass7_0.Length > 0)
        {
            method_1("Got more cards in deck, continuing on!");
            gclass34_0.method_1(false);
            return true;
        }

        method_1("All moves exhausted, deck won't refill, giving up");
        return false;
    }

    private bool method_4(GClass34 gclass34_0)
    {
        var gclass7_1 = gclass34_0.gclass80_1.method_4();
        if (gclass7_1 != null && gclass7_1.method_6())
        {
            method_2("TryAceToSuitStack likes top card in draw");
            gclass7_1.method_3();
            return true;
        }

        for (var index = 0; index < 7; ++index)
        {
            var gclass7_2 = gclass34_0.gclass80_3[index].method_4();
            if (gclass7_2 != null && gclass7_2.method_6())
            {
                method_2("TryAceToSuitStack likes top card in stack #" + index);
                gclass7_2.method_3();
                gclass7_2.method_4();
                return true;
            }
        }

        method_2("TryAceToSuitStack found nothing");
        return false;
    }

    private bool method_5(GClass34 gclass34_0)
    {
        for (var index1 = 6; index1 >= 0; --index1)
        {
            var gclass80_1 = gclass34_0.gclass80_3[index1];
            var gclass7 = gclass80_1.method_8();
            if (gclass7 != null)
                for (var index2 = 0; index2 < 7; ++index2)
                {
                    var gclass80_2 = gclass34_0.gclass80_3[index2];
                    if (gclass80_2 != gclass80_1 && gclass80_2.gclass7_0.Length > 0)
                    {
                        var gclass7_0 = gclass80_2.method_4();
                        if (gclass7.method_9(gclass7_0))
                        {
                            method_2("Moving card from stack #" + index1 + " to stack #" + index2);
                            gclass7.method_2();
                            Thread.Sleep(int_0);
                            gclass7_0.method_2();
                            gclass7.method_4();
                            return true;
                        }
                    }
                }
        }

        method_2("TryMainToMain found nothing");
        return false;
    }

    private bool method_6(GClass34 gclass34_0)
    {
        for (var index1 = 6; index1 >= 0; --index1)
        {
            var gclass80_1 = gclass34_0.gclass80_3[index1];
            var gclass7 = gclass80_1.method_8();
            if (gclass7 != null && gclass7.method_7() && gclass80_1.method_6() > 0)
                for (var index2 = 0; index2 < 7; ++index2)
                {
                    var gclass80_2 = gclass34_0.gclass80_3[index2];
                    if (gclass80_2 != gclass80_1 && gclass80_2.gclass7_0.Length == 0)
                    {
                        method_2("Moving king from stack #" + index1 + " to empty stack #" + index2);
                        gclass7.method_2();
                        Thread.Sleep(int_0);
                        gclass80_2.method_3();
                        gclass7.method_4();
                        return true;
                    }
                }
        }

        method_2("TryMainKingToEmpty found nothing");
        return false;
    }

    private bool method_7(GClass34 gclass34_0)
    {
        var gclass7 = gclass34_0.gclass80_1.method_4();
        if (gclass7 == null)
            return false;
        for (var index = 0; index < 7; ++index)
            if ((gclass7.method_7() && gclass34_0.gclass80_3[index].gclass7_0.Length == 0) ||
                gclass7.method_9(gclass34_0.gclass80_3[index].method_4()))
            {
                method_2("Moving draw card to stack #" + index);
                gclass7.method_2();
                Thread.Sleep(int_0);
                if (gclass7.method_7())
                    gclass34_0.gclass80_3[index].method_3();
                else
                    gclass34_0.gclass80_3[index].method_4().method_2();
                gclass7.method_4();
                return true;
            }

        return false;
    }

    private bool method_8(GClass34 gclass34_0)
    {
        var gclass7_1 = gclass34_0.gclass80_1.method_4();
        for (var index1 = 0; index1 < 4; ++index1)
        {
            var gclass7_0 = gclass34_0.gclass80_2[index1].method_4();
            if (gclass7_0 != null)
            {
                if (gclass7_1 == null || !gclass7_1.method_10(gclass7_0))
                {
                    for (var index2 = 0; index2 < 7; ++index2)
                    {
                        var gclass7_2 = gclass34_0.gclass80_3[index2].method_4();
                        if (gclass7_2 != null && gclass7_2.method_10(gclass7_0))
                        {
                            method_1("Moving stacked card home: " + gclass7_2);
                            gclass7_2.method_2();
                            Thread.Sleep(int_0);
                            gclass7_0.method_2();
                            gclass7_2.method_4();
                            return true;
                        }
                    }
                }
                else
                {
                    method_1("Moving draw card home: " + gclass7_1);
                    gclass7_1.method_2();
                    Thread.Sleep(int_0);
                    gclass7_0.method_2();
                    gclass7_1.method_4();
                    return true;
                }
            }
        }

        return false;
    }

    private bool method_9(GClass34 gclass34_0)
    {
        if (gclass34_0.gclass80_0.gclass7_0.Length <= 0)
            return false;
        method_1("TryGetMoreDraw wants more cards");
        gclass34_0.gclass80_0.method_3();
        return true;
    }

    [DllImport("psapi.dll", SetLastError = true)]
    public static extern bool EnumProcessModules(
        IntPtr intptr_0,
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] [In] [Out]
        uint[] uint_0,
        uint uint_1,
        [MarshalAs(UnmanagedType.U4)] out uint uint_2);

    [DllImport("psapi.dll", SetLastError = true)]
    public static extern uint GetModuleFileNameEx(
        IntPtr intptr_0,
        IntPtr intptr_1,
        [Out] StringBuilder stringBuilder_0,
        uint uint_0);

    public static int smethod_0(IntPtr intptr_0, string string_0)
    {
        var uint_0 = new uint[1024];
        uint uint_2;
        if (!EnumProcessModules(intptr_0, uint_0, 4096U, out uint_2))
        {
            GClass37.smethod_0("EnumProcessModules failed!  Last error = " + Marshal.GetLastWin32Error());
            return 0;
        }

        var num = (int)(uint_2 / 4U);
        var stringBuilder_0 = new StringBuilder(200);
        for (var index = 0; index < num; ++index)
        {
            var moduleFileNameEx = (int)GetModuleFileNameEx(intptr_0, new IntPtr(uint_0[index]), stringBuilder_0, 200U);
            GClass37.smethod_0("0x" + uint_0[index].ToString("x") + " = \"" + stringBuilder_0 + "\"");
            if (stringBuilder_0.ToString().ToLower().EndsWith(string_0.ToLower()))
                return (int)uint_0[index];
        }

        GClass37.smethod_0("GetModuleAddress never found what we wanted!");
        return 0;
    }
}