// Decompiled with JetBrains decompiler
// Type: KeyEditor
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Glider.Common.Objects;

public class KeyEditor : Form
{
    private CheckBox AutoUpdate;
    private ComboBox BarState;
    private Label EditKeyName;
    private GKey gkey_0;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private IContainer icontainer_0;
    private int int_0 = -1;
    private TextBox KeyChar;
    private ListBox KeysList;
    private ComboBox KeyType;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Button MyCancelButton;
    private Button MyHelpButton;
    private Button MySaveButton;
    private Button PickButton;
    private readonly SortedList<string, GKey> sortedList_0 = new SortedList<string, GKey>();
    private ComboBox Spell;
    private CheckBox SS_Alt;
    private CheckBox SS_Ctrl;
    private CheckBox SS_Shift;
    private string string_0;
    private string string_1;

    public KeyEditor(string string_2, string string_3)
    {
        if (StartupClass.bool_13)
            GClass42.gclass42_0.method_23();
        string_0 = string_2;
        InitializeComponent();
        GClass30.smethod_3(this, nameof(KeyEditor));
        var keyEditor = this;
        keyEditor.Text = keyEditor.Text + " " + string_3 + " [" + GProcessMemoryManipulator.smethod_0() + "]";
        foreach (var key in GClass42.gclass42_0.sortedList_0.Keys)
            if (key.StartsWith(string_2))
                KeysList.Items.Add(key);
        KeyType.Items.Add(GClass30.smethod_4("KeyType.Char"));
        KeyType.Items.Add(GClass30.smethod_4("KeyType.VChar"));
        KeyType.Items.Add(GClass30.smethod_4("KeyType.Spell"));
        KeyType.Items.Add(GClass30.smethod_4("KeyType.Item"));
        BarState.Items.Add("Any");
        BarState.Items.Add("1");
        BarState.Items.Add("2");
        BarState.Items.Add("3");
        BarState.Items.Add("4");
        BarState.Items.Add("5");
        BarState.Items.Add("6");
        KeysList.SelectedIndex = 0;
    }

    void Form.Dispose(bool disposing)
    {
        if (disposing && icontainer_0 != null)
            icontainer_0.Dispose();
        // ISSUE: explicit non-virtual call
        __nonvirtual(((Form)this).Dispose(disposing));
    }

