// Decompiled with JetBrains decompiler
// Type: ProfileProps
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Glider.Common.Objects;

public class ProfileProps : Form
{
    private CheckBox AllowShortCircuit;
    private TextBox AvoidList;
    private CheckBox Beach;
    private CheckBox BlacklistOn;
    private RadioButton Circle;
    private Button ClearGhostWaypoints;
    private Button ClearVendorWaypoints;
    private Button ClearWaypoints;
    private Container container_0;
    private TextBox FactionsBox;
    private CheckBox Fishing;
    private Label GhostWaypointsLabel;
    public GProfile gprofile_0;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private GroupBox groupBox3;
    private GroupBox groupBox4;
    private GroupBox groupBox5;
    private GroupBox groupBox6;
    private GroupBox groupBox7;
    private HelpProvider helpProvider_0;
    private CheckBox IgnoreAttackers;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private Label label77;
    private Label label8;
    private Label label9;
    private TextBox LureTimer;
    private TextBox MaxLevelBox;
    private TextBox MinLevelBox;
    private Button MyCancelButton;
    private Button MyHelpButton;
    private Button MyOKButton;
    private CheckBox NaturalRun;
    private CheckBox OneShot;
    private RadioButton OutAndBack;
    private CheckBox ReverseWaypoints;
    private CheckBox RunFromAvoids;
    private CheckBox SkipWaypoints;
    private CheckBox UseBreadcrumbs;
    private TextBox VendorAR;
    private TextBox VendorFW;
    private TextBox VendorRepair;
    private Label VendorWaypointsLabel;
    private CheckBox Wander;
    private Label WaypointsLabel;

