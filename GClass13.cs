// Decompiled with JetBrains decompiler
// Type: GClass13
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
public class GClass13
{
    public int int_0;
    public string[] string_0;

    public GClass13()
    {
        int_0 = 0;
        string_0 = null;
    }

    public void method_0()
    {
        var num = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("Language"), "Language");
        int_0 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("UIFlightpointCount"), "fpcount");
        Logger.smethod_1("Flight point count: " + int_0);
        if (int_0 == 0)
        {
            string_0 = null;
        }
        else
        {
            string_0 = new string[int_0];
            for (var index = 0; index < int_0; ++index)
            {
                var int_29 =
                    GProcessMemoryManipulator.smethod_11(
                        GProcessMemoryManipulator.smethod_11((index << 5) + GClass18.gclass18_0.method_4("UIFlightpointBase"),
                            "fpbase") + 4 * num + GClass18.gclass18_0.method_4("UIFlightpointName"), "fpname");
                if (int_29 != 0)
                {
                    string_0[index] = GProcessMemoryManipulator.smethod_9(int_29, 150, "fpnamebytes");
                    Logger.smethod_1(index + " = \"" + string_0[index] + "\"");
                }
                else
                {
                    string_0[index] = "";
                    Logger.smethod_1(index + " = (null flightpoint name!)");
                }
            }
        }
    }

    public int method_1(string string_1)
    {
        for (var index = 0; index < int_0; ++index)
            if (string_0[index].ToLower().Contains(string_1.ToLower()))
                return index + 1;
        Logger.LogMessage("! Could not find \"" + string_1 + "\" in list of flightpoints!");
        return 0;
    }
}