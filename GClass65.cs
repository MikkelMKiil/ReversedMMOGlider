// Decompiled with JetBrains decompiler
// Type: GClass65
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class GClass65
{
    private const int int_0 = 2;
    private const int int_1 = 4;
    private static bool bool_0;
    public static bool bool_1;
    public GClass66[] gclass66_0;

    public GClass65()
    {
        gclass66_0 = null;
    }

    [DllImport("kernel32")]
    private static extern bool CloseHandle(IntPtr intptr_0);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr CreateToolhelp32Snapshot(uint uint_0, uint uint_1);

    [DllImport("KERNEL32.DLL")]
    private static extern int Process32First(IntPtr intptr_0, ref Struct0 struct0_0);

    [DllImport("KERNEL32.DLL")]
    private static extern int Process32Next(IntPtr intptr_0, ref Struct0 struct0_0);

    [DllImport("KERNEL32.DLL")]
    private static extern int Thread32First(IntPtr intptr_0, ref Struct1 struct1_0);

    [DllImport("KERNEL32.DLL")]
    private static extern int Thread32Next(IntPtr intptr_0, ref Struct1 struct1_0);

    public void method_0()
    {
        var toolhelp32Snapshot = CreateToolhelp32Snapshot(2U, 0U);
        if (toolhelp32Snapshot == IntPtr.Zero)
            throw new Exception(GClass30.smethod_2(710, Marshal.GetLastWin32Error()));
        var arrayList = new ArrayList();
        var struct0_0 = new Struct0();
        struct0_0.uint_0 = 296U;
        if (!bool_0)
            GClass37.smethod_1(GClass30.smethod_1(336));
        for (var index = Process32First(toolhelp32Snapshot, ref struct0_0);
             index != 0;
             index = Process32Next(toolhelp32Snapshot, ref struct0_0))
        {
            var gclass66 = new GClass66((int)struct0_0.uint_2, struct0_0.string_0);
            if (!bool_0)
                GClass37.smethod_1(GClass30.smethod_2(711, gclass66.int_0, gclass66.string_0));
            if (gclass66.string_0 == "glider_loader.exe")
                bool_1 = true;
            arrayList.Add(gclass66);
        }

        bool_0 = true;
        CloseHandle(toolhelp32Snapshot);
        gclass66_0 = (GClass66[])arrayList.ToArray(typeof(GClass66));
    }

    public int method_1(string string_0)
    {
        if (gclass66_0 == null)
            return 0;
        var num = 0;
        foreach (var gclass66 in gclass66_0)
            if (string.Compare(gclass66.string_0, string_0, true) == 0)
                ++num;
        return num;
    }

    public int method_2(int int_2)
    {
        if (gclass66_0 == null)
            return 0;
        var num = 0;
        foreach (var gclass66 in gclass66_0)
            if (gclass66.int_0 == int_2)
                ++num;
        return num;
    }

    public GClass66 method_3(string string_0)
    {
        if (gclass66_0 == null)
            return null;
        foreach (var gclass66 in gclass66_0)
            if (string.Compare(gclass66.string_0, string_0, true) == 0)
                return gclass66;
        return null;
    }

    public uint[] method_4(int int_2)
    {
        var uintList = new List<uint>();
        var toolhelp32Snapshot = CreateToolhelp32Snapshot(4U, 0U);
        if (toolhelp32Snapshot == IntPtr.Zero)
            throw new Exception("CreateToolhelp32Snapshot failed!");
        var arrayList = new ArrayList();
        var struct1_0 = new Struct1();
        struct1_0.uint_0 = (uint)Marshal.SizeOf(struct1_0);
        for (var index = Thread32First(toolhelp32Snapshot, ref struct1_0);
             index != 0;
             index = Thread32Next(toolhelp32Snapshot, ref struct1_0))
            if (struct1_0.uint_3 == int_2)
                uintList.Add(struct1_0.uint_2);
        CloseHandle(toolhelp32Snapshot);
        return uintList.ToArray();
    }

    private struct Struct0
    {
        public uint uint_0;
        public uint uint_1;
        public uint uint_2;
        public IntPtr intptr_0;
        public uint uint_3;
        public uint uint_4;
        public uint uint_5;
        public int int_0;
        public uint uint_6;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string string_0;
    }

    private struct Struct1
    {
        public uint uint_0;
        public uint uint_1;
        public uint uint_2;
        public uint uint_3;
        public uint uint_4;
        public uint uint_5;
        public uint uint_6;
    }
}