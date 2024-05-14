// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GUnit
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Glider.Common.Objects
{
    public class GUnit : GObject
    {
        public const double HEADING_TOLERANCE = 0.34;
        protected const int MAX_BUFFS = 56;
        protected new static int PawSpeedMS;
        protected int _castingID;
        protected long _channelingID;
        protected int _channelingSpellID;
        protected long _createdBy;
        protected GCreatureType _creatureType;
        protected int _dflags;
        protected int _factionID;
        protected int _flags;
        protected double _happiness;
        protected double _heading;
        protected double _health;
        protected int _healthMax;
        protected int _healthPoints;
        protected int _lastBuffCheck = Environment.TickCount - 5000;
        protected GBuff[] _lastBuffs;
        protected int _lastHealthDrop;
        protected int _lastHealthPoints;
        protected GLocation _lastLocation;
        protected int _level;
        protected double _mana;
        protected int _manaMax;
        protected int _manaPoints;
        protected int _monsterDefinition;
        protected int _movementFlags1;
        protected int _movementFlags2;
        protected double _pitch;
        protected GReaction _reaction;
        protected long _target;
        protected bool _wasLootable;
        private readonly double DYING_SANITY_TIME = 30000.0;
        private readonly SortedList<string, int[]> WKBuffs;

        public GUnit(int BaseAddress, int FrameNumber)
            : base(BaseAddress, FrameNumber)
        {
            _lastHealthPoints = 0;
            _lastHealthDrop = 0;
            _wasLootable = false;
            WKBuffs = new SortedList<string, int[]>();
        }

        public bool IsPlayer => Type == GObjectType.Player;

        public double Happiness
        {
            get
            {
                Refresh();
                return _happiness;
            }
        }

        public bool IsMonster => Type == GObjectType.Monster;

        public long CreatedBy
        {
            get
            {
                Refresh();
                return _createdBy;
            }
        }

        public GReaction Reaction
        {
            get
            {
                if (_reaction == GReaction.Unknown)
                    try
                    {
                        _reaction = GetReaction(false);
                    }
                    catch (GException1 ex)
                    {
                        Logger.smethod_1("!! Readfailed in GetReaction, object is no longer valid (details=" + ex +
                                           ")");
                        Cull();
                        _reaction = GReaction.Unknown;
                    }

                return _reaction;
            }
        }

        public double Health
        {
            get
            {
                Refresh();
                return _health;
            }
        }

        public int HealthPoints
        {
            get
            {
                Refresh();
                return _healthPoints;
            }
        }

        public int HealthMax
        {
            get
            {
                Refresh();
                return _healthMax;
            }
        }

        public double Mana
        {
            get
            {
                Refresh();
                return _mana;
            }
        }

        public int ManaPoints
        {
            get
            {
                Refresh();
                return _manaPoints;
            }
        }

        public int ManaMax
        {
            get
            {
                Refresh();
                return _manaMax;
            }
        }

        public int Level
        {
            get
            {
                Refresh();
                return _level;
            }
        }

        public int FactionID
        {
            get
            {
                Refresh();
                return _factionID;
            }
        }

        public double Heading
        {
            get
            {
                Refresh();
                return _heading;
            }
        }

        public double Pitch
        {
            get
            {
                Refresh();
                return _pitch;
            }
        }

        public bool IsTargetingMe => TargetGUID == GPlayerSelf.Me.GUID;

        public bool IsTargetingMyPet => TargetGUID == GPlayerSelf.Me.PetGUID && GPlayerSelf.Me.PetGUID != 0L;

        public virtual long TargetGUID
        {
            get
            {
                Refresh();
                return _target;
            }
        }

        public GUnit Target => TargetGUID == 0L ? null : GObjectList.FindUnit(TargetGUID);

        public virtual bool IsDead => HealthPoints == 0 || (HealthPoints <= 1 && (_flags & 262144) > 0);

        public bool IsInCombat
        {
            get
            {
                Refresh();
                return (_flags & 524288) != 0;
            }
        }

        public int CastingID
        {
            get
            {
                Refresh();
                return _castingID;
            }
        }

        public long ChannelingObjectID
        {
            get
            {
                Refresh();
                return _channelingID;
            }
        }

        public long ChannelingSpellID
        {
            get
            {
                Refresh();
                return _channelingSpellID;
            }
        }

        public bool IsCasting => CastingID != 0 || ChannelingObjectID != 0L || ChannelingSpellID != 0L;

        public virtual GCreatureType CreatureType
        {
            get
            {
                Refresh();
                return _creatureType;
            }
        }

        public double Bearing => GContext.Main.Me.GetHeadingDelta(Location);

        public bool IsInMeleeRange => DistanceToSelf <= GContext.Main.MeleeDistance;

        public bool IsCursorOnUnit =>
            GProcessMemoryManipulator.smethod_12(GClass18.gclass18_0.method_4("UnderCursor"), "UnderCursor") == GUID;

        public bool IsFacingAway =>
            Math.Abs(GContext.Main.Movement.CompareHeadings(GContext.Main.Me.Heading, Heading)) < Math.PI / 2.0;

        public int TicksSinceHealthDrop => Environment.TickCount - _lastHealthDrop;

        public bool IsNotDying
        {
            get
            {
                if (Health < 1.0 && TicksSinceHealthDrop > DYING_SANITY_TIME)
                {
                    Logger.smethod_1("Damaged and not dropping");
                    return true;
                }

                if (Health > 0.99 && StartupClass.CurrentGameClass.TicksSinceCombatStart > DYING_SANITY_TIME)
                {
                    Logger.smethod_1("Undamaged and combat taking too long");
                    return true;
                }

                if (StartupClass.GameClass69Instance.method_10() >= 4)
                    return false;
                Logger.smethod_1("Recent evade entry in combat log");
                return true;
            }
        }

        public bool IsApproaching => _lastLocation != null && GContext.Main.Me.Location.GetDistanceTo(Location) <
            (double)GContext.Main.Me.Location.GetDistanceTo(_lastLocation);

        public bool IsLootable
        {
            get
            {
                Refresh();
                return (_dflags & 1) > 0;
            }
        }

        public bool WasLootable
        {
            get
            {
                Refresh();
                return _wasLootable;
            }
        }

        public bool IsSkinnable
        {
            get
            {
                Refresh();
                return (_flags & 67108864) != 0;
            }
        }

        public GRaidTargetIcon RaidTargetIcon
        {
            get
            {
                var num = GClass18.gclass18_0.method_4(nameof(RaidTargetIcon));
                for (var index = 0; index < 8; ++index)
                    if (GProcessMemoryManipulator.smethod_12(num + index * 8, "rti") == GUID)
                        return (GRaidTargetIcon)(index + 1);
                return GRaidTargetIcon.NotSpecified;
            }
        }

        public int MovementFlags1
        {
            get
            {
                Refresh();
                return _movementFlags1;
            }
        }

        public int MovementFlags2
        {
            get
            {
                Refresh();
                return _movementFlags2;
            }
        }

        protected override void LoadFields()
        {
            base.LoadFields();
            _flags = GetStorageInt("UNIT_FIELD_FLAGS");
            _dflags = GetStorageInt("UNIT_DYNAMIC_FLAGS");
            _healthPoints = GetStorageInt("UNIT_FIELD_HEALTH");
            _healthMax = GetStorageInt("UNIT_FIELD_MAXHEALTH");
            _health = _healthMax <= 0 ? 0.0 : _healthPoints / (double)_healthMax;
            _manaPoints = GetStorageInt("UNIT_FIELD_POWER1");
            _manaMax = GetStorageInt("UNIT_FIELD_MAXPOWER1");
            _mana = _manaMax <= 0 ? 0.0 : _manaPoints / (double)_manaMax;
            _level = GetStorageInt("UNIT_FIELD_LEVEL");
            _factionID = GetStorageInt("UNIT_FIELD_FACTIONTEMPLATE");
            _heading = GetBaseFloat("Heading");
            _pitch = GetBaseFloat("Pitch");
            var baseFloat1 = GetBaseFloat("X");
            var baseFloat2 = GetBaseFloat("Y");
            var baseFloat3 = GetBaseFloat("Z");
            _lastLocation = _location;
            _location = new GLocation(baseFloat1, baseFloat2, baseFloat3);
            _createdBy = GetStorageLong("UNIT_FIELD_CREATEDBY");
            _target = GetStorageLong("UNIT_FIELD_TARGET");
            _channelingID = GetStorageLong("UNIT_FIELD_CHANNEL_OBJECT");
            _castingID = GetBaseInt("PlayerCasting");
            _channelingSpellID = GetBaseInt("PlayerCastingAlt");
            _monsterDefinition = GetBaseInt("MonsterDefinition");
            _creatureType = _monsterDefinition == 0
                ? GCreatureType.NoDefinition
                : (GCreatureType)GProcessMemoryManipulator.smethod_11(_monsterDefinition + GClass18.gclass18_0.method_4("CreatureType"),
                    "rct");
            var storageFloat1 = GetStorageFloat("UNIT_FIELD_POWER5");
            var storageFloat2 = GetStorageFloat("UNIT_FIELD_MAXPOWER5");
            _happiness = storageFloat2 != 0.0 ? storageFloat1 / (double)storageFloat2 : 0.0;
            if (_healthPoints < _lastHealthPoints)
            {
                _lastHealthPoints = _healthPoints;
                _lastHealthDrop = Environment.TickCount;
            }

            _lastHealthPoints = _healthPoints;
            if ((_dflags & 1) > 0)
                _wasLootable = true;
            _movementFlags1 = GProcessMemoryManipulator.smethod_11(BaseAddress + GClass18.gclass18_0.method_4("MoveFlags"), "movefl");
            var num = GProcessMemoryManipulator.smethod_11(BaseAddress + GClass18.gclass18_0.method_4("MoveStruct2"), "movest2");
            if (num != 0)
                _movementFlags2 = GProcessMemoryManipulator.smethod_11(num + GClass18.gclass18_0.method_4("MoveFlags2"), "movefl2");
            else
                _movementFlags2 = 0;
        }

        public bool SetAsTarget(bool WasLastHostile)
        {
            if (DistanceToSelf > 10.0)
                Face();
            if (GUID == GPlayerSelf.Me.TargetGUID)
                return true;
            if (GClass61.gclass61_0.method_5("TargetWithMouse"))
            {
                Select();
                if (GUID == GPlayerSelf.Me.TargetGUID)
                    return true;
            }

            for (var index = 4; index > 0; --index)
            {
                var KeyName = "Common.Target";
                if (WasLastHostile && index == 4)
                    KeyName = "Common.TargetLastHostile";
                if (TargetSomething(KeyName) == GUID)
                    return true;
            }

            return false;
        }

        private long TargetSomething(string KeyName)
        {
            var targetGuid = GContext.Main.Me.TargetGUID;
            var gspellTimer = new GSpellTimer(1000, false);
            GContext.Main.SendKey(KeyName);
            do
            {
                ;
            } while (!gspellTimer.IsReadySlow && GContext.Main.Me.TargetGUID == targetGuid);

            return GContext.Main.Me.TargetGUID;
        }

        public double GetHeadingDelta(GLocation Target)
        {
            var headingTo = GContext.Main.Movement.GetHeadingTo(Location, Target);
            return GContext.Main.Movement.CompareHeadings(Heading, headingTo);
        }

        public double GetHeadingTo(GUnit Target)
        {
            return GetHeadingTo(Target.Location);
        }

        public double GetHeadingTo(GLocation TargetLocation)
        {
            var headingTo = GContext.Main.Movement.GetHeadingTo(Location, TargetLocation);
            return headingTo == -1.0 ? Heading : headingTo;
        }

        public void StartSpinTowards()
        {
            GContext.Main.StartSpinTowards(Bearing);
        }

        public void Face()
        {
            if (!IsValid)
                return;
            GContext.Main.Movement.SetHeading(GContext.Main.Me.GetHeadingTo(this), 0.34);
        }

        public void Face(double Tolerance)
        {
            if (!IsValid)
                return;
            GContext.Main.Movement.SetHeading(GContext.Main.Me.GetHeadingTo(this), Tolerance);
        }

        public void WaitForApproach(double Distance, int Milliseconds)
        {
            if (!IsValid)
                return;
            var gspellTimer = new GSpellTimer(Milliseconds, false);
            do
            {
                ;
            } while (!gspellTimer.IsReadySlow && DistanceToSelf > Distance);
        }

        public bool Approach()
        {
            return Approach(GClass61.gclass61_0.method_4("MeleeDistance"));
        }

        public bool Approach(double Distance)
        {
            return Approach(Distance, false);
        }

        public bool Approach(bool LeaveRunning)
        {
            return Approach(GClass61.gclass61_0.method_4("MeleeDistance"), LeaveRunning);
        }

        public bool Approach(double Distance, bool LeaveRunning)
        {
            Logger.smethod_1("Approaching \"" + Name + "\" to distance of " + Distance);
            return GContext.Main.Movement.MoveToUnit(this, Distance, LeaveRunning);
        }

        public bool ApproachSafe(double Distance, bool LeaveRunning)
        {
            Logger.smethod_1("Approaching (safe) \"" + Name + "\" to distance of " + Distance);
            return GContext.Main.Movement.MoveToUnit(this, Distance, LeaveRunning);
        }

        public bool GetBehind(bool Sneaking)
        {
            Logger.smethod_1("GetBehind: \"" + Name + "\", sneaking = " + Sneaking);
            if (IsFacingAway && DistanceToSelf < GClass61.gclass61_0.method_4("MeleeDistance"))
                return true;
            if (DistanceToSelf > 20.0)
            {
                Logger.smethod_1("Too far away to GetBehind: \"" + Name + "\"");
                return false;
            }

            if (IsFacingAway)
            {
                Approach();
                return IsFacingAway && DistanceToSelf < GContext.Main.MeleeDistance;
            }

            if (Sneaking)
            {
                var KeyName = GContext.Main.Movement.CompareHeadings(GContext.Main.Me.Heading, Heading) > 0.0
                    ? "Common.StrafeLeft"
                    : "Common.StrafeRight";
                GContext.Main.PressKey(KeyName);
                var gspellTimer = new GSpellTimer(8000);
                while (!gspellTimer.IsReady)
                {
                    GContext.Main.Me.Refresh(true);
                    Refresh(true);
                    Face();
                    var num = GContext.Main.Movement.CompareHeadings(GContext.Main.Me.Heading, Heading);
                    if (Math.Abs(num) >= 0.4)
                    {
                        if (Math.Abs(num) < Math.PI / 4.0)
                            GContext.Main.StartRun();
                        else
                            GContext.Main.ReleaseRun();
                        Thread.Sleep(61);
                    }
                    else
                    {
                        break;
                    }
                }

                if (gspellTimer.IsReady)
                {
                    GContext.Main.ReleaseAllKeys();
                    return false;
                }

                GContext.Main.ReleaseKey(KeyName);
                Approach();
                GContext.Main.ReleaseAllKeys();
                return IsFacingAway && DistanceToSelf < GContext.Main.MeleeDistance;
            }

            bool flag;
            var KeyName1 = (flag = StartupClass.random_0.Next() % 2 == 0) ? "Common.StrafeRight" : "Common.StrafeLeft";
            var DeltaRads = flag ? Math.PI / 2.0 : -1.0 * Math.PI / 2.0;
            var NewHeading = GContext.Main.Movement.AdjustHeading(GContext.Main.Me.Heading, DeltaRads);
            GContext.Main.StartRun();
            GContext.Main.StartSpinTowards(NewHeading);
            GContext.Main.PressKey(KeyName1);
            var gspellTimer1 = new GSpellTimer(4000);
            while (!gspellTimer1.IsReady)
            {
                GContext.Main.PulseSpin();
                GContext.Main.Me.Refresh(true);
                Refresh(true);
                var headingTo = GContext.Main.Movement.GetHeadingTo(GContext.Main.Me.Location, Location);
                if (Math.Abs(GContext.Main.Movement.CompareHeadings(headingTo, Heading)) >= Math.PI / 2.0)
                    Thread.Sleep(25);
                else
                    break;
            }

            if (gspellTimer1.IsReady)
            {
                GContext.Main.ReleaseAllKeys();
                return false;
            }

            GContext.Main.ReleaseRun();
            Thread.Sleep(250 + StartupClass.random_0.Next() % 250);
            GContext.Main.ReleaseKey(KeyName1);
            Face();
            GContext.Main.Me.Refresh(true);
            Refresh(true);
            GetToMeleeDistance();
            return IsFacingAway && DistanceToSelf < GContext.Main.MeleeDistance;
        }

        private void GetToMeleeDistance()
        {
            if (DistanceToSelf < GContext.Main.MeleeDistance - 2.5)
                BackAwayFrom(GContext.Main.MeleeDistance - 2.5);
            else
                Approach();
        }

        private void BackAwayFrom(double RequestedDistance)
        {
            if (DistanceToSelf >= RequestedDistance)
                return;
            var gspellTimer = new GSpellTimer(3000);
            GContext.Main.PressKey("Common.Back");
            do
            {
                ;
            } while (!gspellTimer.IsReadySlow && DistanceToSelf < RequestedDistance);

            GContext.Main.ReleaseKey("Common.Back");
        }

        public void TouchHealthDrop()
        {
            _lastHealthDrop = Environment.TickCount;
        }

        public void WaitForLootable()
        {
            var gspellTimer = new GSpellTimer(3000, false);
            do
            {
                ;
            } while (gspellTimer.IsReadySlow && !IsLootable);
        }

        protected int GetFactionGroupRow(int CheckBaseAddress)
        {
            var num1 = GProcessMemoryManipulator.smethod_21(GClass18.gclass18_0.method_4("FactionSub"), "facsub");
            var num2 = GProcessMemoryManipulator.smethod_21(
                GProcessMemoryManipulator.smethod_21(CheckBaseAddress + GClass18.gclass18_0.method_4("FactionOff1"), "fac1") +
                GClass18.gclass18_0.method_4("FactionOff2"), "fac2");
            return GProcessMemoryManipulator.smethod_21(
                GProcessMemoryManipulator.smethod_21(GClass18.gclass18_0.method_4("FactionBase"), "fac3") + (num2 - num1) * 4,
                "faclookup");
        }

        protected GReaction GetReaction(bool Debug)
        {
            if (GContext.Main.Me == null)
            {
                if (Debug)
                    GContext.Main.Log("GetReaction failed, no GContext.Main.Me!");
                return GReaction.Unknown;
            }

            var factionGroupRow1 = GetFactionGroupRow(GContext.Main.Me.BaseAddress);
            var factionGroupRow2 = GetFactionGroupRow(BaseAddress);
            if (Debug)
                GContext.Main.Log("Faction rows: Mine = 0x" + factionGroupRow1.ToString("x8") + ", other = 0x" +
                                  factionGroupRow2.ToString("x8"));
            if (factionGroupRow1 != 0 && factionGroupRow2 != 0)
            {
                if ((GProcessMemoryManipulator.smethod_11(factionGroupRow1 + 12, "rf0") &
                     GProcessMemoryManipulator.smethod_11(factionGroupRow2 + 20, "rf1")) > 0)
                {
                    if (Debug)
                        GContext.Main.Log("Hostile at first hostile check");
                    return GReaction.Hostile;
                }

                var int_29_1 = factionGroupRow2 + 24;
                for (var index = 0; index < 4; ++index)
                {
                    var num = GProcessMemoryManipulator.smethod_11(int_29_1, "rf2");
                    if (num != 0)
                    {
                        if (num != GProcessMemoryManipulator.smethod_11(factionGroupRow1 + 4, "rf3"))
                        {
                            int_29_1 += 4;
                        }
                        else
                        {
                            if (Debug)
                                GContext.Main.Log("Hostile at second hostile check");
                            return GReaction.Hostile;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if ((GProcessMemoryManipulator.smethod_11(factionGroupRow1 + 12, "rf4") &
                     GProcessMemoryManipulator.smethod_11(factionGroupRow2 + 16, "rf5")) > 0)
                {
                    if (Debug)
                        GContext.Main.Log("Friendly at first friendly check");
                    return GReaction.Friendly;
                }

                var int_29_2 = factionGroupRow2 + 40;
                for (var index = 0; index < 4; ++index)
                {
                    var num = GProcessMemoryManipulator.smethod_11(int_29_2, "rf6");
                    if (num != 0)
                    {
                        if (num != GProcessMemoryManipulator.smethod_11(factionGroupRow1 + 4, "rf7"))
                        {
                            int_29_2 += 4;
                        }
                        else
                        {
                            if (Debug)
                                GContext.Main.Log("Friendly at second friendly check");
                            return GReaction.Friendly;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if ((GProcessMemoryManipulator.smethod_11(factionGroupRow1 + 20, "rf8") &
                     GProcessMemoryManipulator.smethod_11(factionGroupRow2 + 12, "rf9")) > 0)
                {
                    if (Debug)
                        GContext.Main.Log("Friendly at third friendly check");
                    return GReaction.Friendly;
                }

                var int_29_3 = factionGroupRow1 + 40;
                for (var index = 0; index < 4; ++index)
                {
                    var num = GProcessMemoryManipulator.smethod_11(int_29_3, "rfa");
                    if (num != 0)
                    {
                        if (num == GProcessMemoryManipulator.smethod_11(factionGroupRow2 + 4, "rfb"))
                        {
                            if (Debug)
                                GContext.Main.Log("Friendly at fourth friendly check");
                            return GReaction.Friendly;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (Debug)
                    GContext.Main.Log("Neutral at final catch-all");
                return GReaction.Neutral;
            }

            if (Debug)
                GContext.Main.Log("GetReaction failed, missing a faction row!  Mine = 0x" +
                                  factionGroupRow1.ToString("x8") + ", other = 0x" + factionGroupRow2.ToString("x8"));
            return GReaction.Unknown;
        }

        public GBuff[] GetBuffSnapshot(bool BypassTimer)
        {
            lock (this)
            {
                if (Environment.TickCount - _lastBuffCheck < 50 && !BypassTimer)
                    return _lastBuffs;
                LoadBuffList();
                return _lastBuffs;
            }
        }

        public GBuff[] GetBuffSnapshot()
        {
            return GetBuffSnapshot(false);
        }

        public bool HasBuff(int SpellID)
        {
            foreach (var gbuff in GetBuffSnapshot())
                if (gbuff.SpellID == SpellID)
                    return true;
            return false;
        }

        private int[] GetWellKnownBuff(string WKBuffName)
        {
            if (WKBuffs.ContainsKey(WKBuffName))
                return WKBuffs[WKBuffName];
            var strArray = GClass18.gclass18_0.method_3("Buff_" + WKBuffName).Split(' ');
            var wellKnownBuff = new int[strArray.Length];
            for (var index = 0; index < strArray.Length; ++index)
                wellKnownBuff[index] = int.Parse(strArray[index], NumberStyles.HexNumber);
            WKBuffs.Add(WKBuffName, wellKnownBuff);
            return wellKnownBuff;
        }

        public bool HasWellKnownBuff(string WKBuffName)
        {
            var wellKnownBuff = GetWellKnownBuff(WKBuffName);
            var buffSnapshot = GetBuffSnapshot();
            if (wellKnownBuff == null)
                return false;
            foreach (var gbuff in buffSnapshot)
            foreach (var num in wellKnownBuff)
                if (gbuff.SpellID == num)
                    return true;
            return false;
        }

        public bool HasBuff(int[] AnySpellID)
        {
            foreach (var gbuff in GetBuffSnapshot())
            foreach (var num in AnySpellID)
                if (gbuff.SpellID == num)
                    return true;
            return false;
        }

        public void SetBuffsDirty()
        {
            lock (this)
            {
                _lastBuffCheck = Environment.TickCount - 5000;
            }
        }

        protected void LoadBuffList()
        {
            var gbuffList = new List<GBuff>();
            var num1 = 24;
            var num2 = 8;
            var num3 = 14;
            var num4 = 12;
            var num5 = 128;
            var num6 = GProcessMemoryManipulator.smethod_11(BaseAddress + GClass18.gclass18_0.method_4("NB_BaseCount"), "ubuffcount");
            var num7 = BaseAddress + GClass18.gclass18_0.method_4("NB_BaseList");
            var num8 = GProcessMemoryManipulator.smethod_11(BaseAddress + GClass18.gclass18_0.method_4("NB_ExtCount"), "extbuffcount");
            if (num8 > 0)
            {
                num6 = num8;
                num7 = GProcessMemoryManipulator.smethod_11(BaseAddress + GClass18.gclass18_0.method_4("NB_ExtListPtr"), "extbuffptr");
            }

            for (var index = 0; index < num6; ++index)
            {
                var num9 = num7 + index * num1;
                var SpellID = GProcessMemoryManipulator.smethod_11(num9 + num2, "buffsid");
                if (SpellID > 0)
                {
                    var IsHarmful = false;
                    int ChargesLeft = GProcessMemoryManipulator.smethod_15(num9 + num3, "buffchgs");
                    if ((GProcessMemoryManipulator.smethod_15(num9 + num4, "buffflgs") & num5) > 0)
                        IsHarmful = true;
                    if (SpellID != 0)
                        gbuffList.Add(new GBuff(SpellID, ChargesLeft, IsHarmful));
                }
            }

            _lastBuffs = gbuffList.ToArray();
            _lastBuffCheck = Environment.TickCount;
        }

        protected virtual void LoadBuffListOld()
        {
            var gbuffList = new List<GBuff>();
            var num1 = 16;
            var num2 = StorageAddress + StartupClass.gclass43_1.GetOffsetValue("UNIT_FIELD_AURA");
            if (IsPlayer)
                num1 = 40;
            for (var index = 0; index < 56; ++index)
            {
                var SpellID = GProcessMemoryManipulator.smethod_11(num2 + index * 4, "BuffSpell" + index);
                var IsHarmful = index >= num1 && index < num1 + 16;
                if (SpellID != 0)
                    gbuffList.Add(new GBuff(SpellID, 0, IsHarmful));
            }

            _lastBuffs = gbuffList.ToArray();
            _lastBuffCheck = Environment.TickCount;
        }
    }
}