// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GObject
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Threading;

namespace Glider.Common.Objects
{
    public class GObject
    {
        protected static int PawSpeedMS;
        protected bool _culled;
        protected GClass43 _descriptor;
        protected GLocation _location;
        protected string _name;
        protected int _tickFirstSeen;
        protected string _title;
        protected GObjectType _type;
        public int BaseAddress;
        public int FrameNumber;
        public long GUID;
        private int LastUpdate;
        public object ObjectTag;
        public int StorageAddress;
        public string Tag;

        protected GObject(int BaseAddress, int FrameNumber)
        {
            _location = null;
            _type = GObjectType.Unknown;
            _descriptor = null;
            _name = "(unknown)";
            _title = "(unknown)";
            _tickFirstSeen = Environment.TickCount;
            LastUpdate = _tickFirstSeen - 5000;
            _culled = false;
            Tag = null;
            ObjectTag = null;
            this.BaseAddress = BaseAddress;
            StorageAddress = GProcessMemoryManipulator.smethod_11(BaseAddress + 8, "GameObjStorage");
            GUID = GProcessMemoryManipulator.smethod_12(BaseAddress + 48, "NewObjGUID");
            this.FrameNumber = FrameNumber;
        }

        public bool IsValid => !_culled;

        public int Age => Environment.TickCount - _tickFirstSeen;

        public GLocation Location
        {
            get
            {
                Refresh();
                return _location;
            }
        }

        public GObjectType Type
        {
            get
            {
                Refresh();
                return _type;
            }
        }

        public bool IsUnit => Type == GObjectType.Monster || Type == GObjectType.Player;

        public virtual string Name
        {
            get
            {
                if ((_name == null || _name.StartsWith("(")) && IsValid)
                    SetName();
                return _name;
            }
        }

        public virtual string Title
        {
            get
            {
                if ((_title == null || _title.StartsWith("(")) && IsValid)
                    SetTitle();
                return _title;
            }
        }

        public float DistanceToSelf => GContext.Main.Me != null ? GContext.Main.Me.GetDistanceTo(this) : 0.0f;

        public bool IsCursorOnObject =>
            GProcessMemoryManipulator.smethod_12(GClass18.gclass18_0.method_4("UnderCursor"), "UnderCursor") == GUID;

        public bool IsBobbing => GetBaseInt("Bobber") == 1;

        public static GObject Create(int BaseAddress, int FrameNumber)
        {
            switch (QuickGetType(BaseAddress))
            {
                case GObjectType.Item:
                    return new GItem(BaseAddress, FrameNumber);
                case GObjectType.Container:
                    return new GContainer(BaseAddress, FrameNumber);
                case GObjectType.Monster:
                    return new GMonster(BaseAddress, FrameNumber);
                case GObjectType.Player:
                    return new GPlayer(BaseAddress, FrameNumber);
                case GObjectType.Node:
                    return new GNode(BaseAddress, FrameNumber);
                default:
                    return new GObject(BaseAddress, FrameNumber);
            }
        }

        private static GObjectType QuickGetType(int BaseAddress)
        {
            return (GObjectType)GProcessMemoryManipulator.smethod_11(BaseAddress + 20, "QuickType");
        }

        public bool Refresh()
        {
            return Refresh(false);
        }

        public bool Refresh(bool BypassTimer)
        {
            if (Environment.TickCount - LastUpdate < 50 && !BypassTimer)
                return true;
            if (_culled)
                return false;
            if (!IsValid)
            {
                GClass37.smethod_1("! Refresh invoked on invalid object: " + ToString());
                return false;
            }

            try
            {
                LoadFields();
            }
            catch (GException1 ex)
            {
                Cull();
                GClass37.smethod_1("Catching readfailed in GObject.Refresh, object is no longer valid (rf: " + ex +
                                   ", object data: " + ToString() + ")");
                return false;
            }

            LastUpdate = Environment.TickCount;
            return IsValid;
        }

        public void Cull()
        {
            _culled = true;
        }

        protected int FindDescriptorOffset(string FieldName)
        {
            return _descriptor == null ? 0 : _descriptor.method_1(FieldName);
        }

        protected int GetStorageInt(string Name)
        {
            var descriptorOffset = FindDescriptorOffset(Name);
            return descriptorOffset == 0
                ? -1
                : GProcessMemoryManipulator.smethod_21(StorageAddress + descriptorOffset, "ReadSI." + Name);
        }

