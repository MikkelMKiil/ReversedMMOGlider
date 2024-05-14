// Decompiled with JetBrains decompiler
// Type: GClass11
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Runtime.InteropServices;

public class GClass11
{
    private const uint uint_0 = 2035711;
    private const int int_0 = 6;
    private const int int_1 = 4;
    private const int int_2 = 32;
    private const int int_3 = 8;
    public string string_0;
    public string string_1;

    public GClass11()
    {
        method_0();
    }

    private void method_0()
    {
        string_0 = null;
        string_1 = null;
    }

    [DllImport("kernel32")]
    private static extern bool CloseHandle(IntPtr intptr_0);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint uint_1, bool bool_0, int int_4);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern uint GetSecurityInfo(
        IntPtr intptr_0,
        int int_4,
        int int_5,
        IntPtr intptr_1,
        IntPtr intptr_2,
        IntPtr intptr_3,
        IntPtr intptr_4,
        out IntPtr intptr_5);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool ConvertSecurityDescriptorToStringSecurityDescriptor(
        IntPtr intptr_0,
        int int_4,
        int int_5,
        out string string_2,
        out int int_6);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool ConvertStringSecurityDescriptorToSecurityDescriptor(
        string string_2,
        int int_4,
        out IntPtr intptr_0,
        out int int_5);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool GetSecurityDescriptorDacl(
        IntPtr intptr_0,
        out int int_4,
        out IntPtr intptr_1,
        out int int_5);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern uint SetSecurityInfo(
        IntPtr intptr_0,
        int int_4,
        int int_5,
        IntPtr intptr_1,
        IntPtr intptr_2,
        IntPtr intptr_3,
        IntPtr intptr_4);

    [DllImport("advapi32", CharSet = CharSet.Auto)]
    public static extern bool LookupPrivilegeValue(
        string string_2,
        string string_3,
        out GStruct2 gstruct2_0);

    [DllImport("advapi32", CharSet = CharSet.Auto)]
    public static extern bool AdjustTokenPrivileges(
        IntPtr intptr_0,
        bool bool_0,
        ref GStruct3 gstruct3_0,
        int int_4,
        IntPtr intptr_1,
        IntPtr intptr_2);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool OpenProcessToken(IntPtr intptr_0, uint uint_1, out IntPtr intptr_1);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool GetTokenInformation(
        IntPtr intptr_0,
        Enum1 enum1_0,
        byte[] byte_0,
        uint uint_1,
        out uint uint_2);

    public bool method_1()
    {
        method_0();
        var intptr_0_1 = OpenProcess(2035711U, false, GProcessMemoryManipulator.GetCurrentProcessId());
        IntPtr intptr_5;
        var securityInfo = GetSecurityInfo(intptr_0_1, 6, 4, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero,
            out intptr_5);
        if (securityInfo != 0U)
        {
            string_0 = GClass30.smethod_2(714, securityInfo);
            return false;
        }

        string string_2;
        if (!ConvertSecurityDescriptorToStringSecurityDescriptor(intptr_5, 1, 4, out string_2, out var _))
        {
            string_0 = GClass30.smethod_2(362, Marshal.GetLastWin32Error());
            return false;
        }

        IntPtr intptr_0_2;
        if (!ConvertStringSecurityDescriptorToSecurityDescriptor("D:" + "(D;;RPWD;;;WD)" + string_2.Substring(2), 1,
                out intptr_0_2, out var _))
        {
            string_0 = GClass30.smethod_2(363, Marshal.GetLastWin32Error());
            return false;
        }

        IntPtr intptr_1;
        if (!GetSecurityDescriptorDacl(intptr_0_2, out var _, out intptr_1, out var _))
        {
            string_0 = GClass30.smethod_2(364, Marshal.GetLastWin32Error());
            return false;
        }

        var num = SetSecurityInfo(intptr_0_1, 6, 4, IntPtr.Zero, IntPtr.Zero, intptr_1, IntPtr.Zero);
        if (num == 0U)
            return true;
        string_0 = GClass30.smethod_2(715, num);
        return false;
    }

    public bool method_2(int int_4)
    {
        method_0();
        if (LookupPrivilegeValue("", "SeDebugPrivilege", out var _))
            return true;
        string_0 = GClass30.smethod_2(328, Marshal.GetLastWin32Error());
        return false;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct GStruct2
    {
        public uint uint_0;
        public uint uint_1;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct GStruct3
    {
        public uint uint_0;
        public GStruct2 gstruct2_0;
        public uint uint_1;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct GStruct4
    {
        public GStruct2 gstruct2_0;
        public int int_0;
    }

    private enum Enum1
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
}