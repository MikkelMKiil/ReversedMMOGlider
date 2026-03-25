// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GMonster
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Threading;

namespace Glider.Common.Objects
{
    public class GMonster : GUnit
    {
        public static bool LogChecks;
        private new int _dflags;
        private new double _happiness;
        private int _harvestFlags;
        private string _skipReason;

        public GMonster(int BaseAddress, int FrameNumber)
            : base(BaseAddress, FrameNumber)
        {
            SetType(GObjectType.Monster);
            _skipReason = "(no skip reason defined)";
        }

        public new double Happiness
        {
            get
            {
                Refresh();
                return _happiness;
            }
        }

        public new bool IsLootable
        {
            get
            {
                Refresh();
                return (_dflags & 1) > 0;
            }
        }

        public bool IsMine
        {
            get
            {
                Refresh();
                return ConfigManager.gclass61_0.method_5("BypassTagCheck") ? IsTagged : (_dflags & 8) > 0;
            }
        }

        public bool IsElite
        {
            get
            {
                Refresh();
                return (_flags & 512) > 0;
            }
        }

        public new bool IsSkinnable
        {
            get
            {
                Refresh();
                return (_flags & 67108864) != 0;
            }
        }

        public bool IsValidProfileTarget
        {
            get
            {
                if (StartupClass.gprofile_0 != null)
                {
                    if (CreatureType == GCreatureType.Critter)
                    {
                        SkipReason = "it's a critter";
                        return false;
                    }

                    if (CreatureType == GCreatureType.NonCombatPet)
                    {
                        SkipReason = "it's a non-combat pet";
                        return false;
                    }

                    if (CreatureType == GCreatureType.Totem)
                    {
                        SkipReason = "it's a totem";
                        return false;
                    }

                    if (GUID == GPlayerSelf.Me.PetGUID)
                    {
                        SkipReason = "it's my pet";
                        return false;
                    }

                    if (Level < StartupClass.gprofile_0.MinLevel && StartupClass.gprofile_0.MinLevel != 0)
                    {
                        SkipReason = "level below min";
                        return false;
                    }

                    if (Level > StartupClass.gprofile_0.MaxLevel && StartupClass.gprofile_0.MaxLevel != 0)
                    {
                        SkipReason = "level above max";
                        return false;
                    }

                    if (!StartupClass.gprofile_0.CheckFaction(FactionID))
                    {
                        SkipReason = "CheckFaction does not like (" + FactionID + ")";
                        return false;
                    }

                    if (StartupClass.gprofile_0.Beach && Math.Abs(GContext.Main.Me.Location.Z - Location.Z) > 5.0)
                    {
                        SkipReason = "altitude too low for beach profile";
                        return false;
                    }

                    if (StartupClass.gprofile_0.AvoidList != null && IsInList(StartupClass.gprofile_0.AvoidList))
                    {
                        SkipReason = "on avoid list for profile";
                        return false;
                    }

                    if (StartupClass.gprofile_0.IsBlacklisted(GUID))
                    {
                        SkipReason = "blacklisted";
                        return false;
                    }
                }

                if (Math.Abs(GContext.Main.Me.Location.Z - Location.Z) > 15.0)
                {
                    SkipReason = "Location.Z is too far from mine";
                    return false;
                }

                if (Health == 0.0)
                {
                    SkipReason = "dead";
                    return false;
                }

                if (Health < 0.99)
                {
                    SkipReason = "health below pull threshold, probably tagged or already damaged";
                    return false;
                }

                var myGuid = GPlayerSelf.Me != null ? GPlayerSelf.Me.GUID : 0UL;
                if (TargetGUID != 0UL && TargetGUID != GUID && TargetGUID != myGuid)
                {
                    SkipReason = "monster already has a target";
                    return false;
                }

                if (IsTagged && !IsMine && !ConfigManager.gclass61_0.method_5("IgnoreTags"))
                {
                    SkipReason = "tagged and not mine";
                    return false;
                }

                if (LogChecks)
                    Logger.smethod_1("Valid: " + Name + " (" + GUID.ToString("x") + "), range to self = " +
                                       DistanceToSelf);
                return true;
            }
        }

