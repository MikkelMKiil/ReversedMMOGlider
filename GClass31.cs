// Decompiled with JetBrains decompiler
// Type: GClass31
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Glider.Common.Objects;

public class GClass31
{
    private GClass14 gclass14_0;
    private readonly GContext gcontext_0;
    private string string_0;

    public GClass31()
    {
        gcontext_0 = GContext.Main;
    }

    public string method_0()
    {
        var byNamePreWorld1 = GContext.Main.Interface.GetByNamePreWorld("GlueDialog");
        if (byNamePreWorld1 == null)
            return "(no GlueDialog found... !?)";
        if (!byNamePreWorld1.IsVisible)
            return null;
        var childObject = GContext.Main.Interface.GetByNamePreWorld("GlueDialogBackground")
            .GetChildObject("GlueDialogText");
        var byNamePreWorld2 = GContext.Main.Interface.GetByNamePreWorld("GlueDialogButton1");
        var labelText = childObject.LabelText;
        byNamePreWorld2.ClickMouse(false);
        Thread.Sleep(3000);
        return labelText;
    }

    public bool method_1(string string_1)
    {
        try
        {
            string_0 = string_1;
            gclass14_0 = new GClass14();
            if (!StartupClass.bool_12)
                return false;
            if (!gclass14_0.method_1("Accounts\\" + string_1 + ".xml"))
                throw new Exception("Unable to read XML file (encrypted on another machine?)");
            GProcessMemoryManipulator.smethod_31();
            return method_3();
        }
        catch (ThreadInterruptedException ex)
        {
            gcontext_0.Log("Auto Login aborted");
            throw ex;
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("Exception while logging into \"" + string_1 + "\": " + ex.Message + "\r\n" +
                               ex.StackTrace);
            return false;
        }
    }

    [SpecialName]
    public bool method_2()
    {
        if (!gcontext_0.Interface.IsPreWorldVisible)
            return false;
        var byNamePreWorld1 = gcontext_0.Interface.GetByNamePreWorld("AccountLoginAccountEdit");
        var byNamePreWorld2 = gcontext_0.Interface.GetByNamePreWorld("AccountLoginPasswordEdit");
        return byNamePreWorld1 != null && byNamePreWorld2 != null && byNamePreWorld1.IsVisible &&
               byNamePreWorld2.IsVisible;
    }

