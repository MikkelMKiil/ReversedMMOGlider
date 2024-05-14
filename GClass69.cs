// Decompiled with JetBrains decompiler
// Type: GClass69
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.IO;
using System.Runtime.CompilerServices;
using Glider.Common;
using Glider.Common.Objects;

public class GClass69
{
    internal DateTime dateTime_0;
    internal DateTime dateTime_1;
    internal DateTime dateTime_2;
    internal DateTime dateTime_3;
    internal DateTime dateTime_4;
    internal DateTime dateTime_5;
    public GClass75 gclass75_0;
    public GClass75 gclass75_1;
    private readonly string string_0;
    internal string string_1;

    public GClass69()
    {
        string_1 = null;
        dateTime_0 = new DateTime(2000, 1, 1);
        dateTime_1 = new DateTime(2000, 1, 1);
        dateTime_2 = new DateTime(2000, 1, 1);
        dateTime_4 = new DateTime(2000, 1, 1);
        dateTime_3 = new DateTime(2000, 1, 1);
        dateTime_5 = new DateTime(2000, 1, 1);
        string_0 = MessageProvider.GetMessage(853).ToLower();
        method_3();
        method_17();
    }

    public void method_0()
    {
        Logger.smethod_1("Chat Manager connecting");
        lock (this)
        {
            method_1();
            method_2();
        }
    }

    internal void method_1()
    {
        var string_2 = "ChatFrame" + method_18(GClass61.gclass61_0.method_2("ChatLogFrame"));
        if (gclass75_0 == null)
            gclass75_0 = new GClass75(string_2);
        if (!gclass75_0.method_0())
            return;
        gclass75_0.method_1();
    }

    internal void method_2()
    {
        var string_2 = "ChatFrame" + method_18(GClass61.gclass61_0.method_2("CombatLogFrame"));
        if (gclass75_1 == null)
            gclass75_1 = new GClass75(string_2);
        if (!gclass75_1.method_0())
            return;
        gclass75_1.method_1();
    }

    public void method_3()
    {
        lock (this)
        {
            Logger.smethod_1("Chat Manager disconnecting");
            gclass75_0 = null;
            gclass75_1 = null;
        }
    }

    public void method_4()
    {
        try
        {
            lock (this)
            {
                if (gclass75_0 != null && gclass75_0.bool_0)
                    method_5();
                if (gclass75_1 == null || !gclass75_1.bool_0)
                    return;
                method_8();
            }
        }
        catch (Exception ex)
        {
            Logger.LogMessage(MessageProvider.smethod_2(817, ex.Message, ex.StackTrace));
            method_3();
        }
    }

    internal void method_5()
    {
        var string_3 = gclass75_0.method_1();
        if (string_3 == null)
        {
            Logger.LogMessage(MessageProvider.smethod_2(818, "Chat", gclass75_0.string_1));
            gclass75_0.method_0();
        }
        else
        {
            if (string_3.Length == 0)
                return;
            method_16("Chat.log", string_3);
            try
            {
                foreach (var str1 in string_3)
                    if (str1 == null)
                    {
                        Logger.smethod_1("* Null chat entry... !?");
                    }
                    else
                    {
                        StartupClass.smethod_17(32, str1);
                        var str2 = smethod_0(str1);
                        StartupClass.smethod_17(8, str2);
                        GContext.Main.FireChatLog(str1, str2);
                        str2.ToLower();
                        var strArray = str2.Split(' ');
                        if (strArray.Length >= 2)
                        {
                            if (strArray[1].Trim() == MessageProvider.GetMessage(34))
                            {
                                Logger.LogMessage(MessageProvider.smethod_2(35, strArray[0].Trim()));
                                if (StartupClass.glideMode_0 == GlideMode.Auto)
                                    method_7(strArray[1].Trim(), str1);
                            }

                            if (strArray[1].Trim() == MessageProvider.GetMessage(743))
                            {
                                Logger.LogMessage(MessageProvider.smethod_2(744, strArray[0].Trim()));
                                if (StartupClass.glideMode_0 == GlideMode.Auto)
                                    method_6(strArray[1].Trim(), str1);
                            }
                        }

                        if ((str1.IndexOf("|cff0070dd") > 0 || str1.IndexOf("|cffa335ee") > 0) &&
                            str1.IndexOf(string_0) > 0)
                            GClass20.smethod_0("Blue.wav");
                    }
            }
            catch (Exception ex)
            {
                Logger.LogMessage("** Exception in chat checking: " + ex.Message + ex.StackTrace);
            }
        }
    }

