// Decompiled with JetBrains decompiler
// Type: ConfigForm
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common;
using Glider.Common.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

public class ConfigForm : Form
{
    private Button AccountCreate;
    private CheckBox AltLayout;
    private TextBox AmmoAmount;
    private ComboBox AutoLogCharacter;
    private CheckBox AutoReply;
    private TextBox AutoReplyText;
    private CheckBox AutoSkin;
    private CheckBox AvoidOtherFaction;
    private CheckBox AvoidSameFaction;
    private CheckBox BackgroundEnable;
    private TextBox BandageHealth;
    private readonly bool bool_0;
    public bool bool_1;
    private bool bool_2;
    private readonly bool bool_3;
    private Button ButtonViewCharacters;
    private CheckBox BypassLootSanity;
    private CheckBox ChatDelete;
    private GroupBox ChatLog;
    private TextBox ChatLogFrame;
    private CheckedListBox ClassFilesList;
    private ComboBox ClassList;
    private Button ClassOptionsButton;
    private TextBox CombatLogFrame;
    private Button CompileButton;
    private Container container_0;
    private Label DebuffsKnown;
    private TextBox DevBuffs;
    private RadioButton DisplayHide;
    private RadioButton DisplayNormal;
    private RadioButton DisplayShrink;
    private Button EditDebuffs;
    private Button EditKeymap;
    private TextBox ExtraPull;
    private CheckBox FastEat;
    private CheckBox FightPlayers;
    private GroupBox FollowerOptionsBox;
    private TextBox FoodAmount;
    private TextBox FriendAlert;
    private TextBox FriendLogout;
    private Label GliderVersionLabel;
    private GroupBox groupBox1;
    private GroupBox groupBox12;
    private GroupBox groupBox13;
    private GroupBox groupBox14;
    private GroupBox groupBox15;
    private GroupBox groupBox16;
    private GroupBox groupBox17;
    private GroupBox groupBox18;
    private GroupBox groupBox19;
    private GroupBox groupBox2;
    private GroupBox groupBox20;
    private GroupBox groupBox21;
    private GroupBox groupBox22;
    private GroupBox groupBox23;
    private GroupBox groupBox24;
    private GroupBox groupBox25;
    private GroupBox groupBox26;
    private GroupBox groupBox27;
    private GroupBox groupBox28;
    private GroupBox groupBox29;
    private GroupBox groupBox3;
    private GroupBox groupBox30;
    private GroupBox groupBox31;
    private GroupBox groupBox32;
    private GroupBox groupBox33;
    private GroupBox groupBox34;
    private GroupBox groupBox4;
    private GroupBox groupBox5;
    private GroupBox groupBox6;
    private GroupBox groupBox7;
    private GroupBox groupBox8;
    private GroupBox groupBox9;
    private TextBox HarvestRange;
    private HelpProvider helpProvider_0;
    private Label InitialProfile;
    private CheckBox JumpMore;
    private TextBox KeyDelay;
    private ComboBox KeyEditClass;
    private Label label1;
    private Label label10;
    private Label label11;
    private Label label12;
    private Label label13;
    private Label label14;
    private Label label15;
    private Label label16;
    private Label label17;
    private Label label18;
    private Label label19;
    private Label label2;
    private Label label20;
    private Label label21;
    private Label label22;
    private Label label23;
    private Label label24;
    private Label label25;
    private Label label26;
    private Label label27;
    private Label label28;
    private Label label29;
    private Label label3;
    private Label label30;
    private Label label31;
    private Label label32;
    private Label label33;
    private Label label34;
    private Label label35;
    private Label label36;
    private Label label37;
    private Label label38;
    private Label label39;
    private Label label4;
    private Label label40;
    private Label label41;
    private Label label42;
    private Label label43;
    private Label label44;
    private Label label45;
    private Label label46;
    private Label label47;
    private Label label48;
    private Label label49;
    private Label label5;
    private Label label50;
    private Label label51;
    private Label label52;
    private Label label53;
    private Label label54;
    private Label label55;
    private Label label56;
    private Label label57;
    private Label label58;
    private Label label59;
    private Label label6;
    private Label label60;
    private Label label61;
    private Label label7;
    private Label label8;
    private Label label9;
    private GroupBox LeaderOptionsBox;
    private LinkLabel linkLabel1;
    private CheckBox ListenEnabled;
    private TextBox ListenPassword;
    private TextBox ListenPort;
    private Button LoadKeymap;
    private CheckBox LootCheckHostiles;
    private Label Looters;
    private TextBox LootSafeDistance;
    private GroupBox MailItemBox;
    private GroupBox MailSetupBox;
    private Label mailtoLabel;
    private TextBox MailToText;
    private CheckBox ManageGamePos;
    private TextBox MaxPopups;
    private TextBox MaxResurrect;
    private CheckBox MediaKeysBox;
    private TextBox MeleeDistance;
    private CheckBox MouseSpin;
    private Button MyCancelButton;
    private Button MyHelpButton;
    private CheckBox NinjaSkin;
    private Button OKButton;
    private CheckBox PartyAdds;
    private TextBox PartyAttackDelay;
    private CheckBox PartyBuff;
    private RadioButton PartyFollower;
    private TextBox PartyFollowerStart;
    private TextBox PartyFollowerStop;
    private ComboBox PartyHealMode;
    private RadioButton PartyLeader;
    private TextBox PartyLeaderName;
    private TextBox PartyLeaderWait;
    private TextBox PartyLooters;
    private TextBox PartyLootPos;
    private TextBox PartyMember1;
    private TextBox PartyMember2;
    private TextBox PartyMember3;
    private TextBox PartyMember4;
    private GroupBox PartyOptionsBox;
    private CheckBox PartySlashFollow;
    private RadioButton PartySolo;
    private TextBox PawSpeed;
    private CheckBox PickupJunk;
    private CheckBox PlaySay;
    private CheckBox PlayWhisper;
    private TextBox ProductKeyBox;
    private Label Profile1;
    private Label Profile2;
    private Label Profile3;
    private TextBox RangedDistance;
    private CheckBox RelogEnabled;
    private CheckBox RemoveDebuffs;
    private CheckBox ResetBuffs;
    private Label ResLabel;
    private TextBox RestHealth;
    private TextBox RestMana;
    private CheckBox Resurrect;
    private CheckBox SendMail;
    private Button SetInitial;
    private Button SetProfile1;
    private Button SetProfile2;
    private Button SetProfile3;
    private CheckBox ShiftLoot;
    private CheckBox SitWhenBored;
    private CheckBox SkipLoot;
    private readonly SortedList<string, string> Offsets = new SortedList<string, string>();
    private CheckBox SoundKill;
    private TextBox SpellLeadDelay;
    private CheckBox StopAfter;
    private TextBox StopAfterMinutes;
    private CheckBox StopLootingWhenFull;
    private CheckBox StopOnVanish;
    private CheckBox StopWhenFull;
    private CheckBox Strafe;
    private CheckBox StrafeObstacles;
    private Label SubjectLabel;
    private TextBox SubjectText;
    private TabPage TabBackground;
    private TabPage TabChat;
    private TabPage TabClasses;
    private TabControl tabControl1;
    private TabPage TabDetection;
    private TabPage TabDev;
    private TabPage TabDistances;
    private TabPage TabGeneral;
    private TabPage TabInvisible;
    private TabPage TabKeys;
    private TabPage TabLimits;
    private TabPage TabMisc;
    private TabPage TabParty;
    private TabPage TabVending;
    private CheckBox TeleportLogout;
    private CheckBox TeleportStop;
    private CheckBox TurboLoot;
    private CheckBox UseBandages;
    private CheckBox UseClipboard;
    private CheckBox UseHook;
    private CheckBox UseTray;
    private RadioButton VendGreen;
    private RadioButton VendGrey;
    private TextBox VendMailList;
    private RadioButton VendWhite;
    private TextBox VendWhiteList;
    private CheckBox WalkLoot;
    private TextBox WaterAmount;
    private TextBox WebNotifyCredentials;
    private CheckBox WebNotifyEnabled;
    private TextBox WebNotifyURL;
    private Label WowVersionLabel;

    public ConfigForm(bool bool_4)
    {
        InitializeComponent();
        MessageProvider.smethod_7(PartyHealMode, "Common.PartyHealMode");
        for (var index = 0; index < StartupClass.ProfileMapping.Keys.Count; ++index)
        {
            var key = StartupClass.ProfileMapping.Keys[index];
            var profile = StartupClass.ProfileMapping[key];
            if (profile.genum1_0 == SpellEnabledState.const_1 &&
                (profile.object_0 == null || ((GGameClass)profile.object_0).IsSelectable))
            {
                ClassList.Items.Add(profile);
                if (key == ConfigManager.gclass61_0.method_2("CustomClassName"))
                    ClassList.SelectedIndex = ClassList.Items.Count - 1;
            }
        }

        if (ClassList.SelectedIndex == -1)
            ClassList.SelectedIndex = (int)StartupClass.SelectedGameClass;
        AltLayout.Enabled = true;
        if (bool_4)
            tabControl1.Controls.Add(TabInvisible);
        ChatLogFrame.Text = ConfigManager.gclass61_0.method_2(nameof(ChatLogFrame));
        CombatLogFrame.Text = ConfigManager.gclass61_0.method_2(nameof(CombatLogFrame));
        WebNotifyEnabled.Checked = ConfigManager.gclass61_0.method_2(nameof(WebNotifyEnabled)) == "True";
        WebNotifyCredentials.Text = ConfigManager.gclass61_0.method_2(nameof(WebNotifyCredentials));
        WebNotifyURL.Text = ConfigManager.gclass61_0.method_2(nameof(WebNotifyURL));
        UseTray.Checked = ConfigManager.gclass61_0.method_2(nameof(UseTray)) == "True";
        BackgroundEnable.Checked = ConfigManager.gclass61_0.method_2(nameof(BackgroundEnable)) == "True";
        BackgroundEnable.Enabled = true;
        ShiftLoot.Checked = ConfigManager.gclass61_0.method_2(nameof(ShiftLoot)) == "True";
        UseHook.Checked = ConfigManager.gclass61_0.method_2(nameof(UseHook)) == "True";
        MouseSpin.Checked = ConfigManager.gclass61_0.method_2(nameof(MouseSpin)) == "True";
        ManageGamePos.Checked = ConfigManager.gclass61_0.method_2(nameof(ManageGamePos)) == "True";
        MediaKeysBox.Checked = ConfigManager.gclass61_0.method_2("UseMediaKeys") == "True";
        AutoSkin.Checked = ConfigManager.gclass61_0.method_2(nameof(AutoSkin)) == "True";
        NinjaSkin.Checked = ConfigManager.gclass61_0.method_2(nameof(NinjaSkin)) == "True";
        WalkLoot.Checked = ConfigManager.gclass61_0.method_2(nameof(WalkLoot)) == "True";
        ProductKeyBox.Text = ConfigManager.gclass61_0.method_2("AppKey");
        SpellLeadDelay.Text = ConfigManager.gclass61_0.method_2(nameof(SpellLeadDelay));
        ExtraPull.Text = ConfigManager.gclass61_0.method_2(nameof(ExtraPull));
        ResetBuffs.Checked = ConfigManager.gclass61_0.method_2(nameof(ResetBuffs)) == "True";
        Resurrect.Checked = ConfigManager.gclass61_0.method_2(nameof(Resurrect)) == "True";
        AltLayout.Checked = ConfigManager.gclass61_0.method_2(nameof(AltLayout)) == "True";
        MaxResurrect.Text = ConfigManager.gclass61_0.method_2(nameof(MaxResurrect));
        StopLootingWhenFull.Checked = ConfigManager.gclass61_0.method_5(nameof(StopLootingWhenFull));
        TurboLoot.Checked = ConfigManager.gclass61_0.method_5(nameof(TurboLoot));
        StopWhenFull.Checked = ConfigManager.gclass61_0.method_5(nameof(StopWhenFull));
        if (ConfigManager.gclass61_0.method_2("AutoStop") == "True")
            StopAfter.Checked = true;
        StopAfterMinutes.Text = ConfigManager.gclass61_0.method_2("AutoStopMinutes");
        StopAfter_CheckedChanged(null, null);
        RestHealth.Text = ConfigManager.gclass61_0.method_2(nameof(RestHealth));
        RestMana.Text = ConfigManager.gclass61_0.method_2(nameof(RestMana));
        ChatDelete.Checked = ConfigManager.gclass61_0.method_2(nameof(ChatDelete)) == "True";
        PlayWhisper.Checked = ConfigManager.gclass61_0.method_2("ChatWhisper") == "True";
        PlaySay.Checked = ConfigManager.gclass61_0.method_2(nameof(PlaySay)) == "True";
        SoundKill.Checked = ConfigManager.gclass61_0.method_2(nameof(SoundKill)) == "True";
        AutoReply.Checked = ConfigManager.gclass61_0.method_2("ChatAutoReply") == "True";
        AutoReplyText.Text = ConfigManager.gclass61_0.method_2("ChatAutoReplyText");
        RemoveDebuffs.Checked = ConfigManager.gclass61_0.method_5(nameof(RemoveDebuffs));
        UseClipboard.Checked = ConfigManager.gclass61_0.method_2(nameof(UseClipboard)) == "True";
        KeyDelay.Text = ConfigManager.gclass61_0.method_2(nameof(KeyDelay));
        PawSpeed.Text = ConfigManager.gclass61_0.method_2(nameof(PawSpeed));
        FastEat.Checked = ConfigManager.gclass61_0.method_2(nameof(FastEat)) == "True";
        UseBandages.Checked = ConfigManager.gclass61_0.method_2(nameof(UseBandages)) == "True";
        SitWhenBored.Checked = ConfigManager.gclass61_0.method_2(nameof(SitWhenBored)) == "True";
        BandageHealth.Text = ConfigManager.gclass61_0.method_2(nameof(BandageHealth));
        JumpMore.Checked = ConfigManager.gclass61_0.method_2(nameof(JumpMore)) == "True";
        Strafe.Checked = ConfigManager.gclass61_0.method_2(nameof(Strafe)) == "True";
        SkipLoot.Checked = ConfigManager.gclass61_0.method_2(nameof(SkipLoot)) == "True";
        HarvestRange.Text = ConfigManager.gclass61_0.method_2(nameof(HarvestRange));
        PickupJunk.Checked = ConfigManager.gclass61_0.method_2(nameof(PickupJunk)) == "True";
        TeleportStop.Checked = ConfigManager.gclass61_0.method_2(nameof(TeleportStop)) == "True";
        TeleportLogout.Checked = ConfigManager.gclass61_0.method_2(nameof(TeleportLogout)) == "True";
        FoodAmount.Text = ConfigManager.gclass61_0.method_2(nameof(FoodAmount));
        AmmoAmount.Text = ConfigManager.gclass61_0.method_2(nameof(AmmoAmount));
        WaterAmount.Text = ConfigManager.gclass61_0.method_2(nameof(WaterAmount));
        FriendAlert.Text = ConfigManager.gclass61_0.method_2(nameof(FriendAlert));
        FriendLogout.Text = ConfigManager.gclass61_0.method_2(nameof(FriendLogout));
        MaxPopups.Text = ConfigManager.gclass61_0.method_2(nameof(MaxPopups));
        AvoidSameFaction.Checked = ConfigManager.gclass61_0.method_2(nameof(AvoidSameFaction)) == "True";
        AvoidOtherFaction.Checked = ConfigManager.gclass61_0.method_2(nameof(AvoidOtherFaction)) == "True";
        LootSafeDistance.Text = ConfigManager.gclass61_0.method_2("LootCheckDistance");
        LootCheckHostiles.Checked = ConfigManager.gclass61_0.method_5(nameof(LootCheckHostiles));
        MeleeDistance.Text = ConfigManager.gclass61_0.method_2(nameof(MeleeDistance));
        RangedDistance.Text = ConfigManager.gclass61_0.method_2(nameof(RangedDistance));
        StopOnVanish.Checked = ConfigManager.gclass61_0.method_5(nameof(StopOnVanish));
        FightPlayers.Checked = ConfigManager.gclass61_0.method_5(nameof(FightPlayers));
        PartyAdds.Checked = ConfigManager.gclass61_0.method_2(nameof(PartyAdds)) == "True";
        PartyBuff.Checked = ConfigManager.gclass61_0.method_2(nameof(PartyBuff)) == "True";
        PartySlashFollow.Checked = ConfigManager.gclass61_0.method_2(nameof(PartySlashFollow)) == "True";
        switch (ConfigManager.gclass61_0.method_2("PartyMode"))
        {
            case "Solo":
                PartySolo.Checked = true;
                break;
            case "Leader":
                PartyLeader.Checked = true;
                break;
            case "Follower":
                PartyFollower.Checked = true;
                break;
        }

        switch (ConfigManager.gclass61_0.method_2(nameof(PartyHealMode)))
        {
            case "Dedicated":
                PartyHealMode.SelectedIndex = 0;
                break;
            case "Normal":
                PartyHealMode.SelectedIndex = 1;
                break;
            case "Never":
                PartyHealMode.SelectedIndex = 2;
                break;
        }

        PartyLooters.Text = ConfigManager.gclass61_0.method_2(nameof(PartyLooters));
        PartyLootPos.Text = ConfigManager.gclass61_0.method_2(nameof(PartyLootPos));
        PartyMember1.Text = ConfigManager.gclass61_0.method_2(nameof(PartyMember1));
        PartyMember2.Text = ConfigManager.gclass61_0.method_2(nameof(PartyMember2));
        PartyMember3.Text = ConfigManager.gclass61_0.method_2(nameof(PartyMember3));
        PartyMember4.Text = ConfigManager.gclass61_0.method_2(nameof(PartyMember4));
        PartyLeaderName.Text = ConfigManager.gclass61_0.method_2(nameof(PartyLeaderName));
        PartyAttackDelay.Text = ConfigManager.gclass61_0.method_2(nameof(PartyAttackDelay));
        PartyLeaderWait.Text = ConfigManager.gclass61_0.method_2(nameof(PartyLeaderWait));
        PartyFollowerStart.Text = ConfigManager.gclass61_0.method_2(nameof(PartyFollowerStart));
        PartyFollowerStop.Text = ConfigManager.gclass61_0.method_2(nameof(PartyFollowerStop));
        bool_0 = false;
        ListenEnabled.Checked = ConfigManager.gclass61_0.method_5(nameof(ListenEnabled));
        ListenPort.Text = ConfigManager.gclass61_0.method_2(nameof(ListenPort));
        ListenPassword.Text = ConfigManager.gclass61_0.method_2(nameof(ListenPassword));
        BypassLootSanity.Checked = ConfigManager.gclass61_0.method_5(nameof(BypassLootSanity));
        RelogEnabled.Checked = ConfigManager.gclass61_0.method_5(nameof(RelogEnabled));
        StrafeObstacles.Checked = ConfigManager.gclass61_0.method_2(nameof(StrafeObstacles)) == "True";
        MessageProvider.smethod_3(this, "Config");
        if (method_20())
            linkLabel1.Text = "http://www.mmoglider.com.cn";
        GliderVersionLabel.Text = MessageProvider.smethod_6("Config.GliderVersionLabel", "1.8.0", "Release");
        WowVersionLabel.Text = MessageProvider.smethod_6("Config.WowVersionLabel", StartupClass.WowVersionLabel);
        DebuffsKnown.Text = MessageProvider.smethod_6("Config.DebuffsKnown", StartupClass.KnownDebuffs.method_9());
        if (ConfigManager.gclass61_0.method_2("LastProfile") != null)
            InitialProfile.Text = ConfigManager.gclass61_0.method_2("LastProfile");
        else
            InitialProfile.Text = MessageProvider.GetMessage(771);
        method_0(nameof(Profile1), Profile1);
        method_0(nameof(Profile2), Profile2);
        method_0(nameof(Profile3), Profile3);
        switch (ConfigManager.gclass61_0.method_2("BackgroundDisplay"))
        {
            case "Normal":
                DisplayNormal.Checked = true;
                break;
            case "Hide":
                DisplayHide.Checked = true;
                break;
            case "Shrink":
                DisplayShrink.Checked = true;
                break;
        }

        switch (ConfigManager.gclass61_0.method_2("VendType"))
        {
            case "Poor":
                VendGrey.Checked = true;
                break;
            case "Common":
                VendWhite.Checked = true;
                break;
            case "Uncommon":
                VendGreen.Checked = true;
                break;
        }

        if (ConfigManager.gclass61_0.method_2(nameof(VendWhiteList)) != null)
            VendWhiteList.Text = ConfigManager.gclass61_0.method_2(nameof(VendWhiteList)).Replace(",", Environment.NewLine);
        if (ConfigManager.gclass61_0.method_2(nameof(VendMailList)) != null)
            VendMailList.Text = ConfigManager.gclass61_0.method_2(nameof(VendMailList)).Replace(",", Environment.NewLine);
        MailToText.Text = ConfigManager.gclass61_0.method_2(nameof(MailToText));
        SubjectText.Text = ConfigManager.gclass61_0.method_2(nameof(SubjectText));
        SendMail.Checked = ConfigManager.gclass61_0.method_2(nameof(SendMail)) == "True";
        // Glider debug flag removed
        method_11();
        method_16();
        method_18();
        GameMemoryAccess.smethod_48(this);
        GameMemoryAccess.smethod_51(helpProvider_0);
        StartupClass.PartyStateManager.bool_4 = false;
        bool_3 = true;
        StartupClass.MainWindowHandle = this;
    }

