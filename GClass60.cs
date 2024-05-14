// Decompiled with JetBrains decompiler
// Type: GClass60
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Threading;
using Glider.Common.Objects;

public class GClass60
{
    private readonly GPlayerSelf gplayerSelf_0;
    public Thread thread_0;

    public GClass60()
    {
        gplayerSelf_0 = GPlayerSelf.Me;
        StartupClass.ginterface0_0.imethod_0();
        thread_0 = null;
        thread_0 = new Thread(method_1);
        thread_0.Start();
    }

    public void method_0()
    {
        if (thread_0 != null && Thread.CurrentThread != thread_0)
        {
            thread_0.Interrupt();
            thread_0.Join();
        }

        thread_0 = null;
    }

    public void method_1()
    {
        try
        {
            if (!StartupClass.bool_19)
                method_2();
            StartupClass.smethod_27(false, "GlideThreadReturned");
        }
        catch (ThreadInterruptedException ex)
        {
        }
        catch (Exception ex)
        {
            GClass37.smethod_0(GClass30.smethod_2(702, ex.Message, ex.StackTrace));
            StartupClass.smethod_27(false, "GlideThreadExcep");
        }
    }

    public void method_2()
    {
        if (!StartupClass.IsGliderInitialized && GClass61.gclass61_0.method_5("BackgroundEnable") &&
            StartupClass.GliderManager != null)
        {
            StartupClass.IsGliderInitialized = true;
            StartupClass.GliderManager.method_34(StartupClass.AnotherIntegerValue, StartupClass.MainApplicationHandle);
        }

        var unit = GObjectList.FindUnit(gplayerSelf_0.TargetGUID);
        if (unit == null)
        {
            GClass37.smethod_0(GClass30.smethod_1(306));
        }
        else
        {
            if (unit.DistanceToSelf > (double)StartupClass.CurrentGameClass.PullDistance)
                unit.Approach(StartupClass.CurrentGameClass.PullDistance - 2.0, true);
            if (unit.Health == 0.0)
            {
                GClass37.smethod_0(GClass30.smethod_1(307));
            }
            else
            {
                StartupClass.smethod_17(1, GClass30.smethod_1(308));
                StartupClass.GameClass69Instance.method_9(unit.Name);
                GClass42.gclass42_0.method_23();
                GContext.Main.Me.SetTargetName(unit.Name);
                unit.TouchHealthDrop();
                StartupClass.CurrentGameClass.StartCombat();
                var num = (int)StartupClass.CurrentGameClass.KillTarget(unit, false);
            }
        }
    }
}