    private bool method_3()
    {
        GContext.Main.Log("Auto-logging \"" + string_0 + "\", select Glider and press Escape to cancel...");
        var gspellTimer1 = new GSpellTimer(15000, false);
        while (!gcontext_0.Interface.IsPreWorldVisible && !gspellTimer1.IsReady)
            Thread.Sleep(3000);
        if (gspellTimer1.IsReady)
        {
            gcontext_0.Log("Non-logged-in UI never became visible, getting out of autolog");
            return false;
        }

        var str1 = method_0();
        if (str1 != null)
            gcontext_0.Log("Dismissed popup, contents were: \"" + str1 + "\"");
        while (true)
        {
            var byNamePreWorld = gcontext_0.Interface.GetByNamePreWorld("AccountLoginAccountEdit");
            if (byNamePreWorld == null || !byNamePreWorld.IsVisible)
                Thread.Sleep(500);
            else
                break;
        }

        gcontext_0.EnsureGameSelected();
        gcontext_0.Interface.GetByNamePreWorld("AccountLoginAccountEdit").ClickMouse(false);
        Thread.Sleep(500);
        gcontext_0.Interface.SendString(gclass14_0.string_0);
        gcontext_0.Interface.GetByNamePreWorld("AccountLoginPasswordEdit").ClickMouse(false);
        Thread.Sleep(500);
        gcontext_0.Interface.SendString(gclass14_0.string_1);
        Thread.Sleep(2500);
        gcontext_0.Interface.GetByNamePreWorld("AccountLoginLoginButton").ClickMouse(false);
        var gspellTimer2 = new GSpellTimer(10000, false);
        var byNamePreWorld1 = gcontext_0.Interface.GetByNamePreWorld("CharacterSelect");
        var byNamePreWorld2 = gcontext_0.Interface.GetByNamePreWorld("RealmWizard");
        do
        {
            ;
        } while (!gspellTimer2.IsReadySlow && !byNamePreWorld1.IsVisible && !byNamePreWorld2.IsVisible);

        if (gspellTimer2.IsReady)
        {
            string str2;
            if (GContext.Main.Interface.GetByNamePreWorld("GlueDialog").IsVisible)
            {
                var childObject = GContext.Main.Interface.GetByNamePreWorld("GlueDialogBackground")
                    .GetChildObject("GlueDialogText");
                GContext.Main.Interface.GetByNamePreWorld("GlueDialogButton1");
                str2 = childObject.LabelText;
            }
            else
            {
                str2 = "(no dialog visible)";
            }

            gcontext_0.Log("Never got to char select or realm wizard after login, dang!  Dialog text: " + str2);
            return false;
        }

        Thread.Sleep(6000);
        if (byNamePreWorld2.IsVisible)
            method_4();
        if (!method_10().ToLower().StartsWith(gclass14_0.string_2.ToLower()))
            method_5();
        GProcessMemoryManipulator.smethod_31();
        var flag = false;
        GInterfaceObject ginterfaceObject = null;
        for (var index = 1; index <= 9; ++index)
        {
            ginterfaceObject = gcontext_0.Interface.GetByNamePreWorld("CharSelectCharacterButton" + index);
            if (ginterfaceObject.IsVisible &&
                gcontext_0.Interface.GetByNamePreWorld("CharSelectCharacterButton" + index + "ButtonText")
                    .GetChildObject("CharSelectCharacterButton" + index + "ButtonTextName").LabelText.ToLower() ==
                gclass14_0.string_3.ToLower())
            {
                flag = true;
                break;
            }
        }

        if (!flag)
        {
            gcontext_0.Log("Never found \"" + gclass14_0.string_3 + "\" on realm!");
            return false;
        }

        GProcessMemoryManipulator.smethod_31();
        ginterfaceObject.ClickMouse(false);
        Thread.Sleep(500);
        gcontext_0.Interface.GetByNamePreWorld("CharSelectEnterWorldButton").ClickMouse(false);
        return true;
    }

    private void method_4()
    {
        gcontext_0.Interface.GetByNamePreWorld("RealmWizardLocationButton1").ClickMouse(false);
        Thread.Sleep(666);
        gcontext_0.Interface.GetByNamePreWorld("RealmWizardSuggest").ClickMouse(false);
        if (method_9("GlueDialog", "RealmList") == 0)
        {
            GClass37.smethod_0("Suggested realm dialog visible after suggest, pawing through it");
            var labelText = gcontext_0.Interface.GetByNamePreWorld("GlueDialogBackground")
                .GetChildObject("GlueDialogText").LabelText;
            var num1 = labelText.IndexOf("|r ");
            var num2 = labelText.IndexOf(" |cffff");
            if (num1 > -1 && num2 > -1 && num2 > num1 && labelText.Substring(num1 + 3, num2 - num1 - 3).ToLower() ==
                gclass14_0.string_2.ToLower())
            {
                gcontext_0.Interface.GetByNamePreWorld("GlueDialogButton1").ClickMouse(false);
                method_8("CharacterSelectCharacterFrame");
                Thread.Sleep(3000);
                return;
            }

            gcontext_0.Interface.GetByNamePreWorld("GlueDialogButton2").ClickMouse(false);
        }
        else
        {
            GClass37.smethod_0("Realm list visible suggest, clicking for a new realm");
        }

        method_6();
    }

    private void method_5()
    {
        gcontext_0.Debug("This realm is no good, switching to \"" + gclass14_0.string_2 + "\"");
        gcontext_0.Interface.GetByNamePreWorld("CharSelectChangeRealmButton").ClickMouse(false);
        method_6();
    }

