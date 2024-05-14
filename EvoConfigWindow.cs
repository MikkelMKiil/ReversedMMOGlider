// Decompiled with JetBrains decompiler
// Type: EvoConfigWindow
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

public class EvoConfigWindow : Form
{
    private TextBox AppKey;
    private TextBox FileHelper;
    private TextBox FileSeeds;
    private TextBox FileStructures;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private IContainer icontainer_0;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private LinkLabel linkLabel1;
    private Button MyCancelButton;
    private Button MyHelpButton;
    private Button MyOKButton;
    private TabControl tabControl1;
    private TabPage TabGeneral;
    private TabPage TabMemory;
    private Label VersionLabel;

    public EvoConfigWindow()
    {
        InitializeComponent();
        Text = GProcessMemoryManipulator.smethod_0();
        AppKey.Text = GClass61.gclass61_0.method_2(nameof(AppKey));
        VersionLabel.Text = "1.8.0 (Release)";
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GClass37.smethod_0("Help button not implemented in Evo config yet");
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Process.Start("http://www.mmoglider.com/mach");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && icontainer_0 != null)
            icontainer_0.Dispose();

        base.Dispose(disposing);
    }


    private void InitializeComponent()
    {
        MyOKButton = new Button();
        MyCancelButton = new Button();
        MyHelpButton = new Button();
        tabControl1 = new TabControl();
        TabGeneral = new TabPage();
        groupBox1 = new GroupBox();
        linkLabel1 = new LinkLabel();
        AppKey = new TextBox();
        VersionLabel = new Label();
        label2 = new Label();
        label1 = new Label();
        TabMemory = new TabPage();
        groupBox2 = new GroupBox();
        FileHelper = new TextBox();
        FileSeeds = new TextBox();
        FileStructures = new TextBox();
        label5 = new Label();
        label4 = new Label();
        label3 = new Label();
        tabControl1.SuspendLayout();
        TabGeneral.SuspendLayout();
        groupBox1.SuspendLayout();
        TabMemory.SuspendLayout();
        groupBox2.SuspendLayout();
        SuspendLayout();
        MyOKButton.DialogResult = DialogResult.OK;
        MyOKButton.Location = new Point(252, 287);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(75, 33);
        MyOKButton.TabIndex = 0;
        MyOKButton.Text = "OK";
        MyOKButton.UseVisualStyleBackColor = true;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(355, 287);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(75, 33);
        MyCancelButton.TabIndex = 1;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.UseVisualStyleBackColor = true;
        MyHelpButton.Location = new Point(451, 287);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(75, 33);
        MyHelpButton.TabIndex = 2;
        MyHelpButton.Text = "Help";
        MyHelpButton.UseVisualStyleBackColor = true;
        MyHelpButton.Click += MyHelpButton_Click;
        tabControl1.Controls.Add(TabGeneral);
        tabControl1.Controls.Add(TabMemory);
        tabControl1.Location = new Point(12, 12);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(506, 269);
        tabControl1.TabIndex = 3;
        TabGeneral.Controls.Add(groupBox1);
        TabGeneral.Location = new Point(4, 22);
        TabGeneral.Name = "TabGeneral";
        TabGeneral.Padding = new Padding(3);
        TabGeneral.Size = new Size(498, 243);
        TabGeneral.TabIndex = 0;
        TabGeneral.Text = "General";
        TabGeneral.UseVisualStyleBackColor = true;
        groupBox1.Controls.Add(linkLabel1);
        groupBox1.Controls.Add(AppKey);
        groupBox1.Controls.Add(VersionLabel);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new Point(6, 6);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(325, 171);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Registration";
        linkLabel1.AutoSize = true;
        linkLabel1.Location = new Point(100, 136);
        linkLabel1.Name = "linkLabel1";
        linkLabel1.Size = new Size(139, 13);
        linkLabel1.TabIndex = 4;
        linkLabel1.TabStop = true;
        linkLabel1.Text = "http://mmoglider.com/mach";
        linkLabel1.LinkClicked += linkLabel1_LinkClicked;
        AppKey.Location = new Point(103, 31);
        AppKey.Name = "AppKey";
        AppKey.Size = new Size(152, 20);
        AppKey.TabIndex = 3;
        VersionLabel.AutoSize = true;
        VersionLabel.ForeColor = SystemColors.Highlight;
        VersionLabel.Location = new Point(103, 69);
        VersionLabel.Name = "VersionLabel";
        VersionLabel.Size = new Size(19, 13);
        VersionLabel.TabIndex = 2;
        VersionLabel.Text = "??";
        label2.AutoSize = true;
        label2.Location = new Point(23, 69);
        label2.Name = "label2";
        label2.Size = new Size(74, 13);
        label2.TabIndex = 1;
        label2.Text = "Glider version:";
        label2.TextAlign = ContentAlignment.TopRight;
        label1.AutoSize = true;
        label1.Location = new Point(30, 34);
        label1.Name = "label1";
        label1.Size = new Size(67, 13);
        label1.TabIndex = 0;
        label1.Text = "Product key:";
        label1.TextAlign = ContentAlignment.TopRight;
        TabMemory.Controls.Add(groupBox2);
        TabMemory.Location = new Point(4, 22);
        TabMemory.Name = "TabMemory";
        TabMemory.Padding = new Padding(3);
        TabMemory.Size = new Size(498, 243);
        TabMemory.TabIndex = 1;
        TabMemory.Text = "Memory";
        TabMemory.UseVisualStyleBackColor = true;
        groupBox2.Controls.Add(FileHelper);
        groupBox2.Controls.Add(FileSeeds);
        groupBox2.Controls.Add(FileStructures);
        groupBox2.Controls.Add(label5);
        groupBox2.Controls.Add(label4);
        groupBox2.Controls.Add(label3);
        groupBox2.Location = new Point(6, 6);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(330, 160);
        groupBox2.TabIndex = 0;
        groupBox2.TabStop = false;
        groupBox2.Text = "Reading";
        FileHelper.Location = new Point(84, 112);
        FileHelper.Name = "FileHelper";
        FileHelper.Size = new Size(224, 20);
        FileHelper.TabIndex = 5;
        FileSeeds.Location = new Point(84, 71);
        FileSeeds.Name = "FileSeeds";
        FileSeeds.Size = new Size(224, 20);
        FileSeeds.TabIndex = 4;
        FileSeeds.Text = "SolitaireVista1.xml";
        FileStructures.Location = new Point(84, 30);
        FileStructures.Name = "FileStructures";
        FileStructures.Size = new Size(224, 20);
        FileStructures.TabIndex = 3;
        FileStructures.Text = "Solitaire.xml";
        label5.AutoSize = true;
        label5.Location = new Point(10, 115);
        label5.Name = "label5";
        label5.Size = new Size(68, 13);
        label5.TabIndex = 2;
        label5.Text = "Helper code:";
        label5.TextAlign = ContentAlignment.TopRight;
        label4.AutoSize = true;
        label4.Location = new Point(38, 74);
        label4.Name = "label4";
        label4.Size = new Size(40, 13);
        label4.TabIndex = 1;
        label4.Text = "Seeds:";
        label4.TextAlign = ContentAlignment.TopRight;
        label3.AutoSize = true;
        label3.Location = new Point(20, 33);
        label3.Name = "label3";
        label3.Size = new Size(58, 13);
        label3.TabIndex = 0;
        label3.Text = "Structures:";
        label3.TextAlign = ContentAlignment.TopRight;
        AcceptButton = MyOKButton;
        AutoScaleDimensions = new SizeF(6f, 13f);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = MyCancelButton;
        ClientSize = new Size(530, 332);
        ControlBox = false;
        Controls.Add(tabControl1);
        Controls.Add(MyHelpButton);
        Controls.Add(MyCancelButton);
        Controls.Add(MyOKButton);
        FormBorderStyle = FormBorderStyle.Fixed3D;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(EvoConfigWindow);
        Text = nameof(EvoConfigWindow);
        tabControl1.ResumeLayout(false);
        TabGeneral.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        TabMemory.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ResumeLayout(false);
    }
}