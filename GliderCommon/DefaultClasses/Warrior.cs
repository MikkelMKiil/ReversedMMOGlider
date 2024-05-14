using System;
using System.Threading;
using Glider.Common.Objects;

namespace Glider.Common.Objects
{
    public class Warrior : GGameClass
    {
        #region Warrior props
        GSpellTimer BattleShout = new GSpellTimer(2 * 60 * 1000);
        GSpellTimer Heroic;
        GSpellTimer Rend = new GSpellTimer(21 * 1000);
          
        bool GotExtra = false;
        bool UseShieldBash;
        bool UseHamstring;
        bool UseBloodrage;
        bool UseDemoralizing;
        bool UseCleave;
        bool ChaseRunners;
        bool UseOverpower;
        bool ChargePull;
        int HeroicRage;
        double ShieldBashLife;
        bool UseConcussion;
        double MeleeDistance;
        bool UseExecute;
        bool UseSunder;
        bool AvoidAdds;
        int AvoidAddDistance;
        bool UseMortalStrike;
        #endregion

        #region GGameClass overrides

        // Don't think we'll be needing any water...
        public override bool ShouldBuyWater { get { return false; } }

        public override string DisplayName
        {
            get { return "Warrior"; }
        }

        public override int PullDistance
        {
            get
            {
                if (ChargePull)
                    return 25;
                else
                    return base.PullDistance;
            }
        }

        public override void Startup()
        {
            //    Context.CombatLog += new GContext.GCombatLogHandler(TestCombatHandle);
        }

        public override void Shutdown()
        {
            //    Context.ChatLog -= new GContext.GChatLogHandler(TestChatHandle);
            //    Context.CombatLog -= new GContext.GCombatLogHandler(TestCombatHandle);
        }

        public override GConfigResult ShowConfiguration()
        {
            return Context.ShowStockConfigDialog(GPlayerClass.Warrior);
        }

        public override bool CanDrink
        {
            get { return false; }   // 12th step, no drinking.
        }

        public override string PowerLabel
        {
            get
            {
                return "Rage";
            }
        }

        public override string PowerValue
        {
            get
            {
                return Me.Rage.ToString();
            }
        }

        public override void CreateDefaultConfig()
        {
            Context.SetConfigValue("Warrior.PullDistance", "30", false);
            Context.SetConfigValue("Warrior.ChargePull", "False", false);
            Context.SetConfigValue("Warrior.UseExecute", "True", false);
            Context.SetConfigValue("Warrior.ChaseRunners", "True", false);
            Context.SetConfigValue("Warrior.UseBloodrage", "True", false);
            Context.SetConfigValue("Warrior.UseConcussion", "False", false);
            Context.SetConfigValue("Warrior.UseSunder", "False", false);
            Context.SetConfigValue("Warrior.UseHamstring", "True", false);
            Context.SetConfigValue("Warrior.UseDemoralizing", "True", false);
            Context.SetConfigValue("Warrior.HeroicRage", "15", false);
            Context.SetConfigValue("Warrior.HeroicCooldown", "6000", false);
            Context.SetConfigValue("Warrior.UseCleave", "False", false);
            Context.SetConfigValue("Warrior.UseOverpower", "True", false);
            Context.SetConfigValue("Warrior.UseShieldBash", "False", false);
            Context.SetConfigValue("Warrior.ShieldBashLife", "40", false);
            Context.SetConfigValue("Warrior.PullDistance", "30", false);
            Context.SetConfigValue("Warrior.MeleeDistance", "4.80", false);
            Context.SetConfigValue("Warrior.AvoidAdds", "True", false);
            Context.SetConfigValue("Warrior.AvoidAddDistance", "30", false);
            Context.SetConfigValue("Warrior.UseMortalStrike", "False", false);
        }

        public override void LoadConfig()
        {
            BattleShout.ForceReady();

            ChaseRunners = Context.GetConfigBool("Warrior.ChaseRunners");            
            HeroicRage = Context.GetConfigInt("Warrior.HeroicRage");
            Heroic = new GSpellTimer(Context.GetConfigInt("HeroicCooldown") * 1000, true);
            UseHamstring = Context.GetConfigBool("Warrior.UseHamstring");
            UseBloodrage = Context.GetConfigBool("Warrior.UseBloodrage");
            UseDemoralizing = Context.GetConfigBool("Warrior.UseDemoralizing");
            UseCleave = Context.GetConfigBool("Warrior.UseCleave");
            UseOverpower = Context.GetConfigBool("Warrior.UseOverpower");
            UseShieldBash = Context.GetConfigBool("Warrior.UseShieldBash");
            ShieldBashLife = Context.GetConfigDouble("Warrior.ShieldBashLife");
            ChargePull = Context.GetConfigBool("Warrior.ChargePull");
            UseConcussion = Context.GetConfigBool("Warrior.UseConcussion");
            MeleeDistance = Context.GetConfigDouble("Warrior.MeleeDistance");
            UseExecute = Context.GetConfigBool("Warrior.UseExecute");
            UseConcussion = Context.GetConfigBool("Warrior.UseConcussion");
            UseSunder = Context.GetConfigBool("Warrior.UseSunder");
            AvoidAdds = Context.GetConfigBool("Warrior.AvoidAdds");
            AvoidAddDistance = Context.GetConfigInt("Warrior.AvoidAddDistance");
            UseMortalStrike = Context.GetConfigBool("Warrior.UseMortalStrike");
        }

