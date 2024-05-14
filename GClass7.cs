// Decompiled with JetBrains decompiler
// Type: GClass7
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Runtime.CompilerServices;
using System.Threading;

public class GClass7
{
    public static string[] string_0 = new string[13]
    {
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "10",
        "J",
        "Q",
        "K",
        "A"
    };

    public static string[] string_1 = new string[4]
    {
        "C",
        "D",
        "S",
        "H"
    };

    public bool bool_0;
    public GClass80 gclass80_0;
    private readonly int int_0;
    public int int_1;
    public int int_2;
    public int int_3;
    public int int_4;

    public GClass7(int int_5, GClass80 gclass80_1)
    {
        int_0 = int_5;
        gclass80_0 = gclass80_1;
        method_0();
    }

    public override string ToString()
    {
        return "Card @ 0x" + int_0.ToString("x") + " - " + string_0[int_1] + string_1[int_2] + ", coords: " + int_3 +
               "/" + int_4 + ", exposed: " + bool_0;
    }

    public void method_0()
    {
        var num1 = GProcessMemoryManipulator.smethod_11(int_0 + GClass41.int_11, "md1");
        int_2 = num1 / 13;
        int_1 = num1 % 13;
        int_3 = 0;
        int_4 = 0;
        var num2 = GProcessMemoryManipulator.smethod_11(int_0 + GClass41.int_14, "md2");
        int_3 = GProcessMemoryManipulator.smethod_11(num2 + GClass41.int_15, "md3");
        int_4 = GProcessMemoryManipulator.smethod_11(num2 + GClass41.int_16, "md4");
        bool_0 = GProcessMemoryManipulator.smethod_15(int_0 + GClass41.int_12, "md5") != 0;
    }

    // Removed the incomplete line at line 71

    public void method_1()
    {
        GClass55.smethod_18(int_3 / 800.0 + 0.045, int_4 / 500.0 + 0.01);
    }

    public void method_2()
    {
        method_1();
        GClass55.smethod_23(false);
    }

    public void method_3()
    {
        method_1();
        GClass55.smethod_23(false);
        Thread.Sleep(100);
        GClass55.smethod_23(false);
    }

    public void method_4()
    {
        gclass80_0.method_5();
    }

    [SpecialName]
    public bool method_5()
    {
        return string_0[int_1] == "2";
    }

    [SpecialName]
    public bool method_6()
    {
        return string_0[int_1] == "A";
    }

    [SpecialName]
    public bool method_7()
    {
        return string_0[int_1] == "K";
    }

    [SpecialName]
    public bool method_8()
    {
        return int_2 == 1 || int_2 == 3;
    }

    public bool method_9(GClass7 gclass7_0)
    {
        return gclass7_0 != null && gclass7_0.method_8() != method_8() && gclass7_0.int_1 == int_1 + 1;
    }

    public bool method_10(GClass7 gclass7_0)
    {
        if (gclass7_0 == null || gclass7_0.int_2 != int_2)
            return false;
        return (gclass7_0.method_6() && method_5()) || int_1 == gclass7_0.int_1 + 1;
    }
}