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
            if (!ConfigManager.gclass61_0.method_5("AllowWriteBytes"))
            {
                GContext.Main.Log("! WriteBytes to 0x" + StartAddress.ToString("x") +
                                  " failed, operation prohibited by configuration (clue = " + DebugClue + ")");
                return false;
            }

            if (!GameMemoryAccess.IsMemoryReadable(StartAddress))
                int_31 = GameMemoryAccess.ReadPointerChain(StartAddress, LengthToWrite, 64);
            var num = GameMemoryAccess.WriteBytes(StartAddress, DataToWrite, LengthToWrite);
            if (num != LengthToWrite)
                GContext.Main.Log("! WriteBytes to 0x" + StartAddress.ToString("x") + " failed, last error: " +
                                  Marshal.GetLastWin32Error() + ",  (clue = " + DebugClue + ")");
            if (int_31 != 0)
                GameMemoryAccess.ReadPointerChain(StartAddress, LengthToWrite, int_31);
            return num == LengthToWrite;
        }

        public byte[] ReadBytes(
            int StartAddress,
            int LengthToRead,
            string DebugClue,
            bool DieIfFailed)
        {
            return GameMemoryAccess.ReadBytes(StartAddress, LengthToRead, DebugClue);
        }

        public byte ReadByte(int StartAddress, string DebugClue)
        {
            return GameMemoryAccess.ReadByte(StartAddress, DebugClue);
        }

        public int ReadInt(int StartAddress, string DebugClue)
        {
            return GameMemoryAccess.ReadInt32(StartAddress, DebugClue);
        }

        public long ReadLong(int StartAddress, string DebugClue)
        {
            return GameMemoryAccess.ReadInt64(StartAddress, DebugClue);
        }

        public float ReadFloat(int StartAddress, string DebugClue)
        {
            return GameMemoryAccess.ReadFloat(StartAddress, DebugClue);
        }

        public string ReadString(int StartAddress, int MaxLength, string DebugClue)
        {
            return GameMemoryAccess.ReadString(StartAddress, MaxLength, DebugClue);
        }
    }
}