    protected void method_6(string string_2, string string_3)
    {
        if (!GClass61.gclass61_0.method_5("PlaySay"))
            return;
        GClass20.smethod_0("Whisper.wav");
    }

    protected void method_7(string string_2, string string_3)
    {
        if (!string_2.StartsWith(MessageProvider.GetMessage(640)) && string_3.ToLower().IndexOf("this is game master") <= -1 &&
            string_3.ToLower().IndexOf("this is account administrator") <= -1 &&
            string_3.ToLower().IndexOf("failure to respond") <= -1 &&
            string_3.ToLower().IndexOf("type /r to reply") <= -1)
        {
            if (!GClass61.gclass61_0.method_5("ChatWhisper"))
                return;
            GClass20.smethod_0("Whisper.wav");
        }
        else
        {
            Logger.LogMessage(MessageProvider.GetMessage(39));
            GClass20.smethod_0("GMWhisper.wav");
            if (GClass61.gclass61_0.method_2("ChatAutoReply") == "True")
            {
                Logger.LogMessage(MessageProvider.GetMessage(40));
                var str = GClass61.gclass61_0.method_2("ChatAutoReplyText");
                if (str.Length > 0)
                    GClass55.smethod_28("/r " + str);
                StartupClass.smethod_39(5000);
                StartupClass.smethod_27(false, "GMWhisper");
            }
            else
            {
                Logger.LogMessage(MessageProvider.GetMessage(41));
                StartupClass.smethod_27(false, "GMTalk");
            }
        }
    }

    internal void method_8()
    {
        var string_3 = gclass75_1.method_1();
        if (string_3 == null)
        {
            Logger.LogMessage(MessageProvider.smethod_2(818, "Combat", gclass75_1.string_1));
            gclass75_1.method_0();
        }
        else
        {
            if (string_3.Length == 0)
                return;
            method_16("Combat.log", string_3);
            var lower1 = MessageProvider.smethod_2(813, string_1).ToLower();
            foreach (var str in string_3)
            {
                StartupClass.smethod_17(4, str);
                if (string_1 != null)
                {
                    var lower2 = str.ToLower();
                    if (lower2.IndexOf(MessageProvider.GetMessage(829)) > 0 && lower2.IndexOf(string_1.ToLower()) > 0)
                        dateTime_5 = DateTime.Now;
                    if (lower2.EndsWith(string_1.ToLower() + MessageProvider.GetMessage(625).ToLower()) ||
                        lower2.IndexOf(MessageProvider.GetMessage(626) + string_1) > -1)
                    {
                        Logger.smethod_1(MessageProvider.GetMessage(746));
                        dateTime_1 = DateTime.Now;
                    }

                    if (lower2.EndsWith(string_1.ToLower() + MessageProvider.GetMessage(625).ToLower()) ||
                        lower2.IndexOf(MessageProvider.GetMessage(626) + string_1) > -1)
                    {
                        Logger.smethod_1(MessageProvider.GetMessage(746));
                        dateTime_1 = DateTime.Now;
                    }

                    if (lower2.IndexOf(lower1) > -1)
                    {
                        Logger.LogMessage(MessageProvider.GetMessage(814));
                        dateTime_0 = DateTime.Now;
                    }

                    if (lower2.IndexOf(string_1.ToLower() + MessageProvider.GetMessage(36).ToLower()) > -1)
                    {
                        Logger.smethod_1(MessageProvider.GetMessage(747));
                        dateTime_2 = DateTime.Now;
                    }

                    if (lower2.IndexOf(MessageProvider.GetMessage(37).ToLower()) > -1)
                        dateTime_3 = DateTime.Now;
                    if (lower2.IndexOf(MessageProvider.GetMessage(38).ToLower()) > -1)
                        dateTime_4 = DateTime.Now;
                }

                GContext.Main.FireCombatLog(str);
            }
        }
    }

