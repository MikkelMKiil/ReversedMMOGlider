// Decompiled with JetBrains decompiler
// Type: GClass19
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Glider.Common;

public class GClass19
{
    public List<GClass15> list_0;
    public SortedList<string, GClass29> sortedList_0;

    public GClass19()
    {
        list_0 = new List<GClass15>();
        sortedList_0 = new SortedList<string, GClass29>();
    }

    public GClass15[] method_0(string string_0)
    {
        var gclass15List = new List<GClass15>();
        foreach (var gclass15 in list_0)
            if (gclass15.string_0 == string_0)
                gclass15List.Add(gclass15);
        return gclass15List.Count != 0
            ? gclass15List.ToArray()
            : throw new Exception("GetDiagram failed, looking for \"" + string_0 + "\"!");
    }

    public static int smethod_0(MemoryStream memoryStream_0)
    {
        var buffer = new byte[4];
        memoryStream_0.Read(buffer, 0, 4);
        return BitConverter.ToInt32(buffer, 0);
    }

    private static string smethod_1(MemoryStream memoryStream_0)
    {
        var numArray = new byte[3];
        memoryStream_0.Read(numArray, 0, 3);
        return Encoding.ASCII.GetString(numArray);
    }

    public void method_1(byte[] byte_0)
    {
        var memoryStream_0 = new MemoryStream(byte_0);
        var num1 = smethod_0(memoryStream_0) - 1393;
        for (var index1 = 0; index1 < num1; ++index1)
        {
            var gclass15_1 = new GClass15(smethod_1(memoryStream_0), "default " + index1);
            var num2 = smethod_0(memoryStream_0) - 1633;
            for (var index2 = 0; index2 < num2; ++index2)
            {
                var wfconditionType_1 = (WFConditionType)(smethod_0(memoryStream_0) & byte.MaxValue);
                var int_1 = smethod_0(memoryStream_0);
                if (wfconditionType_1 != WFConditionType.DataBytes && wfconditionType_1 != WFConditionType.CodeBytes)
                {
                    var string_1 = smethod_1(memoryStream_0);
                    var gclass53 = new GClass53(wfconditionType_1, int_1, string_1, gclass15_1);
                    gclass15_1.list_0.Add(gclass53);
                }
                else
                {
                    var gclass29_1 = new GClass29(memoryStream_0);
                    var gclass53 = new GClass53(wfconditionType_1, int_1, gclass29_1, gclass15_1);
                    gclass15_1.list_0.Add(gclass53);
                }
            }

            list_0.Add(gclass15_1);
        }
    }
}