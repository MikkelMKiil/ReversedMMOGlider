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
    public NetCheckResult NetworkStatus;
    public string WarningMessage;
    public string WarningTimestamp;
    public string WarningDetailsUrl;

    public GClass3()
    {
        try
        {
            InitializeNetworkSafetyCheck();
        }
        catch (Exception ex)
        {
            Logger.LogMessage("! Exception creating NetCheck: " + ex.Message + "\r\n" + ex.StackTrace);
            NetworkStatus = NetCheckResult.Unknown;
            WarningMessage = "Unable to complete NetCheck.";
            WarningTimestamp = DateTime.Now.ToString();
            WarningDetailsUrl = null;
        }

        Logger.smethod_1(ToString());
    }

    public override string ToString()
    {
        return "NetCheck: Result=" + NetworkStatus + ",LinkTo=" + WarningDetailsUrl + ",WarningText=" + WarningMessage +
               ",WarningDate=" + WarningTimestamp;
    }

    private void InitializeNetworkSafetyCheck()
    {
        NetworkStatus = NetCheckResult.Unknown;
        WarningMessage = null;
        WarningTimestamp = null;
        WarningDetailsUrl = null;
        var dataEncryptionManager = new GDataEncryptionManager(2);
        dataEncryptionManager.MarkAsProcessed();
        dataEncryptionManager.PrepareDataStream();
        dataEncryptionManager.SendAndReceiveData();
        if (StartupClass.IsDecryptedStreamEmpty(dataEncryptionManager))
        {
            StartupClass.smethod_37(GEnum0.const_3);
            NetworkStatus = NetCheckResult.Safe;
        }
        else
        {
            var status = dataEncryptionManager.ReadStringFromDecryptedStream();
            WarningMessage = dataEncryptionManager.ReadStringFromDecryptedStream();
            WarningTimestamp = dataEncryptionManager.ReadStringFromDecryptedStream();
            WarningDetailsUrl = dataEncryptionManager.ReadStringFromDecryptedStream();
            switch (status)
            {
                case "Warning":
                    NetworkStatus = NetCheckResult.Warning;
                    break;
                case "Stop":
                    NetworkStatus = NetCheckResult.Stop;
                    break;
                case "Safe":
                    NetworkStatus = NetCheckResult.Safe;
                    break;
                default:
                    throw new Exception("Unknown status in NetCheck: \"" + status + "\"");
            }
        }
    }
    
    public bool ValidateNetworkSafety(bool showDialogs)
    {
        if (NetworkStatus == NetCheckResult.Safe)
            return true;
        if (NetworkStatus == NetCheckResult.Unknown)
            return !showDialogs || MessageBox.Show(null,
                "NetCheck was unable to complete, using Glider may not be safe.  Please check your logs and Glider support forums for more information.\r\n\r\nDo you want to continue with your current action?",
                GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;
        if (NetworkStatus == NetCheckResult.Stop)
        {
            StartupClass.IsExitRequested = true;
            if (showDialogs)
                TerminateWowProcesses();
            else
                StopGlider();
            var warningText = "Stop: " + WarningTimestamp + "\r\n\r\n" + WarningMessage.Replace("|", "\r\n");
            if (WarningDetailsUrl.Length > 0)
            {
                if (MessageBox.Show(null, warningText + "\r\n\r\nOpen link in new browser window?", GProcessMemoryManipulator.smethod_0(),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
                    Process.Start(WarningDetailsUrl);
                StopGlider();
                return false;
            }

            var messageBoxResult = (int)MessageBox.Show(null, warningText, GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            StopGlider();
            return false;
        }

        if (NetworkStatus == NetCheckResult.Warning)
        {
            if (showDialogs)
            {
                var text = "Warning: " + WarningTimestamp + "\r\n\r\n" + WarningMessage.Replace("|", "\r\n");
                if (WarningDetailsUrl.Length > 0)
                {
                    if (MessageBox.Show(null, text + "\r\n\r\nOpen link in new browser window?", GProcessMemoryManipulator.smethod_0(),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        Process.Start(WarningDetailsUrl);
                    return false;
                }

                var num = (int)MessageBox.Show(null, text, GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }

            Logger.LogMessage("Warning from NetCheck: " + WarningMessage);
        }

        return true;
    }

    public void TerminateWowProcesses()
    {
        var gclass65 = new GClass65();
        gclass65.method_0();
        foreach (var gclass66 in gclass65.gclass66_0)
            if (gclass66.string_0.ToLower() == "wow.exe")
                gclass66.method_0();
    }

    public void StopGlider()
    {
        if (StartupClass.GliderManager != null && !StartupClass.bool_33)
            StartupClass.GliderManager.method_11();
        StartupClass.ginterface0_0.imethod_4();
    }
}