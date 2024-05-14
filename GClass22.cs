// Decompiled with JetBrains decompiler
// Type: GClass22
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
public class GClass22
{
    public bool bool_0;
    public bool bool_1;
    public GEnum1 genum1_0;
    public object object_0;
    public string string_0;
    public string string_1;

    public GClass22(string string_2)
    {
        genum1_0 = GEnum1.const_0;
        object_0 = null;
        string_1 = null;
        bool_0 = false;
        bool_1 = false;
        string_0 = string_2;
    }

    public override string ToString()
    {
        return string_0;
    }

    public void method_0()
    {
        if (object_0 != null)
            return;
        GClass74.smethod_9(this);
    }
}