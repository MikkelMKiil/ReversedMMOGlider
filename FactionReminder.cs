// Decompiled with JetBrains decompiler
// Type: FactionReminder
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class FactionReminder : Form
{
    private Container container_0;
    private CheckBox DontRemindMe;
    private Label label1;
    private Button MyHelpButton;
    private Button MyNoButton;
    private Button MyYesButton;

    public FactionReminder()
    {
        InitializeComponent();
        GClass30.smethod_3(this, nameof(FactionReminder));
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && container_0 != null)
            container_0.Dispose();

        base.Dispose(disposing);
    }



    private void InitializeComponent()
    {
        DontRemindMe = new CheckBox();
        MyYesButton = new Button();
        label1 = new Label();
        MyNoButton = new Button();
        MyHelpButton = new Button();
        SuspendLayout();
        DontRemindMe.Location = new Point(80, 104);
        DontRemindMe.Name = "DontRemindMe";
        DontRemindMe.Size = new Size(200, 16);
        DontRemindMe.TabIndex = 4;
        DontRemindMe.Text = "Don't remind me about this again.";
        MyYesButton.DialogResult = DialogResult.Cancel;
        MyYesButton.Location = new Point(104, 136);
        MyYesButton.Name = "MyYesButton";
        MyYesButton.Size = new Size(64, 24);
        MyYesButton.TabIndex = 5;
        MyYesButton.Text = "Yes";
        MyYesButton.Click += MyYesButton_Click;
        label1.Location = new Point(8, 8);
        label1.Name = "label1";
        label1.Size = new Size(336, 88);
        label1.TabIndex = 3;
        label1.Text =
            "This profile is configured without a faction list, which may cause Glider to attack warlock/hunter pets and friendly totems.\r\n\r\nFor more information on factions, click \"Help\".\r\n\r\nDo you want to continue?";
        MyNoButton.DialogResult = DialogResult.Cancel;
        MyNoButton.Location = new Point(192, 136);
        MyNoButton.Name = "MyNoButton";
        MyNoButton.Size = new Size(64, 24);
        MyNoButton.TabIndex = 6;
        MyNoButton.Text = "No";
        MyNoButton.Click += MyNoButton_Click;
        MyHelpButton.DialogResult = DialogResult.Cancel;
        MyHelpButton.Location = new Point(280, 136);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(64, 24);
        MyHelpButton.TabIndex = 7;
        MyHelpButton.Text = "Help";
        MyHelpButton.Click += MyHelpButton_Click;
        AcceptButton = MyYesButton;
        AutoScaleBaseSize = new Size(5, 13);
        CancelButton = MyNoButton;
        ClientSize = new Size(352, 166);
        Controls.Add(MyHelpButton);
        Controls.Add(DontRemindMe);
        Controls.Add(MyNoButton);
        Controls.Add(MyYesButton);
        Controls.Add(label1);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(FactionReminder);
        ShowInTaskbar = false;
        SizeGripStyle = SizeGripStyle.Hide;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "No factions for this profile!";
        Load += FactionReminder_Load;
        ResumeLayout(false);
    }

    private void MyYesButton_Click(object sender, EventArgs e)
    {
        if (DontRemindMe.Checked)
            GClass61.gclass61_0.method_0("RemindFaction", "No");
        DialogResult = DialogResult.Yes;
    }

    private void MyNoButton_Click(object sender, EventArgs e)
    {
        if (DontRemindMe.Checked)
            GClass61.gclass61_0.method_0("RemindFaction", "No");
        DialogResult = DialogResult.No;
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "Profiles.html");
        DialogResult = DialogResult.No;
    }

    private void FactionReminder_Load(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.SetForegroundWindow(Handle);
    }
}