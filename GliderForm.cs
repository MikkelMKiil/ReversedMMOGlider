// Decompiled with JetBrains decompiler
// Type: GliderForm
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Glider.Common;
using Glider.Common.Objects;
using Timer = System.Windows.Forms.Timer;

public class GliderForm : Form, GInterface0
{
    public const int int_0 = 20000;
    private const uint uint_0 = 4874;
    private const int int_1 = 0;
    private const int int_2 = 5;
    public static GliderForm gliderForm_0;
    private Button AddFactionButton;
    private Button AddWaypointButton;
    private ToolStripMenuItem alwaysOnTopToolStripMenuItem1;
    private bool bool_0;
    private bool bool_1;
    private bool bool_10;
    public bool bool_2 = true;
    public bool bool_3;
    public bool bool_4 = true;
    private bool bool_5;
    private bool bool_6;
    private bool bool_7;
    private bool bool_8;
    private bool bool_9;
    private Button ConfigButton;
    private ContextMenuStrip contextMenuStrip2;
    private ContextMenuStrip ContextMenuStripWindow;
    private Button EditProfileButton;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem exitToolStripMenuItem1;
    private Label FactionLabel;
    private readonly GClass36 gclass36_0 = new GClass36(3000);
    private Button GlideButton;
    private GLocation glocation_0;
    protected GLocation glocation_1;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private GroupBox groupBox3;
    private GroupBox groupBox4;
    private GroupBox groupBox5;
    private GroupBox groupBox6;
    private HelpProvider helpProvider_0;
    private Button HideButton;
    private IContainer icontainer_0;
    private int int_3;
    private int int_4;
    private int int_5;
    private Button KillButton;
    private Label label1_1;
    private Label label10_1;
    private CheckBox label11;
    private Label label12;
    private Label label2_1;
    private Label label3_1;
    private Label label4_1;
    private Label label5_1;
    private Label label6_1;
    private Label label7_1;
    private Label label8_1;
    private Label label9_1;
    private Label LabelAttached;
    private Label LabelHealth_1;
    private Label LabelKills_1;
    public Label LabelMana_1;
    private Label LabelManaHeader_1;
    private Button LoadProfileButton;
    private Label Location_3d;
    private Label locXYZLabel;
    private TextBox LogBox;
    private Panel MainPanel;
    private ToolStripMenuItem minimizeToTrayToolStripMenuItem;
    private Button MyHelpButton;
    private Button NewProfileButton;
    private NotifyIcon notifyIcon_0;
    private Panel panel1;
    private Point point_0;
    private Button QuickLoadButton;
    private Rectangle rectangle_0;
    private Button SaveProfileButton;
    private ToolStripMenuItem showWindowToolStripMenuItem;
    private Button ShrinkButton;
    private Label SpeedLabel_1;
    private Label StatusLabel;
    private Button StopButton;
    public string string_0;
    public string string_1;
    private TabControl tabControl1;
    private TabPage tabDefault;
    private TabPage tabPage1;
    private TabPage tabProfile;
    private Label TDistanceLabel_1;
    private Label TFactionLabel_1;
    private Label THealthLabel_1;
    private Timer timer_0;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripSeparator toolStripSeparator2;
    private ToolTip toolTip_0;
    private Label VersionLabel;
    private Panel WaypointsPanel;
    private Label WP_ClosestLabel_1;
    private Label WP_FirstLabel_1;
    private Label WP_NewestLabel_1;
    private RadioButton WPTypeAuto_1;
    private RadioButton WPTypeGhost_1;
    private RadioButton WPTypeNormal_1;
    private RadioButton WPTypeVendor_1;
    private Label XPHour_1;

    public GliderForm()
    {
        try
        {
            Application.ThreadException += method_19;
            method_0();
        }
        catch (Exception ex)
        {
            var num = (int)MessageBox.Show(ex.GetType() + "\n\n" + ex.Message + "\n\n" + ex.StackTrace,
                "Glider Startup Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            Environment.Exit(0);
        }
    }

public class GliderForm : Form, GInterface0
{
    // Other fields and methods...

    void GInterface0.imethod_3(string string_2)
    {
        if (!StartupClass.bool_18)
            return;
        this.imethod_2("[Debug] " + string_2);
    }

    void GInterface0.imethod_2(string string_2)
    {
        // Implementation of imethod_2
    }

    // Other methods...

    protected override void Dispose(bool disposing)
    {
        // Implementation of Dispose
    }

    protected override void OnClosing(CancelEventArgs cancelEventArgs_0)
    {
        // Implementation of OnClosing
    }

    protected override void OnPaint(PaintEventArgs paintEventArgs_0)
    {
        // Implementation of OnPaint
    }
}public class GliderForm : Form, GInterface0
{
    // Other fields and methods...

    void GInterface0.imethod_3(string string_2)
    {
        if (!StartupClass.bool_18)
            return;
        this.imethod_2("[Debug] " + string_2);
    }

    void GInterface0.imethod_2(string string_2)
    {
        // Implementation of imethod_2
    }

    // Other methods...

    protected override void Dispose(bool disposing)
    {
        // Implementation of Dispose
    }

    protected override void OnClosing(CancelEventArgs cancelEventArgs_0)
    {
        // Implementation of OnClosing
    }

    protected override void OnPaint(PaintEventArgs paintEventArgs_0)
    {
        // Implementation of OnPaint
    }
}
    void GInterface0.imethod_2(string string_2)
    {
        lock (this)
        {
            method_6(string_2);
            if (!string_2.StartsWith("[Debug]"))
            {
                var gliderForm = this;
                gliderForm.string_0 = gliderForm.string_0 + "\r\n" + string_2;
                if (string_0.Length > 20000)
                    string_0 = string_0.Substring(20000);
                bool_2 = true;
            }
        }

        if (string_2.StartsWith("[Debug]"))
            return;
        StartupClass.smethod_17(2, string_2);
    }

    void GInterface0.imethod_0()
    {
        bool_1 = true;
    }

    void GInterface0.imethod_1()
    {
        label11.Checked = !label11.Checked;
    }

    void GInterface0.imethod_4()
    {
        bool_5 = true;
        GClass37.smethod_0("Setting stop flag in Gliderform");
        Thread.Sleep(1200);
    }

    private void method_0()
    {
        Application.ThreadException += method_19;
        gliderForm_0 = this;
        StartupClass.ginterface0_0 = this;
        string_0 = "";
        method_13();
        StartupClass.form_0 = this;
        StartupClass.InitStartupMode(AppMode.Normal);
        new GClass65().method_0();
        method_5();
        if (GClass61.gclass61_0.method_5("AltLayout"))
            method_26();
        if (GClass61.gclass61_0.method_5("AlwaysOnTop"))
        {
            TopMost = true;
            alwaysOnTopToolStripMenuItem1.Checked = true;
        }

        if (!GClass61.gclass61_0.bool_0)
            GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "Welcome.html");
        GClass30.smethod_3(this, nameof(GliderForm));
        GClass24.intptr_0 = Handle;
        VersionLabel.Text = "Glider 1.8.0 (Release) -- January 21, 2009";
        toolTip_0.SetToolTip(GlideButton, GClass30.smethod_4("GliderForm.GlideButton!Tooltip"));
        toolTip_0.SetToolTip(KillButton, GClass30.smethod_4("GliderForm.KillButton!Tooltip"));
        toolTip_0.SetToolTip(StopButton, GClass30.smethod_4("GliderForm.StopButton!Tooltip"));
        toolTip_0.SetToolTip(QuickLoadButton, GClass30.smethod_4("GliderForm.QuickLoadButton!Tooltip"));
        toolTip_0.SetToolTip(ConfigButton, GClass30.smethod_4("GliderForm.ConfigButton!Tooltip"));
        toolTip_0.SetToolTip(MyHelpButton, GClass30.smethod_4("GliderForm.MyHelpButton!Tooltip"));
        toolTip_0.SetToolTip(ShrinkButton, GClass30.smethod_4("GliderForm.ShrinkButton!Tooltip"));
        toolTip_0.SetToolTip(HideButton, GClass30.smethod_4("GliderForm.HideButton!Tooltip"));
        if (GClass61.gclass61_0.method_2("WindowPos") != null && GClass61.gclass61_0.method_2("WindowPos").Length > 0)
        {
            var strArray = GClass61.gclass61_0.method_2("WindowPos").Split(',');
            var point_1 = new Point(int.Parse(strArray[0]), int.Parse(strArray[1]));
            if (method_29(point_1))
            {
                StartPosition = FormStartPosition.Manual;
                Location = point_1;
            }
            else
            {
                GClass61.gclass61_0.method_0("WindowPos", "");
                GClass37.smethod_1("Glider saved window position is not visible, using default position instead");
            }
        }

        if (!bool_7)
        {
            SendMessage(tabControl1.Handle, 4874U, 2U, ref rectangle_0);
            GClass37.smethod_1("Rectangle for 1: " + rectangle_0);
            tabControl1.TabPages.Remove(tabPage1);
            bool_7 = true;
            rectangle_0.X += 3;
        }

        method_24();
        StartupClass.form_0 = this;
        method_1();
        StartupClass.smethod_59();
    }