    public void method_9(string string_2)
    {
        Logger.smethod_1(MessageProvider.smethod_2(639, string_2));
        string_1 = string_2;
    }

    [SpecialName]
    public int method_10()
    {
        return (int)(DateTime.Now - dateTime_5).TotalSeconds;
    }

    [SpecialName]
    public int method_11()
    {
        return (int)(DateTime.Now - dateTime_0).TotalSeconds;
    }

    [SpecialName]
    public int method_12()
    {
        return (int)(DateTime.Now - dateTime_1).TotalSeconds;
    }

    [SpecialName]
    public int method_13()
    {
        return (int)(DateTime.Now - dateTime_2).TotalSeconds;
    }

    [SpecialName]
    public int method_14()
    {
        return (int)(DateTime.Now - dateTime_3).TotalSeconds;
    }

    [SpecialName]
    public int method_15()
    {
        return (int)(DateTime.Now - dateTime_4).TotalSeconds;
    }

    public void method_16(string string_2, string[] string_3)
    {
        var now = DateTime.Now;
        try
        {
            var streamWriter = File.AppendText(string_2);
            foreach (var str in string_3)
                streamWriter.WriteLine(now.ToShortTimeString() + " " + str);
            streamWriter.Flush();
            streamWriter.Close();
        }
        catch (Exception ex)
        {
            Logger.LogMessage(MessageProvider.smethod_2(42, ex.Message));
        }
    }

    public void method_17()
    {
        if (!(GClass61.gclass61_0.method_2("ChatDelete") == "True"))
            return;
        Logger.LogMessage(MessageProvider.GetMessage(43));
        try
        {
            File.Delete("Chat.log");
            File.Delete("Combat.log");
        }
        catch (Exception ex)
        {
            Logger.LogMessage(MessageProvider.smethod_2(44, ex.Message));
        }
    }

    private static string smethod_0(string string_2)
    {
        try
        {
            return smethod_1(string_2);
        }
        catch (Exception ex)
        {
            Logger.smethod_1("! Couldn't clean chat line: " + string_2);
            return string_2;
        }
    }

    private static string smethod_1(string string_2)
    {
        var flag = false;
        while (!flag)
        {
            flag = true;
            var length1 = string_2.IndexOf("|c");
            if (length1 >= 0)
            {
                string_2 = string_2.Substring(0, length1) + string_2.Substring(length1 + 10);
                flag = false;
            }

            var num1 = string_2.IndexOf("|H");
            if (num1 > -1)
            {
                var num2 = string_2.IndexOf("|h", num1);
                if (num2 > -1)
                {
                    string_2 = string_2.Substring(0, num1) + string_2.Substring(num2 + 2);
                    flag = false;
                }
            }

            var length2 = string_2.IndexOf("|h");
            if (length2 > -1)
            {
                flag = false;
                string_2 = string_2.Substring(0, length2) + string_2.Substring(length2 + 2);
            }

            var length3 = string_2.IndexOf("|r");
            if (length3 > -1)
            {
                flag = false;
                string_2 = string_2.Substring(0, length3) + string_2.Substring(length3 + 2);
            }
        }

        return string_2;
    }

    private int method_18(string string_2)
    {
        if (string_2.Length == 1 && char.IsDigit(string_2[0]))
            return int.Parse(string_2);
        var num1 = GClass18.gclass18_0.method_4("ChatFrameBase");
        var num2 = GClass18.gclass18_0.method_4("ChatFrameSize");
        for (var index = 0; index <= 10; ++index)
        {
            var str = GProcessMemoryManipulator.smethod_9(num1 + index * num2, 100, "ChatFrameName");
            Logger.smethod_1(MessageProvider.smethod_2(821, index + 1, str));
            if (str.ToLower() == string_2.ToLower())
                return index + 1;
        }

        Logger.LogMessage(MessageProvider.smethod_2(822, string_2));
        return 1;
    }
}