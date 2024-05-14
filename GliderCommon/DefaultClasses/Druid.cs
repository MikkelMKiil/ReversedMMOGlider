// Druid.cs - default Glider class file.
//
// May 01 2007 MMD - Created base class from old druid.
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
    public class Druid : GGameClass
    {
        enum DruidForm { Bear, Cat };

        #region Druid properties/config
        GSpellTimer Mark = new GSpellTimer(27 * 60 * 1000);
        GSpellTimer Thorns = new GSpellTimer(9 * 60 * 1000);
        GSpellTimer Bash = new GSpellTimer(60 * 1000, true);
        GSpellTimer Rejuve = new GSpellTimer(12 * 1000, true);
        GSpellTimer Regrowth = new GSpellTimer(15 * 1000, true);
        GSpellTimer DemoTimer = new GSpellTimer(30 * 1000, true);
        GSpellTimer FaerieTimer = new GSpellTimer(40 * 1000, true);
        GSpellTimer FormSkipCheck = new GSpellTimer(4 * 1000, true);    // Dampener to keep from spamming shapeshift when lagged.
        GSpellTimer HealTimer = new GSpellTimer(4 * 1000, true);        // Dampener to keep from healing too fast.
        DruidForm Form;
        bool DetectBuffs;
        bool UseMangle;
        bool UseDemo;
        bool UseFaerie;
        bool UseBarkskin;
        bool UseBash;
        bool UseCharge;
        bool UseSwiftness;
        bool UseEnrage;
        bool UseRip;
        bool UseFury;
        bool UseStealth;
        bool StealthNear;
        bool UseSwipe;
        bool UseStarfire;
        bool BashCasters;
        int MaulCost;
        int ClawCost;
        int BiteMultiplier;
        #endregion

        #region GGameClass overrides
        public override string DisplayName
        {
            get { return Context.GetLocal("Common.ClassDruid"); }
        }

        public override GConfigResult ShowConfiguration()
        {
            return Context.ShowStockConfigDialog(GPlayerClass.Druid);
        }

        public override void CreateDefaultConfig()
        {
            Context.SetConfigValue("Druid.TankMode", "False", false);
            Context.SetConfigValue("Druid.PullDistance", "30", false);
            Context.SetConfigValue("Druid.UseDemo", "True", false);
            Context.SetConfigValue("Druid.UseFaerie", "False", false);
            Context.SetConfigValue("Druid.UseBarkskin", "False", false);
            Context.SetConfigValue("Druid.UseMangle", "False", false);
            Context.SetConfigValue("Druid.UseBash", "True", false);
            Context.SetConfigValue("Druid.UseSwiftness", "False", false);
            Context.SetConfigValue("Druid.UseCharge", "False", false);
            Context.SetConfigValue("Druid.UseStarfire", "True", false);
            Context.SetConfigValue("Druid.BashCasters", "False", false);
            Context.SetConfigValue("Druid.MaulCost", "15", false);
            Context.SetConfigValue("Druid.ClawCost", "45", false);
            Context.SetConfigValue("Druid.Enrage", "False", false);
            Context.SetConfigValue("Druid.UseForm", "Bear", false);
            Context.SetConfigValue("Druid.UseRip", "True", false);
            Context.SetConfigValue("Druid.UseFury", "True", false);
            Context.SetConfigValue("Druid.UseStealth", "True", false);
            Context.SetConfigValue("Druid.StealthNear", "False", false);
            Context.SetConfigValue("Druid.BiteMultiplier", "12", false);
            Context.SetConfigValue("Druid.DetectBuffs", "False", false);
        }

        public override void Disengage(GUnit Target)
        {
            if (IsInCatForm)
                Context.CastSpell("Druid.Cower");

            base.Disengage(Target);
        }

        public override void LoadConfig()
        {
            string UseForm = Context.GetConfigString("Druid.UseForm");

            if (UseForm == "Cat")
                Form = DruidForm.Cat;
            else
                Form = DruidForm.Bear;

            UseDemo = Context.GetConfigBool("Druid.UseDemo");
            UseFaerie = Context.GetConfigBool("Druid.UseFaerie");
            UseBarkskin = Context.GetConfigBool("Druid.UseBarkskin");
            UseBash = Context.GetConfigBool("Druid.UseBash");
            BashCasters = Context.GetConfigBool("Druid.BashCasters");
            UseMangle = Context.GetConfigBool("Druid.UseMangle");
            UseCharge = Context.GetConfigBool("Druid.UseCharge");
            UseStarfire = Context.GetConfigBool("Druid.UseStarfire");
            UseSwiftness = Context.GetConfigBool("Druid.UseSwiftness");
            UseEnrage = Context.GetConfigBool("Druid.Enrage");
            UseRip = Context.GetConfigBool("Druid.UseRip");
            UseFury = Context.GetConfigBool("Druid.UseFury");
            UseStealth = Context.GetConfigBool("Druid.UseStealth");
            StealthNear = Context.GetConfigBool("Druid.StealthNear");
            UseSwipe = Context.GetConfigBool("Druid.UseSwipe");
            MaulCost = Context.GetConfigInt("Druid.MaulCost");
            ClawCost = Context.GetConfigInt("Druid.ClawCost");
            BiteMultiplier = Context.GetConfigInt("Druid.BiteMultiplier");
            DetectBuffs = Context.GetConfigBool("Druid.DetectBuffs");
        }

        public override void ResetBuffs()
        {
            Mark.Reset();
            Thorns.Reset();
        }

        public override bool Rest()
        {
            // Kill these buff timers if the spell is gone:
            if (DetectBuffs)
            {
                if (!Me.HasWellKnownBuff("Thorns"))
                    Thorns.ForceReady();

                if (!Me.HasWellKnownBuff("MotW"))
                    Mark.ForceReady();
            }

            switch (Form)
            {
                case DruidForm.Bear: BearRest(); break;
                case DruidForm.Cat: CatRest(); break;
            }

            return base.Rest();
        }

        public override bool CanDrink
        {
            get
            {
                // Only return true if we're out of form.
                if (IsInCatForm || IsInBearForm)
                    return false;
                else
                    return true;
            }
        }

        public override void EnterStealth(bool OverrideConfig)
        {
            // Consider prowling if a lot of conditions are true:

            if (IsInCatForm && Interface.IsKeyReady("Druid.Prowl") && !Me.IsStealth)
            {
                // Only go if Glider really wants us to or we have usestealth w/o stealthnear.
                if (OverrideConfig || (UseStealth && !StealthNear && !Context.IsCorpseNearby))
                {
                    Context.CastSpell("Druid.Prowl");
                    Me.SetBuffsDirty();
                }
            }
        }

        public override void RunningAction()
        {
            if (Form == DruidForm.Cat)
            {
                EnterCatForm();
                EnterStealth(false);
            }

            if (Form == DruidForm.Bear)
                CheckDebuffs();
        }

        public override void ApproachingTarget(GUnit Target)
        {
            EnterStealth(true);
        }

        public override void LeaveForm()
        {
            if (IsInBearForm)
            {
                Context.CastSpell("Druid.BearForm");
                Me.SetBuffsDirty();   // Force update on next check to avoid looking at cached buff.
            }

            if (IsInCatForm)
            {
                Context.CastSpell("Druid.CatForm");
                Me.SetBuffsDirty();   // Force update on next check to avoid looking at cached buff.
            }
        }

        public override string PowerValue
        {
            get
            {
                string TheText;

                TheText = String.Format("{0} ({1}%)", Me.ManaPoints.ToString(), (int)(Me.Mana * 100));

                if (IsInBearForm)
                    TheText += String.Format(" [R = {0}]", Me.Rage.ToString());
                else
                    if (IsInCatForm)
                        TheText = String.Format(" E={0}, CP={1}", Me.Energy.ToString(), Me.ComboPoints.ToString());

                return TheText;
            }
        }

        public override GCombatResult KillTarget(GUnit Target, bool IsAmbush)
        {
            Context.Debug("Druid.KillTarget invoked, isAmbush = " + IsAmbush + ", Target = " + Target.ToString() + ", distance = " + Target.DistanceToSelf);
            // Context.ReleaseSpinRun();  // No charging in going on here.

            if (Target.IsPlayer)
                return KillPlayer((GPlayer)Target, GPlayerSelf.Me.Location);

            GMonster Monster = (GMonster)Target;

            if (IsHealer)
                return HealerKillTarget(Monster, IsAmbush);

            switch (Form)
            {
                case DruidForm.Bear: return BearKillTarget(Monster, IsAmbush);
                case DruidForm.Cat: return CatKillTarget(Monster, IsAmbush);
                default: throw new Exception("!! unknown druid form: " + Form.ToString() + "... !?");
            }
        }
        #endregion

        #region Druid helpers
        bool IsHealer
        {
            get
            {
                return Context.Party.HealMode == GHealDisposition.Dedicated;
            }
        }

        bool CheckDebuffs()
        {
            return Context.RemoveDebuffs(GBuffType.Curse,  "Druid.RemoveCurse", false) ||
                   Context.RemoveDebuffs(GBuffType.Poison, "Druid.Cure", false);
        }

        bool CheckSelfHeal(GUnit Target)
        {
            if (!HealTimer.IsReady)
                return false;

            bool AlsoRejuve = false;

            if (Me.Health < .5 && Target.Health > Me.Health ||
                Me.Health < .3)
            {
                // We're going to heal, see if we can bust a stun or mez or something before leaving form:
                if (UseBash && Me.Rage >= 10 && Interface.IsKeyReady("Druid.Bash"))
                    Context.CastSpell("Druid.Bash", true, false);

                LeaveForm();

                if (Me.Health < .3 || Target.Health > .6)
                    AlsoRejuve = true;

                if (UseSwiftness && Interface.IsKeyReady("Druid.NS"))
                    Context.CastSpell("Druid.NS", true, true);
                else
                {
                    if (UseBarkskin && Me.Health < .30)
                        Context.CastSpell("Druid.Barkskin");
                }

                // Bust out the heal.
                Context.CastSpell("Druid.HealingTouch");

                if (AlsoRejuve)
                    Context.CastSpell("Druid.Rejuvenation");

                HealTimer.Reset();

                return true;
            }
            else
                return false;
        }

        // Special no-form kill target routine for healers.
        GCombatResult HealerKillTarget(GMonster Target, bool IsAmbush)
        {
            Context.CastSpell("Druid.Moonfire");

            if (UseFaerie)
                Context.CastSpell("Druid.Faerie");

            while (true)
            {
                Thread.Sleep(101);
                Target.Approach(PullDistance);
                Target.Face();

                GCombatResult CommonResult = Context.CheckCommonCombatResult(Target, IsAmbush);

                if (CommonResult != GCombatResult.Unknown)
                    return CommonResult;

                CheckPartyHeal(Target);
                CheckSelfHeal(Target);
            }
        }

        #endregion

        #region Party stuff
        public override bool CheckPartyHeal(GUnit OriginalTarget)
        {
            bool Healed = false;

            if (Context.Party.HealMode == GHealDisposition.Never ||
                Context.Party.Mode == GPartyMode.Solo)  // Don't think so.
                return false;

            foreach (GPlayer Guy in Context.Party.GetPartyMemberObjects())
            {
                double HotCheck = .80;
                double HealCheck = .55;

                if (IsHealer)         // Faster healing.
                    HealCheck = .65;

                if (OriginalTarget != null && OriginalTarget.IsTargetingMe)  // If it's hitting me, lower threshold...
                {
                    HealCheck = .30;
                    HotCheck = .50;
                }

                if (Guy.Health < HealCheck)
                {
                    Context.Party.CastOnMember(Guy, "Druid.HealingTouchOther", OriginalTarget);
                    Healed = true;
                }

                if (Guy.Health < HotCheck)
                {
                    if (Context.Party.IsBuffReady(Guy, "Druid.RejuvenationOther", 12))
                    {
                        Context.Party.BuffPartyMember(Guy, "Druid.RejuvenationOther", 12, true);
                        Healed = true;
                    }
                }
            }

            return Healed;
        }

        public override bool CheckPartyBuffs()
        {
            // Mark everyone except other druids.
            return Context.Party.BuffParty("Druid.MarkOther", (26 * 60), GPlayerClass.Priest) ||
                   Context.Party.BuffParty("Druid.MarkOther", (26 * 60), GPlayerClass.Hunter) ||
                   Context.Party.BuffParty("Druid.MarkOther", (26 * 60), GPlayerClass.Mage) ||
                   Context.Party.BuffParty("Druid.MarkOther", (26 * 60), GPlayerClass.Paladin) ||
                   Context.Party.BuffParty("Druid.MarkOther", (26 * 60), GPlayerClass.Shaman) ||
                   Context.Party.BuffParty("Druid.MarkOther", (26 * 60), GPlayerClass.Rogue) ||
                   Context.Party.BuffParty("Druid.MarkOther", (26 * 60), GPlayerClass.Warlock) ||
                   Context.Party.BuffParty("Druid.MarkOther", (26 * 60), GPlayerClass.Warrior);
        }
        #endregion

        #region Cat stuff
        void EnterCatForm()
        {
            if (IsInCatForm || !FormSkipCheck.IsReady)  // Hmm... not happening.
                return;

            if (Me.Mana > .30)
            {
                FormSkipCheck.Reset();
                Context.CastSpell("Druid.CatForm");
                Me.SetBuffsDirty();
            }
        }

        bool IsInCatForm
        {
            get
            {
                return Me.HasWellKnownBuff("Cat");
            }
        }

        void CatRest()
        {
            if (Me.Health < Context.RestHealth)
            {
                LeaveForm();

                if (Me.Health <= .60 || !Rejuve.IsReady)
                {
                    Context.CastSpell("Druid.HealingTouch");
                }
                else
                {
                    if (Rejuve.IsReady)
                    {
                        Rejuve.Reset();
                        Context.CastSpell("Druid.Rejuvenation");
                    }
                }
            }

            if (Mark.IsReady)
            {
                LeaveForm();
                Mark.Reset();
                Context.CastSpell("Druid.Mark");
            }

            if (Thorns.IsReady)
            {
                LeaveForm();
                Thorns.Reset();
                Context.CastSpell("Druid.Thorns");
            }

            CheckDebuffs();
        }

        GCombatResult CatKillTarget(GMonster Target, bool IsAmbush)
        {
            bool IsClose = false;
            bool Ripped = !UseRip;   // Default it to true if rip is disabled to go straight to bite.

            if (UseStealth && !Me.IsStealth && !IsAmbush)
                EnterStealth(true);

            // Slop a bit on approach to deal with annoying druid range bug.  Also set leaverunning
            // so that approach doesn't sleep at the end waiting for key game to see key release.  We'll 
            // release them manually after the opener has been pushed.
            Target.Approach(Context.MeleeDistance - 1.5, true);

            StartCombat();      // Call it manually here, since our approach may have used up sanity time.

            if (!Target.IsInMeleeRange)
            {
                Context.Log("Could not approach: \"" + Target.Name + "\"");
                return GCombatResult.Bugged;
            }

            if (UseFury)
            {
                Context.ReleaseSpinRun();
                Context.CastSpell("Druid.Fury");
                Target.Approach(Context.MeleeDistance - 1.0, true);
            }

            // Opener: if we're stealth, do the right thing.  Otherwise, just whack it. 
            // Note that we use SendKey instead of CastSpell for the opener, since we don't
            // want to risk any delays in casting by checking the interface - the mob could
            // wander out of range.
            if (Me.IsStealth)
            {
                if (Target.IsFacingAway)
                {
                    Context.Log("Target is facing away, opening with ravage");
                    Context.SendKey("Druid.Ravage");
                }
                else
                {
                    Context.Log("Target is not facing away, opening with pounce");
                    Context.SendKey("Druid.Pounce");
                }
            }
            else
                Context.SendKey("Druid.Claw");

            Context.ReleaseSpinRun();

            FaerieTimer.ForceReady();  // Clear this timer.

            while (true)
            {
                Thread.Sleep(101);

                GCombatResult CommonResult = Context.CheckCommonCombatResult(Target, IsAmbush);

                if (CommonResult != GCombatResult.Unknown)
                    return CommonResult;

                Target.Face();

                double Distance = Target.DistanceToSelf;

                if (Distance <= Context.MeleeDistance)
                {
                    if (!IsClose)
                        Context.Log("Moved within melee range");

                    IsClose = true;
                }

                if (Distance > Context.MeleeDistance && IsClose)
                {
                    Context.Log("Target is out of melee range, approaching");
                    Target.Approach();
                    IsClose = false;
                }

                if (CheckSelfHeal(Target) || CheckPartyHeal(Target))
                {
                    Context.Debug("Skipping combat move on heal this time around");
                    continue;
                }

                if (!IsInCatForm)
                    EnterCatForm();

                if (TicksSinceCombatStart > 2000)           // Wait a couple seconds before tweaking anything.
                    Context.Movement.TweakMelee(Target);

                // Ok, start plowing various moves:
                if (FaerieTimer.IsReady && UseFaerie && Interface.IsKeyReady("Druid.Faerie"))
                {
                    FaerieTimer.Reset();
                    Context.CastSpell("Druid.Faerie");
                    continue;
                }

                if (Me.Energy >= 30 && Me.ComboPoints >= 3 && !Ripped)
                {
                    Context.CastSpell("Druid.Rip");
                    Ripped = true;
                    continue;
                }

                if (Ripped && Me.Energy >= 35 &&
                    (Me.ComboPoints == 5 || (Me.ComboPoints * BiteMultiplier >= (Target.Health * 100))))
                {
                    Context.CastSpell("Druid.Bite");
                    continue;
                }

                if (Me.Energy >= ClawCost)
                {
                    Context.CastSpell("Druid.Claw");
                    continue;
                }
            }
        }

        #endregion

        #region Bear stuff
        bool IsInBearForm
        {
            get
            {
                return Me.HasWellKnownBuff("Bear");
            }
        }

        void EnterBearForm()
        {
            if (IsInBearForm || !FormSkipCheck.IsReady)
                return;

            FormSkipCheck.Reset();
            Context.CastSpell("Druid.BearForm");
            Me.SetBuffsDirty();
        }

        void BearRest()
        {
            LeaveForm();

            if (Me.Health >= .6 && Me.Health <= .8 && Rejuve.IsReady)
            {
                Rejuve.Reset();
                Context.CastSpell("Druid.Rejuvenation");
            }

            if (Me.Health < .6)
                Context.CastSpell("Druid.HealingTouch");

            if (Mark.IsReady)
            {
                Mark.Reset();
                Context.CastSpell("Druid.Mark");
            }

            if (Thorns.IsReady)
            {
                Thorns.Reset();
                Context.CastSpell("Druid.Thorns");
            }
        }

        GCombatResult BearKillTarget(GMonster Target, bool IsAmbush)
        {
            GSpellTimer MaulTimer = new GSpellTimer(2500, true);
            bool IsClose = false;
            bool WaitedForCharge = false;
            GCombatResult result = GCombatResult.Success;
            Target.Face();

            // Consider big pull:
            if (Target.DistanceToSelf > 15.0 && !IsAmbush && UseStarfire && !IsInBearForm)
            {
                Context.ReleaseSpinRun();
                Context.CastSpell("Druid.Starfire");
            }

            if (Target.IsInMeleeRange)
                Context.ReleaseSpinRun();
            else
                Target.Approach(PullDistance);

            // Might still be running here, but go ahead and fire this off on the move...
            if (!IsInBearForm)  // .. if we're not already in form.
            {
                Context.CastSpell("Druid.Moonfire", true, true);
                Context.ReleaseSpinRun();
            }

            // Consider a quick heal before we drop into form:
            if (Me.Health < .85 && Me.Mana > .30 && Rejuve.IsReady && !IsInBearForm)
            {
                Context.ReleaseSpinRun();
                Rejuve.Reset();
                Context.CastSpell("Druid.Rejuvenation");
            }

            // Sanity check before going into form:
            if (!Target.IsValid)
                return GCombatResult.Bugged;

            if (Target.DistanceToSelf > PullDistance)  // Might be too far away, see if we missed our pull.
                if (!Target.WaitForEngage())
                    return GCombatResult.Retry;  

            if (Target.DistanceToSelf < 15.0)  // Getting close, better let go.
                Context.ReleaseSpinRun();

            EnterBearForm();

            if (Target.DistanceToSelf > 8.0 && Target.DistanceToSelf < 25.0 && UseCharge && Interface.IsKeyReady("Druid.Charge"))
                Context.CastSpell("Druid.Charge");
            else
                Target.Approach(Context.MeleeDistance * 2, false);  // Let up when we get close to avoid overrunning the monster.

            // Clear these timers to force the spells to go off.
            FaerieTimer.ForceReady();
            DemoTimer.ForceReady();

            if (!Target.WaitForEngage())
            {
                Context.Log("Target never engaged, bailing out");
                LeaveForm();

                if (Target.DistanceToSelf > PullDistance)
                    return GCombatResult.Retry;
                else
                    return GCombatResult.Bugged;
            }

            // Give it a couple seconds to close the gap, then close it ourselves:
            Target.WaitForApproach(Context.MeleeDistance, 2000);
            Target.Approach(Context.MeleeDistance, false);

            while (true)
            {
                Thread.Sleep(101);

                result = Context.CheckCommonCombatResult(Target, IsAmbush);

                if (result != GCombatResult.Unknown)
                    break;

                if (!Me.IsMeleeing)
                    Context.SendKey("Common.ToggleCombat");

                if (!IsInBearForm && Me.Mana > .20)         // Came out of form somehow, so go back in.
                    EnterBearForm();


                double Distance = Target.DistanceToSelf;

                if (Distance <= Context.MeleeDistance)
                    IsClose = true;

                if (Distance > Context.MeleeDistance && IsClose)
                {
                    IsClose = false;
                    if (UseCharge && Interface.IsKeyReady("Druid.Charge") && !WaitedForCharge)
                    {
                        // Try to back it up and set off the charge.
                        Context.Log("Monster is running, setting up for charge");

                        if (Target.DistanceToSelf < 8.0)  // He's too close to charge still, so wait a few seconds.
                        {
                            WaitedForCharge = true;       // Don't get stuck repeating this: one wait per fight.
                            GSpellTimer FutileRun = new GSpellTimer(3000, false);

                            while (!FutileRun.IsReadySlow)
                            {
                                Target.Face();

                                if (Target.DistanceToSelf > 8.0 && Target.DistanceToSelf < 25.0)
                                    break;
                            }

                            if (FutileRun.IsReady)  // Never hit it, just approach.
                                Target.Approach();
                        }
                    }
                    else
                    {
                        Target.Approach();
                    }
                }

                // He's in the right spot, nail it!
                if (Target.DistanceToSelf > 8.0 && Target.DistanceToSelf < 25.0 && UseCharge && Interface.IsKeyReady("Druid.Charge"))
                {
                    Context.CastSpell("Druid.Charge");
                    continue;
                }

                // Tweak any positional stuff:
                Context.Movement.TweakMelee(Target);

                // Big checks for adds/healing:
                if (CheckSelfHeal(Target) || CheckPartyHeal(Target) || BearCheckAdd(Target))
                    continue;

                // Ok, start plowing various moves:
                Context.Debug("Check: " + UseEnrage + "/" + Me.Health + "/" + Target.Health + "/" + Interface.IsKeyReady("Druid.Enrage"));

                if (UseEnrage && Me.Health > .50 && Target.Health > .50 && Interface.IsKeyReady("Druid.Enrage"))
                {
                    Context.CastSpell("Druid.Enrage");
                    continue;
                }

                if (FaerieTimer.IsReady && UseFaerie && Interface.IsKeyReady("Druid.Faerie"))
                {
                    FaerieTimer.Reset();
                    Context.CastSpell("Druid.Faerie");
                    continue;
                }

                if (UseBash && Me.Rage >= 10 && Interface.IsKeyReady("Druid.Bash") && (Target.IsCasting || !BashCasters))
                {
                    Context.CastSpell("Druid.Bash");
                    continue;
                }

                if (Me.Rage >= 15 && UseMangle && Interface.IsKeyReady("Druid.Mangle"))
                {
                    Context.CastSpell("Druid.Mangle");
                    continue;
                }

                // Nothing else going, maul it up.
                if (Me.Rage >= MaulCost && MaulTimer.IsReady && Interface.IsKeyReady("Druid.Maul"))
                {
                    MaulTimer.Reset();  // Don't spam the key.
                    Context.CastSpell("Druid.Maul");
                    continue;
                }

                // Last check: demo roar.  Yee, haw.
                if (UseDemo && DemoTimer.IsReady && Me.Rage >= 10 && Target.Health > .10)
                {
                    GUnit Bystander = GObjectList.GetClosestNeutralMonster();

                    if (Bystander != null && Bystander.DistanceToSelf <= 15.0)
                        Context.Debug("Skipping demo roar, would add neutral mob: " + Bystander.ToString());
                    else
                    {
                        Context.CastSpell("Druid.DemoRoar");
                        DemoTimer.Reset();
                        continue;
                    }
                }
            }

            if (GObjectList.GetNearestAttacker(Target.GUID) != null)
                Context.Log("Got an add at the end of combat, staying in bear form");
            else
                LeaveForm();

            return result;
        }

        bool BearCheckAdd(GMonster OriginalTarget)
        {
            GUnit Extra = GObjectList.GetNearestAttacker(OriginalTarget.GUID);

            if (Extra != null && Extra.IsInMeleeRange && UseSwipe && Me.Rage > 20 && Interface.IsKeyReady("Druid.Swipe"))
            {
                Context.Log("Swiping add");
                Context.CastSpell("Druid.Swipe");
                return true;
            }

            return false;
        }

        #endregion

        #region Kill player (not very good)
        GCombatResult KillPlayer(GPlayer Target, GLocation Anchor)
        {
            bool Moved = false;
            Context.Log("Attempting to kill player: " + Target.Name);
            GSpellTimer FutileCombat = new GSpellTimer(60 * 1000, true);
            GSpellTimer MFSpam = new GSpellTimer(6 * 1000, true);
            GCombatResult Result = GCombatResult.Bugged;

            LeaveForm();

            while (!FutileCombat.IsReadySlow)
            {
                if (!Target.IsValid)
                {
                    Result = GCombatResult.Bugged;
                    break;
                }

                if (Target.IsDead || Target.DistanceToSelf > 40.0)
                {
                    Result = GCombatResult.Success;
                    break;
                }

                if (Me.IsDead)
                {
                    Result = GCombatResult.Died;
                    break;
                }

                // Fix heading:
                Target.Face();

                // Approach:
                if (Target.DistanceToSelf > PullDistance && !Moved && Target.DistanceToSelf < 40.0)
                {
                    Moved = true;
                    Target.Approach(PullDistance - 1.0, false);
                    continue;
                }

                // Melee:
                if (Target.DistanceToSelf <= Context.MeleeDistance && !Me.IsMeleeing)
                {
                    Context.CastSpell("Common.ToggleCombat", false, true);
                    continue;
                }

                if (MFSpam.IsReady && Target.DistanceToSelf < PullDistance)
                {
                    MFSpam.Reset();
                    Context.CastSpell("Druid.Moonfire");
                    continue;
                }

                if (Me.Health < .9 && Rejuve.IsReady)
                {
                    Context.CastSpell("Druid.Rejuvenation");
                    Rejuve.Reset();
                }
            }

            GContext.Main.Movement.MoveToLocation(Anchor);
            return Result;
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
                    case "Druid.Ravage":   // Take Claw if we can't Rounce yet.
                        button = GShortcut.FindMatchingSpellGroup("6785 1082");
                        break;

                    case "Druid.Pounce":   // Take Claw if we can't Pounce yet.
                        button = GShortcut.FindMatchingSpellGroup("9005 1082");
                        break;

                    case "Druid.Bite":   // Take Rip if we can't Bite yet.
                        button = GShortcut.FindMatchingSpellGroup("5221 1079");
                        break;

                    case "Druid.Starfire":   // Take Wrath if we have no starfire.
                        button = GShortcut.FindMatchingSpellGroup("2912 5176");
                        break;

                    case "Druid.Faerie":   // Look for Feral version here and take it, otherwise roll with regular.
                        if (UseFaerie)
                            button = GShortcut.FindMatchingSpellGroup("0x41d9 0x302");
                        else
                            continue;

                        break;

                    case "Druid.BearForm":   // Check Bear, Dire Bear spells here.
                        if (Form == DruidForm.Bear)
                            button = GShortcut.FindMatchingSpellGroup("0x25a2 0x156f");
                        else
                            continue;

                        break;

                    case "Druid.Claw":   // Promote to Mangle if we have it.
                        if (Form == DruidForm.Cat)
                            button = GShortcut.FindMatchingSpellGroup("33876 1082");
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