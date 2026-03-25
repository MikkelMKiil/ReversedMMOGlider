// Decompiled with JetBrains decompiler
// Type: GClass52
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;

public class ApplicationInitializer
{
    private const string RSAKeyValue =
        "<RSAKeyValue><Modulus>vBh2qHt0lpcAhOBwoHnCyeAXc5wHWXSHHbbA3/hAoWla7jAG1NCh1DWsh255doo/Op/qTBBy9KPA2Tm1VJYX5zd8rtBn2ulkL51xUZ4uak30y6aTD/eZN2d3jsoEFVM45yU5q7y3S1D1mVDcdPxgdIQ6Pfwq/sNRX+yTeDXrjLU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
    private const string StaticOffsetFileName = "Offsets.static.xml";

    public static bool IsInitialized;
    public static DateTime InitializationTime;
    public static int InitializationCount;

    public static bool InitializeAndValidate(string inputString, bool isValidationRequired)
    {
        try
        {
            InitializationTime = DateTime.Now;
            InitializationCount = 0;
            int int_0;
            if (!TryLoadStaticOffsets(out int_0))
            {
                Logger.LogMessage("Unable to load static offsets from \"" + StaticOffsetFileName + "\".");
                return true;
            }

            StartupClass.isInitializationSuccessful = true;
            StartupClass.isInputStringFourCharacters = false;
            StartupClass.IsBetaAccessGranted = true;
            StartupClass.IsBetaVersion = false;
            StartupClass.isTimeAdded = false;
            if (isValidationRequired)
                StartupClass.IsLicenseValid = true;
            Logger.LogMessage("Loaded " + int_0 + " static offsets from \"" + StaticOffsetFileName + "\".");
        }
        catch (Exception ex)
        {
            var num = (int)MessageBox.Show(null, MessageProvider.smethod_2(696, ex.Message + ex.StackTrace),
                MessageProvider.GetMessage(657), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        Logger.smethod_1("Returning from Logon");
        return true;
    }

    private static bool TryLoadStaticOffsets(out int int_0)
    {
        int_0 = 0;
        string string_1;
        if (!TryResolveStaticOffsetPath(out string_1))
            return false;
        try
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(string_1);
            MemoryOffsetTable.Instance.Clear();
            foreach (XmlNode selectNode in xmlDocument.SelectNodes("/Offsets/*"))
            {
                string string_2;
                string string_3;
                if (selectNode.Name == "Entry")
                {
                    if (selectNode.Attributes == null)
                        continue;
                    var xmlAttribute1 = selectNode.Attributes["name"];
                    var xmlAttribute2 = selectNode.Attributes["value"];
                    if (xmlAttribute1 == null || xmlAttribute2 == null)
                        continue;
                    string_2 = xmlAttribute1.Value;
                    string_3 = xmlAttribute2.Value;
                }
                else
                {
                    string_2 = selectNode.Name;
                    string_3 = selectNode.InnerText;
                }

                if (ApplyStaticOffset(string_2, string_3))
                    ++int_0;
            }

            Logger.LogMessage("Static offsets source: " + string_1);
            return int_0 > 0 && ValidateRequiredOffsets();
        }
        catch (Exception ex)
        {
            Logger.smethod_1("Unable to load static offsets: " + ex.Message);
            return false;
        }
    }

    private static bool TryResolveStaticOffsetPath(out string string_1)
    {
        string_1 = null;
        var stringList = new List<string>();
        stringList.Add(StaticOffsetFileName);
        stringList.Add(Path.Combine(Environment.CurrentDirectory, StaticOffsetFileName));
        stringList.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, StaticOffsetFileName));
        try
        {
            var fullPath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
            var directoryName = Path.GetDirectoryName(fullPath.TrimEnd('\\'));
            for (var index = 0; index < 4 && !string.IsNullOrEmpty(directoryName); ++index)
            {
                stringList.Add(Path.Combine(directoryName, StaticOffsetFileName));
                directoryName = Path.GetDirectoryName(directoryName);
            }
        }
        catch (Exception)
        {
        }

        foreach (var path in stringList)
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                string_1 = path;
                return true;
            }

        return false;
    }

    private static bool ApplyStaticOffset(string string_1, string string_2)
    {
        if (string_1 == null || string_1.Length == 0 || string_2 == null)
            return false;
        string_2 = string_2.Trim();
        if (string_2.Length == 0)
            return false;
        try
        {
            if (string_1 == "SF")
            {
                WardenMonitor.smethod_0(string_2);
                return true;
            }

            if (string_1.StartsWith("Buff_"))
            {
                MemoryOffsetTable.Instance.AddStringOffset(string_1, string_2);
                return true;
            }

            if (string_1.StartsWith("_"))
            {
                ProcessInitializationParameters(string_1, string_2);
                return true;
            }

            if (string_1 == "GH")
            {
                MemoryOffsetTable.Instance.Offsets.Add("GH", string_2);
                return true;
            }

            var num = ParseStaticOffsetValue(string_2);
            MemoryOffsetTable.Instance.AddIntOffset(string_1, num);
            return true;
        }
        catch (Exception ex)
        {
            Logger.smethod_1("Skipping invalid static offset \"" + string_1 + "\": " + ex.Message);
            return false;
        }
    }

    private static int ParseStaticOffsetValue(string string_1)
    {
        if (string_1.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            string_1 = string_1.Substring(2);
        int result;
        if (int.TryParse(string_1, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result))
            return result;
        if (int.TryParse(string_1, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            return result;
        throw new Exception("Offset value is not numeric: " + string_1);
    }

    private static bool ValidateRequiredOffsets()
    {
        foreach (var key in new string[5]
                 {
                     "MainTable", "InitialOffset", "PlayerIdAddr", "D_Player", "D_Object"
                 })
        {
            if (!MemoryOffsetTable.Instance.HasOffset(key))
            {
                Logger.LogMessage("Static offsets file is missing required key: " + key);
                return false;
            }
        }

        return true;
    }

    private static void ProcessInitializationParameters(string string_1, string string_2)
    {
        switch (string_1)
        {
            case "_SED":
                InitializationTime = DateTime.Parse(string_2);
                StartupClass.IsLicenseValid = true;
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
        var path = StartupClass.wowInstallPath + "\\realmlist.wtf";
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