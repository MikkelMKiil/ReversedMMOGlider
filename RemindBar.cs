// Decompiled with JetBrains decompiler
// Type: RemindBar
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class RemindBar : Form
{
    private Container container_0;
    private CheckBox DontRemindMe;
    private Label label1;
    private Button MyNoButton;
    private Button MyYesButton;

    public RemindBar()
    {
        InitializeComponent();
        GClass30.smethod_3(this, nameof(RemindBar));
    }

    void Form.Dispose(bool disposing)
    {
        if (disposing && container_0 != null)
            container_0.Dispose();
        // ISSUE: explicit non-virtual call
        __nonvirtual(((Form)this).Dispose(disposing));
    }

    private void InitializeComponent()
    {
        label1 = new Label();
        MyYesButton = new Button();
        MyNoButton = new Button();
        DontRemindMe = new CheckBox();
        SuspendLayout();
        label1.Location = new Point(10, 9);
        label1.Name = "label1";
        label1.Size = new Size(403, 74);
        label1.TabIndex = 0;
        label1.Text =
            "In order for Glider to run properly, you need to set up two action bars in the game with spells and abilities for your character.\r\n\r\nDo you want to open the help file for this class' action bar now?";
        MyYesButton.DialogResult = DialogResult.Cancel;
        MyYesButton.Location = new Point(115, 120);
        MyYesButton.Name = "MyYesButton";
        MyYesButton.Size = new Size(77, 28);
        MyYesButton.TabIndex = 1;
        MyYesButton.Text = "Yes";
        MyYesButton.Click += MyYesButton_Click;
        MyNoButton.DialogResult = DialogResult.Cancel;
        MyNoButton.Location = new Point(221, 120);
        MyNoButton.Name = "MyNoButton";
        MyNoButton.Size = new Size(77, 28);
        MyNoButton.TabIndex = 2;
        MyNoButton.Text = "No";
        MyNoButton.Click += MyNoButton_Click;
        DontRemindMe.Location = new Point(96, 83);
        DontRemindMe.Name = "DontRemindMe";
        DontRemindMe.Size = new Size(240, 19);
        DontRemindMe.TabIndex = 0;
        DontRemindMe.Text = "Don't remind me about this again.";
        AcceptButton = MyYesButton;
        AutoScaleBaseSize = new Size(6, 15);
        CancelButton = MyNoButton;
        ClientSize = new Size(412, 163);
        Controls.Add(DontRemindMe);
        Controls.Add(MyNoButton);
        Controls.Add(MyYesButton);
        Controls.Add(label1);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(RemindBar);
        ShowInTaskbar = false;
        SizeGripStyle = SizeGripStyle.Hide;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Don't forget to set action bars!";
        ResumeLayout(false);
    }

    private void MyYesButton_Click(object sender, EventArgs e)
    {
        if (DontRemindMe.Checked)
            GClass61.gclass61_0.method_0("RemindActionBars", "No");
        DialogResult = DialogResult.Yes;
    }

    private void MyNoButton_Click(object sender, EventArgs e)
    {
        if (DontRemindMe.Checked)
            GClass61.gclass61_0.method_0("RemindActionBars", "No");
        DialogResult = DialogResult.No;
    }
}