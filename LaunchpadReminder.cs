// Decompiled with JetBrains decompiler
// Type: LaunchpadReminder
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class LaunchpadReminder : Form
{
    private Container container_0;
    private CheckBox DontRemindMe;
    private Button MyOKButton;
    private Label WarningLabel;

    public LaunchpadReminder()
    {
        InitializeComponent();
        GClass30.smethod_3(this, nameof(LaunchpadReminder));
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && container_0 != null)
        {
            container_0.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        WarningLabel = new Label();
        DontRemindMe = new CheckBox();
        MyOKButton = new Button();
        SuspendLayout();
        WarningLabel.Location = new Point(8, 8);
        WarningLabel.Name = "WarningLabel";
        WarningLabel.Size = new Size(376, 48);
        WarningLabel.TabIndex = 0;
        WarningLabel.Text = "(launchpad warning)";
        DontRemindMe.Location = new Point(96, 72);
        DontRemindMe.Name = "DontRemindMe";
        DontRemindMe.Size = new Size(248, 16);
        DontRemindMe.TabIndex = 1;
        DontRemindMe.Text = "Don't remind me about this again.";
        MyOKButton.Location = new Point(136, 104);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(112, 32);
        MyOKButton.TabIndex = 2;
        MyOKButton.Text = "OK";
        MyOKButton.Click += MyOKButton_Click;
        AcceptButton = MyOKButton;
        AutoScaleBaseSize = new Size(6, 15);
        ClientSize = new Size(392, 151);
        Controls.Add(MyOKButton);
        Controls.Add(DontRemindMe);
        Controls.Add(WarningLabel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(LaunchpadReminder);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Launchpad Reminder";
        ResumeLayout(false);
    }

    private void MyOKButton_Click(object sender, EventArgs e)
    {
        if (DontRemindMe.Checked)
            GClass61.gclass61_0.method_0(nameof(LaunchpadReminder), "No");
        DialogResult = DialogResult.OK;
    }
}