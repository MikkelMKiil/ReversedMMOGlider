// Decompiled with JetBrains decompiler
// Type: RogueConfig
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class RogueConfig : Form
{
    private CheckBox ChaseRunners;
    private CheckBox CheapShot;
    private Container container_0;
    private TextBox EvasionCooldown;
    private TextBox EviscMultiplier;
    private HelpProvider helpProvider_0;
    private TextBox KickLife;
    private Label label1;
    private Label label10;
    private Label label11;
    private Label label12;
    private Label label13;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label9;
    private Button MyCancelButton;
    private Button MyHelpButton;
    private Button MyOKButton;
    private CheckBox PoisonMain;
    private CheckBox PoisonOff;
    private TextBox PullDistance;
    private CheckBox SaveBladeFlurry;
    private CheckBox SaveRush;
    private TextBox SinisterCost;
    private CheckBox StealthNear;
    private CheckBox UseBackstab;
    private CheckBox UseBladeFlurry;
    private CheckBox UseColdblood;
    private CheckBox UseGhostly;
    private CheckBox UseKick;
    private CheckBox UseKidneyShot;
    private CheckBox UseRiposte;
    private CheckBox UseRush;
    private CheckBox UseStealth;
    private CheckBox UseVanish;

    public RogueConfig()
    {
        InitializeComponent();
        PullDistance.Text = GClass61.gclass61_0.method_2("Rogue.PullDistance");
        EvasionCooldown.Text = GClass61.gclass61_0.method_2("Rogue.EvasionCooldown");
        EviscMultiplier.Text = GClass61.gclass61_0.method_2("Rogue.EviscMultiplier");
        SinisterCost.Text = GClass61.gclass61_0.method_2("Rogue.SinisterCost");
        UseColdblood.Checked = GClass61.gclass61_0.method_2("Rogue.UseColdblood") == "True";
        UseBackstab.Checked = GClass61.gclass61_0.method_2("Rogue.UseBackstab") == "True";
        CheapShot.Checked = GClass61.gclass61_0.method_2("Rogue.CheapShot") == "True";
        ChaseRunners.Checked = GClass61.gclass61_0.method_2("Rogue.ChaseRunners") == "True";
        UseVanish.Checked = GClass61.gclass61_0.method_2("Rogue.UseVanish") == "True";
        KickLife.Text = GClass61.gclass61_0.method_2("Rogue.KickLife");
        UseKick.Checked = GClass61.gclass61_0.method_2("Rogue.UseKick") == "True";
        UseStealth.Checked = GClass61.gclass61_0.method_2("Rogue.UseStealth") == "True";
        StealthNear.Checked = GClass61.gclass61_0.method_2("Rogue.StealthNear") == "True";
        UseRiposte.Checked = GClass61.gclass61_0.method_2("Rogue.UseRiposte") == "True";
        PoisonMain.Checked = GClass61.gclass61_0.method_2("Rogue.PoisonMain") == "True";
        PoisonOff.Checked = GClass61.gclass61_0.method_2("Rogue.PoisonOff") == "True";
        UseBladeFlurry.Checked = GClass61.gclass61_0.method_2("Rogue.UseBladeFlurry") == "True";
        SaveBladeFlurry.Checked = GClass61.gclass61_0.method_2("Rogue.SaveBladeFlurry") == "True";
        UseKidneyShot.Checked = GClass61.gclass61_0.method_2("Rogue.UseKidneyShot") == "True";
        UseRush.Checked = GClass61.gclass61_0.method_2("Rogue.UseRush") == "True";
        SaveRush.Checked = GClass61.gclass61_0.method_2("Rogue.SaveRush") == "True";
        UseGhostly.Checked = GClass61.gclass61_0.method_2("Rogue.UseGhostly") == "True";
        GClass30.smethod_3(this, "Rogue");
        GProcessMemoryManipulator.smethod_48(this);
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
        MyOKButton = new Button();
        MyCancelButton = new Button();
        label3 = new Label();
        PullDistance = new TextBox();
        label4 = new Label();
        MyHelpButton = new Button();
        helpProvider_0 = new HelpProvider();
        SinisterCost = new TextBox();
        UseColdblood = new CheckBox();
        CheapShot = new CheckBox();
        UseBackstab = new CheckBox();
        ChaseRunners = new CheckBox();
        EvasionCooldown = new TextBox();
        EviscMultiplier = new TextBox();
        UseVanish = new CheckBox();
        UseKick = new CheckBox();
        KickLife = new TextBox();
        UseStealth = new CheckBox();
        StealthNear = new CheckBox();
        UseRiposte = new CheckBox();
        PoisonMain = new CheckBox();
        PoisonOff = new CheckBox();
        UseBladeFlurry = new CheckBox();
        SaveBladeFlurry = new CheckBox();
        UseKidneyShot = new CheckBox();
        UseRush = new CheckBox();
        SaveRush = new CheckBox();
        label1 = new Label();
        label2 = new Label();
        label9 = new Label();
        label10 = new Label();
        label11 = new Label();
        label12 = new Label();
        label13 = new Label();
        UseGhostly = new CheckBox();
        SuspendLayout();
        MyOKButton.Location = new Point(80, 416);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(77, 27);
        MyOKButton.TabIndex = 21;
        MyOKButton.Text = "OK";
        MyOKButton.Click += MyOKButton_Click;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(176, 416);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(77, 27);
        MyCancelButton.TabIndex = 22;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.Click += MyCancelButton_Click;
        label3.Location = new Point(88, 8);
        label3.Name = "label3";
        label3.Size = new Size(115, 19);
        label3.TabIndex = 7;
        label3.Text = "Pull distance:";
        label3.TextAlign = ContentAlignment.MiddleRight;
        helpProvider_0.SetHelpKeyword(PullDistance, "Rogue.html#PullDistance");
        helpProvider_0.SetHelpNavigator(PullDistance, HelpNavigator.Topic);
        PullDistance.Location = new Point(216, 8);
        PullDistance.Name = "PullDistance";
        helpProvider_0.SetShowHelp(PullDistance, true);
        PullDistance.Size = new Size(68, 22);
        PullDistance.TabIndex = 0;
        PullDistance.Text = "";
        label4.Location = new Point(288, 8);
        label4.Name = "label4";
        label4.Size = new Size(67, 19);
        label4.TabIndex = 9;
        label4.Text = "yards";
        label4.TextAlign = ContentAlignment.MiddleLeft;
        MyHelpButton.Location = new Point(296, 416);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(77, 27);
        MyHelpButton.TabIndex = 23;
        MyHelpButton.Text = "Help";
        MyHelpButton.Click += MyHelpButton_Click;
        helpProvider_0.HelpNamespace = "Glider.chm";
        helpProvider_0.SetHelpKeyword(SinisterCost, "Rogue.html#SinisterCost");
        helpProvider_0.SetHelpNavigator(SinisterCost, HelpNavigator.Topic);
        SinisterCost.Location = new Point(216, 40);
        SinisterCost.Name = "SinisterCost";
        helpProvider_0.SetShowHelp(SinisterCost, true);
        SinisterCost.Size = new Size(68, 22);
        SinisterCost.TabIndex = 1;
        SinisterCost.Text = "";
        helpProvider_0.SetHelpKeyword(UseColdblood, "Rogue.html#UseColdblood");
        helpProvider_0.SetHelpNavigator(UseColdblood, HelpNavigator.Topic);
        UseColdblood.Location = new Point(48, 264);
        UseColdblood.Name = "UseColdblood";
        helpProvider_0.SetShowHelp(UseColdblood, true);
        UseColdblood.Size = new Size(154, 18);
        UseColdblood.TabIndex = 8;
        UseColdblood.Text = "Use Cold Blood";
        helpProvider_0.SetHelpKeyword(CheapShot, "Rogue.html#CheapShot");
        helpProvider_0.SetHelpNavigator(CheapShot, HelpNavigator.Topic);
        CheapShot.Location = new Point(264, 336);
        CheapShot.Name = "CheapShot";
        helpProvider_0.SetShowHelp(CheapShot, true);
        CheapShot.Size = new Size(183, 18);
        CheapShot.TabIndex = 19;
        CheapShot.Text = "Open with Cheap Shot";
        helpProvider_0.SetHelpKeyword(UseBackstab, "Rogue.html#UseBackstab");
        helpProvider_0.SetHelpNavigator(UseBackstab, HelpNavigator.Topic);
        UseBackstab.Location = new Point(264, 240);
        UseBackstab.Name = "UseBackstab";
        helpProvider_0.SetShowHelp(UseBackstab, true);
        UseBackstab.Size = new Size(154, 19);
        UseBackstab.TabIndex = 15;
        UseBackstab.Text = "Use Backstab";
        helpProvider_0.SetHelpKeyword(ChaseRunners, "Rogue.html#ChaseRunners");
        helpProvider_0.SetHelpNavigator(ChaseRunners, HelpNavigator.Topic);
        ChaseRunners.Location = new Point(264, 216);
        ChaseRunners.Name = "ChaseRunners";
        helpProvider_0.SetShowHelp(ChaseRunners, true);
        ChaseRunners.Size = new Size(160, 19);
        ChaseRunners.TabIndex = 14;
        ChaseRunners.Text = "Chase runners";
        helpProvider_0.SetHelpKeyword(EvasionCooldown, "Rogue.html#EvasionCooldown");
        helpProvider_0.SetHelpNavigator(EvasionCooldown, HelpNavigator.Topic);
        EvasionCooldown.Location = new Point(216, 104);
        EvasionCooldown.Name = "EvasionCooldown";
        helpProvider_0.SetShowHelp(EvasionCooldown, true);
        EvasionCooldown.Size = new Size(68, 22);
        EvasionCooldown.TabIndex = 3;
        EvasionCooldown.Text = "";
        helpProvider_0.SetHelpKeyword(EviscMultiplier, "Rogue.html#EviscMultiplier");
        helpProvider_0.SetHelpNavigator(EviscMultiplier, HelpNavigator.Topic);
        EviscMultiplier.Location = new Point(216, 72);
        EviscMultiplier.Name = "EviscMultiplier";
        helpProvider_0.SetShowHelp(EviscMultiplier, true);
        EviscMultiplier.Size = new Size(68, 22);
        EviscMultiplier.TabIndex = 2;
        EviscMultiplier.Text = "";
        helpProvider_0.SetHelpKeyword(UseVanish, "Rogue.html#UseVanish");
        helpProvider_0.SetHelpNavigator(UseVanish, HelpNavigator.Topic);
        UseVanish.Location = new Point(264, 264);
        UseVanish.Name = "UseVanish";
        helpProvider_0.SetShowHelp(UseVanish, true);
        UseVanish.Size = new Size(160, 18);
        UseVanish.TabIndex = 16;
        UseVanish.Text = "Use Vanish";
        helpProvider_0.SetHelpKeyword(UseKick, "Rogue.html#UseKick");
        helpProvider_0.SetHelpNavigator(UseKick, HelpNavigator.Topic);
        UseKick.Location = new Point(264, 288);
        UseKick.Name = "UseKick";
        helpProvider_0.SetShowHelp(UseKick, true);
        UseKick.Size = new Size(160, 19);
        UseKick.TabIndex = 17;
        UseKick.Text = "Use Kick";
        UseKick.CheckedChanged += UseKick_CheckedChanged;
        KickLife.Enabled = false;
        helpProvider_0.SetHelpKeyword(KickLife, "Rogue.html#KickLife");
        helpProvider_0.SetHelpNavigator(KickLife, HelpNavigator.Topic);
        KickLife.Location = new Point(216, 136);
        KickLife.Name = "KickLife";
        helpProvider_0.SetShowHelp(KickLife, true);
        KickLife.Size = new Size(68, 22);
        KickLife.TabIndex = 4;
        KickLife.Text = "";
        helpProvider_0.SetHelpKeyword(UseStealth, "Rogue.html#UseStealth");
        helpProvider_0.SetHelpNavigator(UseStealth, HelpNavigator.Topic);
        UseStealth.Location = new Point(48, 216);
        UseStealth.Name = "UseStealth";
        helpProvider_0.SetShowHelp(UseStealth, true);
        UseStealth.Size = new Size(183, 19);
        UseStealth.TabIndex = 6;
        UseStealth.Text = "Use Stealth";
        UseStealth.CheckedChanged += UseStealth_CheckedChanged;
        StealthNear.Enabled = false;
        helpProvider_0.SetHelpKeyword(StealthNear, "Rogue.html#StealthNear");
        helpProvider_0.SetHelpNavigator(StealthNear, HelpNavigator.Topic);
        StealthNear.Location = new Point(48, 240);
        StealthNear.Name = "StealthNear";
        helpProvider_0.SetShowHelp(StealthNear, true);
        StealthNear.Size = new Size(183, 18);
        StealthNear.TabIndex = 7;
        StealthNear.Text = "Stealth only on pull";
        helpProvider_0.SetHelpKeyword(UseRiposte, "Rogue.html#UseRiposte");
        helpProvider_0.SetHelpNavigator(UseRiposte, HelpNavigator.Topic);
        UseRiposte.Location = new Point(264, 312);
        UseRiposte.Name = "UseRiposte";
        helpProvider_0.SetShowHelp(UseRiposte, true);
        UseRiposte.Size = new Size(160, 19);
        UseRiposte.TabIndex = 18;
        UseRiposte.Text = "Use Riposte";
        helpProvider_0.SetHelpKeyword(PoisonMain, "Rogue.html#Poison");
        helpProvider_0.SetHelpNavigator(PoisonMain, HelpNavigator.Topic);
        PoisonMain.Location = new Point(48, 192);
        PoisonMain.Name = "PoisonMain";
        helpProvider_0.SetShowHelp(PoisonMain, true);
        PoisonMain.Size = new Size(183, 19);
        PoisonMain.TabIndex = 5;
        PoisonMain.Text = "Poison main hand";
        helpProvider_0.SetHelpKeyword(PoisonOff, "Rogue.html#Poison");
        helpProvider_0.SetHelpNavigator(PoisonOff, HelpNavigator.Topic);
        PoisonOff.Location = new Point(264, 192);
        PoisonOff.Name = "PoisonOff";
        helpProvider_0.SetShowHelp(PoisonOff, true);
        PoisonOff.Size = new Size(160, 19);
        PoisonOff.TabIndex = 13;
        PoisonOff.Text = "Poison off hand";
        helpProvider_0.SetHelpKeyword(UseBladeFlurry, "Rogue.html#BladeFlurry");
        helpProvider_0.SetHelpNavigator(UseBladeFlurry, HelpNavigator.Topic);
        UseBladeFlurry.Location = new Point(48, 288);
        UseBladeFlurry.Name = "UseBladeFlurry";
        helpProvider_0.SetShowHelp(UseBladeFlurry, true);
        UseBladeFlurry.Size = new Size(183, 18);
        UseBladeFlurry.TabIndex = 9;
        UseBladeFlurry.Text = "Use Blade Flurry";
        UseBladeFlurry.CheckedChanged += UseBladeFlurry_CheckedChanged;
        SaveBladeFlurry.Enabled = false;
        helpProvider_0.SetHelpKeyword(SaveBladeFlurry, "Rogue.html#BladeFlurry");
        helpProvider_0.SetHelpNavigator(SaveBladeFlurry, HelpNavigator.Topic);
        SaveBladeFlurry.Location = new Point(48, 312);
        SaveBladeFlurry.Name = "SaveBladeFlurry";
        helpProvider_0.SetShowHelp(SaveBladeFlurry, true);
        SaveBladeFlurry.Size = new Size(183, 18);
        SaveBladeFlurry.TabIndex = 10;
        SaveBladeFlurry.Text = "Save Flurry for adds";
        helpProvider_0.SetHelpKeyword(UseKidneyShot, "Rogue.html#UseKidneyShot");
        helpProvider_0.SetHelpNavigator(UseKidneyShot, HelpNavigator.Topic);
        UseKidneyShot.Location = new Point(264, 360);
        UseKidneyShot.Name = "UseKidneyShot";
        helpProvider_0.SetShowHelp(UseKidneyShot, true);
        UseKidneyShot.Size = new Size(160, 19);
        UseKidneyShot.TabIndex = 20;
        UseKidneyShot.Text = "Use Kidney Shot";
        helpProvider_0.SetHelpKeyword(UseRush, "Rogue.html#Rush");
        helpProvider_0.SetHelpNavigator(UseRush, HelpNavigator.Topic);
        UseRush.Location = new Point(48, 336);
        UseRush.Name = "UseRush";
        helpProvider_0.SetShowHelp(UseRush, true);
        UseRush.Size = new Size(183, 18);
        UseRush.TabIndex = 11;
        UseRush.Text = "Use Adrenaline Rush";
        UseRush.CheckedChanged += UseRush_CheckedChanged;
        SaveRush.Enabled = false;
        helpProvider_0.SetHelpKeyword(SaveRush, "Rogue.html#Rush");
        helpProvider_0.SetHelpNavigator(SaveRush, HelpNavigator.Topic);
        SaveRush.Location = new Point(48, 360);
        SaveRush.Name = "SaveRush";
        helpProvider_0.SetShowHelp(SaveRush, true);
        SaveRush.Size = new Size(183, 18);
        SaveRush.TabIndex = 12;
        SaveRush.Text = "Save Rush for adds";
        label1.Location = new Point(288, 40);
        label1.Name = "label1";
        label1.Size = new Size(67, 18);
        label1.TabIndex = 14;
        label1.Text = "energy";
        label1.TextAlign = ContentAlignment.MiddleLeft;
        label2.Location = new Point(40, 40);
        label2.Name = "label2";
        label2.Size = new Size(163, 18);
        label2.TabIndex = 12;
        label2.Text = "Sinister strike cost:";
        label2.TextAlign = ContentAlignment.MiddleRight;
        label9.Location = new Point(40, 104);
        label9.Name = "label9";
        label9.Size = new Size(163, 19);
        label9.TabIndex = 21;
        label9.Text = "Evasion cooldown:";
        label9.TextAlign = ContentAlignment.MiddleRight;
        label10.Location = new Point(288, 104);
        label10.Name = "label10";
        label10.Size = new Size(67, 19);
        label10.TabIndex = 22;
        label10.Text = "seconds";
        label10.TextAlign = ContentAlignment.MiddleLeft;
        label11.Location = new Point(40, 72);
        label11.Name = "label11";
        label11.Size = new Size(163, 18);
        label11.TabIndex = 23;
        label11.Text = "Eviscerate multiplier:";
        label11.TextAlign = ContentAlignment.MiddleRight;
        label12.Location = new Point(56, 136);
        label12.Name = "label12";
        label12.Size = new Size(154, 19);
        label12.TabIndex = 25;
        label12.Text = "Kick below:";
        label12.TextAlign = ContentAlignment.MiddleRight;
        label13.Location = new Point(296, 136);
        label13.Name = "label13";
        label13.Size = new Size(67, 19);
        label13.TabIndex = 27;
        label13.Text = "life";
        label13.TextAlign = ContentAlignment.MiddleLeft;
        helpProvider_0.SetHelpKeyword(UseGhostly, "Rogue.html#UseGhostly");
        helpProvider_0.SetHelpNavigator(UseGhostly, HelpNavigator.Topic);
        UseGhostly.Location = new Point(48, 384);
        UseGhostly.Name = "UseGhostly";
        helpProvider_0.SetShowHelp(UseGhostly, true);
        UseGhostly.Size = new Size(183, 18);
        UseGhostly.TabIndex = 28;
        UseGhostly.Text = "Use Ghostly Strike";
        AcceptButton = MyOKButton;
        AutoScaleBaseSize = new Size(6, 15);
        CancelButton = MyCancelButton;
        ClientSize = new Size(466, 457);
        Controls.Add(UseGhostly);
        Controls.Add(SaveRush);
        Controls.Add(UseRush);
        Controls.Add(UseKidneyShot);
        Controls.Add(SaveBladeFlurry);
        Controls.Add(UseBladeFlurry);
        Controls.Add(PoisonOff);
        Controls.Add(PoisonMain);
        Controls.Add(UseRiposte);
        Controls.Add(StealthNear);
        Controls.Add(UseStealth);
        Controls.Add(label13);
        Controls.Add(KickLife);
        Controls.Add(EviscMultiplier);
        Controls.Add(EvasionCooldown);
        Controls.Add(SinisterCost);
        Controls.Add(PullDistance);
        Controls.Add(label12);
        Controls.Add(UseKick);
        Controls.Add(UseVanish);
        Controls.Add(label11);
        Controls.Add(label10);
        Controls.Add(label9);
        Controls.Add(ChaseRunners);
        Controls.Add(UseBackstab);
        Controls.Add(CheapShot);
        Controls.Add(label1);
        Controls.Add(label2);
        Controls.Add(UseColdblood);
        Controls.Add(MyHelpButton);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(MyCancelButton);
        Controls.Add(MyOKButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(RogueConfig);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Config: Rogue";
        ResumeLayout(false);
    }

    private void MyOKButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        if (StartupClass.smethod_19(PullDistance.Text))
            GClass61.gclass61_0.method_0("Rogue.PullDistance", PullDistance.Text);
        if (StartupClass.smethod_19(EvasionCooldown.Text))
            GClass61.gclass61_0.method_0("Rogue.EvasionCooldown", EvasionCooldown.Text);
        if (StartupClass.smethod_19(SinisterCost.Text))
            GClass61.gclass61_0.method_0("Rogue.SinisterCost", SinisterCost.Text);
        if (StartupClass.smethod_19(EviscMultiplier.Text))
            GClass61.gclass61_0.method_0("Rogue.EviscMultiplier", EviscMultiplier.Text);
        if (StartupClass.smethod_19(KickLife.Text))
            GClass61.gclass61_0.method_0("Rogue.KickLife", KickLife.Text);
        GClass61.gclass61_0.method_0("Rogue.UseColdblood", UseColdblood.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.UseBackstab", UseBackstab.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.CheapShot", CheapShot.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.ChaseRunners", ChaseRunners.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.UseVanish", UseVanish.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.UseKick", UseKick.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.UseStealth", UseStealth.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.StealthNear", StealthNear.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.UseRiposte", UseRiposte.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.PoisonMain", PoisonMain.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.PoisonOff", PoisonOff.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.UseBladeFlurry", UseBladeFlurry.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.SaveBladeFlurry", SaveBladeFlurry.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.UseKidneyShot", UseKidneyShot.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.UseRush", UseRush.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.SaveRush", SaveRush.Checked.ToString());
        GClass61.gclass61_0.method_0("Rogue.UseGhostly", UseGhostly.Checked.ToString());
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "Rogue.html");
    }

    private void UseKick_CheckedChanged(object sender, EventArgs e)
    {
        KickLife.Enabled = UseKick.Checked;
    }

    private void UseStealth_CheckedChanged(object sender, EventArgs e)
    {
        StealthNear.Enabled = UseStealth.Checked;
    }

    private void UseBladeFlurry_CheckedChanged(object sender, EventArgs e)
    {
        SaveBladeFlurry.Enabled = UseBladeFlurry.Checked;
    }

    private void UseRush_CheckedChanged(object sender, EventArgs e)
    {
        SaveRush.Enabled = UseRush.Checked;
    }
}