    private void InitializeComponent()
    {
        var componentResourceManager = new ComponentResourceManager(typeof(KeyEditor));
        MySaveButton = new Button();
        MyCancelButton = new Button();
        KeysList = new ListBox();
        groupBox1 = new GroupBox();
        groupBox2 = new GroupBox();
        PickButton = new Button();
        Spell = new ComboBox();
        label5 = new Label();
        SS_Alt = new CheckBox();
        SS_Ctrl = new CheckBox();
        SS_Shift = new CheckBox();
        KeyChar = new TextBox();
        label4 = new Label();
        BarState = new ComboBox();
        label3 = new Label();
        KeyType = new ComboBox();
        label2 = new Label();
        AutoUpdate = new CheckBox();
        EditKeyName = new Label();
        label1 = new Label();
        MyHelpButton = new Button();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        SuspendLayout();
        MySaveButton.AutoSize = true;
        MySaveButton.Location = new Point(189, 275);
        MySaveButton.Name = "MySaveButton";
        MySaveButton.Size = new Size(72, 25);
        MySaveButton.TabIndex = 0;
        MySaveButton.Text = "Save";
        MySaveButton.UseVisualStyleBackColor = true;
        MySaveButton.Click += MySaveButton_Click;
        MyCancelButton.AutoSize = true;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(294, 275);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(72, 25);
        MyCancelButton.TabIndex = 1;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.UseVisualStyleBackColor = true;
        MyCancelButton.Click += MyCancelButton_Click;
        KeysList.FormattingEnabled = true;
        KeysList.Location = new Point(6, 19);
        KeysList.Name = "KeysList";
        KeysList.Size = new Size(215, 212);
        KeysList.TabIndex = 3;
        KeysList.SelectedIndexChanged += KeysList_SelectedIndexChanged;
        groupBox1.Controls.Add(KeysList);
        groupBox1.Location = new Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(227, 247);
        groupBox1.TabIndex = 5;
        groupBox1.TabStop = false;
        groupBox1.Text = "Keys";
        groupBox2.Controls.Add(PickButton);
        groupBox2.Controls.Add(Spell);
        groupBox2.Controls.Add(label5);
        groupBox2.Controls.Add(SS_Alt);
        groupBox2.Controls.Add(SS_Ctrl);
        groupBox2.Controls.Add(SS_Shift);
        groupBox2.Controls.Add(KeyChar);
        groupBox2.Controls.Add(label4);
        groupBox2.Controls.Add(BarState);
        groupBox2.Controls.Add(label3);
        groupBox2.Controls.Add(KeyType);
        groupBox2.Controls.Add(label2);
        groupBox2.Controls.Add(AutoUpdate);
        groupBox2.Controls.Add(EditKeyName);
        groupBox2.Controls.Add(label1);
        groupBox2.Location = new Point(248, 12);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(287, 247);
        groupBox2.TabIndex = 6;
        groupBox2.TabStop = false;
        groupBox2.Text = "Key Properties";
        PickButton.AutoSize = true;
        PickButton.Location = new Point(159, 123);
        PickButton.Name = "PickButton";
        PickButton.Size = new Size(72, 25);
        PickButton.TabIndex = 8;
        PickButton.Text = "Pick";
        PickButton.UseVisualStyleBackColor = true;
        PickButton.Click += PickButton_Click;
        Spell.DropDownStyle = ComboBoxStyle.DropDownList;
        Spell.FormattingEnabled = true;
        Spell.Location = new Point(82, 76);
        Spell.Name = "Spell";
        Spell.Size = new Size(194, 21);
        Spell.TabIndex = 13;
        label5.AutoSize = true;
        label5.Location = new Point(43, 79);
        label5.Name = "label5";
        label5.Size = new Size(33, 13);
        label5.TabIndex = 12;
        label5.Text = "Spell:";
        label5.TextAlign = ContentAlignment.TopRight;
        SS_Alt.AutoSize = true;
        SS_Alt.Location = new Point(193, 163);
        SS_Alt.Name = "SS_Alt";
        SS_Alt.Size = new Size(38, 17);
        SS_Alt.TabIndex = 11;
        SS_Alt.Text = "Alt";
        SS_Alt.UseVisualStyleBackColor = true;
        SS_Ctrl.AutoSize = true;
        SS_Ctrl.Location = new Point(126, 163);
        SS_Ctrl.Name = "SS_Ctrl";
        SS_Ctrl.Size = new Size(41, 17);
        SS_Ctrl.TabIndex = 10;
        SS_Ctrl.Text = "Ctrl";
        SS_Ctrl.UseVisualStyleBackColor = true;
        SS_Shift.AutoSize = true;
        SS_Shift.Location = new Point(50, 163);
        SS_Shift.Name = "SS_Shift";
        SS_Shift.Size = new Size(47, 17);
        SS_Shift.TabIndex = 9;
        SS_Shift.Text = "Shift";
        SS_Shift.UseVisualStyleBackColor = true;
        KeyChar.Location = new Point(82, 126);
        KeyChar.Name = "KeyChar";
        KeyChar.Size = new Size(64, 20);
        KeyChar.TabIndex = 8;
        label4.Location = new Point(5, 129);
        label4.Name = "label4";
        label4.Size = new Size(71, 20);
        label4.TabIndex = 7;
        label4.Text = "Char:";
        label4.TextAlign = ContentAlignment.TopRight;
        BarState.DropDownStyle = ComboBoxStyle.DropDownList;
        BarState.FormattingEnabled = true;
        BarState.Location = new Point(82, 101);
        BarState.Name = "BarState";
        BarState.Size = new Size(54, 21);
        BarState.TabIndex = 6;
        label3.AutoSize = true;
        label3.Location = new Point(50, 104);
        label3.Name = "label3";
        label3.Size = new Size(26, 13);
        label3.TabIndex = 5;
        label3.Text = "Bar:";
        label3.TextAlign = ContentAlignment.TopRight;
        KeyType.DropDownStyle = ComboBoxStyle.DropDownList;
        KeyType.FormattingEnabled = true;
        KeyType.Location = new Point(82, 51);
        KeyType.Name = "KeyType";
        KeyType.Size = new Size(100, 21);
        KeyType.TabIndex = 4;
        KeyType.SelectedIndexChanged += KeyType_SelectedIndexChanged;
        label2.AutoSize = true;
        label2.Location = new Point(42, 54);
        label2.Name = "label2";
        label2.Size = new Size(34, 13);
        label2.TabIndex = 3;
        label2.Text = "Type:";
        label2.TextAlign = ContentAlignment.TopRight;
        AutoUpdate.AutoSize = true;
        AutoUpdate.Location = new Point(70, 213);
        AutoUpdate.Name = "AutoUpdate";
        AutoUpdate.Size = new Size(124, 17);
        AutoUpdate.TabIndex = 2;
        AutoUpdate.Text = "Automatic key select";
        AutoUpdate.UseVisualStyleBackColor = true;
        AutoUpdate.CheckedChanged += AutoUpdate_CheckedChanged;
        EditKeyName.AutoSize = true;
        EditKeyName.ForeColor = SystemColors.Highlight;
        EditKeyName.Location = new Point(82, 29);
        EditKeyName.Name = "EditKeyName";
        EditKeyName.Size = new Size(19, 13);
        EditKeyName.TabIndex = 1;
        EditKeyName.Text = "??";
        label1.AutoSize = true;
        label1.Location = new Point(38, 29);
        label1.Name = "label1";
        label1.Size = new Size(38, 13);
        label1.TabIndex = 0;
        label1.Text = "Name:";
        label1.TextAlign = ContentAlignment.TopRight;
        MyHelpButton.AutoSize = true;
        MyHelpButton.Location = new Point(463, 278);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(72, 25);
        MyHelpButton.TabIndex = 7;
        MyHelpButton.Text = "Help";
        MyHelpButton.UseVisualStyleBackColor = true;
        MyHelpButton.Click += MyHelpButton_Click;
        AcceptButton = MySaveButton;
        AutoScaleDimensions = new SizeF(6f, 13f);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = MyCancelButton;
        ClientSize = new Size(547, 315);
        Controls.Add(MyHelpButton);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        Controls.Add(MyCancelButton);
        Controls.Add(MySaveButton);
        FormBorderStyle = FormBorderStyle.Fixed3D;
        Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(KeyEditor);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Edit Keys:";
        groupBox1.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private void MySaveButton_Click(object sender, EventArgs e)
    {
        if (!method_2())
        {
            KeysList.SelectedIndex = int_0;
        }
        else
        {
            method_3();
            if (sortedList_0.Keys.Count > 0)
            {
                foreach (var gkey in sortedList_0.Values)
                    GClass42.gclass42_0.sortedList_0[gkey.KeyName] = gkey;
                GClass42.gclass42_0.method_14();
            }

            DialogResult = DialogResult.OK;
        }
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void KeysList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gkey_0 != null && KeysList.SelectedIndex != int_0)
        {
            if (!method_2())
            {
                KeysList.SelectedIndex = int_0;
                return;
            }

            method_3();
        }

        int_0 = KeysList.SelectedIndex;
        var key = KeysList.SelectedItem.ToString();
        if (sortedList_0.ContainsKey(key))
        {
            gkey_0 = sortedList_0[key];
        }
        else
        {
            gkey_0 = GClass42.gclass42_0.sortedList_0[key].Clone();
            sortedList_0.Add(key, gkey_0);
        }

        EditKeyName.Text = gkey_0.KeyName;
        KeyType.SelectedIndex = (int)gkey_0.KType;
        AutoUpdate.Checked = gkey_0.AutoUpdate;
        BarState.SelectedIndex = (int)(gkey_0.BarState - 2);
        if (gkey_0.KType == GKeyType.VChar)
        {
            if (gkey_0.VK != 0)
                KeyChar.Text = "0x" + gkey_0.VK.ToString("x");
            label4.Text = GClass30.smethod_4("KeyEditor.label4.Alt");
        }
        else
        {
            if (gkey_0.CharCode != char.MinValue)
                KeyChar.Text = "" + gkey_0.CharCode;
            label4.Text = GClass30.smethod_4("KeyEditor.label4");
        }

        SS_Shift.Checked = (gkey_0.ShiftState & 1) > 0;
        SS_Ctrl.Checked = (gkey_0.ShiftState & 2) > 0;
        SS_Alt.Checked = (gkey_0.ShiftState & 4) > 0;
        method_1();
        method_0();
    }

