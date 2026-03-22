// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GConfigField
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Windows.Forms;

namespace Glider.Common.Objects
{
    internal class GConfigField
    {
        public string ConfigName;
        public string CurrentValue;
        public string DisplayName;
        public string[] EnumValues;
        public ConfigFieldType FieldType;
        public Control GUIControl;
        public string LabelText;
        public bool Required;

        public GConfigField(
            string ConfigName,
            string DisplayName,
            string LabelText,
            ConfigFieldType FieldType,
            bool Required)
        {
            this.ConfigName = ConfigName;
            this.DisplayName = DisplayName;
            this.LabelText = LabelText;
            this.FieldType = FieldType;
            this.Required = Required;
            EnumValues = null;
            CurrentValue = GContext.Main.GetConfigString(ConfigName);
            if (CurrentValue == null)
                CurrentValue = "";
            GUIControl = null;
        }

        public void Debug()
        {
            GContext.Main.Debug("GConfigField: ConfigName=\"" + ConfigName + "\", LabelText=\"" + LabelText +
                                "\", FieldType=" + FieldType + ", Required=" + Required);
            if (EnumValues == null)
                return;
            foreach (var enumValue in EnumValues)
                GContext.Main.Debug("Enum value=\"" + enumValue + "\"");
        }

        public void SetEnumValues(string List)
        {
            EnumValues = List.Split('/');
        }
    }
}