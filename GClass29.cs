// Decompiled with JetBrains decompiler
// Type: GClass29
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

public class GClass29
{
    private static readonly Random random_0 = new Random();
    private readonly int[] int_0;
    public string string_0;

    public GClass29(string string_1)
    {
        string_0 = string_1;
        var strArray = string_1.Split(' ');
        int_0 = new int[strArray.Length];
        for (var index = 0; index < strArray.Length; ++index)
        {
            var s = strArray[index];
            int_0[index] = !(s == "..") ? int.Parse(s, NumberStyles.HexNumber) : -1;
        }
    }

    public GClass29(MemoryStream memoryStream_0)
    {
        var length = GClass19.smethod_0(memoryStream_0) - 25874;
        Console.WriteLine("Constructing WildByte from obf stream, length = " + length);
        int_0 = new int[length];
        for (var index = 0; index < length; ++index)
        {
            var num1 = GClass19.smethod_0(memoryStream_0);
            Console.WriteLine("o: " + num1.ToString("x8") + ", shifted: 0x" + (num1 >> 24).ToString("x"));
            var num2 = num1 >> 24 != 1 ? num1 & byte.MaxValue : -1;
            int_0[index] = num2;
        }
    }

    [SpecialName]
    public int method_0()
    {
        return int_0.Length;
    }

    public bool method_1(byte[] byte_0)
    {
        if (byte_0.Length != int_0.Length)
            return false;
        for (var index = 0; index < byte_0.Length; ++index)
            if (int_0[index] != -1 && int_0[index] != byte_0[index])
                return false;
        return true;
    }

    public static int smethod_0(int int_1)
    {
        return int_1 == -1
            ? 16777216 | ((random_0.Next() % byte.MaxValue) << 16) | ((random_0.Next() % byte.MaxValue) << 8) | int_1
            : ((random_0.Next() % 124 + 2) << 24) | ((random_0.Next() % byte.MaxValue) << 16) |
              ((random_0.Next() % byte.MaxValue) << 8) | int_1;
    }

    public byte[] method_2()
    {
        var memoryStream = new MemoryStream();
        memoryStream.Write(BitConverter.GetBytes(method_0() + 25874), 0, 4);
        foreach (var int_1 in int_0)
            memoryStream.Write(BitConverter.GetBytes(smethod_0(int_1)), 0, 4);
        return memoryStream.ToArray();
    }

    public string method_3()
    {
        var stringBuilder = new StringBuilder();
        foreach (var num in int_0)
        {
            if (num == -1)
                stringBuilder.Append("..");
            else
                stringBuilder.Append(num.ToString("x2"));
            stringBuilder.Append(" ");
        }

        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        return stringBuilder.ToString();
    }
}