// Shaman.cs - default Glider class file.
//
// June 11 2007 MMD - Imported from danbopes.
// June 23 2008 MMD - Added UpdateKeys for new sniffing.
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
using System.Collections.Generic;
using System.Threading;
using Glider.Common.Objects;

namespace Glider.Common.Objects
{
    public class Shaman : GGameClass
    {
        #region Internal properties for the shaman

        const double AVOID_ADD_HEADING_TOLERANCE = 1.04;  // About 120 (60 each way = PI/3) degree arc in front of us.
        const double SHOCK_RANGE = 20.0;
        const double PULL_MIN_DISTANCE = 20.0;

        GSpellTimer Enchants = new GSpellTimer(29 * 60 * 1000, true);
        GSpellTimer NatureSwiftness = new GSpellTimer(3 * 60 * 1000);
        GSpellTimer AddBackup = new GSpellTimer(4 * 1000);
        GSpellTimer Shield = new GSpellTimer(10 * 60 * 1000, true);

        string ShockMode;
        double ShockMana;
        double ShockLife;

        bool DoublePull;
        bool StartTotem;
        bool UseEarthbind;
        bool ShockRunners;
        bool UseSwiftness;
        bool FastMelee;
        bool ShockPull;
        bool UseHealTotem;
        bool UseStormstrike;
        bool ExtraShield;
        bool UseRage;
        bool AvoidAdds;
        bool UseStoneclaw;
        bool UseTotemicCall;
        bool UseLightningShield;
        bool DualWield;

        int AvoidAddDistance;

        GUnit HealTotem = null;
        GUnit FightTotem = null;
        GUnit EarthbindTotem = null;
        GUnit StoneClawTotem = null;

        long AddGUID;
        #endregion

        #region GGameClass overrides
        public override string DisplayName { get { return "Shaman"; } }


        public override GConfigResult ShowConfiguration()
        {
            Context.Debug("Shaman.ShowConfiguration");
            return Context.ShowStockConfigDialog(GPlayerClass.Shaman);
        }

        public override void ResetBuffs()
        {
            Enchants.ForceReady();
            Shield.ForceReady();
        }

        public override void CreateDefaultConfig()
        {
            Context.SetConfigValue("Shaman.PullDistance", "30", false);
            Context.SetConfigValue("Shaman.ShockLife", "100", false);
            Context.SetConfigValue("Shaman.ShockMana", "80", false);
            Context.SetConfigValue("Shaman.ShockMode", "Spam", false);
            Context.SetConfigValue("Shaman.ExtraShield", "False", false);
            Context.SetConfigValue("Shaman.FastMelee", "False", false);
            Context.SetConfigValue("Shaman.ShockPull", "False", false);
            Context.SetConfigValue("Shaman.ShootRunners", "True", false);
            Context.SetConfigValue("Shaman.StartTotem", "True", false);
            Context.SetConfigValue("Shaman.UseEarthbind", "False", false);
            Context.SetConfigValue("Shaman.UseHealTotem", "True", false);
            Context.SetConfigValue("Shaman.UseRage", "False", false);
            Context.SetConfigValue("Shaman.UseStormstrike", "False", false);
            Context.SetConfigValue("Shaman.UseSwiftness", "False", false);
            Context.SetConfigValue("Shaman.UseStoneclaw", "False", false);
            Context.SetConfigValue("Shaman.UseTotemicCall", "False", false);
            Context.SetConfigValue("Shaman.DualWield", "False", false);
            Context.SetConfigValue("Shaman.AvoidAddDistance", "30", false);
            Context.SetConfigValue("Shaman.AvoidAdds", "True", false);
            Context.SetConfigValue("Shaman.UseLightningShield", "True", false);
        }

