// Decompiled with JetBrains decompiler
// Type: GClass71
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

public class GClass71
{
    public enum GEnum10
    {
        const_0 = -1, // 0xFFFFFFFF
        const_1 = 0,
        const_2 = 1,
        const_3 = 2
    }

    [Flags]
    public enum GEnum11 : uint
    {
        flag_0 = 2147483648, // 0x80000000
        flag_1 = 1073741824, // 0x40000000
        flag_2 = 536870912, // 0x20000000
        flag_3 = 268435456 // 0x10000000
    }

    [Flags]
    public enum GEnum12 : uint
    {
        flag_0 = 0,
        flag_1 = 1,
        flag_2 = 2,
        flag_3 = 4
    }

    public enum GEnum13 : uint
    {
        const_0 = 1,
        const_1 = 2,
        const_2 = 3,
        const_3 = 4,
        const_4 = 5
    }

    [Flags]
    public enum GEnum14 : uint
    {
        flag_0 = 1,
        flag_1 = 2,
        flag_2 = 4,
        flag_3 = 16, // 0x00000010
        flag_4 = 32, // 0x00000020
        flag_5 = 64, // 0x00000040
        flag_6 = 128, // 0x00000080
        flag_7 = 256, // 0x00000100
        flag_8 = 512, // 0x00000200
        flag_9 = 1024, // 0x00000400
        flag_10 = 2048, // 0x00000800
        flag_11 = 4096, // 0x00001000
        flag_12 = 8192, // 0x00002000
        flag_13 = 16384, // 0x00004000
        flag_14 = 2147483648, // 0x80000000
        flag_15 = 1073741824, // 0x40000000
        flag_16 = 536870912, // 0x20000000
        flag_17 = 268435456, // 0x10000000
        flag_18 = 134217728, // 0x08000000
        flag_19 = 67108864, // 0x04000000
        flag_20 = 33554432, // 0x02000000
        flag_21 = 16777216, // 0x01000000
        flag_22 = 2097152, // 0x00200000
        flag_23 = 1048576, // 0x00100000
        flag_24 = 524288 // 0x00080000
    }

    private const uint uint_0 = 131088;
    protected const int int_0 = 983103;
    protected const int int_1 = 983551;
    protected const int int_2 = 1;
    protected const int int_3 = 1;
    protected const int int_4 = 3;
    protected const int int_5 = 1;
    protected const int int_6 = 0;
    protected const int int_7 = 1;
    protected const int int_8 = 1;
    private const uint uint_1 = 1;
    private const uint uint_2 = 2;
    private const uint uint_3 = 4;
    private const uint uint_4 = 34832;
    private const int int_9 = 0;
    private const int int_10 = 3;
    private const uint uint_5 = 2032127;
    private const uint uint_6 = 0;
    private static string string_1;
    private bool bool_0;
    private bool bool_1;
    private bool bool_2;
    private byte[] byte_0;
    private GEnum9 genum9_0;
    private int int_11;
    private int int_12;
    private int int_13;
    private int int_14;
    private int int_15;
    private int int_16;
    private int int_17;
    private int int_18;
    private int int_19;
    private int int_20;
    private int int_21;
    private int int_22;
    private int int_23;
    private int int_24;
    private int int_25;
    private int int_26;
    private int int_27;
    private int int_28;
    private int[] int_29;
    private IntPtr intptr_0;
    private IntPtr intptr_1;
    private IntPtr intptr_2;
    public string string_0;

    public GClass71(string string_2)
    {
        string_0 = string_2;
        intptr_0 = IntPtr.Zero;
        intptr_1 = IntPtr.Zero;
        intptr_2 = IntPtr.Zero;
    }

    private static void smethod_0(string string_2)
    {
        Logger.LogMessage(string_2);
    }

    private static void smethod_1(string string_2)
    {
        Logger.smethod_1(string_2);
    }

    public void method_0()
    {
        method_7();
        method_6();
        method_5();
    }

    [SpecialName]
    public bool method_1()
    {
        method_2();
        var intptr_3 = OpenService(intptr_0, string_0, 1);
        if (!(intptr_3 != IntPtr.Zero))
            return false;
        CloseServiceHandle(intptr_3);
        return true;
    }