    private void method_1()
    {
        var flag1 = File.Exists("TWfail.txt");
        var flag2 = File.Exists("TWunsafe.txt");
        var flag3 = File.Exists("DeadSession.txt");
        if (flag1)
        {
            File.Delete("TWfail.txt");
            GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "TripwireFailed.html");
        }
        else if (flag2)
        {
            File.Delete("TWunsafe.txt");
            GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "TripwireUnsafe.html");
        }
        else
        {
            if (!flag3)
                return;
            File.Delete("DeadSession.txt");
            GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "DeadSession.html");
        }
    }

    private void method_2(object sender, KeyEventArgs e)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    private static string smethod_0(string string_2)
    {
        if (string_2.LastIndexOf("\\") > -1)
            string_2 = string_2.Substring(string_2.LastIndexOf("\\") + 1);
        return string_2;
    }

    public void method_3(string string_2)
    {
        if (GClass61.gclass61_0.method_2("TitleBarRename") == "True")
        {
            if (GClass61.gclass61_0.method_2("TitleBarRandom") == "True")
            {
                if (string_1 == null)
                    string_1 = GProcessMemoryManipulator.smethod_0();
                StatusLabel.Text = string_1 + " " + string_2;
            }
            else
            {
                StatusLabel.Text = GClass61.gclass61_0.method_2("TitleBarName") + " " + string_2;
            }
        }
        else
        {
            StatusLabel.Text = GClass30.smethod_2(651, "1.8.0", string_2);
        }
    }

    public void method_4()
    {
        if (GClass61.gclass61_0.method_5("AltLayout"))
            Text = StartupClass.string_5 == null
                ? GClass30.smethod_4("GliderForm.StatusLabel!NewProfile")
                : StartupClass.string_5;
        else if (StartupClass.string_5 == null)
            StatusLabel.Text = GClass30.smethod_4("GliderForm.StatusLabel!NewProfile");
        else
            StatusLabel.Text = smethod_0(StartupClass.string_5);
    }

    void Form.Dispose(bool disposing)
    {
        if (disposing)
        {
            if (!StartupClass.bool_2)
                StartupClass.DebuffsKnown_string.method_10();
            StartupClass.smethod_35();
            GClass61.gclass61_0.method_8();
            if (GClass24.bool_0)
                StartupClass.gclass24_0.method_17();
            StartupClass.smethod_15();
            if (icontainer_0 != null)
                icontainer_0.Dispose();
        }

        // ISSUE: explicit non-virtual call
        __nonvirtual(((Form)this).Dispose(disposing));
    }

    [STAThread]
    private static void Main()
    {
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        if (Environment.CommandLine.ToLower().IndexOf("/invisible") > -1)
            GClass81.smethod_0();
        else
            Application.Run(new GliderForm());
    }

    private void method_5()
    {
        icontainer_0 = new Container();
        var componentResourceManager = new ComponentResourceManager(typeof(GliderForm));
        timer_0 = new Timer(icontainer_0);
        AddWaypointButton = new Button();
        helpProvider_0 = new HelpProvider();
        SaveProfileButton = new Button();
        LoadProfileButton = new Button();
        NewProfileButton = new Button();
        EditProfileButton = new Button();
        tabDefault = new TabPage();
        VersionLabel = new Label();
        MyHelpButton = new Button();
        LogBox = new TextBox();
        groupBox2 = new GroupBox();
        QuickLoadButton = new Button();
        ConfigButton = new Button();
        StopButton = new Button();
        KillButton = new Button();
        GlideButton = new Button();
        ShrinkButton = new Button();
        HideButton = new Button();
        groupBox1 = new GroupBox();
        StatusLabel = new Label();
        LabelAttached = new Label();
        label12 = new Label();
        label3_1 = new Label();
        LabelKills_1 = new Label();
        LabelManaHeader_1 = new Label();
        LabelMana_1 = new Label();
        LabelHealth_1 = new Label();
        label1_1 = new Label();
        label4_1 = new Label();
        label5_1 = new Label();
        label6_1 = new Label();
        THealthLabel_1 = new Label();
        TDistanceLabel_1 = new Label();
        TFactionLabel_1 = new Label();
        label7_1 = new Label();
        XPHour_1 = new Label();
        label2_1 = new Label();
        SpeedLabel_1 = new Label();
        tabProfile = new TabPage();
        groupBox6 = new GroupBox();
        WP_NewestLabel_1 = new Label();
        WP_ClosestLabel_1 = new Label();
        WP_FirstLabel_1 = new Label();
        label10_1 = new Label();
        label9_1 = new Label();
        label8_1 = new Label();
        groupBox5 = new GroupBox();
        WPTypeVendor_1 = new RadioButton();
        WPTypeGhost_1 = new RadioButton();
        WPTypeNormal_1 = new RadioButton();
        WPTypeAuto_1 = new RadioButton();
        label11 = new CheckBox();
        groupBox4 = new GroupBox();
        FactionLabel = new Label();
        AddFactionButton = new Button();
        groupBox3 = new GroupBox();
        tabControl1 = new TabControl();
        tabPage1 = new TabPage();
        toolTip_0 = new ToolTip(icontainer_0);
        notifyIcon_0 = new NotifyIcon(icontainer_0);
        contextMenuStrip2 = new ContextMenuStrip(icontainer_0);
        showWindowToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator2 = new ToolStripSeparator();
        exitToolStripMenuItem = new ToolStripMenuItem();
        ContextMenuStripWindow = new ContextMenuStrip(icontainer_0);
        alwaysOnTopToolStripMenuItem1 = new ToolStripMenuItem();
        minimizeToTrayToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator1 = new ToolStripSeparator();
        exitToolStripMenuItem1 = new ToolStripMenuItem();
        Location_3d = new Label();
        locXYZLabel = new Label();
        tabDefault.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox1.SuspendLayout();
        tabProfile.SuspendLayout();
        groupBox6.SuspendLayout();
        groupBox5.SuspendLayout();
        groupBox4.SuspendLayout();
        groupBox3.SuspendLayout();
        tabControl1.SuspendLayout();
        contextMenuStrip2.SuspendLayout();
        ContextMenuStripWindow.SuspendLayout();
        SuspendLayout();
        timer_0.Enabled = true;
        timer_0.Interval = 350;
        timer_0.Tick += timer_0_Tick;
        AddWaypointButton.Enabled = false;
        helpProvider_0.SetHelpKeyword(AddWaypointButton, "MainWindow.html#AddWaypoint");
        helpProvider_0.SetHelpNavigator(AddWaypointButton, HelpNavigator.Topic);
        AddWaypointButton.Location = new Point(227, 21);
        AddWaypointButton.Name = "AddWaypointButton";
        helpProvider_0.SetShowHelp(AddWaypointButton, true);
        AddWaypointButton.Size = new Size(119, 27);
        AddWaypointButton.TabIndex = 8;
        AddWaypointButton.Text = "Add Waypoint";
        AddWaypointButton.UseVisualStyleBackColor = false;
        AddWaypointButton.Click += AddWaypointButton_Click;
        helpProvider_0.HelpNamespace = ".\\Glider.chm";
        SaveProfileButton.Enabled = false;
        helpProvider_0.SetHelpKeyword(SaveProfileButton, "MainWindow.html#Profiles");
        helpProvider_0.SetHelpNavigator(SaveProfileButton, HelpNavigator.Topic);
        SaveProfileButton.Location = new Point(201, 27);
        SaveProfileButton.Name = "SaveProfileButton";
        helpProvider_0.SetShowHelp(SaveProfileButton, true);
        SaveProfileButton.Size = new Size(119, 27);
        SaveProfileButton.TabIndex = 11;
        SaveProfileButton.Text = "Save Profile";
        SaveProfileButton.UseVisualStyleBackColor = false;
        SaveProfileButton.Click += SaveProfileButton_Click;
        helpProvider_0.SetHelpKeyword(LoadProfileButton, "MainWindow.html#Profiles");
        helpProvider_0.SetHelpNavigator(LoadProfileButton, HelpNavigator.Topic);
        LoadProfileButton.Location = new Point(201, 60);
        LoadProfileButton.Name = "LoadProfileButton";
        helpProvider_0.SetShowHelp(LoadProfileButton, true);
        LoadProfileButton.Size = new Size(119, 27);
        LoadProfileButton.TabIndex = 9;
        LoadProfileButton.Text = "Load Profile";
        LoadProfileButton.UseVisualStyleBackColor = false;
        LoadProfileButton.Click += LoadProfileButton_Click;
        helpProvider_0.SetHelpKeyword(NewProfileButton, "MainWindow.html#Profiles");
        helpProvider_0.SetHelpNavigator(NewProfileButton, HelpNavigator.Topic);
        NewProfileButton.Location = new Point(35, 27);
        NewProfileButton.Name = "NewProfileButton";
        helpProvider_0.SetShowHelp(NewProfileButton, true);
        NewProfileButton.Size = new Size(119, 27);
        NewProfileButton.TabIndex = 8;
        NewProfileButton.Text = "New Profile";
        NewProfileButton.UseVisualStyleBackColor = false;
        NewProfileButton.Click += NewProfileButton_Click;
        EditProfileButton.Enabled = false;
        helpProvider_0.SetHelpKeyword(EditProfileButton, "MainWindow.html#Profiles");
        helpProvider_0.SetHelpNavigator(EditProfileButton, HelpNavigator.Topic);
        EditProfileButton.Location = new Point(35, 60);
        EditProfileButton.Name = "EditProfileButton";
        helpProvider_0.SetShowHelp(EditProfileButton, true);
        EditProfileButton.Size = new Size(119, 27);
        EditProfileButton.TabIndex = 10;
        EditProfileButton.Text = "Edit Profile";
        EditProfileButton.UseVisualStyleBackColor = false;
        EditProfileButton.Click += EditProfileButton_Click;
        tabDefault.Controls.Add(VersionLabel);
        tabDefault.Controls.Add(MyHelpButton);
        tabDefault.Controls.Add(LogBox);
        tabDefault.Controls.Add(groupBox2);
        tabDefault.Controls.Add(groupBox1);
        helpProvider_0.SetHelpNavigator(tabDefault, HelpNavigator.TopicId);
        helpProvider_0.SetHelpString(tabDefault, "MainWindow.html");
        tabDefault.Location = new Point(4, 22);
        tabDefault.Name = "tabDefault";
        tabDefault.Padding = new Padding(3);
        helpProvider_0.SetShowHelp(tabDefault, true);
        tabDefault.Size = new Size(364, 559);
        tabDefault.TabIndex = 0;
        tabDefault.Text = "Default";
        tabDefault.UseVisualStyleBackColor = true;
        VersionLabel.ForeColor = SystemColors.Highlight;
        VersionLabel.Location = new Point(6, 494);
        VersionLabel.Name = "VersionLabel";
        VersionLabel.Size = new Size(300, 28);
        VersionLabel.TabIndex = 39;
        VersionLabel.Text = "(glider version label)";
        VersionLabel.TextAlign = ContentAlignment.TopCenter;
        MyHelpButton.Image = (Image)componentResourceManager.GetObject("MyHelpButton.Image");
        MyHelpButton.Location = new Point(312, 482);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(40, 40);
        MyHelpButton.TabIndex = 38;
        MyHelpButton.UseVisualStyleBackColor = true;
        MyHelpButton.Click += MyHelpButton_Click;
        LogBox.BackColor = Color.FromArgb(254, 239, 200);
        LogBox.Font = new Font("Microsoft Sans Serif", 7f);
        LogBox.Location = new Point(3, 286);
        LogBox.Multiline = true;
        LogBox.Name = "LogBox";
        LogBox.ScrollBars = ScrollBars.Vertical;
        LogBox.Size = new Size(349, 190);
        LogBox.TabIndex = 37;
        LogBox.DoubleClick += LogBox_DoubleClick;
        groupBox2.Controls.Add(QuickLoadButton);
        groupBox2.Controls.Add(ConfigButton);
        groupBox2.Controls.Add(StopButton);
        groupBox2.Controls.Add(KillButton);
        groupBox2.Controls.Add(GlideButton);
        groupBox2.Controls.Add(ShrinkButton);
        groupBox2.Controls.Add(HideButton);
        groupBox2.Location = new Point(6, 188);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(349, 92);
        groupBox2.TabIndex = 36;
        groupBox2.TabStop = false;
        groupBox2.Text = "Action";
        QuickLoadButton.Image = (Image)componentResourceManager.GetObject("QuickLoadButton.Image");
        QuickLoadButton.Location = new Point(221, 32);
        QuickLoadButton.Name = "QuickLoadButton";
        QuickLoadButton.Size = new Size(40, 40);
        QuickLoadButton.TabIndex = 19;
        QuickLoadButton.UseVisualStyleBackColor = true;
        QuickLoadButton.Click += QuickLoadButton_Click;
        ConfigButton.Image = (Image)componentResourceManager.GetObject("ConfigButton.Image");
        ConfigButton.Location = new Point(289, 32);
        ConfigButton.Name = "ConfigButton";
        ConfigButton.Size = new Size(40, 40);
        ConfigButton.TabIndex = 18;
        ConfigButton.UseVisualStyleBackColor = true;
        ConfigButton.Click += ConfigButton_Click;
        StopButton.Enabled = false;
        StopButton.Image = (Image)componentResourceManager.GetObject("StopButton.Image");
        StopButton.Location = new Point(153, 32);
        StopButton.Name = "StopButton";
        StopButton.Size = new Size(40, 40);
        StopButton.TabIndex = 17;
        StopButton.UseVisualStyleBackColor = true;
        StopButton.Click += StopButton_Click;
        KillButton.Enabled = false;
        KillButton.Image = (Image)componentResourceManager.GetObject("KillButton.Image");
        KillButton.Location = new Point(85, 32);
        KillButton.Name = "KillButton";
        KillButton.Size = new Size(40, 40);
        KillButton.TabIndex = 16;
        KillButton.UseVisualStyleBackColor = true;
        KillButton.Click += KillButton_Click;
        GlideButton.Enabled = false;
        GlideButton.Image = (Image)componentResourceManager.GetObject("GlideButton.Image");
        GlideButton.Location = new Point(17, 32);
        GlideButton.Name = "GlideButton";
        GlideButton.Size = new Size(40, 40);
        GlideButton.TabIndex = 15;
        GlideButton.UseVisualStyleBackColor = true;
        GlideButton.Click += GlideButton_Click;
        ShrinkButton.Enabled = false;
        ShrinkButton.Image = (Image)componentResourceManager.GetObject("ShrinkButton.Image");
        ShrinkButton.Location = new Point(17, 32);
        ShrinkButton.Name = "ShrinkButton";
        ShrinkButton.Size = new Size(40, 40);
        ShrinkButton.TabIndex = 20;
        ShrinkButton.UseVisualStyleBackColor = true;
        ShrinkButton.Visible = false;
        ShrinkButton.Click += ShrinkButton_Click;
        HideButton.Enabled = false;
        HideButton.Image = (Image)componentResourceManager.GetObject("HideButton.Image");
        HideButton.Location = new Point(85, 32);
        HideButton.Name = "HideButton";
        HideButton.Size = new Size(40, 40);
        HideButton.TabIndex = 21;
        HideButton.UseVisualStyleBackColor = true;
        HideButton.Visible = false;
        HideButton.Click += HideButton_Click;
        groupBox1.Controls.Add(StatusLabel);
        groupBox1.Controls.Add(LabelAttached);
        groupBox1.Controls.Add(label12);
        groupBox1.Controls.Add(label3_1);
        groupBox1.Controls.Add(LabelKills_1);
        groupBox1.Controls.Add(LabelManaHeader_1);
        groupBox1.Controls.Add(LabelMana_1);
        groupBox1.Controls.Add(LabelHealth_1);
        groupBox1.Controls.Add(label1_1);
        groupBox1.Controls.Add(label4_1);
        groupBox1.Controls.Add(label5_1);
        groupBox1.Controls.Add(label6_1);
        groupBox1.Controls.Add(THealthLabel_1);
        groupBox1.Controls.Add(TDistanceLabel_1);
        groupBox1.Controls.Add(TFactionLabel_1);
        groupBox1.Controls.Add(label7_1);
        groupBox1.Controls.Add(XPHour_1);
        groupBox1.Controls.Add(label2_1);
        groupBox1.Controls.Add(SpeedLabel_1);
        groupBox1.Location = new Point(6, 6);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(349, 176);
        groupBox1.TabIndex = 35;
        groupBox1.TabStop = false;
        groupBox1.Text = "Status";
        StatusLabel.BackColor = Color.Transparent;
        StatusLabel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        StatusLabel.ForeColor = SystemColors.Highlight;
        StatusLabel.Location = new Point(17, 150);
        StatusLabel.Name = "StatusLabel";
        StatusLabel.Size = new Size(312, 18);
        StatusLabel.TabIndex = 50;
        StatusLabel.Text = "(status label with profile, etc)";
        StatusLabel.TextAlign = ContentAlignment.TopCenter;
        LabelAttached.AutoSize = true;
        LabelAttached.BackColor = Color.Transparent;
        LabelAttached.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelAttached.ForeColor = SystemColors.Highlight;
        LabelAttached.Location = new Point(101, 31);
        LabelAttached.Name = "LabelAttached";
        LabelAttached.Size = new Size(21, 13);
        LabelAttached.TabIndex = 49;
        LabelAttached.Text = "No";
        label12.BackColor = Color.Transparent;
        label12.Location = new Point(9, 31);
        label12.Name = "label12";
        label12.Size = new Size(86, 16);
        label12.TabIndex = 48;
        label12.Text = "Attached:";
        label12.TextAlign = ContentAlignment.TopRight;
        label3_1.BackColor = Color.Transparent;
        label3_1.Location = new Point(9, 97);
        label3_1.Name = "label3";
        label3_1.Size = new Size(86, 16);
        label3_1.TabIndex = 36;
        label3_1.Text = "Kills:";
        label3_1.TextAlign = ContentAlignment.TopRight;
        LabelKills_1.AutoSize = true;
        LabelKills_1.BackColor = Color.Transparent;
        LabelKills_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelKills_1.ForeColor = SystemColors.Highlight;
        LabelKills_1.Location = new Point(101, 97);
        LabelKills_1.Name = "LabelKills";
        LabelKills_1.Size = new Size(47, 13);
        LabelKills_1.TabIndex = 37;
        LabelKills_1.Text = "0 / 0 / 0";
        LabelManaHeader_1.BackColor = Color.Transparent;
        LabelManaHeader_1.Location = new Point(9, 76);
        LabelManaHeader_1.Name = "LabelManaHeader";
        LabelManaHeader_1.Size = new Size(86, 16);
        LabelManaHeader_1.TabIndex = 34;
        LabelManaHeader_1.Text = "Mana:";
        LabelManaHeader_1.TextAlign = ContentAlignment.TopRight;
        LabelMana_1.AutoSize = true;
        LabelMana_1.BackColor = Color.Transparent;
        LabelMana_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelMana_1.ForeColor = SystemColors.Highlight;
        LabelMana_1.Location = new Point(101, 75);
        LabelMana_1.Name = "LabelMana";
        LabelMana_1.Size = new Size(19, 13);
        LabelMana_1.TabIndex = 35;
        LabelMana_1.Text = "??";
        LabelHealth_1.AutoSize = true;
        LabelHealth_1.BackColor = Color.Transparent;
        LabelHealth_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelHealth_1.ForeColor = SystemColors.Highlight;
        LabelHealth_1.Location = new Point(101, 53);
        LabelHealth_1.Name = "LabelHealth";
        LabelHealth_1.Size = new Size(19, 13);
        LabelHealth_1.TabIndex = 33;
        LabelHealth_1.Text = "??";
        label1_1.BackColor = Color.Transparent;
        label1_1.Location = new Point(9, 53);
        label1_1.Name = "label1";
        label1_1.Size = new Size(86, 16);
        label1_1.TabIndex = 32;
        label1_1.Text = "Health:";
        label1_1.TextAlign = ContentAlignment.TopRight;
        label4_1.AutoSize = true;
        label4_1.BackColor = Color.Transparent;
        label4_1.Location = new Point(206, 53);
        label4_1.Name = "label4";
        label4_1.Size = new Size(51, 13);
        label4_1.TabIndex = 40;
        label4_1.Text = "T-Health:";
        label4_1.TextAlign = ContentAlignment.TopRight;
        label5_1.AutoSize = true;
        label5_1.BackColor = Color.Transparent;
        label5_1.Location = new Point(192, 75);
        label5_1.Name = "label5";
        label5_1.Size = new Size(62, 13);
        label5_1.TabIndex = 41;
        label5_1.Text = "T-Distance:";
        label5_1.TextAlign = ContentAlignment.TopRight;
        label6_1.AutoSize = true;
        label6_1.BackColor = Color.Transparent;
        label6_1.Location = new Point(201, 97);
        label6_1.Name = "label6";
        label6_1.Size = new Size(55, 13);
        label6_1.TabIndex = 42;
        label6_1.Text = "T-Faction:";
        label6_1.TextAlign = ContentAlignment.TopRight;
        THealthLabel_1.AutoSize = true;
        THealthLabel_1.BackColor = Color.Transparent;
        THealthLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f);
        THealthLabel_1.ForeColor = SystemColors.Highlight;
        THealthLabel_1.Location = new Point(279, 53);
        THealthLabel_1.Name = "THealthLabel";
        THealthLabel_1.Size = new Size(19, 13);
        THealthLabel_1.TabIndex = 43;
        THealthLabel_1.Text = "??";
        TDistanceLabel_1.AutoSize = true;
        TDistanceLabel_1.BackColor = Color.Transparent;
        TDistanceLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f);
        TDistanceLabel_1.ForeColor = SystemColors.Highlight;
        TDistanceLabel_1.Location = new Point(279, 75);
        TDistanceLabel_1.Name = "TDistanceLabel";
        TDistanceLabel_1.Size = new Size(19, 13);
        TDistanceLabel_1.TabIndex = 44;
        TDistanceLabel_1.Text = "??";
        TFactionLabel_1.AutoSize = true;
        TFactionLabel_1.BackColor = Color.Transparent;
        TFactionLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        TFactionLabel_1.ForeColor = SystemColors.Highlight;
        TFactionLabel_1.Location = new Point(279, 97);
        TFactionLabel_1.Name = "TFactionLabel";
        TFactionLabel_1.Size = new Size(19, 13);
        TFactionLabel_1.TabIndex = 45;
        TFactionLabel_1.Text = "??";
        label7_1.AutoSize = true;
        label7_1.BackColor = Color.Transparent;
        label7_1.Location = new Point(208, 119);
        label7_1.Name = "label7";
        label7_1.Size = new Size(52, 13);
        label7_1.TabIndex = 46;
        label7_1.Text = "XP/Hour:";
        label7_1.TextAlign = ContentAlignment.TopRight;
        XPHour_1.AutoSize = true;
        XPHour_1.BackColor = Color.Transparent;
        XPHour_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        XPHour_1.ForeColor = SystemColors.Highlight;
        XPHour_1.Location = new Point(279, 119);
        XPHour_1.Name = "XPHour";
        XPHour_1.Size = new Size(19, 13);
        XPHour_1.TabIndex = 47;
        XPHour_1.Text = "??";
        label2_1.BackColor = Color.Transparent;
        label2_1.Location = new Point(9, 120);
        label2_1.Name = "label2";
        label2_1.Size = new Size(86, 16);
        label2_1.TabIndex = 38;
        label2_1.Text = "Speed:";
        label2_1.TextAlign = ContentAlignment.TopRight;
        SpeedLabel_1.AutoSize = true;
        SpeedLabel_1.BackColor = Color.Transparent;
        SpeedLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        SpeedLabel_1.ForeColor = SystemColors.Highlight;
        SpeedLabel_1.Location = new Point(101, 119);
        SpeedLabel_1.Name = "SpeedLabel";
        SpeedLabel_1.Size = new Size(19, 13);
        SpeedLabel_1.TabIndex = 39;
        SpeedLabel_1.Text = "??";
        tabProfile.Controls.Add(groupBox6);
        tabProfile.Controls.Add(groupBox5);
        tabProfile.Controls.Add(groupBox4);
        tabProfile.Controls.Add(groupBox3);
        helpProvider_0.SetHelpNavigator(tabProfile, HelpNavigator.TopicId);
        helpProvider_0.SetHelpString(tabProfile, "ProfileView.html");
        tabProfile.Location = new Point(4, 22);
        tabProfile.Name = "tabProfile";
        tabProfile.Padding = new Padding(3);
        helpProvider_0.SetShowHelp(tabProfile, true);
        tabProfile.Size = new Size(364, 559);
        tabProfile.TabIndex = 1;
        tabProfile.Text = "Profile";
        tabProfile.UseVisualStyleBackColor = true;
        groupBox6.Controls.Add(locXYZLabel);
        groupBox6.Controls.Add(Location_3d);
        groupBox6.Controls.Add(WP_NewestLabel_1);
        groupBox6.Controls.Add(WP_ClosestLabel_1);
        groupBox6.Controls.Add(WP_FirstLabel_1);
        groupBox6.Controls.Add(label10_1);
        groupBox6.Controls.Add(label9_1);
        groupBox6.Controls.Add(label8_1);
        groupBox6.Location = new Point(5, 266);
        groupBox6.Name = "groupBox6";
        groupBox6.Size = new Size(352, 128);
        groupBox6.TabIndex = 3;
        groupBox6.TabStop = false;
        groupBox6.Text = "Location";
        WP_NewestLabel_1.AutoSize = true;
        WP_NewestLabel_1.BackColor = Color.Transparent;
        WP_NewestLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        WP_NewestLabel_1.ForeColor = SystemColors.Highlight;
        WP_NewestLabel_1.Location = new Point(82, 81);
        WP_NewestLabel_1.Name = "WP_NewestLabel";
        WP_NewestLabel_1.Size = new Size(19, 13);
        WP_NewestLabel_1.TabIndex = 10;
        WP_NewestLabel_1.Text = "??";
        WP_NewestLabel_1.TextAlign = ContentAlignment.MiddleLeft;
        WP_ClosestLabel_1.AutoSize = true;
        WP_ClosestLabel_1.BackColor = Color.Transparent;
        WP_ClosestLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        WP_ClosestLabel_1.ForeColor = SystemColors.Highlight;
        WP_ClosestLabel_1.Location = new Point(82, 57);
        WP_ClosestLabel_1.Name = "WP_ClosestLabel";
        WP_ClosestLabel_1.Size = new Size(19, 13);
        WP_ClosestLabel_1.TabIndex = 9;
        WP_ClosestLabel_1.Text = "??";
        WP_ClosestLabel_1.TextAlign = ContentAlignment.MiddleLeft;
        WP_FirstLabel_1.AutoSize = true;
        WP_FirstLabel_1.BackColor = Color.Transparent;
        WP_FirstLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        WP_FirstLabel_1.ForeColor = SystemColors.Highlight;
        WP_FirstLabel_1.Location = new Point(82, 33);
        WP_FirstLabel_1.Name = "WP_FirstLabel";
        WP_FirstLabel_1.Size = new Size(19, 13);
        WP_FirstLabel_1.TabIndex = 8;
        WP_FirstLabel_1.Text = "??";
        WP_FirstLabel_1.TextAlign = ContentAlignment.MiddleLeft;
        label10_1.AutoSize = true;
        label10_1.BackColor = Color.Transparent;
        label10_1.Location = new Point(41, 81);
        label10_1.Name = "label10";
        label10_1.Size = new Size(32, 13);
        label10_1.TabIndex = 7;
        label10_1.Text = "Next:";
        label10_1.TextAlign = ContentAlignment.MiddleRight;
        label9_1.AutoSize = true;
        label9_1.BackColor = Color.Transparent;
        label9_1.Location = new Point(22, 33);
        label9_1.Name = "label9";
        label9_1.Size = new Size(51, 13);
        label9_1.TabIndex = 6;
        label9_1.Text = "Previous:";
        label9_1.TextAlign = ContentAlignment.MiddleRight;
        label8_1.AutoSize = true;
        label8_1.BackColor = Color.Transparent;
        label8_1.Location = new Point(29, 57);
        label8_1.Name = "label8";
        label8_1.Size = new Size(44, 13);
        label8_1.TabIndex = 5;
        label8_1.Text = "Closest:";
        label8_1.TextAlign = ContentAlignment.MiddleRight;
        groupBox5.Controls.Add(WPTypeVendor_1);
        groupBox5.Controls.Add(WPTypeGhost_1);
        groupBox5.Controls.Add(WPTypeNormal_1);
        groupBox5.Controls.Add(WPTypeAuto_1);
        groupBox5.Controls.Add(label11);
        groupBox5.Controls.Add(AddWaypointButton);
        groupBox5.Location = new Point(6, 117);
        groupBox5.Name = "groupBox5";
        groupBox5.Size = new Size(352, 143);
        groupBox5.TabIndex = 2;
        groupBox5.TabStop = false;
        groupBox5.Text = "Waypoints";
        WPTypeVendor_1.AutoSize = true;
        WPTypeVendor_1.Location = new Point(186, 97);
        WPTypeVendor_1.Name = "WPTypeVendor";
        WPTypeVendor_1.Size = new Size(59, 17);
        WPTypeVendor_1.TabIndex = 15;
        WPTypeVendor_1.Text = "Vendor";
        WPTypeVendor_1.CheckedChanged += WPTypeVendor_1_CheckedChanged;
        WPTypeGhost_1.AutoSize = true;
        WPTypeGhost_1.Location = new Point(186, 70);
        WPTypeGhost_1.Name = "WPTypeGhost";
        WPTypeGhost_1.Size = new Size(53, 17);
        WPTypeGhost_1.TabIndex = 14;
        WPTypeGhost_1.Text = "Ghost";
        WPTypeGhost_1.CheckedChanged += WPTypeGhost_1_CheckedChanged;
        WPTypeNormal_1.AutoSize = true;
        WPTypeNormal_1.Location = new Point(56, 97);
        WPTypeNormal_1.Name = "WPTypeNormal";
        WPTypeNormal_1.Size = new Size(58, 17);
        WPTypeNormal_1.TabIndex = 13;
        WPTypeNormal_1.Text = "Normal";
        WPTypeNormal_1.CheckedChanged += WPTypeNormal_1_CheckedChanged;
        WPTypeAuto_1.AutoSize = true;
        WPTypeAuto_1.Checked = true;
        WPTypeAuto_1.Location = new Point(56, 70);
        WPTypeAuto_1.Name = "WPTypeAuto";
        WPTypeAuto_1.Size = new Size(72, 17);
        WPTypeAuto_1.TabIndex = 12;
        WPTypeAuto_1.TabStop = true;
        WPTypeAuto_1.Text = "Automatic";
        WPTypeAuto_1.CheckedChanged += WPTypeAuto_1_CheckedChanged;
        label11.AutoSize = true;
        label11.Location = new Point(26, 30);
        label11.Name = "AutoAddToggle";
        label11.Size = new Size(119, 17);
        label11.TabIndex = 9;
        label11.Text = "Auto-add waypoints";
        label11.CheckedChanged += label11_CheckedChanged;
        groupBox4.Controls.Add(FactionLabel);
        groupBox4.Controls.Add(AddFactionButton);
        groupBox4.Location = new Point(6, 399);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new Size(352, 86);
        groupBox4.TabIndex = 1;
        groupBox4.TabStop = false;
        groupBox4.Text = "Faction";
        FactionLabel.ForeColor = SystemColors.Highlight;
        FactionLabel.Location = new Point(32, 30);
        FactionLabel.Name = "FactionLabel";
        FactionLabel.Size = new Size(181, 36);
        FactionLabel.TabIndex = 1;
        FactionLabel.Text = "(text here about the monster's faction)";
        AddFactionButton.Location = new Point(219, 30);
        AddFactionButton.Name = "AddFactionButton";
        AddFactionButton.Size = new Size(107, 27);
        AddFactionButton.TabIndex = 0;
        AddFactionButton.Text = "Add Faction";
        AddFactionButton.UseVisualStyleBackColor = true;
        AddFactionButton.Click += AddFactionButton_Click;
        groupBox3.Controls.Add(SaveProfileButton);
        groupBox3.Controls.Add(LoadProfileButton);
        groupBox3.Controls.Add(NewProfileButton);
        groupBox3.Controls.Add(EditProfileButton);
        groupBox3.Location = new Point(6, 6);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(352, 105);
        groupBox3.TabIndex = 0;
        groupBox3.TabStop = false;
        groupBox3.Text = "Load/Save";
        tabControl1.Controls.Add(tabDefault);
        tabControl1.Controls.Add(tabProfile);
        tabControl1.Controls.Add(tabPage1);
        tabControl1.Location = new Point(12, 12);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(372, 585);
        tabControl1.TabIndex = 36;
        tabPage1.Location = new Point(4, 22);
        tabPage1.Name = "tabPage1";
        tabPage1.Size = new Size(364, 559);
        tabPage1.TabIndex = 2;
        tabPage1.Text = "tabPage1";
        tabPage1.UseVisualStyleBackColor = true;
        notifyIcon_0.ContextMenuStrip = contextMenuStrip2;
        notifyIcon_0.Icon = (Icon)componentResourceManager.GetObject("notifyIcon1.Icon");
        notifyIcon_0.Text = "Glider";
        notifyIcon_0.Visible = true;
        notifyIcon_0.MouseDoubleClick += notifyIcon_0_MouseDoubleClick;
        contextMenuStrip2.Items.AddRange(new ToolStripItem[3]
        {
            showWindowToolStripMenuItem,
            toolStripSeparator2,
            exitToolStripMenuItem
        });
        contextMenuStrip2.Name = "contextMenuStrip2";
        contextMenuStrip2.Size = new Size(149, 54);
        showWindowToolStripMenuItem.Name = "showWindowToolStripMenuItem";
        showWindowToolStripMenuItem.Size = new Size(148, 22);
        showWindowToolStripMenuItem.Text = "Show window";
        showWindowToolStripMenuItem.Click += showWindowToolStripMenuItem_Click;
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new Size(145, 6);
        exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        exitToolStripMenuItem.Size = new Size(148, 22);
        exitToolStripMenuItem.Text = "Exit";
        exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
        ContextMenuStripWindow.Items.AddRange(new ToolStripItem[4]
        {
            alwaysOnTopToolStripMenuItem1,
            minimizeToTrayToolStripMenuItem,
            toolStripSeparator1,
            exitToolStripMenuItem1
        });
        ContextMenuStripWindow.Name = "ContextMenuStripWindow";
        ContextMenuStripWindow.Size = new Size(161, 76);
        alwaysOnTopToolStripMenuItem1.Name = "alwaysOnTopToolStripMenuItem1";
        alwaysOnTopToolStripMenuItem1.Size = new Size(160, 22);
        alwaysOnTopToolStripMenuItem1.Text = "Always on top";
        alwaysOnTopToolStripMenuItem1.Click += alwaysOnTopToolStripMenuItem1_Click;
        minimizeToTrayToolStripMenuItem.Name = "minimizeToTrayToolStripMenuItem";
        minimizeToTrayToolStripMenuItem.Size = new Size(160, 22);
        minimizeToTrayToolStripMenuItem.Text = "Minimize to tray";
        minimizeToTrayToolStripMenuItem.Click += minimizeToTrayToolStripMenuItem_Click;
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new Size(157, 6);
        exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
        exitToolStripMenuItem1.Size = new Size(160, 22);
        exitToolStripMenuItem1.Text = "Exit";
        exitToolStripMenuItem1.Click += exitToolStripMenuItem1_Click;
        Location_3d.AutoSize = true;
        Location_3d.ForeColor = SystemColors.Highlight;
        Location_3d.Location = new Point(82, 104);
        Location_3d.Name = "Location_3d";
        Location_3d.Size = new Size(19, 13);
        Location_3d.TabIndex = 11;
        Location_3d.Text = "??";
        Location_3d.TextAlign = ContentAlignment.MiddleLeft;
        Location_3d.Click += Location_3d_Click;
        locXYZLabel.AutoSize = true;
        locXYZLabel.Location = new Point(30, 104);
        locXYZLabel.Name = "locXYZLabel";
        locXYZLabel.Size = new Size(43, 13);
        locXYZLabel.TabIndex = 12;
        locXYZLabel.Text = "Coords:";
        BackgroundImage = (Image)componentResourceManager.GetObject("$this.BackgroundImage");
        ClientSize = new Size(396, 609);
        ContextMenuStrip = ContextMenuStripWindow;
        ControlBox = false;
        Controls.Add(tabControl1);
        FormBorderStyle = FormBorderStyle.Fixed3D;
        HelpButton = true;
        helpProvider_0.SetHelpKeyword(this, "MainWindow.html");
        helpProvider_0.SetHelpNavigator(this, HelpNavigator.Topic);
        MaximizeBox = false;
        Name = nameof(GliderForm);
        helpProvider_0.SetShowHelp(this, true);
        ShowInTaskbar = false;
        MouseUp += GliderForm_MouseUp;
        MouseMove += GliderForm_MouseMove;
        MouseDown += GliderForm_MouseDown;
        tabDefault.ResumeLayout(false);
        tabDefault.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        tabProfile.ResumeLayout(false);
        groupBox6.ResumeLayout(false);
        groupBox6.PerformLayout();
        groupBox5.ResumeLayout(false);
        groupBox5.PerformLayout();
        groupBox4.ResumeLayout(false);
        groupBox3.ResumeLayout(false);
        tabControl1.ResumeLayout(false);
        contextMenuStrip2.ResumeLayout(false);
        ContextMenuStripWindow.ResumeLayout(false);
        ResumeLayout(false);
    }

    private void method_6(string string_2)
    {
        var now = DateTime.Now;
        var path = "Glider.log";
        try
        {
            var streamWriter = File.AppendText(path);
            streamWriter.WriteLine(now.ToString("HH:mm:ss.ffff ") + string_2);
            streamWriter.Flush();
            streamWriter.Close();
        }
        catch (IOException ex)
        {
            Console.WriteLine(GClass30.smethod_2(90, ex.Message));
        }
    }

    private void StopButton_Click(object sender, EventArgs e)
    {
        if (GClass61.gclass61_0.method_5("AltLayout"))
        {
            if (StartupClass.glideMode_0 != GlideMode.None)
            {
                StartupClass.bool_28 = false;
                StartupClass.smethod_27(false, "StopButtonClicked");
            }
            else
            {
                WaypointsPanel.Visible = !WaypointsPanel.Visible;
                MainPanel.Visible = !MainPanel.Visible;
            }
        }
        else
        {
            StartupClass.bool_28 = false;
            if (StartupClass.glideMode_0 != GlideMode.None)
                StartupClass.smethod_27(false, "StopButtonClicked");
        }

        method_16();
    }

    private void timer_0_Tick(object sender, EventArgs e)
    {
        if (bool_5)
        {
            notifyIcon_0.Visible = false;
            Close();
        }
        else
        {
            try
            {
                method_7();
                if (bool_0)
                    return;
                bool_0 = true;
                method_22();
            }
            catch (Exception ex)
            {
                timer_0.Enabled = false;
                GClass37.smethod_0("Timer exception in Glider: The exception is: " + ex.Message + ", " + ex.StackTrace);
                var num = (int)MessageBox.Show(ex.GetType() + "\n\n" + ex.Message + "\n\n" + ex.StackTrace,
                    GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                StartupClass.smethod_27(false, "TimerExcep");
                Environment.Exit(0);
            }
        }
    }

    private void method_7()
    {
        method_20();
        method_8();
        if (StartupClass.gclass36_0 != null && StartupClass.gclass36_0.method_3())
        {
            StartupClass.gclass36_0 = null;
            StartupClass.bool_19 = true;
            gliderForm_0.GInterface0\u002Eimethod_2(GClass30.smethod_1(103));
            StartupClass.smethod_27(false, "Timer1Up");
        }

        if (bool_2)
        {
            bool_2 = false;
            LogBox.Text = string_0;
            LogBox.Select(LogBox.Text.Length, LogBox.Text.Length);
            LogBox.ScrollToCaret();
            LogBox.Refresh();
        }

        if (StartupClass.bool_13 && GPlayerSelf.Me != null)
        {
            if (glocation_0 != null && Environment.TickCount - int_3 > 1200)
            {
                SpeedLabel_1.Text =
                    Math.Round(
                        glocation_0.GetDistanceTo(GPlayerSelf.Me.Location) / ((Environment.TickCount - int_3) / 1000.0),
                        2).ToString();
                int_3 = Environment.TickCount;
                glocation_0 = GPlayerSelf.Me.Location;
            }
            else if (glocation_0 == null)
            {
                int_3 = Environment.TickCount;
                glocation_0 = GPlayerSelf.Me.Location;
            }

            LabelHealth_1.Text = GClass30.smethod_2(653, GPlayerSelf.Me.HealthPoints.ToString(),
                (int)(GPlayerSelf.Me.Health * 100.0));
            LabelKills_1.Text = GClass30.smethod_2(654, StartupClass.int_7, StartupClass.int_8, StartupClass.int_9);
            XPHour_1.Text = StartupClass.smethod_29().ToString();
            if (GPlayerSelf.Me.IsCasting)
                LabelKills_1.Text += " *";
            if (StartupClass.ggameClass_0 != null)
                LabelMana_1.Text = StartupClass.ggameClass_0.PowerValue;
            var target = GPlayerSelf.Me.Target;
            if (target != null)
            {
                AddFactionButton.Enabled = true;
                THealthLabel_1.Text = target.Health + GClass30.smethod_1(104);
                TDistanceLabel_1.Text = Math.Round(target.DistanceToSelf, 2).ToString();
                TFactionLabel_1.Text = target.FactionID.ToString();
                if (StartupClass.gprofile_0.CheckFaction(target.FactionID, true))
                {
                    AddFactionButton.Text = "Del Faction";
                    FactionLabel.Text = GClass30.smethod_4("GliderForm.FactionLabel!AlreadyGot");
                }
                else
                {
                    AddFactionButton.Text = "Add Faction";
                    FactionLabel.Text = GClass30.smethod_4("GliderForm.FactionLabel!NotFound");
                }
            }
            else
            {
                THealthLabel_1.Text = "";
                TDistanceLabel_1.Text = "";
                TFactionLabel_1.Text = "";
                AddFactionButton.Enabled = false;
                FactionLabel.Text = GClass30.smethod_4("GliderForm.FactionLabel!NoTarget");
            }

            if (StartupClass.gclass73_0 != null && StartupClass.gclass73_0.int_8 > 0 && StartupClass.gclass73_0.bool_9)
            {
                XPHour_1.Text =
                    Math.Round(
                        StartupClass.gclass73_0.int_8 / (DateTime.Now - StartupClass.dateTime_0).TotalSeconds * 3600.0,
                        0).ToString();
                StartupClass.gclass73_0.bool_9 = false;
            }

            method_18();
        }

        StartupClass.smethod_38();
        if (StartupClass.bool_21 && StartupClass.gclass36_1.method_3())
        {
            StartupClass.bool_21 = false;
            this.GInterface0\u002Eimethod_2(GClass30.smethod_1(105));
            GClass55.smethod_28(GClass30.smethod_1(655));
        }

        if (StartupClass.int_3 == 0 && StartupClass.int_12 != 0 && StartupClass.bool_8)
            method_25();
        if (!gclass36_0.method_3() || !(StartupClass.intptr_0 != IntPtr.Zero))
            return;
        method_9();
    }

    private void method_8()
    {
        if (bool_6 || !GClass61.gclass61_0.method_5("ManageGamePos") || !(StartupClass.intptr_0 != IntPtr.Zero) ||
            GClass61.gclass61_0.method_2("GameWindowPos") == null)
            return;
        var strArray1 = GClass61.gclass61_0.method_2("GameWindowPos").Split(',');
        var point_0 = new Point(int.Parse(strArray1[0]), int.Parse(strArray1[1]));
        var strArray2 = GClass61.gclass61_0.method_2("GameWindowSize").Split(',');
        var size_0 = new Size(int.Parse(strArray2[0]), int.Parse(strArray2[1]));
        bool_6 = true;
        if (size_0.Height <= 32 || size_0.Width <= 32)
            return;
        GClass37.smethod_0("Positioning game window: location=" + point_0 + ", size=" + size_0);
        GProcessMemoryManipulator.smethod_43(StartupClass.intptr_0, size_0, point_0);
    }

    private void method_9()
    {
        gclass36_0.method_4();
        Point point_0;
        Size size_0;
        if (StartupClass.bool_41 || (!bool_6 && GClass61.gclass61_0.method_2("GameWindowPos") != null) ||
            !GProcessMemoryManipulator.smethod_39(StartupClass.intptr_0, out point_0) ||
            !GProcessMemoryManipulator.smethod_40(StartupClass.intptr_0, out size_0) || size_0.Width <= 100 || size_0.Height <= 100)
            return;
        GClass61.gclass61_0.method_0("GameWindowPos", point_0.X + "," + point_0.Y);
        GClass61.gclass61_0.method_0("GameWindowSize", size_0.Width + "," + size_0.Height);
    }

    private void method_10()
    {
        var dialogResult = new EvoConfigWindow().ShowDialog(this);
        var str = GClass61.gclass61_0.method_2("AppKey");
        if (dialogResult != DialogResult.OK)
            return;
        this.GInterface0\u002Eimethod_2(GClass30.smethod_1(106));
        GClass61.gclass61_0.method_8();
        method_4();
        if (!(str != GClass61.gclass61_0.method_2("AppKey")) && !StartupClass.gclass54_0.bool_4 && StartupClass.bool_22)
            return;
        if (str != GClass61.gclass61_0.method_2("AppKey"))
            GClass37.smethod_1("- Key changed");
        if (StartupClass.gclass54_0.bool_4)
            GClass37.smethod_1("- Party dirty");
        if (!StartupClass.bool_22)
            GClass37.smethod_1("- Need offsets");
        StartupClass.gclass54_0.bool_4 = false;
        StartupClass.smethod_15();
        StartupClass.smethod_9();
    }

    private void ConfigButton_Click(object sender, EventArgs e)
    {
        if (StartupClass.bool_2)
        {
            method_10();
        }
        else
        {
            var str = GClass61.gclass61_0.method_2("AppKey");
            GClass61.gclass61_0.method_2("PartyProductKey");
            StartupClass.gclass54_0.bool_4 = false;
            if (new ConfigForm(false).ShowDialog() != DialogResult.OK)
                return;
            this.GInterface0\u002Eimethod_2(GClass30.smethod_1(106));
            GClass61.gclass61_0.method_8();
            method_4();
            StartupClass.gclass24_0.method_0();
            GClass55.smethod_31(GClass61.gclass61_0);
            StartupClass.smethod_5();
            StartupClass.gclass54_0.method_0(GClass61.gclass61_0);
            if (str != GClass61.gclass61_0.method_2("AppKey") || StartupClass.gclass54_0.bool_4 ||
                !StartupClass.bool_22)
            {
                if (str != GClass61.gclass61_0.method_2("AppKey"))
                    GClass37.smethod_1("- Key changed");
                if (StartupClass.gclass54_0.bool_4)
                    GClass37.smethod_1("- Party dirty");
                if (!StartupClass.bool_22)
                    GClass37.smethod_1("- Need offsets");
                StartupClass.gclass54_0.bool_4 = false;
                StartupClass.smethod_15();
                StartupClass.smethod_9();
            }

            if (StartupClass.ggameClass_0 != null)
                LabelManaHeader_1.Text = StartupClass.ggameClass_0.PowerLabel + ":";
            method_16();
            method_24();
        }
    }

    private void NewProfileButton_Click(object sender, EventArgs e)
    {
        if ((StartupClass.bool_16 && MessageBox.Show(this, GClass30.smethod_1(656), GClass30.smethod_1(657),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) ||
            new ProfileWizard().method_0(this) != DialogResult.No)
            return;
        var profileProps = new ProfileProps(null);
        if (profileProps.ShowDialog() != DialogResult.OK)
            return;
        StartupClass.gprofile_0 = profileProps.gprofile_0;
        StartupClass.string_5 = GClass30.smethod_1(70);
        method_4();
        method_12(true);
        StartupClass.sortedList_2.Clear();
    }

    private void EditProfileButton_Click(object sender, EventArgs e)
    {
        var num = (int)new ProfileProps(StartupClass.gprofile_0).ShowDialog();
    }

    private void AddWaypointButton_Click(object sender, EventArgs e)
    {
        StartupClass.smethod_23();
    }

    private void LoadProfileButton_Click(object sender, EventArgs e)
    {
        method_11();
    }

    private void method_11()
    {
        if (StartupClass.bool_16 && MessageBox.Show(this, GClass30.smethod_1(660), GClass30.smethod_1(657),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;
        timer_0.Enabled = false;
        var openFileDialog = new OpenFileDialog();
        openFileDialog.RestoreDirectory = true;
        openFileDialog.InitialDirectory = ".\\Profiles";
        openFileDialog.Filter = GClass30.smethod_1(661);
        if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            StartupClass.smethod_1(openFileDialog.FileName);
        timer_0.Enabled = true;
    }

    public void method_12(bool bool_11)
    {
        NewProfileButton.Enabled = bool_11;
        LoadProfileButton.Enabled = bool_11;
        SaveProfileButton.Enabled = bool_11;
        EditProfileButton.Enabled = bool_11;
        QuickLoadButton.Enabled = bool_11;
        label11.Enabled = bool_11;
    }

    private void SaveProfileButton_Click(object sender, EventArgs e)
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.RestoreDirectory = true;
        saveFileDialog.InitialDirectory = ".\\Profiles";
        saveFileDialog.Filter = GClass30.smethod_1(661);
        if (saveFileDialog.ShowDialog() != DialogResult.OK)
            return;
        StartupClass.bool_16 = false;
        StartupClass.gprofile_0.Save(saveFileDialog.FileName);
        StartupClass.string_5 = saveFileDialog.FileName;
        GClass37.smethod_0(GClass30.smethod_2(112, StartupClass.string_5));
        GClass61.gclass61_0.method_0("LastProfile", saveFileDialog.FileName);
        method_4();
    }

    private void GlideButton_Click(object sender, EventArgs e)
    {
        if ((StartupClass.gprofile_0.Factions == null || StartupClass.gprofile_0.Factions.Length == 0) &&
            GClass61.gclass61_0.method_2("RemindFaction") == null && !StartupClass.bool_2 &&
            new FactionReminder().ShowDialog(this) == DialogResult.No)
            return;
        GContext.Main.ResetAutoStop();
        if (GClass61.gclass61_0.method_2("AutoStop") == "True")
            GClass37.smethod_0(GClass30.smethod_2(149,
                DateTime.Now.AddMinutes(int.Parse(GClass61.gclass61_0.method_2("AutoStopMinutes")))
                    .ToShortTimeString()));
        StartupClass.smethod_24(false);
    }

    private void KillButton_Click(object sender, EventArgs e)
    {
        StartupClass.smethod_21(false);
    }

    private void method_13()
    {
        var fileInfo1 = new FileInfo("Glider.log");
        if (!fileInfo1.Exists)
            return;
        var fileInfo2 = new FileInfo("Glider.LastRun.log");
        if (fileInfo2.Exists)
            fileInfo2.Delete();
        fileInfo1.MoveTo("Glider.LastRun.log");
    }

    public void method_14(bool bool_11)
    {
        GlideButton.Enabled = bool_11;
        KillButton.Enabled = bool_11;
        AddWaypointButton.Enabled = bool_11;
        AddFactionButton.Enabled = bool_11;
        StopButton.Enabled = bool_11;
    }

    public void method_15(bool bool_11)
    {
        GlideButton.Enabled = bool_11;
        KillButton.Enabled = bool_11;
        AddWaypointButton.Enabled = bool_11;
        AddFactionButton.Enabled = bool_11;
        ConfigButton.Enabled = bool_11;
        if (!GClass61.gclass61_0.method_5("AltLayout"))
            StopButton.Enabled = !bool_11;
        if (!StartupClass.bool_11 || GClass61.gclass61_0.method_5("AltLayout"))
            return;
        GlideButton.Visible = bool_11;
        KillButton.Visible = bool_11;
        ShrinkButton.Visible = !bool_11;
        HideButton.Visible = !bool_11;
        ShrinkButton.Enabled = !bool_11;
        HideButton.Enabled = !bool_11;
    }

    public void method_16()
    {
        LabelAttached.Text = GClass30.smethod_4("GliderForm.LabelAttached!" + StartupClass.bool_13);
        if (StartupClass.bool_3)
            LabelAttached.Text = "Yes*";
        if (!StartupClass.bool_24)
            StartupClass.gclass36_0 = null;
        if (StartupClass.thread_0 != null)
        {
            method_14(false);
            StopButton.Enabled = false;
            method_12(false);
            ConfigButton.Enabled = false;
            AddFactionButton.Enabled = false;
            FactionLabel.Text = GClass30.smethod_4("GliderForm.FactionLabel!Idle");
        }
        else if (StartupClass.bool_3)
        {
            method_15(StartupClass.glideMode_0 == GlideMode.None);
        }
        else if (!StartupClass.bool_13)
        {
            ConfigButton.Enabled = true;
            method_14(false);
            method_12(true);
            AddFactionButton.Enabled = false;
            FactionLabel.Text = GClass30.smethod_4("GliderForm.FactionLabel!Idle");
        }
        else
        {
            if (GClass61.gclass61_0.method_5("AltLayout"))
            {
                StopButton.Enabled = true;
                if (StartupClass.glideMode_0 == GlideMode.None)
                    StopButton.Text = WaypointsPanel.Visible
                        ? GClass30.smethod_4("GliderForm.StopButton.Default")
                        : GClass30.smethod_4("GliderForm.StopButton.Waypoints");
                else
                    StopButton.Text = GClass30.smethod_4("GliderForm.StopButton.Stop");
            }

            method_15(StartupClass.glideMode_0 == GlideMode.None);
            method_12(StartupClass.glideMode_0 == GlideMode.None);
        }
    }

    public void method_17()
    {
        if (SystemInformation.PrimaryMonitorSize.Width >= 1024)
            return;
        var num = (int)MessageBox.Show(this, GClass30.smethod_1(663), GClass30.smethod_1(657), MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);
        AddWaypointButton.Location = new Point(AddWaypointButton.Location.X - 500, AddWaypointButton.Location.Y);
        foreach (Control control in (ArrangedElementCollection)Controls)
            if (control.GetType() == typeof(Label))
                control.Visible = false;
    }

    public void method_18()
    {
        if (StartupClass.gprofile_0 == null || StartupClass.gprofile_0.Waypoints == null)
            return;
        if (StartupClass.gprofile_0.Waypoints.Count > 2)
        {
            var waypointNotes = StartupClass.gprofile_0.GetWaypointNotes();
            WP_FirstLabel_1.Text = waypointNotes[0];
            WP_ClosestLabel_1.Text = waypointNotes[1];
            WP_NewestLabel_1.Text = waypointNotes[2];
        }

        if (Location_3d.Text != GPlayerSelf.Me.Location.ToString3D())
        {
            Location_3d.Text = GPlayerSelf.Me.Location.ToString3D().Replace(" ", ", ");
            Location_3d.ForeColor = SystemColors.Highlight;
        }

        if (!label11.Checked || StartupClass.glideMode_0 != GlideMode.None)
            return;
        if (glocation_1 == null)
        {
            glocation_1 = GPlayerSelf.Me.Location;
        }
        else
        {
            if (GPlayerSelf.Me.Location.GetDistanceTo(glocation_1) <= StartupClass.double_0)
                return;
            if (WPTypeAuto_1.Checked)
                StartupClass.genum2_0 = GEnum2.const_0;
            if (WPTypeNormal_1.Checked)
                StartupClass.genum2_0 = GEnum2.const_1;
            if (WPTypeGhost_1.Checked)
                StartupClass.genum2_0 = GEnum2.const_2;
            if (WPTypeVendor_1.Checked)
                StartupClass.genum2_0 = GEnum2.const_3;
            StartupClass.smethod_23();
            glocation_1 = GPlayerSelf.Me.Location;
            GClass20.smethod_0("Key.wav");
        }
    }

    private void label11_CheckedChanged(object sender, EventArgs e)
    {
        GClass37.smethod_1("AA1");
        if (label11.Checked)
        {
            GClass37.smethod_1("AA2");
            gliderForm_0.GInterface0\u002Eimethod_2(GClass30.smethod_1(138));
            GClass37.smethod_1("AA3");
            if (StartupClass.bool_13)
            {
                GClass37.smethod_1("AA4");
                glocation_1 = GPlayerSelf.Me.Location;
            }
            else
            {
                GClass37.smethod_1("AA5");
                glocation_1 = null;
            }

            GClass37.smethod_1("AA6");
        }
        else
        {
            GClass37.smethod_1("AA7");
            gliderForm_0.GInterface0\u002Eimethod_2(GClass30.smethod_1(139));
        }

        GClass37.smethod_1("AA8");
    }

    private static void smethod_1(object sender, ThreadExceptionEventArgs e)
    {
        var num = (int)MessageBox.Show(null, "!! " + e.Exception.Message + "\r\n\r\n" + e.Exception.StackTrace,
            "tempexcep");
        Environment.Exit(0);
    }

    private void method_19(object sender, ThreadExceptionEventArgs e)
    {
        GClass37.smethod_0("Exception in Glider: The exception is: " + e.Exception.Message + ", " +
                           e.Exception.StackTrace);
        var num = (int)MessageBox.Show(
            e.Exception.GetType() + "\n\n" + e.Exception.Message + "\n\n" + e.Exception.StackTrace, "Glider Exception",
            MessageBoxButtons.OK, MessageBoxIcon.Hand);
        Environment.Exit(0);
    }

    void Form.OnClosing(CancelEventArgs cancelEventArgs_0)
    {
        GClass37.smethod_0("Kills/Loots/Deaths: " +
                           GClass30.smethod_2(654, StartupClass.int_7, StartupClass.int_8, StartupClass.int_9));
        notifyIcon_0.Dispose();
        notifyIcon_0 = null;
        GClass37.smethod_1("Shutdown: SavePos");
        GClass61.gclass61_0.method_0("WindowPos", Location.X + "," + Location.Y);
        if (!StartupClass.bool_2)
        {
            GClass37.smethod_1("Shutdown: NewDebuffs");
            StartupClass.DebuffsKnown_string.method_10();
        }

        if (StartupClass.gprofile_0 != null && StartupClass.bool_16)
        {
            GClass37.smethod_1("Shutdown: SaveProfile");
            StartupClass.gprofile_0.Save("Profiles\\LastChangedProfile.xml");
        }

        if (StartupClass.gclass79_0 != null)
        {
            GClass37.smethod_1("Shutdown: StopRemote");
            StartupClass.gclass79_0.method_1();
            StartupClass.gclass79_0 = null;
        }

        StartupClass.smethod_31();
        GClass37.smethod_1("Shutdown: KillAction");
        StartupClass.smethod_27(true, "WindowClosing");
        GClass37.smethod_1("Shutdown: Done");
        if (StartupClass.gclass71_0 != null && !StartupClass.bool_33)
            StartupClass.gclass71_0.method_11();
        // ISSUE: explicit non-virtual call
        __nonvirtual(((Form)this).OnClosing(cancelEventArgs_0));
    }

    public void method_20()
    {
        if (!bool_1)
            return;
        bool_1 = false;
        if (StartupClass.gclass24_0.bool_5)
        {
            if (GClass61.gclass61_0.method_5("AltLayout"))
                Text = StartupClass.gclass24_0.string_0 + "_";
            else
                StatusLabel.Text = StartupClass.gclass24_0.string_0 + "_";
        }
        else
        {
            method_4();
        }

        method_16();
        if (StartupClass.ggameClass_0 == null)
            return;
        LabelManaHeader_1.Text = StartupClass.ggameClass_0.PowerLabel + ":";
    }

    public static void smethod_2()
    {
        GClass81.smethod_0();
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "MainWindow.html");
    }

    private void QuickLoadButton_Click(object sender, EventArgs e)
    {
        method_11();
    }

    private void AddFactionButton_Click(object sender, EventArgs e)
    {
        if (!StartupClass.bool_13)
            return;
        var target = GPlayerSelf.Me.Target;
        if (target == null)
            return;
        if (!StartupClass.gprofile_0.CheckFaction(target.FactionID, true))
        {
            StartupClass.bool_16 = true;
            StartupClass.gprofile_0.AddFaction(target.FactionID);
            GClass37.smethod_0(GClass30.smethod_2(850, target.FactionID));
            AddFactionButton.Text = "Del Faction";
        }
        else
        {
            StartupClass.bool_16 = true;
            StartupClass.gprofile_0.RemoveFaction(target.FactionID);
            GClass37.smethod_0(GClass30.smethod_2(851, target.FactionID));
            AddFactionButton.Text = "Add Faction";
        }
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern int GetModuleFileName(
        IntPtr intptr_0,
        StringBuilder stringBuilder_0,
        int int_6);

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr intptr_0);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr intptr_0, int int_6);

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(
        IntPtr intptr_0,
        uint uint_1,
        uint uint_2,
        ref Rectangle rectangle_1);

    private void method_21(object sender, EventArgs e)
    {
        method_22();
    }

    private void method_22()
    {
        if (GClass61.gclass61_0.method_5("AltLayout"))
            return;
        var graphics = tabControl1.CreateGraphics();
        rectangle_0.Width = tabControl1.Right - rectangle_0.Left;
        rectangle_0.Location = new Point(rectangle_0.Left, 0);
        var screen = tabControl1.PointToScreen(rectangle_0.Location);
        var y1 = tabControl1.Location.Y;
        var upperLeftSource = new Point(screen.X, screen.Y - y1);
        int num;
        for (var y2 = 0; y2 < rectangle_0.Height; y2 += num)
        {
            num = y1;
            if (y2 + num > rectangle_0.Height)
                num = rectangle_0.Height - y2;
            var blockRegionSize = new Size(rectangle_0.Width, y1);
            graphics.CopyFromScreen(upperLeftSource, new Point(rectangle_0.X, y2), blockRegionSize);
        }
    }

    void Form.OnPaint(PaintEventArgs paintEventArgs_0)
    {
        // ISSUE: explicit non-virtual call
        __nonvirtual(((Form)this).OnPaint(paintEventArgs_0));
        method_22();
    }

    private void GliderForm_MouseUp(object sender, MouseEventArgs e)
    {
        if (!bool_8 && !bool_9)
            return;
        bool_9 = false;
        bool_8 = false;
        Capture = false;
    }

    private void GliderForm_MouseDown(object sender, MouseEventArgs e)
    {
        bool_8 = true;
        int_4 = e.X;
        int_5 = e.Y;
        Capture = true;
        point_0 = MousePosition;
    }

    private void GliderForm_MouseMove(object sender, MouseEventArgs e)
    {
        if (bool_8 && (Math.Abs(e.X - int_4) > 5.0 || Math.Abs(e.Y - int_5) > 5.0))
        {
            bool_9 = true;
            bool_8 = false;
        }

        if (!bool_9)
            return;
        var mousePosition = MousePosition;
        Location = new Point(Location.X + (mousePosition.X - point_0.X), Location.Y + (mousePosition.Y - point_0.Y));
        point_0 = mousePosition;
        int_4 = e.X;
        int_5 = e.Y;
    }

    private void method_23(object sender, EventArgs e)
    {
        Close();
    }

    private void notifyIcon_0_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        Visible = true;
        Activate();
    }

    private void showWindowToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Visible = true;
        Activate();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void method_24()
    {
        if (GClass61.gclass61_0.method_5("UseTray"))
            notifyIcon_0.Visible = true;
        else
            notifyIcon_0.Visible = false;
    }

    private void method_25()
    {
        if (bool_10 || StartupClass.bool_7)
            return;
        bool_10 = true;
        GClass37.smethod_1("HandleGameGone invoked!");
        Activate();
        Focus();
        if (StartupClass.bool_33)
        {
            Close();
        }
        else
        {
            if (MessageBox.Show(this, GClass30.smethod_1(847), "Glider", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            Close();
        }
    }

    private void method_26()
    {
        try
        {
            method_28();
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("! Exception setting up horizontal layout: " + ex.Message + ex.StackTrace);
        }
    }

    private void method_27(Control control_0)
    {
        control_0.Parent.Controls.Remove(control_0);
    }

    private void method_28()
    {
        panel1 = new Panel();
        MainPanel = new Panel();
        WaypointsPanel = new Panel();
        tabControl1.SuspendLayout();
        SuspendLayout();
        panel1.SuspendLayout();
        MainPanel.SuspendLayout();
        WaypointsPanel.SuspendLayout();
        BackgroundImage = null;
        BackColor = SystemColors.ControlLight;
        method_27(LogBox);
        method_27(label1_1);
        method_27(label2_1);
        method_27(label3_1);
        method_27(LabelManaHeader_1);
        method_27(LabelHealth_1);
        method_27(LabelMana_1);
        method_27(LabelKills_1);
        method_27(SpeedLabel_1);
        method_27(label4_1);
        method_27(label5_1);
        method_27(label6_1);
        method_27(label7_1);
        method_27(THealthLabel_1);
        method_27(TDistanceLabel_1);
        method_27(TFactionLabel_1);
        method_27(XPHour_1);
        method_27(NewProfileButton);
        method_27(EditProfileButton);
        method_27(SaveProfileButton);
        method_27(LoadProfileButton);
        method_27(AddFactionButton);
        method_27(AddWaypointButton);
        method_27(GlideButton);
        method_27(KillButton);
        method_27(ConfigButton);
        method_27(StopButton);
        method_27(label8_1);
        method_27(label9_1);
        method_27(label10_1);
        method_27(WPTypeAuto_1);
        method_27(WPTypeGhost_1);
        method_27(WPTypeNormal_1);
        method_27(WPTypeVendor_1);
        method_27(WP_FirstLabel_1);
        method_27(WP_NewestLabel_1);
        method_27(WP_ClosestLabel_1);
        method_27(label11);
        AutoScaleMode = AutoScaleMode.None;
        ControlBox = true;
        MinimizeBox = false;
        ClientSize = new Size(1066, 80);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        MaximizeBox = false;
        StartPosition = FormStartPosition.Manual;
        LogBox.Font = new Font("Microsoft Sans Serif", 7f);
        LogBox.Location = new Point(8, 8);
        LogBox.Size = new Size(248, 64);
        LogBox.TabIndex = 0;
        label1_1.AutoSize = false;
        label1_1.BackColor = Color.Transparent;
        label1_1.Location = new Point(8, 8);
        label1_1.Name = "label1";
        label1_1.Size = new Size(56, 16);
        label1_1.TabIndex = 1;
        label1_1.Text = "Health:";
        label1_1.TextAlign = ContentAlignment.TopRight;
        LabelHealth_1.AutoSize = false;
        LabelHealth_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelHealth_1.ForeColor = SystemColors.Highlight;
        LabelHealth_1.Location = new Point(72, 8);
        LabelHealth_1.Name = "LabelHealth";
        LabelHealth_1.Size = new Size(112, 16);
        LabelHealth_1.TabIndex = 2;
        LabelHealth_1.Text = "??";
        LabelHealth_1.TextAlign = ContentAlignment.TopLeft;
        LabelMana_1.AutoSize = false;
        LabelMana_1.BackColor = Color.Transparent;
        LabelMana_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelMana_1.ForeColor = SystemColors.Highlight;
        LabelMana_1.Location = new Point(72, 24);
        LabelMana_1.Name = "LabelMana";
        LabelMana_1.Size = new Size(128, 16);
        LabelMana_1.TabIndex = 4;
        LabelMana_1.Text = "??";
        LabelMana_1.TextAlign = ContentAlignment.TopLeft;
        LabelManaHeader_1.AutoSize = false;
        LabelManaHeader_1.BackColor = Color.Transparent;
        LabelManaHeader_1.Location = new Point(8, 24);
        LabelManaHeader_1.Name = "LabelManaHeader";
        LabelManaHeader_1.Size = new Size(56, 16);
        LabelManaHeader_1.TabIndex = 3;
        LabelManaHeader_1.Text = "Mana:";
        LabelManaHeader_1.TextAlign = ContentAlignment.TopRight;
        LabelKills_1.AutoSize = false;
        LabelKills_1.BackColor = Color.Transparent;
        LabelKills_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        LabelKills_1.ForeColor = SystemColors.Highlight;
        LabelKills_1.Location = new Point(72, 40);
        LabelKills_1.Name = "LabelKills";
        LabelKills_1.Size = new Size(112, 16);
        LabelKills_1.TabIndex = 6;
        LabelKills_1.Text = "0 / 0 / 0";
        LabelKills_1.TextAlign = ContentAlignment.TopLeft;
        label3_1.AutoSize = false;
        label3_1.BackColor = Color.Transparent;
        label3_1.Location = new Point(8, 40);
        label3_1.Name = "label3";
        label3_1.Size = new Size(56, 16);
        label3_1.TabIndex = 5;
        label3_1.Text = "Kills:";
        label3_1.TextAlign = ContentAlignment.TopRight;
        label2_1.AutoSize = false;
        label2_1.BackColor = Color.Transparent;
        label2_1.Location = new Point(8, 56);
        label2_1.Name = "label2";
        label2_1.Size = new Size(56, 16);
        label2_1.TabIndex = 22;
        label2_1.Text = "Speed:";
        label2_1.TextAlign = ContentAlignment.TopRight;
        SpeedLabel_1.AutoSize = false;
        SpeedLabel_1.BackColor = Color.Transparent;
        SpeedLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        SpeedLabel_1.ForeColor = SystemColors.Highlight;
        SpeedLabel_1.Location = new Point(72, 56);
        SpeedLabel_1.Name = "SpeedLabel";
        SpeedLabel_1.Size = new Size(112, 16);
        SpeedLabel_1.TabIndex = 23;
        SpeedLabel_1.Text = "??";
        SpeedLabel_1.TextAlign = ContentAlignment.TopLeft;
        label4_1.AutoSize = false;
        label4_1.BackColor = Color.Transparent;
        label4_1.Location = new Point(208, 8);
        label4_1.Name = "label4";
        label4_1.Size = new Size(64, 16);
        label4_1.TabIndex = 24;
        label4_1.Text = "T-Health:";
        label4_1.TextAlign = ContentAlignment.TopRight;
        label5_1.AutoSize = false;
        label5_1.BackColor = Color.Transparent;
        label5_1.Location = new Point(196, 24);
        label5_1.Name = "label5";
        label5_1.Size = new Size(76, 16);
        label5_1.TabIndex = 25;
        label5_1.Text = "T-Distance:";
        label5_1.TextAlign = ContentAlignment.TopRight;
        label6_1.AutoSize = false;
        label6_1.BackColor = Color.Transparent;
        label6_1.Location = new Point(200, 40);
        label6_1.Name = "label6";
        label6_1.Size = new Size(72, 16);
        label6_1.TabIndex = 26;
        label6_1.Text = "T-Faction:";
        label6_1.TextAlign = ContentAlignment.TopRight;
        label7_1.AutoSize = false;
        label7_1.BackColor = Color.Transparent;
        label7_1.Location = new Point(200, 56);
        label7_1.Name = "label7";
        label7_1.Size = new Size(72, 16);
        label7_1.TabIndex = 30;
        label7_1.Text = "XP/Hour:";
        label7_1.TextAlign = ContentAlignment.TopRight;
        THealthLabel_1.AutoSize = false;
        THealthLabel_1.BackColor = Color.Transparent;
        THealthLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f);
        THealthLabel_1.ForeColor = SystemColors.Highlight;
        THealthLabel_1.Location = new Point(280, 8);
        THealthLabel_1.Name = "THealthLabel";
        THealthLabel_1.Size = new Size(48, 16);
        THealthLabel_1.TabIndex = 27;
        THealthLabel_1.TextAlign = ContentAlignment.TopLeft;
        TDistanceLabel_1.AutoSize = false;
        TDistanceLabel_1.BackColor = Color.Transparent;
        TDistanceLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f);
        TDistanceLabel_1.ForeColor = SystemColors.Highlight;
        TDistanceLabel_1.Location = new Point(280, 24);
        TDistanceLabel_1.Name = "TDistanceLabel";
        TDistanceLabel_1.Size = new Size(48, 16);
        TDistanceLabel_1.TabIndex = 28;
        TDistanceLabel_1.TextAlign = ContentAlignment.TopLeft;
        TFactionLabel_1.AutoSize = false;
        TFactionLabel_1.BackColor = Color.Transparent;
        TFactionLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        TFactionLabel_1.ForeColor = SystemColors.Highlight;
        TFactionLabel_1.Location = new Point(280, 40);
        TFactionLabel_1.Name = "TFactionLabel";
        TFactionLabel_1.Size = new Size(48, 16);
        TFactionLabel_1.TabIndex = 29;
        TFactionLabel_1.TextAlign = ContentAlignment.TopLeft;
        XPHour_1.AutoSize = false;
        XPHour_1.BackColor = Color.Transparent;
        XPHour_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        XPHour_1.ForeColor = SystemColors.Highlight;
        XPHour_1.Location = new Point(280, 56);
        XPHour_1.Name = "XPHour";
        XPHour_1.Size = new Size(40, 16);
        XPHour_1.TabIndex = 31;
        XPHour_1.TextAlign = ContentAlignment.TopLeft;
        NewProfileButton.Location = new Point(336, 8);
        NewProfileButton.Size = new Size(80, 23);
        NewProfileButton.Text = "New Profile";
        LoadProfileButton.Location = new Point(424, 8);
        LoadProfileButton.Size = new Size(80, 23);
        LoadProfileButton.Text = "Load Profile";
        SaveProfileButton.Location = new Point(424, 40);
        SaveProfileButton.Size = new Size(80, 23);
        SaveProfileButton.Text = "Save Profile";
        EditProfileButton.Location = new Point(336, 40);
        EditProfileButton.Size = new Size(80, 23);
        EditProfileButton.Text = "Edit Profile";
        AddFactionButton.Location = new Point(520, 8);
        AddFactionButton.Size = new Size(96, 24);
        AddFactionButton.Text = GClass30.smethod_4("GliderForm.ShowNPCInfoButton");
        AddWaypointButton.Location = new Point(520, 40);
        AddWaypointButton.Size = new Size(96, 23);
        AddWaypointButton.Text = "Add Waypoint";
        GlideButton.Location = new Point(904, 8);
        GlideButton.Size = new Size(72, 24);
        GlideButton.Text = "Glide";
        GlideButton.Image = null;
        KillButton.Location = new Point(904, 40);
        KillButton.Size = new Size(72, 24);
        KillButton.Text = "1-Kill";
        KillButton.Image = null;
        ConfigButton.Location = new Point(984, 40);
        ConfigButton.Size = new Size(72, 24);
        ConfigButton.Text = "Configure";
        ConfigButton.Image = null;
        StopButton.Location = new Point(984, 8);
        StopButton.Size = new Size(72, 24);
        StopButton.Text = "Stop";
        StopButton.Image = null;
        WaypointsPanel.BackColor = Color.Transparent;
        WaypointsPanel.Location = new Point(264, 0);
        WaypointsPanel.Name = "WaypointsPanel";
        WaypointsPanel.Size = new Size(624, 80);
        WaypointsPanel.Visible = false;
        label10_1.AutoSize = false;
        label10_1.BackColor = Color.Transparent;
        label10_1.Location = new Point(10, 57);
        label10_1.Name = "label10";
        label10_1.Size = new Size(67, 18);
        label10_1.TabIndex = 4;
        label10_1.Text = "Next:";
        label10_1.TextAlign = ContentAlignment.TopRight;
        label9_1.AutoSize = false;
        label9_1.BackColor = Color.Transparent;
        label9_1.Location = new Point(10, 9);
        label9_1.Name = "label9";
        label9_1.Size = new Size(67, 18);
        label9_1.TabIndex = 3;
        label9_1.Text = "Previous:";
        label9_1.TextAlign = ContentAlignment.TopRight;
        label8_1.AutoSize = false;
        label8_1.BackColor = Color.Transparent;
        label8_1.Location = new Point(10, 33);
        label8_1.Name = "label8";
        label8_1.Size = new Size(67, 18);
        label8_1.TabIndex = 2;
        label8_1.Text = "Closest:";
        label8_1.TextAlign = ContentAlignment.TopRight;
        WP_NewestLabel_1.AutoSize = false;
        WP_NewestLabel_1.BackColor = Color.Transparent;
        WP_NewestLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        WP_NewestLabel_1.ForeColor = SystemColors.Highlight;
        WP_NewestLabel_1.Location = new Point(86, 57);
        WP_NewestLabel_1.Name = "WP_NewestLabel";
        WP_NewestLabel_1.Size = new Size(317, 18);
        WP_NewestLabel_1.TabIndex = 7;
        WP_NewestLabel_1.Text = "??";
        WP_NewestLabel_1.TextAlign = ContentAlignment.TopLeft;
        WP_ClosestLabel_1.AutoSize = false;
        WP_ClosestLabel_1.BackColor = Color.Transparent;
        WP_ClosestLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        WP_ClosestLabel_1.ForeColor = SystemColors.Highlight;
        WP_ClosestLabel_1.Location = new Point(86, 33);
        WP_ClosestLabel_1.Name = "WP_ClosestLabel";
        WP_ClosestLabel_1.Size = new Size(317, 18);
        WP_ClosestLabel_1.TabIndex = 6;
        WP_ClosestLabel_1.Text = "??";
        WP_ClosestLabel_1.TextAlign = ContentAlignment.TopLeft;
        WP_FirstLabel_1.AutoSize = false;
        WP_FirstLabel_1.BackColor = Color.Transparent;
        WP_FirstLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
        WP_FirstLabel_1.ForeColor = SystemColors.Highlight;
        WP_FirstLabel_1.Location = new Point(86, 9);
        WP_FirstLabel_1.Name = "WP_FirstLabel";
        WP_FirstLabel_1.Size = new Size(317, 19);
        WP_FirstLabel_1.TabIndex = 5;
        WP_FirstLabel_1.Text = "??";
        WP_FirstLabel_1.TextAlign = ContentAlignment.TopLeft;
        WPTypeVendor_1.Location = new Point(520, 24);
        WPTypeVendor_1.Name = "WPTypeVendor";
        WPTypeVendor_1.Size = new Size(124, 27);
        WPTypeVendor_1.Text = "Vendor";
        WPTypeGhost_1.Location = new Point(442, 61);
        WPTypeGhost_1.Name = "WPTypeGhost";
        WPTypeGhost_1.Size = new Size(124, 27);
        WPTypeGhost_1.Text = "Ghost";
        WPTypeNormal_1.Location = new Point(442, 42);
        WPTypeNormal_1.Name = "WPTypeNormal";
        WPTypeNormal_1.Size = new Size(124, 28);
        WPTypeNormal_1.Text = "Normal";
        WPTypeAuto_1.Checked = true;
        WPTypeAuto_1.Location = new Point(442, 24);
        WPTypeAuto_1.Name = "WPTypeAuto";
        WPTypeAuto_1.Size = new Size(124, 27);
        WPTypeAuto_1.TabStop = true;
        WPTypeAuto_1.Text = "Automatic";
        label11.Location = new Point(422, 5);
        label11.Name = "label11";
        label11.AutoSize = true;
        label11.Text = "Auto-add waypoints as:";
        MainPanel.Controls.Add(label1_1);
        MainPanel.Controls.Add(label2_1);
        MainPanel.Controls.Add(label3_1);
        MainPanel.Controls.Add(LabelManaHeader_1);
        MainPanel.Controls.Add(LabelHealth_1);
        MainPanel.Controls.Add(LabelMana_1);
        MainPanel.Controls.Add(LabelKills_1);
        MainPanel.Controls.Add(SpeedLabel_1);
        MainPanel.Controls.Add(label4_1);
        MainPanel.Controls.Add(label5_1);
        MainPanel.Controls.Add(label6_1);
        MainPanel.Controls.Add(label7_1);
        MainPanel.Controls.Add(THealthLabel_1);
        MainPanel.Controls.Add(TDistanceLabel_1);
        MainPanel.Controls.Add(TFactionLabel_1);
        MainPanel.Controls.Add(XPHour_1);
        MainPanel.Controls.Add(NewProfileButton);
        MainPanel.Controls.Add(EditProfileButton);
        MainPanel.Controls.Add(SaveProfileButton);
        MainPanel.Controls.Add(LoadProfileButton);
        MainPanel.Controls.Add(AddFactionButton);
        MainPanel.Controls.Add(AddWaypointButton);
        MainPanel.BackColor = Color.Transparent;
        MainPanel.Location = new Point(264, 0);
        MainPanel.Name = "MainPanel";
        MainPanel.Size = new Size(624, 80);
        WaypointsPanel.Controls.Add(label8_1);
        WaypointsPanel.Controls.Add(label9_1);
        WaypointsPanel.Controls.Add(label10_1);
        WaypointsPanel.Controls.Add(WP_FirstLabel_1);
        WaypointsPanel.Controls.Add(WP_ClosestLabel_1);
        WaypointsPanel.Controls.Add(WP_NewestLabel_1);
        WaypointsPanel.Controls.Add(WPTypeAuto_1);
        WaypointsPanel.Controls.Add(WPTypeVendor_1);
        WaypointsPanel.Controls.Add(WPTypeGhost_1);
        WaypointsPanel.Controls.Add(WPTypeNormal_1);
        WaypointsPanel.Controls.Add(label11);
        WaypointsPanel.Controls.Add(Location_3d);
        panel1.Controls.Add(LogBox);
        panel1.Controls.Add(MainPanel);
        panel1.Controls.Add(WaypointsPanel);
        panel1.Controls.Add(GlideButton);
        panel1.Controls.Add(KillButton);
        panel1.Controls.Add(ConfigButton);
        panel1.Controls.Add(StopButton);
        panel1.BackColor = Color.Transparent;
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(1064, 80);
        panel1.TabIndex = 34;
        panel1.MouseUp += GliderForm_MouseUp;
        panel1.MouseMove += GliderForm_MouseMove;
        panel1.MouseDown += GliderForm_MouseDown;
        tabControl1.Visible = false;
        tabControl1.ResumeLayout(false);
        Controls.Add(panel1);
        panel1.ResumeLayout(false);
        MainPanel.ResumeLayout();
        WaypointsPanel.ResumeLayout();
        ResumeLayout(false);
    }

    private void LogBox_DoubleClick(object sender, EventArgs e)
    {
        Process.Start("Glider.log");
    }

    private void alwaysOnTopToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        TopMost = !TopMost;
        alwaysOnTopToolStripMenuItem1.Checked = TopMost;
        GClass61.gclass61_0.method_0("AlwaysOnTop", TopMost.ToString());
    }

    private void minimizeToTrayToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!GClass61.gclass61_0.method_5("UseTray"))
            GClass37.smethod_0(GClass30.smethod_1(845));
        else
            Visible = false;
    }

    private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void ShrinkButton_Click(object sender, EventArgs e)
    {
        if (StartupClass.bool_41)
            StartupClass.smethod_50();
        else
            StartupClass.smethod_48();
    }

    private void HideButton_Click(object sender, EventArgs e)
    {
        if (StartupClass.bool_40)
            StartupClass.smethod_49();
        else
            StartupClass.smethod_47();
    }

    private void WPTypeAuto_1_CheckedChanged(object sender, EventArgs e)
    {
        if (!WPTypeAuto_1.Checked)
            return;
        StartupClass.genum2_0 = GEnum2.const_0;
    }

    private void WPTypeNormal_1_CheckedChanged(object sender, EventArgs e)
    {
        if (!WPTypeNormal_1.Checked)
            return;
        StartupClass.genum2_0 = GEnum2.const_1;
    }

    private void WPTypeGhost_1_CheckedChanged(object sender, EventArgs e)
    {
        if (!WPTypeGhost_1.Checked)
            return;
        StartupClass.genum2_0 = GEnum2.const_2;
    }

    private void WPTypeVendor_1_CheckedChanged(object sender, EventArgs e)
    {
        if (!WPTypeVendor_1.Checked)
            return;
        StartupClass.genum2_0 = GEnum2.const_3;
    }

    private void Location_3d_Click(object sender, EventArgs e)
    {
        Clipboard.SetDataObject(Location_3d.Text, true);
        Location_3d.ForeColor = SystemColors.ActiveCaptionText;
    }

    private bool method_29(Point point_1)
    {
        return SystemInformation.VirtualScreen.Contains(point_1);
    }
}