        public override void LoadConfig()
        {
            ShockLife = (Context.GetConfigInt("Shaman.ShockLife") / 100.0);
            ShockMana = (Context.GetConfigInt("Shaman.ShockMana") / 100.0);
            ShockMode = Context.GetConfigString("Shaman.ShockMode");
            DoublePull = Context.GetConfigBool("Shaman.DoublePull");
            ShockPull = Context.GetConfigBool("Shaman.ShockPull");
            ShockRunners = Context.GetConfigBool("Shaman.ShootRunners");
            UseHealTotem = Context.GetConfigBool("Shaman.UseHealTotem");
            StartTotem = Context.GetConfigBool("Shaman.StartTotem");
            UseRage = Context.GetConfigBool("Shaman.UseRage");
            UseStoneclaw = Context.GetConfigBool("Shaman.UseStoneclaw");
            UseTotemicCall = Context.GetConfigBool("Shaman.UseTotemicCall");
            UseSwiftness = Context.GetConfigBool("Shaman.UseSwiftness");
            FastMelee = Context.GetConfigBool("Shaman.FastMelee");
            UseStormstrike = Context.GetConfigBool("Shaman.UseStormstrike");
            ExtraShield = Context.GetConfigBool("Shaman.ExtraShield");
            UseEarthbind = Context.GetConfigBool("Shaman.UseEarthbind");
            DualWield = Context.GetConfigBool("Shaman.DualWield");
            AvoidAddDistance = Context.GetConfigInt("Shaman.AvoidAddDistance");
            AvoidAdds = Context.GetConfigBool("Shaman.AvoidAdds");
            UseLightningShield = Context.GetConfigBool("Shaman.UseLightningShield");
        }

        public override void OnStartGlide()
        {
            // Lets make sure we have our enchantments on
        }

        public override bool Rest()
        {
            int FutileHeals = 5;

            while (Me.Health < Context.RestHealth && FutileHeals > 0 && !Me.IsUnderAttack)
            {
                FutileHeals--;

                DoShamanHeal();

                // Sleep a bit to make sure it takes effect before recasting.
                Thread.Sleep(601);
            }

            if (Enchants.IsReady)
            {
                Context.CastSpell("Shaman.Rockbiter");
                if (DualWield)
                    Context.CastSpell("Shaman.AltWeaponEnchant");
                Enchants.Reset();
            }

            //Our mana is at 100%, why waste it before combat?
            if (Me.Mana > .90 && Shield.IsReady && UseLightningShield)
            {
                Context.CastSpell("Shaman.LightningShield");
                Shield.Reset();
            }
            // Drink it up.
            return base.Rest();
        }

        // Careful to only cast one spell at a time here to avoid Glider overrunning
        // while you fire a bunch of spells.
        public override void RunningAction()
        {
            if (Context.RemoveDebuffs(GBuffType.Disease, "Shaman.CureDisease", false))
                return;
            if (Context.RemoveDebuffs(GBuffType.Poison, "Shaman.CurePoison", false))
                return;
        }
        #endregion

