// Warlock.cs - default Glider class file.
//
// Jul 12 2007 MMD - Created base class from old warlock.
// Jun 19 2008 MMD - Added in UpdateKeys method for new shortcut mania.
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
    enum PetType { Imp, Voidie, Felhunter, Succubus, Felguard, NoDemon };

    public class Warlock : GGameClass
    {
        const string BIG_HEALTHSTONE_LIST = "5510 19010 19011 5509 19008 19009 5511 19006 19007 9421 19012 19013 22103 22104 22105 5512 19004 19005";
        const int SOUL_SHARD_ITEMDEF = 0x1879;

        #region Warlock props
        // State stuff:
        GSpellTimer Armor = new GSpellTimer(28 * 60 * 1000, false);      // 28 minutes oughta be enough.
        GSpellTimer Curse = new GSpellTimer(24 * 1000, true);            // 24 seconds.
        GSpellTimer Corruption = new GSpellTimer(15 * 1000, true);       // 15 seconds.
        GSpellTimer SpellLock = new GSpellTimer(30 * 1000, true);        // 30 seconds
        bool GotHealthStone = false;                                     // Do we think we have a healthstone conjured?
        bool KitingFutile = false;                                       // Fear not working out any more?
        int BadFears;                                                    // How many times we've cast fear and had no joy.

        // Config stuff:
        PetType PType;
        bool PetAttack;
        bool UseDarkPact;
        bool ShardOneKill;
        int FarmShardCount;
        double SpellLockLife;
        bool UseReckless;
        bool FearAdds;
        bool Kite;
        bool UseNightfall;
        bool UseDeathcoil;
        bool UseWand;
        bool ThreeDotPull;
        bool UseSoulLink;
        bool StopShards;
        bool StopSummoning;
        bool RunPull;
        bool Jump;
        double FearRange;
        bool WantShard;
        #endregion

        #region GGameClass overrides
        public override string DisplayName
        {
            get { return "LockMan"; }
        }

        public override GConfigResult ShowConfiguration()
        {
            return Context.ShowStockConfigDialog(GPlayerClass.Warlock);
        }

        public override void LoadConfig()
        {
            ShardOneKill = Context.GetConfigBool("Warlock.ShardOneKill");
            PetAttack = Context.GetConfigBool("Warlock.PetAttack");
            UseDarkPact = Context.GetConfigBool("Warlock.DarkPact");
            UseReckless = Context.GetConfigBool("Warlock.Reckless");
            FearAdds = Context.GetConfigBool("Warlock.UseFear");
            Kite = Context.GetConfigBool("Warlock.Kite");
            ThreeDotPull = Context.GetConfigBool("Warlock.ThreeDotPull");
            UseWand = Context.GetConfigBool("Warlock.UseWand");
            UseNightfall = Context.GetConfigBool("Warlock.UseNightfall");
            UseDeathcoil = Context.GetConfigBool("Warlock.UseDeathcoil");
            UseSoulLink = Context.GetConfigBool("Warlock.UseSoulLink");
            StopShards = Context.GetConfigBool("Warlock.StopShards");
            RunPull = Context.GetConfigBool("Warlock.RunPull");
            Jump = Context.GetConfigBool("Warlock.Jump");

            PType = (PetType)Context.GetConfigInt("Warlock.Pet");
            FarmShardCount = Context.GetConfigInt("Warlock.FarmShards");
            SpellLockLife = Context.GetConfigDouble("Warlock.SpellLockLife");
            FearRange = Context.GetConfigDouble("Warlock.FearRange");

            if (FearRange == 0.0)   // Nothing set, calculate from pull.
                FearRange = Context.GetConfigDouble("Warlock.PullDistance") * .666;
        }

        public override void CreateDefaultConfig()
        {
            Context.AddAutoKey("Warlock.DemonArmor");
            Context.SetConfigValue("Warlock.PullDistance", "30", false);
            Context.SetConfigValue("Warlock.ShardOneKill", "True", false);
            Context.SetConfigValue("Warlock.PetAttack", "True", false);
            Context.SetConfigValue("Warlock.DarkPact", "True", false);
            Context.SetConfigValue("Warlock.Reckless", "False", false);
            Context.SetConfigValue("Warlock.UseFear", "False", false);
            Context.SetConfigValue("Warlock.Kite", "False", false);
            Context.SetConfigValue("Warlock.ThreeDotPull", "True", false);
            Context.SetConfigValue("Warlock.RunPull", "True", false);
            Context.SetConfigValue("Warlock.UseWand", "False", false);
            Context.SetConfigValue("Warlock.UseNightfall", "False", false);
            Context.SetConfigValue("Warlock.UseDeathcoil", "False", false);
            Context.SetConfigValue("Warlock.UseSoulLink", "False", false);
            Context.SetConfigValue("Warlock.StopShards", "False", false);
            Context.SetConfigValue("Warlock.FarmShards", "0", false);
            Context.SetConfigValue("Warlock.SpellLockLife", "50", false);
            Context.SetConfigValue("Warlock.Pet", "0", false);
            Context.SetConfigValue("Warlock.Jump", "True", false);
            Context.SetConfigValue("Warlock.FearRange", "0", false);
        }

        public override void OnResurrect()
        {
            Armor.ForceReady();
        }
        public override void ResetBuffs()
        {
            Armor.ForceReady();
        }

        public override void OnStartGlide()
        {
            StopSummoning = false;      // Try at least once before giving up...
            KitingFutile = false;
        }

        public override bool Rest()
        {
            if (!GotHealthStone && Me.Mana > .30)
            {
                Context.ReleaseSpinRun();
                Context.CastSpell("Warlock.CreateHealthstone");
                GotHealthStone = true;
            }

            if (PType != PetType.NoDemon && !Me.HasLivePet && Me.Mana > .30 && !StopSummoning)
            {
                Context.ReleaseSpinRun();
                Context.CastSpell("Warlock.SummonDemon");
                GSpellTimer DemonTimer = new GSpellTimer(6 * 1000, false);

                while (!DemonTimer.IsReadySlow)
                    if (Me.HasLivePet)
                        break;

                if (!Me.HasLivePet)
                {
                    Context.Log("Demon never showed up on summon - maybe you should change config assign \"NoDemon\" pet?");
                    StopSummoning = true;
                }
            }

            if (Me.HasLivePet && PType == PetType.Voidie && Me.Pet.Health < .50 && !Me.IsInCombat)
            {
                Context.ReleaseSpinRun();
                DoConsumeShadows();
            }

            if (Me.HasLivePet && Me.Pet.Health < .25)
            {
                Context.ReleaseSpinRun();
                Context.CastSpell("Warlock.HealthFunnel");
            }

            return base.Rest();
        }

        public override void RunningAction()
        {
            if (Armor.IsReady && Me.Mana > .30)
            {
                Context.CastSpell("Warlock.DemonArmor");
                Armor.Reset();
                return;
            }

            if (UseSoulLink && Me.HasLivePet)
            {
                if (!Me.HasWellKnownBuff("SoulLink"))
                {
                    Context.CastSpell("Warlock.SoulLink");
                    return;
                }
            }

            if (UseDarkPact && Me.HasLivePet && Me.Mana < .80)
            {
                Context.CastSpell("Warlock.DarkPact");
                return;
            }

            if (Me.Mana < .80 && Me.Health > .85)
            {
                Context.CastSpell("Warlock.Lifetap");
                return;
            }
        }
        #endregion

        #region Big kiting code
        void DoKite(GMonster Monster, bool WantShard, bool WasAmbush, bool ApplyThirdDot)
        {
            bool WasFar = false;
            double HarassRange = PullDistance;
            int AddSafeDistance = 30;  // Lotsa room for now, maybe change to: GContext.Main.GetConfigInt("LootSafeDistance") + 5;
            GSpellTimer JumpTimer = new GSpellTimer(3500);
            GSpellTimer FearTimer = new GSpellTimer(20 * 1000, false);
            bool ApproachOk;

            if (UseWand)
                HarassRange = 29.0;

            double StopRunDistance = HarassRange - 7.0;

            // See if we're close enough to kite.
            if (!Monster.Approach(FearRange))
                return;

            // Fear it!
            Context.CastSpell("Warlock.Fear");

            // See if he took off.
            if (!WaitForFear(Monster))
            {
                BadFears++;

                if (BadFears > 4)   // Not happening, forget it.
                    KitingFutile = true;

                return;
            }

            if (ApplyThirdDot)
            {
                Monster.Face();
                Context.CastSpell("Warlock.Immolate");
            }

            // Ok, time for more pain!
            if (Corruption.IsReady)
            {
                Corruption.Reset();
                Context.CastSpell("Warlock.Corruption");
            }

            bool PetEngaged = PetAttack;

            // Run him around a bit.
            while (!FearTimer.IsReadySlow)
            {
                Monster.Face(.17);

                if (Context.CheckCommonCombatResult(Monster, WasAmbush) != GCombatResult.Unknown)
                    break;

                if (Monster.DistanceToSelf > 15.0 && !WasFar)
                {
                    Context.Log("Monster is running away from fear");
                    WasFar = true;
                }

                if (Monster.IsInMeleeRange && WasFar)   // He came back, damn.
                {
                    Context.Log("Monster has returned from fear");
                    Context.ReleaseRun();
                    return;
                }

                ApproachOk = true;
                GUnit PossibleAdd = GObjectList.GetNearestHostile(Me.Location, Monster.GUID, false);

                if (PossibleAdd != null && PossibleAdd.DistanceToSelf < AddSafeDistance)  // Possible add, don't run.
                {
                    if (Context.IsRunning)
                    {
                        Context.Log("Getting close to possible add, stopping run: " + PossibleAdd.ToString() + ", distance = " + PossibleAdd.DistanceToSelf);
                        Context.ReleaseRun();
                    }

                    ApproachOk = false;
                }
                else
                {
                    if (PossibleAdd == null)
                    {
                        Context.Debug("No adds close enough to be afraid of");
                    }
                    else
                    {
                        Context.Debug("Closest add is ok: " + PossibleAdd.ToString() + ", distance = " + PossibleAdd.DistanceToSelf);
                    }
                }

                if (Monster.DistanceToSelf > HarassRange && ApproachOk)
                {
                    Context.StartRun();
                    continue;
                }

                if (Me.HasLivePet && !IsSuccubusBusy)
                {
                    GMonster PetAdd = GObjectList.GetNearestHostile(Me.Pet.Location, Monster.GUID, false);

                    if (PetEngaged)
                        if (PetAdd != null && PetAdd.Location.GetDistanceTo(Me.Pet.Location) < AddSafeDistance && !(PType == PetType.Imp && Monster.Location.GetDistanceTo(Me.Pet.Location) <= 30.0))
                        {
#if DEBUG
                            Context.Log("Calling off pet, will get too close to: " + PetAdd.ToString());
#endif
                            Context.SendKey("Common.PetFollow");
                            PetEngaged = false;
                        }

                    if (!PetEngaged)
                        if (PetAdd == null || PetAdd.Location.GetDistanceTo(Me.Pet.Location) > AddSafeDistance || (PType == PetType.Imp && Monster.Location.GetDistanceTo(Me.Pet.Location) <= 30.0))   // Far enough.
                        {
#if DEBUG
                            if (PetAdd == null)
                                Context.Log("Nobody close to pet, sending it");
                            else
                                Context.Log("Sending pet, closest monster: " + PetAdd.ToString());
#endif
                            Context.SendKey("Common.PetAttack");
                            PetEngaged = true;
                        }
                }

                // See if he went out of range.
                if (Monster.DistanceToSelf < StopRunDistance && Context.IsRunning)
                    Context.ReleaseRun();
                else
                {
                    if (JumpTimer.IsReady)
                    {
                        JumpTimer.Reset();
                        if (Context.IsRunning && Jump && Context.RNG.Next() % 10 == 0)
                        {
                            Context.SendKey("Common.Jump");
                            Thread.Sleep(900);
                        }
                    }
                }

                if (Monster.Health < (IsWandFiring ? .30 : .20) && WantShard && !Context.IsRunning && Monster.DistanceToSelf <= HarassRange)
                {
                    StopWand();
                    Context.CastSpell("Warlock.DrainSoul");
                    continue;
                }

                if (UseNightfall && Me.HasWellKnownBuff("Nightfall") && !IsWandFiring &&
                    Interface.IsKeyReady("Warlock.Shadowbolt") && Monster.DistanceToSelf < HarassRange)
                {
                    Context.CastSpell("Warlock.Shadowbolt");
                    continue;
                }

                if (Me.Mana < .80 && Me.Health > .80)
                {
                    Context.CastSpell("Warlock.Lifetap");
                    continue;
                }

                // Bottom of loop and we're not doing anything?!
                if (Monster.DistanceToSelf < HarassRange && !Context.IsRunning)
                {
                    if (ShouldLifeTap(Monster) && !IsWandFiring)
                        Context.CastSpell("Warlock.Lifetap");

                    if (UseWand)
                    {
                        if (!IsWandFiring)
                            Context.CastSpell("Warlock.Wand");

                        continue;
                    }
                    else
                    {
                        DrainLifeAndMaybeSoul(Monster, WantShard);
                    }
                }
            }

            Context.Log("Stopping kite on timer - good fear or something weird going on?");
            Context.ReleaseRun();
        }

        bool WaitForFear(GMonster Target)
        {
            GSpellTimer FearWait = new GSpellTimer(2000, false);
            GLocation Anchor = Target.Location;

            while (!FearWait.IsReadySlow)
            {
                if (Target.Location.GetDistanceTo(Anchor) > 5.0)   // He's running!
                    return true;
            }

            return false;
        }
        void DrainLifeAndMaybeSoul(GUnit Target, bool GetShard) //props blah
        {
            Context.CastSpell("Warlock.DrainLife", true, true);
            Thread.Sleep(500);//Wait for .5 seconds before checking for channeling.  
            GSpellTimer DrainLife = new GSpellTimer(5 * 1000, false);
            bool DrainSoulDuringDrainLife = false;//By default, don't do a drain soul.
            while (Context.Me.ChannelingSpellID != 0)
            {
                Thread.Sleep(100);
                if (Target.Health < .25 && GetShard)
                {
                    DrainSoulDuringDrainLife = true;
                    break;
                }
                if (DrainLife.IsReady)
                    break;
            }
            if (DrainSoulDuringDrainLife)
            {
                Context.Log("Breaking Drain Life in order to Drain Soul");
                Context.CastSpell("Warlock.DrainSoul");
            }
        }
        void DoConsumeShadows()
        {
            Context.Log("Trying buggy Consume shadows");
            int TriesLeft = 2;

            while (TriesLeft > 0)
            {
                double StartHealth = Me.Pet.Health;

                Context.SendKey("Warlock.ConsumeShadows");
                GSpellTimer ConsumeDone = new GSpellTimer(8000, false);
                GSpellTimer ConsumeStart = new GSpellTimer(3000, false);
                bool Started = false;

                while (!ConsumeDone.IsReadySlow)
                {
                    if (Me.IsUnderAttack)       // Guh!
                        return;

                    if (ConsumeStart.IsReady && !Started)   // Make sure it started.
                    {
                        if (Me.Pet.Health - StartHealth < .10)
                        {
                            Context.Log("Consume shadows doesn't seem to be starting, might try again");
                            break;
                        }
                    }
                }

                if (Me.Pet.Health > .90)
                {
                    Context.Log("Consume shadows looks like it worked, moving on");
                    Context.SendKey("Common.PetFollow");
                    return;
                }

                TriesLeft--;
            }
        }

        #endregion

        #region Warlock helpers
        int ShardCount
        {
            get
            {
                int Shards = 0;

                GItem[] Items = GObjectList.GetItems();

                foreach (GItem i in Items)
                {
                    Context.Debug(i.ToString());

                    if (i.ItemDefID == SOUL_SHARD_ITEMDEF)
                        Shards++;
                }

                return Shards;
            }
        }

        bool ShouldBeConcernedAboutHealth(GMonster Target)
        {
            return (Me.Health < .40 && Target.Health > .50) || Me.Health < .20;
        }

        bool IsWandFiring
        {
            get
            {
                return Interface.IsKeyFiring("Warlock.Wand");
            }
        }

        void StopWand()
        {
            if (IsWandFiring)
            {
                Context.CastSpell("Warlock.Wand", false, true);
                Context.WaitForNotFiring("Warlock.Wand");
            }
        }

        bool IsSuccubusBusy
        {
            get
            {
                return PType == PetType.Succubus && Me.HasLivePet && Me.Pet.TargetGUID != 0 && Me.Pet.IsCasting;
            }
        }

        bool ShouldLifeTap(GMonster Target)
        {
            if (Me.Mana > .80)  // No.
                return false;

            if (Me.Health < .70)   // No.
                return false;

            if (Target.TargetGUID == Me.GUID)  // It's attacking me, only tap if we have a LOT of health.
                return Me.Health > .90;
            else
                return Me.Health > .80;        // It's attacking pet, not so bad.
        }

        #endregion

        #region KillTarget big guy
        public override GCombatResult KillTarget(GUnit Target, bool IsAmbush)
        {
            GCombatResult CommonResult;
            WantShard = false;
            bool AlreadyCastThirdDot = false;
            GUnit Add = null;
            bool AlreadyCastReckless = false;
            bool Funneled = false;

            BadFears = 0;

            if (Context.IsManualKill)
                WantShard = ShardOneKill;

            if (FarmShardCount > 0 && FarmShardCount > ShardCount)
                WantShard = true;

            if (PetAttack)
                Context.SendKey("Common.PetAttack");

            if (Target.IsPlayer)
                return KillPlayer((GPlayer)Target, Me.Location);

            if (WantShard)
                Context.Log("Will attempt to get a shard from this monster");

            GMonster Monster = (GMonster)Target;

            if (IsAmbush && (Kite || FearAdds))
            {
                Context.ReleaseSpinRun();
                Context.CastSpell("Warlock.Fear");
            }

            if (!RunPull)
                Context.ReleaseSpinRun();
            else
                if (Jump && Context.RNG.Next() % 10 == 0 && Context.IsRunning)
                {
                    Context.SendKey("Common.Jump");
                    Thread.Sleep(200 + (Context.RNG.Next() % 300));
                    Context.ReleaseSpinRun();
                }

            // First DoT:
            if (RunPull && Context.Interface.IsKeyPopulated("Warlock.Curse"))
            {
                Context.SendKey("Warlock.Curse");
            }
            else
                Context.CastSpell("Warlock.Curse", true, false);

            Curse.Reset();

            // Maybe let go?
            Context.ReleaseSpinRun();
            Target.Face();

            // Sanity check.
            CommonResult = Context.CheckCommonCombatResult(Monster, IsAmbush);

            if (CommonResult != GCombatResult.Unknown)
            {
                if (!Target.IsDead && PetAttack)
                    Context.SendKey("Common.PetFollow");

                return CommonResult;
            }

            // Second DoT:
            Target.Approach(PullDistance - 1);
            Context.CastSpell("Warlock.Corruption");
            Corruption.Reset();

            // More sanity check.
            CommonResult = Context.CheckCommonCombatResult(Monster, IsAmbush);

            if (CommonResult != GCombatResult.Unknown)
            {
                if (!Target.IsDead && PetAttack)
                    Context.SendKey("Common.PetFollow");

                return CommonResult;
            }

            // Third dot, if configured.
            if (ThreeDotPull)
            {
                if (!Kite || Monster.TargetGUID != Me.GUID)
                {
                    Target.Approach(PullDistance - 1);
                    Target.Face();
                    Context.CastSpell("Warlock.Immolate");
                    AlreadyCastThirdDot = true;
                }
            }

            // TODO: make this optional backup?
            if (Jump && Target.DistanceToSelf > 10.0)
            {
                if (Context.RNG.Next() % 3 == 0)
                {
                    // Back up a bit.
                    Context.PressKey("Common.Back");
                    Thread.Sleep(500 + (Context.RNG.Next() % 300));

                    if (Context.RNG.Next() % 3 == 0)
                        Context.SendKey("Common.Jump");

                    Thread.Sleep(500 + (Context.RNG.Next() % 300));
                    Context.ReleaseKey("Common.Back");
                }
            }

            while (true)
            {
                Thread.Sleep(101);

                CommonResult = Context.CheckCommonCombatResult(Monster, IsAmbush);

                if (CommonResult != GCombatResult.Unknown)
                    break;

                if (Monster.IsInMeleeRange && !IsWandFiring)
                    Context.Movement.TweakSpell(Monster);

                Monster.Face();
                Monster.Approach(PullDistance);

                // Check my panic stuff first:
                if (ShouldBeConcernedAboutHealth(Monster))
                {
                    if (UseDeathcoil && Interface.IsKeyReady("Warlock.Deathcoil"))
                    {
                        StopWand();
                        Context.CastSpell("Warlock.Deathcoil");
                        continue;
                    }

                    if (GotHealthStone && Interface.IsKeyReady("Warlock.UseHealthstone"))
                    {
                        StopWand();
                        Context.CastSpell("Warlock.UseHealthstone");
                        GotHealthStone = false;
                        continue;
                    }

                    if (Me.HasLivePet && PType == PetType.Voidie)
                    {
                        Context.CastSpell("Warlock.VoidieSacrifice");
                        continue;
                    }

                    if (Interface.IsKeyReady("Common.Potion") && Interface.IsKeyEnabled("Common.Potion"))
                    {
                        StopWand();
                        Context.CastSpell("Common.Potion");
                        continue;
                    }
                }

                // Interrupt?
                if (PType == PetType.Felhunter && SpellLock.IsReady &&
                    Target.Health <= SpellLockLife && Me.HasLivePet && Target.IsCasting)
                {
                    Context.CastSpell("Warlock.SpellLock", false, true);
                    SpellLock.Reset();
                }

                if (Me.HasLivePet && Me.Pet.Health < .25 && !Funneled)
                {
                    Me.Pet.Approach(30.0);
                    Context.CastSpell("Warlock.HealthFunnel");
                    Funneled = true;                                // Only do it once per fight to avoid looping.
                    continue;
                }

                //! Look for extra attackers.
                GUnit Extra = GObjectList.GetNearestAttacker(Target.GUID);

                if (Extra != null && Extra != Add)
                {
                    Add = Extra;
                    Context.Log("Extra attacker: " + Add.ToString());

                    if (FearAdds)
                    {
                        Context.Log("Fearing add");

                        if (Extra.SetAsTarget(false))
                            Context.CastSpell("Warlock.Fear");

                        if (!Target.SetAsTarget(true))
                        {
                            Context.Log("!! Unable to re-acquire target after fearing add!");
                            return GCombatResult.Retry;
                        }
                    }
                    else
                    {
                        if (Me.HasLivePet && PType != PetType.Imp && PType != PetType.Succubus)   // Let 'em off-tank it.
                        {
                            Context.Log("Having demon pick up add");

                            if (Extra.SetAsTarget(false))
                                Context.SendKey("Common.PetAttack");

                            if (!Target.SetAsTarget(true))
                            {
                                Context.Log("!! Unable to re-acquire target after telling demon to get add!");
                                return GCombatResult.Retry;
                            }
                        }
                    }
                }

                // Looks good to me.  Consider stupid stuff, like kiting or spamming.
                if (Kite && !KitingFutile && Target.Health > .35 && Monster.DistanceToSelf < 20.0)
                {
                    StopWand();
                    DoKite(Monster, WantShard, IsAmbush, ThreeDotPull && !AlreadyCastThirdDot);
                    AlreadyCastThirdDot = true; // Definitely don't do this again.
                    continue;
                }

                if (Target.Health < (IsWandFiring ? .30 : .20) && WantShard)
                {
                    StopWand();

                    Context.CastSpell("Warlock.DrainSoul");
                    continue;
                }

                if (Target.Health < .35 && Target.Health > .05 && UseReckless && !IsWandFiring && !AlreadyCastReckless)
                {
                    AlreadyCastReckless = true;
                    Context.CastSpell("Warlock.Reckless");
                    continue;
                }

                if (UseNightfall && Me.HasWellKnownBuff("Nightfall") && !IsWandFiring && Interface.IsKeyReady("Warlock.Shadowbolt"))
                {
                    Context.CastSpell("Warlock.Shadowbolt");
                    continue;
                }

                if (Corruption.IsReady && Monster.Health > .25)
                {
                    Corruption.Reset();
                    Context.CastSpell("Warlock.Corruption");
                    continue;
                }

                if (Curse.IsReady && Monster.Health > .25)
                {
                    Curse.Reset();
                    Context.CastSpell("Warlock.Curse");
                    continue;
                }

                if (UseWand && Me.Health > .35)
                {
                    if (!IsWandFiring)
                    {
                        Monster.Approach(29.0);
                        Context.CastSpell("Warlock.Wand");
                    }
                }
                else
                {
                    if (ShouldLifeTap(Monster))
                        Context.CastSpell("Warlock.Lifetap");

                    DrainLifeAndMaybeSoul(Monster, WantShard);
                }
            }

            if (!Target.IsDead && PetAttack && !IsSuccubusBusy)
                Context.SendKey("Common.PetFollow");

            if (IsSuccubusBusy && Me.Pet.Target != null && Me.Pet.Target.IsMonster)   // She's got someone...
            {
                Context.Log("Succubus appears busy, taking her target as add");
                Add = (GMonster)Me.Pet.Target;
                CheckBandageApply(true);
            }

            if (Add != null && CommonResult == GCombatResult.Success)
            {
                Context.Log("We got an add, get to it");

                Add.SetAsTarget(true);
                CommonResult = GCombatResult.SuccessWithAdd;
            }

            if (Context.IsRunning)
            {
                Context.Log("Leaving combat with running key down - will it continue?");
            }

            return CommonResult;
        }
        #endregion

        #region KillPlayer
        GCombatResult KillPlayer(GPlayer Target, GLocation Anchor)
        {
            Context.Log("Attempting to kill player: " + Target.Name);
            Context.ReleaseSpinRun();
            bool Moved = false;

            GSpellTimer FutileCombat = new GSpellTimer(2 * 60 * 1000, true);
            GCombatResult Result = GCombatResult.Bugged;

            Context.CastSpell("Warlock.Curse");
            Target.Face();
            Context.CastSpell("Warlock.Corruption");

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

                if (Context.RNG.Next() % 2 == 1)
                    Context.CastSpell("Warlock.Shadowbolt");
                else
                    DrainLifeAndMaybeSoul(Target, WantShard);
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
                    case "Warlock.DemonArmor":        // Look for Demon Armor first...
                        button = GShortcut.FindMatchingSpellGroup("0x2c2 0x2af");
                        break;

                    case "Warlock.Immolate":        // Use Siphon Life instead, if spec'd that way.  Otherwise, take Immolate.
                        button = GShortcut.FindMatchingSpellGroup("0x4759 0x15C");
                        break;

                    case "Warlock.SummonDemon":     // Pick the demon to match user selection.
                        switch (PType)
                        {
                            case PetType.Imp: button = GShortcut.FindMatchingShortcut(GShortcutType.Spell, 0x2b0); break;
                            case PetType.Voidie: button = GShortcut.FindMatchingShortcut(GShortcutType.Spell, 0x2B9); break;
                            case PetType.Succubus: button = GShortcut.FindMatchingShortcut(GShortcutType.Spell, 0x2C8); break;
                            case PetType.Felhunter: button = GShortcut.FindMatchingShortcut(GShortcutType.Spell, 0x2B3); break;
                            case PetType.Felguard: button = GShortcut.FindMatchingShortcut(GShortcutType.Spell, 0x75C2); break;
                            case PetType.NoDemon: One.SIM = 0; continue;   // No big deal, skip complaint.
                        }
                        break;

                    case "Warlock.UseHealthstone":
                        button = GShortcut.FindMatchingShortcut(GShortcutType.Item, BIG_HEALTHSTONE_LIST);
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