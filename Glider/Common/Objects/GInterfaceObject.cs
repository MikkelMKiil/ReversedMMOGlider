// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GInterfaceObject
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections.Generic;

namespace Glider.Common.Objects
{
    public class GInterfaceObject
    {
        protected GInterfaceObject _cooldown;
        protected GInterfaceObject _icon;
        protected GClass8 _inner;

        public GInterfaceObject(GClass8 InnerObject)
        {
            _inner = InnerObject;
            _cooldown = null;
            _icon = null;
        }

        public int BaseAddress => _inner.int_0;

        public bool IsReady
        {
            get
            {
                if (_cooldown == null)
                {
                    _cooldown = GContext.Main.Interface.GetByName(_inner.string_0 + "Cooldown");
                    if (_cooldown == null)
                        GClass37.smethod_1("Never found cooldown object for: \"" + _inner.string_0 + "\"");
                }

                if (_cooldown == null)
                {
                    GClass37.smethod_1("No cooldown for object: " + _inner.string_0 + ", can't check ready!");
                    return false;
                }

                return !_cooldown._inner.method_10() || _cooldown.InnerIsReady;
            }
        }

        public bool IsEnabled
        {
            get
            {
                if (_icon == null)
                {
                    var InnerObject = _inner.method_6(_inner.string_0 + "Icon");
                    if (InnerObject == null)
                        GClass37.smethod_1("Never found icon object for: \"" + _inner.string_0 + "\"");
                    _icon = new GInterfaceObject(InnerObject);
                }

                if (_icon != null)
                    return GProcessMemoryManipulator.smethod_15(_icon._inner.int_0 + GClass18.gclass18_0.method_4("UIDisabled"),
                        "UIDisabled") == 0;
                GClass37.smethod_1("No icon for object: " + _inner.string_0 + ", can't check enabled!");
                return false;
            }
        }

        private bool InnerIsReady =>
            GProcessMemoryManipulator.smethod_15(_inner.int_0 + GClass18.gclass18_0.method_4("UIReady"), "UIReady") == 0;

        public bool IsFiring =>
            GProcessMemoryManipulator.smethod_15(_inner.int_0 + GClass18.gclass18_0.method_4("UIFiring"), "UIFiring") != 0;

        public bool IsVisible => _inner.method_10();

        public string LabelText => _inner.method_3();

        public GInterfaceObject[] Children
        {
            get
            {
                var gclass8Array = _inner.method_7();
                var ginterfaceObjectList = new List<GInterfaceObject>();
                foreach (var InnerObject in gclass8Array)
                    ginterfaceObjectList.Add(new GInterfaceObject(InnerObject));
                return ginterfaceObjectList.ToArray();
            }
        }

        public bool IsEnabledInFrame =>
            GProcessMemoryManipulator.smethod_11(_inner.int_0 + GClass18.gclass18_0.method_4("MBEnabled"), "ieif") != 0;

        public override string ToString()
        {
            return "UIObject @ 0x" + _inner.int_0.ToString("x") + ": \"" + _inner.string_0 + "\", Label=\"" +
                   _inner.method_3() + "\"";
        }

        public void Hover()
        {
            _inner.method_1();
            _inner.method_15();
        }

        public void ClickMouse(bool UseRight)
        {
            GClass37.smethod_1("ClickMouse on: \"" + _inner.string_0 + "\"");
            _inner.method_1();
            _inner.method_16(UseRight);
        }

        public void BeginDrag(bool UseRight)
        {
            GClass37.smethod_1("BeginDrag on: \"" + _inner.string_0 + "\"");
            _inner.method_1();
            _inner.method_17(UseRight);
        }

        public void EndDrag(bool UseRight)
        {
            GClass37.smethod_1("EndDrag on: \"" + _inner.string_0 + "\"");
            _inner.method_1();
            _inner.method_18(UseRight);
        }

        public GInterfaceObject GetChildObject(string Name)
        {
            var InnerObject = _inner.method_6(Name);
            if (InnerObject != null)
                return new GInterfaceObject(InnerObject);
            GContext.Main.Debug("Unable to get child object \"" + Name + "\" from \"" + _inner.string_0 + "\"!");
            return null;
        }
    }
}