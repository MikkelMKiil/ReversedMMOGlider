// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GContainer
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
namespace Glider.Common.Objects
{
    public class GContainer : GItem
    {
        public GContainer(int BaseAddress, int FrameNumber)
            : base(BaseAddress, FrameNumber)
        {
            SetType(GObjectType.Container);
            SlotCount = 0;
        }

        public int SlotCount { get; private set; }

        public long[] BagContents
        {
            get
            {
                var bagContents = new long[SlotCount];
                var num = _descriptor.GetOffsetValue("CONTAINER_FIELD_SLOT_1");
                for (var index = 0; index < SlotCount; ++index)
                    bagContents[index] = GProcessMemoryManipulator.smethod_12(StorageAddress + num + index * 8, "bagc");
                return bagContents;
            }
        }

        protected override void LoadFields()
        {
            base.LoadFields();
            SlotCount = GetStorageInt("CONTAINER_FIELD_NUM_SLOTS");
        }

        public override string ToString()
        {
            return base.ToString() + ", SlotCount=" + SlotCount;
        }
    }
}