    public ProfileProps(GProfile gprofile_1)
    {
        gprofile_0 = gprofile_1;
        InitializeComponent();
        if (gprofile_0 == null)
        {
            Text = "Create New Profile";
            Circle.Checked = true;
            NaturalRun.Checked = true;
        }
        else
        {
            Text = "Edit Profile";
            BlacklistOn.Checked = gprofile_1.BlacklistOn;
            NaturalRun.Checked = gprofile_1.NaturalRun;
            MinLevelBox.Text = method_1(gprofile_1.MinLevel);
            MaxLevelBox.Text = method_1(gprofile_1.MaxLevel);
            FactionsBox.Text = gprofile_1.GetFactionsAsString();
            if (gprofile_1.LureMinutes != 0)
                LureTimer.Text = gprofile_1.LureMinutes.ToString();
            Fishing.Checked = gprofile_1.Fishing;
            IgnoreAttackers.Checked = gprofile_1.IgnoreAttackers;
            ReverseWaypoints.Checked = gprofile_1.ReverseWaypoints;
            SkipWaypoints.Checked = gprofile_1.SkipWaypoints;
            Beach.Checked = gprofile_1.Beach;
            Wander.Checked = gprofile_1.Wander;
            RunFromAvoids.Checked = gprofile_1.RunFromAvoids;
            OneShot.Checked = gprofile_1.OneShot;
            UseBreadcrumbs.Checked = gprofile_1.UseBreadcrumbs;
            AllowShortCircuit.Checked = gprofile_1.AllowShortCircuit;
            if (gprofile_1.AvoidList != null)
            {
                foreach (var avoid in gprofile_1.AvoidList)
                {
                    var avoidList = AvoidList;
                    avoidList.Text = avoidList.Text + avoid + "\r\n";
                }

                AvoidList.Text = AvoidList.Text.Substring(0, AvoidList.Text.Length - 2);
            }

            if (gprofile_1.Reversible)
                OutAndBack.Checked = true;
            else
                Circle.Checked = true;
            if (gprofile_1.VendorAR != null)
                VendorAR.Text = gprofile_1.VendorAR;
            if (gprofile_1.VendorRepair != null)
                VendorRepair.Text = gprofile_1.VendorRepair;
            if (gprofile_1.VendorFW != null)
                VendorFW.Text = gprofile_1.VendorFW;
        }

        method_4();
        GClass30.smethod_3(this, nameof(ProfileProps));
        GProcessMemoryManipulator.smethod_48(this);
        if (gprofile_0 == null)
            return;
        if (gprofile_1.Waypoints.Count > 0)
        {
            WaypointsLabel.Text =
                GClass30.smethod_6("ProfileProps.WaypointsLabel.Contains", gprofile_1.Waypoints.Count);
            ClearWaypoints.Enabled = true;
        }

        if (gprofile_1.GhostWaypoints.Count > 0)
        {
            GhostWaypointsLabel.Text = GClass30.smethod_6("ProfileProps.GhostWaypointsLabel.Contains",
                gprofile_1.GhostWaypoints.Count);
            ClearGhostWaypoints.Enabled = true;
        }

        if (gprofile_1.VendorWaypoints.Count <= 0)
            return;
        VendorWaypointsLabel.Text =
            GClass30.smethod_6("ProfileProps.VendorWaypointsLabel.Contains", gprofile_1.VendorWaypoints.Count);
        ClearVendorWaypoints.Enabled = true;
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
        groupBox1 = new GroupBox();
        MaxLevelBox = new TextBox();
        MinLevelBox = new TextBox();
        label3 = new Label();
        label2 = new Label();
        groupBox2 = new GroupBox();
        label4 = new Label();
        FactionsBox = new TextBox();
        groupBox3 = new GroupBox();
        OutAndBack = new RadioButton();
        Circle = new RadioButton();
        WaypointsLabel = new Label();
        ClearWaypoints = new Button();
        groupBox4 = new GroupBox();
        ClearGhostWaypoints = new Button();
        GhostWaypointsLabel = new Label();
        MyHelpButton = new Button();
        helpProvider_0 = new HelpProvider();
        Fishing = new CheckBox();
        LureTimer = new TextBox();
        BlacklistOn = new CheckBox();
        NaturalRun = new CheckBox();
        SkipWaypoints = new CheckBox();
        ReverseWaypoints = new CheckBox();
        Beach = new CheckBox();
        Wander = new CheckBox();
        RunFromAvoids = new CheckBox();
        OneShot = new CheckBox();
        IgnoreAttackers = new CheckBox();
        UseBreadcrumbs = new CheckBox();
        AllowShortCircuit = new CheckBox();
        VendorFW = new TextBox();
        VendorAR = new TextBox();
        groupBox5 = new GroupBox();
        label77 = new Label();
        label5 = new Label();
        groupBox6 = new GroupBox();
        AvoidList = new TextBox();
        label6 = new Label();
        groupBox7 = new GroupBox();
        label8 = new Label();
        label7 = new Label();
        ClearVendorWaypoints = new Button();
        VendorWaypointsLabel = new Label();
        label9 = new Label();
        VendorRepair = new TextBox();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox3.SuspendLayout();
        groupBox4.SuspendLayout();
        groupBox5.SuspendLayout();
        groupBox6.SuspendLayout();
        groupBox7.SuspendLayout();
        SuspendLayout();
        MyOKButton.Location = new Point(310, 566);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(75, 23);
        MyOKButton.TabIndex = 0;
        MyOKButton.Text = "OK";
        MyOKButton.Click += MyOKButton_Click;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(403, 566);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(75, 23);
        MyCancelButton.TabIndex = 1;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.Click += MyCancelButton_Click;
        label1.Location = new Point(8, 8);
        label1.Name = "label1";
        label1.Size = new Size(519, 40);
        label1.TabIndex = 2;
        label1.Text =
            "A profile is information that Glider uses to automatically target and kill monsters.  For more information on profiles, click the Help button below.";
        groupBox1.Controls.Add(MaxLevelBox);
        groupBox1.Controls.Add(MinLevelBox);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Location = new Point(8, 56);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(182, 54);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Levels";
        helpProvider_0.SetHelpKeyword(MaxLevelBox, "Profiles.html#Levels");
        helpProvider_0.SetHelpNavigator(MaxLevelBox, HelpNavigator.Topic);
        MaxLevelBox.Location = new Point(134, 26);
        MaxLevelBox.Name = "MaxLevelBox";
        helpProvider_0.SetShowHelp(MaxLevelBox, true);
        MaxLevelBox.Size = new Size(40, 20);
        MaxLevelBox.TabIndex = 1;
        helpProvider_0.SetHelpKeyword(MinLevelBox, "Profiles.html#Levels");
        helpProvider_0.SetHelpNavigator(MinLevelBox, HelpNavigator.Topic);
        MinLevelBox.Location = new Point(39, 24);
        MinLevelBox.Name = "MinLevelBox";
        helpProvider_0.SetShowHelp(MinLevelBox, true);
        MinLevelBox.Size = new Size(40, 20);
        MinLevelBox.TabIndex = 0;
        label3.Location = new Point(102, 26);
        label3.Name = "label3";
        label3.Size = new Size(32, 16);
        label3.TabIndex = 1;
        label3.Text = "Max:";
        label3.TextAlign = ContentAlignment.MiddleRight;
        label2.Location = new Point(7, 24);
        label2.Name = "label2";
        label2.Size = new Size(32, 16);
        label2.TabIndex = 0;
        label2.Text = "Min:";
        label2.TextAlign = ContentAlignment.MiddleRight;
        groupBox2.Controls.Add(label4);
        groupBox2.Controls.Add(FactionsBox);
        groupBox2.Location = new Point(195, 56);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(173, 54);
        groupBox2.TabIndex = 1;
        groupBox2.TabStop = false;
        groupBox2.Text = "Factions";
        label4.AutoSize = true;
        label4.Location = new Point(8, 27);
        label4.Name = "label4";
        label4.Size = new Size(50, 13);
        label4.TabIndex = 0;
        label4.Text = "Factions:";
        label4.TextAlign = ContentAlignment.TopRight;
        helpProvider_0.SetHelpKeyword(FactionsBox, "Profiles.html#Faction");
        helpProvider_0.SetHelpNavigator(FactionsBox, HelpNavigator.Topic);
        FactionsBox.Location = new Point(67, 23);
        FactionsBox.Name = "FactionsBox";
        helpProvider_0.SetShowHelp(FactionsBox, true);
        FactionsBox.Size = new Size(101, 20);
        FactionsBox.TabIndex = 0;
        groupBox3.Controls.Add(OutAndBack);
        groupBox3.Controls.Add(Circle);
        groupBox3.Controls.Add(WaypointsLabel);
        groupBox3.Controls.Add(ClearWaypoints);
        groupBox3.Location = new Point(7, 328);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(276, 138);
        groupBox3.TabIndex = 3;
        groupBox3.TabStop = false;
        groupBox3.Text = "Waypoints";
        OutAndBack.Location = new Point(32, 64);
        OutAndBack.Name = "OutAndBack";
        OutAndBack.Size = new Size(136, 16);
        OutAndBack.TabIndex = 1;
        OutAndBack.Text = "Wander out-and-back";
        OutAndBack.CheckedChanged += OutAndBack_CheckedChanged;
        Circle.Location = new Point(32, 48);
        Circle.Name = "Circle";
        Circle.Size = new Size(96, 16);
        Circle.TabIndex = 0;
        Circle.Text = "Wander circle";
        Circle.CheckedChanged += Circle_CheckedChanged;
        WaypointsLabel.AutoSize = true;
        WaypointsLabel.ForeColor = SystemColors.Highlight;
        WaypointsLabel.Location = new Point(16, 24);
        WaypointsLabel.Name = "WaypointsLabel";
        WaypointsLabel.Size = new Size(163, 13);
        WaypointsLabel.TabIndex = 0;
        WaypointsLabel.Text = "This profile has no waypoints yet.";
        ClearWaypoints.CausesValidation = false;
        ClearWaypoints.Enabled = false;
        ClearWaypoints.Location = new Point(195, 104);
        ClearWaypoints.Name = "ClearWaypoints";
        ClearWaypoints.Size = new Size(75, 23);
        ClearWaypoints.TabIndex = 2;
        ClearWaypoints.Text = "Clear";
        ClearWaypoints.Click += ClearWaypoints_Click;
        groupBox4.Controls.Add(ClearGhostWaypoints);
        groupBox4.Controls.Add(GhostWaypointsLabel);
        groupBox4.Location = new Point(7, 472);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new Size(276, 88);
        groupBox4.TabIndex = 4;
        groupBox4.TabStop = false;
        groupBox4.Text = "Ghost Waypoints";
        ClearGhostWaypoints.CausesValidation = false;
        ClearGhostWaypoints.Enabled = false;
        ClearGhostWaypoints.Location = new Point(195, 59);
        ClearGhostWaypoints.Name = "ClearGhostWaypoints";
        ClearGhostWaypoints.Size = new Size(75, 23);
        ClearGhostWaypoints.TabIndex = 1;
        ClearGhostWaypoints.Text = "Clear";
        ClearGhostWaypoints.Click += ClearGhostWaypoints_Click;
        GhostWaypointsLabel.AutoSize = true;
        GhostWaypointsLabel.ForeColor = Color.FromArgb(192, 0, 0);
        GhostWaypointsLabel.Location = new Point(17, 21);
        GhostWaypointsLabel.Name = "GhostWaypointsLabel";
        GhostWaypointsLabel.Size = new Size(192, 13);
        GhostWaypointsLabel.TabIndex = 0;
        GhostWaypointsLabel.Text = "This profile has no ghost waypoints yet.";
        MyHelpButton.Location = new Point(496, 566);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(64, 24);
        MyHelpButton.TabIndex = 2;
        MyHelpButton.Text = "&Help";
        MyHelpButton.Click += MyHelpButton_Click;
        helpProvider_0.HelpNamespace = ".\\Glider.chm";
        Fishing.AutoSize = true;
        helpProvider_0.SetHelpKeyword(Fishing, "Profiles.html#Fishing");
        helpProvider_0.SetHelpNavigator(Fishing, HelpNavigator.Topic);
        Fishing.Location = new Point(197, 114);
        Fishing.Name = "Fishing";
        helpProvider_0.SetShowHelp(Fishing, true);
        Fishing.Size = new Size(59, 17);
        Fishing.TabIndex = 6;
        Fishing.Text = "Fishing";
        Fishing.CheckedChanged += Fishing_CheckedChanged;
        helpProvider_0.SetHelpKeyword(LureTimer, "Profiles.html#Levels");
        helpProvider_0.SetHelpNavigator(LureTimer, HelpNavigator.Topic);
        LureTimer.Location = new Point(byte.MaxValue, 136);
        LureTimer.Name = "LureTimer";
        helpProvider_0.SetShowHelp(LureTimer, true);
        LureTimer.Size = new Size(33, 20);
        LureTimer.TabIndex = 7;
        BlacklistOn.AutoSize = true;
        helpProvider_0.SetHelpKeyword(BlacklistOn, "Profiles.html#Blacklist");
        helpProvider_0.SetHelpNavigator(BlacklistOn, HelpNavigator.Topic);
        BlacklistOn.Location = new Point(16, 23);
        BlacklistOn.Name = "BlacklistOn";
        helpProvider_0.SetShowHelp(BlacklistOn, true);
        BlacklistOn.Size = new Size(106, 17);
        BlacklistOn.TabIndex = 0;
        BlacklistOn.Text = "Blacklist enabled";
        NaturalRun.AutoSize = true;
        helpProvider_0.SetHelpKeyword(NaturalRun, "Profiles.html#NaturalRun");
        helpProvider_0.SetHelpNavigator(NaturalRun, HelpNavigator.Topic);
        NaturalRun.Location = new Point(16, 46);
        NaturalRun.Name = "NaturalRun";
        helpProvider_0.SetShowHelp(NaturalRun, true);
        NaturalRun.Size = new Size(98, 17);
        NaturalRun.TabIndex = 1;
        NaturalRun.Text = "Natural running";
        SkipWaypoints.AutoSize = true;
        helpProvider_0.SetHelpKeyword(SkipWaypoints, "Profiles.html#Blacklist");
        helpProvider_0.SetHelpNavigator(SkipWaypoints, HelpNavigator.Topic);
        SkipWaypoints.Location = new Point(16, 91);
        SkipWaypoints.Name = "SkipWaypoints";
        helpProvider_0.SetShowHelp(SkipWaypoints, true);
        SkipWaypoints.Size = new Size(97, 17);
        SkipWaypoints.TabIndex = 3;
        SkipWaypoints.Text = "Skip waypoints";
        ReverseWaypoints.AutoSize = true;
        helpProvider_0.SetHelpKeyword(ReverseWaypoints, "Profiles.html#Blacklist");
        helpProvider_0.SetHelpNavigator(ReverseWaypoints, HelpNavigator.Topic);
        ReverseWaypoints.Location = new Point(16, 68);
        ReverseWaypoints.Name = "ReverseWaypoints";
        helpProvider_0.SetShowHelp(ReverseWaypoints, true);
        ReverseWaypoints.Size = new Size(116, 17);
        ReverseWaypoints.TabIndex = 2;
        ReverseWaypoints.Text = "Reverse waypoints";
        Beach.AutoSize = true;
        helpProvider_0.SetHelpKeyword(Beach, "Profiles.html#Beach");
        helpProvider_0.SetHelpNavigator(Beach, HelpNavigator.Topic);
        Beach.Location = new Point(16, 114);
        Beach.Name = "Beach";
        helpProvider_0.SetShowHelp(Beach, true);
        Beach.Size = new Size(88, 17);
        Beach.TabIndex = 4;
        Beach.Text = "Beach profile";
        Wander.AutoSize = true;
        helpProvider_0.SetHelpKeyword(Wander, "Profiles.html#Wander");
        helpProvider_0.SetHelpNavigator(Wander, HelpNavigator.Topic);
        Wander.Location = new Point(16, 136);
        Wander.Name = "Wander";
        helpProvider_0.SetShowHelp(Wander, true);
        Wander.Size = new Size(64, 17);
        Wander.TabIndex = 5;
        Wander.Text = "Wander";
        RunFromAvoids.AutoSize = true;
        helpProvider_0.SetHelpKeyword(RunFromAvoids, "Profiles.html#RunFromAvoids");
        helpProvider_0.SetHelpNavigator(RunFromAvoids, HelpNavigator.Topic);
        RunFromAvoids.Location = new Point(197, 23);
        RunFromAvoids.Name = "RunFromAvoids";
        helpProvider_0.SetShowHelp(RunFromAvoids, true);
        RunFromAvoids.Size = new Size(103, 17);
        RunFromAvoids.TabIndex = 8;
        RunFromAvoids.Text = "Run from avoids";
        OneShot.AutoSize = true;
        helpProvider_0.SetHelpKeyword(OneShot, "Profiles.html#OneShot");
        helpProvider_0.SetHelpNavigator(OneShot, HelpNavigator.Topic);
        OneShot.Location = new Point(197, 46);
        OneShot.Name = "OneShot";
        helpProvider_0.SetShowHelp(OneShot, true);
        OneShot.Size = new Size(118, 17);
        OneShot.TabIndex = 10;
        OneShot.Text = "Stop after one pass";
        OneShot.CheckedChanged += OneShot_CheckedChanged;
        IgnoreAttackers.AutoSize = true;
        helpProvider_0.SetHelpKeyword(IgnoreAttackers, "Profiles.html#IgnoreAttackers");
        helpProvider_0.SetHelpNavigator(IgnoreAttackers, HelpNavigator.Topic);
        IgnoreAttackers.Location = new Point(197, 68);
        IgnoreAttackers.Name = "IgnoreAttackers";
        helpProvider_0.SetShowHelp(IgnoreAttackers, true);
        IgnoreAttackers.Size = new Size(103, 17);
        IgnoreAttackers.TabIndex = 11;
        IgnoreAttackers.Text = "Ignore attackers";
        UseBreadcrumbs.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseBreadcrumbs, "Profiles.html#Breadcrumbs");
        helpProvider_0.SetHelpNavigator(UseBreadcrumbs, HelpNavigator.Topic);
        UseBreadcrumbs.Location = new Point(16, 159);
        UseBreadcrumbs.Name = "UseBreadcrumbs";
        helpProvider_0.SetShowHelp(UseBreadcrumbs, true);
        UseBreadcrumbs.Size = new Size(88, 17);
        UseBreadcrumbs.TabIndex = 12;
        UseBreadcrumbs.Text = "Breadcrumbs";
        UseBreadcrumbs.CheckedChanged += UseBreadcrumbs_CheckedChanged;
        AllowShortCircuit.AutoSize = true;
        AllowShortCircuit.Enabled = false;
        helpProvider_0.SetHelpKeyword(AllowShortCircuit, "Profiles.html#Breadcrumbs");
        helpProvider_0.SetHelpNavigator(AllowShortCircuit, HelpNavigator.Topic);
        AllowShortCircuit.Location = new Point(16, 183);
        AllowShortCircuit.Name = "AllowShortCircuit";
        helpProvider_0.SetShowHelp(AllowShortCircuit, true);
        AllowShortCircuit.Size = new Size(134, 17);
        AllowShortCircuit.TabIndex = 13;
        AllowShortCircuit.Text = "Short-circuit on resume";
        helpProvider_0.SetHelpKeyword(VendorFW, "Profiles.html#Levels");
        helpProvider_0.SetHelpNavigator(VendorFW, HelpNavigator.Topic);
        VendorFW.Location = new Point(37, 66);
        VendorFW.Name = "VendorFW";
        helpProvider_0.SetShowHelp(VendorFW, true);
        VendorFW.Size = new Size(178, 20);
        VendorFW.TabIndex = 4;
        helpProvider_0.SetHelpKeyword(VendorAR, "Profiles.html#Levels");
        helpProvider_0.SetHelpNavigator(VendorAR, HelpNavigator.Topic);
        VendorAR.Location = new Point(37, 114);
        VendorAR.Name = "VendorAR";
        helpProvider_0.SetShowHelp(VendorAR, true);
        VendorAR.Size = new Size(178, 20);
        VendorAR.TabIndex = 5;
        groupBox5.Controls.Add(AllowShortCircuit);
        groupBox5.Controls.Add(UseBreadcrumbs);
        groupBox5.Controls.Add(IgnoreAttackers);
        groupBox5.Controls.Add(OneShot);
        groupBox5.Controls.Add(RunFromAvoids);
        groupBox5.Controls.Add(Wander);
        groupBox5.Controls.Add(Beach);
        groupBox5.Controls.Add(ReverseWaypoints);
        groupBox5.Controls.Add(SkipWaypoints);
        groupBox5.Controls.Add(NaturalRun);
        groupBox5.Controls.Add(BlacklistOn);
        groupBox5.Controls.Add(label77);
        groupBox5.Controls.Add(LureTimer);
        groupBox5.Controls.Add(label5);
        groupBox5.Controls.Add(Fishing);
        groupBox5.Location = new Point(11, 115);
        groupBox5.Name = "groupBox5";
        groupBox5.Size = new Size(357, 207);
        groupBox5.TabIndex = 2;
        groupBox5.TabStop = false;
        groupBox5.Text = "Options";
        label77.Location = new Point(295, 136);
        label77.Name = "label77";
        label77.Size = new Size(47, 17);
        label77.TabIndex = 3;
        label77.Text = "minutes";
        label77.TextAlign = ContentAlignment.MiddleLeft;
        label5.Location = new Point(182, 136);
        label5.Name = "label5";
        label5.Size = new Size(63, 17);
        label5.TabIndex = 1;
        label5.Text = "Lure every:";
        label5.TextAlign = ContentAlignment.MiddleRight;
        groupBox6.Controls.Add(AvoidList);
        groupBox6.Controls.Add(label6);
        groupBox6.Location = new Point(373, 55);
        groupBox6.Name = "groupBox6";
        groupBox6.Size = new Size(187, 267);
        groupBox6.TabIndex = 5;
        groupBox6.TabStop = false;
        groupBox6.Text = "Avoid";
        AvoidList.AcceptsReturn = true;
        AvoidList.Location = new Point(13, 49);
        AvoidList.Multiline = true;
        AvoidList.Name = "AvoidList";
        AvoidList.Size = new Size(160, 201);
        AvoidList.TabIndex = 0;
        label6.Location = new Point(13, 28);
        label6.Name = "label6";
        label6.Size = new Size(140, 14);
        label6.TabIndex = 0;
        label6.Text = "Monster names containing:";
        groupBox7.Controls.Add(VendorRepair);
        groupBox7.Controls.Add(label9);
        groupBox7.Controls.Add(VendorAR);
        groupBox7.Controls.Add(VendorFW);
        groupBox7.Controls.Add(label8);
        groupBox7.Controls.Add(label7);
        groupBox7.Controls.Add(ClearVendorWaypoints);
        groupBox7.Controls.Add(VendorWaypointsLabel);
        groupBox7.Location = new Point(289, 328);
        groupBox7.Name = "groupBox7";
        groupBox7.Size = new Size(271, 232);
        groupBox7.TabIndex = 5;
        groupBox7.TabStop = false;
        groupBox7.Text = "Vendor Waypoints";
        label8.AutoSize = true;
        label8.Location = new Point(34, 98);
        label8.Name = "label8";
        label8.Size = new Size(106, 13);
        label8.TabIndex = 3;
        label8.Text = "Ammo/repair vendor:";
        label8.TextAlign = ContentAlignment.TopRight;
        label7.AutoSize = true;
        label7.Location = new Point(34, 48);
        label7.Name = "label7";
        label7.Size = new Size(101, 13);
        label7.TabIndex = 2;
        label7.Text = "Food/water vendor:";
        label7.TextAlign = ContentAlignment.TopRight;
        ClearVendorWaypoints.CausesValidation = false;
        ClearVendorWaypoints.Enabled = false;
        ClearVendorWaypoints.Location = new Point(190, 203);
        ClearVendorWaypoints.Name = "ClearVendorWaypoints";
        ClearVendorWaypoints.Size = new Size(75, 23);
        ClearVendorWaypoints.TabIndex = 1;
        ClearVendorWaypoints.Text = "Clear";
        ClearVendorWaypoints.Click += ClearVendorWaypoints_Click;
        VendorWaypointsLabel.AutoSize = true;
        VendorWaypointsLabel.ForeColor = Color.Green;
        VendorWaypointsLabel.Location = new Point(16, 24);
        VendorWaypointsLabel.Name = "VendorWaypointsLabel";
        VendorWaypointsLabel.Size = new Size(199, 13);
        VendorWaypointsLabel.TabIndex = 0;
        VendorWaypointsLabel.Text = "This profile has no vendor waypoints yet.";
        label9.AutoSize = true;
        label9.Location = new Point(34, 149);
        label9.Name = "label9";
        label9.Size = new Size(117, 13);
        label9.TabIndex = 6;
        label9.Text = "Alternate repair vendor:";
        label9.TextAlign = ContentAlignment.TopRight;
        helpProvider_0.SetHelpKeyword(VendorRepair, "Profiles.html#Levels");
        helpProvider_0.SetHelpNavigator(VendorRepair, HelpNavigator.Topic);
        VendorRepair.Location = new Point(37, 165);
        VendorRepair.Name = "VendorRepair";
        helpProvider_0.SetShowHelp(VendorRepair, true);
        VendorRepair.Size = new Size(178, 20);
        VendorRepair.TabIndex = 7;
        AcceptButton = MyOKButton;
        AutoScaleBaseSize = new Size(5, 13);
        CancelButton = MyCancelButton;
        ClientSize = new Size(572, 597);
        Controls.Add(groupBox7);
        Controls.Add(groupBox6);
        Controls.Add(groupBox5);
        Controls.Add(MyHelpButton);
        Controls.Add(groupBox4);
        Controls.Add(groupBox3);
        Controls.Add(groupBox2);
        Controls.Add(groupBox1);
        Controls.Add(label1);
        Controls.Add(MyCancelButton);
        Controls.Add(MyOKButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(ProfileProps);
        StartPosition = FormStartPosition.CenterScreen;
        Text = nameof(ProfileProps);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        groupBox4.ResumeLayout(false);
        groupBox4.PerformLayout();
        groupBox5.ResumeLayout(false);
        groupBox5.PerformLayout();
        groupBox6.ResumeLayout(false);
        groupBox6.PerformLayout();
        groupBox7.ResumeLayout(false);
        groupBox7.PerformLayout();
        ResumeLayout(false);
    }

