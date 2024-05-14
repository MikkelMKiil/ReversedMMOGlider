// Decompiled with JetBrains decompiler
// Type: GDataEncryptionManager
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

public class GDataEncryptionManager
{
    private const string RSAKeyValue =
        "<RSAKeyValue><Modulus>oR97bOVGOLZngLaX0hquQQXn76zCgVZCD4UhxNJJ1iZ1vpsdY4orqNni+dugxzFm5naMWb2ecqXt99lTD8CJfMePvrhhIo0qR8HiSSxKmkUIhuRBUv84LgB4rTE36xtIV76jkV7qbYsr8qmYh5iD7R/cswBFQwCqbnBalDK3L70=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

    private const string MainUrl = "http://www.mmoglider.com/EM.aspx";
    private const string BackupUrl = "http://vforums.mmoglider.com/GliderApp/EM.aspx";
    private const int DefaultIntValue = 1;
    private bool isProcessed;
    private byte[] encryptedData;
    private int dataLength;
    private int encryptedDataLength;
    private readonly MemoryStream inputDataStream;
    private MemoryStream decryptedDataStream;
    private RijndaelManaged rijndaelEncryption;

    public GDataEncryptionManager(int int_3)
    {
        inputDataStream = new MemoryStream();
        InitializeRijndaelEncryption();
        encryptedData = null;
        decryptedDataStream = null;
        dataLength = int_3;
        isProcessed = false;
        WriteIntToStream(int_3);
    }

    public void PrepareEncryptionData()
    {
        var array = inputDataStream.ToArray();
        var numArray = new byte[40];
        Array.Copy(BitConverter.GetBytes(1), 0, numArray, 0, 4);
        Array.Copy(BitConverter.GetBytes(array.Length), 0, numArray, 4, 4);
        Array.Copy(rijndaelEncryption.Key, 0, numArray, 8, 32);
        RSACryptoServiceProvider.UseMachineKeyStore = true;
        var cryptoServiceProvider = new RSACryptoServiceProvider();
        cryptoServiceProvider.FromXmlString(
            "<RSAKeyValue><Modulus>oR97bOVGOLZngLaX0hquQQXn76zCgVZCD4UhxNJJ1iZ1vpsdY4orqNni+dugxzFm5naMWb2ecqXt99lTD8CJfMePvrhhIo0qR8HiSSxKmkUIhuRBUv84LgB4rTE36xtIV76jkV7qbYsr8qmYh5iD7R/cswBFQwCqbnBalDK3L70=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
        var sourceArray1 = cryptoServiceProvider.Encrypt(numArray, false);
        encryptedDataLength = sourceArray1.Length;
        var sourceArray2 = rijndaelEncryption.CreateEncryptor().TransformFinalBlock(array, 0, array.Length);
        encryptedData = new byte[encryptedDataLength + sourceArray2.Length + 4];
        Array.Copy(BitConverter.GetBytes(encryptedDataLength), 0, encryptedData, 0, 4);
        Array.Copy(sourceArray1, 0, encryptedData, 4, encryptedDataLength);
        Array.Copy(sourceArray2, 0, encryptedData, encryptedDataLength + 4, sourceArray2.Length);
    }

    private void InitializeRijndaelEncryption()
    {
        var data = new byte[32];
        var numArray = new byte[32];
        for (var index = 0; index < numArray.Length; ++index)
            numArray[index] = (byte)(64 - index);
        RandomNumberGenerator.Create().GetBytes(data);
        rijndaelEncryption = new RijndaelManaged();
        rijndaelEncryption.KeySize = 256;
        rijndaelEncryption.BlockSize = 256;
        rijndaelEncryption.Padding = PaddingMode.PKCS7;
        rijndaelEncryption.Key = data;
        rijndaelEncryption.IV = numArray;
        rijndaelEncryption.Mode = CipherMode.CBC;
    }

    public void WriteIntToStream(int int_3)
    {
        inputDataStream.Write(BitConverter.GetBytes(int_3), 0, 4);
    }

    public void WriteStringToStream(string string_3)
    {
        var bytes = Encoding.ASCII.GetBytes(string_3);
        WriteIntToStream(bytes.Length);
        inputDataStream.Write(bytes, 0, bytes.Length);
    }

    public void WriteBytesToStream(byte[] byte_1)
    {
        WriteIntToStream(byte_1.Length);
        inputDataStream.Write(byte_1, 0, byte_1.Length);
    }

    public void PrepareDataStream()
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
            Logger.smethod_1("Could not parse game version, must be alphanumeric.  Using placeholder.");
            int_3_2 = 0;
        }

