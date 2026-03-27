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
                Logger.LogMessage("Unable to load static offsets from GameMemoryConstants.");
                return true;
            }

            StartupClass.isInitializationSuccessful = true;
            StartupClass.isInputStringFourCharacters = false;
            StartupClass.IsBetaAccessGranted = true;
            StartupClass.IsBetaVersion = false;
            StartupClass.isTimeAdded = false;
            if (isValidationRequired)
                StartupClass.IsSomeConditionMet = true;
            Logger.LogMessage("Loaded " + int_0 + " static offsets from GameMemoryConstants.");
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
        int_0 = Glider.Common.Objects.GameMemoryConstants.LoadStaticOffsets(MemoryOffsetTable.Instance);
        Logger.LogMessage("Static offsets source: GameMemoryConstants");
        if (int_0 <= 0 || !Glider.Common.Objects.GameMemoryConstants.ValidateRequiredOffsets(MemoryOffsetTable.Instance))
            return false;

        if (!MemoryOffsetTable.Instance.HasOffset("CameraBase"))
        {
            Logger.LogMessage("Camera base offset missing; projection and camera spin are disabled.");
            return false;
        }

        Logger.LogMessage("Camera base validated at 0x" + MemoryOffsetTable.Instance.GetIntOffset("CameraBase").ToString("x") +
                          " with WotLK camera block layout 0x8(position), 0x14(view matrix), 0x40(FOV)");
        return true;
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
