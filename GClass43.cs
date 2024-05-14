// Decompiled with JetBrains decompiler
// Type: GClass43
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections;

public class GClass43
{
    private readonly int int_0;
    private readonly SortedList sortedList_0;
    private string string_0;

    public GClass43(string string_1, int int_1)
    {
        string_0 = string_1;
        int_0 = int_1;
        sortedList_0 = new SortedList();
        method_0();
    }

    public void method_0()
    {
        var int0 = int_0;
        var num = 0;
        while (true)
        {
            var int_29 = GProcessMemoryManipulator.smethod_11(int0, "DescriptorStringPtr");
            if ((int_29 & 1073741824) != 1073741824)
            {
                var key = GProcessMemoryManipulator.smethod_9(int_29, 64, "DescriptorString");
                switch (key)
                {
                    case null:
                        goto label_7;
                    case "OBJECT_FIELD_GUID":
                        if (num > 2)
                            goto label_8;
                        break;
                }

                if (!sortedList_0.ContainsKey(key))
                    sortedList_0.Add(key, num * 4);
                int0 += 20;
                ++num;
            }
            else
            {
                goto label_9;
            }
        }

        label_7:
        return;
        label_8:
        return;
        label_9: ;
    }

    public int method_1(string string_1)
    {
        return sortedList_0.ContainsKey(string_1) ? (int)sortedList_0[string_1] : 0;
    }
}