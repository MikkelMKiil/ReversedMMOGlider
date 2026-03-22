// Decompiled with JetBrains decompiler
// Type: Matrix3
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
public class Matrix3
{
    public float[,] float_0;

    public Matrix3()
    {
        float_0 = new float[3, 3];
    }

    public void method_0(int int_0)
    {
        for (var index1 = 0; index1 < 3; ++index1)
        for (var index2 = 0; index2 < 3; ++index2)
        {
            float_0[index1, index2] = GProcessMemoryManipulator.ReadFloat(int_0, "matrix3d");
            int_0 += 4;
        }
    }

    public Vector3 method_1(int int_0)
    {
        return new Vector3
        {
            float_0 = float_0[int_0, 0],
            float_1 = float_0[int_0, 1],
            float_2 = float_0[int_0, 2]
        };
    }

    public Matrix3 method_2()
    {
        var gclass4 = new Matrix3();
        for (var index1 = 0; index1 < 3; ++index1)
        for (var index2 = 0; index2 < 3; ++index2)
            gclass4.float_0[index1, index2] = float_0[index1, index2];
        return gclass4;
    }

    public void method_3()
    {
        var gclass4 = new Matrix3();
        gclass4.float_0[0, 0] = (float)(float_0[1, 1] * (double)float_0[2, 2] - float_0[1, 2] * (double)float_0[2, 1]);
        gclass4.float_0[1, 0] = (float)(float_0[1, 2] * (double)float_0[2, 0] - float_0[1, 0] * (double)float_0[2, 2]);
        gclass4.float_0[2, 0] = (float)(float_0[1, 0] * (double)float_0[2, 1] - float_0[1, 1] * (double)float_0[2, 0]);
        var num = 1f / (float)(float_0[0, 0] * (double)gclass4.float_0[0, 0] +
                               float_0[0, 1] * (double)gclass4.float_0[1, 0] +
                               float_0[0, 2] * (double)gclass4.float_0[2, 0]);
        gclass4.float_0[0, 1] = (float)(float_0[0, 2] * (double)float_0[2, 1] - float_0[0, 1] * (double)float_0[2, 2]);
        gclass4.float_0[0, 2] = (float)(float_0[0, 1] * (double)float_0[1, 2] - float_0[0, 2] * (double)float_0[1, 1]);
        gclass4.float_0[1, 1] = (float)(float_0[0, 0] * (double)float_0[2, 2] - float_0[0, 2] * (double)float_0[2, 0]);
        gclass4.float_0[1, 2] = (float)(float_0[0, 2] * (double)float_0[1, 0] - float_0[0, 0] * (double)float_0[1, 2]);
        gclass4.float_0[2, 1] = (float)(float_0[0, 1] * (double)float_0[2, 0] - float_0[0, 0] * (double)float_0[2, 1]);
        gclass4.float_0[2, 2] = (float)(float_0[0, 0] * (double)float_0[1, 1] - float_0[0, 1] * (double)float_0[1, 0]);
        float_0[0, 0] = gclass4.float_0[0, 0] * num;
        float_0[0, 1] = gclass4.float_0[0, 1] * num;
        float_0[0, 2] = gclass4.float_0[0, 2] * num;
        float_0[1, 0] = gclass4.float_0[1, 0] * num;
        float_0[1, 1] = gclass4.float_0[1, 1] * num;
        float_0[1, 2] = gclass4.float_0[1, 2] * num;
        float_0[2, 0] = gclass4.float_0[2, 0] * num;
        float_0[2, 1] = gclass4.float_0[2, 1] * num;
        float_0[2, 2] = gclass4.float_0[2, 2] * num;
    }

    public Vector3 method_4(Vector3 gclass2_0)
    {
        return new Vector3
        {
            float_0 = (float)(float_0[0, 0] * (double)gclass2_0.float_0 + float_0[1, 0] * (double)gclass2_0.float_1 +
                              float_0[2, 0] * (double)gclass2_0.float_2),
            float_1 = (float)(float_0[0, 1] * (double)gclass2_0.float_0 + float_0[1, 1] * (double)gclass2_0.float_1 +
                              float_0[2, 1] * (double)gclass2_0.float_2),
            float_2 = (float)(float_0[0, 2] * (double)gclass2_0.float_0 + float_0[1, 2] * (double)gclass2_0.float_1 +
                              float_0[2, 2] * (double)gclass2_0.float_2)
        };
    }
}