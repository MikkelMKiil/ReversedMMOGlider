// Decompiled with JetBrains decompiler
// Type: GClass46
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using Glider.Common;

public class GClass46
{
    private readonly GClass0 gclass0_0;
    private GClass19 gclass19_0;
    public GEnum5 genum5_0;
    private int int_0;
    private int int_1;
    private int int_2;
    public string string_0 = "(no error)";
    public string string_1;

    public GClass46(GClass0 gclass0_1)
    {
        gclass0_0 = gclass0_1;
        int_2 = 0;
        string_1 = "";
    }

    private void method_0(string string_2, GEnum5 genum5_1)
    {
        GClass0.smethod_1("Failed to status: " + (int)genum5_1 + ", last error = " + string_2);
        string_0 = string_2;
        genum5_0 = genum5_1;
    }

    public void method_1(GClass19 gclass19_1)
    {
        string_0 = "(no error)";
        string_1 = "";
        genum5_0 = GEnum5.const_0;
        gclass19_0 = gclass19_1;
        try
        {
            method_2();
        }
        catch (Exception ex)
        {
            method_0(ex.Message + "\r\n" + ex.StackTrace, GEnum5.const_0);
        }
    }

    private void method_2()
    {
        if (int_2 == 0)
            method_0("No ClassTop defined, do FindCore first, dummy!", GEnum5.const_0);
        else if (method_3(gclass19_0.list_0[0].string_0, int_2))
            genum5_0 = GEnum5.const_1;
        else
            genum5_0 = GEnum5.const_2;
    }

    private bool method_3(string string_2, int int_3)
    {
        var flag = false;
        GClass0.smethod_0("FireMatch, Name=\"" + string_2 + "\", origin = 0x" + int_3.ToString("x"));
        ++int_0;
        if (int_0 > 50)
            throw new Exception("Recursing too much, don't be stupid!");
        foreach (var gclass15_0 in gclass19_0.method_0(string_2))
            if (!method_4(gclass15_0, int_3))
            {
                string_1 = string_1 + "(-" + gclass15_0.string_0;
            }
            else
            {
                string_1 = string_1 + "(" + gclass15_0.string_0;
                flag = true;
                if (int_0 >= 2)
                {
                    GClass0.smethod_0("Matched " + gclass15_0.string_0 + " on: " + gclass15_0.string_1);
                }

                break;
            }

        --int_0;
        return flag;
    }

    private bool method_4(GClass15 gclass15_0, int int_3)
    {
        GClass0.smethod_0("Match diagram, name=" + gclass15_0.string_0 + ", note=\"" + gclass15_0.string_1 +
                          "\", origin = 0x" + int_3.ToString("x"));
        foreach (var gclass53_0 in gclass15_0.list_0)
            if (!method_5(gclass53_0, int_3))
            {
                GClass0.smethod_0("Mismatch on: " + gclass53_0);
                return false;
            }

        return true;
    }

