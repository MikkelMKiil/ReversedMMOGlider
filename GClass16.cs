// Decompiled with JetBrains decompiler
// Type: GClass16
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

public class GClass16
{
    private const string string_0 =
        "<RSAKeyValue><Modulus>oR97bOVGOLZngLaX0hquQQXn76zCgVZCD4UhxNJJ1iZ1vpsdY4orqNni+dugxzFm5naMWb2ecqXt99lTD8CJfMePvrhhIo0qR8HiSSxKmkUIhuRBUv84LgB4rTE36xtIV76jkV7qbYsr8qmYh5iD7R/cswBFQwCqbnBalDK3L70=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

    private const uint uint_0 = 4294967295;
    private readonly bool bool_0;
    private bool bool_1;
    private byte[] byte_0;
    private DateTime dateTime_0;
    private GClass19 gclass19_0;
    private readonly GClass71 gclass71_0;
    private int int_0;
    private int int_1;
    private int int_2;
    private IntPtr intptr_0;
    private readonly SortedList<string, int> sortedList_0;
    private string string_1;
    private Thread thread_0;

    public GClass16(GClass71 gclass71_1, bool bool_2, int int_3)
    {
        gclass71_0 = gclass71_1;
        bool_0 = bool_2;
        int_1 = int_3;
        intptr_0 = IntPtr.Zero;
        thread_0 = null;
        bool_1 = false;
        sortedList_0 = new SortedList<string, int>();
        method_1();
        GClass37.smethod_1("TW: Setting up VAP");
        gclass71_1.method_24(int_3);
        if (GClass18.gclass18_0.method_5("FLPeek"))
            gclass71_1.method_25(GClass18.gclass18_0.method_4("FLPeek"));
        int_0 = 0;
        method_5();
        GClass37.smethod_0("New TW instance created");
    }

    public void method_0()
    {
        if (bool_1)
            return;
        GClass37.smethod_0("Cleaning up TW");
        bool_1 = true;
        gclass71_0.method_31();
        if (intptr_0 != IntPtr.Zero)
            SetEvent(intptr_0);
        if (thread_0 != null && Thread.CurrentThread != thread_0)
            thread_0.Join();
        if (!(intptr_0 != IntPtr.Zero))
            return;
        CloseHandle(intptr_0);
        intptr_0 = IntPtr.Zero;
    }

    private void method_1()
    {
        intptr_0 = CreateEvent(IntPtr.Zero, true, false, null);
        if (intptr_0 == IntPtr.Zero)
            throw new Exception("CreateEvent failed, last error = " + Marshal.GetLastWin32Error());
        gclass71_0.method_29(intptr_0);
        thread_0 = new Thread(method_2);
        thread_0.Start();
    }

    private void method_2()
    {
        try
        {
            method_3();
        }
        catch (Exception ex)
        {
            if (!bool_1)
            {
                GClass37.smethod_0("* Exception in event thread: " + ex.Message + "\r\n" + ex.StackTrace);
                thread_0 = null;
                CloseHandle(intptr_0);
            }
            else
            {
                GClass37.smethod_0("Exception in shutting down, no big deal: " + ex.Message);
            }
        }

        GClass37.smethod_1("TW event guy is done");
    }

    private void method_3()
    {
        while (WaitForSingleObject(intptr_0, uint.MaxValue) == 0U)
        {
            if (bool_1)
                return;
            ResetEvent(intptr_0);
            var genum0_0 = method_8();
            if (genum0_0 != GEnum0.const_0)
            {
                GClass37.smethod_1("ThisPoll is bad");
                StartupClass.smethod_37(genum0_0);
            }
        }

        throw new Exception("Unexpected error in WaitForSingleObject: " + Marshal.GetLastWin32Error());
    }

