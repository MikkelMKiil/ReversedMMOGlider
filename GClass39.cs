// Decompiled with JetBrains decompiler
// Type: GClass39
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
public class GClass39
{
    public int int_0;
    public long long_0;
    public string string_0;

    public GClass39(long long_1, int int_1, string string_1)
    {
        long_0 = long_1;
        string_0 = string_1;
        int_0 = int_1;
    }

    public string method_0()
    {
        return long_0.ToString("x") + ", ItemID=" + int_0.ToString("x") + ", Name=\"" + string_0 + "\"";
    }
}