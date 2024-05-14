// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GPlayerSelf
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections.Generic;

namespace Glider.Common.Objects
{
    public class GPlayerSelf : GPlayer
    {
        public const int GHOST_BUFF = 8326;
        public const int EQUIPPED_ITEM_SLOTS = 18;
        private const int FindHerbsSkillID = 2383;
        private const int FindMineralsSkillID = 2580;
        public static GPlayerSelf Me = null;

        private static readonly int[] SkinningIDs = new int[6]
        {
            10768,
            8617,
            8618,
            8613,
            32678,
            50305
        };

        protected int _ammoItemDefID;
        protected long[] _bags;
        protected int _coinage;
        protected int _comboPoints;
        protected long[] _equippedItems;
        protected int _experience;
        protected int _fightingID;
        protected int[] _knownSpells;
        protected int _nextLevelExperience;
        protected int _restedExperience;
        protected string _targetName;

        public GPlayerSelf(int BaseAddress, int FrameNumber)
            : base(BaseAddress, FrameNumber)
        {
            Me = this;
        }

        public long[] EquippedItems
        {
            get
            {
                Refresh();
                return _equippedItems;
            }
        }

        public bool IsTargetingEnemy
        {
            get
            {
                Refresh();
                return TargetGUID != 0L && TargetGUID != GUID && TargetGUID != PetGUID;
            }
        }

        public int Coinage
        {
            get
            {
                Refresh();
                return _coinage;
            }
        }

        public int ComboPoints
        {
            get
            {
                Refresh();
                return _comboPoints;
            }
        }

        public bool IsMeleeing
        {
            get
            {
                Refresh();
                return _fightingID != 0;
            }
        }

        public int Experience
        {
            get
            {
                Refresh();
                return _experience;
            }
        }

        public int AmmoItemDefID
        {
            get
            {
                Refresh();
                return _ammoItemDefID;
            }
        }

        public int NextLevelExperience
        {
            get
            {
                Refresh();
                return _nextLevelExperience;
            }
        }

        public int RestedExperience
        {
            get
            {
                Refresh();
                return _restedExperience;
            }
        }

        public int[] KnownSpells
        {
            get
            {
                Refresh();
                return _knownSpells;
            }
        }

        public bool HasAmmo
        {
            get
            {
                foreach (var gitem in GObjectList.GetItems())
                    if (AmmoItemDefID != 0 && gitem.ItemDefID == AmmoItemDefID)
                        return true;
                return false;
            }
        }

        public int AmmoCount
        {
            get
            {
                var ammoCount = 0;
                foreach (var gitem in GObjectList.GetItems())
                    if (gitem.ItemDefID == AmmoItemDefID)
                        ammoCount += gitem.StackSize;
                return ammoCount;
            }
        }

        public string AmmoName
        {
            get
            {
                var ammoName = "";
                foreach (var gitem in GObjectList.GetItems())
                    if (AmmoItemDefID != 0 && gitem.ItemDefID == AmmoItemDefID)
                        ammoName = gitem.Name;
                return ammoName;
            }
        }

        public string TargetName => _targetName != null ? _targetName : "(none)";

        public bool HasSkinning => GClass61.gclass61_0.method_5("ForceSkin") || HasSkill(SkinningIDs);

        public bool HasHerbalism => HasSkill(2383);

        public bool HasMining => HasSkill(2580);

        public bool IsStealth => HasWellKnownBuff("Stealth");

        public GLocation CombatStartLocation { get; private set; }

        public int Power2
        {
            get
            {
                switch (PlayerClass)
                {
                    case GPlayerClass.Warrior:
                        return Rage;
                    case GPlayerClass.Paladin:
                        return ManaPoints;
                    case GPlayerClass.Hunter:
                        return ManaPoints;
                    case GPlayerClass.Rogue:
                        return Energy;
                    case GPlayerClass.Priest:
                        return ManaPoints;
                    case GPlayerClass.Shaman:
                        return ManaPoints;
                    case GPlayerClass.Mage:
                        return ManaPoints;
                    case GPlayerClass.Warlock:
                        return ManaPoints;
                    case GPlayerClass.Druid:
                        return ManaPoints;
                    default:
                        return 0;
                }
            }
        }