        protected long GetStorageLong(string Name)
        {
            var descriptorOffset = FindDescriptorOffset(Name);
            return descriptorOffset == 0
                ? -1L
                : GProcessMemoryManipulator.smethod_24(StorageAddress + descriptorOffset, "ReadSL." + Name);
        }

        protected float GetStorageFloat(string Name)
        {
            var descriptorOffset = FindDescriptorOffset(Name);
            return descriptorOffset == 0
                ? float.NaN
                : GProcessMemoryManipulator.smethod_22(StorageAddress + descriptorOffset, "ReadSF." + Name);
        }

        protected int GetBaseInt(string OffsetName)
        {
            return GProcessMemoryManipulator.smethod_21(BaseAddress + GClass18.gclass18_0.method_4(OffsetName), "ReadBI." + OffsetName);
        }

        protected long GetBaseLong(string OffsetName)
        {
            return GProcessMemoryManipulator.smethod_24(BaseAddress + GClass18.gclass18_0.method_4(OffsetName), "ReadBL." + OffsetName);
        }

        protected float GetBaseFloat(string OffsetName)
        {
            return GProcessMemoryManipulator.smethod_22(BaseAddress + GClass18.gclass18_0.method_4(OffsetName), "ReadBF." + OffsetName);
        }

        protected void SetType(GObjectType Type)
        {
            _type = Type;
            switch (_type)
            {
                case GObjectType.Item:
                    _descriptor = StartupClass.gclass43_3;
                    break;
                case GObjectType.Container:
                    _descriptor = StartupClass.gclass43_4;
                    break;
                case GObjectType.Monster:
                    _descriptor = StartupClass.gclass43_1;
                    break;
                case GObjectType.Player:
                    _descriptor = StartupClass.gclass43_0;
                    break;
                case GObjectType.Node:
                    _descriptor = StartupClass.gclass43_2;
                    break;
            }
        }

        public override string ToString()
        {
            return "GUID=" + GUID.ToString("x") + ",BaseAddr=" + BaseAddress.ToString("x8") + ",Type=" + Type +
                   ",Name=\"" + Name + "\", Culled=" + _culled;
        }

        public float GetDistanceTo(GObject Target)
        {
            return GetDistanceTo(Target.Location);
        }

        public float GetDistanceTo(GLocation Location)
        {
            return Location.GetDistanceTo(this.Location);
        }

        public bool Hover()
        {
            PawSpeedMS = GClass61.gclass61_0.method_3("PawSpeed");
            StartupClass.gclass68_0.method_3(true);
            if (StartupClass.bool_11)
                GClass55.smethod_18(GClass55.double_0, GClass55.double_1);
            if ((IsCursorOnObject && !StartupClass.bool_11) || TryPaw(0.5) || TryPaw(0.0) || TryPaw(1.0) ||
                TryPaw(-0.5) || TryPaw(1.5) || TryPaw(2.0))
                return true;
            foreach (var glocation in StartupClass.gclass33_0.list_0)
            {
                GClass55.smethod_18(glocation.X, glocation.Y);
                Thread.Sleep(PawSpeedMS);
                if (IsCursorOnObject)
                    return true;
            }

            return false;
        }

        public bool Interact(bool Ignored)
        {
            return Interact();
        }

        public bool Hover(bool Ignored)
        {
            return Hover();
        }

        public bool Interact()
        {
            if (!Hover())
                return false;
            GClass55.smethod_23(true);
            Thread.Sleep(371);
            return true;
        }

        public bool Select()
        {
            if (!Hover())
                return false;
            GClass55.smethod_23(false);
            Thread.Sleep(271);
            return true;
        }

        protected bool TryPaw(double ZAdjust)
        {
            double double_1;
            double double_2;
            if (GClass6.smethod_0(Location, ZAdjust, out double_1, out double_2))
            {
                GClass55.smethod_18(double_1, double_2);
                Thread.Sleep(PawSpeedMS);
                if (IsCursorOnObject)
                    return true;
                GContext.Main.ReleaseSpinRun();
                for (var num1 = -0.02; num1 <= 0.02; num1 += 0.01)
                for (var num2 = -0.02; num2 <= 0.02; num2 += 0.01)
                {
                    GClass55.smethod_18(double_1 + num1, double_2 + num2);
                    Thread.Sleep(PawSpeedMS);
                    if (IsCursorOnObject)
                        return true;
                }
            }

            return false;
        }

        protected virtual void LoadFields()
        {
        }

        protected virtual void SetName()
        {
        }

        protected virtual void SetTitle()
        {
        }
    }
}