// Decompiled with JetBrains decompiler
// Type: WarriorConfig
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class WarriorConfig : Form
{
    private TextBox AvoidAddDistance;
    private CheckBox AvoidAdds;
    private CheckBox ChargePull;
    private CheckBox ChaseRunners;
    private Container container_0;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private HelpProvider helpProvider_0;
    private TextBox HeroicCooldown;
    private TextBox HeroicRage;
    private Label label1;
    private Label label10;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private Label label8;
    private Label label9;
    private Button MyCancelButton;
    private Button MyHelpButton;
    private Button MyOKButton;
    private TextBox PullDistance;
    private TextBox ShieldBashLife;
    private CheckBox UseBloodrage;
    private CheckBox UseCleave;
    private CheckBox UseConcussion;
    private CheckBox UseDemoralizing;
    private CheckBox UseExecute;
    private CheckBox UseHamstring;
    private CheckBox UseMortalStrike;
    private CheckBox UseOverpower;
    private CheckBox UseShieldBash;
    private CheckBox UseSunder;

    public WarriorConfig()
    {
        InitializeComponent();
        AvoidAddDistance.Text = GClass61.gclass61_0.method_2("Warrior.AvoidAddDistance");
        PullDistance.Text = GClass61.gclass61_0.method_2("Warrior.PullDistance");
        HeroicRage.Text = GClass61.gclass61_0.method_2("Warrior.HeroicRage");
        HeroicCooldown.Text = GClass61.gclass61_0.method_2("Warrior.HeroicCooldown");
        ShieldBashLife.Text = GClass61.gclass61_0.method_2("Warrior.ShieldBashLife");
        ChargePull.Checked = GClass61.gclass61_0.method_2("Warrior.ChargePull") == "True";
        UseConcussion.Checked = GClass61.gclass61_0.method_2("Warrior.UseConcussion") == "True";
        UseExecute.Checked = GClass61.gclass61_0.method_2("Warrior.UseExecute") == "True";
        UseSunder.Checked = GClass61.gclass61_0.method_2("Warrior.UseSunder") == "True";
        ChaseRunners.Checked = GClass61.gclass61_0.method_2("Warrior.ChaseRunners") == "True";
        UseHamstring.Checked = GClass61.gclass61_0.method_2("Warrior.UseHamstring") == "True";
        UseBloodrage.Checked = GClass61.gclass61_0.method_2("Warrior.UseBloodrage") == "True";
        UseDemoralizing.Checked = GClass61.gclass61_0.method_2("Warrior.UseDemoralizing") == "True";
        UseCleave.Checked = GClass61.gclass61_0.method_2("Warrior.UseCleave") == "True";
        UseOverpower.Checked = GClass61.gclass61_0.method_2("Warrior.UseOverpower") == "True";
        UseShieldBash.Checked = GClass61.gclass61_0.method_2("Warrior.UseShieldBash") == "True";
        AvoidAdds.Checked = GClass61.gclass61_0.method_2("Warrior.AvoidAdds") == "True";
        UseMortalStrike.Checked = GClass61.gclass61_0.method_2("Warrior.UseMortalStrike") == "True";
        GClass30.smethod_3(this, "Warrior");
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
        ChargePull = new CheckBox();
        UseConcussion = new CheckBox();
        UseExecute = new CheckBox();
        UseSunder = new CheckBox();
        label1 = new Label();
        HeroicCooldown = new TextBox();
        label2 = new Label();
        MyHelpButton = new Button();
        helpProvider_0 = new HelpProvider();
        ChaseRunners = new CheckBox();
        UseHamstring = new CheckBox();
        UseBloodrage = new CheckBox();
        HeroicRage = new TextBox();
        UseDemoralizing = new CheckBox();
        UseCleave = new CheckBox();
        UseOverpower = new CheckBox();
        ShieldBashLife = new TextBox();
        UseShieldBash = new CheckBox();
        AvoidAddDistance = new TextBox();
        AvoidAdds = new CheckBox();
        UseMortalStrike = new CheckBox();
        label5 = new Label();
        label6 = new Label();
        label7 = new Label();
        label8 = new Label();
        label9 = new Label();
        label10 = new Label();
        groupBox1 = new GroupBox();
        groupBox2 = new GroupBox();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        SuspendLayout();
        MyOKButton.Location = new Point(243, 224);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(80, 31);
        MyOKButton.TabIndex = 12;
        MyOKButton.Text = "OK";
        MyOKButton.Click += MyOKButton_Click;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(355, 224);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(80, 31);
        MyCancelButton.TabIndex = 13;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.Click += MyCancelButton_Click;
        label3.Location = new Point(36, 26);
        label3.Name = "label3";
        label3.Size = new Size(104, 16);
        label3.TabIndex = 7;
        label3.Text = "Pull distance:";
        label3.TextAlign = ContentAlignment.MiddleRight;
        helpProvider_0.SetHelpKeyword(PullDistance, "Warrior.html#PullDistance");
        helpProvider_0.SetHelpNavigator(PullDistance, HelpNavigator.Topic);
        PullDistance.Location = new Point(148, 26);
        PullDistance.Name = "PullDistance";
        helpProvider_0.SetShowHelp(PullDistance, true);
        PullDistance.Size = new Size(48, 20);
        PullDistance.TabIndex = 0;
        label4.Location = new Point(204, 26);
        label4.Name = "label4";
        label4.Size = new Size(56, 16);
        label4.TabIndex = 9;
        label4.Text = "yards";
        label4.TextAlign = ContentAlignment.MiddleLeft;
        ChargePull.AutoSize = true;
        helpProvider_0.SetHelpKeyword(ChargePull, "Warrior.html#PullWithCharge");
        helpProvider_0.SetHelpNavigator(ChargePull, HelpNavigator.Topic);
        ChargePull.Location = new Point(23, 69);
        ChargePull.Name = "ChargePull";
        helpProvider_0.SetShowHelp(ChargePull, true);
        ChargePull.Size = new Size(102, 17);
        ChargePull.TabIndex = 3;
        ChargePull.Text = "Pull with Charge";
        UseConcussion.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseConcussion, "Warrior.html#Concussion");
        helpProvider_0.SetHelpNavigator(UseConcussion, HelpNavigator.Topic);
        UseConcussion.Location = new Point(23, 92);
        UseConcussion.Name = "UseConcussion";
        helpProvider_0.SetShowHelp(UseConcussion, true);
        UseConcussion.Size = new Size(129, 17);
        UseConcussion.TabIndex = 4;
        UseConcussion.Text = "Use Concussion Blow";
        UseExecute.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseExecute, "Warrior.html#Execute");
        helpProvider_0.SetHelpNavigator(UseExecute, HelpNavigator.Topic);
        UseExecute.Location = new Point(23, 115);
        UseExecute.Name = "UseExecute";
        helpProvider_0.SetShowHelp(UseExecute, true);
        UseExecute.Size = new Size(87, 17);
        UseExecute.TabIndex = 5;
        UseExecute.Text = "Use Execute";
        UseSunder.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseSunder, "Warrior.html#Sunder");
        helpProvider_0.SetHelpNavigator(UseSunder, HelpNavigator.Topic);
        UseSunder.Location = new Point(23, 138);
        UseSunder.Name = "UseSunder";
        helpProvider_0.SetShowHelp(UseSunder, true);
        UseSunder.Size = new Size(112, 17);
        UseSunder.TabIndex = 6;
        UseSunder.Text = "Use Sunder Armor";
        label1.Location = new Point(12, 50);
        label1.Name = "label1";
        label1.Size = new Size(128, 16);
        label1.TabIndex = 14;
        label1.Text = "Heroic Strike cooldown:";
        label1.TextAlign = ContentAlignment.MiddleRight;
        helpProvider_0.SetHelpKeyword(HeroicCooldown, "Warrior.html#Heroic");
        helpProvider_0.SetHelpNavigator(HeroicCooldown, HelpNavigator.Topic);
        HeroicCooldown.Location = new Point(148, 50);
        HeroicCooldown.Name = "HeroicCooldown";
        helpProvider_0.SetShowHelp(HeroicCooldown, true);
        HeroicCooldown.Size = new Size(48, 20);
        HeroicCooldown.TabIndex = 1;
        label2.Location = new Point(204, 50);
        label2.Name = "label2";
        label2.Size = new Size(56, 16);
        label2.TabIndex = 16;
        label2.Text = "seconds";
        label2.TextAlign = ContentAlignment.MiddleLeft;
        MyHelpButton.Location = new Point(587, 224);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(80, 31);
        MyHelpButton.TabIndex = 14;
        MyHelpButton.Text = "Help";
        MyHelpButton.Click += MyHelpButton_Click;
        helpProvider_0.HelpNamespace = "Glider.chm";
        ChaseRunners.AutoSize = true;
        helpProvider_0.SetHelpKeyword(ChaseRunners, "Warrior.html#ChaseRunners");
        helpProvider_0.SetHelpNavigator(ChaseRunners, HelpNavigator.Topic);
        ChaseRunners.Location = new Point(206, 23);
        ChaseRunners.Name = "ChaseRunners";
        helpProvider_0.SetShowHelp(ChaseRunners, true);
        ChaseRunners.Size = new Size(94, 17);
        ChaseRunners.TabIndex = 7;
        ChaseRunners.Text = "Chase runners";
        UseHamstring.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseHamstring, "Warrior.html#UseHamstring");
        helpProvider_0.SetHelpNavigator(UseHamstring, HelpNavigator.Topic);
        UseHamstring.Location = new Point(206, 46);
        UseHamstring.Name = "UseHamstring";
        helpProvider_0.SetShowHelp(UseHamstring, true);
        UseHamstring.Size = new Size(136, 17);
        UseHamstring.TabIndex = 8;
        UseHamstring.Text = "Hamstring at low health";
        UseBloodrage.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseBloodrage, "Warrior.html#UseBloodrage");
        helpProvider_0.SetHelpNavigator(UseBloodrage, HelpNavigator.Topic);
        UseBloodrage.Location = new Point(206, 69);
        UseBloodrage.Name = "UseBloodrage";
        helpProvider_0.SetShowHelp(UseBloodrage, true);
        UseBloodrage.Size = new Size(96, 17);
        UseBloodrage.TabIndex = 9;
        UseBloodrage.Text = "Use Bloodrage";
        helpProvider_0.SetHelpKeyword(HeroicRage, "Warrior.html#HeroicRage");
        helpProvider_0.SetHelpNavigator(HeroicRage, HelpNavigator.Topic);
        HeroicRage.Location = new Point(148, 74);
        HeroicRage.Name = "HeroicRage";
        helpProvider_0.SetShowHelp(HeroicRage, true);
        HeroicRage.Size = new Size(48, 20);
        HeroicRage.TabIndex = 2;
        UseDemoralizing.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseDemoralizing, "Warrior.html#UseBloodrage");
        helpProvider_0.SetHelpNavigator(UseDemoralizing, HelpNavigator.Topic);
        UseDemoralizing.Location = new Point(206, 92);
        UseDemoralizing.Name = "UseDemoralizing";
        helpProvider_0.SetShowHelp(UseDemoralizing, true);
        UseDemoralizing.Size = new Size(139, 17);
        UseDemoralizing.TabIndex = 10;
        UseDemoralizing.Text = "Use Demoralizing Shout";
        UseCleave.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseCleave, "Warrior.html#UseCleave");
        helpProvider_0.SetHelpNavigator(UseCleave, HelpNavigator.Topic);
        UseCleave.Location = new Point(206, 115);
        UseCleave.Name = "UseCleave";
        helpProvider_0.SetShowHelp(UseCleave, true);
        UseCleave.Size = new Size(122, 17);
        UseCleave.TabIndex = 11;
        UseCleave.Text = "Use Cleave on adds";
        UseOverpower.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseOverpower, "Warrior.html#UseOverpower");
        helpProvider_0.SetHelpNavigator(UseOverpower, HelpNavigator.Topic);
        UseOverpower.Location = new Point(206, 138);
        UseOverpower.Name = "UseOverpower";
        helpProvider_0.SetShowHelp(UseOverpower, true);
        UseOverpower.Size = new Size(100, 17);
        UseOverpower.TabIndex = 21;
        UseOverpower.Text = "Use Overpower";
        ShieldBashLife.Enabled = false;
        helpProvider_0.SetHelpKeyword(ShieldBashLife, "Warrior.html#ShieldBashLife");
        helpProvider_0.SetHelpNavigator(ShieldBashLife, HelpNavigator.Topic);
        ShieldBashLife.Location = new Point(148, 98);
        ShieldBashLife.Name = "ShieldBashLife";
        helpProvider_0.SetShowHelp(ShieldBashLife, true);
        ShieldBashLife.Size = new Size(48, 20);
        ShieldBashLife.TabIndex = 24;
        UseShieldBash.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseShieldBash, "Warrior.html#UseShieldBash");
        helpProvider_0.SetHelpNavigator(UseShieldBash, HelpNavigator.Topic);
        UseShieldBash.Location = new Point(23, 46);
        UseShieldBash.Name = "UseShieldBash";
        helpProvider_0.SetShowHelp(UseShieldBash, true);
        UseShieldBash.Size = new Size(104, 17);
        UseShieldBash.TabIndex = 25;
        UseShieldBash.Text = "Use Shield Bash";
        UseShieldBash.CheckedChanged += UseShieldBash_CheckedChanged;
        AvoidAddDistance.Enabled = false;
        helpProvider_0.SetHelpKeyword(AvoidAddDistance, "Paladin.html#AvoidAdds");
        helpProvider_0.SetHelpNavigator(AvoidAddDistance, HelpNavigator.Topic);
        AvoidAddDistance.Location = new Point(148, 125);
        AvoidAddDistance.Name = "AvoidAddDistance";
        helpProvider_0.SetShowHelp(AvoidAddDistance, true);
        AvoidAddDistance.Size = new Size(40, 20);
        AvoidAddDistance.TabIndex = 28;
        AvoidAdds.AutoSize = true;
        helpProvider_0.SetHelpKeyword(AvoidAdds, "Paladin.html#AvoidAdds");
        helpProvider_0.SetHelpNavigator(AvoidAdds, HelpNavigator.Topic);
        AvoidAdds.Location = new Point(23, 23);
        AvoidAdds.Name = "AvoidAdds";
        helpProvider_0.SetShowHelp(AvoidAdds, true);
        AvoidAdds.Size = new Size(120, 17);
        AvoidAdds.TabIndex = 26;
        AvoidAdds.Text = "Avoid possible adds";
        AvoidAdds.CheckedChanged += AvoidAdds_CheckedChanged;
        UseMortalStrike.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseMortalStrike, "Warrior.html#UseMortalStrike");
        helpProvider_0.SetHelpNavigator(UseMortalStrike, HelpNavigator.Topic);
        UseMortalStrike.Location = new Point(23, 161);
        UseMortalStrike.Name = "UseMortalStrike";
        helpProvider_0.SetShowHelp(UseMortalStrike, true);
        UseMortalStrike.Size = new Size(107, 17);
        UseMortalStrike.TabIndex = 27;
        UseMortalStrike.Text = "Use Mortal Strike";
        label5.Location = new Point(12, 74);
        label5.Name = "label5";
        label5.Size = new Size(128, 16);
        label5.TabIndex = 18;
        label5.Text = "Heroic Strike cost:";
        label5.TextAlign = ContentAlignment.MiddleRight;
        label6.Location = new Point(204, 74);
        label6.Name = "label6";
        label6.Size = new Size(56, 16);
        label6.TabIndex = 20;
        label6.Text = "rage";
        label6.TextAlign = ContentAlignment.MiddleLeft;
        label7.Location = new Point(12, 98);
        label7.Name = "label7";
        label7.Size = new Size(128, 16);
        label7.TabIndex = 22;
        label7.Text = "Shield Bash below:";
        label7.TextAlign = ContentAlignment.MiddleRight;
        label8.Location = new Point(204, 98);
        label8.Name = "label8";
        label8.Size = new Size(56, 16);
        label8.TabIndex = 23;
        label8.Text = "life";
        label8.TextAlign = ContentAlignment.MiddleLeft;
        label9.AutoSize = true;
        label9.Location = new Point(199, 128);
        label9.Name = "label9";
        label9.Size = new Size(32, 13);
        label9.TabIndex = 29;
        label9.Text = "yards";
        label10.AutoSize = true;
        label10.Location = new Point(39, 128);
        label10.Name = "label10";
        label10.Size = new Size(101, 13);
        label10.TabIndex = 27;
        label10.Text = "Avoid add distance:";
        label10.TextAlign = ContentAlignment.TopRight;
        groupBox1.Controls.Add(UseMortalStrike);
        groupBox1.Controls.Add(UseDemoralizing);
        groupBox1.Controls.Add(ChargePull);
        groupBox1.Controls.Add(AvoidAdds);
        groupBox1.Controls.Add(UseConcussion);
        groupBox1.Controls.Add(UseShieldBash);
        groupBox1.Controls.Add(UseExecute);
        groupBox1.Controls.Add(UseOverpower);
        groupBox1.Controls.Add(UseSunder);
        groupBox1.Controls.Add(UseCleave);
        groupBox1.Controls.Add(ChaseRunners);
        groupBox1.Controls.Add(UseHamstring);
        groupBox1.Controls.Add(UseBloodrage);
        groupBox1.Location = new Point(300, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(367, 206);
        groupBox1.TabIndex = 30;
        groupBox1.TabStop = false;
        groupBox1.Text = "Options";
        groupBox2.Controls.Add(label10);
        groupBox2.Controls.Add(label3);
        groupBox2.Controls.Add(label9);
        groupBox2.Controls.Add(label4);
        groupBox2.Controls.Add(AvoidAddDistance);
        groupBox2.Controls.Add(label1);
        groupBox2.Controls.Add(label2);
        groupBox2.Controls.Add(label5);
        groupBox2.Controls.Add(PullDistance);
        groupBox2.Controls.Add(ShieldBashLife);
        groupBox2.Controls.Add(HeroicCooldown);
        groupBox2.Controls.Add(label8);
        groupBox2.Controls.Add(HeroicRage);
        groupBox2.Controls.Add(label7);
        groupBox2.Controls.Add(label6);
        groupBox2.Location = new Point(11, 12);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(283, 206);
        groupBox2.TabIndex = 31;
        groupBox2.TabStop = false;
        groupBox2.Text = "Limits";
        AcceptButton = MyOKButton;
        AutoScaleBaseSize = new Size(5, 13);
        CancelButton = MyCancelButton;
        ClientSize = new Size(683, 269);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        Controls.Add(MyHelpButton);
        Controls.Add(MyCancelButton);
        Controls.Add(MyOKButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        helpProvider_0.SetHelpKeyword(this, "Warrior.html");
        helpProvider_0.SetHelpNavigator(this, HelpNavigator.Topic);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(WarriorConfig);
        helpProvider_0.SetShowHelp(this, true);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Config: Warrior";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ResumeLayout(false);
    }

    private void MyOKButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        GClass61.gclass61_0.method_0("Warrior.ChargePull", ChargePull.Checked.ToString());
        GClass61.gclass61_0.method_0("Warrior.UseConcussion", UseConcussion.Checked.ToString());
        GClass61.gclass61_0.method_0("Warrior.UseExecute", UseExecute.Checked.ToString());
        GClass61.gclass61_0.method_0("Warrior.UseSunder", UseSunder.Checked.ToString());
        GClass61.gclass61_0.method_0("Warrior.ChaseRunners", ChaseRunners.Checked.ToString());
        GClass61.gclass61_0.method_0("Warrior.UseHamstring", UseHamstring.Checked.ToString());
        GClass61.gclass61_0.method_0("Warrior.UseBloodrage", UseBloodrage.Checked.ToString());
        GClass61.gclass61_0.method_0("Warrior.UseDemoralizing", UseDemoralizing.Checked.ToString());
        GClass61.gclass61_0.method_0("Warrior.UseCleave", UseCleave.Checked.ToString());
        GClass61.gclass61_0.method_0("Warrior.UseOverpower", UseOverpower.Checked.ToString());
        GClass61.gclass61_0.method_0("Warrior.UseShieldBash", UseShieldBash.Checked.ToString());
        GClass61.gclass61_0.method_0("Warrior.AvoidAdds", AvoidAdds.Checked.ToString());
        GClass61.gclass61_0.method_0("Warrior.UseMortalStrike", UseMortalStrike.Checked.ToString());
        if (StartupClass.smethod_19(PullDistance.Text))
            GClass61.gclass61_0.method_0("Warrior.PullDistance", PullDistance.Text);
        if (StartupClass.smethod_19(HeroicRage.Text))
            GClass61.gclass61_0.method_0("Warrior.HeroicRage", HeroicRage.Text);
        if (StartupClass.smethod_19(HeroicCooldown.Text))
            GClass61.gclass61_0.method_0("Warrior.HeroicCooldown", HeroicCooldown.Text);
        if (StartupClass.smethod_19(ShieldBashLife.Text))
            GClass61.gclass61_0.method_0("Warrior.ShieldBashLife", ShieldBashLife.Text);
        if (!StartupClass.smethod_19(AvoidAddDistance.Text))
            return;
        GClass61.gclass61_0.method_0("Warrior.AvoidAddDistance", AvoidAddDistance.Text);
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "Warrior.html");
    }

    private void UseShieldBash_CheckedChanged(object sender, EventArgs e)
    {
        ShieldBashLife.Enabled = UseShieldBash.Checked;
    }

    private void AvoidAdds_CheckedChanged(object sender, EventArgs e)
    {
        AvoidAddDistance.Enabled = AvoidAdds.Checked;
    }
}