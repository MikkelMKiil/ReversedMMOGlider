// Decompiled with JetBrains decompiler
// Type: GClass75
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class GClass75
{
    public bool bool_0;
    private int int_0;
    private int int_1;
    private int int_2;
    private int int_3;
    internal int int_4;
    internal int int_5;
    internal int int_6;
    internal int int_7;
    internal int int_8;
    private readonly string string_0;
    public string string_1;

    public GClass75(string string_2)
    {
        string_0 = string_2;
        bool_0 = false;
        string_1 = "(no error)";
        int_2 = 0;
        int_3 = ushort.MaxValue;
        int_7 = GClass18.gclass18_0.method_4("CFStrings");
        int_8 = GClass18.gclass18_0.method_4("CFStringSize");
        var num = GClass18.gclass18_0.method_4("CFCounterBase");
        int_4 = num;
        int_5 = num + 4;
        int_6 = num + 8;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern int VirtualQueryEx(
        IntPtr intptr_0,
        int int_9,
        out GStruct20 gstruct20_0,
        int int_10);

    public bool method_0()
    {
        bool_0 = false;
        int_2 = 0;
        int_3 = ushort.MaxValue;
        int_0 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("UIParent"), "uiparent");
        if (int_0 == 0)
        {
            string_1 = "UIParent is null - no top object!";
            return false;
        }

        var num = int_0;
        do
        {
            var int_29 = GProcessMemoryManipulator.smethod_11(num + GClass18.gclass18_0.method_4("UIName"), "onameui");
            if (int_29 == 0 || !(GProcessMemoryManipulator.smethod_9(int_29, 200, "objectnamec1") == string_0))
                num = GProcessMemoryManipulator.smethod_11(num + GClass18.gclass18_0.method_4("UINext"), "onext");
            else
                goto label_5;
        } while (num != 0);

        goto label_6;
        label_5:
        int_1 = num;
        Logger.smethod_1(MessageProvider.smethod_2(815, string_0, int_1.ToString("x")));
        bool_0 = true;
        return true;
        label_6:
        string_1 = "Reached end of object list, never found: " + string_0;
        return false;
    }

    public string[] method_1()
    {
        try
        {
            return method_2();
        }
        catch (Exception ex)
        {
            Logger.LogMessage(MessageProvider.smethod_2(816, string_0, ex.Message, ex.StackTrace));
            bool_0 = false;
            return null;
        }
    }

    internal string[] method_2()
    {
        if (!bool_0 && !method_0())
            return null;
        var stringList = new List<string>();
        var num1 = GProcessMemoryManipulator.smethod_11(int_1 + int_4, "tablebomb");
        var num2 = GProcessMemoryManipulator.smethod_11(int_1 + int_5, "capacity");
        var num3 = GProcessMemoryManipulator.smethod_11(int_1 + int_6, "insertpos");
        if (num3 == int_3)
            return stringList.ToArray();
        int_3 = num3;
        var num4 = GProcessMemoryManipulator.smethod_11(int_1 + int_7, "tablebomb");
        if (num4 == 0)
        {
            string_1 = "Object has no pointer to bomb of strings!";
            return null;
        }

        var flag = false;
        var num5 = 0;
        var num6 = 0;
        if (num1 == num2)
            num5 = num3 + 1;
        while (num1 > 0)
        {
            if (num5 >= num2)
                num5 -= num2;
            num6 = GProcessMemoryManipulator.smethod_11(num4 + num5 * int_8, "chatlinepointer");
            if (num6 != 0)
            {
                if (flag)
                {
                    var str = GProcessMemoryManipulator.smethod_10(num6, 1024, "chatline3");
                    if (!(str == "(read failed)"))
                    {
                        stringList.Add(str);
                        int_2 = num6;
                    }
                    else
                    {
                        int_2 = num6;
                        Logger.smethod_1("! Chat read failed (" + GProcessMemoryManipulator.int_27 + ") for " + string_0 + " @ 0x" +
                                           num6.ToString("x") + ", ri/ca/ip=" + num5 + "/" + num2 + "/" + num3);
                        GStruct20 gstruct20_0;
                        if (VirtualQueryEx(StartupClass.AdditionalApplicationHandle, num6, out gstruct20_0, 28) != 28)
                        {
                            Logger.smethod_1("! VirtualQueryEx failed at 0x" + num6.ToString("x") +
                                               ", can't get more debug, last error = " + Marshal.GetLastWin32Error());
                            break;
                        }

                        Logger.smethod_1("! VQEx info: Base=" + gstruct20_0.int_0.ToString("x") + ", Size=0x" +
                                           gstruct20_0.int_2.ToString("x") + ", Flags=0x" +
                                           gstruct20_0.uint_2.ToString("x"));
                        break;
                    }
                }
                else
                {
                    flag = num6 == int_2;
                }

                --num1;
                ++num5;
            }
            else
            {
                break;
            }
        }

        if (!flag)
            int_2 = num6;
        return stringList.ToArray();
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct GStruct20
    {
        [FieldOffset(0)] public int int_0;
        [FieldOffset(4)] public int int_1;
        [FieldOffset(8)] public uint uint_0;
        [FieldOffset(12)] public int int_2;
        [FieldOffset(16)] public uint uint_1;
        [FieldOffset(20)] public uint uint_2;
        [FieldOffset(24)] public uint uint_3;
    }
}