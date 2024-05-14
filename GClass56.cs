// Decompiled with JetBrains decompiler
// Type: GClass56
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

public class GClass56
{
    private const string string_0 =
        "<RSAKeyValue><Modulus>oR97bOVGOLZngLaX0hquQQXn76zCgVZCD4UhxNJJ1iZ1vpsdY4orqNni+dugxzFm5naMWb2ecqXt99lTD8CJfMePvrhhIo0qR8HiSSxKmkUIhuRBUv84LgB4rTE36xtIV76jkV7qbYsr8qmYh5iD7R/cswBFQwCqbnBalDK3L70=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

    private const string string_1 = "http://www.mmoglider.com/EM.aspx";
    private const string string_2 = "http://vforums.mmoglider.com/GliderApp/EM.aspx";
    private const int int_0 = 1;
    private bool bool_0;
    private byte[] byte_0;
    private int int_1;
    private int int_2;
    private readonly MemoryStream memoryStream_0;
    private MemoryStream memoryStream_1;
    private RijndaelManaged rijndaelManaged_0;

    public GClass56(int int_3)
    {
        memoryStream_0 = new MemoryStream();
        method_1();
        byte_0 = null;
        memoryStream_1 = null;
        int_1 = int_3;
        bool_0 = false;
        method_2(int_3);
    }

