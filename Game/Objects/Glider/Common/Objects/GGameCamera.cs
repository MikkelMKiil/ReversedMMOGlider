// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GGameCamera
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;

namespace Glider.Common.Objects
{
    public class GGameCamera
    {
        private readonly bool _valid;
        private readonly int BaseAddress;

        public GGameCamera()
        {
            _valid = false;
            var num = GameMemoryAccess.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("CameraBase"), "CameraBase");
            if (num == 0)
                return;
            BaseAddress = GameMemoryAccess.ReadInt32(num + MemoryOffsetTable.Instance.GetIntOffset("CameraOff1"), "CameraOff1");
            _valid = true;
            Logger.smethod_1(ToString());
        }

        public float Yaw =>
            !_valid ? 0.0f : GameMemoryAccess.ReadFloat(BaseAddress + MemoryOffsetTable.Instance.GetIntOffset("CameraYaw"), "CameraYaw");

        public float Pitch =>
            !_valid
                ? 0.0f
                : GameMemoryAccess.ReadFloat(BaseAddress + MemoryOffsetTable.Instance.GetIntOffset("CameraPitch"), "CameraPitch");

        public float Zoom =>
            !_valid
                ? 0.0f
                : GameMemoryAccess.ReadFloat(BaseAddress + MemoryOffsetTable.Instance.GetIntOffset("CameraZoom"), "CameraZoom");

        public override string ToString()
        {
            return "GCamera @ 0x" + BaseAddress.ToString("x") + ", Yaw=" + Math.Round(Yaw, 3) + ", Pitch=" +
                   Math.Round(Pitch, 3) + ", Zoom=" + Math.Round(Zoom, 3);
        }
    }
}
