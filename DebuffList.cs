// Decompiled with JetBrains decompiler
// Type: DebuffList
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;

public class DebuffList : Form
{
    private ListBox DebuffBox;
    private HelpProvider helpProvider_0;
    private IContainer icontainer_0;
    private Label label1;
    private Button MyHelpButton;
    private Button MyOKButton;
    public SortedList<int, string> sortedList_0;

    public DebuffList()
    {
        InitializeComponent();
        GClass30.smethod_3(this, nameof(DebuffList));
        GProcessMemoryManipulator.smethod_48(this);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && icontainer_0 != null)
            icontainer_0.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        MyOKButton = new Button();
        MyHelpButton = new Button();
        label1 = new Label();
        DebuffBox = new ListBox();
        helpProvider_0 = new HelpProvider();
        SuspendLayout();
        MyOKButton.DialogResult = DialogResult.Cancel;
        MyOKButton.Location = new Point(150, 323);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(82, 32);
        MyOKButton.TabIndex = 0;
        MyOKButton.Text = "Done";
        MyOKButton.UseVisualStyleBackColor = true;
        MyOKButton.Click += MyOKButton_Click;
        MyHelpButton.DialogResult = DialogResult.Cancel;
        MyHelpButton.Location = new Point(273, 323);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(82, 32);
        MyHelpButton.TabIndex = 1;
        MyHelpButton.Text = "Help";
        MyHelpButton.UseVisualStyleBackColor = true;
        MyHelpButton.Click += MyHelpButton_Click;
        label1.AutoSize = true;
        label1.Location = new Point(12, 9);
        label1.Name = "label1";
        label1.Size = new Size(46, 17);
        label1.TabIndex = 2;
        label1.Text = "label1";
        DebuffBox.FormattingEnabled = true;
        DebuffBox.ItemHeight = 16;
        DebuffBox.Location = new Point(15, 114);
        DebuffBox.Name = "DebuffBox";
        DebuffBox.Size = new Size(473, 196);
        DebuffBox.TabIndex = 3;
        DebuffBox.DoubleClick += DebuffBox_DoubleClick;
        helpProvider_0.HelpNamespace = "Glider.chm";
        AcceptButton = MyOKButton;
        AutoScaleDimensions = new SizeF(8f, 16f);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = MyOKButton;
        ClientSize = new Size(500, 368);
        Controls.Add(DebuffBox);
        Controls.Add(label1);
        Controls.Add(MyHelpButton);
        Controls.Add(MyOKButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(DebuffList);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Manage Debuffs";
        ResumeLayout(false);
        PerformLayout();
    }

    public void method_0()
    {
        var str1 = "NewDebuffs.xml";
        sortedList_0 = new SortedList<int, string>();
        if (!File.Exists(str1))
            return;
        try
        {
            var xmlTextReader = new XmlTextReader(str1);
            GClass37.smethod_1("Reading from xml");
            while (xmlTextReader.Read())
                if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name == "Debuff")
                {
                    var num = 0;
                    var str2 = "(unknown)";
                    while (xmlTextReader.MoveToNextAttribute())
                        switch (xmlTextReader.Name.ToUpper())
                        {
                            case "ID":
                                num = int.Parse(xmlTextReader.Value, NumberStyles.HexNumber);
                                continue;
                            case "DESCRIPTION":
                                str2 = xmlTextReader.Value;
                                continue;
                            default:
                                continue;
                        }

                    if (!sortedList_0.ContainsKey(num) && !StartupClass.DebuffsKnown_string.method_6(num))
                    {
                        GClass37.smethod_1(num.ToString("x") + " = \"" + str2 + "\"");
                        sortedList_0.Add(num, str2);
                        DebuffBox.Items.Add(str2 + " (" + num.ToString("x") + ")");
                    }
                }

            xmlTextReader.Close();
        }
        catch (Exception ex)
        {
            GClass37.smethod_0(GClass30.smethod_2(799, ex.Message, ex.StackTrace));
        }
    }

    private void DebuffBox_DoubleClick(object sender, EventArgs e)
    {
        var str = DebuffBox.Items[DebuffBox.SelectedIndex].ToString();
        var num1 = str.LastIndexOf('(');
        var num2 = str.LastIndexOf(')');
        var int_0 = int.Parse(str.Substring(num1 + 1, num2 - num1 - 1), NumberStyles.HexNumber);
        var debuffPick = new DebuffPick(str);
        if (debuffPick.ShowDialog(this) != DialogResult.OK)
            return;
        DebuffBox.Items.RemoveAt(DebuffBox.SelectedIndex);
        StartupClass.DebuffsKnown_string.method_1(int_0, str, debuffPick.genum4_0);
    }

    private void MyOKButton_Click(object sender, EventArgs e)
    {
        StartupClass.DebuffsKnown_string.method_4();
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "Debuffs.html");
    }
}