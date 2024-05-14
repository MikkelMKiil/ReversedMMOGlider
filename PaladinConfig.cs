// Decompiled with JetBrains decompiler
// Type: PaladinConfig
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class PaladinConfig : Form
{
    private TextBox AvoidAddDistance;
    private CheckBox AvoidAdds;
    private Container container_0;
    private TextBox FinishJudgeLife;
    private HelpProvider helpProvider_0;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Button MyCancelButton;
    private Button MyHelpButton;
    private Button MyOKButton;
    private TextBox PullDistance;
    private CheckBox UseCrusaderStrike;
    private CheckBox UseDivineFavor;
    private CheckBox UseWrath;

    public PaladinConfig()
    {
        InitializeComponent();
        PullDistance.Text = GClass61.gclass61_0.method_2("Paladin.PullDistance");
        FinishJudgeLife.Text = ((int)(GClass61.gclass61_0.method_4("Paladin.FinishJudgeLife") * 100.0)).ToString();
        UseDivineFavor.Checked = GClass61.gclass61_0.method_2("Paladin.UseDivineFavor") == "True";
        UseWrath.Checked = GClass61.gclass61_0.method_2("Paladin.UseWrath") == "True";
        UseCrusaderStrike.Checked = GClass61.gclass61_0.method_2("Paladin.UseCrusaderStrike") == "True";
        AvoidAdds.Checked = GClass61.gclass61_0.method_2("Paladin.AvoidAdds") == "True";
        AvoidAddDistance.Text = GClass61.gclass61_0.method_2("Paladin.AvoidAddDistance");
        MessageProvider.smethod_3(this, "Paladin");
        GProcessMemoryManipulator.smethod_48(this);
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
        MyOKButton = new Button();
        MyCancelButton = new Button();
        label3 = new Label();
        PullDistance = new TextBox();
        label4 = new Label();
        MyHelpButton = new Button();
        helpProvider_0 = new HelpProvider();
        UseDivineFavor = new CheckBox();
        UseWrath = new CheckBox();
        FinishJudgeLife = new TextBox();
        UseCrusaderStrike = new CheckBox();
        AvoidAdds = new CheckBox();
        AvoidAddDistance = new TextBox();
        label1 = new Label();
        label2 = new Label();
        label5 = new Label();
        label6 = new Label();
        SuspendLayout();
        MyOKButton.Location = new Point(26, 229);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(64, 23);
        MyOKButton.TabIndex = 5;
        MyOKButton.Text = "OK";
        MyOKButton.Click += MyOKButton_Click;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(106, 229);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(64, 23);
        MyCancelButton.TabIndex = 6;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.Click += MyCancelButton_Click;
        label3.AutoSize = true;
        label3.Location = new Point(51, 14);
        label3.Name = "label3";
        label3.Size = new Size(70, 13);
        label3.TabIndex = 7;
        label3.Text = "Pull distance:";
        label3.TextAlign = ContentAlignment.TopRight;
        helpProvider_0.SetHelpKeyword(PullDistance, "Paladin.html#PullDistance");
        helpProvider_0.SetHelpNavigator(PullDistance, HelpNavigator.Topic);
        PullDistance.Location = new Point(130, 11);
        PullDistance.Name = "PullDistance";
        helpProvider_0.SetShowHelp(PullDistance, true);
        PullDistance.Size = new Size(40, 20);
        PullDistance.TabIndex = 0;
        label4.AutoSize = true;
        label4.Location = new Point(175, 14);
        label4.Name = "label4";
        label4.Size = new Size(32, 13);
        label4.TabIndex = 9;
        label4.Text = "yards";
        MyHelpButton.Location = new Point(186, 229);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(64, 23);
        MyHelpButton.TabIndex = 7;
        MyHelpButton.Text = "Help";
        MyHelpButton.Click += MyHelpButton_Click;
        helpProvider_0.HelpNamespace = "Glider.chm";
        UseDivineFavor.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseDivineFavor, "Paladin.html#UseDivineFavor");
        helpProvider_0.SetHelpNavigator(UseDivineFavor, HelpNavigator.Topic);
        UseDivineFavor.Location = new Point(87, 142);
        UseDivineFavor.Name = "UseDivineFavor";
        helpProvider_0.SetShowHelp(UseDivineFavor, true);
        UseDivineFavor.Size = new Size(108, 17);
        UseDivineFavor.TabIndex = 2;
        UseDivineFavor.Text = "Use Divine Favor";
        UseWrath.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseWrath, "Paladin.html#UseWrath");
        helpProvider_0.SetHelpNavigator(UseWrath, HelpNavigator.Topic);
        UseWrath.Location = new Point(87, 164);
        UseWrath.Name = "UseWrath";
        helpProvider_0.SetShowHelp(UseWrath, true);
        UseWrath.Size = new Size(131, 17);
        UseWrath.TabIndex = 4;
        UseWrath.Text = "Use Hammer of Wrath";
        helpProvider_0.SetHelpKeyword(FinishJudgeLife, "Paladin.html#PullDistance");
        helpProvider_0.SetHelpNavigator(FinishJudgeLife, HelpNavigator.Topic);
        FinishJudgeLife.Location = new Point(130, 42);
        FinishJudgeLife.Name = "FinishJudgeLife";
        helpProvider_0.SetShowHelp(FinishJudgeLife, true);
        FinishJudgeLife.Size = new Size(40, 20);
        FinishJudgeLife.TabIndex = 11;
        UseCrusaderStrike.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseCrusaderStrike, "Paladin.html#UseCrusader");
        helpProvider_0.SetHelpNavigator(UseCrusaderStrike, HelpNavigator.Topic);
        UseCrusaderStrike.Location = new Point(87, 187);
        UseCrusaderStrike.Name = "UseCrusaderStrike";
        helpProvider_0.SetShowHelp(UseCrusaderStrike, true);
        UseCrusaderStrike.Size = new Size(120, 17);
        UseCrusaderStrike.TabIndex = 13;
        UseCrusaderStrike.Text = "Use Crusader Strike";
        AvoidAdds.AutoSize = true;
        helpProvider_0.SetHelpKeyword(AvoidAdds, "Paladin.html#AvoidAdds");
        helpProvider_0.SetHelpNavigator(AvoidAdds, HelpNavigator.Topic);
        AvoidAdds.Location = new Point(87, 119);
        AvoidAdds.Name = "AvoidAdds";
        helpProvider_0.SetShowHelp(AvoidAdds, true);
        AvoidAdds.Size = new Size(79, 17);
        AvoidAdds.TabIndex = 14;
        AvoidAdds.Text = "Avoid adds";
        AvoidAdds.CheckedChanged += AvoidAdds_CheckedChanged;
        AvoidAddDistance.Enabled = false;
        helpProvider_0.SetHelpKeyword(AvoidAddDistance, "Paladin.html#AvoidAdds");
        helpProvider_0.SetHelpNavigator(AvoidAddDistance, HelpNavigator.Topic);
        AvoidAddDistance.Location = new Point(130, 71);
        AvoidAddDistance.Name = "AvoidAddDistance";
        helpProvider_0.SetShowHelp(AvoidAddDistance, true);
        AvoidAddDistance.Size = new Size(40, 20);
        AvoidAddDistance.TabIndex = 16;
        label1.AutoSize = true;
        label1.Location = new Point(43, 44);
        label1.Name = "label1";
        label1.Size = new Size(79, 13);
        label1.TabIndex = 10;
        label1.Text = "Finish w/judge:";
        label1.TextAlign = ContentAlignment.TopRight;
        label2.AutoSize = true;
        label2.Location = new Point(175, 44);
        label2.Name = "label2";
        label2.Size = new Size(59, 13);
        label2.TabIndex = 12;
        label2.Text = "percent life";
        label5.AutoSize = true;
        label5.Location = new Point(20, 75);
        label5.Name = "label5";
        label5.Size = new Size(101, 13);
        label5.TabIndex = 15;
        label5.Text = "Avoid add distance:";
        label5.TextAlign = ContentAlignment.TopRight;
        label6.AutoSize = true;
        label6.Location = new Point(175, 75);
        label6.Name = "label6";
        label6.Size = new Size(32, 13);
        label6.TabIndex = 17;
        label6.Text = "yards";
        AcceptButton = MyOKButton;
        AutoScaleBaseSize = new Size(5, 13);
        CancelButton = MyCancelButton;
        ClientSize = new Size(302, 277);
        Controls.Add(label6);
        Controls.Add(AvoidAddDistance);
        Controls.Add(label5);
        Controls.Add(AvoidAdds);
        Controls.Add(UseCrusaderStrike);
        Controls.Add(FinishJudgeLife);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(UseWrath);
        Controls.Add(UseDivineFavor);
        Controls.Add(PullDistance);
        Controls.Add(MyHelpButton);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(MyCancelButton);
        Controls.Add(MyOKButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(PaladinConfig);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Config: Paladin";
        ResumeLayout(false);
        PerformLayout();
    }

    private void MyOKButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        if (StartupClass.smethod_19(PullDistance.Text))
            GClass61.gclass61_0.method_0("Paladin.PullDistance", PullDistance.Text);
        if (StartupClass.smethod_19(FinishJudgeLife.Text))
            GClass61.gclass61_0.method_0("Paladin.FinishJudgeLife",
                (double.Parse(FinishJudgeLife.Text) / 100.0).ToString());
        if (StartupClass.smethod_19(AvoidAddDistance.Text))
            GClass61.gclass61_0.method_0("Paladin.AvoidAddDistance", AvoidAddDistance.Text);
        GClass61.gclass61_0.method_0("Paladin.UseDivineFavor", UseDivineFavor.Checked.ToString());
        GClass61.gclass61_0.method_0("Paladin.UseWrath", UseWrath.Checked.ToString());
        GClass61.gclass61_0.method_0("Paladin.UseCrusaderStrike", UseCrusaderStrike.Checked.ToString());
        GClass61.gclass61_0.method_0("Paladin.AvoidAdds", AvoidAdds.Checked.ToString());
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "Paladin.html");
    }

    private void AvoidAdds_CheckedChanged(object sender, EventArgs e)
    {
        AvoidAddDistance.Enabled = AvoidAdds.Checked;
    }
}