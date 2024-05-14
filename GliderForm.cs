// Decompiled with JetBrains decompiler
// Type: GliderForm
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

using Glider.Common;
using Glider.Common.Objects;
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
using Ginterface0 = GInterface0;

#nullable disable
public class GliderForm : Form, GInterface0
{
  public const int int_0 = 20000;
  private const uint uint_0 = 4874;
  private const int int_1 = 0;
  private const int int_2 = 5;
  private IContainer icontainer_0;
  private Button AddWaypointButton;
  private HelpProvider helpProvider_0;
  private System.Windows.Forms.Timer timer_0;
  private Panel panel1;
  private Panel MainPanel;
  private Panel WaypointsPanel;
  private bool bool_0;
  private bool bool_1;
  public static GliderForm gliderForm_0;
  public string string_0;
  public bool bool_2 = true;
  public bool bool_3;
  public bool bool_4 = true;
  private GLocation glocation_0;
  private int int_3;
  public string string_1;
  private GroupBox groupBox1;
  private Label label3_1;
  private Label LabelKills_1;
  private Label LabelManaHeader_1;
  public Label LabelMana_1;
  private Label LabelHealth_1;
  private Label label1_1;
  private Label label4_1;
  private Label label5_1;
  private Label label6_1;
  private Label THealthLabel_1;
  private Label TDistanceLabel_1;
  private Label TFactionLabel_1;
  private Label label7_1;
  private Label XPHour_1;
  private Label label2_1;
  private Label SpeedLabel_1;
  private TabControl tabControl1;
  private TabPage tabDefault;
  private TabPage tabProfile;
  private Label LabelAttached;
  private Label label12;
  private GroupBox groupBox2;
  private Button GlideButton;
  private Button KillButton;
  private Button StopButton;
  private Button QuickLoadButton;
  private Button ConfigButton;
  private TextBox LogBox;
  private Label VersionLabel;
  private Button MyHelpButton;
  private GroupBox groupBox3;
  private Button SaveProfileButton;
  private Button LoadProfileButton;
  private Button NewProfileButton;
  private Button EditProfileButton;
  private GroupBox groupBox4;
  private Label FactionLabel;
  private Button AddFactionButton;
  private GroupBox groupBox5;
  private GroupBox groupBox6;
  private Label label10_1;
  private Label label9_1;
  private Label label8_1;
  private RadioButton WPTypeGhost_1;
  private RadioButton WPTypeNormal_1;
  private RadioButton WPTypeAuto_1;
  private CheckBox label11;
  private Label WP_NewestLabel_1;
  private Label WP_ClosestLabel_1;
  private Label WP_FirstLabel_1;
  private TabPage tabPage1;
  private Label StatusLabel;
  private NotifyIcon notifyIcon_0;
  private ContextMenuStrip contextMenuStrip2;
  private ToolStripMenuItem showWindowToolStripMenuItem;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripMenuItem exitToolStripMenuItem;
  private ContextMenuStrip ContextMenuStripWindow;
  private ToolStripMenuItem alwaysOnTopToolStripMenuItem1;
  private ToolStripMenuItem minimizeToTrayToolStripMenuItem;
  private ToolStripMenuItem exitToolStripMenuItem1;
  private ToolStripSeparator toolStripSeparator1;
  private ToolTip toolTip_0;
  private bool bool_5;
  private Button ShrinkButton;
  private Button HideButton;
  private RadioButton WPTypeVendor_1;
  private Label Location_3d;
  private Label locXYZLabel;
  private bool bool_6;
  private GClass36 gclass36_0 = new GClass36(3000);
  protected GLocation glocation_1;
  private Rectangle rectangle_0;
  private bool bool_7;
  private bool bool_8;
  private bool bool_9;
  private int int_4;
  private int int_5;
  private Point point_0;
  private bool bool_10;

  public GliderForm()
  {
    try
    {
      Application.ThreadException += new ThreadExceptionEventHandler(this.method_19);
      this.method_0();
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.GetType().ToString() + "\n\n" + ex.Message + "\n\n" + ex.StackTrace, "Glider Startup Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      Environment.Exit(0);
    }
  }

  private void method_0()
  {
    Application.ThreadException += new ThreadExceptionEventHandler(this.method_19);
    GliderForm.gliderForm_0 = this;
    StartupClass.ginterface0_0 = (GInterface0) this;
    this.string_0 = "";
    this.method_13();
    StartupClass.form_0 = (Form) this;
    StartupClass.InitStartupMode(AppMode.Normal);
    new GClass65().method_0();
    this.method_5();
    if (GClass61.gclass61_0.method_5("AltLayout"))
      this.method_26();
    if (GClass61.gclass61_0.method_5("AlwaysOnTop"))
    {
      this.TopMost = true;
      this.alwaysOnTopToolStripMenuItem1.Checked = true;
    }
    if (!GClass61.gclass61_0.bool_0)
      GProcessMemoryManipulator.smethod_44((Control) this, "Glider.chm", HelpNavigator.Topic, (object) "Welcome.html");
    GClass30.smethod_3((Form) this, nameof (GliderForm));
    GClass24.intptr_0 = this.Handle;
    this.VersionLabel.Text = "Glider 1.8.0 (Release) -- January 21, 2009";
    this.toolTip_0.SetToolTip((Control) this.GlideButton, GClass30.smethod_4("GliderForm.GlideButton!Tooltip"));
    this.toolTip_0.SetToolTip((Control) this.KillButton, GClass30.smethod_4("GliderForm.KillButton!Tooltip"));
    this.toolTip_0.SetToolTip((Control) this.StopButton, GClass30.smethod_4("GliderForm.StopButton!Tooltip"));
    this.toolTip_0.SetToolTip((Control) this.QuickLoadButton, GClass30.smethod_4("GliderForm.QuickLoadButton!Tooltip"));
    this.toolTip_0.SetToolTip((Control) this.ConfigButton, GClass30.smethod_4("GliderForm.ConfigButton!Tooltip"));
    this.toolTip_0.SetToolTip((Control) this.MyHelpButton, GClass30.smethod_4("GliderForm.MyHelpButton!Tooltip"));
    this.toolTip_0.SetToolTip((Control) this.ShrinkButton, GClass30.smethod_4("GliderForm.ShrinkButton!Tooltip"));
    this.toolTip_0.SetToolTip((Control) this.HideButton, GClass30.smethod_4("GliderForm.HideButton!Tooltip"));
    if (GClass61.gclass61_0.method_2("WindowPos") != null && GClass61.gclass61_0.method_2("WindowPos").Length > 0)
    {
      string[] strArray = GClass61.gclass61_0.method_2("WindowPos").Split(',');
      Point point_1 = new Point(int.Parse(strArray[0]), int.Parse(strArray[1]));
      if (this.method_29(point_1))
      {
        this.StartPosition = FormStartPosition.Manual;
        this.Location = point_1;
      }
      else
      {
        GClass61.gclass61_0.method_0("WindowPos", "");
        GClass37.smethod_1("Glider saved window position is not visible, using default position instead");
      }
    }
    if (!this.bool_7)
    {
      GliderForm.SendMessage(this.tabControl1.Handle, 4874U, 2U, ref this.rectangle_0);
      GClass37.smethod_1("Rectangle for 1: " + this.rectangle_0.ToString());
      this.tabControl1.TabPages.Remove(this.tabPage1);
      this.bool_7 = true;
      this.rectangle_0.X += 3;
    }
    this.method_24();
    StartupClass.form_0 = (Form) this;
    this.method_1();
    StartupClass.smethod_59();
  }