    private bool method_5(GClass53 gclass53_0, int int_3)
    {
        GClass0.smethod_0("Match condition: " + gclass53_0 + ", origin = 0x" + int_3.ToString("x"));
        try
        {
            if (gclass53_0.wfconditionType_0 == WFConditionType.Reference)
            {
                var int_3_1 = gclass0_0.method_4(int_3 + gclass53_0.int_0);
                return method_3(gclass53_0.string_0, int_3_1);
            }

            if (gclass53_0.wfconditionType_0 == WFConditionType.Call)
            {
                GClass0.smethod_0("Looking for call start @ 0x" + (int_3 + gclass53_0.int_0).ToString("x"));
                var num = gclass0_0.method_3(int_3 + gclass53_0.int_0);
                if (num != 232)
                {
                    GClass0.smethod_0("Expected a call @ " + (int_3 + gclass53_0.int_0) + ", got byte 0x" +
                                      num.ToString("x") + " instead");
                    return false;
                }

                var int_3_2 = gclass0_0.method_4(int_3 + gclass53_0.int_0 + 1) + int_3 + gclass53_0.int_0 + 5;
                GClass0.smethod_0("Jump is to: 0x" + int_3_2.ToString("x"));
                return method_3(gclass53_0.string_0, int_3_2);
            }

            if (gclass53_0.wfconditionType_0 == WFConditionType.CodeBytes)
            {
                var numArray = gclass0_0.method_6(int_3 + gclass53_0.int_0, gclass53_0.gclass29_0.method_0());
                bool flag;
                if (!(flag = gclass53_0.gclass29_0.method_1(numArray)))
                {
                    GClass0.smethod_0("CFound  @ 0x" + (int_3 + gclass53_0.int_0).ToString("x") + ": \"" +
                                      GClass0.smethod_2(numArray) + "\"");
                    GClass0.smethod_0("CWanted @ 0x" + (int_3 + gclass53_0.int_0).ToString("x") + ": \"" +
                                      gclass53_0.string_0 + "\"");
                }

                return flag;
            }

            if (gclass53_0.wfconditionType_0 != WFConditionType.DataBytes)
                throw new Exception("Unknown condition type: " + gclass53_0.wfconditionType_0);
            GClass0.smethod_0("Reading data pointer @ 0x" + (int_3 + gclass53_0.int_0).ToString("x"));
            var int_0 = gclass0_0.method_4(int_3 + gclass53_0.int_0);
            GClass0.smethod_0("Points to: 0x" + int_0.ToString("x"));
            var numArray1 = gclass0_0.method_6(int_0, gclass53_0.gclass29_0.method_0());
            bool flag1;
            if (!(flag1 = gclass53_0.gclass29_0.method_1(numArray1)))
            {
                GClass0.smethod_0("DFound  @ 0x" + int_0.ToString("x") + ": \"" + GClass0.smethod_2(numArray1) + "\"");
                GClass0.smethod_0("DWanted @ 0x" + int_0.ToString("x") + ": \"" + gclass53_0.string_0 + "\"");
            }

            return flag1;
        }
        catch (Exception ex)
        {
            GClass0.smethod_0("Exception trying to match condition " + gclass53_0 + ", must not have hit");
            return false;
        }
    }

    public int method_6()
    {
        try
        {
            var num1 = gclass0_0.method_8(GClass0.smethod_3("558bec8b450c8b4d0803c15dc20800"));
            var numArray1 = gclass0_0.method_10(GClass0.smethod_3("558bec5dff25"));
            var numArray2 = new byte[8];
            var num2 = 0;
            var num3 = 0;
            Array.Copy(BitConverter.GetBytes(num1), 0, numArray2, 4, 4);
            foreach (var num4 in numArray1)
            {
                Array.Copy(BitConverter.GetBytes(num4), 0, numArray2, 0, 4);
                var numArray3 = gclass0_0.method_11(numArray2);
                if (numArray3.Length == 1)
                {
                    ++num2;
                    num3 = numArray3[0];
                }
            }

            if (num2 != 1)
                throw new Exception("Could not find core class structure");
            var num5 = num3;
            int_2 = num5 - 8;
            GClass0.smethod_0("Found core class, call to Warden_GetModuleHandle @ 0x" + num5.ToString("x"));
            var int_0_1 = num5 + 48;
            if (gclass0_0.method_9(gclass0_0.method_4(int_0_1)))
            {
                GClass0.smethod_0("RickRoll appears valid, warden rev: 2 or 4");
                var int_0_2 = int_0_1 + 4;
                var int_0_3 = gclass0_0.method_4(int_0_2);
                var int_0_4 = gclass0_0.method_4(int_0_2 + 4);
                var int_0_5 = gclass0_0.method_4(int_0_2 + 8);
                if (gclass0_0.method_9(int_0_3) && gclass0_0.method_9(int_0_4) && !gclass0_0.method_9(int_0_5))
                {
                    GClass0.smethod_0("Two function pointers after rickroll, rev 4");
                    int_1 = 4;
                    return int_1;
                }

                if (!gclass0_0.method_9(int_0_3) || gclass0_0.method_9(int_0_4))
                    throw new Exception("Don't like bytes after rickroll");
                GClass0.smethod_0("Only one valid pointer after rickroll, rev 2");
                int_1 = 2;
                return int_1;
            }

            int_1 = 1;
            GClass0.smethod_0("RickRoll pointer is bogus, warden rev: 1");
        }
        catch (Exception ex)
        {
            GClass0.smethod_1("Couldn't find core: " + ex.Message + "\r\n" + ex.StackTrace);
            int_1 = 0;
        }

        return int_1;
    }
}