    private void method_0(string string_0, Label label_0)
    {
        var str = ConfigManager.gclass61_0.method_2(string_0);
        if (str == null)
            label_0.Text = MessageProvider.GetMessage(771);
        else
            label_0.Text = str;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && container_0 != null)
            container_0.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.OKButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ClassOptionsButton = new System.Windows.Forms.Button();
            this.ClassList = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RelogEnabled = new System.Windows.Forms.CheckBox();
            this.TurboLoot = new System.Windows.Forms.CheckBox();
            this.StopLootingWhenFull = new System.Windows.Forms.CheckBox();
            this.ManageGamePos = new System.Windows.Forms.CheckBox();
            this.AltLayout = new System.Windows.Forms.CheckBox();
            this.SoundKill = new System.Windows.Forms.CheckBox();
            this.UseTray = new System.Windows.Forms.CheckBox();
            this.ShiftLoot = new System.Windows.Forms.CheckBox();
            this.NinjaSkin = new System.Windows.Forms.CheckBox();
            this.RemoveDebuffs = new System.Windows.Forms.CheckBox();
            this.StrafeObstacles = new System.Windows.Forms.CheckBox();
            this.BypassLootSanity = new System.Windows.Forms.CheckBox();
            this.FightPlayers = new System.Windows.Forms.CheckBox();
            this.SitWhenBored = new System.Windows.Forms.CheckBox();
            this.SkipLoot = new System.Windows.Forms.CheckBox();
            this.AutoSkin = new System.Windows.Forms.CheckBox();
            this.MediaKeysBox = new System.Windows.Forms.CheckBox();
            this.ResetBuffs = new System.Windows.Forms.CheckBox();
            this.WalkLoot = new System.Windows.Forms.CheckBox();
            this.helpProvider_0 = new System.Windows.Forms.HelpProvider();
            this.MyHelpButton = new System.Windows.Forms.Button();
            this.ProductKeyBox = new System.Windows.Forms.TextBox();
            this.StopAfterMinutes = new System.Windows.Forms.TextBox();
            this.RestHealth = new System.Windows.Forms.TextBox();
            this.RestMana = new System.Windows.Forms.TextBox();
            this.StopAfter = new System.Windows.Forms.CheckBox();
            this.ChatDelete = new System.Windows.Forms.CheckBox();
            this.PlayWhisper = new System.Windows.Forms.CheckBox();
            this.AutoReply = new System.Windows.Forms.CheckBox();
            this.AutoReplyText = new System.Windows.Forms.TextBox();
            this.KeyDelay = new System.Windows.Forms.TextBox();
            this.BandageHealth = new System.Windows.Forms.TextBox();
            this.UseBandages = new System.Windows.Forms.CheckBox();
            this.FastEat = new System.Windows.Forms.CheckBox();
            this.UseClipboard = new System.Windows.Forms.CheckBox();
            this.LoadKeymap = new System.Windows.Forms.Button();
            this.PartyAttackDelay = new System.Windows.Forms.TextBox();
            this.PartyLeaderName = new System.Windows.Forms.TextBox();
            this.PartyLootPos = new System.Windows.Forms.TextBox();
            this.PartyAdds = new System.Windows.Forms.CheckBox();
            this.PartyLooters = new System.Windows.Forms.TextBox();
            this.PartyFollower = new System.Windows.Forms.RadioButton();
            this.PartyLeader = new System.Windows.Forms.RadioButton();
            this.PartySolo = new System.Windows.Forms.RadioButton();
            this.PartyMember1 = new System.Windows.Forms.TextBox();
            this.PartyMember2 = new System.Windows.Forms.TextBox();
            this.PartyMember3 = new System.Windows.Forms.TextBox();
            this.PartyMember4 = new System.Windows.Forms.TextBox();
            this.PartyBuff = new System.Windows.Forms.CheckBox();
            this.PartySlashFollow = new System.Windows.Forms.CheckBox();
            this.ListenPassword = new System.Windows.Forms.TextBox();
            this.ListenPort = new System.Windows.Forms.TextBox();
            this.ListenEnabled = new System.Windows.Forms.CheckBox();
            this.StopWhenFull = new System.Windows.Forms.CheckBox();
            this.PlaySay = new System.Windows.Forms.CheckBox();
            this.CombatLogFrame = new System.Windows.Forms.TextBox();
            this.ChatLogFrame = new System.Windows.Forms.TextBox();
            this.BackgroundEnable = new System.Windows.Forms.CheckBox();
            this.TabGeneral = new System.Windows.Forms.TabPage();
            this.groupBox31 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.AutoLogCharacter = new System.Windows.Forms.ComboBox();
            this.ButtonViewCharacters = new System.Windows.Forms.Button();
            this.AccountCreate = new System.Windows.Forms.Button();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.WowVersionLabel = new System.Windows.Forms.Label();
            this.GliderVersionLabel = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TabLimits = new System.Windows.Forms.TabPage();
            this.groupBox32 = new System.Windows.Forms.GroupBox();
            this.AmmoAmount = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.WaterAmount = new System.Windows.Forms.TextBox();
            this.FoodAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.MaxResurrect = new System.Windows.Forms.TextBox();
            this.ResLabel = new System.Windows.Forms.Label();
            this.Resurrect = new System.Windows.Forms.CheckBox();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.DebuffsKnown = new System.Windows.Forms.Label();
            this.EditDebuffs = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TabDetection = new System.Windows.Forms.TabPage();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.StopOnVanish = new System.Windows.Forms.CheckBox();
            this.TeleportLogout = new System.Windows.Forms.CheckBox();
            this.TeleportStop = new System.Windows.Forms.CheckBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.Strafe = new System.Windows.Forms.CheckBox();
            this.JumpMore = new System.Windows.Forms.CheckBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.label50 = new System.Windows.Forms.Label();
            this.MaxPopups = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.AvoidOtherFaction = new System.Windows.Forms.CheckBox();
            this.AvoidSameFaction = new System.Windows.Forms.CheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.FriendLogout = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.FriendAlert = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.TabKeys = new System.Windows.Forms.TabPage();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.MouseSpin = new System.Windows.Forms.CheckBox();
            this.label48 = new System.Windows.Forms.Label();
            this.PawSpeed = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.EditKeymap = new System.Windows.Forms.Button();
            this.KeyEditClass = new System.Windows.Forms.ComboBox();
            this.label61 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.SpellLeadDelay = new System.Windows.Forms.TextBox();
            this.UseHook = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TabDistances = new System.Windows.Forms.TabPage();
            this.groupBox30 = new System.Windows.Forms.GroupBox();
            this.label54 = new System.Windows.Forms.Label();
            this.LootSafeDistance = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.LootCheckHostiles = new System.Windows.Forms.CheckBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.PickupJunk = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ExtraPull = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.HarvestRange = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.PartyFollowerStop = new System.Windows.Forms.TextBox();
            this.PartyFollowerStart = new System.Windows.Forms.TextBox();
            this.PartyLeaderWait = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.RangedDistance = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.MeleeDistance = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TabChat = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ChatLog = new System.Windows.Forms.GroupBox();
            this.label53 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.TabParty = new System.Windows.Forms.TabPage();
            this.FollowerOptionsBox = new System.Windows.Forms.GroupBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.LeaderOptionsBox = new System.Windows.Forms.GroupBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.PartyOptionsBox = new System.Windows.Forms.GroupBox();
            this.PartyHealMode = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.Looters = new System.Windows.Forms.Label();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.TabMisc = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TabBackground = new System.Windows.Forms.TabPage();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.WebNotifyCredentials = new System.Windows.Forms.TextBox();
            this.WebNotifyURL = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.WebNotifyEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox25 = new System.Windows.Forms.GroupBox();
            this.DisplayShrink = new System.Windows.Forms.RadioButton();
            this.DisplayHide = new System.Windows.Forms.RadioButton();
            this.DisplayNormal = new System.Windows.Forms.RadioButton();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.TabVending = new System.Windows.Forms.TabPage();
            this.MailItemBox = new System.Windows.Forms.GroupBox();
            this.VendMailList = new System.Windows.Forms.TextBox();
            this.MailSetupBox = new System.Windows.Forms.GroupBox();
            this.SendMail = new System.Windows.Forms.CheckBox();
            this.SubjectLabel = new System.Windows.Forms.Label();
            this.SubjectText = new System.Windows.Forms.TextBox();
            this.mailtoLabel = new System.Windows.Forms.Label();
            this.MailToText = new System.Windows.Forms.TextBox();
            this.groupBox33 = new System.Windows.Forms.GroupBox();
            this.VendGrey = new System.Windows.Forms.RadioButton();
            this.VendWhite = new System.Windows.Forms.RadioButton();
            this.VendGreen = new System.Windows.Forms.RadioButton();
            this.groupBox34 = new System.Windows.Forms.GroupBox();
            this.VendWhiteList = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabClasses = new System.Windows.Forms.TabPage();
            this.groupBox29 = new System.Windows.Forms.GroupBox();
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.ClassFilesList = new System.Windows.Forms.CheckedListBox();
            this.CompileButton = new System.Windows.Forms.Button();
            this.TabInvisible = new System.Windows.Forms.TabPage();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.SetProfile3 = new System.Windows.Forms.Button();
            this.SetProfile2 = new System.Windows.Forms.Button();
            this.SetProfile1 = new System.Windows.Forms.Button();
            this.SetInitial = new System.Windows.Forms.Button();
            this.Profile3 = new System.Windows.Forms.Label();
            this.Profile2 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.Profile1 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.InitialProfile = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.TabDev = new System.Windows.Forms.TabPage();
            this.DevBuffs = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.TabGeneral.SuspendLayout();
            this.groupBox31.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.TabLimits.SuspendLayout();
            this.groupBox32.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.TabDetection.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.TabKeys.SuspendLayout();
            this.groupBox27.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.TabDistances.SuspendLayout();
            this.groupBox30.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.TabChat.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.ChatLog.SuspendLayout();
            this.TabParty.SuspendLayout();
            this.FollowerOptionsBox.SuspendLayout();
            this.LeaderOptionsBox.SuspendLayout();
            this.PartyOptionsBox.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.TabMisc.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.TabBackground.SuspendLayout();
            this.groupBox26.SuspendLayout();
            this.groupBox25.SuspendLayout();
            this.groupBox24.SuspendLayout();
            this.TabVending.SuspendLayout();
            this.MailItemBox.SuspendLayout();
            this.MailSetupBox.SuspendLayout();
            this.groupBox33.SuspendLayout();
            this.groupBox34.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.TabClasses.SuspendLayout();
            this.groupBox28.SuspendLayout();
            this.TabInvisible.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.TabDev.SuspendLayout();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(611, 441);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(115, 37);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(760, 441);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(115, 37);
            this.MyCancelButton.TabIndex = 1;
            this.MyCancelButton.Text = "Cancel";
            this.MyCancelButton.Click += new System.EventHandler(this.MyCancelButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ClassOptionsButton);
            this.groupBox1.Controls.Add(this.ClassList);
            this.groupBox1.Location = new System.Drawing.Point(26, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 187);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Class";
            // 
            // ClassOptionsButton
            // 
            this.helpProvider_0.SetHelpKeyword(this.ClassOptionsButton, "General.html");
            this.helpProvider_0.SetHelpNavigator(this.ClassOptionsButton, System.Windows.Forms.HelpNavigator.Topic);
            this.ClassOptionsButton.Location = new System.Drawing.Point(122, 98);
            this.ClassOptionsButton.Name = "ClassOptionsButton";
            this.helpProvider_0.SetShowHelp(this.ClassOptionsButton, true);
            this.ClassOptionsButton.Size = new System.Drawing.Size(140, 34);
            this.ClassOptionsButton.TabIndex = 1;
            this.ClassOptionsButton.Text = "Options";
            // 
            // ClassList
            // 
            this.ClassList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.helpProvider_0.SetHelpKeyword(this.ClassList, "General.html");
            this.helpProvider_0.SetHelpNavigator(this.ClassList, System.Windows.Forms.HelpNavigator.Topic);
            this.ClassList.Location = new System.Drawing.Point(35, 60);
            this.ClassList.Name = "ClassList";
            this.helpProvider_0.SetShowHelp(this.ClassList, true);
            this.ClassList.Size = new System.Drawing.Size(336, 28);
            this.ClassList.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RelogEnabled);
            this.groupBox2.Controls.Add(this.TurboLoot);
            this.groupBox2.Controls.Add(this.StopLootingWhenFull);
            this.groupBox2.Controls.Add(this.ManageGamePos);
            this.groupBox2.Controls.Add(this.AltLayout);
            this.groupBox2.Controls.Add(this.SoundKill);
            this.groupBox2.Controls.Add(this.UseTray);
            this.groupBox2.Controls.Add(this.ShiftLoot);
            this.groupBox2.Controls.Add(this.NinjaSkin);
            this.groupBox2.Controls.Add(this.RemoveDebuffs);
            this.groupBox2.Controls.Add(this.StrafeObstacles);
            this.groupBox2.Controls.Add(this.BypassLootSanity);
            this.groupBox2.Controls.Add(this.FightPlayers);
            this.groupBox2.Controls.Add(this.SitWhenBored);
            this.groupBox2.Controls.Add(this.SkipLoot);
            this.groupBox2.Controls.Add(this.AutoSkin);
            this.groupBox2.Controls.Add(this.MediaKeysBox);
            this.groupBox2.Controls.Add(this.ResetBuffs);
            this.groupBox2.Controls.Add(this.WalkLoot);
            this.groupBox2.Location = new System.Drawing.Point(13, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(974, 277);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Miscellaneous";
            // 
            // RelogEnabled
            // 
            this.RelogEnabled.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.RelogEnabled, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.RelogEnabled, System.Windows.Forms.HelpNavigator.Topic);
            this.RelogEnabled.Location = new System.Drawing.Point(32, 235);
            this.RelogEnabled.Name = "RelogEnabled";
            this.helpProvider_0.SetShowHelp(this.RelogEnabled, true);
            this.RelogEnabled.Size = new System.Drawing.Size(180, 24);
            this.RelogEnabled.TabIndex = 19;
            this.RelogEnabled.Text = "Relog on disconnect";
            // 
            // TurboLoot
            // 
            this.TurboLoot.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.TurboLoot, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.TurboLoot, System.Windows.Forms.HelpNavigator.Topic);
            this.TurboLoot.Location = new System.Drawing.Point(584, 202);
            this.TurboLoot.Name = "TurboLoot";
            this.helpProvider_0.SetShowHelp(this.TurboLoot, true);
            this.TurboLoot.Size = new System.Drawing.Size(183, 24);
            this.TurboLoot.TabIndex = 18;
            this.TurboLoot.Text = "Turbo loot when safe";
            // 
            // StopLootingWhenFull
            // 
            this.StopLootingWhenFull.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.StopLootingWhenFull, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.StopLootingWhenFull, System.Windows.Forms.HelpNavigator.Topic);
            this.StopLootingWhenFull.Location = new System.Drawing.Point(584, 167);
            this.StopLootingWhenFull.Name = "StopLootingWhenFull";
            this.helpProvider_0.SetShowHelp(this.StopLootingWhenFull, true);
            this.StopLootingWhenFull.Size = new System.Drawing.Size(268, 24);
            this.StopLootingWhenFull.TabIndex = 6;
            this.StopLootingWhenFull.Text = "Stop looting when inventory is full";
            // 
            // ManageGamePos
            // 
            this.ManageGamePos.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.ManageGamePos, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.ManageGamePos, System.Windows.Forms.HelpNavigator.Topic);
            this.ManageGamePos.Location = new System.Drawing.Point(584, 133);
            this.ManageGamePos.Name = "ManageGamePos";
            this.helpProvider_0.SetShowHelp(this.ManageGamePos, true);
            this.ManageGamePos.Size = new System.Drawing.Size(246, 24);
            this.ManageGamePos.TabIndex = 17;
            this.ManageGamePos.Text = "Remember game window size";
            // 
            // AltLayout
            // 
            this.AltLayout.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.AltLayout, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.AltLayout, System.Windows.Forms.HelpNavigator.Topic);
            this.AltLayout.Location = new System.Drawing.Point(584, 99);
            this.AltLayout.Name = "AltLayout";
            this.helpProvider_0.SetShowHelp(this.AltLayout, true);
            this.AltLayout.Size = new System.Drawing.Size(209, 24);
            this.AltLayout.TabIndex = 16;
            this.AltLayout.Text = "Horizontal window layout";
            // 
            // SoundKill
            // 
            this.SoundKill.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.SoundKill, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.SoundKill, System.Windows.Forms.HelpNavigator.Topic);
            this.SoundKill.Location = new System.Drawing.Point(32, 202);
            this.SoundKill.Name = "SoundKill";
            this.helpProvider_0.SetShowHelp(this.SoundKill, true);
            this.SoundKill.Size = new System.Drawing.Size(116, 24);
            this.SoundKill.TabIndex = 15;
            this.SoundKill.Text = "Beep on kill";
            // 
            // UseTray
            // 
            this.UseTray.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.UseTray, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.UseTray, System.Windows.Forms.HelpNavigator.Topic);
            this.UseTray.Location = new System.Drawing.Point(584, 64);
            this.UseTray.Name = "UseTray";
            this.helpProvider_0.SetShowHelp(this.UseTray, true);
            this.UseTray.Size = new System.Drawing.Size(166, 24);
            this.UseTray.TabIndex = 13;
            this.UseTray.Text = "Icon in system tray";
            // 
            // ShiftLoot
            // 
            this.ShiftLoot.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.ShiftLoot, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.ShiftLoot, System.Windows.Forms.HelpNavigator.Topic);
            this.ShiftLoot.Location = new System.Drawing.Point(584, 31);
            this.ShiftLoot.Name = "ShiftLoot";
            this.helpProvider_0.SetShowHelp(this.ShiftLoot, true);
            this.ShiftLoot.Size = new System.Drawing.Size(148, 24);
            this.ShiftLoot.TabIndex = 12;
            this.ShiftLoot.Text = "Shift to autoloot";
            // 
            // NinjaSkin
            // 
            this.NinjaSkin.AutoSize = true;
            this.NinjaSkin.Enabled = false;
            this.helpProvider_0.SetHelpKeyword(this.NinjaSkin, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.NinjaSkin, System.Windows.Forms.HelpNavigator.Topic);
            this.NinjaSkin.Location = new System.Drawing.Point(266, 64);
            this.NinjaSkin.Name = "NinjaSkin";
            this.helpProvider_0.SetShowHelp(this.NinjaSkin, true);
            this.NinjaSkin.Size = new System.Drawing.Size(102, 24);
            this.NinjaSkin.TabIndex = 7;
            this.NinjaSkin.Text = "Ninja skin";
            // 
            // RemoveDebuffs
            // 
            this.RemoveDebuffs.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.RemoveDebuffs, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.RemoveDebuffs, System.Windows.Forms.HelpNavigator.Topic);
            this.RemoveDebuffs.Location = new System.Drawing.Point(32, 167);
            this.RemoveDebuffs.Name = "RemoveDebuffs";
            this.helpProvider_0.SetShowHelp(this.RemoveDebuffs, true);
            this.RemoveDebuffs.Size = new System.Drawing.Size(152, 24);
            this.RemoveDebuffs.TabIndex = 4;
            this.RemoveDebuffs.Text = "Remove debuffs";
            // 
            // StrafeObstacles
            // 
            this.StrafeObstacles.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.StrafeObstacles, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.StrafeObstacles, System.Windows.Forms.HelpNavigator.Topic);
            this.StrafeObstacles.Location = new System.Drawing.Point(266, 167);
            this.StrafeObstacles.Name = "StrafeObstacles";
            this.helpProvider_0.SetShowHelp(this.StrafeObstacles, true);
            this.StrafeObstacles.Size = new System.Drawing.Size(205, 24);
            this.StrafeObstacles.TabIndex = 10;
            this.StrafeObstacles.Text = "Strafe around obstacles";
            // 
            // BypassLootSanity
            // 
            this.BypassLootSanity.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.BypassLootSanity, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.BypassLootSanity, System.Windows.Forms.HelpNavigator.Topic);
            this.BypassLootSanity.Location = new System.Drawing.Point(266, 202);
            this.BypassLootSanity.Name = "BypassLootSanity";
            this.helpProvider_0.SetShowHelp(this.BypassLootSanity, true);
            this.BypassLootSanity.Size = new System.Drawing.Size(163, 24);
            this.BypassLootSanity.TabIndex = 11;
            this.BypassLootSanity.Text = "Skip sanity on loot";
            // 
            // FightPlayers
            // 
            this.FightPlayers.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.FightPlayers, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.FightPlayers, System.Windows.Forms.HelpNavigator.Topic);
            this.FightPlayers.Location = new System.Drawing.Point(266, 133);
            this.FightPlayers.Name = "FightPlayers";
            this.helpProvider_0.SetShowHelp(this.FightPlayers, true);
            this.FightPlayers.Size = new System.Drawing.Size(219, 24);
            this.FightPlayers.TabIndex = 9;
            this.FightPlayers.Text = "Fight back against players";
            // 
            // SitWhenBored
            // 
            this.SitWhenBored.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.SitWhenBored, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.SitWhenBored, System.Windows.Forms.HelpNavigator.Topic);
            this.SitWhenBored.Location = new System.Drawing.Point(32, 133);
            this.SitWhenBored.Name = "SitWhenBored";
            this.helpProvider_0.SetShowHelp(this.SitWhenBored, true);
            this.SitWhenBored.Size = new System.Drawing.Size(141, 24);
            this.SitWhenBored.TabIndex = 3;
            this.SitWhenBored.Text = "Sit when bored";
            // 
            // SkipLoot
            // 
            this.SkipLoot.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.SkipLoot, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.SkipLoot, System.Windows.Forms.HelpNavigator.Topic);
            this.SkipLoot.Location = new System.Drawing.Point(32, 99);
            this.SkipLoot.Name = "SkipLoot";
            this.helpProvider_0.SetShowHelp(this.SkipLoot, true);
            this.SkipLoot.Size = new System.Drawing.Size(136, 24);
            this.SkipLoot.TabIndex = 2;
            this.SkipLoot.Text = "Skip all looting";
            // 
            // AutoSkin
            // 
            this.AutoSkin.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.AutoSkin, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.AutoSkin, System.Windows.Forms.HelpNavigator.Topic);
            this.AutoSkin.Location = new System.Drawing.Point(266, 31);
            this.AutoSkin.Name = "AutoSkin";
            this.helpProvider_0.SetShowHelp(this.AutoSkin, true);
            this.AutoSkin.Size = new System.Drawing.Size(126, 24);
            this.AutoSkin.TabIndex = 6;
            this.AutoSkin.Text = "Skin corpses";
            // 
            // MediaKeysBox
            // 
            this.MediaKeysBox.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.MediaKeysBox, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.MediaKeysBox, System.Windows.Forms.HelpNavigator.Topic);
            this.helpProvider_0.SetHelpString(this.MediaKeysBox, "");
            this.MediaKeysBox.Location = new System.Drawing.Point(32, 31);
            this.MediaKeysBox.Name = "MediaKeysBox";
            this.helpProvider_0.SetShowHelp(this.MediaKeysBox, true);
            this.MediaKeysBox.Size = new System.Drawing.Size(147, 24);
            this.MediaKeysBox.TabIndex = 0;
            this.MediaKeysBox.Text = "Use media keys";
            // 
            // ResetBuffs
            // 
            this.ResetBuffs.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.ResetBuffs, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.ResetBuffs, System.Windows.Forms.HelpNavigator.Topic);
            this.ResetBuffs.Location = new System.Drawing.Point(266, 99);
            this.ResetBuffs.Name = "ResetBuffs";
            this.helpProvider_0.SetShowHelp(this.ResetBuffs, true);
            this.ResetBuffs.Size = new System.Drawing.Size(118, 24);
            this.ResetBuffs.TabIndex = 8;
            this.ResetBuffs.Text = "Reset buffs";
            // 
            // WalkLoot
            // 
            this.WalkLoot.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.WalkLoot, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.WalkLoot, System.Windows.Forms.HelpNavigator.Topic);
            this.WalkLoot.Location = new System.Drawing.Point(32, 64);
            this.WalkLoot.Name = "WalkLoot";
            this.helpProvider_0.SetShowHelp(this.WalkLoot, true);
            this.WalkLoot.Size = new System.Drawing.Size(118, 24);
            this.WalkLoot.TabIndex = 1;
            this.WalkLoot.Text = "Walk to loot";
            // 
            // helpProvider_0
            // 
            this.helpProvider_0.HelpNamespace = "Glider.chm";
            // 
            // MyHelpButton
            // 
            this.helpProvider_0.SetHelpKeyword(this.MyHelpButton, "General.html");
            this.helpProvider_0.SetHelpNavigator(this.MyHelpButton, System.Windows.Forms.HelpNavigator.Topic);
            this.MyHelpButton.Location = new System.Drawing.Point(899, 441);
            this.MyHelpButton.Name = "MyHelpButton";
            this.helpProvider_0.SetShowHelp(this.MyHelpButton, true);
            this.MyHelpButton.Size = new System.Drawing.Size(114, 37);
            this.MyHelpButton.TabIndex = 2;
            this.MyHelpButton.Text = "Help";
            // 
            // ProductKeyBox
            // 
            this.helpProvider_0.SetHelpKeyword(this.ProductKeyBox, "General.html");
            this.helpProvider_0.SetHelpNavigator(this.ProductKeyBox, System.Windows.Forms.HelpNavigator.Topic);
            this.ProductKeyBox.Location = new System.Drawing.Point(26, 70);
            this.ProductKeyBox.Name = "ProductKeyBox";
            this.helpProvider_0.SetShowHelp(this.ProductKeyBox, true);
            this.ProductKeyBox.Size = new System.Drawing.Size(198, 26);
            this.ProductKeyBox.TabIndex = 0;
            // 
            // StopAfterMinutes
            // 
            this.helpProvider_0.SetHelpKeyword(this.StopAfterMinutes, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.StopAfterMinutes, System.Windows.Forms.HelpNavigator.Topic);
            this.StopAfterMinutes.Location = new System.Drawing.Point(157, 41);
            this.StopAfterMinutes.Name = "StopAfterMinutes";
            this.helpProvider_0.SetShowHelp(this.StopAfterMinutes, true);
            this.StopAfterMinutes.Size = new System.Drawing.Size(78, 26);
            this.StopAfterMinutes.TabIndex = 1;
            // 
            // RestHealth
            // 
            this.helpProvider_0.SetHelpKeyword(this.RestHealth, "Limits.html");
            this.helpProvider_0.SetHelpNavigator(this.RestHealth, System.Windows.Forms.HelpNavigator.Topic);
            this.RestHealth.Location = new System.Drawing.Point(141, 35);
            this.RestHealth.MaxLength = 3;
            this.RestHealth.Name = "RestHealth";
            this.helpProvider_0.SetShowHelp(this.RestHealth, true);
            this.RestHealth.Size = new System.Drawing.Size(77, 26);
            this.RestHealth.TabIndex = 0;
            // 
            // RestMana
            // 
            this.helpProvider_0.SetHelpKeyword(this.RestMana, "Limits.html");
            this.helpProvider_0.SetHelpNavigator(this.RestMana, System.Windows.Forms.HelpNavigator.Topic);
            this.RestMana.Location = new System.Drawing.Point(406, 35);
            this.RestMana.MaxLength = 3;
            this.RestMana.Name = "RestMana";
            this.helpProvider_0.SetShowHelp(this.RestMana, true);
            this.RestMana.Size = new System.Drawing.Size(90, 26);
            this.RestMana.TabIndex = 1;
            // 
            // StopAfter
            // 
            this.StopAfter.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.StopAfter, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.StopAfter, System.Windows.Forms.HelpNavigator.Topic);
            this.StopAfter.Location = new System.Drawing.Point(21, 41);
            this.StopAfter.Name = "StopAfter";
            this.helpProvider_0.SetShowHelp(this.StopAfter, true);
            this.StopAfter.Size = new System.Drawing.Size(110, 24);
            this.StopAfter.TabIndex = 0;
            this.StopAfter.Text = "Stop after:";
            // 
            // ChatDelete
            // 
            this.helpProvider_0.SetHelpKeyword(this.ChatDelete, "Chat.html");
            this.helpProvider_0.SetHelpNavigator(this.ChatDelete, System.Windows.Forms.HelpNavigator.Topic);
            this.ChatDelete.Location = new System.Drawing.Point(558, 26);
            this.ChatDelete.Name = "ChatDelete";
            this.helpProvider_0.SetShowHelp(this.ChatDelete, true);
            this.ChatDelete.Size = new System.Drawing.Size(312, 32);
            this.ChatDelete.TabIndex = 2;
            this.ChatDelete.Text = "Delete prior chat log on start";
            // 
            // PlayWhisper
            // 
            this.helpProvider_0.SetHelpKeyword(this.PlayWhisper, "Chat.html");
            this.helpProvider_0.SetHelpNavigator(this.PlayWhisper, System.Windows.Forms.HelpNavigator.Topic);
            this.PlayWhisper.Location = new System.Drawing.Point(26, 35);
            this.PlayWhisper.Name = "PlayWhisper";
            this.helpProvider_0.SetShowHelp(this.PlayWhisper, true);
            this.PlayWhisper.Size = new System.Drawing.Size(230, 35);
            this.PlayWhisper.TabIndex = 0;
            this.PlayWhisper.Text = "Play sound on whisper";
            // 
            // AutoReply
            // 
            this.helpProvider_0.SetHelpKeyword(this.AutoReply, "Chat.html");
            this.helpProvider_0.SetHelpNavigator(this.AutoReply, System.Windows.Forms.HelpNavigator.Topic);
            this.AutoReply.Location = new System.Drawing.Point(26, 70);
            this.AutoReply.Name = "AutoReply";
            this.helpProvider_0.SetShowHelp(this.AutoReply, true);
            this.AutoReply.Size = new System.Drawing.Size(329, 28);
            this.AutoReply.TabIndex = 1;
            this.AutoReply.Text = "Auto-reply to GM:";
            // 
            // AutoReplyText
            // 
            this.helpProvider_0.SetHelpKeyword(this.AutoReplyText, "Chat.html");
            this.helpProvider_0.SetHelpNavigator(this.AutoReplyText, System.Windows.Forms.HelpNavigator.Topic);
            this.AutoReplyText.Location = new System.Drawing.Point(51, 105);
            this.AutoReplyText.Name = "AutoReplyText";
            this.helpProvider_0.SetShowHelp(this.AutoReplyText, true);
            this.AutoReplyText.Size = new System.Drawing.Size(435, 26);
            this.AutoReplyText.TabIndex = 2;
            // 
            // KeyDelay
            // 
            this.helpProvider_0.SetHelpKeyword(this.KeyDelay, "Keys.html");
            this.helpProvider_0.SetHelpNavigator(this.KeyDelay, System.Windows.Forms.HelpNavigator.Topic);
            this.KeyDelay.Location = new System.Drawing.Point(179, 35);
            this.KeyDelay.Name = "KeyDelay";
            this.helpProvider_0.SetShowHelp(this.KeyDelay, true);
            this.KeyDelay.Size = new System.Drawing.Size(64, 26);
            this.KeyDelay.TabIndex = 0;
            // 
            // BandageHealth
            // 
            this.helpProvider_0.SetHelpKeyword(this.BandageHealth, "Limits.html");
            this.helpProvider_0.SetHelpNavigator(this.BandageHealth, System.Windows.Forms.HelpNavigator.Topic);
            this.BandageHealth.Location = new System.Drawing.Point(64, 105);
            this.BandageHealth.MaxLength = 3;
            this.BandageHealth.Name = "BandageHealth";
            this.helpProvider_0.SetShowHelp(this.BandageHealth, true);
            this.BandageHealth.Size = new System.Drawing.Size(77, 26);
            this.BandageHealth.TabIndex = 2;
            // 
            // UseBandages
            // 
            this.helpProvider_0.SetHelpKeyword(this.UseBandages, "Limits.html");
            this.helpProvider_0.SetHelpNavigator(this.UseBandages, System.Windows.Forms.HelpNavigator.Topic);
            this.UseBandages.Location = new System.Drawing.Point(38, 70);
            this.UseBandages.Name = "UseBandages";
            this.helpProvider_0.SetShowHelp(this.UseBandages, true);
            this.UseBandages.Size = new System.Drawing.Size(180, 24);
            this.UseBandages.TabIndex = 1;
            this.UseBandages.Text = "Use bandages at:";
            // 
            // FastEat
            // 
            this.helpProvider_0.SetHelpKeyword(this.FastEat, "Limits.html");
            this.helpProvider_0.SetHelpNavigator(this.FastEat, System.Windows.Forms.HelpNavigator.Topic);
            this.FastEat.Location = new System.Drawing.Point(38, 35);
            this.FastEat.Name = "FastEat";
            this.helpProvider_0.SetShowHelp(this.FastEat, true);
            this.FastEat.Size = new System.Drawing.Size(180, 23);
            this.FastEat.TabIndex = 0;
            this.FastEat.Text = "Fast eat/drink";
            // 
            // UseClipboard
            // 
            this.UseClipboard.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.UseClipboard, "Keys.html");
            this.helpProvider_0.SetHelpNavigator(this.UseClipboard, System.Windows.Forms.HelpNavigator.Topic);
            this.UseClipboard.Location = new System.Drawing.Point(51, 82);
            this.UseClipboard.Name = "UseClipboard";
            this.helpProvider_0.SetShowHelp(this.UseClipboard, true);
            this.UseClipboard.Size = new System.Drawing.Size(235, 24);
            this.UseClipboard.TabIndex = 1;
            this.UseClipboard.Text = "Paste via Windows clipboard";
            // 
            // LoadKeymap
            // 
            this.LoadKeymap.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.LoadKeymap, "LoadingAndSaving.html");
            this.helpProvider_0.SetHelpNavigator(this.LoadKeymap, System.Windows.Forms.HelpNavigator.Topic);
            this.LoadKeymap.Location = new System.Drawing.Point(194, 133);
            this.LoadKeymap.Name = "LoadKeymap";
            this.helpProvider_0.SetShowHelp(this.LoadKeymap, true);
            this.LoadKeymap.Size = new System.Drawing.Size(220, 44);
            this.LoadKeymap.TabIndex = 1;
            this.LoadKeymap.Text = "Reload from disk";
            // 
            // PartyAttackDelay
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyAttackDelay, "Party.html#Follower");
            this.helpProvider_0.SetHelpNavigator(this.PartyAttackDelay, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyAttackDelay.Location = new System.Drawing.Point(149, 61);
            this.PartyAttackDelay.Name = "PartyAttackDelay";
            this.helpProvider_0.SetShowHelp(this.PartyAttackDelay, true);
            this.PartyAttackDelay.Size = new System.Drawing.Size(64, 26);
            this.PartyAttackDelay.TabIndex = 1;
            // 
            // PartyLeaderName
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyLeaderName, "Party.html#Follower");
            this.helpProvider_0.SetHelpNavigator(this.PartyLeaderName, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyLeaderName.Location = new System.Drawing.Point(149, 31);
            this.PartyLeaderName.Name = "PartyLeaderName";
            this.helpProvider_0.SetShowHelp(this.PartyLeaderName, true);
            this.PartyLeaderName.Size = new System.Drawing.Size(118, 26);
            this.PartyLeaderName.TabIndex = 0;
            // 
            // PartyLootPos
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyLootPos, "Party.html#Options");
            this.helpProvider_0.SetHelpNavigator(this.PartyLootPos, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyLootPos.Location = new System.Drawing.Point(139, 121);
            this.PartyLootPos.Name = "PartyLootPos";
            this.helpProvider_0.SetShowHelp(this.PartyLootPos, true);
            this.PartyLootPos.Size = new System.Drawing.Size(64, 26);
            this.PartyLootPos.TabIndex = 3;
            // 
            // PartyAdds
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyAdds, "Party.html#Options");
            this.helpProvider_0.SetHelpNavigator(this.PartyAdds, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyAdds.Location = new System.Drawing.Point(43, 20);
            this.PartyAdds.Name = "PartyAdds";
            this.helpProvider_0.SetShowHelp(this.PartyAdds, true);
            this.PartyAdds.Size = new System.Drawing.Size(160, 31);
            this.PartyAdds.TabIndex = 0;
            this.PartyAdds.Text = "Handle extras";
            // 
            // PartyLooters
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyLooters, "Party.html#Options");
            this.helpProvider_0.SetHelpNavigator(this.PartyLooters, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyLooters.Location = new System.Drawing.Point(139, 91);
            this.PartyLooters.Name = "PartyLooters";
            this.helpProvider_0.SetShowHelp(this.PartyLooters, true);
            this.PartyLooters.Size = new System.Drawing.Size(64, 26);
            this.PartyLooters.TabIndex = 2;
            // 
            // PartyFollower
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyFollower, "Party.html#Mode");
            this.helpProvider_0.SetHelpNavigator(this.PartyFollower, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyFollower.Location = new System.Drawing.Point(32, 80);
            this.PartyFollower.Name = "PartyFollower";
            this.helpProvider_0.SetShowHelp(this.PartyFollower, true);
            this.PartyFollower.Size = new System.Drawing.Size(171, 31);
            this.PartyFollower.TabIndex = 2;
            this.PartyFollower.Text = "Follower";
            // 
            // PartyLeader
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyLeader, "Party.html#Mode");
            this.helpProvider_0.SetHelpNavigator(this.PartyLeader, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyLeader.Location = new System.Drawing.Point(32, 51);
            this.PartyLeader.Name = "PartyLeader";
            this.helpProvider_0.SetShowHelp(this.PartyLeader, true);
            this.PartyLeader.Size = new System.Drawing.Size(171, 29);
            this.PartyLeader.TabIndex = 1;
            this.PartyLeader.Text = "Leader";
            // 
            // PartySolo
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartySolo, "Party.html#Mode");
            this.helpProvider_0.SetHelpNavigator(this.PartySolo, System.Windows.Forms.HelpNavigator.Topic);
            this.PartySolo.Location = new System.Drawing.Point(32, 20);
            this.PartySolo.Name = "PartySolo";
            this.helpProvider_0.SetShowHelp(this.PartySolo, true);
            this.PartySolo.Size = new System.Drawing.Size(171, 31);
            this.PartySolo.TabIndex = 0;
            this.PartySolo.Text = "Solo";
            // 
            // PartyMember1
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyMember1, "Party.html#Leader");
            this.helpProvider_0.SetHelpNavigator(this.PartyMember1, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyMember1.Location = new System.Drawing.Point(53, 61);
            this.PartyMember1.Name = "PartyMember1";
            this.helpProvider_0.SetShowHelp(this.PartyMember1, true);
            this.PartyMember1.Size = new System.Drawing.Size(267, 26);
            this.PartyMember1.TabIndex = 0;
            // 
            // PartyMember2
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyMember2, "Party.html#Leader");
            this.helpProvider_0.SetHelpNavigator(this.PartyMember2, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyMember2.Location = new System.Drawing.Point(53, 91);
            this.PartyMember2.Name = "PartyMember2";
            this.helpProvider_0.SetShowHelp(this.PartyMember2, true);
            this.PartyMember2.Size = new System.Drawing.Size(267, 26);
            this.PartyMember2.TabIndex = 2;
            // 
            // PartyMember3
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyMember3, "Party.html#Leader");
            this.helpProvider_0.SetHelpNavigator(this.PartyMember3, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyMember3.Location = new System.Drawing.Point(53, 121);
            this.PartyMember3.Name = "PartyMember3";
            this.helpProvider_0.SetShowHelp(this.PartyMember3, true);
            this.PartyMember3.Size = new System.Drawing.Size(267, 26);
            this.PartyMember3.TabIndex = 4;
            // 
            // PartyMember4
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyMember4, "Party.html#Leader");
            this.helpProvider_0.SetHelpNavigator(this.PartyMember4, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyMember4.Location = new System.Drawing.Point(53, 152);
            this.PartyMember4.Name = "PartyMember4";
            this.helpProvider_0.SetShowHelp(this.PartyMember4, true);
            this.PartyMember4.Size = new System.Drawing.Size(267, 26);
            this.PartyMember4.TabIndex = 6;
            // 
            // PartyBuff
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyBuff, "Party.html#Options");
            this.helpProvider_0.SetHelpNavigator(this.PartyBuff, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyBuff.Location = new System.Drawing.Point(43, 51);
            this.PartyBuff.Name = "PartyBuff";
            this.helpProvider_0.SetShowHelp(this.PartyBuff, true);
            this.PartyBuff.Size = new System.Drawing.Size(160, 29);
            this.PartyBuff.TabIndex = 1;
            this.PartyBuff.Text = "Buff others";
            // 
            // PartySlashFollow
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartySlashFollow, "Party.html#Follower");
            this.helpProvider_0.SetHelpNavigator(this.PartySlashFollow, System.Windows.Forms.HelpNavigator.Topic);
            this.PartySlashFollow.Location = new System.Drawing.Point(309, 31);
            this.PartySlashFollow.Name = "PartySlashFollow";
            this.helpProvider_0.SetShowHelp(this.PartySlashFollow, true);
            this.PartySlashFollow.Size = new System.Drawing.Size(160, 30);
            this.PartySlashFollow.TabIndex = 2;
            this.PartySlashFollow.Text = "Use /follow";
            // 
            // ListenPassword
            // 
            this.ListenPassword.Enabled = false;
            this.helpProvider_0.SetHelpKeyword(this.ListenPassword, "General.html#Remote");
            this.helpProvider_0.SetHelpNavigator(this.ListenPassword, System.Windows.Forms.HelpNavigator.Topic);
            this.ListenPassword.Location = new System.Drawing.Point(154, 70);
            this.ListenPassword.Name = "ListenPassword";
            this.helpProvider_0.SetShowHelp(this.ListenPassword, true);
            this.ListenPassword.Size = new System.Drawing.Size(128, 26);
            this.ListenPassword.TabIndex = 2;
            // 
            // ListenPort
            // 
            this.ListenPort.Enabled = false;
            this.helpProvider_0.SetHelpKeyword(this.ListenPort, "General.html#Remote");
            this.helpProvider_0.SetHelpNavigator(this.ListenPort, System.Windows.Forms.HelpNavigator.Topic);
            this.ListenPort.Location = new System.Drawing.Point(154, 39);
            this.ListenPort.Name = "ListenPort";
            this.helpProvider_0.SetShowHelp(this.ListenPort, true);
            this.ListenPort.Size = new System.Drawing.Size(64, 26);
            this.ListenPort.TabIndex = 1;
            // 
            // ListenEnabled
            // 
            this.helpProvider_0.SetHelpKeyword(this.ListenEnabled, "General.html#Remote");
            this.helpProvider_0.SetHelpNavigator(this.ListenEnabled, System.Windows.Forms.HelpNavigator.Topic);
            this.ListenEnabled.Location = new System.Drawing.Point(104, 124);
            this.ListenEnabled.Name = "ListenEnabled";
            this.helpProvider_0.SetShowHelp(this.ListenEnabled, true);
            this.ListenEnabled.Size = new System.Drawing.Size(160, 31);
            this.ListenEnabled.TabIndex = 0;
            this.ListenEnabled.Text = "Enabled";
            // 
            // StopWhenFull
            // 
            this.StopWhenFull.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.StopWhenFull, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.StopWhenFull, System.Windows.Forms.HelpNavigator.Topic);
            this.StopWhenFull.Location = new System.Drawing.Point(371, 41);
            this.StopWhenFull.Name = "StopWhenFull";
            this.helpProvider_0.SetShowHelp(this.StopWhenFull, true);
            this.StopWhenFull.Size = new System.Drawing.Size(266, 24);
            this.StopWhenFull.TabIndex = 2;
            this.StopWhenFull.Text = "Stop gliding when inventory is full";
            // 
            // PlaySay
            // 
            this.helpProvider_0.SetHelpKeyword(this.PlaySay, "Chat.html");
            this.helpProvider_0.SetHelpNavigator(this.PlaySay, System.Windows.Forms.HelpNavigator.Topic);
            this.PlaySay.Location = new System.Drawing.Point(384, 35);
            this.PlaySay.Name = "PlaySay";
            this.helpProvider_0.SetShowHelp(this.PlaySay, true);
            this.PlaySay.Size = new System.Drawing.Size(331, 35);
            this.PlaySay.TabIndex = 3;
            this.PlaySay.Text = "Play sound on say";
            // 
            // CombatLogFrame
            // 
            this.helpProvider_0.SetHelpKeyword(this.CombatLogFrame, "Chat.html");
            this.helpProvider_0.SetHelpNavigator(this.CombatLogFrame, System.Windows.Forms.HelpNavigator.Topic);
            this.CombatLogFrame.Location = new System.Drawing.Point(229, 69);
            this.CombatLogFrame.Name = "CombatLogFrame";
            this.helpProvider_0.SetShowHelp(this.CombatLogFrame, true);
            this.CombatLogFrame.Size = new System.Drawing.Size(209, 26);
            this.CombatLogFrame.TabIndex = 1;
            // 
            // ChatLogFrame
            // 
            this.helpProvider_0.SetHelpKeyword(this.ChatLogFrame, "Chat.html");
            this.helpProvider_0.SetHelpNavigator(this.ChatLogFrame, System.Windows.Forms.HelpNavigator.Topic);
            this.ChatLogFrame.Location = new System.Drawing.Point(229, 34);
            this.ChatLogFrame.Name = "ChatLogFrame";
            this.helpProvider_0.SetShowHelp(this.ChatLogFrame, true);
            this.ChatLogFrame.Size = new System.Drawing.Size(209, 26);
            this.ChatLogFrame.TabIndex = 0;
            // 
            // BackgroundEnable
            // 
            this.helpProvider_0.SetHelpKeyword(this.BackgroundEnable, "Background.html");
            this.helpProvider_0.SetHelpNavigator(this.BackgroundEnable, System.Windows.Forms.HelpNavigator.Topic);
            this.BackgroundEnable.Location = new System.Drawing.Point(21, 39);
            this.BackgroundEnable.Name = "BackgroundEnable";
            this.helpProvider_0.SetShowHelp(this.BackgroundEnable, true);
            this.BackgroundEnable.Size = new System.Drawing.Size(224, 34);
            this.BackgroundEnable.TabIndex = 0;
            this.BackgroundEnable.Text = "Background enable";
            // 
            // TabGeneral
            // 
            this.TabGeneral.Controls.Add(this.groupBox31);
            this.TabGeneral.Controls.Add(this.groupBox19);
            this.TabGeneral.Controls.Add(this.groupBox7);
            this.TabGeneral.Controls.Add(this.groupBox3);
            this.TabGeneral.Controls.Add(this.groupBox1);
            this.helpProvider_0.SetHelpNavigator(this.TabGeneral, System.Windows.Forms.HelpNavigator.TopicId);
            this.helpProvider_0.SetHelpString(this.TabGeneral, "General.html");
            this.TabGeneral.Location = new System.Drawing.Point(4, 29);
            this.TabGeneral.Name = "TabGeneral";
            this.helpProvider_0.SetShowHelp(this.TabGeneral, true);
            this.TabGeneral.Size = new System.Drawing.Size(1022, 389);
            this.TabGeneral.TabIndex = 0;
            this.TabGeneral.Text = "General";
            this.TabGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox31
            // 
            this.groupBox31.Controls.Add(this.label26);
            this.groupBox31.Controls.Add(this.AutoLogCharacter);
            this.groupBox31.Controls.Add(this.ButtonViewCharacters);
            this.groupBox31.Controls.Add(this.AccountCreate);
            this.groupBox31.Location = new System.Drawing.Point(427, 200);
            this.groupBox31.Name = "groupBox31";
            this.groupBox31.Size = new System.Drawing.Size(557, 164);
            this.groupBox31.TabIndex = 10;
            this.groupBox31.TabStop = false;
            this.groupBox31.Text = "Auto Login";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(50, 48);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(161, 20);
            this.label26.TabIndex = 4;
            this.label26.Text = "Auto Login character:";
            // 
            // AutoLogCharacter
            // 
            this.AutoLogCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AutoLogCharacter.FormattingEnabled = true;
            this.AutoLogCharacter.Location = new System.Drawing.Point(54, 76);
            this.AutoLogCharacter.Name = "AutoLogCharacter";
            this.AutoLogCharacter.Size = new System.Drawing.Size(236, 28);
            this.AutoLogCharacter.TabIndex = 3;
            // 
            // ButtonViewCharacters
            // 
            this.ButtonViewCharacters.Location = new System.Drawing.Point(350, 102);
            this.ButtonViewCharacters.Name = "ButtonViewCharacters";
            this.ButtonViewCharacters.Size = new System.Drawing.Size(186, 37);
            this.ButtonViewCharacters.TabIndex = 1;
            this.ButtonViewCharacters.Text = "View Characters";
            this.ButtonViewCharacters.UseVisualStyleBackColor = true;
            // 
            // AccountCreate
            // 
            this.AccountCreate.Location = new System.Drawing.Point(350, 39);
            this.AccountCreate.Name = "AccountCreate";
            this.AccountCreate.Size = new System.Drawing.Size(186, 37);
            this.AccountCreate.TabIndex = 0;
            this.AccountCreate.Text = "Create Character";
            this.AccountCreate.UseVisualStyleBackColor = true;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.ListenPassword);
            this.groupBox19.Controls.Add(this.ListenPort);
            this.groupBox19.Controls.Add(this.ListenEnabled);
            this.groupBox19.Controls.Add(this.label42);
            this.groupBox19.Controls.Add(this.label41);
            this.groupBox19.Location = new System.Drawing.Point(682, 12);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(305, 187);
            this.groupBox19.TabIndex = 9;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Remote Control";
            // 
            // label42
            // 
            this.label42.Location = new System.Drawing.Point(26, 39);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(116, 21);
            this.label42.TabIndex = 1;
            this.label42.Text = "Port:";
            this.label42.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label41
            // 
            this.label41.Location = new System.Drawing.Point(26, 70);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(116, 21);
            this.label41.TabIndex = 0;
            this.label41.Text = "Password:";
            this.label41.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.WowVersionLabel);
            this.groupBox7.Controls.Add(this.GliderVersionLabel);
            this.groupBox7.Controls.Add(this.linkLabel1);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Location = new System.Drawing.Point(26, 200);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(393, 164);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Information";
            // 
            // WowVersionLabel
            // 
            this.WowVersionLabel.AutoSize = true;
            this.WowVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WowVersionLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.WowVersionLabel.Location = new System.Drawing.Point(147, 80);
            this.WowVersionLabel.Name = "WowVersionLabel";
            this.WowVersionLabel.Size = new System.Drawing.Size(29, 20);
            this.WowVersionLabel.TabIndex = 4;
            this.WowVersionLabel.Text = "??";
            // 
            // GliderVersionLabel
            // 
            this.GliderVersionLabel.AutoSize = true;
            this.GliderVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GliderVersionLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.GliderVersionLabel.Location = new System.Drawing.Point(147, 39);
            this.GliderVersionLabel.Name = "GliderVersionLabel";
            this.GliderVersionLabel.Size = new System.Drawing.Size(29, 20);
            this.GliderVersionLabel.TabIndex = 3;
            this.GliderVersionLabel.Text = "??";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Location = new System.Drawing.Point(11, 136);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(347, 22);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://www.mmoglider.com";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 20);
            this.label10.TabIndex = 1;
            this.label10.Text = "WoW version:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "Glider version:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ProductKeyBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(427, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(245, 187);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Registration";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(26, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Product key:";
            // 
            // TabLimits
            // 
            this.TabLimits.Controls.Add(this.groupBox32);
            this.TabLimits.Controls.Add(this.groupBox22);
            this.TabLimits.Controls.Add(this.groupBox23);
            this.TabLimits.Controls.Add(this.groupBox13);
            this.TabLimits.Controls.Add(this.groupBox5);
            this.helpProvider_0.SetHelpNavigator(this.TabLimits, System.Windows.Forms.HelpNavigator.TopicId);
            this.helpProvider_0.SetHelpString(this.TabLimits, "Limits.html");
            this.TabLimits.Location = new System.Drawing.Point(4, 29);
            this.TabLimits.Name = "TabLimits";
            this.helpProvider_0.SetShowHelp(this.TabLimits, true);
            this.TabLimits.Size = new System.Drawing.Size(1022, 389);
            this.TabLimits.TabIndex = 3;
            this.TabLimits.Text = "Limits";
            this.TabLimits.UseVisualStyleBackColor = true;
            // 
            // groupBox32
            // 
            this.groupBox32.Controls.Add(this.AmmoAmount);
            this.groupBox32.Controls.Add(this.label60);
            this.groupBox32.Controls.Add(this.WaterAmount);
            this.groupBox32.Controls.Add(this.FoodAmount);
            this.groupBox32.Controls.Add(this.label3);
            this.groupBox32.Controls.Add(this.label2);
            this.groupBox32.Location = new System.Drawing.Point(574, 102);
            this.groupBox32.Name = "groupBox32";
            this.groupBox32.Size = new System.Drawing.Size(410, 259);
            this.groupBox32.TabIndex = 16;
            this.groupBox32.TabStop = false;
            this.groupBox32.Text = "Vendoring";
            // 
            // AmmoAmount
            // 
            this.helpProvider_0.SetHelpKeyword(this.AmmoAmount, "Limits.html");
            this.helpProvider_0.SetHelpNavigator(this.AmmoAmount, System.Windows.Forms.HelpNavigator.Topic);
            this.AmmoAmount.Location = new System.Drawing.Point(170, 148);
            this.AmmoAmount.Name = "AmmoAmount";
            this.helpProvider_0.SetShowHelp(this.AmmoAmount, true);
            this.AmmoAmount.Size = new System.Drawing.Size(96, 26);
            this.AmmoAmount.TabIndex = 18;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(37, 152);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(117, 20);
            this.label60.TabIndex = 17;
            this.label60.Text = "Ammo amount:";
            this.label60.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // WaterAmount
            // 
            this.helpProvider_0.SetHelpKeyword(this.WaterAmount, "Limits.html");
            this.helpProvider_0.SetHelpNavigator(this.WaterAmount, System.Windows.Forms.HelpNavigator.Topic);
            this.WaterAmount.Location = new System.Drawing.Point(170, 77);
            this.WaterAmount.Name = "WaterAmount";
            this.helpProvider_0.SetShowHelp(this.WaterAmount, true);
            this.WaterAmount.Size = new System.Drawing.Size(96, 26);
            this.WaterAmount.TabIndex = 16;
            // 
            // FoodAmount
            // 
            this.helpProvider_0.SetHelpKeyword(this.FoodAmount, "Limits.html");
            this.helpProvider_0.SetHelpNavigator(this.FoodAmount, System.Windows.Forms.HelpNavigator.Topic);
            this.FoodAmount.Location = new System.Drawing.Point(170, 44);
            this.FoodAmount.Name = "FoodAmount";
            this.helpProvider_0.SetShowHelp(this.FoodAmount, true);
            this.FoodAmount.Size = new System.Drawing.Size(96, 26);
            this.FoodAmount.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Water amount:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Food amount:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.MaxResurrect);
            this.groupBox22.Controls.Add(this.ResLabel);
            this.groupBox22.Controls.Add(this.Resurrect);
            this.groupBox22.Location = new System.Drawing.Point(275, 101);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(290, 260);
            this.groupBox22.TabIndex = 15;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Resurrection";
            // 
            // MaxResurrect
            // 
            this.helpProvider_0.SetHelpKeyword(this.MaxResurrect, "Limits.html");
            this.helpProvider_0.SetHelpNavigator(this.MaxResurrect, System.Windows.Forms.HelpNavigator.Topic);
            this.MaxResurrect.Location = new System.Drawing.Point(14, 177);
            this.MaxResurrect.Name = "MaxResurrect";
            this.helpProvider_0.SetShowHelp(this.MaxResurrect, true);
            this.MaxResurrect.Size = new System.Drawing.Size(96, 26);
            this.MaxResurrect.TabIndex = 14;
            // 
            // ResLabel
            // 
            this.ResLabel.AutoSize = true;
            this.ResLabel.Location = new System.Drawing.Point(10, 153);
            this.ResLabel.Name = "ResLabel";
            this.ResLabel.Size = new System.Drawing.Size(155, 20);
            this.ResLabel.TabIndex = 13;
            this.ResLabel.Text = "Stop after this many:";
            // 
            // Resurrect
            // 
            this.helpProvider_0.SetHelpKeyword(this.Resurrect, "Limits.html");
            this.helpProvider_0.SetHelpNavigator(this.Resurrect, System.Windows.Forms.HelpNavigator.Topic);
            this.Resurrect.Location = new System.Drawing.Point(13, 50);
            this.Resurrect.Name = "Resurrect";
            this.helpProvider_0.SetShowHelp(this.Resurrect, true);
            this.Resurrect.Size = new System.Drawing.Size(219, 79);
            this.Resurrect.TabIndex = 9;
            this.Resurrect.Text = "Auto resurrect if profile has ghost waypoints";
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.DebuffsKnown);
            this.groupBox23.Controls.Add(this.EditDebuffs);
            this.groupBox23.Location = new System.Drawing.Point(573, 12);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(414, 82);
            this.groupBox23.TabIndex = 14;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Debuff Removal";
            // 
            // DebuffsKnown
            // 
            this.DebuffsKnown.AutoSize = true;
            this.DebuffsKnown.ForeColor = System.Drawing.SystemColors.Highlight;
            this.DebuffsKnown.Location = new System.Drawing.Point(24, 34);
            this.DebuffsKnown.Name = "DebuffsKnown";
            this.DebuffsKnown.Size = new System.Drawing.Size(63, 20);
            this.DebuffsKnown.TabIndex = 1;
            this.DebuffsKnown.Text = "??????";
            // 
            // EditDebuffs
            // 
            this.EditDebuffs.Location = new System.Drawing.Point(203, 26);
            this.EditDebuffs.Name = "EditDebuffs";
            this.EditDebuffs.Size = new System.Drawing.Size(122, 32);
            this.EditDebuffs.TabIndex = 0;
            this.EditDebuffs.Text = "Manage";
            this.EditDebuffs.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label17);
            this.groupBox13.Controls.Add(this.BandageHealth);
            this.groupBox13.Controls.Add(this.UseBandages);
            this.groupBox13.Controls.Add(this.FastEat);
            this.groupBox13.Location = new System.Drawing.Point(11, 101);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(255, 260);
            this.groupBox13.TabIndex = 11;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Rest Options";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(154, 105);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 24);
            this.label17.TabIndex = 7;
            this.label17.Text = "health";
            this.label17.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.RestMana);
            this.groupBox5.Controls.Add(this.RestHealth);
            this.groupBox5.Location = new System.Drawing.Point(11, 10);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(554, 82);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Rest Percentages";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(274, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 23);
            this.label6.TabIndex = 3;
            this.label6.Text = "Mana:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(11, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Health:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // TabDetection
            // 
            this.TabDetection.Controls.Add(this.groupBox21);
            this.TabDetection.Controls.Add(this.groupBox9);
            this.TabDetection.Controls.Add(this.groupBox14);
            this.helpProvider_0.SetHelpNavigator(this.TabDetection, System.Windows.Forms.HelpNavigator.TopicId);
            this.helpProvider_0.SetHelpString(this.TabDetection, "Detection.html");
            this.TabDetection.Location = new System.Drawing.Point(4, 29);
            this.TabDetection.Name = "TabDetection";
            this.helpProvider_0.SetShowHelp(this.TabDetection, true);
            this.TabDetection.Size = new System.Drawing.Size(1022, 389);
            this.TabDetection.TabIndex = 6;
            this.TabDetection.Text = "Detection";
            this.TabDetection.UseVisualStyleBackColor = true;
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.StopOnVanish);
            this.groupBox21.Controls.Add(this.TeleportLogout);
            this.groupBox21.Controls.Add(this.TeleportStop);
            this.groupBox21.Location = new System.Drawing.Point(568, 12);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(419, 343);
            this.groupBox21.TabIndex = 9;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Other";
            // 
            // StopOnVanish
            // 
            this.StopOnVanish.AutoSize = true;
            this.StopOnVanish.Location = new System.Drawing.Point(26, 112);
            this.StopOnVanish.Name = "StopOnVanish";
            this.StopOnVanish.Size = new System.Drawing.Size(223, 24);
            this.StopOnVanish.TabIndex = 4;
            this.StopOnVanish.Text = "Stop on vanishing monster";
            // 
            // TeleportLogout
            // 
            this.TeleportLogout.AutoSize = true;
            this.TeleportLogout.Enabled = false;
            this.TeleportLogout.Location = new System.Drawing.Point(26, 82);
            this.TeleportLogout.Name = "TeleportLogout";
            this.TeleportLogout.Size = new System.Drawing.Size(198, 24);
            this.TeleportLogout.TabIndex = 3;
            this.TeleportLogout.Text = "Also log out on teleport";
            // 
            // TeleportStop
            // 
            this.TeleportStop.AutoSize = true;
            this.TeleportStop.Location = new System.Drawing.Point(26, 52);
            this.TeleportStop.Name = "TeleportStop";
            this.TeleportStop.Size = new System.Drawing.Size(236, 24);
            this.TeleportStop.TabIndex = 2;
            this.TeleportStop.Text = "Stop on unexpected teleport";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.Strafe);
            this.groupBox9.Controls.Add(this.JumpMore);
            this.groupBox9.Location = new System.Drawing.Point(11, 200);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(547, 155);
            this.groupBox9.TabIndex = 6;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Movement";
            // 
            // Strafe
            // 
            this.Strafe.AutoSize = true;
            this.Strafe.Location = new System.Drawing.Point(21, 61);
            this.Strafe.Name = "Strafe";
            this.Strafe.Size = new System.Drawing.Size(79, 24);
            this.Strafe.TabIndex = 1;
            this.Strafe.Text = "Strafe";
            // 
            // JumpMore
            // 
            this.JumpMore.AutoSize = true;
            this.JumpMore.Location = new System.Drawing.Point(21, 31);
            this.JumpMore.Name = "JumpMore";
            this.JumpMore.Size = new System.Drawing.Size(114, 24);
            this.JumpMore.TabIndex = 0;
            this.JumpMore.Text = "Jump more";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.label50);
            this.groupBox14.Controls.Add(this.MaxPopups);
            this.groupBox14.Controls.Add(this.label49);
            this.groupBox14.Controls.Add(this.AvoidOtherFaction);
            this.groupBox14.Controls.Add(this.AvoidSameFaction);
            this.groupBox14.Controls.Add(this.label21);
            this.groupBox14.Controls.Add(this.FriendLogout);
            this.groupBox14.Controls.Add(this.label20);
            this.groupBox14.Controls.Add(this.label19);
            this.groupBox14.Controls.Add(this.FriendAlert);
            this.groupBox14.Controls.Add(this.label18);
            this.groupBox14.Location = new System.Drawing.Point(11, 12);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(551, 179);
            this.groupBox14.TabIndex = 4;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Followers";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(314, 99);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(69, 20);
            this.label50.TabIndex = 9;
            this.label50.Text = "windows";
            // 
            // MaxPopups
            // 
            this.MaxPopups.Location = new System.Drawing.Point(248, 95);
            this.MaxPopups.Name = "MaxPopups";
            this.MaxPopups.Size = new System.Drawing.Size(51, 26);
            this.MaxPopups.TabIndex = 8;
            // 
            // label49
            // 
            this.label49.Location = new System.Drawing.Point(144, 99);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(96, 22);
            this.label49.TabIndex = 7;
            this.label49.Text = "Popups:";
            this.label49.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // AvoidOtherFaction
            // 
            this.AvoidOtherFaction.Location = new System.Drawing.Point(323, 148);
            this.AvoidOtherFaction.Name = "AvoidOtherFaction";
            this.AvoidOtherFaction.Size = new System.Drawing.Size(224, 24);
            this.AvoidOtherFaction.TabIndex = 3;
            this.AvoidOtherFaction.Text = "Avoid other faction";
            // 
            // AvoidSameFaction
            // 
            this.AvoidSameFaction.Location = new System.Drawing.Point(48, 148);
            this.AvoidSameFaction.Name = "AvoidSameFaction";
            this.AvoidSameFaction.Size = new System.Drawing.Size(224, 24);
            this.AvoidSameFaction.TabIndex = 2;
            this.AvoidSameFaction.Text = "Avoid same faction";
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(312, 64);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(77, 24);
            this.label21.TabIndex = 6;
            this.label21.Text = "minutes";
            // 
            // FriendLogout
            // 
            this.FriendLogout.Location = new System.Drawing.Point(248, 60);
            this.FriendLogout.Name = "FriendLogout";
            this.FriendLogout.Size = new System.Drawing.Size(51, 26);
            this.FriendLogout.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(64, 64);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(176, 24);
            this.label20.TabIndex = 4;
            this.label20.Text = "Log out after:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(312, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 24);
            this.label19.TabIndex = 3;
            this.label19.Text = "minutes";
            // 
            // FriendAlert
            // 
            this.FriendAlert.Location = new System.Drawing.Point(248, 23);
            this.FriendAlert.Name = "FriendAlert";
            this.FriendAlert.Size = new System.Drawing.Size(51, 26);
            this.FriendAlert.TabIndex = 0;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(64, 23);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(176, 24);
            this.label18.TabIndex = 0;
            this.label18.Text = "Alert after:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TabKeys
            // 
            this.TabKeys.Controls.Add(this.groupBox27);
            this.TabKeys.Controls.Add(this.groupBox15);
            this.TabKeys.Controls.Add(this.groupBox8);
            this.helpProvider_0.SetHelpNavigator(this.TabKeys, System.Windows.Forms.HelpNavigator.TopicId);
            this.helpProvider_0.SetHelpString(this.TabKeys, "Keys.html");
            this.TabKeys.Location = new System.Drawing.Point(4, 29);
            this.TabKeys.Name = "TabKeys";
            this.helpProvider_0.SetShowHelp(this.TabKeys, true);
            this.TabKeys.Size = new System.Drawing.Size(1022, 389);
            this.TabKeys.TabIndex = 5;
            this.TabKeys.Text = "Keys";
            this.TabKeys.UseVisualStyleBackColor = true;
            // 
            // groupBox27
            // 
            this.groupBox27.Controls.Add(this.MouseSpin);
            this.groupBox27.Controls.Add(this.label48);
            this.groupBox27.Controls.Add(this.PawSpeed);
            this.groupBox27.Controls.Add(this.label40);
            this.groupBox27.Location = new System.Drawing.Point(536, 140);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(451, 192);
            this.groupBox27.TabIndex = 3;
            this.groupBox27.TabStop = false;
            this.groupBox27.Text = "Mouse";
            // 
            // MouseSpin
            // 
            this.MouseSpin.AutoSize = true;
            this.MouseSpin.Location = new System.Drawing.Point(29, 113);
            this.MouseSpin.Name = "MouseSpin";
            this.MouseSpin.Size = new System.Drawing.Size(151, 24);
            this.MouseSpin.TabIndex = 4;
            this.MouseSpin.Text = "Spin with mouse";
            this.MouseSpin.UseVisualStyleBackColor = true;
            // 
            // label48
            // 
            this.label48.Location = new System.Drawing.Point(264, 54);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(115, 23);
            this.label48.TabIndex = 3;
            this.label48.Text = "milliseconds";
            // 
            // PawSpeed
            // 
            this.helpProvider_0.SetHelpKeyword(this.PawSpeed, "Keys.html");
            this.helpProvider_0.SetHelpNavigator(this.PawSpeed, System.Windows.Forms.HelpNavigator.Topic);
            this.PawSpeed.Location = new System.Drawing.Point(192, 51);
            this.PawSpeed.Name = "PawSpeed";
            this.helpProvider_0.SetShowHelp(this.PawSpeed, true);
            this.PawSpeed.Size = new System.Drawing.Size(64, 26);
            this.PawSpeed.TabIndex = 1;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(26, 54);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(132, 20);
            this.label40.TabIndex = 0;
            this.label40.Text = "Paw speed delay:";
            this.label40.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.EditKeymap);
            this.groupBox15.Controls.Add(this.KeyEditClass);
            this.groupBox15.Controls.Add(this.label61);
            this.groupBox15.Controls.Add(this.LoadKeymap);
            this.groupBox15.Location = new System.Drawing.Point(13, 140);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(515, 192);
            this.groupBox15.TabIndex = 2;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Key Mapping";
            // 
            // EditKeymap
            // 
            this.EditKeymap.AutoSize = true;
            this.EditKeymap.Enabled = false;
            this.helpProvider_0.SetHelpKeyword(this.EditKeymap, "LoadingAndSaving.html");
            this.helpProvider_0.SetHelpNavigator(this.EditKeymap, System.Windows.Forms.HelpNavigator.Topic);
            this.EditKeymap.Location = new System.Drawing.Point(306, 58);
            this.EditKeymap.Name = "EditKeymap";
            this.helpProvider_0.SetShowHelp(this.EditKeymap, true);
            this.EditKeymap.Size = new System.Drawing.Size(132, 44);
            this.EditKeymap.TabIndex = 4;
            this.EditKeymap.Text = "Edit";
            // 
            // KeyEditClass
            // 
            this.KeyEditClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KeyEditClass.FormattingEnabled = true;
            this.KeyEditClass.Location = new System.Drawing.Point(29, 63);
            this.KeyEditClass.Name = "KeyEditClass";
            this.KeyEditClass.Size = new System.Drawing.Size(253, 28);
            this.KeyEditClass.Sorted = true;
            this.KeyEditClass.TabIndex = 3;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(24, 39);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(52, 20);
            this.label61.TabIndex = 2;
            this.label61.Text = "Class:";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label58);
            this.groupBox8.Controls.Add(this.label59);
            this.groupBox8.Controls.Add(this.SpellLeadDelay);
            this.groupBox8.Controls.Add(this.UseHook);
            this.groupBox8.Controls.Add(this.UseClipboard);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.KeyDelay);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Location = new System.Drawing.Point(13, 12);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(974, 117);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Timing";
            // 
            // label58
            // 
            this.label58.Location = new System.Drawing.Point(406, 35);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(252, 23);
            this.label58.TabIndex = 15;
            this.label58.Text = "Spell lead delay:";
            this.label58.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label59
            // 
            this.label59.Location = new System.Drawing.Point(778, 35);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(49, 23);
            this.label59.TabIndex = 16;
            this.label59.Text = "ms";
            // 
            // SpellLeadDelay
            // 
            this.helpProvider_0.SetHelpKeyword(this.SpellLeadDelay, "Miscellaneous.html");
            this.helpProvider_0.SetHelpNavigator(this.SpellLeadDelay, System.Windows.Forms.HelpNavigator.Topic);
            this.SpellLeadDelay.Location = new System.Drawing.Point(666, 31);
            this.SpellLeadDelay.Name = "SpellLeadDelay";
            this.helpProvider_0.SetShowHelp(this.SpellLeadDelay, true);
            this.SpellLeadDelay.Size = new System.Drawing.Size(104, 26);
            this.SpellLeadDelay.TabIndex = 17;
            // 
            // UseHook
            // 
            this.UseHook.AutoSize = true;
            this.UseHook.Location = new System.Drawing.Point(554, 82);
            this.UseHook.Name = "UseHook";
            this.UseHook.Size = new System.Drawing.Size(185, 24);
            this.UseHook.TabIndex = 2;
            this.UseHook.Text = "Install keyboard hook";
            this.UseHook.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(256, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(115, 23);
            this.label12.TabIndex = 2;
            this.label12.Text = "milliseconds";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(13, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(153, 23);
            this.label11.TabIndex = 0;
            this.label11.Text = "Keystroke delay:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TabDistances
            // 
            this.TabDistances.Controls.Add(this.groupBox30);
            this.TabDistances.Controls.Add(this.groupBox12);
            this.TabDistances.Controls.Add(this.groupBox18);
            this.TabDistances.Controls.Add(this.groupBox16);
            this.helpProvider_0.SetHelpNavigator(this.TabDistances, System.Windows.Forms.HelpNavigator.TopicId);
            this.helpProvider_0.SetHelpString(this.TabDistances, "Distances.html");
            this.TabDistances.Location = new System.Drawing.Point(4, 29);
            this.TabDistances.Name = "TabDistances";
            this.helpProvider_0.SetShowHelp(this.TabDistances, true);
            this.TabDistances.Size = new System.Drawing.Size(1022, 389);
            this.TabDistances.TabIndex = 1;
            this.TabDistances.Text = "Distances";
            this.TabDistances.UseVisualStyleBackColor = true;
            // 
            // groupBox30
            // 
            this.groupBox30.Controls.Add(this.label54);
            this.groupBox30.Controls.Add(this.LootSafeDistance);
            this.groupBox30.Controls.Add(this.label51);
            this.groupBox30.Controls.Add(this.LootCheckHostiles);
            this.groupBox30.Location = new System.Drawing.Point(480, 193);
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.Size = new System.Drawing.Size(491, 150);
            this.groupBox30.TabIndex = 18;
            this.groupBox30.TabStop = false;
            this.groupBox30.Text = "Hostile Monsters";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(250, 91);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(47, 20);
            this.label54.TabIndex = 13;
            this.label54.Text = "yards";
            this.label54.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // LootSafeDistance
            // 
            this.helpProvider_0.SetHelpKeyword(this.LootSafeDistance, "Distances.html");
            this.helpProvider_0.SetHelpNavigator(this.LootSafeDistance, System.Windows.Forms.HelpNavigator.Topic);
            this.LootSafeDistance.Location = new System.Drawing.Point(178, 88);
            this.LootSafeDistance.MaxLength = 6;
            this.LootSafeDistance.Name = "LootSafeDistance";
            this.helpProvider_0.SetShowHelp(this.LootSafeDistance, true);
            this.LootSafeDistance.Size = new System.Drawing.Size(64, 26);
            this.LootSafeDistance.TabIndex = 12;
            // 
            // label51
            // 
            this.label51.Location = new System.Drawing.Point(21, 88);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(149, 29);
            this.label51.TabIndex = 10;
            this.label51.Text = "Safe distance:";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LootCheckHostiles
            // 
            this.LootCheckHostiles.AutoSize = true;
            this.helpProvider_0.SetHelpKeyword(this.LootCheckHostiles, "Distances.html");
            this.helpProvider_0.SetHelpNavigator(this.LootCheckHostiles, System.Windows.Forms.HelpNavigator.Topic);
            this.LootCheckHostiles.Location = new System.Drawing.Point(46, 42);
            this.LootCheckHostiles.Name = "LootCheckHostiles";
            this.helpProvider_0.SetShowHelp(this.LootCheckHostiles, true);
            this.LootCheckHostiles.Size = new System.Drawing.Size(308, 24);
            this.LootCheckHostiles.TabIndex = 0;
            this.LootCheckHostiles.Text = "Avoid hostiles when looting and resting";
            this.LootCheckHostiles.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.PickupJunk);
            this.groupBox12.Controls.Add(this.label13);
            this.groupBox12.Controls.Add(this.label7);
            this.groupBox12.Controls.Add(this.ExtraPull);
            this.groupBox12.Controls.Add(this.label16);
            this.groupBox12.Controls.Add(this.HarvestRange);
            this.groupBox12.Controls.Add(this.label15);
            this.groupBox12.Location = new System.Drawing.Point(16, 25);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(456, 161);
            this.groupBox12.TabIndex = 17;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Wandering";
            // 
            // PickupJunk
            // 
            this.helpProvider_0.SetHelpKeyword(this.PickupJunk, "Distances.html");
            this.helpProvider_0.SetHelpNavigator(this.PickupJunk, System.Windows.Forms.HelpNavigator.Topic);
            this.PickupJunk.Location = new System.Drawing.Point(93, 102);
            this.PickupJunk.Name = "PickupJunk";
            this.helpProvider_0.SetShowHelp(this.PickupJunk, true);
            this.PickupJunk.Size = new System.Drawing.Size(232, 35);
            this.PickupJunk.TabIndex = 2;
            this.PickupJunk.Text = "Pickup junk";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(267, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 24);
            this.label13.TabIndex = 16;
            this.label13.Text = "yards";
            this.label13.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(21, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 23);
            this.label7.TabIndex = 15;
            this.label7.Text = "Walk-to-pull:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ExtraPull
            // 
            this.helpProvider_0.SetHelpKeyword(this.ExtraPull, "Distances.html");
            this.helpProvider_0.SetHelpNavigator(this.ExtraPull, System.Windows.Forms.HelpNavigator.Topic);
            this.ExtraPull.Location = new System.Drawing.Point(182, 32);
            this.ExtraPull.Name = "ExtraPull";
            this.helpProvider_0.SetShowHelp(this.ExtraPull, true);
            this.ExtraPull.Size = new System.Drawing.Size(77, 26);
            this.ExtraPull.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(267, 34);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 23);
            this.label16.TabIndex = 5;
            this.label16.Text = "yards";
            this.label16.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // HarvestRange
            // 
            this.helpProvider_0.SetHelpKeyword(this.HarvestRange, "Distances.html");
            this.helpProvider_0.SetHelpNavigator(this.HarvestRange, System.Windows.Forms.HelpNavigator.Topic);
            this.HarvestRange.Location = new System.Drawing.Point(182, 67);
            this.HarvestRange.MaxLength = 3;
            this.HarvestRange.Name = "HarvestRange";
            this.helpProvider_0.SetShowHelp(this.HarvestRange, true);
            this.HarvestRange.Size = new System.Drawing.Size(77, 26);
            this.HarvestRange.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(26, 72);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(148, 22);
            this.label15.TabIndex = 3;
            this.label15.Text = "Harvest range:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.label57);
            this.groupBox18.Controls.Add(this.label56);
            this.groupBox18.Controls.Add(this.label55);
            this.groupBox18.Controls.Add(this.PartyFollowerStop);
            this.groupBox18.Controls.Add(this.PartyFollowerStart);
            this.groupBox18.Controls.Add(this.PartyLeaderWait);
            this.groupBox18.Controls.Add(this.label37);
            this.groupBox18.Controls.Add(this.label36);
            this.groupBox18.Controls.Add(this.label35);
            this.groupBox18.Location = new System.Drawing.Point(480, 25);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(491, 161);
            this.groupBox18.TabIndex = 16;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Party";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(283, 108);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(47, 20);
            this.label57.TabIndex = 16;
            this.label57.Text = "yards";
            this.label57.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(283, 69);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(47, 20);
            this.label56.TabIndex = 15;
            this.label56.Text = "yards";
            this.label56.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(283, 34);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(47, 20);
            this.label55.TabIndex = 14;
            this.label55.Text = "yards";
            this.label55.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // PartyFollowerStop
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyFollowerStop, "Party.html#Limits");
            this.helpProvider_0.SetHelpNavigator(this.PartyFollowerStop, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyFollowerStop.Location = new System.Drawing.Point(198, 105);
            this.PartyFollowerStop.MaxLength = 3;
            this.PartyFollowerStop.Name = "PartyFollowerStop";
            this.helpProvider_0.SetShowHelp(this.PartyFollowerStop, true);
            this.PartyFollowerStop.Size = new System.Drawing.Size(77, 26);
            this.PartyFollowerStop.TabIndex = 2;
            // 
            // PartyFollowerStart
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyFollowerStart, "Party.html#Limits");
            this.helpProvider_0.SetHelpNavigator(this.PartyFollowerStart, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyFollowerStart.Location = new System.Drawing.Point(198, 66);
            this.PartyFollowerStart.MaxLength = 3;
            this.PartyFollowerStart.Name = "PartyFollowerStart";
            this.helpProvider_0.SetShowHelp(this.PartyFollowerStart, true);
            this.PartyFollowerStart.Size = new System.Drawing.Size(77, 26);
            this.PartyFollowerStart.TabIndex = 1;
            // 
            // PartyLeaderWait
            // 
            this.helpProvider_0.SetHelpKeyword(this.PartyLeaderWait, "Party.html#Limits");
            this.helpProvider_0.SetHelpNavigator(this.PartyLeaderWait, System.Windows.Forms.HelpNavigator.Topic);
            this.PartyLeaderWait.Location = new System.Drawing.Point(198, 31);
            this.PartyLeaderWait.MaxLength = 3;
            this.PartyLeaderWait.Name = "PartyLeaderWait";
            this.helpProvider_0.SetShowHelp(this.PartyLeaderWait, true);
            this.PartyLeaderWait.Size = new System.Drawing.Size(77, 26);
            this.PartyLeaderWait.TabIndex = 0;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(43, 105);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(125, 20);
            this.label37.TabIndex = 2;
            this.label37.Text = "Follower walk to:";
            this.label37.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(21, 70);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(143, 20);
            this.label36.TabIndex = 1;
            this.label36.Text = "Follower walk start:";
            this.label36.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(77, 34);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(95, 20);
            this.label35.TabIndex = 0;
            this.label35.Text = "Leader wait:";
            this.label35.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.label22);
            this.groupBox16.Controls.Add(this.RangedDistance);
            this.groupBox16.Controls.Add(this.label23);
            this.groupBox16.Controls.Add(this.label14);
            this.groupBox16.Controls.Add(this.MeleeDistance);
            this.groupBox16.Controls.Add(this.label8);
            this.groupBox16.Location = new System.Drawing.Point(16, 193);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(456, 150);
            this.groupBox16.TabIndex = 15;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Combat Range";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(179, 70);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(47, 20);
            this.label22.TabIndex = 11;
            this.label22.Text = "yards";
            this.label22.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // RangedDistance
            // 
            this.helpProvider_0.SetHelpKeyword(this.RangedDistance, "Distances.html");
            this.helpProvider_0.SetHelpNavigator(this.RangedDistance, System.Windows.Forms.HelpNavigator.Topic);
            this.RangedDistance.Location = new System.Drawing.Point(107, 67);
            this.RangedDistance.MaxLength = 6;
            this.RangedDistance.Name = "RangedDistance";
            this.helpProvider_0.SetShowHelp(this.RangedDistance, true);
            this.RangedDistance.Size = new System.Drawing.Size(64, 26);
            this.RangedDistance.TabIndex = 1;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(16, 72);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(70, 20);
            this.label23.TabIndex = 9;
            this.label23.Text = "Ranged:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(179, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 20);
            this.label14.TabIndex = 8;
            this.label14.Text = "yards";
            // 
            // MeleeDistance
            // 
            this.helpProvider_0.SetHelpKeyword(this.MeleeDistance, "Distances.html");
            this.helpProvider_0.SetHelpNavigator(this.MeleeDistance, System.Windows.Forms.HelpNavigator.Topic);
            this.MeleeDistance.Location = new System.Drawing.Point(107, 32);
            this.MeleeDistance.MaxLength = 6;
            this.MeleeDistance.Name = "MeleeDistance";
            this.helpProvider_0.SetShowHelp(this.MeleeDistance, true);
            this.MeleeDistance.Size = new System.Drawing.Size(64, 26);
            this.MeleeDistance.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Melee:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TabChat
            // 
            this.TabChat.Controls.Add(this.groupBox6);
            this.TabChat.Controls.Add(this.ChatLog);
            this.helpProvider_0.SetHelpNavigator(this.TabChat, System.Windows.Forms.HelpNavigator.TopicId);
            this.helpProvider_0.SetHelpString(this.TabChat, "Chat.html");
            this.TabChat.Location = new System.Drawing.Point(4, 29);
            this.TabChat.Name = "TabChat";
            this.helpProvider_0.SetShowHelp(this.TabChat, true);
            this.TabChat.Size = new System.Drawing.Size(1022, 389);
            this.TabChat.TabIndex = 4;
            this.TabChat.Text = "Chat";
            this.TabChat.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.PlaySay);
            this.groupBox6.Controls.Add(this.AutoReplyText);
            this.groupBox6.Controls.Add(this.AutoReply);
            this.groupBox6.Controls.Add(this.PlayWhisper);
            this.groupBox6.Location = new System.Drawing.Point(13, 152);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(974, 231);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Reaction";
            // 
            // ChatLog
            // 
            this.ChatLog.Controls.Add(this.ChatLogFrame);
            this.ChatLog.Controls.Add(this.CombatLogFrame);
            this.ChatLog.Controls.Add(this.label53);
            this.ChatLog.Controls.Add(this.label52);
            this.ChatLog.Controls.Add(this.ChatDelete);
            this.ChatLog.Location = new System.Drawing.Point(13, 12);
            this.ChatLog.Name = "ChatLog";
            this.ChatLog.Size = new System.Drawing.Size(974, 128);
            this.ChatLog.TabIndex = 0;
            this.ChatLog.TabStop = false;
            this.ChatLog.Text = "Logging";
            // 
            // label53
            // 
            this.label53.Location = new System.Drawing.Point(21, 72);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(205, 30);
            this.label53.TabIndex = 3;
            this.label53.Text = "Combat log frame:";
            this.label53.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label52
            // 
            this.label52.Location = new System.Drawing.Point(21, 37);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(205, 30);
            this.label52.TabIndex = 2;
            this.label52.Text = "Chat log frame:";
            this.label52.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TabParty
            // 
            this.TabParty.Controls.Add(this.FollowerOptionsBox);
            this.TabParty.Controls.Add(this.LeaderOptionsBox);
            this.TabParty.Controls.Add(this.PartyOptionsBox);
            this.TabParty.Controls.Add(this.groupBox17);
            this.helpProvider_0.SetHelpNavigator(this.TabParty, System.Windows.Forms.HelpNavigator.TopicId);
            this.helpProvider_0.SetHelpString(this.TabParty, "Party1.html");
            this.TabParty.Location = new System.Drawing.Point(4, 29);
            this.TabParty.Name = "TabParty";
            this.helpProvider_0.SetShowHelp(this.TabParty, true);
            this.TabParty.Size = new System.Drawing.Size(1022, 389);
            this.TabParty.TabIndex = 7;
            this.TabParty.Text = "Party";
            this.TabParty.UseVisualStyleBackColor = true;
            // 
            // FollowerOptionsBox
            // 
            this.FollowerOptionsBox.Controls.Add(this.PartySlashFollow);
            this.FollowerOptionsBox.Controls.Add(this.label29);
            this.FollowerOptionsBox.Controls.Add(this.PartyAttackDelay);
            this.FollowerOptionsBox.Controls.Add(this.label28);
            this.FollowerOptionsBox.Controls.Add(this.PartyLeaderName);
            this.FollowerOptionsBox.Controls.Add(this.label27);
            this.FollowerOptionsBox.Location = new System.Drawing.Point(277, 224);
            this.FollowerOptionsBox.Name = "FollowerOptionsBox";
            this.FollowerOptionsBox.Size = new System.Drawing.Size(710, 100);
            this.FollowerOptionsBox.TabIndex = 3;
            this.FollowerOptionsBox.TabStop = false;
            this.FollowerOptionsBox.Text = "Follower";
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(224, 61);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(80, 19);
            this.label29.TabIndex = 10;
            this.label29.Text = "seconds";
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(11, 61);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(117, 19);
            this.label28.TabIndex = 8;
            this.label28.Text = "Attack delay:";
            this.label28.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(21, 31);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(118, 20);
            this.label27.TabIndex = 6;
            this.label27.Text = "Leader name:";
            this.label27.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LeaderOptionsBox
            // 
            this.LeaderOptionsBox.Controls.Add(this.PartyMember4);
            this.LeaderOptionsBox.Controls.Add(this.PartyMember3);
            this.LeaderOptionsBox.Controls.Add(this.PartyMember2);
            this.LeaderOptionsBox.Controls.Add(this.PartyMember1);
            this.LeaderOptionsBox.Controls.Add(this.label33);
            this.LeaderOptionsBox.Controls.Add(this.label32);
            this.LeaderOptionsBox.Controls.Add(this.label31);
            this.LeaderOptionsBox.Controls.Add(this.label30);
            this.LeaderOptionsBox.Controls.Add(this.label25);
            this.LeaderOptionsBox.Location = new System.Drawing.Point(277, 10);
            this.LeaderOptionsBox.Name = "LeaderOptionsBox";
            this.LeaderOptionsBox.Size = new System.Drawing.Size(710, 203);
            this.LeaderOptionsBox.TabIndex = 2;
            this.LeaderOptionsBox.TabStop = false;
            this.LeaderOptionsBox.Text = "Leader";
            // 
            // label33
            // 
            this.label33.Location = new System.Drawing.Point(21, 152);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(32, 20);
            this.label33.TabIndex = 5;
            this.label33.Text = "4.";
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(21, 121);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(32, 21);
            this.label32.TabIndex = 4;
            this.label32.Text = "3.";
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(21, 91);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(32, 20);
            this.label31.TabIndex = 3;
            this.label31.Text = "2.";
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(21, 61);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(32, 19);
            this.label30.TabIndex = 2;
            this.label30.Text = "1.";
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(53, 31);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(182, 20);
            this.label25.TabIndex = 0;
            this.label25.Text = "Member name";
            // 
            // PartyOptionsBox
            // 
            this.PartyOptionsBox.Controls.Add(this.PartyHealMode);
            this.PartyOptionsBox.Controls.Add(this.label34);
            this.PartyOptionsBox.Controls.Add(this.PartyBuff);
            this.PartyOptionsBox.Controls.Add(this.PartyLootPos);
            this.PartyOptionsBox.Controls.Add(this.label24);
            this.PartyOptionsBox.Controls.Add(this.PartyAdds);
            this.PartyOptionsBox.Controls.Add(this.Looters);
            this.PartyOptionsBox.Controls.Add(this.PartyLooters);
            this.PartyOptionsBox.Location = new System.Drawing.Point(11, 132);
            this.PartyOptionsBox.Name = "PartyOptionsBox";
            this.PartyOptionsBox.Size = new System.Drawing.Size(256, 203);
            this.PartyOptionsBox.TabIndex = 1;
            this.PartyOptionsBox.TabStop = false;
            this.PartyOptionsBox.Text = "Options";
            // 
            // PartyHealMode
            // 
            this.PartyHealMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PartyHealMode.Location = new System.Drawing.Point(85, 162);
            this.PartyHealMode.Name = "PartyHealMode";
            this.PartyHealMode.Size = new System.Drawing.Size(150, 28);
            this.PartyHealMode.TabIndex = 4;
            // 
            // label34
            // 
            this.label34.Location = new System.Drawing.Point(21, 162);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(54, 21);
            this.label34.TabIndex = 4;
            this.label34.Text = "Heal:";
            this.label34.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(11, 121);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(117, 21);
            this.label24.TabIndex = 3;
            this.label24.Text = "Loot position:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Looters
            // 
            this.Looters.Location = new System.Drawing.Point(21, 91);
            this.Looters.Name = "Looters";
            this.Looters.Size = new System.Drawing.Size(107, 20);
            this.Looters.TabIndex = 2;
            this.Looters.Text = "Looters:";
            this.Looters.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.PartyFollower);
            this.groupBox17.Controls.Add(this.PartyLeader);
            this.groupBox17.Controls.Add(this.PartySolo);
            this.groupBox17.Location = new System.Drawing.Point(11, 10);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(256, 119);
            this.groupBox17.TabIndex = 0;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Mode";
            // 
            // TabMisc
            // 
            this.TabMisc.Controls.Add(this.groupBox4);
            this.TabMisc.Controls.Add(this.groupBox2);
            this.helpProvider_0.SetHelpNavigator(this.TabMisc, System.Windows.Forms.HelpNavigator.TopicId);
            this.helpProvider_0.SetHelpString(this.TabMisc, "Miscellaneous.html");
            this.TabMisc.Location = new System.Drawing.Point(4, 29);
            this.TabMisc.Name = "TabMisc";
            this.helpProvider_0.SetShowHelp(this.TabMisc, true);
            this.TabMisc.Size = new System.Drawing.Size(1022, 389);
            this.TabMisc.TabIndex = 2;
            this.TabMisc.Text = "Miscellaneous";
            this.TabMisc.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.StopWhenFull);
            this.groupBox4.Controls.Add(this.StopAfter);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.StopAfterMinutes);
            this.groupBox4.Location = new System.Drawing.Point(13, 289);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(974, 82);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Auto-Stop";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(245, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 22);
            this.label4.TabIndex = 5;
            this.label4.Text = "minutes";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // TabBackground
            // 
            this.TabBackground.Controls.Add(this.groupBox26);
            this.TabBackground.Controls.Add(this.groupBox25);
            this.TabBackground.Controls.Add(this.groupBox24);
            this.helpProvider_0.SetHelpNavigator(this.TabBackground, System.Windows.Forms.HelpNavigator.TopicId);
            this.helpProvider_0.SetHelpString(this.TabBackground, "Background.html");
            this.TabBackground.Location = new System.Drawing.Point(4, 29);
            this.TabBackground.Name = "TabBackground";
            this.helpProvider_0.SetShowHelp(this.TabBackground, true);
            this.TabBackground.Size = new System.Drawing.Size(1022, 389);
            this.TabBackground.TabIndex = 8;
            this.TabBackground.Text = "Background";
            this.TabBackground.UseVisualStyleBackColor = true;
            // 
            // groupBox26
            // 
            this.groupBox26.Controls.Add(this.WebNotifyCredentials);
            this.groupBox26.Controls.Add(this.WebNotifyURL);
            this.groupBox26.Controls.Add(this.label39);
            this.groupBox26.Controls.Add(this.label38);
            this.groupBox26.Controls.Add(this.WebNotifyEnabled);
            this.groupBox26.Location = new System.Drawing.Point(1027, 88);
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.Size = new System.Drawing.Size(560, 163);
            this.groupBox26.TabIndex = 10;
            this.groupBox26.TabStop = false;
            this.groupBox26.Text = "Web Notify";
            this.groupBox26.Visible = false;
            // 
            // WebNotifyCredentials
            // 
            this.WebNotifyCredentials.Enabled = false;
            this.WebNotifyCredentials.Location = new System.Drawing.Point(126, 72);
            this.WebNotifyCredentials.Name = "WebNotifyCredentials";
            this.WebNotifyCredentials.Size = new System.Drawing.Size(405, 26);
            this.WebNotifyCredentials.TabIndex = 4;
            // 
            // WebNotifyURL
            // 
            this.WebNotifyURL.Enabled = false;
            this.WebNotifyURL.Location = new System.Drawing.Point(126, 37);
            this.WebNotifyURL.Name = "WebNotifyURL";
            this.WebNotifyURL.Size = new System.Drawing.Size(405, 26);
            this.WebNotifyURL.TabIndex = 3;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(8, 75);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(93, 20);
            this.label39.TabIndex = 2;
            this.label39.Text = "Credentials:";
            this.label39.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(66, 41);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(46, 20);
            this.label38.TabIndex = 1;
            this.label38.Text = "URL:";
            this.label38.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // WebNotifyEnabled
            // 
            this.WebNotifyEnabled.AutoSize = true;
            this.WebNotifyEnabled.Location = new System.Drawing.Point(254, 117);
            this.WebNotifyEnabled.Name = "WebNotifyEnabled";
            this.WebNotifyEnabled.Size = new System.Drawing.Size(94, 24);
            this.WebNotifyEnabled.TabIndex = 0;
            this.WebNotifyEnabled.Text = "Enabled";
            this.WebNotifyEnabled.UseVisualStyleBackColor = true;
            // 
            // groupBox25
            // 
            this.groupBox25.Controls.Add(this.DisplayShrink);
            this.groupBox25.Controls.Add(this.DisplayHide);
            this.groupBox25.Controls.Add(this.DisplayNormal);
            this.groupBox25.Enabled = false;
            this.groupBox25.Location = new System.Drawing.Point(451, 16);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Size = new System.Drawing.Size(392, 186);
            this.groupBox25.TabIndex = 20;
            this.groupBox25.TabStop = false;
            this.groupBox25.Text = "Display";
            // 
            // DisplayShrink
            // 
            this.DisplayShrink.AutoSize = true;
            this.DisplayShrink.Location = new System.Drawing.Point(27, 105);
            this.DisplayShrink.Name = "DisplayShrink";
            this.DisplayShrink.Size = new System.Drawing.Size(179, 24);
            this.DisplayShrink.TabIndex = 2;
            this.DisplayShrink.TabStop = true;
            this.DisplayShrink.Text = "Shrink game window";
            this.DisplayShrink.UseVisualStyleBackColor = true;
            // 
            // DisplayHide
            // 
            this.DisplayHide.AutoSize = true;
            this.DisplayHide.Location = new System.Drawing.Point(27, 72);
            this.DisplayHide.Name = "DisplayHide";
            this.DisplayHide.Size = new System.Drawing.Size(167, 24);
            this.DisplayHide.TabIndex = 1;
            this.DisplayHide.TabStop = true;
            this.DisplayHide.Text = "Hide game window";
            this.DisplayHide.UseVisualStyleBackColor = true;
            this.DisplayHide.CheckedChanged += new System.EventHandler(this.DisplayHide_CheckedChanged);
            // 
            // DisplayNormal
            // 
            this.DisplayNormal.AutoSize = true;
            this.DisplayNormal.Location = new System.Drawing.Point(27, 37);
            this.DisplayNormal.Name = "DisplayNormal";
            this.DisplayNormal.Size = new System.Drawing.Size(194, 24);
            this.DisplayNormal.TabIndex = 0;
            this.DisplayNormal.TabStop = true;
            this.DisplayNormal.Text = "Leave game as normal";
            this.DisplayNormal.UseVisualStyleBackColor = true;
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.BackgroundEnable);
            this.groupBox24.Location = new System.Drawing.Point(16, 16);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new System.Drawing.Size(400, 186);
            this.groupBox24.TabIndex = 19;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "Options";
            // 
            // TabVending
            // 
            this.TabVending.Controls.Add(this.MailItemBox);
            this.TabVending.Controls.Add(this.MailSetupBox);
            this.TabVending.Controls.Add(this.groupBox33);
            this.TabVending.Controls.Add(this.groupBox34);
            this.helpProvider_0.SetHelpNavigator(this.TabVending, System.Windows.Forms.HelpNavigator.TopicId);
            this.helpProvider_0.SetHelpString(this.TabVending, "Vending.html");
            this.TabVending.Location = new System.Drawing.Point(4, 29);
            this.TabVending.Name = "TabVending";
            this.helpProvider_0.SetShowHelp(this.TabVending, true);
            this.TabVending.Size = new System.Drawing.Size(1022, 389);
            this.TabVending.TabIndex = 10;
            this.TabVending.Text = "Vending";
            this.TabVending.UseVisualStyleBackColor = true;
            // 
            // MailItemBox
            // 
            this.MailItemBox.Controls.Add(this.VendMailList);
            this.MailItemBox.Location = new System.Drawing.Point(741, 16);
            this.MailItemBox.Name = "MailItemBox";
            this.MailItemBox.Size = new System.Drawing.Size(254, 335);
            this.MailItemBox.TabIndex = 3;
            this.MailItemBox.TabStop = false;
            this.MailItemBox.Text = "Mail Items";
            // 
            // VendMailList
            // 
            this.VendMailList.AcceptsReturn = true;
            this.VendMailList.Location = new System.Drawing.Point(10, 22);
            this.VendMailList.Multiline = true;
            this.VendMailList.Name = "VendMailList";
            this.VendMailList.Size = new System.Drawing.Size(233, 291);
            this.VendMailList.TabIndex = 0;
            // 
            // MailSetupBox
            // 
            this.MailSetupBox.Controls.Add(this.SendMail);
            this.MailSetupBox.Controls.Add(this.SubjectLabel);
            this.MailSetupBox.Controls.Add(this.SubjectText);
            this.MailSetupBox.Controls.Add(this.mailtoLabel);
            this.MailSetupBox.Controls.Add(this.MailToText);
            this.MailSetupBox.Location = new System.Drawing.Point(6, 190);
            this.MailSetupBox.Name = "MailSetupBox";
            this.MailSetupBox.Size = new System.Drawing.Size(460, 161);
            this.MailSetupBox.TabIndex = 2;
            this.MailSetupBox.TabStop = false;
            this.MailSetupBox.Text = "Mail Item Setup";
            // 
            // SendMail
            // 
            this.SendMail.AutoSize = true;
            this.SendMail.Location = new System.Drawing.Point(126, 114);
            this.SendMail.Name = "SendMail";
            this.SendMail.Size = new System.Drawing.Size(105, 24);
            this.SendMail.TabIndex = 4;
            this.SendMail.Text = "Send Mail";
            this.SendMail.UseVisualStyleBackColor = true;
            // 
            // SubjectLabel
            // 
            this.SubjectLabel.AutoSize = true;
            this.SubjectLabel.Location = new System.Drawing.Point(10, 67);
            this.SubjectLabel.Name = "SubjectLabel";
            this.SubjectLabel.Size = new System.Drawing.Size(71, 20);
            this.SubjectLabel.TabIndex = 3;
            this.SubjectLabel.Text = "Subject: ";
            // 
            // SubjectText
            // 
            this.SubjectText.Location = new System.Drawing.Point(91, 63);
            this.SubjectText.Name = "SubjectText";
            this.SubjectText.Size = new System.Drawing.Size(267, 26);
            this.SubjectText.TabIndex = 2;
            // 
            // mailtoLabel
            // 
            this.mailtoLabel.AutoSize = true;
            this.mailtoLabel.Location = new System.Drawing.Point(10, 31);
            this.mailtoLabel.Name = "mailtoLabel";
            this.mailtoLabel.Size = new System.Drawing.Size(63, 20);
            this.mailtoLabel.TabIndex = 1;
            this.mailtoLabel.Text = "Mail to: ";
            // 
            // MailToText
            // 
            this.MailToText.Location = new System.Drawing.Point(91, 26);
            this.MailToText.Name = "MailToText";
            this.MailToText.Size = new System.Drawing.Size(267, 26);
            this.MailToText.TabIndex = 0;
            // 
            // groupBox33
            // 
            this.groupBox33.Controls.Add(this.VendGrey);
            this.groupBox33.Controls.Add(this.VendWhite);
            this.groupBox33.Controls.Add(this.VendGreen);
            this.groupBox33.Location = new System.Drawing.Point(6, 16);
            this.groupBox33.Name = "groupBox33";
            this.groupBox33.Size = new System.Drawing.Size(460, 161);
            this.groupBox33.TabIndex = 0;
            this.groupBox33.TabStop = false;
            this.groupBox33.Text = "Sell Item Type";
            // 
            // VendGrey
            // 
            this.VendGrey.AutoSize = true;
            this.VendGrey.Checked = true;
            this.VendGrey.Location = new System.Drawing.Point(14, 37);
            this.VendGrey.Name = "VendGrey";
            this.VendGrey.Size = new System.Drawing.Size(170, 24);
            this.VendGrey.TabIndex = 0;
            this.VendGrey.TabStop = true;
            this.VendGrey.Text = "Sell only poor items";
            // 
            // VendWhite
            // 
            this.VendWhite.AutoSize = true;
            this.VendWhite.Location = new System.Drawing.Point(14, 73);
            this.VendWhite.Name = "VendWhite";
            this.VendWhite.Size = new System.Drawing.Size(234, 24);
            this.VendWhite.TabIndex = 1;
            this.VendWhite.Text = "Sell poor and common items";
            // 
            // VendGreen
            // 
            this.VendGreen.AutoSize = true;
            this.VendGreen.Location = new System.Drawing.Point(14, 110);
            this.VendGreen.Name = "VendGreen";
            this.VendGreen.Size = new System.Drawing.Size(325, 24);
            this.VendGreen.TabIndex = 2;
            this.VendGreen.Text = "Sell poor, common, and uncommon items";
            // 
            // groupBox34
            // 
            this.groupBox34.Controls.Add(this.VendWhiteList);
            this.groupBox34.Location = new System.Drawing.Point(475, 16);
            this.groupBox34.Name = "groupBox34";
            this.groupBox34.Size = new System.Drawing.Size(256, 335);
            this.groupBox34.TabIndex = 1;
            this.groupBox34.TabStop = false;
            this.groupBox34.Text = "Protected Items";
            // 
            // VendWhiteList
            // 
            this.VendWhiteList.AcceptsReturn = true;
            this.VendWhiteList.Location = new System.Drawing.Point(10, 22);
            this.VendWhiteList.Multiline = true;
            this.VendWhiteList.Name = "VendWhiteList";
            this.VendWhiteList.Size = new System.Drawing.Size(236, 294);
            this.VendWhiteList.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabGeneral);
            this.tabControl1.Controls.Add(this.TabLimits);
            this.tabControl1.Controls.Add(this.TabDistances);
            this.tabControl1.Controls.Add(this.TabDetection);
            this.tabControl1.Controls.Add(this.TabKeys);
            this.tabControl1.Controls.Add(this.TabChat);
            this.tabControl1.Controls.Add(this.TabParty);
            this.tabControl1.Controls.Add(this.TabMisc);
            this.tabControl1.Controls.Add(this.TabBackground);
            this.tabControl1.Controls.Add(this.TabClasses);
            this.tabControl1.Controls.Add(this.TabVending);
            this.tabControl1.Location = new System.Drawing.Point(13, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1030, 422);
            this.tabControl1.TabIndex = 0;
            // 
            // TabClasses
            // 
            this.TabClasses.Controls.Add(this.groupBox29);
            this.TabClasses.Controls.Add(this.groupBox28);
            this.TabClasses.Location = new System.Drawing.Point(4, 29);
            this.TabClasses.Name = "TabClasses";
            this.TabClasses.Size = new System.Drawing.Size(1022, 389);
            this.TabClasses.TabIndex = 9;
            this.TabClasses.Text = "Classes";
            this.TabClasses.UseVisualStyleBackColor = true;
            // 
            // groupBox29
            // 
            this.groupBox29.Location = new System.Drawing.Point(539, 4);
            this.groupBox29.Name = "groupBox29";
            this.groupBox29.Size = new System.Drawing.Size(303, 179);
            this.groupBox29.TabIndex = 1;
            this.groupBox29.TabStop = false;
            this.groupBox29.Text = "Options";
            // 
            // groupBox28
            // 
            this.groupBox28.Controls.Add(this.ClassFilesList);
            this.groupBox28.Controls.Add(this.CompileButton);
            this.groupBox28.Location = new System.Drawing.Point(3, 4);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new System.Drawing.Size(501, 335);
            this.groupBox28.TabIndex = 0;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "Classes Present";
            // 
            // ClassFilesList
            // 
            this.ClassFilesList.FormattingEnabled = true;
            this.ClassFilesList.Location = new System.Drawing.Point(8, 26);
            this.ClassFilesList.Name = "ClassFilesList";
            this.ClassFilesList.Size = new System.Drawing.Size(477, 214);
            this.ClassFilesList.TabIndex = 2;
            // 
            // CompileButton
            // 
            this.CompileButton.Enabled = false;
            this.CompileButton.Location = new System.Drawing.Point(155, 276);
            this.CompileButton.Name = "CompileButton";
            this.CompileButton.Size = new System.Drawing.Size(186, 41);
            this.CompileButton.TabIndex = 1;
            this.CompileButton.Text = "Test Compile";
            this.CompileButton.UseVisualStyleBackColor = true;
            // 
            // TabInvisible
            // 
            this.TabInvisible.Controls.Add(this.groupBox20);
            this.TabInvisible.Location = new System.Drawing.Point(4, 25);
            this.TabInvisible.Name = "TabInvisible";
            this.TabInvisible.Size = new System.Drawing.Size(582, 276);
            this.TabInvisible.TabIndex = 8;
            this.TabInvisible.Text = "Invisible";
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.SetProfile3);
            this.groupBox20.Controls.Add(this.SetProfile2);
            this.groupBox20.Controls.Add(this.SetProfile1);
            this.groupBox20.Controls.Add(this.SetInitial);
            this.groupBox20.Controls.Add(this.Profile3);
            this.groupBox20.Controls.Add(this.Profile2);
            this.groupBox20.Controls.Add(this.label47);
            this.groupBox20.Controls.Add(this.label46);
            this.groupBox20.Controls.Add(this.Profile1);
            this.groupBox20.Controls.Add(this.label45);
            this.groupBox20.Controls.Add(this.InitialProfile);
            this.groupBox20.Controls.Add(this.label44);
            this.groupBox20.Location = new System.Drawing.Point(8, 8);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(560, 168);
            this.groupBox20.TabIndex = 0;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Startup";
            // 
            // SetProfile3
            // 
            this.SetProfile3.Location = new System.Drawing.Point(488, 136);
            this.SetProfile3.Name = "SetProfile3";
            this.SetProfile3.Size = new System.Drawing.Size(48, 24);
            this.SetProfile3.TabIndex = 15;
            this.SetProfile3.Text = "Set";
            // 
            // SetProfile2
            // 
            this.SetProfile2.Location = new System.Drawing.Point(488, 104);
            this.SetProfile2.Name = "SetProfile2";
            this.SetProfile2.Size = new System.Drawing.Size(48, 24);
            this.SetProfile2.TabIndex = 14;
            this.SetProfile2.Text = "Set";
            // 
            // SetProfile1
            // 
            this.SetProfile1.Location = new System.Drawing.Point(488, 72);
            this.SetProfile1.Name = "SetProfile1";
            this.SetProfile1.Size = new System.Drawing.Size(48, 24);
            this.SetProfile1.TabIndex = 13;
            this.SetProfile1.Text = "Set";
            // 
            // SetInitial
            // 
            this.SetInitial.Location = new System.Drawing.Point(488, 32);
            this.SetInitial.Name = "SetInitial";
            this.SetInitial.Size = new System.Drawing.Size(48, 24);
            this.SetInitial.TabIndex = 12;
            this.SetInitial.Text = "Set";
            // 
            // Profile3
            // 
            this.Profile3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Profile3.Location = new System.Drawing.Point(104, 136);
            this.Profile3.Name = "Profile3";
            this.Profile3.Size = new System.Drawing.Size(376, 18);
            this.Profile3.TabIndex = 11;
            this.Profile3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Profile2
            // 
            this.Profile2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Profile2.Location = new System.Drawing.Point(104, 104);
            this.Profile2.Name = "Profile2";
            this.Profile2.Size = new System.Drawing.Size(376, 18);
            this.Profile2.TabIndex = 10;
            this.Profile2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label47
            // 
            this.label47.Location = new System.Drawing.Point(16, 136);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(80, 16);
            this.label47.TabIndex = 9;
            this.label47.Text = "Profile 3:";
            this.label47.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label46
            // 
            this.label46.Location = new System.Drawing.Point(16, 104);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(80, 16);
            this.label46.TabIndex = 8;
            this.label46.Text = "Profile 2:";
            this.label46.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Profile1
            // 
            this.Profile1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Profile1.Location = new System.Drawing.Point(104, 72);
            this.Profile1.Name = "Profile1";
            this.Profile1.Size = new System.Drawing.Size(376, 18);
            this.Profile1.TabIndex = 7;
            this.Profile1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label45
            // 
            this.label45.Location = new System.Drawing.Point(16, 72);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(80, 16);
            this.label45.TabIndex = 6;
            this.label45.Text = "Profile 1:";
            this.label45.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // InitialProfile
            // 
            this.InitialProfile.ForeColor = System.Drawing.SystemColors.Highlight;
            this.InitialProfile.Location = new System.Drawing.Point(104, 32);
            this.InitialProfile.Name = "InitialProfile";
            this.InitialProfile.Size = new System.Drawing.Size(376, 18);
            this.InitialProfile.TabIndex = 5;
            this.InitialProfile.Text = "??";
            this.InitialProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label44
            // 
            this.label44.Location = new System.Drawing.Point(16, 32);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(80, 16);
            this.label44.TabIndex = 0;
            this.label44.Text = "Initial profile:";
            this.label44.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TabDev
            // 
            this.TabDev.Controls.Add(this.DevBuffs);
            this.TabDev.Controls.Add(this.label43);
            this.TabDev.Location = new System.Drawing.Point(4, 25);
            this.TabDev.Name = "TabDev";
            this.TabDev.Size = new System.Drawing.Size(582, 276);
            this.TabDev.TabIndex = 8;
            this.TabDev.Text = "Dev";
            // 
            // DevBuffs
            // 
            this.DevBuffs.Location = new System.Drawing.Point(8, 48);
            this.DevBuffs.Multiline = true;
            this.DevBuffs.Name = "DevBuffs";
            this.DevBuffs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DevBuffs.Size = new System.Drawing.Size(240, 120);
            this.DevBuffs.TabIndex = 1;
            // 
            // label43
            // 
            this.label43.Location = new System.Drawing.Point(8, 24);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(104, 16);
            this.label43.TabIndex = 0;
            this.label43.Text = "Buffs:";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(1069, 540);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.MyHelpButton);
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.OKButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.helpProvider_0.SetHelpKeyword(this, "General.html");
            this.helpProvider_0.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.helpProvider_0.SetShowHelp(this, true);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Glider Configuration";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.TabGeneral.ResumeLayout(false);
            this.groupBox31.ResumeLayout(false);
            this.groupBox31.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.TabLimits.ResumeLayout(false);
            this.groupBox32.ResumeLayout(false);
            this.groupBox32.PerformLayout();
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.TabDetection.ResumeLayout(false);
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.TabKeys.ResumeLayout(false);
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.TabDistances.ResumeLayout(false);
            this.groupBox30.ResumeLayout(false);
            this.groupBox30.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.TabChat.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ChatLog.ResumeLayout(false);
            this.ChatLog.PerformLayout();
            this.TabParty.ResumeLayout(false);
            this.FollowerOptionsBox.ResumeLayout(false);
            this.FollowerOptionsBox.PerformLayout();
            this.LeaderOptionsBox.ResumeLayout(false);
            this.LeaderOptionsBox.PerformLayout();
            this.PartyOptionsBox.ResumeLayout(false);
            this.PartyOptionsBox.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.TabMisc.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.TabBackground.ResumeLayout(false);
            this.groupBox26.ResumeLayout(false);
            this.groupBox26.PerformLayout();
            this.groupBox25.ResumeLayout(false);
            this.groupBox25.PerformLayout();
            this.groupBox24.ResumeLayout(false);
            this.TabVending.ResumeLayout(false);
            this.MailItemBox.ResumeLayout(false);
            this.MailItemBox.PerformLayout();
            this.MailSetupBox.ResumeLayout(false);
            this.MailSetupBox.PerformLayout();
            this.groupBox33.ResumeLayout(false);
            this.groupBox33.PerformLayout();
            this.groupBox34.ResumeLayout(false);
            this.groupBox34.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.TabClasses.ResumeLayout(false);
            this.groupBox28.ResumeLayout(false);
            this.TabInvisible.ResumeLayout(false);
            this.groupBox20.ResumeLayout(false);
            this.TabDev.ResumeLayout(false);
            this.TabDev.PerformLayout();
            this.ResumeLayout(false);

    }

    private void OKButton_Click(object sender, EventArgs e)
    {
        if (bool_0)
        {
            var num = (int)MessageBox.Show(this,
                "Note: changes to process renaming will not take effect until Glider is restarted!  Close and re-start Glider to load new changes.",
                "Process Settings Changed", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        var selectedItem = (SpellActionData)ClassList.SelectedItem;
        ConfigManager.gclass61_0.method_0("CustomClassName", selectedItem.string_1);
        
        ConfigManager.gclass61_0.method_0("ManageGamePos", ManageGamePos.Checked.ToString());
        ConfigManager.gclass61_0.method_0("UseMediaKeys", MediaKeysBox.Checked.ToString());
        ConfigManager.gclass61_0.method_0("AutoSkin", AutoSkin.Checked.ToString());
        ConfigManager.gclass61_0.method_0("NinjaSkin", NinjaSkin.Checked.ToString());
        ConfigManager.gclass61_0.method_0("WalkLoot", WalkLoot.Checked.ToString());
        ConfigManager.gclass61_0.method_0("ResetBuffs", ResetBuffs.Checked.ToString());
        ConfigManager.gclass61_0.method_0("Resurrect", Resurrect.Checked.ToString());
        ConfigManager.gclass61_0.method_0("AltLayout", AltLayout.Checked.ToString());
        ConfigManager.gclass61_0.method_0("ChatDelete", ChatDelete.Checked.ToString());
        ConfigManager.gclass61_0.method_0("ChatWhisper", PlayWhisper.Checked.ToString());
        ConfigManager.gclass61_0.method_0("PlaySay", PlaySay.Checked.ToString());
        ConfigManager.gclass61_0.method_0("SoundKill", SoundKill.Checked.ToString());
        ConfigManager.gclass61_0.method_0("ChatAutoReply", AutoReply.Checked.ToString());
        ConfigManager.gclass61_0.method_0("ChatAutoReplyText", AutoReplyText.Text.Trim());
        ConfigManager.gclass61_0.method_0("RemoveDebuffs", RemoveDebuffs.Checked.ToString());
        ConfigManager.gclass61_0.method_0("AppKey", ProductKeyBox.Text);
        ConfigManager.gclass61_0.method_0("UseClipboard", UseClipboard.Checked.ToString());
        ConfigManager.gclass61_0.method_0("SkipLoot", SkipLoot.Checked.ToString());
        ConfigManager.gclass61_0.method_0("TeleportStop", TeleportStop.Checked.ToString());
        ConfigManager.gclass61_0.method_0("TeleportLogout", TeleportLogout.Checked.ToString());
        ConfigManager.gclass61_0.method_0("UseBandages", UseBandages.Checked.ToString());
        ConfigManager.gclass61_0.method_0("JumpMore", JumpMore.Checked.ToString());
        ConfigManager.gclass61_0.method_0("Strafe", Strafe.Checked.ToString());
        ConfigManager.gclass61_0.method_0("FastEat", FastEat.Checked.ToString());
        ConfigManager.gclass61_0.method_0("SitWhenBored", SitWhenBored.Checked.ToString());
        ConfigManager.gclass61_0.method_0("PickupJunk", PickupJunk.Checked.ToString());
        
        ConfigManager.gclass61_0.method_0("FightPlayers", FightPlayers.Checked.ToString());
        ConfigManager.gclass61_0.method_0("StopOnVanish", StopOnVanish.Checked.ToString());
        ConfigManager.gclass61_0.method_0("StopLootingWhenFull", StopLootingWhenFull.Checked.ToString());
        ConfigManager.gclass61_0.method_0("StopWhenFull", StopWhenFull.Checked.ToString());
        ConfigManager.gclass61_0.method_0("UseHook", UseHook.Checked.ToString());
        ConfigManager.gclass61_0.method_0("MouseSpin", MouseSpin.Checked.ToString());
        ConfigManager.gclass61_0.method_0("ShiftLoot", ShiftLoot.Checked.ToString());
        ConfigManager.gclass61_0.method_0("BackgroundEnable", BackgroundEnable.Checked.ToString());
        ConfigManager.gclass61_0.method_0("UseTray", UseTray.Checked.ToString());
        ConfigManager.gclass61_0.method_0("TurboLoot", TurboLoot.Checked.ToString());
        ConfigManager.gclass61_0.method_0("WebNotifyEnabled", WebNotifyEnabled.Checked.ToString());
        ConfigManager.gclass61_0.method_0("WebNotifyURL", WebNotifyURL.Text);
        ConfigManager.gclass61_0.method_0("WebNotifyCredentials", WebNotifyCredentials.Text);
        if (AutoLogCharacter.SelectedIndex == 0)
            ConfigManager.gclass61_0.method_0("AutoLog", "");
        else
            ConfigManager.gclass61_0.method_0("AutoLog", AutoLogCharacter.Items[AutoLogCharacter.SelectedIndex].ToString());
        if (StopAfter.Checked)
            ConfigManager.gclass61_0.method_0("AutoStop", "True");
        else
            ConfigManager.gclass61_0.method_0("AutoStop", "False");
        if (ProductKeyBox.Text.Trim().Length == 0)
            ConfigManager.gclass61_0.method_0("AppKey", "demo");
        if (StartupClass.IsIntegerInput(SpellLeadDelay.Text))
            ConfigManager.gclass61_0.method_0("SpellLeadDelay", SpellLeadDelay.Text);
        if (StartupClass.IsIntegerInput(ExtraPull.Text))
            ConfigManager.gclass61_0.method_0("ExtraPull", ExtraPull.Text);
        if (StartupClass.IsIntegerInput(StopAfterMinutes.Text))
            ConfigManager.gclass61_0.method_0("AutoStopMinutes", StopAfterMinutes.Text);
        if (StartupClass.IsIntegerInput(MaxResurrect.Text))
            ConfigManager.gclass61_0.method_0("MaxResurrect", MaxResurrect.Text);
        if (StartupClass.IsIntegerInput(RestHealth.Text))
            ConfigManager.gclass61_0.method_0("RestHealth", RestHealth.Text);
        if (StartupClass.IsIntegerInput(RestMana.Text))
            ConfigManager.gclass61_0.method_0("RestMana", RestMana.Text);
        if (StartupClass.IsIntegerInput(KeyDelay.Text))
            ConfigManager.gclass61_0.method_0("KeyDelay", KeyDelay.Text);
        if (StartupClass.IsIntegerInput(FoodAmount.Text))
            ConfigManager.gclass61_0.method_0("FoodAmount", FoodAmount.Text);
        if (StartupClass.IsIntegerInput(AmmoAmount.Text))
            ConfigManager.gclass61_0.method_0("AmmoAmount", AmmoAmount.Text);
        if (StartupClass.IsIntegerInput(WaterAmount.Text))
            ConfigManager.gclass61_0.method_0("WaterAmount", WaterAmount.Text);
        if (StartupClass.IsIntegerInput(PawSpeed.Text))
            ConfigManager.gclass61_0.method_0("PawSpeed", PawSpeed.Text);
        if (StartupClass.IsIntegerInput(HarvestRange.Text))
            ConfigManager.gclass61_0.method_0("HarvestRange", HarvestRange.Text);
        if (StartupClass.IsIntegerInput(BandageHealth.Text))
            ConfigManager.gclass61_0.method_0("BandageHealth", BandageHealth.Text);
        if (method_2(FriendAlert.Text))
            ConfigManager.gclass61_0.method_0("FriendAlert", FriendAlert.Text);
        if (StartupClass.IsIntegerInput(FriendLogout.Text))
            ConfigManager.gclass61_0.method_0("FriendLogout", FriendLogout.Text);
        if (StartupClass.IsIntegerInput(MaxPopups.Text))
            ConfigManager.gclass61_0.method_0("MaxPopups", MaxPopups.Text);
        ConfigManager.gclass61_0.method_0("LootCheckHostiles", LootCheckHostiles.Checked.ToString());
        if (StartupClass.IsIntegerInput(LootSafeDistance.Text))
            ConfigManager.gclass61_0.method_0("LootCheckDistance", LootSafeDistance.Text);
        if (method_2(MeleeDistance.Text))
            ConfigManager.gclass61_0.method_0("MeleeDistance", MeleeDistance.Text);
        if (method_2(RangedDistance.Text))
            ConfigManager.gclass61_0.method_0("RangedDistance", RangedDistance.Text);
        ConfigManager.gclass61_0.method_0("AvoidSameFaction", AvoidSameFaction.Checked.ToString());
        ConfigManager.gclass61_0.method_0("AvoidOtherFaction", AvoidOtherFaction.Checked.ToString());
        ConfigManager.gclass61_0.method_0("PartyAdds", PartyAdds.Checked.ToString());
        ConfigManager.gclass61_0.method_0("PartyBuff", PartyBuff.Checked.ToString());
        ConfigManager.gclass61_0.method_0("PartySlashFollow", PartySlashFollow.Checked.ToString());
        switch (PartyHealMode.SelectedIndex)
        {
            case 0:
                ConfigManager.gclass61_0.method_0("PartyHealMode", "Dedicated");
                break;
            case 1:
                ConfigManager.gclass61_0.method_0("PartyHealMode", "Normal");
                break;
            case 2:
                ConfigManager.gclass61_0.method_0("PartyHealMode", "Never");
                break;
        }

        if (StartupClass.IsIntegerInput(PartyLooters.Text))
            ConfigManager.gclass61_0.method_0("PartyLooters", PartyLooters.Text);
        if (StartupClass.IsIntegerInput(PartyLootPos.Text))
            ConfigManager.gclass61_0.method_0("PartyLootPos", PartyLootPos.Text);
        if (StartupClass.IsIntegerInput(PartyAttackDelay.Text))
            ConfigManager.gclass61_0.method_0("PartyAttackDelay", PartyAttackDelay.Text);
        if (StartupClass.IsIntegerInput(PartyLeaderWait.Text))
            ConfigManager.gclass61_0.method_0("PartyLeaderWait", PartyLeaderWait.Text);
        if (StartupClass.IsIntegerInput(PartyFollowerStart.Text))
            ConfigManager.gclass61_0.method_0("PartyFollowerStart", PartyFollowerStart.Text);
        if (StartupClass.IsIntegerInput(PartyFollowerStop.Text))
            ConfigManager.gclass61_0.method_0("PartyFollowerStop", PartyFollowerStop.Text);
        if (PartySolo.Checked)
            ConfigManager.gclass61_0.method_0("PartyMode", "Solo");
        if (PartyLeader.Checked)
            ConfigManager.gclass61_0.method_0("PartyMode", "Leader");
        if (PartyFollower.Checked)
            ConfigManager.gclass61_0.method_0("PartyMode", "Follower");
        ConfigManager.gclass61_0.method_0("PartyLeaderName", PartyLeaderName.Text);
        ConfigManager.gclass61_0.method_0("PartyMember1", PartyMember1.Text.Trim());
        ConfigManager.gclass61_0.method_0("PartyMember2", PartyMember2.Text.Trim());
        ConfigManager.gclass61_0.method_0("PartyMember3", PartyMember3.Text.Trim());
        ConfigManager.gclass61_0.method_0("PartyMember4", PartyMember4.Text.Trim());
        ConfigManager.gclass61_0.method_0("ListenEnabled", ListenEnabled.Checked.ToString());
        if (StartupClass.IsIntegerInput(ListenPort.Text))
            ConfigManager.gclass61_0.method_0("ListenPort", ListenPort.Text);
        ConfigManager.gclass61_0.method_0("ListenPassword", ListenPassword.Text);
        ConfigManager.gclass61_0.method_0("RelogEnabled", RelogEnabled.Checked.ToString());
        ConfigManager.gclass61_0.method_0("BypassLootSanity", BypassLootSanity.Checked.ToString());
        ConfigManager.gclass61_0.method_0("StrafeObstacles", StrafeObstacles.Checked.ToString());
        ConfigManager.gclass61_0.method_0("ChatLogFrame", ChatLogFrame.Text.Trim());
        ConfigManager.gclass61_0.method_0("CombatLogFrame", CombatLogFrame.Text.Trim());
        method_1("LastProfile", InitialProfile);
        method_1("Profile1", Profile1);
        method_1("Profile2", Profile2);
        method_1("Profile3", Profile3);
        if (DisplayNormal.Checked)
            ConfigManager.gclass61_0.method_0("BackgroundDisplay", "Normal");
        if (DisplayHide.Checked)
            ConfigManager.gclass61_0.method_0("BackgroundDisplay", "Hide");
        if (DisplayShrink.Checked)
            ConfigManager.gclass61_0.method_0("BackgroundDisplay", "Shrink");
        if (VendGrey.Checked)
            ConfigManager.gclass61_0.method_0("VendType", "Poor");
        if (VendWhite.Checked)
            ConfigManager.gclass61_0.method_0("VendType", "Common");
        if (VendGreen.Checked)
            ConfigManager.gclass61_0.method_0("VendType", "Uncommon");
        var str1 = "";
        var stringList1 = new List<string>();
        var stringReader1 = new StringReader(VendWhiteList.Text);
        string str2;
        while ((str2 = stringReader1.ReadLine()) != null)
            if (str2.Length >= 2)
                stringList1.Add(str2);
        stringReader1.Close();
        for (var index = 0; index < stringList1.Count; ++index)
            str1 = index != stringList1.Count - 1 ? str1 + stringList1[index] + "," : str1 + stringList1[index];
        ConfigManager.gclass61_0.method_0("VendWhiteList", str1);
        ConfigManager.gclass61_0.method_8();
        ConfigManager.gclass61_0.method_0("SendMail", SendMail.Checked.ToString());
        if (MailToText.Text.Length < 1)
            ConfigManager.gclass61_0.method_0("MailToText", "");
        else
            ConfigManager.gclass61_0.method_0("MailToText", MailToText.Text.Trim());
        if (SubjectText.Text.Length < 1)
            ConfigManager.gclass61_0.method_0("SubjectText", "");
        else
            ConfigManager.gclass61_0.method_0("SubjectText", SubjectText.Text.Trim());
        var str3 = "";
        var stringList2 = new List<string>();
        var stringReader2 = new StringReader(VendMailList.Text);
        string str4;
        while ((str4 = stringReader2.ReadLine()) != null)
            if (str4.Length >= 2)
                stringList2.Add(str4);
        stringReader2.Close();
        for (var index = 0; index < stringList2.Count; ++index)
            str3 = index != stringList2.Count - 1 ? str3 + stringList2[index] + "," : str3 + stringList2[index];
        ConfigManager.gclass61_0.method_0("VendMailList", str3);
        ConfigManager.gclass61_0.method_8();
        StartupClass.IsGliderInitialized = false;
        method_18();
        StartupClass.MainWindowHandle = null;
        DialogResult = DialogResult.OK;
    }

    private void method_1(string string_0, Label label_0)
    {
        if (!(label_0.Text != MessageProvider.GetMessage(771)))
            return;
        ConfigManager.gclass61_0.method_0(string_0, label_0.Text);
    }

    private bool method_2(string string_0)
    {
        try
        {
            double.Parse(string_0);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private void MyCancelButton_Click(object sender, EventArgs e)
    {
        StartupClass.MainWindowHandle = null;
        DialogResult = DialogResult.Cancel;
    }

    private void ClassOptionsButton_Click(object sender, EventArgs e)
    {
        var selectedItem = (SpellActionData)ClassList.SelectedItem;
        selectedItem.method_0();
        var object0 = (GGameClass)selectedItem.object_0;
        Logger.LogMessage("Calling show config on " + selectedItem.string_0);
        var gconfigResult = object0.ShowConfiguration();
        switch (gconfigResult)
        {
            case GConfigResult.NotSupported:
                var num = (int)MessageBox.Show(this, MessageProvider.GetMessage(852), GameMemoryAccess.GenerateRandomString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                break;
            case GConfigResult.Accept:
                object0.LoadConfig();
                break;
        }

        if (gconfigResult != GConfigResult.Accept)
            return;
        if (ConfigManager.gclass61_0.method_2("RemindActionBars") == null)
            method_3();
        StartupClass.RequiresConfigReload = true;
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        var helpString = helpProvider_0.GetHelpString(tabControl1.SelectedTab);
        if (helpString == null || helpString.Length <= 0)
            return;
        GameMemoryAccess.IsWindowVisible(this, "Glider.chm", HelpNavigator.Topic, helpString);
    }

    private void StopAfter_CheckedChanged(object sender, EventArgs e)
    {
        if (StopAfter.Checked)
            StopAfterMinutes.Enabled = true;
        else
            StopAfterMinutes.Enabled = false;
    }

    private void method_3()
    {
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        if (method_20())
            Process.Start("http://www.mmoglider.com.cn");
        else
            Process.Start("http://www.mmoglider.com");
    }

    private void UseBandages_CheckedChanged(object sender, EventArgs e)
    {
        BandageHealth.Enabled = UseBandages.Checked;
    }

    private void UseClipboard_CheckedChanged(object sender, EventArgs e)
    {
        KeyDelay.Enabled = !UseClipboard.Checked;
    }

    private void method_4(object sender, EventArgs e)
    {
        TeleportLogout.Enabled = TeleportStop.Checked;
    }

    private void method_5(object sender, EventArgs e)
    {
        SpellcastingManager.gclass42_0.method_14();
    }

    private void LoadKeymap_Click(object sender, EventArgs e)
    {
        SpellcastingManager.gclass42_0.method_12();
    }

    private void PartySolo_CheckedChanged(object sender, EventArgs e)
    {
        foreach (Control control in (ArrangedElementCollection)PartyOptionsBox.Controls)
            control.Enabled = false;
        foreach (Control control in (ArrangedElementCollection)LeaderOptionsBox.Controls)
            control.Enabled = false;
        foreach (Control control in (ArrangedElementCollection)FollowerOptionsBox.Controls)
            control.Enabled = false;
    }

    private void PartyFollower_CheckedChanged(object sender, EventArgs e)
    {
        foreach (Control control in (ArrangedElementCollection)PartyOptionsBox.Controls)
            control.Enabled = true;
        foreach (Control control in (ArrangedElementCollection)LeaderOptionsBox.Controls)
            control.Enabled = false;
        foreach (Control control in (ArrangedElementCollection)FollowerOptionsBox.Controls)
            control.Enabled = true;
    }

    private void PartyLeader_CheckedChanged(object sender, EventArgs e)
    {
        foreach (Control control in (ArrangedElementCollection)PartyOptionsBox.Controls)
            control.Enabled = true;
        foreach (Control control in (ArrangedElementCollection)LeaderOptionsBox.Controls)
            control.Enabled = true;
        foreach (Control control in (ArrangedElementCollection)FollowerOptionsBox.Controls)
            control.Enabled = false;
    }

    private void PartyMember1_TextChanged(object sender, EventArgs e)
    {
        StartupClass.PartyStateManager.bool_4 = true;
    }

    private void method_6(object sender, EventArgs e)
    {
        StartupClass.PartyStateManager.bool_4 = true;
    }

    private void PartyMember2_TextChanged(object sender, EventArgs e)
    {
        StartupClass.PartyStateManager.bool_4 = true;
    }

    private void method_7(object sender, EventArgs e)
    {
        StartupClass.PartyStateManager.bool_4 = true;
    }

    private void PartyMember3_TextChanged(object sender, EventArgs e)
    {
        StartupClass.PartyStateManager.bool_4 = true;
    }

    private void method_8(object sender, EventArgs e)
    {
        StartupClass.PartyStateManager.bool_4 = true;
    }

    private void PartyMember4_TextChanged(object sender, EventArgs e)
    {
        StartupClass.PartyStateManager.bool_4 = true;
    }

    private void method_9(object sender, EventArgs e)
    {
        StartupClass.PartyStateManager.bool_4 = true;
    }

    private void ListenEnabled_CheckedChanged(object sender, EventArgs e)
    {
        ListenPort.Enabled = ListenEnabled.Checked;
        ListenPassword.Enabled = ListenEnabled.Checked;
    }

    private void ClassList_SelectedIndexChanged(object sender, EventArgs e)
    {
        StartupClass.RequiresConfigReload = true;
    }

    private void SetInitial_Click(object sender, EventArgs e)
    {
        method_10(InitialProfile, "LastProfile");
    }

    private void method_10(Label label_0, string string_0)
    {
        var openFileDialog = new OpenFileDialog();
        openFileDialog.RestoreDirectory = true;
        openFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\Profiles";
        openFileDialog.Filter = MessageProvider.GetMessage(661);
        if (openFileDialog.ShowDialog(this) != DialogResult.OK)
            return;
        label_0.Text = openFileDialog.FileName;
    }

    private void SetProfile1_Click(object sender, EventArgs e)
    {
        method_10(Profile1, "Profile1");
    }

    private void SetProfile2_Click(object sender, EventArgs e)
    {
        method_10(Profile2, "Profile2");
    }

    private void SetProfile3_Click(object sender, EventArgs e)
    {
        method_10(Profile3, "Profile3");
    }

    private void EditDebuffs_Click(object sender, EventArgs e)
    {
        if (StartupClass.ApplicationStartupMode == AppMode.Normal)
            StartupClass.KnownDebuffs.method_10();
        var debuffList = new DebuffList();
        debuffList.method_0();
        if (debuffList.Offsets.Keys.Count == 0)
        {
            var num1 = (int)MessageBox.Show(this, MessageProvider.smethod_4("DebuffList.NoneNew"),
                MessageProvider.smethod_4("DebuffList.NoneNewTitle"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        else
        {
            var num2 = (int)debuffList.ShowDialog(this);
            DebuffsKnown.Text = MessageProvider.smethod_6("Config.DebuffsKnown", StartupClass.KnownDebuffs.method_9());
        }
    }

    private void AutoSkin_CheckedChanged(object sender, EventArgs e)
    {
        NinjaSkin.Enabled = AutoSkin.Checked;
    }

    private void BackgroundEnable_CheckedChanged(object sender, EventArgs e)
    {
        groupBox25.Enabled = BackgroundEnable.Checked;
        if (bool_1)
            return;
    }

    private void method_11()
    {
        if (!Directory.Exists("Classes"))
            Directory.CreateDirectory("Classes");
        var files = Directory.GetFiles("Classes", "*.cs");
        ConfigManager.gclass61_0.method_10("CustomClasses");
        foreach (var string_0 in files)
        {
            var key = method_13(string_0);
            ClassFilesList.Items.Add(key, StartupClass.ProfileMapping.ContainsKey(key));
        }
    }

    private bool method_12(string string_0, string[] string_1)
    {
        foreach (var str in string_1)
            if (string_0.ToLower() == str.ToLower())
                return true;
        return false;
    }

    private void WebNotifyEnabled_CheckedChanged(object sender, EventArgs e)
    {
        WebNotifyCredentials.Enabled = WebNotifyEnabled.Checked;
        WebNotifyURL.Enabled = WebNotifyEnabled.Checked;
    }

    private string method_13(string string_0)
    {
        var num = string_0.LastIndexOf('\\');
        return num == -1 ? string_0 : string_0.Substring(num + 1);
    }

    private void method_14(object sender, EventArgs e)
    {
        method_15();
    }

    private void CompileButton_Click(object sender, EventArgs e)
    {
        method_15();
    }

    private void method_15()
    {
        var selectedItem = (string)ClassFilesList.SelectedItem;
        string string_1;
        var assembly_0 = CodeCompiler.smethod_0(selectedItem, out string_1);
        if (assembly_0 == null)
        {
            var num1 = (int)MessageBox.Show(this, string_1, "Compile Failed", MessageBoxButtons.OK,
                MessageBoxIcon.Hand);
        }
        else
        {
            var str = "";
            if (StartupClass.ProfileMapping.ContainsKey(selectedItem))
                str = "\r\n\r\nClass is already loaded in Glider, refreshing with new code.";
            if (string_1.Length > 0)
            {
                var num2 = (int)MessageBox.Show(this, string_1 + str, "Compile Successful w/Warnings",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                var num3 = (int)MessageBox.Show(this, "No warnings!" + str, "Compile Successful.", MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
            }

            if (!StartupClass.ProfileMapping.ContainsKey(selectedItem))
                return;
            var profile = StartupClass.ProfileMapping[selectedItem];
            ((GGameClass)profile.object_0).Shutdown();
            profile.object_0 = CodeCompiler.smethod_7(selectedItem, assembly_0, false, true);
        }
    }

    private void ClassFilesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        CompileButton.Enabled = true;
    }

    private void ClassFilesList_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (!bool_3)
            return;
        var selectedItem = (string)ClassFilesList.SelectedItem;
        if (ConfigManager.gclass61_0.method_11("CustomClasses", selectedItem))
        {
            if (!StartupClass.ProfileMapping.ContainsKey(selectedItem))
                return;
            var profile = StartupClass.ProfileMapping[selectedItem];
            if (ClassList.Items.Contains(profile))
            {
                if (ClassList.SelectedItem == profile)
                {
                    var num = (int)MessageBox.Show(this,
                        "This class cannot be unloaded because it is in use.  Switch Glider to another class in the \"General\" tab before unloading this class.",
                        GameMemoryAccess.GenerateRandomString(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    e.NewValue = CheckState.Checked;
                    return;
                }

                ClassList.Items.Remove(profile);
            }

            CodeCompiler.smethod_5(selectedItem);
            ConfigManager.gclass61_0.method_13("CustomClasses", selectedItem);
        }
        else
        {
            string string_1;
            if (CodeCompiler.smethod_13(selectedItem, out string_1))
            {
                ConfigManager.gclass61_0.method_12("CustomClasses", selectedItem);
                ClassList.Items.Add(StartupClass.ProfileMapping[selectedItem]);
            }
            else
            {
                var num = (int)MessageBox.Show(this, string_1, "Compile Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Hand);
                e.NewValue = CheckState.Unchecked;
            }
        }
    }

    private void LootCheckHostiles_CheckedChanged(object sender, EventArgs e)
    {
        LootSafeDistance.Enabled = LootCheckHostiles.Checked;
    }

    private void Resurrect_CheckedChanged(object sender, EventArgs e)
    {
        MaxResurrect.Enabled = Resurrect.Checked;
    }

    private void AccountCreate_Click(object sender, EventArgs e)
    {
        if (Directory.GetFiles("Accounts\\", "*.xml").Length == 0 && MessageBox.Show(this, MessageProvider.GetMessage(867),
                GameMemoryAccess.GenerateRandomString(), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            GameMemoryAccess.IsWindowVisible(this, "Glider.chm", HelpNavigator.Topic, "AutoLogin.html");
        var num = (int)new AccountInfo().ShowDialog(this);
        method_16();
    }

    private void ButtonViewCharacters_Click(object sender, EventArgs e)
    {
        Process.Start(Environment.CurrentDirectory + "\\Accounts");
    }

    private void method_16()
    {
        if (!Directory.Exists("Accounts"))
            Directory.CreateDirectory("Accounts");
        bool_2 = false;
        AutoLogCharacter.Items.Clear();
        AutoLogCharacter.Items.Add(MessageProvider.GetMessage(874));
        AutoLogCharacter.SelectedIndex = 0;
        var files = Directory.GetFiles("Accounts\\", "*.xml");
        var str1 = ConfigManager.gclass61_0.method_2("AutoLog");
        foreach (var string_0 in files)
        {
            var str2 = method_17(string_0);
            AutoLogCharacter.Items.Add(str2);
            if (str2 == str1)
                AutoLogCharacter.SelectedItem = str2;
        }

        bool_2 = true;
    }

    private string method_17(string string_0)
    {
        if (string_0.LastIndexOf("\\") > -1)
            string_0 = string_0.Substring(string_0.LastIndexOf("\\") + 1);
        if (string_0.EndsWith(".xml"))
            string_0 = string_0.Substring(0, string_0.Length - 4);
        return string_0;
    }

    private void AutoLogCharacter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!bool_2)
            ;
    }

    protected void method_18()
    {
        KeyEditClass.Items.Add("(Select...)");
        foreach (var gkey in SpellcastingManager.gclass42_0.Offsets.Values)
            if (gkey.KeyName.IndexOf('.') > 0)
            {
                var key = gkey.KeyName.Substring(0, gkey.KeyName.IndexOf('.'));
                if (!Offsets.ContainsKey(key))
                {
                    var str = key;
                    var string_6 = "Common.Class" + key;
                    if (MessageProvider.smethod_5(string_6))
                        str = MessageProvider.smethod_4(string_6);
                    Offsets.Add(key, str);
                    KeyEditClass.Items.Add(str);
                }
            }

        KeyEditClass.SelectedIndex = 0;
    }

    private void KeyEditClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        EditKeymap.Enabled = KeyEditClass.SelectedIndex != 0;
    }

    private void EditKeymap_Click(object sender, EventArgs e)
    {
        var str = KeyEditClass.SelectedItem.ToString();
        foreach (var key in Offsets.Keys)
            if (Offsets[key] == str)
            {
                method_19(key, Offsets[key]);
                break;
            }
    }

    protected void method_19(string string_0, string string_1)
    {
        var num = (int)new KeyEditor(string_0, string_1).ShowDialog(this);
    }

    [SpecialName]
    private bool method_20()
    {
        return ConfigManager.gclass61_0.method_2("AppKey").StartsWith("02") ||
               MessageProvider.gclass30_0.string_0.ToLower().IndexOf("zh") > -1;
    }

    private void DoSecCheck_Click(object sender, EventArgs e)
    {

    }

    private void AllowNetCheck_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void AllowAutoSecCheck_CheckedChanged(object sender, EventArgs e)
    {

    }

    private void GliderDebug_CheckedChanged_1(object sender, EventArgs e)
    {

    }

    private void DisplayHide_CheckedChanged(object sender, EventArgs e)
    {

    }
}


