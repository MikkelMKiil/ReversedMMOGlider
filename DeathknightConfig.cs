// Decompiled with JetBrains decompiler
// Type: DeathknightConfig
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class DeathknightConfig : Form
{
    private TextBox AvoidAddDistance;
    private CheckBox AvoidAdds;
    private GroupBox groupBox1;
    private IContainer icontainer_0;
    private Label label1;
    private Label label10;
    private Label label2;
    private Label label9;
    private Button MyCancelButton;
    private Button MyHelpButton;
    private Button MyOKButton;
    private TextBox PullDistance;
    private CheckBox UseCorpseDust;
    private CheckBox UseGhoul;

    public DeathknightConfig()
    {
        InitializeComponent();
        PullDistance.Text = GClass61.gclass61_0.method_2("Deathknight.PullDistance");
        AvoidAddDistance.Text = GClass61.gclass61_0.method_2("Deathknight.AvoidAddDistance");
        AvoidAdds.Checked = GClass61.gclass61_0.method_5("Deathknight.AvoidAdds");
        UseGhoul.Checked = GClass61.gclass61_0.method_5("Deathknight.UseGhoul");
        UseCorpseDust.Checked = GClass61.gclass61_0.method_5("Deathknight.UseCorpseDust");
        GClass30.smethod_3(this, "Deathknight");
        GProcessMemoryManipulator.smethod_48(this);
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "Deathknight.html");
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void MyOKButton_Click(object sender, EventArgs e)
    {
        if (StartupClass.smethod_19(PullDistance.Text))
            GClass61.gclass61_0.method_0("Deathknight.PullDistance", PullDistance.Text);
        if (StartupClass.smethod_19(PullDistance.Text))
            GClass61.gclass61_0.method_0("Deathknight.AvoidAddDistance", AvoidAddDistance.Text);
        GClass61.gclass61_0.method_0("Deathknight.UseGhoul", UseGhoul.Checked.ToString());
        GClass61.gclass61_0.method_0("Deathknight.UseCorpseDust", UseCorpseDust.Checked.ToString());
        GClass61.gclass61_0.method_0("Deathknight.AvoidAdds", AvoidAdds.Checked.ToString());
        DialogResult = DialogResult.OK;
    }

    private void UseGhoul_CheckedChanged(object sender, EventArgs e)
    {
        UseCorpseDust.Enabled = UseGhoul.Checked;
    }

    private void AvoidAdds_CheckedChanged(object sender, EventArgs e)
    {
        AvoidAddDistance.Enabled = AvoidAdds.Checked;
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
        groupBox1 = new GroupBox();
        UseGhoul = new CheckBox();
        UseCorpseDust = new CheckBox();
        AvoidAdds = new CheckBox();
        label10 = new Label();
        label9 = new Label();
        AvoidAddDistance = new TextBox();
        label2 = new Label();
        PullDistance = new TextBox();
        label1 = new Label();
        groupBox1.SuspendLayout();
        SuspendLayout();
        MyOKButton.Location = new Point(87, 195);
        MyOKButton.Name = "MyOKButton";
        MyOKButton.Size = new Size(73, 28);
        MyOKButton.TabIndex = 0;
        MyOKButton.Text = "OK";
        MyOKButton.UseVisualStyleBackColor = true;
        MyOKButton.Click += MyOKButton_Click;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(176, 195);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(73, 28);
        MyCancelButton.TabIndex = 1;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.UseVisualStyleBackColor = true;
        MyCancelButton.Click += MyCancelButton_Click;
        MyHelpButton.Location = new Point(295, 195);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(73, 28);
        MyHelpButton.TabIndex = 2;
        MyHelpButton.Text = "Help";
        MyHelpButton.UseVisualStyleBackColor = true;
        MyHelpButton.Click += MyHelpButton_Click;
        groupBox1.Controls.Add(UseGhoul);
        groupBox1.Controls.Add(UseCorpseDust);
        groupBox1.Controls.Add(AvoidAdds);
        groupBox1.Controls.Add(label10);
        groupBox1.Controls.Add(label9);
        groupBox1.Controls.Add(AvoidAddDistance);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(PullDistance);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(356, 177);
        groupBox1.TabIndex = 3;
        groupBox1.TabStop = false;
        groupBox1.Text = "General";
        UseGhoul.AutoSize = true;
        UseGhoul.Location = new Point(90, 94);
        UseGhoul.Name = "UseGhoul";
        UseGhoul.Size = new Size(96, 17);
        UseGhoul.TabIndex = 35;
        UseGhoul.Text = "Summon ghoul";
        UseGhoul.CheckedChanged += UseGhoul_CheckedChanged;
        UseCorpseDust.AutoSize = true;
        UseCorpseDust.Enabled = false;
        UseCorpseDust.Location = new Point(90, 117);
        UseCorpseDust.Name = "UseCorpseDust";
        UseCorpseDust.Size = new Size(106, 17);
        UseCorpseDust.TabIndex = 34;
        UseCorpseDust.Text = "Use Corpse Dust";
        AvoidAdds.AutoSize = true;
        AvoidAdds.Location = new Point(90, 140);
        AvoidAdds.Name = "AvoidAdds";
        AvoidAdds.Size = new Size(120, 17);
        AvoidAdds.TabIndex = 33;
        AvoidAdds.Text = "Avoid possible adds";
        AvoidAdds.CheckedChanged += AvoidAdds_CheckedChanged;
        label10.AutoSize = true;
        label10.Location = new Point(18, 55);
        label10.Name = "label10";
        label10.Size = new Size(101, 13);
        label10.TabIndex = 30;
        label10.Text = "Avoid add distance:";
        label10.TextAlign = ContentAlignment.TopRight;
        label9.AutoSize = true;
        label9.Location = new Point(174, 55);
        label9.Name = "label9";
        label9.Size = new Size(32, 13);
        label9.TabIndex = 32;
        label9.Text = "yards";
        AvoidAddDistance.Enabled = false;
        AvoidAddDistance.Location = new Point(125, 52);
        AvoidAddDistance.Name = "AvoidAddDistance";
        AvoidAddDistance.Size = new Size(43, 20);
        AvoidAddDistance.TabIndex = 31;
        label2.AutoSize = true;
        label2.Location = new Point(173, 29);
        label2.Name = "label2";
        label2.Size = new Size(32, 13);
        label2.TabIndex = 2;
        label2.Text = "yards";
        PullDistance.Location = new Point(124, 26);
        PullDistance.Name = "PullDistance";
        PullDistance.Size = new Size(43, 20);
        PullDistance.TabIndex = 1;
        label1.AutoSize = true;
        label1.Location = new Point(48, 29);
        label1.Name = "label1";
        label1.Size = new Size(70, 13);
        label1.TabIndex = 0;
        label1.Text = "Pull distance:";
        label1.TextAlign = ContentAlignment.TopRight;
        AcceptButton = MyOKButton;
        AutoScaleDimensions = new SizeF(6f, 13f);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = MyCancelButton;
        ClientSize = new Size(380, 240);
        ControlBox = false;
        Controls.Add(groupBox1);
        Controls.Add(MyHelpButton);
        Controls.Add(MyCancelButton);
        Controls.Add(MyOKButton);
        FormBorderStyle = FormBorderStyle.Fixed3D;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(DeathknightConfig);
        StartPosition = FormStartPosition.CenterParent;
        Text = nameof(DeathknightConfig);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ResumeLayout(false);
    }
}