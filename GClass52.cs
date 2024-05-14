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

public class GClass52
{
    private const string string_0 =
        "<RSAKeyValue><Modulus>vBh2qHt0lpcAhOBwoHnCyeAXc5wHWXSHHbbA3/hAoWla7jAG1NCh1DWsh255doo/Op/qTBBy9KPA2Tm1VJYX5zd8rtBn2ulkL51xUZ4uak30y6aTD/eZN2d3jsoEFVM45yU5q7y3S1D1mVDcdPxgdIQ6Pfwq/sNRX+yTeDXrjLU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

    public static bool bool_0;
    public static DateTime dateTime_0;
    public static int int_0;

    public static bool smethod_0(string string_1, bool bool_1)
    {
        try
        {
            dateTime_0 = DateTime.Now;
            int_0 = 0;
            var gclass56 = new GDataEncryptionManager(7);
            gclass56.PrepareDataStream();
            gclass56.WriteStringToStream("Release");
            gclass56.WriteIntToStream(0);
            gclass56.WriteIntToStream(!bool_1 ? 1 : 0);
            gclass56.WriteStringToStream(StartupClass.WowVersionLabel_string);
            if (bool_1)
                StartupClass.bool_12 = false;
            gclass56.SendAndReceiveData();
            var str1 = gclass56.ReadStringFromDecryptedStream();
            var str2 = gclass56.ReadStringFromDecryptedStream();
            var fileName = gclass56.ReadStringFromDecryptedStream();
            if (str1 != "Good" && bool_1)
            {
                if (fileName.Length == 0)
                {
                    var num = (int)MessageBox.Show(null, GClass30.smethod_2(690, str1, str2), GClass30.smethod_1(657),
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                if (MessageBox.Show(null, GClass30.smethod_2(791, str1, str2, fileName), GClass30.smethod_1(657),
                        MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
                    Process.Start(fileName);
                return false;
            }

            if (!bool_1)
                return true;
            var num1 = gclass56.ReadIntFromDecryptedStream();
            if (StartupClass.StartupMode == AppMode.Invisible || StartupClass.StartupMode == AppMode.Normal)
                StartupClass.int_4 = num1;
            var str3 = gclass56.ReadStringFromDecryptedStream();
            var string_1_1 = gclass56.ReadStringFromDecryptedStream();
            if (str3.Length > 0)
            {
                var string_0 = str3.Replace("|", "\r\n");
                GClass37.smethod_0(GClass30.smethod_1(288));
                if (string_1_1 == null)
                    GliderWarning.smethod_1(string_0);
                else
                    GliderWarning.smethod_0(GClass30.smethod_2(691, string_0), string_1_1);
            }

            var str4 = gclass56.ReadStringFromDecryptedStream();
            if (str4.Length > 0)
            {
                var str5 = smethod_4().ToLower().Replace("\"", "").Replace("'", "");
                var str6 = str4.Replace("\"", "").Replace("'", "");
                if (!str5.ToLower().StartsWith(str6))
                {
                    var num2 = (int)MessageBox.Show(null, GClass30.smethod_2(788, str6), GClass30.smethod_1(657),
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
            }

            var str7 = gclass56.ReadStringFromDecryptedStream();
            StartupClass.int_1 = gclass56.ReadIntFromDecryptedStream();
            int_0 = gclass56.ReadIntFromDecryptedStream();
            var s = gclass56.ReadStringFromDecryptedStream();
            if (s.Length > 0)
            {
                dateTime_0 = DateTime.Parse(s);
                GClass37.smethod_1("SubExpire parsed: " + dateTime_0 + ", Now: " + DateTime.Now);
            }
            else
            {
                dateTime_0 = DateTime.Now;
            }

            var num3 = gclass56.ReadIntFromDecryptedStream();
            GClass18.gclass18_0.method_0();
            var num4 = gclass56.ReadIntFromDecryptedStream();
            for (var index = 0; index < num4; ++index)
            {
                var str8 = gclass56.ReadStringFromDecryptedStream();
                var str9 = gclass56.ReadStringFromDecryptedStream();
                try
                {
                    if (str8 == "SF")
                    {
                        GClass16.smethod_0(str9);
                    }
                    else if (str8.StartsWith("Buff_"))
                    {
                        GClass18.gclass18_0.method_1(str8, str9);
                    }
                    else if (str8.StartsWith("_"))
                    {
                        smethod_1(str8, str9);
                    }
                    else
                    {
                        var int_0 = int.Parse(str9, NumberStyles.HexNumber);
                        GClass18.gclass18_0.method_2(str8, int_0);
                    }
                }
                catch (Exception ex)
                {
                    if (str8 == "GH")
                        GClass18.gclass18_0.sortedList_0.Add("GH", str9);
                }
            }

            StartupClass.bool_22 = true;
            StartupClass.bool_24 = string_1.Length == 4;
            GClass37.smethod_0(GClass30.smethod_1(289));
            if (num3 > 0)
            {
                StartupClass.bool_25 = true;
                StartupClass.dateTime_1 = DateTime.Now.AddSeconds(num3);
            }
            else
            {
                StartupClass.bool_25 = false;
            }

            switch (str7)
            {
                case "Beta":
                    StartupClass.bool_17 = true;
                    StartupClass.bool_18 = true;
                    GClass37.smethod_0(GClass30.smethod_1(290));
                    break;
                case "Old":
                    GClass37.smethod_0(GClass30.smethod_1(872));
                    break;
            }

            if (string_1.ToUpper() == "DEMO")
                StartupClass.bool_24 = true;
            if (DateTime.Now < dateTime_0)
                if (bool_1)
                    StartupClass.bool_12 = true;
        }
        catch (Exception ex)
        {
            var num = (int)MessageBox.Show(null, GClass30.smethod_2(696, ex.Message + ex.StackTrace),
                GClass30.smethod_1(657), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        GClass37.smethod_1("Returning from Logon");
        return true;
    }

    private static void smethod_1(string string_1, string string_2)
    {
        switch (string_1)
        {
            case "_SED":
                dateTime_0 = DateTime.Parse(string_2);
                StartupClass.bool_12 = true;
                bool_0 = true;
                break;
            case "_STI":
                int_0 = int.Parse(string_2);
                break;
        }
    }

    public static int smethod_2(XmlDocument xmlDocument_0, string string_1)
    {
        return int.Parse(
            (xmlDocument_0.SelectSingleNode("/Offsets/" + string_1) ??
             throw new Exception(GClass30.smethod_2(293, string_1))).InnerText.Trim(), NumberStyles.HexNumber);
    }

    public static byte[] smethod_3(XmlDocument xmlDocument_0, string string_1)
    {
        var strArray = xmlDocument_0.SelectSingleNode("/Offsets/" + string_1).InnerText.Trim().Split(' ');
        var numArray = new byte[strArray.Length];
        for (var index = 0; index < strArray.Length; ++index)
            numArray[index] = byte.Parse(strArray[index], NumberStyles.HexNumber);
        return numArray;
    }

    public static string smethod_4()
    {
        var path = StartupClass.string_4 + "\\realmlist.wtf";
        try
        {
            var streamReader = new StreamReader(path);
            var str = streamReader.ReadToEnd().Trim().Split(' ')[2];
            streamReader.Close();
            return str;
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("* Exception in getrealmlist: " + ex.Message);
            return null;
        }
    }
}