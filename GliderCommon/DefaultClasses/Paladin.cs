// Paladin.cs - default Glider class file.
//
// May 21 2007 MMD - Added combat debuff removal call.
// Apr 17 2007 MMD - Fixed horrible looping heal bug with bad counter check.
// Mar 02 2007 MMD - Added logic to back away from possible adds.
// Mar 01 2007 MMD - Added Crusader Strike functionality.
// Feb 26 2007 MMD - Added FinishJudgeLife functionality.
// Feb 20 2007 MMD - Base paladin functionality implemented.
// Feb 07 2007 MMD - Created base paladin file and template for other classes.
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
    public class Paladin : GGameClass
    {
        #region Internal properties for the paladin
        const double JUDGMENT_RANGE = 10.0;
        const double AVOID_ADD_HEADING_TOLERANCE = 1.04;  // About 120 (60 each way = PI/3) degree arc in front of us.

        GSpellTimer DivineProt = new GSpellTimer(5 * 60 * 1000, true);
        GSpellTimer Blessing = new GSpellTimer(4 * 60 * 1000, true);
        GSpellTimer DivineFavor = new GSpellTimer(2 * 60 * 1000, true);
        GSpellTimer Seal = new GSpellTimer(29 * 1000, true);
        GSpellTimer Hammer = new GSpellTimer(60 * 1000, true);
        GSpellTimer Wrath = new GSpellTimer(6 * 1000, true);
        GSpellTimer DivineShield = new GSpellTimer(5 * 60 * 1000, true);
        GSpellTimer BOP = new GSpellTimer(5 * 1000 * 60);   

        string CurrentSeal = "None";
        bool GotAura;
        bool UseDivineFavor;
        bool UseWrath;
        bool JudgeRunners;
        bool UseCrusaderStrike;
        double FinishJudgeLife;
        bool AvoidAdds;
        int AvoidAddDistance;
        #endregion

        #region GGameClass overrides
        public override string DisplayName { get { return "Paladin"; } }

        //public override void Startup()
        //{
        //    Context.ChatLog += new GContext.GChatLogHandler(TestChatHandle);
        //    Context.CombatLog += new GContext.GCombatLogHandler(TestCombatHandle);
        //}

        //public override void Shutdown()
        //{
        //    Context.ChatLog -= new GContext.GChatLogHandler(TestChatHandle);
        //    Context.CombatLog -= new GContext.GCombatLogHandler(TestCombatHandle);
        //}

        public override GConfigResult ShowConfiguration()
        {
            Context.Debug("Paladin.ShowConfiguration");
            return Context.ShowStockConfigDialog(GPlayerClass.Paladin);
        }

        public override void CreateDefaultConfig()
        {
            Context.Debug("Paladin.CreateDefaultConfig");
            Context.SetConfigValue("Paladin.PullDistance", "30", false);
            Context.SetConfigValue("Paladin.UseDivineFavor", "False", false);
            Context.SetConfigValue("Paladin.UseWrath", "True", false);
            Context.SetConfigValue("Paladin.JudgeRunners", "True", false);
            Context.SetConfigValue("Paladin.FinishJudgeLife", ".15", false);
            Context.SetConfigValue("Paladin.UseCrusaderStrike", "False", false);
            Context.SetConfigValue("Paladin.AvoidAdds", "True", false);
            Context.SetConfigValue("Paladin.AvoidAddDistance", "30", false);
        }

        public override void LoadConfig()
        {
            Context.Debug("Paladin.LoadConfig");
            UseDivineFavor = Context.GetConfigBool("Paladin.UseDivineFavor");
            UseWrath = Context.GetConfigBool("Paladin.UseWrath");
            JudgeRunners = Context.GetConfigBool("Paladin.JudgeRunners");
            FinishJudgeLife = Context.GetConfigDouble("Paladin.FinishJudgeLife");
            UseCrusaderStrike = Context.GetConfigBool("Paladin.UseCrusaderStrike");
            AvoidAdds = Context.GetConfigBool("Paladin.AvoidAdds");
            AvoidAddDistance = Context.GetConfigInt("Paladin.AvoidAddDistance");
            GotAura = true;  // Assume the player has an aura already.
        }

        public override void OnResurrect()
        {
            Context.Debug("Paladin.OnResurrect");
            GotAura = false;    // Assume the aura died with the player.
        }

        public override bool Rest()
        {
            int FutileHeals = 5;

            while (Me.Health < Context.RestHealth && FutileHeals > 0 && !Me.IsUnderAttack)
            {
                FutileHeals--;

                if (Me.Health < GContext.Main.RestHealth - .20)
                    Context.CastSpell("Paladin.Heal");
                else
                    Context.CastSpell("Paladin.FastHeal");

                // Sleep a bit to make sure it takes effect before recasting.
                Thread.Sleep(601);
            }

            // Drink it up.
            return base.Rest();
        }

        public override void ApproachingTarget(GUnit Target)
        {
            // Put up Seal of Crusader if: something else is up or we think it'll expire before we engage.
            if (CurrentSeal != "Paladin.SealCrusader" || Seal.TicksLeft < 5000)
                SetSeal("Paladin.SealCrusader");
        }

        // Careful to only cast one spell at a time here to avoid Glider overrunning
        // while you fire a bunch of spells.
        public override void RunningAction()
        {
            if (Blessing.IsReady)
            {
                Context.CastSpell("Paladin.Blessing");
                Blessing.Reset();
                return;
            }

            if (Context.RemoveDebuffs(GBuffType.Poison, "Paladin.Cleanse", false) ||
                Context.RemoveDebuffs(GBuffType.Disease, "Paladin.Cleanse", false) ||
                Context.RemoveDebuffs(GBuffType.Magic, "Paladin.Cleanse", false))
                return;

            if (!GotAura)
            {
                GotAura = true;
                Context.CastSpell("Paladin.Aura");
                return;
            }

        }
        #endregion

        #region Big override: KillTarget
        public override GCombatResult KillTarget(GUnit Target, bool IsAmbush)
        {
            bool WasClose = false;
            GSpellTimer HealTimer = new GSpellTimer(6000);
            int Heals = 0;
            bool JudgeLater = false;
            bool Finished = false;

            if (Target.IsPlayer)
            {
                Context.Log("Paladin can't kill players yet, sorry.");
                WaitForPlayerDeath();
                return GCombatResult.Died;
            }

            // We know it's a monster, so cast it down to the subclass for quick
            // access to monster-specific methods and properties.
            GMonster Monster = (GMonster)Target;

            // Check to see if we're set up a a dedicated healer.  If so, use the tiny
            // helper method to do it.
            if (Context.Party.HealMode == GHealDisposition.Dedicated)
                return KillTargetAsHealer(Target, IsAmbush);

            // First up, make sure we have crusader seal going.
            if (CurrentSeal != "Paladin.SealCrusader" || Seal.TicksLeft < 5000)
                SetSeal("Paladin.SealCrusader");

            // Let go of keys if we're close enough.
            if (Monster.DistanceToSelf <= Context.MeleeDistance)
                Context.ReleaseSpinRun();

            // If he's too far, run up:
            if (Monster.DistanceToSelf > JUDGMENT_RANGE)
                Monster.Approach(JUDGMENT_RANGE, true);
            else
                Monster.Face(GContext.TOLERANCE_SPELLCASTING);

            // Last checks before pulling, in case someone else tagged it:
            Monster.Refresh(true);
            if (!Monster.IsValid)
                return GCombatResult.Vanished;

            if (Monster.IsTagged && !Monster.IsMine && !IsAmbush)
                return GCombatResult.OtherPlayerTag;

            if (Monster.DistanceToSelf > JUDGMENT_RANGE)        // Couldn't approach... ?!
                return GCombatResult.Bugged;

            // Ok, it's on.  Throw down the seal, which also starts combat:
            Context.ReleaseSpinRun();

            if (Interface.IsKeyReady("Paladin.Judge"))
                JudgeSeal();
            else
            {
                Context.Log("Judge not ready at combat start, will judge later");
                Context.SendKey("Common.ToggleCombat");
                JudgeLater = true;
            }

            // Put up the holy damage seal:
            if (!JudgeLater)    
                SetSeal("Paladin.SealCommand");

            // Make sure we're at melee distance here:
            Monster.Approach();

            // Combat loop waiting for guy to die.
            while (true)
            {
                Thread.Sleep(101);

                // See if any of the normal combat exits got triggered - bail out if so.
                GCombatResult CommonResult = Context.CheckCommonCombatResult(Monster, IsAmbush);

                if (CommonResult != GCombatResult.Unknown)
                    return CommonResult;

                // If we're in melee range, remember it for later.
                if (Target.DistanceToSelf <= Context.MeleeDistance)
                    WasClose = true;

                // Check to see if he's running away:
                if (Target.DistanceToSelf > Context.MeleeDistance)
                {
                    if (WasClose)
                    {
                        WasClose = false;

                        if (JudgeRunners && Target.Health < .20)   // Make sure it wasn't a punt or other confusion before judging...
                        {
                            if (Interface.IsKeyReady("Paladin.Judge"))
                            {
                                Context.Log("Judging runner");
                                JudgeSeal();
                                continue;
                            }
                            else
                            {
                                Context.Log("Judgement not ready, chasing down runner");
                            }
                        }
                    }

                    // Track him down.
                    Monster.Approach();
                }

                // Fix up our positioning:
                Context.Movement.TweakMelee(Monster);

                // Consider healing:
                if (HealTimer.IsReady)
                {
                    if ((Me.Health < .5 && Monster.Health > .40) ||
                        (Me.Health < .25 && Target.Health > .5) ||
                        (Me.Health < .15))
                    {
                        HealTimer.Reset();
                        Heals++;
                        DoPaladinHeal(false);
                        continue;
                    }

                    // Consider doing some big healing:
                    if ((Me.Health < .25 && Heals >= 2) && Target.Health > .10)   // Getting beat on, try to help out for real.
                    {
                        Context.Log("Doing emergency heal, IsDead = " + Me.IsDead + ", Health = " + Me.Health + ", HealthPoints = " + Me.HealthPoints);
                        DoEmergencyHeal(Monster);
                        continue;
                    }
                }

                // Consider executing:
                if (UseWrath && Target.Health <= .20 && Target.Health > .04 && Wrath.IsReady)
                {
                    Wrath.Reset();
                    Context.CastSpell("Paladin.HammerWrath");
                    continue;
                }

                // Maybe whack this guy if he's low?
                if (FinishJudgeLife > 0.0 && Monster.Health <= FinishJudgeLife && Interface.IsKeyReady("Paladin.Judge") && !Finished)
                {
                    Finished = true;
                    Context.Log("Trying to finish off target with judgment");
                    Context.CastSpell("Paladin.Judge");
                    Seal.SetTicksLeft(3000);  // Fool it so we don't recast seal right away.
                    continue;
                }

                // Remove any debuffs that are specified as combat remove:
                if (Context.RemoveDebuffs(GBuffType.Poison, "Paladin.Cleanse", true) ||
                    Context.RemoveDebuffs(GBuffType.Disease, "Paladin.Cleanse", true) ||
                    Context.RemoveDebuffs(GBuffType.Magic, "Paladin.Cleanse", true))
                    continue;

                // Pick up the later judge call if we couldn't do it at combat start:
                if (JudgeLater && Interface.IsKeyReady("Paladin.Judge"))
                {
                    JudgeLater = false;
                    JudgeSeal();
                    SetSeal("Paladin.SealCommand");
                    continue;
                }

                // Nothing too exciting.  Refresh the seal if it's off:
                if (Seal.IsReady && Monster.Health > .10)
                {
                    SetSeal("Paladin.SealCommand");
                    continue;
                }

                // Combat debuff removal.
                if (Context.RemoveDebuffs(GBuffType.Poison, "Paladin.Cleanse", true) ||
                    Context.RemoveDebuffs(GBuffType.Disease, "Paladin.Cleanse", true) ||
                    Context.RemoveDebuffs(GBuffType.Magic, "Paladin.Cleanse", true))
                    continue;


                // Really bored.  Crusader Strike!
                if (UseCrusaderStrike && Me.Mana > .10 && Interface.IsKeyReady("Paladin.CrusaderStrike"))
                {
                    Context.CastSpell("Paladin.CrusaderStrike");
                    continue;
                }

                // Still bored, maybe a party member needs healing?
                if (CheckPartyHeal(Target))
                    continue;

                // Extremely bored - maybe we should back up?
                if (AvoidAdds && Target.DistanceToSelf <= Context.MeleeDistance && !Target.IsCasting)
                    Movement.ConsiderAvoidAdds(AvoidAddDistance);
            }
        }
        #endregion

        #region Helper methods for paladin
        /// <summary>
        /// Internal paladin method for helping out as a healer.
        /// </summary>
        /// <param name="Target">GUnit being attacked</param>
        /// <param name="IsAmbush">Ambush (no pull) flag</param>
        /// <returns>How well we did</returns>
        GCombatResult KillTargetAsHealer(GUnit Target, bool IsAmbush)
        {
            while (true)
            {
                Thread.Sleep(101);

                // See if any of the normal combat exits got triggered - bail out if so.
                GCombatResult CommonResult = Context.CheckCommonCombatResult((GMonster) Target, IsAmbush);

                if (CommonResult != GCombatResult.Unknown)
                    return CommonResult;

                CheckPartyHeal(Target);
            }

            return GCombatResult.Success;
        }


        /// <summary>
        /// Cast this spell and remember the seal name & timer info.
        /// </summary>
        /// <param name="KeyName">Key name for the seal</param>
        public void SetSeal(string KeyName)
        {
            Context.CastSpell(KeyName, true, false);
            CurrentSeal = KeyName;
            Seal.Reset();
        }

        /// <summary>
        /// Judge the current seal and dump the timer so we know that it's gone.
        /// </summary>
        public void JudgeSeal()
        {
            Context.CastSpell("Paladin.Judge");
            Seal.ForceReady();
        }

        /// <summary>
        /// Cast a heal and use divine favor, if it's configured.
        /// </summary>
        /// <param name="FastHeal">True if we should go for quick, small heal</param>
        public void DoPaladinHeal(bool FastHeal)
        {
            if (UseDivineFavor && DivineFavor.IsReady)
            {
                DivineFavor.Reset();
                Context.CastSpell("Paladin.DivineFavor");
            }

            Context.CastSpell(FastHeal ? "Paladin.FastHeal" : "Paladin.Heal");
        }

        /// <summary>
        /// Do a heal and burn whatever cooldowns are necessary to get it off.
        /// </summary>
        public void DoEmergencyHeal(GMonster Target)
        {
            Context.Log("Doing emergency heal vs \"" + Target.Name + "\"");

            int Extras = GObjectList.GetAttackers().Length - 1;

            if (Hammer.IsReady && Extras == 0)
            {
                Hammer.Reset();
                Context.CastSpell("Paladin.HammerJustice");

                DoPaladinHeal(false);
                return;
            }

            if (DivineShield.IsReady)
            {
                DivineShield.Reset();
                Context.CastSpell("Paladin.DivineShield");

                DoPaladinHeal(false);
                return;
            }

            if (Interface.IsKeyReady("Common.Potion") && Interface.IsKeyEnabled("Common.Potion"))
            {
                Context.CastSpell("Common.Potion");
                return;
            }
            if (Interface.IsKeyReady("Paladin.LayHands"))
            {
                Context.CastSpell("Paladin.LayHands");
                return;
            }
            // No good options, try it anyway.
            DoPaladinHeal(false);
        }

        /// <summary>
        /// Small wrapper to wait for a PvP opponent to kill me - or just leave.
        /// </summary>
        void WaitForPlayerDeath()
        {
            GSpellTimer WaitTime = new GSpellTimer(60 * 1000);
            WaitTime.Wait();

            if (!Me.IsDead)
                Context.KillAction("PlayerLeftAlive", true);
        }

        //void TestChatHandle(string RawText, string ParsedText)
        //{
        //    Context.Log("Chat: \"" + RawText + "\"");
        //}

        //void TestCombatHandle(string RawText)
        //{
        //    Context.Log("Combat: \"" + RawText + "\"");
        //}

        #endregion

        #region Party helper overrides
        public override bool CheckPartyBuffs()
        {
            if (Context.Party.BuffParty("Paladin.BlessingOther", 280, GPlayerClass.Druid) ||
                Context.Party.BuffParty("Paladin.BlessingOther", 280, GPlayerClass.Hunter) ||
                Context.Party.BuffParty("Paladin.BlessingOther", 280, GPlayerClass.Rogue) ||
                Context.Party.BuffParty("Paladin.BlessingOther", 280, GPlayerClass.Shaman) ||
                Context.Party.BuffParty("Paladin.BlessingOther", 280, GPlayerClass.Warrior) ||
                Context.Party.BuffParty("Paladin.BlessingWisdomOther", 280, GPlayerClass.Mage) ||
                Context.Party.BuffParty("Paladin.BlessingWisdomOther", 280, GPlayerClass.Priest) ||
                Context.Party.BuffParty("Paladin.BlessingWisdomOther", 280, GPlayerClass.Warlock) ||
                Context.Party.BuffParty("Paladin.BlessingWisdomOther", 280, GPlayerClass.Paladin))
                return true;

            return false;
        }

        public override bool CheckPartyHeal(GUnit OriginalTarget)
        {
            if (!Context.Party.HealParty)  // Nope!
                return false;

            long[] PartyMembers = Context.Party.GetPartyMembers();

            double SmallHealCheck = .60;
            double BigHealCheck = .40;
            double ShieldCheck = .20;

            if (Me.IsUnderAttack)  // I'm under attack, only heal in bad situation.
            {
                SmallHealCheck = .30;
                BigHealCheck = .25;
                ShieldCheck = .15;
            }

            foreach (long OneGuy in PartyMembers)
            {
                GObject TargetObject = GObjectList.FindObject(OneGuy);

                if (TargetObject == null)  // Party member is not around, no big deal.
                    continue;

                GPlayer Member = (GPlayer)TargetObject;

                if (Member.Health < ShieldCheck && OriginalTarget != null && BOP.IsReady)
                {
                    Context.Party.CastOnMember(Member, "Paladin.BOPOther", OriginalTarget);
                    BOP.Reset();
                    // Keep going because we're going to heal him, anyway.
                }

                if (Member.Health < BigHealCheck)
                {
                    if (UseDivineFavor && DivineFavor.IsReady)
                    {
                        Context.CastSpell("Paladin.DivineFavor", true, true);
                        DivineFavor.Reset();
                    }

                    Context.Party.CastOnMember(Member, "Paladin.HealOther", OriginalTarget);
                    return true;
                }

                if (Member.Health < SmallHealCheck)
                {
                    Context.Party.CastOnMember(Member, "Paladin.FastHealOther", OriginalTarget);
                    return true;
                }
            }

            return false;
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
                    case "Paladin.FastHeal":         // Try FoL, then Holy Light.
                    case "Paladin.FastHealOther":
                        button = GShortcut.FindMatchingSpellGroup("0x4d26 0x27b");
                        break;

                    case "Paladin.Heal":         // Try Holy Light, then FoL.
                    case "Paladin.HealOther":
                        button = GShortcut.FindMatchingSpellGroup("0x27b 0x4d26");
                        break;

                    case "Paladin.DivineShield":         // Try real Divine Shield, take wienie Divine Protection if not.
                        button = GShortcut.FindMatchingSpellGroup("0x282 0x1f2");
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
