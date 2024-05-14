// Decompiled with JetBrains decompiler
// Type: GClass77
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Glider.Common;
using Glider.Common.Objects;
using Encoder = System.Drawing.Imaging.Encoder;

public class GClass77
{
    public const int int_0 = 0;
    public const int int_1 = 65535;
    public const int int_2 = 1;
    public const int int_3 = 2;
    public const int int_4 = 4;
    public const int int_5 = 8;
    public const int int_6 = 16;
    public const int int_7 = 32;
    private const int int_8 = 8192;
    private const int int_9 = 500000;
    private static int int_10;
    private bool bool_0;
    private bool bool_1;
    private bool bool_2;
    private bool bool_3;
    private readonly byte[] byte_0;
    private byte[] byte_1;
    private DateTime dateTime_0 = DateTime.MinValue;
    private readonly GClass79 gclass79_0;
    private ImageCodecInfo imageCodecInfo_0;
    private int int_11;
    private readonly int int_12;
    private int int_13;
    private int int_14 = 100;
    private int int_15 = 100;
    private long long_0 = 75;
    public Socket socket_0;
    private string string_0 = string.Empty;
    private Thread thread_0;

    public GClass77(GClass79 gclass79_1, Socket socket_1)
    {
        ++int_10;
        int_12 = int_10;
        gclass79_0 = gclass79_1;
        socket_0 = socket_1;
        thread_0 = null;
        bool_0 = false;
        bool_1 = false;
        byte_0 = null;
        bool_2 = false;
        int_11 = 0;
    }

    public void method_0()
    {
        thread_0 = new Thread(method_2);
        thread_0.Start();
    }

    public void method_1()
    {
        try
        {
            GClass37.smethod_1("Stopping a remoteguy");
            bool_0 = true;
            if (socket_0 != null)
            {
                socket_0.Close();
                socket_0 = null;
            }

            if (thread_0 != null)
            {
                GClass37.smethod_1("Waiting for work thread");
                thread_0.Join();
                GClass37.smethod_1("Done waiting for work thread");
            }

            GClass37.smethod_1("Done with stopping a remoteguy");
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("!! Exception stopping a remoteguy: " + ex.Message + "\r\n" + ex.StackTrace);
        }
    }

    public void method_2()
    {
        try
        {
            method_3();
        }
        catch (Exception ex1)
        {
            if (socket_0 != null)
            {
                try
                {
                    socket_0.Close();
                }
                catch (Exception ex2)
                {
                    GClass37.smethod_1("Exception tossing socket in cleanup, no big deal");
                }

                socket_0 = null;
            }

            if (!bool_0)
            {
                GClass37.smethod_0(GClass30.smethod_2(354, ex1.Message));
                GClass37.smethod_1("** " + ex1.Message + ex1.StackTrace);
            }
        }

        thread_0 = null;
        StartupClass.gclass79_0.method_3(this);
    }

    public void method_3()
    {
        var str = method_4();
        if (!(str != GClass61.gclass61_0.method_2("ListenPassword")))
        {
            method_6("Authenticated ok\r\n");
            GClass37.smethod_0(GClass30.smethod_1(357));
            bool_1 = true;
            while (true)
            {
                string string_1;
                do
                {
                    string_1 = method_4();
                } while (string_1.Length <= 0 || !(string_1.Substring(0, 1) == "/"));

                lock (this)
                {
                    method_7(string_1);
                    GClass37.smethod_1(int_12 + " <- processing complete");
                    method_6("---\r\n");
                }
            }
        }

        GClass37.smethod_0(GClass30.smethod_2(355, str));
        socket_0.Close();
        socket_0 = null;
    }

