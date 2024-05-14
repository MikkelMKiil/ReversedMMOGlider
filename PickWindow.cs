// Decompiled with JetBrains decompiler
// Type: PickWindow
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class PickWindow : Form
{
    private IContainer icontainer_0;
    public int int_0;
    private Label label1;

    public PickWindow()
    {
        InitializeComponent();
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
        label1 = new Label();
        SuspendLayout();
        label1.ForeColor = Color.LightBlue;
        label1.Location = new Point(12, 9);
        label1.Name = "label1";
        label1.Size = new Size(350, 42);
        label1.TabIndex = 0;
        label1.Text = "Press the key you want to record.  Do not use Alt, Shift, or Control!";
        AutoScaleDimensions = new SizeF(6f, 13f);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.DimGray;
        ClientSize = new Size(374, 60);
        ControlBox = false;
        Controls.Add(label1);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(PickWindow);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Load += PickWindow_Load;
        KeyUp += PickWindow_KeyUp;
        ResumeLayout(false);
    }

    private void PickWindow_Load(object sender, EventArgs e)
    {
        MessageProvider.smethod_3(this, nameof(PickWindow));
    }

    private void PickWindow_KeyUp(object sender, KeyEventArgs e)
    {
        int_0 = (int)e.KeyCode;
        DialogResult = DialogResult.OK;
    }
}