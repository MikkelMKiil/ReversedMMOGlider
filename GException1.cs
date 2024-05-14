// Decompiled with JetBrains decompiler
// Type: GException1
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;

public class GException1 : Exception
{
    public int int_0;
    public int int_1;
    public string string_0;

    public GException1(int int_2, int int_3, string string_1)
    {
        int_1 = int_2;
        int_0 = int_3;
        string_0 = string_1;
    }

    public override string ToString()
    {
        return "Addr=0x" + int_1.ToString("x") + ",Length=" + int_0 + ",DebugClue=" + string_0;
    }
}