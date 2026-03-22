// Decompiled with JetBrains decompiler
// Type: FlightPointManager
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
public class FlightPointManager
{
    public int int_0;
    public string[] string_0;

    public FlightPointManager()
    {
        int_0 = 0;
        string_0 = null;
    }

    public void method_0()
    {
        var num = GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("Language"), "Language");
        int_0 = GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("UIFlightpointCount"), "fpcount");
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
                    GProcessMemoryManipulator.ReadInt32(
                        GProcessMemoryManipulator.ReadInt32((index << 5) + MemoryOffsetTable.Instance.GetIntOffset("UIFlightpointBase"),
                            "fpbase") + 4 * num + MemoryOffsetTable.Instance.GetIntOffset("UIFlightpointName"), "fpname");
                if (int_29 != 0)
                {
                    string_0[index] = GProcessMemoryManipulator.ReadString(int_29, 150, "fpnamebytes");
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