    private string method_4()
    {
        var stringBuilder = new StringBuilder();
        while (true)
        {
            byte[] numArray;
            do
            {
                numArray = new byte[1];
                if (socket_0.Receive(numArray, 0, 1, SocketFlags.None) != 0)
                    switch (numArray[0])
                    {
                        case 8:
                            continue;
                        case 10:
                            continue;
                        case 13:
                            goto label_7;
                        default:
                            goto label_4;
                    }

                goto label_6;
            } while (stringBuilder.Length <= 0);

            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            continue;
            label_4:
            stringBuilder.Append(Encoding.ASCII.GetChars(numArray));
        }

        label_6:
        GClass37.smethod_1(int_10 + " -- Lost connection in read");
        bool_0 = true;
        throw new Exception(GClass30.smethod_1(358));
        label_7:
        return stringBuilder.ToString();
    }

    public void method_5(int int_16, string string_1)
    {
        try
        {
            if (socket_0 != null && bool_1)
            {
                if ((int_11 & int_16) <= 0)
                    return;
                method_6("[" + smethod_0(int_16) + "] " + string_1 + "\r\n");
            }
            else
            {
                GClass37.smethod_1("(skipping notify on dead/unauthed connection #" + int_12 + ")");
            }
        }
        catch (Exception ex)
        {
            GClass37.smethod_1(GClass30.smethod_1(359));
            method_1();
        }
    }

    private static string smethod_0(int int_16)
    {
        switch (int_16)
        {
            case 1:
                return "Status";
            case 2:
                return "GliderLog";
            case 4:
                return "Combat";
            case 8:
                return "Chat";
            case 16:
                return "ChatSay";
            case 32:
                return "ChatRaw";
            default:
                return int_16.ToString("x");
        }
    }

    public void method_6(string string_1)
    {
        int num;
        if (bool_2)
        {
            var stringBuilder = new StringBuilder();
            foreach (var ch in string_1)
                if (ch <= '\u007F')
                {
                    stringBuilder.Append(ch);
                }
                else
                {
                    stringBuilder.Append("&&#");
                    stringBuilder.Append((int)ch);
                    stringBuilder.Append(";");
                }

            num = socket_0.Send(Encoding.ASCII.GetBytes(stringBuilder.ToString()));
        }
        else
        {
            num = socket_0.Send(Encoding.ASCII.GetBytes(string_1));
        }

        if (num == 0)
        {
            bool_0 = true;
            socket_0.Close();
            socket_0 = null;
            throw new Exception(GClass30.smethod_1(360));
        }
    }

