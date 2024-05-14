// Decompiled with JetBrains decompiler
// Type: HunterConfig
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class HunterConfig : Form
{
    private CheckBox ApproachPull;
    private CheckBox AvoidMelee;
    private Container container_0;
    private CheckBox DotShot;
    private CheckBox FeedMacro;
    private HelpProvider helpProvider_0;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private TextBox MinHappiness;
    private Button MyCancelButton;
    private Button MyHelpButton;
    private Button MyOKButton;
    private CheckBox NoPet;
    private CheckBox PetAttack;
    private TextBox PullDistance;
    private CheckBox SeparateGroups;
    private CheckBox SetTrap;
    private CheckBox TrapAdds;
    private CheckBox UseIntimidate;
    private CheckBox UseViper;
    private CheckBox UseWrath;

    public HunterConfig()
    {
        InitializeComponent();
        PullDistance.Text = GClass61.gclass61_0.method_2("Hunter.PullDistance");
        MinHappiness.Text = GClass61.gclass61_0.method_2("Hunter.MinHappiness");
        DotShot.Checked = GClass61.gclass61_0.method_5("Hunter.DotShot");
        UseIntimidate.Checked = GClass61.gclass61_0.method_5("Hunter.UseIntimidate");
        UseWrath.Checked = GClass61.gclass61_0.method_5("Hunter.UseWrath");
        ApproachPull.Checked = GClass61.gclass61_0.method_5("Hunter.ApproachPull");
        SetTrap.Checked = GClass61.gclass61_0.method_5("Hunter.SetTrap");
        SeparateGroups.Checked = GClass61.gclass61_0.method_5("Hunter.SeparateGroups");
        PetAttack.Checked = GClass61.gclass61_0.method_5("Hunter.PetAttack");
        AvoidMelee.Checked = GClass61.gclass61_0.method_5("Hunter.AvoidMelee");
        NoPet.Checked = GClass61.gclass61_0.method_5("Hunter.NoPet");
        TrapAdds.Checked = GClass61.gclass61_0.method_5("Hunter.TrapAdds");
        FeedMacro.Checked = GClass61.gclass61_0.method_5("Hunter.FeedMacro");
        UseViper.Checked = GClass61.gclass61_0.method_5("Hunter.UseViper");
        GClass30.smethod_3(this, "Hunter");
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
        DotShot = new CheckBox();
        ApproachPull = new CheckBox();
        MinHappiness = new TextBox();
        SetTrap = new CheckBox();
        TrapAdds = new CheckBox();
        UseIntimidate = new CheckBox();
        UseWrath = new CheckBox();
        SeparateGroups = new CheckBox();
        PetAttack = new CheckBox();
        AvoidMelee = new CheckBox();
        NoPet = new CheckBox();
        label1 = new Label();
        label2 = new Label();
        FeedMacro = new CheckBox();
        UseViper = new CheckBox();
        SuspendLayout();
        MyOKButton.Location = new Point(25, 341);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(63, 24);
        MyOKButton.TabIndex = 11;
        MyOKButton.Text = "OK";
        MyOKButton.Click += MyOKButton_Click;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(105, 341);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(63, 24);
        MyCancelButton.TabIndex = 12;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.Click += MyCancelButton_Click;
        label3.Location = new Point(8, 8);
        label3.Name = "label3";
        label3.Size = new Size(72, 16);
        label3.TabIndex = 7;
        label3.Text = "Pull distance:";
        label3.TextAlign = ContentAlignment.MiddleRight;
        helpProvider_0.SetHelpKeyword(PullDistance, "Hunter.html#PullDistance");
        helpProvider_0.SetHelpNavigator(PullDistance, HelpNavigator.Topic);
        PullDistance.Location = new Point(88, 8);
        PullDistance.Name = "PullDistance";
        helpProvider_0.SetShowHelp(PullDistance, true);
        PullDistance.Size = new Size(48, 20);
        PullDistance.TabIndex = 0;
        label4.Location = new Point(144, 8);
        label4.Name = "label4";
        label4.Size = new Size(56, 16);
        label4.TabIndex = 9;
        label4.Text = "yards";
        label4.TextAlign = ContentAlignment.MiddleLeft;
        MyHelpButton.Location = new Point(185, 341);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(63, 24);
        MyHelpButton.TabIndex = 13;
        MyHelpButton.Text = "Help";
        MyHelpButton.Click += MyHelpButton_Click;
        helpProvider_0.HelpNamespace = "Glider.chm";
        DotShot.AutoSize = true;
        helpProvider_0.SetHelpKeyword(DotShot, "Hunter.html#TwoShotPull");
        helpProvider_0.SetHelpNavigator(DotShot, HelpNavigator.Topic);
        DotShot.Location = new Point(53, 122);
        DotShot.Name = "DotShot";
        helpProvider_0.SetShowHelp(DotShot, true);
        DotShot.Size = new Size(89, 17);
        DotShot.TabIndex = 3;
        DotShot.Text = "Two-shot pull";
        ApproachPull.AutoSize = true;
        helpProvider_0.SetHelpKeyword(ApproachPull, "Hunter.html#ApproachPull");
        helpProvider_0.SetHelpNavigator(ApproachPull, HelpNavigator.Topic);
        ApproachPull.Location = new Point(53, 143);
        ApproachPull.Name = "ApproachPull";
        helpProvider_0.SetShowHelp(ApproachPull, true);
        ApproachPull.Size = new Size(129, 17);
        ApproachPull.TabIndex = 4;
        ApproachPull.Text = "Approach on long pull";
        helpProvider_0.SetHelpKeyword(MinHappiness, "Hunter.html#MinHappiness");
        helpProvider_0.SetHelpNavigator(MinHappiness, HelpNavigator.Topic);
        MinHappiness.Location = new Point(88, 32);
        MinHappiness.Name = "MinHappiness";
        helpProvider_0.SetShowHelp(MinHappiness, true);
        MinHappiness.Size = new Size(48, 20);
        MinHappiness.TabIndex = 1;
        SetTrap.AutoSize = true;
        helpProvider_0.SetHelpKeyword(SetTrap, "Hunter.html#SetTrap");
        helpProvider_0.SetHelpNavigator(SetTrap, HelpNavigator.Topic);
        SetTrap.Location = new Point(53, 101);
        SetTrap.Name = "SetTrap";
        helpProvider_0.SetShowHelp(SetTrap, true);
        SetTrap.Size = new Size(115, 17);
        SetTrap.TabIndex = 2;
        SetTrap.Text = "Set trap before pull";
        TrapAdds.AutoSize = true;
        helpProvider_0.SetHelpKeyword(TrapAdds, "Hunter.html#TrapAdds");
        TrapAdds.Location = new Point(53, 164);
        TrapAdds.Name = "TrapAdds";
        helpProvider_0.SetShowHelp(TrapAdds, true);
        TrapAdds.Size = new Size(143, 17);
        TrapAdds.TabIndex = 4;
        TrapAdds.Text = "Trap additional attackers";
        TrapAdds.UseVisualStyleBackColor = true;
        UseIntimidate.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseIntimidate, "Hunter.html#UseIntimidate");
        helpProvider_0.SetHelpNavigator(UseIntimidate, HelpNavigator.Topic);
        UseIntimidate.Location = new Point(53, 185);
        UseIntimidate.Name = "UseIntimidate";
        helpProvider_0.SetShowHelp(UseIntimidate, true);
        UseIntimidate.Size = new Size(101, 17);
        UseIntimidate.TabIndex = 6;
        UseIntimidate.Text = "Use Intimidation";
        UseWrath.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseWrath, "Hunter.html#UseWrath");
        helpProvider_0.SetHelpNavigator(UseWrath, HelpNavigator.Topic);
        UseWrath.Location = new Point(53, 206);
        UseWrath.Name = "UseWrath";
        helpProvider_0.SetShowHelp(UseWrath, true);
        UseWrath.Size = new Size(111, 17);
        UseWrath.TabIndex = 7;
        UseWrath.Text = "Use Bestial Wrath";
        SeparateGroups.AutoSize = true;
        helpProvider_0.SetHelpKeyword(SeparateGroups, "Hunter.html#SeparateGroups");
        helpProvider_0.SetHelpNavigator(SeparateGroups, HelpNavigator.Topic);
        SeparateGroups.Location = new Point(53, 227);
        SeparateGroups.Name = "SeparateGroups";
        helpProvider_0.SetShowHelp(SeparateGroups, true);
        SeparateGroups.Size = new Size(132, 17);
        SeparateGroups.TabIndex = 8;
        SeparateGroups.Text = "Try to separate groups";
        PetAttack.AutoSize = true;
        helpProvider_0.SetHelpKeyword(PetAttack, "Hunter.html#PetAttack");
        helpProvider_0.SetHelpNavigator(PetAttack, HelpNavigator.Topic);
        PetAttack.Location = new Point(53, 248);
        PetAttack.Name = "PetAttack";
        helpProvider_0.SetShowHelp(PetAttack, true);
        PetAttack.Size = new Size(75, 17);
        PetAttack.TabIndex = 9;
        PetAttack.Text = "Pet attack";
        AvoidMelee.AutoSize = true;
        helpProvider_0.SetHelpKeyword(AvoidMelee, "Hunter.html#AvoidMelee");
        helpProvider_0.SetHelpNavigator(AvoidMelee, HelpNavigator.Topic);
        AvoidMelee.Location = new Point(53, 269);
        AvoidMelee.Name = "AvoidMelee";
        helpProvider_0.SetShowHelp(AvoidMelee, true);
        AvoidMelee.Size = new Size(154, 17);
        AvoidMelee.TabIndex = 10;
        AvoidMelee.Text = "Avoid melee when possible";
        NoPet.AutoSize = true;
        helpProvider_0.SetHelpKeyword(NoPet, "Hunter.html#NoPet");
        helpProvider_0.SetHelpNavigator(NoPet, HelpNavigator.Topic);
        NoPet.Location = new Point(53, 290);
        NoPet.Name = "NoPet";
        helpProvider_0.SetShowHelp(NoPet, true);
        NoPet.Size = new Size(101, 17);
        NoPet.TabIndex = 16;
        NoPet.Text = "Play without pet";
        label1.Location = new Point(8, 32);
        label1.Name = "label1";
        label1.Size = new Size(72, 16);
        label1.TabIndex = 13;
        label1.Text = "Feed pet at:";
        label1.TextAlign = ContentAlignment.MiddleRight;
        label2.Location = new Point(144, 32);
        label2.Name = "label2";
        label2.Size = new Size(88, 16);
        label2.TabIndex = 15;
        label2.Text = "happiness %";
        label2.TextAlign = ContentAlignment.MiddleLeft;
        FeedMacro.AutoSize = true;
        FeedMacro.Location = new Point(53, 314);
        FeedMacro.Name = "FeedMacro";
        FeedMacro.Size = new Size(122, 17);
        FeedMacro.TabIndex = 17;
        FeedMacro.Text = "Feed pet with macro";
        FeedMacro.UseVisualStyleBackColor = true;
        UseViper.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseViper, "Hunter.html#SetTrap");
        helpProvider_0.SetHelpNavigator(UseViper, HelpNavigator.Topic);
        UseViper.Location = new Point(53, 78);
        UseViper.Name = "UseViper";
        helpProvider_0.SetShowHelp(UseViper, true);
        UseViper.Size = new Size(138, 17);
        UseViper.TabIndex = 18;
        UseViper.Text = "Use Aspect of the Viper";
        AutoScaleBaseSize = new Size(5, 13);
        ClientSize = new Size(273, 377);
        Controls.Add(UseViper);
        Controls.Add(FeedMacro);
        Controls.Add(TrapAdds);
        Controls.Add(NoPet);
        Controls.Add(AvoidMelee);
        Controls.Add(PetAttack);
        Controls.Add(SeparateGroups);
        Controls.Add(UseWrath);
        Controls.Add(UseIntimidate);
        Controls.Add(SetTrap);
        Controls.Add(label2);
        Controls.Add(MinHappiness);
        Controls.Add(PullDistance);
        Controls.Add(label1);
        Controls.Add(ApproachPull);
        Controls.Add(DotShot);
        Controls.Add(MyHelpButton);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(MyCancelButton);
        Controls.Add(MyOKButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(HunterConfig);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Config: Hunter";
        ResumeLayout(false);
        PerformLayout();
    }

    private void MyOKButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        GClass61.gclass61_0.method_0("Hunter.DotShot", DotShot.Checked.ToString());
        GClass61.gclass61_0.method_0("Hunter.ApproachPull", ApproachPull.Checked.ToString());
        GClass61.gclass61_0.method_0("Hunter.SetTrap", SetTrap.Checked.ToString());
        GClass61.gclass61_0.method_0("Hunter.UseIntimidate", UseIntimidate.Checked.ToString());
        GClass61.gclass61_0.method_0("Hunter.UseWrath", UseWrath.Checked.ToString());
        GClass61.gclass61_0.method_0("Hunter.SeparateGroups", SeparateGroups.Checked.ToString());
        GClass61.gclass61_0.method_0("Hunter.PetAttack", PetAttack.Checked.ToString());
        GClass61.gclass61_0.method_0("Hunter.AvoidMelee", AvoidMelee.Checked.ToString());
        GClass61.gclass61_0.method_0("Hunter.NoPet", NoPet.Checked.ToString());
        GClass61.gclass61_0.method_0("Hunter.TrapAdds", TrapAdds.Checked.ToString());
        GClass61.gclass61_0.method_0("Hunter.FeedMacro", FeedMacro.Checked.ToString());
        GClass61.gclass61_0.method_0("Hunter.UseViper", UseViper.Checked.ToString());
        if (StartupClass.smethod_19(PullDistance.Text))
            GClass61.gclass61_0.method_0("Hunter.PullDistance", PullDistance.Text);
        if (!StartupClass.smethod_19(MinHappiness.Text))
            return;
        GClass61.gclass61_0.method_0("Hunter.MinHappiness", MinHappiness.Text);
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "Hunter.html");
    }

    private void method_0(object sender, EventArgs e)
    {
    }
}