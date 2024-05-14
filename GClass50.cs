// Decompiled with JetBrains decompiler
// Type: GClass50
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Glider.Common;
using Glider.Common.Objects;

public class GClass50
{
    protected const double double_0 = 15.0;
    public const double double_1 = 0.05;
    private const int int_0 = 4;
    public static double double_2;
    public static double double_3;
    public bool bool_0;
    protected bool bool_1;
    public double double_4;
    public double double_5;
    private double double_6;
    private GLocation glocation_0;
    public GPlayerSelf gplayerSelf_0;
    private readonly GSpellTimer gspellTimer_0;
    private readonly GSpellTimer gspellTimer_1 = new GSpellTimer(5500);
    private int int_1;
    public long long_0;

    public GClass50()
    {
        if (GPlayerSelf.Me != null)
            gplayerSelf_0 = GPlayerSelf.Me;
        gspellTimer_0 = new GSpellTimer(60000);
        method_0();
    }

    public void method_0()
    {
        double_4 = GClass61.gclass61_0.method_3("RestHealth") / 100.0;
        double_5 = GClass61.gclass61_0.method_3("RestMana") / 100.0;
    }

    public void method_1()
    {
        gspellTimer_1.Reset();
        int_1 = 0;
        double_6 = 0.0;
    }

    public bool method_2(GUnit gunit_0, bool bool_2)
    {
        if (gunit_0.Health != double_6)
        {
            double_6 = gunit_0.Health;
            gspellTimer_1.Reset();
            return false;
        }

        if (!gspellTimer_1.IsReady)
            return false;
        gspellTimer_1.Reset();
        var str = GProcessMemoryManipulator.smethod_9(GClass18.gclass18_0.method_4("RedMessage"), 128, "RedMessage") ??
                  GClass30.smethod_1(6);
        GClass37.smethod_1(GClass30.smethod_2(7, str));
        ++int_1;
        if (int_1 == 4)
        {
            GClass37.smethod_0(GClass30.smethod_1(8));
            return true;
        }

        if (bool_2 && (str.ToLower().IndexOf(GClass30.smethod_1(629)) >= 0 ||
                       str.ToLower().IndexOf(GClass30.smethod_1(630)) >= 0))
        {
            GClass37.smethod_0(GClass30.smethod_1(9));
            if (gunit_0.DistanceToSelf > double_2)
            {
                gunit_0.Approach();
            }
            else
            {
                GClass37.smethod_0(GClass30.smethod_1(10));
                gunit_0.Approach(GContext.Main.MeleeDistance - 2.0);
            }
        }

        return false;
    }

    protected void method_3(GMonster gmonster_0, int int_2, int int_3)
    {
        method_5(gmonster_0, int_2, int_3);
    }

    [SpecialName]
    public bool method_4()
    {
        return StartupClass.glideMode_0 == GlideMode.Auto && StartupClass.gclass54_0.healMode_0 == HealMode.Dedicated &&
               StartupClass.gclass54_0.genum7_0 != GEnum7.const_0;
    }

    public bool method_5(GMonster gmonster_0, double double_7, int int_2)
    {
        var gspellTimer = new GSpellTimer(int_2, false);
        while (!gspellTimer.IsReadySlow)
            if (gmonster_0.DistanceToSelf <= double_7)
                return true;
        return false;
    }

    public void method_6(string string_0, bool bool_2)
    {
        GContext.Main.CastSpell(string_0, true, !bool_2);
    }

    public void method_7(string string_0)
    {
        GContext.Main.CastSpell(string_0);
    }

    public void method_8(string string_0)
    {
        GClass37.smethod_1(GClass30.smethod_2(12, string_0));
        GClass42.gclass42_0.method_0(string_0);
    }

    public void method_9(string string_0)
    {
        GContext.Main.CastSpell(string_0);
    }

    public virtual bool vmethod_0(GUnit gunit_0)
    {
        return false;
    }

