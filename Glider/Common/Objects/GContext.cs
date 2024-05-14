// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GContext
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Glider.Common.Objects
{
    public class GContext
    {
        public delegate void GChatLogHandler(string RawText, string ParsedText);

        public delegate void GCombatLogHandler(string RawText);

        public const double TOLERANCE_SPELLCASTING = 1.57;
        public const int UPDATE_DURATION = 50;
        public const int SS_NONE = 0;
        public const int SS_SHIFT = 1;
        public const int SS_CTRL = 2;
        public const int SS_ALT = 4;
        public static GContext Main;
        private GSpellTimer _autoStop;
        private int _spellLeadDelay;
        private readonly GSpellTimer _spinFutility;
        public SortedList<string, object> Cache = new SortedList<string, object>();
        public GInterfaceHelper Interface;
        public GItemHelper Items;
        private readonly GSpellTimer LastCastFast = new GSpellTimer(2000, true);
        public GPlayerSelf Me;
        public GMovement Movement;
        public GPartyHelper Party;
        protected bool Running;
        protected string SpinningKey;
        public GTendency T_Skinnable;
        protected double TargetHeading;

        public GContext()
        {
            Main = this;
            Memory = new GMemory();
            Interface = new GInterfaceHelper();
            Items = new GItemHelper();
            Movement = new GMovement();
            Running = false;
            SpinningKey = null;
            Me = null;
            Party = new GPartyHelper();
            _spinFutility = new GSpellTimer(4000);
            T_Skinnable = new GTendency();
            ApplyConfig();
        }

        public bool MouseSpin { get; private set; }

        public Random RNG => StartupClass.random_0;

        public bool IsRunning => Running;

        public bool IsSpinning => MouseSpin ? StartupClass.gclass68_0.method_9() : SpinningKey != null;

        public bool Overspin => MouseSpin
            ? StartupClass.gclass68_0.method_9() && _spinFutility.IsReady
            : SpinningKey != null && _spinFutility.IsReady;

        public double MeleeDistance { get; private set; }

        public double RangedDistance { get; private set; }

        public double RestHealth { get; private set; }

        public double RestMana { get; private set; }

        public GGlideMode CurrentMode
        {
            get
            {
                switch (StartupClass.glideMode_0)
                {
                    case GlideMode.None:
                        return GGlideMode.None;
                    case GlideMode.Manual:
                        return GGlideMode.OneKill;
                    case GlideMode.Auto:
                        return GGlideMode.Glide;
                    default:
                        throw new NotImplementedException("Don't know about GlideMode: " + StartupClass.glideMode_0);
                }
            }
        }

        public int MaxCombatDuration => 90000;

        public bool IsGliding => StartupClass.glideMode_0 != GlideMode.None;

        public bool IsAttached => StartupClass.bool_13;

        public bool StoppedOnDetach => StartupClass.bool_36;

        public string RedMessage =>
            GProcessMemoryManipulator.smethod_9(GClass18.gclass18_0.method_4(nameof(RedMessage)), 128, nameof(RedMessage));

        public bool IsManualKill => StartupClass.glideMode_0 == GlideMode.Manual;

        public GProfile Profile => StartupClass.gprofile_0;

        public bool IsCorpseNearby => GClass5.smethod_1();

        public bool IsGliderRunning =>
            StartupClass.ApplicationStartupMode == AppMode.Invisible || StartupClass.ApplicationStartupMode == AppMode.Normal;

        public GMoveHelper MoveHelper { get; private set; }

        public string WorldMap => GProcessMemoryManipulator.smethod_9(GClass18.gclass18_0.method_4(nameof(WorldMap)), 64, "worldmap")
            .Substring(11);

        public string ZoneText =>
            GProcessMemoryManipulator.smethod_9(GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4(nameof(ZoneText)), "zonetext"), 64,
                "zonetext");

        public string SubZoneText
        {
            get
            {
                var str = GProcessMemoryManipulator.smethod_9(
                    GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4(nameof(SubZoneText)), "subzonetext"), 64,
                    "subzonetext");
                return str.Length > 1 ? str : "n/a";
            }
        }

        public bool IsAutoStopReady => _autoStop != null && _autoStop.IsReady;

        public string Version => "1.8.0";

        public string SubVersion => "Release";

        public GMemory Memory { get; }

        public event GCombatLogHandler CombatLog;

        public event GChatLogHandler ChatLog;

        public void ApplyConfig()
        {
            MeleeDistance = GClass61.gclass61_0.method_4("MeleeDistance");
            RangedDistance = GClass61.gclass61_0.method_4("RangedDistance");
            RestHealth = GClass61.gclass61_0.method_4("RestHealth") / 100.0;
            RestMana = GClass61.gclass61_0.method_4("RestMana") / 100.0;
            MouseSpin = GClass61.gclass61_0.method_5("MouseSpin");
            _spellLeadDelay = GClass61.gclass61_0.method_3("SpellLeadDelay");
            GMonster.LogChecks = GClass61.gclass61_0.method_5("LogMonsterChecks");
            if (StartupClass.gclass68_0 != null)
                StartupClass.gclass68_0.method_1();
            if (GetConfigBool("AutoStop"))
                _autoStop = new GSpellTimer(60000 * GetConfigInt("AutoStopMinutes"));
            else
                _autoStop = null;
        }

        public void KillAction(string Why, bool AlsoDetach)
        {
            Running = false;
            StartupClass.smethod_27(AlsoDetach, Why);
        }

        public GConfigResult ShowStockConfigDialog(GPlayerClass WhichClass)
        {
            Form form = null;
            switch (WhichClass)
            {
                case GPlayerClass.Warrior:
                    form = new WarriorConfig();
                    break;
                case GPlayerClass.Paladin:
                    form = new PaladinConfig();
                    break;
                case GPlayerClass.Hunter:
                    form = new HunterConfig();
                    break;
                case GPlayerClass.Rogue:
                    form = new RogueConfig();
                    break;
                case GPlayerClass.Priest:
                    form = new PriestConfig();
                    break;
                case GPlayerClass.Deathknight:
                    form = new DeathknightConfig();
                    break;
                case GPlayerClass.Shaman:
                    form = new ShamanConfig();
                    break;
                case GPlayerClass.Mage:
                    form = new MageConfig();
                    break;
                case GPlayerClass.Warlock:
                    form = new WarlockConfig();
                    break;
                case GPlayerClass.Druid:
                    form = new DruidConfig();
                    break;
            }

            if (form == null)
                return GConfigResult.NotSupported;
            return form.ShowDialog() == DialogResult.OK ? GConfigResult.Accept : GConfigResult.Cancel;
        }

        public void Log(string What)
        {
            GClass37.smethod_0(What);
        }

        public void Debug(string What)
        {
            GClass37.smethod_1(What);
        }

        public void SetConfigValue(string Name, string Value, bool ReplaceIfPresent)
        {
            if (!ReplaceIfPresent && IsConfigured(Name))
                return;
            if (Name.IndexOf('.') == -1)
                throw new ArgumentException("Cannot change values of root config items");
            GClass61.gclass61_0.method_0(Name, Value);
        }

        public void SaveConfig()
        {
            GClass61.gclass61_0.method_8();
        }

        public void AddAutoKey(string KeyName)
        {
            if (GClass42.gclass42_0.sortedList_0.ContainsKey(KeyName))
                return;
            GClass42.gclass42_0.method_4(KeyName);
        }

        public void SetKeyValue(
            string KeyName,
            GBarState RequiredBarState,
            int ShiftState,
            char TheChar)
        {
            GClass42.gclass42_0.method_5(KeyName, RequiredBarState, ShiftState, TheChar, 0);
        }

        public void SetKeyValue(string KeyName, GBarState RequiredBarState, int ShiftState, short VK)
        {
            GClass42.gclass42_0.method_5(KeyName, RequiredBarState, ShiftState, char.MinValue, VK);
        }

        public void SetSpellKey(string KeyName, int ShiftState, int SpellID)
        {
            GClass42.gclass42_0.method_8(KeyName, SpellID, ShiftState);
        }

        public void SetItemKey(string KeyName, int ItemID)
        {
            GClass42.gclass42_0.method_7(KeyName, ItemID);
        }

        public bool IsConfigured(string Name)
        {
            return GetConfigString(Name) != null;
        }

        public string GetConfigString(string Name)
        {
            return IsUnsafeConfig(Name) ? "demo" : GClass61.gclass61_0.method_2(Name);
        }

        public int GetConfigInt(string Name)
        {
            return IsUnsafeConfig(Name) ? 0 : GClass61.gclass61_0.method_3(Name);
        }

        public bool GetConfigBool(string Name)
        {
            return !IsUnsafeConfig(Name) && GClass61.gclass61_0.method_5(Name);
        }

        public double GetConfigDouble(string Name)
        {
            return IsUnsafeConfig(Name) ? 0.0 : GClass61.gclass61_0.method_4(Name);
        }

        private bool IsUnsafeConfig(string Name)
        {
            return Name == "AppKey" || Name.StartsWith("Remote");
        }

        public bool CastSpellWithInterrupt(
            string KeyName,
            GUnit Target,
            double InterruptMin,
            string InterruptKey,
            bool StopOnInterrupt)
        {
            var flag = false;
            Interface.WaitForReady(KeyName);
            Debug("CastSpellWithInterrupt: \"" + KeyName + "\", Target=" + Target.Name + ", InterruptMin=" +
                  InterruptMin + ", InterruptKey=" + InterruptKey + ", StopOnInterrupt=" + StopOnInterrupt);
            if (!LastCastFast.IsReady)
            {
                Debug("Sleeping for spell lead delay with interrupt");
                Thread.Sleep(_spellLeadDelay);
            }

            LastCastFast.Reset();
            SendKey(KeyName);
            Thread.Sleep(600);
            if (Me.IsCasting)
            {
                flag = true;
                var gspellTimer = new GSpellTimer(15000);
                while (!gspellTimer.IsReady && Me.IsCasting)
                {
                    if (Target.IsCasting && Target.Health <= InterruptMin && Interface.IsKeyReady(InterruptKey))
                    {
                        Debug("Target is casting below life limit, interrupting");
                        if (!StopOnInterrupt)
                        {
                            SendKey(InterruptKey);
                        }
                        else
                        {
                            SendKey("Common.Escape");
                            Thread.Sleep(401);
                            CastSpell(InterruptKey, true, true);
                            break;
                        }
                    }

                    Thread.Sleep(101);
                }
            }

            return flag;
        }

        public bool CastSpell(string KeyName, bool WaitGCD, bool FastReturn)
        {
            return CastSpell(KeyName, WaitGCD, FastReturn, 600);
        }

        public bool CastSpell(string KeyName, bool WaitGCD, bool FastReturn, int ChannelWaitTime)
        {
            var flag = false;
            if (!Interface.IsKeyPopulated(KeyName))
            {
                Debug("Skipping cast of \"" + KeyName + "\", is not populated");
                return false;
            }

            if (WaitGCD)
                Interface.WaitForReady(KeyName);
            if (!LastCastFast.IsReady)
            {
                Debug("Sleeping for spell lead delay with no interrupt");
                Thread.Sleep(_spellLeadDelay);
            }

            LastCastFast.Reset();
            Debug("CastSpell: \"" + KeyName + "\", WaitGCD=" + WaitGCD + ", FastReturn=" + FastReturn);
            SendKey(KeyName);
            Thread.Sleep(FastReturn ? 101 : ChannelWaitTime);
            if (Me.IsCasting && !FastReturn)
            {
                flag = true;
                var gspellTimer = new GSpellTimer(15000);
                while (!gspellTimer.IsReady && Me.IsCasting)
                    Thread.Sleep(101);
            }

            return flag;
        }

        public bool CastSpell(string KeyName)
        {
            return CastSpell(KeyName, true, false);
        }

        public void SendKey(string KeyName)
        {
            GClass37.smethod_1("SendKey: \"" + KeyName + "\"");
            if (GClass42.gclass42_0.sortedList_0.ContainsKey(KeyName))
            {
                GClass42.gclass42_0.sortedList_0[KeyName].FilloutKey();
                GClass42.gclass42_0.method_0(KeyName);
            }
            else
            {
                GClass37.smethod_0("!! SendKey failed, missing key: \"" + KeyName + "\"");
            }
        }

        public void PressKey(string KeyName)
        {
            GClass42.gclass42_0.method_1(KeyName);
        }

        public void ReleaseKey(string KeyName)
        {
            GClass42.gclass42_0.method_2(KeyName);
        }

        public void ReleaseAllKeys()
        {
            StartupClass.gclass68_0.method_3(false);
            Running = false;
            SpinningKey = null;
            GClass55.smethod_27();
        }

        public void ReleaseSpinRun()
        {
            ReleaseRun();
            ReleaseSpin();
        }

        public void StartRun()
        {
            if (Running)
                return;
            Running = true;
            PressKey("Common.Forward");
        }

        public void ReleaseRun()
        {
            if (!Running)
                return;
            Running = false;
            ReleaseKey("Common.Forward");
        }

        public void StartSpinTowards(double NewHeading)
        {
            var num = Movement.CompareHeadings(NewHeading, Me.Heading);
            if (Math.Abs(num) < 0.03)
            {
                GClass37.smethod_1("Close enough to current, not bothering with spin");
            }
            else
            {
                if (MouseSpin)
                {
                    StartupClass.gclass68_0.method_4(NewHeading);
                }
                else
                {
                    TargetHeading = NewHeading;
                    var str = num <= 0.0 ? "Common.RotateRight" : "Common.RotateLeft";
                    if (SpinningKey != null)
                    {
                        if (str == SpinningKey)
                            return;
                        GClass37.smethod_1("Sudden spin change!");
                        ReleaseSpin();
                    }

                    SpinningKey = str;
                    PressKey(SpinningKey);
                }

                _spinFutility.Reset();
            }
        }

        public void PulseSpin()
        {
            PulseSpin(true);
        }

        public void PulseSpin(bool Fast)
        {
            if (MouseSpin && StartupClass.gclass68_0.method_9())
                StartupClass.gclass68_0.method_8(Fast);
            if (MouseSpin || SpinningKey == null ||
                Math.Abs(Movement.CompareHeadings(GPlayerSelf.Me.Heading, TargetHeading)) >= Math.PI / 18.0)
                return;
            ReleaseSpin();
        }

        public void ReleaseSpin()
        {
            if (MouseSpin)
                StartupClass.gclass68_0.method_3(false);
            else
                lock (this)
                {
                    if (SpinningKey == null)
                        return;
                    GClass37.smethod_1("StopSpin");
                    ReleaseKey(SpinningKey);
                    SpinningKey = null;
                }
        }

        public void ClearTarget()
        {
            Me.Refresh(true);
            if (Me.TargetGUID == 0L)
                return;
            SendKey("Common.Escape");
        }

        public bool RemoveDebuffs(GBuffType DType, string SpellName, bool InCombat)
        {
            return StartupClass.DebuffsKnown_string.method_7((GEnum4)DType, SpellName, InCombat);
        }

        public GCombatResult CheckCommonCombatResult(GMonster Monster, bool WasAmbush)
        {
            if (!Monster.IsValid)
            {
                Log("Monster is gone from object list, ending combat");
                return GCombatResult.Vanished;
            }

            if (Monster.IsDead)
                return GCombatResult.Success;
            if (Me.IsDead)
            {
                Log("Player is dead, ending combat");
                return GCombatResult.Died;
            }

            if (Monster.IsNotDying)
            {
                Log("Monster's health is not dropping, ending combat");
                return GCombatResult.Bugged;
            }

            if (Monster.IsTagged && !Monster.IsMine && !WasAmbush && CurrentMode == GGlideMode.Glide)
            {
                Log("Monster is tagged by another player, ending combat");
                return GCombatResult.OtherPlayerTag;
            }

            if (StartupClass.CurrentGameClass.TicksSinceCombatStart > MaxCombatDuration)
            {
                Log("Monster is taking too long to die, ending combat");
                return GCombatResult.Bugged;
            }

            if (GPlayerSelf.Me.IsInCombat || StartupClass.CurrentGameClass.TicksSinceCombatStart <= 10000)
                return GCombatResult.Unknown;
            Log("We're not in combat, this can't be working out, ending combat");
            return GCombatResult.Bugged;
        }

        public bool StartGlide()
        {
            if (!IsAttached)
            {
                Log("Can't start glide, not attached!");
                return false;
            }

            if (IsGliding)
            {
                Log("Can't start glide, already gliding!");
                return false;
            }

            if (!StartupClass.smethod_24(true))
                return false;
            Thread.Sleep(1500);
            return IsGliding;
        }

        public bool LoadProfile(string Filename)
        {
            return StartupClass.smethod_1(Filename);
        }

        public void FireChatLog(string RawText, string ParsedText)
        {
            try
            {
                if (ChatLog == null)
                    return;
                ChatLog(RawText, ParsedText);
            }
            catch (Exception ex)
            {
                GClass37.smethod_0("!! Exception processing chat log in custom class: " + ex.Message + ex.StackTrace);
            }
        }

        public void FireCombatLog(string RawText)
        {
            try
            {
                if (CombatLog == null)
                    return;
                CombatLog(RawText);
            }
            catch (Exception ex)
            {
                GClass37.smethod_0("!! Exception processing combat log in custom class: " + ex.Message + ex.StackTrace);
            }
        }

        public void OnAttach()
        {
            GClass37.smethod_1("GContext.OnAttach starting");
            Me = null;
            GObjectList.ClearCache();
            GObjectList.GetObjects();
            Interface.OnAttach();
            Items.OnAttach();
            T_Skinnable.Reset();
            GClass37.smethod_1("GContext.OnAttach is done");
        }

        public bool IsHostileNear(GLocation Location)
        {
            if (!GetConfigBool("LootCheckHostiles"))
                return false;
            GUnit nearestHostile = GObjectList.GetNearestHostile(Location, 0L, false);
            if (nearestHostile == null)
                return false;
            var flag = nearestHostile.Location.GetDistanceTo(Location) < (double)GetConfigInt("LootCheckDistance");
            GClass37.smethod_1("Closest hostile is " + nearestHostile + ", TooClose=" + flag + " (distance = " +
                               nearestHostile.DistanceToSelf + ")");
            return flag;
        }

        public GCombatResult WaitForEngage(GMonster Target, int PullDistance)
        {
            var gspellTimer1 = new GSpellTimer(2000, false);
            var gspellTimer2 = new GSpellTimer(4000, false);
            while (!gspellTimer2.IsReady)
            {
                if (Target.IsMine || Target.IsTagged || Target.TargetGUID == Me.GUID)
                    return GCombatResult.Unknown;
                if (!gspellTimer1.IsReady || Me.IsInCombat || Target.DistanceToSelf <= (double)PullDistance)
                {
                    Thread.Sleep(102);
                }
                else
                {
                    Log("Monster appears to have wandered out of pull range");
                    return GCombatResult.Retry;
                }
            }

            Log("Monster could not be pulled");
            return GCombatResult.Bugged;
        }

        public void HearthSoon()
        {
            StartupClass.gclass73_0.bool_2 = true;
        }

        public void HearthAndExit()
        {
            StartupClass.gclass73_0.method_21(false);
        }

        public void HearthSoon(bool AllowResume)
        {
            StartupClass.gclass73_0.bool_2 = true;
            StartupClass.gclass73_0.bool_3 = AllowResume;
        }

        public string GetLocal(int StringID)
        {
            return GClass30.smethod_1(StringID);
        }

        public string GetLocal(string StringID)
        {
            return GClass30.smethod_4(StringID) ?? "(bogus string name: \"" + StringID + "\")";
        }

        public void ExecuteScript(string EventName, bool ForceBase)
        {
            StartupClass.GameMemoryWriter.method_2(EventName, ForceBase);
        }

        public void EnsureGameSelected()
        {
            if (StartupClass.IsGliderInitialized)
                return;
            StartupClass.smethod_22();
        }

        public void DoAutoLogin()
        {
            StartupClass.smethod_56();
        }

        public string GetRandomString()
        {
            return GProcessMemoryManipulator.smethod_0();
        }

        public void DoHearthAction()
        {
            StartupClass.gclass73_0.method_62();
        }

        public void HandlePopups()
        {
            GClass67.smethod_2();
        }

        public bool RegisterMoveHelper(GMoveHelper NewGuy, string DisplayNote)
        {
            if (MoveHelper == null)
            {
                MoveHelper = NewGuy;
                GClass37.smethod_0("Registering GMoveHelper: \"" + DisplayNote + "\"");
                return true;
            }

            GClass37.smethod_0("Can't register GMoveHelper: \"" + DisplayNote +
                               "\", another helper is already registered!");
            return false;
        }

        public void DeRegisterMoveHelper(GMoveHelper OldGuy, string DisplayNote)
        {
            if (MoveHelper == null)
            {
                GClass37.smethod_0("Can't de-register GMoveHelper: \"" + DisplayNote + "\", it wasn't registered!");
            }
            else if (MoveHelper != OldGuy)
            {
                GClass37.smethod_0("Can't de-register GMoveHelper: \"" + DisplayNote +
                                   "\", someone else was registered!");
            }
            else
            {
                GClass37.smethod_0("De-registered GMoveHelper: \"" + DisplayNote + "\"");
                MoveHelper = null;
            }
        }

        public void ResetAutoStop()
        {
            if (_autoStop == null)
                return;
            _autoStop.Reset();
        }

        public void EnableCursorHook()
        {
            if (StartupClass.GliderManager == null)
                return;
            StartupClass.GliderManager.method_33(true);
        }

        public void DisableCursorHook()
        {
            if (StartupClass.GliderManager == null)
                return;
            StartupClass.GliderManager.method_33(false);
        }

        public void WaitForNotFiring(string KeyName)
        {
            var visibleInterfaceObject = GClass42.gclass42_0.sortedList_0[KeyName].FindVisibleInterfaceObject();
            if (visibleInterfaceObject == null)
                return;
            var byName = Interface.GetByName(visibleInterfaceObject);
            var gspellTimer = new GSpellTimer(4000, false);
            do
            {
                ;
            } while (!gspellTimer.IsReadySlow && byName.IsFiring);
        }
    }
}