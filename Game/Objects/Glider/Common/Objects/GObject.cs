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
        protected OffsetManager _descriptor;
        protected GLocation _location;
        protected string _name;
        protected int _tickFirstSeen;
        protected string _title;
        protected GObjectType _type;
        public uint BaseAddress;
        public int FrameNumber;
        public ulong GUID;
        private int LastUpdate;
        public object ObjectTag;
        public uint StorageAddress;
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
            this.BaseAddress = unchecked((uint)BaseAddress);
            StorageAddress = unchecked((uint)GameMemoryAccess.ReadObjectStorageAddress(BaseAddress));
            GUID = GameMemoryAccess.ReadObjectGuid(BaseAddress);
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
            GameMemoryAccess.ReadUnderCursorGuid() == GUID;

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
            return (GObjectType)GameMemoryAccess.ReadQuickObjectType(BaseAddress);
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
                Logger.smethod_1("! Refresh invoked on invalid object: " + ToString());
                return false;
            }

            try
            {
                var storageAddress = GameMemoryAccess.ReadRefreshStorageAddress(BaseAddress);
                if (storageAddress != 0)
                    StorageAddress = storageAddress;

                LoadFields();
            }
            catch (InvalidOperationException ex)
            {
                Logger.LogMessage("!! CRITICAL: Refresh memory read failed for object GUID=0x" + GUID.ToString("x") +
                                  ", BaseAddr=0x" + BaseAddress.ToString("x8") + ": " + ex);
                Cull();
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
            return _descriptor == null ? 0 : _descriptor.GetOffsetValue(FieldName);
        }

        protected int GetStorageInt(string Name)
        {
            var descriptorOffset = FindDescriptorOffset(Name);
            if (descriptorOffset == 0 && MemoryOffsetTable.Instance.HasOffset(Name))
                descriptorOffset = MemoryOffsetTable.Instance.GetIntOffset(Name);
            return descriptorOffset == 0
                ? 0
                : GameMemoryAccess.ReadStorageInt(StorageAddress, descriptorOffset, Name);
        }

        protected ulong GetStorageULong(string Name)
        {
            var descriptorOffset = FindDescriptorOffset(Name);
            if (descriptorOffset == 0 && MemoryOffsetTable.Instance.HasOffset(Name))
                descriptorOffset = MemoryOffsetTable.Instance.GetIntOffset(Name);
            return descriptorOffset == 0
                ? 0UL
                : GameMemoryAccess.ReadStorageULong(StorageAddress, descriptorOffset, Name);
        }

        protected float GetStorageFloat(string Name)
        {
            var descriptorOffset = FindDescriptorOffset(Name);
            if (descriptorOffset == 0 && MemoryOffsetTable.Instance.HasOffset(Name))
                descriptorOffset = MemoryOffsetTable.Instance.GetIntOffset(Name);
            return descriptorOffset == 0
                ? 0.0f
                : GameMemoryAccess.ReadStorageFloat(StorageAddress, descriptorOffset, Name);
        }

        protected int GetBaseInt(string OffsetName)
        {
            return GameMemoryAccess.ReadBaseInt(BaseAddress, OffsetName);
        }

        protected ulong GetBaseLong(string OffsetName)
        {
            return GameMemoryAccess.ReadBaseLong(BaseAddress, OffsetName);
        }

        protected float GetBaseFloat(string OffsetName)
        {
            return GameMemoryAccess.ReadBaseFloat(BaseAddress, OffsetName);
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
            PawSpeedMS = ConfigManager.gclass61_0.method_3("PawSpeed");
            StartupClass.gclass68_0.method_3(true);
            if (StartupClass.IsGliderInitialized)
                InputController.smethod_18(InputController.double_0, InputController.double_1);
            if ((IsCursorOnObject && !StartupClass.IsGliderInitialized) || TryPaw(0.5) || TryPaw(0.0) || TryPaw(1.0) ||
                TryPaw(-0.5) || TryPaw(1.5) || TryPaw(2.0))
                return true;
            foreach (var glocation in StartupClass.gclass33_0.list_0)
            {
                InputController.smethod_18(glocation.X, glocation.Y);
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
            InputController.smethod_23(true);
            Thread.Sleep(371);
            return true;
        }

        public bool Select()
        {
            if (!Hover())
                return false;
            InputController.smethod_23(false);
            Thread.Sleep(271);
            return true;
        }

        protected bool TryPaw(double ZAdjust)
        {
            double double_1;
            double double_2;
            if (WorldToScreenProjector.smethod_0(Location, ZAdjust, out double_1, out double_2))
            {
                InputController.smethod_18(double_1, double_2);
                Thread.Sleep(PawSpeedMS);
                if (IsCursorOnObject)
                    return true;
                GContext.Main.ReleaseSpinRun();
                for (var num1 = -0.02; num1 <= 0.02; num1 += 0.01)
                    for (var num2 = -0.02; num2 <= 0.02; num2 += 0.01)
                    {
                        InputController.smethod_18(double_1 + num1, double_2 + num2);
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