        WriteIntToStream(StartupClass.int_4);
        WriteStringToStream(GClass61.gclass61_0.method_2("AppKey"));
        if (GProcessMemoryManipulator.bool_3)
            WriteIntToStream(int_3_1);
        else
            WriteIntToStream(int_3_1 + 100000);
        WriteIntToStream(int_3_2);
    }

    public void MarkAsProcessed()
    {
        isProcessed = true;
    }

    public void SendAndReceiveData()
    {
        if (encryptedData == null)
            PrepareEncryptionData();
        var string_3 = GClass61.gclass61_0.method_2("AuthPage") != null
            ? GClass61.gclass61_0.method_2("AuthPage")
            : "http://www.mmoglider.com/EM.aspx";
        bool flag;
        if (!(flag = SendDataAndReceiveResponse(string_3)) && isProcessed)
            flag = SendDataAndReceiveResponse("http://vforums.mmoglider.com/GliderApp/EM.aspx");
        if (!flag)
            throw new Exception("Server message unable to get through, is network broken?");
    }

    public bool SendDataAndReceiveResponse(string string_3)
    {
        var inputBuffer = SendDataWithInternalStack(string_3);
        if (inputBuffer == null)
        {
            Logger.smethod_1("Internal stack didn't work, switching to IE");
            inputBuffer = SendDataWithIE(string_3);
        }

        if (inputBuffer == null)
            return false;
        try
        {
            decryptedDataStream = new MemoryStream(rijndaelEncryption.CreateDecryptor()
                .TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            return true;
        }
        catch (Exception ex)
        {
            Logger.smethod_1("Exception decrypting response from server, no good");
            return false;
        }
    }

    private byte[] SendDataWithInternalStack(string string_3)
    {
        try
        {
            return new GClass70(string_3, encryptedData).method_1();
        }
        catch (Exception ex)
        {
            Logger.smethod_1("Exception getting message from server with internal stack:\r\n" + ex.Message + "\r\n" +
                               ex.StackTrace);
            return null;
        }
    }

    private byte[] SendDataWithIE(string string_3)
    {
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(string_3);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept =
                "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/pdf, text/xml, application/octet-stream";
            httpWebRequest.UserAgent = "Glider/1.0";
            httpWebRequest.ContentLength = encryptedData.Length;
            httpWebRequest.GetRequestStream().Write(encryptedData, 0, encryptedData.Length);
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
            } while (offset != encryptedData.Length);

            response.Close();
            if (buffer.Length != offset)
                throw new Exception("!! Expected " + buffer.Length + " bytes from response, only got " + offset);
            return buffer;
        }
        catch (Exception ex)
        {
            Logger.LogMessage("Exception posting EM, uh-oh!");
            return null;
        }
    }

    public int ReadIntFromDecryptedStream()
    {
        var buffer = new byte[4];
        decryptedDataStream.Read(buffer, 0, 4);
        return BitConverter.ToInt32(buffer, 0);
    }

    public string ReadStringFromDecryptedStream()
    {
        var count = ReadIntFromDecryptedStream();
        var numArray = new byte[count];
        decryptedDataStream.Read(numArray, 0, count);
        return Encoding.ASCII.GetString(numArray);
    }

    public byte[] ReadBytesFromDecryptedStream()
    {
        var count = ReadIntFromDecryptedStream();
        var buffer = new byte[count];
        decryptedDataStream.Read(buffer, 0, count);
        return buffer;
    }
}