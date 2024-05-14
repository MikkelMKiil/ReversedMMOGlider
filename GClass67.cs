// Decompiled with JetBrains decompiler
// Type: GClass67
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Threading;
using Glider.Common.Objects;

public class GClass67
{
    public static bool bool_0;
    public static bool bool_1;
    public static string string_0 = "(unknown)";
    private static GClass8 gclass8_0;
    private static GClass8 gclass8_1;
    private static int int_0;
    private static int int_1;
    private static int int_2;
    private static int int_3;
    private static readonly int int_4 = 2500;

    public static void smethod_0()
    {
        bool_0 = false;
        bool_1 = false;
        gclass8_0 = GClass8.smethod_2("TradeFrame");
        gclass8_1 = GClass8.smethod_2("StaticPopup1");
        int_2 = 0;
        int_3 = 0;
    }

    public static void smethod_1()
    {
        if (!StartupClass.bool_13)
            return;
        if (gclass8_0.method_10())
        {
            if (!bool_0)
            {
                int_0 = Environment.TickCount;
                bool_0 = true;
                GClass37.smethod_0("Trade window is visible");
            }
        }
        else if (bool_0)
        {
            GClass37.smethod_0("Trade window is gone");
            bool_0 = false;
        }

        if (gclass8_1.method_10())
        {
            if (bool_1)
                return;
            int_1 = Environment.TickCount;
            bool_1 = true;
            var gclass8 = gclass8_1.method_6("StaticPopup1Text");
            if (gclass8 != null)
                string_0 = gclass8.method_3();
            GClass37.smethod_1("Static popup is visible: \"" + string_0 + "\"");
        }
        else
        {
            if (!bool_1)
                return;
            GClass37.smethod_1("Static popup is gone");
            bool_1 = false;
        }
    }

    public static void smethod_2()
    {
        if (!bool_0 && !bool_1)
            return;
        if (bool_0 && Environment.TickCount - int_0 > int_4)
            smethod_4("");
        if (!bool_1 || Environment.TickCount - int_1 <= int_4)
            return;
        smethod_4(string_0);
    }

    private static bool smethod_3(string string_1)
    {
        return string_1 != null && (string_1.ToLower().IndexOf(GClass30.smethod_1(871)) > -1 ||
                                    string_1.ToLower().IndexOf(GClass30.smethod_1(873)) > -1);
    }

    private static void smethod_4(string string_1)
    {
        if (smethod_3(string_1))
        {
            GClass37.smethod_0(GClass30.smethod_1(859));
        }
        else
        {
            GClass37.smethod_0(GClass30.smethod_1(856));
            if (GClass61.gclass61_0.method_5("AlertOnPopup"))
                GClass20.smethod_0("Whisper.wav");
            if (int_3 > 0 && Environment.TickCount - int_2 >= 120000)
                --int_3;
        }

        GContext.Main.ReleaseAllKeys();
        Thread.Sleep(StartupClass.random_0.Next() % 1000 + 1000);
        GClass55.smethod_9(27);
        Thread.Sleep(StartupClass.random_0.Next() % 800 + 500);
        if (smethod_3(string_1))
            return;
        ++int_3;
        int_2 = Environment.TickCount;
        if (int_3 < GClass61.gclass61_0.method_3("MaxPopups"))
            return;
        GClass37.smethod_0(GClass30.smethod_2(855, GClass61.gclass61_0.method_3("MaxPopups"), int_3));
        StartupClass.gclass73_0.method_21(true);
    }
}