  private void method_1()
  {
    bool flag1 = File.Exists("TWfail.txt");
    bool flag2 = File.Exists("TWunsafe.txt");
    bool flag3 = File.Exists("DeadSession.txt");
    if (flag1)
    {
      File.Delete("TWfail.txt");
      GProcessMemoryManipulator.smethod_44((Control) this, "Glider.chm", HelpNavigator.Topic, (object) "TripwireFailed.html");
    }
    else if (flag2)
    {
      File.Delete("TWunsafe.txt");
      GProcessMemoryManipulator.smethod_44((Control) this, "Glider.chm", HelpNavigator.Topic, (object) "TripwireUnsafe.html");
    }
    else
    {
      if (!flag3)
        return;
      File.Delete("DeadSession.txt");
      GProcessMemoryManipulator.smethod_44((Control) this, "Glider.chm", HelpNavigator.Topic, (object) "DeadSession.html");
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
        if (this.string_1 == null)
          this.string_1 = GProcessMemoryManipulator.smethod_0();
        this.StatusLabel.Text = this.string_1 + " " + string_2;
      }
      else
        this.StatusLabel.Text = GClass61.gclass61_0.method_2("TitleBarName") + " " + string_2;
    }
    else
      this.StatusLabel.Text = GClass30.smethod_2(651, (object) "1.8.0", (object) string_2);
  }

  public void method_4()
  {
    if (GClass61.gclass61_0.method_5("AltLayout"))
      this.Text = StartupClass.string_5 == null ? GClass30.smethod_4("GliderForm.StatusLabel!NewProfile") : StartupClass.string_5;
    else if (StartupClass.string_5 == null)
      this.StatusLabel.Text = GClass30.smethod_4("GliderForm.StatusLabel!NewProfile");
    else
      this.StatusLabel.Text = GliderForm.smethod_0(StartupClass.string_5);
  }

  protected override void Dispose(bool disposing)
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
      if (this.icontainer_0 != null)
        this.icontainer_0.Dispose();
    }
    base.Dispose(disposing);
  }

  [STAThread]
  private static void Main()
  {
    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
    if (Environment.CommandLine.ToLower().IndexOf("/invisible") > -1)
      GClass81.smethod_0();
    else
      Application.Run((Form) new GliderForm());
  }

  private void method_5()
  {
    this.icontainer_0 = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (GliderForm));
    this.timer_0 = new System.Windows.Forms.Timer(this.icontainer_0);
    this.AddWaypointButton = new Button();
    this.helpProvider_0 = new HelpProvider();
    this.SaveProfileButton = new Button();
    this.LoadProfileButton = new Button();
    this.NewProfileButton = new Button();
    this.EditProfileButton = new Button();
    this.tabDefault = new TabPage();
    this.VersionLabel = new Label();
    this.MyHelpButton = new Button();
    this.LogBox = new TextBox();
    this.groupBox2 = new GroupBox();
    this.QuickLoadButton = new Button();
    this.ConfigButton = new Button();
    this.StopButton = new Button();
    this.KillButton = new Button();
    this.GlideButton = new Button();
    this.ShrinkButton = new Button();
    this.HideButton = new Button();
    this.groupBox1 = new GroupBox();
    this.StatusLabel = new Label();
    this.LabelAttached = new Label();
    this.label12 = new Label();
    this.label3_1 = new Label();
    this.LabelKills_1 = new Label();
    this.LabelManaHeader_1 = new Label();
    this.LabelMana_1 = new Label();
    this.LabelHealth_1 = new Label();
    this.label1_1 = new Label();
    this.label4_1 = new Label();
    this.label5_1 = new Label();
    this.label6_1 = new Label();
    this.THealthLabel_1 = new Label();
    this.TDistanceLabel_1 = new Label();
    this.TFactionLabel_1 = new Label();
    this.label7_1 = new Label();
    this.XPHour_1 = new Label();
    this.label2_1 = new Label();
    this.SpeedLabel_1 = new Label();
    this.tabProfile = new TabPage();
    this.groupBox6 = new GroupBox();
    this.WP_NewestLabel_1 = new Label();
    this.WP_ClosestLabel_1 = new Label();
    this.WP_FirstLabel_1 = new Label();
    this.label10_1 = new Label();
    this.label9_1 = new Label();
    this.label8_1 = new Label();
    this.groupBox5 = new GroupBox();
    this.WPTypeVendor_1 = new RadioButton();
    this.WPTypeGhost_1 = new RadioButton();
    this.WPTypeNormal_1 = new RadioButton();
    this.WPTypeAuto_1 = new RadioButton();
    this.label11 = new CheckBox();
    this.groupBox4 = new GroupBox();
    this.FactionLabel = new Label();
    this.AddFactionButton = new Button();
    this.groupBox3 = new GroupBox();
    this.tabControl1 = new TabControl();
    this.tabPage1 = new TabPage();
    this.toolTip_0 = new ToolTip(this.icontainer_0);
    this.notifyIcon_0 = new NotifyIcon(this.icontainer_0);
    this.contextMenuStrip2 = new ContextMenuStrip(this.icontainer_0);
    this.showWindowToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this.exitToolStripMenuItem = new ToolStripMenuItem();
    this.ContextMenuStripWindow = new ContextMenuStrip(this.icontainer_0);
    this.alwaysOnTopToolStripMenuItem1 = new ToolStripMenuItem();
    this.minimizeToTrayToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.exitToolStripMenuItem1 = new ToolStripMenuItem();
    this.Location_3d = new Label();
    this.locXYZLabel = new Label();
    this.tabDefault.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.tabProfile.SuspendLayout();
    this.groupBox6.SuspendLayout();
    this.groupBox5.SuspendLayout();
    this.groupBox4.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.tabControl1.SuspendLayout();
    this.contextMenuStrip2.SuspendLayout();
    this.ContextMenuStripWindow.SuspendLayout();
    this.SuspendLayout();
    this.timer_0.Enabled = true;
    this.timer_0.Interval = 350;
    this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
    this.AddWaypointButton.Enabled = false;
    this.helpProvider_0.SetHelpKeyword((Control) this.AddWaypointButton, "MainWindow.html#AddWaypoint");
    this.helpProvider_0.SetHelpNavigator((Control) this.AddWaypointButton, HelpNavigator.Topic);
    this.AddWaypointButton.Location = new Point(227, 21);
    this.AddWaypointButton.Name = "AddWaypointButton";
    this.helpProvider_0.SetShowHelp((Control) this.AddWaypointButton, true);
    this.AddWaypointButton.Size = new Size(119, 27);
    this.AddWaypointButton.TabIndex = 8;
    this.AddWaypointButton.Text = "Add Waypoint";
    this.AddWaypointButton.UseVisualStyleBackColor = false;
    this.AddWaypointButton.Click += new EventHandler(this.AddWaypointButton_Click);
    this.helpProvider_0.HelpNamespace = ".\\Glider.chm";
    this.SaveProfileButton.Enabled = false;
    this.helpProvider_0.SetHelpKeyword((Control) this.SaveProfileButton, "MainWindow.html#Profiles");
    this.helpProvider_0.SetHelpNavigator((Control) this.SaveProfileButton, HelpNavigator.Topic);
    this.SaveProfileButton.Location = new Point(201, 27);
    this.SaveProfileButton.Name = "SaveProfileButton";
    this.helpProvider_0.SetShowHelp((Control) this.SaveProfileButton, true);
    this.SaveProfileButton.Size = new Size(119, 27);
    this.SaveProfileButton.TabIndex = 11;
    this.SaveProfileButton.Text = "Save Profile";
    this.SaveProfileButton.UseVisualStyleBackColor = false;
    this.SaveProfileButton.Click += new EventHandler(this.SaveProfileButton_Click);
    this.helpProvider_0.SetHelpKeyword((Control) this.LoadProfileButton, "MainWindow.html#Profiles");
    this.helpProvider_0.SetHelpNavigator((Control) this.LoadProfileButton, HelpNavigator.Topic);
    this.LoadProfileButton.Location = new Point(201, 60);
    this.LoadProfileButton.Name = "LoadProfileButton";
    this.helpProvider_0.SetShowHelp((Control) this.LoadProfileButton, true);
    this.LoadProfileButton.Size = new Size(119, 27);
    this.LoadProfileButton.TabIndex = 9;
    this.LoadProfileButton.Text = "Load Profile";
    this.LoadProfileButton.UseVisualStyleBackColor = false;
    this.LoadProfileButton.Click += new EventHandler(this.LoadProfileButton_Click);
    this.helpProvider_0.SetHelpKeyword((Control) this.NewProfileButton, "MainWindow.html#Profiles");
    this.helpProvider_0.SetHelpNavigator((Control) this.NewProfileButton, HelpNavigator.Topic);
    this.NewProfileButton.Location = new Point(35, 27);
    this.NewProfileButton.Name = "NewProfileButton";
    this.helpProvider_0.SetShowHelp((Control) this.NewProfileButton, true);
    this.NewProfileButton.Size = new Size(119, 27);
    this.NewProfileButton.TabIndex = 8;
    this.NewProfileButton.Text = "New Profile";
    this.NewProfileButton.UseVisualStyleBackColor = false;
    this.NewProfileButton.Click += new EventHandler(this.NewProfileButton_Click);
    this.EditProfileButton.Enabled = false;
    this.helpProvider_0.SetHelpKeyword((Control) this.EditProfileButton, "MainWindow.html#Profiles");
    this.helpProvider_0.SetHelpNavigator((Control) this.EditProfileButton, HelpNavigator.Topic);
    this.EditProfileButton.Location = new Point(35, 60);
    this.EditProfileButton.Name = "EditProfileButton";
    this.helpProvider_0.SetShowHelp((Control) this.EditProfileButton, true);
    this.EditProfileButton.Size = new Size(119, 27);
    this.EditProfileButton.TabIndex = 10;
    this.EditProfileButton.Text = "Edit Profile";
    this.EditProfileButton.UseVisualStyleBackColor = false;
    this.EditProfileButton.Click += new EventHandler(this.EditProfileButton_Click);
    this.tabDefault.Controls.Add((Control) this.VersionLabel);
    this.tabDefault.Controls.Add((Control) this.MyHelpButton);
    this.tabDefault.Controls.Add((Control) this.LogBox);
    this.tabDefault.Controls.Add((Control) this.groupBox2);
    this.tabDefault.Controls.Add((Control) this.groupBox1);
    this.helpProvider_0.SetHelpNavigator((Control) this.tabDefault, HelpNavigator.TopicId);
    this.helpProvider_0.SetHelpString((Control) this.tabDefault, "MainWindow.html");
    this.tabDefault.Location = new Point(4, 22);
    this.tabDefault.Name = "tabDefault";
    this.tabDefault.Padding = new Padding(3);
    this.helpProvider_0.SetShowHelp((Control) this.tabDefault, true);
    this.tabDefault.Size = new Size(364, 559);
    this.tabDefault.TabIndex = 0;
    this.tabDefault.Text = "Default";
    this.tabDefault.UseVisualStyleBackColor = true;
    this.VersionLabel.ForeColor = SystemColors.Highlight;
    this.VersionLabel.Location = new Point(6, 494);
    this.VersionLabel.Name = "VersionLabel";
    this.VersionLabel.Size = new Size(300, 28);
    this.VersionLabel.TabIndex = 39;
    this.VersionLabel.Text = "(glider version label)";
    this.VersionLabel.TextAlign = ContentAlignment.TopCenter;
    this.MyHelpButton.Image = (Image) componentResourceManager.GetObject("MyHelpButton.Image");
    this.MyHelpButton.Location = new Point(312, 482);
    this.MyHelpButton.Name = "MyHelpButton";
    this.MyHelpButton.Size = new Size(40, 40);
    this.MyHelpButton.TabIndex = 38;
    this.MyHelpButton.UseVisualStyleBackColor = true;
    this.MyHelpButton.Click += new EventHandler(this.MyHelpButton_Click);
    this.LogBox.BackColor = Color.FromArgb(254, 239, 200);
    this.LogBox.Font = new Font("Microsoft Sans Serif", 7f);
    this.LogBox.Location = new Point(3, 286);
    this.LogBox.Multiline = true;
    this.LogBox.Name = "LogBox";
    this.LogBox.ScrollBars = ScrollBars.Vertical;
    this.LogBox.Size = new Size(349, 190);
    this.LogBox.TabIndex = 37;
    this.LogBox.DoubleClick += new EventHandler(this.LogBox_DoubleClick);
    this.groupBox2.Controls.Add((Control) this.QuickLoadButton);
    this.groupBox2.Controls.Add((Control) this.ConfigButton);
    this.groupBox2.Controls.Add((Control) this.StopButton);
    this.groupBox2.Controls.Add((Control) this.KillButton);
    this.groupBox2.Controls.Add((Control) this.GlideButton);
    this.groupBox2.Controls.Add((Control) this.ShrinkButton);
    this.groupBox2.Controls.Add((Control) this.HideButton);
    this.groupBox2.Location = new Point(6, 188);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(349, 92);
    this.groupBox2.TabIndex = 36;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Action";
    this.QuickLoadButton.Image = (Image) componentResourceManager.GetObject("QuickLoadButton.Image");
    this.QuickLoadButton.Location = new Point(221, 32);
    this.QuickLoadButton.Name = "QuickLoadButton";
    this.QuickLoadButton.Size = new Size(40, 40);
    this.QuickLoadButton.TabIndex = 19;
    this.QuickLoadButton.UseVisualStyleBackColor = true;
    this.QuickLoadButton.Click += new EventHandler(this.QuickLoadButton_Click);
    this.ConfigButton.Image = (Image) componentResourceManager.GetObject("ConfigButton.Image");
    this.ConfigButton.Location = new Point(289, 32);
    this.ConfigButton.Name = "ConfigButton";
    this.ConfigButton.Size = new Size(40, 40);
    this.ConfigButton.TabIndex = 18;
    this.ConfigButton.UseVisualStyleBackColor = true;
    this.ConfigButton.Click += new EventHandler(this.ConfigButton_Click);
    this.StopButton.Enabled = false;
    this.StopButton.Image = (Image) componentResourceManager.GetObject("StopButton.Image");
    this.StopButton.Location = new Point(153, 32);
    this.StopButton.Name = "StopButton";
    this.StopButton.Size = new Size(40, 40);
    this.StopButton.TabIndex = 17;
    this.StopButton.UseVisualStyleBackColor = true;
    this.StopButton.Click += new EventHandler(this.StopButton_Click);
    this.KillButton.Enabled = false;
    this.KillButton.Image = (Image) componentResourceManager.GetObject("KillButton.Image");
    this.KillButton.Location = new Point(85, 32);
    this.KillButton.Name = "KillButton";
    this.KillButton.Size = new Size(40, 40);
    this.KillButton.TabIndex = 16;
    this.KillButton.UseVisualStyleBackColor = true;
    this.KillButton.Click += new EventHandler(this.KillButton_Click);
    this.GlideButton.Enabled = false;
    this.GlideButton.Image = (Image) componentResourceManager.GetObject("GlideButton.Image");
    this.GlideButton.Location = new Point(17, 32);
    this.GlideButton.Name = "GlideButton";
    this.GlideButton.Size = new Size(40, 40);
    this.GlideButton.TabIndex = 15;
    this.GlideButton.UseVisualStyleBackColor = true;
    this.GlideButton.Click += new EventHandler(this.GlideButton_Click);
    this.ShrinkButton.Enabled = false;
    this.ShrinkButton.Image = (Image) componentResourceManager.GetObject("ShrinkButton.Image");
    this.ShrinkButton.Location = new Point(17, 32);
    this.ShrinkButton.Name = "ShrinkButton";
    this.ShrinkButton.Size = new Size(40, 40);
    this.ShrinkButton.TabIndex = 20;
    this.ShrinkButton.UseVisualStyleBackColor = true;
    this.ShrinkButton.Visible = false;
    this.ShrinkButton.Click += new EventHandler(this.ShrinkButton_Click);
    this.HideButton.Enabled = false;
    this.HideButton.Image = (Image) componentResourceManager.GetObject("HideButton.Image");
    this.HideButton.Location = new Point(85, 32);
    this.HideButton.Name = "HideButton";
    this.HideButton.Size = new Size(40, 40);
    this.HideButton.TabIndex = 21;
    this.HideButton.UseVisualStyleBackColor = true;
    this.HideButton.Visible = false;
    this.HideButton.Click += new EventHandler(this.HideButton_Click);
    this.groupBox1.Controls.Add((Control) this.StatusLabel);
    this.groupBox1.Controls.Add((Control) this.LabelAttached);
    this.groupBox1.Controls.Add((Control) this.label12);
    this.groupBox1.Controls.Add((Control) this.label3_1);
    this.groupBox1.Controls.Add((Control) this.LabelKills_1);
    this.groupBox1.Controls.Add((Control) this.LabelManaHeader_1);
    this.groupBox1.Controls.Add((Control) this.LabelMana_1);
    this.groupBox1.Controls.Add((Control) this.LabelHealth_1);
    this.groupBox1.Controls.Add((Control) this.label1_1);
    this.groupBox1.Controls.Add((Control) this.label4_1);
    this.groupBox1.Controls.Add((Control) this.label5_1);
    this.groupBox1.Controls.Add((Control) this.label6_1);
    this.groupBox1.Controls.Add((Control) this.THealthLabel_1);
    this.groupBox1.Controls.Add((Control) this.TDistanceLabel_1);
    this.groupBox1.Controls.Add((Control) this.TFactionLabel_1);
    this.groupBox1.Controls.Add((Control) this.label7_1);
    this.groupBox1.Controls.Add((Control) this.XPHour_1);
    this.groupBox1.Controls.Add((Control) this.label2_1);
    this.groupBox1.Controls.Add((Control) this.SpeedLabel_1);
    this.groupBox1.Location = new Point(6, 6);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(349, 176);
    this.groupBox1.TabIndex = 35;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Status";
    this.StatusLabel.BackColor = Color.Transparent;
    this.StatusLabel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.StatusLabel.ForeColor = SystemColors.Highlight;
    this.StatusLabel.Location = new Point(17, 150);
    this.StatusLabel.Name = "StatusLabel";
    this.StatusLabel.Size = new Size(312, 18);
    this.StatusLabel.TabIndex = 50;
    this.StatusLabel.Text = "(status label with profile, etc)";
    this.StatusLabel.TextAlign = ContentAlignment.TopCenter;
    this.LabelAttached.AutoSize = true;
    this.LabelAttached.BackColor = Color.Transparent;
    this.LabelAttached.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.LabelAttached.ForeColor = SystemColors.Highlight;
    this.LabelAttached.Location = new Point(101, 31);
    this.LabelAttached.Name = "LabelAttached";
    this.LabelAttached.Size = new Size(21, 13);
    this.LabelAttached.TabIndex = 49;
    this.LabelAttached.Text = "No";
    this.label12.BackColor = Color.Transparent;
    this.label12.Location = new Point(9, 31);
    this.label12.Name = "label12";
    this.label12.Size = new Size(86, 16);
    this.label12.TabIndex = 48;
    this.label12.Text = "Attached:";
    this.label12.TextAlign = ContentAlignment.TopRight;
    this.label3_1.BackColor = Color.Transparent;
    this.label3_1.Location = new Point(9, 97);
    this.label3_1.Name = "label3";
    this.label3_1.Size = new Size(86, 16);
    this.label3_1.TabIndex = 36;
    this.label3_1.Text = "Kills:";
    this.label3_1.TextAlign = ContentAlignment.TopRight;
    this.LabelKills_1.AutoSize = true;
    this.LabelKills_1.BackColor = Color.Transparent;
    this.LabelKills_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.LabelKills_1.ForeColor = SystemColors.Highlight;
    this.LabelKills_1.Location = new Point(101, 97);
    this.LabelKills_1.Name = "LabelKills";
    this.LabelKills_1.Size = new Size(47, 13);
    this.LabelKills_1.TabIndex = 37;
    this.LabelKills_1.Text = "0 / 0 / 0";
    this.LabelManaHeader_1.BackColor = Color.Transparent;
    this.LabelManaHeader_1.Location = new Point(9, 76);
    this.LabelManaHeader_1.Name = "LabelManaHeader";
    this.LabelManaHeader_1.Size = new Size(86, 16);
    this.LabelManaHeader_1.TabIndex = 34;
    this.LabelManaHeader_1.Text = "Mana:";
    this.LabelManaHeader_1.TextAlign = ContentAlignment.TopRight;
    this.LabelMana_1.AutoSize = true;
    this.LabelMana_1.BackColor = Color.Transparent;
    this.LabelMana_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.LabelMana_1.ForeColor = SystemColors.Highlight;
    this.LabelMana_1.Location = new Point(101, 75);
    this.LabelMana_1.Name = "LabelMana";
    this.LabelMana_1.Size = new Size(19, 13);
    this.LabelMana_1.TabIndex = 35;
    this.LabelMana_1.Text = "??";
    this.LabelHealth_1.AutoSize = true;
    this.LabelHealth_1.BackColor = Color.Transparent;
    this.LabelHealth_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.LabelHealth_1.ForeColor = SystemColors.Highlight;
    this.LabelHealth_1.Location = new Point(101, 53);
    this.LabelHealth_1.Name = "LabelHealth";
    this.LabelHealth_1.Size = new Size(19, 13);
    this.LabelHealth_1.TabIndex = 33;
    this.LabelHealth_1.Text = "??";
    this.label1_1.BackColor = Color.Transparent;
    this.label1_1.Location = new Point(9, 53);
    this.label1_1.Name = "label1";
    this.label1_1.Size = new Size(86, 16);
    this.label1_1.TabIndex = 32;
    this.label1_1.Text = "Health:";
    this.label1_1.TextAlign = ContentAlignment.TopRight;
    this.label4_1.AutoSize = true;
    this.label4_1.BackColor = Color.Transparent;
    this.label4_1.Location = new Point(206, 53);
    this.label4_1.Name = "label4";
    this.label4_1.Size = new Size(51, 13);
    this.label4_1.TabIndex = 40;
    this.label4_1.Text = "T-Health:";
    this.label4_1.TextAlign = ContentAlignment.TopRight;
    this.label5_1.AutoSize = true;
    this.label5_1.BackColor = Color.Transparent;
    this.label5_1.Location = new Point(192, 75);
    this.label5_1.Name = "label5";
    this.label5_1.Size = new Size(62, 13);
    this.label5_1.TabIndex = 41;
    this.label5_1.Text = "T-Distance:";
    this.label5_1.TextAlign = ContentAlignment.TopRight;
    this.label6_1.AutoSize = true;
    this.label6_1.BackColor = Color.Transparent;
    this.label6_1.Location = new Point(201, 97);
    this.label6_1.Name = "label6";
    this.label6_1.Size = new Size(55, 13);
    this.label6_1.TabIndex = 42;
    this.label6_1.Text = "T-Faction:";
    this.label6_1.TextAlign = ContentAlignment.TopRight;
    this.THealthLabel_1.AutoSize = true;
    this.THealthLabel_1.BackColor = Color.Transparent;
    this.THealthLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f);
    this.THealthLabel_1.ForeColor = SystemColors.Highlight;
    this.THealthLabel_1.Location = new Point(279, 53);
    this.THealthLabel_1.Name = "THealthLabel";
    this.THealthLabel_1.Size = new Size(19, 13);
    this.THealthLabel_1.TabIndex = 43;
    this.THealthLabel_1.Text = "??";
    this.TDistanceLabel_1.AutoSize = true;
    this.TDistanceLabel_1.BackColor = Color.Transparent;
    this.TDistanceLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f);
    this.TDistanceLabel_1.ForeColor = SystemColors.Highlight;
    this.TDistanceLabel_1.Location = new Point(279, 75);
    this.TDistanceLabel_1.Name = "TDistanceLabel";
    this.TDistanceLabel_1.Size = new Size(19, 13);
    this.TDistanceLabel_1.TabIndex = 44;
    this.TDistanceLabel_1.Text = "??";
    this.TFactionLabel_1.AutoSize = true;
    this.TFactionLabel_1.BackColor = Color.Transparent;
    this.TFactionLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.TFactionLabel_1.ForeColor = SystemColors.Highlight;
    this.TFactionLabel_1.Location = new Point(279, 97);
    this.TFactionLabel_1.Name = "TFactionLabel";
    this.TFactionLabel_1.Size = new Size(19, 13);
    this.TFactionLabel_1.TabIndex = 45;
    this.TFactionLabel_1.Text = "??";
    this.label7_1.AutoSize = true;
    this.label7_1.BackColor = Color.Transparent;
    this.label7_1.Location = new Point(208, 119);
    this.label7_1.Name = "label7";
    this.label7_1.Size = new Size(52, 13);
    this.label7_1.TabIndex = 46;
    this.label7_1.Text = "XP/Hour:";
    this.label7_1.TextAlign = ContentAlignment.TopRight;
    this.XPHour_1.AutoSize = true;
    this.XPHour_1.BackColor = Color.Transparent;
    this.XPHour_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.XPHour_1.ForeColor = SystemColors.Highlight;
    this.XPHour_1.Location = new Point(279, 119);
    this.XPHour_1.Name = "XPHour";
    this.XPHour_1.Size = new Size(19, 13);
    this.XPHour_1.TabIndex = 47;
    this.XPHour_1.Text = "??";
    this.label2_1.BackColor = Color.Transparent;
    this.label2_1.Location = new Point(9, 120);
    this.label2_1.Name = "label2";
    this.label2_1.Size = new Size(86, 16);
    this.label2_1.TabIndex = 38;
    this.label2_1.Text = "Speed:";
    this.label2_1.TextAlign = ContentAlignment.TopRight;
    this.SpeedLabel_1.AutoSize = true;
    this.SpeedLabel_1.BackColor = Color.Transparent;
    this.SpeedLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.SpeedLabel_1.ForeColor = SystemColors.Highlight;
    this.SpeedLabel_1.Location = new Point(101, 119);
    this.SpeedLabel_1.Name = "SpeedLabel";
    this.SpeedLabel_1.Size = new Size(19, 13);
    this.SpeedLabel_1.TabIndex = 39;
    this.SpeedLabel_1.Text = "??";
    this.tabProfile.Controls.Add((Control) this.groupBox6);
    this.tabProfile.Controls.Add((Control) this.groupBox5);
    this.tabProfile.Controls.Add((Control) this.groupBox4);
    this.tabProfile.Controls.Add((Control) this.groupBox3);
    this.helpProvider_0.SetHelpNavigator((Control) this.tabProfile, HelpNavigator.TopicId);
    this.helpProvider_0.SetHelpString((Control) this.tabProfile, "ProfileView.html");
    this.tabProfile.Location = new Point(4, 22);
    this.tabProfile.Name = "tabProfile";
    this.tabProfile.Padding = new Padding(3);
    this.helpProvider_0.SetShowHelp((Control) this.tabProfile, true);
    this.tabProfile.Size = new Size(364, 559);
    this.tabProfile.TabIndex = 1;
    this.tabProfile.Text = "Profile";
    this.tabProfile.UseVisualStyleBackColor = true;
    this.groupBox6.Controls.Add((Control) this.locXYZLabel);
    this.groupBox6.Controls.Add((Control) this.Location_3d);
    this.groupBox6.Controls.Add((Control) this.WP_NewestLabel_1);
    this.groupBox6.Controls.Add((Control) this.WP_ClosestLabel_1);
    this.groupBox6.Controls.Add((Control) this.WP_FirstLabel_1);
    this.groupBox6.Controls.Add((Control) this.label10_1);
    this.groupBox6.Controls.Add((Control) this.label9_1);
    this.groupBox6.Controls.Add((Control) this.label8_1);
    this.groupBox6.Location = new Point(5, 266);
    this.groupBox6.Name = "groupBox6";
    this.groupBox6.Size = new Size(352, 128);
    this.groupBox6.TabIndex = 3;
    this.groupBox6.TabStop = false;
    this.groupBox6.Text = "Location";
    this.WP_NewestLabel_1.AutoSize = true;
    this.WP_NewestLabel_1.BackColor = Color.Transparent;
    this.WP_NewestLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.WP_NewestLabel_1.ForeColor = SystemColors.Highlight;
    this.WP_NewestLabel_1.Location = new Point(82, 81);
    this.WP_NewestLabel_1.Name = "WP_NewestLabel";
    this.WP_NewestLabel_1.Size = new Size(19, 13);
    this.WP_NewestLabel_1.TabIndex = 10;
    this.WP_NewestLabel_1.Text = "??";
    this.WP_NewestLabel_1.TextAlign = ContentAlignment.MiddleLeft;
    this.WP_ClosestLabel_1.AutoSize = true;
    this.WP_ClosestLabel_1.BackColor = Color.Transparent;
    this.WP_ClosestLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.WP_ClosestLabel_1.ForeColor = SystemColors.Highlight;
    this.WP_ClosestLabel_1.Location = new Point(82, 57);
    this.WP_ClosestLabel_1.Name = "WP_ClosestLabel";
    this.WP_ClosestLabel_1.Size = new Size(19, 13);
    this.WP_ClosestLabel_1.TabIndex = 9;
    this.WP_ClosestLabel_1.Text = "??";
    this.WP_ClosestLabel_1.TextAlign = ContentAlignment.MiddleLeft;
    this.WP_FirstLabel_1.AutoSize = true;
    this.WP_FirstLabel_1.BackColor = Color.Transparent;
    this.WP_FirstLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.WP_FirstLabel_1.ForeColor = SystemColors.Highlight;
    this.WP_FirstLabel_1.Location = new Point(82, 33);
    this.WP_FirstLabel_1.Name = "WP_FirstLabel";
    this.WP_FirstLabel_1.Size = new Size(19, 13);
    this.WP_FirstLabel_1.TabIndex = 8;
    this.WP_FirstLabel_1.Text = "??";
    this.WP_FirstLabel_1.TextAlign = ContentAlignment.MiddleLeft;
    this.label10_1.AutoSize = true;
    this.label10_1.BackColor = Color.Transparent;
    this.label10_1.Location = new Point(41, 81);
    this.label10_1.Name = "label10";
    this.label10_1.Size = new Size(32, 13);
    this.label10_1.TabIndex = 7;
    this.label10_1.Text = "Next:";
    this.label10_1.TextAlign = ContentAlignment.MiddleRight;
    this.label9_1.AutoSize = true;
    this.label9_1.BackColor = Color.Transparent;
    this.label9_1.Location = new Point(22, 33);
    this.label9_1.Name = "label9";
    this.label9_1.Size = new Size(51, 13);
    this.label9_1.TabIndex = 6;
    this.label9_1.Text = "Previous:";
    this.label9_1.TextAlign = ContentAlignment.MiddleRight;
    this.label8_1.AutoSize = true;
    this.label8_1.BackColor = Color.Transparent;
    this.label8_1.Location = new Point(29, 57);
    this.label8_1.Name = "label8";
    this.label8_1.Size = new Size(44, 13);
    this.label8_1.TabIndex = 5;
    this.label8_1.Text = "Closest:";
    this.label8_1.TextAlign = ContentAlignment.MiddleRight;
    this.groupBox5.Controls.Add((Control) this.WPTypeVendor_1);
    this.groupBox5.Controls.Add((Control) this.WPTypeGhost_1);
    this.groupBox5.Controls.Add((Control) this.WPTypeNormal_1);
    this.groupBox5.Controls.Add((Control) this.WPTypeAuto_1);
    this.groupBox5.Controls.Add((Control) this.label11);
    this.groupBox5.Controls.Add((Control) this.AddWaypointButton);
    this.groupBox5.Location = new Point(6, 117);
    this.groupBox5.Name = "groupBox5";
    this.groupBox5.Size = new Size(352, 143);
    this.groupBox5.TabIndex = 2;
    this.groupBox5.TabStop = false;
    this.groupBox5.Text = "Waypoints";
    this.WPTypeVendor_1.AutoSize = true;
    this.WPTypeVendor_1.Location = new Point(186, 97);
    this.WPTypeVendor_1.Name = "WPTypeVendor";
    this.WPTypeVendor_1.Size = new Size(59, 17);
    this.WPTypeVendor_1.TabIndex = 15;
    this.WPTypeVendor_1.Text = "Vendor";
    this.WPTypeVendor_1.CheckedChanged += new EventHandler(this.WPTypeVendor_1_CheckedChanged);
    this.WPTypeGhost_1.AutoSize = true;
    this.WPTypeGhost_1.Location = new Point(186, 70);
    this.WPTypeGhost_1.Name = "WPTypeGhost";
    this.WPTypeGhost_1.Size = new Size(53, 17);
    this.WPTypeGhost_1.TabIndex = 14;
    this.WPTypeGhost_1.Text = "Ghost";
    this.WPTypeGhost_1.CheckedChanged += new EventHandler(this.WPTypeGhost_1_CheckedChanged);
    this.WPTypeNormal_1.AutoSize = true;
    this.WPTypeNormal_1.Location = new Point(56, 97);
    this.WPTypeNormal_1.Name = "WPTypeNormal";
    this.WPTypeNormal_1.Size = new Size(58, 17);
    this.WPTypeNormal_1.TabIndex = 13;
    this.WPTypeNormal_1.Text = "Normal";
    this.WPTypeNormal_1.CheckedChanged += new EventHandler(this.WPTypeNormal_1_CheckedChanged);
    this.WPTypeAuto_1.AutoSize = true;
    this.WPTypeAuto_1.Checked = true;
    this.WPTypeAuto_1.Location = new Point(56, 70);
    this.WPTypeAuto_1.Name = "WPTypeAuto";
    this.WPTypeAuto_1.Size = new Size(72, 17);
    this.WPTypeAuto_1.TabIndex = 12;
    this.WPTypeAuto_1.TabStop = true;
    this.WPTypeAuto_1.Text = "Automatic";
    this.WPTypeAuto_1.CheckedChanged += new EventHandler(this.WPTypeAuto_1_CheckedChanged);
    this.label11.AutoSize = true;
    this.label11.Location = new Point(26, 30);
    this.label11.Name = "AutoAddToggle";
    this.label11.Size = new Size(119, 17);
    this.label11.TabIndex = 9;
    this.label11.Text = "Auto-add waypoints";
    this.label11.CheckedChanged += new EventHandler(this.label11_CheckedChanged);
    this.groupBox4.Controls.Add((Control) this.FactionLabel);
    this.groupBox4.Controls.Add((Control) this.AddFactionButton);
    this.groupBox4.Location = new Point(6, 399);
    this.groupBox4.Name = "groupBox4";
    this.groupBox4.Size = new Size(352, 86);
    this.groupBox4.TabIndex = 1;
    this.groupBox4.TabStop = false;
    this.groupBox4.Text = "Faction";
    this.FactionLabel.ForeColor = SystemColors.Highlight;
    this.FactionLabel.Location = new Point(32, 30);
    this.FactionLabel.Name = "FactionLabel";
    this.FactionLabel.Size = new Size(181, 36);
    this.FactionLabel.TabIndex = 1;
    this.FactionLabel.Text = "(text here about the monster's faction)";
    this.AddFactionButton.Location = new Point(219, 30);
    this.AddFactionButton.Name = "AddFactionButton";
    this.AddFactionButton.Size = new Size(107, 27);
    this.AddFactionButton.TabIndex = 0;
    this.AddFactionButton.Text = "Add Faction";
    this.AddFactionButton.UseVisualStyleBackColor = true;
    this.AddFactionButton.Click += new EventHandler(this.AddFactionButton_Click);
    this.groupBox3.Controls.Add((Control) this.SaveProfileButton);
    this.groupBox3.Controls.Add((Control) this.LoadProfileButton);
    this.groupBox3.Controls.Add((Control) this.NewProfileButton);
    this.groupBox3.Controls.Add((Control) this.EditProfileButton);
    this.groupBox3.Location = new Point(6, 6);
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.Size = new Size(352, 105);
    this.groupBox3.TabIndex = 0;
    this.groupBox3.TabStop = false;
    this.groupBox3.Text = "Load/Save";
    this.tabControl1.Controls.Add((Control) this.tabDefault);
    this.tabControl1.Controls.Add((Control) this.tabProfile);
    this.tabControl1.Controls.Add((Control) this.tabPage1);
    this.tabControl1.Location = new Point(12, 12);
    this.tabControl1.Name = "tabControl1";
    this.tabControl1.SelectedIndex = 0;
    this.tabControl1.Size = new Size(372, 585);
    this.tabControl1.TabIndex = 36;
    this.tabPage1.Location = new Point(4, 22);
    this.tabPage1.Name = "tabPage1";
    this.tabPage1.Size = new Size(364, 559);
    this.tabPage1.TabIndex = 2;
    this.tabPage1.Text = "tabPage1";
    this.tabPage1.UseVisualStyleBackColor = true;
    this.notifyIcon_0.ContextMenuStrip = this.contextMenuStrip2;
    this.notifyIcon_0.Icon = (Icon) componentResourceManager.GetObject("notifyIcon1.Icon");
    this.notifyIcon_0.Text = "Glider";
    this.notifyIcon_0.Visible = true;
    this.notifyIcon_0.MouseDoubleClick += new MouseEventHandler(this.notifyIcon_0_MouseDoubleClick);
    this.contextMenuStrip2.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.showWindowToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this.exitToolStripMenuItem
    });
    this.contextMenuStrip2.Name = "contextMenuStrip2";
    this.contextMenuStrip2.Size = new Size(149, 54);
    this.showWindowToolStripMenuItem.Name = "showWindowToolStripMenuItem";
    this.showWindowToolStripMenuItem.Size = new Size(148, 22);
    this.showWindowToolStripMenuItem.Text = "Show window";
    this.showWindowToolStripMenuItem.Click += new EventHandler(this.showWindowToolStripMenuItem_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    this.toolStripSeparator2.Size = new Size(145, 6);
    this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
    this.exitToolStripMenuItem.Size = new Size(148, 22);
    this.exitToolStripMenuItem.Text = "Exit";
    this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
    this.ContextMenuStripWindow.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.alwaysOnTopToolStripMenuItem1,
      (ToolStripItem) this.minimizeToTrayToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.exitToolStripMenuItem1
    });
    this.ContextMenuStripWindow.Name = "ContextMenuStripWindow";
    this.ContextMenuStripWindow.Size = new Size(161, 76);
    this.alwaysOnTopToolStripMenuItem1.Name = "alwaysOnTopToolStripMenuItem1";
    this.alwaysOnTopToolStripMenuItem1.Size = new Size(160, 22);
    this.alwaysOnTopToolStripMenuItem1.Text = "Always on top";
    this.alwaysOnTopToolStripMenuItem1.Click += new EventHandler(this.alwaysOnTopToolStripMenuItem1_Click);
    this.minimizeToTrayToolStripMenuItem.Name = "minimizeToTrayToolStripMenuItem";
    this.minimizeToTrayToolStripMenuItem.Size = new Size(160, 22);
    this.minimizeToTrayToolStripMenuItem.Text = "Minimize to tray";
    this.minimizeToTrayToolStripMenuItem.Click += new EventHandler(this.minimizeToTrayToolStripMenuItem_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(157, 6);
    this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
    this.exitToolStripMenuItem1.Size = new Size(160, 22);
    this.exitToolStripMenuItem1.Text = "Exit";
    this.exitToolStripMenuItem1.Click += new EventHandler(this.exitToolStripMenuItem1_Click);
    this.Location_3d.AutoSize = true;
    this.Location_3d.ForeColor = SystemColors.Highlight;
    this.Location_3d.Location = new Point(82, 104);
    this.Location_3d.Name = "Location_3d";
    this.Location_3d.Size = new Size(19, 13);
    this.Location_3d.TabIndex = 11;
    this.Location_3d.Text = "??";
    this.Location_3d.TextAlign = ContentAlignment.MiddleLeft;
    this.Location_3d.Click += new EventHandler(this.Location_3d_Click);
    this.locXYZLabel.AutoSize = true;
    this.locXYZLabel.Location = new Point(30, 104);
    this.locXYZLabel.Name = "locXYZLabel";
    this.locXYZLabel.Size = new Size(43, 13);
    this.locXYZLabel.TabIndex = 12;
    this.locXYZLabel.Text = "Coords:";
    this.BackgroundImage = (Image) componentResourceManager.GetObject("$this.BackgroundImage");
    this.ClientSize = new Size(396, 609);
    this.ContextMenuStrip = this.ContextMenuStripWindow;
    this.ControlBox = false;
    this.Controls.Add((Control) this.tabControl1);
    this.FormBorderStyle = FormBorderStyle.Fixed3D;
    this.HelpButton = true;
    this.helpProvider_0.SetHelpKeyword((Control) this, "MainWindow.html");
    this.helpProvider_0.SetHelpNavigator((Control) this, HelpNavigator.Topic);
    this.MaximizeBox = false;
    this.Name = nameof (GliderForm);
    this.helpProvider_0.SetShowHelp((Control) this, true);
    this.ShowInTaskbar = false;
    this.MouseUp += new MouseEventHandler(this.GliderForm_MouseUp);
    this.MouseMove += new MouseEventHandler(this.GliderForm_MouseMove);
    this.MouseDown += new MouseEventHandler(this.GliderForm_MouseDown);
    this.tabDefault.ResumeLayout(false);
    this.tabDefault.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.tabProfile.ResumeLayout(false);
    this.groupBox6.ResumeLayout(false);
    this.groupBox6.PerformLayout();
    this.groupBox5.ResumeLayout(false);
    this.groupBox5.PerformLayout();
    this.groupBox4.ResumeLayout(false);
    this.groupBox3.ResumeLayout(false);
    this.tabControl1.ResumeLayout(false);
    this.contextMenuStrip2.ResumeLayout(false);
    this.ContextMenuStripWindow.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  void GInterface0.imethod_3(string string_2)
  {
    if (!StartupClass.bool_18)
      return;
    ((GInterface0)this).imethod_2("[Debug] " + string_2);
  }

  void GInterface0.imethod_2(string string_2)
  {
    lock (this)
    {
      this.method_6(string_2);
      if (!string_2.StartsWith("[Debug]"))
      {
        GliderForm gliderForm = this;
        gliderForm.string_0 = gliderForm.string_0 + "\r\n" + string_2;
        if (this.string_0.Length > 20000)
          this.string_0 = this.string_0.Substring(20000);
        this.bool_2 = true;
      }
    }
    if (string_2.StartsWith("[Debug]"))
      return;
    StartupClass.smethod_17(2, string_2);
  }

  private void method_6(string string_2)
  {
    DateTime now = DateTime.Now;
    string path = "Glider.log";
    try
    {
      StreamWriter streamWriter = File.AppendText(path);
      streamWriter.WriteLine(now.ToString("HH:mm:ss.ffff ") + string_2);
      streamWriter.Flush();
      streamWriter.Close();
    }
    catch (IOException ex)
    {
      Console.WriteLine(GClass30.smethod_2(90, (object) ex.Message));
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
        this.WaypointsPanel.Visible = !this.WaypointsPanel.Visible;
        this.MainPanel.Visible = !this.MainPanel.Visible;
      }
    }
    else
    {
      StartupClass.bool_28 = false;
      if (StartupClass.glideMode_0 != GlideMode.None)
        StartupClass.smethod_27(false, "StopButtonClicked");
    }
    this.method_16();
  }

  private void timer_0_Tick(object sender, EventArgs e)
  {
    if (this.bool_5)
    {
      this.notifyIcon_0.Visible = false;
      this.Close();
    }
    else
    {
      try
      {
        this.method_7();
        if (this.bool_0)
          return;
        this.bool_0 = true;
        this.method_22();
      }
      catch (Exception ex)
      {
        this.timer_0.Enabled = false;
        GClass37.smethod_0("Timer exception in Glider: The exception is: " + ex.Message + ", " + ex.StackTrace);
        int num = (int) MessageBox.Show(ex.GetType().ToString() + "\n\n" + ex.Message + "\n\n" + ex.StackTrace, GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        StartupClass.smethod_27(false, "TimerExcep");
        Environment.Exit(0);
      }
    }
  }

  private void method_7()
  {
    this.method_20();
    this.method_8();
    if (StartupClass.gclass36_0 != null && StartupClass.gclass36_0.method_3())
    {
      StartupClass.gclass36_0 = (GClass36) null;
      StartupClass.bool_19 = true;
      ((GInterface0)gliderForm_0).imethod_2(GClass30.smethod_1(103));
      StartupClass.smethod_27(false, "Timer1Up");
    }
    if (this.bool_2)
    {
      this.bool_2 = false;
      this.LogBox.Text = this.string_0;
      this.LogBox.Select(this.LogBox.Text.Length, this.LogBox.Text.Length);
      this.LogBox.ScrollToCaret();
      this.LogBox.Refresh();
    }
    if (StartupClass.bool_13 && GPlayerSelf.Me != null)
    {
      if (this.glocation_0 != null && Environment.TickCount - this.int_3 > 1200)
      {
        this.SpeedLabel_1.Text = Math.Round((double) this.glocation_0.GetDistanceTo(GPlayerSelf.Me.Location) / ((double) (Environment.TickCount - this.int_3) / 1000.0), 2).ToString();
        this.int_3 = Environment.TickCount;
        this.glocation_0 = GPlayerSelf.Me.Location;
      }
      else if (this.glocation_0 == null)
      {
        this.int_3 = Environment.TickCount;
        this.glocation_0 = GPlayerSelf.Me.Location;
      }
      this.LabelHealth_1.Text = GClass30.smethod_2(653, (object) GPlayerSelf.Me.HealthPoints.ToString(), (object) (int) (GPlayerSelf.Me.Health * 100.0));
      this.LabelKills_1.Text = GClass30.smethod_2(654, (object) StartupClass.int_7, (object) StartupClass.int_8, (object) StartupClass.int_9);
      this.XPHour_1.Text = StartupClass.smethod_29().ToString();
      if (GPlayerSelf.Me.IsCasting)
        this.LabelKills_1.Text += " *";
      if (StartupClass.ggameClass_0 != null)
        this.LabelMana_1.Text = StartupClass.ggameClass_0.PowerValue;
      GUnit target = GPlayerSelf.Me.Target;
      if (target != null)
      {
        this.AddFactionButton.Enabled = true;
        this.THealthLabel_1.Text = target.Health.ToString() + GClass30.smethod_1(104);
        this.TDistanceLabel_1.Text = Math.Round((double) target.DistanceToSelf, 2).ToString();
        this.TFactionLabel_1.Text = target.FactionID.ToString();
        if (StartupClass.gprofile_0.CheckFaction(target.FactionID, true))
        {
          this.AddFactionButton.Text = "Del Faction";
          this.FactionLabel.Text = GClass30.smethod_4("GliderForm.FactionLabel!AlreadyGot");
        }
        else
        {
          this.AddFactionButton.Text = "Add Faction";
          this.FactionLabel.Text = GClass30.smethod_4("GliderForm.FactionLabel!NotFound");
        }
      }
      else
      {
        this.THealthLabel_1.Text = "";
        this.TDistanceLabel_1.Text = "";
        this.TFactionLabel_1.Text = "";
        this.AddFactionButton.Enabled = false;
        this.FactionLabel.Text = GClass30.smethod_4("GliderForm.FactionLabel!NoTarget");
      }
      if (StartupClass.gclass73_0 != null && StartupClass.gclass73_0.int_8 > 0 && StartupClass.gclass73_0.bool_9)
      {
        this.XPHour_1.Text = Math.Round((double) StartupClass.gclass73_0.int_8 / (DateTime.Now - StartupClass.dateTime_0).TotalSeconds * 3600.0, 0).ToString();
        StartupClass.gclass73_0.bool_9 = false;
      }
      this.method_18();
    }
    StartupClass.smethod_38();
    if (StartupClass.bool_21 && StartupClass.gclass36_1.method_3())
    {
      StartupClass.bool_21 = false;
      ((GInterface0)this).imethod_2(GClass30.smethod_1(105));
      GClass55.smethod_28(GClass30.smethod_1(655));
    }
    if (StartupClass.int_3 == 0 && StartupClass.int_12 != 0 && StartupClass.bool_8)
      this.method_25();
    if (!this.gclass36_0.method_3() || !(StartupClass.intptr_0 != IntPtr.Zero))
      return;
    this.method_9();
  }

  private void method_8()
  {
    if (this.bool_6 || !GClass61.gclass61_0.method_5("ManageGamePos") || !(StartupClass.intptr_0 != IntPtr.Zero) || GClass61.gclass61_0.method_2("GameWindowPos") == null)
      return;
    string[] strArray1 = GClass61.gclass61_0.method_2("GameWindowPos").Split(',');
    Point point_0 = new Point(int.Parse(strArray1[0]), int.Parse(strArray1[1]));
    string[] strArray2 = GClass61.gclass61_0.method_2("GameWindowSize").Split(',');
    Size size_0 = new Size(int.Parse(strArray2[0]), int.Parse(strArray2[1]));
    this.bool_6 = true;
    if (size_0.Height <= 32 || size_0.Width <= 32)
      return;
    GClass37.smethod_0("Positioning game window: location=" + point_0.ToString() + ", size=" + size_0.ToString());
    GProcessMemoryManipulator.smethod_43(StartupClass.intptr_0, size_0, point_0);
  }

  private void method_9()
  {
    this.gclass36_0.method_4();
    Point point_0;
    Size size_0;
    if (StartupClass.bool_41 || !this.bool_6 && GClass61.gclass61_0.method_2("GameWindowPos") != null || !GProcessMemoryManipulator.smethod_39(StartupClass.intptr_0, out point_0) || !GProcessMemoryManipulator.smethod_40(StartupClass.intptr_0, out size_0) || size_0.Width <= 100 || size_0.Height <= 100)
      return;
    GClass61.gclass61_0.method_0("GameWindowPos", point_0.X.ToString() + "," + (object) point_0.Y);
    GClass61.gclass61_0.method_0("GameWindowSize", size_0.Width.ToString() + "," + (object) size_0.Height);
  }

  private void method_10()
  {
    DialogResult dialogResult = new EvoConfigWindow().ShowDialog((IWin32Window) this);
    string str = GClass61.gclass61_0.method_2("AppKey");
    if (dialogResult != DialogResult.OK)
      return;
    ((GInterface0)this).imethod_2(GClass30.smethod_1(106));
    GClass61.gclass61_0.method_8();
    this.method_4();
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
      this.method_10();
    }
    else
    {
      string str = GClass61.gclass61_0.method_2("AppKey");
      GClass61.gclass61_0.method_2("PartyProductKey");
      StartupClass.gclass54_0.bool_4 = false;
      if (new ConfigForm(false).ShowDialog() != DialogResult.OK)
        return;
      ((GInterface0)this).imethod_2(GClass30.smethod_1(106));
      GClass61.gclass61_0.method_8();
      this.method_4();
      StartupClass.gclass24_0.method_0();
      GClass55.smethod_31(GClass61.gclass61_0);
      StartupClass.smethod_5();
      StartupClass.gclass54_0.method_0(GClass61.gclass61_0);
      if (str != GClass61.gclass61_0.method_2("AppKey") || StartupClass.gclass54_0.bool_4 || !StartupClass.bool_22)
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
        this.LabelManaHeader_1.Text = StartupClass.ggameClass_0.PowerLabel + ":";
      this.method_16();
      this.method_24();
    }
  }

  private void NewProfileButton_Click(object sender, EventArgs e)
  {
    if (StartupClass.bool_16 && MessageBox.Show((IWin32Window) this, GClass30.smethod_1(656), GClass30.smethod_1(657), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes || new ProfileWizard().method_0((Form) this) != DialogResult.No)
      return;
    ProfileProps profileProps = new ProfileProps((GProfile) null);
    if (profileProps.ShowDialog() != DialogResult.OK)
      return;
    StartupClass.gprofile_0 = profileProps.gprofile_0;
    StartupClass.string_5 = GClass30.smethod_1(70);
    this.method_4();
    this.method_12(true);
    StartupClass.sortedList_2.Clear();
  }

  private void EditProfileButton_Click(object sender, EventArgs e)
  {
    int num = (int) new ProfileProps(StartupClass.gprofile_0).ShowDialog();
  }

  private void AddWaypointButton_Click(object sender, EventArgs e) => StartupClass.smethod_23();

  private void LoadProfileButton_Click(object sender, EventArgs e) => this.method_11();

  private void method_11()
  {
    if (StartupClass.bool_16 && MessageBox.Show((IWin32Window) this, GClass30.smethod_1(660), GClass30.smethod_1(657), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this.timer_0.Enabled = false;
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.RestoreDirectory = true;
    openFileDialog.InitialDirectory = ".\\Profiles";
    openFileDialog.Filter = GClass30.smethod_1(661);
    if (openFileDialog.ShowDialog((IWin32Window) this) == DialogResult.OK)
      StartupClass.smethod_1(openFileDialog.FileName);
    this.timer_0.Enabled = true;
  }

  public void method_12(bool bool_11)
  {
    this.NewProfileButton.Enabled = bool_11;
    this.LoadProfileButton.Enabled = bool_11;
    this.SaveProfileButton.Enabled = bool_11;
    this.EditProfileButton.Enabled = bool_11;
    this.QuickLoadButton.Enabled = bool_11;
    this.label11.Enabled = bool_11;
  }

  private void SaveProfileButton_Click(object sender, EventArgs e)
  {
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    saveFileDialog.RestoreDirectory = true;
    saveFileDialog.InitialDirectory = ".\\Profiles";
    saveFileDialog.Filter = GClass30.smethod_1(661);
    if (saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    StartupClass.bool_16 = false;
    StartupClass.gprofile_0.Save(saveFileDialog.FileName);
    StartupClass.string_5 = saveFileDialog.FileName;
    GClass37.smethod_0(GClass30.smethod_2(112, (object) StartupClass.string_5));
    GClass61.gclass61_0.method_0("LastProfile", saveFileDialog.FileName);
    this.method_4();
  }

  private void GlideButton_Click(object sender, EventArgs e)
  {
    if ((StartupClass.gprofile_0.Factions == null || StartupClass.gprofile_0.Factions.Length == 0) && GClass61.gclass61_0.method_2("RemindFaction") == null && !StartupClass.bool_2 && new FactionReminder().ShowDialog((IWin32Window) this) == DialogResult.No)
      return;
    GContext.Main.ResetAutoStop();
    if (GClass61.gclass61_0.method_2("AutoStop") == "True")
      GClass37.smethod_0(GClass30.smethod_2(149, (object) DateTime.Now.AddMinutes((double) int.Parse(GClass61.gclass61_0.method_2("AutoStopMinutes"))).ToShortTimeString()));
    StartupClass.smethod_24(false);
  }

  private void KillButton_Click(object sender, EventArgs e) => StartupClass.smethod_21(false);

  private void method_13()
  {
    FileInfo fileInfo1 = new FileInfo("Glider.log");
    if (!fileInfo1.Exists)
      return;
    FileInfo fileInfo2 = new FileInfo("Glider.LastRun.log");
    if (fileInfo2.Exists)
      fileInfo2.Delete();
    fileInfo1.MoveTo("Glider.LastRun.log");
  }

  public void method_14(bool bool_11)
  {
    this.GlideButton.Enabled = bool_11;
    this.KillButton.Enabled = bool_11;
    this.AddWaypointButton.Enabled = bool_11;
    this.AddFactionButton.Enabled = bool_11;
    this.StopButton.Enabled = bool_11;
  }

  public void method_15(bool bool_11)
  {
    this.GlideButton.Enabled = bool_11;
    this.KillButton.Enabled = bool_11;
    this.AddWaypointButton.Enabled = bool_11;
    this.AddFactionButton.Enabled = bool_11;
    this.ConfigButton.Enabled = bool_11;
    if (!GClass61.gclass61_0.method_5("AltLayout"))
      this.StopButton.Enabled = !bool_11;
    if (!StartupClass.bool_11 || GClass61.gclass61_0.method_5("AltLayout"))
      return;
    this.GlideButton.Visible = bool_11;
    this.KillButton.Visible = bool_11;
    this.ShrinkButton.Visible = !bool_11;
    this.HideButton.Visible = !bool_11;
    this.ShrinkButton.Enabled = !bool_11;
    this.HideButton.Enabled = !bool_11;
  }

  public void method_16()
  {
    this.LabelAttached.Text = GClass30.smethod_4("GliderForm.LabelAttached!" + StartupClass.bool_13.ToString());
    if (StartupClass.bool_3)
      this.LabelAttached.Text = "Yes*";
    if (!StartupClass.bool_24)
      StartupClass.gclass36_0 = (GClass36) null;
    if (StartupClass.thread_0 != null)
    {
      this.method_14(false);
      this.StopButton.Enabled = false;
      this.method_12(false);
      this.ConfigButton.Enabled = false;
      this.AddFactionButton.Enabled = false;
      this.FactionLabel.Text = GClass30.smethod_4("GliderForm.FactionLabel!Idle");
    }
    else if (StartupClass.bool_3)
      this.method_15(StartupClass.glideMode_0 == GlideMode.None);
    else if (!StartupClass.bool_13)
    {
      this.ConfigButton.Enabled = true;
      this.method_14(false);
      this.method_12(true);
      this.AddFactionButton.Enabled = false;
      this.FactionLabel.Text = GClass30.smethod_4("GliderForm.FactionLabel!Idle");
    }
    else
    {
      if (GClass61.gclass61_0.method_5("AltLayout"))
      {
        this.StopButton.Enabled = true;
        if (StartupClass.glideMode_0 == GlideMode.None)
          this.StopButton.Text = this.WaypointsPanel.Visible ? GClass30.smethod_4("GliderForm.StopButton.Default") : GClass30.smethod_4("GliderForm.StopButton.Waypoints");
        else
          this.StopButton.Text = GClass30.smethod_4("GliderForm.StopButton.Stop");
      }
      this.method_15(StartupClass.glideMode_0 == GlideMode.None);
      this.method_12(StartupClass.glideMode_0 == GlideMode.None);
    }
  }

  public void method_17()
  {
    if (SystemInformation.PrimaryMonitorSize.Width >= 1024)
      return;
    int num = (int) MessageBox.Show((IWin32Window) this, GClass30.smethod_1(663), GClass30.smethod_1(657), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    this.AddWaypointButton.Location = new Point(this.AddWaypointButton.Location.X - 500, this.AddWaypointButton.Location.Y);
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      if (control.GetType() == typeof (Label))
        control.Visible = false;
    }
  }

  public void method_18()
  {
    if (StartupClass.gprofile_0 == null || StartupClass.gprofile_0.Waypoints == null)
      return;
    if (StartupClass.gprofile_0.Waypoints.Count > 2)
    {
      string[] waypointNotes = StartupClass.gprofile_0.GetWaypointNotes();
      this.WP_FirstLabel_1.Text = waypointNotes[0];
      this.WP_ClosestLabel_1.Text = waypointNotes[1];
      this.WP_NewestLabel_1.Text = waypointNotes[2];
    }
    if (this.Location_3d.Text != GPlayerSelf.Me.Location.ToString3D())
    {
      this.Location_3d.Text = GPlayerSelf.Me.Location.ToString3D().Replace(" ", ", ");
      this.Location_3d.ForeColor = SystemColors.Highlight;
    }
    if (!this.label11.Checked || StartupClass.glideMode_0 != GlideMode.None)
      return;
    if (this.glocation_1 == null)
    {
      this.glocation_1 = GPlayerSelf.Me.Location;
    }
    else
    {
      if ((double) GPlayerSelf.Me.Location.GetDistanceTo(this.glocation_1) <= StartupClass.double_0)
        return;
      if (this.WPTypeAuto_1.Checked)
        StartupClass.genum2_0 = GEnum2.const_0;
      if (this.WPTypeNormal_1.Checked)
        StartupClass.genum2_0 = GEnum2.const_1;
      if (this.WPTypeGhost_1.Checked)
        StartupClass.genum2_0 = GEnum2.const_2;
      if (this.WPTypeVendor_1.Checked)
        StartupClass.genum2_0 = GEnum2.const_3;
      StartupClass.smethod_23();
      this.glocation_1 = GPlayerSelf.Me.Location;
      GClass20.smethod_0("Key.wav");
    }
  }

  private void label11_CheckedChanged(object sender, EventArgs e)
  {
    GClass37.smethod_1("AA1");
    if (this.label11.Checked)
    {
      GClass37.smethod_1("AA2");
      ((GInterface0)gliderForm_0).imethod_2(GClass30.smethod_1(138));
      GClass37.smethod_1("AA3");
      if (StartupClass.bool_13)
      {
        GClass37.smethod_1("AA4");
        this.glocation_1 = GPlayerSelf.Me.Location;
      }
      else
      {
        GClass37.smethod_1("AA5");
        this.glocation_1 = (GLocation) null;
      }
      GClass37.smethod_1("AA6");
    }
    else
    {
      GClass37.smethod_1("AA7");
      ((GInterface0)gliderForm_0).imethod_2(GClass30.smethod_1(139));
    }
    GClass37.smethod_1("AA8");
  }

  private static void smethod_1(object sender, ThreadExceptionEventArgs e)
  {
    int num = (int) MessageBox.Show((IWin32Window) null, "!! " + e.Exception.Message + "\r\n\r\n" + e.Exception.StackTrace, "tempexcep");
    Environment.Exit(0);
  }

  private void method_19(object sender, ThreadExceptionEventArgs e)
  {
    GClass37.smethod_0("Exception in Glider: The exception is: " + e.Exception.Message + ", " + e.Exception.StackTrace);
    int num = (int) MessageBox.Show(e.Exception.GetType().ToString() + "\n\n" + e.Exception.Message + "\n\n" + e.Exception.StackTrace, "Glider Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    Environment.Exit(0);
  }

  protected override void OnClosing(CancelEventArgs cancelEventArgs_0)
  {
    GClass37.smethod_0("Kills/Loots/Deaths: " + GClass30.smethod_2(654, (object) StartupClass.int_7, (object) StartupClass.int_8, (object) StartupClass.int_9));
    this.notifyIcon_0.Dispose();
    this.notifyIcon_0 = (NotifyIcon) null;
    GClass37.smethod_1("Shutdown: SavePos");
    GClass61.gclass61_0.method_0("WindowPos", this.Location.X.ToString() + "," + (object) this.Location.Y);
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
      StartupClass.gclass79_0 = (GClass79) null;
    }
    StartupClass.smethod_31();
    GClass37.smethod_1("Shutdown: KillAction");
    StartupClass.smethod_27(true, "WindowClosing");
    GClass37.smethod_1("Shutdown: Done");
    if (StartupClass.gclass71_0 != null && !StartupClass.bool_33)
    {
      StartupClass.gclass71_0.method_11();
    }
    base.OnClosing(cancelEventArgs_0);
  }

  void GInterface0.imethod_0() => this.bool_1 = true;

  public void method_20()
  {
    if (!this.bool_1)
      return;
    this.bool_1 = false;
    if (StartupClass.gclass24_0.bool_5)
    {
      if (GClass61.gclass61_0.method_5("AltLayout"))
        this.Text = StartupClass.gclass24_0.string_0 + "_";
      else
        this.StatusLabel.Text = StartupClass.gclass24_0.string_0 + "_";
    }
    else
      this.method_4();
    this.method_16();
    if (StartupClass.ggameClass_0 == null)
      return;
    this.LabelManaHeader_1.Text = StartupClass.ggameClass_0.PowerLabel + ":";
  }

  void GInterface0.imethod_1() => this.label11.Checked = !this.label11.Checked;

  public static void smethod_2() => GClass81.smethod_0();

  void GInterface0.imethod_4()
  {
    this.bool_5 = true;
    GClass37.smethod_0("Setting stop flag in Gliderform");
    Thread.Sleep(1200);
  }

  private void MyHelpButton_Click(object sender, EventArgs e)
  {
    GProcessMemoryManipulator.smethod_44((Control) this, "Glider.chm", HelpNavigator.Topic, (object) "MainWindow.html");
  }

  private void QuickLoadButton_Click(object sender, EventArgs e) => this.method_11();

  private void AddFactionButton_Click(object sender, EventArgs e)
  {
    if (!StartupClass.bool_13)
      return;
    GUnit target = GPlayerSelf.Me.Target;
    if (target == null)
      return;
    if (!StartupClass.gprofile_0.CheckFaction(target.FactionID, true))
    {
      StartupClass.bool_16 = true;
      StartupClass.gprofile_0.AddFaction(target.FactionID);
      GClass37.smethod_0(GClass30.smethod_2(850, (object) target.FactionID));
      this.AddFactionButton.Text = "Del Faction";
    }
    else
    {
      StartupClass.bool_16 = true;
      StartupClass.gprofile_0.RemoveFaction(target.FactionID);
      GClass37.smethod_0(GClass30.smethod_2(851, (object) target.FactionID));
      this.AddFactionButton.Text = "Add Faction";
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

  private void method_21(object sender, EventArgs e) => this.method_22();

  private void method_22()
  {
    if (GClass61.gclass61_0.method_5("AltLayout"))
      return;
    Graphics graphics = this.tabControl1.CreateGraphics();
    this.rectangle_0.Width = this.tabControl1.Right - this.rectangle_0.Left;
    this.rectangle_0.Location = new Point(this.rectangle_0.Left, 0);
    Point screen = this.tabControl1.PointToScreen(this.rectangle_0.Location);
    int y1 = this.tabControl1.Location.Y;
    Point upperLeftSource = new Point(screen.X, screen.Y - y1);
    int num;
    for (int y2 = 0; y2 < this.rectangle_0.Height; y2 += num)
    {
      num = y1;
      if (y2 + num > this.rectangle_0.Height)
        num = this.rectangle_0.Height - y2;
      Size blockRegionSize = new Size(this.rectangle_0.Width, y1);
      graphics.CopyFromScreen(upperLeftSource, new Point(this.rectangle_0.X, y2), blockRegionSize);
    }
  }

  protected override void OnPaint(PaintEventArgs paintEventArgs_0)
  {
    base.OnPaint(paintEventArgs_0);
    this.method_22();
  }

  private void GliderForm_MouseUp(object sender, MouseEventArgs e)
  {
    if (!this.bool_8 && !this.bool_9)
      return;
    this.bool_9 = false;
    this.bool_8 = false;
    this.Capture = false;
  }

  private void GliderForm_MouseDown(object sender, MouseEventArgs e)
  {
    this.bool_8 = true;
    this.int_4 = e.X;
    this.int_5 = e.Y;
    this.Capture = true;
    this.point_0 = Control.MousePosition;
  }

  private void GliderForm_MouseMove(object sender, MouseEventArgs e)
  {
    if (this.bool_8 && ((double) Math.Abs(e.X - this.int_4) > 5.0 || (double) Math.Abs(e.Y - this.int_5) > 5.0))
    {
      this.bool_9 = true;
      this.bool_8 = false;
    }
    if (!this.bool_9)
      return;
    Point mousePosition = Control.MousePosition;
    this.Location = new Point(this.Location.X + (mousePosition.X - this.point_0.X), this.Location.Y + (mousePosition.Y - this.point_0.Y));
    this.point_0 = mousePosition;
    this.int_4 = e.X;
    this.int_5 = e.Y;
  }

  private void method_23(object sender, EventArgs e) => this.Close();

  private void notifyIcon_0_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    this.Visible = true;
    this.Activate();
  }

  private void showWindowToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.Visible = true;
    this.Activate();
  }

  private void exitToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

  private void method_24()
  {
    if (GClass61.gclass61_0.method_5("UseTray"))
      this.notifyIcon_0.Visible = true;
    else
      this.notifyIcon_0.Visible = false;
  }

  private void method_25()
  {
    if (this.bool_10 || StartupClass.bool_7)
      return;
    this.bool_10 = true;
    GClass37.smethod_1("HandleGameGone invoked!");
    this.Activate();
    this.Focus();
    if (StartupClass.bool_33)
    {
      this.Close();
    }
    else
    {
      if (MessageBox.Show((IWin32Window) this, GClass30.smethod_1(847), "Glider", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      this.Close();
    }
  }

  private void method_26()
  {
    try
    {
      this.method_28();
    }
    catch (Exception ex)
    {
      GClass37.smethod_0("! Exception setting up horizontal layout: " + ex.Message + ex.StackTrace);
    }
  }

  private void method_27(Control control_0) => control_0.Parent.Controls.Remove(control_0);

  private void method_28()
  {
    this.panel1 = new Panel();
    this.MainPanel = new Panel();
    this.WaypointsPanel = new Panel();
    this.tabControl1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.SuspendLayout();
    this.MainPanel.SuspendLayout();
    this.WaypointsPanel.SuspendLayout();
    this.BackgroundImage = (Image) null;
    this.BackColor = SystemColors.ControlLight;
    this.method_27((Control) this.LogBox);
    this.method_27((Control) this.label1_1);
    this.method_27((Control) this.label2_1);
    this.method_27((Control) this.label3_1);
    this.method_27((Control) this.LabelManaHeader_1);
    this.method_27((Control) this.LabelHealth_1);
    this.method_27((Control) this.LabelMana_1);
    this.method_27((Control) this.LabelKills_1);
    this.method_27((Control) this.SpeedLabel_1);
    this.method_27((Control) this.label4_1);
    this.method_27((Control) this.label5_1);
    this.method_27((Control) this.label6_1);
    this.method_27((Control) this.label7_1);
    this.method_27((Control) this.THealthLabel_1);
    this.method_27((Control) this.TDistanceLabel_1);
    this.method_27((Control) this.TFactionLabel_1);
    this.method_27((Control) this.XPHour_1);
    this.method_27((Control) this.NewProfileButton);
    this.method_27((Control) this.EditProfileButton);
    this.method_27((Control) this.SaveProfileButton);
    this.method_27((Control) this.LoadProfileButton);
    this.method_27((Control) this.AddFactionButton);
    this.method_27((Control) this.AddWaypointButton);
    this.method_27((Control) this.GlideButton);
    this.method_27((Control) this.KillButton);
    this.method_27((Control) this.ConfigButton);
    this.method_27((Control) this.StopButton);
    this.method_27((Control) this.label8_1);
    this.method_27((Control) this.label9_1);
    this.method_27((Control) this.label10_1);
    this.method_27((Control) this.WPTypeAuto_1);
    this.method_27((Control) this.WPTypeGhost_1);
    this.method_27((Control) this.WPTypeNormal_1);
    this.method_27((Control) this.WPTypeVendor_1);
    this.method_27((Control) this.WP_FirstLabel_1);
    this.method_27((Control) this.WP_NewestLabel_1);
    this.method_27((Control) this.WP_ClosestLabel_1);
    this.method_27((Control) this.label11);
    this.AutoScaleMode = AutoScaleMode.None;
    this.ControlBox = true;
    this.MinimizeBox = false;
    this.ClientSize = new Size(1066, 80);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.MaximizeBox = false;
    this.StartPosition = FormStartPosition.Manual;
    this.LogBox.Font = new Font("Microsoft Sans Serif", 7f);
    this.LogBox.Location = new Point(8, 8);
    this.LogBox.Size = new Size(248, 64);
    this.LogBox.TabIndex = 0;
    this.label1_1.AutoSize = false;
    this.label1_1.BackColor = Color.Transparent;
    this.label1_1.Location = new Point(8, 8);
    this.label1_1.Name = "label1";
    this.label1_1.Size = new Size(56, 16);
    this.label1_1.TabIndex = 1;
    this.label1_1.Text = "Health:";
    this.label1_1.TextAlign = ContentAlignment.TopRight;
    this.LabelHealth_1.AutoSize = false;
    this.LabelHealth_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.LabelHealth_1.ForeColor = SystemColors.Highlight;
    this.LabelHealth_1.Location = new Point(72, 8);
    this.LabelHealth_1.Name = "LabelHealth";
    this.LabelHealth_1.Size = new Size(112, 16);
    this.LabelHealth_1.TabIndex = 2;
    this.LabelHealth_1.Text = "??";
    this.LabelHealth_1.TextAlign = ContentAlignment.TopLeft;
    this.LabelMana_1.AutoSize = false;
    this.LabelMana_1.BackColor = Color.Transparent;
    this.LabelMana_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.LabelMana_1.ForeColor = SystemColors.Highlight;
    this.LabelMana_1.Location = new Point(72, 24);
    this.LabelMana_1.Name = "LabelMana";
    this.LabelMana_1.Size = new Size(128, 16);
    this.LabelMana_1.TabIndex = 4;
    this.LabelMana_1.Text = "??";
    this.LabelMana_1.TextAlign = ContentAlignment.TopLeft;
    this.LabelManaHeader_1.AutoSize = false;
    this.LabelManaHeader_1.BackColor = Color.Transparent;
    this.LabelManaHeader_1.Location = new Point(8, 24);
    this.LabelManaHeader_1.Name = "LabelManaHeader";
    this.LabelManaHeader_1.Size = new Size(56, 16);
    this.LabelManaHeader_1.TabIndex = 3;
    this.LabelManaHeader_1.Text = "Mana:";
    this.LabelManaHeader_1.TextAlign = ContentAlignment.TopRight;
    this.LabelKills_1.AutoSize = false;
    this.LabelKills_1.BackColor = Color.Transparent;
    this.LabelKills_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.LabelKills_1.ForeColor = SystemColors.Highlight;
    this.LabelKills_1.Location = new Point(72, 40);
    this.LabelKills_1.Name = "LabelKills";
    this.LabelKills_1.Size = new Size(112, 16);
    this.LabelKills_1.TabIndex = 6;
    this.LabelKills_1.Text = "0 / 0 / 0";
    this.LabelKills_1.TextAlign = ContentAlignment.TopLeft;
    this.label3_1.AutoSize = false;
    this.label3_1.BackColor = Color.Transparent;
    this.label3_1.Location = new Point(8, 40);
    this.label3_1.Name = "label3";
    this.label3_1.Size = new Size(56, 16);
    this.label3_1.TabIndex = 5;
    this.label3_1.Text = "Kills:";
    this.label3_1.TextAlign = ContentAlignment.TopRight;
    this.label2_1.AutoSize = false;
    this.label2_1.BackColor = Color.Transparent;
    this.label2_1.Location = new Point(8, 56);
    this.label2_1.Name = "label2";
    this.label2_1.Size = new Size(56, 16);
    this.label2_1.TabIndex = 22;
    this.label2_1.Text = "Speed:";
    this.label2_1.TextAlign = ContentAlignment.TopRight;
    this.SpeedLabel_1.AutoSize = false;
    this.SpeedLabel_1.BackColor = Color.Transparent;
    this.SpeedLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.SpeedLabel_1.ForeColor = SystemColors.Highlight;
    this.SpeedLabel_1.Location = new Point(72, 56);
    this.SpeedLabel_1.Name = "SpeedLabel";
    this.SpeedLabel_1.Size = new Size(112, 16);
    this.SpeedLabel_1.TabIndex = 23;
    this.SpeedLabel_1.Text = "??";
    this.SpeedLabel_1.TextAlign = ContentAlignment.TopLeft;
    this.label4_1.AutoSize = false;
    this.label4_1.BackColor = Color.Transparent;
    this.label4_1.Location = new Point(208, 8);
    this.label4_1.Name = "label4";
    this.label4_1.Size = new Size(64, 16);
    this.label4_1.TabIndex = 24;
    this.label4_1.Text = "T-Health:";
    this.label4_1.TextAlign = ContentAlignment.TopRight;
    this.label5_1.AutoSize = false;
    this.label5_1.BackColor = Color.Transparent;
    this.label5_1.Location = new Point(196, 24);
    this.label5_1.Name = "label5";
    this.label5_1.Size = new Size(76, 16);
    this.label5_1.TabIndex = 25;
    this.label5_1.Text = "T-Distance:";
    this.label5_1.TextAlign = ContentAlignment.TopRight;
    this.label6_1.AutoSize = false;
    this.label6_1.BackColor = Color.Transparent;
    this.label6_1.Location = new Point(200, 40);
    this.label6_1.Name = "label6";
    this.label6_1.Size = new Size(72, 16);
    this.label6_1.TabIndex = 26;
    this.label6_1.Text = "T-Faction:";
    this.label6_1.TextAlign = ContentAlignment.TopRight;
    this.label7_1.AutoSize = false;
    this.label7_1.BackColor = Color.Transparent;
    this.label7_1.Location = new Point(200, 56);
    this.label7_1.Name = "label7";
    this.label7_1.Size = new Size(72, 16);
    this.label7_1.TabIndex = 30;
    this.label7_1.Text = "XP/Hour:";
    this.label7_1.TextAlign = ContentAlignment.TopRight;
    this.THealthLabel_1.AutoSize = false;
    this.THealthLabel_1.BackColor = Color.Transparent;
    this.THealthLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f);
    this.THealthLabel_1.ForeColor = SystemColors.Highlight;
    this.THealthLabel_1.Location = new Point(280, 8);
    this.THealthLabel_1.Name = "THealthLabel";
    this.THealthLabel_1.Size = new Size(48, 16);
    this.THealthLabel_1.TabIndex = 27;
    this.THealthLabel_1.TextAlign = ContentAlignment.TopLeft;
    this.TDistanceLabel_1.AutoSize = false;
    this.TDistanceLabel_1.BackColor = Color.Transparent;
    this.TDistanceLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f);
    this.TDistanceLabel_1.ForeColor = SystemColors.Highlight;
    this.TDistanceLabel_1.Location = new Point(280, 24);
    this.TDistanceLabel_1.Name = "TDistanceLabel";
    this.TDistanceLabel_1.Size = new Size(48, 16);
    this.TDistanceLabel_1.TabIndex = 28;
    this.TDistanceLabel_1.TextAlign = ContentAlignment.TopLeft;
    this.TFactionLabel_1.AutoSize = false;
    this.TFactionLabel_1.BackColor = Color.Transparent;
    this.TFactionLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.TFactionLabel_1.ForeColor = SystemColors.Highlight;
    this.TFactionLabel_1.Location = new Point(280, 40);
    this.TFactionLabel_1.Name = "TFactionLabel";
    this.TFactionLabel_1.Size = new Size(48, 16);
    this.TFactionLabel_1.TabIndex = 29;
    this.TFactionLabel_1.TextAlign = ContentAlignment.TopLeft;
    this.XPHour_1.AutoSize = false;
    this.XPHour_1.BackColor = Color.Transparent;
    this.XPHour_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.XPHour_1.ForeColor = SystemColors.Highlight;
    this.XPHour_1.Location = new Point(280, 56);
    this.XPHour_1.Name = "XPHour";
    this.XPHour_1.Size = new Size(40, 16);
    this.XPHour_1.TabIndex = 31;
    this.XPHour_1.TextAlign = ContentAlignment.TopLeft;
    this.NewProfileButton.Location = new Point(336, 8);
    this.NewProfileButton.Size = new Size(80, 23);
    this.NewProfileButton.Text = "New Profile";
    this.LoadProfileButton.Location = new Point(424, 8);
    this.LoadProfileButton.Size = new Size(80, 23);
    this.LoadProfileButton.Text = "Load Profile";
    this.SaveProfileButton.Location = new Point(424, 40);
    this.SaveProfileButton.Size = new Size(80, 23);
    this.SaveProfileButton.Text = "Save Profile";
    this.EditProfileButton.Location = new Point(336, 40);
    this.EditProfileButton.Size = new Size(80, 23);
    this.EditProfileButton.Text = "Edit Profile";
    this.AddFactionButton.Location = new Point(520, 8);
    this.AddFactionButton.Size = new Size(96, 24);
    this.AddFactionButton.Text = GClass30.smethod_4("GliderForm.ShowNPCInfoButton");
    this.AddWaypointButton.Location = new Point(520, 40);
    this.AddWaypointButton.Size = new Size(96, 23);
    this.AddWaypointButton.Text = "Add Waypoint";
    this.GlideButton.Location = new Point(904, 8);
    this.GlideButton.Size = new Size(72, 24);
    this.GlideButton.Text = "Glide";
    this.GlideButton.Image = (Image) null;
    this.KillButton.Location = new Point(904, 40);
    this.KillButton.Size = new Size(72, 24);
    this.KillButton.Text = "1-Kill";
    this.KillButton.Image = (Image) null;
    this.ConfigButton.Location = new Point(984, 40);
    this.ConfigButton.Size = new Size(72, 24);
    this.ConfigButton.Text = "Configure";
    this.ConfigButton.Image = (Image) null;
    this.StopButton.Location = new Point(984, 8);
    this.StopButton.Size = new Size(72, 24);
    this.StopButton.Text = "Stop";
    this.StopButton.Image = (Image) null;
    this.WaypointsPanel.BackColor = Color.Transparent;
    this.WaypointsPanel.Location = new Point(264, 0);
    this.WaypointsPanel.Name = "WaypointsPanel";
    this.WaypointsPanel.Size = new Size(624, 80);
    this.WaypointsPanel.Visible = false;
    this.label10_1.AutoSize = false;
    this.label10_1.BackColor = Color.Transparent;
    this.label10_1.Location = new Point(10, 57);
    this.label10_1.Name = "label10";
    this.label10_1.Size = new Size(67, 18);
    this.label10_1.TabIndex = 4;
    this.label10_1.Text = "Next:";
    this.label10_1.TextAlign = ContentAlignment.TopRight;
    this.label9_1.AutoSize = false;
    this.label9_1.BackColor = Color.Transparent;
    this.label9_1.Location = new Point(10, 9);
    this.label9_1.Name = "label9";
    this.label9_1.Size = new Size(67, 18);
    this.label9_1.TabIndex = 3;
    this.label9_1.Text = "Previous:";
    this.label9_1.TextAlign = ContentAlignment.TopRight;
    this.label8_1.AutoSize = false;
    this.label8_1.BackColor = Color.Transparent;
    this.label8_1.Location = new Point(10, 33);
    this.label8_1.Name = "label8";
    this.label8_1.Size = new Size(67, 18);
    this.label8_1.TabIndex = 2;
    this.label8_1.Text = "Closest:";
    this.label8_1.TextAlign = ContentAlignment.TopRight;
    this.WP_NewestLabel_1.AutoSize = false;
    this.WP_NewestLabel_1.BackColor = Color.Transparent;
    this.WP_NewestLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.WP_NewestLabel_1.ForeColor = SystemColors.Highlight;
    this.WP_NewestLabel_1.Location = new Point(86, 57);
    this.WP_NewestLabel_1.Name = "WP_NewestLabel";
    this.WP_NewestLabel_1.Size = new Size(317, 18);
    this.WP_NewestLabel_1.TabIndex = 7;
    this.WP_NewestLabel_1.Text = "??";
    this.WP_NewestLabel_1.TextAlign = ContentAlignment.TopLeft;
    this.WP_ClosestLabel_1.AutoSize = false;
    this.WP_ClosestLabel_1.BackColor = Color.Transparent;
    this.WP_ClosestLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.WP_ClosestLabel_1.ForeColor = SystemColors.Highlight;
    this.WP_ClosestLabel_1.Location = new Point(86, 33);
    this.WP_ClosestLabel_1.Name = "WP_ClosestLabel";
    this.WP_ClosestLabel_1.Size = new Size(317, 18);
    this.WP_ClosestLabel_1.TabIndex = 6;
    this.WP_ClosestLabel_1.Text = "??";
    this.WP_ClosestLabel_1.TextAlign = ContentAlignment.TopLeft;
    this.WP_FirstLabel_1.AutoSize = false;
    this.WP_FirstLabel_1.BackColor = Color.Transparent;
    this.WP_FirstLabel_1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.WP_FirstLabel_1.ForeColor = SystemColors.Highlight;
    this.WP_FirstLabel_1.Location = new Point(86, 9);
    this.WP_FirstLabel_1.Name = "WP_FirstLabel";
    this.WP_FirstLabel_1.Size = new Size(317, 19);
    this.WP_FirstLabel_1.TabIndex = 5;
    this.WP_FirstLabel_1.Text = "??";
    this.WP_FirstLabel_1.TextAlign = ContentAlignment.TopLeft;
    this.WPTypeVendor_1.Location = new Point(520, 24);
    this.WPTypeVendor_1.Name = "WPTypeVendor";
    this.WPTypeVendor_1.Size = new Size(124, 27);
    this.WPTypeVendor_1.Text = "Vendor";
    this.WPTypeGhost_1.Location = new Point(442, 61);
    this.WPTypeGhost_1.Name = "WPTypeGhost";
    this.WPTypeGhost_1.Size = new Size(124, 27);
    this.WPTypeGhost_1.Text = "Ghost";
    this.WPTypeNormal_1.Location = new Point(442, 42);
    this.WPTypeNormal_1.Name = "WPTypeNormal";
    this.WPTypeNormal_1.Size = new Size(124, 28);
    this.WPTypeNormal_1.Text = "Normal";
    this.WPTypeAuto_1.Checked = true;
    this.WPTypeAuto_1.Location = new Point(442, 24);
    this.WPTypeAuto_1.Name = "WPTypeAuto";
    this.WPTypeAuto_1.Size = new Size(124, 27);
    this.WPTypeAuto_1.TabStop = true;
    this.WPTypeAuto_1.Text = "Automatic";
    this.label11.Location = new Point(422, 5);
    this.label11.Name = "label11";
    this.label11.AutoSize = true;
    this.label11.Text = "Auto-add waypoints as:";
    this.MainPanel.Controls.Add((Control) this.label1_1);
    this.MainPanel.Controls.Add((Control) this.label2_1);
    this.MainPanel.Controls.Add((Control) this.label3_1);
    this.MainPanel.Controls.Add((Control) this.LabelManaHeader_1);
    this.MainPanel.Controls.Add((Control) this.LabelHealth_1);
    this.MainPanel.Controls.Add((Control) this.LabelMana_1);
    this.MainPanel.Controls.Add((Control) this.LabelKills_1);
    this.MainPanel.Controls.Add((Control) this.SpeedLabel_1);
    this.MainPanel.Controls.Add((Control) this.label4_1);
    this.MainPanel.Controls.Add((Control) this.label5_1);
    this.MainPanel.Controls.Add((Control) this.label6_1);
    this.MainPanel.Controls.Add((Control) this.label7_1);
    this.MainPanel.Controls.Add((Control) this.THealthLabel_1);
    this.MainPanel.Controls.Add((Control) this.TDistanceLabel_1);
    this.MainPanel.Controls.Add((Control) this.TFactionLabel_1);
    this.MainPanel.Controls.Add((Control) this.XPHour_1);
    this.MainPanel.Controls.Add((Control) this.NewProfileButton);
    this.MainPanel.Controls.Add((Control) this.EditProfileButton);
    this.MainPanel.Controls.Add((Control) this.SaveProfileButton);
    this.MainPanel.Controls.Add((Control) this.LoadProfileButton);
    this.MainPanel.Controls.Add((Control) this.AddFactionButton);
    this.MainPanel.Controls.Add((Control) this.AddWaypointButton);
    this.MainPanel.BackColor = Color.Transparent;
    this.MainPanel.Location = new Point(264, 0);
    this.MainPanel.Name = "MainPanel";
    this.MainPanel.Size = new Size(624, 80);
    this.WaypointsPanel.Controls.Add((Control) this.label8_1);
    this.WaypointsPanel.Controls.Add((Control) this.label9_1);
    this.WaypointsPanel.Controls.Add((Control) this.label10_1);
    this.WaypointsPanel.Controls.Add((Control) this.WP_FirstLabel_1);
    this.WaypointsPanel.Controls.Add((Control) this.WP_ClosestLabel_1);
    this.WaypointsPanel.Controls.Add((Control) this.WP_NewestLabel_1);
    this.WaypointsPanel.Controls.Add((Control) this.WPTypeAuto_1);
    this.WaypointsPanel.Controls.Add((Control) this.WPTypeVendor_1);
    this.WaypointsPanel.Controls.Add((Control) this.WPTypeGhost_1);
    this.WaypointsPanel.Controls.Add((Control) this.WPTypeNormal_1);
    this.WaypointsPanel.Controls.Add((Control) this.label11);
    this.WaypointsPanel.Controls.Add((Control) this.Location_3d);
    this.panel1.Controls.Add((Control) this.LogBox);
    this.panel1.Controls.Add((Control) this.MainPanel);
    this.panel1.Controls.Add((Control) this.WaypointsPanel);
    this.panel1.Controls.Add((Control) this.GlideButton);
    this.panel1.Controls.Add((Control) this.KillButton);
    this.panel1.Controls.Add((Control) this.ConfigButton);
    this.panel1.Controls.Add((Control) this.StopButton);
    this.panel1.BackColor = Color.Transparent;
    this.panel1.Location = new Point(0, 0);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(1064, 80);
    this.panel1.TabIndex = 34;
    this.panel1.MouseUp += new MouseEventHandler(this.GliderForm_MouseUp);
    this.panel1.MouseMove += new MouseEventHandler(this.GliderForm_MouseMove);
    this.panel1.MouseDown += new MouseEventHandler(this.GliderForm_MouseDown);
    this.tabControl1.Visible = false;
    this.tabControl1.ResumeLayout(false);
    this.Controls.Add((Control) this.panel1);
    this.panel1.ResumeLayout(false);
    this.MainPanel.ResumeLayout();
    this.WaypointsPanel.ResumeLayout();
    this.ResumeLayout(false);
  }

  private void LogBox_DoubleClick(object sender, EventArgs e) => Process.Start("Glider.log");

  private void alwaysOnTopToolStripMenuItem1_Click(object sender, EventArgs e)
  {
    this.TopMost = !this.TopMost;
    this.alwaysOnTopToolStripMenuItem1.Checked = this.TopMost;
    GClass61.gclass61_0.method_0("AlwaysOnTop", this.TopMost.ToString());
  }

  private void minimizeToTrayToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (!GClass61.gclass61_0.method_5("UseTray"))
      GClass37.smethod_0(GClass30.smethod_1(845));
    else
      this.Visible = false;
  }

  private void exitToolStripMenuItem1_Click(object sender, EventArgs e) => this.Close();

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
    if (!this.WPTypeAuto_1.Checked)
      return;
    StartupClass.genum2_0 = GEnum2.const_0;
  }

  private void WPTypeNormal_1_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.WPTypeNormal_1.Checked)
      return;
    StartupClass.genum2_0 = GEnum2.const_1;
  }

  private void WPTypeGhost_1_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.WPTypeGhost_1.Checked)
      return;
    StartupClass.genum2_0 = GEnum2.const_2;
  }

  private void WPTypeVendor_1_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.WPTypeVendor_1.Checked)
      return;
    StartupClass.genum2_0 = GEnum2.const_3;
  }

  private void Location_3d_Click(object sender, EventArgs e)
  {
    Clipboard.SetDataObject((object) this.Location_3d.Text.ToString(), true);
    this.Location_3d.ForeColor = SystemColors.ActiveCaptionText;
  }

  private bool method_29(Point point_1) => SystemInformation.VirtualScreen.Contains(point_1);
}
