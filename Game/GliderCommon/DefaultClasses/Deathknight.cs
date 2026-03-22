// Deathknight.cs - default Glider class file.
//
// May 14 2007 MMD - Created base class from thin air.
//
//////////////////////////////////////////////////////////////////////////
//
// IMPORTANT!  DO NOT MODIFY THIS FILE!  It is not read by Glider during
// usage.  To change the behavior of this class, copy this file into the
// "Classes" folder and give it a new name, then modify it.  And remove
// this warning, too.
//
// For more information on custom classes, please visit:
//
//          http://mmoglider.com/customclasses
//

using System;
using System.Threading;
using Glider.Common.Objects;

namespace Glider.Common.Objects
{
    public class Deathknight : GGameClass
    {
        #region DK properties/config
        static int[] HORN_OF_WINTER = new int[] { 57330, 57623 };
        bool UseGhoul;
        bool UseCorpseDust;
        bool AvoidAdds;
        int AvoidAddDistance;
        GSpellTimer GnawSpam = new GSpellTimer(700);   // Don't hit the key that fast.

        bool PickedUpAdd;
        bool CanHorn;                                  // Do we have Horn of Winter in the spellbook?

        const int BUFF_BONESHIELD = 0xc046;
        const int BUFF_DEATHTRANCE = 0xc522;
        const int DEBUFF_FROSTFEVER = 0xd737;
        const int DEBUFF_BLOODPLAGUE = 0xd726;
        const int ITEMID_CORPSEDUST = 0x9151;
        #endregion

        #region Death knight runes and helpers
        // Pointers to the little rune cooldowns under the character frame.
        GInterfaceObject RuneBlood1;
        GInterfaceObject RuneBlood2;
        GInterfaceObject RuneFrost1;
        GInterfaceObject RuneFrost2;
        GInterfaceObject RuneUnholy1;
        GInterfaceObject RuneUnholy2;
        GInterfaceObject PetButton1;

        // Combat code can use these counters after UpdateAvailableRunes to determine which spells are ready to fire.
        int BloodAvailable;
        int FrostAvailable;
        int UnholyAvailable;

        public override void OnAttach()
        {
            // Base class needs to do important stuff, do not disrupt!
            base.OnAttach();

            // Cache these pointers for later so we can rip through them quickly.

            RuneBlood1 = Interface.GetByName("RuneButtonIndividual1Cooldown");
            RuneBlood2 = Interface.GetByName("RuneButtonIndividual2Cooldown");
            RuneFrost1 = Interface.GetByName("RuneButtonIndividual3Cooldown");
            RuneFrost2 = Interface.GetByName("RuneButtonIndividual4Cooldown");
            RuneUnholy1 = Interface.GetByName("RuneButtonIndividual5Cooldown");
            RuneUnholy2 = Interface.GetByName("RuneButtonIndividual6Cooldown");
            PetButton1 = Interface.GetByName("PetActionButton1");

            CanHorn = Me.HasSkill(HORN_OF_WINTER);
            Context.Log("CanHorn: " + CanHorn);
        }

        void UpdateAvailableRunes()
        {
            // Assume all runes are up, then subtract any which are on cooldown:

            BloodAvailable = 2;

            if (RuneBlood1.IsVisible)
                BloodAvailable--;

            if (RuneBlood2.IsVisible)
                BloodAvailable--;

            FrostAvailable = 2;

            if (RuneFrost1.IsVisible)
                FrostAvailable--;

            if (RuneFrost2.IsVisible)
                FrostAvailable--;

            UnholyAvailable = 2;

            if (RuneUnholy1.IsVisible)
                UnholyAvailable--;

            if (RuneUnholy2.IsVisible)
                UnholyAvailable--;
        }

        #endregion

        #region GGameClass overrides
        public override string DisplayName
        {
            get { return Context.GetLocal("Common.ClassDeathknight"); }
        }

        public override GConfigResult ShowConfiguration()
        {
            return Context.ShowStockConfigDialog(GPlayerClass.Deathknight);
        }

        public override void CreateDefaultConfig()
        {
            Context.SetConfigValue("Deathknight.PullDistance", "20", false);
            Context.SetConfigValue("Deathknight.UseGhoul", "True", false);
            Context.SetConfigValue("Deathknight.UseCorpseDust", "False", false);
            Context.SetConfigValue("Deathknight.AvoidAdds", "True", false);
            Context.SetConfigValue("Deathknight.AvoidAddDistance", "30", false);
        }

        // We'll probably need these later.
        public override void LoadConfig() 
        {
            UseGhoul = Context.GetConfigBool("Deathknight.UseGhoul");
            UseCorpseDust = Context.GetConfigBool("Deathknight.UseCorpseDust");
            AvoidAdds = Context.GetConfigBool("Deathknight.AvoidAdds");
            AvoidAddDistance = Context.GetConfigInt("Deathknight.AvoidAddDistance");
        }

