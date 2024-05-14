// Decompiled with JetBrains decompiler
// Type: WarlockConfig
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class WarlockConfig : Form
{
    private Container container_0;
    private CheckBox DarkPact;
    private TextBox FarmShards;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private HelpProvider helpProvider_0;
    private CheckBox Jump;
    private CheckBox Kite;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Button MyCancelButton;
    private Button MyHelpButton;
    private Button MyOKButton;
    private CheckBox PetAttack;
    private ComboBox PetComboBox;
    private TextBox PullDistance;
    private CheckBox RunPull;
    private CheckBox ShardOneKill;
    private TextBox SpellLockLife;
    private CheckBox StopShards;
    private CheckBox ThreeDotPull;
    private CheckBox UseDeathCoil;
    private CheckBox UseFear;
    private CheckBox UseNightfall;
    private CheckBox UseReckless;
    private CheckBox UseSoulLink;
    private CheckBox UseWand;

    public WarlockConfig()
    {
        InitializeComponent();
        for (var index = 0; index < 6; ++index)
            PetComboBox.Items.Add(GClass30.smethod_4("Warlock.Pet" + index));
        PullDistance.Text = GClass61.gclass61_0.method_2("Warlock.PullDistance");
        SpellLockLife.Text = GClass61.gclass61_0.method_2("Warlock.SpellLockLife");
        FarmShards.Text = GClass61.gclass61_0.method_2("Warlock.FarmShards");
        DarkPact.Checked = GClass61.gclass61_0.method_2("Warlock.DarkPact") == "True";
        UseNightfall.Checked = GClass61.gclass61_0.method_2("Warlock.UseNightfall") == "True";
        UseDeathCoil.Checked = GClass61.gclass61_0.method_2("Warlock.UseDeathcoil") == "True";
        UseFear.Checked = GClass61.gclass61_0.method_2("Warlock.UseFear") == "True";
        RunPull.Checked = GClass61.gclass61_0.method_2("Warlock.RunPull") == "True";
        PetAttack.Checked = GClass61.gclass61_0.method_2("Warlock.PetAttack") == "True";
        ShardOneKill.Checked = GClass61.gclass61_0.method_2("Warlock.ShardOneKill") == "True";
        UseReckless.Checked = GClass61.gclass61_0.method_2("Warlock.Reckless") == "True";
        UseWand.Checked = GClass61.gclass61_0.method_2("Warlock.UseWand") == "True";
        ThreeDotPull.Checked = GClass61.gclass61_0.method_2("Warlock.ThreeDotPull") == "True";
        Kite.Checked = GClass61.gclass61_0.method_2("Warlock.Kite") == "True";
        UseSoulLink.Checked = GClass61.gclass61_0.method_2("Warlock.UseSoulLink") == "True";
        PetComboBox.SelectedIndex = int.Parse(GClass61.gclass61_0.method_2("Warlock.Pet"));
        StopShards.Checked = GClass61.gclass61_0.method_5("Warlock.StopShards");
        Jump.Checked = GClass61.gclass61_0.method_5("Warlock.Jump");
        GClass30.smethod_3(this, "Warlock");
        GProcessMemoryManipulator.smethod_48(this);
        GProcessMemoryManipulator.smethod_51(helpProvider_0);
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
        MyHelpButton = new Button();
        helpProvider_0 = new HelpProvider();
        PullDistance = new TextBox();
        ShardOneKill = new CheckBox();
        FarmShards = new TextBox();
        DarkPact = new CheckBox();
        PetAttack = new CheckBox();
        PetComboBox = new ComboBox();
        UseReckless = new CheckBox();
        UseFear = new CheckBox();
        UseNightfall = new CheckBox();
        UseDeathCoil = new CheckBox();
        UseWand = new CheckBox();
        SpellLockLife = new TextBox();
        ThreeDotPull = new CheckBox();
        UseSoulLink = new CheckBox();
        StopShards = new CheckBox();
        Kite = new CheckBox();
        RunPull = new CheckBox();
        Jump = new CheckBox();
        groupBox1 = new GroupBox();
        label2 = new Label();
        groupBox2 = new GroupBox();
        label6 = new Label();
        label5 = new Label();
        label1 = new Label();
        label4 = new Label();
        label3 = new Label();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        SuspendLayout();
        MyOKButton.Location = new Point(135, 352);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(82, 28);
        MyOKButton.TabIndex = 0;
        MyOKButton.Text = "OK";
        MyOKButton.Click += MyOKButton_Click;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(236, 352);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(81, 28);
        MyCancelButton.TabIndex = 1;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.Click += MyCancelButton_Click;
        MyHelpButton.Location = new Point(371, 352);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(81, 28);
        MyHelpButton.TabIndex = 2;
        MyHelpButton.Text = "Help";
        MyHelpButton.Click += MyHelpButton_Click;
        helpProvider_0.HelpNamespace = "Glider.chm";
        helpProvider_0.SetHelpKeyword(PullDistance, "Warlock.html#PullDistance");
        helpProvider_0.SetHelpNavigator(PullDistance, HelpNavigator.Topic);
        PullDistance.Location = new Point(207, 16);
        PullDistance.Name = "PullDistance";
        helpProvider_0.SetShowHelp(PullDistance, true);
        PullDistance.Size = new Size(55, 20);
        PullDistance.TabIndex = 1;
        ShardOneKill.AutoSize = true;
        helpProvider_0.SetHelpKeyword(ShardOneKill, "Warlock.html#ShardOneKill");
        helpProvider_0.SetHelpNavigator(ShardOneKill, HelpNavigator.Topic);
        ShardOneKill.Location = new Point(227, 46);
        ShardOneKill.Name = "ShardOneKill";
        helpProvider_0.SetShowHelp(ShardOneKill, true);
        ShardOneKill.Size = new Size(94, 17);
        ShardOneKill.TabIndex = 2;
        ShardOneKill.Text = "Shard on 1-Kill";
        helpProvider_0.SetHelpKeyword(FarmShards, "Warlock.html#FarmShards");
        helpProvider_0.SetHelpNavigator(FarmShards, HelpNavigator.Topic);
        FarmShards.Location = new Point(111, 34);
        FarmShards.Name = "FarmShards";
        helpProvider_0.SetShowHelp(FarmShards, true);
        FarmShards.Size = new Size(47, 20);
        FarmShards.TabIndex = 0;
        DarkPact.AutoSize = true;
        helpProvider_0.SetHelpKeyword(DarkPact, "Warlock.html#DarkPact");
        helpProvider_0.SetHelpNavigator(DarkPact, HelpNavigator.Topic);
        DarkPact.Location = new Point(61, 124);
        DarkPact.Name = "DarkPact";
        helpProvider_0.SetShowHelp(DarkPact, true);
        DarkPact.Size = new Size(74, 17);
        DarkPact.TabIndex = 4;
        DarkPact.Text = "Dark Pact";
        PetAttack.AutoSize = true;
        helpProvider_0.SetHelpKeyword(PetAttack, "Warlock.html#PetAttack");
        helpProvider_0.SetHelpNavigator(PetAttack, HelpNavigator.Topic);
        PetAttack.Location = new Point(61, 103);
        PetAttack.Name = "PetAttack";
        helpProvider_0.SetShowHelp(PetAttack, true);
        PetAttack.Size = new Size(75, 17);
        PetAttack.TabIndex = 3;
        PetAttack.Text = "Pet attack";
        PetComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        helpProvider_0.SetHelpKeyword(PetComboBox, "Warlock.html#Pet");
        helpProvider_0.SetHelpNavigator(PetComboBox, HelpNavigator.Topic);
        PetComboBox.Location = new Point(207, 42);
        PetComboBox.Name = "PetComboBox";
        helpProvider_0.SetShowHelp(PetComboBox, true);
        PetComboBox.Size = new Size(110, 21);
        PetComboBox.TabIndex = 0;
        PetComboBox.SelectedIndexChanged += PetComboBox_SelectedIndexChanged;
        UseReckless.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseReckless, "Warlock.html#PreventRunners");
        helpProvider_0.SetHelpNavigator(UseReckless, HelpNavigator.Topic);
        UseReckless.Location = new Point(61, 145);
        UseReckless.Name = "UseReckless";
        helpProvider_0.SetShowHelp(UseReckless, true);
        UseReckless.Size = new Size(101, 17);
        UseReckless.TabIndex = 5;
        UseReckless.Text = "Prevent runners";
        UseFear.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseFear, "Warlock.html#UseFear");
        helpProvider_0.SetHelpNavigator(UseFear, HelpNavigator.Topic);
        UseFear.Location = new Point(61, 186);
        UseFear.Name = "UseFear";
        helpProvider_0.SetShowHelp(UseFear, true);
        UseFear.Size = new Size(142, 17);
        UseFear.TabIndex = 6;
        UseFear.Text = "Fear additional attackers";
        UseNightfall.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseNightfall, "Warlock.html#UseNightfall");
        helpProvider_0.SetHelpNavigator(UseNightfall, HelpNavigator.Topic);
        UseNightfall.Location = new Point(248, 103);
        UseNightfall.Name = "UseNightfall";
        helpProvider_0.SetShowHelp(UseNightfall, true);
        UseNightfall.Size = new Size(99, 17);
        UseNightfall.TabIndex = 7;
        UseNightfall.Text = "Detect Nightfall";
        UseDeathCoil.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseDeathCoil, "Warlock.html#UseDeathCoil");
        helpProvider_0.SetHelpNavigator(UseDeathCoil, HelpNavigator.Topic);
        UseDeathCoil.Location = new Point(248, 124);
        UseDeathCoil.Name = "UseDeathCoil";
        helpProvider_0.SetShowHelp(UseDeathCoil, true);
        UseDeathCoil.Size = new Size(97, 17);
        UseDeathCoil.TabIndex = 8;
        UseDeathCoil.Text = "Use Death Coil";
        UseWand.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseWand, "Warlock.html#UseWand");
        helpProvider_0.SetHelpNavigator(UseWand, HelpNavigator.Topic);
        UseWand.Location = new Point(248, 145);
        UseWand.Name = "UseWand";
        helpProvider_0.SetShowHelp(UseWand, true);
        UseWand.Size = new Size(74, 17);
        UseWand.TabIndex = 9;
        UseWand.Text = "Use wand";
        SpellLockLife.Enabled = false;
        helpProvider_0.SetHelpKeyword(SpellLockLife, "Warlock.html#SpellLockLife");
        helpProvider_0.SetHelpNavigator(SpellLockLife, HelpNavigator.Topic);
        SpellLockLife.Location = new Point(207, 69);
        SpellLockLife.Name = "SpellLockLife";
        helpProvider_0.SetShowHelp(SpellLockLife, true);
        SpellLockLife.Size = new Size(55, 20);
        SpellLockLife.TabIndex = 2;
        ThreeDotPull.AutoSize = true;
        helpProvider_0.SetHelpKeyword(ThreeDotPull, "Warlock.html#ThreeDotPull");
        helpProvider_0.SetHelpNavigator(ThreeDotPull, HelpNavigator.Topic);
        ThreeDotPull.Location = new Point(248, 166);
        ThreeDotPull.Name = "ThreeDotPull";
        helpProvider_0.SetShowHelp(ThreeDotPull, true);
        ThreeDotPull.Size = new Size(96, 17);
        ThreeDotPull.TabIndex = 10;
        ThreeDotPull.Text = "Use Third DoT";
        UseSoulLink.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseSoulLink, "Warlock.html#UseSoulLink");
        helpProvider_0.SetHelpNavigator(UseSoulLink, HelpNavigator.Topic);
        UseSoulLink.Location = new Point(248, 186);
        UseSoulLink.Name = "UseSoulLink";
        helpProvider_0.SetShowHelp(UseSoulLink, true);
        UseSoulLink.Size = new Size(92, 17);
        UseSoulLink.TabIndex = 11;
        UseSoulLink.Text = "Use Soul Link";
        StopShards.AutoSize = true;
        helpProvider_0.SetHelpKeyword(StopShards, "Warlock.html#StopShards");
        helpProvider_0.SetHelpNavigator(StopShards, HelpNavigator.Topic);
        StopShards.Location = new Point(227, 23);
        StopShards.Name = "StopShards";
        helpProvider_0.SetShowHelp(StopShards, true);
        StopShards.Size = new Size(126, 17);
        StopShards.TabIndex = 1;
        StopShards.Text = "Stop when shards ok";
        Kite.AutoSize = true;
        helpProvider_0.SetHelpKeyword(Kite, "Warlock.html#Kite");
        helpProvider_0.SetHelpNavigator(Kite, HelpNavigator.Topic);
        Kite.Location = new Point(61, 166);
        Kite.Name = "Kite";
        helpProvider_0.SetShowHelp(Kite, true);
        Kite.Size = new Size(90, 17);
        Kite.TabIndex = 25;
        Kite.Text = "Kite with Fear";
        RunPull.AutoSize = true;
        helpProvider_0.SetHelpKeyword(RunPull, "Warlock.html#RunPull");
        helpProvider_0.SetHelpNavigator(RunPull, HelpNavigator.Topic);
        RunPull.Location = new Point(61, 210);
        RunPull.Name = "RunPull";
        helpProvider_0.SetShowHelp(RunPull, true);
        RunPull.Size = new Size(108, 17);
        RunPull.TabIndex = 26;
        RunPull.Text = "Pull while running";
        Jump.AutoSize = true;
        helpProvider_0.SetHelpKeyword(Jump, "Warlock.html#Jump");
        helpProvider_0.SetHelpNavigator(Jump, HelpNavigator.Topic);
        Jump.Location = new Point(248, 210);
        Jump.Name = "Jump";
        helpProvider_0.SetShowHelp(Jump, true);
        Jump.Size = new Size(51, 17);
        Jump.TabIndex = 27;
        Jump.Text = "Jump";
        groupBox1.Controls.Add(StopShards);
        groupBox1.Controls.Add(ShardOneKill);
        groupBox1.Controls.Add(FarmShards);
        groupBox1.Controls.Add(label2);
        groupBox1.Location = new Point(8, 259);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(444, 79);
        groupBox1.TabIndex = 1;
        groupBox1.TabStop = false;
        groupBox1.Text = "Shards";
        label2.AutoSize = true;
        label2.Location = new Point(12, 36);
        label2.Name = "label2";
        label2.Size = new Size(84, 13);
        label2.TabIndex = 0;
        label2.Text = "Maintain shards:";
        label2.TextAlign = ContentAlignment.MiddleRight;
        groupBox2.Controls.Add(Jump);
        groupBox2.Controls.Add(RunPull);
        groupBox2.Controls.Add(Kite);
        groupBox2.Controls.Add(UseSoulLink);
        groupBox2.Controls.Add(ThreeDotPull);
        groupBox2.Controls.Add(label6);
        groupBox2.Controls.Add(SpellLockLife);
        groupBox2.Controls.Add(label5);
        groupBox2.Controls.Add(UseWand);
        groupBox2.Controls.Add(UseDeathCoil);
        groupBox2.Controls.Add(UseNightfall);
        groupBox2.Controls.Add(UseFear);
        groupBox2.Controls.Add(UseReckless);
        groupBox2.Controls.Add(DarkPact);
        groupBox2.Controls.Add(PetAttack);
        groupBox2.Controls.Add(PetComboBox);
        groupBox2.Controls.Add(label1);
        groupBox2.Controls.Add(PullDistance);
        groupBox2.Controls.Add(label4);
        groupBox2.Controls.Add(label3);
        groupBox2.Location = new Point(8, 8);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(444, 246);
        groupBox2.TabIndex = 0;
        groupBox2.TabStop = false;
        groupBox2.Text = "General";
        label6.AutoSize = true;
        label6.Location = new Point(267, 72);
        label6.Name = "label6";
        label6.Size = new Size(20, 13);
        label6.TabIndex = 24;
        label6.Text = "life";
        label6.TextAlign = ContentAlignment.MiddleLeft;
        label5.Location = new Point(59, 71);
        label5.Name = "label5";
        label5.Size = new Size(143, 16);
        label5.TabIndex = 22;
        label5.Text = "Spell Lock at:";
        label5.TextAlign = ContentAlignment.MiddleRight;
        label1.Location = new Point(137, 44);
        label1.Name = "label1";
        label1.Size = new Size(65, 17);
        label1.TabIndex = 21;
        label1.Text = "Pet:";
        label1.TextAlign = ContentAlignment.MiddleRight;
        label4.AutoSize = true;
        label4.Location = new Point(267, 18);
        label4.Name = "label4";
        label4.Size = new Size(32, 13);
        label4.TabIndex = 20;
        label4.Text = "yards";
        label4.TextAlign = ContentAlignment.MiddleLeft;
        label3.Location = new Point(89, 17);
        label3.Name = "label3";
        label3.Size = new Size(113, 17);
        label3.TabIndex = 18;
        label3.Text = "Pull distance:";
        label3.TextAlign = ContentAlignment.MiddleRight;
        AcceptButton = MyOKButton;
        AutoScaleBaseSize = new Size(5, 13);
        CancelButton = MyCancelButton;
        ClientSize = new Size(473, 397);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        Controls.Add(MyHelpButton);
        Controls.Add(MyCancelButton);
        Controls.Add(MyOKButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(WarlockConfig);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Config: Warlock";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ResumeLayout(false);
    }

    private void MyOKButton_Click(object sender, EventArgs e)
    {
        if (StopShards.Checked &&
            MessageBox.Show(this, GClass30.smethod_1(828), "Glider", MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation) != DialogResult.Yes)
            return;
        DialogResult = DialogResult.OK;
        if (StartupClass.smethod_19(PullDistance.Text))
            GClass61.gclass61_0.method_0("Warlock.PullDistance", PullDistance.Text);
        if (StartupClass.smethod_19(SpellLockLife.Text))
            GClass61.gclass61_0.method_0("Warlock.SpellLockLife", SpellLockLife.Text);
        if (StartupClass.smethod_19(FarmShards.Text))
            GClass61.gclass61_0.method_0("Warlock.FarmShards", FarmShards.Text);
        else
            GClass61.gclass61_0.method_0("Warlock.FarmShards", "0");
        GClass61.gclass61_0.method_0("Warlock.RunPull", RunPull.Checked.ToString());
        GClass61.gclass61_0.method_0("Warlock.UseFear", UseFear.Checked.ToString());
        GClass61.gclass61_0.method_0("Warlock.UseNightfall", UseNightfall.Checked.ToString());
        GClass61.gclass61_0.method_0("Warlock.UseDeathcoil", UseDeathCoil.Checked.ToString());
        GClass61.gclass61_0.method_0("Warlock.PetAttack", PetAttack.Checked.ToString());
        GClass61.gclass61_0.method_0("Warlock.DarkPact", DarkPact.Checked.ToString());
        GClass61.gclass61_0.method_0("Warlock.Reckless", UseReckless.Checked.ToString());
        GClass61.gclass61_0.method_0("Warlock.ThreeDotPull", ThreeDotPull.Checked.ToString());
        GClass61.gclass61_0.method_0("Warlock.Kite", Kite.Checked.ToString());
        GClass61.gclass61_0.method_0("Warlock.UseWand", UseWand.Checked.ToString());
        GClass61.gclass61_0.method_0("Warlock.UseSoulLink", UseSoulLink.Checked.ToString());
        GClass61.gclass61_0.method_0("Warlock.ShardOneKill", ShardOneKill.Checked.ToString());
        GClass61.gclass61_0.method_0("Warlock.Pet", PetComboBox.SelectedIndex.ToString());
        GClass61.gclass61_0.method_0("Warlock.StopShards", StopShards.Checked.ToString());
        GClass61.gclass61_0.method_0("Warlock.Jump", Jump.Checked.ToString());
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "Warlock.html");
    }

    private void PetComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        SpellLockLife.Enabled = PetComboBox.SelectedIndex == 2;
    }
}