// Decompiled with JetBrains decompiler
// Type: GClass63
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.IO;
using Glider.Common.Objects;

public class GClass63
{
    private int int_0;
    public SortedList<int, GClass64> sortedList_0;

    public GClass63()
    {
        sortedList_0 = new SortedList<int, GClass64>();
        var num = GClass18.gclass18_0.method_4("MySpells");
        method_1(method_5("SpellListBase"), "slb");
        method_1(method_5("SpellListRowCount"), "slrc");
        method_1(method_5("SpellListSub"), "sls");
        for (var index = 0; index < 1024; ++index)
        {
            var int_1 = method_1(num + index * 4, "sid");
            if (int_1 > 0)
                method_9(int_1);
        }

        method_4("Created SpellbookEx structure, initial count: " + sortedList_0.Keys.Count + " spells");
    }

    private byte[] method_0(int int_1, int int_2, string string_0)
    {
        return GProcessMemoryManipulator.smethod_17(int_1, int_2, string_0);
    }

    private int method_1(int int_1, string string_0)
    {
        return GProcessMemoryManipulator.smethod_11(int_1, string_0);
    }

    private string method_2(int int_1, string string_0)
    {
        return GProcessMemoryManipulator.smethod_9(int_1, 100, string_0);
    }

    private void method_3(string string_0)
    {
        Logger.smethod_1(string_0);
    }

    private void method_4(string string_0)
    {
        Logger.LogMessage(string_0);
    }

    private int method_5(string string_0)
    {
        return GClass18.gclass18_0.method_4(string_0);
    }

    public void method_6()
    {
        foreach (var key in sortedList_0.Keys)
            method_4(sortedList_0[key].ToString());
    }

    public GClass64 method_7(int int_1)
    {
        method_9(int_1);
        return sortedList_0[int_1];
    }

    public void method_8(int int_1)
    {
        for (var int_1_1 = 1; int_1_1 < 36864; ++int_1_1)
        {
            var gclass64 = method_10(int_1_1);
            if (gclass64.int_1 == int_1)
                method_4(gclass64.ToString());
        }
    }

    public void method_9(int int_1)
    {
        if (int_1 == 0 || sortedList_0.ContainsKey(int_1))
            return;
        var gclass64 = method_10(int_1);
        sortedList_0.Add(gclass64.int_0, gclass64);
    }

    private GClass64 method_10(int int_1)
    {
        var gclass64 = new GClass64(int_1);
        var numArray = method_16(int_1);
        if (numArray != null)
        {
            var int32_1 = BitConverter.ToInt32(numArray, method_5("SpellNameRLE"));
            if (int32_1 > 0)
                gclass64.string_0 = method_2(int32_1, "spname");
            var num1 = BitConverter.ToInt32(numArray, method_5("BuffTypeRLE"));
            if (num1 > 6)
            {
                method_3("Don't like bufftype for spell 0x" + int_1.ToString("x") + ": " + gclass64.gbuffType_0);
                num1 = -1;
            }

            gclass64.gbuffType_0 = (GBuffType)num1;
            gclass64.int_1 = BitConverter.ToInt32(numArray, method_5("SpellGroupRLE"));
            var int32_2 = BitConverter.ToInt32(numArray, method_5("SpellRankRLE"));
            if (int32_2 > 0)
            {
                var str = method_2(int32_2, "sprank");
                if (str != null && str.Length > 0)
                {
                    var num2 = str.LastIndexOf(' ');
                    int result;
                    if (num2 > 0 && int.TryParse(str.Substring(num2 + 1), out result))
                        gclass64.int_2 = result;
                }
            }
        }

        return gclass64;
    }

    public string method_11(int int_1)
    {
        method_9(int_1);
        return !sortedList_0.ContainsKey(int_1)
            ? "(no such spell 0x" + int_1.ToString("x") + ")"
            : sortedList_0[int_1].string_0;
    }

    public int method_12(int int_1, string string_0)
    {
        if (!sortedList_0.ContainsKey(int_1))
            return 0;
        var gclass64 = sortedList_0[int_1];
        var num = 0;
        foreach (var key in sortedList_0.Keys)
            if (sortedList_0[key].string_0 == gclass64.string_0)
            {
                sortedList_0[key].string_1 = string_0;
                ++num;
            }

        method_3("Assigned " + num + " spells to family");
        return num;
    }

    public int[] method_13(int int_1)
    {
        method_9(int_1);
        if (!sortedList_0.ContainsKey(int_1))
            return null;
        var string0 = sortedList_0[int_1].string_0;
        if (string0 == null)
        {
            method_4("! Could not find spell name for 0x" + int_1.ToString("x") + ", not good!");
            return null;
        }

        var intList = new List<int>();
        foreach (var gclass64 in sortedList_0.Values)
            if (gclass64.string_0 == string0)
                intList.Add(gclass64.int_0);
        return intList.ToArray();
    }