    private void MyOKButton_Click(object sender, EventArgs e)
    {
        if (!method_2())
            return;
        if (gprofile_0 == null)
            gprofile_0 = new GProfile();
        gprofile_0.MinLevel = method_0(MinLevelBox.Text);
        gprofile_0.MaxLevel = method_0(MaxLevelBox.Text);
        gprofile_0.LureMinutes = method_0(LureTimer.Text);
        if (FactionsBox.Text.Length > 0)
            gprofile_0.SetFactionsFromString(FactionsBox.Text);
        else
            gprofile_0.Factions = null;
        gprofile_0.Beach = Beach.Checked;
        gprofile_0.IgnoreAttackers = IgnoreAttackers.Checked;
        gprofile_0.Reversible = OutAndBack.Checked;
        gprofile_0.Fishing = Fishing.Checked;
        gprofile_0.BlacklistOn = BlacklistOn.Checked;
        gprofile_0.NaturalRun = NaturalRun.Checked;
        gprofile_0.ReverseWaypoints = ReverseWaypoints.Checked;
        gprofile_0.SkipWaypoints = SkipWaypoints.Checked;
        gprofile_0.Wander = Wander.Checked;
        gprofile_0.RunFromAvoids = RunFromAvoids.Checked;
        gprofile_0.OneShot = OneShot.Checked;
        gprofile_0.UseBreadcrumbs = UseBreadcrumbs.Checked;
        gprofile_0.AllowShortCircuit = AllowShortCircuit.Checked;
        gprofile_0.VendorAR = VendorAR.Text.Length <= 0 ? null : VendorAR.Text;
        gprofile_0.VendorRepair = VendorRepair.Text.Length <= 0 ? null : VendorRepair.Text;
        gprofile_0.VendorFW = VendorFW.Text.Length <= 0 ? null : VendorFW.Text;
        StartupClass.bool_16 = true;
        StartupClass.int_6 = !gprofile_0.ReverseWaypoints || gprofile_0.Reversible ? 1 : -1;
        gprofile_0.AvoidList = null;
        if (AvoidList.Text.Length > 2)
        {
            var arrayList = new ArrayList();
            var text = AvoidList.Text;
            var chArray = new char[1] { '\r' };
            foreach (var str in text.Split(chArray))
                if (str.Length > 2)
                    arrayList.Add(str.Replace("\n", ""));
            if (arrayList.Count > 0)
                gprofile_0.AvoidList = (string[])arrayList.ToArray(typeof(string));
        }

        DialogResult = DialogResult.OK;
    }