        #region Big override: KillTarget
        public override GCombatResult KillTarget(GUnit Target, bool IsAmbush)
        {
            Context.Debug("Shaman.KillTarget invoked, isAmbush = " + IsAmbush + ", Target = " + Target.ToString() + ", distance = " + Target.DistanceToSelf);

            GCombatResult CommonResult;
            GSpellTimer ExtraShieldTimer = new GSpellTimer(12 * 1000);
            GSpellTimer HealTimer = new GSpellTimer(6000);
            bool WasClose = false;

            AddGUID = 0;

            if (Target.IsPlayer)
            {
                Context.Log("Shaman can't kill players yet, sorry.");
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

            // Let go of keys if we're close enough.
            if (Monster.DistanceToSelf <= Context.MeleeDistance)
                Context.ReleaseSpinRun();

            if (!ShockPull && ((Target.DistanceToSelf > PULL_MIN_DISTANCE && !IsAmbush) ||
                (Target.Reaction == GReaction.Neutral && Target.TargetGUID == 0)))
            {
                Context.ReleaseSpinRun();

                Context.CastSpell("Shaman.LightningBolt");
                Target.Face();

                Context.SendKey("Common.ToggleCombat");

                //Lets put up our three wonderful round things
                if (Shield.IsReady && UseLightningShield)
                    Context.CastSpell("Shaman.LightningShield");
                else
                    Shield.ForceReady();

                GCombatResult PullCheck = Context.WaitForEngage(Monster, this.PullDistance);
                if (PullCheck == GCombatResult.Retry)
                {
                    Context.Log("Monster wandered outside Pull Distance, must have gone out of range");
                    return GCombatResult.Retry;
                }
                else if (PullCheck == GCombatResult.Bugged)
                {
                    return GCombatResult.Bugged;
                }

                if (!FastMelee)
                    Monster.WaitForApproach(PullDistance - 1, 2 * 1000);
            }
            else
            {
                if (Monster.DistanceToSelf > SHOCK_RANGE)
                    Monster.Approach(SHOCK_RANGE - 1, true);

                // Last checks before pulling, in case someone else tagged it:
                Monster.Refresh(true);
                if (!Monster.IsValid)
                    return GCombatResult.Vanished;

                if (Monster.IsTagged && !Monster.IsMine && !IsAmbush)
                    return GCombatResult.OtherPlayerTag;

                if (Monster.DistanceToSelf > SHOCK_RANGE && (ShockPull || IsAmbush))        // Couldn't approach... ?!
                    return GCombatResult.Bugged;

                // Pop 'em with a shock if they're close enough
                if (Interface.IsKeyReady("Shaman.EarthShock"))
                {
                    Context.CastSpell("Shaman.EarthShock");
                    Context.SendKey("Common.ToggleCombat");
                }
                else
                    Context.SendKey("Common.ToggleCombat");

                Monster.Approach(Context.MeleeDistance + (Monster.DistanceToSelf * .2));

                if (Shield.IsReady && UseLightningShield)
                    Context.CastSpell("Shaman.LightningShield");
                else
                    Shield.ForceReady();
            }

            Monster.Face();

            // Make sure this totem is up.
            if (StartTotem)
                FightTotem = ConfirmTotem("Shaman.StartTotem", FightTotem);

            if (!FastMelee)
                Monster.WaitForApproach(Context.MeleeDistance, 2 * 1000);

            // Make sure we're at melee distance here:
            Monster.Approach();

            // Combat loop waiting for guy to die.
            while (true)
            {
                Thread.Sleep(101);

                // See if any of the normal combat exits got triggered - bail out if so.
                CommonResult = Context.CheckCommonCombatResult(Monster, IsAmbush);

                if (CommonResult != GCombatResult.Unknown)
                    break;

                // Lets make sure we are still bashing skulls
                if (Target.DistanceToSelf <= Context.MeleeDistance && !Me.IsMeleeing)
                    Context.SendKey("Common.ToggleCombat");

                // If we're in melee range, remember it for later.
                if (Target.DistanceToSelf <= Context.MeleeDistance)
                    WasClose = true;

                // Check to see if he's running away:
                if (Target.DistanceToSelf > Context.MeleeDistance)
                {
                    if (WasClose)
                    {
                        WasClose = false;

                        if (ShockRunners && Target.Health < .20)   // Make sure it wasn't a punt or other confusion before judging...
                        {
                            if (Interface.IsKeyReady("Shaman.EarthShock"))
                            {
                                Context.Log("Shocking Runner");
                                Context.CastSpell("Shaman.EarthShock");
                                continue;
                            }
                            else
                            {
                                Context.Log("EarthShock not ready, chasing down runner");
                            }
                        }
                    }

                    // Track him down.
                    Monster.Approach();
                }

                // Fix up our positioning:
                Context.Movement.TweakMelee(Monster);

                // Extra attacker?
                if (CheckAdd(Monster))
                    continue;

                // Consider healing:
                if (HealTimer.IsReady)
                {
                    if ((Me.Health < .5 && Monster.Health > .40) ||
                        (Me.Health < .25 && Target.Health > .5) ||
                        (Me.Health < .15))
                    {
                        if (UseHealTotem)
                            HealTotem = ConfirmTotem("Shaman.HealTotem", HealTotem);

                        DoShamanHeal();
                        HealTimer.Reset();
                        continue;
                    }
                }

                // We should have cast heal, so if we are still low, use potion
                if (Me.Health < .10 && Interface.IsKeyReady("Common.Potion") && Interface.IsKeyEnabled("Common.Potion"))
                {
                    Context.CastSpell("Common.Potion");
                    continue;
                }

                // Earthbind on low health?
                if (UseEarthbind && Target.Health < .30)
                    EarthbindTotem = ConfirmTotem("Shaman.Earthbind", EarthbindTotem);

                // Consider earth shocking:
                if (ShouldShock(Monster))
                {
                    Context.CastSpell("Shaman.EarthShock");
                    continue;
                }

                // Should we Rage?
                if (UseRage && Interface.IsKeyReady("Shaman.Rage"))
                    if ((Me.Mana < .70 && Target.Health > .75) ||
                        (Me.Mana < .40 && Target.Health > .50))
                    {
                        Context.CastSpell("Shaman.Rage");
                        continue;
                    }

                // Stormstrike?
                if (UseStormstrike && Interface.IsKeyReady("Shaman.Stormstrike"))
                {
                    Context.CastSpell("Shaman.Stormstrike");
                    continue;
                }

                // Remove any debuffs that are specified as combat remove:
                if (Context.RemoveDebuffs(GBuffType.Poison, "Shaman.CurePoision", true) ||
                    Context.RemoveDebuffs(GBuffType.Disease, "Shaman.CureDisease", true))
                    continue;

                // Lets check if we need an extra shield
                if (ExtraShield && ExtraShieldTimer.IsReady && Target.Health > .25 && UseLightningShield)
                {
                    Context.CastSpell("Shaman.LightningShield");
                    ExtraShieldTimer.Reset();
                    continue;
                }

                // Still bored, maybe a party member needs healing?
                if (CheckPartyHeal(Target))
                    continue;

                // Make sure our starting totem is up if this monster has health left:
                if (StartTotem && Target.Health > .40)
                    FightTotem = ConfirmTotem("Shaman.StartTotem", FightTotem);


                // Extremely bored - maybe we should back up?
                if (AvoidAdds && AddBackup.IsReady && Target.DistanceToSelf <= Context.MeleeDistance && !Target.IsCasting)
                    ConsiderAvoidAdds();
            }

            if (CommonResult == GCombatResult.Success)
            {
                if (AddGUID != 0)
                {
                    GUnit Add = GObjectList.FindUnit(AddGUID);

                    if (Add == null)
                    {
                        Context.Log("! Could not find add after combat, id = " + AddGUID.ToString("x"));
                        return GCombatResult.Success;
                    }

                    if (!Add.SetAsTarget(false))
                    {
                        Context.Log("! Could not target add after combat, name = \"" + Add.Name + "\", id = " + Add.GUID.ToString("x"));
                        return GCombatResult.Success;
                    }

                    // Maybe fire off a quick heal.
                    if (Me.Health < GContext.Main.RestHealth - .20 && !Add.IsTargetingMe)
                        DoShamanHeal();

                    // Tell Glider to immediately begin wasting this guy and not rest:
                    CommonResult = GCombatResult.SuccessWithAdd;
                }
                else if (UseTotemicCall)
                {
                    //Time to check if there are still totems out there
                    if ((HealTotem != null && HealTotem.IsValid) ||
                        (FightTotem != null && FightTotem.IsValid) ||
                        (StoneClawTotem != null && StoneClawTotem.IsValid) ||
                        (EarthbindTotem != null && EarthbindTotem.IsValid))
                    {
                        //We have some totems, lets do come cleaning up
                        Context.CastSpell("Shaman.TotemicCall");
                        HealTotem = FightTotem = StoneClawTotem = EarthbindTotem = null;
                    }
                }

            }
            return CommonResult;
        }
        #endregion

        #region Helper methods for shaman

        /// <summary>
        /// See if we have an extra attacker and should do something about it.  If something was
        /// done, returns True.  Otherwise, False.
        /// </summary>
        /// <returns>If there was an add or not.</returns>
        bool CheckAdd(GMonster OriginalTarget)
        {
            if (AddGUID != 0 || !UseStoneclaw)  // Skip it if we already got an add or don't care.
                return false;

            GUnit Add = GObjectList.GetNearestAttacker(OriginalTarget.GUID);

            if (Add == null)
                return false;

            // Got an add!
            Context.Log("Additional attacker: \"" + Add.Name + "\", 0x" + Add.GUID.ToString("x") + ", throwing Stoneclaw down");

            AddGUID = Add.GUID;
            StoneClawTotem = ConfirmTotem("Shaman.StoneclawTotem", StoneClawTotem);
            return true;

        }
        bool ShouldShock(GUnit Monster)
        {
            if (
                ((ShockMode == "Spam" && Me.Mana > ShockMana) ||
                (ShockMode == "Interrupt" && Monster.IsCasting && Monster.Health <= ShockLife) ||
                (ShockMode == "Runners" && Monster.Health >= .70)) &&
                Interface.IsKeyReady("Shaman.EarthShock")
               )
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Internal shaman method for helping out as a healer.
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
                GCombatResult CommonResult = Context.CheckCommonCombatResult((GMonster)Target, IsAmbush);

                if (CommonResult != GCombatResult.Unknown)
                    return CommonResult;

                CheckPartyHeal(Target);
            }
        }

