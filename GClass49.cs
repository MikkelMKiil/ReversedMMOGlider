// Decompiled with JetBrains decompiler
// Type: GClass49
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Threading;

public class GClass49
{
    public Thread thread_0;

    public bool method_0()
    {
        Logger.smethod_1("Starting mach glide thread");
        thread_0 = new Thread(method_2);
        thread_0.Start();
        return true;
    }

    public void method_1()
    {
        if (thread_0 == null)
            return;
        if (Thread.CurrentThread != thread_0)
        {
            thread_0.Interrupt();
            thread_0.Join();
        }

        thread_0 = null;
    }

    private void method_2()
    {
        try
        {
            method_3();
            StartupClass.smethod_27(false, "MachStopClean");
        }
        catch (ThreadInterruptedException ex)
        {
            Logger.LogMessage("Mach thread stopped");
        }
        catch (Exception ex)
        {
            Logger.LogMessage("! Exception in MachThreadBody: " + ex.Message + "\r\n" + ex.StackTrace);
            StartupClass.smethod_27(true, "MTBExcep");
        }
    }

    private void method_3()
    {
        if (StartupClass.GliderManager != null)
            StartupClass.GliderManager.method_33(true);
        new GClass40().method_0();
    }
}