    public bool method_14()
    {
        var num1 = method_1(method_5("CooldownStart") + method_5("CooldownStep"), "cdstart");
        var num2 = GProcessMemoryManipulator.smethod_34();
        if (num1 != 0 && num1 % 2 == 0)
        {
            for (; num1 != 0 && num1 % 2 == 0; num1 = method_1(num1 + 4, "c1next"))
            {
                var num3 = method_1(num1 + method_5("CD_TicksAtCast"), "c1tac");
                var num4 = method_1(num1 + method_5("CD_DurationGCD"), "c1durgcd");
                if (num4 != 0)
                {
                    if (num3 + num4 > int_0 || int_0 == 0)
                        int_0 = num3 + num4;
                    if (num3 + num4 > num2)
                        return false;
                }
            }

            return true;
        }

        return num2 >= int_0 || int_0 == 0;
    }

    public bool method_15(int int_1)
    {
        if (int_1 == 0)
            return method_14();
        if (sortedList_0.ContainsKey(int_1))
            method_9(int_1);
        var gclass64 = sortedList_0[int_1];
        var num1 = method_1(method_5("CooldownStart") + method_5("CooldownStep"), "cdstart");
        var num2 = GProcessMemoryManipulator.smethod_34();
        if (num1 != 0 && num1 % 2 == 0)
        {
            while (num1 != 0 && num1 % 2 == 0)
            {
                var num3 = method_1(num1 + method_5("CD_SpellID"), "c1sid");
                var num4 = method_1(num1 + method_5("CD_TicksAtCast"), "c1tac");
                var num5 = method_1(num1 + method_5("CD_GroupID"), "c1sgid");
                var num6 = method_1(num1 + method_5("CD_ActiveDuration"), "c1adur");
                var num7 = method_1(num1 + method_5("CD_DurationSpell"), "c1dur1");
                var num8 = method_1(num1 + method_5("CD_DurationGroup"), "c1dur2");
                var num9 = method_1(num1 + method_5("CD_DurationGCD"), "c1durgcd");
                if (num6 == 1)
                {
                    if (num3 == gclass64.int_0)
                        return false;
                    num1 = method_1(num1 + 4, "c1next");
                }
                else
                {
                    if (num9 != 0)
                    {
                        if (num4 + num9 > int_0 || int_0 == 0)
                            int_0 = num4 + num9;
                        if (num4 + num9 > num2)
                            return false;
                    }

                    if ((num7 != 0 && gclass64.int_0 == num3 && num4 + num7 > num2) ||
                        (num8 != 0 && gclass64.int_1 == num5 && num5 != 0 && num4 + num8 > num2))
                        return false;
                    num1 = method_1(num1 + 4, "c1next");
                }
            }

            return true;
        }

        return num2 >= int_0 || int_0 == 0;
    }

    private byte[] method_16(int int_1)
    {
        var num1 = method_1(method_5("SpellListBase"), "slstart");
        var num2 = method_1(method_5("SpellListSub"), "slsbottom");
        var int_1_1 = method_1(num1 + (int_1 - num2) * 4, "satidx");
        if (int_1_1 == 0)
            return null;
        try
        {
            var numArray = method_17(int_1_1, method_5("SpellNameRLE") + 48);
            if (numArray.Length >= GClass18.gclass18_0.method_4("SpellNameRLE") + 4)
                return numArray;
            method_4("! Can't read spelldata for 0x" + int_1.ToString("x") + ", RLE data too short: 0x" +
                     numArray.Length.ToString("x") + " bytes");
            return null;
        }
        catch (Exception ex)
        {
            method_4("** Exception reading RLE: " + ex.Message + ex.StackTrace);
            return null;
        }
    }

    private byte[] method_17(int int_1, int int_2)
    {
        var memoryStream = new MemoryStream();
        var numArray = method_0(int_1, 276, "srled");
        if (numArray == null)
            throw new Exception("ReadBytes returned null for: " + int_1.ToString("x"));
        memoryStream.WriteByte(numArray[0]);
        var index1 = 1;
        while (memoryStream.Length < int_2)
        {
            memoryStream.WriteByte(numArray[index1]);
            if (numArray[index1 - 1] != numArray[index1])
            {
                ++index1;
            }
            else
            {
                for (int index2 = numArray[index1 + 1]; index2 > 0; --index2)
                    memoryStream.WriteByte(numArray[index1]);
                var index3 = index1 + 2;
                memoryStream.WriteByte(numArray[index3]);
                index1 = index3 + 1;
            }
        }

        return memoryStream.ToArray();
    }
}