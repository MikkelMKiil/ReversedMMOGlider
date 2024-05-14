// Decompiled with JetBrains decompiler
// Type: ProfileWizard
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Glider.Common.Objects;

public class ProfileWizard : Form
{
    private const double double_0 = 100.0;
    private bool bool_0;
    private bool bool_1;
    private bool bool_2;
    private bool bool_3;
    private Label CurrentARLabel;
    private Label CurrentFWLabel;
    private readonly double double_1 = 5.5;
    private double double_2;
    private Enum0 enum0_0;
    private Label FilenameLabel;
    private GLocation glocation_0;
    private GProfile gprofile_0;
    private IContainer icontainer_0;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label LabelFactions;
    private Label LabelGhostWPClose;
    private Label LabelGhostWPRecord;
    private Label LabelGhostWPStart;
    private Label LabelLevelRange;
    private Label LabelProfileDone;
    private Label LabelVendorConnect;
    private Label LabelVendorRecordAR;
    private Label LabelVendorRecordFW;
    private Label LabelVendorStart;
    private Label LabelWaypointCount;
    private Button NextButton;
    private Panel PanelDone;
    private Panel PanelGhostWaypointsRecord;
    private Panel PanelGhostWaypointsStart;
    private Panel PanelStartup;
    private Panel PanelStdWaypoints;
    private Panel PanelVendorConnect;
    private Panel PanelVendorRecordAR;
    private Panel PanelVendorRecordFW;
    private Panel PanelVendorStart;
    private CheckBox PauseStdBox;
    private Button PrevButton;
    private TextBox ProfileNameBox;
    private CheckBox SkipGhostBox;
    private CheckBox SkipVendorBox;
    private CheckBox SkipWizardBox;
    private Label StartupLabel;
    private Label StdWaypointsClose;
    private Label StdWaypointsLabel;
    private string string_0;
    private string string_1;
    private string string_2;
    private string string_3;
    private string string_4;
    private Timer timer_0;
    private Label VendorAmmoRepair;
    private Label VendorConnectClosest;
    private Label VendorFoodWater;

