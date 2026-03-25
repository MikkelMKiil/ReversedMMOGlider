// Decompiled with JetBrains decompiler
// Type: MACAddressReader
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Net.NetworkInformation;

public class MACAddressReader
{
    public const int int_0 = 256;
    public const int int_1 = 128;
    public const int int_2 = 8;
    private const int int_3 = 1;
    private const int int_4 = 6;
    private const int int_5 = 9;
    private const int int_6 = 15;
    private const int int_7 = 23;
    private const int int_8 = 24;
    private const int int_9 = 28;
    private const int int_10 = 111;

    public static byte[] smethod_0()
    {
        var destinationArray = new byte[6];
        foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Loopback ||
                networkInterface.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
                continue;

            var addressBytes = networkInterface.GetPhysicalAddress().GetAddressBytes();
            if (addressBytes.Length < destinationArray.Length)
                continue;

            Array.Copy(addressBytes, destinationArray, destinationArray.Length);
            return destinationArray;
        }

        return destinationArray;
    }
}
