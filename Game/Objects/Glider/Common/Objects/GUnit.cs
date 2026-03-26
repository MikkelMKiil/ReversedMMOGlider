#nullable disable

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Glider.Common.Objects
{
    /// <summary>
    /// Represents a generic unit in the game world (e.g., a player, monster, or NPC).
    /// This class provides access to unit-specific information like health, mana, level, and state.
    /// This file has been adapted for compatibility with World of Warcraft 3.3.5a (build 12340).
    /// GUIDs are represented as 'ulong' (64-bit unsigned) to be compatible with the 32-bit client.
    /// </summary>
    public class GUnit : GObject
    {
        /// <summary>
        /// Tolerance for heading comparisons, in radians.
        /// </summary>
        public const double HEADING_TOLERANCE = 0.34;

        /// <summary>
        /// The maximum number of auras (buffs/debuffs) a unit can have in WotLK 3.3.5a.
        /// </summary>
        protected const int MAX_AURAS = 56;

        protected int _castingID;
        protected ulong _channelingID;
        protected ulong _createdBy;
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
        protected int _movementFlags;
        protected int _movementFlags2;
        protected double _pitch;
        protected GReaction _reaction;
        protected ulong _target;
        protected int _channelingSpellID;
        protected bool _wasLootable;

        /// <summary>
        /// A time window in milliseconds to prevent wrongly assuming a unit is stuck/evading when it's just dying slowly.
        /// </summary>
        private readonly double DYING_SANITY_TIME = 30000.0;

        /// <summary>
        /// A cache for "Well Known" buff IDs to avoid repeated lookups from memory offsets.
        /// </summary>
        private readonly SortedList<string, int[]> WKBuffs;

        public GUnit(uint BaseAddress, int FrameNumber)
            : base(BaseAddress, FrameNumber)
        {
            _lastHealthPoints = 0;
            _lastHealthDrop = 0;
            _wasLootable = false;
            WKBuffs = new SortedList<string, int[]>();
        }

        /// <summary>
        /// Gets a value indicating whether this unit is a player.
        /// </summary>
        public bool IsPlayer => Type == GObjectType.Player;

        /// <summary>
        /// Gets the happiness level of the unit (typically for pets). Represented as a value from 0.0 to 1.0.
        /// </summary>
        public double Happiness
        {
            get
            {
                Refresh();
                return _happiness;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this unit is a monster/NPC.
        /// </summary>
        public bool IsMonster => Type == GObjectType.Monster;

        /// <summary>
        /// Gets the GUID of the object that created this unit (e.g., the owner of a pet or totem).
        /// </summary>
        public ulong CreatedBy
        {
            get
            {
                Refresh();
                return _createdBy;
            }
        }

        /// <summary>
        /// Gets the reaction of this unit towards the player (e.g., Hostile, Friendly, Neutral).
        /// </summary>
        public GReaction Reaction
        {
            get
            {
                if (_reaction == GReaction.Unknown)
                    _reaction = GetReaction(false);
                return _reaction;
            }
        }

        /// <summary>
        /// Gets the health of the unit as a percentage (0.0 to 1.0).
        /// </summary>
        public double Health
        {
            get
            {
                Refresh();
                return _health;
            }
        }

        /// <summary>
        /// Gets the current health points of the unit.
        /// </summary>
        public int HealthPoints
        {
            get
            {
                Refresh();
                return _healthPoints;
            }
        }

        /// <summary>
        /// Gets the maximum health points of the unit.
        /// </summary>
        public int HealthMax
        {
            get
            {
                Refresh();
                return _healthMax;
            }
        }

        /// <summary>
        // Gets the primary power(Mana, Rage, Energy, Runic Power) of the unit as a percentage(0.0 to 1.0).
        /// </summary>
        public double Mana
        {
            get
            {
                Refresh();
                return _mana;
            }
        }

        /// <summary>
        /// Gets the current points of the primary power resource.
        /// </summary>
        public int ManaPoints
        {
            get
            {
                Refresh();
                return _manaPoints;
            }
        }

        /// <summary>
        /// Gets the maximum points of the primary power resource.
        /// </summary>
        public int ManaMax
        {
            get
            {
                Refresh();
                return _manaMax;
            }
        }

        /// <summary>
        /// Gets the level of the unit.
        /// </summary>
        public int Level
        {
            get
            {
                Refresh();
                return _level;
            }
        }

        /// <summary>
        /// Gets the faction template ID of the unit.
        /// </summary>
        public int FactionID
        {
            get
            {
                Refresh();
                return _factionID;
            }
        }

        /// <summary>
        /// Gets the direction the unit is facing, in radians.
        /// </summary>
        public double Heading
        {
            get
            {
                Refresh();
                return _heading;
            }
        }

        /// <summary>
        /// Gets the pitch of the unit's view, in radians.
        /// </summary>
        public double Pitch
        {
            get
            {
                Refresh();
                return _pitch;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this unit is targeting the player.
        /// </summary>
        public bool IsTargetingMe => TargetGUID == GPlayerSelf.Me.GUID;

        /// <summary>
        /// Gets a value indicating whether this unit is targeting the player's pet.
        /// </summary>
        public bool IsTargetingMyPet => GPlayerSelf.Me.PetGUID != 0 && TargetGUID == GPlayerSelf.Me.PetGUID;

        /// <summary>
        /// Gets the GUID of this unit's current target.
        /// </summary>
        public virtual ulong TargetGUID
        {
            get
            {
                Refresh();
                return _target;
            }
        }

        /// <summary>
        /// Gets the GUnit object for this unit's target. Returns null if there is no target.
        /// </summary>
        public GUnit Target => GObjectList.ResolveUnitByGuid(TargetGUID);

        /// <summary>
        /// Gets a value indicating whether the unit is dead.
        /// </summary>
        public virtual bool IsDead => HealthPoints <= 1;

        /// <summary>
        /// Gets a value indicating whether the unit is in combat.
        /// </summary>
        public bool IsInCombat
        {
            get
            {
                Refresh();
                return (_flags & 0x80000) != 0; // UNIT_FLAG_IN_COMBAT
            }
        }

        /// <summary>
        /// Gets the ID of the spell the unit is currently casting.
        /// </summary>
        public int CastingID
        {
            get
            {
                Refresh();
                return _castingID;
            }
        }

        /// <summary>
        /// Gets the GUID of the object being channeled on.
        /// </summary>
        public ulong ChannelingObjectID
        {
            get
            {
                Refresh();
                return _channelingID;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the unit is actively casting or channeling a spell.
        /// </summary>
        public bool IsCasting => CastingID != 0 || ChannelingObjectID != 0;

        public int ChannelingSpellID
        {
            get
            {
                Refresh();
                return _channelingSpellID;
            }
        }

        /// <summary>
        /// Gets the creature type of the unit (e.g., Beast, Humanoid, Undead).
        /// </summary>
        public virtual GCreatureType CreatureType
        {
            get
            {
                Refresh();
                return _creatureType;
            }
        }

        /// <summary>
        /// Gets the bearing (relative heading) from the player to this unit.
        /// </summary>
        public double Bearing => GContext.Main.Me.GetHeadingDelta(Location);

        /// <summary>
        /// Gets a value indicating whether this unit is within melee range of the player.
        /// </summary>
        public bool IsInMeleeRange => DistanceToSelf <= GContext.Main.MeleeDistance;

        /// <summary>
        /// Gets a value indicating whether the mouse cursor is currently over this unit.
        /// </summary>
        public bool IsCursorOnUnit => GameMemoryAccess.ReadUnderCursorGuid() == GUID;

        /// <summary>
        /// Gets a value indicating whether the unit is facing away from the player.
        /// </summary>
        public bool IsFacingAway => Math.Abs(GContext.Main.Movement.CompareHeadings(GContext.Main.Me.Heading, Heading)) < Math.PI / 2.0;

        /// <summary>
        /// Gets the number of milliseconds that have passed since the unit's health last dropped.
        /// </summary>
        public int TicksSinceHealthDrop => Environment.TickCount - _lastHealthDrop;

        /// <summary>
        /// A sanity check to see if the unit is stuck in a dying state (e.g., evading).
        /// </summary>
        public bool IsNotDying
        {
            get
            {
                if (Health < 1.0 && TicksSinceHealthDrop > DYING_SANITY_TIME)
                {
                    Logger.smethod_1("Unit is damaged but health has not changed for a while, assuming evaded.");
                    return true;
                }

                if (Health > 0.99 && StartupClass.CurrentGameClass.TicksSinceCombatStart > DYING_SANITY_TIME)
                {
                    Logger.smethod_1("Unit is undamaged and combat is taking too long, assuming evaded.");
                    return true;
                }

                // This check is likely specific to the Glider bot's internal logic.
                if (StartupClass.GameClass69Instance.method_10() >= 4)
                    return false;

                Logger.smethod_1("Recent evade entry in combat log indicates bugged state.");
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this unit is moving towards the player.
        /// </summary>
        public bool IsApproaching => _lastLocation != null && Location.GetDistanceTo(GContext.Main.Me.Location) < _lastLocation.GetDistanceTo(GContext.Main.Me.Location);

        /// <summary>
        /// Gets a value indicating whether the unit's corpse is lootable.
        /// </summary>
        public bool IsLootable
        {
            get
            {
                Refresh();
                return (_dflags & 1) > 0; // UNIT_DYNAMIC_FLAG_LOOTABLE
            }
        }

        /// <summary>
        /// Gets a value indicating whether the unit was ever lootable during its existence.
        /// </summary>
        public bool WasLootable
        {
            get
            {
                Refresh();
                return _wasLootable;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the unit can be skinned.
        /// </summary>
        public bool IsSkinnable
        {
            get
            {
                Refresh();
                return (_flags & 0x4000000) != 0; // UNIT_FLAG_SKINNABLE
            }
        }

        /// <summary>
        /// Gets the raid target icon (star, circle, etc.) assigned to this unit.
        /// </summary>
        public GRaidTargetIcon RaidTargetIcon
        {
            get
            {
                var num = MemoryOffsetTable.Instance.GetIntOffset(nameof(RaidTargetIcon));
                for (var index = 0; index < 8; ++index)
                    if (GameMemoryAccess.ReadRaidTargetGuid(num, index) == GUID)
                        return (GRaidTargetIcon)(index + 1);
                return GRaidTargetIcon.NotSpecified;
            }
        }

        /// <summary>
        /// Gets the movement flags for this unit.
        /// </summary>
        public int MovementFlags
        {
            get
            {
                Refresh();
                return _movementFlags;
            }
        }

        public int MovementFlags1 => MovementFlags;

        public int MovementFlags2
        {
            get
            {
                Refresh();
                return _movementFlags2;
            }
        }

        /// <summary>
        /// Reloads all fields for this object from game memory.
        /// </summary>
        protected override void LoadFields()
        {
            base.LoadFields();

            // Read descriptor fields, which are version-specific. These are for 3.3.5a.
            _flags = GetStorageInt("UNIT_FIELD_FLAGS");
            _dflags = GetStorageInt("UNIT_DYNAMIC_FLAGS");
            _healthPoints = GetStorageInt("UNIT_FIELD_HEALTH");
            _healthMax = GetStorageInt("UNIT_FIELD_MAXHEALTH");
            _manaPoints = GetStorageInt("UNIT_FIELD_POWER1");
            _manaMax = GetStorageInt("UNIT_FIELD_MAXPOWER1");
            _level = GetStorageInt("UNIT_FIELD_LEVEL");
            _factionID = GetStorageInt("UNIT_FIELD_FACTIONTEMPLATE");
            _castingID = GetStorageInt("UNIT_FIELD_CASTING");
            _channelingSpellID = GetStorageInt("UNIT_CHANNEL_SPELL");

            // Calculate percentages
            _health = _healthMax <= 0 ? 0.0 : _healthPoints / (double)_healthMax;
            _mana = _manaMax <= 0 ? 0.0 : _manaPoints / (double)_manaMax;

            // Pet happiness is stored in the "FOCUS" power field for 3.3.5a
            var currentHappiness = GetStorageFloat("UNIT_FIELD_POWER4");
            var maxHappiness = GetStorageFloat("UNIT_FIELD_MAXPOWER4");
            _happiness = maxHappiness != 0.0 ? currentHappiness / maxHappiness : 0.0;

            // Read GUID fields
            _createdBy = GetStorageULong("UNIT_FIELD_CREATEDBY");
            _target = GetStorageULong("UNIT_FIELD_TARGET");
            _channelingID = GetStorageULong("UNIT_FIELD_CHANNEL_OBJECT");

            // Read base address offsets
            _heading = GetBaseFloat("Heading");
            _pitch = GetBaseFloat("Pitch");
            var x = GetBaseFloat("X");
            var y = GetBaseFloat("Y");
            var z = GetBaseFloat("Z");

            _lastLocation = _location;
            _location = new GLocation(x, y, z);

            _monsterDefinition = GetBaseInt("MonsterDefinition");
            _creatureType = _monsterDefinition == 0 ? GCreatureType.NoDefinition : (GCreatureType)GameMemoryAccess.ReadCreatureType(_monsterDefinition);
            _movementFlags = GameMemoryAccess.ReadMovementFlags1(BaseAddress);
            var moveStruct2Address = GameMemoryAccess.ReadMoveStruct2(BaseAddress);
            _movementFlags2 = moveStruct2Address != 0 ? GameMemoryAccess.ReadMovementFlags2(moveStruct2Address) : 0;

            // Health drop tracking
            if (_healthPoints < _lastHealthPoints)
            {
                _lastHealthPoints = _healthPoints;
                _lastHealthDrop = Environment.TickCount;
            }
            _lastHealthPoints = _healthPoints;

            // Lootable flag tracking
            if ((_dflags & 1) > 0)
                _wasLootable = true;
        }

        /// <summary>
        /// Sets this unit as the player's current target.
        /// </summary>
        /// <param name="WasLastHostile">If true, attempts to use "Target Last Hostile" first.</param>
        /// <returns>True if the unit was successfully targeted.</returns>
        public bool SetAsTarget(bool WasLastHostile)
        {
            if (DistanceToSelf > 10.0)
                Face();

            if (GUID == GPlayerSelf.Me.TargetGUID)
                return true;

            // Attempt mouse-based targeting if configured
            if (ConfigManager.gclass61_0.method_5("TargetWithMouse"))
            {
                Select();
                if (GUID == GPlayerSelf.Me.TargetGUID)
                    return true;
            }

            // Attempt key-based targeting multiple times
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

        /// <summary>
        /// Sends a keybind to target a unit and waits briefly for the target to change.
        /// </summary>
        private ulong TargetSomething(string KeyName)
        {
            var initialTargetGuid = GContext.Main.Me.TargetGUID;
            var timer = new GSpellTimer(1000, false);
            GContext.Main.SendKey(KeyName);

            // Wait until the timer is up or the target has changed
            do
            {
                Thread.Sleep(15);
            } while (!timer.IsReadySlow && GContext.Main.Me.TargetGUID == initialTargetGuid);

            return GContext.Main.Me.TargetGUID;
        }

        /// <summary>
        /// Calculates the difference in heading between this unit's orientation and the direction to a target location.
        /// </summary>
        public double GetHeadingDelta(GLocation Target)
        {
            var headingTo = GContext.Main.Movement.GetHeadingTo(Location, Target);
            return GContext.Main.Movement.CompareHeadings(Heading, headingTo);
        }

        /// <summary>
        /// Gets the heading from this unit to a target unit.
        /// </summary>
        public double GetHeadingTo(GUnit Target)
        {
            return GetHeadingTo(Target.Location);
        }

        /// <summary>
        /// Gets the heading from this unit to a target location.
        /// </summary>
        public double GetHeadingTo(GLocation TargetLocation)
        {
            var headingTo = GContext.Main.Movement.GetHeadingTo(Location, TargetLocation);
            return headingTo == -1.0 ? Heading : headingTo;
        }

        /// <summary>
        /// Initiates a smooth turn towards this unit.
        /// </summary>
        public void StartSpinTowards()
        {
            GContext.Main.StartSpinTowards(Bearing);
        }

        /// <summary>
        /// Instantly faces this unit.
        /// </summary>
        public void Face()
        {
            if (!IsValid) return;
            GContext.Main.Movement.SetHeading(GContext.Main.Me.GetHeadingTo(this), HEADING_TOLERANCE);
        }

        /// <summary>
        /// Instantly faces this unit within a given tolerance.
        /// </summary>
        /// <param name="Tolerance">The allowed deviation in radians.</param>
        public void Face(double Tolerance)
        {
            if (!IsValid) return;
            GContext.Main.Movement.SetHeading(GContext.Main.Me.GetHeadingTo(this), Tolerance);
        }

        /// <summary>
        /// Waits for the player to get within a certain distance of this unit.
        /// </summary>
        public void WaitForApproach(double Distance, int Milliseconds)
        {
            if (!IsValid) return;
            var timer = new GSpellTimer(Milliseconds, false);
            do
            {
                Thread.Sleep(25);
            } while (!timer.IsReadySlow && DistanceToSelf > Distance);
        }

        /// <summary>
        /// Moves the player towards this unit until within melee range.
        /// </summary>
        public bool Approach()
        {
            return Approach(ConfigManager.gclass61_0.method_4("MeleeDistance"));
        }

        /// <summary>
        /// Moves the player towards this unit until within a specified distance.
        /// </summary>
        public bool Approach(double Distance)
        {
            return Approach(Distance, false);
        }

        public bool Approach(bool LeaveRunning)
        {
            return Approach(ConfigManager.gclass61_0.method_4("MeleeDistance"), LeaveRunning);
        }

        /// <summary>
        /// Moves the player towards this unit.
        /// </summary>
        /// <param name="Distance">The desired distance to stop at.</param>
        /// <param name="LeaveRunning">If true, does not stop autorun upon arrival.</param>
        /// <returns>True on success.</returns>
        public bool Approach(double Distance, bool LeaveRunning)
        {
            Logger.smethod_1("Approaching \"" + Name + "\" to distance of " + Distance);
            return GContext.Main.Movement.MoveToUnit(this, Distance, LeaveRunning);
        }

        /// <summary>
        /// Moves behind the unit, useful for backstabbing classes.
        /// </summary>
        /// <param name="Sneaking">Whether to use a slower, more precise method for sneaking.</param>
        /// <returns>True if successfully positioned behind the target.</returns>
        public bool GetBehind(bool Sneaking)
        {
            Logger.smethod_1("GetBehind: \"" + Name + "\", sneaking = " + Sneaking);

            // We are already behind the target and in melee range.
            if (IsFacingAway && DistanceToSelf < ConfigManager.gclass61_0.method_4("MeleeDistance"))
                return true;

            // Target is too far away to reasonably get behind.
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
                // Sneaking logic: strafe carefully around the target while facing it.
                var strafeKey = GContext.Main.Movement.CompareHeadings(GContext.Main.Me.Heading, Heading) > 0.0
                    ? "Common.StrafeLeft"
                    : "Common.StrafeRight";
                GContext.Main.PressKey(strafeKey);
                var timer = new GSpellTimer(8000);
                while (!timer.IsReady)
                {
                    GContext.Main.Me.Refresh(true);
                    Refresh(true);
                    Face(); // Continuously face the target while strafing.
                    if (IsFacingAway) break; // We are behind, exit loop.
                    Thread.Sleep(50);
                }
                GContext.Main.ReleaseKey(strafeKey);

                if (timer.IsReady) // Timed out
                {
                    GContext.Main.ReleaseAllKeys();
                    return false;
                }

                Approach(); // Move into melee range now that we are behind.
                GContext.Main.ReleaseAllKeys();
                return IsFacingAway && DistanceToSelf < GContext.Main.MeleeDistance;
            }
            else
            {
                // Non-sneaking logic: quickly run/strafe around the target.
                bool goRight = StartupClass.random_0.Next() % 2 == 0;
                var strafeKey = goRight ? "Common.StrafeRight" : "Common.StrafeLeft";

                GContext.Main.StartRun();
                GContext.Main.PressKey(strafeKey);
                var timer = new GSpellTimer(4000);
                while (!timer.IsReady)
                {
                    GContext.Main.Me.Refresh(true);
                    Refresh(true);
                    if (IsFacingAway) break; // We are behind, exit loop.
                    Thread.Sleep(25);
                }

                GContext.Main.ReleaseAllKeys();

                if (timer.IsReady) return false; // Timed out

                Face();
                GetToMeleeDistance();
                return IsFacingAway && DistanceToSelf < GContext.Main.MeleeDistance;
            }
        }

        /// <summary>
        /// Adjusts player position to be within melee range but not too close.
        /// </summary>
        private void GetToMeleeDistance()
        {
            if (DistanceToSelf < GContext.Main.MeleeDistance - 2.5)
                BackAwayFrom(GContext.Main.MeleeDistance - 2.5);
            else
                Approach();
        }

        /// <summary>
        /// Backs away from the unit until a specific distance is reached.
        /// </summary>
        private void BackAwayFrom(double RequestedDistance)
        {
            if (DistanceToSelf >= RequestedDistance) return;
            var timer = new GSpellTimer(3000);
            GContext.Main.PressKey("Common.Back");
            do
            {
                Thread.Sleep(25);
            } while (!timer.IsReadySlow && DistanceToSelf < RequestedDistance);
            GContext.Main.ReleaseKey("Common.Back");
        }

        /// <summary>
        /// Manually updates the last health drop timestamp to the current time.
        /// </summary>
        public void TouchHealthDrop()
        {
            _lastHealthDrop = Environment.TickCount;
        }

        /// <summary>
        /// Waits for a corpse to become lootable.
        /// </summary>
        public void WaitForLootable()
        {
            var timer = new GSpellTimer(3000, false);
            do
            {
                Thread.Sleep(50);
            } while (!timer.IsReadySlow && !IsLootable);
        }

        /// <summary>
        /// Reads faction data from memory to determine the reaction between this unit and the player.
        /// This is a complex and client-version-specific operation.
        /// </summary>
        protected GReaction GetReaction(bool Debug)
        {
            if (GContext.Main.Me == null)
            {
                if (Debug) GContext.Main.Log("GetReaction failed, no GContext.Main.Me!");
                return GReaction.Unknown;
            }

            var myFactionData = GetFactionGroupRow(GContext.Main.Me.BaseAddress);
            var otherFactionData = GetFactionGroupRow(BaseAddress);

            if (Debug) GContext.Main.Log("Faction rows: Mine = 0x" + myFactionData.ToString("x8") + ", other = 0x" + otherFactionData.ToString("x8"));

            if (myFactionData != 0 && otherFactionData != 0)
            {
                // This logic compares faction flags to determine relationships (hostile, friendly, neutral).
                if ((GameMemoryAccess.ReadReactionValue(myFactionData + 12, "rf0") & GameMemoryAccess.ReadReactionValue(otherFactionData + 20, "rf1")) > 0)
                    return GReaction.Hostile;

                for (int i = 0, ptr = otherFactionData + 24; i < 4; ++i, ptr += 4)
                {
                    var val = GameMemoryAccess.ReadReactionValue(ptr, "rf2");
                    if (val == 0) break;
                    if (val == GameMemoryAccess.ReadReactionValue(myFactionData + 4, "rf3"))
                        return GReaction.Hostile;
                }

                if ((GameMemoryAccess.ReadReactionValue(myFactionData + 12, "rf4") & GameMemoryAccess.ReadReactionValue(otherFactionData + 16, "rf5")) > 0)
                    return GReaction.Friendly;

                for (int i = 0, ptr = otherFactionData + 40; i < 4; ++i, ptr += 4)
                {
                    var val = GameMemoryAccess.ReadReactionValue(ptr, "rf6");
                    if (val == 0) break;
                    if (val == GameMemoryAccess.ReadReactionValue(myFactionData + 4, "rf7"))
                        return GReaction.Friendly;
                }

                if ((GameMemoryAccess.ReadReactionValue(myFactionData + 20, "rf8") & GameMemoryAccess.ReadReactionValue(otherFactionData + 12, "rf9")) > 0)
                    return GReaction.Friendly;

                for (int i = 0, ptr = myFactionData + 40; i < 4; ++i, ptr += 4)
                {
                    var val = GameMemoryAccess.ReadReactionValue(ptr, "rfa");
                    if (val == 0) break;
                    if (val == GameMemoryAccess.ReadReactionValue(otherFactionData + 4, "rfb"))
                        return GReaction.Friendly;
                }

                return GReaction.Neutral;
            }

            if (Debug) GContext.Main.Log("GetReaction failed, missing a faction row! Mine = 0x" + myFactionData.ToString("x8") + ", other = 0x" + otherFactionData.ToString("x8"));
            return GReaction.Unknown;
        }

        protected int GetFactionGroupRow(int CheckBaseAddress)
        {
            var factionSub = GameMemoryAccess.ReadFactionSub();
            var factionOff1 = GameMemoryAccess.ReadFactionOff1(CheckBaseAddress);
            if (factionSub == 0 || factionOff1 == 0)
                return 0;

            var factionOff2 = GameMemoryAccess.ReadFactionOff2(factionOff1);
            var factionBase = GameMemoryAccess.ReadFactionBase();
            if (factionOff2 == 0 || factionBase == 0)
                return 0;

            var sub = unchecked((uint)factionSub);
            var off2 = unchecked((uint)factionOff2);
            if (off2 < sub)
                return 0;

            var rowDelta = off2 - sub;
            if (rowDelta > 131072U)
                return 0;

            return GameMemoryAccess.ReadFactionLookup(factionBase, unchecked((int)rowDelta));
        }

        protected int GetFactionGroupRow(uint CheckBaseAddress)
        {
            return GetFactionGroupRow(unchecked((int)CheckBaseAddress));
        }

        /// <summary>
        /// Gets a snapshot of the unit's current auras (buffs and debuffs).
        /// </summary>
        /// <param name="BypassTimer">If true, forces a refresh from memory, ignoring the cache timer.</param>
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

        public GBuff[] GetBuffSnapshot() => GetBuffSnapshot(false);

        /// <summary>
        /// Checks if the unit has a specific buff/debuff.
        /// </summary>
        /// <param name="SpellID">The spell ID of the aura.</param>
        public bool HasBuff(int SpellID)
        {
            foreach (var gbuff in GetBuffSnapshot())
                if (gbuff.SpellID == SpellID)
                    return true;
            return false;
        }

        /// <summary>
        /// Checks if the unit has any of the specified buffs/debuffs.
        /// </summary>
        /// <param name="AnySpellID">An array of spell IDs to check for.</param>
        public bool HasBuff(int[] AnySpellID)
        {
            foreach (var gbuff in GetBuffSnapshot())
                foreach (var num in AnySpellID)
                    if (gbuff.SpellID == num)
                        return true;
            return false;
        }

        /// <summary>
        /// Checks if the unit has a buff from a pre-defined group (e.g., "Food", "Drink").
        /// </summary>
        public bool HasWellKnownBuff(string WKBuffName)
        {
            var wellKnownBuffIds = GetWellKnownBuff(WKBuffName);
            var currentBuffs = GetBuffSnapshot();

            if (wellKnownBuffIds == null) return false;

            foreach (var gbuff in currentBuffs)
                foreach (var id in wellKnownBuffIds)
                    if (gbuff.SpellID == id)
                        return true;
            return false;
        }

        /// <summary>
        /// Retrieves and caches a list of spell IDs for a "Well Known" buff category.
        /// </summary>
        private int[] GetWellKnownBuff(string WKBuffName)
        {
            if (WKBuffs.ContainsKey(WKBuffName))
                return WKBuffs[WKBuffName];

            var buffIdStrings = MemoryOffsetTable.Instance.GetStringOffset("Buff_" + WKBuffName)
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var wellKnownBuffList = new List<int>();
            for (var index = 0; index < buffIdStrings.Length; ++index)
            {
                if (!int.TryParse(buffIdStrings[index], NumberStyles.HexNumber, null, out var value))
                {
                    Logger.smethod_1($"Invalid hex value for buff '{WKBuffName}': '{buffIdStrings[index]}'");
                    continue;
                }
                wellKnownBuffList.Add(value);
            }

            var wellKnownBuff = wellKnownBuffList.ToArray();
            WKBuffs.Add(WKBuffName, wellKnownBuff);
            return wellKnownBuff;
        }

        /// <summary>
        /// Marks the buff list as outdated, forcing a refresh on the next query.
        /// </summary>
        public void SetBuffsDirty()
        {
            lock (this)
            {
                _lastBuffCheck = Environment.TickCount - 5000;
            }
        }

        /// <summary>
        /// Reads the aura list for the unit from memory. This is the correct method for 3.3.5a.
        /// </summary>
        protected virtual void LoadBuffList()
        {
            if (!MemoryOffsetTable.Instance.HasOffset("UNIT_FIELD_AURA"))
            {
                _lastBuffs = new GBuff[0];
                _lastBuffCheck = Environment.TickCount;
                return;
            }

            var gbuffList = new List<GBuff>();
            // Debuffs start at index 40 for players, 16 for NPCs.
            var debuffStartIndex = IsPlayer ? 40 : 16;
            var auraStructAddress = StorageAddress + StartupClass.gclass43_1.GetOffsetValue("UNIT_FIELD_AURA");

            for (var index = 0; index < MAX_AURAS; ++index)
            {
                var spellId = GameMemoryAccess.ReadOldBuffSpellId(auraStructAddress, index);
                if (spellId > 0 && spellId < 200000)
                {
                    // In 3.3.5a, harmful auras (debuffs) are located in a specific block of the aura array.
                    var isHarmful = index >= debuffStartIndex && index < debuffStartIndex + 16;
                    gbuffList.Add(new GBuff(spellId, 0, isHarmful));
                }
            }

            _lastBuffs = gbuffList.ToArray();
            _lastBuffCheck = Environment.TickCount;
        }
    }
}