    protected int method_0(string string_0)
    {
        return string_0.Length > 0 ? int.Parse(string_0) : 0;
    }

    protected string method_1(int int_0)
    {
        return int_0 == 0 ? "" : int_0.ToString();
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    protected bool method_2()
    {
        if (!method_3(MinLevelBox.Text, true))
        {
            var num = (int)MessageBox.Show(this, GClass30.smethod_1(736), GClass30.smethod_1(657), MessageBoxButtons.OK,
                MessageBoxIcon.Hand);
            return false;
        }

        if (!method_3(MaxLevelBox.Text, true))
        {
            var num = (int)MessageBox.Show(this, GClass30.smethod_1(737), GClass30.smethod_1(657), MessageBoxButtons.OK,
                MessageBoxIcon.Hand);
            return false;
        }

        if (!method_3(LureTimer.Text, true))
        {
            var num = (int)MessageBox.Show(this, GClass30.smethod_1(738), GClass30.smethod_1(657), MessageBoxButtons.OK,
                MessageBoxIcon.Hand);
            return false;
        }

        if (FactionsBox.Text.Length > 0)
        {
            var text = FactionsBox.Text;
            var chArray = new char[1] { ' ' };
            foreach (var string_0 in text.Split(chArray))
                if (!method_3(string_0, false))
                {
                    var num = (int)MessageBox.Show(this, GClass30.smethod_1(739), GClass30.smethod_1(657),
                        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
        }

        return true;
    }

    protected bool method_3(string string_0, bool bool_0)
    {
        if (string_0 == "")
            return bool_0;
        try
        {
            int.Parse(string_0);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private void ClearWaypoints_Click(object sender, EventArgs e)
    {
        gprofile_0.Waypoints.Clear();
        ClearWaypoints.Enabled = false;
        WaypointsLabel.Text = GClass30.smethod_4("ProfileProps.WaypointsLabel");
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "Profiles.html");
    }

    private void ClearGhostWaypoints_Click(object sender, EventArgs e)
    {
        gprofile_0.GhostWaypoints.Clear();
        ClearGhostWaypoints.Enabled = false;
        GhostWaypointsLabel.Text = GClass30.smethod_4("ProfileProps.GhostWaypointsLabel");
    }

    private void Fishing_CheckedChanged(object sender, EventArgs e)
    {
        method_4();
    }

    public void method_4()
    {
        LureTimer.Enabled = Fishing.Checked;
    }

    private void OutAndBack_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void Circle_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void OneShot_CheckedChanged(object sender, EventArgs e)
    {
        IgnoreAttackers.Enabled = OneShot.Checked;
    }

    private void UseBreadcrumbs_CheckedChanged(object sender, EventArgs e)
    {
        AllowShortCircuit.Enabled = UseBreadcrumbs.Checked;
    }

    private void ClearVendorWaypoints_Click(object sender, EventArgs e)
    {
        gprofile_0.VendorWaypoints.Clear();
        VendorRepair.Text = "";
        VendorAR.Text = "";
        VendorFW.Text = "";
        ClearVendorWaypoints.Enabled = false;
        VendorWaypointsLabel.Text = GClass30.smethod_4("ProfileProps.VendorWaypointsLabel");
    }
}