// Decompiled with JetBrains decompiler
// Type: GClass3
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Glider.Common;

public class GClass3
{
    public NetCheckResult netCheckResult_0;
    public string string_0;
    public string string_1;
    public string string_2;

    public GClass3()
    {
        try
        {
            method_0();
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("! Exception creating NetCheck: " + ex.Message + "\r\n" + ex.StackTrace);
            netCheckResult_0 = NetCheckResult.Unknown;
            string_0 = "Unable to complete NetCheck.";
            string_1 = DateTime.Now.ToString();
            string_2 = null;
        }

        GClass37.smethod_1(ToString());
    }

    public override string ToString()
    {
        return "NetCheck: Result=" + netCheckResult_0 + ",LinkTo=" + string_2 + ",WarningText=" + string_0 +
               ",WarningDate=" + string_1;
    }

    private void method_0()
    {
        netCheckResult_0 = NetCheckResult.Unknown;
        string_0 = null;
        string_1 = null;
        string_2 = null;
        var gclass56_0 = new GDataEncryptionManager(2);
        gclass56_0.MarkAsProcessed();
        gclass56_0.PrepareDataStream();
        gclass56_0.SendAndReceiveData();
        if (StartupClass.smethod_57(gclass56_0))
        {
            StartupClass.smethod_37(GEnum0.const_3);
            netCheckResult_0 = NetCheckResult.Safe;
        }
        else
        {
            var str = gclass56_0.ReadStringFromDecryptedStream();
            string_0 = gclass56_0.ReadStringFromDecryptedStream();
            string_1 = gclass56_0.ReadStringFromDecryptedStream();
            string_2 = gclass56_0.ReadStringFromDecryptedStream();
            switch (str)
            {
                case "Warning":
                    netCheckResult_0 = NetCheckResult.Warning;
                    break;
                case "Stop":
                    netCheckResult_0 = NetCheckResult.Stop;
                    break;
                case "Safe":
                    netCheckResult_0 = NetCheckResult.Safe;
                    break;
                default:
                    throw new Exception("Unknown status in NetCheck: \"" + str + "\"");
            }
        }
    }
    
    public bool method_1(bool bool_0)
    {
        if (netCheckResult_0 == NetCheckResult.Safe)
            return true;
        if (netCheckResult_0 == NetCheckResult.Unknown)
            return !bool_0 || MessageBox.Show(null,
                "NetCheck was unable to complete, using Glider may not be safe.  Please check your logs and Glider support forums for more information.\r\n\r\nDo you want to continue with your current action?",
                GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;
        if (netCheckResult_0 == NetCheckResult.Stop)
        {
            StartupClass.bool_7 = true;
            if (bool_0)
                method_2();
            else
                method_3();
            var text = "Stop: " + string_1 + "\r\n\r\n" + string_0.Replace("|", "\r\n");
            if (string_2.Length > 0)
            {
                if (MessageBox.Show(null, text + "\r\n\r\nOpen link in new browser window?", GProcessMemoryManipulator.smethod_0(),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
                    Process.Start(string_2);
                method_3();
                return false;
            }

            var num = (int)MessageBox.Show(null, text, GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            method_3();
            return false;
        }

        if (netCheckResult_0 == NetCheckResult.Warning)
        {
            if (bool_0)
            {
                var text = "Warning: " + string_1 + "\r\n\r\n" + string_0.Replace("|", "\r\n");
                if (string_2.Length > 0)
                {
                    if (MessageBox.Show(null, text + "\r\n\r\nOpen link in new browser window?", GProcessMemoryManipulator.smethod_0(),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        Process.Start(string_2);
                    return false;
                }

                var num = (int)MessageBox.Show(null, text, GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }

            GClass37.smethod_0("Warning from NetCheck: " + string_0);
        }

        return true;
    }

    public void method_2()
    {
        var gclass65 = new GClass65();
        gclass65.method_0();
        foreach (var gclass66 in gclass65.gclass66_0)
            if (gclass66.string_0.ToLower() == "wow.exe")
                gclass66.method_0();
    }

    public void method_3()
    {
        if (StartupClass.gclass71_0 != null && !StartupClass.bool_33)
            StartupClass.gclass71_0.method_11();
        StartupClass.ginterface0_0.imethod_4();
    }
}