// Decompiled with JetBrains decompiler
// Type: GClass53
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common;

public class GClass53
{
    public GClass15 gclass15_0;
    public GClass29 gclass29_0;
    public int int_0;
    public string string_0;
    public WFConditionType wfconditionType_0;

    public GClass53(
        WFConditionType wfconditionType_1,
        int int_1,
        string string_1,
        GClass15 gclass15_1)
    {
        wfconditionType_0 = wfconditionType_1;
        int_0 = int_1;
        string_0 = string_1;
        gclass15_0 = gclass15_1;
        if (wfconditionType_1 != WFConditionType.CodeBytes && wfconditionType_1 != WFConditionType.DataBytes)
            gclass29_0 = null;
        else
            gclass29_0 = new GClass29(string_1);
    }

    public GClass53(
        WFConditionType wfconditionType_1,
        int int_1,
        GClass29 gclass29_1,
        GClass15 gclass15_1)
    {
        wfconditionType_0 = wfconditionType_1;
        int_0 = int_1;
        string_0 = gclass29_1.method_3();
        gclass15_0 = gclass15_1;
        gclass29_0 = gclass29_1;
    }
}