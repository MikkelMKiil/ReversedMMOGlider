// Decompiled with JetBrains decompiler
// Type: GClass17
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Runtime.InteropServices;

public class GClass17
{
    private const int int_0 = 32;
    private const int int_1 = 8;
    private static IntPtr intptr_0 = IntPtr.Zero;
    private static bool bool_0;

    [DllImport("advapi32", CharSet = CharSet.Auto)]
    public static extern bool LookupPrivilegeValue(
        string string_0,
        string string_1,
        out GStruct5 gstruct5_0);

    [DllImport("advapi32", CharSet = CharSet.Auto)]
    public static extern bool AdjustTokenPrivileges(
        IntPtr intptr_1,
        bool bool_1,
        ref GStruct6 gstruct6_0,
        int int_2,
        IntPtr intptr_2,
        IntPtr intptr_3);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool OpenThreadToken(
        IntPtr intptr_1,
        uint uint_0,
        bool bool_1,
        out IntPtr intptr_2);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetCurrentThread();

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool OpenProcessToken(IntPtr intptr_1, uint uint_0, out IntPtr intptr_2);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetCurrentProcess();

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool GetTokenInformation(
        IntPtr intptr_1,
        Enum2 enum2_0,
        byte[] byte_0,
        uint uint_0,
        out uint uint_1);

    public static void smethod_0()
    {
        if (bool_0)
            return;
        bool_0 = true;
        GStruct5 gstruct5_0;
        if (!LookupPrivilegeValue("", "SeDebugPrivilege", out gstruct5_0))
        {
            GClass37.smethod_1(GClass30.smethod_2(328, Marshal.GetLastWin32Error()));
        }
        else
        {
            GClass37.smethod_1("SeDebugPrivilege LUID: " + gstruct5_0.uint_0.ToString("x") + "/" +
                               gstruct5_0.uint_1.ToString("x"));
            if (!OpenProcessToken(GetCurrentProcess(), 40U, out intptr_0))
            {
                GClass37.smethod_1(GClass30.smethod_2(329, Marshal.GetLastWin32Error()));
            }
            else
            {
                var byte_0 = new byte[512];
                if (!GetTokenInformation(intptr_0, Enum2.const_2, byte_0, 512U, out var _))
                {
                    GClass37.smethod_1(GClass30.smethod_2(330, Marshal.GetLastWin32Error()));
                }
                else
                {
                    var int32_1 = BitConverter.ToInt32(byte_0, 0);
                    GClass37.smethod_1(GClass30.smethod_2(331, int32_1));
                    var flag1 = true;
                    var flag2 = false;
                    for (var index = 0; index < int32_1; ++index)
                    {
                        var startIndex = 4 + 12 * index;
                        var int32_2 = BitConverter.ToInt32(byte_0, startIndex);
                        var int32_3 = BitConverter.ToInt32(byte_0, startIndex + 4);
                        var int32_4 = BitConverter.ToInt32(byte_0, startIndex + 8);
                        if (int32_2 == gstruct5_0.uint_0 && int32_3 == gstruct5_0.uint_1)
                        {
                            flag1 = true;
                            if ((int32_4 & 2) > 0)
                                flag2 = true;
                        }
                    }

                    if (!flag1)
                    {
                        GClass37.smethod_1(GClass30.smethod_1(332));
                    }
                    else if (flag2)
                    {
                        GClass37.smethod_1(GClass30.smethod_1(333));
                    }
                    else
                    {
                        var gstruct6_0 = new GStruct6();
                        gstruct6_0.uint_0 = 1U;
                        gstruct6_0.gstruct5_0 = gstruct5_0;
                        gstruct6_0.uint_1 = 2U;
                        if (!AdjustTokenPrivileges(intptr_0, false, ref gstruct6_0, Marshal.SizeOf(gstruct6_0),
                                IntPtr.Zero, IntPtr.Zero))
                            GClass37.smethod_1(GClass30.smethod_2(334, Marshal.GetLastWin32Error()));
                        else
                            GClass37.smethod_1(GClass30.smethod_1(335));
                    }
                }
            }
        }
    }

    private enum Enum2
    {
        const_0 = 1,
        const_1 = 2,
        const_2 = 3,
        const_3 = 4,
        const_4 = 5,
        const_5 = 6,
        const_6 = 7,
        const_7 = 8,
        const_8 = 9,
        const_9 = 10, // 0x0000000A
        const_10 = 11, // 0x0000000B
        const_11 = 12, // 0x0000000C
        const_12 = 13, // 0x0000000D
        const_13 = 14, // 0x0000000E
        const_14 = 15, // 0x0000000F
        const_15 = 16, // 0x00000010
        const_16 = 17 // 0x00000011
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct GStruct5
    {
        public uint uint_0;
        public uint uint_1;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct GStruct6
    {
        public uint uint_0;
        public GStruct5 gstruct5_0;
        public uint uint_1;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct GStruct7
    {
        public GStruct5 gstruct5_0;
        public int int_0;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct GStruct8
    {
        public int int_0;
        public GStruct7[] gstruct7_0;
    }
}