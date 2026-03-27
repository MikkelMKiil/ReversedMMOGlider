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
    public const int int_4 = 4;
    public const int int_5 = 8;
    public const int int_6 = 16;
    public const int int_7 = 32;
    private const int SocketChunkSize = 8192;
    private const int CaptureBufferSize = 500000;
    private static int nextClientId;
    private bool isStopping;
    private bool isAuthenticated;
    private bool escapeHighBitCharacters;
    private bool isCaptureInProgress;
    private readonly byte[] streamBuffer;
    private byte[] captureBuffer;
    private DateTime lastCaptureTime = DateTime.MinValue;
    private readonly RemoteViewerServer server;
    private ImageCodecInfo jpegCodecInfo;
    private int enabledLogChannels;
    private readonly int clientId;
    private int cachedCaptureLength;
    private int captureCacheDurationMs = 100;
    private int captureScalePercent = 100;
    private long captureQualityPercent = 75;
    public Socket socket;
    private string queuedKeySequence = string.Empty;
    private Thread workerThread;

    public RemoteViewerClient(RemoteViewerServer remoteServer, Socket clientSocket)
    {
        ++nextClientId;
        clientId = nextClientId;
        server = remoteServer;
        socket = clientSocket;
        workerThread = null;
        isStopping = false;
        isAuthenticated = false;
        streamBuffer = null;
        escapeHighBitCharacters = false;
        enabledLogChannels = 0;
    }

    public void method_0()
    {
        workerThread = new Thread(method_2);
        workerThread.Start();
    }

    public void method_1()
    {
        try
        {
            Logger.smethod_1("Stopping a remoteguy");
            isStopping = true;
            if (socket != null)
            {
                socket.Close();
                socket = null;
            }

            if (workerThread != null)
            {
                Logger.smethod_1("Waiting for work thread");
                workerThread.Join();
                Logger.smethod_1("Done waiting for work thread");
            }

            Logger.smethod_1("Done with stopping a remoteguy");
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
            if (socket != null)
            {
                try
                {
                    socket.Close();
                }
                catch (Exception ex2)
                {
                    Logger.smethod_1("Exception tossing socket in cleanup, no big deal");
                }

                socket = null;
            }

            if (!isStopping)
            {
                Logger.LogMessage(MessageProvider.smethod_2(354, ex1.Message));
                Logger.smethod_1("** " + ex1.Message + ex1.StackTrace);
            }
        }

        workerThread = null;
        StartupClass.RemoteViewer.method_3(this);
    }

    public void method_3()
    {
        var str = method_4();
        if (!(str != ConfigManager.gclass61_0.method_2("ListenPassword")))
        {
            method_6("Authenticated ok\r\n");
            Logger.LogMessage(MessageProvider.GetMessage(357));
            isAuthenticated = true;
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
                    Logger.smethod_1(clientId + " <- processing complete");
                    method_6("---\r\n");
                }
            }
        }

        Logger.LogMessage(MessageProvider.smethod_2(355, str));
        socket.Close();
        socket = null;
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
                if (socket.Receive(numArray, 0, 1, SocketFlags.None) != 0)
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
        Logger.smethod_1(clientId + " -- Lost connection in read");
        isStopping = true;
        throw new Exception(MessageProvider.GetMessage(358));
    label_7:
        return stringBuilder.ToString();
    }

    public void method_5(int logChannel, string message)
    {
        try
        {
            if (socket != null && isAuthenticated)
            {
                if ((enabledLogChannels & logChannel) <= 0)
                    return;
                method_6("[" + smethod_0(logChannel) + "] " + message + "\r\n");
            }
            else
            {
                Logger.smethod_1("(skipping notify on dead/unauthed connection #" + clientId + ")");
            }
        }
        catch (Exception ex)
        {
            Logger.smethod_1(MessageProvider.GetMessage(359));
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
        if (escapeHighBitCharacters)
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

            num = socket.Send(Encoding.ASCII.GetBytes(stringBuilder.ToString()));
        }
        else
        {
            num = socket.Send(Encoding.ASCII.GetBytes(string_1));
        }

        if (num == 0)
        {
            isStopping = true;
            socket.Close();
            socket = null;
            throw new Exception(MessageProvider.GetMessage(360));
        }
    }

    private void method_7(string string_1)
    {
        Logger.smethod_1(clientId + " -> Remote cmd: [" + string_1 + "]");
        var strArray1 = string_1.Split(' ');
        var lower1 = strArray1[0].ToLower();
        if (lower1 == "/exit")
        {
            enabledLogChannels = 0;
            method_6("Bye!\r\n");
            StartupClass.SleepMilliseconds(1000);
            isStopping = true;
            socket.Close();
            socket = null;
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
                        enabledLogChannels |= ushort.MaxValue;
                        break;
                    case "none":
                        enabledLogChannels = 0;
                        break;
                    case "status":
                        enabledLogChannels |= 1;
                        break;
                    case "gliderlog":
                        enabledLogChannels |= 2;
                        break;
                    case "chat":
                        enabledLogChannels |= 8;
                        break;
                    case "chatsay":
                        enabledLogChannels |= 16;
                        break;
                    case "combat":
                        enabledLogChannels |= 4;
                        break;
                    case "chatraw":
                        enabledLogChannels |= 32;
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
                else
                {
                    if (!StartupClass.IsGliderInitialized)
                    {
                        StartupClass.InitializeBackgroundModeIfNeeded();
                    }

                    switch (lower3)
                    {
                        case "true":
                            InputController.smethod_21(true);
                            method_6("Mouse grabbed\r\n");
                            break;
                        case "false":
                            InputController.smethod_21(false);
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
                        enabledLogChannels = 0;
                        goto case "none";
                    case "none":
                        method_6("Log mode removed: " + lower4 + "\r\n");
                        break;
                    case "status":
                        enabledLogChannels &= -2;
                        goto case "none";
                    case "gliderlog":
                        enabledLogChannels &= -3;
                        goto case "none";
                    case "chat":
                        enabledLogChannels &= -9;
                        goto case "none";
                    case "chatsay":
                        enabledLogChannels &= -17;
                        goto case "none";
                    case "combat":
                        enabledLogChannels &= -5;
                        goto case "none";
                    case "chatraw":
                        enabledLogChannels &= -33;
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
                        escapeHighBitCharacters = true;
                        break;
                    case "off":
                        escapeHighBitCharacters = false;
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
                    if (StartupClass.CurrentGlideMode == GlideMode.Auto)
                    {
                        StartupClass.ActiveCombatController.method_23(str, false);
                        method_6("Queued for sending\r\n");
                    }
                    else
                    {
                        if (StartupClass.IsGliderInitialized || !InputController.UseClipboard)
                        {
                            InputController.TapKey(13);
                            StartupClass.SleepMilliseconds(300);
                        }

                        InputController.smethod_28(str);
                        method_6("Sent\r\n");
                    }
                }

                if (lower1 == "/loadprofile" && strArray1.Length > 1)
                {
                    if (StartupClass.CurrentGlideMode != GlideMode.None)
                    {
                        method_6("Failed: can't load a profile while gliding!\r\n");
                        return;
                    }

                    var str = string_1.Substring(string_1.IndexOf(" ") + 1);
                    method_6(StartupClass.TryLoadProfileOrProfileGroup(str[1] != ':' ? "Profiles\\" + str : str)
                        ? "Loaded profile ok\r\n"
                        : "Load profile failed - bogus name?\r\n");
                }

                if (lower1 == "/exitglider")
                {
                    method_6("Exiting Glider, this connection will close very soon!\r\n");
                    Thread.Sleep(1000);
                    StartupClass.SignalKillEventOrExit();
                }
                else
                {
                    if (lower1 == "/queuekeys" && strArray1.Length > 1)
                    {
                        var str = string_1.Substring(string_1.IndexOf(" ") + 1);
                        if (StartupClass.CurrentGlideMode == GlideMode.Auto)
                        {
                            StartupClass.ActiveCombatController.method_23(str, true);
                            method_6("Queued for sending\r\n");
                        }
                        else
                        {
                            StartupClass.SendInputSequence(str);
                            method_6("Keys sent\r\n");
                        }
                    }

                    if (lower1 == "/forcekeys" && strArray1.Length > 1)
                    {
                        var string_11 = string_1.Substring(string_1.IndexOf(" ") + 1);
                        lock (SpellcastingManager.gclass42_0)
                        {
                            StartupClass.SendInputSequence(string_11);
                        }

                        method_6("Keys sent\r\n");
                    }

                    if (lower1 == "/holdkey" && strArray1.Length > 1)
                    {
                        var short_0 = smethod_1(string_1.Substring(string_1.IndexOf(" ") + 1));
                        if (short_0 > 0)
                        {
                            InputController.SendKey((short)short_0, true);
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
                            InputController.SendKey((short)short_0, false);
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
                        method_6("Game: " + StartupClass.WowVersionLabel + "\r\n");
                    }

                    if (lower1 == "/selectgame")
                    {
                        StartupClass.BringGameToForeground();
                        GameMemoryAccess.CloseCurrentProcessHandle();
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
                                    StartupClass.RestoreGameWindowSize();
                                if (StartupClass.IsWindowHidden) StartupClass.ShowGameWindow();
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
                        if (StartupClass.CurrentGlideMode != GlideMode.None)
                        {
                            method_6("Failed: can't reconfigure while gliding, stop first!\r\n");
                        }
                        else
                        {
                            var str = ConfigManager.gclass61_0.method_2("AppKey");
                            ConfigManager.gclass61_0.method_3("Class");
                            ConfigManager.gclass61_0.method_7(false);
                            StartupClass.RequiresConfigReload = true;
                            StartupClass.KeyboardHook.method_0();
                            SpellcastingManager.gclass42_0.method_12();
                            InputController.smethod_31(ConfigManager.gclass61_0);
                            StartupClass.ApplyRuntimeConfiguration();
                            StartupClass.PartyStateManager.method_0(ConfigManager.gclass61_0);
                            if (str != ConfigManager.gclass61_0.method_2("AppKey") || StartupClass.PartyStateManager.bool_4 ||
                                !StartupClass.isInitializationSuccessful)
                            {
                                StartupClass.PartyStateManager.bool_4 = false;
                                StartupClass.DetachRuntime();
                                StartupClass.BeginBackgroundInitialization();
                            }

                            method_6("Success: loaded config\r\n");
                        }
                    }

                    if (lower1 == "/status")
                    {
                        var bool13 = StartupClass.IsRuntimeAttached;
                        var glideMode0 = StartupClass.CurrentGlideMode;
                        var stringBuilder = new StringBuilder();
                        method_6("Version: 1.8.0\r\n");
                        method_6("Attached: " + bool13 + "\r\n");
                        method_6("Mode: " + glideMode0 + "\r\n");
                        method_6("Profile: " + StartupClass.ActiveProfilePath + "\r\n");
                        if ((enabledLogChannels & 8) > 0)
                            stringBuilder.Append("Chat ");
                        if ((enabledLogChannels & 32) > 0)
                            stringBuilder.Append("ChatRaw ");
                        if ((enabledLogChannels & 16) > 0)
                            stringBuilder.Append("ChatSay ");
                        if ((enabledLogChannels & 2) > 0)
                            stringBuilder.Append("GliderLog ");
                        if ((enabledLogChannels & 1) > 0)
                            stringBuilder.Append("Status ");
                        if ((enabledLogChannels & 4) > 0)
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
                            method_6("XP/Hour: " + StartupClass.GetExperiencePerHour() + "\r\n");
                            method_6("Location: " + GPlayerSelf.Me.Location.ToString3D() + "\r\n");
                            method_6("Heading: " + GPlayerSelf.Me.Heading + "\r\n");
                            method_6("Pitch: " + GPlayerSelf.Me.Pitch + "\r\n");
                            method_6("KLD: " + StartupClass.DynamicClassCount + "/" + StartupClass.CompiledClassCount + "/" +
                                     StartupClass.InternalClassCount + "\r\n");
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
                        if (StartupClass.IsRuntimeAttached)
                            method_6("Already attached\r\n");
                        else
                            method_6("Attach command not supported any more, passive attach is automatic\r\n");
                    }

                    if (lower1 == "/clearsay")
                    {
                        if (StartupClass.CurrentGlideMode != GlideMode.Auto)
                        {
                            method_6("Not gliding, nothing to clear\r\n");
                            return;
                        }

                        if (StartupClass.ActiveCombatController.method_25())
                        {
                            StartupClass.ActiveCombatController.method_24();
                            method_6("Queue cleared\r\n");
                        }
                        else
                        {
                            method_6("Nothing queued to remove\r\n");
                        }
                    }

                    if (lower1 == "/startglide")
                    {
                        if (!StartupClass.IsRuntimeAttached)
                        {
                            method_6("Attaching\r\n");
                            StartupClass.TryAttachAndInitializeRuntime();
                            if (!StartupClass.IsRuntimeAttached)
                            {
                                method_6("Could not attach\r\n");
                                return;
                            }
                        }

                        if (StartupClass.CurrentGlideMode != GlideMode.None)
                        {
                            method_6("Can't start glide, already in auto mode\r\n");
                            return;
                        }

                        method_6("Attempting start\r\n");
                        StartupClass.StartAutoGlide(false);
                    }

                    if (lower1 == "/stopglide")
                    {
                        if (!StartupClass.IsRuntimeAttached)
                        {
                            method_6("Not attached, nothing to stop\r\n");
                            return;
                        }

                        if (StartupClass.CurrentGlideMode == GlideMode.None)
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
                        InputController.smethod_22(out double_2, out double_3);
                        method_6("Mouse coords: " + Math.Round(double_2, 3) + "/" + Math.Round(double_3, 3) + "\r\n");
                    }

                    if (lower1 == "/clickmouse" && strArray1.Length > 1)
                    {
                        if (strArray1[1].ToLower() == "left")
                        {
                            method_6("Clicked left button\r\n");
                            InputController.smethod_23(false);
                        }

                        if (strArray1[1].ToLower() == "right")
                        {
                            method_6("Clicked right button\r\n");
                            InputController.smethod_23(true);
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

                        var double_2 = StartupClass.ParseInvariantDouble(strArray2[0]);
                        var double_3 = StartupClass.ParseInvariantDouble(strArray2[1]);
                        InputController.smethod_18(double_2, double_3);
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
                        var int_2 = smethod_1(string_1.Substring(string_1.IndexOf(" ") + 1));
                        if (int_2 > 0)
                        {
                            method_6("New capture cache set\r\n");
                            ConfigManager.gclass61_0.method_0("CaptureDelay", int_2.ToString());
                            ConfigManager.gclass61_0.method_8();
                            server.gclass36_0 = new GameTimer(int_2);
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

    private void method_8(string filePath)
    {
        try
        {
            var readBuffer = new byte[1024];
            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                socket.Send(BitConverter.GetBytes(fileStream.Length), 0, 4, SocketFlags.None);
                while (true)
                {
                    var bytesRead = fileStream.Read(readBuffer, 0, 1024);
                    if (bytesRead != 0)
                        socket.Send(readBuffer, 0, bytesRead, SocketFlags.None);
                    else
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.LogMessage("** Exception in SendFile: " + ex.Message + ex.StackTrace);
            throw;
        }
    }

    private void method_9(MemoryStream stream)
    {
        var streamLength = (int)stream.Position;
        Logger.smethod_1("Stream length: " + streamLength);
        try
        {
            socket.Send(BitConverter.GetBytes(streamLength), 0, 4, SocketFlags.None);
            SendBytesInChunks(streamBuffer, streamLength, 1024);
        }
        catch (Exception ex)
        {
            Logger.LogMessage("** Exception in SendStream: " + ex.Message + ex.StackTrace);
            throw;
        }
    }

    private static int smethod_1(string string_1)
    {
        int parsedValue;
        return int.TryParse(string_1, out parsedValue) ? parsedValue : 0;
    }

    [SpecialName]
    public string method_10()
    {
        return queuedKeySequence;
    }

    [SpecialName]
    public void method_11(string string_1)
    {
        queuedKeySequence = string_1;
    }

    [SpecialName]
    public int method_12()
    {
        return captureCacheDurationMs;
    }

    [SpecialName]
    public void method_13(int int_16)
    {
        captureCacheDurationMs = int_16;
    }

    [SpecialName]
    public long method_14()
    {
        return captureQualityPercent;
    }

    [SpecialName]
    public void method_15(long long_1)
    {
        captureQualityPercent = ClampLong(long_1, 10L, 100L);
    }

    [SpecialName]
    public int method_16()
    {
        return captureScalePercent;
    }

    [SpecialName]
    public void method_17(int int_16)
    {
        captureScalePercent = ClampInt(int_16, 10, 100);
    }

    public void method_18()
    {
        if (!isCaptureInProgress && (cachedCaptureLength == 0 || lastCaptureTime.AddMilliseconds(method_12()) < DateTime.Now))
        {
            isCaptureInProgress = true;
            Bitmap image_0 = null;
            MemoryStream memoryStream = null;
            try
            {
                var intptr_0 = GameMemoryAccess.GetWindowHandle();
                if (intptr_0 == IntPtr.Zero)
                {
                    Logger.LogMessage("Capture failed: no window handle to game");
                    cachedCaptureLength = 0;
                    return;
                }

                image_0 = ScreenCapture.smethod_0(intptr_0);
                if (image_0 == null)
                {
                    Logger.LogMessage("Capture failed: unable to capture image");
                    cachedCaptureLength = 0;
                    return;
                }

                if (captureBuffer == null)
                    captureBuffer = new byte[CaptureBufferSize];
                memoryStream = new MemoryStream(captureBuffer);
                if (method_16() < 100)
                {
                    image_0 = ScreenCapture.smethod_2(image_0, method_16());
                    Logger.smethod_1("Resizing to pct: " + method_16());
                }
                else
                {
                    Logger.smethod_1("Not resizing, pct = " + method_16());
                }

                var encoderParams = new EncoderParameters(1);
                Logger.smethod_1("CaptureQuality: " + method_14());
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, method_14());
                if (jpegCodecInfo == null)
                    jpegCodecInfo = ScreenCapture.smethod_1("image/jpeg");
                image_0.Save(memoryStream, jpegCodecInfo, encoderParams);
                cachedCaptureLength = (int)memoryStream.Position;
                lastCaptureTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                Logger.LogMessage("Capture failed: " + ex.Message);
                cachedCaptureLength = 0;
            }
            finally
            {
                image_0?.Dispose();
                memoryStream?.Dispose();
                isCaptureInProgress = false;
            }
        }

        if (!isCaptureInProgress)
        {
            if (cachedCaptureLength <= 0)
                return;
            method_6("Success, 4-byte length and JPG image stream follow\r\n");
            Logger.smethod_1("Sending image size: " + cachedCaptureLength + " bytes");
            socket.Send(BitConverter.GetBytes(cachedCaptureLength), 0, 4, SocketFlags.None);
            SendBytesInChunks(captureBuffer, cachedCaptureLength, SocketChunkSize);
        }
        else
        {
            Logger.LogMessage("Capture skipped: capture cache is being updated");
        }
    }

    private static int ClampInt(int value, int minValue, int maxValue)
    {
        if (value < minValue)
            return minValue;
        return value > maxValue ? maxValue : value;
    }

    private static long ClampLong(long value, long minValue, long maxValue)
    {
        if (value < minValue)
            return minValue;
        return value > maxValue ? maxValue : value;
    }

    private void SendBytesInChunks(byte[] buffer, int totalLength, int chunkSize)
    {
        for (var offset = 0; offset < totalLength; offset += Math.Min(chunkSize, totalLength - offset))
            socket.Send(buffer, offset, Math.Min(chunkSize, totalLength - offset), SocketFlags.None);
    }
}