        public string SkipReason
        {
            get => _skipReason;
            set
            {
                _skipReason = value;
                if (!LogChecks)
                    return;
                Logger.smethod_1("Invalid: " + Name + " (" + GUID.ToString("x") + "), range to self = " +
                                   DistanceToSelf + " --> " + _skipReason);
            }
        }

        public bool IsTagged
        {
            get
            {
                Refresh();
                return (_dflags & 4) > 0;
            }
        }

        public bool IsTrivial => CreatureType == GCreatureType.NonCombatPet || CreatureType == GCreatureType.Totem ||
                                 CreatureType == GCreatureType.Critter;

        public bool IsMinable
        {
            get
            {
                Refresh();
                return (_harvestFlags & 512) > 0;
            }
        }

        public bool IsHarvestable
        {
            get
            {
                Refresh();
                return (_harvestFlags & 256) > 0;
            }
        }

        protected override void LoadFields()
        {
            base.LoadFields();
            var storageFloat1 = GetStorageFloat("UNIT_FIELD_POWER5");
            var storageFloat2 = GetStorageFloat("UNIT_FIELD_MAXPOWER5");
            _happiness = storageFloat2 == 0.0 ? 0.0 : storageFloat1 / (double)storageFloat2;
            _dflags = GetStorageInt("UNIT_DYNAMIC_FLAGS");
            _harvestFlags = 0;
            var baseInt = GetBaseInt("MonsterDefinition");
            if (baseInt == 0 || !MemoryOffsetTable.Instance.HasOffset("HarvestType"))
                return;
            _harvestFlags = GameMemoryAccess.ReadInt32(baseInt + MemoryOffsetTable.Instance.GetIntOffset("HarvestType"), "harvesttype");
        }

        protected override void SetName()
        {
            if (_name != null && !_name.StartsWith("("))
                return;
            _name = "(unknown)";
            var baseInt = GetBaseInt("MonsterDefinition");
            if (baseInt == 0)
                return;
            var num = 0;
            if (MemoryOffsetTable.Instance.HasOffset("UnitNameSecond"))
                num += MemoryOffsetTable.Instance.GetIntOffset("UnitNameSecond");
            var int_29 = GameMemoryAccess.ReadInt32(baseInt + num, "UnitNamePtr");
            if (int_29 == 0)
                return;
            _name = GameMemoryAccess.ReadStringInternal(int_29, 64, "unitname2");
        }

        protected override void SetTitle()
        {
            if (_title != null && !_title.StartsWith("("))
                return;
            _title = "(unknown)";
            var baseInt = GetBaseInt("MonsterDefinition");
            if (baseInt == 0)
                return;
            var num = 0;
            if (MemoryOffsetTable.Instance.HasOffset("UnitTitle"))
                num += MemoryOffsetTable.Instance.GetIntOffset("UnitTitle");
            var int_29 = GameMemoryAccess.ReadInt32(baseInt + num, "UnitTitlePtr");
            if (int_29 == 0)
                return;
            _title = GameMemoryAccess.ReadStringInternal(int_29, 64, "unittitle2");
        }

        public bool IsInList(string[] Names)
        {
            var lower = Name.ToLower();
            foreach (var name in Names)
                if (lower.IndexOf(name.ToLower()) > -1)
                    return true;
            return false;
        }

        public bool WaitForEngage()
        {
            var gspellTimer = new GSpellTimer(4000, false);
            while (!gspellTimer.IsReady)
            {
                if (IsMine || IsTargetingMe)
                    return true;
                Thread.Sleep(101);
            }

            GContext.Main.Log("Never engaged: " + ToString());
            return false;
        }
    }
}
