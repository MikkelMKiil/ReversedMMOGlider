// Decompiled with JetBrains decompiler
// Type: PriestConfig
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class PriestConfig : Form
{
    private CheckBox AlwaysShield;
    private Container container_0;
    private CheckBox ExtraFlay;
    private CheckBox FlayRunners;
    private HelpProvider helpProvider_0;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private Label label8;
    private TextBox MindblastCooldown;
    private CheckBox MindFlay;
    private Button MyCancelButton;
    private Button MyHelpButton;
    private Button MyOKButton;
    private CheckBox PreShield;
    private TextBox PullDistance;
    private TextBox ShadowWordCooldown;
    private TextBox ShieldCooldown;
    private CheckBox SkipFlayRange;
    private CheckBox UseShadowform;
    private CheckBox UseVampiric;
    private CheckBox UseWand;

    public PriestConfig()
    {
        InitializeComponent();
        MindblastCooldown.Text = GClass61.gclass61_0.method_2("Priest.MindblastCooldown");
        ShadowWordCooldown.Text = GClass61.gclass61_0.method_2("Priest.ShadowWordCooldown");
        ShieldCooldown.Text = GClass61.gclass61_0.method_2("Priest.ShieldCooldown");
        PullDistance.Text = GClass61.gclass61_0.method_2("Priest.PullDistance");
        UseWand.Checked = GClass61.gclass61_0.method_2("Priest.UseWand") == "True";
        MindFlay.Checked = GClass61.gclass61_0.method_2("Priest.MindFlay") == "True";
        PreShield.Checked = GClass61.gclass61_0.method_2("Priest.PreShield") == "True";
        AlwaysShield.Checked = GClass61.gclass61_0.method_2("Priest.AlwaysShield") == "True";
        SkipFlayRange.Checked = GClass61.gclass61_0.method_2("Priest.SkipFlayRange") == "True";
        FlayRunners.Checked = GClass61.gclass61_0.method_2("Priest.FlayRunners") == "True";
        UseShadowform.Checked = GClass61.gclass61_0.method_2("Priest.UseShadowform") == "True";
        UseVampiric.Checked = GClass61.gclass61_0.method_2("Priest.UseVampiric") == "True";
        ExtraFlay.Checked = GClass61.gclass61_0.method_2("Priest.ExtraFlay") == "True";
        GClass30.smethod_3(this, "Priest");
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
        label1 = new Label();
        MindblastCooldown = new TextBox();
        label2 = new Label();
        UseWand = new CheckBox();
        MindFlay = new CheckBox();
        label3 = new Label();
        PullDistance = new TextBox();
        label4 = new Label();
        MyHelpButton = new Button();
        helpProvider_0 = new HelpProvider();
        ShadowWordCooldown = new TextBox();
        ShieldCooldown = new TextBox();
        PreShield = new CheckBox();
        AlwaysShield = new CheckBox();
        SkipFlayRange = new CheckBox();
        FlayRunners = new CheckBox();
        UseShadowform = new CheckBox();
        UseVampiric = new CheckBox();
        ExtraFlay = new CheckBox();
        label5 = new Label();
        label6 = new Label();
        label7 = new Label();
        label8 = new Label();
        SuspendLayout();
        MyOKButton.Location = new Point(16, 368);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(76, 28);
        MyOKButton.TabIndex = 13;
        MyOKButton.Text = "OK";
        MyOKButton.Click += MyOKButton_Click;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(112, 368);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(76, 28);
        MyCancelButton.TabIndex = 14;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.Click += MyCancelButton_Click;
        label1.Location = new Point(10, 37);
        label1.Name = "label1";
        label1.Size = new Size(134, 18);
        label1.TabIndex = 2;
        label1.Text = "Mind Blast cooldown:";
        label1.TextAlign = ContentAlignment.MiddleRight;
        helpProvider_0.SetHelpKeyword(MindblastCooldown, "Priest.html#MindBlastCooldown");
        helpProvider_0.SetHelpNavigator(MindblastCooldown, HelpNavigator.Topic);
        MindblastCooldown.Location = new Point(144, 37);
        MindblastCooldown.Name = "MindblastCooldown";
        helpProvider_0.SetShowHelp(MindblastCooldown, true);
        MindblastCooldown.Size = new Size(67, 22);
        MindblastCooldown.TabIndex = 1;
        MindblastCooldown.Text = "";
        label2.Location = new Point(221, 37);
        label2.Name = "label2";
        label2.Size = new Size(67, 18);
        label2.TabIndex = 4;
        label2.Text = "seconds";
        label2.TextAlign = ContentAlignment.MiddleLeft;
        helpProvider_0.SetHelpKeyword(UseWand, "Priest.html#UseWand");
        helpProvider_0.SetHelpNavigator(UseWand, HelpNavigator.Topic);
        UseWand.Location = new Point(48, 176);
        UseWand.Name = "UseWand";
        helpProvider_0.SetShowHelp(UseWand, true);
        UseWand.Size = new Size(211, 18);
        UseWand.TabIndex = 6;
        UseWand.Text = "Wand between Mind Blasts";
        helpProvider_0.SetHelpKeyword(MindFlay, "Priest.html#MindFlay");
        helpProvider_0.SetHelpNavigator(MindFlay, HelpNavigator.Topic);
        MindFlay.Location = new Point(48, 200);
        MindFlay.Name = "MindFlay";
        helpProvider_0.SetShowHelp(MindFlay, true);
        MindFlay.Size = new Size(192, 19);
        MindFlay.TabIndex = 7;
        MindFlay.Text = "Mind Flay on pull";
        MindFlay.CheckedChanged += MindFlay_CheckedChanged;
        label3.Location = new Point(19, 9);
        label3.Name = "label3";
        label3.Size = new Size(125, 19);
        label3.TabIndex = 7;
        label3.Text = "Pull distance:";
        label3.TextAlign = ContentAlignment.MiddleRight;
        helpProvider_0.SetHelpKeyword(PullDistance, "Priest.html#PullDistance");
        helpProvider_0.SetHelpNavigator(PullDistance, HelpNavigator.Topic);
        PullDistance.Location = new Point(144, 9);
        PullDistance.Name = "PullDistance";
        helpProvider_0.SetShowHelp(PullDistance, true);
        PullDistance.Size = new Size(67, 22);
        PullDistance.TabIndex = 0;
        PullDistance.Text = "";
        label4.Location = new Point(221, 9);
        label4.Name = "label4";
        label4.Size = new Size(67, 19);
        label4.TabIndex = 9;
        label4.Text = "yards";
        label4.TextAlign = ContentAlignment.MiddleLeft;
        MyHelpButton.Location = new Point(208, 368);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(76, 28);
        MyHelpButton.TabIndex = 15;
        MyHelpButton.Text = "Help";
        MyHelpButton.Click += MyHelpButton_Click;
        helpProvider_0.HelpNamespace = "Glider.chm";
        helpProvider_0.SetHelpKeyword(ShadowWordCooldown, "Priest.html#ShadowWordCooldown");
        helpProvider_0.SetHelpNavigator(ShadowWordCooldown, HelpNavigator.Topic);
        ShadowWordCooldown.Location = new Point(144, 65);
        ShadowWordCooldown.Name = "ShadowWordCooldown";
        helpProvider_0.SetShowHelp(ShadowWordCooldown, true);
        ShadowWordCooldown.Size = new Size(67, 22);
        ShadowWordCooldown.TabIndex = 2;
        ShadowWordCooldown.Text = "";
        helpProvider_0.SetHelpKeyword(ShieldCooldown, "Priest.html#ShieldCooldown");
        helpProvider_0.SetHelpNavigator(ShieldCooldown, HelpNavigator.Topic);
        ShieldCooldown.Location = new Point(144, 92);
        ShieldCooldown.Name = "ShieldCooldown";
        helpProvider_0.SetShowHelp(ShieldCooldown, true);
        ShieldCooldown.Size = new Size(67, 22);
        ShieldCooldown.TabIndex = 3;
        ShieldCooldown.Text = "";
        helpProvider_0.SetHelpKeyword(PreShield, "Priest.html#PreShield");
        helpProvider_0.SetHelpNavigator(PreShield, HelpNavigator.Topic);
        PreShield.Location = new Point(48, 129);
        PreShield.Name = "PreShield";
        helpProvider_0.SetShowHelp(PreShield, true);
        PreShield.Size = new Size(211, 19);
        PreShield.TabIndex = 4;
        PreShield.Text = "PW:Shield at combat start";
        PreShield.CheckedChanged += PreShield_CheckedChanged;
        AlwaysShield.Enabled = false;
        helpProvider_0.SetHelpKeyword(AlwaysShield, "Priest.html#AlwaysShield");
        helpProvider_0.SetHelpNavigator(AlwaysShield, HelpNavigator.Topic);
        AlwaysShield.Location = new Point(48, 152);
        AlwaysShield.Name = "AlwaysShield";
        helpProvider_0.SetShowHelp(AlwaysShield, true);
        AlwaysShield.Size = new Size(211, 18);
        AlwaysShield.TabIndex = 5;
        AlwaysShield.Text = "PW:Shield always on";
        SkipFlayRange.Enabled = false;
        helpProvider_0.SetHelpKeyword(SkipFlayRange, "Priest.html#MindFlay");
        helpProvider_0.SetHelpNavigator(SkipFlayRange, HelpNavigator.Topic);
        SkipFlayRange.Location = new Point(48, 224);
        SkipFlayRange.Name = "SkipFlayRange";
        helpProvider_0.SetShowHelp(SkipFlayRange, true);
        SkipFlayRange.Size = new Size(192, 18);
        SkipFlayRange.TabIndex = 8;
        SkipFlayRange.Text = "Skip flay if too far";
        helpProvider_0.SetHelpKeyword(FlayRunners, "Priest.html#FlayRunners");
        helpProvider_0.SetHelpNavigator(FlayRunners, HelpNavigator.Topic);
        FlayRunners.Location = new Point(48, 272);
        FlayRunners.Name = "FlayRunners";
        helpProvider_0.SetShowHelp(FlayRunners, true);
        FlayRunners.Size = new Size(192, 18);
        FlayRunners.TabIndex = 10;
        FlayRunners.Text = "Mind Flay runners";
        helpProvider_0.SetHelpKeyword(UseShadowform, "Priest.html#UseShadowform");
        helpProvider_0.SetHelpNavigator(UseShadowform, HelpNavigator.Topic);
        UseShadowform.Location = new Point(48, 296);
        UseShadowform.Name = "UseShadowform";
        helpProvider_0.SetShowHelp(UseShadowform, true);
        UseShadowform.Size = new Size(192, 18);
        UseShadowform.TabIndex = 11;
        UseShadowform.Text = "Use Shadowform";
        helpProvider_0.SetHelpKeyword(UseVampiric, "Priest.html#UseVampiric");
        helpProvider_0.SetHelpNavigator(UseVampiric, HelpNavigator.Topic);
        UseVampiric.Location = new Point(48, 320);
        UseVampiric.Name = "UseVampiric";
        helpProvider_0.SetShowHelp(UseVampiric, true);
        UseVampiric.Size = new Size(192, 18);
        UseVampiric.TabIndex = 12;
        UseVampiric.Text = "Use Vampiric Embrace";
        ExtraFlay.Enabled = false;
        helpProvider_0.SetHelpKeyword(ExtraFlay, "Priest.html#ExtraFlay");
        helpProvider_0.SetHelpNavigator(ExtraFlay, HelpNavigator.Topic);
        ExtraFlay.Location = new Point(48, 248);
        ExtraFlay.Name = "ExtraFlay";
        helpProvider_0.SetShowHelp(ExtraFlay, true);
        ExtraFlay.Size = new Size(192, 18);
        ExtraFlay.TabIndex = 9;
        ExtraFlay.Text = "Extra flay after DoT";
        label5.Location = new Point(10, 65);
        label5.Name = "label5";
        label5.Size = new Size(134, 18);
        label5.TabIndex = 11;
        label5.Text = "SW:Pain cooldown:";
        label5.TextAlign = ContentAlignment.MiddleRight;
        label6.Location = new Point(221, 65);
        label6.Name = "label6";
        label6.Size = new Size(67, 18);
        label6.TabIndex = 13;
        label6.Text = "seconds";
        label6.TextAlign = ContentAlignment.MiddleLeft;
        label7.Location = new Point(10, 92);
        label7.Name = "label7";
        label7.Size = new Size(134, 19);
        label7.TabIndex = 14;
        label7.Text = "PW:Shield cooldown:";
        label7.TextAlign = ContentAlignment.MiddleRight;
        label8.Location = new Point(221, 92);
        label8.Name = "label8";
        label8.Size = new Size(67, 19);
        label8.TabIndex = 16;
        label8.Text = "seconds";
        label8.TextAlign = ContentAlignment.MiddleLeft;
        AutoScaleBaseSize = new Size(6, 15);
        ClientSize = new Size(290, 409);
        Controls.Add(ExtraFlay);
        Controls.Add(UseVampiric);
        Controls.Add(UseShadowform);
        Controls.Add(FlayRunners);
        Controls.Add(SkipFlayRange);
        Controls.Add(AlwaysShield);
        Controls.Add(PreShield);
        Controls.Add(label8);
        Controls.Add(ShieldCooldown);
        Controls.Add(ShadowWordCooldown);
        Controls.Add(PullDistance);
        Controls.Add(MindblastCooldown);
        Controls.Add(label7);
        Controls.Add(label6);
        Controls.Add(label5);
        Controls.Add(MyHelpButton);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(MindFlay);
        Controls.Add(UseWand);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(MyCancelButton);
        Controls.Add(MyOKButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(PriestConfig);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Config: Priest";
        ResumeLayout(false);
    }

    private void MyOKButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        GClass61.gclass61_0.method_0("Priest.UseWand", UseWand.Checked.ToString());
        GClass61.gclass61_0.method_0("Priest.MindFlay", MindFlay.Checked.ToString());
        GClass61.gclass61_0.method_0("Priest.PreShield", PreShield.Checked.ToString());
        GClass61.gclass61_0.method_0("Priest.AlwaysShield", AlwaysShield.Checked.ToString());
        GClass61.gclass61_0.method_0("Priest.SkipFlayRange", SkipFlayRange.Checked.ToString());
        GClass61.gclass61_0.method_0("Priest.FlayRunners", FlayRunners.Checked.ToString());
        GClass61.gclass61_0.method_0("Priest.UseShadowform", UseShadowform.Checked.ToString());
        GClass61.gclass61_0.method_0("Priest.UseVampiric", UseVampiric.Checked.ToString());
        GClass61.gclass61_0.method_0("Priest.ExtraFlay", ExtraFlay.Checked.ToString());
        if (StartupClass.smethod_19(MindblastCooldown.Text))
            GClass61.gclass61_0.method_0("Priest.MindblastCooldown", MindblastCooldown.Text);
        if (StartupClass.smethod_19(ShadowWordCooldown.Text))
            GClass61.gclass61_0.method_0("Priest.ShadowWordCooldown", ShadowWordCooldown.Text);
        if (StartupClass.smethod_19(ShieldCooldown.Text))
            GClass61.gclass61_0.method_0("Priest.ShieldCooldown", ShieldCooldown.Text);
        if (!StartupClass.smethod_19(PullDistance.Text))
            return;
        GClass61.gclass61_0.method_0("Priest.PullDistance", PullDistance.Text);
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "Priest.html");
    }

    private void PreShield_CheckedChanged(object sender, EventArgs e)
    {
        AlwaysShield.Enabled = PreShield.Checked;
        ExtraFlay.Enabled = PreShield.Checked;
    }

    private void MindFlay_CheckedChanged(object sender, EventArgs e)
    {
        SkipFlayRange.Enabled = MindFlay.Checked;
    }
}