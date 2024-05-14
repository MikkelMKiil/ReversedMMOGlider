// Decompiled with JetBrains decompiler
// Type: AccountInfo
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class AccountInfo : Form
{
    private TextBox AccountName;
    private TextBox AccountPassword;
    private TextBox CharacterName;
    private Button CreateButton;
    private GroupBox groupBox1;
    private IContainer icontainer_0;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Button MyCancelButton;
    private Button MyHelpButton;
    private TextBox Nickname;
    private TextBox RealmName;
    private CheckBox UseEncrypt;

    public AccountInfo()
    {
        InitializeComponent();
        Text = GProcessMemoryManipulator.smethod_0();
        AccountName.Focus();
        AccountName.Select();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && icontainer_0 != null)
            icontainer_0.Dispose();
    
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        groupBox1 = new GroupBox();
        CreateButton = new Button();
        MyCancelButton = new Button();
        MyHelpButton = new Button();
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        label4 = new Label();
        UseEncrypt = new CheckBox();
        label5 = new Label();
        AccountName = new TextBox();
        AccountPassword = new TextBox();
        RealmName = new TextBox();
        CharacterName = new TextBox();
        Nickname = new TextBox();
        groupBox1.SuspendLayout();
        SuspendLayout();
        groupBox1.Controls.Add(Nickname);
        groupBox1.Controls.Add(CharacterName);
        groupBox1.Controls.Add(RealmName);
        groupBox1.Controls.Add(AccountPassword);
        groupBox1.Controls.Add(AccountName);
        groupBox1.Controls.Add(label5);
        groupBox1.Controls.Add(UseEncrypt);
        groupBox1.Controls.Add(label4);
        groupBox1.Controls.Add(label3);
        groupBox1.Controls.Add(label2);
        groupBox1.Controls.Add(label1);
        groupBox1.Location = new Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(354, 248);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Character Info";
        CreateButton.Location = new Point(105, 266);
        CreateButton.Name = "CreateButton";
        CreateButton.Size = new Size(77, 25);
        CreateButton.TabIndex = 0;
        CreateButton.Text = "Create";
        CreateButton.UseVisualStyleBackColor = true;
        CreateButton.Click += CreateButton_Click;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(188, 266);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(77, 25);
        MyCancelButton.TabIndex = 1;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.UseVisualStyleBackColor = true;
        MyCancelButton.Click += MyCancelButton_Click;
        MyHelpButton.Location = new Point(289, 266);
        MyHelpButton.Name = "MyHelpButton";
        MyHelpButton.Size = new Size(77, 25);
        MyHelpButton.TabIndex = 2;
        MyHelpButton.Text = "Help";
        MyHelpButton.UseVisualStyleBackColor = true;
        MyHelpButton.Click += MyHelpButton_Click;
        label1.AutoSize = true;
        label1.Location = new Point(40, 32);
        label1.Name = "label1";
        label1.Size = new Size(79, 13);
        label1.TabIndex = 0;
        label1.Text = "Account name:";
        label1.TextAlign = ContentAlignment.TopRight;
        label2.AutoSize = true;
        label2.Location = new Point(21, 58);
        label2.Name = "label2";
        label2.Size = new Size(98, 13);
        label2.TabIndex = 1;
        label2.Text = "Account password:";
        label2.TextAlign = ContentAlignment.TopRight;
        label3.AutoSize = true;
        label3.Location = new Point(50, 84);
        label3.Name = "label3";
        label3.Size = new Size(69, 13);
        label3.TabIndex = 2;
        label3.Text = "Realm name:";
        label3.TextAlign = ContentAlignment.TopRight;
        label4.AutoSize = true;
        label4.Location = new Point(34, 110);
        label4.Name = "label4";
        label4.Size = new Size(85, 13);
        label4.TabIndex = 3;
        label4.Text = "Character name:";
        label4.TextAlign = ContentAlignment.TopRight;
        UseEncrypt.AutoSize = true;
        UseEncrypt.Checked = true;
        UseEncrypt.CheckState = CheckState.Checked;
        UseEncrypt.Location = new Point(124, 207);
        UseEncrypt.Name = "UseEncrypt";
        UseEncrypt.Size = new Size(86, 17);
        UseEncrypt.TabIndex = 5;
        UseEncrypt.Text = "Encrypt data";
        UseEncrypt.UseVisualStyleBackColor = true;
        UseEncrypt.CheckedChanged += UseEncrypt_CheckedChanged;
        label5.AutoSize = true;
        label5.Location = new Point(19, 171);
        label5.Name = "label5";
        label5.Size = new Size(99, 13);
        label5.TabIndex = 5;
        label5.Text = "Account nickname:";
        label5.TextAlign = ContentAlignment.TopRight;
        AccountName.Location = new Point(125, 29);
        AccountName.Name = "AccountName";
        AccountName.Size = new Size(164, 20);
        AccountName.TabIndex = 0;
        AccountPassword.Location = new Point(125, 55);
        AccountPassword.Name = "AccountPassword";
        AccountPassword.Size = new Size(164, 20);
        AccountPassword.TabIndex = 1;
        AccountPassword.UseSystemPasswordChar = true;
        RealmName.Location = new Point(125, 81);
        RealmName.Name = "RealmName";
        RealmName.Size = new Size(164, 20);
        RealmName.TabIndex = 2;
        CharacterName.Location = new Point(125, 107);
        CharacterName.Name = "CharacterName";
        CharacterName.Size = new Size(164, 20);
        CharacterName.TabIndex = 3;
        CharacterName.TextChanged += CharacterName_TextChanged;
        Nickname.Location = new Point(124, 168);
        Nickname.Name = "Nickname";
        Nickname.Size = new Size(164, 20);
        Nickname.TabIndex = 4;
        AcceptButton = CreateButton;
        AutoScaleDimensions = new SizeF(6f, 13f);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = MyCancelButton;
        ClientSize = new Size(382, 305);
        ControlBox = false;
        Controls.Add(MyHelpButton);
        Controls.Add(MyCancelButton);
        Controls.Add(CreateButton);
        Controls.Add(groupBox1);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(AccountInfo);
        StartPosition = FormStartPosition.CenterParent;
        Text = nameof(AccountInfo);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ResumeLayout(false);
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void CreateButton_Click(object sender, EventArgs e)
    {
        if (AccountName.Text.Trim().Length != 0 && AccountPassword.Text.Trim().Length != 0 &&
            CharacterName.Text.Trim().Length != 0 && RealmName.Text.Trim().Length != 0 &&
            Nickname.Text.Trim().Length != 0)
        {
            var str = "Accounts\\" + Nickname.Text.Trim() + ".xml";
            Logger.smethod_1("Saving to: \"" + str + "\"");
            if (File.Exists(str) && MessageBox.Show(this,
                    "An Auto Login character by that nickname already exists.  Do you want to overwrite it with this information?",
                    GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            new GClass14
            {
                string_0 = AccountName.Text.Trim(),
                string_1 = AccountPassword.Text.Trim(),
                string_2 = RealmName.Text.Trim(),
                string_3 = CharacterName.Text.Trim()
            }.method_0(str, UseEncrypt.Checked);
            var num = (int)MessageBox.Show(this, "Character data saved successfully!", GProcessMemoryManipulator.smethod_0(),
                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            DialogResult = DialogResult.OK;
        }
        else
        {
            var num1 = (int)MessageBox.Show(this, "Fill out all of the fields before selecting \"Create\".",
                GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "AutoLogin.html");
    }

    private void UseEncrypt_CheckedChanged(object sender, EventArgs e)
    {
        if (UseEncrypt.Checked)
            return;
        var num = (int)MessageBox.Show(this,
            "Encryption has been turned off for this Auto Login character.  Note that the account password will be plainly visible in the configuration file for this account.\r\n\r\nFor the best account security, enable the \"Encrypt data\" option again.",
            GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    private bool method_0()
    {
        return Nickname.Text.Length == 0 ||
               (Nickname.Text.Length == CharacterName.Text.Length - 1 &&
                Nickname.Text == CharacterName.Text.Substring(0, CharacterName.Text.Length - 1)) ||
               (Nickname.Text.Length - 1 == CharacterName.Text.Length &&
                CharacterName.Text == Nickname.Text.Substring(0, Nickname.Text.Length - 1));
    }

    private void CharacterName_TextChanged(object sender, EventArgs e)
    {
        if (!method_0())
            return;
        Nickname.Text = CharacterName.Text;
    }
}