    private void method_7(string string_1)
    {
        GClass37.smethod_1(int_12 + " -> Remote cmd: [" + string_1 + "]");
        var strArray1 = string_1.Split(' ');
        var lower1 = strArray1[0].ToLower();
        if (lower1 == "/exit")
        {
            int_11 = 0;
            method_6("Bye!\r\n");
            StartupClass.smethod_39(1000);
            bool_0 = true;
            socket_0.Close();
            socket_0 = null;
            throw new Exception(GClass30.smethod_1(361));
        }

        if (!(lower1 == "/help") && !(lower1 == "/?"))
        {
            if (lower1 == "/log" && strArray1.Length > 1)
            {
                var lower2 = strArray1[1].ToLower();
                switch (lower2)
                {
                    case "all":
                        int_11 |= ushort.MaxValue;
                        break;
                    case "none":
                        int_11 = 0;
                        break;
                    case "status":
                        int_11 |= 1;
                        break;
                    case "gliderlog":
                        int_11 |= 2;
                        break;
                    case "chat":
                        int_11 |= 8;
                        break;
                    case "chatsay":
                        int_11 |= 16;
                        break;
                    case "combat":
                        int_11 |= 4;
                        break;
                    case "chatraw":
                        int_11 |= 32;
                        break;
                    default:
                        method_6("Unknown /log mode: \"" + lower2 + "\"\r\n");
                        return;
                }

                method_6("Log mode added: " + lower2 + "\r\n");
            }
            else if (lower1 == "/grabmouse" && strArray1.Length > 1)
            {
                var lower3 = strArray1[1].ToLower();
                if (!StartupClass.bool_12)
                {
                    method_6("Not Elite, can't grab mouse\r\n");
                }
                else if (!GClass61.gclass61_0.method_5("BackgroundEnable"))
                {
                    method_6("BackgroundEnable not set, can't grab mouse\r\n");
                }
                else if (StartupClass.gclass71_0 == null)
                {
                    method_6("No Shadow driver, can't grab mouse\r\n");
                }
                else
                {
                    if (!StartupClass.bool_11)
                    {
                        GClass37.smethod_1("Setting up bg stuff");
                        StartupClass.intptr_0 = GProcessMemoryManipulator.smethod_29(StartupClass.int_3);
                        StartupClass.gclass71_0.method_34(StartupClass.int_3, StartupClass.intptr_0);
                        StartupClass.bool_11 = true;
                    }

                    switch (lower3)
                    {
                        case "true":
                            StartupClass.gclass71_0.method_33(true);
                            method_6("Mouse grabbed\r\n");
                            break;
                        case "false":
                            GClass55.smethod_21(false);
                            StartupClass.gclass71_0.method_33(false);
                            method_6("Mouse released\r\n");
                            break;
                        default:
                            method_6("Specify true or false for /grabmouse!\r\n");
                            break;
                    }
                }
            }
            else if (lower1 == "/nolog" && strArray1.Length > 1)
            {
                var lower4 = strArray1[1].ToLower();
                switch (lower4)
                {
                    case "all":
                        int_11 = 0;
                        goto case "none";
                    case "none":
                        method_6("Log mode removed: " + lower4 + "\r\n");
                        break;
                    case "status":
                        int_11 &= -2;
                        goto case "none";
                    case "gliderlog":
                        int_11 &= -3;
                        goto case "none";
                    case "chat":
                        int_11 &= -9;
                        goto case "none";
                    case "chatsay":
                        int_11 &= -17;
                        goto case "none";
                    case "combat":
                        int_11 &= -5;
                        goto case "none";
                    case "chatraw":
                        int_11 &= -33;
                        goto case "none";
                    default:
                        method_6("Unknown /nolog mode: \"" + lower4 + "\"\r\n");
                        break;
                }
            }
            else if (lower1 == "/escapehi" && strArray1.Length > 1)
            {
                var lower5 = strArray1[1].ToLower();
                switch (lower5)
                {
                    case "on":
                        bool_2 = true;
                        break;
                    case "off":
                        bool_2 = false;
                        break;
                    default:
                        method_6("Unknown /escapehi value: \"" + lower5 + "\"\r\n");
                        return;
                }

                method_6("Escapehi mode: " + lower5 + "\r\n");
            }
            else
            {
                if (lower1 == "/say" && strArray1.Length > 1)
                {
                    var str = string_1.Substring(4);
                    if (StartupClass.glideMode_0 == GlideMode.Auto)
                    {
                        StartupClass.gclass73_0.method_23(str, false);
                        method_6("Queued for sending\r\n");
                    }
                    else
                    {
                        if (StartupClass.bool_11 || !GClass55.bool_0)
                        {
                            GClass55.smethod_9(13);
                            StartupClass.smethod_39(300);
                        }

                        GClass55.smethod_28(str);
                        method_6("Sent\r\n");
                    }
                }

                if (lower1 == "/loadprofile" && strArray1.Length > 1)
                {
                    if (StartupClass.glideMode_0 != GlideMode.None)
                    {
                        method_6("Failed: can't load a profile while gliding!\r\n");
                        return;
                    }

                    var str = string_1.Substring(string_1.IndexOf(" ") + 1);
                    method_6(StartupClass.smethod_1(str[1] != ':' ? "Profiles\\" + str : str)
                        ? "Loaded profile ok\r\n"
                        : "Load profile failed - bogus name?\r\n");
                }

                if (lower1 == "/exitglider")
                {
                    method_6("Exiting Glider, this connection will close very soon!\r\n");
                    Thread.Sleep(1000);
                    StartupClass.smethod_34();
                }
                else
                {
                    if (lower1 == "/queuekeys" && strArray1.Length > 1)
                    {
                        var str = string_1.Substring(string_1.IndexOf(" ") + 1);
                        if (StartupClass.glideMode_0 == GlideMode.Auto)
                        {
                            StartupClass.gclass73_0.method_23(str, true);
                            method_6("Queued for sending\r\n");
                        }
                        else
                        {
                            StartupClass.smethod_20(str);
                            method_6("Keys sent\r\n");
                        }
                    }

                    if (lower1 == "/forcekeys" && strArray1.Length > 1)
                    {
                        var string_11 = string_1.Substring(string_1.IndexOf(" ") + 1);
                        lock (GClass42.gclass42_0)
                        {
                            StartupClass.smethod_20(string_11);
                        }

                        method_6("Keys sent\r\n");
                    }

                    if (lower1 == "/holdkey" && strArray1.Length > 1)
                    {
                        var short_0 = smethod_1(string_1.Substring(string_1.IndexOf(" ") + 1));
                        if (short_0 > 0)
                        {
                            GClass55.smethod_0((short)short_0, true);
                            method_6("Key down\r\n");
                        }
                        else
                        {
                            method_6("Unable to parse key value, should be integer value of VK you want to press");
                        }
                    }

                    if (lower1 == "/releasekey" && strArray1.Length > 1)
                    {
                        var short_0 = smethod_1(string_1.Substring(string_1.IndexOf(" ") + 1));
                        if (short_0 > 0)
                        {
                            GClass55.smethod_0((short)short_0, false);
                            method_6("Key up\r\n");
                        }
                        else
                        {
                            method_6("Unable to parse key value, should be integer value of VK you want to release");
                        }
                    }

                    if (lower1 == "/version")
                    {
                        method_6("Version: 1.8.0\r\n");
                        method_6("Subversion: Release\r\n");
                        method_6("Elite: " + StartupClass.bool_12 + "\r\n");
                        method_6("Game: " + StartupClass.WowVersionLabel_string + "\r\n");
                    }

                    if (lower1 == "/selectgame")
                    {
                        StartupClass.smethod_22();
                        GProcessMemoryManipulator.smethod_33();
                        method_6("Game selected\r\n");
                    }

                    if (lower1 == "/getgamews")
                    {
                        var str = "normal";
                        if (StartupClass.bool_40)
                            str = "hidden";
                        if (StartupClass.bool_41)
                            str = "shrunk";
                        method_6("Game window state: " + str + "\r\n");
                    }

                    if (lower1 == "/setgamews" && strArray1.Length > 1)
                    {
                        var str = string_1.Substring(string_1.IndexOf(" ") + 1);
                        switch (str)
                        {
                            case "normal":
                                method_6("Setting new state: normal\r\n");
                                if (StartupClass.bool_41)
                                    StartupClass.smethod_50();
                                if (StartupClass.bool_40) StartupClass.smethod_49();
                                break;
                            case "hidden":
                                method_6("Setting new state: hidden\r\n");
                                StartupClass.smethod_47();
                                break;
                            case "shrunk":
                                method_6("Setting new state: shrunk\r\n");
                                StartupClass.smethod_48();
                                break;
                            default:
                                method_6("Unknown state: " + str + "\r\n");
                                break;
                        }
                    }

                    if (lower1 == "/config")
                    {
                        if (StartupClass.glideMode_0 != GlideMode.None)
                        {
                            method_6("Failed: can't reconfigure while gliding, stop first!\r\n");
                        }
                        else
                        {
                            var str = GClass61.gclass61_0.method_2("AppKey");
                            GClass61.gclass61_0.method_3("Class");
                            GClass61.gclass61_0.method_7(false);
                            StartupClass.bool_29 = true;
                            StartupClass.gclass24_0.method_0();
                            GClass42.gclass42_0.method_12();
                            GClass55.smethod_31(GClass61.gclass61_0);
                            StartupClass.smethod_5();
                            StartupClass.gclass54_0.method_0(GClass61.gclass61_0);
                            if (str != GClass61.gclass61_0.method_2("AppKey") || StartupClass.gclass54_0.bool_4 ||
                                !StartupClass.bool_22)
                            {
                                StartupClass.gclass54_0.bool_4 = false;
                                StartupClass.smethod_15();
                                StartupClass.smethod_9();
                            }

                            method_6("Success: loaded config\r\n");
                        }
                    }

                    if (lower1 == "/status")
                    {
                        var bool13 = StartupClass.bool_13;
                        var glideMode0 = StartupClass.glideMode_0;
                        var stringBuilder = new StringBuilder();
                        method_6("Version: 1.8.0\r\n");
                        method_6("Attached: " + bool13 + "\r\n");
                        method_6("Mode: " + glideMode0 + "\r\n");
                        method_6("Profile: " + StartupClass.string_5 + "\r\n");
                        if ((int_11 & 8) > 0)
                            stringBuilder.Append("Chat ");
                        if ((int_11 & 32) > 0)
                            stringBuilder.Append("ChatRaw ");
                        if ((int_11 & 16) > 0)
                            stringBuilder.Append("ChatSay ");
                        if ((int_11 & 2) > 0)
                            stringBuilder.Append("GliderLog ");
                        if ((int_11 & 1) > 0)
                            stringBuilder.Append("Status ");
                        if ((int_11 & 4) > 0)
                            stringBuilder.Append("Combat ");
                        if (stringBuilder.Length == 0)
                            stringBuilder.Append("None ");
                        stringBuilder.Remove(stringBuilder.Length - 1, 1);
                        method_6("Log: " + stringBuilder + "\r\n");
                        if (bool13)
                        {
                            var num = GPlayerSelf.Me.Health;
                            if (GPlayerSelf.Me.IsDead)
                                num = 0.0;
                            method_6("Health: " + num + "\r\n");
                            if (StartupClass.ggameClass_0 != null)
                                method_6("Mana: " + StartupClass.ggameClass_0.PowerValue + "\r\n");
                            method_6("Name: " + GPlayerSelf.Me.Name + "\r\n");
                            method_6("Class: " + GPlayerSelf.Me.PlayerClass + "\r\n");
                            method_6("Level: " + GPlayerSelf.Me.Level + "\r\n");
                            method_6("Experience: " + GPlayerSelf.Me.Experience + "\r\n");
                            method_6("Next-Experience: " + GPlayerSelf.Me.NextLevelExperience + "\r\n");
                            method_6("XP/Hour: " + StartupClass.smethod_29() + "\r\n");
                            method_6("Location: " + GPlayerSelf.Me.Location.ToString3D() + "\r\n");
                            method_6("Heading: " + GPlayerSelf.Me.Heading + "\r\n");
                            method_6("Pitch: " + GPlayerSelf.Me.Pitch + "\r\n");
                            method_6("KLD: " + StartupClass.int_7 + "/" + StartupClass.int_8 + "/" +
                                     StartupClass.int_9 + "\r\n");
                            var target = GPlayerSelf.Me.Target;
                            if (target != null)
                            {
                                method_6("Target-Name: " + target.Name + "\r\n");
                                method_6("Target-Level: " + target.Level + "\r\n");
                                method_6("Target-Health: " + target.Health + "\r\n");
                                method_6("Target-FactionID: " + target.FactionID + "\r\n");
                                method_6("Target-Mana: " + target.Mana + "\r\n");
                                method_6("Target-Location: " + target.Location + "\r\n");
                            }
                        }
                    }

                    if (lower1 == "/attach")
                    {
                        if (StartupClass.bool_13)
                            method_6("Already attached\r\n");
                        else
                            method_6("Attach command not supported any more, passive attach is automatic\r\n");
                    }

                    if (lower1 == "/clearsay")
                    {
                        if (StartupClass.glideMode_0 != GlideMode.Auto)
                        {
                            method_6("Not gliding, nothing to clear\r\n");
                            return;
                        }

                        if (StartupClass.gclass73_0.method_25())
                        {
                            StartupClass.gclass73_0.method_24();
                            method_6("Queue cleared\r\n");
                        }
                        else
                        {
                            method_6("Nothing queued to remove\r\n");
                        }
                    }

                    if (lower1 == "/startglide")
                    {
                        if (!StartupClass.bool_13)
                        {
                            method_6("Attaching\r\n");
                            StartupClass.smethod_13();
                            if (!StartupClass.bool_13)
                            {
                                method_6("Could not attach\r\n");
                                return;
                            }
                        }

                        if (StartupClass.glideMode_0 != GlideMode.None)
                        {
                            method_6("Can't start glide, already in auto mode\r\n");
                            return;
                        }

                        method_6("Attempting start\r\n");
                        StartupClass.smethod_24(false);
                    }

                    if (lower1 == "/stopglide")
                    {
                        if (!StartupClass.bool_13)
                        {
                            method_6("Not attached, nothing to stop\r\n");
                            return;
                        }

                        if (StartupClass.glideMode_0 == GlideMode.None)
                        {
                            method_6("Already stopped\r\n");
                            return;
                        }

                        method_6("Attempting stop\r\n");
                        StartupClass.smethod_27(false, "StopGlideFromRemote");
                    }

                    if (lower1 == "/getmouse")
                    {
                        double double_2;
                        double double_3;
                        GClass55.smethod_22(out double_2, out double_3);
                        method_6("Mouse coords: " + Math.Round(double_2, 3) + "/" + Math.Round(double_3, 3) + "\r\n");
                    }

                    if (lower1 == "/clickmouse" && strArray1.Length > 1)
                    {
                        if (strArray1[1].ToLower() == "left")
                        {
                            method_6("Clicked left button\r\n");
                            GClass55.smethod_23(false);
                        }

                        if (strArray1[1].ToLower() == "right")
                        {
                            method_6("Clicked right button\r\n");
                            GClass55.smethod_23(true);
                        }
                    }

                    if (lower1 == "/setmouse" && strArray1.Length > 1)
                    {
                        var strArray2 = strArray1[1].Split('/');
                        if (strArray2.Length != 2)
                        {
                            method_6("Usage: /setmousepos [X/Y]\r\n");
                            return;
                        }

                        var double_2 = StartupClass.smethod_6(strArray2[0]);
                        var double_3 = StartupClass.smethod_6(strArray2[1]);
                        GClass55.smethod_18(double_2, double_3);
                        method_6("Moved mouse to " + double_2 + "/" + double_3 + "\r\n");
                    }

                    if (lower1 == "/queryconfig" && strArray1.Length > 1)
                    {
                        var string_3 = strArray1[1];
                        if (string_3 == "AppKey")
                        {
                            method_6("Can't query AppKey via remote interface\r\n");
                            return;
                        }

                        var str = GClass61.gclass61_0.method_2(string_3);
                        if (str == null)
                            method_6("Failed: no config value \"" + string_3 + "\" found\r\n");
                        else
                            method_6("Config value: " + str + "\r\n");
                    }

                    if (lower1 == "/capturecache" && strArray1.Length > 1)
                    {
                        var int_2 = smethod_1(string_1.Substring(string_1.IndexOf(" ") + 1));
                        if (int_2 > 0)
                        {
                            method_6("New capture cache set\r\n");
                            GClass61.gclass61_0.method_0("CaptureDelay", int_2.ToString());
                            GClass61.gclass61_0.method_8();
                            gclass79_0.gclass36_0 = new GClass36(int_2);
                        }
                        else
                        {
                            method_6(
                                "Unable to parse capture cache, should be integer value of milliseconds to cache capture\r\n");
                        }
                    }

                    if (lower1 == "/capturequality" && strArray1.Length > 1)
                    {
                        var long_1 = smethod_1(string_1.Substring(string_1.IndexOf(" ") + 1));
                        if (long_1 >= 10 && long_1 <= 100)
                        {
                            method_6("New capture quality set\r\n");
                            method_15(long_1);
                        }
                        else
                        {
                            method_6(
                                "Unable to parse capture quality, should be integer value of percentage from 10 to 100\r\n");
                        }
                    }

                    if (lower1 == "/capturescale" && strArray1.Length > 1)
                    {
                        var int_16 = smethod_1(string_1.Substring(string_1.IndexOf(" ") + 1));
                        if (int_16 >= 10 && int_16 <= 100)
                        {
                            method_6("New capture scale set\r\n");
                            method_17(int_16);
                        }
                        else
                        {
                            method_6(
                                "Unable to parse capture scale, should be integer value of percentage from 10 to 100\r\n");
                        }
                    }

                    if (!(lower1 == "/capture"))
                        return;
                    method_18();
                }
            }
        }
        else
        {
            method_6(
                "Glider remote menu:\r\n\r\n/exit                         - shut down this connection\r\n/exitglider                   - shut down Glider completely\r\n/status                       - return current status of the game/char\r\n/version                      - return Glider and game version info\r\n/log [none|all|status|chatraw|\r\n      gliderlog|chat|combat]  - add logging of data on this channel\r\n\r\n/nolog [all|status|chatraw|\r\n      gliderlog|chat|combat]  - remove logging of data on this channel\r\n\r\n/say [message]                - queue chat for sending\r\n/queuekeys [keys]             - queue string for injection, | = CR, #VK# = VK\r\n/clearsay                     - clear queue of message, if pending\r\n/forcekeys [keys]             - force keys in right now (dangerous!)\r\n/holdkey [VK code]            - press and hold this VK code (dangerous!)\r\n/releasekey [VK code]         - release this VK code (dangerous!)\r\n/grabmouse [true/false]       - tell driver to grab/release mouse for bg ops\r\n/setmouse [X/Y]               - position mouse, use 0 - .999 for coord\r\n/getmouse                     - return current mouse position in percentages\r\n/clickmouse [left|right]      - click mouse button\r\n/attach                       - attach to the game\r\n/startglide                   - start gliding\r\n/stopglide                    - stop gliding\r\n/loadprofile [filename]       - load a profile\r\n/capture                      - capture screen and send as JPG stream\r\n/capturecache [ms]            - set capture caching time in milliseconds\r\n/capturescale [10-100]        - set capture scaling from 10% to 100%\r\n/capturequality [10-100]      - set capture image quality from 10% to 100%\r\n/queryconfig [name]           - retrieve a config value from Glider.config.xml\r\n                                (name is case-sensitive!)\r\n/config                       - reload configuration\r\n/selectgame                   - bring the game window to the foreground\r\n/getgamews                    - get the game window state\r\n/setgamews [normal|hidden|\r\n            shrunk]           - set the game window state\r\n/escapehi [on/off]            - escape hi-bit (intl) characters with &&#...;\r\n\r\n");
        }
    }