        /// <summary>
        /// Cast a heal and use Nature Swiftness, if it's configured and we are in combat
        /// and need a big heal.
        /// </summary>
        /// <param name="WithTotem">True if we want to drop totem while healing</param>
        public void DoShamanHeal()
        {
            if (UseSwiftness && Me.IsInCombat && NatureSwiftness.IsReady)
            {
                NatureSwiftness.Reset();
                Context.CastSpell("Shaman.NS");
                Thread.Sleep(600);
            }

            Context.CastSpell("Shaman.Heal");
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

        // Maybe someone is getting close and we should back it up?
        void ConsiderAvoidAdds()
        {
            GUnit[] adds = GObjectList.GetLikelyAdds();

            if (adds.Length == 0) // Well, that makes things simple...
                return;

            GUnit closestAdd = (GUnit)GObjectList.GetClosest(adds);

            // Somebody is close enough to maybe jump in.  If the monster is in front of us and close
            // enough, might be time to back it up.

            if (closestAdd.DistanceToSelf < AvoidAddDistance &&
                closestAdd.IsApproaching &&
                Math.Abs(closestAdd.Bearing) < AVOID_ADD_HEADING_TOLERANCE)
            {
                Context.Log("Possible add: \"" + closestAdd.Name + "\" (distance = " + closestAdd.DistanceToSelf + ", bearing = " + closestAdd.Bearing + "), backing up combat");
                AddBackup.Reset();
                GSpellTimer Futility = new GSpellTimer(3000);

                Context.PressKey("Common.Back");
                closestAdd.StartSpinTowards();

                while (!Futility.IsReadySlow)
                {
                    Context.PulseSpin();

                    if (Math.Abs(closestAdd.Bearing) < (Math.PI / 10))  // Fairly straight on.
                        Context.ReleaseSpin();

                    if (closestAdd.DistanceToSelf > AvoidAddDistance + 6.0)  // Slack space.
                        break;
                }

                Context.ReleaseSpin();
                Context.ReleaseKey("Common.Back");

                if (Futility.IsReady)
                    Context.Log("Backed up for max time, stopping");

                Thread.Sleep(601);

                AddBackup.Reset();
            }
        }

        #endregion

        #region Party helper overrides
        public override bool CheckPartyBuffs()
        {
            return false;
        }

        public override bool CheckPartyHeal(GUnit OriginalTarget)
        {
            if (!Context.Party.HealParty)  // Nope!
                return false;

            long[] PartyMembers = Context.Party.GetPartyMembers();

            double SmallHealCheck = .60;
            double BigHealCheck = .40;

            if (Me.IsUnderAttack)  // I'm under attack, only heal in bad situation.
            {
                SmallHealCheck = .30;
                BigHealCheck = .25;
            }

            foreach (long OneGuy in PartyMembers)
            {
                GObject TargetObject = GObjectList.FindObject(OneGuy);

                if (TargetObject == null)  // Party member is not around, no big deal.
                    continue;

                GPlayer Member = (GPlayer)TargetObject;

                if (Member.Health < BigHealCheck)
                {
                    Context.Party.CastOnMember(Member, "Shaman.HealOther", OriginalTarget);
                    return true;
                }

                if (Member.Health < SmallHealCheck)
                {
                    Context.Party.CastOnMember(Member, "Shaman.FastHealOther", OriginalTarget);
                    return true;
                }
            }

            return false;
        }

        // Make sure this totem is up and close.  If it's not, cast it and return it.
        GUnit ConfirmTotem(string SpellName, GUnit LastKnown)
        {
            if (LastKnown == null || !LastKnown.IsValid || LastKnown.DistanceToSelf > 20.0)
            {
                if (LastKnown == null)
                    Context.Debug("LastKnown = null for " + SpellName + ", recasting");
                else
                    Context.Debug("LastKnown no good for " + SpellName + ", recasting (distance = " + LastKnown.DistanceToSelf + ", debug = " + LastKnown.ToString());

                return CastTotem(SpellName);
            }
            else
                return LastKnown;
        }

        // Return a list of all my totems.
        GUnit[] GetMyTotems()
        {
            GUnit[] All = GObjectList.GetUnits();
            List<GUnit> MyTotems = new List<GUnit>();

            foreach (GUnit one in All)
                if (one.CreatedBy == Me.GUID)
                    MyTotems.Add(one);

            return MyTotems.ToArray();
        }

        /// <summary>
        /// Cast a totem spell and return the newly created totem.
        /// </summary>
        /// <returns>The GUnit Totem</returns>
        GUnit CastTotem(string SpellName)
        {
            if (!Interface.IsKeyPopulated(SpellName))  // Not there.
                return null;

            // Cast it:
            Context.CastSpell(SpellName);

            // Wait for a new totem.
            GSpellTimer Futility = new GSpellTimer(5000, false);

            while (!Futility.IsReadySlow)
            {
                GUnit[] NewTotems = GetMyTotems();

                foreach (GUnit totem in NewTotems)
                {
                    if (totem.Age < 1000)
                    {
                        Context.Debug("New totem is: " + totem.ToString());
                        return totem;
                    }
                }
            }

            // Never found it, damn.
            Context.Log("Never found new totem when casting, damn!");
            return null;
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
                    case "Shaman.StartTotem":
                        if (StartTotem)
                            button = GShortcut.FindMatchingSpellGroup("0xe0f 0x1f8b 0x1f87 0x2283");
                        else
                            continue;
                        break;

                    case "Shaman.Heal":
                        button = GShortcut.FindMatchingSpellGroup("0x1f44 0x14b");
                        break;

                    case "Shaman.HealOther":
                        button = GShortcut.FindMatchingSpellGroup("0x1f44 0x14b");
                        break;

                    case "Shaman.AltWeaponEnchant":
                        button = GShortcut.FindMatchingSpellGroup("0x2028 0x1f51 0x1f61");
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
