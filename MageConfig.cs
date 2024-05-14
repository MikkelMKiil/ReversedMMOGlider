// Decompiled with JetBrains decompiler
// Type: MageConfig
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class MageConfig : Form
{
    private CheckBox ApproachFireblast;
    private Container container_0;
    private TextBox CounterspellLife;
    private ComboBox Finisher;
    private TextBox FinishLife;
    private TextBox FireblastCooldown;
    private TextBox FireblastDistance;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private HelpProvider helpProvider_0;
    private ComboBox IceBarrier;
    private Label label1;
    private Label label10;
    private Label label11;
    private Label label12;
    private Label label13;
    private Label label14;
    private Label label15;
    private Label label16;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private Label label8;
    private Label label9;
    private TextBox MeleeSpellCooldown;
    private Button MyCancelButton;
    private Button MyHelpButton;
    private Button MyOKButton;
    private TextBox PullDistance;
    private CheckBox SaveBlast;
    private TextBox ShieldLife;
    private CheckBox UseCombustion;
    private CheckBox UseCounterspell;
    private CheckBox UseDampen;
    private CheckBox UseEvocation;
    private CheckBox UseFrostNova;
    private CheckBox UseManaStones;
    private CheckBox UseMeleeSpell;
    private CheckBox UsePoly;
    private CheckBox WaitOnPull;

    public MageConfig()
    {
        InitializeComponent();
        for (var index = 0; index < 4; ++index)
            IceBarrier.Items.Add(MessageProvider.smethod_4("Mage.IceBarrier" + index));
        for (var index = 0; index < 3; ++index)
            Finisher.Items.Add(MessageProvider.smethod_4("Mage.Finisher" + index));
        FireblastCooldown.Text = GClass61.gclass61_0.method_2("Mage.FireblastCooldownSec");
        PullDistance.Text = GClass61.gclass61_0.method_2("Mage.PullDistance");
        FinishLife.Text = GClass61.gclass61_0.method_2("Mage.FinishLife");
        FireblastDistance.Text = GClass61.gclass61_0.method_2("Mage.FireblastDistance");
        MeleeSpellCooldown.Text = GClass61.gclass61_0.method_2("Mage.MeleeSpellCooldown");
        CounterspellLife.Text = GClass61.gclass61_0.method_2("Mage.CounterspellLife");
        ShieldLife.Text = GClass61.gclass61_0.method_2("Mage.ShieldLife");
        UseManaStones.Checked = GClass61.gclass61_0.method_2("Mage.UseManaStones") == "True";
        SaveBlast.Checked = GClass61.gclass61_0.method_2("Mage.SaveBlast") == "True";
        UseFrostNova.Checked = GClass61.gclass61_0.method_2("Mage.UseFrostNova") == "True";
        UsePoly.Checked = GClass61.gclass61_0.method_2("Mage.UsePoly") == "True";
        ApproachFireblast.Checked = GClass61.gclass61_0.method_2("Mage.ApproachFireblast") == "True";
        UseCounterspell.Checked = GClass61.gclass61_0.method_2("Mage.UseCounterspell") == "True";
        UseEvocation.Checked = GClass61.gclass61_0.method_2("Mage.UseEvocation") == "True";
        UseCombustion.Checked = GClass61.gclass61_0.method_2("Mage.UseCombustion") == "True";
        UseMeleeSpell.Checked = GClass61.gclass61_0.method_2("Mage.UseMeleeSpell") == "True";
        WaitOnPull.Checked = GClass61.gclass61_0.method_2("Mage.WaitOnPull") == "True";
        UseDampen.Checked = GClass61.gclass61_0.method_2("Mage.UseDampen") == "True";
        IceBarrier.SelectedIndex = GClass61.gclass61_0.method_3("Mage.IceBarrier");
        Finisher.SelectedIndex = GClass61.gclass61_0.method_3("Mage.Finisher");
        MessageProvider.smethod_3(this, "Mage");
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
        MyHelpButton = new Button();
        helpProvider_0 = new HelpProvider();
        Finisher = new ComboBox();
        IceBarrier = new ComboBox();
        MeleeSpellCooldown = new TextBox();
        CounterspellLife = new TextBox();
        FireblastDistance = new TextBox();
        FinishLife = new TextBox();
        PullDistance = new TextBox();
        FireblastCooldown = new TextBox();
        UseDampen = new CheckBox();
        UseCombustion = new CheckBox();
        WaitOnPull = new CheckBox();
        UseCounterspell = new CheckBox();
        UseMeleeSpell = new CheckBox();
        ApproachFireblast = new CheckBox();
        UsePoly = new CheckBox();
        UseFrostNova = new CheckBox();
        SaveBlast = new CheckBox();
        UseManaStones = new CheckBox();
        UseEvocation = new CheckBox();
        groupBox1 = new GroupBox();
        groupBox2 = new GroupBox();
        label15 = new Label();
        label8 = new Label();
        ShieldLife = new TextBox();
        label2 = new Label();
        label4 = new Label();
        label13 = new Label();
        label12 = new Label();
        label7 = new Label();
        label6 = new Label();
        label5 = new Label();
        label16 = new Label();
        label14 = new Label();
        label10 = new Label();
        label11 = new Label();
        label9 = new Label();
        label3 = new Label();
        label1 = new Label();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        SuspendLayout();
        MyOKButton.Location = new Point(223, 345);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(64, 24);
        MyOKButton.TabIndex = 20;
        MyOKButton.Text = "OK";
        MyOKButton.Click += MyOKButton_Click;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(322, 345);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(64, 24);
        MyCancelButton.TabIndex = 21;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.Click += MyCancelButton_Click;
        MyHelpButton.Location = new Point(487, 345);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(65, 24);
        MyHelpButton.TabIndex = 22;
        MyHelpButton.Text = "Help";
        MyHelpButton.Click += MyHelpButton_Click;
        helpProvider_0.HelpNamespace = "Glider.chm";
        Finisher.DropDownStyle = ComboBoxStyle.DropDownList;
        Finisher.FormattingEnabled = true;
        helpProvider_0.SetHelpKeyword(Finisher, "Mage.html#Finisher");
        helpProvider_0.SetHelpNavigator(Finisher, HelpNavigator.Topic);
        helpProvider_0.SetHelpString(Finisher, "");
        Finisher.Location = new Point(153, 136);
        Finisher.Name = "Finisher";
        helpProvider_0.SetShowHelp(Finisher, true);
        Finisher.Size = new Size(101, 21);
        Finisher.TabIndex = 65;
        Finisher.SelectedIndexChanged += Finisher_SelectedIndexChanged;
        IceBarrier.DropDownStyle = ComboBoxStyle.DropDownList;
        IceBarrier.FormattingEnabled = true;
        helpProvider_0.SetHelpKeyword(IceBarrier, "Mage.html#IceBarrier");
        helpProvider_0.SetHelpNavigator(IceBarrier, HelpNavigator.Topic);
        helpProvider_0.SetHelpString(IceBarrier, "");
        IceBarrier.Location = new Point(153, 110);
        IceBarrier.Name = "IceBarrier";
        helpProvider_0.SetShowHelp(IceBarrier, true);
        IceBarrier.Size = new Size(101, 21);
        IceBarrier.TabIndex = 63;
        IceBarrier.SelectedIndexChanged += IceBarrier_SelectedIndexChanged;
        MeleeSpellCooldown.Enabled = false;
        helpProvider_0.SetHelpKeyword(MeleeSpellCooldown, "Mage.html#MeleeSpellCooldown");
        helpProvider_0.SetHelpNavigator(MeleeSpellCooldown, HelpNavigator.Topic);
        MeleeSpellCooldown.Location = new Point(153, 86);
        MeleeSpellCooldown.Name = "MeleeSpellCooldown";
        helpProvider_0.SetShowHelp(MeleeSpellCooldown, true);
        MeleeSpellCooldown.Size = new Size(48, 20);
        MeleeSpellCooldown.TabIndex = 50;
        CounterspellLife.Enabled = false;
        helpProvider_0.SetHelpKeyword(CounterspellLife, "Mage.html#CounterspellLife");
        helpProvider_0.SetHelpNavigator(CounterspellLife, HelpNavigator.Topic);
        CounterspellLife.Location = new Point(153, 186);
        CounterspellLife.Name = "CounterspellLife";
        helpProvider_0.SetShowHelp(CounterspellLife, true);
        CounterspellLife.Size = new Size(48, 20);
        CounterspellLife.TabIndex = 54;
        helpProvider_0.SetHelpKeyword(FireblastDistance, "Mage.html#FireblastDistance");
        helpProvider_0.SetHelpNavigator(FireblastDistance, HelpNavigator.Topic);
        FireblastDistance.Location = new Point(153, 37);
        FireblastDistance.Name = "FireblastDistance";
        helpProvider_0.SetShowHelp(FireblastDistance, true);
        FireblastDistance.Size = new Size(48, 20);
        FireblastDistance.TabIndex = 47;
        helpProvider_0.SetHelpKeyword(FinishLife, "Mage.html#FinishLife");
        helpProvider_0.SetHelpNavigator(FinishLife, HelpNavigator.Topic);
        FinishLife.Location = new Point(153, 162);
        FinishLife.Name = "FinishLife";
        helpProvider_0.SetShowHelp(FinishLife, true);
        FinishLife.Size = new Size(48, 20);
        FinishLife.TabIndex = 52;
        helpProvider_0.SetHelpKeyword(PullDistance, "Mage.html#PullDistance");
        helpProvider_0.SetHelpNavigator(PullDistance, HelpNavigator.Topic);
        PullDistance.Location = new Point(153, 13);
        PullDistance.Name = "PullDistance";
        helpProvider_0.SetShowHelp(PullDistance, true);
        PullDistance.Size = new Size(48, 20);
        PullDistance.TabIndex = 46;
        helpProvider_0.SetHelpKeyword(FireblastCooldown, "Mage.html#FireblastCooldown");
        helpProvider_0.SetHelpNavigator(FireblastCooldown, HelpNavigator.Topic);
        FireblastCooldown.Location = new Point(153, 62);
        FireblastCooldown.Name = "FireblastCooldown";
        helpProvider_0.SetShowHelp(FireblastCooldown, true);
        FireblastCooldown.Size = new Size(48, 20);
        FireblastCooldown.TabIndex = 49;
        UseDampen.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseDampen, "Mage.html#UseDampen");
        helpProvider_0.SetHelpNavigator(UseDampen, HelpNavigator.Topic);
        UseDampen.Location = new Point(22, 122);
        UseDampen.Name = "UseDampen";
        helpProvider_0.SetShowHelp(UseDampen, true);
        UseDampen.Size = new Size(120, 17);
        UseDampen.TabIndex = 51;
        UseDampen.Text = "Use Dampen Magic";
        UseCombustion.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseCombustion, "Mage.html#UseCombustion");
        helpProvider_0.SetHelpNavigator(UseCombustion, HelpNavigator.Topic);
        UseCombustion.Location = new Point(22, 99);
        UseCombustion.Name = "UseCombustion";
        helpProvider_0.SetShowHelp(UseCombustion, true);
        UseCombustion.Size = new Size(103, 17);
        UseCombustion.TabIndex = 50;
        UseCombustion.Text = "Use Combustion";
        WaitOnPull.AutoSize = true;
        helpProvider_0.SetHelpKeyword(WaitOnPull, "Mage.html#WaitOnPull");
        helpProvider_0.SetHelpNavigator(WaitOnPull, HelpNavigator.Topic);
        WaitOnPull.Location = new Point(22, 75);
        WaitOnPull.Name = "WaitOnPull";
        helpProvider_0.SetShowHelp(WaitOnPull, true);
        WaitOnPull.Size = new Size(145, 17);
        WaitOnPull.TabIndex = 44;
        WaitOnPull.Text = "Wait for approach on pull";
        UseCounterspell.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseCounterspell, "Mage.html#UseCounterspell");
        helpProvider_0.SetHelpNavigator(UseCounterspell, HelpNavigator.Topic);
        UseCounterspell.Location = new Point(22, 241);
        UseCounterspell.Name = "UseCounterspell";
        helpProvider_0.SetShowHelp(UseCounterspell, true);
        UseCounterspell.Size = new Size(106, 17);
        UseCounterspell.TabIndex = 49;
        UseCounterspell.Text = "Use Counterspell";
        UseCounterspell.CheckedChanged += UseCounterspell_CheckedChanged;
        UseMeleeSpell.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseMeleeSpell, "Mage.html#UseMeleeSpell");
        helpProvider_0.SetHelpNavigator(UseMeleeSpell, HelpNavigator.Topic);
        UseMeleeSpell.Location = new Point(22, 52);
        UseMeleeSpell.Name = "UseMeleeSpell";
        helpProvider_0.SetShowHelp(UseMeleeSpell, true);
        UseMeleeSpell.Size = new Size(130, 17);
        UseMeleeSpell.TabIndex = 43;
        UseMeleeSpell.Text = "Use melee-range spell";
        UseMeleeSpell.CheckedChanged += UseMeleeSpell_CheckedChanged;
        ApproachFireblast.AutoSize = true;
        helpProvider_0.SetHelpKeyword(ApproachFireblast, "Mage.html#ApproachFireblast");
        helpProvider_0.SetHelpNavigator(ApproachFireblast, HelpNavigator.Topic);
        ApproachFireblast.Location = new Point(22, 218);
        ApproachFireblast.Name = "ApproachFireblast";
        helpProvider_0.SetShowHelp(ApproachFireblast, true);
        ApproachFireblast.Size = new Size(129, 17);
        ApproachFireblast.TabIndex = 48;
        ApproachFireblast.Text = "Approach for Fireblast";
        UsePoly.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UsePoly, "Mage.html#UsePolymorph");
        helpProvider_0.SetHelpNavigator(UsePoly, HelpNavigator.Topic);
        UsePoly.Location = new Point(22, 194);
        UsePoly.Name = "UsePoly";
        helpProvider_0.SetShowHelp(UsePoly, true);
        UsePoly.Size = new Size(138, 17);
        UsePoly.TabIndex = 47;
        UsePoly.Text = "Use Polymorph on adds";
        UseFrostNova.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseFrostNova, "Mage.html#UseFrostNova");
        helpProvider_0.SetHelpNavigator(UseFrostNova, HelpNavigator.Topic);
        UseFrostNova.Location = new Point(22, 171);
        UseFrostNova.Name = "UseFrostNova";
        helpProvider_0.SetShowHelp(UseFrostNova, true);
        UseFrostNova.Size = new Size(100, 17);
        UseFrostNova.TabIndex = 46;
        UseFrostNova.Text = "Use Frost Nova";
        SaveBlast.AutoSize = true;
        helpProvider_0.SetHelpKeyword(SaveBlast, "Mage.html#SaveBlast");
        helpProvider_0.SetHelpNavigator(SaveBlast, HelpNavigator.Topic);
        SaveBlast.Location = new Point(22, 147);
        SaveBlast.Name = "SaveBlast";
        helpProvider_0.SetShowHelp(SaveBlast, true);
        SaveBlast.Size = new Size(146, 17);
        SaveBlast.TabIndex = 45;
        SaveBlast.Text = "Save Fireblast for runners";
        UseManaStones.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseManaStones, "Mage.html#UseManaStones");
        helpProvider_0.SetHelpNavigator(UseManaStones, HelpNavigator.Topic);
        UseManaStones.Location = new Point(22, 29);
        UseManaStones.Name = "UseManaStones";
        helpProvider_0.SetShowHelp(UseManaStones, true);
        UseManaStones.Size = new Size(108, 17);
        UseManaStones.TabIndex = 42;
        UseManaStones.Text = "Use mana stones";
        UseEvocation.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseEvocation, "Mage.html#UseEvocation");
        helpProvider_0.SetHelpNavigator(UseEvocation, HelpNavigator.Topic);
        UseEvocation.Location = new Point(22, 264);
        UseEvocation.Name = "UseEvocation";
        helpProvider_0.SetShowHelp(UseEvocation, true);
        UseEvocation.Size = new Size(96, 17);
        UseEvocation.TabIndex = 52;
        UseEvocation.Text = "Use Evocation";
        groupBox1.Controls.Add(UseEvocation);
        groupBox1.Controls.Add(UseDampen);
        groupBox1.Controls.Add(UseCombustion);
        groupBox1.Controls.Add(WaitOnPull);
        groupBox1.Controls.Add(UseCounterspell);
        groupBox1.Controls.Add(UseMeleeSpell);
        groupBox1.Controls.Add(ApproachFireblast);
        groupBox1.Controls.Add(UsePoly);
        groupBox1.Controls.Add(UseFrostNova);
        groupBox1.Controls.Add(SaveBlast);
        groupBox1.Controls.Add(UseManaStones);
        groupBox1.Location = new Point(343, 10);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(209, 318);
        groupBox1.TabIndex = 46;
        groupBox1.TabStop = false;
        groupBox1.Text = "Options";
        groupBox2.Controls.Add(label15);
        groupBox2.Controls.Add(label8);
        groupBox2.Controls.Add(ShieldLife);
        groupBox2.Controls.Add(label2);
        groupBox2.Controls.Add(label4);
        groupBox2.Controls.Add(label13);
        groupBox2.Controls.Add(label12);
        groupBox2.Controls.Add(label7);
        groupBox2.Controls.Add(label6);
        groupBox2.Controls.Add(Finisher);
        groupBox2.Controls.Add(label5);
        groupBox2.Controls.Add(IceBarrier);
        groupBox2.Controls.Add(label16);
        groupBox2.Controls.Add(MeleeSpellCooldown);
        groupBox2.Controls.Add(label14);
        groupBox2.Controls.Add(CounterspellLife);
        groupBox2.Controls.Add(label10);
        groupBox2.Controls.Add(FireblastDistance);
        groupBox2.Controls.Add(label11);
        groupBox2.Controls.Add(FinishLife);
        groupBox2.Controls.Add(label9);
        groupBox2.Controls.Add(PullDistance);
        groupBox2.Controls.Add(FireblastCooldown);
        groupBox2.Controls.Add(label3);
        groupBox2.Controls.Add(label1);
        groupBox2.Location = new Point(15, 10);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(315, 318);
        groupBox2.TabIndex = 47;
        groupBox2.TabStop = false;
        groupBox2.Text = "Limits";
        label15.Location = new Point(206, 217);
        label15.Name = "label15";
        label15.Size = new Size(56, 16);
        label15.TabIndex = 74;
        label15.Text = "life";
        label8.AutoSize = true;
        label8.Location = new Point(75, 216);
        label8.Name = "label8";
        label8.Size = new Size(70, 13);
        label8.TabIndex = 73;
        label8.Text = "Shield below:";
        ShieldLife.Enabled = false;
        ShieldLife.Location = new Point(153, 213);
        ShieldLife.Name = "ShieldLife";
        ShieldLife.Size = new Size(48, 20);
        ShieldLife.TabIndex = 72;
        label2.Location = new Point(206, 64);
        label2.Name = "label2";
        label2.Size = new Size(71, 17);
        label2.TabIndex = 68;
        label2.Text = "seconds";
        label4.Location = new Point(206, 16);
        label4.Name = "label4";
        label4.Size = new Size(56, 16);
        label4.TabIndex = 69;
        label4.Text = "yards";
        label13.Location = new Point(206, 88);
        label13.Name = "label13";
        label13.Size = new Size(71, 17);
        label13.TabIndex = 71;
        label13.Text = "seconds";
        label12.Location = new Point(206, 40);
        label12.Name = "label12";
        label12.Size = new Size(56, 15);
        label12.TabIndex = 70;
        label12.Text = "yards";
        label7.Location = new Point(206, 189);
        label7.Name = "label7";
        label7.Size = new Size(56, 16);
        label7.TabIndex = 67;
        label7.Text = "life";
        label6.Location = new Point(206, 165);
        label6.Name = "label6";
        label6.Size = new Size(56, 16);
        label6.TabIndex = 66;
        label6.Text = "life";
        label5.Location = new Point(28, 139);
        label5.Name = "label5";
        label5.Size = new Size(117, 16);
        label5.TabIndex = 64;
        label5.Text = "Finisher:";
        label5.TextAlign = ContentAlignment.TopRight;
        label16.Location = new Point(5, 113);
        label16.Name = "label16";
        label16.Size = new Size(140, 16);
        label16.TabIndex = 62;
        label16.Text = "Ice Barrier mode:";
        label16.TextAlign = ContentAlignment.TopRight;
        label14.Location = new Point(8, 88);
        label14.Name = "label14";
        label14.Size = new Size(140, 17);
        label14.TabIndex = 60;
        label14.Text = "Melee spell cooldown:";
        label14.TextAlign = ContentAlignment.TopRight;
        label10.Location = new Point(37, 189);
        label10.Name = "label10";
        label10.Size = new Size(111, 16);
        label10.TabIndex = 59;
        label10.Text = "Counterspell below:";
        label10.TextAlign = ContentAlignment.TopRight;
        label11.Location = new Point(44, 40);
        label11.Name = "label11";
        label11.Size = new Size(104, 15);
        label11.TabIndex = 57;
        label11.Text = "Fireblast distance:";
        label11.TextAlign = ContentAlignment.TopRight;
        label9.Location = new Point(37, 165);
        label9.Name = "label9";
        label9.Size = new Size(111, 15);
        label9.TabIndex = 56;
        label9.Text = "Finish at:";
        label9.TextAlign = ContentAlignment.TopRight;
        label3.Location = new Point(44, 16);
        label3.Name = "label3";
        label3.Size = new Size(104, 16);
        label3.TabIndex = 53;
        label3.Text = "Pull distance:";
        label3.TextAlign = ContentAlignment.TopRight;
        label1.Location = new Point(37, 64);
        label1.Name = "label1";
        label1.Size = new Size(111, 17);
        label1.TabIndex = 48;
        label1.Text = "Fireblast cooldown:";
        label1.TextAlign = ContentAlignment.TopRight;
        AcceptButton = MyOKButton;
        AutoScaleBaseSize = new Size(5, 13);
        CancelButton = MyCancelButton;
        ClientSize = new Size(591, 393);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        Controls.Add(MyHelpButton);
        Controls.Add(MyCancelButton);
        Controls.Add(MyOKButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(MageConfig);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Config: Mage";
        Load += MageConfig_Load;
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ResumeLayout(false);
    }

    private void MyOKButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        if (StartupClass.smethod_19(FireblastCooldown.Text))
            GClass61.gclass61_0.method_0("Mage.FireblastCooldownSec", FireblastCooldown.Text);
        if (StartupClass.smethod_19(PullDistance.Text))
            GClass61.gclass61_0.method_0("Mage.PullDistance", PullDistance.Text);
        if (StartupClass.smethod_19(FinishLife.Text))
            GClass61.gclass61_0.method_0("Mage.FinishLife", FinishLife.Text);
        if (StartupClass.smethod_19(FireblastDistance.Text))
            GClass61.gclass61_0.method_0("Mage.FireblastDistance", FireblastDistance.Text);
        if (StartupClass.smethod_19(CounterspellLife.Text))
            GClass61.gclass61_0.method_0("Mage.CounterspellLife", CounterspellLife.Text);
        if (StartupClass.smethod_19(ShieldLife.Text))
            GClass61.gclass61_0.method_0("Mage.ShieldLife", ShieldLife.Text);
        if (StartupClass.smethod_19(MeleeSpellCooldown.Text))
            GClass61.gclass61_0.method_0("Mage.MeleeSpellCooldown", MeleeSpellCooldown.Text);
        GClass61.gclass61_0.method_0("Mage.UseManaStones", UseManaStones.Checked.ToString());
        GClass61.gclass61_0.method_0("Mage.SaveBlast", SaveBlast.Checked.ToString());
        GClass61.gclass61_0.method_0("Mage.UseFrostNova", UseFrostNova.Checked.ToString());
        GClass61.gclass61_0.method_0("Mage.UsePoly", UsePoly.Checked.ToString());
        GClass61.gclass61_0.method_0("Mage.UseEvocation", UseEvocation.Checked.ToString());
        GClass61.gclass61_0.method_0("Mage.UseCounterspell", UseCounterspell.Checked.ToString());
        GClass61.gclass61_0.method_0("Mage.UseCombustion", UseCombustion.Checked.ToString());
        GClass61.gclass61_0.method_0("Mage.UseMeleeSpell", UseMeleeSpell.Checked.ToString());
        GClass61.gclass61_0.method_0("Mage.ApproachFireblast", ApproachFireblast.Checked.ToString());
        GClass61.gclass61_0.method_0("Mage.WaitOnPull", WaitOnPull.Checked.ToString());
        GClass61.gclass61_0.method_0("Mage.UseDampen", UseDampen.Checked.ToString());
        GClass61.gclass61_0.method_0("Mage.IceBarrier", IceBarrier.SelectedIndex.ToString());
        GClass61.gclass61_0.method_0("Mage.Finisher", Finisher.SelectedIndex.ToString());
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "Mage.html");
    }

    private void MageConfig_Load(object sender, EventArgs e)
    {
    }

    private void method_0(object sender, EventArgs e)
    {
        MeleeSpellCooldown.Enabled = UseMeleeSpell.Checked;
    }

    private void method_1(object sender, EventArgs e)
    {
        CounterspellLife.Enabled = UseCounterspell.Checked;
    }

    private void Finisher_SelectedIndexChanged(object sender, EventArgs e)
    {
        FinishLife.Enabled = Finisher.SelectedIndex != 0;
    }

    private void UseMeleeSpell_CheckedChanged(object sender, EventArgs e)
    {
        MeleeSpellCooldown.Enabled = UseMeleeSpell.Checked;
    }

    private void UseCounterspell_CheckedChanged(object sender, EventArgs e)
    {
        CounterspellLife.Enabled = UseCounterspell.Checked;
    }

    private void IceBarrier_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShieldLife.Enabled = IceBarrier.SelectedIndex == 3;
    }
}