    private void method_0()
    {
        if (AutoUpdate.Checked)
        {
            SS_Shift.Enabled = false;
            SS_Ctrl.Enabled = false;
            SS_Alt.Enabled = false;
            Spell.Enabled = false;
            KeyType.Enabled = false;
            BarState.Enabled = false;
            KeyChar.Enabled = false;
            PickButton.Enabled = false;
        }
        else
        {
            SS_Shift.Enabled = true;
            SS_Ctrl.Enabled = true;
            SS_Alt.Enabled = true;
            KeyType.Enabled = true;
            if (gkey_0.KType == GKeyType.Char)
            {
                Spell.Enabled = false;
                BarState.Enabled = true;
                KeyChar.Enabled = true;
                PickButton.Enabled = false;
            }

            if (gkey_0.KType == GKeyType.VChar)
            {
                Spell.Enabled = false;
                BarState.Enabled = false;
                KeyChar.Enabled = true;
                PickButton.Enabled = true;
            }

            if (gkey_0.KType != GKeyType.ItemDefID && gkey_0.KType != GKeyType.Macro &&
                gkey_0.KType != GKeyType.SpellID)
                return;
            PickButton.Enabled = false;
            Spell.Enabled = true;
            BarState.Enabled = false;
            KeyChar.Enabled = false;
        }
    }

