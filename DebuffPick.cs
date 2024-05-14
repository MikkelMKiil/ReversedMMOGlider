// Decompiled with JetBrains decompiler
// Type: DebuffPick
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class DebuffPick : Form
{
    private Button ButtonCancel;
    private Button ButtonCurse;
    private Button ButtonDisease;
    private Button ButtonMagic;
    private Button ButtonPoison;
    public GEnum4 genum4_0;
    private IContainer icontainer_0;
    private Label label1;
    private Label label2;
    public string string_0;

    public DebuffPick(string string_1)
    {
        InitializeComponent();
        MessageProvider.smethod_3(this, nameof(DebuffPick));
        GProcessMemoryManipulator.smethod_48(this);
        string_0 = string_1;
        Text = string_1;
        label2.Text = string_1;
    }

    private void ButtonMagic_Click(object sender, EventArgs e)
    {
        genum4_0 = GEnum4.const_1;
        DialogResult = DialogResult.OK;
    }

    private void ButtonPoison_Click(object sender, EventArgs e)
    {
        genum4_0 = GEnum4.const_2;
        DialogResult = DialogResult.OK;
    }

    private void ButtonDisease_Click(object sender, EventArgs e)
    {
        genum4_0 = GEnum4.const_4;
        DialogResult = DialogResult.OK;
    }

    private void ButtonCurse_Click(object sender, EventArgs e)
    {
        genum4_0 = GEnum4.const_3;
        DialogResult = DialogResult.OK;
    }

    private void ButtonCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && icontainer_0 != null)
            icontainer_0.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        label1 = new Label();
        label2 = new Label();
        ButtonMagic = new Button();
        ButtonPoison = new Button();
        ButtonDisease = new Button();
        ButtonCurse = new Button();
        ButtonCancel = new Button();
        SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new Point(12, 9);
        label1.Name = "label1";
        label1.Size = new Size(46, 17);
        label1.TabIndex = 0;
        label1.Text = "label1";
        label2.AutoSize = true;
        label2.Location = new Point(12, 71);
        label2.Name = "label2";
        label2.Size = new Size(46, 17);
        label2.TabIndex = 1;
        label2.Text = "label2";
        ButtonMagic.Location = new Point(37, 123);
        ButtonMagic.Name = "ButtonMagic";
        ButtonMagic.Size = new Size(85, 34);
        ButtonMagic.TabIndex = 1;
        ButtonMagic.Text = "Magic";
        ButtonMagic.UseVisualStyleBackColor = true;
        ButtonMagic.Click += ButtonMagic_Click;
        ButtonPoison.Location = new Point(144, 123);
        ButtonPoison.Name = "ButtonPoison";
        ButtonPoison.Size = new Size(85, 34);
        ButtonPoison.TabIndex = 2;
        ButtonPoison.Text = "Poison";
        ButtonPoison.UseVisualStyleBackColor = true;
        ButtonPoison.Click += ButtonPoison_Click;
        ButtonDisease.Location = new Point(251, 123);
        ButtonDisease.Name = "ButtonDisease";
        ButtonDisease.Size = new Size(85, 34);
        ButtonDisease.TabIndex = 3;
        ButtonDisease.Text = "Disease";
        ButtonDisease.UseVisualStyleBackColor = true;
        ButtonDisease.Click += ButtonDisease_Click;
        ButtonCurse.Location = new Point(358, 123);
        ButtonCurse.Name = "ButtonCurse";
        ButtonCurse.Size = new Size(85, 34);
        ButtonCurse.TabIndex = 4;
        ButtonCurse.Text = "Curse";
        ButtonCurse.UseVisualStyleBackColor = true;
        ButtonCurse.Click += ButtonCurse_Click;
        ButtonCancel.Location = new Point(489, 123);
        ButtonCancel.Name = "ButtonCancel";
        ButtonCancel.Size = new Size(85, 34);
        ButtonCancel.TabIndex = 0;
        ButtonCancel.Text = "Cancel";
        ButtonCancel.UseVisualStyleBackColor = true;
        ButtonCancel.Click += ButtonCancel_Click;
        AcceptButton = ButtonCancel;
        AutoScaleDimensions = new SizeF(8f, 16f);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(587, 172);
        Controls.Add(ButtonCancel);
        Controls.Add(ButtonCurse);
        Controls.Add(ButtonDisease);
        Controls.Add(ButtonPoison);
        Controls.Add(ButtonMagic);
        Controls.Add(label2);
        Controls.Add(label1);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(DebuffPick);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = nameof(DebuffPick);
        ResumeLayout(false);
        PerformLayout();
    }
}