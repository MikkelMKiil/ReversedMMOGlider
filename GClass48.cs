// Decompiled with JetBrains decompiler
// Type: GClass48
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;
using Glider.Common;
using Glider.Common.Objects;

public class GClass48
{
    private const double double_0 = 8.0;
    public DateTime dateTime_0;
    public GClass51 gclass51_0;
    public GClass72 gclass72_0;
    private int int_0;
    private int int_1;
    private int int_2;
    private int int_3;
    public List<GClass72> list_0;
    public string string_0;

    public GClass48()
    {
        list_0 = new List<GClass72>();
        string_0 = "(new profile group)";
    }

    private void method_0()
    {
        dateTime_0 = DateTime.Now;
        int_0 = 0;
        int_1 = 0;
        int_2 = 0;
        gclass51_0 = null;
    }

    public void method_1(string string_1)
    {
        string_0 = string_1;
        var xmlDocument_0 = new XmlDocument();
        xmlDocument_0.AppendChild(xmlDocument_0.CreateXmlDeclaration("1.0", null, null));
        xmlDocument_0.AppendChild(xmlDocument_0.CreateElement("ProfileGroup"));
        method_3(xmlDocument_0, "FileVersion", "1");
        var element = xmlDocument_0.CreateElement("Steps");
        xmlDocument_0.DocumentElement.AppendChild(element);
        for (var index = 0; index < list_0.Count; ++index)
        {
            list_0[index].string_0 = method_2(list_0[index].string_0, string_1);
            list_0[index].method_1(element);
        }

        xmlDocument_0.Save(string_1);
    }

    private string method_2(string string_1, string string_2)
    {
        return string_1;
    }

    public void method_3(XmlDocument xmlDocument_0, string string_1, string string_2)
    {
        var element = xmlDocument_0.CreateElement(string_1);
        element.InnerText = string_2;
        xmlDocument_0.DocumentElement.AppendChild(element);
    }

