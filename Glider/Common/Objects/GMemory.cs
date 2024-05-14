// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GMemory
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Runtime.InteropServices;

namespace Glider.Common.Objects
{
    public class GMemory
    {
        public IntPtr ProcessHandle => StartupClass.AdditionalApplicationHandle;

        public bool WriteBytes(
            int StartAddress,
            byte[] DataToWrite,
            int LengthToWrite,
            string DebugClue)
        {
            var int_31 = 0;
            if (!GClass61.gclass61_0.method_5("AllowWriteBytes"))
            {
                GContext.Main.Log("! WriteBytes to 0x" + StartAddress.ToString("x") +
                                  " failed, operation prohibited by configuration (clue = " + DebugClue + ")");
                return false;
            }

            if (!GProcessMemoryManipulator.smethod_35(StartAddress))
                int_31 = GProcessMemoryManipulator.smethod_36(StartAddress, LengthToWrite, 64);
            var num = GProcessMemoryManipulator.smethod_16(StartAddress, DataToWrite, LengthToWrite);
            if (num != LengthToWrite)
                GContext.Main.Log("! WriteBytes to 0x" + StartAddress.ToString("x") + " failed, last error: " +
                                  Marshal.GetLastWin32Error() + ",  (clue = " + DebugClue + ")");
            if (int_31 != 0)
                GProcessMemoryManipulator.smethod_36(StartAddress, LengthToWrite, int_31);
            return num == LengthToWrite;
        }

        public byte[] ReadBytes(
            int StartAddress,
            int LengthToRead,
            string DebugClue,
            bool DieIfFailed)
        {
            if (DieIfFailed)
                return GProcessMemoryManipulator.smethod_17(StartAddress, LengthToRead, DebugClue);
            var numArray = GProcessMemoryManipulator.smethod_20(StartAddress, LengthToRead);
            if (numArray == null)
                GContext.Main.Log("! Read failed at 0x" + StartAddress.ToString("x") + ", debug clue = " + DebugClue);
            return numArray;
        }

        public byte ReadByte(int StartAddress, string DebugClue)
        {
            return GProcessMemoryManipulator.smethod_15(StartAddress, DebugClue);
        }

        public int ReadInt(int StartAddress, string DebugClue)
        {
            return GProcessMemoryManipulator.smethod_11(StartAddress, DebugClue);
        }

        public long ReadLong(int StartAddress, string DebugClue)
        {
            return GProcessMemoryManipulator.smethod_12(StartAddress, DebugClue);
        }

        public float ReadFloat(int StartAddress, string DebugClue)
        {
            return GProcessMemoryManipulator.smethod_13(StartAddress, DebugClue);
        }

        public string ReadString(int StartAddress, int MaxLength, string DebugClue)
        {
            return GProcessMemoryManipulator.smethod_9(StartAddress, MaxLength, DebugClue);
        }
    }
}