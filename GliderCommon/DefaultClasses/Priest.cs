// Priest.cs - default Glider class file.
//
// Ma6 21 2007 MMD - Added call for combat debuff removal.
// Apr 27 2007 MMD - Created base class from old priest.
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
    public class Priest : GGameClass
    {
        const double MIN_FLAY_DISTANCE = 10.0;

        #region Priest properties/config
        GSpellTimer Fortitude = new GSpellTimer(27 * 60 * 1000, false);         // Assume it's not totally fresh each time.
        GSpellTimer InnerFire = new GSpellTimer(5 * 60 * 1000, true);          // Five minutes if we don't find the id...
        int InnerFireBuffID = 0;
        GSpellTimer Mindblast;  // Duration set by config.
        GSpellTimer Pain;       // Duration set by config.
        GSpellTimer Shield;     // Duration set by config.
        GSpellTimer Renew = new GSpellTimer(15 * 1000);
        GSpellTimer FlashHeal = new GSpellTimer(10 * 1000);   // Avoid loop of spamming flash heal.
        GSpellTimer Fade = new GSpellTimer(30 * 1000);

        bool ShouldFlay;
        bool FlayRunners;
        bool ShouldWand;
        bool PreShield;
        bool AlwaysShield;
        bool SkipFlayRange;
        bool UseShadowform;
        bool UseVampiric;
        bool ExtraFlay;

        #endregion

        #region GGameClass overrides
        public override string DisplayName
        {
            get { return "Priest"; }
        }

        public override GConfigResult ShowConfiguration()
        {
            return Context.ShowStockConfigDialog(GPlayerClass.Priest);
        }

        public override void CreateDefaultConfig()
        {
            Context.SetConfigValue("Priest.ShadowWordCooldown", "18", false);
            Context.SetConfigValue("Priest.MindblastCooldown", "8", false);
            Context.SetConfigValue("Priest.MindFlay", "True", false);
            Context.SetConfigValue("Priest.UseWand", "True", false);
            Context.SetConfigValue("Priest.PullDistance", "30", false);
            Context.SetConfigValue("Priest.ShieldCooldown", "15", false);
            Context.SetConfigValue("Priest.PreShield", "True", false);
            Context.SetConfigValue("Priest.AlwaysShield", "False", false);
            Context.SetConfigValue("Priest.SkipFlayRange", "False", false);
            Context.SetConfigValue("Priest.FlayRunners", "True", false);
            Context.SetConfigValue("Priest.UseShadowform", "False", false);
            Context.SetConfigValue("Priest.UseVampiric", "False", false);
            Context.SetConfigValue("Priest.ExtraFlay", "False", false);
        }

        public override void LoadConfig()
        {
            Pain = new GSpellTimer(Context.GetConfigInt("Priest.ShadowWordCooldown") * 1000, true);
            Mindblast = new GSpellTimer(Context.GetConfigInt("Priest.MindblastCooldown") * 1000, true);
            Shield = new GSpellTimer(Context.GetConfigInt("Priest.ShieldCooldown") * 1000, true);
            ShouldFlay = Context.GetConfigBool("Priest.MindFlay");
            ShouldWand = Context.GetConfigBool("Priest.UseWand");
            PreShield = Context.GetConfigBool("Priest.PreShield");
            AlwaysShield = Context.GetConfigBool("Priest.AlwaysShield");
            SkipFlayRange = Context.GetConfigBool("Priest.SkipFlayRange");
            FlayRunners = Context.GetConfigBool("Priest.FlayRunners");
            UseShadowform = Context.GetConfigBool("Priest.UseShadowform");
            UseVampiric = Context.GetConfigBool("Priest.UseVampiric");
            ExtraFlay = Context.GetConfigBool("Priest.ExtraFlay");
        }

        public override void ResetBuffs()
        {
            Context.Log("Priest.ResetBuffs invoked, clearing out buffs");
            InnerFire.ForceReady();
            Fortitude.ForceReady();
        }

        public override bool Rest()
        {
            // Check to see if we should cast a heal spell here:

            if (!IsShadowform)
            {
                if (Me.Health < .6)
                    Context.CastSpell("Priest.RestHeal");
                else
                {
                    if (Me.Health < .8 && Renew.IsReady)
                    {
                        Context.CastSpell("Priest.Renew");
                        Renew.Reset();
                    }
                }
            }
            else
            {
                if (Me.Health < Context.RestHealth)
                {
                    Context.CastSpell("Priest.Shadowform");

                    if (Me.Health < .4)
                        Context.CastSpell("Priest.RestHeal");
                    else
                        Context.CastSpell("Priest.FlashHeal");

                    Thread.Sleep(1777);

                    if (Me.Health < .85 && Renew.IsReady)
                    {
                        Context.CastSpell("Priest.Renew");
                        Renew.Reset();
                    }
                }
            }

            if (InnerFireBuffID == 0)
            {
                // We don't know the buffid, try to guess it.
                Context.Log("Guessing inner fire buffid");
                Context.CastSpell("Priest.InnerFire");
                GSpellTimer Futile = new GSpellTimer(5000, false);

                while (!Futile.IsReadySlow)
                {
                    InnerFireBuffID = FindInnerFire();

                    if (InnerFireBuffID != 0)
                        break;
                }

                if (InnerFireBuffID == 0)  // Never found it.
                {
                    Context.Log("Never found inner fire buff, going with timer");
                    InnerFireBuffID = -1;
                }
                else
                    Context.Log("Inner fire buffid = 0x" + InnerFireBuffID.ToString("x"));
            }

            return base.Rest();
        }

        public override void RunningAction()
        {
            Context.Debug("Priest.RunningAction invoked");

            if (Context.RemoveDebuffs(GBuffType.Disease, "Priest.CureDisease", false) ||
                Context.RemoveDebuffs(GBuffType.Magic, "Priest.Dispel", false))
                return;

            // Got Inner Fire buffid but it's gone?  Recast.
            if (InnerFireBuffID > 0 && !Me.HasBuff(InnerFireBuffID))
            {
                Context.CastSpell("Priest.InnerFire");
                return;
            }

            // No buffid?  Check timer.
            if (InnerFireBuffID <= 0 && InnerFire.IsReady)
            {
                InnerFire.Reset();
                Context.CastSpell("Priest.InnerFire");
                return;
            }

            // Only cast fort if we have a ton of mana.
            Context.Debug("Fortitude.IsReady = " + Fortitude.IsReady + ", Me.Mana = " + Me.Mana);

            if (Fortitude.IsReady && Me.Mana > .4)   
            {
                Context.CastSpell("Priest.PWFort");
                Fortitude.Reset();
                return;
            }

            // Pop into shadowform.
            if (UseShadowform && !IsShadowform && Me.Mana > .40)  
            {
                Context.CastSpell("Priest.Shadowform");
                return;
            }
        }

        public override void ApproachingTarget(GUnit Target)
        {
            if (PreShield && Shield.IsReady)
            {
                Shield.Reset();
                Context.CastSpell("Priest.Shield");
            }
        }

        public override GCombatResult KillTarget(GUnit Target, bool IsAmbush)
        {
            GSpellTimer CombatCheck = new GSpellTimer(3000, false);
            Context.Debug("Priest.KillTarget invoked, isAmbush = " + IsAmbush + ", Target = " + Target.ToString() + ", distance = " + Target.DistanceToSelf);
            Context.ReleaseSpinRun();  // No charging in going on here.

            if (Target.IsPlayer)
                return KillPlayer((GPlayer)Target, GPlayerSelf.Me.Location);

            GMonster Monster = (GMonster)Target;

            Target.Face();

            if (!IsHealer)
            {
                if (AlwaysShield || PreShield)
                {
                    if (Shield.IsReady)
                    {
                        Shield.Reset();
                        Context.CastSpell("Priest.Shield");

                        if (Target.DistanceToSelf > PullDistance)
                            Target.Approach(PullDistance, false);
                    }
                }

                Mindblast.Reset();
                Context.CastSpell("Priest.MindBlast");

                if (UseVampiric && Me.Health < .85)
                {
                    if (Target.DistanceToSelf <= PullDistance)
                        Context.CastSpell("Priest.Vampiric");
                }

                if (ShouldFlay && Target.DistanceToSelf >= MIN_FLAY_DISTANCE)
                {
                    if (Target.DistanceToSelf > FlayPull && !SkipFlayRange)
                        Target.WaitForApproach(FlayPull, 3000);

                    if (Target.DistanceToSelf <= FlayPull)
                        Context.CastSpell("Priest.MindFlay");
                }

                GCombatResult PullCheck = Context.WaitForEngage(Monster, this.PullDistance);
                if (PullCheck != GCombatResult.Unknown)
                    return PullCheck;

                Pain.Reset();
                Context.CastSpell("Priest.SWPain");
            }

            if (!IsHealer && Target.IsTargetingMe && PreShield && Shield.IsReady)
            {
                Shield.Reset();
                Context.CastSpell("Priest.Shield");
            }

            // Before anything, see if we should throw on a quick renew:
            if (Me.Health < .8 && Renew.IsReady && !IsShadowform)
            {
                Context.CastSpell("Priest.Renew");
                Renew.Reset();
            }

            if (ShouldWand || IsHealer)
                Target.Approach(30.0, false);  // Wand range.

            if (ExtraFlay && Shield.TicksLeft > 5000 && Target.DistanceToSelf < FlayPull && !IsHealer)
                Context.CastSpell("Priest.MindFlay");

            if (IsShadowform)
                CheckHealthStuffShadowform(Target);
            else
                CheckHealthStuff(Target);

            bool IsClose = false;

            while (true)
            {
                Thread.Sleep(101);

                GCombatResult CommonResult = Context.CheckCommonCombatResult(Monster, IsAmbush);

                if (CommonResult != GCombatResult.Unknown)
                    return CommonResult;

                if (ShouldWand && (Target.Health < .8 || !IsHealer))
                    StartWand();

                Target.Face();

                double Distance = Target.DistanceToSelf;

                if (Distance <= Context.MeleeDistance)
                    IsClose = true;

                if (Distance > Context.MeleeDistance && IsClose && FlayRunners)
                {
                    IsClose = false;
                    StopWand();
                    Context.CastSpell("Priest.MindFlay");
                    continue;
                }

                if (CheckPartyHeal(Target))
                    continue;

                if (IsHealer)
                {
                    if (Target.Health < .90 && Target.Health > .10 && Pain.IsReady && !Interface.IsKeyFiring("Priest.Wand"))
                    {
                        Context.CastSpell("Priest.SWPain");
                        Pain.Reset();
                    }

                    if (Target.DistanceToSelf <= Context.MeleeDistance && !Me.IsMeleeing)
                        Context.CastSpell("Common.ToggleCombat", false, true);

                    CheckHealthStuff(Target);

                    if (Target.IsTargetingMe && IsHealer && Fade.IsReady)  // Monster is targeting me.  Damn.
                    {
                        Context.CastSpell("Priest.Fade");
                        Fade.Reset();
                    }

                    if (Me.Health < .85 && Renew.IsReady)
                    {
                        Context.CastSpell("Priest.Renew");
                        Renew.Reset();
                        continue;
                    }

                    if (Me.Health < .60)
                    {
                        Context.CastSpell("Priest.FlashHeal");
                        continue;
                    }

                    continue;
                }

                // Ok, combat moves:
                if (Mindblast.IsReady && Mindblast.Duration > 0)
                {
                    StopWand();

                    if (AlwaysShield && Shield.IsReady && Target.Health > .20)
                    {
                        Shield.Reset();
                        Context.CastSpell("Priest.Shield");
                    }

                    Context.CastSpell("Priest.MindBlast");
                    Mindblast.Reset();
                }

                // Maybe SW:P wore off?  Probably not good if it's taking this long...
                if (Pain.IsReady && Target.Health > .35)
                {
                    StopWand();

                    Pain.Reset();
                    Context.CastSpell("Priest.SWPain");
                }

                if (Target.DistanceToSelf <= Context.MeleeDistance && !Me.IsMeleeing && !ShouldWand)
                    Context.CastSpell("Common.ToggleCombat", false, true);

                // Put IF back up if we know the id and we're not wanding.
                if (InnerFireBuffID > 0 && !Me.HasBuff(InnerFireBuffID) && !Interface.IsKeyFiring("Priest.Wand"))
                    Context.CastSpell("Priest.InnerFire");

                if (Context.RemoveDebuffs(GBuffType.Disease, "Priest.CureDisease", true) ||
                    Context.RemoveDebuffs(GBuffType.Magic, "Priest.Dispel", true))
                    continue;

                if (IsShadowform)
                    CheckHealthStuffShadowform(Target);
                else
                    CheckHealthStuff(Target);
            }
        }
        #endregion

        #region Priest helpers
        bool IsShadowform
        {
            get
            {
                return Me.HasWellKnownBuff("Shadowform");
            }
        }

        int FindInnerFire()
        {
            GBuff[] Buffs = Me.GetBuffSnapshot();

            foreach (GBuff one in Buffs)
                if (one.ChargesLeft == 20)  // Looks like a fresh inner fire to me.
                    return one.SpellID;

            return 0;   // Never found it.
        }

        public bool CheckHealthStuffShadowform(GUnit Target)
        {
            if (AlwaysShield && Shield.IsReady && Target.Health > .25)
            {
                StopWand();
                Shield.Reset();
                Context.CastSpell("Priest.Shield");
                return true;
            }

            if ((Me.Health < .5 && Target.Health > .40) ||
                (Me.Health < .25))
            {
                if (Shield.IsReady)
                {
                    Shield.Reset();
                    Context.CastSpell("Priest.Shield");
                }

                Context.CastSpell("Priest.Shadowform");

                if (Me.Health < .2 && Interface.IsKeyReady("Common.Potion") && Interface.IsKeyEnabled("Common.Potion"))
                {
                    Context.CastSpell("Common.Potion");
                }

                if (Me.Health < .4)
                    Context.CastSpell("Priest.RestHeal");
                else
                    Context.CastSpell("Priest.FlashHeal");

                if (Me.Health < .85 && Renew.IsReady)
                {
                    Context.CastSpell("Priest.Renew");       // Throw this on.
                    Renew.Reset();
                }

                if (Me.Mana >= .50)
                    Context.CastSpell("Priest.Shadowform");

                return true;
            }

            return false;
        }

        public bool CheckHealthStuff(GUnit Target)
        {
            if (AlwaysShield && Shield.IsReady && Target.Health > .15)
            {
                StopWand();
                Shield.Reset();
                Context.CastSpell("Priest.Shield");
                return true;
            }

            // Check for mini-heal:
            if (Me.Health < .7 && Me.Health > .5 && FlashHeal.IsReady && Target.Health > .20)
            {

                FlashHeal.Reset();
                StopWand();
                if (Shield.IsReady)
                {
                    Shield.Reset();
                    Context.CastSpell("Priest.Shield");
                }

                Context.CastSpell("Priest.FlashHeal");
                return true;
            }

            // Check for big-time heal next:
            if ((Me.Health < .6 && Target.Health > .20) ||
                Me.Health < .4)
            {
                if (Shield.IsReady || AlwaysShield)   // Do the big heal.
                {
                    StopWand();
                    if (!AlwaysShield)
                    {
                        Shield.Reset();
                        Context.CastSpell("Priest.Shield");
                    }

                    Context.CastSpell("Priest.RestHeal");
                    return true;
                }

                // No shield, do flash heal.

                if (Me.Mana > .08)
                {
                    StopWand();
                    Context.CastSpell("Priest.FlashHeal");
                    return true;
                }

                if (Interface.IsKeyReady("Common.Potion") && Interface.IsKeyEnabled("Common.Potion"))
                {
                    StopWand();
                    Context.CastSpell("Common.Potion");
                    return true;
                }
            }

            // Check for light heal first:
            double RenewPct = .8;

            if (Context.Interface.IsKeyFiring("Priest.Wand"))   // Don't break out so easily if we're wanding.
                RenewPct = .6;

            if (Me.Health < RenewPct && Target.Health > .35)
            {
                if (Renew.IsReady)
                {
                    StopWand();
                    Context.CastSpell("Priest.Renew");
                    Renew.Reset();
                    return true;
                }
            }

            return false;
        }


        void StopWand()
        {
            if (!Context.Interface.IsKeyFiring("Priest.Wand"))  // Not firing, screw it.
                return;

            Context.CastSpell("Priest.Wand", false, false);
            Context.WaitForNotFiring("Priest.Wand");
        }

        void StartWand()
        {
            if (ShouldWand && !Context.Interface.IsKeyFiring("Priest.Wand"))
                Context.CastSpell("Priest.Wand");
        }

        bool IsHealer
        {
            get
            {
                return Context.Party.HealMode == GHealDisposition.Dedicated;
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
                double ShieldCheck = .25;

                if (IsHealer)         // Faster healing.
                    HealCheck = .65;

                if (OriginalTarget != null && OriginalTarget.IsTargetingMe)  // If it's hitting me, lower threshold...
                {
                    HealCheck = .30;
                    HotCheck = .50;
                    ShieldCheck = .15;
                }

                if (Guy.Health < ShieldCheck && OriginalTarget != null)   // Target check to make sure we don't shield out of combat.
                {
                    if (Context.Party.IsBuffReady(Guy, "Priest.ShieldOther", 30))
                    {
                        StopWand();
                        Context.Party.BuffPartyMember(Guy, "Priest.ShieldOther", 30, true);
                        Healed = true;
                    }
                }

                if (Guy.Health < HealCheck)
                {
                    string HealSpell = "Priest.FlashHealOther";

                    if (Guy.Health < ShieldCheck)
                        HealSpell = "Priest.RestHealOther";

                    StopWand();
                    Context.Party.CastOnMember(Guy, HealSpell, OriginalTarget);
                    Healed = true;
                }

                if (Guy.Health < HotCheck)
                {
                    if (Context.Party.IsBuffReady(Guy, "Priest.RenewOther", 15))
                    {
                        StopWand();
                        Context.Party.BuffPartyMember(Guy, "Priest.RenewOther", 15, true);
                        Healed = true;
                    }
                }
            }

            return Healed;
        }

        public override bool CheckPartyBuffs()
        {
            // PWFort for everyone except priests.
            return Context.Party.BuffParty("Priest.PWFortOther", (26 * 60), GPlayerClass.Druid) ||
                   Context.Party.BuffParty("Priest.PWFortOther", (26 * 60), GPlayerClass.Hunter) ||
                   Context.Party.BuffParty("Priest.PWFortOther", (26 * 60), GPlayerClass.Mage) ||
                   Context.Party.BuffParty("Priest.PWFortOther", (26 * 60), GPlayerClass.Paladin) ||
                   Context.Party.BuffParty("Priest.PWFortOther", (26 * 60), GPlayerClass.Shaman) ||
                   Context.Party.BuffParty("Priest.PWFortOther", (26 * 60), GPlayerClass.Rogue) ||
                   Context.Party.BuffParty("Priest.PWFortOther", (26 * 60), GPlayerClass.Warlock) ||
                   Context.Party.BuffParty("Priest.PWFortOther", (26 * 60), GPlayerClass.Warrior);
        }

        // Flay distance is a chunk of full shadow distance, so virtualize it:
        int FlayPull
        {
            get
            {
                return (int)(PullDistance * .6666);
            }
        }

        #endregion

        #region Kill player (not very good)
        GCombatResult KillPlayer(GPlayer Target, GLocation Anchor)
        {
            Context.Log("Attempting to kill player: " + Target.Name);
            bool Moved = false;

            GSpellTimer FutileCombat = new GSpellTimer(2 * 60 * 1000, true);
            GSpellTimer MFSpam = new GSpellTimer(5 * 1000, true);
            GSpellTimer MBSpam = new GSpellTimer(9 * 1000, true);
            GSpellTimer HealSpam = new GSpellTimer(9 * 1000, true);
            GCombatResult Result = GCombatResult.Bugged;

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

                // Check heal:
                if (Me.Health < .80 && Shield.IsReady)
                {
                    Context.CastSpell("Priest.Shield");
                    Shield.Reset();
                    continue;
                }

                if (Me.Health < .70 && Renew.IsReady && !IsShadowform)
                {
                    Context.CastSpell("Priest.Renew");
                    Renew.Reset();
                    continue;
                }

                if (Me.Health < .50 && HealSpam.IsReady && !IsShadowform)
                {
                    Context.CastSpell("Priest.FlashHeal");
                    HealSpam.Reset();
                    continue;
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

                // Attack:

                if (MBSpam.IsReady)
                {
                    Context.CastSpell("Priest.MindBlast");
                    MFSpam.Reset();
                    continue;
                }

                if (MFSpam.IsReady)
                {
                    Context.CastSpell("Priest.MindFlay");
                    MFSpam.Reset();
                    continue;
                }

                // Melee:
                if (Target.DistanceToSelf <= Context.MeleeDistance && !Me.IsMeleeing)
                {
                    Context.CastSpell("Common.ToggleCombat", false, true);
                    continue;
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
                    case "Priest.RestHeal":         // Try Greater Heal, then Heal, then Lesser Heal.
                    case "Priest.RestHealOther":
                        button = GShortcut.FindMatchingSpellGroup("0x80c 0x806 0x802");
                        break;

                    case "Priest.FlashHeal":         // Try Flash Heal, then Heal, then Lesser Heal.
                    case "Priest.FlashHealOther":
                        button = GShortcut.FindMatchingSpellGroup("0x80d 0x806 0x802");
                        break;

                    case "Priest.CureDisease":         // Promote to Abolish Disease if we got it.
                        button = GShortcut.FindMatchingSpellGroup("0x228 0x210");
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