    public ProfileWizard()
    {
        InitializeComponent();
        MessageProvider.smethod_3(this, nameof(ProfileWizard));
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && icontainer_0 != null)
        {
            icontainer_0.Dispose();
        }
        base.Dispose(disposing);
    }
    private void InitializeComponent()
    {
        icontainer_0 = new Container();
        NextButton = new Button();
        PrevButton = new Button();
        PanelStartup = new Panel();
        ProfileNameBox = new TextBox();
        FilenameLabel = new Label();
        SkipWizardBox = new CheckBox();
        StartupLabel = new Label();
        PanelStdWaypoints = new Panel();
        StdWaypointsClose = new Label();
        PauseStdBox = new CheckBox();
        LabelFactions = new Label();
        LabelLevelRange = new Label();
        LabelWaypointCount = new Label();
        label3 = new Label();
        label2 = new Label();
        label1 = new Label();
        StdWaypointsLabel = new Label();
        timer_0 = new Timer(icontainer_0);
        PanelGhostWaypointsStart = new Panel();
        SkipGhostBox = new CheckBox();
        LabelGhostWPStart = new Label();
        PanelGhostWaypointsRecord = new Panel();
        LabelGhostWPClose = new Label();
        LabelGhostWPRecord = new Label();
        PanelVendorStart = new Panel();
        LabelVendorStart = new Label();
        SkipVendorBox = new CheckBox();
        PanelDone = new Panel();
        LabelProfileDone = new Label();
        PanelVendorRecordFW = new Panel();
        LabelVendorRecordFW = new Label();
        VendorFoodWater = new Label();
        CurrentFWLabel = new Label();
        PanelVendorRecordAR = new Panel();
        CurrentARLabel = new Label();
        VendorAmmoRepair = new Label();
        LabelVendorRecordAR = new Label();
        PanelVendorConnect = new Panel();
        VendorConnectClosest = new Label();
        LabelVendorConnect = new Label();
        PanelStartup.SuspendLayout();
        PanelStdWaypoints.SuspendLayout();
        PanelGhostWaypointsStart.SuspendLayout();
        PanelGhostWaypointsRecord.SuspendLayout();
        PanelVendorStart.SuspendLayout();
        PanelDone.SuspendLayout();
        PanelVendorRecordFW.SuspendLayout();
        PanelVendorRecordAR.SuspendLayout();
        PanelVendorConnect.SuspendLayout();
        SuspendLayout();
        NextButton.Location = new Point(358, 278);
        NextButton.Name = "NextButton";
        NextButton.Size = new Size(83, 28);
        NextButton.TabIndex = 0;
        NextButton.Text = "Next >>";
        NextButton.UseVisualStyleBackColor = true;
        NextButton.Click += NextButton_Click;
        PrevButton.Location = new Point(256, 278);
        PrevButton.Name = "PrevButton";
        PrevButton.Size = new Size(83, 28);
        PrevButton.TabIndex = 1;
        PrevButton.Text = "<< Previous";
        PrevButton.UseVisualStyleBackColor = true;
        PrevButton.Click += PrevButton_Click;
        PanelStartup.Controls.Add(ProfileNameBox);
        PanelStartup.Controls.Add(FilenameLabel);
        PanelStartup.Controls.Add(SkipWizardBox);
        PanelStartup.Controls.Add(StartupLabel);
        PanelStartup.Location = new Point(12, 12);
        PanelStartup.Name = "PanelStartup";
        PanelStartup.Size = new Size(429, 251);
        PanelStartup.TabIndex = 2;
        ProfileNameBox.Location = new Point(16, 181);
        ProfileNameBox.Name = "ProfileNameBox";
        ProfileNameBox.Size = new Size(205, 20);
        ProfileNameBox.TabIndex = 3;
        FilenameLabel.AutoSize = true;
        FilenameLabel.Location = new Point(13, 165);
        FilenameLabel.Name = "FilenameLabel";
        FilenameLabel.Size = new Size(68, 13);
        FilenameLabel.TabIndex = 2;
        FilenameLabel.Text = "Profile name:";
        SkipWizardBox.AutoSize = true;
        SkipWizardBox.Location = new Point(16, 223);
        SkipWizardBox.Name = "SkipWizardBox";
        SkipWizardBox.Size = new Size(54, 17);
        SkipWizardBox.TabIndex = 1;
        SkipWizardBox.Text = "(local)";
        SkipWizardBox.UseVisualStyleBackColor = true;
        SkipWizardBox.CheckedChanged += SkipWizardBox_CheckedChanged;
        StartupLabel.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
        StartupLabel.Location = new Point(13, 13);
        StartupLabel.Name = "StartupLabel";
        StartupLabel.Size = new Size(404, 140);
        StartupLabel.TabIndex = 0;
        StartupLabel.Text = "(local)";
        PanelStdWaypoints.Controls.Add(StdWaypointsClose);
        PanelStdWaypoints.Controls.Add(PauseStdBox);
        PanelStdWaypoints.Controls.Add(LabelFactions);
        PanelStdWaypoints.Controls.Add(LabelLevelRange);
        PanelStdWaypoints.Controls.Add(LabelWaypointCount);
        PanelStdWaypoints.Controls.Add(label3);
        PanelStdWaypoints.Controls.Add(label2);
        PanelStdWaypoints.Controls.Add(label1);
        PanelStdWaypoints.Controls.Add(StdWaypointsLabel);
        PanelStdWaypoints.Location = new Point(12, 12);
        PanelStdWaypoints.Name = "PanelStdWaypoints";
        PanelStdWaypoints.Size = new Size(429, 251);
        PanelStdWaypoints.TabIndex = 3;
        StdWaypointsClose.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
        StdWaypointsClose.Location = new Point(13, 198);
        StdWaypointsClose.Name = "StdWaypointsClose";
        StdWaypointsClose.Size = new Size(404, 42);
        StdWaypointsClose.TabIndex = 8;
        StdWaypointsClose.Text = "(local)";
        PauseStdBox.AutoSize = true;
        PauseStdBox.Location = new Point(24, 90);
        PauseStdBox.Name = "PauseStdBox";
        PauseStdBox.Size = new Size(103, 17);
        PauseStdBox.TabIndex = 7;
        PauseStdBox.Text = "Pause recording";
        PauseStdBox.UseVisualStyleBackColor = true;
        PauseStdBox.CheckedChanged += PauseStdBox_CheckedChanged;
        LabelFactions.AutoSize = true;
        LabelFactions.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
        LabelFactions.ForeColor = SystemColors.Highlight;
        LabelFactions.Location = new Point(138, 165);
        LabelFactions.Name = "LabelFactions";
        LabelFactions.Size = new Size(60, 13);
        LabelFactions.TabIndex = 6;
        LabelFactions.Text = "(factions)";
        LabelLevelRange.AutoSize = true;
        LabelLevelRange.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
        LabelLevelRange.ForeColor = SystemColors.Highlight;
        LabelLevelRange.Location = new Point(138, 143);
        LabelLevelRange.Name = "LabelLevelRange";
        LabelLevelRange.Size = new Size(48, 13);
        LabelLevelRange.TabIndex = 5;
        LabelLevelRange.Text = "(levels)";
        LabelWaypointCount.AutoSize = true;
        LabelWaypointCount.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
        LabelWaypointCount.ForeColor = SystemColors.Highlight;
        LabelWaypointCount.Location = new Point(138, 121);
        LabelWaypointCount.Name = "LabelWaypointCount";
        LabelWaypointCount.Size = new Size(71, 13);
        LabelWaypointCount.TabIndex = 4;
        LabelWaypointCount.Text = "(waypoints)";
        label3.AutoSize = true;
        label3.Location = new Point(72, 165);
        label3.Name = "label3";
        label3.Size = new Size(60, 13);
        label3.TabIndex = 3;
        label3.Text = "Faction list:";
        label3.TextAlign = ContentAlignment.TopRight;
        label2.AutoSize = true;
        label2.Location = new Point(66, 143);
        label2.Name = "label2";
        label2.Size = new Size(66, 13);
        label2.TabIndex = 2;
        label2.Text = "Level range:";
        label2.TextAlign = ContentAlignment.TopRight;
        label1.AutoSize = true;
        label1.Location = new Point(40, 121);
        label1.Name = "label1";
        label1.Size = new Size(92, 13);
        label1.TabIndex = 1;
        label1.Text = "Waypoints stored:";
        label1.TextAlign = ContentAlignment.TopRight;
        StdWaypointsLabel.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
        StdWaypointsLabel.Location = new Point(13, 13);
        StdWaypointsLabel.Name = "StdWaypointsLabel";
        StdWaypointsLabel.Size = new Size(404, 74);
        StdWaypointsLabel.TabIndex = 0;
        StdWaypointsLabel.Text = "Adding waypoints panel...";
        timer_0.Enabled = true;
        timer_0.Interval = 500;
        timer_0.Tick += timer_0_Tick;
        PanelGhostWaypointsStart.Controls.Add(SkipGhostBox);
        PanelGhostWaypointsStart.Controls.Add(LabelGhostWPStart);
        PanelGhostWaypointsStart.Location = new Point(12, 12);
        PanelGhostWaypointsStart.Name = "PanelGhostWaypointsStart";
        PanelGhostWaypointsStart.Size = new Size(429, 251);
        PanelGhostWaypointsStart.TabIndex = 9;
        SkipGhostBox.AutoSize = true;
        SkipGhostBox.Location = new Point(16, 207);
        SkipGhostBox.Name = "SkipGhostBox";
        SkipGhostBox.Size = new Size(194, 17);
        SkipGhostBox.TabIndex = 1;
        SkipGhostBox.Text = "Skip ghost waypoints for this profile.";
        SkipGhostBox.UseVisualStyleBackColor = true;
        LabelGhostWPStart.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelGhostWPStart.Location = new Point(13, 13);
        LabelGhostWPStart.Name = "LabelGhostWPStart";
        LabelGhostWPStart.Size = new Size(404, 121);
        LabelGhostWPStart.TabIndex = 0;
        LabelGhostWPStart.Text = "(local)";
        PanelGhostWaypointsRecord.Controls.Add(LabelGhostWPClose);
        PanelGhostWaypointsRecord.Controls.Add(LabelGhostWPRecord);
        PanelGhostWaypointsRecord.Location = new Point(12, 12);
        PanelGhostWaypointsRecord.Name = "PanelGhostWaypointsRecord";
        PanelGhostWaypointsRecord.Size = new Size(429, 251);
        PanelGhostWaypointsRecord.TabIndex = 10;
        LabelGhostWPClose.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
        LabelGhostWPClose.Location = new Point(13, 161);
        LabelGhostWPClose.Name = "LabelGhostWPClose";
        LabelGhostWPClose.Size = new Size(404, 43);
        LabelGhostWPClose.TabIndex = 1;
        LabelGhostWPClose.Text = "(local)";
        LabelGhostWPRecord.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelGhostWPRecord.Location = new Point(13, 13);
        LabelGhostWPRecord.Name = "LabelGhostWPRecord";
        LabelGhostWPRecord.Size = new Size(404, 121);
        LabelGhostWPRecord.TabIndex = 0;
        LabelGhostWPRecord.Text = "(local)";
        PanelVendorStart.Controls.Add(SkipVendorBox);
        PanelVendorStart.Controls.Add(LabelVendorStart);
        PanelVendorStart.Location = new Point(12, 12);
        PanelVendorStart.Name = "PanelVendorStart";
        PanelVendorStart.Size = new Size(429, 251);
        PanelVendorStart.TabIndex = 11;
        LabelVendorStart.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelVendorStart.Location = new Point(12, 9);
        LabelVendorStart.Name = "LabelVendorStart";
        LabelVendorStart.Size = new Size(404, 121);
        LabelVendorStart.TabIndex = 1;
        LabelVendorStart.Text = "(local)";
        SkipVendorBox.AutoSize = true;
        SkipVendorBox.Location = new Point(15, 210);
        SkipVendorBox.Name = "SkipVendorBox";
        SkipVendorBox.Size = new Size(54, 17);
        SkipVendorBox.TabIndex = 2;
        SkipVendorBox.Text = "(local)";
        SkipVendorBox.UseVisualStyleBackColor = true;
        SkipVendorBox.CheckedChanged += SkipVendorBox_CheckedChanged;
        PanelDone.Controls.Add(LabelProfileDone);
        PanelDone.Location = new Point(12, 12);
        PanelDone.Name = "PanelDone";
        PanelDone.Size = new Size(429, 251);
        PanelDone.TabIndex = 12;
        LabelProfileDone.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelProfileDone.Location = new Point(12, 9);
        LabelProfileDone.Name = "LabelProfileDone";
        LabelProfileDone.Size = new Size(404, 121);
        LabelProfileDone.TabIndex = 1;
        LabelProfileDone.Text = "(local)";
        PanelVendorRecordFW.Controls.Add(CurrentFWLabel);
        PanelVendorRecordFW.Controls.Add(VendorFoodWater);
        PanelVendorRecordFW.Controls.Add(LabelVendorRecordFW);
        PanelVendorRecordFW.Location = new Point(12, 12);
        PanelVendorRecordFW.Name = "PanelVendorRecordFW";
        PanelVendorRecordFW.Size = new Size(429, 251);
        PanelVendorRecordFW.TabIndex = 13;
        LabelVendorRecordFW.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelVendorRecordFW.Location = new Point(12, 9);
        LabelVendorRecordFW.Name = "LabelVendorRecordFW";
        LabelVendorRecordFW.Size = new Size(404, 121);
        LabelVendorRecordFW.TabIndex = 1;
        LabelVendorRecordFW.Text = "(vendor record panel)";
        VendorFoodWater.AutoSize = true;
        VendorFoodWater.Location = new Point(13, 165);
        VendorFoodWater.Name = "VendorFoodWater";
        VendorFoodWater.Size = new Size(35, 13);
        VendorFoodWater.TabIndex = 2;
        VendorFoodWater.Text = "(local)";
        CurrentFWLabel.AutoSize = true;
        CurrentFWLabel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
        CurrentFWLabel.ForeColor = SystemColors.Highlight;
        CurrentFWLabel.Location = new Point(13, 184);
        CurrentFWLabel.Name = "CurrentFWLabel";
        CurrentFWLabel.Size = new Size(42, 13);
        CurrentFWLabel.TabIndex = 3;
        CurrentFWLabel.Text = "(local)";
        PanelVendorRecordAR.Controls.Add(CurrentARLabel);
        PanelVendorRecordAR.Controls.Add(VendorAmmoRepair);
        PanelVendorRecordAR.Controls.Add(LabelVendorRecordAR);
        PanelVendorRecordAR.Location = new Point(12, 12);
        PanelVendorRecordAR.Name = "PanelVendorRecordAR";
        PanelVendorRecordAR.Size = new Size(429, 251);
        PanelVendorRecordAR.TabIndex = 14;
        CurrentARLabel.AutoSize = true;
        CurrentARLabel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
        CurrentARLabel.ForeColor = SystemColors.Highlight;
        CurrentARLabel.Location = new Point(13, 184);
        CurrentARLabel.Name = "CurrentARLabel";
        CurrentARLabel.Size = new Size(42, 13);
        CurrentARLabel.TabIndex = 3;
        CurrentARLabel.Text = "(local)";
        VendorAmmoRepair.AutoSize = true;
        VendorAmmoRepair.Location = new Point(13, 165);
        VendorAmmoRepair.Name = "VendorAmmoRepair";
        VendorAmmoRepair.Size = new Size(35, 13);
        VendorAmmoRepair.TabIndex = 2;
        VendorAmmoRepair.Text = "(local)";
        LabelVendorRecordAR.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelVendorRecordAR.Location = new Point(12, 9);
        LabelVendorRecordAR.Name = "LabelVendorRecordAR";
        LabelVendorRecordAR.Size = new Size(404, 121);
        LabelVendorRecordAR.TabIndex = 1;
        LabelVendorRecordAR.Text = "(vendor record panel)";
        PanelVendorConnect.Controls.Add(VendorConnectClosest);
        PanelVendorConnect.Controls.Add(LabelVendorConnect);
        PanelVendorConnect.Location = new Point(12, 12);
        PanelVendorConnect.Name = "PanelVendorConnect";
        PanelVendorConnect.Size = new Size(429, 251);
        PanelVendorConnect.TabIndex = 15;
        VendorConnectClosest.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
        VendorConnectClosest.Location = new Point(13, 165);
        VendorConnectClosest.Name = "VendorConnectClosest";
        VendorConnectClosest.Size = new Size(403, 62);
        VendorConnectClosest.TabIndex = 2;
        VendorConnectClosest.Text = "(local)";
        LabelVendorConnect.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelVendorConnect.Location = new Point(12, 9);
        LabelVendorConnect.Name = "LabelVendorConnect";
        LabelVendorConnect.Size = new Size(404, 121);
        LabelVendorConnect.TabIndex = 1;
        LabelVendorConnect.Text = "(local)";
        AcceptButton = NextButton;
        AutoScaleDimensions = new SizeF(6f, 13f);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(456, 320);
        Controls.Add(PanelVendorStart);
        Controls.Add(PanelVendorRecordAR);
        Controls.Add(PanelStdWaypoints);
        Controls.Add(PanelStartup);
        Controls.Add(PanelGhostWaypointsRecord);
        Controls.Add(PanelVendorConnect);
        Controls.Add(PanelVendorRecordFW);
        Controls.Add(PanelDone);
        Controls.Add(PanelGhostWaypointsStart);
        Controls.Add(PrevButton);
        Controls.Add(NextButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(ProfileWizard);
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Profile Wizard";
        TopMost = true;
        PanelStartup.ResumeLayout(false);
        PanelStartup.PerformLayout();
        PanelStdWaypoints.ResumeLayout(false);
        PanelStdWaypoints.PerformLayout();
        PanelGhostWaypointsStart.ResumeLayout(false);
        PanelGhostWaypointsStart.PerformLayout();
        PanelGhostWaypointsRecord.ResumeLayout(false);
        PanelVendorStart.ResumeLayout(false);
        PanelVendorStart.PerformLayout();
        PanelDone.ResumeLayout(false);
        PanelVendorRecordFW.ResumeLayout(false);
        PanelVendorRecordFW.PerformLayout();
        PanelVendorRecordAR.ResumeLayout(false);
        PanelVendorRecordAR.PerformLayout();
        PanelVendorConnect.ResumeLayout(false);
        ResumeLayout(false);
    }

    public DialogResult method_0(Form form_0)
    {
        Text = GProcessMemoryManipulator.smethod_0();
        string_2 = MessageProvider.smethod_4("ProfileWizard.Left");
        string_3 = MessageProvider.smethod_4("ProfileWizard.Right");
        string_4 = MessageProvider.smethod_4("ProfileWizard.Yards");
        enum0_0 = Enum0.const_0;
        method_1();
        return ShowDialog(form_0);
    }

    protected override void OnClosed(EventArgs eventArgs_0)
    {
        timer_0.Enabled = false;
        base.OnClosed(eventArgs_0);
    }
    private void method_1()
    {
        switch (enum0_0)
        {
            case Enum0.const_0:
                method_2(PanelStartup);
                PrevButton.Enabled = false;
                break;
            case Enum0.const_1:
                double_2 = GClass61.gclass61_0.method_4("AutoAddDistance");
                method_2(PanelStdWaypoints);
                PrevButton.Enabled = true;
                bool_1 = false;
                StdWaypointsClose.Visible = false;
                break;
            case Enum0.const_2:
                gprofile_0.Save(string_0);
                method_2(PanelGhostWaypointsStart);
                break;
            case Enum0.const_3:
                gprofile_0.GhostWaypoints.Clear();
                glocation_0 = null;
                if (SkipGhostBox.Checked)
                {
                    method_14();
                    break;
                }

                method_2(PanelGhostWaypointsRecord);
                break;
            case Enum0.const_4:
                gprofile_0.Save(string_0);
                method_2(PanelVendorStart);
                SkipVendorBox.Checked = !StartupClass.IsSomeConditionMet;
                break;
            case Enum0.const_5:
                if (SkipVendorBox.Checked)
                {
                    enum0_0 = Enum0.const_8;
                    method_1();
                    break;
                }

                glocation_0 = GPlayerSelf.Me.Location;
                gprofile_0.VendorWaypoints.Clear();
                gprofile_0.VendorWaypoints.Add(glocation_0);
                method_2(PanelVendorRecordFW);
                break;
            case Enum0.const_6:
                bool_3 = false;
                method_2(PanelVendorRecordAR);
                break;
            case Enum0.const_7:
                method_2(PanelVendorConnect);
                break;
            case Enum0.const_8:
                gprofile_0.Save(string_0);
                StartupClass.smethod_1(string_0);
                method_2(PanelDone);
                PrevButton.Enabled = false;
                NextButton.Text = MessageProvider.smethod_4("ProfileWizard.Finished");
                break;
        }
    }

    private void method_2(Panel panel_0)
    {
        foreach (Control control in (ArrangedElementCollection)Controls)
            if (control.GetType() == typeof(Panel))
                control.Visible = control == panel_0;
    }

    private bool method_3()
    {
        switch (enum0_0)
        {
            case Enum0.const_0:
                return method_5();
            case Enum0.const_1:
                return method_4();
            case Enum0.const_2:
                return true;
            case Enum0.const_3:
                method_6("GhostWPNotDone", null);
                return false;
            case Enum0.const_4:
                return true;
            case Enum0.const_5:
                if (gprofile_0.VendorFW != null)
                    return true;
                method_6("NoVendorFW", null);
                return false;
            case Enum0.const_6:
                if (gprofile_0.VendorAR != null)
                    return true;
                method_6("NoVendorAR", null);
                return false;
            case Enum0.const_7:
                method_6("VendorWPNotDone", null);
                return false;
            case Enum0.const_8:
                DialogResult = DialogResult.OK;
                return true;
            default:
                Logger.LogMessage("! Unable to validate current state in profile wiz: " + (int)enum0_0);
                return false;
        }
    }

    private bool method_4()
    {
        if (gprofile_0.MinLevel != 0 && gprofile_0.MaxLevel != 0)
        {
            if (gprofile_0.Waypoints.Count < 5)
            {
                method_6("NoWPs", null);
                return false;
            }

            gprofile_0.Reversible =
                gprofile_0.Waypoints[0].GetDistanceTo(gprofile_0.Waypoints[gprofile_0.Waypoints.Count - 1]) > 50.0;
            return true;
        }

        method_6("NoKillsError", null);
        return false;
    }

    private bool method_5()
    {
        if (SkipWizardBox.Checked)
        {
            DialogResult = DialogResult.No;
            return false;
        }

        if (ProfileNameBox.Text.Trim().Length == 0)
        {
            method_6("NoProfileName", ProfileNameBox);
            return false;
        }

        var path = GClass61.gclass61_0.method_2("ProfilesDir") + ProfileNameBox.Text.Trim() + ".xml";
        if (File.Exists(path))
            if (MessageBox.Show(this, MessageProvider.smethod_6("ProfileWizard.ProfileExists", ProfileNameBox.Text.Trim()),
                    GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
            {
                ProfileNameBox.Focus();
                return false;
            }

        string_0 = path;
        string_1 = ProfileNameBox.Text.Trim();
        if (gprofile_0 == null)
        {
            gprofile_0 = new GProfile();
            gprofile_0.NaturalRun = true;
            gprofile_0.SkipWaypoints = true;
            gprofile_0.BlacklistOn = true;
        }

        return true;
    }

    private void method_6(string string_5, Control control_0)
    {
        var num = (int)MessageBox.Show(this, MessageProvider.smethod_4("ProfileWizard." + string_5), GProcessMemoryManipulator.smethod_0(),
            MessageBoxButtons.OK, MessageBoxIcon.Hand);
        control_0?.Focus();
    }

    private void NextButton_Click(object sender, EventArgs e)
    {
        if (!method_3())
            return;
        method_14();
    }

    private void PrevButton_Click(object sender, EventArgs e)
    {
        --enum0_0;
        method_1();
    }

    private void timer_0_Tick(object sender, EventArgs e)
    {
        try
        {
            if (!StartupClass.bool_13 || enum0_0 == Enum0.const_0)
                return;
            switch (enum0_0)
            {
                case Enum0.const_1:
                    method_11();
                    break;
                case Enum0.const_2:
                    method_12();
                    break;
                case Enum0.const_3:
                    method_13();
                    break;
                case Enum0.const_5:
                    method_9();
                    break;
                case Enum0.const_6:
                    method_10();
                    break;
                case Enum0.const_7:
                    method_8();
                    break;
            }
        }
        catch (Exception ex)
        {
            Logger.smethod_1("* Exception in profile wizard timer: " + ex.Message + "\r\n" + ex.StackTrace);
            timer_0.Enabled = false;
        }
    }

    private void method_7(double double_3)
    {
        if (GPlayerSelf.Me.Location.GetDistanceTo(glocation_0) < double_3)
            return;
        glocation_0 = GPlayerSelf.Me.Location;
        gprofile_0.VendorWaypoints.Add(glocation_0);
        GClass20.smethod_0("Key.wav");
    }

    private void method_8()
    {
        var closestWaypoint = gprofile_0.FindClosestWaypoint(GPlayerSelf.Me.Location);
        double headingTo = GPlayerSelf.Me.Location.GetHeadingTo(closestWaypoint);
        var num = GContext.Main.Movement.CompareHeadings(GPlayerSelf.Me.Heading, headingTo);
        string str;
        if (num < 0.0)
        {
            num *= -1.0;
            str = string_2;
        }
        else
        {
            str = string_3;
        }

        VendorConnectClosest.Text = MessageProvider.smethod_6("ProfileWizard.WaypointsClose",
            Math.Round(GPlayerSelf.Me.Location.GetDistanceTo(closestWaypoint), 0), Math.Round(num / Math.PI * 180.0, 0),
            str);
        method_7(double_2);
        if (glocation_0.GetDistanceTo(closestWaypoint) >= double_2 * 2.0)
            return;
        method_14();
    }

    private void method_9()
    {
        if (GPlayerSelf.Me.Target != null && GPlayerSelf.Me.Target.IsInMeleeRange)
        {
            gprofile_0.VendorFW = GPlayerSelf.Me.Target.Name;
            CurrentFWLabel.Text = gprofile_0.VendorFW;
        }

        method_7(double_1);
    }

    private void method_10()
    {
        if (GPlayerSelf.Me.Target == null || GPlayerSelf.Me.Target.Name != gprofile_0.VendorFW)
            bool_3 = true;
        if (GPlayerSelf.Me.Target != null && GPlayerSelf.Me.Target.IsInMeleeRange && bool_3)
        {
            gprofile_0.VendorAR = GPlayerSelf.Me.Target.Name;
            CurrentARLabel.Text = gprofile_0.VendorAR;
        }

        method_7(double_1);
    }

    private void method_11()
    {
        if (!PauseStdBox.Checked)
        {
            if (glocation_0 == null || GPlayerSelf.Me.GetDistanceTo(glocation_0) > double_2)
            {
                glocation_0 = GPlayerSelf.Me.Location;
                gprofile_0.Waypoints.Add(glocation_0);
                GClass20.smethod_0("Key.wav");
                if (!bool_1 && gprofile_0.Waypoints.Count > 2 &&
                    glocation_0.GetDistanceTo(gprofile_0.Waypoints[0]) > 100.0)
                {
                    bool_1 = true;
                    StdWaypointsClose.Visible = true;
                    if (!bool_0)
                    {
                        StdWaypointsLabel.Text = MessageProvider.smethod_4("ProfileWizard.StdWaypointsLabelAway");
                        GClass20.smethod_0("PlayerNear.wav");
                        bool_0 = true;
                    }
                }

                if (bool_1 && glocation_0.GetDistanceTo(gprofile_0.Waypoints[0]) < double_2 * 2.0 && !bool_2)
                {
                    bool_2 = true;
                    method_14();
                    return;
                }
            }

            if (GPlayerSelf.Me.IsInCombat && GPlayerSelf.Me.TargetGUID != 0L && GPlayerSelf.Me.Target.IsMonster &&
                GPlayerSelf.Me.Target.Health < 1.0)
            {
                var target = GPlayerSelf.Me.Target;
                var num = 1;
                if (GPlayerSelf.Me.Level >= 26)
                    ++num;
                if (target.Level <= gprofile_0.MinLevel || gprofile_0.MinLevel == 0)
                {
                    gprofile_0.MinLevel = target.Level - num;
                    if (gprofile_0.MinLevel == 0)
                        gprofile_0.MinLevel = 1;
                }

                if (target.Level >= gprofile_0.MaxLevel || gprofile_0.MaxLevel == 0)
                    gprofile_0.MaxLevel = target.Level + num;
                if (!gprofile_0.CheckFaction(target.FactionID, true))
                    gprofile_0.AddFaction(target.FactionID);
            }
        }

        LabelWaypointCount.Text = gprofile_0.Waypoints.Count.ToString();
        if (gprofile_0.MinLevel > 0 && gprofile_0.MaxLevel > 0)
            LabelLevelRange.Text = gprofile_0.MinLevel + " - " + gprofile_0.MaxLevel;
        else
            LabelLevelRange.Text = MessageProvider.smethod_4("ProfileWizard.NoKills");
        var factionsAsString = gprofile_0.GetFactionsAsString();
        if (factionsAsString.Length > 0)
            LabelFactions.Text = factionsAsString;
        else
            LabelLevelRange.Text = MessageProvider.smethod_4("ProfileWizard.NoKills");
        if (!StdWaypointsClose.Visible)
            return;
        double headingTo = GPlayerSelf.Me.Location.GetHeadingTo(gprofile_0.Waypoints[0]);
        var num1 = GContext.Main.Movement.CompareHeadings(GPlayerSelf.Me.Heading, headingTo);
        string str;
        if (num1 < 0.0)
        {
            num1 *= -1.0;
            str = string_2;
        }
        else
        {
            str = string_3;
        }

        StdWaypointsClose.Text = MessageProvider.smethod_6("ProfileWizard.StdWaypointsClose",
            Math.Round(GPlayerSelf.Me.Location.GetDistanceTo(gprofile_0.Waypoints[0]), 0),
            Math.Round(num1 / Math.PI * 180.0, 0), str);
    }

    private void method_12()
    {
        if (SkipGhostBox.Checked || !GPlayerSelf.Me.IsGhost)
            return;
        method_14();
    }

    private void method_13()
    {
        var closestWaypoint = gprofile_0.FindClosestWaypoint(GPlayerSelf.Me.Location);
        double headingTo = GPlayerSelf.Me.Location.GetHeadingTo(closestWaypoint);
        var num = GContext.Main.Movement.CompareHeadings(GPlayerSelf.Me.Heading, headingTo);
        string str1;
        if (num < 0.0)
        {
            num *= -1.0;
            str1 = string_2;
        }
        else
        {
            str1 = string_3;
        }

        var str2 = MessageProvider.smethod_6("ProfileWizard.WaypointsClose",
            Math.Round(GPlayerSelf.Me.Location.GetDistanceTo(closestWaypoint), 0), Math.Round(num / Math.PI * 180.0, 0),
            str1);
        if (glocation_0 == null || GPlayerSelf.Me.GetDistanceTo(glocation_0) > double_2)
        {
            glocation_0 = GPlayerSelf.Me.Location;
            gprofile_0.GhostWaypoints.Add(glocation_0);
            GClass20.smethod_0("Key.wav");
        }

        if (gprofile_0.GhostWaypoints.Count > 0 &&
            gprofile_0.GhostWaypoints[gprofile_0.GhostWaypoints.Count - 1].GetDistanceTo(closestWaypoint) <
            double_2 * 2.0)
            method_14();
        else
            LabelGhostWPClose.Text = str2;
    }

    private void SkipWizardBox_CheckedChanged(object sender, EventArgs e)
    {
        ProfileNameBox.Enabled = !SkipWizardBox.Checked;
    }

    private void PauseStdBox_CheckedChanged(object sender, EventArgs e)
    {
        if (PauseStdBox.Checked)
            return;
        glocation_0 = GPlayerSelf.Me.Location;
    }

    private void method_14()
    {
        ++enum0_0;
        method_1();
    }

    private void SkipVendorBox_CheckedChanged(object sender, EventArgs e)
    {
        if (SkipVendorBox.Checked || StartupClass.IsSomeConditionMet || MessageBox.Show(this,
                MessageProvider.smethod_4("ProfileWizard.Elite"), GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            return;
        SkipVendorBox.Checked = true;
    }

    private enum Enum0
    {
        const_0,
        const_1,
        const_2,
        const_3,
        const_4,
        const_5,
        const_6,
        const_7,
        const_8
    }
}