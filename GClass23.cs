// Decompiled with JetBrains decompiler
// Type: GClass23
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Runtime.InteropServices;

public class GClass23
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

    [DllImport("iphlpapi.dll", CharSet = CharSet.Ansi)]
    private static extern int GetAdaptersInfo(IntPtr intptr_0, ref long long_0);

    public static byte[] smethod_0()
    {
        var destinationArray = new byte[6];
        long long_0 = Marshal.SizeOf(typeof(GStruct11));
        var num = Marshal.AllocHGlobal(new IntPtr(long_0));
        var adaptersInfo = GetAdaptersInfo(num, ref long_0);
        if (adaptersInfo == 111)
        {
            num = Marshal.ReAllocHGlobal(num, new IntPtr(long_0));
            adaptersInfo = GetAdaptersInfo(num, ref long_0);
        }

        if (adaptersInfo == 0)
        {
            var ptr = num;
            GStruct11 structure;
            do
            {
                structure = (GStruct11)Marshal.PtrToStructure(ptr, typeof(GStruct11));
                switch (structure.uint_1)
                {
                    default:
                        if (structure.gstruct10_0.gstruct9_0.string_0 == null ||
                            structure.gstruct10_1.gstruct9_0.string_0.Length <= 0)
                        {
                            ptr = structure.intptr_0;
                            continue;
                        }

                        goto label_7;
                }
            } while (ptr != IntPtr.Zero);

            goto label_9;
            label_7:
            Array.Copy(structure.byte_0, destinationArray, destinationArray.Length);
            label_9:
            Marshal.FreeHGlobal(num);
            return destinationArray;
        }

        Marshal.FreeHGlobal(num);
        throw new InvalidOperationException("GetAdaptersInfo failed: " + adaptersInfo);
    }
}