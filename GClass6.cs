// Decompiled with JetBrains decompiler
// Type: GClass6
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using Glider.Common.Objects;

public class GClass6
{
    private const float float_0 = 0.0174532924f;

    public static bool smethod_0(
        GLocation glocation_0,
        double double_0,
        out double double_1,
        out double double_2)
    {
        double_1 = 0.0;
        double_2 = 0.0;
        var num1 = GProcessMemoryManipulator.smethod_11(
            GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("CameraBase"), "camerabase") +
            GClass18.gclass18_0.method_4("CameraOff1"), "camerasub");
        var gclass4 = new GClass4();
        gclass4.method_0(num1 + GClass18.gclass18_0.method_4("CC_ViewMatrix"));
        var gclass2_0_1 = GClass2.smethod_0(glocation_0);
        gclass2_0_1.float_2 += (float)double_0;
        var gclass2_1 = new GClass2();
        gclass2_1.method_0(num1 + GClass18.gclass18_0.method_4("CC_Position"));
        var gclass2_0_2 = GClass2.smethod_1(gclass2_0_1, gclass2_1);
        var num2 = GProcessMemoryManipulator.smethod_13(num1 + GClass18.gclass18_0.method_4("CC_FOV"), "camerafov");
        if (GClass2.smethod_2(gclass2_0_2, gclass4.method_1(0)) < 0.0)
        {
            GClass37.smethod_1("! Screen coord lookup failed, dotproduct is no good");
            return false;
        }

        gclass4.method_3();
        var gclass2_2 = gclass4.method_4(gclass2_0_2);
        var gclass2_3 = new GClass2(-gclass2_2.float_1, -gclass2_2.float_2, gclass2_2.float_0);
        if (gclass2_3.float_2 <= 0.0)
        {
            GClass37.smethod_1("! Screen coord lookup failed, cameraz is no good");
            return false;
        }

        var gstruct22 = GProcessMemoryManipulator.smethod_4();
        var num3 = gstruct22.method_1() / 2f;
        var num4 = gstruct22.method_0() / 2f;
        var num5 = num3 / (float)Math.Tan(num2 * 44.0 / 2.0 * (Math.PI / 180.0));
        var num6 = num4 / (float)Math.Tan(num2 * 35.0 / 2.0 * (Math.PI / 180.0));
        var num7 = (int)(gclass2_3.float_0 * (double)num5 / gclass2_3.float_2 + num3);
        var num8 = (int)(gclass2_3.float_1 * (double)num6 / gclass2_3.float_2 + num4);
        if (gstruct22.method_5(gstruct22.int_0 + num7, gstruct22.int_1 + num8))
        {
            double_1 = num7 / (double)gstruct22.method_1();
            double_2 = num8 / (double)gstruct22.method_0();
            return true;
        }

        GClass37.smethod_1("! Screen coord lookup failed, would be clicking out of window");
        return false;
    }
}