// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GNode
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
namespace Glider.Common.Objects
{
    public class GNode : GObject
    {
        private string _displayInfo;
        private float _heading;
        private float _rotation;

        public GNode(int BaseAddress, int FrameNumber)
            : base(BaseAddress, FrameNumber)
        {
            SetType(GObjectType.Node);
            _displayInfo = null;
        }

        public string DisplayInfo
        {
            get
            {
                if (_displayInfo == null || _displayInfo.StartsWith("("))
                    SetDisplayInfo();
                return _displayInfo;
            }
        }

        public float Rotation
        {
            get
            {
                Refresh();
                return _rotation;
            }
        }

        public float Heading
        {
            get
            {
                Refresh();
                return _heading;
            }
        }

        public bool IsFlower => DisplayInfo.IndexOf("tradeskillnodes\\bush") > -1;

        public bool IsMineral => DisplayInfo.IndexOf("_miningnode_") > -1;

        public bool IsTreasure => DisplayInfo.IndexOf("treasure") > -1;

        public bool IsMailBox
        {
            get
            {
                DisplayInfo.ToLower();
                SetName();
                return DisplayInfo.IndexOf("postbox") > -1 || _name.ToLower().IndexOf("mailbox") > -1;
            }
        }

        protected override void LoadFields()
        {
            _heading = GetStorageFloat("GAMEOBJECT_FACING");
            _rotation = GetStorageFloat("GAMEOBJECT_ROTATION");
            _location = new GLocation(GetStorageFloat("GAMEOBJECT_POS_X"), GetStorageFloat("GAMEOBJECT_POS_Y"),
                GetStorageFloat("GAMEOBJECT_POS_Z"));
        }

        protected void SetDisplayInfo()
        {
            _displayInfo = "(unknown)";
            var num1 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("ObjectInfo"), "InfoBase");
            var storageInt = GetStorageInt("GAMEOBJECT_DISPLAYID");
            var num2 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("ObjectInfoSub"), "InfoBaseSub");
            if (storageInt < num2)
                return;
            var num3 = GProcessMemoryManipulator.smethod_11(num1 + (storageInt - num2) * 4, "InfoThisGuy");
            if (num3 == 0 || num3 < 8388608)
                return;
            var int_29 = GProcessMemoryManipulator.smethod_11(num3 + 4, "InfoDisplayPtr");
            if (int_29 == 0)
                _displayInfo = "(null)";
            else
                _displayInfo = GProcessMemoryManipulator.smethod_9(int_29, 128, "DisplayInfo").ToLower();
        }

        protected override void SetName()
        {
            var baseInt = GetBaseInt("NodeName");
            if (baseInt == 0)
                return;
            var int_29 = GProcessMemoryManipulator.smethod_11(baseInt + GClass18.gclass18_0.method_4("NodeNameSecond"), "noden2");
            if (int_29 == 0)
                return;
            _name = GProcessMemoryManipulator.smethod_9(int_29, 64, "nodename");
        }
    }
}