    public virtual bool vmethod_1()
    {
        return false;
    }

    public virtual void vmethod_2()
    {
    }

    public virtual bool vmethod_3()
    {
        return true;
    }

    public virtual void vmethod_4()
    {
    }

    public virtual void vmethod_5(Label label_0)
    {
        label_0.Text = GClass30.smethod_1(20);
    }

    [SpecialName]
    public virtual string vmethod_6()
    {
        return GClass30.smethod_2(632, gplayerSelf_0.Mana, (int)(gplayerSelf_0.Mana * 100.0));
    }

    public virtual void vmethod_7()
    {
    }

    public virtual void vmethod_8()
    {
    }

    public virtual void vmethod_9()
    {
    }

    public virtual bool vmethod_10()
    {
        return false;
    }

    public virtual void vmethod_11(GPlayer gplayer_0, GLocation glocation_1)
    {
    }

    public virtual bool vmethod_12(GUnit gunit_0, bool bool_2, bool bool_3)
    {
        return true;
    }

    public virtual bool vmethod_13(GUnit gunit_0, bool bool_2, bool bool_3)
    {
        return true;
    }

    public virtual DialogResult vmethod_14()
    {
        GClass37.smethod_0(GClass30.smethod_1(21));
        return DialogResult.Cancel;
    }

    public virtual bool vmethod_15()
    {
        return true;
    }

    public virtual int vmethod_16()
    {
        return 0;
    }

    public virtual void vmethod_17()
    {
    }

    public virtual void vmethod_18()
    {
    }

    public virtual bool vmethod_19()
    {
        return false;
    }

    [SpecialName]
    public virtual bool vmethod_20()
    {
        return false;
    }

    [SpecialName]
    public virtual bool vmethod_21()
    {
        return false;
    }

    public virtual void vmethod_22(bool bool_2)
    {
    }

    public void method_10()
    {
        for (var index = 0; index < 5; ++index)
        {
            GContext.Main.Movement.SetHeading(StartupClass.random_0.NextDouble() * 6.14);
            StartupClass.gclass73_0.method_34(1500, 5000);
            if (StartupClass.random_0.Next() % 3 == 0)
            {
                GClass42.gclass42_0.method_0("Common.Jump");
                StartupClass.gclass73_0.method_34(1500, 3000);
            }
        }
    }

    public void method_11()
    {
        glocation_0 = gplayerSelf_0.Location;
    }

    public void method_12()
    {
        if (StartupClass.glideMode_0 != GlideMode.Auto || glocation_0 == null)
            return;
        double distanceTo = glocation_0.GetDistanceTo(gplayerSelf_0.Location);
        if (distanceTo <= 100.0)
            return;
        GClass37.smethod_0(GClass30.smethod_2(634, distanceTo));
        glocation_0 = gplayerSelf_0.Location;
        StartupClass.gclass73_0.method_58();
    }

    public void method_13()
    {
        method_11();
    }

    public void method_14()
    {
        method_12();
    }

    public bool method_15()
    {
        if (GObjectList.GetNearestAttacker(0L) != null || !(GClass61.gclass61_0.method_2("UseBandages") == "True"))
            return false;
        if (gplayerSelf_0.Health * 100.0 < GClass61.gclass61_0.method_3("BandageHealth") && gspellTimer_0.IsReady)
        {
            GClass37.smethod_0(GClass30.smethod_1(27));
            var actionInventory = GContext.Main.Interface.GetActionInventory("Common.ApplyBandage");
            GClass37.smethod_1(GClass30.smethod_2(28, actionInventory));
            if (actionInventory == 0)
                return false;
            var gclass36 = new GClass36(8000);
            StartupClass.smethod_39(200);
            GClass42.gclass42_0.method_0("Common.ApplyBandage");
            StartupClass.smethod_39(500);
            method_9("Common.TargetSelf");
            gspellTimer_0.Reset();
            GClass73.smethod_1();
        }

        return true;
    }
}