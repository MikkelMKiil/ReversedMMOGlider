// Decompiled with JetBrains decompiler
// Type: RemoteViewerClient
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common;
using Glider.Common.Objects;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Encoder = System.Drawing.Imaging.Encoder;

public class RemoteViewerClient
{
    public const int int_0 = 0;
    public const int int_1 = 65535;
    public const int int_2 = 1;
    public const int int_3 = 2;
    public const int pgEditProfileCount = 4;
    public const int objectManagerBasePointer = 8;
    public const int initCount = 16;
    public const int knownVersion = 32;
    private const int expectedVersion = 8192;
    private const int versionPatchLevel = 500000;
    private static int lastAclProcessId;
    private bool bool_0;
    private bool bool_1;
    private bool bool_2;
    private bool bool_3;
    private readonly byte[] byte_0;
    private byte[] byte_1;
    private DateTime dateTime_0 = DateTime.MinValue;
    private readonly RemoteViewerServer remoteViewerServer;
    private ImageCodecInfo imageCodecInfo_0;
    private int cachedGlideRate;
    private readonly int attachPidOverride;
    private int killActionNestingCount;
    private int int_14 = 100;
    private int int_15 = 100;
    private long playerGuid = 75;
    public Socket socket_0;
    private string string_0 = string.Empty;
    private Thread thread_0;

    public RemoteViewerClient(RemoteViewerServer gclass79_1, Socket socket_1)
    {
        ++lastAclProcessId;
        attachPidOverride = lastAclProcessId;
        remoteViewerServer = gclass79_1;
        socket_0 = socket_1;
        thread_0 = null;
        bool_0 = false;
        bool_1 = false;
        byte_0 = null;
        bool_2 = false;
        cachedGlideRate = 0;
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
            Logger.LoadProfile("Stopping a remoteguy");
            bool_0 = true;
            if (socket_0 != null)
            {
                socket_0.Close();
                socket_0 = null;
            }

            if (thread_0 != null)
            {
                Logger.LoadProfile("Waiting for work thread");
                thread_0.Join();
                Logger.LoadProfile("Done waiting for work thread");
            }

            Logger.LoadProfile("Done with stopping a remoteguy");
        }
        catch (Exception ex)
        {
            Logger.LogMessage("!! Exception stopping a remoteguy: " + ex.Message + "\r\n" + ex.StackTrace);
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
                    Logger.LoadProfile("Exception tossing socket in cleanup, no big deal");
                }

