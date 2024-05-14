// Decompiled with JetBrains decompiler
// Type: GClass81
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Glider.Common;
using Glider.Common.Objects;

public class GClass81 : GInterface0
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
        StartupClass.smethod_17(2, string_0);
    }

    void GInterface0.imethod_0()
    {
    }

    void GInterface0.imethod_1()
    {
        bool_0 = !bool_0;
    }

    void GInterface0.imethod_4()
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
        StartupClass.genum2_0 = GEnum2.const_0;
        thread_0 = new Thread(method_3);
        thread_0.Start();
    }

    public static void smethod_0()
    {
        try
        {
            new GClass81().method_0();
        }
        catch (Exception ex)
        {
            var num = (int)MessageBox.Show("** Last chance exception from inviso: " + ex.Message + "\r\n\r\n" +
                                           ex.StackTrace);
        }
    }

    private void method_1()
    {
        var fileInfo1 = new FileInfo("Glider.log");
        if (!fileInfo1.Exists)
            return;
        var fileInfo2 = new FileInfo("Glider.LastRun.log");
        if (fileInfo2.Exists)
            fileInfo2.Delete();
        fileInfo1.MoveTo("Glider.LastRun.log");
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
            Thread.Sleep(200);
            if (bool_0 && StartupClass.bool_13)
                goto label_6;
            label_1:
            if (!StartupClass.isInputStringFourCharacters)
                StartupClass.gclass36_0 = null;
            if (StartupClass.gclass36_0 != null && StartupClass.gclass36_0.method_3())
            {
                StartupClass.gclass36_0 = null;
                StartupClass.bool_19 = true;
                Logger.LogMessage(MessageProvider.GetMessage(103));
                StartupClass.smethod_27(false, "Timer2Up");
            }

            StartupClass.smethod_38();
            continue;
            label_6:
            if (glocation_0 == null)
            {
                glocation_0 = GPlayerSelf.Me.Location;
            }
            else if (GPlayerSelf.Me.Location.GetDistanceTo(glocation_0) > StartupClass.double_0)
            {
                StartupClass.smethod_23();
                glocation_0 = GPlayerSelf.Me.Location;
                GClass20.smethod_0("Key.wav");
                goto label_1;
            }
            else
            {
                goto label_1;
            }
        }
    }
}