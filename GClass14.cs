// Decompiled with JetBrains decompiler
// Type: GClass14
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

public class GClass14
{
    public string string_0;
    public string string_1;
    public string string_2;
    public string string_3;

    public GClass14()
    {
        string_0 = "";
        string_1 = "";
        string_2 = "";
        string_3 = "";
    }

    public void method_0(string string_4, bool bool_0)
    {
        var xmlDocument_0 = new XmlDocument();
        xmlDocument_0.AppendChild(xmlDocument_0.CreateXmlDeclaration("1.0", null, null));
        xmlDocument_0.AppendChild(xmlDocument_0.CreateElement("Account"));
        smethod_2(xmlDocument_0, "AccountName", string_0);
        smethod_2(xmlDocument_0, "AccountPW", string_1);
        smethod_2(xmlDocument_0, "RealmName", string_2);
        smethod_2(xmlDocument_0, "CharName", string_3);
        if (!bool_0)
        {
            xmlDocument_0.Save(string_4);
        }
        else
        {
            var outStream = new MemoryStream();
            xmlDocument_0.Save(outStream);
            var buffer = outStream.GetBuffer();
            var contents = "<?xml version=\"1.0\"?>\r\n<AccountEnc>" +
                           smethod_3(smethod_1().CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length)) +
                           "</AccountEnc>";
            File.WriteAllText(string_4, contents);
        }
    }

    public bool method_1(string string_4)
    {
        var xmlDocument = new XmlDocument();
        xmlDocument.Load(string_4);
        if (xmlDocument.SelectSingleNode("/AccountEnc") != null)
        {
            var inputBuffer = smethod_4(xmlDocument.SelectSingleNode("/AccountEnc").InnerText);
            var decryptor = smethod_1().CreateDecryptor();
            byte[] buffer;
            try
            {
                buffer = decryptor.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            }
            catch (Exception ex)
            {
                return false;
            }

            var inStream = new MemoryStream(buffer);
            xmlDocument.Load(inStream);
        }

        string_0 = xmlDocument.SelectSingleNode("/Account/AccountName").InnerText;
        string_1 = xmlDocument.SelectSingleNode("/Account/AccountPW").InnerText;
        string_2 = xmlDocument.SelectSingleNode("/Account/RealmName").InnerText;
        string_3 = xmlDocument.SelectSingleNode("/Account/CharName").InnerText;
        return true;
    }

    private static byte[] smethod_0()
    {
        var destinationArray = new byte[32];
        for (var index = 0; index < destinationArray.Length; ++index)
            destinationArray[index] = (byte)(index * 4);
        var sourceArray = GClass23.smethod_0();
        Array.Copy(sourceArray, destinationArray, sourceArray.Length);
        return destinationArray;
    }

    private static RijndaelManaged smethod_1()
    {
        var numArray = new byte[32];
        for (var index = 0; index < numArray.Length; ++index)
            numArray[index] = (byte)index;
        var rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.KeySize = 256;
        rijndaelManaged.BlockSize = 256;
        rijndaelManaged.Padding = PaddingMode.PKCS7;
        rijndaelManaged.Key = smethod_0();
        rijndaelManaged.IV = numArray;
        rijndaelManaged.Mode = CipherMode.CBC;
        return rijndaelManaged;
    }

    private static void smethod_2(XmlDocument xmlDocument_0, string string_4, string string_5)
    {
        if (string_5 == null)
            return;
        var element = xmlDocument_0.CreateElement(string_4);
        element.InnerText = string_5;
        xmlDocument_0.DocumentElement.AppendChild(element);
    }

    private static string smethod_3(byte[] byte_0)
    {
        var stringBuilder = new StringBuilder();
        foreach (var num in byte_0)
            stringBuilder.Append(num.ToString("x2"));
        return stringBuilder.ToString();
    }

    private static byte[] smethod_4(string string_4)
    {
        var numArray = new byte[string_4.Length / 2];
        for (var index = 0; index < numArray.Length; ++index)
            numArray[index] = (byte)int.Parse(string_4.Substring(index * 2, 2), NumberStyles.HexNumber);
        return numArray;
    }
}