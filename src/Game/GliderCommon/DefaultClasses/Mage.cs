// Mage.cs - default Glider class file.
//
// May 05 2008 MMD - Added futility timer for conjuring stones.
// Jun 01 2007 MMD - Added futility timers for conjuration food/water.
// May 21 2007 MMD - Changed evocation to use config value, added 
//                 - call for combat debuff removal.
// Apr 27 2007 MMD - Added party buffs.
// Apr 26 2007 MMD - Added obstructed check in breaking off pull.
// Apr 17 2007 MMD - Finished off main stuff for 1.3.1 RC3.
// Apr 02 2007 MMD - Created base mage class.
// Jun 24 2008 MMD - Added in UpdateKeys method.
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
    public enum IceBarrierMode { None = 0, Always = 1, OnHit = 2, Panic = 3 };
    public enum Finisher { None = 0, Wand = 1, Scorch = 2 };

    public class Mage : GGameClass
    {
        const double PULL_MIN_DISTANCE = 20.0;  // Don't try to use slow pull spell if closer than this.
        const double WAND_MAX_RANGE = 30.0;
        const double COUNTERSPELL_RANGE = 30.0;
        const int BUFFID_COMBUSTION = 0x700a;

        #region Mage properties/config
        GSpellTimer ManaShieldTimer = new GSpellTimer(60 * 1000, false);   // 60 seconds for ManaShield
        GSpellTimer Armor = new GSpellTimer(27 * 60 * 1000, false);        // Frost Armor
        GSpellTimer Intellect = new GSpellTimer(27 * 60 * 1000, false);    // Arcane intellect.
        GSpellTimer Dampen = new GSpellTimer(9 * 60 * 1000, false);        // Dampen magic.
        GSpellTimer IceBarrier = new GSpellTimer(30 * 1000, true);         // Actual cooldown on casting.
        GSpellTimer Manastone = new GSpellTimer(3 * 60 * 1000, true);      // Cooldown between using mana stones.
        GSpellTimer Fireblast;
        GSpellTimer MeleeSpell;
        GSpellTimer Evocation = new GSpellTimer(8 * 60 * 1000, true);
        GSpellTimer FoodConjure = new GSpellTimer(60 * 1000, true);        // Futility timer for conjuring to avoid looping action.
        GSpellTimer WaterConjure = new GSpellTimer(60 * 1000, true);       // Futility timer for conjuring to avoid looping action.
        GSpellTimer StoneConjure = new GSpellTimer(2 * 60 * 1000, true);   // Futility timer to avoid looping stones when bag is full.
        

        bool UseStoneInv;     // Read inventory to determine if we have a manastone?
        bool UseCounterspell;
        bool UseDampen;
        bool UseManastones;
        bool WaitOnPull;
        bool UsePoly;
        bool UseCombustion;
        bool UseFrostNova;
        bool UseMeleeSpell;
        bool ApproachFireblast;
        bool SaveFireblast;
        bool UseEvocation;
        int FireblastDistance;
        IceBarrierMode ShieldMode;
        Finisher FinishMode;
        double FinishLife;
        double CounterspellLife;
        double ShieldLife;

        bool GotStone;
        bool Polyd;
        long PolyGUID;
        #endregion

        #region GGameClass overrides
        public override string DisplayName
        {
            get { return "Mage"; }
        }

        public override void Startup()
        {
        }

        public override GConfigResult ShowConfiguration()
        {
            return Context.ShowStockConfigDialog(GPlayerClass.Mage);
        }

        public override void CreateDefaultConfig()
        {
            Context.SetConfigValue("Mage.FireblastCooldownSec", "8", false);
            Context.SetConfigValue("Mage.UseFrostNova", "True", false);
            Context.SetConfigValue("Mage.PullDistance", "30", false);
            Context.SetConfigValue("Mage.ApproachFireblast", "True", false);
            Context.SetConfigValue("Mage.SaveFireblast", "False", false);
            Context.SetConfigValue("Mage.UseCounterspell", "False", false);
            Context.SetConfigValue("Mage.UseCombustion", "False", false);
            Context.SetConfigValue("Mage.UsePoly", "False", false);
            Context.SetConfigValue("Mage.UseEvocation", "False", false);
            Context.SetConfigValue("Mage.UseMeleeSpell", "False", false);
            Context.SetConfigValue("Mage.MeleeSpellCooldown", "10", false);
            Context.SetConfigValue("Mage.WaitOnPull", "True", false);
            Context.SetConfigValue("Mage.UseDampen", "False", false);
            Context.SetConfigValue("Mage.UseManaStones", "False", false);
            Context.SetConfigValue("Mage.IceBarrier", "0", false);
            Context.SetConfigValue("Mage.FinishLife", "10", false);
            Context.SetConfigValue("Mage.CounterspellLife", "40", false);
            Context.SetConfigValue("Mage.FireblastDistance", "20", false);
            Context.SetConfigValue("Mage.Finisher", ((int) Finisher.Wand).ToString(), false);
            Context.SetConfigValue("Mage.ShieldLife", "50", false);
        }

        public override void LoadConfig()
        {
            CounterspellLife = ((double)Context.GetConfigInt("Mage.CounterspellLife")) / 100.0;  // Down to pct.
            FinishLife = ((double)Context.GetConfigInt("Mage.FinishLife")) / 100.0;  // Down to pct.
            ApproachFireblast = Context.GetConfigBool("Mage.ApproachFireblast");
            FireblastDistance = Context.GetConfigInt("Mage.FireblastDistance");
            SaveFireblast = Context.GetConfigBool("Mage.SaveFireblast");
            UseFrostNova = Context.GetConfigBool("Mage.UseFrostNova");
            WaitOnPull = Context.GetConfigBool("Mage.WaitOnPull");
            UseCounterspell = Context.GetConfigBool("Mage.UseCounterspell");
            UseCombustion = Context.GetConfigBool("Mage.UseCombustion");
            UsePoly = Context.GetConfigBool("Mage.UsePoly");
            UseEvocation = Context.GetConfigBool("Mage.UseEvocation");
            UseDampen = Context.GetConfigBool("Mage.UseDampen");
            UseMeleeSpell = Context.GetConfigBool("Mage.UseMeleeSpell");
            UseManastones = Context.GetConfigBool("Mage.UseManaStones");
            ShieldMode = (IceBarrierMode) Context.GetConfigInt("Mage.IceBarrier");
            FinishMode = (Finisher) Context.GetConfigInt("Mage.Finisher");
            Fireblast = new GSpellTimer(Context.GetConfigInt("Mage.FireblastCooldownSec") * 1000, true);
            MeleeSpell = new GSpellTimer(Context.GetConfigInt("Mage.MeleeSpellCooldown") * 1000, true);
            ShieldLife = ((double)Context.GetConfigInt("Mage.ShieldLife")) / 100.0;  // Down to pct.;
            
        }

        // Don't buy this stuff, we can make it.
        public override bool ShouldBuyFood { get { return false; } }
        public override bool ShouldBuyWater { get { return false; } }
        public override bool CanDrink
        {
            get { return true; }   // umm drinking good.
        }
        public override bool Rest()
        {
            if (Me.IsSitting)
            {
                Context.SendKey("Common.Back");
                Thread.Sleep(1100);
            }

            int FoodLeft = Interface.GetActionInventory("Common.Eat");
            int WaterLeft = Interface.GetActionInventory("Common.Drink");

            if (FoodLeft < 10 && FoodConjure.IsReady)
            {
                FoodConjure.Reset();
                Context.Log("Only have " + FoodLeft + " food left, conjuring more");
                Context.CastSpell("Mage.ConjureFood");
                Thread.Sleep(771);  // Slight delay to let response come from server.
                return true;
            }

            if (WaterLeft < 10 && WaterConjure.IsReady)
            {
                WaterConjure.Reset();
                Context.Log("Only have " + WaterLeft + " water left, conjuring more");
                Context.CastSpell("Mage.ConjureWater");
                Thread.Sleep(771);  // Slight delay to let response come from server.
                return true;
            }

            if (UseManastones && StoneConjure.IsReady)
            {
                if (UseStoneInv)        // Jam flag with value from game.
                    GotStone = Context.Interface.GetActionInventory("Mage.UseManastone") != 0;

                if (!GotStone && Me.Mana > .35)
                {
                    StoneConjure.Reset();
                    Context.CastSpell("Mage.CreateManastone");
                    Thread.Sleep(1001);  // Let spell finish so inventory updates.
                    GotStone = true;
                    return true;
                }
            }

            if (Intellect.IsReady && Me.Mana > .2)
            {
                Context.CastSpell("Mage.ArcaneIntellect");
                Intellect.Reset();
                return true;
            }

            if (UseEvocation &&
                Me.Mana < Context.RestMana && Interface.IsKeyReady("Mage.Evocation") && !UsePoly && Me.Health > Context.RestHealth &&
                Evocation.IsReady)
            {
                Evocation.Reset();
                Context.CastSpell("Mage.Evocation", true, false, 2000);
                return true;
            }

            return base.Rest();
        }

        public override void ResetBuffs()
        {
            Intellect.ForceReady();
            Armor.ForceReady();
            Dampen.ForceReady();
            ManaShieldTimer.ForceReady();
        }

        public override void OnStartGlide()
        {
            if (UseManastones)
            {
                int Stones = Context.Interface.GetActionInventory("Mage.UseManastone");

                if (Stones == -1)
                {
                    Context.Log("Can't read manastone from inventory (barstate should not be indifferent), assuming no manastone present");
                    GotStone = false;
                    UseStoneInv = false;
                }
                else
                {
                    if (Stones == 0)
                        Context.Log("No mana stone, will conjure at rest");
                    else
                        Context.Log("Looks like we have a mana stone");

                    UseStoneInv = true;
                }
            }

            base.OnStartGlide();
        }

        public override void RunningAction()
        {
            if (Context.RemoveDebuffs(GBuffType.Curse, "Mage.RemoveCurse", false))
                return;

            // Check for IsKeyReady explicitly to keep from waiting for a GCD while running - this
            // will only cast the spell if the key is ready, so there's no chance of running past something.
            if (Armor.IsReady && Me.Mana > .2 && Interface.IsKeyReady("Mage.FrostArmor"))
            {
                Context.CastSpell("Mage.FrostArmor");
                Armor.Reset();
                return;
            }

            if (UseDampen && Dampen.IsReady && Me.Mana > .2 && Interface.IsKeyReady("Mage.DampenMagic"))
            {
                Context.CastSpell("Mage.DampenMagic");
                Dampen.Reset();
                return;
            }
        }

        public override void ApproachingTarget(GUnit Target)
        {
            if (ShieldMode == IceBarrierMode.Always && IceBarrier.IsReady)
            {
                Context.CastSpell("Mage.IceBarrier");
                IceBarrier.Reset();
            }
        }

        public override GCombatResult KillTarget(GUnit Target, bool IsAmbush)
        {
            Context.Debug("Mage.KillTarget invoked, isAmbush = " + IsAmbush + ", Target = " + Target.ToString() + ", distance = " + Target.DistanceToSelf);

            GCombatResult CommonResult;
            double StartLife = Me.Health;
            bool WasClose = false;

            Polyd = false;

            if (Target.IsPlayer)
                return KillPlayer((GPlayer) Target);

            GMonster Monster = (GMonster) Target;

            if (IceBarrier.IsReady)
            {
                if (ShieldMode == IceBarrierMode.Always ||
                    (ShieldMode == IceBarrierMode.OnHit && IsAmbush))
                {
                    Context.CastSpell("Mage.IceBarrier");
                    IceBarrier.Reset();
                }
            }

            if (Context.IsRunning || Context.IsSpinning)
            {
                Context.ReleaseSpinRun();
                Thread.Sleep(201);  // Let the game register the arrow key reelase.
            }

            Target.Face();
            Context.Debug("Faced target");

            if (UseCombustion && Interface.IsKeyReady("Mage.Combustion") && !Me.HasWellKnownBuff("Combustion"))
                Context.CastSpell("Mage.Combustion");

            // Cast pull spell if there's time:
            if ((Target.DistanceToSelf > PULL_MIN_DISTANCE && !IsAmbush) ||
                (Target.Reaction == GReaction.Neutral && Target.TargetGUID == 0))  
            {
                Context.CastSpell("Mage.Frostbolt");

                if (Target.DistanceToSelf > PULL_MIN_DISTANCE)
                {
                    bool Channeled = Context.CastSpell("Mage.Fireball");

                    if (!Channeled && !Me.IsInCombat)
                    {
                        // No good, try again?
                        if (Target.DistanceToSelf > PullDistance)
                        {
                            Context.Log("No channeling/combat on second spell, must have gone out of range");
                            return GCombatResult.Retry;
                        }
                        else
                        {
                            Context.Log("No combat after second spell and in range, must be obstructed");
                            return GCombatResult.Bugged;
                        }
                    }

                    if (WaitOnPull)
                        Target.WaitForApproach(base.PullDistance, 3000);
                }
            }

            // Pop 'em with a fireblast if they're close enough and it's an ambush:
            if (IsAmbush && Monster.DistanceToSelf < FireblastDistance && Fireblast.IsReady)
            {
                Context.CastSpell("Mage.Fireblast");
                Fireblast.Reset();
            }

            // Ok, main loop:
            while (true)
            {
                Thread.Sleep(101);

                // See if any of the normal combat exits got triggered - bail out if so.
                CommonResult = Context.CheckCommonCombatResult(Monster, IsAmbush);

                if (CommonResult != GCombatResult.Unknown)
                    break;

                if (Monster.DistanceToSelf > PullDistance)
                {
                    if (Monster.DistanceToSelf > 100.0)
                    {
                        Context.Log("! Monster is way too far away!");
                        return GCombatResult.Vanished;
                    }
                    else
                    {
                        Monster.Approach(this.PullDistance);
                    }
                }
                else
                    Monster.Face();

                // Runner!
                if (WasClose && Target.DistanceToSelf > Context.MeleeDistance)
                {
                    WasClose = false;

                    if (Interface.IsKeyReady("Mage.Fireblast") && SaveFireblast)
                    {
                        Context.Log("Shooting runner");
                        Context.CastSpell("Mage.Fireblast", true, false);
                        continue;
                    }
                }

                if (Target.DistanceToSelf < Context.MeleeDistance)
                    WasClose = true;

                // Counterspell!
                if (UseCounterspell && Monster.IsCasting && Monster.Health < CounterspellLife &&
                    Interface.IsKeyReady("Mage.Counterspell") && Monster.DistanceToSelf <= COUNTERSPELL_RANGE)
                {
                    Context.CastSpell("Mage.Counterspell", true, false);
                    continue;
                }

                if ((Me.Health < StartLife && ShieldMode == IceBarrierMode.OnHit) ||
                    (Me.Health < ShieldLife && ShieldMode != IceBarrierMode.None) ||
                    ShieldMode == IceBarrierMode.Always)
                {
                    if (IceBarrier.IsReady)
                    {
                        Context.CastSpell("Mage.IceBarrier");
                        IceBarrier.Reset();
                        StartLife = Me.Health;  // Remember this, just in case.
                        continue;
                    }
                }

                // Potion?
                if (Me.Health < .20 && Interface.IsKeyReady("Common.Potion") && Interface.IsKeyEnabled("Common.Potion"))
                {
                    Context.CastSpell("Common.Potion");
                    continue;
                }

                if (UseManastones)
                {
                    if (UseStoneInv)        // Jam flag with value from game.
                        GotStone = Context.Interface.GetActionInventory("Mage.UseManastone") != 0;

                    // Mana stone?
                    if (GotStone && Manastone.IsReady &&
                        ((Target.Health > .20 && Me.Mana < .20) ||
                         Me.Mana < .10))
                    {
                        Context.CastSpell("Mage.UseManastone");
                        Manastone.Reset();
                        GotStone = false;
                        continue;
                    }
                }

                // Defense is good.  Check offense:
                //

                // Extra attacker?
                if (CheckAdd(Monster))
                    continue;

                // Frost nova?
                if (ShouldFrostNova(Monster))
                {
                    if (DoFrostNova(Monster, true))
                        continue;
                }

                // Fireblast time?
                if (Fireblast.IsReady && Monster.DistanceToSelf < FireblastDistance && (Target.Health > .70 || !SaveFireblast))
                {
                    Context.CastSpell("Mage.Fireblast");
                    Fireblast.Reset();
                    continue;
                }

                // Melee spell?
                if (UseMeleeSpell && MeleeSpell.IsReady && Interface.IsKeyReady("Mage.MeleeSpell") && Target.DistanceToSelf <= Context.MeleeDistance)
                {
                    MeleeSpell.Reset();
                    Context.CastSpell("Mage.MeleeSpell");
                    continue;
                }

                // Go into the wand loop if they're low enough or we're gassed:
                if (((FinishMode == Finisher.Wand && Target.Health < FinishLife) || Me.Mana < .02) && Target.DistanceToSelf < WAND_MAX_RANGE)
                {
                    Context.Debug("Going into wand for finisher");
                    CommonResult = DoWand(Monster);

                    if (CommonResult != GCombatResult.Unknown)  // Something happened, we're done.
                        break;

                    continue;
                }

                if (Context.RemoveDebuffs(GBuffType.Curse, "Mage.RemoveCurse", true))
                    continue;

                // Spam it up.
                string SpamSpell = "Mage.Fireball";

                if (Target.Health < FinishLife && FinishMode == Finisher.Scorch)
                    SpamSpell = "Mage.Scorch";

                if (UseCounterspell && Target.DistanceToSelf <= COUNTERSPELL_RANGE)
                    Context.CastSpellWithInterrupt(SpamSpell, Monster, 
                                                   CounterspellLife, "Mage.Counterspell", true);
                else
                    Context.CastSpell(SpamSpell);

            }

            // If we poly'd someone, try to set it up as the next target and possibly grab some health/mana
            // before the next fight.  We won't be able to rest, so it has to be now:
            if (CommonResult == GCombatResult.Success && Polyd)
            {
                GUnit Add = GObjectList.FindUnit(PolyGUID);

                if (Add == null)
                {
                    Context.Log("! Could not find poly'd unit after combat, id = " + PolyGUID.ToString("x"));
                    return GCombatResult.Success;
                }

                if (!Add.SetAsTarget(false))
                {
                    Context.Log("! Could not target poly'd unit after combat, name = \"" + Add.Name + "\", id = " + Add.GUID.ToString("x"));
                    return GCombatResult.Success;
                }

                // If we have no other (third) attackers, consider bandaging and evocation:
                if (GObjectList.GetNearestAttacker(Add.GUID) == null)
                {
                    base.CheckBandageApply(true);

                    if (UseEvocation && Me.Mana < .33 && Interface.IsKeyReady("Mage.Evocation"))
                    {
                        Context.Log("Mana is low on poly, firing evocation");
                        Context.CastSpell("Mage.Evocation", true, false, 2000);
                    }

                    //! Back away from this guy if he's too close.
                    if (Add.DistanceToSelf < 12.0)
                        Movement.BackAway(Add, 17.0);
                }

                // Tell Glider to immediately begin wasting this guy and not rest:
                CommonResult = GCombatResult.SuccessWithAdd;
            }

            return CommonResult;
        }

        GCombatResult KillPlayer(GPlayer Target)
        {
            Context.Log("Killing player: \"" + Target + "\" (note: finish this later!)");

            while (!Me.IsDead)
                Thread.Sleep(1000);

            return GCombatResult.Died;
        }

        #endregion

        #region Complex functions
        bool ShouldFrostNova(GMonster Target)
        {
             return (Target.Health > .35 || Me.Health < .30) && Target.DistanceToSelf <= Context.MeleeDistance && UseFrostNova && Me.Mana > .2 && Interface.IsKeyReady("Mage.FrostNova") && !Polyd;
        }

        // Do a frost nova move and return true if we did something.  False if we changed our minds.
        bool DoFrostNova(GMonster Monster, bool ShootAfter)
        {
            // Stupid check for neutral mobs:
            GMonster Nearest = GObjectList.GetClosestNeutralMonster();

            if (Nearest != null && Nearest.DistanceToSelf < 12.0)  // Neutral mobs too close, will probably jump in.
                return false;

            GLocation MyStartPos = Me.Location;
            GLocation MonsterStartPos = Monster.Location;
            bool CheckedDistance = false;

            Thread.Sleep(676);

            // Second check in here in case the monster's health dropped while we were thinking about FN,
            // which happens a lot due to general server lag from damage spells.
            if (!ShouldFrostNova(Monster))
                return false;

            Context.CastSpell("Mage.FrostNova", true, true);
            Thread.Sleep(431);
            Context.PressKey("Common.Back");

            GSpellTimer FutileBackup = new GSpellTimer(4000, false);
            GSpellTimer DistanceCheck = new GSpellTimer(1000, false);
            bool JumpWait = false;

            while (!FutileBackup.IsReadySlow)
            {
                if (DistanceCheck.IsReady && !CheckedDistance)
                {
                    CheckedDistance = true;

                    if (MyStartPos.GetDistanceTo(Me.Location) < 3.0)
                    {
                        Context.Log("Character is not moving in Frost Nova, must be stuck!");
                        break;
                    }

                    if (MonsterStartPos.GetDistanceTo(Monster.Location) > 5.0)
                    {
                        Context.Log("Monster is moving in Frost Nova, must have resisted!");
                        break;
                    }

                    if (Context.RNG.Next() % 2 == 0)
                    {
                        Context.CastSpell("Common.Jump", false, true);
                        JumpWait = true;
                    }
                }

                if (Monster.DistanceToSelf > 9.0)
                    break;
            }

            Context.ReleaseKey("Common.Back");
            Thread.Sleep(401);  // Let game register key release.

            if (Monster.DistanceToSelf > 9.0)
            {
                if (JumpWait)
                    Thread.Sleep(700);

                if (ShootAfter)
                    Context.CastSpell("Mage.Fireball");
            }

            return true;
        }

        // Start wanding this guy and keep at it until he's dead or we decide it's time to
        // stop wanding:
        GCombatResult DoWand(GMonster Monster)
        {
            bool StopWand = false;
            GSpellTimer FutileWand = new GSpellTimer(12 * 1000, false);

            Context.CastSpell("Mage.Wand", true, true);

            while (!FutileWand.IsReadySlow)
            {
                GCombatResult CommonResult = Context.CheckCommonCombatResult(Monster, false);

                if (CommonResult != GCombatResult.Unknown)
                    return CommonResult;

                if (!Interface.IsKeyFiring("Mage.Wand"))
                {
                    if (!Monster.IsDead)
                        Context.Log("Wand is no longer firing - we get knocked down or stunned?");

                    break;
                }

                Monster.Face(Math.PI / 4.0);   // About 45 degrees.

                // Got an add and this guy isn't dead yet, better bust out.
                if (GObjectList.GetNearestAttacker(Monster.GUID) != null && Monster.Health > .20)
                    StopWand = true;

                // We're getting low and he's not, better bust out.
                if ((Me.Health < .30 && Monster.Health > .1) ||
                    Me.Health < .20)
                    StopWand = true;

                if (StopWand)
                {
                    Context.CastSpell("Mage.Wand", false, false);   // Stop the wand and get out!
                    Context.WaitForNotFiring("Mage.Wand");
                    break;
                }
            }

            return GCombatResult.Unknown;
        }

        // See if we have an extra attacker and should do something about it.  If something was
        // done, returns True.  Otherwise, False.
        bool CheckAdd(GMonster OriginalTarget)
        {
            if (Polyd || !UsePoly)
                return false;

            GUnit Add = GObjectList.GetNearestAttacker(OriginalTarget.GUID);

            if (Add == null)
                return false;

            if (!CanPoly(Add))
                return false;

            // Got an add!
            Context.Log("Additional attacker: \"" + Add.Name + "\", 0x" + Add.GUID.ToString("x") + ", polymorphing");

            if (!Add.SetAsTarget(false))    // Couldn't select it.
            {
                Context.Log("Could not select with Tab key, turning off poly option");
                UsePoly = false;
                OriginalTarget.SetAsTarget(true);
                return false;
            }

            if (UseFrostNova && Interface.IsKeyReady("Mage.FrostNova") &&
                (OriginalTarget.DistanceToSelf <= Context.MeleeDistance ||
                 Add.DistanceToSelf <= Context.MeleeDistance))
            {
                Context.Log("Using FrostNova to make some room for poly");
                DoFrostNova(OriginalTarget, false);
            }


            // Add is targeted!  Poly 'em:
            Context.CastSpell("Mage.Poly");
            Polyd = true;
            PolyGUID = Add.GUID;
            OriginalTarget.SetAsTarget(true);
            OriginalTarget.Face();

            return true;

        }

        private bool CanPoly(GUnit MyTarget)
        {
            if (MyTarget.CreatureType == GCreatureType.Beast || MyTarget.CreatureType == GCreatureType.Humanoid)
            {
                return true;
            }
            return false;
        }

        public override bool CheckPartyBuffs()
        {
            //LogHelper.Debug("CheckPartyBuffs for mage");
            return Context.Party.BuffParty("Mage.ArcaneIntellectOther", (26 * 60), GPlayerClass.Druid) ||
                   Context.Party.BuffParty("Mage.ArcaneIntellectOther", (26 * 60), GPlayerClass.Hunter) ||
                   Context.Party.BuffParty("Mage.ArcaneIntellectOther", (26 * 60), GPlayerClass.Paladin) ||
                   Context.Party.BuffParty("Mage.ArcaneIntellectOther", (26 * 60), GPlayerClass.Priest) ||
                   Context.Party.BuffParty("Mage.ArcaneIntellectOther", (26 * 60), GPlayerClass.Shaman) ||
                   Context.Party.BuffParty("Mage.ArcaneIntellectOther", (26 * 60), GPlayerClass.Warlock);
        }

        #endregion

        #region UpdateKeys method
        public override void UpdateKeys(GKey[] UpdatableKeys)
        {
            foreach (GKey One in UpdatableKeys)
            {
                GShortcut button = null;

                switch (One.KeyName)
                {
                    case "Mage.MeleeSpell":     // Take Dragon's Breath if it's there, otherwise CoC.
                        if (UseMeleeSpell)
                            button = GShortcut.FindMatchingSpellGroup("0x7bad 0x78");
                        else
                            continue;
                        break;

                    case "Mage.FrostArmor":     // Molten Armor if it's there, otherwise Ice Armor or wienie Frost Armor.
                        button = GShortcut.FindMatchingSpellGroup("0x7712 0x1c86 0xa8");
                        break;

                    case "Mage.UseManastone":   // Highest stone first, then others.
                        if (UseManastones)
                            button = GShortcut.FindMatchingShortcut(GShortcutType.Item, "0x561c 0x1f48 0x1f47 0x1589 0x158a");
                        else
                            continue;
                        break;

                    case "Mage.CreateManastone":  // Highest stone first, then others.
                        if (UseManastones)
                            button = GShortcut.FindMatchingShortcut(GShortcutType.Spell, "0x69dd 0x2746 0x2745 0xde0 0x2f7");
                        else
                            continue;
                        break;

                    default:
                        continue;  // Don't try to handle this, we didn't do anything smart.
                }

                if (button != null)
                {
                    Context.Debug("Mapped " + One.KeyName + " -> " + button.ToString());
                    One.SIM = button.ShortcutValue;
                }
                else
                    Context.Log("Unable to find suitable button for " + One.KeyName + ", see help section \"Re-Assigning Keys\" under Key Mappings.");

            }
        }
        #endregion
    }
}