    public bool method_4(string string_1)
    {
        string_0 = string_1;
        Logger.LogMessage("Reading profile group: " + string_1);
        try
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(string_1);
            int.Parse(xmlDocument.SelectSingleNode("/ProfileGroup/FileVersion").InnerText);
            foreach (XmlNode selectNode in xmlDocument.SelectNodes("/ProfileGroup/Steps/Step"))
            {
                var gclass72 = new GClass72();
                gclass72.method_4(selectNode);
                list_0.Add(gclass72);
            }

            if (StartupClass.ApplicationStartupMode == AppMode.Invisible || StartupClass.ApplicationStartupMode == AppMode.Normal)
                method_5();
            return true;
        }
        catch (Exception ex)
        {
            Logger.LogMessage("!! Exception loading " + string_1 + "!  " + ex.Message + ex.StackTrace);
            list_0.Clear();
            string_0 = "(new profile group)";
            return false;
        }
    }

    public void method_5()
    {
        foreach (var gclass72 in list_0)
            gclass72.method_10();
    }

    public GProfile method_6()
    {
        GClass72 gclass72_1 = null;
        var num = 99999.0;
        Logger.smethod_1("Looking for closest profile in group");
        foreach (var gclass72_2 in list_0)
            if (gclass72_2.gprofile_0.Waypoints.Count > 0 && gclass72_2.bool_2)
            {
                var distanceTo = gclass72_2.gprofile_0.GetDistanceTo(GPlayerSelf.Me.Location);
                if (distanceTo < num)
                {
                    gclass72_1 = gclass72_2;
                    num = distanceTo;
                }
            }

        method_0();
        gclass72_0 = gclass72_1;
        if (gclass72_0 == null)
        {
            Logger.LogMessage("No closest profile!");
            return null;
        }

        Logger.LogMessage("Closest profile: " + gclass72_0.string_0 + ", distance: " + num);
        StartupClass.gprofile_0 = gclass72_0.gprofile_0;
        return gclass72_0.gprofile_0;
    }

    public static void smethod_0()
    {
        if (StartupClass.gclass48_0 == null)
            return;
        ++StartupClass.gclass48_0.int_2;
        Logger.smethod_1("LogDeath, new count: " + StartupClass.gclass48_0.int_2);
    }

    public static void smethod_1()
    {
        if (StartupClass.gclass48_0 == null)
            return;
        ++StartupClass.gclass48_0.int_1;
        Logger.smethod_1("LogFollower, new count: " + StartupClass.gclass48_0.int_1);
    }

    public static void smethod_2()
    {
        if (StartupClass.gclass48_0 == null)
            return;
        ++StartupClass.gclass48_0.int_0;
        Logger.smethod_1("LogLoop, new count: " + StartupClass.gclass48_0.int_0);
    }

    public static bool smethod_3()
    {
        return StartupClass.gclass48_0 != null && StartupClass.gclass48_0.method_8() &&
               StartupClass.gclass48_0.method_7();
    }

    private bool method_7()
    {
        if (gclass51_0 == null || GPlayerSelf.Me.TargetGUID != 0L)
            return false;
        while (StartupClass.gclass73_0.method_19() || StartupClass.gclass73_0.method_52(true))
            Logger.LogMessage(MessageProvider.GetMessage(832));
        if (gclass72_0.bool_0)
            method_9();
        if (gclass51_0.int_1 < 0)
            method_17((GEnum15)gclass51_0.int_1);
        var gclass72 = list_0[gclass51_0.int_1];
        if (!gclass72_0.bool_0)
        {
            if (gclass72_0.gprofile_0.Waypoints.Count != 0 && gclass72.gprofile_0.Waypoints.Count != 0)
            {
                Logger.smethod_1("Looking for waypoint match between: " + gclass72_0.string_0 + " and " +
                                   gclass72.string_0);
                var num = method_10(gclass72_0.gprofile_0, gclass72.gprofile_0);
                var waypoint = gclass72_0.gprofile_0.Waypoints[num];
                double distanceTo = GPlayerSelf.Me.Location.GetDistanceTo(waypoint);
                if (gclass72_0.bool_1 && gclass72.gprofile_0.IgnoreAttackers)
                    method_15();
                if (distanceTo > 10.0)
                    method_12(num);
            }
            else
            {
                Logger.smethod_1("One profile is missing waypoints, assuming we're already there");
            }
        }

        gclass72_0 = gclass72;
        StartupClass.smethod_3(gclass72_0.string_0);
        gclass72_0.gprofile_0.Select();
        method_0();
        if (gclass72_0.gprofile_0.IgnoreAttackers && gclass72_0.bool_1 && !method_13())
            method_15();
        if (!gclass72_0.gprofile_0.IgnoreAttackers && method_13())
            smethod_4();
        return true;
    }

    private bool method_8()
    {
        var gclass51_1 = gclass72_0.method_6(GEnum6.const_0);
        if (gclass51_1 != null && int_0 > 0)
        {
            Logger.LogMessage(MessageProvider.smethod_2(831, gclass51_1.int_1, "Always"));
            gclass51_0 = gclass51_1;
            return true;
        }

        var gclass51_2 = gclass72_0.method_6(GEnum6.const_4);
        if (gclass51_2 != null && int_1 >= gclass51_2.int_0)
        {
            Logger.LogMessage(MessageProvider.smethod_2(831, gclass51_2.int_1, "Alerts"));
            gclass51_0 = gclass51_2;
            return true;
        }

        var gclass51_3 = gclass72_0.method_6(GEnum6.const_5);
        if (gclass51_3 != null && int_2 >= gclass51_3.int_0)
        {
            Logger.LogMessage(MessageProvider.smethod_2(831, gclass51_3.int_1, "Deaths"));
            gclass51_0 = gclass51_3;
            return true;
        }

        var gclass51_4 = gclass72_0.method_6(GEnum6.const_1);
        if (gclass51_4 != null && GPlayerSelf.Me.Level >= gclass51_4.int_0)
        {
            Logger.LogMessage(MessageProvider.smethod_2(831, gclass51_4.int_1, "Level"));
            gclass51_0 = gclass51_4;
            return true;
        }

        var gclass51_5 = gclass72_0.method_6(GEnum6.const_2);
        if (gclass51_5 != null && int_0 >= gclass51_5.int_0)
        {
            Logger.LogMessage(MessageProvider.smethod_2(831, gclass51_5.int_1, "Loops"));
            gclass51_0 = gclass51_5;
            return true;
        }

        var gclass51_6 = gclass72_0.method_6(GEnum6.const_3);
        if (gclass51_6 == null || (DateTime.Now - dateTime_0).TotalMinutes < gclass51_6.int_0)
            return false;
        Logger.LogMessage(MessageProvider.smethod_2(831, gclass51_6.int_1, "Time"));
        gclass51_0 = gclass51_6;
        return true;
    }

    private void method_9()
    {
        smethod_4();
        var unit = GObjectList.FindUnit(gclass72_0.string_1);
        if (unit == null)
        {
            Logger.LogMessage(MessageProvider.smethod_2(835, gclass72_0.string_1, gclass72_0.string_0));
            StartupClass.smethod_27(false, "NoFlightMaster");
        }

        unit.Approach();
        if (!unit.Interact())
        {
            Logger.LogMessage(MessageProvider.smethod_2(836, gclass72_0.string_1, gclass72_0.string_0));
            StartupClass.smethod_27(false, "CantClickFM");
        }

        Thread.Sleep(4000);
        var gclass8 = GClass8.smethod_2("GossipTitleButton1");
        if (gclass8 == null)
        {
            Logger.LogMessage(MessageProvider.smethod_2(836, gclass72_0.string_1, gclass72_0.string_0));
            StartupClass.smethod_27(false, "NoGossipFrame");
        }

        if (gclass8.method_10())
        {
            Logger.LogMessage("Gossip title is visible, positioning cursor");
            Thread.Sleep(1000);
            gclass8.method_16(false);
            Thread.Sleep(5000);
        }

        var gclass13 = new GClass13();
        gclass13.method_0();
        var num = gclass13.method_1(gclass72_0.string_2);
        if (num == 0)
        {
            Logger.LogMessage("Couldn't activate flight, stopping");
            StartupClass.smethod_27(false, "NoTaxiButton");
        }

        GClass8.smethod_2("TaxiButton" + num).method_16(false);
        Thread.Sleep(5000);
        if (!method_11())
        {
            Logger.LogMessage("!! Clicked on flightpoint, but we don't seem to be flying");
            StartupClass.smethod_27(false, "NotFlyingVeryWell");
        }

        Logger.LogMessage("Flight is underway");
        do
        {
            ;
        } while (method_11());

        Logger.LogMessage("Flight complete");
        Thread.Sleep(500);
        GClass42.gclass42_0.method_0("Common.Forward");
        Thread.Sleep(500);
    }

    private int method_10(GProfile gprofile_0, GProfile gprofile_1)
    {
        var num1 = 0;
        var num2 = 9999.0;
        for (var index1 = 0; index1 < gprofile_0.Waypoints.Count; ++index1)
        for (var index2 = 0; index2 < gprofile_1.Waypoints.Count; ++index2)
        {
            double distanceTo = gprofile_0.Waypoints[index1].GetDistanceTo(gprofile_1.Waypoints[index2]);
            if (distanceTo < num2)
            {
                num2 = distanceTo;
                num1 = index1;
            }
        }

        Logger.smethod_1("Best match is waypoint " + num1 + " in this profile");
        return num1;
    }

    private bool method_11()
    {
        var location = GPlayerSelf.Me.Location;
        Thread.Sleep(500);
        return location.GetDistanceTo(GPlayerSelf.Me.Location) > 0.0;
    }

    private void method_12(int int_4)
    {
        Logger.LogMessage(MessageProvider.smethod_2(833, int_4 + 1));
        gclass72_0.gprofile_0.BeginProfile(GPlayerSelf.Me.Location);
        var currentIndex = gclass72_0.gprofile_0.CurrentIndex;
        var flag1 = false;
        int num1;
        if (gclass72_0.gprofile_0.Reversible)
        {
            num1 = currentIndex >= int_4 ? -1 : 1;
        }
        else
        {
            Logger.smethod_1("MyIndex: " + currentIndex + ", DestIndex: " + int_4);
            var num2 = currentIndex - int_4;
            var num3 = int_4 - currentIndex;
            if (num2 < 0)
                num2 += gclass72_0.gprofile_0.Waypoints.Count;
            if (num3 < 0)
                num3 += gclass72_0.gprofile_0.Waypoints.Count;
            num1 = num2 >= num3 ? 1 : -1;
            Logger.smethod_1("LeftSteps: " + num2 + ", RightSteps: " + num3 + ", Delta: " + num1);
            if (num3 == 0 && num2 == 0)
            {
                Logger.smethod_1("Must be close to that waypoint, skipping move action");
                return;
            }
        }

        var gclass36_1 = new GClass36(15000);
        gclass36_1.method_4();
        var index = currentIndex;
        var gclass36_2 = new GClass36(1700);
        gclass36_2.method_4();
        GLocation L = null;
        var flag2 = false;
        var flag3 = method_13();
        while (true)
        {
            if (gclass36_1.method_3())
                goto label_41;
            label_9:
            var waypoint1 = gclass72_0.gprofile_0.Waypoints[index];
            if (GPlayerSelf.Me.Location.GetDistanceTo(waypoint1) < 8.0)
            {
                if (!flag1)
                {
                    index += num1;
                    if (index < 0)
                    {
                        if (gclass72_0.gprofile_0.Reversible)
                        {
                            index = 1;
                            num1 = 1;
                        }
                        else
                        {
                            index = gclass72_0.gprofile_0.Waypoints.Count - 1;
                        }
                    }

                    if (index == gclass72_0.gprofile_0.Waypoints.Count)
                    {
                        if (gclass72_0.gprofile_0.Reversible)
                        {
                            index -= 2;
                            num1 = -1;
                        }
                        else
                        {
                            index = 0;
                        }
                    }

                    if (index == int_4)
                        flag1 = true;
                    gclass36_1.method_4();
                }
                else
                {
                    break;
                }
            }

            if (gclass36_2.method_3())
            {
                if (L == null)
                {
                    L = GPlayerSelf.Me.Location;
                }
                else
                {
                    if (GPlayerSelf.Me.Location.GetDistanceTo(L) < 3.0)
                    {
                        if (GClass61.gclass61_0.method_5("StrafeObstacles") && !flag2)
                        {
                            Logger.LogMessage(MessageProvider.GetMessage(742));
                            var string_1 = "Common.StrafeLeft";
                            if (StartupClass.random_0.Next() % 2 == 0)
                                string_1 = "Common.StrafeRight";
                            GClass42.gclass42_0.method_1(string_1);
                            StartupClass.smethod_39(1200);
                            GClass42.gclass42_0.method_2(string_1);
                            flag2 = true;
                        }
                        else
                        {
                            Logger.LogMessage(MessageProvider.GetMessage(256));
                            GContext.Main.ReleaseSpinRun();
                            StartupClass.smethod_39(600);
                            GContext.Main.PressKey("Common.Back");
                            StartupClass.smethod_39(2000);
                            GContext.Main.ReleaseKey("Common.Back");
                            var NewHeading = StartupClass.random_0.Next() % 2 != 0
                                ? GPlayerSelf.Me.Heading + Math.PI / 2.0
                                : GPlayerSelf.Me.Heading - Math.PI / 2.0;
                            if (NewHeading > 2.0 * Math.PI)
                                NewHeading -= 2.0 * Math.PI;
                            GContext.Main.Movement.SetHeading(NewHeading);
                            GClass42.gclass42_0.method_1("Common.Forward");
                            Thread.Sleep(1700);
                            flag2 = false;
                        }
                    }

                    L = GPlayerSelf.Me.Location;
                }

                gclass36_2.method_4();
            }

            var waypoint2 = gclass72_0.gprofile_0.Waypoints[index];
            if (!GContext.Main.IsRunning)
                GContext.Main.Movement.BasePatrolTowards(waypoint2);
            else
                GContext.Main.Movement.SetHeading(waypoint2);
            if (flag3 && !method_13() && !GPlayerSelf.Me.IsInCombat)
            {
                Logger.LogMessage(MessageProvider.GetMessage(843));
                GContext.Main.ReleaseSpinRun();
                method_15();
                flag3 = method_13();
            }

            Thread.Sleep(200);
            continue;
            label_41:
            Logger.LogMessage(MessageProvider.GetMessage(834));
            StartupClass.smethod_27(false, "FutileStepInMoveToWP");
            goto label_9;
        }
    }

    public static void smethod_4()
    {
        if (StartupClass.gclass48_0 == null)
            return;
        StartupClass.gclass48_0.method_14();
    }

    public static bool smethod_5()
    {
        return StartupClass.gclass48_0 != null && StartupClass.gclass48_0.method_13();
    }

    [SpecialName]
    private bool method_13()
    {
        return int_3 != 0 && GPlayerSelf.Me.HasBuff(int_3);
    }

    private void method_14()
    {
        if (!method_13())
            return;
        GContext.Main.ReleaseSpinRun();
        GContext.Main.CastSpell("Common.Mount", false, true);
        if (GClass42.gclass42_0.method_15("Common.PostDismount"))
            return;
        GContext.Main.CastSpell("Common.PostDismount");
    }

    private void method_15()
    {
        int_3 = 0;
        while (GPlayerSelf.Me.IsInCombat)
        {
            if (GPlayerSelf.Me.TargetGUID != 0L)
            {
                StartupClass.gclass73_0.method_12(true);
                StartupClass.gclass73_0.method_52(true);
            }

            Thread.Sleep(500);
        }

        StartupClass.CurrentGameClass.LeaveForm();
        GContext.Main.ReleaseAllKeys();
        Thread.Sleep(1500);
        var buffSnapshot = GPlayerSelf.Me.GetBuffSnapshot();
        if (!GClass42.gclass42_0.method_15("Common.PreMount"))
            GContext.Main.CastSpell("Common.PreMount");
        GContext.Main.CastSpell("Common.Mount");
        var gclass36 = new GClass36(4000);
        gclass36.method_4();
        while (!gclass36.method_3())
        {
            int_3 = method_16(buffSnapshot);
            Logger.smethod_1("Looking for new mount buff id: " + int_3.ToString("x"));
            if (int_3 == 0)
                Thread.Sleep(100);
            else
                break;
        }

        if (int_3 != 0)
            return;
        Logger.LogMessage("! Never got a new buff after trying to mount");
    }

    private int method_16(GBuff[] gbuff_0)
    {
        var gbuffArray = method_20(gbuff_0, GPlayerSelf.Me.GetBuffSnapshot());
        if (gbuffArray.Length != 1)
            return 0;
        Logger.smethod_1("Mounted buffid: " + gbuffArray[0].SpellID + ", name =\"" + gbuffArray[0].SpellName + "\"");
        return gbuffArray[0].SpellID;
    }

    public static void smethod_6()
    {
        if (StartupClass.gclass48_0 == null || !StartupClass.gclass48_0.gclass72_0.bool_1)
            return;
        StartupClass.gclass48_0.method_15();
    }

    private void method_17(GEnum15 genum15_0)
    {
        switch (genum15_0)
        {
            case GEnum15.const_2:
                Logger.LogMessage(MessageProvider.smethod_2(844, "HearthAndLog"));
                StartupClass.gclass73_0.method_21(true);
                break;
            case GEnum15.const_1:
                StartupClass.gclass73_0.method_22();
                break;
            case GEnum15.const_0:
                Logger.LogMessage(MessageProvider.smethod_2(844, "Stop"));
                StartupClass.smethod_27(false, "ForceStepStop");
                break;
        }
    }

    public void method_18()
    {
        while (true)
        {
            StartupClass.gclass73_0.gprofile_0 = gclass72_0.gprofile_0;
            gclass72_0.gprofile_0.Select();
            gclass72_0.gprofile_0.BeginProfile(GPlayerSelf.Me.Location);
            Logger.smethod_1("Going into run loop for: " + gclass72_0.string_0);
            if (!gclass72_0.gprofile_0.Fishing)
            {
                if (gclass72_0.gprofile_0.NaturalRun)
                {
                    StartupClass.gclass73_0.method_39();
                }
                else
                {
                    GContext.Main.ReleaseSpinRun();
                    StartupClass.gclass73_0.method_5();
                }
            }
            else
            {
                GContext.Main.ReleaseSpinRun();
                StartupClass.gclass73_0.method_29();
            }
        }
    }

    private bool method_19(GBuff[] gbuff_0, int int_4)
    {
        foreach (var gbuff in gbuff_0)
            if (gbuff.SpellID == int_4)
                return true;
        return false;
    }

    private GBuff[] method_20(GBuff[] gbuff_0, GBuff[] gbuff_1)
    {
        var gbuffList = new List<GBuff>();
        foreach (var gbuff in gbuff_0)
            if (!method_19(gbuff_1, gbuff.SpellID))
                gbuffList.Add(gbuff);
        foreach (var gbuff in gbuff_1)
            if (!method_19(gbuff_0, gbuff.SpellID))
                gbuffList.Add(gbuff);
        return gbuffList.ToArray();
    }
}