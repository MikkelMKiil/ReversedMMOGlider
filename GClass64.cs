// Decompiled with JetBrains decompiler
// Type: GClass64
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common.Objects;

public class GClass64
{
    public bool bool_0;
    public GBuffType gbuffType_0;
    public int int_0;
    public int int_1;
    public int int_2;
    public string string_0;
    public string string_1;

    public GClass64(int int_3)
    {
        int_0 = int_3;
        string_0 = "(no name)";
        int_1 = 0;
        int_2 = 0;
        string_1 = "(no family)";
        gbuffType_0 = GBuffType.NotSpecified;
        bool_0 = false;
    }

    public override string ToString()
    {
        return "[SPX 0x" + int_0.ToString("x") + ": \"" + string_0 + "\", Rank=" + int_2 + ", Group=0x" +
               int_1.ToString("x") + ", BuffType=" + gbuffType_0 + ", Family=" + string_1 + "]";
    }
}