        #endregion

        #region KillTarget
        public override GCombatResult KillTarget(GUnit Target, bool IsAmbush)
        {
            int SundersLeft = 2;
            bool WasClose = false;
            bool UsedHamstring = false;
            bool UsedDemoralizing = false;
            bool BloodBefore = Context.RNG.Next() % 2 == 0;
            if (ChargePull)
                BloodBefore = false;

            if (Target.IsPlayer)
                return KillPlayer((GPlayer)Target);
            GMonster Monster = (GMonster)Target;
            double Distance = Target.DistanceToSelf;

            if (UseBloodrage && Interface.IsKeyReady("Warrior.Bloodrage") && Me.Health > .70 && BloodBefore)
            {
                Context.CastSpell("Warrior.Bloodrage");
                if (Target.DistanceToSelf > PullDistance - 1.0)
                    Target.Approach(PullDistance - 1.0);
            }
            if (!IsAmbush)
            {                
                if (ChargePull && !Me.IsInCombat)
                {
                    if (Target.DistanceToSelf < 8.0)
                    {
                        Monster.Approach();
                        Context.SendKey("Common.ToggleCombat");
                    }
                    else if (Interface.IsKeyReady("Warrior.Charge"))
                        {
                            if (!DoAndCheckCharge(Target))             // Never got there, fuggit.
                                return GCombatResult.Retry;
                        }
                }
                else
                {
                    if (Target.DistanceToSelf >= (MeleeDistance + 1.0))
                    {
                        Context.ReleaseSpinRun();
                        Context.CastSpell("Warrior.Ranged");
                        Target.WaitForApproach(Context.MeleeDistance, 5000);//wait for it....arm thy self!//wait for it....arm thy self!
                        Context.SendKey("Common.ToggleCombat");
                    }
                    else
                    {
                        Context.SendKey("Common.ToggleCombat");
                        Context.ReleaseSpinRun();
                    }
                }
            }
            else
            {
                Monster.Approach();
                if (Interface.IsKeyReady("Warrior.HeroicStrike") && Me.Rage >= HeroicRage)
                {
                    if (GotExtra && (UseCleave || Interface.IsKeyReady("Warrior.Thunderclap")))  // Bypass heroic strike in this case.
                    {
                        Context.CastSpell("Warrior.Thunderclap");
                    }
                    else
                    {
                        Heroic.Reset();
                        Context.CastSpell("Warrior.HeroicStrike");
                    }
                }
                else
                {
                    Context.SendKey("Common.ToggleCombat");
                }
                
            }

            Context.Debug("Going into warrior main loop");
            Context.ReleaseSpinRun();
            Rend.ForceReady();

            while (true)
            {
                Thread.Sleep(101);

                GCombatResult CommonResult = Context.CheckCommonCombatResult(Monster, IsAmbush);                

                if (CommonResult != GCombatResult.Unknown)
                    return CommonResult;

                Context.Movement.TweakMelee(Monster);

                Distance = Target.DistanceToSelf;

                if (Distance <= MeleeDistance + 1.0)
                    WasClose = true;

                if (Distance > (MeleeDistance + 1.0) && ChaseRunners)
                {
                    Monster.Approach(MeleeDistance - 2.0);
                    continue;
                }

                if (Distance > (MeleeDistance + 1.0) && WasClose && !ChaseRunners && Interface.IsKeyReady("Warrior.Ranged") && Distance < PullDistance)
                {
                    Thread.Sleep(666);
                    Context.ReleaseSpinRun();
                    Context.CastSpell("Warrior.Ranged");
                    continue;
                }

                if (UseBloodrage && Interface.IsKeyReady("Warrior.Bloodrage") && Me.Health > .70 && !BloodBefore)
                {
                    Context.CastSpell("Warrior.Bloodrage");
                    continue;
                }
                
                // Check for heal/panic first:
                if (Me.Health < .35 && Target.Health > .35 || Me.Health < .20)
                {
                    if (Interface.IsKeyReady("Common.Potion") && Interface.IsKeyEnabled("Common.Potion"))
                    {
                        Context.CastSpell("Common.Potion");
                        continue;
                    }
                }

                // Most important combat-move: hamstring!
                if (UseHamstring && Target.Health <= .50 && Me.Rage >= 10 && !UsedHamstring)
                {
                    UsedHamstring = true;
                    Context.CastSpell("Warrior.Hamstring");
                    continue;
                }
                

                if (UseShieldBash && Me.Rage >= 10)
                {
                    if (Target.IsCasting && Target.Health <= ShieldBashLife && Interface.IsKeyReady("Warrior.ShieldBash"))
                    {
                        Context.CastSpell("Warrior.ShieldBash");
                        continue;
                    }
                }

                if (CheckAdditional(Target))
                    continue;

                if (UseOverpower && Interface.IsKeyEnabled("Warrior.Overpower") && Me.Rage >= 5)
                {
                    Context.CastSpell("Warrior.Overpower");
                    continue;
                }

                if (UseDemoralizing && !UsedDemoralizing && Me.Rage >= 10)
                {
                    UsedDemoralizing = true;
                    Context.CastSpell("Warrior.DemoShout");
                    continue;
                }

                // Ok, combat moves.  First, keep battle shout up:
                if (BattleShout.IsReady && Me.Rage >= 10)
                {
                    BattleShout.Reset();
                    Context.CastSpell("Warrior.BattleShout");
                    continue;
                }

                // Execute if we can!
                if (UseExecute && Target.Health <= .20 && Me.Rage >= 15)
                {
                    Context.CastSpell("Warrior.Execute");
                    continue;
                }

                // No big stuff ready, see if rend is ticking:
                if (Interface.IsKeyReady("Warrior.Rend") && Me.Rage >= 10 && Target.Health > .45 && Rend.IsReady)
                {
                    Context.CastSpell("Warrior.Rend");
                    Rend.Reset();
                    continue;
                }

                if (UseConcussion && Interface.IsKeyReady("Warrior.Concussion") && Me.Rage >= 15)
                {
                    Context.CastSpell("Warrior.Concussion");
                    continue;
                }

                if (UseSunder && Me.Rage >= 15 && SundersLeft > 0)
                {
                    SundersLeft--;
                    Context.CastSpell("Warrior.SunderArmor");
                    continue;
                }

                // Spam something:
                if (UseMortalStrike && Me.Rage >= 30 && Interface.IsKeyReady("Warrior.MortalStrike"))
                {
                    Context.CastSpell("Warrior.MortalStrike");
                    continue;
                }

                // Spam something else:
                if (Interface.IsKeyReady("Warrior.HeroicStrike") && Me.Rage >= HeroicRage && Heroic.IsReady)
                {
                    Heroic.Reset();
                    Context.CastSpell("Warrior.HeroicStrike");
                    continue;
                }

                if (!Me.IsMeleeing)
                {
                    Context.SendKey("Common.ToggleCombat");
                    Context.ReleaseSpinRun();
                    continue;
                }

                // Extremely bored - maybe we should back up?
                if (AvoidAdds && Target.DistanceToSelf <= Context.MeleeDistance && !Target.IsCasting)
                    Movement.ConsiderAvoidAdds(AvoidAddDistance);
            }
        }
        #endregion

