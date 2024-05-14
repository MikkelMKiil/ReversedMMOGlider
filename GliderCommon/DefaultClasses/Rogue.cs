
// Rogue.cs - default Glider class file.
//
// May 21 2007 MMD - Created base class from old rogue.
// Jun 24 2008 MMD - Added UpdateKeys for new sniffing.
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
    public class Rogue : GGameClass
    {
        #region Rogue props
        int SinisterCost;
        bool UseBackstab;
        bool UseColdblood;
        bool CheapShot;
        bool ChaseRunners;
        bool UseVanish;
        bool UseKick;
        bool UseStealth;
        bool UseRiposte;
        bool StealthNear;
        bool PoisonMain;
        bool PoisonOff;
        bool UseBladeFlurry;
        bool SaveBladeFlurry;
        bool UseKidneyShot;
        bool UseRush;
        bool UseGhostly;
        bool SaveRush;
        double KickLife;
        int EviscMultiplier;
        GSpellTimer StealthButton = new GSpellTimer(2 * 1000, true);
        GSpellTimer Stun = new GSpellTimer(4 * 1000);

        bool GotExtra;
        #endregion

        #region GGameClass overrides
        public override bool ShouldBuyWater { get { return false; } }

        public override string DisplayName
        {
            get { return "Rogue"; }
        }

        public override void Startup()
        {
           // Context.CombatLog += new GContext.GCombatLogHandler(TestCombatHandle);
        }

        public override void Shutdown()
        {
            //    Context.ChatLog -= new GContext.GChatLogHandler(TestChatHandle);
            //    Context.CombatLog -= new GContext.GCombatLogHandler(TestCombatHandle);
        }

        public override GConfigResult ShowConfiguration()
        {
            return Context.ShowStockConfigDialog(GPlayerClass.Rogue);
        }

        public override bool CanDrink
        {
            get { return false; }   // 12th step, no drinking.
        }

        public override string PowerLabel
        {
            get
            {
                return "Energy";
            }
        }

        public override string PowerValue
        {
            get
            {
                return Me.Energy + " (CP = " + Me.ComboPoints + ")";
            }
        }

        public override void CreateDefaultConfig()
        {
            Context.SetConfigValue("Rogue.PullDistance", "30", false);
            Context.SetConfigValue("Rogue.SinisterCost", "45", false);
            Context.SetConfigValue("Rogue.KickLife", "100", false);
            Context.SetConfigValue("Rogue.UseColdblood", "False", false);
            Context.SetConfigValue("Rogue.UseBackstab", "False", false);
            Context.SetConfigValue("Rogue.CheapShot", "False", false);
            Context.SetConfigValue("Rogue.EviscMultiplier", "10", false);
            Context.SetConfigValue("Rogue.UseVanish", "False", false);
            Context.SetConfigValue("Rogue.UseKick", "False", false);
            Context.SetConfigValue("Rogue.UseStealth", "True", false);
            Context.SetConfigValue("Rogue.StealthNear", "False", false);
            Context.SetConfigValue("Rogue.UseRiposte", "False", false);
            Context.SetConfigValue("Rogue.PoisonMain", "False", false);
            Context.SetConfigValue("Rogue.PoisonOff", "False", false);
            Context.SetConfigValue("Rogue.UseBladeFlurry", "False", false);
            Context.SetConfigValue("Rogue.SaveBladeFlurry", "False", false);
            Context.SetConfigValue("Rogue.UseKidneyShot", "False", false);
            Context.SetConfigValue("Rogue.UseRush", "False", false);
            Context.SetConfigValue("Rogue.SaveRush", "False", false);
            Context.SetConfigValue("Rogue.UseGhostly", "False", false);
            Context.SetConfigValue("Rogue.ChaseRunners", "True", false);
        }

        public override void LoadConfig()
        {
            KickLife = Context.GetConfigInt("Rogue.KickLife") / 100.0;
            SinisterCost = Context.GetConfigInt("Rogue.SinisterCost");
            UseColdblood = Context.GetConfigBool("Rogue.UseColdblood");
            UseBackstab = Context.GetConfigBool("Rogue.UseBackstab");
            CheapShot = Context.GetConfigBool("Rogue.CheapShot");
            ChaseRunners = Context.GetConfigBool("Rogue.ChaseRunners");
            UseVanish = Context.GetConfigBool("Rogue.UseVanish");
            UseKick = Context.GetConfigBool("Rogue.UseKick");
            UseStealth = Context.GetConfigBool("Rogue.UseStealth");
            StealthNear = Context.GetConfigBool("Rogue.StealthNear");
            UseRiposte = Context.GetConfigBool("Rogue.UseRiposte");
            PoisonMain = Context.GetConfigBool("Rogue.PoisonMain");
            PoisonOff = Context.GetConfigBool("Rogue.PoisonOff");
            UseBladeFlurry = Context.GetConfigBool("Rogue.UseBladeFlurry");
            SaveBladeFlurry = Context.GetConfigBool("Rogue.SaveBladeFlurry");
            UseKidneyShot = Context.GetConfigBool("Rogue.UseKidneyShot");
            UseRush = Context.GetConfigBool("Rogue.UseRush");
            UseGhostly = Context.GetConfigBool("Rogue.UseGhostly");
            SaveRush = Context.GetConfigBool("Rogue.SaveRush");
            EviscMultiplier = Context.GetConfigInt("Rogue.EviscMultiplier");
        }

        #endregion

        #region Poison stuff
        void CheckPoison(string Key, string Slot)
        {
            long ItemGUID = Context.Items.GetEquippedGUID(Slot);

            if (ItemGUID == 0)
            {
                Context.Log("No item equipped in \"" + Slot + "\"... !?");
                return;
            }

            int[] Enchants = Context.Items.GetItemEnchants(ItemGUID);

            foreach (int OneEnchant in Enchants)
            {
                string EnchantName = Context.Items.GetEnchantName(OneEnchant);

                if (EnchantName != null && EnchantName.ToLower().IndexOf("poison") > 0)   // Got some poison, nothing to see here.
                    return;
            }

            // Made it this far, the item needs poison!
            Context.Debug("Item needs poison in slot: " + Slot);

            int PoisonLeft = GContext.Main.Interface.GetActionInventory(Key);

            if (PoisonLeft > 0)
            {
                GInterfaceObject CharFrame = Context.Interface.GetByName("CharacterFrame");
                GInterfaceObject SlotSpot = Context.Interface.GetByName("Character" + Slot);

                if (CharFrame == null || SlotSpot == null)
                {
                    Context.Log("Interface angry, couldn't get CharacterFrame or Character" + Slot + "!");
                    return;
                }

                if (!CharFrame.IsVisible)
                {
                    Context.SendKey("Common.Character");
                    Thread.Sleep(1000);

                    if (!CharFrame.IsVisible)
                    {
                        Context.Debug("CharFrame never became visible after keystroke!");
                        return;
                    }
                }

                // Start poison application:
                Context.SendKey(Key);
                Thread.Sleep(500);
                SlotSpot.ClickMouse(false);
                Thread.Sleep(1000);

                // Get rid of the character screen and wait for channeling:
                Context.SendKey("Common.Character");
                Thread.Sleep(1000);

                GSpellTimer FutilePoison = new GSpellTimer(9000, false);

                while (!FutilePoison.IsReadySlow)
                {
                    if (!Me.IsCasting)
                        break;
                }
            }
        }

        public override bool Rest()
        {
            if (PoisonMain)
                CheckPoison("Rogue.Poison1", "MainHandSlot");

            if (PoisonOff)
                CheckPoison("Rogue.Poison2", "SecondaryHandSlot");

            return base.Rest();
        }

        public override void RunningAction()
        {
            Context.Debug("EnterStealth called from RunningAction");
            EnterStealth(false);
        }

        public override void EnterStealth(bool OverrideConfig)
        {
            if (Me.IsStealth || !Interface.IsKeyReady("Rogue.Stealth"))   // Can't go into stealth now.
                return;

            if (OverrideConfig ||  // Glider *really* wants to stealth, so we better.
                (UseStealth && !Context.IsCorpseNearby && !StealthNear))
            {
                Context.CastSpell("Rogue.Stealth");
                Thread.Sleep(333);
                Me.SetBuffsDirty();
            }     
        }
        #endregion

        #region KillTarget
        public override GCombatResult KillTarget(GUnit Target, bool IsAmbush)
        {
            bool IsClose = false;
            int UsableEnergy;

            GotExtra = false;

            if (Target.IsPlayer)
                return KillPlayer((GPlayer)Target);

            GMonster Monster = (GMonster)Target;

            if (!IsAmbush)
            {
                if (UseStealth && !Me.IsStealth && Target.DistanceToSelf > 10.0)    //  Drop into stealth for approach.
                {
                    Context.Debug("EnterStealth called from Combat1");
                    EnterStealth(true);
                }

                GCombatResult OpenerResult = DoRogueOpener(Monster);

                if (OpenerResult != GCombatResult.Unknown)   // Well, opener did something, so we're done.
                    return OpenerResult;
            }

            // Ok, combat is on, have at it:
            while (true)
            {
                Thread.Sleep(101);

                UsableEnergy = Me.Energy;

                if (UseKick && Target.Health < KickLife)   // He's below the kick line, better save some energy.
                    UsableEnergy -= 15;

                GCombatResult CommonResult = Context.CheckCommonCombatResult(Monster, IsAmbush);

                if (CommonResult != GCombatResult.Unknown)
                    return CommonResult;

                Monster.Face();

                double Distance = Monster.DistanceToSelf;

                if (Distance <= Context.MeleeDistance)
                    IsClose = true;

                if (Distance > Context.MeleeDistance)
                {
                    if (IsClose)
                    {
                        Context.Log("Monster is running");

                        if (ChaseRunners)
                            Target.Approach();
                        else
                            Context.CastSpell("Rogue.Ranged");
                    }
                    else
                        Target.Approach();

                    IsClose = false;
                    continue;
                }

                if (TicksSinceCombatStart > 2000)           // Wait a couple seconds before tweaking anything.
                    Context.Movement.TweakMelee(Monster);

           if (((Me.Health < .25 && Target.Health > .40) || Me.Health < .18) && UseVanish && Interface.IsKeyReady("Rogue.Vanish"))
                {
                    DoVanish();
                    return GCombatResult.Retry;
                }

                if ((Me.Health < .30 || (Me.Health < .5 && Target.Health > .40)) && Interface.IsKeyReady("Rogue.Evasion") &&
                    Target.IsInMeleeRange)
                {
                    Context.CastSpell("Rogue.Evasion");
                    continue;
                }

                // Check panic health:
                if (Me.Health < .2 && Interface.IsKeyReady("Common.Potion") && Target.Health > .1 && Interface.IsKeyEnabled("Common.Potion"))
                {
                    Context.CastSpell("Common.Potion");
                    continue;
                }
                // Most important non-panic move: interrupt.
                if (Target.IsCasting && UseKick && Me.Energy >= 25 && Interface.IsKeyReady("Rogue.Kick"))
                {
                    Context.CastSpell("Rogue.Kick");
                    continue;
                }

                // Maybe throw out AR or BF?
                if (CheckAdditional(Target))
                    continue;

                if (UseRush && Me.Energy < 30 && Target.Health > .70 && !SaveRush && Interface.IsKeyReady("Rogue.AdrenalineRush"))
                {
                    Context.CastSpell("Rogue.AdrenalineRush");
                    continue;
                }

                // Kidney shot stun time.
                if (Stun.IsReady && UseKidneyShot && Me.Energy >= 45 && Interface.IsKeyReady("Rogue.KidneyShot") &&
                    (Me.ComboPoints >= 4 || (Me.ComboPoints == 3 && Target.Health < .40)))
                {
                    Context.CastSpell("Rogue.KidneyShot");
                    Stun.Reset();
                    continue;
                }

                // Riposte?
                if (UseRiposte && Context.Interface.IsKeyEnabled("Rogue.Riposte") && UsableEnergy >= 10 && Interface.IsKeyReady("Rogue.Riposte"))
                {
                    Context.CastSpell("Rogue.Riposte");
                    continue;
                }

                // Evi before any CP-generating moves:
                if (UsableEnergy >= 35 &&
                    (Me.ComboPoints == 5 ||
                    (Me.ComboPoints * EviscMultiplier >= Target.Health * 100.0)))
                {
                    if (UseColdblood && Target.Health > .30 && Interface.IsKeyReady("Rogue.ColdBlood"))
                        Context.CastSpell("Rogue.ColdBlood");

                    Context.CastSpell("Rogue.Eviscerate");
                    continue;
                }

                // Gouge-n-bs?
                if (Me.Energy >= 90 && UseBackstab &&
                    Interface.IsKeyReady("Rogue.Gouge") && Me.ComboPoints < 4 && Stun.IsReady
                    && Monster.Health > .20)  // Time for some backstab.
                {
                    GougeAndBackstab(Monster);
                    continue;
                }

                // General spam: GS, SS, BF.
                //
                //

                if (UseGhostly && UsableEnergy >= 40 && Interface.IsKeyReady("Rogue.GhostlyStrike") && Stun.IsReady)
                {
                    Context.CastSpell("Rogue.GhostlyStrike");
                    continue;
                }

                if (UsableEnergy >= SinisterCost && Interface.IsKeyReady("Rogue.Sinister"))
                {
                    Context.CastSpell("Rogue.Sinister");
                    continue;
                }

                if (UseBladeFlurry && UsableEnergy >= 25 && !SaveBladeFlurry && Target.Health > .25 && Interface.IsKeyReady("Rogue.BladeFlurry"))
                {
                    Context.CastSpell("Rogue.BladeFlurry");
                    continue;
                }
            }
        }
        #endregion

        #region Rogue combat helpers
        GCombatResult DoRogueOpener(GMonster Monster)
        {
            if (CheapShot)
            {
                Context.Log("Approaching for melee opener");
                // TODO: try harder to get behind monster.
                if (!Monster.Approach(Context.MeleeDistance - 1.0, true))
                {
                    Context.Log("Never able to approach, giving up on this guy");
                    Context.ReleaseSpinRun();
                    return GCombatResult.Bugged;    // Couldn't get close, forget this guy altogether.
                }

                // We're close enough, send the opener fast before the guy is out of range.
                StartCombat();      // Call it manually here, since our approach may have used up sanity time.

                if (Monster.IsFacingAway && UseBackstab)
                    Context.SendKey("Rogue.Backstab");
                else
                {
                    Context.SendKey("Rogue.CheapShot");
                    Stun.Reset();                         // Don't hit KS too soon.
                }

                // Better stop running, too.
                Context.ReleaseSpinRun();
            }
            else
            {
                // No melee opener, so just toss our thrown out there, if we're not too close.
                if (Monster.DistanceToSelf > 8.0)
                {
                    Context.Log("Pulling monster with ranged attack");
                    Context.ReleaseSpinRun();
                    Context.CastSpell("Rogue.Ranged");
                }
                else
                {
                    // Too close for ranged, just walk up and pop it.
                    Context.Log("Monster is too close for ranged pull, opening with SS");
                    Monster.Approach();

                    if (Monster.IsFacingAway && UseBackstab)
                        Context.SendKey("Rogue.Backstab");
                    else
                        Context.SendKey("Rogue.Sinister");
                }
            }

            if (!WaitForEngage(Monster))
            {
                if (Monster.DistanceToSelf > PullDistance)  // Wandered out, no biggie.
                {
                    Context.Log("Wandered out of range during pull, will try again later");
                    return GCombatResult.Retry;
                }

                // Couldn't engage, this is not right.
                Context.Log("Couldn't engage combat with monster, giving up on this guy");
                return GCombatResult.Bugged;
            }

            return GCombatResult.Unknown;  // Good to go.
        }

        bool WaitForEngage(GMonster Monster)
        {
            GSpellTimer Futile = new GSpellTimer(8000, false);

            while (!Futile.IsReadySlow)
                if (Monster.IsMine || Monster.IsTagged || Monster.TargetGUID == Me.GUID || Me.ComboPoints > 0)   // He's on me, good!
                    return true;

            return false;
        }

        GCombatResult KillPlayer(GPlayer Player)
        {
            //TODO: vanish if we can and maybe we'll get lucky.
            Context.Log("Attacked by player, waiting to die off");

            GSpellTimer WaitTime = new GSpellTimer(60 * 1000);
            WaitTime.Wait();

            if (!Me.IsDead)
                Context.KillAction("PlayerLeftAlive", true);
            return GCombatResult.Died;
        }

        void GougeAndBackstab(GMonster Monster)
        {
            Monster.Face();

            int OldComboPoints = GPlayerSelf.Me.ComboPoints;
            GSpellTimer Gouge = new GSpellTimer(3700, false);
            GSpellTimer NoGouge = new GSpellTimer(1500, false);

            Context.CastSpell("Rogue.Gouge");

            // Give it a second to go:
            while (!NoGouge.IsReadySlow)
            {
                Thread.Sleep(101);

                if (Me.ComboPoints > OldComboPoints)
                    break;
            }

            if (Me.ComboPoints == OldComboPoints)  // Never got a cp.
                return;

            // Awesome, line this guy up for some backstab:
            Monster.GetBehind(false);
            Context.Movement.TweakMelee(Monster, false);
            Gouge.Wait();
            Context.CastSpell("Rogue.Backstab");
        }

        void DoVanish()
        {
            Context.Log("Looks bad, trying to vanish");

            // Sanity check to make sure something is fighting us.
            GUnit ClosestEnemy = GObjectList.GetNearestAttacker(0);

            if (ClosestEnemy == null)
                return;

            Context.SendKey("Rogue.Vanish");

            double Heading = ClosestEnemy.Location.GetHeadingTo(Me.Location);
            GContext.Main.Movement.SetHeading(Heading);

            GSpellTimer RunawayTime = new GSpellTimer(12 * 1000, false);
            Context.StartRun();

            while (!RunawayTime.IsReadySlow)
            {
                ClosestEnemy = GObjectList.GetNearestHostile(Me.Location, 0, true);

                if (ClosestEnemy == null)
                {
                    Context.Log("No closest enemy, done running");
                    break;
                }

                if (ClosestEnemy.DistanceToSelf >= 25.0)
                {
                    Context.Log("Closest enemy is far away, done running: " + ClosestEnemy.ToString() + ", distance = " + ClosestEnemy.DistanceToSelf);
                    break;
                }

                double NewHeading = ClosestEnemy.Location.GetHeadingTo(Me.Location);

                if (Math.Abs(GContext.Main.Movement.CompareHeadings(Me.Heading, NewHeading)) > .10)
                    Context.Movement.SetHeading(NewHeading);
            }

            if (RunawayTime.IsReady)
                Context.Log("Runaway time is all done");

            Context.ClearTarget();
            Context.ReleaseSpinRun();

            GSpellTimer LurkyTime = new GSpellTimer(45 * 1000, false);

            while (!LurkyTime.IsReadySlow)
            {
                if (Me.TargetGUID != 0 && Me.Target != null && Me.Target.TargetGUID == Me.GUID)
                {
                    Context.Log("Attacked out of vanish lurk time!");
                    return;
                }

                if (Me.Health >= GContext.Main.RestHealth)
                {
                    Context.Log("Health is back up after lurking in vanish");
                    break;
                }

                GUnit hostile = GObjectList.GetNearestHostile(Me.Location, 0, true);

                if (hostile == null || hostile.DistanceToSelf > 20.0)
                {
                    Context.Log("No hostile monsters close by, getting out of vanish lurk");
                    break;
                }
            }

            this.CheckBandageApply(false);
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

            if (!Extra.IsInMeleeRange)  // Too far.
            {
                GotExtra = false;
                return false;
            }

            if (!GotExtra)
            {
                GotExtra = true;
                Context.Log("Rogue: extra attacker detected");
            }

            if (UseRush && Interface.IsKeyReady("Rogue.AdrenalineRush"))
            {
                Context.CastSpell("Rogue.AdrenalineRush");
                UsedSpell = true;
            }

            if (UseBladeFlurry && Interface.IsKeyReady("Rogue.BladeFlurry") && Me.Energy >= 25)
            {
                Context.CastSpell("Rogue.BladeFlurry");
                UsedSpell = true;
            }

            return UsedSpell;
        }

        public override void Disengage(GUnit Target)
        {
            base.Disengage(Target);
            Context.CastSpell("Rogue.Feint");
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
                    case "Rogue.Backstab":  // Take mutilate if we have it somehow.
                        if (UseBackstab)
                            button = GShortcut.FindMatchingSpellGroup("0x531 0x35");
                        else
                            continue;
                        break;

                    case "Rogue.Ranged":
                        button = GShortcut.FindMatchingShortcut(GShortcutType.Spell, "0xacc 0xbca");
                        break;

                    case "Rogue.Poison1":
                        button = GShortcut.FindMatchingShortcut(GShortcutType.Item, "2892 2893 8984 8985 20844 22053 22054 6947 6949 6950 8926 8928 21927 3775 3776 5237 6951 9186");
                        break;

                    case "Rogue.Poison2":
                        button = GShortcut.FindMatchingShortcut(GShortcutType.Item, "2892 2893 8984 8985 20844 22053 22054 6947 6949 6950 8926 8928 21927 3775 3776 5237 6951 9186");
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