    public void method_0()
    {
        var array = memoryStream_0.ToArray();
        var numArray = new byte[40];
        Array.Copy(BitConverter.GetBytes(1), 0, numArray, 0, 4);
        Array.Copy(BitConverter.GetBytes(array.Length), 0, numArray, 4, 4);
        Array.Copy(rijndaelManaged_0.Key, 0, numArray, 8, 32);
        RSACryptoServiceProvider.UseMachineKeyStore = true;
        var cryptoServiceProvider = new RSACryptoServiceProvider();
        cryptoServiceProvider.FromXmlString(
            "<RSAKeyValue><Modulus>oR97bOVGOLZngLaX0hquQQXn76zCgVZCD4UhxNJJ1iZ1vpsdY4orqNni+dugxzFm5naMWb2ecqXt99lTD8CJfMePvrhhIo0qR8HiSSxKmkUIhuRBUv84LgB4rTE36xtIV76jkV7qbYsr8qmYh5iD7R/cswBFQwCqbnBalDK3L70=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
        var sourceArray1 = cryptoServiceProvider.Encrypt(numArray, false);
        int_2 = sourceArray1.Length;
        var sourceArray2 = rijndaelManaged_0.CreateEncryptor().TransformFinalBlock(array, 0, array.Length);
        byte_0 = new byte[int_2 + sourceArray2.Length + 4];
        Array.Copy(BitConverter.GetBytes(int_2), 0, byte_0, 0, 4);
        Array.Copy(sourceArray1, 0, byte_0, 4, int_2);
        Array.Copy(sourceArray2, 0, byte_0, int_2 + 4, sourceArray2.Length);
    }

    private void method_1()
    {
        var data = new byte[32];
        var numArray = new byte[32];
        for (var index = 0; index < numArray.Length; ++index)
            numArray[index] = (byte)(64 - index);
        RandomNumberGenerator.Create().GetBytes(data);
        rijndaelManaged_0 = new RijndaelManaged();
        rijndaelManaged_0.KeySize = 256;
        rijndaelManaged_0.BlockSize = 256;
        rijndaelManaged_0.Padding = PaddingMode.PKCS7;
        rijndaelManaged_0.Key = data;
        rijndaelManaged_0.IV = numArray;
        rijndaelManaged_0.Mode = CipherMode.CBC;
    }

    public void method_2(int int_3)
    {
        memoryStream_0.Write(BitConverter.GetBytes(int_3), 0, 4);
    }

    public void method_3(string string_3)
    {
        var bytes = Encoding.ASCII.GetBytes(string_3);
        method_2(bytes.Length);
        memoryStream_0.Write(bytes, 0, bytes.Length);
    }

    public void method_4(byte[] byte_1)
    {
        method_2(byte_1.Length);
        memoryStream_0.Write(byte_1, 0, byte_1.Length);
    }

    public void method_5()
    {
        var int_3_1 = int.Parse("1.8.0".Replace(".", ""));
        var int_3_2 = 0;
        try
        {
            if (StartupClass.WowVersionLabel_string.Length > 3)
                int_3_2 = int.Parse(
                    StartupClass.WowVersionLabel_string.Substring(StartupClass.WowVersionLabel_string.Length - 4));
        }
        catch (Exception ex)
        {
            GClass37.smethod_1("Could not parse game version, must be alphanumeric.  Using placeholder.");
            int_3_2 = 0;
        }

        method_2(StartupClass.int_4);
        method_3(GClass61.gclass61_0.method_2("AppKey"));
        if (GProcessMemoryManipulator.bool_3)
            method_2(int_3_1);
        else
            method_2(int_3_1 + 100000);
        method_2(int_3_2);
    }

    public void method_6()
    {
        bool_0 = true;
    }

    public void method_7()
    {
        if (byte_0 == null)
            method_0();
        var string_3 = GClass61.gclass61_0.method_2("AuthPage") != null
            ? GClass61.gclass61_0.method_2("AuthPage")
            : "http://www.mmoglider.com/EM.aspx";
        bool flag;
        if (!(flag = method_8(string_3)) && bool_0)
            flag = method_8("http://vforums.mmoglider.com/GliderApp/EM.aspx");
        if (!flag)
            throw new Exception("Server message unable to get through, is network broken?");
    }

    public bool method_8(string string_3)
    {
        var inputBuffer = method_9(string_3);
        if (inputBuffer == null)
        {
            GClass37.smethod_1("Internal stack didn't work, switching to IE");
            inputBuffer = method_10(string_3);
        }

        if (inputBuffer == null)
            return false;
        try
        {
            memoryStream_1 = new MemoryStream(rijndaelManaged_0.CreateDecryptor()
                .TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            return true;
        }
        catch (Exception ex)
        {
            GClass37.smethod_1("Exception decrypting response from server, no good");
            return false;
        }
    }

    private byte[] method_9(string string_3)
    {
        try
        {
            return new GClass70(string_3, byte_0).method_1();
        }
        catch (Exception ex)
        {
            GClass37.smethod_1("Exception getting message from server with internal stack:\r\n" + ex.Message + "\r\n" +
                               ex.StackTrace);
            return null;
        }
    }

    private byte[] method_10(string string_3)
    {
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(string_3);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept =
                "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/pdf, text/xml, application/octet-stream";
            httpWebRequest.UserAgent = "Glider/1.0";
            httpWebRequest.ContentLength = byte_0.Length;
            httpWebRequest.GetRequestStream().Write(byte_0, 0, byte_0.Length);
            var response = httpWebRequest.GetResponse();
            var buffer = response.ContentLength != 0L
                ? new byte[response.ContentLength]
                : throw new Exception("Zero-length response from server, proxy issues?");
            var offset = 0;
            do
            {
                var num = response.GetResponseStream().Read(buffer, offset, buffer.Length - offset);
                if (num != 0)
                    offset += num;
                else
                    break;
            } while (offset != byte_0.Length);

            response.Close();
            if (buffer.Length != offset)
                throw new Exception("!! Expected " + buffer.Length + " bytes from response, only got " + offset);
            return buffer;
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("Exception posting EM, uh-oh!");
            return null;
        }
    }

    public int method_11()
    {
        var buffer = new byte[4];
        memoryStream_1.Read(buffer, 0, 4);
        return BitConverter.ToInt32(buffer, 0);
    }

    public string method_12()
    {
        var count = method_11();
        var numArray = new byte[count];
        memoryStream_1.Read(numArray, 0, count);
        return Encoding.ASCII.GetString(numArray);
    }

    public byte[] method_13()
    {
        var count = method_11();
        var buffer = new byte[count];
        memoryStream_1.Read(buffer, 0, count);
        return buffer;
    }
}