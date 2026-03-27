using System;

#nullable disable

public class GameTimer
{
    public int int_0;
    private int int_1;

    public GameTimer(int int_2)
    {
        int_0 = int_2;
        int_1 = 0;
    }

    public GameTimer(int int_2, int int_3)
    {
        int_0 = StartupClass.RandomGenerator.Next() % (int_3 - int_2);
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
        StartupClass.SleepMilliseconds(int_14);
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