        public int Power2Max
        {
            get
            {
                switch (PlayerClass)
                {
                    case GPlayerClass.Warrior:
                        return 100;
                    case GPlayerClass.Paladin:
                        return ManaMax;
                    case GPlayerClass.Hunter:
                        return ManaMax;
                    case GPlayerClass.Rogue:
                        return 100;
                    case GPlayerClass.Priest:
                        return ManaMax;
                    case GPlayerClass.Shaman:
                        return ManaMax;
                    case GPlayerClass.Mage:
                        return ManaMax;
                    case GPlayerClass.Warlock:
                        return ManaMax;
                    case GPlayerClass.Druid:
                        return ManaMax;
                    default:
                        return 1;
                }
            }
        }

        public bool isBeingTargeted => GObjectList.CheckForAttackers();

        public bool IsUnderAttack => GObjectList.GetNearestAttacker(0L) != null;

        public override bool IsDead => base.IsDead || HasWellKnownBuff("Ghost");

        public bool IsGhost => HasWellKnownBuff("Ghost");

        public override long TargetGUID => GProcessMemoryManipulator.smethod_12(GClass18.gclass18_0.method_4("TargetId"), "PSelfTarget");

        public long[] Bags => _bags;

        public long[] BagContents
        {
            get
            {
                var bagContents = new long[16];
                var num = _descriptor.method_1("PLAYER_FIELD_PACK_SLOT_1");
                for (var index = 0; index < 16; ++index)
                    bagContents[index] = GProcessMemoryManipulator.smethod_12(StorageAddress + num + index * 8, "pbagc");
                return bagContents;
            }
        }

        public int SlotCount => 16;

        public GLocation CorpseLocation => new GLocation(
            GProcessMemoryManipulator.smethod_13(GClass18.gclass18_0.method_4(nameof(CorpseLocation)) - 8, "CorpseX"),
            GProcessMemoryManipulator.smethod_13(GClass18.gclass18_0.method_4(nameof(CorpseLocation)) - 4, "CorpseY"),
            GProcessMemoryManipulator.smethod_13(GClass18.gclass18_0.method_4(nameof(CorpseLocation)), "CorpseZ"));

        protected override void LoadFields()
        {
            base.LoadFields();
            _comboPoints = (int)GProcessMemoryManipulator.smethod_23(GClass18.gclass18_0.method_4("ComboPointsAddr"), "ComboPoints");
            _fightingID = GetBaseInt("Combat");
            _experience = GetStorageInt("PLAYER_XP");
            _ammoItemDefID = GetStorageInt("PLAYER_AMMO_ID");
            _nextLevelExperience = GetStorageInt("PLAYER_NEXT_LEVEL_XP");
            _restedExperience = GetStorageInt("PLAYER_REST_STATE_EXPERIENCE");
            _coinage = GetStorageInt("PLAYER_FIELD_COINAGE");
            if (_knownSpells == null)
                LoadKnownSpells();
            SetupBags();
            if (_equippedItems == null)
                _equippedItems = new long[18];
            var int_29 = StorageAddress + _descriptor.method_1("PLAYER_FIELD_INV_SLOT_HEAD");
            var index = 0;
            while (index < 18)
            {
                _equippedItems[index] = GProcessMemoryManipulator.smethod_12(int_29, "eqir");
                ++index;
                int_29 += 8;
            }
        }

        public bool HasSkill(int SpellID)
        {
            foreach (var knownSpell in KnownSpells)
                if (knownSpell == SpellID)
                    return true;
            return false;
        }

        public bool HasSkill(int[] AnySpellID)
        {
            foreach (var knownSpell in KnownSpells)
            foreach (var num in AnySpellID)
                if (knownSpell == num)
                    return true;
            return false;
        }

