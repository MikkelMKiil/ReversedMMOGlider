// Decompiled with JetBrains decompiler
// Type: DruidConfig
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

public class DruidConfig : Form
{
    private CheckBox BashCasters;
    private GroupBox BearFormBox;
    private TextBox BiteMultiplier;
    private GroupBox CatFormBox;
    private TextBox ClawCost;
    private Container container_0;
    private CheckBox DetectBuffs;
    private GroupBox groupBox1;
    private HelpProvider helpProvider_0;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label7;
    private TextBox MaulCost;
    private Button MyCancelButton;
    private Button MyHelpButton;
    private Button MyOKButton;
    private TextBox PullDistance;
    private CheckBox StealthNear;
    private CheckBox UseBarkskin;
    private CheckBox UseBash;
    private CheckBox UseBearForm;
    private CheckBox UseCatForm;
    private CheckBox UseCharge;
    private CheckBox UseDemo;
    private CheckBox UseEnrage;
    private CheckBox UseFaerie;
    private CheckBox UseFury;
    private CheckBox UseMangle;
    private CheckBox UseRip;
    private CheckBox UseStarfire;
    private CheckBox UseStealth;
    private CheckBox UseSwiftness;
    private CheckBox UseSwipe;

    public DruidConfig()
    {
        InitializeComponent();
        PullDistance.Text = GClass61.gclass61_0.method_2("Druid.PullDistance");
        MaulCost.Text = GClass61.gclass61_0.method_2("Druid.MaulCost");
        ClawCost.Text = GClass61.gclass61_0.method_2("Druid.ClawCost");
        BiteMultiplier.Text = GClass61.gclass61_0.method_2("Druid.BiteMultiplier");
        UseDemo.Checked = GClass61.gclass61_0.method_2("Druid.UseDemo") == "True";
        UseFaerie.Checked = GClass61.gclass61_0.method_2("Druid.UseFaerie") == "True";
        UseBash.Checked = GClass61.gclass61_0.method_2("Druid.UseBash") == "True";
        BashCasters.Checked = GClass61.gclass61_0.method_2("Druid.BashCasters") == "True";
        UseBarkskin.Checked = GClass61.gclass61_0.method_2("Druid.UseBarkskin") == "True";
        UseMangle.Checked = GClass61.gclass61_0.method_2("Druid.UseMangle") == "True";
        UseCharge.Checked = GClass61.gclass61_0.method_2("Druid.UseCharge") == "True";
        UseSwiftness.Checked = GClass61.gclass61_0.method_2("Druid.UseSwiftness") == "True";
        UseEnrage.Checked = GClass61.gclass61_0.method_2("Druid.Enrage") == "True";
        UseRip.Checked = GClass61.gclass61_0.method_2("Druid.UseRip") == "True";
        UseFury.Checked = GClass61.gclass61_0.method_2("Druid.UseFury") == "True";
        UseStealth.Checked = GClass61.gclass61_0.method_2("Druid.UseStealth") == "True";
        StealthNear.Checked = GClass61.gclass61_0.method_2("Druid.StealthNear") == "True";
        UseSwipe.Checked = GClass61.gclass61_0.method_2("Druid.UseSwipe") == "True";
        UseStarfire.Checked = GClass61.gclass61_0.method_2("Druid.UseStarfire") == "True";
        DetectBuffs.Checked = GClass61.gclass61_0.method_2("Druid.DetectBuffs") == "True";
        if (GClass61.gclass61_0.method_2("Druid.UseForm") == "Bear")
            UseBearForm.Checked = true;
        if (GClass61.gclass61_0.method_2("Druid.UseForm") == "Cat")
            UseCatForm.Checked = true;
        GClass30.smethod_3(this, "Druid");
        GProcessMemoryManipulator.smethod_48(this);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && container_0 != null)
            container_0.Dispose();

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
        UseDemo = new CheckBox();
        MaulCost = new TextBox();
        UseFaerie = new CheckBox();
        UseBarkskin = new CheckBox();
        UseBash = new CheckBox();
        UseCharge = new CheckBox();
        UseSwiftness = new CheckBox();
        UseEnrage = new CheckBox();
        UseBearForm = new CheckBox();
        UseCatForm = new CheckBox();
        ClawCost = new TextBox();
        UseFury = new CheckBox();
        UseRip = new CheckBox();
        BiteMultiplier = new TextBox();
        UseStealth = new CheckBox();
        StealthNear = new CheckBox();
        UseSwipe = new CheckBox();
        UseStarfire = new CheckBox();
        UseMangle = new CheckBox();
        BashCasters = new CheckBox();
        DetectBuffs = new CheckBox();
        label1 = new Label();
        label2 = new Label();
        BearFormBox = new GroupBox();
        CatFormBox = new GroupBox();
        label7 = new Label();
        label5 = new Label();
        groupBox1 = new GroupBox();
        BearFormBox.SuspendLayout();
        CatFormBox.SuspendLayout();
        groupBox1.SuspendLayout();
        SuspendLayout();
        MyOKButton.Location = new Point(183, 449);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(64, 24);
        MyOKButton.TabIndex = 8;
        MyOKButton.Text = "OK";
        MyOKButton.Click += MyOKButton_Click;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(263, 449);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(64, 24);
        MyCancelButton.TabIndex = 9;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.Click += MyCancelButton_Click;
        label3.Location = new Point(5, 16);
        label3.Name = "label3";
        label3.Size = new Size(sbyte.MaxValue, 16);
        label3.TabIndex = 7;
        label3.Text = "Pull distance:";
        label3.TextAlign = ContentAlignment.MiddleRight;
        helpProvider_0.SetHelpKeyword(PullDistance, "Druid.html#PullDistance");
        helpProvider_0.SetHelpNavigator(PullDistance, HelpNavigator.Topic);
        PullDistance.Location = new Point(136, 14);
        PullDistance.Name = "PullDistance";
        helpProvider_0.SetShowHelp(PullDistance, true);
        PullDistance.Size = new Size(56, 20);
        PullDistance.TabIndex = 0;
        label4.Location = new Point(197, 16);
        label4.Name = "label4";
        label4.Size = new Size(55, 16);
        label4.TabIndex = 9;
        label4.Text = "yards";
        label4.TextAlign = ContentAlignment.MiddleLeft;
        MyHelpButton.Location = new Point(351, 449);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(63, 24);
        MyHelpButton.TabIndex = 10;
        MyHelpButton.Text = "Help";
        MyHelpButton.Click += MyHelpButton_Click;
        helpProvider_0.HelpNamespace = "Glider.chm";
        UseDemo.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseDemo, "Druid.html#UseDemo");
        helpProvider_0.SetHelpNavigator(UseDemo, HelpNavigator.Topic);
        UseDemo.Location = new Point(27, 104);
        UseDemo.Name = "UseDemo";
        helpProvider_0.SetShowHelp(UseDemo, true);
        UseDemo.Size = new Size(134, 17);
        UseDemo.TabIndex = 2;
        UseDemo.Text = "Use Demoralizing Roar";
        helpProvider_0.SetHelpKeyword(MaulCost, "Druid.html#MaulCost");
        helpProvider_0.SetHelpNavigator(MaulCost, HelpNavigator.Topic);
        MaulCost.Location = new Point(93, 69);
        MaulCost.Name = "MaulCost";
        helpProvider_0.SetShowHelp(MaulCost, true);
        MaulCost.Size = new Size(34, 20);
        MaulCost.TabIndex = 1;
        UseFaerie.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseFaerie, "Druid.html#UseFaerieFire");
        helpProvider_0.SetHelpNavigator(UseFaerie, HelpNavigator.Topic);
        UseFaerie.Location = new Point(27, 46);
        UseFaerie.Name = "UseFaerie";
        helpProvider_0.SetShowHelp(UseFaerie, true);
        UseFaerie.Size = new Size(97, 17);
        UseFaerie.TabIndex = 1;
        UseFaerie.Text = "Use Faerie Fire";
        UseBarkskin.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseBarkskin, "Druid.html#UseBarkskin");
        helpProvider_0.SetHelpNavigator(UseBarkskin, HelpNavigator.Topic);
        UseBarkskin.Location = new Point(256, 69);
        UseBarkskin.Name = "UseBarkskin";
        helpProvider_0.SetShowHelp(UseBarkskin, true);
        UseBarkskin.Size = new Size(89, 17);
        UseBarkskin.TabIndex = 2;
        UseBarkskin.Text = "Use Barkskin";
        UseBash.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseBash, "Druid.html#UseBash");
        helpProvider_0.SetHelpNavigator(UseBash, HelpNavigator.Topic);
        UseBash.Location = new Point(27, 147);
        UseBash.Name = "UseBash";
        helpProvider_0.SetShowHelp(UseBash, true);
        UseBash.Size = new Size(72, 17);
        UseBash.TabIndex = 4;
        UseBash.Text = "Use Bash";
        UseBash.CheckedChanged += UseBash_CheckedChanged;
        UseCharge.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseCharge, "Druid.html#UseFeralCharge");
        helpProvider_0.SetHelpNavigator(UseCharge, HelpNavigator.Topic);
        UseCharge.Location = new Point(27, 191);
        UseCharge.Name = "UseCharge";
        helpProvider_0.SetShowHelp(UseCharge, true);
        UseCharge.Size = new Size(108, 17);
        UseCharge.TabIndex = 5;
        UseCharge.Text = "Use Feral Charge";
        UseSwiftness.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseSwiftness, "Druid.html#UseSwiftness");
        helpProvider_0.SetHelpNavigator(UseSwiftness, HelpNavigator.Topic);
        UseSwiftness.Location = new Point(27, 69);
        UseSwiftness.Name = "UseSwiftness";
        helpProvider_0.SetShowHelp(UseSwiftness, true);
        UseSwiftness.Size = new Size(151, 17);
        UseSwiftness.TabIndex = 3;
        UseSwiftness.Text = "Nature's Swiftness on heal";
        UseEnrage.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseEnrage, "Druid.html#Enrage");
        helpProvider_0.SetHelpNavigator(UseEnrage, HelpNavigator.Topic);
        UseEnrage.Location = new Point(27, 126);
        UseEnrage.Name = "UseEnrage";
        helpProvider_0.SetShowHelp(UseEnrage, true);
        UseEnrage.Size = new Size(82, 17);
        UseEnrage.TabIndex = 3;
        UseEnrage.Text = "Use Enrage";
        UseBearForm.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseBearForm, "Druid.html#UseBearForm");
        helpProvider_0.SetHelpNavigator(UseBearForm, HelpNavigator.Topic);
        UseBearForm.Location = new Point(20, 28);
        UseBearForm.Name = "UseBearForm";
        helpProvider_0.SetShowHelp(UseBearForm, true);
        UseBearForm.Size = new Size(96, 17);
        UseBearForm.TabIndex = 0;
        UseBearForm.Text = "Use Bear Form";
        UseBearForm.CheckedChanged += UseBearForm_CheckedChanged;
        UseCatForm.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseCatForm, "Druid.html#UseCatForm");
        helpProvider_0.SetHelpNavigator(UseCatForm, HelpNavigator.Topic);
        UseCatForm.Location = new Point(20, 28);
        UseCatForm.Name = "UseCatForm";
        helpProvider_0.SetShowHelp(UseCatForm, true);
        UseCatForm.Size = new Size(90, 17);
        UseCatForm.TabIndex = 0;
        UseCatForm.Text = "Use Cat Form";
        UseCatForm.CheckedChanged += UseCatForm_CheckedChanged;
        helpProvider_0.SetHelpKeyword(ClawCost, "Druid.html#ClawCost");
        helpProvider_0.SetHelpNavigator(ClawCost, HelpNavigator.Topic);
        ClawCost.Location = new Point(sbyte.MaxValue, 55);
        ClawCost.Name = "ClawCost";
        helpProvider_0.SetShowHelp(ClawCost, true);
        ClawCost.Size = new Size(40, 20);
        ClawCost.TabIndex = 1;
        UseFury.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseFury, "Druid.html#UseFury");
        helpProvider_0.SetHelpNavigator(UseFury, HelpNavigator.Topic);
        UseFury.Location = new Point(33, 132);
        UseFury.Name = "UseFury";
        helpProvider_0.SetShowHelp(UseFury, true);
        UseFury.Size = new Size(102, 17);
        UseFury.TabIndex = 3;
        UseFury.Text = "Use Tiger's Fury";
        UseRip.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseRip, "Druid.html#UseRip");
        helpProvider_0.SetHelpNavigator(UseRip, HelpNavigator.Topic);
        UseRip.Location = new Point(33, 111);
        UseRip.Name = "UseRip";
        helpProvider_0.SetShowHelp(UseRip, true);
        UseRip.Size = new Size(64, 17);
        UseRip.TabIndex = 3;
        UseRip.Text = "Use Rip";
        helpProvider_0.SetHelpKeyword(BiteMultiplier, "Druid.html#ClawCost");
        helpProvider_0.SetHelpNavigator(BiteMultiplier, HelpNavigator.Topic);
        BiteMultiplier.Location = new Point(sbyte.MaxValue, 83);
        BiteMultiplier.Name = "BiteMultiplier";
        helpProvider_0.SetShowHelp(BiteMultiplier, true);
        BiteMultiplier.Size = new Size(40, 20);
        BiteMultiplier.TabIndex = 2;
        UseStealth.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseStealth, "Druid.html#UseStealth");
        helpProvider_0.SetHelpNavigator(UseStealth, HelpNavigator.Topic);
        UseStealth.Location = new Point(33, 153);
        UseStealth.Name = "UseStealth";
        helpProvider_0.SetShowHelp(UseStealth, true);
        UseStealth.Size = new Size(81, 17);
        UseStealth.TabIndex = 17;
        UseStealth.Text = "Use Stealth";
        UseStealth.CheckedChanged += UseStealth_CheckedChanged;
        StealthNear.AutoSize = true;
        helpProvider_0.SetHelpKeyword(StealthNear, "Druid.html#StealthNear");
        helpProvider_0.SetHelpNavigator(StealthNear, HelpNavigator.Topic);
        StealthNear.Location = new Point(33, 173);
        StealthNear.Name = "StealthNear";
        helpProvider_0.SetShowHelp(StealthNear, true);
        StealthNear.Size = new Size(115, 17);
        StealthNear.TabIndex = 18;
        StealthNear.Text = "Stealth only on pull";
        UseSwipe.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseSwipe, "Druid.html#UseSwipe");
        helpProvider_0.SetHelpNavigator(UseSwipe, HelpNavigator.Topic);
        UseSwipe.Location = new Point(27, 212);
        UseSwipe.Name = "UseSwipe";
        helpProvider_0.SetShowHelp(UseSwipe, true);
        UseSwipe.Size = new Size(77, 17);
        UseSwipe.TabIndex = 14;
        UseSwipe.Text = "Use Swipe";
        UseStarfire.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseStarfire, "Druid.html#UseStarfire");
        helpProvider_0.SetHelpNavigator(UseStarfire, HelpNavigator.Topic);
        UseStarfire.Location = new Point(27, 234);
        UseStarfire.Name = "UseStarfire";
        helpProvider_0.SetShowHelp(UseStarfire, true);
        UseStarfire.Size = new Size(81, 17);
        UseStarfire.TabIndex = 15;
        UseStarfire.Text = "Use Starfire";
        UseMangle.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseMangle, "Druid.html#UseMangle");
        helpProvider_0.SetHelpNavigator(UseMangle, HelpNavigator.Topic);
        UseMangle.Location = new Point(27, 256);
        UseMangle.Name = "UseMangle";
        helpProvider_0.SetShowHelp(UseMangle, true);
        UseMangle.Size = new Size(83, 17);
        UseMangle.TabIndex = 11;
        UseMangle.Text = "Use Mangle";
        BashCasters.AutoSize = true;
        helpProvider_0.SetHelpKeyword(BashCasters, "Druid.html#BashCasters");
        helpProvider_0.SetHelpNavigator(BashCasters, HelpNavigator.Topic);
        BashCasters.Location = new Point(27, 169);
        BashCasters.Name = "BashCasters";
        helpProvider_0.SetShowHelp(BashCasters, true);
        BashCasters.Size = new Size(155, 17);
        BashCasters.TabIndex = 16;
        BashCasters.Text = "Save Bash for casters/heal";
        DetectBuffs.AutoSize = true;
        helpProvider_0.SetHelpKeyword(DetectBuffs, "Druid.html#DetectBuffs");
        helpProvider_0.SetHelpNavigator(DetectBuffs, HelpNavigator.Topic);
        DetectBuffs.Location = new Point(256, 46);
        DetectBuffs.Name = "DetectBuffs";
        helpProvider_0.SetShowHelp(DetectBuffs, true);
        DetectBuffs.Size = new Size(84, 17);
        DetectBuffs.TabIndex = 10;
        DetectBuffs.Text = "Detect buffs";
        label1.Location = new Point(7, 69);
        label1.Name = "label1";
        label1.Size = new Size(81, 17);
        label1.TabIndex = 12;
        label1.Text = "Maul cost:";
        label1.TextAlign = ContentAlignment.MiddleRight;
        label2.Location = new Point(133, 69);
        label2.Name = "label2";
        label2.Size = new Size(40, 17);
        label2.TabIndex = 13;
        label2.Text = "rage";
        label2.TextAlign = ContentAlignment.MiddleLeft;
        BearFormBox.Controls.Add(UseMangle);
        BearFormBox.Controls.Add(BashCasters);
        BearFormBox.Controls.Add(UseStarfire);
        BearFormBox.Controls.Add(UseSwipe);
        BearFormBox.Controls.Add(UseBearForm);
        BearFormBox.Controls.Add(UseDemo);
        BearFormBox.Controls.Add(UseBash);
        BearFormBox.Controls.Add(UseEnrage);
        BearFormBox.Controls.Add(UseCharge);
        BearFormBox.Controls.Add(label2);
        BearFormBox.Controls.Add(MaulCost);
        BearFormBox.Controls.Add(label1);
        BearFormBox.Location = new Point(7, 140);
        BearFormBox.Name = "BearFormBox";
        BearFormBox.Size = new Size(200, 298);
        BearFormBox.TabIndex = 6;
        BearFormBox.TabStop = false;
        BearFormBox.Text = "Bear Form";
        CatFormBox.Controls.Add(StealthNear);
        CatFormBox.Controls.Add(UseStealth);
        CatFormBox.Controls.Add(BiteMultiplier);
        CatFormBox.Controls.Add(label7);
        CatFormBox.Controls.Add(UseRip);
        CatFormBox.Controls.Add(UseFury);
        CatFormBox.Controls.Add(ClawCost);
        CatFormBox.Controls.Add(label5);
        CatFormBox.Controls.Add(UseCatForm);
        CatFormBox.Location = new Point(212, 140);
        CatFormBox.Name = "CatFormBox";
        CatFormBox.Size = new Size(202, 298);
        CatFormBox.TabIndex = 7;
        CatFormBox.TabStop = false;
        CatFormBox.Text = "Cat Form";
        label7.Location = new Point(13, 83);
        label7.Name = "label7";
        label7.Size = new Size(100, 17);
        label7.TabIndex = 16;
        label7.Text = "Bite multiplier:";
        label7.TextAlign = ContentAlignment.MiddleRight;
        label5.Location = new Point(13, 55);
        label5.Name = "label5";
        label5.Size = new Size(100, 17);
        label5.TabIndex = 13;
        label5.Text = "Claw energy cost:";
        label5.TextAlign = ContentAlignment.MiddleRight;
        groupBox1.Controls.Add(DetectBuffs);
        groupBox1.Controls.Add(UseSwiftness);
        groupBox1.Controls.Add(UseBarkskin);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(UseFaerie);
        groupBox1.Controls.Add(PullDistance);
        groupBox1.Location = new Point(7, 10);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(407, 112);
        groupBox1.TabIndex = 11;
        groupBox1.TabStop = false;
        groupBox1.Text = "General";
        AcceptButton = MyOKButton;
        AutoScaleBaseSize = new Size(5, 13);
        CancelButton = MyCancelButton;
        ClientSize = new Size(440, 489);
        Controls.Add(groupBox1);
        Controls.Add(CatFormBox);
        Controls.Add(BearFormBox);
        Controls.Add(MyHelpButton);
        Controls.Add(MyCancelButton);
        Controls.Add(MyOKButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(DruidConfig);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Config: Druid";
        BearFormBox.ResumeLayout(false);
        BearFormBox.PerformLayout();
        CatFormBox.ResumeLayout(false);
        CatFormBox.PerformLayout();
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ResumeLayout(false);
    }

    private void MyOKButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        if (StartupClass.smethod_19(PullDistance.Text))
            GClass61.gclass61_0.method_0("Druid.PullDistance", PullDistance.Text);
        if (StartupClass.smethod_19(MaulCost.Text))
            GClass61.gclass61_0.method_0("Druid.MaulCost", MaulCost.Text);
        if (StartupClass.smethod_19(ClawCost.Text))
            GClass61.gclass61_0.method_0("Druid.ClawCost", ClawCost.Text);
        if (StartupClass.smethod_19(BiteMultiplier.Text))
            GClass61.gclass61_0.method_0("Druid.BiteMultiplier", BiteMultiplier.Text);
        GClass61.gclass61_0.method_0("Druid.BashCasters", BashCasters.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.UseBash", UseBash.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.UseDemo", UseDemo.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.UseFaerie", UseFaerie.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.UseBarkskin", UseBarkskin.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.UseMangle", UseMangle.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.UseCharge", UseCharge.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.UseSwiftness", UseSwiftness.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.Enrage", UseEnrage.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.UseRip", UseRip.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.UseFury", UseFury.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.UseStealth", UseStealth.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.StealthNear", StealthNear.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.UseStarfire", UseStarfire.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.UseSwipe", UseSwipe.Checked.ToString());
        GClass61.gclass61_0.method_0("Druid.DetectBuffs", DetectBuffs.Checked.ToString());
        if (UseBearForm.Checked)
            GClass61.gclass61_0.method_0("Druid.UseForm", "Bear");
        if (!UseCatForm.Checked)
            return;
        GClass61.gclass61_0.method_0("Druid.UseForm", "Cat");
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "Druid.html");
    }

    private void UseBearForm_CheckedChanged(object sender, EventArgs e)
    {
        if (!UseBearForm.Checked)
            return;
        UseCatForm.Checked = false;
        foreach (Control control in (ArrangedElementCollection)CatFormBox.Controls)
            if (control != UseCatForm)
                control.Enabled = false;
        foreach (Control control in (ArrangedElementCollection)BearFormBox.Controls)
            control.Enabled = true;
    }

    private void UseCatForm_CheckedChanged(object sender, EventArgs e)
    {
        if (!UseCatForm.Checked)
            return;
        UseBearForm.Checked = false;
        foreach (Control control in (ArrangedElementCollection)BearFormBox.Controls)
            if (control != UseBearForm)
                control.Enabled = false;
        foreach (Control control in (ArrangedElementCollection)CatFormBox.Controls)
            control.Enabled = true;
    }

    private void UseStealth_CheckedChanged(object sender, EventArgs e)
    {
        StealthNear.Enabled = UseStealth.Checked;
    }

    private void UseBash_CheckedChanged(object sender, EventArgs e)
    {
        BashCasters.Enabled = UseBash.Checked;
    }
}