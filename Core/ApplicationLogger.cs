// Decompiled with JetBrains decompiler
// Type: ApplicationLogger
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common;
using Glider.Common.Objects;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

public class ApplicationLogger : ILogger
{
    private bool bool_0;
    private GLocation glocation_0;
    private Thread thread_0;

    public void imethod_3(string string_0)
    {
        if (!StartupClass.IsBetaAccessGranted)
            return;
        this.imethod_2("[Debug] " + string_0);
    }

    public void imethod_2(string string_0)
    {
        lock (this)
        {
            method_2(string_0);
        }

        if (string_0.StartsWith("[Debug]"))
            return;
        StartupClass.NotifyStatusChange(2, string_0);
    }

    void ILogger.imethod_0()
    {
    }

    void ILogger.imethod_1()
    {
        bool_0 = !bool_0;
    }

    void ILogger.imethod_4()
    {
        if (thread_0 != null)
            thread_0.Interrupt();
        Environment.Exit(0);
    }

    public void method_0()
    {
        method_1();
        StartupClass.ginterface0_0 = this;
        StartupClass.InitStartupMode(AppMode.Normal);
        StartupClass.genum2_0 = WaypointType.const_0;
        thread_0 = new Thread(method_3);
        thread_0.Start();
    }

    public static void smethod_0()
    {
        try
        {
            new ApplicationLogger().method_0();
        }
        catch (Exception ex)
        {
            var num = (int)MessageBox.Show("** Last chance exception from inviso: " + ex.Message + "\r\n\r\n" +
                                           ex.StackTrace);
        }
    }

    private void method_1()
    {
        var path = "Glider.log";
        if (File.Exists(path))
            File.Delete(path);
        using (File.Create(path))
        {
        }
    }

    private void method_2(string string_0)
    {
        var now = DateTime.Now;
        var path = "Glider.log";
        try
        {
            var streamWriter = File.AppendText(path);
            if (StartupClass.IsBetaAccessGranted)
                streamWriter.WriteLine(now.ToLongTimeString() + " (" + now.Millisecond + ") " + string_0);
            else
                streamWriter.WriteLine(now.ToLongTimeString() + " " + string_0);
            streamWriter.Flush();
            streamWriter.Close();
        }
        catch (IOException ex)
        {
            Console.WriteLine(MessageProvider.smethod_2(90, ex.Message));
        }
    }

    public void method_3()
    {
        try
        {
            method_4();
        }
        catch (ThreadInterruptedException ex)
        {
            this.imethod_2("Work thread interrupted, shutting down now");
        }
        catch (Exception ex)
        {
            this.imethod_2("!! Exception in workthread: " + ex.Message + ex.StackTrace);
        }
    }

    private void method_4()
    {
        while (true)
        {
            Thread.smethod_39(200);
            if (bool_0 && StartupClass.IsGameProcessAttached)
                goto label_6;
        label_1:
            if (!StartupClass.isInputStringFourCharacters)
                StartupClass.licenseCheckTimer = null;
            if (StartupClass.licenseCheckTimer != null && StartupClass.licenseCheckTimer.method_3())
            {
                StartupClass.licenseCheckTimer = null;
                StartupClass.IsVersionMismatch = true;
                Logger.LogMessage(MessageProvider.GetMessage(103));
                StartupClass.StopGlide(false, "Timer2Up");
            }

            StartupClass.TickMainLoop();
            continue;
        label_6:
            if (glocation_0 == null)
            {
                glocation_0 = GPlayerSelf.Me.Location;
            }
            else if (GPlayerSelf.Me.Location.GetDistanceTo(glocation_0) > StartupClass.autoAddDistance)
            {
                StartupClass.AddWaypoint();
                glocation_0 = GPlayerSelf.Me.Location;
                SoundPlayer.smethod_0("Key.wav");
                goto label_1;
            }
            else
            {
                goto label_1;
            }
        }
    }
}