        public override void ResetBuffs() { }

        public override void RunningAction()
        {
            if (Interface.IsKeyPopulated("Deathknight.BoneShield") && !Me.HasBuff(BUFF_BONESHIELD))
            {
                UpdateAvailableRunes();

                if (UnholyAvailable > 0 && Interface.IsKeyReady("Deathknight.BoneShield"))
                    Context.CastSpell("Deathknight.BoneShield");
            }
        }

        // 12th step...
        public override bool CanDrink { get { return false; } }

        public override string PowerLabel
        {
            get
            {
                return "RunicP";
            }
        }

        public override string PowerValue
        {
            get
            {
                if (GPlayerSelf.Me != null)
                    return GPlayerSelf.Me.RunicPower.ToString();
                else
                    return "";
            }
        }

        public double GetClosestHumanoidCorpse()
        {
            GUnit[] nearby = GObjectList.GetUnits();
            double Closest = 99999.0;
            GUnit ClosestUnit = null;

            foreach (GUnit one in nearby)
                if (one.DistanceToSelf < Closest && one.IsDead && (one.IsPlayer || one.CreatureType == GCreatureType.Humanoid))
                {
                    Closest = one.DistanceToSelf;
                    ClosestUnit = one;
                }

            return Closest;
        }

        bool IsHornUp
        {
            get
            {
                return Me.HasBuff(HORN_OF_WINTER);
            }
        }

        bool IsCarryingItem(int ItemDefID)
        {
            GBagItem[] stuff = GPlayerSelf.Me.GetBagCollection(GItemBagAction.Unknown);

            foreach (GBagItem one in stuff)
                if (one.Item.ItemDefID == ItemDefID)
                    return true;

            return false;
        }

        public override bool Rest()
        {
            // Consider summoning a ghoul.
            if (!IsGhoulUp && UseGhoul && Interface.IsKeyReady("Deathknight.RaiseDead") && !Me.IsUnderAttack)
            {
                bool BringGhoul = false;

                // TODO: unit test this specially
                Context.Log("Thinking about ghoul, corpse dust present: " + IsCarryingItem(ITEMID_CORPSEDUST));

                // See if there's a corpse handy to use.  If not, screw it.
                if (UseCorpseDust && IsCarryingItem(ITEMID_CORPSEDUST))
                    BringGhoul = true;
                else
                    if (GetClosestHumanoidCorpse() < 20.0)
                        BringGhoul = true;

                if (BringGhoul)
                {
                    Context.ReleaseAllKeys();
                    Context.CastSpell("Deathknight.RaiseDead");
                    Thread.Sleep(4501);
                }
            }

            return base.Rest();
        }