    public void method_4()
    {
        if (int_0 < 1 && StartupClass.bool_35)
        {
            var num = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("Warden1"), "twsanity");
            if (num != 0)
            {
                GClass37.smethod_1("WF: " + int_0 + ", check: 0x" + num.ToString("x"));
                StartupClass.smethod_37(GEnum0.const_2);
            }
            else
            {
                GClass37.smethod_0("Tripwire: nothing to analyze at attach");
            }
        }
        else
        {
            GClass37.smethod_0("TW initial check on attach is ok (" + int_0 + ")");
        }
    }

    public void method_5()
    {
        dateTime_0 = DateTime.Now;
    }

    public void method_6(int int_3, int int_4)
    {
        var byte_1 = GProcessMemoryManipulator.smethod_17(int_3, int_4, "bsp");
        if (byte_1 == null)
            return;
        method_7(byte_1);
    }

    public void method_7(byte[] byte_1)
    {
        try
        {
            smethod_0(method_18(byte_1));
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("** Exception processing bsb: " + ex.Message + "\r\n" + ex.StackTrace);
        }

        GProcessMemoryManipulator.smethod_54();
    }

    private GEnum0 method_8()
    {
        var flag1 = false;
        try
        {
            GClass71.GEnum10 genum10_0;
            var byte_1 = gclass71_0.method_23(out genum10_0);
            if (byte_1 == null)
                return GEnum0.const_0;
            if (genum10_0 != GClass71.GEnum10.const_2 && genum10_0 != GClass71.GEnum10.const_3)
            {
                var key = method_13(byte_1);
                if (sortedList_0.ContainsKey(key))
                {
                    gclass71_0.method_30();
                    return GEnum0.const_0;
                }

                ++int_0;
                var totalSeconds = (DateTime.Now - dateTime_0).TotalSeconds;
                string_1 = "";
                if (bool_0)
                    method_18(byte_1);
                var gclass56_0 = method_9(3, "", byte_1);
                gclass56_0.SendAndReceiveData();
                if (StartupClass.IsDecryptedStreamEmpty(gclass56_0))
                {
                    gclass71_0.method_30();
                    flag1 = true;
                    Thread.Sleep(2000);
                    return GEnum0.const_3;
                }

                var str = gclass56_0.ReadStringFromDecryptedStream();
                var string_2_1 = gclass56_0.ReadStringFromDecryptedStream();
                if (string_2_1.Length > 1)
                    smethod_0(string_2_1);
                GEnum0 int_3;
                bool flag2;
                switch (str)
                {
                    case "Safe":
                        gclass71_0.method_30();
                        flag1 = true;
                        sortedList_0.Add(key, 1);
                        int_3 = GEnum0.const_0;
                        break;
                    case "Unsafe":
                        int_3 = GEnum0.const_1;
                        gclass71_0.method_30();
                        flag2 = true;
                        break;
                    case "ReqFP":
                        int_3 = method_20(byte_1);
                        var gclass56 = method_9((int)int_3, string_1, byte_1);
                        gclass56.SendAndReceiveData();
                        gclass56.ReadIntFromDecryptedStream();
                        gclass56.ReadStringFromDecryptedStream();
                        var string_2_2 = gclass56.ReadStringFromDecryptedStream();
                        if (string_2_2.Length > 1)
                            smethod_0(string_2_2);
                        gclass71_0.method_30();
                        flag2 = true;
                        break;
                    default:
                        int_3 = GEnum0.const_2;
                        gclass71_0.method_30();
                        flag2 = true;
                        break;
                }

                return int_3;
            }

            method_7(byte_1);
            gclass71_0.method_30();
            return GEnum0.const_1;
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("TW: Exception in poller: " + ex.Message + "\r\n" + ex.StackTrace);
            if (!flag1)
                gclass71_0.method_30();
            return GEnum0.const_2;
        }
    }

    private GDataEncryptionManager method_9(int int_3, string string_2, byte[] byte_1)
    {
        var gclass56 = new GDataEncryptionManager(3);
        gclass56.PrepareDataStream();
        gclass56.WriteIntToStream(int_3);
        gclass56.WriteIntToStream((int)(DateTime.Now - dateTime_0).TotalSeconds);
        gclass56.WriteIntToStream(byte_1.Length);
        gclass56.WriteBytesToStream(method_17(byte_1));
        gclass56.WriteBytesToStream(method_16(byte_1));
        gclass56.WriteStringToStream(string_2);
        return gclass56;
    }

    private void method_10()
    {
        if (Directory.Exists("TW"))
            return;
        Directory.CreateDirectory("TW");
    }

    private string method_11(byte[] byte_1)
    {
        var hash = new MD5CryptoServiceProvider().ComputeHash(byte_1);
        var stringBuilder = new StringBuilder(32);
        foreach (var num in hash)
            stringBuilder.Append(num.ToString("x2"));
        return stringBuilder.ToString();
    }

    private string method_12(byte[] byte_1)
    {
        var stringBuilder = new StringBuilder();
        foreach (var num in byte_1)
            stringBuilder.Append(num.ToString("x2"));
        return stringBuilder.ToString();
    }

    private string method_13(byte[] byte_1)
    {
        var hash = new SHA256Managed().ComputeHash(byte_1);
        var stringBuilder = new StringBuilder(64);
        foreach (var num in hash)
            stringBuilder.Append(num.ToString("x2"));
        return stringBuilder.ToString();
    }

    private string method_14(byte[] byte_1, int int_3)
    {
        var numArray = new byte[4 + byte_1.Length];
        Array.Copy(BitConverter.GetBytes(int_3), 0, numArray, 0, 4);
        Array.Copy(byte_1, 0, numArray, 4, byte_1.Length);
        return method_13(numArray);
    }

    private string method_15(byte[] byte_1, int int_3)
    {
        var numArray = new byte[4 + byte_1.Length];
        Array.Copy(BitConverter.GetBytes(int_3), 0, numArray, 0, 4);
        Array.Copy(byte_1, 0, numArray, 4, byte_1.Length);
        return method_11(numArray);
    }

    private byte[] method_16(byte[] byte_1)
    {
        return new MD5CryptoServiceProvider().ComputeHash(byte_1);
    }

    private byte[] method_17(byte[] byte_1)
    {
        return new SHA256Managed().ComputeHash(byte_1);
    }

    private string method_18(byte[] byte_1)
    {
        method_10();
        var string_2 = "TW\\" + method_14(byte_1, 1904313471) + ".data";
        try
        {
            method_19(byte_1, string_2);
            method_19(byte_1, "TW\\Last.data");
            return string_2;
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("Exception saving data: " + ex.Message + "\r\n" + ex.StackTrace);
            return null;
        }
    }

    private void method_19(byte[] byte_1, string string_2)
    {
        RSACryptoServiceProvider.UseMachineKeyStore = true;
        var cryptoServiceProvider = new RSACryptoServiceProvider();
        cryptoServiceProvider.FromXmlString(
            "<RSAKeyValue><Modulus>oR97bOVGOLZngLaX0hquQQXn76zCgVZCD4UhxNJJ1iZ1vpsdY4orqNni+dugxzFm5naMWb2ecqXt99lTD8CJfMePvrhhIo0qR8HiSSxKmkUIhuRBUv84LgB4rTE36xtIV76jkV7qbYsr8qmYh5iD7R/cswBFQwCqbnBalDK3L70=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
        var numArray1 = new byte[32];
        var numArray2 = new byte[32];
        for (var index = 0; index < numArray2.Length; ++index)
            numArray2[index] = (byte)(64 - index);
        RandomNumberGenerator.Create().GetBytes(numArray1);
        var rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.KeySize = 256;
        rijndaelManaged.BlockSize = 256;
        rijndaelManaged.Padding = PaddingMode.PKCS7;
        rijndaelManaged.Key = numArray1;
        rijndaelManaged.IV = numArray2;
        rijndaelManaged.Mode = CipherMode.CBC;
        var buffer1 = cryptoServiceProvider.Encrypt(numArray1, false);
        var fileStream = new FileStream(string_2, FileMode.Create);
        fileStream.Write(buffer1, 0, buffer1.Length);
        var buffer2 = rijndaelManaged.CreateEncryptor().TransformFinalBlock(byte_1, 0, byte_1.Length);
        fileStream.Write(buffer2, 0, buffer2.Length);
        fileStream.Flush();
        fileStream.Close();
    }

    private GEnum0 method_20(byte[] byte_1)
    {
        string_1 = "(step0)";
        var gclass46 = new GClass46(new GClass0(byte_1));
        var int_0 = gclass46.method_6();
        if (byte_0 == null || int_0 != int_2)
        {
            byte_0 = GClass1.smethod_1(int_0);
            if (byte_0 == null)
            {
                string_1 = "(getwdf)";
                GClass37.smethod_0("Unable to get TW data from Glider server!");
                return GEnum0.const_2;
            }

            int_2 = int_0;
        }

        string_1 = "(0x" + byte_0.Length.ToString("x") + ")";
        gclass19_0 = new GClass19();
        gclass19_0.method_1(byte_0);
        if (int_0 != 2 && int_0 != 4)
        {
            GClass37.smethod_1("Unexpected Warden rev from TW: " + int_0);
            var gclass16 = this;
            gclass16.string_1 = gclass16.string_1 + "(nocorer=" + int_0 + ")";
            return GEnum0.const_1;
        }

        gclass46.method_1(gclass19_0);
        string_1 += gclass46.string_1;
        switch (gclass46.genum5_0)
        {
            case GEnum5.const_0:
                string_1 += "(error)";
                GClass37.smethod_1("TW error during match, not safe, last error = " + gclass46.string_0);
                return GEnum0.const_2;
            case GEnum5.const_1:
                string_1 += "(match)";
                GClass37.smethod_1("TW matched ok");
                return GEnum0.const_0;
            case GEnum5.const_2:
                string_1 += "(unsafe)";
                GClass37.smethod_1("TW mismatched, not safe, last error = " + gclass46.string_0);
                return GEnum0.const_1;
            default:
                return GEnum0.const_0;
        }
    }

    public static void smethod_0(string string_2)
    {
        if (!File.Exists(string_2))
            return;
        var byte_1 = File.ReadAllBytes(string_2);
        var gclass56 = new GDataEncryptionManager(4);
        gclass56.PrepareDataStream();
        gclass56.WriteStringToStream(string_2);
        gclass56.WriteBytesToStream(byte_1);
        gclass56.SendAndReceiveData();
    }

    private RijndaelManaged method_21()
    {
        var numArray = new byte[32];
        for (var index = 0; index < numArray.Length; ++index)
            numArray[index] = (byte)index;
        var rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.KeySize = 256;
        rijndaelManaged.BlockSize = 256;
        rijndaelManaged.Padding = PaddingMode.PKCS7;
        var data = new byte[32];
        RandomNumberGenerator.Create().GetBytes(data);
        rijndaelManaged.Key = data;
        rijndaelManaged.IV = numArray;
        rijndaelManaged.Mode = CipherMode.CBC;
        return rijndaelManaged;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr CreateEvent(
        IntPtr intptr_1,
        bool bool_2,
        bool bool_3,
        string string_2);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern uint WaitForSingleObject(IntPtr intptr_1, uint uint_1);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern void CloseHandle(IntPtr intptr_1);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool ResetEvent(IntPtr intptr_1);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetEvent(IntPtr intptr_1);
}