        #region Warrior combat helpers

        GCombatResult KillPlayer(GPlayer Player)
        {
            //TODO: Warrior stuff
             Context.Log("Attacked by player, waiting to die off");

            GSpellTimer WaitTime = new GSpellTimer(60 * 1000);
            WaitTime.Wait();

            if (!Me.IsDead)
                Context.KillAction("PlayerLeftAlive", true);
            return GCombatResult.Died;
        }

        public bool CheckAdditional(GUnit Target)
        {
            bool UsedSpell = false;
            GUnit Extra = GObjectList.GetNearestAttacker(Target.GUID);

            if (Extra == null)   // No extra.
            {
                GotExtra = false;
                return false;
            }

            if (!Extra.IsInMeleeRange)  // Too far to care about.
            {
                GotExtra = false;
                return false;
            }

            if (!GotExtra)
            {
                GotExtra = true;
                Context.Log("Additional attacker!");
            }

            if (Interface.IsKeyReady("Warrior.Thunderclap") && Me.Rage >= 20)
            {
                Context.CastSpell("Warrior.Thunderclap");
                return true;
            }

            if (UseCleave && Me.Rage >= 20)
            {
                Context.CastSpell("Warrior.Cleave");
                return true;
            }

            return false;
        }

        bool DoAndCheckCharge(GUnit Target)
        {
            GLocation Anchor = Me.Location;
            GSpellTimer ChargeWait = new GSpellTimer(2000, false);

            Context.CastSpell("Warrior.Charge");
            Context.ReleaseSpinRun();

            while (!ChargeWait.IsReadySlow)
            {
                if (Me.Location.GetDistanceTo(Anchor) >= 7.0)
                {
                    Context.Debug("Charge is moving us, excellent!");
                    Target.WaitForApproach(Context.MeleeDistance, 2000);
                    return true;
                }
            }

            Context.Log("Never seemed to get anywhere on charge");
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
                    case "Warrior.Ranged":
                        button = GShortcut.FindMatchingShortcut(GShortcutType.Spell, "0xacc 0xbca");
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