        public void SetTargetName(string NewTargetName)
        {
            _targetName = NewTargetName;
        }

        public void LockCombatLocation()
        {
            CombatStartLocation = Location;
        }

        public int GetHeadingAddress()
        {
            return BaseAddress + GClass18.gclass18_0.method_4("Heading");
        }

        public int GetPitchAddress()
        {
            return BaseAddress + GClass18.gclass18_0.method_4("Pitch");
        }

        public void WaitForCombat()
        {
            var gspellTimer = new GSpellTimer(5000, false);
            while (!gspellTimer.IsReadySlow)
                if (!IsInCombat)
                    return;
            GContext.Main.Log("!! Futility waiting for combat flag to expire!");
        }

        private void SetupBags()
        {
            var longList = new List<long>();
            for (var index = 1; index < 5; ++index)
            {
                var num1 = StartupClass.gclass43_0.method_1("PLAYER_FIELD_INV_SLOT_HEAD") + 144 + index * 8;
                var num2 = GProcessMemoryManipulator.smethod_12(Me.StorageAddress + num1, "BagGuid1");
                longList.Add(num2);
            }

            _bags = longList.ToArray();
        }

        public GBagItem[] GetBagCollection(GItemBagAction BagAction)
        {
            var gbagItemList = new List<GBagItem>();
            var bagContents1 = BagContents;
            var bags = Bags;
            for (var Slot = 0; Slot < bagContents1.Length; ++Slot)
                if (bagContents1[Slot] != 0L)
                {
                    var gitem = (GItem)GObjectList.FindObject(bagContents1[Slot]);
                    GClass37.smethod_1("Backpack Item:" + gitem.Name);
                    if (BagAction == GItemBagAction.Mail && gitem.IsMailable)
                        gbagItemList.Add(new GBagItem(gitem, "ContainerFrame1", Slot, SlotCount));
                    if (BagAction == GItemBagAction.Sell && gitem.IsSellable)
                        gbagItemList.Add(new GBagItem(gitem, "ContainerFrame1", Slot, SlotCount));
                    if (BagAction == GItemBagAction.Unknown)
                        gbagItemList.Add(new GBagItem(gitem, "ContainerFrame1", Slot, SlotCount));
                }

            for (var index = 0; index < bags.Length; ++index)
                if (bags[index] != 0L)
                {
                    var gcontainer = (GContainer)GObjectList.FindObject(bags[index]);
                    var bagContents2 = gcontainer.BagContents;
                    for (var Slot = 0; Slot < bagContents2.Length; ++Slot)
                        if (bagContents2[Slot] != 0L)
                        {
                            var gitem = (GItem)GObjectList.FindObject(bagContents2[Slot]);
                            GClass37.smethod_1("Bag Item:" + gitem.Name);
                            if (BagAction == GItemBagAction.Mail && gitem.IsMailable)
                                gbagItemList.Add(new GBagItem(gitem, "ContainerFrame" + (index + 2), Slot,
                                    gcontainer.SlotCount));
                            if (BagAction == GItemBagAction.Sell && gitem.IsSellable)
                                gbagItemList.Add(new GBagItem(gitem, "ContainerFrame" + (index + 2), Slot,
                                    gcontainer.SlotCount));
                            if (BagAction == GItemBagAction.Unknown)
                                gbagItemList.Add(new GBagItem(gitem, "ContainerFrame" + (index + 2), Slot,
                                    gcontainer.SlotCount));
                        }
                }

            return gbagItemList.ToArray();
        }

        protected void LoadKnownSpells()
        {
            var intList = new List<int>();
            var num1 = GClass18.gclass18_0.method_4("MySpells");
            for (var index = 0; index < 1024; ++index)
            {
                var num2 = GProcessMemoryManipulator.smethod_11(num1 + index * 4, "SpellID");
                if (num2 != 0)
                    intList.Add(num2);
                else
                    break;
            }

            _knownSpells = intList.ToArray();
        }
    }
}