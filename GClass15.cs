// Decompiled with JetBrains decompiler
// Type: GClass15
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections.Generic;

public class GClass15
{
    public List<GClass53> list_0;
    public string string_0;
    public string string_1;

    public GClass15(string string_2, string string_3)
    {
        string_0 = string_2;
        string_1 = string_3;
        list_0 = new List<GClass53>();
    }

    public void method_0(GClass53 gclass53_0)
    {
        list_0.Add(gclass53_0);
    }
}