                socket_0 = null;
            }

            if (!bool_0)
            {
                Logger.LogMessage(MessageProvider.IsGroupProfile(354, ex1.Message));
                Logger.LoadProfile("** " + ex1.Message + ex1.StackTrace);
            }
        }

        thread_0 = null;
        StartupClass.remoteViewerServer.method_3(this);
    }

    public void method_3()
    {
        var str = method_4();
        if (!(str != ConfigManager.gclass61_0.method_2("ListenPassword")))
        {
            method_6("Authenticated ok\r\n");
            Logger.LogMessage(MessageProvider.GetMessage(357));
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
                    Logger.LoadProfile(attachPidOverride + " <- processing complete");
                    method_6("---\r\n");
                }
            }
        }

        Logger.LogMessage(MessageProvider.IsGroupProfile(355, str));
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
        Logger.LoadProfile(lastAclProcessId + " -- Lost connection in read");
        bool_0 = true;
        throw new Exception(MessageProvider.GetMessage(358));
    label_7:
        return stringBuilder.ToString();
    }

    public void method_5(int int_16, string string_1)
    {
        try
        {
            if (socket_0 != null && bool_1)
            {
                if ((cachedGlideRate & int_16) <= 0)
                    return;
                method_6("[" + smethod_0(int_16) + "] " + string_1 + "\r\n");
            }
            else
            {
                Logger.LoadProfile("(skipping notify on dead/unauthed connection #" + attachPidOverride + ")");
            }
        }
        catch (Exception ex)
        {
            Logger.LoadProfile(MessageProvider.GetMessage(359));
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
            throw new Exception(MessageProvider.GetMessage(360));
        }
    }

    private void method_7(string string_1)
    {
        Logger.LoadProfile(attachPidOverride + " -> Remote cmd: [" + string_1 + "]");
        var strArray1 = string_1.Split(' ');
        var lower1 = strArray1[0].ToLower();
        if (lower1 == "/exit")
        {
            cachedGlideRate = 0;
            method_6("Bye!\r\n");
            StartupClass.Sleep(1000);
            bool_0 = true;
            socket_0.Close();
            socket_0 = null;
            throw new Exception(MessageProvider.GetMessage(361));
        }

        if (!(lower1 == "/help") && !(lower1 == "/?"))
        {
            if (lower1 == "/log" && strArray1.Length > 1)
            {
                var lower2 = strArray1[1].ToLower();
                switch (lower2)
                {
                    case "all":
                        cachedGlideRate |= ushort.MaxValue;
                        break;
                    case "none":
                        cachedGlideRate = 0;
                        break;
                    case "status":
                        cachedGlideRate |= 1;
                        break;
                    case "gliderlog":
                        cachedGlideRate |= 2;
                        break;
                    case "chat":
                        cachedGlideRate |= 8;
                        break;
                    case "chatsay":
                        cachedGlideRate |= 16;
                        break;
                    case "combat":
                        cachedGlideRate |= 4;
                        break;
                    case "chatraw":
                        cachedGlideRate |= 32;
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
                if (!StartupClass.IsSomeConditionMet)
                {
                    method_6("Not Elite, can't grab mouse\r\n");
                }
                else if (!ConfigManager.gclass61_0.method_5("BackgroundEnable"))
                {
                    method_6("BackgroundEnable not set, can't grab mouse\r\n");
                }
                else if (StartupClass.GliderManager == null)
                {
                    method_6("No Shadow driver, can't grab mouse\r\n");
                }
                else
                {
                    if (!StartupClass.IsGliderInitialized)
                    {
                        Logger.LoadProfile("Setting up bg stuff");
                        StartupClass.MainApplicationHandle = GProcessMemoryManipulator.OpenProcessWithAccess(StartupClass.AnotherIntegerValue);
                        StartupClass.GliderManager.method_34(StartupClass.AnotherIntegerValue, StartupClass.MainApplicationHandle);
                        StartupClass.IsGliderInitialized = true;
                    }

                    switch (lower3)
                    {
                        case "true":
                            StartupClass.GliderManager.method_33(true);
                            method_6("Mouse grabbed\r\n");
                            break;
                        case "false":
                            InputController.StartManualGlide(false);
                            StartupClass.GliderManager.method_33(false);
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
                        cachedGlideRate = 0;
                        goto case "none";
                    case "none":
                        method_6("Log mode removed: " + lower4 + "\r\n");
                        break;
                    case "status":
                        cachedGlideRate &= -2;
                        goto case "none";
                    case "gliderlog":
                        cachedGlideRate &= -3;
                        goto case "none";
                    case "chat":
                        cachedGlideRate &= -9;
                        goto case "none";
                    case "chatsay":
                        cachedGlideRate &= -17;
                        goto case "none";
                    case "combat":
                        cachedGlideRate &= -5;
                        goto case "none";
                    case "chatraw":
                        cachedGlideRate &= -33;
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
                        StartupClass.combatController.method_23(str, false);
                        method_6("Queued for sending\r\n");
                    }
                    else
                    {
                        if (StartupClass.IsGliderInitialized || !InputController.bool_0)
                        {
                            InputController.StartMainThread(13);
                            StartupClass.Sleep(300);
                        }

                        InputController.ExecuteStopGlide(str);
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
                    method_6(StartupClass.LoadProfile(str[1] != ':' ? "Profiles\\" + str : str)
                        ? "Loaded profile ok\r\n"
                        : "Load profile failed - bogus name?\r\n");
                }

                if (lower1 == "/exitglider")
                {
                    method_6("Exiting Glider, this connection will close very soon!\r\n");
                    Thread.Sleep(1000);
                    StartupClass.TriggerKillEvent();
                }
                else
                {
                    if (lower1 == "/queuekeys" && strArray1.Length > 1)
                    {
                        var str = string_1.Substring(string_1.IndexOf(" ") + 1);
                        if (StartupClass.glideMode_0 == GlideMode.Auto)
                        {
                            StartupClass.combatController.method_23(str, true);
                            method_6("Queued for sending\r\n");
                        }
                        else
                        {
                            StartupClass.SendInputString(str);
                            method_6("Keys sent\r\n");
                        }
                    }

                    if (lower1 == "/forcekeys" && strArray1.Length > 1)
                    {
                        var string_11 = string_1.Substring(string_1.IndexOf(" ") + 1);
                        lock (SpellcastingManager.gclass42_0)
                        {
                            StartupClass.SendInputString(string_11);
                        }

                        method_6("Keys sent\r\n");
                    }

                    if (lower1 == "/holdkey" && strArray1.Length > 1)
                    {
                        var short_0 = LoadProfile(string_1.Substring(string_1.IndexOf(" ") + 1));
                        if (short_0 > 0)
                        {
                            InputController.smethod_0((short)short_0, true);
                            method_6("Key down\r\n");
                        }
                        else
                        {
                            method_6("Unable to parse key value, should be integer value of VK you want to press");
                        }
                    }

                    if (lower1 == "/releasekey" && strArray1.Length > 1)
                    {
                        var short_0 = LoadProfile(string_1.Substring(string_1.IndexOf(" ") + 1));
                        if (short_0 > 0)
                        {
                            InputController.smethod_0((short)short_0, false);
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
                        method_6("Elite: " + StartupClass.IsSomeConditionMet + "\r\n");
                        method_6("Game: " + StartupClass.WowVersionLabel_string + "\r\n");
                    }

                    if (lower1 == "/selectgame")
                    {
                        StartupClass.BringGameWindowToForeground();
                        GProcessMemoryManipulator.CloseCurrentProcessHandle();
                        method_6("Game selected\r\n");
                    }

                    if (lower1 == "/getgamews")
                    {
                        var str = "normal";
                        if (StartupClass.IsWindowHidden)
                            str = "hidden";
                        if (StartupClass.IsWindowShrunk)
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
                                if (StartupClass.IsWindowShrunk)
                                    StartupClass.RestoreShrunkWindow();
                                if (StartupClass.IsWindowHidden) StartupClass.RestoreHiddenWindow();
                                break;
                            case "hidden":
                                method_6("Setting new state: hidden\r\n");
                                StartupClass.HideGameWindow();
                                break;
                            case "shrunk":
                                method_6("Setting new state: shrunk\r\n");
                                StartupClass.ShrinkGameWindow();
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
                            var str = ConfigManager.gclass61_0.method_2("AppKey");
                            ConfigManager.gclass61_0.method_3("Class");
                            ConfigManager.gclass61_0.method_7(false);
                            StartupClass.NeedsClassReload = true;
                            StartupClass.keyboardHookManager.method_0();
                            SpellcastingManager.gclass42_0.method_12();
                            InputController.Shutdown(ConfigManager.gclass61_0);
                            StartupClass.ApplyConfig();
                            StartupClass.partyManager.method_0(ConfigManager.gclass61_0);
                            if (str != ConfigManager.gclass61_0.method_2("AppKey") || StartupClass.partyManager.bool_4 ||
                                !StartupClass.isInitializationSuccessful)
                            {
                                StartupClass.partyManager.bool_4 = false;
                                StartupClass.Detach();
                                StartupClass.StartMainThread();
                            }

                            method_6("Success: loaded config\r\n");
                        }
                    }

                    if (lower1 == "/status")
                    {
                        var bool13 = StartupClass.IsGameProcessAttached;
                        var glideMode0 = StartupClass.glideMode_0;
                        var stringBuilder = new StringBuilder();
                        method_6("Version: 1.8.0\r\n");
                        method_6("Attached: " + bool13 + "\r\n");
                        method_6("Mode: " + glideMode0 + "\r\n");
                        method_6("Profile: " + StartupClass.currentProfilePath + "\r\n");
                        if ((cachedGlideRate & 8) > 0)
                            stringBuilder.Append("Chat ");
                        if ((cachedGlideRate & 32) > 0)
                            stringBuilder.Append("ChatRaw ");
                        if ((cachedGlideRate & 16) > 0)
                            stringBuilder.Append("ChatSay ");
                        if ((cachedGlideRate & 2) > 0)
                            stringBuilder.Append("GliderLog ");
                        if ((cachedGlideRate & 1) > 0)
                            stringBuilder.Append("Status ");
                        if ((cachedGlideRate & 4) > 0)
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
                            if (StartupClass.CurrentGameClass != null)
                                method_6("Mana: " + StartupClass.CurrentGameClass.PowerValue + "\r\n");
                            method_6("Name: " + GPlayerSelf.Me.Name + "\r\n");
                            method_6("Class: " + GPlayerSelf.Me.PlayerClass + "\r\n");
                            method_6("Level: " + GPlayerSelf.Me.Level + "\r\n");
                            method_6("Experience: " + GPlayerSelf.Me.Experience + "\r\n");
                            method_6("Next-Experience: " + GPlayerSelf.Me.NextLevelExperience + "\r\n");
                            method_6("XP/Hour: " + StartupClass.GetGlideRate() + "\r\n");
                            method_6("Location: " + GPlayerSelf.Me.Location.ToString3D() + "\r\n");
                            method_6("Heading: " + GPlayerSelf.Me.Heading + "\r\n");
                            method_6("Pitch: " + GPlayerSelf.Me.Pitch + "\r\n");
                            method_6("KLD: " + StartupClass.knownVersion + "/" + StartupClass.expectedVersion + "/" +
                                     StartupClass.versionPatchLevel + "\r\n");
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
                        if (StartupClass.IsGameProcessAttached)
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

                        if (StartupClass.combatController.method_25())
                        {
                            StartupClass.combatController.method_24();
                            method_6("Queue cleared\r\n");
                        }
                        else
                        {
                            method_6("Nothing queued to remove\r\n");
                        }
                    }

                    if (lower1 == "/startglide")
                    {
                        if (!StartupClass.IsGameProcessAttached)
                        {
                            method_6("Attaching\r\n");
                            StartupClass.TryAttach();
                            if (!StartupClass.IsGameProcessAttached)
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
                        StartupClass.StartAutoGlide(false);
                    }

                    if (lower1 == "/stopglide")
                    {
                        if (!StartupClass.IsGameProcessAttached)
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
                        StartupClass.StopGlide(false, "StopGlideFromRemote");
                    }

                    if (lower1 == "/getmouse")
                    {
                        double double_2;
                        double double_3;
                        InputController.BringGameWindowToForeground(out double_2, out double_3);
                        method_6("Mouse coords: " + Math.Round(double_2, 3) + "/" + Math.Round(double_3, 3) + "\r\n");
                    }

                    if (lower1 == "/clickmouse" && strArray1.Length > 1)
                    {
                        if (strArray1[1].ToLower() == "left")
                        {
                            method_6("Clicked left button\r\n");
                            InputController.AddWaypoint(false);
                        }

                        if (strArray1[1].ToLower() == "right")
                        {
                            method_6("Clicked right button\r\n");
                            InputController.AddWaypoint(true);
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

                        var double_2 = StartupClass.ParseDouble(strArray2[0]);
                        var double_3 = StartupClass.ParseDouble(strArray2[1]);
                        InputController.ParseProcessIdFromCommandLine(double_2, double_3);
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

                        var str = ConfigManager.gclass61_0.method_2(string_3);
                        if (str == null)
                            method_6("Failed: no config value \"" + string_3 + "\" found\r\n");
                        else
                            method_6("Config value: " + str + "\r\n");
                    }

                    if (lower1 == "/capturecache" && strArray1.Length > 1)
                    {
                        var int_2 = LoadProfile(string_1.Substring(string_1.IndexOf(" ") + 1));
                        if (int_2 > 0)
                        {
                            method_6("New capture cache set\r\n");
                            ConfigManager.gclass61_0.method_0("CaptureDelay", int_2.ToString());
                            ConfigManager.gclass61_0.method_8();
                            remoteViewerServer.licenseCheckTimer = new GameTimer(int_2);
                        }
                        else
                        {
                            method_6(
                                "Unable to parse capture cache, should be integer value of milliseconds to cache capture\r\n");
                        }
                    }

                    if (lower1 == "/capturequality" && strArray1.Length > 1)
                    {
                        var long_1 = LoadProfile(string_1.Substring(string_1.IndexOf(" ") + 1));
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
                        var int_16 = LoadProfile(string_1.Substring(string_1.IndexOf(" ") + 1));
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
            Logger.LogMessage("** Exception in SendFile: " + ex.Message + ex.StackTrace);
            throw ex;
        }
    }

    private void method_9(MemoryStream memoryStream_0)
    {
        var position = (int)memoryStream_0.Position;
        var offset = 0;
        Logger.LoadProfile("Stream length: " + position);
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
            Logger.LogMessage("** Exception in SendStream: " + ex.Message + ex.StackTrace);
            throw ex;
        }
    }

    private static int LoadProfile(string string_1)
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
        return playerGuid;
    }

    [SpecialName]
    public void method_15(long long_1)
    {
        if (long_1 > 100L)
            playerGuid = 100L;
        else if (long_1 < 10L)
            playerGuid = 10L;
        else
            playerGuid = long_1;
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
        if (!bool_3 && (killActionNestingCount == 0 || dateTime_0.AddMilliseconds(method_12()) < DateTime.Now))
        {
            bool_3 = true;
            Bitmap image_0 = null;
            MemoryStream memoryStream = null;
            try
            {
                var intptr_0 = GProcessMemoryManipulator.GetWindowHandle();
                if (intptr_0 == IntPtr.Zero)
                {
                    Console.WriteLine("Failed: no window handle to game\r\n");
                    killActionNestingCount = 0;
                    return;
                }

                image_0 = ScreenCapture.smethod_0(intptr_0);
                if (image_0 == null)
                {
                    Console.WriteLine("Failed: unable to capture image (!?)\r\n");
                    killActionNestingCount = 0;
                    return;
                }

                if (byte_1 == null)
                    byte_1 = new byte[500000];
                memoryStream = new MemoryStream(byte_1);
                if (method_16() < 100)
                {
                    image_0 = ScreenCapture.IsGroupProfile(image_0, method_16());
                    Logger.LoadProfile("Resizing to pct: " + method_16());
                }
                else
                {
                    Logger.LoadProfile("Not resizing, pct = " + method_16());
                }

                var encoderParams = new EncoderParameters(1);
                Logger.LoadProfile("CaptureQuality: " + method_14());
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, method_14());
                if (imageCodecInfo_0 == null)
                    imageCodecInfo_0 = ScreenCapture.LoadProfile("image/jpeg");
                image_0.Save(memoryStream, imageCodecInfo_0, encoderParams);
                killActionNestingCount = (int)memoryStream.Position;
                dateTime_0 = DateTime.Now;
            }
            catch (Exception ex)
            {
                Logger.LogMessage("Capture failed: " + ex.Message);
                killActionNestingCount = 0;
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
            if (killActionNestingCount <= 0)
                return;
            method_6("Success, 4-byte length and JPG image stream follow\r\n");
            Logger.LoadProfile("Sending image size: " + killActionNestingCount + " bytes");
            socket_0.Send(BitConverter.GetBytes(killActionNestingCount), 0, 4, SocketFlags.None);
            for (var offset = 0; offset < killActionNestingCount; offset += Math.Min(8192, killActionNestingCount - offset))
                socket_0.Send(byte_1, offset, Math.Min(8192, killActionNestingCount - offset), SocketFlags.None);
        }
        else
        {
            Console.WriteLine("Failed: Capture cache being updated.\r\n");
        }
    }
}