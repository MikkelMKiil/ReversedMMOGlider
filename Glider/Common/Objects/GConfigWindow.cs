// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GConfigWindow
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Glider.Common.Objects
{
    public class GConfigWindow : Form
    {
        public delegate void HandleHelp();

        public delegate bool ValidateForm(GConfigWindow owner, SortedList<string, string> FieldValues);

        private const int LABEL_WIDTH = 234;
        private const int LABEL_SLACK = 6;
        private const int EDIT_WIDTH = 160;
        private const int CONTROL_WIDTH = 400;
        private const int CONTROL_HEIGHT = 24;
        private const int CONTROL_HEIGHT_PAD = 4;
        private const int START_X = 10;
        private const int START_Y = 54;
        private const int BASELINE_TWEAK = 3;
        private const int SPLIT_COLUMN_PAD = 20;
        private int Columns = 1;
        private IContainer components;
        private readonly List<GConfigField> ConfigFields;
        private HandleHelp HelpDelegate;
        private Button MyCancelButton;
        private Button MyHelpButton;
        private Button MyOKButton;
        private Label TopLabel;
        private string TopLabelText;
        private ValidateForm ValidateDelegate;

        public GConfigWindow()
        {
            ConfigFields = new List<GConfigField>();
            InitializeComponent();
            if (GContext.Main.IsGliderRunning)
                Text = GContext.Main.GetRandomString();
            else
                Text = "Glider Configuration";
        }

        private void CreateConfigControls()
        {
            var num1 = 0;
            int num2;
            if (Columns == 1)
            {
                num2 = ConfigFields.Count * 28;
            }
            else
            {
                var num3 = ConfigFields.Count / 2;
                if (ConfigFields.Count % 2 == 1)
                    ++num3;
                num2 = num3 * 28;
                num1 = 420;
            }

            GContext.Main.Debug("Extra width/height: " + num1 + "/" + num2);
            Size = new Size(Size.Width + num1, Size.Height + num2);
            MyOKButton.Location = new Point(MyOKButton.Location.X, MyOKButton.Location.Y + num2);
            MyCancelButton.Location = new Point(MyCancelButton.Location.X, MyCancelButton.Location.Y + num2);
            MyHelpButton.Location = new Point(MyHelpButton.Location.X, MyHelpButton.Location.Y + num2);
            if (num1 > 0)
            {
                TopLabel.Size = new Size(TopLabel.Size.Width + num1, TopLabel.Size.Height);
                MyOKButton.Location = new Point(MyOKButton.Location.X + num1 / 2, MyOKButton.Location.Y);
                MyCancelButton.Location = new Point(MyCancelButton.Location.X + num1 / 2, MyCancelButton.Location.Y);
                MyHelpButton.Location = new Point(MyHelpButton.Location.X + num1 / 2, MyHelpButton.Location.Y);
            }

            for (var index1 = 0; index1 < ConfigFields.Count; ++index1)
            {
                var configField = ConfigFields[index1];
                int x1;
                int y1;
                if (Columns == 1)
                {
                    x1 = 10;
                    y1 = 54 + index1 * 28;
                }
                else
                {
                    x1 = 10;
                    y1 = 54 + index1 / 2 * 28;
                    if (index1 % 2 == 1)
                        x1 += 420;
                }

                var label = new Label();
                label.Text = ConfigFields[index1].LabelText;
                label.Location = new Point(x1, y1);
                label.Size = new Size(234, 24);
                label.TextAlign = ContentAlignment.MiddleRight;
                Controls.Add(label);
                var x2 = x1 + 240;
                var y2 = y1 + 3;
                switch (configField.FieldType)
                {
                    case ConfigFieldType.CString:
                        var textBox1 = new TextBox();
                        textBox1.Location = new Point(x2, y2);
                        textBox1.Size = new Size(160, 24);
                        configField.GUIControl = textBox1;
                        textBox1.Text = configField.CurrentValue;
                        Controls.Add(textBox1);
                        break;
                    case ConfigFieldType.CInt:
                        var textBox2 = new TextBox();
                        textBox2.Location = new Point(x2, y2);
                        textBox2.Size = new Size(160, 24);
                        configField.GUIControl = textBox2;
                        textBox2.Text = configField.CurrentValue;
                        Controls.Add(textBox2);
                        break;
                    case ConfigFieldType.CBool:
                        var comboBox1 = new ComboBox();
                        comboBox1.Location = new Point(x2, y2);
                        comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                        comboBox1.Size = new Size(80, 24);
                        comboBox1.Items.Add(GContext.Main.GetLocal("ConfigWindow.True"));
                        comboBox1.Items.Add(GContext.Main.GetLocal("ConfigWindow.False"));
                        if (configField.CurrentValue == "True")
                            comboBox1.SelectedIndex = 0;
                        else
                            comboBox1.SelectedIndex = 1;
                        configField.GUIControl = comboBox1;
                        Controls.Add(comboBox1);
                        break;
                    case ConfigFieldType.CEnum:
                        var comboBox2 = new ComboBox();
                        comboBox2.Location = new Point(x2, y2);
                        comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                        comboBox2.Size = new Size(160, 24);
                        for (var index2 = 0; index2 < configField.EnumValues.Length; ++index2)
                        {
                            comboBox2.Items.Add(configField.EnumValues[index2]);
                            if (configField.EnumValues[index2].ToLower() == configField.CurrentValue.ToLower())
                                comboBox2.SelectedIndex = index2;
                        }

                        configField.GUIControl = comboBox2;
                        Controls.Add(comboBox2);
                        break;
                }
            }

            TopLabel.Text = TopLabelText;
        }

        public GConfigResult DoShow()
        {
            return DoShow(null);
        }

        public GConfigResult DoShow(IWin32Window owner)
        {
            if (owner == null && StartupClass.iwin32Window_0 != null)
                owner = StartupClass.iwin32Window_0;
            if (HelpDelegate == null)
                MyHelpButton.Enabled = false;
            return ShowDialog(owner) == DialogResult.OK ? GConfigResult.Accept : GConfigResult.Cancel;
        }

        public void Load(string XmlFilename)
        {
            LoadXml(File.ReadAllText(XmlFilename));
        }

        public void LoadXml(string XMLText)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(XMLText);
            ConfigFields.Clear();
            foreach (XmlNode selectNode in xmlDocument.SelectNodes("/GliderConfigWindow/*"))
            {
                var ConfigName = selectNode.Attributes["name"].Value.Trim();
                var DisplayName = ConfigName;
                var LabelText = selectNode.Attributes["label"].Value.Trim();
                var lower = selectNode.Attributes["type"].Value.Trim().ToLower();
                var Required = selectNode.Attributes["required"].Value.Trim().ToLower() == "true";
                ConfigFieldType FieldType;
                switch (lower)
                {
                    case "int":
                        FieldType = ConfigFieldType.CInt;
                        break;
                    case "string":
                        FieldType = ConfigFieldType.CString;
                        break;
                    case "enum":
                        FieldType = ConfigFieldType.CEnum;
                        break;
                    case "bool":
                        FieldType = ConfigFieldType.CBool;
                        break;
                    default:
                        throw new Exception("Unknown field type for \"" + ConfigName + "\": " + lower);
                }

                if (selectNode.Attributes["displayname"] != null)
                    DisplayName = selectNode.Attributes["displayname"].Value.Trim();
                var gconfigField = new GConfigField(ConfigName, DisplayName, LabelText, FieldType, Required);
                if (FieldType == ConfigFieldType.CEnum)
                {
                    var List = selectNode.Attributes["values"].Value.Trim();
                    gconfigField.SetEnumValues(List);
                }

                gconfigField.Debug();
                ConfigFields.Add(gconfigField);
            }

            var xmlNode = xmlDocument.SelectSingleNode("/GliderConfigWindow");
            TopLabelText = xmlNode.Attributes["toplabel"] == null
                ? "Configuration settings"
                : xmlNode.Attributes["toplabel"].Value.Trim();
            if (xmlNode.Attributes["columns"] != null)
            {
                Columns = int.Parse(xmlNode.Attributes["columns"].Value.Trim());
                if (Columns != 1 && Columns != 2)
                    throw new Exception("Columns attribute must be 1 or 2 for now, sorry.");
            }

            CreateConfigControls();
        }

        private void MyHelpButton_Click(object sender, EventArgs e)
        {
            if (HelpDelegate == null)
            {
                var num = (int)MessageBox.Show(this, GContext.Main.GetLocal("ConfigWindow.NoHelp"),
                    GContext.Main.GetRandomString(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                HelpDelegate();
            }
        }

        public void SetHelpHandler(HandleHelp NewGuy)
        {
            HelpDelegate = NewGuy;
        }

        private void RefreshValues()
        {
            foreach (var configField in ConfigFields)
                switch (configField.FieldType)
                {
                    case ConfigFieldType.CString:
                        configField.CurrentValue = configField.GUIControl.Text.Trim();
                        continue;
                    case ConfigFieldType.CInt:
                        configField.CurrentValue = configField.GUIControl.Text.Trim();
                        continue;
                    case ConfigFieldType.CBool:
                        configField.CurrentValue =
                            ((ListControl)configField.GUIControl).SelectedIndex == 0 ? "True" : "False";
                        continue;
                    case ConfigFieldType.CEnum:
                        configField.CurrentValue = ((ComboBox)configField.GUIControl).SelectedItem.ToString();
                        continue;
                    default:
                        continue;
                }
        }

        private void MyOKButton_Click(object sender, EventArgs e)
        {
            RefreshValues();
            foreach (var configField in ConfigFields)
                if (configField.Required &&
                    (configField.CurrentValue == "" || configField.CurrentValue.StartsWith("(")))
                {
                    var Complaint = string.Format(GContext.Main.GetLocal("ConfigWindow.RequiredField"),
                        configField.DisplayName);
                    ComplainAboutField(configField.ConfigName, Complaint);
                    return;
                }

            foreach (var configField in ConfigFields)
                if (configField.FieldType == ConfigFieldType.CInt &&
                    (!(configField.CurrentValue == "") || configField.Required) &&
                    !int.TryParse(configField.CurrentValue, out var _))
                {
                    var Complaint = !configField.Required
                        ? string.Format(GContext.Main.GetLocal("ConfigWindow.BadIntNoReq"), configField.DisplayName)
                        : string.Format(GContext.Main.GetLocal("ConfigWindow.BadIntReq"), configField.DisplayName);
                    ComplainAboutField(configField.ConfigName, Complaint);
                    return;
                }

            if (ValidateDelegate != null)
            {
                var FieldValues = new SortedList<string, string>();
                foreach (var configField in ConfigFields)
                {
                    GContext.Main.Debug("\"" + configField.ConfigName + "\" = \"" + configField.CurrentValue + "\"");
                    FieldValues.Add(configField.ConfigName, configField.CurrentValue);
                }

                if (!ValidateDelegate(this, FieldValues))
                {
                    GContext.Main.Debug("Validation delegate veto'd form submit");
                    DialogResult = DialogResult.None;
                    return;
                }
            }

            GContext.Main.Debug("Config form is valid, committing values to config");
            foreach (var configField in ConfigFields)
                GContext.Main.SetConfigValue(configField.ConfigName, configField.CurrentValue, true);
            GContext.Main.SaveConfig();
        }

        public void ComplainAboutField(string ConfigName, string Complaint)
        {
            DialogResult = DialogResult.None;
            var num = (int)MessageBox.Show(this, Complaint, GContext.Main.GetRandomString(), MessageBoxButtons.OK,
                MessageBoxIcon.Hand);
            foreach (var configField in ConfigFields)
                if (configField.ConfigName == ConfigName)
                    configField.GUIControl.Focus();
        }

        public void SetValidateHandler(ValidateForm NewGuy)
        {
            ValidateDelegate = NewGuy;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            MyOKButton = new Button();
            MyCancelButton = new Button();
            TopLabel = new Label();
            MyHelpButton = new Button();
            SuspendLayout();
            MyOKButton.DialogResult = DialogResult.OK;
            MyOKButton.Location = new Point(87, 71);
            MyOKButton.Name = "MyOKButton";
            MyOKButton.Size = new Size(79, 25);
            MyOKButton.TabIndex = 0;
            MyOKButton.Text = "OK";
            MyOKButton.UseVisualStyleBackColor = true;
            MyOKButton.Click += MyOKButton_Click;
            MyCancelButton.DialogResult = DialogResult.Cancel;
            MyCancelButton.Location = new Point(183, 71);
            MyCancelButton.Name = "MyCancelButton";
            MyCancelButton.Size = new Size(79, 25);
            MyCancelButton.TabIndex = 1;
            MyCancelButton.Text = "Cancel";
            MyCancelButton.UseVisualStyleBackColor = true;
            TopLabel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            TopLabel.ForeColor = SystemColors.Highlight;
            TopLabel.Location = new Point(72, 9);
            TopLabel.Name = "TopLabel";
            TopLabel.Size = new Size(274, 29);
            TopLabel.TabIndex = 2;
            TopLabel.Text = "label1";
            TopLabel.TextAlign = ContentAlignment.MiddleCenter;
            MyHelpButton.Location = new Point(279, 71);
            MyHelpButton.Name = "MyHelpButton";
            MyHelpButton.Size = new Size(79, 25);
            MyHelpButton.TabIndex = 3;
            MyHelpButton.Text = "Help";
            MyHelpButton.UseVisualStyleBackColor = true;
            MyHelpButton.Click += MyHelpButton_Click;
            AcceptButton = MyOKButton;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = MyCancelButton;
            ClientSize = new Size(420, 106);
            Controls.Add(MyHelpButton);
            Controls.Add(TopLabel);
            Controls.Add(MyCancelButton);
            Controls.Add(MyOKButton);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = nameof(GConfigWindow);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = nameof(GConfigWindow);
            ResumeLayout(false);
        }
    }
}