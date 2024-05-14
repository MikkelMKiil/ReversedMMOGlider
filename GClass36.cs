// Decompiled with JetBrains decompiler
// Type: GClass36
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;

public class GClass36
{
    public int int_0;
    private int int_1;

    public GClass36(int int_2)
    {
        int_0 = int_2;
        int_1 = 0;
    }

    public GClass36(int int_2, int int_3)
    {
        int_0 = StartupClass.random_0.Next() % (int_3 - int_2);
        int_0 += int_2;
        method_4();
    }

    public int method_0()
    {
        return int_1 + int_0 - Environment.TickCount;
    }

    public int method_1()
    {
        return Environment.TickCount - int_1;
    }

    public void method_2()
    {
        var int_14 = method_0();
        if (int_14 <= 0)
            return;
        StartupClass.smethod_39(int_14);
    }

    public bool method_3()
    {
        return Environment.TickCount - int_1 > int_0;
    }

    public void method_4()
    {
        int_1 = Environment.TickCount;
    }

    public void method_5()
    {
        int_1 = Environment.TickCount - (int_0 + 1);
    }
}