    private void method_2()
    {
        if (intptr_0 != IntPtr.Zero)
            return;
        intptr_0 = OpenSCManagerW(null, null, 983103U);
        if (intptr_0 == IntPtr.Zero)
            throw new Exception("OpenSCManager failed, last error: " + Marshal.GetLastWin32Error());
    }

    private bool method_3()
    {
        if (intptr_1 != IntPtr.Zero)
            return true;
        intptr_1 = OpenService(intptr_0, string_0, 983551);
        if (!(intptr_1 == IntPtr.Zero))
            return true;
        smethod_0("Couldn't open service: \"" + string_0 + "\", must be an old driver");
        return false;
    }

    private bool method_4()
    {
        if (intptr_2 != IntPtr.Zero)
            return true;
        intptr_2 = CreateFile("\\\\.\\" + string_0, GEnum11.flag_0, 3U, IntPtr.Zero, GEnum13.const_2, GEnum14.flag_6,
            IntPtr.Zero);
        if (!(intptr_2 == IntPtr.Zero) && intptr_2.ToInt32() != -1)
            return true;
        smethod_0("! Couldn't open \"" + string_0 + "\" driver, last error = " + Marshal.GetLastWin32Error());
        return false;
    }

    private void method_5()
    {
        if (!(intptr_0 != IntPtr.Zero))
            return;
        CloseHandle(intptr_0);
        intptr_0 = IntPtr.Zero;
    }

    private void method_6()
    {
        if (!(intptr_1 != IntPtr.Zero))
            return;
        CloseServiceHandle(intptr_1);
        intptr_1 = IntPtr.Zero;
    }

    private void method_7()
    {
        if (!(intptr_2 != IntPtr.Zero))
            return;
        CloseHandle(intptr_2);
        intptr_2 = IntPtr.Zero;
    }

    public void method_8()
    {
        var string_4 = Environment.CurrentDirectory + "\\" + string_0 + ".sys";
        method_2();
        smethod_0("Installing service, DriverName=\"" + string_0 + "\", FullDriverName=\"" + string_4 + "\"");
        var service = CreateService(intptr_0, string_0, "", 983551, 1, 3, 1, string_4, null, IntPtr.Zero, null, null,
            null);
        if (service == IntPtr.Zero)
            throw new Exception("CreateService failed, last error: " + Marshal.GetLastWin32Error());
        smethod_0("Created service for: " + string_4 + ", hDriver: " + service.ToInt32().ToString("x"));
        method_9();
    }

    public void method_9()
    {
        method_3();
        if (!StartService(intptr_1, 0U, IntPtr.Zero))
            throw new Exception("StartService failed, last error: " + Marshal.GetLastWin32Error());
        smethod_0("Service started successfully");
    }

    [SpecialName]
    public bool method_10()
    {
        if (bool_2)
            return false;
        method_2();
        if (!method_3())
            return false;
        GStruct18 gstruct18_0;
        if (!QueryServiceStatusEx(intptr_1, 0, out gstruct18_0, Marshal.SizeOf(typeof(GStruct18)), out var _))
            throw new Exception("Unable to query service status for: " + string_0);
        return gstruct18_0.uint_1 != 1U;
    }

    public void method_11()
    {
        if (bool_2)
            return;
        bool_2 = true;
        var path = string_0 + ".sys";
        smethod_0("Removing old driver: " + string_0);
        method_2();
        if (!method_3())
        {
            File.Delete(path);
        }
        else
        {
            GStruct18 gstruct18_0;
            if (!QueryServiceStatusEx(intptr_1, 0, out gstruct18_0, Marshal.SizeOf(typeof(GStruct18)), out var _))
                throw new Exception("Unable to query service status for: " + string_0);
            if (gstruct18_0.uint_1 != 1U)
            {
                try
                {
                    method_21();
                }
                catch (Exception ex)
                {
                    smethod_0("* Exception in CommandUnhook: " + ex.Message + ex.StackTrace);
                    smethod_0("Stopping driver anyway");
                }

                method_7();
                smethod_0("Stopping service");
                if (!ControlService(intptr_1, 1, out var _))
                    throw new Exception("Unable to stop service: " + string_0);
            }

            if (!DeleteService(intptr_1))
                throw new Exception("Unable to delete service: " + string_0);
            method_0();
            File.Delete(path);
        }
    }