    private void method_8(string string_1)
    {
        try
        {
            var buffer = new byte[1024];
            var num = 0;
            var fileStream = File.Open(string_1, FileMode.Open, FileAccess.Read, FileShare.Read);
            socket_0.Send(BitConverter.GetBytes(fileStream.Length), 0, 4, SocketFlags.None);
            while (true)
            {
                var size = fileStream.Read(buffer, 0, 1024);
                if (size != 0)
                {
                    socket_0.Send(buffer, 0, size, SocketFlags.None);
                    num += size;
                }
                else
                {
                    break;
                }
            }

            fileStream.Close();
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("** Exception in SendFile: " + ex.Message + ex.StackTrace);
            throw ex;
        }
    }

    private void method_9(MemoryStream memoryStream_0)
    {
        var position = (int)memoryStream_0.Position;
        var offset = 0;
        GClass37.smethod_1("Stream length: " + position);
        try
        {
            socket_0.Send(BitConverter.GetBytes(position), 0, 4, SocketFlags.None);
            while (true)
            {
                var size = position - offset;
                if (size != 0)
                {
                    if (size > 1024)
                        size = 1024;
                    socket_0.Send(byte_0, offset, size, SocketFlags.None);
                    offset += size;
                }
                else
                {
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("** Exception in SendStream: " + ex.Message + ex.StackTrace);
            throw ex;
        }
    }

    private static int smethod_1(string string_1)
    {
        try
        {
            return int.Parse(string_1);
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    [SpecialName]
    public string method_10()
    {
        return string_0;
    }

    [SpecialName]
    public void method_11(string string_1)
    {
        string_0 = string_1;
    }

    [SpecialName]
    public int method_12()
    {
        return int_14;
    }

    [SpecialName]
    public void method_13(int int_16)
    {
        int_14 = int_16;
    }

    [SpecialName]
    public long method_14()
    {
        return long_0;
    }

    [SpecialName]
    public void method_15(long long_1)
    {
        if (long_1 > 100L)
            long_0 = 100L;
        else if (long_1 < 10L)
            long_0 = 10L;
        else
            long_0 = long_1;
    }

    [SpecialName]
    public int method_16()
    {
        return int_15;
    }

    [SpecialName]
    public void method_17(int int_16)
    {
        if (int_16 > 100)
            int_15 = 100;
        else if (int_16 < 10)
            int_15 = 10;
        else
            int_15 = int_16;
    }

    public void method_18()
    {
        if (!bool_3 && (int_13 == 0 || dateTime_0.AddMilliseconds(method_12()) < DateTime.Now))
        {
            bool_3 = true;
            Bitmap image_0 = null;
            MemoryStream memoryStream = null;
            try
            {
                var intptr_0 = GProcessMemoryManipulator.smethod_3();
                if (intptr_0 == IntPtr.Zero)
                {
                    Console.WriteLine("Failed: no window handle to game\r\n");
                    int_13 = 0;
                    return;
                }

                image_0 = GClass57.smethod_0(intptr_0);
                if (image_0 == null)
                {
                    Console.WriteLine("Failed: unable to capture image (!?)\r\n");
                    int_13 = 0;
                    return;
                }

                if (byte_1 == null)
                    byte_1 = new byte[500000];
                memoryStream = new MemoryStream(byte_1);
                if (method_16() < 100)
                {
                    image_0 = GClass57.smethod_2(image_0, method_16());
                    GClass37.smethod_1("Resizing to pct: " + method_16());
                }
                else
                {
                    GClass37.smethod_1("Not resizing, pct = " + method_16());
                }

                var encoderParams = new EncoderParameters(1);
                GClass37.smethod_1("CaptureQuality: " + method_14());
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, method_14());
                if (imageCodecInfo_0 == null)
                    imageCodecInfo_0 = GClass57.smethod_1("image/jpeg");
                image_0.Save(memoryStream, imageCodecInfo_0, encoderParams);
                int_13 = (int)memoryStream.Position;
                dateTime_0 = DateTime.Now;
            }
            catch (Exception ex)
            {
                GClass37.smethod_0("Capture failed: " + ex.Message);
                int_13 = 0;
            }
            finally
            {
                image_0?.Dispose();
                memoryStream?.Dispose();
                bool_3 = false;
            }
        }

        if (!bool_3)
        {
            if (int_13 <= 0)
                return;
            method_6("Success, 4-byte length and JPG image stream follow\r\n");
            GClass37.smethod_1("Sending image size: " + int_13 + " bytes");
            socket_0.Send(BitConverter.GetBytes(int_13), 0, 4, SocketFlags.None);
            for (var offset = 0; offset < int_13; offset += Math.Min(8192, int_13 - offset))
                socket_0.Send(byte_1, offset, Math.Min(8192, int_13 - offset), SocketFlags.None);
        }
        else
        {
            Console.WriteLine("Failed: Capture cache being updated.\r\n");
        }
    }
}