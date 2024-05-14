// Decompiled with JetBrains decompiler
// Type: GClass18
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections;

public class GClass18
{
    public static GClass18 gclass18_0;
    public SortedList sortedList_0;

    public GClass18()
    {
        gclass18_0 = this;
        sortedList_0 = new SortedList();
    }

    public void method_0()
    {
        sortedList_0.Clear();
    }

    public void method_1(string string_0, string string_1)
    {
        sortedList_0.Add(string_0, string_1);
    }

    public void method_2(string string_0, int int_0)
    {
        sortedList_0.Add(string_0, int_0);
    }

    public string method_3(string string_0)
    {
        if (sortedList_0.ContainsKey(string_0))
            return (string)sortedList_0[string_0];
        Logger.LogMessage(MessageProvider.smethod_2(314, string_0));
        return "";
    }

    public int method_4(string string_0)
    {
        if (sortedList_0.ContainsKey(string_0))
            return (int)sortedList_0[string_0];
        Logger.LogMessage(MessageProvider.smethod_2(314, string_0));
        return 0;
    }

    public bool method_5(string string_0)
    {
        return sortedList_0.ContainsKey(string_0);
    }
}