    private void method_12(uint uint_7)
    {
        smethod_1("Shadow command: 0x" + uint_7.ToString("x"));
        if (!method_4())
            smethod_0("! No instance of \"" + string_0 + "\", not sending command");
        else if (!DeviceIoControl(intptr_2, smethod_8(34832U, uint_7, 3U, 0U), IntPtr.Zero, 0U, IntPtr.Zero, 0U,
                     out var _, IntPtr.Zero))
            throw new Exception("! DeviceIoControl failed in ShadowDriver.SendCommand, last error: " +
                                Marshal.GetLastWin32Error());
    }

    private void method_13(uint uint_7, byte[] byte_1)
    {
        smethod_1("Shadow command: 0x" + uint_7.ToString("x") + ", parameters: " + byte_1.Length + " bytes");
        if (!method_4())
            smethod_0("! No instance of \"" + string_0 + "\", not sending command");
        var gcHandle = GCHandle.Alloc(byte_1, GCHandleType.Pinned);
        var flag = DeviceIoControl(intptr_2, smethod_8(34832U, uint_7, 0U, 0U), gcHandle.AddrOfPinnedObject(),
            (uint)byte_1.Length, IntPtr.Zero, 0U, out var _, IntPtr.Zero);
        gcHandle.Free();
        if (!flag)
            throw new Exception("! DeviceIoControl failed in ShadowDriver.SendCommand, last error: " +
                                Marshal.GetLastWin32Error());
    }

    public int method_14(uint uint_7, byte[] byte_1)
    {
        var gcHandle = GCHandle.Alloc(byte_1, GCHandleType.Pinned);
        var num = method_16(uint_7, gcHandle.AddrOfPinnedObject(), (uint)byte_1.Length);
        gcHandle.Free();
        return num;
    }

    public int method_15(uint uint_7, int[] int_30)
    {
        var gcHandle = GCHandle.Alloc(int_30, GCHandleType.Pinned);
        var num = method_16(uint_7, gcHandle.AddrOfPinnedObject(), (uint)(int_30.Length * 4));
        gcHandle.Free();
        return num;
    }

    public int method_16(uint uint_7, IntPtr intptr_3, uint uint_8)
    {
        if (!method_4())
        {
            smethod_1("! No instance of \"" + string_0 + "\", not sending command");
            return 0;
        }

        uint uint_10;
        if (!DeviceIoControl(intptr_2, smethod_8(34832U, uint_7, 3U, 0U), IntPtr.Zero, 0U, intptr_3, uint_8,
                out uint_10, IntPtr.Zero))
            throw new Exception("! DeviceIoControl failed in ShadowDriver.SendCommand, last error: " +
                                Marshal.GetLastWin32Error());
        return (int)uint_10;
    }

    public byte[] method_17(uint uint_7, uint uint_8)
    {
        var numArray = new byte[uint_8];
        smethod_1("Shadow command: 0x" + uint_7.ToString("x"));
        if (!method_4())
        {
            smethod_1("! No instance of \"" + string_0 + "\", not sending command");
            return null;
        }

        var gcHandle = GCHandle.Alloc(numArray, GCHandleType.Pinned);
        smethod_1("Buffer is at: 0x" + gcHandle.AddrOfPinnedObject().ToInt32().ToString("x"));
        uint uint_10;
        var flag = DeviceIoControl(intptr_2, smethod_8(34832U, uint_7, 3U, 0U), IntPtr.Zero, 0U,
            gcHandle.AddrOfPinnedObject(), uint_8, out uint_10, IntPtr.Zero);
        gcHandle.Free();
        if (!flag)
            throw new Exception("! DeviceIoControl failed in ShadowDriver.SendCommand, last error: " +
                                Marshal.GetLastWin32Error());
        if ((int)uint_10 != (int)uint_8)
            throw new Exception("! Expected " + uint_8 + " bytes back from command 0x" + uint_7.ToString("x") +
                                ", only got " + uint_10);
        return numArray;
    }