    private void AutoUpdate_CheckedChanged(object sender, EventArgs e)
    {
        method_0();
    }

    private void KeyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        gkey_0.KType = (GKeyType)KeyType.SelectedIndex;
        method_0();
    }

    private void method_1()
    {
        var flag = false;
        Spell.Items.Clear();
        if (GContext.Main.IsAttached)
        {
            gkey_0.FilloutKey();
            for (var SlotNumber = 1; SlotNumber <= 108; ++SlotNumber)
            {
                var gshortcut_0 = new GShortcut(SlotNumber);
                var class0 = new Class0();
                if (gshortcut_0.ShortcutValue > 0)
                {
                    class0.int_0 = gshortcut_0.SlotNumber;
                    switch (gshortcut_0.ShortcutType)
                    {
                        case GShortcutType.Spell:
                            class0.string_0 = "0x" + gshortcut_0.ShortcutValue.ToString("x") + " " +
                                              StartupClass.gclass63_0.method_11(gshortcut_0.ShortcutValue);
                            break;
                        case GShortcutType.Item:
                            class0.string_0 = "0x" + gshortcut_0.ShortcutValue.ToString("x") + " " +
                                              new GItemDefinition(gshortcut_0.ShortcutValue).Name + " (item)";
                            break;
                        case GShortcutType.Macro:
                            continue;
                    }

                    if (class0.string_0 != null)
                    {
                        Spell.Items.Add(class0);
                        if (gkey_0.MatchesShortcut(gshortcut_0))
                        {
                            flag = true;
                            Spell.SelectedIndex = Spell.Items.Count - 1;
                        }
                    }
                }
            }

            if (flag)
                return;
            var class0_1 = new Class0();
            class0_1.int_0 = -1;
            class0_1.string_0 = "0x" + gkey_0.SIM.ToString("x") + " ";
            class0_1.string_0 += StartupClass.gclass63_0.method_11(gkey_0.SIM);
            class0_1.string_0 += " (not on visible bar!)";
            Spell.Items.Add(class0_1);
            Spell.SelectedIndex = Spell.Items.Count - 1;
        }
        else
        {
            Spell.Items.Add("0x" + gkey_0.SIM.ToString("x") + " (not attached)");
            Spell.SelectedIndex = 0;
        }
    }

    private bool method_2()
    {
        return (gkey_0.KType != GKeyType.Char ||
                !method_4(KeyChar.Text.Length > 1, "KeyEditor.Error.CharLength", KeyChar)) &&
               (gkey_0.KType != GKeyType.VChar || KeyChar.Text.Length == 0 || !method_4(
                   !int.TryParse(KeyChar.Text.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture,
                       out var _),
                   "KeyEditor.Error.VCharValue", KeyChar));
    }

    private void method_3()
    {
        gkey_0.ShiftState = 0;
        if (SS_Shift.Checked)
            gkey_0.ShiftState |= 1;
        if (SS_Ctrl.Checked)
            gkey_0.ShiftState |= 2;
        if (SS_Alt.Checked)
            gkey_0.ShiftState |= 4;
        gkey_0.AutoUpdate = AutoUpdate.Checked;
        if (gkey_0.KType == GKeyType.Char)
        {
            gkey_0.CharCode = KeyChar.Text.Length != 0 ? KeyChar.Text[0] : char.MinValue;
            gkey_0.BarState = (GBarState)(BarState.SelectedIndex + 2);
        }

        if (gkey_0.KType == GKeyType.VChar)
            gkey_0.VK = KeyChar.Text.Length != 0
                ? (short)int.Parse(KeyChar.Text.Substring(2), NumberStyles.HexNumber)
                : (short)0;
        if (gkey_0.KType == GKeyType.ItemDefID || gkey_0.KType == GKeyType.Macro || gkey_0.KType == GKeyType.SpellID)
        {
            if (Spell.Items.Count == 1)
            {
                gkey_0.SIM = int.Parse(Spell.SelectedItem.ToString().Split(' ')[0].Substring(2),
                    NumberStyles.HexNumber);
            }
            else
            {
                if (Spell.SelectedIndex == -1)
                {
                    gkey_0.SIM = 0;
                    return;
                }

                if (((Class0)Spell.SelectedItem).int_0 != -1)
                {
                    var gshortcut = new GShortcut(((Class0)Spell.SelectedItem).int_0);
                    gkey_0.SIM = gshortcut.ShortcutValue;
                    switch (gshortcut.ShortcutType)
                    {
                        case GShortcutType.Spell:
                            gkey_0.KType = GKeyType.SpellID;
                            break;
                        case GShortcutType.Item:
                            gkey_0.KType = GKeyType.ItemDefID;
                            break;
                        case GShortcutType.Macro:
                            gkey_0.KType = GKeyType.Macro;
                            break;
                    }
                }
            }
        }

        if (gkey_0.AutoUpdate && GClass42.gclass42_0.sortedList_1.ContainsKey(gkey_0.KeyName))
        {
            GClass42.gclass42_0.sortedList_1[gkey_0.KeyName].CopyTo(gkey_0);
            gkey_0.AutoUpdate = true;
        }

        gkey_0.UnFill();
        gkey_0.FilloutKey();
    }

    private bool method_4(bool bool_0, string string_2, Control control_0)
    {
        if (!bool_0)
            return false;
        var num = (int)MessageBox.Show(this, GClass30.smethod_4(string_2), "Key Error [" + GProcessMemoryManipulator.smethod_0() + "]",
            MessageBoxButtons.OK, MessageBoxIcon.Hand);
        control_0?.Focus();
        return true;
    }

    private void PickButton_Click(object sender, EventArgs e)
    {
        var pickWindow = new PickWindow();
        var num = (int)pickWindow.ShowDialog(this);
        KeyChar.Text = "0x" + pickWindow.int_0.ToString("x");
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "LoadingAndSaving.html");
    }
}