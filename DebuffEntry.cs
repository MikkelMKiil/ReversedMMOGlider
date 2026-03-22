// Decompiled with JetBrains decompiler
// Type: DebuffEntry
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
public class DebuffEntry
{
    public bool bool_0;
    public DebuffType genum4_0;
    public int int_0;
    public string string_0;

    public DebuffEntry(int int_1, DebuffType genum4_1, string string_1, bool bool_1)
    {
        int_0 = int_1;
        genum4_0 = genum4_1;
        string_0 = string_1;
        bool_0 = bool_1;
    }

    public DebuffEntry()
    {
        int_0 = 0;
        genum4_0 = DebuffType.const_0;
        string_0 = "(unknown)";
        bool_0 = false;
    }
}