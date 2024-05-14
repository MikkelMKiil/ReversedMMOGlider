// Decompiled with JetBrains decompiler
// Type: GClass34
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Runtime.CompilerServices;

public class GClass34
{
    private bool bool_0;
    public GClass80 gclass80_0;
    public GClass80 gclass80_1;
    public GClass80[] gclass80_2;
    public GClass80[] gclass80_3;
    private readonly int int_0;

    public GClass34(int int_1)
    {
        int_0 = int_1;
        method_2();
    }

    [SpecialName]
    public bool method_0()
    {
        return bool_0;
    }

    [SpecialName]
    public void method_1(bool bool_1)
    {
        bool_0 = bool_1;
    }

    public void method_2()
    {
        gclass80_0 = new GClass80(GProcessMemoryManipulator.smethod_11(int_0 + GClass41.int_8, "md30"), this);
        gclass80_1 = new GClass80(GProcessMemoryManipulator.smethod_11(int_0 + GClass41.int_5, "md31"), this);
        gclass80_2 = new GClass80[4];
        var num1 = GProcessMemoryManipulator.smethod_11(int_0 + GClass41.int_7, "md20");
        for (var index = 0; index < 4; ++index)
        {
            var int_3 = GProcessMemoryManipulator.smethod_11(num1 + index * 4, "md21");
            gclass80_2[index] = new GClass80(int_3, this);
        }

        gclass80_3 = new GClass80[7];
        var num2 = GProcessMemoryManipulator.smethod_11(int_0 + GClass41.int_6, "md22");
        for (var index = 0; index < 7; ++index)
        {
            var int_3 = GProcessMemoryManipulator.smethod_11(num2 + index * 4, "md23");
            gclass80_3[index] = new GClass80(int_3, this);
        }
    }

    public void method_3()
    {
        Logger.LogMessage("SolitaireGame @ 0x" + int_0.ToString("x"));
        gclass80_0.method_1("Deck");
        gclass80_1.method_1("Draw");
        for (var index = 0; index < 4; ++index)
            gclass80_2[index].method_1("Suit" + index);
        for (var index = 0; index < 7; ++index)
            gclass80_3[index].method_1("InPlay" + index);
    }

    [SpecialName]
    public bool method_4()
    {
        foreach (var gclass80 in gclass80_2)
            if (gclass80.gclass7_0.Length != 13)
                return false;
        return true;
    }
}