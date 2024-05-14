// Decompiled with JetBrains decompiler
// Type: SecCheck
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Glider.Common.Objects;

public class SecCheck : Form
{
    private const string string_0 = "Profiles\\";
    private ColumnHeader columnHeader_0;
    private ColumnHeader columnHeader_1;
    private Container container_0;
    private Button DoneButton;
    private int int_0;
    private Label label1;
    private ListView TheList;

    public SecCheck()
    {
        InitializeComponent();
        Text = GProcessMemoryManipulator.smethod_0();
        TheList.DoubleClick += TheList_DoubleClick;
        method_0();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            StartupClass.IsInitialized = false;
            if (container_0 != null)
            {
                container_0.Dispose();
            }
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        var resourceManager = new ResourceManager(typeof(SecCheck));
        DoneButton = new Button();
        label1 = new Label();
        TheList = new ListView();
        columnHeader_0 = new ColumnHeader();
        columnHeader_1 = new ColumnHeader();
        SuspendLayout();
        DoneButton.Location = new Point(216, 280);
        DoneButton.Name = "DoneButton";
        DoneButton.Size = new Size(104, 32);
        DoneButton.TabIndex = 0;
        DoneButton.Text = "Done";
        DoneButton.Click += DoneButton_Click;
        label1.Location = new Point(8, 8);
        label1.Name = "label1";
        label1.Size = new Size(528, 64);
        label1.TabIndex = 2;
        label1.Text =
            "SecCheck is used to check your configuration for common setup problems that increase the risk of detection.  For more detail on an entry, double-click it in the list below to open a browser window.";
        TheList.Columns.AddRange(new ColumnHeader[2]
        {
            columnHeader_0,
            columnHeader_1
        });
        TheList.FullRowSelect = true;
        TheList.GridLines = true;
        TheList.Location = new Point(8, 72);
        TheList.MultiSelect = false;
        TheList.Name = "TheList";
        TheList.Size = new Size(512, 192);
        TheList.TabIndex = 3;
        TheList.View = View.Details;
        columnHeader_0.Text = "Risk";
        columnHeader_1.Text = "Description";
        columnHeader_1.Width = 448;
        AcceptButton = DoneButton;
        AutoScaleBaseSize = new Size(6, 15);
        ClientSize = new Size(534, 317);
        Controls.Add(TheList);
        Controls.Add(label1);
        Controls.Add(DoneButton);
        FormBorderStyle = FormBorderStyle.Fixed3D;
        Icon = (Icon)resourceManager.GetObject("$this.Icon");
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(SecCheck);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Launchpad SecCheck";
        ResumeLayout(false);
    }

    private void DoneButton_Click(object sender, EventArgs e)
    {
        Close();
    }

    public void method_0()
    {
        var flag = false;
        TheList.Items.Clear();
        if (!GClass61.gclass61_0.method_5("AllowWW"))
            method_1("High", "Tripwire is not enabled", "Tripwire");
        if (!GClass61.gclass61_0.method_5("ChatEnabled"))
        {
            method_1("High", "Chat processing is disabled", "ChatEnable");
        }
        else
        {
            if (!GClass61.gclass61_0.method_5("ChatWhisper"))
                method_1("High", "Alert on whisper is disabled", "AlertWhisper");
            if (!GClass61.gclass61_0.method_5("PlaySay"))
                flag = true;
        }

        if (GClass61.gclass61_0.method_5("ListenEnabled") && GClass61.gclass61_0.method_2("ListenPassword").Length == 0)
            method_1("High", "Remote is enabled with no password", "RemotePass");
        if (GClass61.gclass61_0.method_4("FriendAlert") == 0.0 || GClass61.gclass61_0.method_4("FriendAlert") > 3.0)
            method_1("High", "Follower detection is off/slow", "FollowerDetect");
        if (GClass61.gclass61_0.method_3("FriendLogout") == 0 || GClass61.gclass61_0.method_3("FriendLogout") > 5)
            method_1("High", "Follower logout is off/slow", "FollowerLogout");
        if (!GClass61.gclass61_0.method_5("AllowNetCheck"))
            method_1("Medium", "NetCheck is not enabled", "NetCheck");
        if (!GClass61.gclass61_0.method_5("EscapeClear"))
            method_1("Medium", "Clear target with Escape is disabled", "EscapeClear");
        if (flag)
            method_1("Medium", "Alert on say is disabled", "AlertSay");
        if (!GClass61.gclass61_0.method_5("StrafeObstacles"))
            method_1("Medium", "Strafe obstacles is disabled", "StrafeObstacles");
        if (GClass61.gclass61_0.method_5("ListenEnabled") && GClass61.gclass61_0.method_3("ListenPort") == 3200)
            method_1("Medium", "Remote is enabled on default port", "RemotePort");
        if (!GClass61.gclass61_0.method_5("WalkLoot"))
            method_1("Medium", "Walk to loot is disabled", "WalkToLoot");
        if (!GClass61.gclass61_0.method_5("AutoStop"))
            method_1("Medium", "Auto-stop after time is disabled", "AutoStopTime");
        if (GClass61.gclass61_0.method_5("Resurrect") && GClass61.gclass61_0.method_3("MaxResurrect") > 5)
            method_1("Medium", "Excessive automatic resurrections", "ManyRes");
        if (!GClass61.gclass61_0.method_5("StopWhenFull"))
            method_1("Low", "Auto-stop when full is disabled", "AutoStopInv");
        method_2(1, 10, "Profiles", "High");
        method_2(11, 20, "Profiles", "Medium");
        if (TheList.Items.Count != 0)
            return;
        method_1("--", "All checks passed!", "None");
    }

    private void method_1(string string_1, string string_2, string string_3)
    {
        TheList.Items.Add(new ListViewItem(new string[3]
        {
            string_1,
            string_2,
            string_3
        }));
    }

    private void TheList_DoubleClick(object sender, EventArgs e)
    {
        var text = TheList.SelectedItems[0].SubItems[2].Text;
        if (!(text != ""))
            return;
        Process.Start("http://www.mmoglider.com/SecCheck.aspx?SecCheck=" + text);
    }

    private void method_2(int int_1, int int_2, string string_1, string string_2)
    {
        try
        {
            GClass37.smethod_1("Check profiles: " + string_1);
            foreach (var file in Directory.GetFiles(string_1, "*.xml"))
                method_3(int_1, int_2, file, string_2);
            foreach (var directory in Directory.GetDirectories(string_1))
                method_2(int_1, int_2, directory, string_2);
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("** Exception checking profiles: " + ex.Message);
        }
    }

    private void method_3(int int_1, int int_2, string string_1, string string_2)
    {
        try
        {
            var gprofile = new GProfile();
            gprofile.Load(string_1);
            if (gprofile.Waypoints.Count < int_1 || gprofile.Waypoints.Count > int_2 || gprofile.Fishing)
                return;
            if (int_0 == 0)
                method_1("", "-- Profiles --", "");
            ++int_0;
            method_1(string_2, "Short profile: " + method_4(string_1), "ShortProfile");
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("** Exception checking " + string_1 + " -> " + ex.Message + ex.StackTrace);
        }
    }

    private string method_4(string string_1)
    {
        var num = string_1.ToLower().IndexOf("Profiles\\");
        return num == -1 ? string_1 : string_1.Substring(num + "Profiles\\".Length);
    }
}