    public byte[] method_18(uint uint_7, int int_30, uint uint_8)
    {
        var numArray = new byte[uint_8];
        smethod_1("Shadow command: 0x" + uint_7.ToString("x"));
        if (!method_4())
        {
            smethod_1("! No instance of \"" + string_0 + "\", not sending command");
            return null;
        }

        var gcHandle1 = GCHandle.Alloc(BitConverter.GetBytes(int_30), GCHandleType.Pinned);
        var gcHandle2 = GCHandle.Alloc(numArray, GCHandleType.Pinned);
        uint uint_10;
        var flag = DeviceIoControl(intptr_2, smethod_8(34832U, uint_7, 3U, 0U), gcHandle1.AddrOfPinnedObject(), 4U,
            gcHandle2.AddrOfPinnedObject(), uint_8, out uint_10, IntPtr.Zero);
        gcHandle2.Free();
        gcHandle1.Free();
        if (!flag)
            throw new Exception("! DeviceIoControl failed in ShadowDriver.SendCommand, last error: " +
                                Marshal.GetLastWin32Error());
        if ((int)uint_10 != (int)uint_8)
            throw new Exception("! Expected " + uint_8 + " bytes back from command 0x" + uint_7.ToString("x") +
                                ", only got " + uint_10);
        return numArray;
    }

    private void method_19(uint uint_7, int int_30)
    {
        method_13(uint_7, BitConverter.GetBytes(int_30));
    }

    public IntPtr method_20(int int_30)
    {
        return new IntPtr(BitConverter.ToInt32(method_18(2086U, int_30, 4U), 0));
    }

    public void method_21()
    {
        method_12(2049U);
    }

    public void method_22()
    {
        method_12(2082U);
    }

    public byte[] method_23(out GEnum10 genum10_0)
    {
        genum10_0 = GEnum10.const_0;
        if (byte_0 == null)
            byte_0 = new byte[65536];
        var num = method_14(2070U, byte_0);
        if (num < 4)
            throw new Exception("Unexpected reply length in GetWarden: 0x" + num.ToString("x"));
        var int32_1 = BitConverter.ToInt32(byte_0, 0);
        switch (int32_1)
        {
            case -1:
                throw new Exception("Unexpected Warden length in GetWarden - buffer too small?");
            case 0:
                return null;
            case 1:
                var int32_2 = BitConverter.ToInt32(byte_0, 4);
                genum10_0 = GEnum10.const_2;
                return GProcessMemoryManipulator.smethod_17(int32_2, 8192, "bsb1");
            case 2:
                var int32_3 = BitConverter.ToInt32(byte_0, 4);
                genum10_0 = GEnum10.const_3;
                return GProcessMemoryManipulator.smethod_17(int32_3, 4096, "bsb2");
            default:
                var destinationArray = new byte[int32_1];
                Array.Copy(byte_0, 4, destinationArray, 0, int32_1);
                return destinationArray;
        }
    }

    public void method_24(int int_30)
    {
        method_19(2071U, int_30);
    }

    public void method_25(int int_30)
    {
        method_19(2087U, int_30);
    }

    public bool method_26(int int_30)
    {
        try
        {
            method_19(2083U, int_30);
            return Marshal.GetLastWin32Error() != -777;
        }
        catch (Exception ex)
        {
            if (Marshal.GetLastWin32Error() != -777)
                throw ex;
            return false;
        }
    }

    public void method_27(int int_30)
    {
        method_19(2067U, int_30);
    }

    public void method_28(int int_30)
    {
        method_19(2051U, int_30);
    }

    public void method_29(IntPtr intptr_3)
    {
        method_19(2072U, intptr_3.ToInt32());
    }

    public void method_30()
    {
        method_12(2073U);
    }

    public void method_31()
    {
        method_12(2080U);
    }

    public void method_32()
    {
        method_12(2050U);
    }

    public void method_33(bool bool_3)
    {
        if (bool_2 || bool_3 == bool_0)
            return;
        bool_0 = bool_3;
        method_19(2066U, bool_3 ? 1 : 0);
    }

    public void method_34(int int_30, IntPtr intptr_3)
    {
        if (bool_1)
            return;
        bool_1 = true;
        var memoryStream = new MemoryStream();
        memoryStream.Write(BitConverter.GetBytes(int_30), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(intptr_3.ToInt32()), 0, 4);
        method_13(2064U, memoryStream.ToArray());
    }

