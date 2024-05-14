// Decompiled with JetBrains decompiler
// Type: ShamanConfig
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class ShamanConfig : Form
{
    private TextBox AvoidAddDistance;
    private CheckBox AvoidAdds;
    private Container container_0;
    private CheckBox DualWield;
    private CheckBox ExtraShield;
    private CheckBox FastMelee;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private HelpProvider helpProvider_0;
    private Label label10;
    private Label label11;
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
    private TextBox ShockLife;
    private TextBox ShockMana;
    private ComboBox ShockMode;
    private CheckBox ShockPull;
    private CheckBox ShootRunners;
    private CheckBox StartTotem;
    private CheckBox UseEarthbind;
    private CheckBox UseHealTotem;
    private CheckBox UseLightningShield;
    private CheckBox UseRage;
    private CheckBox UseStoneclaw;
    private CheckBox UseStormstrike;
    private CheckBox UseSwiftness;
    private CheckBox UseTotemicCall;

    public ShamanConfig()
    {
        InitializeComponent();
        GClass30.smethod_7(ShockMode, "Common.ShockMode");
        PullDistance.Text = GClass61.gclass61_0.method_2("Shaman.PullDistance");
        ShockLife.Text = GClass61.gclass61_0.method_2("Shaman.ShockLife");
        ShockMana.Text = GClass61.gclass61_0.method_2("Shaman.ShockMana");
        ShootRunners.Checked = GClass61.gclass61_0.method_2("Shaman.ShootRunners") == "True";
        UseSwiftness.Checked = GClass61.gclass61_0.method_2("Shaman.UseSwiftness") == "True";
        StartTotem.Checked = GClass61.gclass61_0.method_2("Shaman.StartTotem") == "True";
        FastMelee.Checked = GClass61.gclass61_0.method_2("Shaman.FastMelee") == "True";
        ShockPull.Checked = GClass61.gclass61_0.method_2("Shaman.ShockPull") == "True";
        UseHealTotem.Checked = GClass61.gclass61_0.method_2("Shaman.UseHealTotem") == "True";
        UseEarthbind.Checked = GClass61.gclass61_0.method_2("Shaman.UseEarthbind") == "True";
        DualWield.Checked = GClass61.gclass61_0.method_2("Shaman.DualWield") == "True";
        ExtraShield.Checked = GClass61.gclass61_0.method_2("Shaman.ExtraShield") == "True";
        UseStormstrike.Checked = GClass61.gclass61_0.method_2("Shaman.UseStormstrike") == "True";
        UseRage.Checked = GClass61.gclass61_0.method_2("Shaman.UseRage") == "True";
        AvoidAdds.Checked = GClass61.gclass61_0.method_2("Shaman.AvoidAdds") == "True";
        UseTotemicCall.Checked = GClass61.gclass61_0.method_2("Shaman.UseTotemicCall") == "True";
        UseLightningShield.Checked = GClass61.gclass61_0.method_2("Shaman.UseLightningShield") == "True";
        UseStoneclaw.Checked = GClass61.gclass61_0.method_2("Shaman.UseStoneclaw") == "True";
        AvoidAddDistance.Text = GClass61.gclass61_0.method_2("Shaman.AvoidAddDistance");
        switch (GClass61.gclass61_0.method_2("Shaman.ShockMode"))
        {
            case "Spam":
                ShockMode.SelectedIndex = 0;
                break;
            case "Runners":
                ShockMode.SelectedIndex = 1;
                break;
            case "Interrupt":
                ShockMode.SelectedIndex = 2;
                break;
        }

        GClass30.smethod_3(this, "Shaman");
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
        ShootRunners = new CheckBox();
        UseSwiftness = new CheckBox();
        StartTotem = new CheckBox();
        FastMelee = new CheckBox();
        ShockPull = new CheckBox();
        UseHealTotem = new CheckBox();
        UseEarthbind = new CheckBox();
        ShockMana = new TextBox();
        ShockLife = new TextBox();
        ShockMode = new ComboBox();
        DualWield = new CheckBox();
        UseStormstrike = new CheckBox();
        ExtraShield = new CheckBox();
        UseRage = new CheckBox();
        AvoidAdds = new CheckBox();
        AvoidAddDistance = new TextBox();
        UseTotemicCall = new CheckBox();
        UseStoneclaw = new CheckBox();
        UseLightningShield = new CheckBox();
        label5 = new Label();
        label6 = new Label();
        label7 = new Label();
        label8 = new Label();
        label9 = new Label();
        label10 = new Label();
        label11 = new Label();
        groupBox1 = new GroupBox();
        groupBox2 = new GroupBox();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        SuspendLayout();
        MyOKButton.Location = new Point(268, 274);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(64, 24);
        MyOKButton.TabIndex = 0;
        MyOKButton.Text = "OK";
        MyOKButton.Click += MyOKButton_Click;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(348, 274);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(64, 24);
        MyCancelButton.TabIndex = 1;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.Click += MyCancelButton_Click;
        label3.Location = new Point(34, 29);
        label3.Name = "label3";
        label3.Size = new Size(104, 16);
        label3.TabIndex = 7;
        label3.Text = "Pull distance:";
        label3.TextAlign = ContentAlignment.TopRight;
        helpProvider_0.SetHelpKeyword(PullDistance, "Shaman.html#PullDistance");
        helpProvider_0.SetHelpNavigator(PullDistance, HelpNavigator.Topic);
        PullDistance.Location = new Point(143, 27);
        PullDistance.Name = "PullDistance";
        helpProvider_0.SetShowHelp(PullDistance, true);
        PullDistance.Size = new Size(47, 20);
        PullDistance.TabIndex = 0;
        label4.AutoSize = true;
        label4.Location = new Point(195, 29);
        label4.Name = "label4";
        label4.Size = new Size(32, 13);
        label4.TabIndex = 9;
        label4.Text = "yards";
        MyHelpButton.Location = new Point(428, 274);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(64, 24);
        MyHelpButton.TabIndex = 2;
        MyHelpButton.Text = "Help";
        MyHelpButton.Click += MyHelpButton_Click;
        helpProvider_0.HelpNamespace = "Glider.chm";
        ShootRunners.AutoSize = true;
        helpProvider_0.SetHelpKeyword(ShootRunners, "Shaman.html#ShootRunners");
        helpProvider_0.SetHelpNavigator(ShootRunners, HelpNavigator.Topic);
        ShootRunners.Location = new Point(12, 78);
        ShootRunners.Name = "ShootRunners";
        helpProvider_0.SetShowHelp(ShootRunners, true);
        ShootRunners.Size = new Size(92, 17);
        ShootRunners.TabIndex = 2;
        ShootRunners.Text = "Shoot runners";
        UseSwiftness.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseSwiftness, "Shaman.html#UseSwiftness");
        helpProvider_0.SetHelpNavigator(UseSwiftness, HelpNavigator.Topic);
        UseSwiftness.Location = new Point(12, 105);
        UseSwiftness.Name = "UseSwiftness";
        helpProvider_0.SetShowHelp(UseSwiftness, true);
        UseSwiftness.Size = new Size(151, 17);
        UseSwiftness.TabIndex = 3;
        UseSwiftness.Text = "Nature's Swiftness on heal";
        StartTotem.AutoSize = true;
        helpProvider_0.SetHelpKeyword(StartTotem, "Shaman.html#StartTotem");
        helpProvider_0.SetHelpNavigator(StartTotem, HelpNavigator.Topic);
        StartTotem.Location = new Point(12, 24);
        StartTotem.Name = "StartTotem";
        helpProvider_0.SetShowHelp(StartTotem, true);
        StartTotem.Size = new Size(122, 17);
        StartTotem.TabIndex = 0;
        StartTotem.Text = "Start fight with totem";
        FastMelee.AutoSize = true;
        helpProvider_0.SetHelpKeyword(FastMelee, "Shaman.html#UseSwiftness");
        helpProvider_0.SetHelpNavigator(FastMelee, HelpNavigator.Topic);
        FastMelee.Location = new Point(12, 132);
        FastMelee.Name = "FastMelee";
        helpProvider_0.SetShowHelp(FastMelee, true);
        FastMelee.Size = new Size(77, 17);
        FastMelee.TabIndex = 4;
        FastMelee.Text = "Fast melee";
        ShockPull.AutoSize = true;
        helpProvider_0.SetHelpKeyword(ShockPull, "Shaman.html#UseSwiftness");
        helpProvider_0.SetHelpNavigator(ShockPull, HelpNavigator.Topic);
        ShockPull.Location = new Point(12, 159);
        ShockPull.Name = "ShockPull";
        helpProvider_0.SetShowHelp(ShockPull, true);
        ShockPull.Size = new Size(97, 17);
        ShockPull.TabIndex = 5;
        ShockPull.Text = "Pull with shock";
        UseHealTotem.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseHealTotem, "Shaman.html#UseHealTotem");
        helpProvider_0.SetHelpNavigator(UseHealTotem, HelpNavigator.Topic);
        UseHealTotem.Location = new Point(200, 52);
        UseHealTotem.Name = "UseHealTotem";
        helpProvider_0.SetShowHelp(UseHealTotem, true);
        UseHealTotem.Size = new Size(140, 17);
        UseHealTotem.TabIndex = 8;
        UseHealTotem.Text = "Use totem when healing";
        UseEarthbind.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseEarthbind, "Shaman.html#UseEarthbind");
        helpProvider_0.SetHelpNavigator(UseEarthbind, HelpNavigator.Topic);
        UseEarthbind.Location = new Point(12, 51);
        UseEarthbind.Name = "UseEarthbind";
        helpProvider_0.SetShowHelp(UseEarthbind, true);
        UseEarthbind.Size = new Size(109, 17);
        UseEarthbind.TabIndex = 1;
        UseEarthbind.Text = "Earthbind runners";
        helpProvider_0.SetHelpKeyword(ShockMana, "Shaman.html#ShockMana");
        helpProvider_0.SetHelpNavigator(ShockMana, HelpNavigator.Topic);
        ShockMana.Location = new Point(143, 77);
        ShockMana.Name = "ShockMana";
        helpProvider_0.SetShowHelp(ShockMana, true);
        ShockMana.Size = new Size(47, 20);
        ShockMana.TabIndex = 2;
        helpProvider_0.SetHelpKeyword(ShockLife, "Shaman.html#ShockLife");
        helpProvider_0.SetHelpNavigator(ShockLife, HelpNavigator.Topic);
        ShockLife.Location = new Point(143, 101);
        ShockLife.Name = "ShockLife";
        helpProvider_0.SetShowHelp(ShockLife, true);
        ShockLife.Size = new Size(47, 20);
        ShockLife.TabIndex = 3;
        ShockMode.DropDownStyle = ComboBoxStyle.DropDownList;
        helpProvider_0.SetHelpKeyword(ShockMode, "Shaman.html#ShockMode");
        helpProvider_0.SetHelpNavigator(ShockMode, HelpNavigator.Topic);
        ShockMode.Location = new Point(143, 51);
        ShockMode.Name = "ShockMode";
        helpProvider_0.SetShowHelp(ShockMode, true);
        ShockMode.Size = new Size(80, 21);
        ShockMode.TabIndex = 1;
        ShockMode.SelectedIndexChanged += ShockMode_SelectedIndexChanged;
        DualWield.AutoSize = true;
        helpProvider_0.SetHelpKeyword(DualWield, "Shaman.html#DualWield");
        helpProvider_0.SetHelpNavigator(DualWield, HelpNavigator.Topic);
        DualWield.Location = new Point(200, 79);
        DualWield.Name = "DualWield";
        helpProvider_0.SetShowHelp(DualWield, true);
        DualWield.Size = new Size(75, 17);
        DualWield.TabIndex = 9;
        DualWield.Text = "Dual wield";
        UseStormstrike.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseStormstrike, "Shaman.html#Stormstrike");
        helpProvider_0.SetHelpNavigator(UseStormstrike, HelpNavigator.Topic);
        UseStormstrike.Location = new Point(200, 105);
        UseStormstrike.Name = "UseStormstrike";
        helpProvider_0.SetShowHelp(UseStormstrike, true);
        UseStormstrike.Size = new Size(100, 17);
        UseStormstrike.TabIndex = 10;
        UseStormstrike.Text = "Use Stormstrike";
        ExtraShield.AutoSize = true;
        helpProvider_0.SetHelpKeyword(ExtraShield, "Shaman.html#ExtraShield");
        helpProvider_0.SetHelpNavigator(ExtraShield, HelpNavigator.Topic);
        ExtraShield.Location = new Point(200, 132);
        ExtraShield.Name = "ExtraShield";
        helpProvider_0.SetShowHelp(ExtraShield, true);
        ExtraShield.Size = new Size(128, 17);
        ExtraShield.TabIndex = 11;
        ExtraShield.Text = "Extra Lightning Shield";
        UseRage.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseRage, "Shaman.html#UseRage");
        helpProvider_0.SetHelpNavigator(UseRage, HelpNavigator.Topic);
        UseRage.Location = new Point(200, 159);
        UseRage.Name = "UseRage";
        helpProvider_0.SetShowHelp(UseRage, true);
        UseRage.Size = new Size(134, 17);
        UseRage.TabIndex = 12;
        UseRage.Text = "Use Shamanistic Rage";
        AvoidAdds.AutoSize = true;
        helpProvider_0.SetHelpKeyword(AvoidAdds, "Shaman.html#AvoidAdds");
        helpProvider_0.SetHelpNavigator(AvoidAdds, HelpNavigator.Topic);
        AvoidAdds.Location = new Point(200, 24);
        AvoidAdds.Name = "AvoidAdds";
        helpProvider_0.SetShowHelp(AvoidAdds, true);
        AvoidAdds.Size = new Size(120, 17);
        AvoidAdds.TabIndex = 7;
        AvoidAdds.Text = "Avoid possible adds";
        AvoidAdds.CheckedChanged += AvoidAdds_CheckedChanged;
        AvoidAddDistance.Enabled = false;
        helpProvider_0.SetHelpKeyword(AvoidAddDistance, "Paladin.html#AvoidAdds");
        helpProvider_0.SetHelpNavigator(AvoidAddDistance, HelpNavigator.Topic);
        AvoidAddDistance.Location = new Point(143, 126);
        AvoidAddDistance.Name = "AvoidAddDistance";
        helpProvider_0.SetShowHelp(AvoidAddDistance, true);
        AvoidAddDistance.Size = new Size(47, 20);
        AvoidAddDistance.TabIndex = 4;
        UseTotemicCall.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseTotemicCall, "Shaman.html#UseTotemicCall");
        helpProvider_0.SetHelpNavigator(UseTotemicCall, HelpNavigator.Topic);
        UseTotemicCall.Location = new Point(12, 185);
        UseTotemicCall.Name = "UseTotemicCall";
        helpProvider_0.SetShowHelp(UseTotemicCall, true);
        UseTotemicCall.Size = new Size(106, 17);
        UseTotemicCall.TabIndex = 6;
        UseTotemicCall.Text = "Use Totemic Call";
        UseStoneclaw.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseStoneclaw, "Shaman.html#UseStoneclaw");
        helpProvider_0.SetHelpNavigator(UseStoneclaw, HelpNavigator.Topic);
        UseStoneclaw.Location = new Point(200, 185);
        UseStoneclaw.Name = "UseStoneclaw";
        helpProvider_0.SetShowHelp(UseStoneclaw, true);
        UseStoneclaw.Size = new Size(139, 17);
        UseStoneclaw.TabIndex = 13;
        UseStoneclaw.Text = "Use Stoneclaw on adds";
        UseLightningShield.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseLightningShield, "Shaman.html#UseLightningShield");
        helpProvider_0.SetHelpNavigator(UseLightningShield, HelpNavigator.Topic);
        UseLightningShield.Location = new Point(12, 212);
        UseLightningShield.Name = "UseLightningShield";
        helpProvider_0.SetShowHelp(UseLightningShield, true);
        UseLightningShield.Size = new Size(123, 17);
        UseLightningShield.TabIndex = 14;
        UseLightningShield.Text = "Use Lightning Shield";
        label5.Location = new Point(37, 53);
        label5.Name = "label5";
        label5.Size = new Size(101, 15);
        label5.TabIndex = 17;
        label5.Text = "Shock mode:";
        label5.TextAlign = ContentAlignment.TopRight;
        label6.Location = new Point(34, 79);
        label6.Name = "label6";
        label6.Size = new Size(104, 15);
        label6.TabIndex = 20;
        label6.Text = "Shock mana:";
        label6.TextAlign = ContentAlignment.TopRight;
        label7.AutoSize = true;
        label7.Location = new Point(195, 79);
        label7.Name = "label7";
        label7.Size = new Size(43, 13);
        label7.TabIndex = 22;
        label7.Text = "percent";
        label8.Location = new Point(34, 105);
        label8.Name = "label8";
        label8.Size = new Size(104, 15);
        label8.TabIndex = 23;
        label8.Text = "Interrupt below:";
        label8.TextAlign = ContentAlignment.TopRight;
        label9.AutoSize = true;
        label9.Location = new Point(195, 104);
        label9.Name = "label9";
        label9.Size = new Size(20, 13);
        label9.TabIndex = 25;
        label9.Text = "life";
        label10.AutoSize = true;
        label10.Location = new Point(195, 128);
        label10.Name = "label10";
        label10.Size = new Size(32, 13);
        label10.TabIndex = 33;
        label10.Text = "yards";
        label11.AutoSize = true;
        label11.Location = new Point(36, 128);
        label11.Name = "label11";
        label11.Size = new Size(101, 13);
        label11.TabIndex = 31;
        label11.Text = "Avoid add distance:";
        label11.TextAlign = ContentAlignment.TopRight;
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(PullDistance);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(label11);
        groupBox1.Controls.Add(label10);
        groupBox1.Controls.Add(ShockMode);
        groupBox1.Controls.Add(AvoidAddDistance);
        groupBox1.Controls.Add(ShockMana);
        groupBox1.Controls.Add(label7);
        groupBox1.Controls.Add(label6);
        groupBox1.Controls.Add(ShockLife);
        groupBox1.Controls.Add(label9);
        groupBox1.Controls.Add(label8);
        groupBox1.Location = new Point(10, 10);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(252, 248);
        groupBox1.TabIndex = 36;
        groupBox1.TabStop = false;
        groupBox1.Text = "Limits";
        groupBox2.Controls.Add(UseLightningShield);
        groupBox2.Controls.Add(UseStoneclaw);
        groupBox2.Controls.Add(ShootRunners);
        groupBox2.Controls.Add(UseSwiftness);
        groupBox2.Controls.Add(UseTotemicCall);
        groupBox2.Controls.Add(StartTotem);
        groupBox2.Controls.Add(AvoidAdds);
        groupBox2.Controls.Add(FastMelee);
        groupBox2.Controls.Add(UseRage);
        groupBox2.Controls.Add(ShockPull);
        groupBox2.Controls.Add(ExtraShield);
        groupBox2.Controls.Add(UseHealTotem);
        groupBox2.Controls.Add(UseStormstrike);
        groupBox2.Controls.Add(UseEarthbind);
        groupBox2.Controls.Add(DualWield);
        groupBox2.Location = new Point(267, 10);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(468, 248);
        groupBox2.TabIndex = 37;
        groupBox2.TabStop = false;
        groupBox2.Text = "Options";
        AutoScaleBaseSize = new Size(5, 13);
        ClientSize = new Size(767, 313);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        Controls.Add(MyHelpButton);
        Controls.Add(MyCancelButton);
        Controls.Add(MyOKButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(ShamanConfig);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Config: Shaman";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ResumeLayout(false);
    }

    private void MyOKButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        if (StartupClass.smethod_19(PullDistance.Text))
            GClass61.gclass61_0.method_0("Shaman.PullDistance", PullDistance.Text);
        if (StartupClass.smethod_19(ShockMana.Text))
            GClass61.gclass61_0.method_0("Shaman.ShockMana", ShockMana.Text);
        if (StartupClass.smethod_19(ShockLife.Text))
            GClass61.gclass61_0.method_0("Shaman.ShockLife", ShockLife.Text);
        if (StartupClass.smethod_19(AvoidAddDistance.Text))
            GClass61.gclass61_0.method_0("Shaman.AvoidAddDistance", AvoidAddDistance.Text);
        GClass61.gclass61_0.method_0("Shaman.ShootRunners", ShootRunners.Checked.ToString());
        GClass61.gclass61_0.method_0("Shaman.UseSwiftness", UseSwiftness.Checked.ToString());
        GClass61.gclass61_0.method_0("Shaman.StartTotem", StartTotem.Checked.ToString());
        GClass61.gclass61_0.method_0("Shaman.ShockPull", ShockPull.Checked.ToString());
        GClass61.gclass61_0.method_0("Shaman.UseHealTotem", UseHealTotem.Checked.ToString());
        GClass61.gclass61_0.method_0("Shaman.UseEarthbind", UseEarthbind.Checked.ToString());
        GClass61.gclass61_0.method_0("Shaman.FastMelee", FastMelee.Checked.ToString());
        GClass61.gclass61_0.method_0("Shaman.DualWield", DualWield.Checked.ToString());
        GClass61.gclass61_0.method_0("Shaman.ExtraShield", ExtraShield.Checked.ToString());
        GClass61.gclass61_0.method_0("Shaman.UseStormstrike", UseStormstrike.Checked.ToString());
        GClass61.gclass61_0.method_0("Shaman.UseTotemicCall", UseTotemicCall.Checked.ToString());
        GClass61.gclass61_0.method_0("Shaman.UseLightningShield", UseLightningShield.Checked.ToString());
        GClass61.gclass61_0.method_0("Shaman.UseRage", UseRage.Checked.ToString());
        GClass61.gclass61_0.method_0("Shaman.UseStoneclaw", UseStoneclaw.Checked.ToString());
        GClass61.gclass61_0.method_0("Shaman.AvoidAdds", AvoidAdds.Checked.ToString());
        switch (ShockMode.SelectedIndex)
        {
            case 0:
                GClass61.gclass61_0.method_0("Shaman.ShockMode", "Spam");
                break;
            case 1:
                GClass61.gclass61_0.method_0("Shaman.ShockMode", "Runners");
                break;
            case 2:
                GClass61.gclass61_0.method_0("Shaman.ShockMode", "Interrupt");
                break;
        }
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "Shaman.html");
    }

    private void ShockMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ShockMode.SelectedItem.ToString() == "Interrupt")
        {
            ShockLife.Enabled = true;
            ShockMana.Enabled = false;
        }
        else
        {
            ShockLife.Enabled = false;
            ShockMana.Enabled = true;
        }
    }

    private void AvoidAdds_CheckedChanged(object sender, EventArgs e)
    {
        AvoidAddDistance.Enabled = AvoidAdds.Checked;
    }
}