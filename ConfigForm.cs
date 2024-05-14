// Decompiled with JetBrains decompiler
// Type: ConfigForm
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Glider.Common;
using Glider.Common.Objects;

public class ConfigForm : Form
{
    private Button AccountCreate;
    private CheckBox AllowAutoSecCheck;
    private CheckBox AllowNetCheck;
    private CheckBox AllowWW;
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
    private Button DoSecCheck;
    private Button EditDebuffs;
    private Button EditKeymap;
    private TextBox ExtraPull;
    private CheckBox FastEat;
    private CheckBox FightPlayers;
    private GroupBox FollowerOptionsBox;
    private TextBox FoodAmount;
    private TextBox FriendAlert;
    private TextBox FriendLogout;
    private CheckBox GliderDebug;
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
    private readonly SortedList<string, string> sortedList_0 = new SortedList<string, string>();
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
            if (profile.genum1_0 == GEnum1.const_1 &&
                (profile.object_0 == null || ((GGameClass)profile.object_0).IsSelectable))
            {
                ClassList.Items.Add(profile);
                if (key == GClass61.gclass61_0.method_2("CustomClassName"))
                    ClassList.SelectedIndex = ClassList.Items.Count - 1;
            }
        }

        if (ClassList.SelectedIndex == -1)
            ClassList.SelectedIndex = (int)StartupClass.gameClass_0;
        AltLayout.Enabled = true;
        AllowWW.Enabled = true;
        if (bool_4)
            tabControl1.Controls.Add(TabInvisible);
        ChatLogFrame.Text = GClass61.gclass61_0.method_2(nameof(ChatLogFrame));
        CombatLogFrame.Text = GClass61.gclass61_0.method_2(nameof(CombatLogFrame));
        WebNotifyEnabled.Checked = GClass61.gclass61_0.method_2(nameof(WebNotifyEnabled)) == "True";
        WebNotifyCredentials.Text = GClass61.gclass61_0.method_2(nameof(WebNotifyCredentials));
        WebNotifyURL.Text = GClass61.gclass61_0.method_2(nameof(WebNotifyURL));
        UseTray.Checked = GClass61.gclass61_0.method_2(nameof(UseTray)) == "True";
        BackgroundEnable.Checked = GClass61.gclass61_0.method_2(nameof(BackgroundEnable)) == "True";
        BackgroundEnable.Enabled = StartupClass.GliderManager != null;
        ShiftLoot.Checked = GClass61.gclass61_0.method_2(nameof(ShiftLoot)) == "True";
        UseHook.Checked = GClass61.gclass61_0.method_2(nameof(UseHook)) == "True";
        MouseSpin.Checked = GClass61.gclass61_0.method_2(nameof(MouseSpin)) == "True";
        ManageGamePos.Checked = GClass61.gclass61_0.method_2(nameof(ManageGamePos)) == "True";
        MediaKeysBox.Checked = GClass61.gclass61_0.method_2("UseMediaKeys") == "True";
        AutoSkin.Checked = GClass61.gclass61_0.method_2(nameof(AutoSkin)) == "True";
        NinjaSkin.Checked = GClass61.gclass61_0.method_2(nameof(NinjaSkin)) == "True";
        WalkLoot.Checked = GClass61.gclass61_0.method_2(nameof(WalkLoot)) == "True";
        ProductKeyBox.Text = GClass61.gclass61_0.method_2("AppKey");
        SpellLeadDelay.Text = GClass61.gclass61_0.method_2(nameof(SpellLeadDelay));
        ExtraPull.Text = GClass61.gclass61_0.method_2(nameof(ExtraPull));
        ResetBuffs.Checked = GClass61.gclass61_0.method_2(nameof(ResetBuffs)) == "True";
        Resurrect.Checked = GClass61.gclass61_0.method_2(nameof(Resurrect)) == "True";
        AltLayout.Checked = GClass61.gclass61_0.method_2(nameof(AltLayout)) == "True";
        MaxResurrect.Text = GClass61.gclass61_0.method_2(nameof(MaxResurrect));
        StopLootingWhenFull.Checked = GClass61.gclass61_0.method_5(nameof(StopLootingWhenFull));
        TurboLoot.Checked = GClass61.gclass61_0.method_5(nameof(TurboLoot));
        StopWhenFull.Checked = GClass61.gclass61_0.method_5(nameof(StopWhenFull));
        AllowNetCheck.Checked = GClass61.gclass61_0.method_5(nameof(AllowNetCheck));
        if (GClass61.gclass61_0.method_2("AutoStop") == "True")
            StopAfter.Checked = true;
        StopAfterMinutes.Text = GClass61.gclass61_0.method_2("AutoStopMinutes");
        StopAfter_CheckedChanged(null, null);
        RestHealth.Text = GClass61.gclass61_0.method_2(nameof(RestHealth));
        RestMana.Text = GClass61.gclass61_0.method_2(nameof(RestMana));
        ChatDelete.Checked = GClass61.gclass61_0.method_2(nameof(ChatDelete)) == "True";
        PlayWhisper.Checked = GClass61.gclass61_0.method_2("ChatWhisper") == "True";
        PlaySay.Checked = GClass61.gclass61_0.method_2(nameof(PlaySay)) == "True";
        SoundKill.Checked = GClass61.gclass61_0.method_2(nameof(SoundKill)) == "True";
        AutoReply.Checked = GClass61.gclass61_0.method_2("ChatAutoReply") == "True";
        AutoReplyText.Text = GClass61.gclass61_0.method_2("ChatAutoReplyText");
        RemoveDebuffs.Checked = GClass61.gclass61_0.method_5(nameof(RemoveDebuffs));
        UseClipboard.Checked = GClass61.gclass61_0.method_2(nameof(UseClipboard)) == "True";
        KeyDelay.Text = GClass61.gclass61_0.method_2(nameof(KeyDelay));
        PawSpeed.Text = GClass61.gclass61_0.method_2(nameof(PawSpeed));
        FastEat.Checked = GClass61.gclass61_0.method_2(nameof(FastEat)) == "True";
        UseBandages.Checked = GClass61.gclass61_0.method_2(nameof(UseBandages)) == "True";
        SitWhenBored.Checked = GClass61.gclass61_0.method_2(nameof(SitWhenBored)) == "True";
        BandageHealth.Text = GClass61.gclass61_0.method_2(nameof(BandageHealth));
        JumpMore.Checked = GClass61.gclass61_0.method_2(nameof(JumpMore)) == "True";
        Strafe.Checked = GClass61.gclass61_0.method_2(nameof(Strafe)) == "True";
        AllowWW.Checked = GClass61.gclass61_0.method_5(nameof(AllowWW));
        SkipLoot.Checked = GClass61.gclass61_0.method_2(nameof(SkipLoot)) == "True";
        HarvestRange.Text = GClass61.gclass61_0.method_2(nameof(HarvestRange));
        PickupJunk.Checked = GClass61.gclass61_0.method_2(nameof(PickupJunk)) == "True";
        AllowAutoSecCheck.Checked = GClass61.gclass61_0.method_2(nameof(AllowAutoSecCheck)) == "True";
        TeleportStop.Checked = GClass61.gclass61_0.method_2(nameof(TeleportStop)) == "True";
        TeleportLogout.Checked = GClass61.gclass61_0.method_2(nameof(TeleportLogout)) == "True";
        FoodAmount.Text = GClass61.gclass61_0.method_2(nameof(FoodAmount));
        AmmoAmount.Text = GClass61.gclass61_0.method_2(nameof(AmmoAmount));
        WaterAmount.Text = GClass61.gclass61_0.method_2(nameof(WaterAmount));
        FriendAlert.Text = GClass61.gclass61_0.method_2(nameof(FriendAlert));
        FriendLogout.Text = GClass61.gclass61_0.method_2(nameof(FriendLogout));
        MaxPopups.Text = GClass61.gclass61_0.method_2(nameof(MaxPopups));
        AvoidSameFaction.Checked = GClass61.gclass61_0.method_2(nameof(AvoidSameFaction)) == "True";
        AvoidOtherFaction.Checked = GClass61.gclass61_0.method_2(nameof(AvoidOtherFaction)) == "True";
        LootSafeDistance.Text = GClass61.gclass61_0.method_2("LootCheckDistance");
        LootCheckHostiles.Checked = GClass61.gclass61_0.method_5(nameof(LootCheckHostiles));
        MeleeDistance.Text = GClass61.gclass61_0.method_2(nameof(MeleeDistance));
        RangedDistance.Text = GClass61.gclass61_0.method_2(nameof(RangedDistance));
        StopOnVanish.Checked = GClass61.gclass61_0.method_5(nameof(StopOnVanish));
        FightPlayers.Checked = GClass61.gclass61_0.method_5(nameof(FightPlayers));
        PartyAdds.Checked = GClass61.gclass61_0.method_2(nameof(PartyAdds)) == "True";
        PartyBuff.Checked = GClass61.gclass61_0.method_2(nameof(PartyBuff)) == "True";
        PartySlashFollow.Checked = GClass61.gclass61_0.method_2(nameof(PartySlashFollow)) == "True";
        switch (GClass61.gclass61_0.method_2("PartyMode"))
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

        switch (GClass61.gclass61_0.method_2(nameof(PartyHealMode)))
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

        PartyLooters.Text = GClass61.gclass61_0.method_2(nameof(PartyLooters));
        PartyLootPos.Text = GClass61.gclass61_0.method_2(nameof(PartyLootPos));
        PartyMember1.Text = GClass61.gclass61_0.method_2(nameof(PartyMember1));
        PartyMember2.Text = GClass61.gclass61_0.method_2(nameof(PartyMember2));
        PartyMember3.Text = GClass61.gclass61_0.method_2(nameof(PartyMember3));
        PartyMember4.Text = GClass61.gclass61_0.method_2(nameof(PartyMember4));
        PartyLeaderName.Text = GClass61.gclass61_0.method_2(nameof(PartyLeaderName));
        PartyAttackDelay.Text = GClass61.gclass61_0.method_2(nameof(PartyAttackDelay));
        PartyLeaderWait.Text = GClass61.gclass61_0.method_2(nameof(PartyLeaderWait));
        PartyFollowerStart.Text = GClass61.gclass61_0.method_2(nameof(PartyFollowerStart));
        PartyFollowerStop.Text = GClass61.gclass61_0.method_2(nameof(PartyFollowerStop));
        bool_0 = false;
        ListenEnabled.Checked = GClass61.gclass61_0.method_5(nameof(ListenEnabled));
        ListenPort.Text = GClass61.gclass61_0.method_2(nameof(ListenPort));
        ListenPassword.Text = GClass61.gclass61_0.method_2(nameof(ListenPassword));
        BypassLootSanity.Checked = GClass61.gclass61_0.method_5(nameof(BypassLootSanity));
        RelogEnabled.Checked = GClass61.gclass61_0.method_5(nameof(RelogEnabled));
        StrafeObstacles.Checked = GClass61.gclass61_0.method_2(nameof(StrafeObstacles)) == "True";
        MessageProvider.smethod_3(this, "Config");
        if (method_20())
            linkLabel1.Text = "http://www.mmoglider.com.cn";
        GliderVersionLabel.Text = MessageProvider.smethod_6("Config.GliderVersionLabel", "1.8.0", "Release");
        WowVersionLabel.Text = MessageProvider.smethod_6("Config.WowVersionLabel", StartupClass.WowVersionLabel_string);
        DebuffsKnown.Text = MessageProvider.smethod_6("Config.DebuffsKnown", StartupClass.DebuffsKnown_string.method_9());
        if (GClass61.gclass61_0.method_2("LastProfile") != null)
            InitialProfile.Text = GClass61.gclass61_0.method_2("LastProfile");
        else
            InitialProfile.Text = MessageProvider.GetMessage(771);
        method_0(nameof(Profile1), Profile1);
        method_0(nameof(Profile2), Profile2);
        method_0(nameof(Profile3), Profile3);
        switch (GClass61.gclass61_0.method_2("BackgroundDisplay"))
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

        switch (GClass61.gclass61_0.method_2("VendType"))
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

        if (GClass61.gclass61_0.method_2(nameof(VendWhiteList)) != null)
            VendWhiteList.Text = GClass61.gclass61_0.method_2(nameof(VendWhiteList)).Replace(",", Environment.NewLine);
        if (GClass61.gclass61_0.method_2(nameof(VendMailList)) != null)
            VendMailList.Text = GClass61.gclass61_0.method_2(nameof(VendMailList)).Replace(",", Environment.NewLine);
        MailToText.Text = GClass61.gclass61_0.method_2(nameof(MailToText));
        SubjectText.Text = GClass61.gclass61_0.method_2(nameof(SubjectText));
        SendMail.Checked = GClass61.gclass61_0.method_2(nameof(SendMail)) == "True";
        if (GClass61.gclass61_0.method_5(nameof(GliderDebug)))
            Logger.LogMessage("Glider debug mode enabled");
        method_11();
        method_16();
        method_18();
        GProcessMemoryManipulator.smethod_48(this);
        GProcessMemoryManipulator.smethod_51(helpProvider_0);
        StartupClass.gclass54_0.bool_4 = false;
        bool_3 = true;
        StartupClass.MainWindowHandle = this;
    }

    private void method_0(string string_0, Label label_0)
    {
        var str = GClass61.gclass61_0.method_2(string_0);
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
        OKButton = new Button();
        MyCancelButton = new Button();
        groupBox1 = new GroupBox();
        ClassOptionsButton = new Button();
        ClassList = new ComboBox();
        groupBox2 = new GroupBox();
        GliderDebug = new CheckBox();
        RelogEnabled = new CheckBox();
        TurboLoot = new CheckBox();
        StopLootingWhenFull = new CheckBox();
        ManageGamePos = new CheckBox();
        AltLayout = new CheckBox();
        SoundKill = new CheckBox();
        UseTray = new CheckBox();
        ShiftLoot = new CheckBox();
        NinjaSkin = new CheckBox();
        RemoveDebuffs = new CheckBox();
        StrafeObstacles = new CheckBox();
        BypassLootSanity = new CheckBox();
        FightPlayers = new CheckBox();
        SitWhenBored = new CheckBox();
        SkipLoot = new CheckBox();
        AutoSkin = new CheckBox();
        MediaKeysBox = new CheckBox();
        ResetBuffs = new CheckBox();
        WalkLoot = new CheckBox();
        helpProvider_0 = new HelpProvider();
        MyHelpButton = new Button();
        ProductKeyBox = new TextBox();
        StopAfterMinutes = new TextBox();
        RestHealth = new TextBox();
        RestMana = new TextBox();
        StopAfter = new CheckBox();
        ChatDelete = new CheckBox();
        PlayWhisper = new CheckBox();
        AutoReply = new CheckBox();
        AutoReplyText = new TextBox();
        KeyDelay = new TextBox();
        BandageHealth = new TextBox();
        UseBandages = new CheckBox();
        FastEat = new CheckBox();
        UseClipboard = new CheckBox();
        LoadKeymap = new Button();
        PartyAttackDelay = new TextBox();
        PartyLeaderName = new TextBox();
        PartyLootPos = new TextBox();
        PartyAdds = new CheckBox();
        PartyLooters = new TextBox();
        PartyFollower = new RadioButton();
        PartyLeader = new RadioButton();
        PartySolo = new RadioButton();
        PartyMember1 = new TextBox();
        PartyMember2 = new TextBox();
        PartyMember3 = new TextBox();
        PartyMember4 = new TextBox();
        PartyBuff = new CheckBox();
        PartySlashFollow = new CheckBox();
        ListenPassword = new TextBox();
        ListenPort = new TextBox();
        ListenEnabled = new CheckBox();
        StopWhenFull = new CheckBox();
        PlaySay = new CheckBox();
        CombatLogFrame = new TextBox();
        ChatLogFrame = new TextBox();
        BackgroundEnable = new CheckBox();
        TabGeneral = new TabPage();
        groupBox31 = new GroupBox();
        label26 = new Label();
        AutoLogCharacter = new ComboBox();
        ButtonViewCharacters = new Button();
        AccountCreate = new Button();
        groupBox19 = new GroupBox();
        label42 = new Label();
        label41 = new Label();
        groupBox7 = new GroupBox();
        WowVersionLabel = new Label();
        GliderVersionLabel = new Label();
        linkLabel1 = new LinkLabel();
        label10 = new Label();
        label9 = new Label();
        groupBox3 = new GroupBox();
        label1 = new Label();
        TabLimits = new TabPage();
        groupBox32 = new GroupBox();
        AmmoAmount = new TextBox();
        label60 = new Label();
        WaterAmount = new TextBox();
        FoodAmount = new TextBox();
        label3 = new Label();
        label2 = new Label();
        groupBox22 = new GroupBox();
        MaxResurrect = new TextBox();
        ResLabel = new Label();
        Resurrect = new CheckBox();
        groupBox23 = new GroupBox();
        DebuffsKnown = new Label();
        EditDebuffs = new Button();
        groupBox13 = new GroupBox();
        label17 = new Label();
        groupBox5 = new GroupBox();
        label6 = new Label();
        label5 = new Label();
        TabDetection = new TabPage();
        groupBox21 = new GroupBox();
        AllowAutoSecCheck = new CheckBox();
        DoSecCheck = new Button();
        AllowNetCheck = new CheckBox();
        StopOnVanish = new CheckBox();
        AllowWW = new CheckBox();
        TeleportLogout = new CheckBox();
        TeleportStop = new CheckBox();
        groupBox9 = new GroupBox();
        Strafe = new CheckBox();
        JumpMore = new CheckBox();
        groupBox14 = new GroupBox();
        label50 = new Label();
        MaxPopups = new TextBox();
        label49 = new Label();
        AvoidOtherFaction = new CheckBox();
        AvoidSameFaction = new CheckBox();
        label21 = new Label();
        FriendLogout = new TextBox();
        label20 = new Label();
        label19 = new Label();
        FriendAlert = new TextBox();
        label18 = new Label();
        TabKeys = new TabPage();
        groupBox27 = new GroupBox();
        MouseSpin = new CheckBox();
        label48 = new Label();
        PawSpeed = new TextBox();
        label40 = new Label();
        groupBox15 = new GroupBox();
        EditKeymap = new Button();
        KeyEditClass = new ComboBox();
        label61 = new Label();
        groupBox8 = new GroupBox();
        label58 = new Label();
        label59 = new Label();
        SpellLeadDelay = new TextBox();
        UseHook = new CheckBox();
        label12 = new Label();
        label11 = new Label();
        TabDistances = new TabPage();
        groupBox30 = new GroupBox();
        label54 = new Label();
        LootSafeDistance = new TextBox();
        label51 = new Label();
        LootCheckHostiles = new CheckBox();
        groupBox12 = new GroupBox();
        PickupJunk = new CheckBox();
        label13 = new Label();
        label7 = new Label();
        ExtraPull = new TextBox();
        label16 = new Label();
        HarvestRange = new TextBox();
        label15 = new Label();
        groupBox18 = new GroupBox();
        label57 = new Label();
        label56 = new Label();
        label55 = new Label();
        PartyFollowerStop = new TextBox();
        PartyFollowerStart = new TextBox();
        PartyLeaderWait = new TextBox();
        label37 = new Label();
        label36 = new Label();
        label35 = new Label();
        groupBox16 = new GroupBox();
        label22 = new Label();
        RangedDistance = new TextBox();
        label23 = new Label();
        label14 = new Label();
        MeleeDistance = new TextBox();
        label8 = new Label();
        TabChat = new TabPage();
        groupBox6 = new GroupBox();
        ChatLog = new GroupBox();
        label53 = new Label();
        label52 = new Label();
        TabParty = new TabPage();
        FollowerOptionsBox = new GroupBox();
        label29 = new Label();
        label28 = new Label();
        label27 = new Label();
        LeaderOptionsBox = new GroupBox();
        label33 = new Label();
        label32 = new Label();
        label31 = new Label();
        label30 = new Label();
        label25 = new Label();
        PartyOptionsBox = new GroupBox();
        PartyHealMode = new ComboBox();
        label34 = new Label();
        label24 = new Label();
        Looters = new Label();
        groupBox17 = new GroupBox();
        TabMisc = new TabPage();
        groupBox4 = new GroupBox();
        label4 = new Label();
        TabBackground = new TabPage();
        groupBox26 = new GroupBox();
        WebNotifyCredentials = new TextBox();
        WebNotifyURL = new TextBox();
        label39 = new Label();
        label38 = new Label();
        WebNotifyEnabled = new CheckBox();
        groupBox25 = new GroupBox();
        DisplayShrink = new RadioButton();
        DisplayHide = new RadioButton();
        DisplayNormal = new RadioButton();
        groupBox24 = new GroupBox();
        TabVending = new TabPage();
        MailItemBox = new GroupBox();
        VendMailList = new TextBox();
        MailSetupBox = new GroupBox();
        SendMail = new CheckBox();
        SubjectLabel = new Label();
        SubjectText = new TextBox();
        mailtoLabel = new Label();
        MailToText = new TextBox();
        groupBox33 = new GroupBox();
        VendGrey = new RadioButton();
        VendWhite = new RadioButton();
        VendGreen = new RadioButton();
        groupBox34 = new GroupBox();
        VendWhiteList = new TextBox();
        tabControl1 = new TabControl();
        TabClasses = new TabPage();
        groupBox29 = new GroupBox();
        groupBox28 = new GroupBox();
        ClassFilesList = new CheckedListBox();
        CompileButton = new Button();
        TabInvisible = new TabPage();
        groupBox20 = new GroupBox();
        SetProfile3 = new Button();
        SetProfile2 = new Button();
        SetProfile1 = new Button();
        SetInitial = new Button();
        Profile3 = new Label();
        Profile2 = new Label();
        label47 = new Label();
        label46 = new Label();
        Profile1 = new Label();
        label45 = new Label();
        InitialProfile = new Label();
        label44 = new Label();
        TabDev = new TabPage();
        DevBuffs = new TextBox();
        label43 = new Label();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        TabGeneral.SuspendLayout();
        groupBox31.SuspendLayout();
        groupBox19.SuspendLayout();
        groupBox7.SuspendLayout();
        groupBox3.SuspendLayout();
        TabLimits.SuspendLayout();
        groupBox32.SuspendLayout();
        groupBox22.SuspendLayout();
        groupBox23.SuspendLayout();
        groupBox13.SuspendLayout();
        groupBox5.SuspendLayout();
        TabDetection.SuspendLayout();
        groupBox21.SuspendLayout();
        groupBox9.SuspendLayout();
        groupBox14.SuspendLayout();
        TabKeys.SuspendLayout();
        groupBox27.SuspendLayout();
        groupBox15.SuspendLayout();
        groupBox8.SuspendLayout();
        TabDistances.SuspendLayout();
        groupBox30.SuspendLayout();
        groupBox12.SuspendLayout();
        groupBox18.SuspendLayout();
        groupBox16.SuspendLayout();
        TabChat.SuspendLayout();
        groupBox6.SuspendLayout();
        ChatLog.SuspendLayout();
        TabParty.SuspendLayout();
        FollowerOptionsBox.SuspendLayout();
        LeaderOptionsBox.SuspendLayout();
        PartyOptionsBox.SuspendLayout();
        groupBox17.SuspendLayout();
        TabMisc.SuspendLayout();
        groupBox4.SuspendLayout();
        TabBackground.SuspendLayout();
        groupBox26.SuspendLayout();
        groupBox25.SuspendLayout();
        groupBox24.SuspendLayout();
        TabVending.SuspendLayout();
        MailItemBox.SuspendLayout();
        MailSetupBox.SuspendLayout();
        groupBox33.SuspendLayout();
        groupBox34.SuspendLayout();
        tabControl1.SuspendLayout();
        TabClasses.SuspendLayout();
        groupBox28.SuspendLayout();
        TabInvisible.SuspendLayout();
        groupBox20.SuspendLayout();
        TabDev.SuspendLayout();
        SuspendLayout();
        OKButton.Location = new Point(382, 302);
        OKButton.Name = "OKButton";
        OKButton.Size = new Size(72, 25);
        OKButton.TabIndex = 0;
        OKButton.Text = "OK";
        OKButton.Click += OKButton_Click;
        MyCancelButton.DialogResult = DialogResult.Cancel;
        MyCancelButton.Location = new Point(475, 302);
        MyCancelButton.Name = "MyCancelButton";
        MyCancelButton.Size = new Size(72, 25);
        MyCancelButton.TabIndex = 1;
        MyCancelButton.Text = "Cancel";
        MyCancelButton.Click += MyCancelButton_Click;
        groupBox1.Controls.Add(ClassOptionsButton);
        groupBox1.Controls.Add(ClassList);
        groupBox1.Location = new Point(16, 8);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(246, 128);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Class";
        helpProvider_0.SetHelpKeyword(ClassOptionsButton, "General.html");
        helpProvider_0.SetHelpNavigator(ClassOptionsButton, HelpNavigator.Topic);
        ClassOptionsButton.Location = new Point(76, 67);
        ClassOptionsButton.Name = "ClassOptionsButton";
        helpProvider_0.SetShowHelp(ClassOptionsButton, true);
        ClassOptionsButton.Size = new Size(88, 23);
        ClassOptionsButton.TabIndex = 1;
        ClassOptionsButton.Text = "Options";
        ClassOptionsButton.Click += ClassOptionsButton_Click;
        ClassList.DropDownStyle = ComboBoxStyle.DropDownList;
        helpProvider_0.SetHelpKeyword(ClassList, "General.html");
        helpProvider_0.SetHelpNavigator(ClassList, HelpNavigator.Topic);
        ClassList.Location = new Point(22, 41);
        ClassList.Name = "ClassList";
        helpProvider_0.SetShowHelp(ClassList, true);
        ClassList.Size = new Size(210, 21);
        ClassList.TabIndex = 0;
        ClassList.SelectedIndexChanged += ClassList_SelectedIndexChanged;
        groupBox2.Controls.Add(GliderDebug);
        groupBox2.Controls.Add(RelogEnabled);
        groupBox2.Controls.Add(TurboLoot);
        groupBox2.Controls.Add(StopLootingWhenFull);
        groupBox2.Controls.Add(ManageGamePos);
        groupBox2.Controls.Add(AltLayout);
        groupBox2.Controls.Add(SoundKill);
        groupBox2.Controls.Add(UseTray);
        groupBox2.Controls.Add(ShiftLoot);
        groupBox2.Controls.Add(NinjaSkin);
        groupBox2.Controls.Add(RemoveDebuffs);
        groupBox2.Controls.Add(StrafeObstacles);
        groupBox2.Controls.Add(BypassLootSanity);
        groupBox2.Controls.Add(FightPlayers);
        groupBox2.Controls.Add(SitWhenBored);
        groupBox2.Controls.Add(SkipLoot);
        groupBox2.Controls.Add(AutoSkin);
        groupBox2.Controls.Add(MediaKeysBox);
        groupBox2.Controls.Add(ResetBuffs);
        groupBox2.Controls.Add(WalkLoot);
        groupBox2.Location = new Point(8, 8);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(609, 190);
        groupBox2.TabIndex = 0;
        groupBox2.TabStop = false;
        groupBox2.Text = "Miscellaneous";
        GliderDebug.AutoSize = true;
        GliderDebug.Location = new Point(166, 162);
        GliderDebug.Name = "GliderDebug";
        GliderDebug.Size = new Size(115, 17);
        GliderDebug.TabIndex = 20;
        GliderDebug.Text = "Glider debug mode";
        GliderDebug.UseVisualStyleBackColor = true;
        GliderDebug.Visible = false;
        GliderDebug.CheckedChanged += GliderDebug_CheckedChanged;
        RelogEnabled.AutoSize = true;
        helpProvider_0.SetHelpKeyword(RelogEnabled, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(RelogEnabled, HelpNavigator.Topic);
        RelogEnabled.Location = new Point(20, 161);
        RelogEnabled.Name = "RelogEnabled";
        helpProvider_0.SetShowHelp(RelogEnabled, true);
        RelogEnabled.Size = new Size(124, 17);
        RelogEnabled.TabIndex = 19;
        RelogEnabled.Text = "Relog on disconnect";
        TurboLoot.AutoSize = true;
        helpProvider_0.SetHelpKeyword(TurboLoot, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(TurboLoot, HelpNavigator.Topic);
        TurboLoot.Location = new Point(365, 138);
        TurboLoot.Name = "TurboLoot";
        helpProvider_0.SetShowHelp(TurboLoot, true);
        TurboLoot.Size = new Size(126, 17);
        TurboLoot.TabIndex = 18;
        TurboLoot.Text = "Turbo loot when safe";
        StopLootingWhenFull.AutoSize = true;
        helpProvider_0.SetHelpKeyword(StopLootingWhenFull, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(StopLootingWhenFull, HelpNavigator.Topic);
        StopLootingWhenFull.Location = new Point(365, 114);
        StopLootingWhenFull.Name = "StopLootingWhenFull";
        helpProvider_0.SetShowHelp(StopLootingWhenFull, true);
        StopLootingWhenFull.Size = new Size(183, 17);
        StopLootingWhenFull.TabIndex = 6;
        StopLootingWhenFull.Text = "Stop looting when inventory is full";
        ManageGamePos.AutoSize = true;
        helpProvider_0.SetHelpKeyword(ManageGamePos, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(ManageGamePos, HelpNavigator.Topic);
        ManageGamePos.Location = new Point(365, 91);
        ManageGamePos.Name = "ManageGamePos";
        helpProvider_0.SetShowHelp(ManageGamePos, true);
        ManageGamePos.Size = new Size(166, 17);
        ManageGamePos.TabIndex = 17;
        ManageGamePos.Text = "Remember game window size";
        AltLayout.AutoSize = true;
        helpProvider_0.SetHelpKeyword(AltLayout, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(AltLayout, HelpNavigator.Topic);
        AltLayout.Location = new Point(365, 68);
        AltLayout.Name = "AltLayout";
        helpProvider_0.SetShowHelp(AltLayout, true);
        AltLayout.Size = new Size(143, 17);
        AltLayout.TabIndex = 16;
        AltLayout.Text = "Horizontal window layout";
        SoundKill.AutoSize = true;
        helpProvider_0.SetHelpKeyword(SoundKill, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(SoundKill, HelpNavigator.Topic);
        SoundKill.Location = new Point(20, 138);
        SoundKill.Name = "SoundKill";
        helpProvider_0.SetShowHelp(SoundKill, true);
        SoundKill.Size = new Size(81, 17);
        SoundKill.TabIndex = 15;
        SoundKill.Text = "Beep on kill";
        UseTray.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseTray, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(UseTray, HelpNavigator.Topic);
        UseTray.Location = new Point(365, 44);
        UseTray.Name = "UseTray";
        helpProvider_0.SetShowHelp(UseTray, true);
        UseTray.Size = new Size(113, 17);
        UseTray.TabIndex = 13;
        UseTray.Text = "Icon in system tray";
        ShiftLoot.AutoSize = true;
        helpProvider_0.SetHelpKeyword(ShiftLoot, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(ShiftLoot, HelpNavigator.Topic);
        ShiftLoot.Location = new Point(365, 21);
        ShiftLoot.Name = "ShiftLoot";
        helpProvider_0.SetShowHelp(ShiftLoot, true);
        ShiftLoot.Size = new Size(100, 17);
        ShiftLoot.TabIndex = 12;
        ShiftLoot.Text = "Shift to autoloot";
        NinjaSkin.AutoSize = true;
        NinjaSkin.Enabled = false;
        helpProvider_0.SetHelpKeyword(NinjaSkin, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(NinjaSkin, HelpNavigator.Topic);
        NinjaSkin.Location = new Point(166, 44);
        NinjaSkin.Name = "NinjaSkin";
        helpProvider_0.SetShowHelp(NinjaSkin, true);
        NinjaSkin.Size = new Size(72, 17);
        NinjaSkin.TabIndex = 7;
        NinjaSkin.Text = "Ninja skin";
        RemoveDebuffs.AutoSize = true;
        helpProvider_0.SetHelpKeyword(RemoveDebuffs, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(RemoveDebuffs, HelpNavigator.Topic);
        RemoveDebuffs.Location = new Point(20, 114);
        RemoveDebuffs.Name = "RemoveDebuffs";
        helpProvider_0.SetShowHelp(RemoveDebuffs, true);
        RemoveDebuffs.Size = new Size(104, 17);
        RemoveDebuffs.TabIndex = 4;
        RemoveDebuffs.Text = "Remove debuffs";
        StrafeObstacles.AutoSize = true;
        helpProvider_0.SetHelpKeyword(StrafeObstacles, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(StrafeObstacles, HelpNavigator.Topic);
        StrafeObstacles.Location = new Point(166, 114);
        StrafeObstacles.Name = "StrafeObstacles";
        helpProvider_0.SetShowHelp(StrafeObstacles, true);
        StrafeObstacles.Size = new Size(138, 17);
        StrafeObstacles.TabIndex = 10;
        StrafeObstacles.Text = "Strafe around obstacles";
        BypassLootSanity.AutoSize = true;
        helpProvider_0.SetHelpKeyword(BypassLootSanity, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(BypassLootSanity, HelpNavigator.Topic);
        BypassLootSanity.Location = new Point(166, 138);
        BypassLootSanity.Name = "BypassLootSanity";
        helpProvider_0.SetShowHelp(BypassLootSanity, true);
        BypassLootSanity.Size = new Size(112, 17);
        BypassLootSanity.TabIndex = 11;
        BypassLootSanity.Text = "Skip sanity on loot";
        FightPlayers.AutoSize = true;
        helpProvider_0.SetHelpKeyword(FightPlayers, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(FightPlayers, HelpNavigator.Topic);
        FightPlayers.Location = new Point(166, 91);
        FightPlayers.Name = "FightPlayers";
        helpProvider_0.SetShowHelp(FightPlayers, true);
        FightPlayers.Size = new Size(149, 17);
        FightPlayers.TabIndex = 9;
        FightPlayers.Text = "Fight back against players";
        SitWhenBored.AutoSize = true;
        helpProvider_0.SetHelpKeyword(SitWhenBored, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(SitWhenBored, HelpNavigator.Topic);
        SitWhenBored.Location = new Point(20, 91);
        SitWhenBored.Name = "SitWhenBored";
        helpProvider_0.SetShowHelp(SitWhenBored, true);
        SitWhenBored.Size = new Size(97, 17);
        SitWhenBored.TabIndex = 3;
        SitWhenBored.Text = "Sit when bored";
        SkipLoot.AutoSize = true;
        helpProvider_0.SetHelpKeyword(SkipLoot, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(SkipLoot, HelpNavigator.Topic);
        SkipLoot.Location = new Point(20, 68);
        SkipLoot.Name = "SkipLoot";
        helpProvider_0.SetShowHelp(SkipLoot, true);
        SkipLoot.Size = new Size(94, 17);
        SkipLoot.TabIndex = 2;
        SkipLoot.Text = "Skip all looting";
        AutoSkin.AutoSize = true;
        helpProvider_0.SetHelpKeyword(AutoSkin, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(AutoSkin, HelpNavigator.Topic);
        AutoSkin.Location = new Point(166, 21);
        AutoSkin.Name = "AutoSkin";
        helpProvider_0.SetShowHelp(AutoSkin, true);
        AutoSkin.Size = new Size(87, 17);
        AutoSkin.TabIndex = 6;
        AutoSkin.Text = "Skin corpses";
        AutoSkin.CheckedChanged += AutoSkin_CheckedChanged;
        MediaKeysBox.AutoSize = true;
        helpProvider_0.SetHelpKeyword(MediaKeysBox, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(MediaKeysBox, HelpNavigator.Topic);
        helpProvider_0.SetHelpString(MediaKeysBox, "");
        MediaKeysBox.Location = new Point(20, 21);
        MediaKeysBox.Name = "MediaKeysBox";
        helpProvider_0.SetShowHelp(MediaKeysBox, true);
        MediaKeysBox.Size = new Size(101, 17);
        MediaKeysBox.TabIndex = 0;
        MediaKeysBox.Text = "Use media keys";
        ResetBuffs.AutoSize = true;
        helpProvider_0.SetHelpKeyword(ResetBuffs, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(ResetBuffs, HelpNavigator.Topic);
        ResetBuffs.Location = new Point(166, 68);
        ResetBuffs.Name = "ResetBuffs";
        helpProvider_0.SetShowHelp(ResetBuffs, true);
        ResetBuffs.Size = new Size(80, 17);
        ResetBuffs.TabIndex = 8;
        ResetBuffs.Text = "Reset buffs";
        WalkLoot.AutoSize = true;
        helpProvider_0.SetHelpKeyword(WalkLoot, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(WalkLoot, HelpNavigator.Topic);
        WalkLoot.Location = new Point(20, 44);
        WalkLoot.Name = "WalkLoot";
        helpProvider_0.SetShowHelp(WalkLoot, true);
        WalkLoot.Size = new Size(83, 17);
        WalkLoot.TabIndex = 1;
        WalkLoot.Text = "Walk to loot";
        helpProvider_0.HelpNamespace = "Glider.chm";
        helpProvider_0.SetHelpKeyword(MyHelpButton, "General.html");
        helpProvider_0.SetHelpNavigator(MyHelpButton, HelpNavigator.Topic);
        MyHelpButton.Location = new Point(562, 302);
        MyHelpButton.Name = "MyHelpButton";
        helpProvider_0.SetShowHelp(MyHelpButton, true);
        MyHelpButton.Size = new Size(71, 25);
        MyHelpButton.TabIndex = 2;
        MyHelpButton.Text = "Help";
        MyHelpButton.Click += MyHelpButton_Click;
        helpProvider_0.SetHelpKeyword(ProductKeyBox, "General.html");
        helpProvider_0.SetHelpNavigator(ProductKeyBox, HelpNavigator.Topic);
        ProductKeyBox.Location = new Point(16, 48);
        ProductKeyBox.Name = "ProductKeyBox";
        helpProvider_0.SetShowHelp(ProductKeyBox, true);
        ProductKeyBox.Size = new Size(124, 20);
        ProductKeyBox.TabIndex = 0;
        helpProvider_0.SetHelpKeyword(StopAfterMinutes, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(StopAfterMinutes, HelpNavigator.Topic);
        StopAfterMinutes.Location = new Point(98, 28);
        StopAfterMinutes.Name = "StopAfterMinutes";
        helpProvider_0.SetShowHelp(StopAfterMinutes, true);
        StopAfterMinutes.Size = new Size(49, 20);
        StopAfterMinutes.TabIndex = 1;
        helpProvider_0.SetHelpKeyword(RestHealth, "Limits.html");
        helpProvider_0.SetHelpNavigator(RestHealth, HelpNavigator.Topic);
        RestHealth.Location = new Point(88, 24);
        RestHealth.MaxLength = 3;
        RestHealth.Name = "RestHealth";
        helpProvider_0.SetShowHelp(RestHealth, true);
        RestHealth.Size = new Size(48, 20);
        RestHealth.TabIndex = 0;
        helpProvider_0.SetHelpKeyword(RestMana, "Limits.html");
        helpProvider_0.SetHelpNavigator(RestMana, HelpNavigator.Topic);
        RestMana.Location = new Point(254, 24);
        RestMana.MaxLength = 3;
        RestMana.Name = "RestMana";
        helpProvider_0.SetShowHelp(RestMana, true);
        RestMana.Size = new Size(56, 20);
        RestMana.TabIndex = 1;
        StopAfter.AutoSize = true;
        helpProvider_0.SetHelpKeyword(StopAfter, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(StopAfter, HelpNavigator.Topic);
        StopAfter.Location = new Point(13, 28);
        StopAfter.Name = "StopAfter";
        helpProvider_0.SetShowHelp(StopAfter, true);
        StopAfter.Size = new Size(75, 17);
        StopAfter.TabIndex = 0;
        StopAfter.Text = "Stop after:";
        StopAfter.CheckedChanged += StopAfter_CheckedChanged;
        helpProvider_0.SetHelpKeyword(ChatDelete, "Chat.html");
        helpProvider_0.SetHelpNavigator(ChatDelete, HelpNavigator.Topic);
        ChatDelete.Location = new Point(349, 18);
        ChatDelete.Name = "ChatDelete";
        helpProvider_0.SetShowHelp(ChatDelete, true);
        ChatDelete.Size = new Size(195, 22);
        ChatDelete.TabIndex = 2;
        ChatDelete.Text = "Delete prior chat log on start";
        helpProvider_0.SetHelpKeyword(PlayWhisper, "Chat.html");
        helpProvider_0.SetHelpNavigator(PlayWhisper, HelpNavigator.Topic);
        PlayWhisper.Location = new Point(16, 24);
        PlayWhisper.Name = "PlayWhisper";
        helpProvider_0.SetShowHelp(PlayWhisper, true);
        PlayWhisper.Size = new Size(144, 24);
        PlayWhisper.TabIndex = 0;
        PlayWhisper.Text = "Play sound on whisper";
        helpProvider_0.SetHelpKeyword(AutoReply, "Chat.html");
        helpProvider_0.SetHelpNavigator(AutoReply, HelpNavigator.Topic);
        AutoReply.Location = new Point(16, 48);
        AutoReply.Name = "AutoReply";
        helpProvider_0.SetShowHelp(AutoReply, true);
        AutoReply.Size = new Size(206, 19);
        AutoReply.TabIndex = 1;
        AutoReply.Text = "Auto-reply to GM:";
        helpProvider_0.SetHelpKeyword(AutoReplyText, "Chat.html");
        helpProvider_0.SetHelpNavigator(AutoReplyText, HelpNavigator.Topic);
        AutoReplyText.Location = new Point(32, 72);
        AutoReplyText.Name = "AutoReplyText";
        helpProvider_0.SetShowHelp(AutoReplyText, true);
        AutoReplyText.Size = new Size(272, 20);
        AutoReplyText.TabIndex = 2;
        helpProvider_0.SetHelpKeyword(KeyDelay, "Keys.html");
        helpProvider_0.SetHelpNavigator(KeyDelay, HelpNavigator.Topic);
        KeyDelay.Location = new Point(112, 24);
        KeyDelay.Name = "KeyDelay";
        helpProvider_0.SetShowHelp(KeyDelay, true);
        KeyDelay.Size = new Size(40, 20);
        KeyDelay.TabIndex = 0;
        helpProvider_0.SetHelpKeyword(BandageHealth, "Limits.html");
        helpProvider_0.SetHelpNavigator(BandageHealth, HelpNavigator.Topic);
        BandageHealth.Location = new Point(40, 72);
        BandageHealth.MaxLength = 3;
        BandageHealth.Name = "BandageHealth";
        helpProvider_0.SetShowHelp(BandageHealth, true);
        BandageHealth.Size = new Size(48, 20);
        BandageHealth.TabIndex = 2;
        helpProvider_0.SetHelpKeyword(UseBandages, "Limits.html");
        helpProvider_0.SetHelpNavigator(UseBandages, HelpNavigator.Topic);
        UseBandages.Location = new Point(24, 48);
        UseBandages.Name = "UseBandages";
        helpProvider_0.SetShowHelp(UseBandages, true);
        UseBandages.Size = new Size(112, 16);
        UseBandages.TabIndex = 1;
        UseBandages.Text = "Use bandages at:";
        UseBandages.CheckedChanged += UseBandages_CheckedChanged;
        helpProvider_0.SetHelpKeyword(FastEat, "Limits.html");
        helpProvider_0.SetHelpNavigator(FastEat, HelpNavigator.Topic);
        FastEat.Location = new Point(24, 24);
        FastEat.Name = "FastEat";
        helpProvider_0.SetShowHelp(FastEat, true);
        FastEat.Size = new Size(112, 16);
        FastEat.TabIndex = 0;
        FastEat.Text = "Fast eat/drink";
        UseClipboard.AutoSize = true;
        helpProvider_0.SetHelpKeyword(UseClipboard, "Keys.html");
        helpProvider_0.SetHelpNavigator(UseClipboard, HelpNavigator.Topic);
        UseClipboard.Location = new Point(32, 56);
        UseClipboard.Name = "UseClipboard";
        helpProvider_0.SetShowHelp(UseClipboard, true);
        UseClipboard.Size = new Size(163, 17);
        UseClipboard.TabIndex = 1;
        UseClipboard.Text = "Paste via Windows clipboard";
        UseClipboard.CheckedChanged += UseClipboard_CheckedChanged;
        LoadKeymap.AutoSize = true;
        helpProvider_0.SetHelpKeyword(LoadKeymap, "LoadingAndSaving.html");
        helpProvider_0.SetHelpNavigator(LoadKeymap, HelpNavigator.Topic);
        LoadKeymap.Location = new Point(121, 91);
        LoadKeymap.Name = "LoadKeymap";
        helpProvider_0.SetShowHelp(LoadKeymap, true);
        LoadKeymap.Size = new Size(96, 24);
        LoadKeymap.TabIndex = 1;
        LoadKeymap.Text = "Reload from disk";
        LoadKeymap.Click += LoadKeymap_Click;
        helpProvider_0.SetHelpKeyword(PartyAttackDelay, "Party.html#Follower");
        helpProvider_0.SetHelpNavigator(PartyAttackDelay, HelpNavigator.Topic);
        PartyAttackDelay.Location = new Point(93, 42);
        PartyAttackDelay.Name = "PartyAttackDelay";
        helpProvider_0.SetShowHelp(PartyAttackDelay, true);
        PartyAttackDelay.Size = new Size(40, 20);
        PartyAttackDelay.TabIndex = 1;
        helpProvider_0.SetHelpKeyword(PartyLeaderName, "Party.html#Follower");
        helpProvider_0.SetHelpNavigator(PartyLeaderName, HelpNavigator.Topic);
        PartyLeaderName.Location = new Point(93, 21);
        PartyLeaderName.Name = "PartyLeaderName";
        helpProvider_0.SetShowHelp(PartyLeaderName, true);
        PartyLeaderName.Size = new Size(74, 20);
        PartyLeaderName.TabIndex = 0;
        helpProvider_0.SetHelpKeyword(PartyLootPos, "Party.html#Options");
        helpProvider_0.SetHelpNavigator(PartyLootPos, HelpNavigator.Topic);
        PartyLootPos.Location = new Point(87, 83);
        PartyLootPos.Name = "PartyLootPos";
        helpProvider_0.SetShowHelp(PartyLootPos, true);
        PartyLootPos.Size = new Size(40, 20);
        PartyLootPos.TabIndex = 3;
        helpProvider_0.SetHelpKeyword(PartyAdds, "Party.html#Options");
        helpProvider_0.SetHelpNavigator(PartyAdds, HelpNavigator.Topic);
        PartyAdds.Location = new Point(27, 14);
        PartyAdds.Name = "PartyAdds";
        helpProvider_0.SetShowHelp(PartyAdds, true);
        PartyAdds.Size = new Size(100, 21);
        PartyAdds.TabIndex = 0;
        PartyAdds.Text = "Handle extras";
        helpProvider_0.SetHelpKeyword(PartyLooters, "Party.html#Options");
        helpProvider_0.SetHelpNavigator(PartyLooters, HelpNavigator.Topic);
        PartyLooters.Location = new Point(87, 62);
        PartyLooters.Name = "PartyLooters";
        helpProvider_0.SetShowHelp(PartyLooters, true);
        PartyLooters.Size = new Size(40, 20);
        PartyLooters.TabIndex = 2;
        helpProvider_0.SetHelpKeyword(PartyFollower, "Party.html#Mode");
        helpProvider_0.SetHelpNavigator(PartyFollower, HelpNavigator.Topic);
        PartyFollower.Location = new Point(20, 55);
        PartyFollower.Name = "PartyFollower";
        helpProvider_0.SetShowHelp(PartyFollower, true);
        PartyFollower.Size = new Size(107, 21);
        PartyFollower.TabIndex = 2;
        PartyFollower.Text = "Follower";
        PartyFollower.CheckedChanged += PartyFollower_CheckedChanged;
        helpProvider_0.SetHelpKeyword(PartyLeader, "Party.html#Mode");
        helpProvider_0.SetHelpNavigator(PartyLeader, HelpNavigator.Topic);
        PartyLeader.Location = new Point(20, 35);
        PartyLeader.Name = "PartyLeader";
        helpProvider_0.SetShowHelp(PartyLeader, true);
        PartyLeader.Size = new Size(107, 20);
        PartyLeader.TabIndex = 1;
        PartyLeader.Text = "Leader";
        PartyLeader.CheckedChanged += PartyLeader_CheckedChanged;
        helpProvider_0.SetHelpKeyword(PartySolo, "Party.html#Mode");
        helpProvider_0.SetHelpNavigator(PartySolo, HelpNavigator.Topic);
        PartySolo.Location = new Point(20, 14);
        PartySolo.Name = "PartySolo";
        helpProvider_0.SetShowHelp(PartySolo, true);
        PartySolo.Size = new Size(107, 21);
        PartySolo.TabIndex = 0;
        PartySolo.Text = "Solo";
        PartySolo.CheckedChanged += PartySolo_CheckedChanged;
        helpProvider_0.SetHelpKeyword(PartyMember1, "Party.html#Leader");
        helpProvider_0.SetHelpNavigator(PartyMember1, HelpNavigator.Topic);
        PartyMember1.Location = new Point(33, 42);
        PartyMember1.Name = "PartyMember1";
        helpProvider_0.SetShowHelp(PartyMember1, true);
        PartyMember1.Size = new Size(167, 20);
        PartyMember1.TabIndex = 0;
        PartyMember1.TextChanged += PartyMember1_TextChanged;
        helpProvider_0.SetHelpKeyword(PartyMember2, "Party.html#Leader");
        helpProvider_0.SetHelpNavigator(PartyMember2, HelpNavigator.Topic);
        PartyMember2.Location = new Point(33, 62);
        PartyMember2.Name = "PartyMember2";
        helpProvider_0.SetShowHelp(PartyMember2, true);
        PartyMember2.Size = new Size(167, 20);
        PartyMember2.TabIndex = 2;
        PartyMember2.TextChanged += PartyMember2_TextChanged;
        helpProvider_0.SetHelpKeyword(PartyMember3, "Party.html#Leader");
        helpProvider_0.SetHelpNavigator(PartyMember3, HelpNavigator.Topic);
        PartyMember3.Location = new Point(33, 83);
        PartyMember3.Name = "PartyMember3";
        helpProvider_0.SetShowHelp(PartyMember3, true);
        PartyMember3.Size = new Size(167, 20);
        PartyMember3.TabIndex = 4;
        PartyMember3.TextChanged += PartyMember3_TextChanged;
        helpProvider_0.SetHelpKeyword(PartyMember4, "Party.html#Leader");
        helpProvider_0.SetHelpNavigator(PartyMember4, HelpNavigator.Topic);
        PartyMember4.Location = new Point(33, 104);
        PartyMember4.Name = "PartyMember4";
        helpProvider_0.SetShowHelp(PartyMember4, true);
        PartyMember4.Size = new Size(167, 20);
        PartyMember4.TabIndex = 6;
        PartyMember4.TextChanged += PartyMember4_TextChanged;
        helpProvider_0.SetHelpKeyword(PartyBuff, "Party.html#Options");
        helpProvider_0.SetHelpNavigator(PartyBuff, HelpNavigator.Topic);
        PartyBuff.Location = new Point(27, 35);
        PartyBuff.Name = "PartyBuff";
        helpProvider_0.SetShowHelp(PartyBuff, true);
        PartyBuff.Size = new Size(100, 20);
        PartyBuff.TabIndex = 1;
        PartyBuff.Text = "Buff others";
        helpProvider_0.SetHelpKeyword(PartySlashFollow, "Party.html#Follower");
        helpProvider_0.SetHelpNavigator(PartySlashFollow, HelpNavigator.Topic);
        PartySlashFollow.Location = new Point(193, 21);
        PartySlashFollow.Name = "PartySlashFollow";
        helpProvider_0.SetShowHelp(PartySlashFollow, true);
        PartySlashFollow.Size = new Size(100, 21);
        PartySlashFollow.TabIndex = 2;
        PartySlashFollow.Text = "Use /follow";
        ListenPassword.Enabled = false;
        helpProvider_0.SetHelpKeyword(ListenPassword, "General.html#Remote");
        helpProvider_0.SetHelpNavigator(ListenPassword, HelpNavigator.Topic);
        ListenPassword.Location = new Point(96, 48);
        ListenPassword.Name = "ListenPassword";
        helpProvider_0.SetShowHelp(ListenPassword, true);
        ListenPassword.Size = new Size(80, 20);
        ListenPassword.TabIndex = 2;
        ListenPort.Enabled = false;
        helpProvider_0.SetHelpKeyword(ListenPort, "General.html#Remote");
        helpProvider_0.SetHelpNavigator(ListenPort, HelpNavigator.Topic);
        ListenPort.Location = new Point(96, 27);
        ListenPort.Name = "ListenPort";
        helpProvider_0.SetShowHelp(ListenPort, true);
        ListenPort.Size = new Size(40, 20);
        ListenPort.TabIndex = 1;
        helpProvider_0.SetHelpKeyword(ListenEnabled, "General.html#Remote");
        helpProvider_0.SetHelpNavigator(ListenEnabled, HelpNavigator.Topic);
        ListenEnabled.Location = new Point(65, 85);
        ListenEnabled.Name = "ListenEnabled";
        helpProvider_0.SetShowHelp(ListenEnabled, true);
        ListenEnabled.Size = new Size(100, 21);
        ListenEnabled.TabIndex = 0;
        ListenEnabled.Text = "Enabled";
        ListenEnabled.CheckedChanged += ListenEnabled_CheckedChanged;
        StopWhenFull.AutoSize = true;
        helpProvider_0.SetHelpKeyword(StopWhenFull, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(StopWhenFull, HelpNavigator.Topic);
        StopWhenFull.Location = new Point(232, 28);
        StopWhenFull.Name = "StopWhenFull";
        helpProvider_0.SetShowHelp(StopWhenFull, true);
        StopWhenFull.Size = new Size(182, 17);
        StopWhenFull.TabIndex = 2;
        StopWhenFull.Text = "Stop gliding when inventory is full";
        helpProvider_0.SetHelpKeyword(PlaySay, "Chat.html");
        helpProvider_0.SetHelpNavigator(PlaySay, HelpNavigator.Topic);
        PlaySay.Location = new Point(240, 24);
        PlaySay.Name = "PlaySay";
        helpProvider_0.SetShowHelp(PlaySay, true);
        PlaySay.Size = new Size(207, 24);
        PlaySay.TabIndex = 3;
        PlaySay.Text = "Play sound on say";
        helpProvider_0.SetHelpKeyword(CombatLogFrame, "Chat.html");
        helpProvider_0.SetHelpNavigator(CombatLogFrame, HelpNavigator.Topic);
        CombatLogFrame.Location = new Point(143, 47);
        CombatLogFrame.Name = "CombatLogFrame";
        helpProvider_0.SetShowHelp(CombatLogFrame, true);
        CombatLogFrame.Size = new Size(131, 20);
        CombatLogFrame.TabIndex = 1;
        helpProvider_0.SetHelpKeyword(ChatLogFrame, "Chat.html");
        helpProvider_0.SetHelpNavigator(ChatLogFrame, HelpNavigator.Topic);
        ChatLogFrame.Location = new Point(143, 23);
        ChatLogFrame.Name = "ChatLogFrame";
        helpProvider_0.SetShowHelp(ChatLogFrame, true);
        ChatLogFrame.Size = new Size(131, 20);
        ChatLogFrame.TabIndex = 0;
        helpProvider_0.SetHelpKeyword(BackgroundEnable, "Background.html");
        helpProvider_0.SetHelpNavigator(BackgroundEnable, HelpNavigator.Topic);
        BackgroundEnable.Location = new Point(13, 27);
        BackgroundEnable.Name = "BackgroundEnable";
        helpProvider_0.SetShowHelp(BackgroundEnable, true);
        BackgroundEnable.Size = new Size(140, 23);
        BackgroundEnable.TabIndex = 0;
        BackgroundEnable.Text = "Background enable";
        BackgroundEnable.CheckedChanged += BackgroundEnable_CheckedChanged;
        TabGeneral.Controls.Add(groupBox31);
        TabGeneral.Controls.Add(groupBox19);
        TabGeneral.Controls.Add(groupBox7);
        TabGeneral.Controls.Add(groupBox3);
        TabGeneral.Controls.Add(groupBox1);
        helpProvider_0.SetHelpNavigator(TabGeneral, HelpNavigator.TopicId);
        helpProvider_0.SetHelpString(TabGeneral, "General.html");
        TabGeneral.Location = new Point(4, 22);
        TabGeneral.Name = "TabGeneral";
        helpProvider_0.SetShowHelp(TabGeneral, true);
        TabGeneral.Size = new Size(636, 263);
        TabGeneral.TabIndex = 0;
        TabGeneral.Text = "General";
        TabGeneral.UseVisualStyleBackColor = true;
        groupBox31.Controls.Add(label26);
        groupBox31.Controls.Add(AutoLogCharacter);
        groupBox31.Controls.Add(ButtonViewCharacters);
        groupBox31.Controls.Add(AccountCreate);
        groupBox31.Location = new Point(267, 137);
        groupBox31.Name = "groupBox31";
        groupBox31.Size = new Size(348, 112);
        groupBox31.TabIndex = 10;
        groupBox31.TabStop = false;
        groupBox31.Text = "Auto Login";
        label26.AutoSize = true;
        label26.Location = new Point(31, 33);
        label26.Name = "label26";
        label26.Size = new Size(109, 13);
        label26.TabIndex = 4;
        label26.Text = "Auto Login character:";
        AutoLogCharacter.DropDownStyle = ComboBoxStyle.DropDownList;
        AutoLogCharacter.FormattingEnabled = true;
        AutoLogCharacter.Location = new Point(34, 52);
        AutoLogCharacter.Name = "AutoLogCharacter";
        AutoLogCharacter.Size = new Size(147, 21);
        AutoLogCharacter.TabIndex = 3;
        AutoLogCharacter.SelectedIndexChanged += AutoLogCharacter_SelectedIndexChanged;
        ButtonViewCharacters.Location = new Point(219, 70);
        ButtonViewCharacters.Name = "ButtonViewCharacters";
        ButtonViewCharacters.Size = new Size(116, 25);
        ButtonViewCharacters.TabIndex = 1;
        ButtonViewCharacters.Text = "View Characters";
        ButtonViewCharacters.UseVisualStyleBackColor = true;
        ButtonViewCharacters.Click += ButtonViewCharacters_Click;
        AccountCreate.Location = new Point(219, 27);
        AccountCreate.Name = "AccountCreate";
        AccountCreate.Size = new Size(116, 25);
        AccountCreate.TabIndex = 0;
        AccountCreate.Text = "Create Character";
        AccountCreate.UseVisualStyleBackColor = true;
        AccountCreate.Click += AccountCreate_Click;
        groupBox19.Controls.Add(ListenPassword);
        groupBox19.Controls.Add(ListenPort);
        groupBox19.Controls.Add(ListenEnabled);
        groupBox19.Controls.Add(label42);
        groupBox19.Controls.Add(label41);
        groupBox19.Location = new Point(426, 8);
        groupBox19.Name = "groupBox19";
        groupBox19.Size = new Size(191, 128);
        groupBox19.TabIndex = 9;
        groupBox19.TabStop = false;
        groupBox19.Text = "Remote Control";
        label42.Location = new Point(16, 27);
        label42.Name = "label42";
        label42.Size = new Size(73, 14);
        label42.TabIndex = 1;
        label42.Text = "Port:";
        label42.TextAlign = ContentAlignment.TopRight;
        label41.Location = new Point(16, 48);
        label41.Name = "label41";
        label41.Size = new Size(73, 14);
        label41.TabIndex = 0;
        label41.Text = "Password:";
        label41.TextAlign = ContentAlignment.TopRight;
        groupBox7.Controls.Add(WowVersionLabel);
        groupBox7.Controls.Add(GliderVersionLabel);
        groupBox7.Controls.Add(linkLabel1);
        groupBox7.Controls.Add(label10);
        groupBox7.Controls.Add(label9);
        groupBox7.Location = new Point(16, 137);
        groupBox7.Name = "groupBox7";
        groupBox7.Size = new Size(246, 112);
        groupBox7.TabIndex = 8;
        groupBox7.TabStop = false;
        groupBox7.Text = "Information";
        WowVersionLabel.AutoSize = true;
        WowVersionLabel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
        WowVersionLabel.ForeColor = SystemColors.Highlight;
        WowVersionLabel.Location = new Point(92, 55);
        WowVersionLabel.Name = "WowVersionLabel";
        WowVersionLabel.Size = new Size(21, 13);
        WowVersionLabel.TabIndex = 4;
        WowVersionLabel.Text = "??";
        GliderVersionLabel.AutoSize = true;
        GliderVersionLabel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
        GliderVersionLabel.ForeColor = SystemColors.Highlight;
        GliderVersionLabel.Location = new Point(92, 27);
        GliderVersionLabel.Name = "GliderVersionLabel";
        GliderVersionLabel.Size = new Size(21, 13);
        GliderVersionLabel.TabIndex = 3;
        GliderVersionLabel.Text = "??";
        linkLabel1.Location = new Point(7, 93);
        linkLabel1.Name = "linkLabel1";
        linkLabel1.Size = new Size(217, 15);
        linkLabel1.TabIndex = 2;
        linkLabel1.TabStop = true;
        linkLabel1.Text = "http://www.mmoglider.com";
        linkLabel1.TextAlign = ContentAlignment.TopCenter;
        linkLabel1.LinkClicked += linkLabel1_LinkClicked;
        label10.AutoSize = true;
        label10.Location = new Point(7, 55);
        label10.Name = "label10";
        label10.Size = new Size(75, 13);
        label10.TabIndex = 1;
        label10.Text = "WoW version:";
        label10.TextAlign = ContentAlignment.TopRight;
        label9.AutoSize = true;
        label9.Location = new Point(7, 28);
        label9.Name = "label9";
        label9.Size = new Size(74, 13);
        label9.TabIndex = 0;
        label9.Text = "Glider version:";
        label9.TextAlign = ContentAlignment.TopRight;
        groupBox3.Controls.Add(ProductKeyBox);
        groupBox3.Controls.Add(label1);
        groupBox3.Location = new Point(267, 8);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(153, 128);
        groupBox3.TabIndex = 1;
        groupBox3.TabStop = false;
        groupBox3.Text = "Registration";
        label1.Location = new Point(16, 32);
        label1.Name = "label1";
        label1.Size = new Size(72, 16);
        label1.TabIndex = 6;
        label1.Text = "Product key:";
        TabLimits.Controls.Add(groupBox32);
        TabLimits.Controls.Add(groupBox22);
        TabLimits.Controls.Add(groupBox23);
        TabLimits.Controls.Add(groupBox13);
        TabLimits.Controls.Add(groupBox5);
        helpProvider_0.SetHelpNavigator(TabLimits, HelpNavigator.TopicId);
        helpProvider_0.SetHelpString(TabLimits, "Limits.html");
        TabLimits.Location = new Point(4, 22);
        TabLimits.Name = "TabLimits";
        helpProvider_0.SetShowHelp(TabLimits, true);
        TabLimits.Size = new Size(636, 263);
        TabLimits.TabIndex = 3;
        TabLimits.Text = "Limits";
        TabLimits.UseVisualStyleBackColor = true;
        groupBox32.Controls.Add(AmmoAmount);
        groupBox32.Controls.Add(label60);
        groupBox32.Controls.Add(WaterAmount);
        groupBox32.Controls.Add(FoodAmount);
        groupBox32.Controls.Add(label3);
        groupBox32.Controls.Add(label2);
        groupBox32.Location = new Point(359, 70);
        groupBox32.Name = "groupBox32";
        groupBox32.Size = new Size(256, 177);
        groupBox32.TabIndex = 16;
        groupBox32.TabStop = false;
        groupBox32.Text = "Vendoring";
        helpProvider_0.SetHelpKeyword(AmmoAmount, "Limits.html");
        helpProvider_0.SetHelpNavigator(AmmoAmount, HelpNavigator.Topic);
        AmmoAmount.Location = new Point(106, 101);
        AmmoAmount.Name = "AmmoAmount";
        helpProvider_0.SetShowHelp(AmmoAmount, true);
        AmmoAmount.Size = new Size(60, 20);
        AmmoAmount.TabIndex = 18;
        label60.AutoSize = true;
        label60.Location = new Point(23, 104);
        label60.Name = "label60";
        label60.Size = new Size(77, 13);
        label60.TabIndex = 17;
        label60.Text = "Ammo amount:";
        label60.TextAlign = ContentAlignment.TopRight;
        helpProvider_0.SetHelpKeyword(WaterAmount, "Limits.html");
        helpProvider_0.SetHelpNavigator(WaterAmount, HelpNavigator.Topic);
        WaterAmount.Location = new Point(106, 53);
        WaterAmount.Name = "WaterAmount";
        helpProvider_0.SetShowHelp(WaterAmount, true);
        WaterAmount.Size = new Size(60, 20);
        WaterAmount.TabIndex = 16;
        helpProvider_0.SetHelpKeyword(FoodAmount, "Limits.html");
        helpProvider_0.SetHelpNavigator(FoodAmount, HelpNavigator.Topic);
        FoodAmount.Location = new Point(106, 30);
        FoodAmount.Name = "FoodAmount";
        helpProvider_0.SetShowHelp(FoodAmount, true);
        FoodAmount.Size = new Size(60, 20);
        FoodAmount.TabIndex = 15;
        label3.AutoSize = true;
        label3.Location = new Point(23, 57);
        label3.Name = "label3";
        label3.Size = new Size(77, 13);
        label3.TabIndex = 2;
        label3.Text = "Water amount:";
        label3.TextAlign = ContentAlignment.TopRight;
        label2.AutoSize = true;
        label2.Location = new Point(28, 33);
        label2.Name = "label2";
        label2.Size = new Size(72, 13);
        label2.TabIndex = 1;
        label2.Text = "Food amount:";
        label2.TextAlign = ContentAlignment.TopRight;
        groupBox22.Controls.Add(MaxResurrect);
        groupBox22.Controls.Add(ResLabel);
        groupBox22.Controls.Add(Resurrect);
        groupBox22.Location = new Point(172, 69);
        groupBox22.Name = "groupBox22";
        groupBox22.Size = new Size(181, 178);
        groupBox22.TabIndex = 15;
        groupBox22.TabStop = false;
        groupBox22.Text = "Resurrection";
        helpProvider_0.SetHelpKeyword(MaxResurrect, "Limits.html");
        helpProvider_0.SetHelpNavigator(MaxResurrect, HelpNavigator.Topic);
        MaxResurrect.Location = new Point(9, 121);
        MaxResurrect.Name = "MaxResurrect";
        helpProvider_0.SetShowHelp(MaxResurrect, true);
        MaxResurrect.Size = new Size(60, 20);
        MaxResurrect.TabIndex = 14;
        ResLabel.AutoSize = true;
        ResLabel.Location = new Point(6, 105);
        ResLabel.Name = "ResLabel";
        ResLabel.Size = new Size(103, 13);
        ResLabel.TabIndex = 13;
        ResLabel.Text = "Stop after this many:";
        helpProvider_0.SetHelpKeyword(Resurrect, "Limits.html");
        helpProvider_0.SetHelpNavigator(Resurrect, HelpNavigator.Topic);
        Resurrect.Location = new Point(8, 34);
        Resurrect.Name = "Resurrect";
        helpProvider_0.SetShowHelp(Resurrect, true);
        Resurrect.Size = new Size(137, 54);
        Resurrect.TabIndex = 9;
        Resurrect.Text = "Auto resurrect if profile has ghost waypoints";
        Resurrect.CheckedChanged += Resurrect_CheckedChanged;
        groupBox23.Controls.Add(DebuffsKnown);
        groupBox23.Controls.Add(EditDebuffs);
        groupBox23.Location = new Point(358, 8);
        groupBox23.Name = "groupBox23";
        groupBox23.Size = new Size(259, 56);
        groupBox23.TabIndex = 14;
        groupBox23.TabStop = false;
        groupBox23.Text = "Debuff Removal";
        DebuffsKnown.AutoSize = true;
        DebuffsKnown.ForeColor = SystemColors.Highlight;
        DebuffsKnown.Location = new Point(15, 23);
        DebuffsKnown.Name = "DebuffsKnown";
        DebuffsKnown.Size = new Size(43, 13);
        DebuffsKnown.TabIndex = 1;
        DebuffsKnown.Text = "??????";
        EditDebuffs.Location = new Point(sbyte.MaxValue, 18);
        EditDebuffs.Name = "EditDebuffs";
        EditDebuffs.Size = new Size(76, 22);
        EditDebuffs.TabIndex = 0;
        EditDebuffs.Text = "Manage";
        EditDebuffs.UseVisualStyleBackColor = true;
        EditDebuffs.Click += EditDebuffs_Click;
        groupBox13.Controls.Add(label17);
        groupBox13.Controls.Add(BandageHealth);
        groupBox13.Controls.Add(UseBandages);
        groupBox13.Controls.Add(FastEat);
        groupBox13.Location = new Point(7, 69);
        groupBox13.Name = "groupBox13";
        groupBox13.Size = new Size(159, 178);
        groupBox13.TabIndex = 11;
        groupBox13.TabStop = false;
        groupBox13.Text = "Rest Options";
        label17.Location = new Point(96, 72);
        label17.Name = "label17";
        label17.Size = new Size(48, 16);
        label17.TabIndex = 7;
        label17.Text = "health";
        label17.TextAlign = ContentAlignment.BottomLeft;
        groupBox5.Controls.Add(label6);
        groupBox5.Controls.Add(label5);
        groupBox5.Controls.Add(RestMana);
        groupBox5.Controls.Add(RestHealth);
        groupBox5.Location = new Point(7, 7);
        groupBox5.Name = "groupBox5";
        groupBox5.Size = new Size(346, 56);
        groupBox5.TabIndex = 9;
        groupBox5.TabStop = false;
        groupBox5.Text = "Rest Percentages";
        label6.Location = new Point(171, 24);
        label6.Name = "label6";
        label6.Size = new Size(78, 16);
        label6.TabIndex = 3;
        label6.Text = "Mana:";
        label6.TextAlign = ContentAlignment.BottomRight;
        label5.Location = new Point(7, 24);
        label5.Name = "label5";
        label5.Size = new Size(76, 16);
        label5.TabIndex = 0;
        label5.Text = "Health:";
        label5.TextAlign = ContentAlignment.BottomRight;
        TabDetection.Controls.Add(groupBox21);
        TabDetection.Controls.Add(groupBox9);
        TabDetection.Controls.Add(groupBox14);
        helpProvider_0.SetHelpNavigator(TabDetection, HelpNavigator.TopicId);
        helpProvider_0.SetHelpString(TabDetection, "Detection.html");
        TabDetection.Location = new Point(4, 22);
        TabDetection.Name = "TabDetection";
        helpProvider_0.SetShowHelp(TabDetection, true);
        TabDetection.Size = new Size(636, 263);
        TabDetection.TabIndex = 6;
        TabDetection.Text = "Detection";
        TabDetection.UseVisualStyleBackColor = true;
        groupBox21.Controls.Add(AllowAutoSecCheck);
        groupBox21.Controls.Add(DoSecCheck);
        groupBox21.Controls.Add(AllowNetCheck);
        groupBox21.Controls.Add(StopOnVanish);
        groupBox21.Controls.Add(AllowWW);
        groupBox21.Controls.Add(TeleportLogout);
        groupBox21.Controls.Add(TeleportStop);
        groupBox21.Location = new Point(357, 8);
        groupBox21.Name = "groupBox21";
        groupBox21.Size = new Size(262, 235);
        groupBox21.TabIndex = 9;
        groupBox21.TabStop = false;
        groupBox21.Text = "Other";
        AllowAutoSecCheck.AutoSize = true;
        AllowAutoSecCheck.Location = new Point(12, 139);
        AllowAutoSecCheck.Name = "AllowAutoSecCheck";
        AllowAutoSecCheck.Size = new Size(188, 17);
        AllowAutoSecCheck.TabIndex = 6;
        AllowAutoSecCheck.Text = "Prompt for SecCheck occasionally";
        DoSecCheck.Location = new Point(80, 191);
        DoSecCheck.Name = "DoSecCheck";
        DoSecCheck.Size = new Size(117, 25);
        DoSecCheck.TabIndex = 3;
        DoSecCheck.Text = "SecCheck";
        DoSecCheck.Click += DoSecCheck_Click;
        AllowNetCheck.AutoSize = true;
        AllowNetCheck.Location = new Point(12, 116);
        AllowNetCheck.Name = "AllowNetCheck";
        AllowNetCheck.Size = new Size(115, 17);
        AllowNetCheck.TabIndex = 5;
        AllowNetCheck.Text = "NetCheck enabled";
        StopOnVanish.AutoSize = true;
        StopOnVanish.Location = new Point(12, 93);
        StopOnVanish.Name = "StopOnVanish";
        StopOnVanish.Size = new Size(151, 17);
        StopOnVanish.TabIndex = 4;
        StopOnVanish.Text = "Stop on vanishing monster";
        AllowWW.AutoSize = true;
        AllowWW.Location = new Point(12, 23);
        AllowWW.Name = "AllowWW";
        AllowWW.Size = new Size(99, 17);
        AllowWW.TabIndex = 1;
        AllowWW.Text = "Enable Tripwire";
        AllowWW.UseVisualStyleBackColor = true;
        AllowWW.CheckedChanged += AllowWW_CheckedChanged;
        TeleportLogout.AutoSize = true;
        TeleportLogout.Enabled = false;
        TeleportLogout.Location = new Point(12, 69);
        TeleportLogout.Name = "TeleportLogout";
        TeleportLogout.Size = new Size(134, 17);
        TeleportLogout.TabIndex = 3;
        TeleportLogout.Text = "Also log out on teleport";
        TeleportStop.AutoSize = true;
        TeleportStop.Location = new Point(12, 46);
        TeleportStop.Name = "TeleportStop";
        TeleportStop.Size = new Size(160, 17);
        TeleportStop.TabIndex = 2;
        TeleportStop.Text = "Stop on unexpected teleport";
        groupBox9.Controls.Add(Strafe);
        groupBox9.Controls.Add(JumpMore);
        groupBox9.Location = new Point(7, 137);
        groupBox9.Name = "groupBox9";
        groupBox9.Size = new Size(342, 106);
        groupBox9.TabIndex = 6;
        groupBox9.TabStop = false;
        groupBox9.Text = "Movement";
        Strafe.AutoSize = true;
        Strafe.Location = new Point(13, 42);
        Strafe.Name = "Strafe";
        Strafe.Size = new Size(54, 17);
        Strafe.TabIndex = 1;
        Strafe.Text = "Strafe";
        JumpMore.AutoSize = true;
        JumpMore.Location = new Point(13, 21);
        JumpMore.Name = "JumpMore";
        JumpMore.Size = new Size(77, 17);
        JumpMore.TabIndex = 0;
        JumpMore.Text = "Jump more";
        groupBox14.Controls.Add(label50);
        groupBox14.Controls.Add(MaxPopups);
        groupBox14.Controls.Add(label49);
        groupBox14.Controls.Add(AvoidOtherFaction);
        groupBox14.Controls.Add(AvoidSameFaction);
        groupBox14.Controls.Add(label21);
        groupBox14.Controls.Add(FriendLogout);
        groupBox14.Controls.Add(label20);
        groupBox14.Controls.Add(label19);
        groupBox14.Controls.Add(FriendAlert);
        groupBox14.Controls.Add(label18);
        groupBox14.Location = new Point(7, 8);
        groupBox14.Name = "groupBox14";
        groupBox14.Size = new Size(344, 123);
        groupBox14.TabIndex = 4;
        groupBox14.TabStop = false;
        groupBox14.Text = "Followers";
        label50.AutoSize = true;
        label50.Location = new Point(196, 68);
        label50.Name = "label50";
        label50.Size = new Size(48, 13);
        label50.TabIndex = 9;
        label50.Text = "windows";
        MaxPopups.Location = new Point(155, 65);
        MaxPopups.Name = "MaxPopups";
        MaxPopups.Size = new Size(32, 20);
        MaxPopups.TabIndex = 8;
        label49.Location = new Point(90, 68);
        label49.Name = "label49";
        label49.Size = new Size(60, 15);
        label49.TabIndex = 7;
        label49.Text = "Popups:";
        label49.TextAlign = ContentAlignment.TopRight;
        AvoidOtherFaction.Location = new Point(202, 101);
        AvoidOtherFaction.Name = "AvoidOtherFaction";
        AvoidOtherFaction.Size = new Size(140, 17);
        AvoidOtherFaction.TabIndex = 3;
        AvoidOtherFaction.Text = "Avoid other faction";
        AvoidSameFaction.Location = new Point(30, 101);
        AvoidSameFaction.Name = "AvoidSameFaction";
        AvoidSameFaction.Size = new Size(140, 17);
        AvoidSameFaction.TabIndex = 2;
        AvoidSameFaction.Text = "Avoid same faction";
        label21.Location = new Point(195, 44);
        label21.Name = "label21";
        label21.Size = new Size(48, 16);
        label21.TabIndex = 6;
        label21.Text = "minutes";
        FriendLogout.Location = new Point(155, 41);
        FriendLogout.Name = "FriendLogout";
        FriendLogout.Size = new Size(32, 20);
        FriendLogout.TabIndex = 1;
        label20.Location = new Point(40, 44);
        label20.Name = "label20";
        label20.Size = new Size(110, 16);
        label20.TabIndex = 4;
        label20.Text = "Log out after:";
        label20.TextAlign = ContentAlignment.TopRight;
        label19.Location = new Point(195, 16);
        label19.Name = "label19";
        label19.Size = new Size(48, 16);
        label19.TabIndex = 3;
        label19.Text = "minutes";
        FriendAlert.Location = new Point(155, 16);
        FriendAlert.Name = "FriendAlert";
        FriendAlert.Size = new Size(32, 20);
        FriendAlert.TabIndex = 0;
        label18.Location = new Point(40, 16);
        label18.Name = "label18";
        label18.Size = new Size(110, 16);
        label18.TabIndex = 0;
        label18.Text = "Alert after:";
        label18.TextAlign = ContentAlignment.TopRight;
        TabKeys.Controls.Add(groupBox27);
        TabKeys.Controls.Add(groupBox15);
        TabKeys.Controls.Add(groupBox8);
        helpProvider_0.SetHelpNavigator(TabKeys, HelpNavigator.TopicId);
        helpProvider_0.SetHelpString(TabKeys, "Keys.html");
        TabKeys.Location = new Point(4, 22);
        TabKeys.Name = "TabKeys";
        helpProvider_0.SetShowHelp(TabKeys, true);
        TabKeys.Size = new Size(636, 263);
        TabKeys.TabIndex = 5;
        TabKeys.Text = "Keys";
        TabKeys.UseVisualStyleBackColor = true;
        groupBox27.Controls.Add(MouseSpin);
        groupBox27.Controls.Add(label48);
        groupBox27.Controls.Add(PawSpeed);
        groupBox27.Controls.Add(label40);
        groupBox27.Location = new Point(335, 96);
        groupBox27.Name = "groupBox27";
        groupBox27.Size = new Size(282, 131);
        groupBox27.TabIndex = 3;
        groupBox27.TabStop = false;
        groupBox27.Text = "Mouse";
        MouseSpin.AutoSize = true;
        MouseSpin.Location = new Point(18, 77);
        MouseSpin.Name = "MouseSpin";
        MouseSpin.Size = new Size(103, 17);
        MouseSpin.TabIndex = 4;
        MouseSpin.Text = "Spin with mouse";
        MouseSpin.UseVisualStyleBackColor = true;
        label48.Location = new Point(165, 37);
        label48.Name = "label48";
        label48.Size = new Size(72, 16);
        label48.TabIndex = 3;
        label48.Text = "milliseconds";
        helpProvider_0.SetHelpKeyword(PawSpeed, "Keys.html");
        helpProvider_0.SetHelpNavigator(PawSpeed, HelpNavigator.Topic);
        PawSpeed.Location = new Point(120, 35);
        PawSpeed.Name = "PawSpeed";
        helpProvider_0.SetShowHelp(PawSpeed, true);
        PawSpeed.Size = new Size(40, 20);
        PawSpeed.TabIndex = 1;
        label40.AutoSize = true;
        label40.Location = new Point(16, 37);
        label40.Name = "label40";
        label40.Size = new Size(91, 13);
        label40.TabIndex = 0;
        label40.Text = "Paw speed delay:";
        label40.TextAlign = ContentAlignment.TopRight;
        groupBox15.Controls.Add(EditKeymap);
        groupBox15.Controls.Add(KeyEditClass);
        groupBox15.Controls.Add(label61);
        groupBox15.Controls.Add(LoadKeymap);
        groupBox15.Location = new Point(8, 96);
        groupBox15.Name = "groupBox15";
        groupBox15.Size = new Size(322, 131);
        groupBox15.TabIndex = 2;
        groupBox15.TabStop = false;
        groupBox15.Text = "Key Mapping";
        EditKeymap.AutoSize = true;
        EditKeymap.Enabled = false;
        helpProvider_0.SetHelpKeyword(EditKeymap, "LoadingAndSaving.html");
        helpProvider_0.SetHelpNavigator(EditKeymap, HelpNavigator.Topic);
        EditKeymap.Location = new Point(191, 40);
        EditKeymap.Name = "EditKeymap";
        helpProvider_0.SetShowHelp(EditKeymap, true);
        EditKeymap.Size = new Size(83, 24);
        EditKeymap.TabIndex = 4;
        EditKeymap.Text = "Edit";
        EditKeymap.Click += EditKeymap_Click;
        KeyEditClass.DropDownStyle = ComboBoxStyle.DropDownList;
        KeyEditClass.FormattingEnabled = true;
        KeyEditClass.Location = new Point(18, 43);
        KeyEditClass.Name = "KeyEditClass";
        KeyEditClass.Size = new Size(158, 21);
        KeyEditClass.Sorted = true;
        KeyEditClass.TabIndex = 3;
        KeyEditClass.SelectedIndexChanged += KeyEditClass_SelectedIndexChanged;
        label61.AutoSize = true;
        label61.Location = new Point(15, 27);
        label61.Name = "label61";
        label61.Size = new Size(35, 13);
        label61.TabIndex = 2;
        label61.Text = "Class:";
        groupBox8.Controls.Add(label58);
        groupBox8.Controls.Add(label59);
        groupBox8.Controls.Add(SpellLeadDelay);
        groupBox8.Controls.Add(UseHook);
        groupBox8.Controls.Add(UseClipboard);
        groupBox8.Controls.Add(label12);
        groupBox8.Controls.Add(KeyDelay);
        groupBox8.Controls.Add(label11);
        groupBox8.Location = new Point(8, 8);
        groupBox8.Name = "groupBox8";
        groupBox8.Size = new Size(609, 80);
        groupBox8.TabIndex = 0;
        groupBox8.TabStop = false;
        groupBox8.Text = "Timing";
        label58.Location = new Point(254, 24);
        label58.Name = "label58";
        label58.Size = new Size(157, 16);
        label58.TabIndex = 15;
        label58.Text = "Spell lead delay:";
        label58.TextAlign = ContentAlignment.TopRight;
        label59.Location = new Point(486, 24);
        label59.Name = "label59";
        label59.Size = new Size(31, 16);
        label59.TabIndex = 16;
        label59.Text = "ms";
        helpProvider_0.SetHelpKeyword(SpellLeadDelay, "Miscellaneous.html");
        helpProvider_0.SetHelpNavigator(SpellLeadDelay, HelpNavigator.Topic);
        SpellLeadDelay.Location = new Point(416, 21);
        SpellLeadDelay.Name = "SpellLeadDelay";
        helpProvider_0.SetShowHelp(SpellLeadDelay, true);
        SpellLeadDelay.Size = new Size(65, 20);
        SpellLeadDelay.TabIndex = 17;
        UseHook.AutoSize = true;
        UseHook.Location = new Point(346, 56);
        UseHook.Name = "UseHook";
        UseHook.Size = new Size(sbyte.MaxValue, 17);
        UseHook.TabIndex = 2;
        UseHook.Text = "Install keyboard hook";
        UseHook.UseVisualStyleBackColor = true;
        label12.Location = new Point(160, 24);
        label12.Name = "label12";
        label12.Size = new Size(72, 16);
        label12.TabIndex = 2;
        label12.Text = "milliseconds";
        label11.Location = new Point(8, 24);
        label11.Name = "label11";
        label11.Size = new Size(96, 16);
        label11.TabIndex = 0;
        label11.Text = "Keystroke delay:";
        label11.TextAlign = ContentAlignment.TopRight;
        TabDistances.Controls.Add(groupBox30);
        TabDistances.Controls.Add(groupBox12);
        TabDistances.Controls.Add(groupBox18);
        TabDistances.Controls.Add(groupBox16);
        helpProvider_0.SetHelpNavigator(TabDistances, HelpNavigator.TopicId);
        helpProvider_0.SetHelpString(TabDistances, "Distances.html");
        TabDistances.Location = new Point(4, 22);
        TabDistances.Name = "TabDistances";
        helpProvider_0.SetShowHelp(TabDistances, true);
        TabDistances.Size = new Size(636, 263);
        TabDistances.TabIndex = 1;
        TabDistances.Text = "Distances";
        TabDistances.UseVisualStyleBackColor = true;
        groupBox30.Controls.Add(label54);
        groupBox30.Controls.Add(LootSafeDistance);
        groupBox30.Controls.Add(label51);
        groupBox30.Controls.Add(LootCheckHostiles);
        groupBox30.Location = new Point(300, 132);
        groupBox30.Name = "groupBox30";
        groupBox30.Size = new Size(307, 103);
        groupBox30.TabIndex = 18;
        groupBox30.TabStop = false;
        groupBox30.Text = "Hostile Monsters";
        label54.AutoSize = true;
        label54.Location = new Point(156, 62);
        label54.Name = "label54";
        label54.Size = new Size(32, 13);
        label54.TabIndex = 13;
        label54.Text = "yards";
        label54.TextAlign = ContentAlignment.BottomLeft;
        helpProvider_0.SetHelpKeyword(LootSafeDistance, "Distances.html");
        helpProvider_0.SetHelpNavigator(LootSafeDistance, HelpNavigator.Topic);
        LootSafeDistance.Location = new Point(111, 60);
        LootSafeDistance.MaxLength = 6;
        LootSafeDistance.Name = "LootSafeDistance";
        helpProvider_0.SetShowHelp(LootSafeDistance, true);
        LootSafeDistance.Size = new Size(40, 20);
        LootSafeDistance.TabIndex = 12;
        label51.Location = new Point(13, 60);
        label51.Name = "label51";
        label51.Size = new Size(93, 20);
        label51.TabIndex = 10;
        label51.Text = "Safe distance:";
        label51.TextAlign = ContentAlignment.MiddleRight;
        LootCheckHostiles.AutoSize = true;
        helpProvider_0.SetHelpKeyword(LootCheckHostiles, "Distances.html");
        helpProvider_0.SetHelpNavigator(LootCheckHostiles, HelpNavigator.Topic);
        LootCheckHostiles.Location = new Point(29, 29);
        LootCheckHostiles.Name = "LootCheckHostiles";
        helpProvider_0.SetShowHelp(LootCheckHostiles, true);
        LootCheckHostiles.Size = new Size(209, 17);
        LootCheckHostiles.TabIndex = 0;
        LootCheckHostiles.Text = "Avoid hostiles when looting and resting";
        LootCheckHostiles.UseVisualStyleBackColor = true;
        LootCheckHostiles.CheckedChanged += LootCheckHostiles_CheckedChanged;
        groupBox12.Controls.Add(PickupJunk);
        groupBox12.Controls.Add(label13);
        groupBox12.Controls.Add(label7);
        groupBox12.Controls.Add(ExtraPull);
        groupBox12.Controls.Add(label16);
        groupBox12.Controls.Add(HarvestRange);
        groupBox12.Controls.Add(label15);
        groupBox12.Location = new Point(10, 17);
        groupBox12.Name = "groupBox12";
        groupBox12.Size = new Size(285, 110);
        groupBox12.TabIndex = 17;
        groupBox12.TabStop = false;
        groupBox12.Text = "Wandering";
        helpProvider_0.SetHelpKeyword(PickupJunk, "Distances.html");
        helpProvider_0.SetHelpNavigator(PickupJunk, HelpNavigator.Topic);
        PickupJunk.Location = new Point(58, 70);
        PickupJunk.Name = "PickupJunk";
        helpProvider_0.SetShowHelp(PickupJunk, true);
        PickupJunk.Size = new Size(145, 24);
        PickupJunk.TabIndex = 2;
        PickupJunk.Text = "Pickup junk";
        label13.Location = new Point(167, 46);
        label13.Name = "label13";
        label13.Size = new Size(49, 16);
        label13.TabIndex = 16;
        label13.Text = "yards";
        label13.TextAlign = ContentAlignment.BottomLeft;
        label7.Location = new Point(13, 24);
        label7.Name = "label7";
        label7.Size = new Size(96, 16);
        label7.TabIndex = 15;
        label7.Text = "Walk-to-pull:";
        label7.TextAlign = ContentAlignment.TopRight;
        helpProvider_0.SetHelpKeyword(ExtraPull, "Distances.html");
        helpProvider_0.SetHelpNavigator(ExtraPull, HelpNavigator.Topic);
        ExtraPull.Location = new Point(114, 22);
        ExtraPull.Name = "ExtraPull";
        helpProvider_0.SetShowHelp(ExtraPull, true);
        ExtraPull.Size = new Size(48, 20);
        ExtraPull.TabIndex = 1;
        label16.Location = new Point(167, 23);
        label16.Name = "label16";
        label16.Size = new Size(49, 16);
        label16.TabIndex = 5;
        label16.Text = "yards";
        label16.TextAlign = ContentAlignment.BottomLeft;
        helpProvider_0.SetHelpKeyword(HarvestRange, "Distances.html");
        helpProvider_0.SetHelpNavigator(HarvestRange, HelpNavigator.Topic);
        HarvestRange.Location = new Point(114, 46);
        HarvestRange.MaxLength = 3;
        HarvestRange.Name = "HarvestRange";
        helpProvider_0.SetShowHelp(HarvestRange, true);
        HarvestRange.Size = new Size(48, 20);
        HarvestRange.TabIndex = 0;
        label15.Location = new Point(16, 49);
        label15.Name = "label15";
        label15.Size = new Size(93, 15);
        label15.TabIndex = 3;
        label15.Text = "Harvest range:";
        label15.TextAlign = ContentAlignment.TopRight;
        groupBox18.Controls.Add(label57);
        groupBox18.Controls.Add(label56);
        groupBox18.Controls.Add(label55);
        groupBox18.Controls.Add(PartyFollowerStop);
        groupBox18.Controls.Add(PartyFollowerStart);
        groupBox18.Controls.Add(PartyLeaderWait);
        groupBox18.Controls.Add(label37);
        groupBox18.Controls.Add(label36);
        groupBox18.Controls.Add(label35);
        groupBox18.Location = new Point(300, 17);
        groupBox18.Name = "groupBox18";
        groupBox18.Size = new Size(307, 110);
        groupBox18.TabIndex = 16;
        groupBox18.TabStop = false;
        groupBox18.Text = "Party";
        label57.AutoSize = true;
        label57.Location = new Point(177, 74);
        label57.Name = "label57";
        label57.Size = new Size(32, 13);
        label57.TabIndex = 16;
        label57.Text = "yards";
        label57.TextAlign = ContentAlignment.BottomLeft;
        label56.AutoSize = true;
        label56.Location = new Point(177, 47);
        label56.Name = "label56";
        label56.Size = new Size(32, 13);
        label56.TabIndex = 15;
        label56.Text = "yards";
        label56.TextAlign = ContentAlignment.BottomLeft;
        label55.AutoSize = true;
        label55.Location = new Point(177, 23);
        label55.Name = "label55";
        label55.Size = new Size(32, 13);
        label55.TabIndex = 14;
        label55.Text = "yards";
        label55.TextAlign = ContentAlignment.BottomLeft;
        helpProvider_0.SetHelpKeyword(PartyFollowerStop, "Party.html#Limits");
        helpProvider_0.SetHelpNavigator(PartyFollowerStop, HelpNavigator.Topic);
        PartyFollowerStop.Location = new Point(124, 72);
        PartyFollowerStop.MaxLength = 3;
        PartyFollowerStop.Name = "PartyFollowerStop";
        helpProvider_0.SetShowHelp(PartyFollowerStop, true);
        PartyFollowerStop.Size = new Size(48, 20);
        PartyFollowerStop.TabIndex = 2;
        helpProvider_0.SetHelpKeyword(PartyFollowerStart, "Party.html#Limits");
        helpProvider_0.SetHelpNavigator(PartyFollowerStart, HelpNavigator.Topic);
        PartyFollowerStart.Location = new Point(124, 45);
        PartyFollowerStart.MaxLength = 3;
        PartyFollowerStart.Name = "PartyFollowerStart";
        helpProvider_0.SetShowHelp(PartyFollowerStart, true);
        PartyFollowerStart.Size = new Size(48, 20);
        PartyFollowerStart.TabIndex = 1;
        helpProvider_0.SetHelpKeyword(PartyLeaderWait, "Party.html#Limits");
        helpProvider_0.SetHelpNavigator(PartyLeaderWait, HelpNavigator.Topic);
        PartyLeaderWait.Location = new Point(124, 21);
        PartyLeaderWait.MaxLength = 3;
        PartyLeaderWait.Name = "PartyLeaderWait";
        helpProvider_0.SetShowHelp(PartyLeaderWait, true);
        PartyLeaderWait.Size = new Size(48, 20);
        PartyLeaderWait.TabIndex = 0;
        label37.AutoSize = true;
        label37.Location = new Point(27, 72);
        label37.Name = "label37";
        label37.Size = new Size(86, 13);
        label37.TabIndex = 2;
        label37.Text = "Follower walk to:";
        label37.TextAlign = ContentAlignment.TopRight;
        label36.AutoSize = true;
        label36.Location = new Point(13, 48);
        label36.Name = "label36";
        label36.Size = new Size(97, 13);
        label36.TabIndex = 1;
        label36.Text = "Follower walk start:";
        label36.TextAlign = ContentAlignment.TopRight;
        label35.AutoSize = true;
        label35.Location = new Point(48, 23);
        label35.Name = "label35";
        label35.Size = new Size(65, 13);
        label35.TabIndex = 0;
        label35.Text = "Leader wait:";
        label35.TextAlign = ContentAlignment.TopRight;
        groupBox16.Controls.Add(label22);
        groupBox16.Controls.Add(RangedDistance);
        groupBox16.Controls.Add(label23);
        groupBox16.Controls.Add(label14);
        groupBox16.Controls.Add(MeleeDistance);
        groupBox16.Controls.Add(label8);
        groupBox16.Location = new Point(10, 132);
        groupBox16.Name = "groupBox16";
        groupBox16.Size = new Size(285, 103);
        groupBox16.TabIndex = 15;
        groupBox16.TabStop = false;
        groupBox16.Text = "Combat Range";
        label22.AutoSize = true;
        label22.Location = new Point(112, 48);
        label22.Name = "label22";
        label22.Size = new Size(32, 13);
        label22.TabIndex = 11;
        label22.Text = "yards";
        label22.TextAlign = ContentAlignment.BottomLeft;
        helpProvider_0.SetHelpKeyword(RangedDistance, "Distances.html");
        helpProvider_0.SetHelpNavigator(RangedDistance, HelpNavigator.Topic);
        RangedDistance.Location = new Point(67, 46);
        RangedDistance.MaxLength = 6;
        RangedDistance.Name = "RangedDistance";
        helpProvider_0.SetShowHelp(RangedDistance, true);
        RangedDistance.Size = new Size(40, 20);
        RangedDistance.TabIndex = 1;
        label23.AutoSize = true;
        label23.Location = new Point(10, 49);
        label23.Name = "label23";
        label23.Size = new Size(48, 13);
        label23.TabIndex = 9;
        label23.Text = "Ranged:";
        label23.TextAlign = ContentAlignment.TopRight;
        label14.AutoSize = true;
        label14.Location = new Point(112, 24);
        label14.Name = "label14";
        label14.Size = new Size(32, 13);
        label14.TabIndex = 8;
        label14.Text = "yards";
        helpProvider_0.SetHelpKeyword(MeleeDistance, "Distances.html");
        helpProvider_0.SetHelpNavigator(MeleeDistance, HelpNavigator.Topic);
        MeleeDistance.Location = new Point(67, 22);
        MeleeDistance.MaxLength = 6;
        MeleeDistance.Name = "MeleeDistance";
        helpProvider_0.SetShowHelp(MeleeDistance, true);
        MeleeDistance.Size = new Size(40, 20);
        MeleeDistance.TabIndex = 0;
        label8.AutoSize = true;
        label8.Location = new Point(22, 24);
        label8.Name = "label8";
        label8.Size = new Size(39, 13);
        label8.TabIndex = 0;
        label8.Text = "Melee:";
        label8.TextAlign = ContentAlignment.TopRight;
        TabChat.Controls.Add(groupBox6);
        TabChat.Controls.Add(ChatLog);
        helpProvider_0.SetHelpNavigator(TabChat, HelpNavigator.TopicId);
        helpProvider_0.SetHelpString(TabChat, "Chat.html");
        TabChat.Location = new Point(4, 22);
        TabChat.Name = "TabChat";
        helpProvider_0.SetShowHelp(TabChat, true);
        TabChat.Size = new Size(636, 263);
        TabChat.TabIndex = 4;
        TabChat.Text = "Chat";
        TabChat.UseVisualStyleBackColor = true;
        groupBox6.Controls.Add(PlaySay);
        groupBox6.Controls.Add(AutoReplyText);
        groupBox6.Controls.Add(AutoReply);
        groupBox6.Controls.Add(PlayWhisper);
        groupBox6.Location = new Point(8, 104);
        groupBox6.Name = "groupBox6";
        groupBox6.Size = new Size(609, 158);
        groupBox6.TabIndex = 1;
        groupBox6.TabStop = false;
        groupBox6.Text = "Reaction";
        ChatLog.Controls.Add(ChatLogFrame);
        ChatLog.Controls.Add(CombatLogFrame);
        ChatLog.Controls.Add(label53);
        ChatLog.Controls.Add(label52);
        ChatLog.Controls.Add(ChatDelete);
        ChatLog.Location = new Point(8, 8);
        ChatLog.Name = "ChatLog";
        ChatLog.Size = new Size(609, 88);
        ChatLog.TabIndex = 0;
        ChatLog.TabStop = false;
        ChatLog.Text = "Logging";
        label53.Location = new Point(13, 49);
        label53.Name = "label53";
        label53.Size = new Size(128, 21);
        label53.TabIndex = 3;
        label53.Text = "Combat log frame:";
        label53.TextAlign = ContentAlignment.TopRight;
        label52.Location = new Point(13, 25);
        label52.Name = "label52";
        label52.Size = new Size(128, 21);
        label52.TabIndex = 2;
        label52.Text = "Chat log frame:";
        label52.TextAlign = ContentAlignment.TopRight;
        TabParty.Controls.Add(FollowerOptionsBox);
        TabParty.Controls.Add(LeaderOptionsBox);
        TabParty.Controls.Add(PartyOptionsBox);
        TabParty.Controls.Add(groupBox17);
        helpProvider_0.SetHelpNavigator(TabParty, HelpNavigator.TopicId);
        helpProvider_0.SetHelpString(TabParty, "Party1.html");
        TabParty.Location = new Point(4, 22);
        TabParty.Name = "TabParty";
        helpProvider_0.SetShowHelp(TabParty, true);
        TabParty.Size = new Size(636, 263);
        TabParty.TabIndex = 7;
        TabParty.Text = "Party";
        TabParty.UseVisualStyleBackColor = true;
        FollowerOptionsBox.Controls.Add(PartySlashFollow);
        FollowerOptionsBox.Controls.Add(label29);
        FollowerOptionsBox.Controls.Add(PartyAttackDelay);
        FollowerOptionsBox.Controls.Add(label28);
        FollowerOptionsBox.Controls.Add(PartyLeaderName);
        FollowerOptionsBox.Controls.Add(label27);
        FollowerOptionsBox.Location = new Point(173, 153);
        FollowerOptionsBox.Name = "FollowerOptionsBox";
        FollowerOptionsBox.Size = new Size(444, 69);
        FollowerOptionsBox.TabIndex = 3;
        FollowerOptionsBox.TabStop = false;
        FollowerOptionsBox.Text = "Follower";
        label29.Location = new Point(140, 42);
        label29.Name = "label29";
        label29.Size = new Size(50, 13);
        label29.TabIndex = 10;
        label29.Text = "seconds";
        label28.Location = new Point(7, 42);
        label28.Name = "label28";
        label28.Size = new Size(73, 13);
        label28.TabIndex = 8;
        label28.Text = "Attack delay:";
        label28.TextAlign = ContentAlignment.TopRight;
        label27.Location = new Point(13, 21);
        label27.Name = "label27";
        label27.Size = new Size(74, 14);
        label27.TabIndex = 6;
        label27.Text = "Leader name:";
        label27.TextAlign = ContentAlignment.TopRight;
        LeaderOptionsBox.Controls.Add(PartyMember4);
        LeaderOptionsBox.Controls.Add(PartyMember3);
        LeaderOptionsBox.Controls.Add(PartyMember2);
        LeaderOptionsBox.Controls.Add(PartyMember1);
        LeaderOptionsBox.Controls.Add(label33);
        LeaderOptionsBox.Controls.Add(label32);
        LeaderOptionsBox.Controls.Add(label31);
        LeaderOptionsBox.Controls.Add(label30);
        LeaderOptionsBox.Controls.Add(label25);
        LeaderOptionsBox.Location = new Point(173, 7);
        LeaderOptionsBox.Name = "LeaderOptionsBox";
        LeaderOptionsBox.Size = new Size(444, 139);
        LeaderOptionsBox.TabIndex = 2;
        LeaderOptionsBox.TabStop = false;
        LeaderOptionsBox.Text = "Leader";
        label33.Location = new Point(13, 104);
        label33.Name = "label33";
        label33.Size = new Size(20, 14);
        label33.TabIndex = 5;
        label33.Text = "4.";
        label32.Location = new Point(13, 83);
        label32.Name = "label32";
        label32.Size = new Size(20, 14);
        label32.TabIndex = 4;
        label32.Text = "3.";
        label31.Location = new Point(13, 62);
        label31.Name = "label31";
        label31.Size = new Size(20, 14);
        label31.TabIndex = 3;
        label31.Text = "2.";
        label30.Location = new Point(13, 42);
        label30.Name = "label30";
        label30.Size = new Size(20, 13);
        label30.TabIndex = 2;
        label30.Text = "1.";
        label25.Location = new Point(33, 21);
        label25.Name = "label25";
        label25.Size = new Size(114, 14);
        label25.TabIndex = 0;
        label25.Text = "Member name";
        PartyOptionsBox.Controls.Add(PartyHealMode);
        PartyOptionsBox.Controls.Add(label34);
        PartyOptionsBox.Controls.Add(PartyBuff);
        PartyOptionsBox.Controls.Add(PartyLootPos);
        PartyOptionsBox.Controls.Add(label24);
        PartyOptionsBox.Controls.Add(PartyAdds);
        PartyOptionsBox.Controls.Add(Looters);
        PartyOptionsBox.Controls.Add(PartyLooters);
        PartyOptionsBox.Location = new Point(7, 90);
        PartyOptionsBox.Name = "PartyOptionsBox";
        PartyOptionsBox.Size = new Size(160, 139);
        PartyOptionsBox.TabIndex = 1;
        PartyOptionsBox.TabStop = false;
        PartyOptionsBox.Text = "Options";
        PartyHealMode.DropDownStyle = ComboBoxStyle.DropDownList;
        PartyHealMode.Location = new Point(53, 111);
        PartyHealMode.Name = "PartyHealMode";
        PartyHealMode.Size = new Size(94, 21);
        PartyHealMode.TabIndex = 4;
        label34.Location = new Point(13, 111);
        label34.Name = "label34";
        label34.Size = new Size(34, 14);
        label34.TabIndex = 4;
        label34.Text = "Heal:";
        label34.TextAlign = ContentAlignment.TopRight;
        label24.Location = new Point(7, 83);
        label24.Name = "label24";
        label24.Size = new Size(73, 14);
        label24.TabIndex = 3;
        label24.Text = "Loot position:";
        label24.TextAlign = ContentAlignment.TopRight;
        Looters.Location = new Point(13, 62);
        Looters.Name = "Looters";
        Looters.Size = new Size(67, 14);
        Looters.TabIndex = 2;
        Looters.Text = "Looters:";
        Looters.TextAlign = ContentAlignment.TopRight;
        groupBox17.Controls.Add(PartyFollower);
        groupBox17.Controls.Add(PartyLeader);
        groupBox17.Controls.Add(PartySolo);
        groupBox17.Location = new Point(7, 7);
        groupBox17.Name = "groupBox17";
        groupBox17.Size = new Size(160, 81);
        groupBox17.TabIndex = 0;
        groupBox17.TabStop = false;
        groupBox17.Text = "Mode";
        TabMisc.Controls.Add(groupBox4);
        TabMisc.Controls.Add(groupBox2);
        helpProvider_0.SetHelpNavigator(TabMisc, HelpNavigator.TopicId);
        helpProvider_0.SetHelpString(TabMisc, "Miscellaneous.html");
        TabMisc.Location = new Point(4, 22);
        TabMisc.Name = "TabMisc";
        helpProvider_0.SetShowHelp(TabMisc, true);
        TabMisc.Size = new Size(636, 263);
        TabMisc.TabIndex = 2;
        TabMisc.Text = "Miscellaneous";
        TabMisc.UseVisualStyleBackColor = true;
        groupBox4.Controls.Add(StopWhenFull);
        groupBox4.Controls.Add(StopAfter);
        groupBox4.Controls.Add(label4);
        groupBox4.Controls.Add(StopAfterMinutes);
        groupBox4.Location = new Point(8, 198);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new Size(609, 56);
        groupBox4.TabIndex = 8;
        groupBox4.TabStop = false;
        groupBox4.Text = "Auto-Stop";
        label4.Location = new Point(153, 28);
        label4.Name = "label4";
        label4.Size = new Size(49, 15);
        label4.TabIndex = 5;
        label4.Text = "minutes";
        label4.TextAlign = ContentAlignment.BottomLeft;
        TabBackground.Controls.Add(groupBox26);
        TabBackground.Controls.Add(groupBox25);
        TabBackground.Controls.Add(groupBox24);
        helpProvider_0.SetHelpNavigator(TabBackground, HelpNavigator.TopicId);
        helpProvider_0.SetHelpString(TabBackground, "Background.html");
        TabBackground.Location = new Point(4, 22);
        TabBackground.Name = "TabBackground";
        helpProvider_0.SetShowHelp(TabBackground, true);
        TabBackground.Size = new Size(636, 263);
        TabBackground.TabIndex = 8;
        TabBackground.Text = "Background";
        TabBackground.UseVisualStyleBackColor = true;
        groupBox26.Controls.Add(WebNotifyCredentials);
        groupBox26.Controls.Add(WebNotifyURL);
        groupBox26.Controls.Add(label39);
        groupBox26.Controls.Add(label38);
        groupBox26.Controls.Add(WebNotifyEnabled);
        groupBox26.Location = new Point(642, 60);
        groupBox26.Name = "groupBox26";
        groupBox26.Size = new Size(350, 112);
        groupBox26.TabIndex = 10;
        groupBox26.TabStop = false;
        groupBox26.Text = "Web Notify";
        groupBox26.Visible = false;
        WebNotifyCredentials.Enabled = false;
        WebNotifyCredentials.Location = new Point(79, 49);
        WebNotifyCredentials.Name = "WebNotifyCredentials";
        WebNotifyCredentials.Size = new Size(253, 20);
        WebNotifyCredentials.TabIndex = 4;
        WebNotifyURL.Enabled = false;
        WebNotifyURL.Location = new Point(79, 25);
        WebNotifyURL.Name = "WebNotifyURL";
        WebNotifyURL.Size = new Size(253, 20);
        WebNotifyURL.TabIndex = 3;
        label39.AutoSize = true;
        label39.Location = new Point(5, 51);
        label39.Name = "label39";
        label39.Size = new Size(62, 13);
        label39.TabIndex = 2;
        label39.Text = "Credentials:";
        label39.TextAlign = ContentAlignment.TopRight;
        label38.AutoSize = true;
        label38.Location = new Point(41, 28);
        label38.Name = "label38";
        label38.Size = new Size(32, 13);
        label38.TabIndex = 1;
        label38.Text = "URL:";
        label38.TextAlign = ContentAlignment.TopRight;
        WebNotifyEnabled.AutoSize = true;
        WebNotifyEnabled.Location = new Point(159, 80);
        WebNotifyEnabled.Name = "WebNotifyEnabled";
        WebNotifyEnabled.Size = new Size(65, 17);
        WebNotifyEnabled.TabIndex = 0;
        WebNotifyEnabled.Text = "Enabled";
        WebNotifyEnabled.UseVisualStyleBackColor = true;
        WebNotifyEnabled.CheckedChanged += WebNotifyEnabled_CheckedChanged;
        groupBox25.Controls.Add(DisplayShrink);
        groupBox25.Controls.Add(DisplayHide);
        groupBox25.Controls.Add(DisplayNormal);
        groupBox25.Enabled = false;
        groupBox25.Location = new Point(282, 11);
        groupBox25.Name = "groupBox25";
        groupBox25.Size = new Size(245, sbyte.MaxValue);
        groupBox25.TabIndex = 20;
        groupBox25.TabStop = false;
        groupBox25.Text = "Display";
        DisplayShrink.AutoSize = true;
        DisplayShrink.Location = new Point(17, 72);
        DisplayShrink.Name = "DisplayShrink";
        DisplayShrink.Size = new Size(123, 17);
        DisplayShrink.TabIndex = 2;
        DisplayShrink.TabStop = true;
        DisplayShrink.Text = "Shrink game window";
        DisplayShrink.UseVisualStyleBackColor = true;
        DisplayHide.AutoSize = true;
        DisplayHide.Location = new Point(17, 49);
        DisplayHide.Name = "DisplayHide";
        DisplayHide.Size = new Size(115, 17);
        DisplayHide.TabIndex = 1;
        DisplayHide.TabStop = true;
        DisplayHide.Text = "Hide game window";
        DisplayHide.UseVisualStyleBackColor = true;
        DisplayNormal.AutoSize = true;
        DisplayNormal.Location = new Point(17, 25);
        DisplayNormal.Name = "DisplayNormal";
        DisplayNormal.Size = new Size(132, 17);
        DisplayNormal.TabIndex = 0;
        DisplayNormal.TabStop = true;
        DisplayNormal.Text = "Leave game as normal";
        DisplayNormal.UseVisualStyleBackColor = true;
        groupBox24.Controls.Add(BackgroundEnable);
        groupBox24.Location = new Point(10, 11);
        groupBox24.Name = "groupBox24";
        groupBox24.Size = new Size(250, sbyte.MaxValue);
        groupBox24.TabIndex = 19;
        groupBox24.TabStop = false;
        groupBox24.Text = "Options";
        TabVending.Controls.Add(MailItemBox);
        TabVending.Controls.Add(MailSetupBox);
        TabVending.Controls.Add(groupBox33);
        TabVending.Controls.Add(groupBox34);
        helpProvider_0.SetHelpNavigator(TabVending, HelpNavigator.TopicId);
        helpProvider_0.SetHelpString(TabVending, "Vending.html");
        TabVending.Location = new Point(4, 22);
        TabVending.Name = "TabVending";
        helpProvider_0.SetShowHelp(TabVending, true);
        TabVending.Size = new Size(636, 263);
        TabVending.TabIndex = 10;
        TabVending.Text = "Vending";
        TabVending.UseVisualStyleBackColor = true;
        MailItemBox.Controls.Add(VendMailList);
        MailItemBox.Location = new Point(463, 11);
        MailItemBox.Name = "MailItemBox";
        MailItemBox.Size = new Size(159, 229);
        MailItemBox.TabIndex = 3;
        MailItemBox.TabStop = false;
        MailItemBox.Text = "Mail Items";
        VendMailList.AcceptsReturn = true;
        VendMailList.Location = new Point(6, 15);
        VendMailList.Multiline = true;
        VendMailList.Name = "VendMailList";
        VendMailList.Size = new Size(146, 199);
        VendMailList.TabIndex = 0;
        MailSetupBox.Controls.Add(SendMail);
        MailSetupBox.Controls.Add(SubjectLabel);
        MailSetupBox.Controls.Add(SubjectText);
        MailSetupBox.Controls.Add(mailtoLabel);
        MailSetupBox.Controls.Add(MailToText);
        MailSetupBox.Location = new Point(4, 130);
        MailSetupBox.Name = "MailSetupBox";
        MailSetupBox.Size = new Size(287, 110);
        MailSetupBox.TabIndex = 2;
        MailSetupBox.TabStop = false;
        MailSetupBox.Text = "Mail Item Setup";
        SendMail.AutoSize = true;
        SendMail.Location = new Point(79, 78);
        SendMail.Name = "SendMail";
        SendMail.Size = new Size(73, 17);
        SendMail.TabIndex = 4;
        SendMail.Text = "Send Mail";
        SendMail.UseVisualStyleBackColor = true;
        SubjectLabel.AutoSize = true;
        SubjectLabel.Location = new Point(6, 46);
        SubjectLabel.Name = "SubjectLabel";
        SubjectLabel.Size = new Size(49, 13);
        SubjectLabel.TabIndex = 3;
        SubjectLabel.Text = "Subject: ";
        SubjectText.Location = new Point(57, 43);
        SubjectText.Name = "SubjectText";
        SubjectText.Size = new Size(167, 20);
        SubjectText.TabIndex = 2;
        mailtoLabel.AutoSize = true;
        mailtoLabel.Location = new Point(6, 21);
        mailtoLabel.Name = "mailtoLabel";
        mailtoLabel.Size = new Size(44, 13);
        mailtoLabel.TabIndex = 1;
        mailtoLabel.Text = "Mail to: ";
        MailToText.Location = new Point(57, 18);
        MailToText.Name = "MailToText";
        MailToText.Size = new Size(167, 20);
        MailToText.TabIndex = 0;
        groupBox33.Controls.Add(VendGrey);
        groupBox33.Controls.Add(VendWhite);
        groupBox33.Controls.Add(VendGreen);
        groupBox33.Location = new Point(4, 11);
        groupBox33.Name = "groupBox33";
        groupBox33.Size = new Size(287, 110);
        groupBox33.TabIndex = 0;
        groupBox33.TabStop = false;
        groupBox33.Text = "Sell Item Type";
        VendGrey.AutoSize = true;
        VendGrey.Checked = true;
        VendGrey.Location = new Point(9, 25);
        VendGrey.Name = "VendGrey";
        VendGrey.Size = new Size(115, 17);
        VendGrey.TabIndex = 0;
        VendGrey.TabStop = true;
        VendGrey.Text = "Sell only poor items";
        VendWhite.AutoSize = true;
        VendWhite.Location = new Point(9, 50);
        VendWhite.Name = "VendWhite";
        VendWhite.Size = new Size(157, 17);
        VendWhite.TabIndex = 1;
        VendWhite.Text = "Sell poor and common items";
        VendGreen.AutoSize = true;
        VendGreen.Location = new Point(9, 75);
        VendGreen.Name = "VendGreen";
        VendGreen.Size = new Size(218, 17);
        VendGreen.TabIndex = 2;
        VendGreen.Text = "Sell poor, common, and uncommon items";
        groupBox34.Controls.Add(VendWhiteList);
        groupBox34.Location = new Point(297, 11);
        groupBox34.Name = "groupBox34";
        groupBox34.Size = new Size(160, 229);
        groupBox34.TabIndex = 1;
        groupBox34.TabStop = false;
        groupBox34.Text = "Protected Items";
        VendWhiteList.AcceptsReturn = true;
        VendWhiteList.Location = new Point(6, 15);
        VendWhiteList.Multiline = true;
        VendWhiteList.Name = "VendWhiteList";
        VendWhiteList.Size = new Size(148, 201);
        VendWhiteList.TabIndex = 0;
        tabControl1.Controls.Add(TabGeneral);
        tabControl1.Controls.Add(TabLimits);
        tabControl1.Controls.Add(TabDistances);
        tabControl1.Controls.Add(TabDetection);
        tabControl1.Controls.Add(TabKeys);
        tabControl1.Controls.Add(TabChat);
        tabControl1.Controls.Add(TabParty);
        tabControl1.Controls.Add(TabMisc);
        tabControl1.Controls.Add(TabBackground);
        tabControl1.Controls.Add(TabClasses);
        tabControl1.Controls.Add(TabVending);
        tabControl1.Location = new Point(8, 8);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(644, 289);
        tabControl1.TabIndex = 0;
        TabClasses.Controls.Add(groupBox29);
        TabClasses.Controls.Add(groupBox28);
        TabClasses.Location = new Point(4, 22);
        TabClasses.Name = "TabClasses";
        TabClasses.Size = new Size(636, 263);
        TabClasses.TabIndex = 9;
        TabClasses.Text = "Classes";
        TabClasses.UseVisualStyleBackColor = true;
        groupBox29.Location = new Point(337, 3);
        groupBox29.Name = "groupBox29";
        groupBox29.Size = new Size(189, 122);
        groupBox29.TabIndex = 1;
        groupBox29.TabStop = false;
        groupBox29.Text = "Options";
        groupBox28.Controls.Add(ClassFilesList);
        groupBox28.Controls.Add(CompileButton);
        groupBox28.Location = new Point(2, 3);
        groupBox28.Name = "groupBox28";
        groupBox28.Size = new Size(313, 229);
        groupBox28.TabIndex = 0;
        groupBox28.TabStop = false;
        groupBox28.Text = "Classes Present";
        ClassFilesList.FormattingEnabled = true;
        ClassFilesList.Location = new Point(5, 18);
        ClassFilesList.Name = "ClassFilesList";
        ClassFilesList.Size = new Size(298, 154);
        ClassFilesList.TabIndex = 2;
        ClassFilesList.SelectedIndexChanged += ClassFilesList_SelectedIndexChanged;
        ClassFilesList.ItemCheck += ClassFilesList_ItemCheck;
        CompileButton.Enabled = false;
        CompileButton.Location = new Point(97, 189);
        CompileButton.Name = "CompileButton";
        CompileButton.Size = new Size(116, 28);
        CompileButton.TabIndex = 1;
        CompileButton.Text = "Test Compile";
        CompileButton.UseVisualStyleBackColor = true;
        CompileButton.Click += CompileButton_Click;
        TabInvisible.Controls.Add(groupBox20);
        TabInvisible.Location = new Point(4, 25);
        TabInvisible.Name = "TabInvisible";
        TabInvisible.Size = new Size(582, 276);
        TabInvisible.TabIndex = 8;
        TabInvisible.Text = "Invisible";
        groupBox20.Controls.Add(SetProfile3);
        groupBox20.Controls.Add(SetProfile2);
        groupBox20.Controls.Add(SetProfile1);
        groupBox20.Controls.Add(SetInitial);
        groupBox20.Controls.Add(Profile3);
        groupBox20.Controls.Add(Profile2);
        groupBox20.Controls.Add(label47);
        groupBox20.Controls.Add(label46);
        groupBox20.Controls.Add(Profile1);
        groupBox20.Controls.Add(label45);
        groupBox20.Controls.Add(InitialProfile);
        groupBox20.Controls.Add(label44);
        groupBox20.Location = new Point(8, 8);
        groupBox20.Name = "groupBox20";
        groupBox20.Size = new Size(560, 168);
        groupBox20.TabIndex = 0;
        groupBox20.TabStop = false;
        groupBox20.Text = "Startup";
        SetProfile3.Location = new Point(488, 136);
        SetProfile3.Name = "SetProfile3";
        SetProfile3.Size = new Size(48, 24);
        SetProfile3.TabIndex = 15;
        SetProfile3.Text = "Set";
        SetProfile3.Click += SetProfile3_Click;
        SetProfile2.Location = new Point(488, 104);
        SetProfile2.Name = "SetProfile2";
        SetProfile2.Size = new Size(48, 24);
        SetProfile2.TabIndex = 14;
        SetProfile2.Text = "Set";
        SetProfile2.Click += SetProfile2_Click;
        SetProfile1.Location = new Point(488, 72);
        SetProfile1.Name = "SetProfile1";
        SetProfile1.Size = new Size(48, 24);
        SetProfile1.TabIndex = 13;
        SetProfile1.Text = "Set";
        SetProfile1.Click += SetProfile1_Click;
        SetInitial.Location = new Point(488, 32);
        SetInitial.Name = "SetInitial";
        SetInitial.Size = new Size(48, 24);
        SetInitial.TabIndex = 12;
        SetInitial.Text = "Set";
        SetInitial.Click += SetInitial_Click;
        Profile3.ForeColor = SystemColors.Highlight;
        Profile3.Location = new Point(104, 136);
        Profile3.Name = "Profile3";
        Profile3.Size = new Size(376, 18);
        Profile3.TabIndex = 11;
        Profile3.TextAlign = ContentAlignment.MiddleLeft;
        Profile2.ForeColor = SystemColors.Highlight;
        Profile2.Location = new Point(104, 104);
        Profile2.Name = "Profile2";
        Profile2.Size = new Size(376, 18);
        Profile2.TabIndex = 10;
        Profile2.TextAlign = ContentAlignment.MiddleLeft;
        label47.Location = new Point(16, 136);
        label47.Name = "label47";
        label47.Size = new Size(80, 16);
        label47.TabIndex = 9;
        label47.Text = "Profile 3:";
        label47.TextAlign = ContentAlignment.TopRight;
        label46.Location = new Point(16, 104);
        label46.Name = "label46";
        label46.Size = new Size(80, 16);
        label46.TabIndex = 8;
        label46.Text = "Profile 2:";
        label46.TextAlign = ContentAlignment.TopRight;
        Profile1.ForeColor = SystemColors.Highlight;
        Profile1.Location = new Point(104, 72);
        Profile1.Name = "Profile1";
        Profile1.Size = new Size(376, 18);
        Profile1.TabIndex = 7;
        Profile1.TextAlign = ContentAlignment.MiddleLeft;
        label45.Location = new Point(16, 72);
        label45.Name = "label45";
        label45.Size = new Size(80, 16);
        label45.TabIndex = 6;
        label45.Text = "Profile 1:";
        label45.TextAlign = ContentAlignment.TopRight;
        InitialProfile.ForeColor = SystemColors.Highlight;
        InitialProfile.Location = new Point(104, 32);
        InitialProfile.Name = "InitialProfile";
        InitialProfile.Size = new Size(376, 18);
        InitialProfile.TabIndex = 5;
        InitialProfile.Text = "??";
        InitialProfile.TextAlign = ContentAlignment.MiddleLeft;
        label44.Location = new Point(16, 32);
        label44.Name = "label44";
        label44.Size = new Size(80, 16);
        label44.TabIndex = 0;
        label44.Text = "Initial profile:";
        label44.TextAlign = ContentAlignment.TopRight;
        TabDev.Controls.Add(DevBuffs);
        TabDev.Controls.Add(label43);
        TabDev.Location = new Point(4, 25);
        TabDev.Name = "TabDev";
        TabDev.Size = new Size(582, 276);
        TabDev.TabIndex = 8;
        TabDev.Text = "Dev";
        DevBuffs.Location = new Point(8, 48);
        DevBuffs.Multiline = true;
        DevBuffs.Name = "DevBuffs";
        DevBuffs.ScrollBars = ScrollBars.Vertical;
        DevBuffs.Size = new Size(240, 120);
        DevBuffs.TabIndex = 1;
        label43.Location = new Point(8, 24);
        label43.Name = "label43";
        label43.Size = new Size(104, 16);
        label43.TabIndex = 0;
        label43.Text = "Buffs:";
        label43.TextAlign = ContentAlignment.MiddleLeft;
        AcceptButton = OKButton;
        AutoScaleBaseSize = new Size(5, 13);
        CancelButton = MyCancelButton;
        ClientSize = new Size(664, 342);
        Controls.Add(tabControl1);
        Controls.Add(MyHelpButton);
        Controls.Add(MyCancelButton);
        Controls.Add(OKButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        helpProvider_0.SetHelpKeyword(this, "General.html");
        helpProvider_0.SetHelpNavigator(this, HelpNavigator.Topic);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = nameof(ConfigForm);
        helpProvider_0.SetShowHelp(this, true);
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Glider Configuration";
        groupBox1.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        TabGeneral.ResumeLayout(false);
        groupBox31.ResumeLayout(false);
        groupBox31.PerformLayout();
        groupBox19.ResumeLayout(false);
        groupBox19.PerformLayout();
        groupBox7.ResumeLayout(false);
        groupBox7.PerformLayout();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        TabLimits.ResumeLayout(false);
        groupBox32.ResumeLayout(false);
        groupBox32.PerformLayout();
        groupBox22.ResumeLayout(false);
        groupBox22.PerformLayout();
        groupBox23.ResumeLayout(false);
        groupBox23.PerformLayout();
        groupBox13.ResumeLayout(false);
        groupBox13.PerformLayout();
        groupBox5.ResumeLayout(false);
        groupBox5.PerformLayout();
        TabDetection.ResumeLayout(false);
        groupBox21.ResumeLayout(false);
        groupBox21.PerformLayout();
        groupBox9.ResumeLayout(false);
        groupBox9.PerformLayout();
        groupBox14.ResumeLayout(false);
        groupBox14.PerformLayout();
        TabKeys.ResumeLayout(false);
        groupBox27.ResumeLayout(false);
        groupBox27.PerformLayout();
        groupBox15.ResumeLayout(false);
        groupBox15.PerformLayout();
        groupBox8.ResumeLayout(false);
        groupBox8.PerformLayout();
        TabDistances.ResumeLayout(false);
        groupBox30.ResumeLayout(false);
        groupBox30.PerformLayout();
        groupBox12.ResumeLayout(false);
        groupBox12.PerformLayout();
        groupBox18.ResumeLayout(false);
        groupBox18.PerformLayout();
        groupBox16.ResumeLayout(false);
        groupBox16.PerformLayout();
        TabChat.ResumeLayout(false);
        groupBox6.ResumeLayout(false);
        groupBox6.PerformLayout();
        ChatLog.ResumeLayout(false);
        ChatLog.PerformLayout();
        TabParty.ResumeLayout(false);
        FollowerOptionsBox.ResumeLayout(false);
        FollowerOptionsBox.PerformLayout();
        LeaderOptionsBox.ResumeLayout(false);
        LeaderOptionsBox.PerformLayout();
        PartyOptionsBox.ResumeLayout(false);
        PartyOptionsBox.PerformLayout();
        groupBox17.ResumeLayout(false);
        TabMisc.ResumeLayout(false);
        groupBox4.ResumeLayout(false);
        groupBox4.PerformLayout();
        TabBackground.ResumeLayout(false);
        groupBox26.ResumeLayout(false);
        groupBox26.PerformLayout();
        groupBox25.ResumeLayout(false);
        groupBox25.PerformLayout();
        groupBox24.ResumeLayout(false);
        TabVending.ResumeLayout(false);
        MailItemBox.ResumeLayout(false);
        MailItemBox.PerformLayout();
        MailSetupBox.ResumeLayout(false);
        MailSetupBox.PerformLayout();
        groupBox33.ResumeLayout(false);
        groupBox33.PerformLayout();
        groupBox34.ResumeLayout(false);
        groupBox34.PerformLayout();
        tabControl1.ResumeLayout(false);
        TabClasses.ResumeLayout(false);
        groupBox28.ResumeLayout(false);
        TabInvisible.ResumeLayout(false);
        groupBox20.ResumeLayout(false);
        TabDev.ResumeLayout(false);
        TabDev.PerformLayout();
        ResumeLayout(false);
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
        if (bool_0)
        {
            var num = (int)MessageBox.Show(this,
                "Note: changes to process renaming will not take effect until Glider is restarted!  Close and re-start Glider to load new changes.",
                "Process Settings Changed", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        var selectedItem = (GClass22)ClassList.SelectedItem;
        GClass61.gclass61_0.method_0("CustomClassName", selectedItem.string_1);
        GClass61.gclass61_0.method_0("AllowNetCheck", AllowNetCheck.Checked.ToString());
        GClass61.gclass61_0.method_0("AllowWW", AllowWW.Checked.ToString());
        GClass61.gclass61_0.method_0("ManageGamePos", ManageGamePos.Checked.ToString());
        GClass61.gclass61_0.method_0("UseMediaKeys", MediaKeysBox.Checked.ToString());
        GClass61.gclass61_0.method_0("AutoSkin", AutoSkin.Checked.ToString());
        GClass61.gclass61_0.method_0("NinjaSkin", NinjaSkin.Checked.ToString());
        GClass61.gclass61_0.method_0("WalkLoot", WalkLoot.Checked.ToString());
        GClass61.gclass61_0.method_0("ResetBuffs", ResetBuffs.Checked.ToString());
        GClass61.gclass61_0.method_0("Resurrect", Resurrect.Checked.ToString());
        GClass61.gclass61_0.method_0("AltLayout", AltLayout.Checked.ToString());
        GClass61.gclass61_0.method_0("ChatDelete", ChatDelete.Checked.ToString());
        GClass61.gclass61_0.method_0("ChatWhisper", PlayWhisper.Checked.ToString());
        GClass61.gclass61_0.method_0("PlaySay", PlaySay.Checked.ToString());
        GClass61.gclass61_0.method_0("SoundKill", SoundKill.Checked.ToString());
        GClass61.gclass61_0.method_0("ChatAutoReply", AutoReply.Checked.ToString());
        GClass61.gclass61_0.method_0("ChatAutoReplyText", AutoReplyText.Text.Trim());
        GClass61.gclass61_0.method_0("RemoveDebuffs", RemoveDebuffs.Checked.ToString());
        GClass61.gclass61_0.method_0("AppKey", ProductKeyBox.Text);
        GClass61.gclass61_0.method_0("UseClipboard", UseClipboard.Checked.ToString());
        GClass61.gclass61_0.method_0("SkipLoot", SkipLoot.Checked.ToString());
        GClass61.gclass61_0.method_0("TeleportStop", TeleportStop.Checked.ToString());
        GClass61.gclass61_0.method_0("TeleportLogout", TeleportLogout.Checked.ToString());
        GClass61.gclass61_0.method_0("UseBandages", UseBandages.Checked.ToString());
        GClass61.gclass61_0.method_0("JumpMore", JumpMore.Checked.ToString());
        GClass61.gclass61_0.method_0("Strafe", Strafe.Checked.ToString());
        GClass61.gclass61_0.method_0("FastEat", FastEat.Checked.ToString());
        GClass61.gclass61_0.method_0("SitWhenBored", SitWhenBored.Checked.ToString());
        GClass61.gclass61_0.method_0("PickupJunk", PickupJunk.Checked.ToString());
        GClass61.gclass61_0.method_0("AllowAutoSecCheck", AllowAutoSecCheck.Checked.ToString());
        GClass61.gclass61_0.method_0("FightPlayers", FightPlayers.Checked.ToString());
        GClass61.gclass61_0.method_0("StopOnVanish", StopOnVanish.Checked.ToString());
        GClass61.gclass61_0.method_0("StopLootingWhenFull", StopLootingWhenFull.Checked.ToString());
        GClass61.gclass61_0.method_0("StopWhenFull", StopWhenFull.Checked.ToString());
        GClass61.gclass61_0.method_0("UseHook", UseHook.Checked.ToString());
        GClass61.gclass61_0.method_0("MouseSpin", MouseSpin.Checked.ToString());
        GClass61.gclass61_0.method_0("ShiftLoot", ShiftLoot.Checked.ToString());
        GClass61.gclass61_0.method_0("BackgroundEnable", BackgroundEnable.Checked.ToString());
        GClass61.gclass61_0.method_0("UseTray", UseTray.Checked.ToString());
        GClass61.gclass61_0.method_0("TurboLoot", TurboLoot.Checked.ToString());
        GClass61.gclass61_0.method_0("WebNotifyEnabled", WebNotifyEnabled.Checked.ToString());
        GClass61.gclass61_0.method_0("WebNotifyURL", WebNotifyURL.Text);
        GClass61.gclass61_0.method_0("WebNotifyCredentials", WebNotifyCredentials.Text);
        if (AutoLogCharacter.SelectedIndex == 0)
            GClass61.gclass61_0.method_0("AutoLog", "");
        else
            GClass61.gclass61_0.method_0("AutoLog", AutoLogCharacter.Items[AutoLogCharacter.SelectedIndex].ToString());
        if (StopAfter.Checked)
            GClass61.gclass61_0.method_0("AutoStop", "True");
        else
            GClass61.gclass61_0.method_0("AutoStop", "False");
        if (ProductKeyBox.Text.Trim().Length == 0)
            GClass61.gclass61_0.method_0("AppKey", "demo");
        if (StartupClass.smethod_19(SpellLeadDelay.Text))
            GClass61.gclass61_0.method_0("SpellLeadDelay", SpellLeadDelay.Text);
        if (StartupClass.smethod_19(ExtraPull.Text))
            GClass61.gclass61_0.method_0("ExtraPull", ExtraPull.Text);
        if (StartupClass.smethod_19(StopAfterMinutes.Text))
            GClass61.gclass61_0.method_0("AutoStopMinutes", StopAfterMinutes.Text);
        if (StartupClass.smethod_19(MaxResurrect.Text))
            GClass61.gclass61_0.method_0("MaxResurrect", MaxResurrect.Text);
        if (StartupClass.smethod_19(RestHealth.Text))
            GClass61.gclass61_0.method_0("RestHealth", RestHealth.Text);
        if (StartupClass.smethod_19(RestMana.Text))
            GClass61.gclass61_0.method_0("RestMana", RestMana.Text);
        if (StartupClass.smethod_19(KeyDelay.Text))
            GClass61.gclass61_0.method_0("KeyDelay", KeyDelay.Text);
        if (StartupClass.smethod_19(FoodAmount.Text))
            GClass61.gclass61_0.method_0("FoodAmount", FoodAmount.Text);
        if (StartupClass.smethod_19(AmmoAmount.Text))
            GClass61.gclass61_0.method_0("AmmoAmount", AmmoAmount.Text);
        if (StartupClass.smethod_19(WaterAmount.Text))
            GClass61.gclass61_0.method_0("WaterAmount", WaterAmount.Text);
        if (StartupClass.smethod_19(PawSpeed.Text))
            GClass61.gclass61_0.method_0("PawSpeed", PawSpeed.Text);
        if (StartupClass.smethod_19(HarvestRange.Text))
            GClass61.gclass61_0.method_0("HarvestRange", HarvestRange.Text);
        if (StartupClass.smethod_19(BandageHealth.Text))
            GClass61.gclass61_0.method_0("BandageHealth", BandageHealth.Text);
        if (method_2(FriendAlert.Text))
            GClass61.gclass61_0.method_0("FriendAlert", FriendAlert.Text);
        if (StartupClass.smethod_19(FriendLogout.Text))
            GClass61.gclass61_0.method_0("FriendLogout", FriendLogout.Text);
        if (StartupClass.smethod_19(MaxPopups.Text))
            GClass61.gclass61_0.method_0("MaxPopups", MaxPopups.Text);
        GClass61.gclass61_0.method_0("LootCheckHostiles", LootCheckHostiles.Checked.ToString());
        if (StartupClass.smethod_19(LootSafeDistance.Text))
            GClass61.gclass61_0.method_0("LootCheckDistance", LootSafeDistance.Text);
        if (method_2(MeleeDistance.Text))
            GClass61.gclass61_0.method_0("MeleeDistance", MeleeDistance.Text);
        if (method_2(RangedDistance.Text))
            GClass61.gclass61_0.method_0("RangedDistance", RangedDistance.Text);
        GClass61.gclass61_0.method_0("AvoidSameFaction", AvoidSameFaction.Checked.ToString());
        GClass61.gclass61_0.method_0("AvoidOtherFaction", AvoidOtherFaction.Checked.ToString());
        GClass61.gclass61_0.method_0("PartyAdds", PartyAdds.Checked.ToString());
        GClass61.gclass61_0.method_0("PartyBuff", PartyBuff.Checked.ToString());
        GClass61.gclass61_0.method_0("PartySlashFollow", PartySlashFollow.Checked.ToString());
        switch (PartyHealMode.SelectedIndex)
        {
            case 0:
                GClass61.gclass61_0.method_0("PartyHealMode", "Dedicated");
                break;
            case 1:
                GClass61.gclass61_0.method_0("PartyHealMode", "Normal");
                break;
            case 2:
                GClass61.gclass61_0.method_0("PartyHealMode", "Never");
                break;
        }

        if (StartupClass.smethod_19(PartyLooters.Text))
            GClass61.gclass61_0.method_0("PartyLooters", PartyLooters.Text);
        if (StartupClass.smethod_19(PartyLootPos.Text))
            GClass61.gclass61_0.method_0("PartyLootPos", PartyLootPos.Text);
        if (StartupClass.smethod_19(PartyAttackDelay.Text))
            GClass61.gclass61_0.method_0("PartyAttackDelay", PartyAttackDelay.Text);
        if (StartupClass.smethod_19(PartyLeaderWait.Text))
            GClass61.gclass61_0.method_0("PartyLeaderWait", PartyLeaderWait.Text);
        if (StartupClass.smethod_19(PartyFollowerStart.Text))
            GClass61.gclass61_0.method_0("PartyFollowerStart", PartyFollowerStart.Text);
        if (StartupClass.smethod_19(PartyFollowerStop.Text))
            GClass61.gclass61_0.method_0("PartyFollowerStop", PartyFollowerStop.Text);
        if (PartySolo.Checked)
            GClass61.gclass61_0.method_0("PartyMode", "Solo");
        if (PartyLeader.Checked)
            GClass61.gclass61_0.method_0("PartyMode", "Leader");
        if (PartyFollower.Checked)
            GClass61.gclass61_0.method_0("PartyMode", "Follower");
        GClass61.gclass61_0.method_0("PartyLeaderName", PartyLeaderName.Text);
        GClass61.gclass61_0.method_0("PartyMember1", PartyMember1.Text.Trim());
        GClass61.gclass61_0.method_0("PartyMember2", PartyMember2.Text.Trim());
        GClass61.gclass61_0.method_0("PartyMember3", PartyMember3.Text.Trim());
        GClass61.gclass61_0.method_0("PartyMember4", PartyMember4.Text.Trim());
        GClass61.gclass61_0.method_0("ListenEnabled", ListenEnabled.Checked.ToString());
        if (StartupClass.smethod_19(ListenPort.Text))
            GClass61.gclass61_0.method_0("ListenPort", ListenPort.Text);
        GClass61.gclass61_0.method_0("ListenPassword", ListenPassword.Text);
        GClass61.gclass61_0.method_0("RelogEnabled", RelogEnabled.Checked.ToString());
        GClass61.gclass61_0.method_0("BypassLootSanity", BypassLootSanity.Checked.ToString());
        GClass61.gclass61_0.method_0("StrafeObstacles", StrafeObstacles.Checked.ToString());
        GClass61.gclass61_0.method_0("ChatLogFrame", ChatLogFrame.Text.Trim());
        GClass61.gclass61_0.method_0("CombatLogFrame", CombatLogFrame.Text.Trim());
        method_1("LastProfile", InitialProfile);
        method_1("Profile1", Profile1);
        method_1("Profile2", Profile2);
        method_1("Profile3", Profile3);
        if (DisplayNormal.Checked)
            GClass61.gclass61_0.method_0("BackgroundDisplay", "Normal");
        if (DisplayHide.Checked)
            GClass61.gclass61_0.method_0("BackgroundDisplay", "Hide");
        if (DisplayShrink.Checked)
            GClass61.gclass61_0.method_0("BackgroundDisplay", "Shrink");
        if (VendGrey.Checked)
            GClass61.gclass61_0.method_0("VendType", "Poor");
        if (VendWhite.Checked)
            GClass61.gclass61_0.method_0("VendType", "Common");
        if (VendGreen.Checked)
            GClass61.gclass61_0.method_0("VendType", "Uncommon");
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
        GClass61.gclass61_0.method_0("VendWhiteList", str1);
        GClass61.gclass61_0.method_8();
        GClass61.gclass61_0.method_0("SendMail", SendMail.Checked.ToString());
        if (MailToText.Text.Length < 1)
            GClass61.gclass61_0.method_0("MailToText", "");
        else
            GClass61.gclass61_0.method_0("MailToText", MailToText.Text.Trim());
        if (SubjectText.Text.Length < 1)
            GClass61.gclass61_0.method_0("SubjectText", "");
        else
            GClass61.gclass61_0.method_0("SubjectText", SubjectText.Text.Trim());
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
        GClass61.gclass61_0.method_0("VendMailList", str3);
        GClass61.gclass61_0.method_8();
        StartupClass.IsGliderInitialized = false;
        method_18();
        StartupClass.MainWindowHandle = null;
        DialogResult = DialogResult.OK;
    }

    private void method_1(string string_0, Label label_0)
    {
        if (!(label_0.Text != MessageProvider.GetMessage(771)))
            return;
        GClass61.gclass61_0.method_0(string_0, label_0.Text);
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
        var selectedItem = (GClass22)ClassList.SelectedItem;
        selectedItem.method_0();
        var object0 = (GGameClass)selectedItem.object_0;
        Logger.LogMessage("Calling show config on " + selectedItem.string_0);
        var gconfigResult = object0.ShowConfiguration();
        switch (gconfigResult)
        {
            case GConfigResult.NotSupported:
                var num = (int)MessageBox.Show(this, MessageProvider.GetMessage(852), GProcessMemoryManipulator.smethod_0(),
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                break;
            case GConfigResult.Accept:
                object0.LoadConfig();
                break;
        }

        if (gconfigResult != GConfigResult.Accept)
            return;
        if (GClass61.gclass61_0.method_2("RemindActionBars") == null)
            method_3();
        StartupClass.bool_29 = true;
    }

    private void MyHelpButton_Click(object sender, EventArgs e)
    {
        var helpString = helpProvider_0.GetHelpString(tabControl1.SelectedTab);
        if (helpString == null || helpString.Length <= 0)
            return;
        GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, helpString);
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
        GClass42.gclass42_0.method_14();
    }

    private void LoadKeymap_Click(object sender, EventArgs e)
    {
        GClass42.gclass42_0.method_12();
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
        StartupClass.gclass54_0.bool_4 = true;
    }

    private void method_6(object sender, EventArgs e)
    {
        StartupClass.gclass54_0.bool_4 = true;
    }

    private void PartyMember2_TextChanged(object sender, EventArgs e)
    {
        StartupClass.gclass54_0.bool_4 = true;
    }

    private void method_7(object sender, EventArgs e)
    {
        StartupClass.gclass54_0.bool_4 = true;
    }

    private void PartyMember3_TextChanged(object sender, EventArgs e)
    {
        StartupClass.gclass54_0.bool_4 = true;
    }

    private void method_8(object sender, EventArgs e)
    {
        StartupClass.gclass54_0.bool_4 = true;
    }

    private void PartyMember4_TextChanged(object sender, EventArgs e)
    {
        StartupClass.gclass54_0.bool_4 = true;
    }

    private void method_9(object sender, EventArgs e)
    {
        StartupClass.gclass54_0.bool_4 = true;
    }

    private void ListenEnabled_CheckedChanged(object sender, EventArgs e)
    {
        ListenPort.Enabled = ListenEnabled.Checked;
        ListenPassword.Enabled = ListenEnabled.Checked;
    }

    private void ClassList_SelectedIndexChanged(object sender, EventArgs e)
    {
        StartupClass.bool_29 = true;
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
            StartupClass.DebuffsKnown_string.method_10();
        var debuffList = new DebuffList();
        debuffList.method_0();
        if (debuffList.sortedList_0.Keys.Count == 0)
        {
            var num1 = (int)MessageBox.Show(this, MessageProvider.smethod_4("DebuffList.NoneNew"),
                MessageProvider.smethod_4("DebuffList.NoneNewTitle"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        else
        {
            var num2 = (int)debuffList.ShowDialog(this);
            DebuffsKnown.Text = MessageProvider.smethod_6("Config.DebuffsKnown", StartupClass.DebuffsKnown_string.method_9());
        }
    }

    private void AutoSkin_CheckedChanged(object sender, EventArgs e)
    {
        NinjaSkin.Enabled = AutoSkin.Checked;
    }

    private void BackgroundEnable_CheckedChanged(object sender, EventArgs e)
    {
        groupBox25.Enabled = BackgroundEnable.Checked;
        if (bool_1 || !AllowWW.Checked || GClass71.smethod_2(false) != GEnum9.const_1)
            return;
        var num = (int)MessageBox.Show(this, MessageProvider.GetMessage(858), GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.OK,
            MessageBoxIcon.Hand);
        BackgroundEnable.Checked = false;
    }

    private void method_11()
    {
        if (!Directory.Exists("Classes"))
            Directory.CreateDirectory("Classes");
        var files = Directory.GetFiles("Classes", "*.cs");
        GClass61.gclass61_0.method_10("CustomClasses");
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
        var assembly_0 = GClass74.smethod_0(selectedItem, out string_1);
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
            profile.object_0 = GClass74.smethod_7(selectedItem, assembly_0, false, true);
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
        if (GClass61.gclass61_0.method_11("CustomClasses", selectedItem))
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
                        GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    e.NewValue = CheckState.Checked;
                    return;
                }

                ClassList.Items.Remove(profile);
            }

            GClass74.smethod_5(selectedItem);
            GClass61.gclass61_0.method_13("CustomClasses", selectedItem);
        }
        else
        {
            string string_1;
            if (GClass74.smethod_13(selectedItem, out string_1))
            {
                GClass61.gclass61_0.method_12("CustomClasses", selectedItem);
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

    private void AllowWW_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void AccountCreate_Click(object sender, EventArgs e)
    {
        if (Directory.GetFiles("Accounts\\", "*.xml").Length == 0 && MessageBox.Show(this, MessageProvider.GetMessage(867),
                GProcessMemoryManipulator.smethod_0(), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            GProcessMemoryManipulator.smethod_44(this, "Glider.chm", HelpNavigator.Topic, "AutoLogin.html");
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
        var str1 = GClass61.gclass61_0.method_2("AutoLog");
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

    private void DoSecCheck_Click(object sender, EventArgs e)
    {
        StartupClass.smethod_58();
    }

    private void GliderDebug_CheckedChanged(object sender, EventArgs e)
    {
        GClass61.gclass61_0.method_0("GliderDebug", GliderDebug.Checked.ToString());
    }

    protected void method_18()
    {
        KeyEditClass.Items.Add("(Select...)");
        foreach (var gkey in GClass42.gclass42_0.sortedList_0.Values)
            if (gkey.KeyName.IndexOf('.') > 0)
            {
                var key = gkey.KeyName.Substring(0, gkey.KeyName.IndexOf('.'));
                if (!sortedList_0.ContainsKey(key))
                {
                    var str = key;
                    var string_6 = "Common.Class" + key;
                    if (MessageProvider.smethod_5(string_6))
                        str = MessageProvider.smethod_4(string_6);
                    sortedList_0.Add(key, str);
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
        foreach (var key in sortedList_0.Keys)
            if (sortedList_0[key] == str)
            {
                method_19(key, sortedList_0[key]);
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
        return GClass61.gclass61_0.method_2("AppKey").StartsWith("02") ||
               MessageProvider.gclass30_0.string_0.ToLower().IndexOf("zh") > -1;
    }
}