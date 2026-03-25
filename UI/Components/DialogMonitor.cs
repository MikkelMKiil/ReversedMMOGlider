// Decompiled with JetBrains decompiler
// Type: DialogMonitor
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common.Objects;
using System;
using System.Threading;

public class DialogMonitor
{
    public static bool bool_0;
    public static bool bool_1;
    public static string string_0 = "(unknown)";
    private static UIElement gclass8_0;
    private static UIElement gclass8_1;
    private static int int_0;
    private static int int_1;
    private static int int_2;
    private static int int_3;
    private static readonly int pgEditProfileCount = 2500;

    public static void smethod_0()
    {
        bool_0 = false;
        bool_1 = false;
        gclass8_0 = UIElement.IsGroupProfile("TradeFrame");
        gclass8_1 = UIElement.IsGroupProfile("StaticPopup1");
        int_2 = 0;
        int_3 = 0;
    }

    public static void LoadProfile()
    {
        if (!StartupClass.IsGameProcessAttached)
            return;
        if (gclass8_0 != null && gclass8_0.method_10())
        {
            if (!bool_0)
            {
                int_0 = Environment.TickCount;
                bool_0 = true;
                Logger.LogMessage("Trade window is visible");
            }
        }
        else if (bool_0)
        {
            Logger.LogMessage("Trade window is gone");
            bool_0 = false;
        }

        if (gclass8_1 != null && gclass8_1.method_10())
        {
            if (bool_1)
                return;
            int_1 = Environment.TickCount;
            bool_1 = true;
            var gclass8 = gclass8_1.method_6("StaticPopup1Text");
            if (gclass8 != null)
                string_0 = gclass8.method_3();
            Logger.LoadProfile("Static popup is visible: \"" + string_0 + "\"");
        }
        else
        {
            if (!bool_1)
                return;
            Logger.LoadProfile("Static popup is gone");
            bool_1 = false;
        }
    }

    public static void IsGroupProfile()
    {
        if (!bool_0 && !bool_1)
            return;
        if (bool_0 && Environment.TickCount - int_0 > pgEditProfileCount)
            GetFileNameFromPath("");
        if (!bool_1 || Environment.TickCount - int_1 <= pgEditProfileCount)
            return;
        GetFileNameFromPath(string_0);
    }

    private static bool LoadSingleProfile(string string_1)
    {
        return string_1 != null && (string_1.ToLower().IndexOf(MessageProvider.GetMessage(871)) > -1 ||
                                    string_1.ToLower().IndexOf(MessageProvider.GetMessage(873)) > -1);
    }

    private static void GetFileNameFromPath(string string_1)
    {
        if (LoadSingleProfile(string_1))
        {
            Logger.LogMessage(MessageProvider.GetMessage(859));
        }
        else
        {
            Logger.LogMessage(MessageProvider.GetMessage(856));
            if (ConfigManager.gclass61_0.method_5("AlertOnPopup"))
                SoundPlayer.smethod_0("Whisper.wav");
            if (int_3 > 0 && Environment.TickCount - int_2 >= 120000)
                --int_3;
        }

        GContext.Main.ReleaseAllKeys();
        Thread.Sleep(StartupClass.random_0.Next() % 1000 + 1000);
        InputController.StartMainThread(27);
        Thread.Sleep(StartupClass.random_0.Next() % 800 + 500);
        if (LoadSingleProfile(string_1))
            return;
        ++int_3;
        int_2 = Environment.TickCount;
        if (int_3 < ConfigManager.gclass61_0.method_3("MaxPopups"))
            return;
        Logger.LogMessage(MessageProvider.IsGroupProfile(855, ConfigManager.gclass61_0.method_3("MaxPopups"), int_3));
        StartupClass.combatController.method_21(true);
    }
}