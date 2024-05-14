// Decompiled with JetBrains decompiler
// Type: GClass2
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using Glider.Common.Objects;

public class GClass2
{
    public float float_0;
    public float float_1;
    public float float_2;

    public GClass2()
    {
        float_0 = 0.0f;
        float_1 = 0.0f;
        float_2 = 0.0f;
    }

    public GClass2(float float_3, float float_4, float float_5)
    {
        float_0 = float_3;
        float_1 = float_4;
        float_2 = float_5;
    }

    public override string ToString()
    {
        return "(Vector3D: " + Math.Round(float_0, 4) + "/" + Math.Round(float_1, 4) + "/" + Math.Round(float_2, 4) +
               ")";
    }


    public static GClass2 smethod_0(GLocation glocation_0)
    {
        return new GClass2
        {
            float_0 = glocation_0.X,
            float_1 = glocation_0.Y,
            float_2 = glocation_0.Z
        };
    }

    public static GClass2 smethod_1(GClass2 gclass2_0, GClass2 gclass2_1)
    {
        return new GClass2
        {
            float_0 = gclass2_0.float_0 - gclass2_1.float_0,
            float_1 = gclass2_0.float_1 - gclass2_1.float_1,
            float_2 = gclass2_0.float_2 - gclass2_1.float_2
        };
    }

    public static float smethod_2(GClass2 gclass2_0, GClass2 gclass2_1)
    {
        return (float)(gclass2_0.float_0 * (double)gclass2_1.float_0 + gclass2_0.float_1 * (double)gclass2_1.float_1 +
                       gclass2_0.float_2 * (double)gclass2_1.float_2);
    }


    public void method_0(int int_0)
    {
        float_0 = GProcessMemoryManipulator.smethod_13(int_0, "v3dx");
        float_1 = GProcessMemoryManipulator.smethod_13(int_0 + 4, "v3dy");
        float_2 = GProcessMemoryManipulator.smethod_13(int_0 + 8, "v3dz");
    }
}