        public override GCombatResult KillTarget(GUnit Target, bool IsAmbush)
        {
            bool IsClose = false;
            int FutileIcy = 6;
            int FutileBP = 6;

            // Send the ghoul first, if we can.
            if (IsGhoulUp && IsPetBarVisible)
                Context.SendKey("Common.PetAttack");

            // On pull, yank it with Icy Touch, if we can.
            UpdateAvailableRunes();

            if (FrostAvailable >= 1)
                Context.CastSpell("Deathknight.IcyTouch");

            Target.Approach(10.0, false);

            PickedUpAdd = false;

            if (Target.IsPlayer)
                return KillPlayer((GPlayer)Target);

            GMonster Monster = (GMonster)Target;

            while (true)
            {
                Thread.Sleep(101);

                GCombatResult CommonResult = Context.CheckCommonCombatResult(Monster, IsAmbush);

                if (CommonResult != GCombatResult.Unknown)
                    return CommonResult;

                Monster.Face();
                UpdateAvailableRunes();

                double Distance = Monster.DistanceToSelf;

                if (Distance <= Context.MeleeDistance)
                    IsClose = true;

                if (Distance > Context.MeleeDistance)
                {
                    if (IsClose)
                    {
                        Context.Log("Monster is running");

                        if (Interface.IsKeyReady("Deathknight.DeathGrip"))    // Yank him back.
                            Context.CastSpell("Deathknight.DeathGrip");
                        else
                            Target.Approach();
                    }
                    else
                        Target.Approach();

                    IsClose = false;
                    continue;
                }

                // Consider Gnaw.  Note that this is not a spell, just a key, so we don't skip processing if
                // it's gnawing time.  We can't use a duration timer because gnaw could be cooled down but unavailable from
                // energy... in which case we want to spam it until we catch the ghoul with enough energy to gnaw.  
                // The timer here is just to slow down the spam speed.
                if (GnawSpam.IsReady && IsGhoulUp && IsPetBarVisible && Target.Health > .50)
                {
                    GInterfaceObject gnawCooldown = Interface.GetByName("PetActionButton5Cooldown");

                    if (gnawCooldown != null && !gnawCooldown.IsVisible)
                    {
                        Context.SendKey("Deathknight.Gnaw");
                        GnawSpam.Reset();
                    }
                }

                // TODO: check to see if we resurrected as a ghoul

                // Big check: melee tweak.
                if (TicksSinceCombatStart > 1200)           // Wait a bit before tweaking anything.
                    Context.Movement.TweakMelee(Monster);

                // Big check: additional attackers.
                if (CheckAdd(Monster))           
                    continue;
                
                // Interrupt casters:
                if (Target.IsCasting && Me.RunicPower >= 20 && Interface.IsKeyReady("Deathknight.MindFreeze"))
                {
                    Context.CastSpell("Deathknight.MindFreeze");
                    continue;
                }

                // Put up Frost Fever if it's not up:
                if (!Target.HasBuff(DEBUFF_FROSTFEVER) && FrostAvailable > 0 && FutileIcy > 0)
                {
                    Context.CastSpell("Deathknight.IcyTouch");
                    FutileIcy--;
                    continue;
                }

                // Put up Blood Plague if it's not up:
                if (!Target.HasBuff(DEBUFF_BLOODPLAGUE) && UnholyAvailable > 0 && FutileBP > 0)
                {
                    Context.CastSpell("Deathknight.PlagueStrike");
                    FutileBP--;
                    continue;
                }

                // Keep up Horn of Winter:
                if (CanHorn && !IsHornUp && Interface.IsKeyReady("Deathknight.HornOfWinter"))
                {
                    Context.CastSpell("Deathknight.HornOfWinter");
                    continue;
                }

                // Fire off death coil when it's up:
                if ((Me.RunicPower >= 40 || Me.HasBuff(BUFF_DEATHTRANCE)) && Interface.IsKeyReady("Deathknight.DeathCoil"))
                {
                    Context.CastSpell("Deathknight.DeathCoil");
                    continue;
                }

                // Spam stuff here:
                if (ConsiderSpam(UnholyAvailable > 0 && FrostAvailable > 0, "Deathknight.ScourgeStrike"))
                    continue;

                if (ConsiderSpam(UnholyAvailable > 0 && FrostAvailable > 0 && Me.Health < .60, "Deathknight.DeathStrike"))
                    continue;

                if (ConsiderSpam(FrostAvailable > 0, "Deathknight.FrostStrike"))
                    continue;

                if (ConsiderSpam(BloodAvailable > 0 && Me.Health < .60, "Deathknight.RuneTap"))
                    continue;

                if (ConsiderSpam(BloodAvailable > 0, "Deathknight.HeartStrike"))
                    continue;

                if (ConsiderSpam(BloodAvailable > 0, "Deathknight.BloodStrike"))
                    continue;

                if (AvoidAdds && Target.DistanceToSelf <= Context.MeleeDistance && !Target.IsCasting)
                    Movement.ConsiderAvoidAdds(AvoidAddDistance);
            }
        }

        #endregion

        #region Various DK helpers
        bool ConsiderSpam(bool GotResources, string SpellName)
        {
            if (GotResources && Interface.IsKeyPopulated(SpellName) && Interface.IsKeyReady(SpellName))
            {
                Context.CastSpell(SpellName);
                return true;
            }
            else
                return false;
        }

        bool IsGhoulUp
        {
            get
            {
                return Me.Pet != null && Me.Pet.Health > 0.0;
            }
        }

        bool IsPetBarVisible
        {
            get
            {
                return PetButton1.IsVisible; 
            }
        }

        GCombatResult KillPlayer(GPlayer Player)
        {
            Context.Log("Attacked by player, waiting to die off");

            GSpellTimer WaitTime = new GSpellTimer(60 * 1000);
            WaitTime.Wait();

            if (!Me.IsDead)
                Context.KillAction("PlayerLeftAlive", true);

            return GCombatResult.Died;
        }

        bool CheckAdd(GMonster OriginalTarget)
        {
            if (PickedUpAdd)
                return false;

            GUnit Add = GObjectList.GetNearestAttacker(OriginalTarget.GUID);

            if (Add == null)
                return false;

            if (!IsPetBarVisible || Me.Pet == null || Me.Pet.Health < .10)       // No ghoul control, anyway.
                return false;

            // Got an add!
            Context.Log("Additional attacker: \"" + Add.Name + "\", 0x" + Add.GUID.ToString("x") + ", off-tanking with ghoul");

            if (!Add.SetAsTarget(false))    // Couldn't select it.
            {
                Context.Log("Could not select with Tab key, giving up");
                OriginalTarget.SetAsTarget(true);
                return false;
            }

            // Add is targeted!  Poly 'em:

            Context.SendKey("Common.PetAttack");
            PickedUpAdd = true;
            OriginalTarget.SetAsTarget(true);
            OriginalTarget.Face();

            return true;
        }
        #endregion

    }
}