    private void method_6()
    {
        method_8("RealmList");
        Thread.Sleep(700);
        var num = 1;
        while (true)
        {
            var byNamePreWorld = gcontext_0.Interface.GetByNamePreWorld("RealmListTab" + num);
            if (byNamePreWorld != null && byNamePreWorld.IsVisible)
            {
                gcontext_0.Log("Trying realm tab: \"" +
                               byNamePreWorld.GetChildObject("RealmListTab" + num + "Text").LabelText + "\"");
                byNamePreWorld.ClickMouse(false);
                Thread.Sleep(1000);
                if (!method_7())
                    ++num;
                else
                    break;
            }
            else
            {
                goto label_4;
            }
        }

        return;
        label_4:
        throw new Exception("Never able to find realm: \"" + gclass14_0.string_2 + "\" in all tabs, giving up!");
    }

    private bool method_7()
    {
        var byNamePreWorld1 = gcontext_0.Interface.GetByNamePreWorld("RealmListScrollFrameScrollBarScrollUpButton");
        var byNamePreWorld2 = gcontext_0.Interface.GetByNamePreWorld("RealmListScrollFrameScrollBarScrollDownButton");
        while (byNamePreWorld1.IsVisible && byNamePreWorld1.IsEnabledInFrame)
        {
            byNamePreWorld1.ClickMouse(false);
            Thread.Sleep(388);
        }

        GInterfaceObject byNamePreWorld3;
        while (true)
        {
            var num = 1;
            while (true)
            {
                byNamePreWorld3 = gcontext_0.Interface.GetByNamePreWorld("RealmListRealmButton" + num);
                if (byNamePreWorld3 != null && byNamePreWorld3.IsVisible)
                {
                    if (!(byNamePreWorld3.GetChildObject("RealmListRealmButton" + num + "NormalText").LabelText.Trim()
                            .ToLower() == gclass14_0.string_2.ToLower()))
                        ++num;
                    else
                        goto label_9;
                }
                else
                {
                    break;
                }
            }

            if (byNamePreWorld2.IsEnabledInFrame)
            {
                byNamePreWorld2.ClickMouse(false);
                Thread.Sleep(388);
            }
            else
            {
                goto label_10;
            }
        }

        label_9:
        byNamePreWorld3.ClickMouse(false);
        Thread.Sleep(500);
        gcontext_0.Interface.GetByNamePreWorld("RealmListOkButton").ClickMouse(false);
        method_8("CharacterSelect");
        Thread.Sleep(3000);
        return true;
        label_10:
        gcontext_0.Log("Never able to find realm \"" + gclass14_0.string_2 + "\" in list, will try next tab");
        return false;
    }

    private void method_8(string string_1)
    {
        var gspellTimer = new GSpellTimer(10000, false);
        var byNamePreWorld = gcontext_0.Interface.GetByNamePreWorld(string_1);
        if (byNamePreWorld == null)
            throw new Exception("No such interface object to wait for: \"" + string_1 + "\"");
        while (!gspellTimer.IsReadySlow)
            if (byNamePreWorld.IsVisible)
                return;
        throw new Exception("Timed out waiting for interface object to appear: \"" + string_1 + "\"");
    }

    private int method_9(string string_1, string string_2)
    {
        var gspellTimer = new GSpellTimer(10000, false);
        var byNamePreWorld1 = gcontext_0.Interface.GetByNamePreWorld(string_1);
        var byNamePreWorld2 = gcontext_0.Interface.GetByNamePreWorld(string_2);
        if (byNamePreWorld1 != null && byNamePreWorld2 != null)
        {
            while (!gspellTimer.IsReadySlow)
            {
                if (byNamePreWorld1.IsVisible)
                    return 0;
                if (byNamePreWorld2.IsVisible)
                    return 1;
            }

            throw new Exception("Timed out waiting for interface objects to appear: \"" + string_1 + "\" or \"" +
                                string_2 + "\"");
        }

        throw new Exception("No such interface object to wait for: \"" + string_1 + "\" or \"" + string_2 + "\"");
    }

    private string method_10()
    {
        method_8("CharacterSelectCharacterFrame");
        var byNamePreWorld = gcontext_0.Interface.GetByNamePreWorld("CharacterSelectCharacterFrame");
        if (byNamePreWorld == null || !byNamePreWorld.IsVisible)
            return "(no realm selected)";
        var str = byNamePreWorld.GetChildObject("CharSelectRealmName").LabelText.Trim();
        GClass37.smethod_0("Current realm: \"" + str + "\"");
        return str;
    }
}