// Decompiled with JetBrains decompiler
// Type: GClass52
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Glider.Common;

public class ApplicationInitializer
{
    private const string RSAKeyValue =
        "<RSAKeyValue><Modulus>vBh2qHt0lpcAhOBwoHnCyeAXc5wHWXSHHbbA3/hAoWla7jAG1NCh1DWsh255doo/Op/qTBBy9KPA2Tm1VJYX5zd8rtBn2ulkL51xUZ4uak30y6aTD/eZN2d3jsoEFVM45yU5q7y3S1D1mVDcdPxgdIQ6Pfwq/sNRX+yTeDXrjLU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

    public static bool IsInitialized;
    public static DateTime InitializationTime;
    public static int InitializationCount;

    public static bool InitializeAndValidate(string inputString, bool isValidationRequired)
    {
        try
        {
            InitializationTime = DateTime.Now;
            InitializationCount = 0;
            var dataEncryptionManager = new GDataEncryptionManager(7);
            dataEncryptionManager.PrepareDataStream();
            dataEncryptionManager.WriteStringToStream("Release");
            dataEncryptionManager.WriteIntToStream(0);
            dataEncryptionManager.WriteIntToStream(!isValidationRequired ? 1 : 0);
            dataEncryptionManager.WriteStringToStream(StartupClass.WowVersionLabel_string);
            if (isValidationRequired)
                StartupClass.IsSomeConditionMet = false;
            dataEncryptionManager.SendAndReceiveData();
            var responseStatus = dataEncryptionManager.ReadStringFromDecryptedStream();
            var responseMessage = dataEncryptionManager.ReadStringFromDecryptedStream();
            var fileName = dataEncryptionManager.ReadStringFromDecryptedStream();
            if (responseStatus != "Good" && isValidationRequired)
            {
                if (fileName.Length == 0)
                {
                    var messageBoxResult = (int)MessageBox.Show(null, MessageProvider.smethod_2(690, responseStatus, responseMessage), MessageProvider.GetMessage(657),
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                if (MessageBox.Show(null, MessageProvider.smethod_2(791, responseStatus, responseMessage, fileName), MessageProvider.GetMessage(657),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
                    Process.Start(fileName);
                return false;
            }

            if (!isValidationRequired)
                return true;
            var streamIntValue = dataEncryptionManager.ReadIntFromDecryptedStream();
            if (StartupClass.ApplicationStartupMode == AppMode.Invisible || StartupClass.ApplicationStartupMode == AppMode.Normal)
                StartupClass.int_4 = streamIntValue;
            var firstDecryptedString = dataEncryptionManager.ReadStringFromDecryptedStream();
            var secondDecryptedString = dataEncryptionManager.ReadStringFromDecryptedStream();
            if (firstDecryptedString.Length > 0)
            {
                var formattedString = firstDecryptedString.Replace("|", "\r\n");
                Logger.LogMessage(MessageProvider.GetMessage(288));
                if (secondDecryptedString == null)
                    GliderWarning.smethod_1(formattedString);
                else
                    GliderWarning.smethod_0(MessageProvider.smethod_2(691, formattedString), secondDecryptedString);
            }

            var thirdDecryptedString = dataEncryptionManager.ReadStringFromDecryptedStream();
            if (thirdDecryptedString.Length > 0)
            {
                var cleanedRealmList = GetRealmFromConfigFile().ToLower().Replace("\"", "").Replace("'", "");
                var cleanedThirdDecryptedString = thirdDecryptedString.Replace("\"", "").Replace("'", "");
                if (!cleanedRealmList.ToLower().StartsWith(cleanedThirdDecryptedString))
                {
                    var messageBoxResult = (int)MessageBox.Show(null, MessageProvider.smethod_2(788, cleanedThirdDecryptedString), MessageProvider.GetMessage(657),
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
            }

            var applicationStatus = dataEncryptionManager.ReadStringFromDecryptedStream();
            StartupClass.InitializationCount = dataEncryptionManager.ReadIntFromDecryptedStream();
            InitializationCount = dataEncryptionManager.ReadIntFromDecryptedStream();
            var dateString = dataEncryptionManager.ReadStringFromDecryptedStream();
            if (dateString.Length > 0)
            {
                InitializationTime = DateTime.Parse(dateString);
                Logger.smethod_1("SubExpire parsed: " + InitializationTime + ", Now: " + DateTime.Now);
            }
            else
            {
                InitializationTime = DateTime.Now;
            }

            var secondsToAdd = dataEncryptionManager.ReadIntFromDecryptedStream();
            GClass18.gclass18_0.method_0();
            var streamIntValue3 = dataEncryptionManager.ReadIntFromDecryptedStream();
            for (var index = 0; index < streamIntValue3; ++index)
            {
                var fifthDecryptedString = dataEncryptionManager.ReadStringFromDecryptedStream();
                var sixthDecryptedString = dataEncryptionManager.ReadStringFromDecryptedStream();
                try
                {
                    if (fifthDecryptedString == "SF")
                    {
                        GClass16.smethod_0(sixthDecryptedString);
                    }
                    else if (fifthDecryptedString.StartsWith("Buff_"))
                    {
                        GClass18.gclass18_0.method_1(fifthDecryptedString, sixthDecryptedString);
                    }
                    else if (fifthDecryptedString.StartsWith("_"))
                    {
                        ProcessInitializationParameters(fifthDecryptedString, sixthDecryptedString);
                    }
                    else
                    {
                        var parsedIntValue = int.Parse(sixthDecryptedString, NumberStyles.HexNumber);
                        GClass18.gclass18_0.method_2(fifthDecryptedString, parsedIntValue);
                    }
                }
                catch (Exception ex)
                {
                    if (fifthDecryptedString == "GH")
                        GClass18.gclass18_0.sortedList_0.Add("GH", sixthDecryptedString);
                }
            }

            StartupClass.isInitializationSuccessful = true;
            StartupClass.isInputStringFourCharacters = inputString.Length == 4;
            Logger.LogMessage(MessageProvider.GetMessage(289));
            if (secondsToAdd > 0)
            {
                StartupClass.isTimeAdded = true;
                StartupClass.expiryTime = DateTime.Now.AddSeconds(secondsToAdd);
            }
            else
            {
                StartupClass.isTimeAdded = false;
            }

            switch (applicationStatus)
            {
                case "Beta":
                    StartupClass.IsBetaVersion = true;
                    StartupClass.IsBetaAccessGranted = true;
                    Logger.LogMessage(MessageProvider.GetMessage(290));
                    break;
                case "Old":
                    Logger.LogMessage(MessageProvider.GetMessage(872));
                    break;
            }

            if (inputString.ToUpper() == "DEMO")
                StartupClass.isInputStringFourCharacters = true;
            if (DateTime.Now < InitializationTime)
                if (isValidationRequired)
                    StartupClass.IsSomeConditionMet = true;
        }
        catch (Exception ex)
        {
            var num = (int)MessageBox.Show(null, MessageProvider.smethod_2(696, ex.Message + ex.StackTrace),
                MessageProvider.GetMessage(657), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        Logger.smethod_1("Returning from Logon");
        return true;
    }

    private static void ProcessInitializationParameters(string string_1, string string_2)
    {
        switch (string_1)
        {
            case "_SED":
                InitializationTime = DateTime.Parse(string_2);
                StartupClass.IsSomeConditionMet = true;
                IsInitialized = true;
                break;
            case "_STI":
                InitializationCount = int.Parse(string_2);
                break;
        }
    }

    public static int ReadOffsetFromXml(XmlDocument xmlDocument_0, string string_1)
    {
        return int.Parse(
            (xmlDocument_0.SelectSingleNode("/Offsets/" + string_1) ??
             throw new Exception(MessageProvider.smethod_2(293, string_1))).InnerText.Trim(), NumberStyles.HexNumber);
    }

    public static byte[] ParseOffsetValuesFromXml(XmlDocument xmlDocument_0, string string_1)
    {
        var strArray = xmlDocument_0.SelectSingleNode("/Offsets/" + string_1).InnerText.Trim().Split(' ');
        var numArray = new byte[strArray.Length];
        for (var index = 0; index < strArray.Length; ++index)
            numArray[index] = byte.Parse(strArray[index], NumberStyles.HexNumber);
        return numArray;
    }

    public static string GetRealmFromConfigFile()
    {
        var path = StartupClass.SomeStringData + "\\realmlist.wtf";
        try
        {
            var streamReader = new StreamReader(path);
            var realmInfo = streamReader.ReadToEnd().Trim().Split(' ')[2];
            streamReader.Close();
            return realmInfo;
        }
        catch (Exception ex)
        {
            Logger.LogMessage("* Exception in getrealmlist: " + ex.Message);
            return null;
        }
    }
}