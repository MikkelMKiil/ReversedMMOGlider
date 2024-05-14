// Decompiled with JetBrains decompiler
// Type: GClass0
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

public class GClass0
{
    private readonly byte[] byte_0;
    private byte[] byte_1;
    private GStruct0 gstruct0_0;
    private GStruct1[] gstruct1_0;

    public GClass0(string string_0)
    {
        byte_0 = File.ReadAllBytes(string_0);
        method_0();
    }

    public GClass0(byte[] byte_2)
    {
        byte_0 = byte_2;
        method_0();
    }

    public static void smethod_0(string string_0)
    {
    }

    public static void smethod_1(string string_0)
    {
        GClass37.smethod_1(string_0);
    }

    private void method_0()
    {
        var gcHandle = GCHandle.Alloc(byte_0, GCHandleType.Pinned);
        smethod_0("WFImage preparing, image length = 0x" + byte_0.Length.ToString("x") + " bytes");
        gstruct0_0 = (GStruct0)Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof(GStruct0));
        gstruct1_0 = new GStruct1[(uint)(IntPtr)gstruct0_0.uint_9];
        var num = Marshal.SizeOf(gstruct0_0);
        byte_1 = new byte[(long)(IntPtr)(gstruct0_0.uint_0 + 4096U)];
        smethod_0("Header.dwImageSize = 0x" + gstruct0_0.uint_0.ToString("x") + ", import count = " +
                  gstruct0_0.uint_8 + ", section count = " + gstruct0_0.uint_9);
        var index = 0;
        while (index < gstruct1_0.Length)
        {
            gstruct1_0[index] =
                (GStruct1)Marshal.PtrToStructure(new IntPtr(gcHandle.AddrOfPinnedObject().ToInt32() + num),
                    typeof(GStruct1));
            smethod_0("Section " + index + ": VA=0x" + gstruct1_0[index].uint_0.ToString("x") + ", Size=0x" +
                      gstruct1_0[index].uint_1.ToString("x") + ", Flags=0x" + gstruct1_0[index].uint_2.ToString("x"));
            ++index;
            num += Marshal.SizeOf(gstruct1_0[0]);
        }

        var destinationIndex = 4096;
        var flag = true;
        while (num < byte_0.Length)
        {
            var int16 = BitConverter.ToInt16(byte_0, num);
            num += 2;
            if (flag)
            {
                Array.Copy(byte_0, num, byte_1, destinationIndex, int16);
                num += int16;
                flag = false;
            }
            else
            {
                flag = true;
            }

            destinationIndex += int16;
        }

        gcHandle.Free();
    }

    private void method_1(int int_0, int int_1)
    {
        if (int_0 < 4096)
            throw new ArgumentOutOfRangeException("VA too low: 0x" + int_0.ToString("x"));
        if (int_0 > byte_1.Length)
            throw new ArgumentOutOfRangeException("VA too high: 0x" + int_0.ToString("x"));
        if (int_0 + int_1 > byte_1.Length)
            throw new ArgumentOutOfRangeException("VA + length off top: 0x" + int_0.ToString("x") + ", length = 0x" +
                                                  int_1.ToString("x"));
    }

    public uint method_2(int int_0)
    {
        method_1(int_0, 4);
        return BitConverter.ToUInt32(byte_1, int_0);
    }

    public byte method_3(int int_0)
    {
        method_1(int_0, 1);
        return byte_1[int_0];
    }

    public int method_4(int int_0)
    {
        method_1(int_0, 4);
        return BitConverter.ToInt32(byte_1, int_0);
    }

    public short method_5(int int_0)
    {
        method_1(int_0, 2);
        return BitConverter.ToInt16(byte_1, int_0);
    }

    public byte[] method_6(int int_0, int int_1)
    {
        method_1(int_0, int_1);
        var destinationArray = new byte[int_1];
        Array.Copy(byte_1, int_0, destinationArray, 0, int_1);
        return destinationArray;
    }

    public void method_7(int int_0, int int_1)
    {
        smethod_0("Bytes @ 0x" + int_0.ToString("x") + ": " + smethod_2(method_6(int_0, int_1)));
    }

    public static string smethod_2(byte[] byte_2)
    {
        var stringBuilder = new StringBuilder();
        foreach (var num in byte_2)
            stringBuilder.Append(num.ToString("x2") + " ");
        if (stringBuilder.Length > 0)
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
        return stringBuilder.ToString();
    }

    public int method_8(byte[] byte_2)
    {
        var numArray = method_10(byte_2);
        return numArray.Length == 1
            ? numArray[0]
            : throw new Exception("SeekCodeBytesSingle failed on " + smethod_2(byte_2) + ", got " + numArray.Length +
                                  " hits");
    }

    public bool method_9(int int_0)
    {
        return int_0 >= (int)gstruct1_0[0].uint_0 && int_0 < gstruct1_0[1].uint_0;
    }

    public int[] method_10(byte[] byte_2)
    {
        return method_12(byte_2, (int)gstruct1_0[0].uint_0, (int)gstruct1_0[1].uint_0);
    }

    public int[] method_11(byte[] byte_2)
    {
        return method_12(byte_2, (int)gstruct1_0[1].uint_0, (int)gstruct1_0[2].uint_0);
    }

    public int[] method_12(byte[] byte_2, int int_0, int int_1)
    {
        var intList = new List<int>();
        var num = -1;
        var index1 = 0;
        for (var index2 = int_0; index2 < int_1 && index2 < byte_1.Length; ++index2)
            if (byte_1[index2] == byte_2[index1])
            {
                if (index1 == 0)
                    num = index2;
                ++index1;
                if (index1 == byte_2.Length)
                {
                    intList.Add(num);
                    index2 = num;
                    index1 = 0;
                }
            }
            else if (index1 != 0)
            {
                index1 = 0;
                index2 = num;
            }

        return intList.ToArray();
    }

    public static byte[] smethod_3(string string_0)
    {
        var numArray = new byte[string_0.Length / 2];
        for (var index = 0; index < numArray.Length; ++index)
            numArray[index] = (byte)int.Parse(string_0.Substring(index * 2, 2), NumberStyles.HexNumber);
        return numArray;
    }

    public struct GStruct0
    {
        public uint uint_0;
        public uint uint_1;
        public uint uint_2;
        public uint uint_3;
        public uint uint_4;
        public uint uint_5;
        public uint uint_6;
        public uint uint_7;
        public uint uint_8;
        public uint uint_9;
    }

    public struct GStruct1
    {
        public uint uint_0;
        public uint uint_1;
        public uint uint_2;
    }
}