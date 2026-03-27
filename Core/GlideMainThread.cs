// Decompiled with JetBrains decompiler
// Type: GlideMainThread
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common.Objects;
using System;
using System.Threading;

public class GlideMainThread
{
    private const int int_0 = 5000;
    private readonly GPlayerSelf gplayerSelf_0;
    public Thread thread_0;

    public GlideMainThread()
    {
        gplayerSelf_0 = GPlayerSelf.Me;
        StartupClass.StartupLogger.imethod_0();
        thread_0 = null;
        thread_0 = new Thread(method_1);
        thread_0.IsBackground = true;
        thread_0.Start();
    }

    public void method_0()
    {
        if (thread_0 != null && Thread.CurrentThread != thread_0)
        {
            thread_0.Interrupt();
            if (!thread_0.Join(int_0))
                Logger.LogMessage("Glide main thread did not stop within 5000ms after interrupt.");
        }

        thread_0 = null;
    }

    public void method_1()
    {
        try
        {
            if (!StartupClass.HasClassLoadMismatch)
                method_2();
            StartupClass.smethod_27(false, "GlideThreadReturned");
        }
        catch (ThreadInterruptedException ex)
        {
        }
        catch (Exception ex)
        {
            Logger.LogMessage(MessageProvider.smethod_2(702, ex.Message, ex.StackTrace));
            StartupClass.smethod_27(false, "GlideThreadExcep");
        }
    }

    public void method_2()
    {
        StartupClass.smethod_62();

        var player = GPlayerSelf.Me ?? gplayerSelf_0;
        if (player == null)
        {
            Logger.LogMessage("Unable to acquire player object for one-kill action");
            return;
        }

        var unit = GObjectList.FindUnit(player.TargetGUID);
        if (unit == null)
        {
            Logger.LogMessage(MessageProvider.GetMessage(306));
        }
        else
        {
            if (unit.DistanceToSelf > (double)StartupClass.CurrentGameClass.PullDistance)
                unit.Approach(StartupClass.CurrentGameClass.PullDistance - 2.0, true);
            if (unit.Health == 0.0)
            {
                Logger.LogMessage(MessageProvider.GetMessage(307));
            }
            else
            {
                StartupClass.smethod_17(1, MessageProvider.GetMessage(308));
                StartupClass.GameClass69Instance.method_9(unit.Name);
                SpellcastingManager.gclass42_0.method_23();
                GContext.Main.Me.SetTargetName(unit.Name);
                unit.TouchHealthDrop();
                StartupClass.CurrentGameClass.StartCombat();
                var num = (int)StartupClass.CurrentGameClass.KillTarget(unit, false);
            }
        }
    }
}