    public void method_35()
    {
        method_37();
        if (int_11 == 0)
            return;
        var memoryStream = new MemoryStream();
        memoryStream.Write(BitConverter.GetBytes((int)genum9_0), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_12), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_11), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_13), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_14), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_15), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_16), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_17), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_18), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_19), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_20), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_21), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_22), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_23), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_25), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_24), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_26), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_27), 0, 4);
        memoryStream.Write(BitConverter.GetBytes(int_28), 0, 4);
        method_13(2053U, memoryStream.ToArray());
    }

    public void method_36(out int int_30, out int int_31)
    {
        var numArray = method_17(2069U, 8U);
        int_30 = BitConverter.ToInt32(numArray, 0);
        int_31 = BitConverter.ToInt32(numArray, 4);
    }

    public bool method_37()
    {
        genum9_0 = smethod_2(false);
        switch (genum9_0)
        {
            case GEnum9.const_1:
                int_12 = 156;
                int_11 = 160;
                int_21 = 151;
                int_20 = 106;
                int_28 = 16;
                return true;
            case GEnum9.const_2:
                int_12 = 132;
                int_11 = 136;
                int_13 = 323;
                int_14 = 38;
                int_15 = 324;
                int_16 = 111;
                int_17 = 404;
                int_18 = 435;
                int_19 = 1;
                int_20 = 122;
                int_21 = 173;
                int_22 = 400;
                int_23 = 333;
                int_25 = 66;
                int_24 = 511;
                int_26 = 312;
                int_27 = 483;
                int_28 = 17;
                return true;
            case GEnum9.const_3:
                int_12 = 156;
                int_11 = 160;
                int_13 = 334;
                int_14 = 104;
                int_15 = 334;
                int_16 = 115;
                int_17 = 418;
                int_18 = 449;
                int_19 = 1;
                int_20 = 194;
                int_21 = 248;
                int_22 = 414;
                int_23 = 323;
                int_25 = 64;
                int_24 = 534;
                int_26 = 322;
                int_27 = 504;
                int_28 = 18;
                return true;
            default:
                return false;
        }
    }

    public static GEnum9 smethod_2(bool bool_3)
    {
        var struct2_0 = new Struct2();
        struct2_0.int_0 = Marshal.SizeOf(struct2_0);
        GetVersionEx(ref struct2_0);
        string_1 = struct2_0.string_0;
        if (bool_3)
            smethod_0("Major: " + struct2_0.int_1 + ", minor: " + struct2_0.int_2 + ", SP: \"" +
                      (string_1 == null ? "(null)" : (object)string_1) + "\"");
        if (struct2_0.int_1 == 5 && struct2_0.int_2 == 0)
            return GEnum9.const_1;
        if (struct2_0.int_1 == 5 && struct2_0.int_2 == 1)
            return GEnum9.const_2;
        if (struct2_0.int_1 == 6 && struct2_0.int_2 == 0)
            return GEnum9.const_3;
        if (bool_3)
            smethod_0("!! Unknown operating system!  Major=" + struct2_0.int_1 + ", Minor=" + struct2_0.int_2);
        return GEnum9.const_0;
    }

    public static int smethod_3()
    {
        if (string_1 != null)
            if (string_1.Length != 0)
                try
                {
                    return int.Parse(string_1.Substring(string_1.Length - 1, 1));
                }
                catch (Exception ex)
                {
                    return 0;
                }

        return 0;
    }

    public bool method_38()
    {
        return method_39() && method_40();
    }

    public bool method_39()
    {
        var num = OpenProcess(131088U, false, 489335);
        if (num.ToInt32() == 419430)
            return true;
        smethod_0("CNT not right, handle = 0x" + num.ToInt32().ToString("x") + ", last error = 0x" +
                  Marshal.GetLastWin32Error().ToString("x"));
        return false;
    }

    public bool method_40()
    {
        uint uint_7 = 489335;
        if (VirtualAlloc(IntPtr.Zero, ref uint_7, 4096U, 23U).ToInt32() != 21841)
        {
            Logger.LogMessage("VAP not right in CVA");
            return false;
        }

        Logger.smethod_1("VAP is good in CVA");
        return true;
    }

    public int method_41(int int_30, byte[] byte_1, int int_31, out int int_32)
    {
        var gcHandle = GCHandle.Alloc(byte_1, GCHandleType.Pinned);
        lock (this)
        {
            if (int_29 == null)
                int_29 = new int[4];
            int_29[0] = int_30;
            int_29[1] = int_31;
            int_29[2] = 0;
            int_29[3] = gcHandle.AddrOfPinnedObject().ToInt32();
            method_15(2081U, int_29);
            int_32 = int_29[2];
        }

        gcHandle.Free();
        return int_32;
    }

    public static void smethod_4()
    {
        foreach (var file in Directory.GetFiles(Environment.CurrentDirectory, "*.sys"))
        {
            smethod_0("OneDriver: \"" + file + "\"");
            var str = file.Substring(file.LastIndexOf('\\') + 1);
            smethod_0("DriverFilename: \"" + str + "\"");
            var string_2 = str.Substring(0, str.IndexOf('.'));
            smethod_0("DriverName: \"" + string_2 + "\"");
            new GClass71(string_2).method_11();
        }
    }

    public static string smethod_5()
    {
        string string_2;
        do
        {
            string_2 = smethod_7();
        } while (smethod_6(string_2));

        return string_2;
    }

    public static bool smethod_6(string string_2)
    {
        var gclass71 = new GClass71(string_2);
        var flag = gclass71.method_1();
        gclass71.method_0();
        return flag;
    }

    private static string smethod_7()
    {
        var random = new Random();
        var num = random.Next() % 8 + 3;
        var stringBuilder = new StringBuilder();
        while (stringBuilder.Length < num)
            stringBuilder.Append((char)(random.Next() % 26 + 97));
        return stringBuilder.ToString();
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint uint_7, bool bool_3, int int_30);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool DeviceIoControl(
        IntPtr intptr_3,
        uint uint_7,
        IntPtr intptr_4,
        uint uint_8,
        IntPtr intptr_5,
        uint uint_9,
        out uint uint_10,
        IntPtr intptr_6);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr CreateFile(
        string string_2,
        GEnum11 genum11_0,
        uint uint_7,
        IntPtr intptr_3,
        GEnum13 genum13_0,
        GEnum14 genum14_0,
        IntPtr intptr_4);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetModuleHandle(string string_2);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
    public static extern IntPtr GetProcAddress(IntPtr intptr_3, string string_2);

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(Delegate0 delegate0_0, IntPtr intptr_3);

    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool QueryServiceStatusEx(
        IntPtr intptr_3,
        int int_30,
        out GStruct18 gstruct18_0,
        int int_31,
        out int int_32);

    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool ControlService(
        IntPtr intptr_3,
        int int_30,
        out GStruct19 gstruct19_0);

    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool DeleteService(IntPtr intptr_3);

    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool StartService(IntPtr intptr_3, uint uint_7, IntPtr intptr_4);

    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    protected static extern IntPtr OpenSCManagerW(string string_2, string string_3, uint uint_7);

    [DllImport("advapi32.dll", SetLastError = true)]
    protected static extern IntPtr CreateService(
        IntPtr intptr_3,
        string string_2,
        string string_3,
        int int_30,
        int int_31,
        int int_32,
        int int_33,
        string string_4,
        string string_5,
        IntPtr intptr_4,
        string string_6,
        string string_7,
        string string_8);

    [DllImport("advapi32.dll", SetLastError = true)]
    protected static extern bool CloseServiceHandle(IntPtr intptr_3);

    [DllImport("kernel32.dll", SetLastError = true)]
    protected static extern bool CloseHandle(IntPtr intptr_3);

    [DllImport("advapi32.dll", SetLastError = true)]
    protected static extern IntPtr OpenService(IntPtr intptr_3, string string_2, int int_30);

    [DllImport("kernel32.dll")]
    public static extern int DeviceIoControl(
        IntPtr intptr_3,
        int int_30,
        ref short short_0,
        int int_31,
        IntPtr intptr_4,
        int int_32,
        ref int int_33,
        IntPtr intptr_5);

    protected static uint smethod_8(uint uint_7, uint uint_8, uint uint_9, uint uint_10)
    {
        return (uint)(((int)uint_7 << 16) | ((int)uint_10 << 14) | ((int)uint_8 << 2)) | uint_9;
    }

    [DllImport("kernel32")]
    private static extern bool GetVersionEx(ref Struct2 struct2_0);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr VirtualAlloc(
        IntPtr intptr_3,
        ref uint uint_7,
        uint uint_8,
        uint uint_9);

    private delegate int Delegate0(IntPtr intptr_0, IntPtr intptr_1);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct GStruct18
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
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct GStruct19
    {
        public uint uint_0;
        public uint uint_1;
        public uint uint_2;
        public uint uint_3;
        public uint uint_4;
        public uint uint_5;
        public uint uint_6;
    }

    private struct Struct2
    {
        public int int_0;
        public int int_1;
        public int int_2;
        public int int_3;
        public int int_4;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string string_0;
    }
}