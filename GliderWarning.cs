// Decompiled with JetBrains decompiler
// Type: GliderWarning
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

public class GliderWarning : Form
{
    private IContainer icontainer_0;
    private Label LabelWarning;
    private Button MyNoButton;
    private Button MyOKButton;
    private Button MyYesButton;
    private CheckBox NoMoreWarning;

    public GliderWarning()
    {
        InitializeComponent();
    }

    public static void smethod_0(string string_0, string string_1)
    {
        if (GClass61.gclass61_0.method_2("NoWarn") == string_0)
            return;
        GClass20.smethod_2("SystemExclamation");
        var gliderWarning = new GliderWarning();
        gliderWarning.LabelWarning.Text = string_0;
        gliderWarning.method_1();
        gliderWarning.MyOKButton.Visible = false;
        gliderWarning.MyYesButton.Visible = true;
        gliderWarning.MyNoButton.Visible = true;
        var dialogResult = gliderWarning.ShowDialog();
        if (gliderWarning.NoMoreWarning.Checked)
        {
            GClass61.gclass61_0.method_0("NoWarn", string_0);
            GClass61.gclass61_0.method_8();
        }

        if (dialogResult != DialogResult.Yes)
            return;
        Process.Start(string_1);
    }

    public static void smethod_1(string string_0)
    {
        if (GClass61.gclass61_0.method_2("NoWarn") == string_0)
            return;
        GClass20.smethod_2("SystemExclamation");
        var gliderWarning = new GliderWarning();
        gliderWarning.LabelWarning.Text = string_0;
        gliderWarning.method_1();
        gliderWarning.MyOKButton.Visible = true;
        gliderWarning.MyYesButton.Visible = false;
        gliderWarning.MyNoButton.Visible = false;
        var num = (int)gliderWarning.ShowDialog();
        if (!gliderWarning.NoMoreWarning.Checked)
            return;
        GClass61.gclass61_0.method_0("NoWarn", string_0);
        GClass61.gclass61_0.method_8();
    }

    private void GliderWarning_Load(object sender, EventArgs e)
    {
        MessageProvider.smethod_3(this, nameof(GliderWarning));
        Text = GProcessMemoryManipulator.smethod_0();
    }

    private void MyYesButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Yes;
    }

    private void MyNoButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.No;
    }

    private void MyOKButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
    }

    private int method_0()
    {
        var graphics = LabelWarning.CreateGraphics();
        var sizeF = graphics.MeasureString(LabelWarning.Text, LabelWarning.Font, LabelWarning.Width);
        graphics.Dispose();
        return (int)sizeF.Height + 24;
    }

    public void method_1()
    {
        var num = method_0();
        Height += num - LabelWarning.Height;
        LabelWarning.Height = num;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && icontainer_0 != null)
        {
            icontainer_0.Dispose();
        }
        base.Dispose(disposing);
    }
    private void InitializeComponent()
    {
        MyYesButton = new Button();
        MyNoButton = new Button();
        MyOKButton = new Button();
        LabelWarning = new Label();
        NoMoreWarning = new CheckBox();
        SuspendLayout();
        MyYesButton.Anchor = AnchorStyles.Bottom;
        MyYesButton.Location = new Point(88, 94);
        MyYesButton.Margin = new Padding(2, 2, 2, 2);
        MyYesButton.Name = "MyYesButton";
        MyYesButton.Size = new Size(66, 28);
        MyYesButton.TabIndex = 0;
        MyYesButton.Text = "Yes";
        MyYesButton.UseVisualStyleBackColor = true;
        MyYesButton.Click += MyYesButton_Click;
        MyNoButton.Anchor = AnchorStyles.Bottom;
        MyNoButton.Location = new Point(193, 94);
        MyNoButton.Margin = new Padding(2, 2, 2, 2);
        MyNoButton.Name = "MyNoButton";
        MyNoButton.Size = new Size(66, 28);
        MyNoButton.TabIndex = 1;
        MyNoButton.Text = "No";
        MyNoButton.UseVisualStyleBackColor = true;
        MyNoButton.Click += MyNoButton_Click;
        MyOKButton.Anchor = AnchorStyles.Bottom;
        MyOKButton.Location = new Point(136, 94);
        MyOKButton.Margin = new Padding(2, 2, 2, 2);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(66, 28);
        MyOKButton.TabIndex = 2;
        MyOKButton.Text = "OK";
        MyOKButton.UseVisualStyleBackColor = true;
        MyOKButton.Visible = false;
        MyOKButton.Click += MyOKButton_Click;
        LabelWarning.Anchor = AnchorStyles.Top;
        LabelWarning.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelWarning.Location = new Point(9, 7);
        LabelWarning.Margin = new Padding(2, 0, 2, 0);
        LabelWarning.Name = "LabelWarning";
        LabelWarning.Size = new Size(334, 18);
        LabelWarning.TabIndex = 3;
        LabelWarning.Text = "label1";
        NoMoreWarning.Anchor = AnchorStyles.Bottom;
        NoMoreWarning.AutoSize = true;
        NoMoreWarning.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
        NoMoreWarning.Location = new Point(96, 63);
        NoMoreWarning.Margin = new Padding(2, 2, 2, 2);
        NoMoreWarning.Name = "NoMoreWarning";
        NoMoreWarning.Size = new Size(190, 19);
        NoMoreWarning.TabIndex = 5;
        NoMoreWarning.Text = "Don't show this warning again";
        NoMoreWarning.UseVisualStyleBackColor = true;
        AutoScaleDimensions = new SizeF(6f, 13f);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(352, 148);
        ControlBox = false;
        Controls.Add(NoMoreWarning);
        Controls.Add(LabelWarning);
        Controls.Add(MyOKButton);
        Controls.Add(MyNoButton);
        Controls.Add(MyYesButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Margin = new Padding(2, 2, 2, 2);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(GliderWarning);
        StartPosition = FormStartPosition.CenterScreen;
        Text = nameof(GliderWarning);
        Load += GliderWarning_Load;
        ResumeLayout(false);
        PerformLayout();
    }
}