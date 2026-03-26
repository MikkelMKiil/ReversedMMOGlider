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
        MessageProvider.smethod_3(this, nameof(FactionReminder));
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && container_0 != null)
            container_0.Dispose();

        base.Dispose(disposing);
    }



    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FactionReminder));
            this.DontRemindMe = new System.Windows.Forms.CheckBox();
            this.MyYesButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MyNoButton = new System.Windows.Forms.Button();
            this.MyHelpButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DontRemindMe
            // 
            this.DontRemindMe.Location = new System.Drawing.Point(128, 152);
            this.DontRemindMe.Name = "DontRemindMe";
            this.DontRemindMe.Size = new System.Drawing.Size(320, 23);
            this.DontRemindMe.TabIndex = 4;
            this.DontRemindMe.Text = "Don\'t remind me about this again.";
            // 
            // MyYesButton
            // 
            this.MyYesButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyYesButton.Location = new System.Drawing.Point(166, 199);
            this.MyYesButton.Name = "MyYesButton";
            this.MyYesButton.Size = new System.Drawing.Size(103, 35);
            this.MyYesButton.TabIndex = 5;
            this.MyYesButton.Text = "Yes";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(537, 128);
            this.label1.TabIndex = 3;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // MyNoButton
            // 
            this.MyNoButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyNoButton.Location = new System.Drawing.Point(307, 199);
            this.MyNoButton.Name = "MyNoButton";
            this.MyNoButton.Size = new System.Drawing.Size(103, 35);
            this.MyNoButton.TabIndex = 6;
            this.MyNoButton.Text = "No";
            // 
            // MyHelpButton
            // 
            this.MyHelpButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyHelpButton.Location = new System.Drawing.Point(448, 199);
            this.MyHelpButton.Name = "MyHelpButton";
            this.MyHelpButton.Size = new System.Drawing.Size(102, 35);
            this.MyHelpButton.TabIndex = 7;
            this.MyHelpButton.Text = "Help";
            // 
            // FactionReminder
            // 
            this.AcceptButton = this.MyYesButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.CancelButton = this.MyNoButton;
            this.ClientSize = new System.Drawing.Size(632, 299);
            this.Controls.Add(this.MyHelpButton);
            this.Controls.Add(this.DontRemindMe);
            this.Controls.Add(this.MyNoButton);
            this.Controls.Add(this.MyYesButton);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FactionReminder";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "No factions for this profile!";
            this.ResumeLayout(false);

    }

    private void MyYesButton_Click(object sender, EventArgs e)
    {
        if (DontRemindMe.Checked)
            ConfigManager.gclass61_0.method_0("RemindFaction", "No");
        DialogResult = DialogResult.Yes;
    }

    private void MyNoButton_Click(object sender, EventArgs e)
    {
        if (DontRemindMe.Checked)
            ConfigManager.gclass61_0.method_0("RemindFaction", "No");
        DialogResult = DialogResult.No;
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GameMemoryAccess.IsWindowVisible(this, "Glider.chm", HelpNavigator.Topic, "Profiles.html");
        DialogResult = DialogResult.No;
    }

    private void FactionReminder_Load(object sender, EventArgs e)
    {
        GameMemoryAccess.SetForegroundWindow(Handle);
    }
}
