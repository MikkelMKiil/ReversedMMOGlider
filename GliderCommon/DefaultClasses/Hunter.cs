// Hunter.cs - default Glider class file.
//
// July 19 2007 H - Created base class from old Hunter.
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
    public class Hunter : GGameClass
    {
        #region Hunter props
        const int MINIMUM_AMMO = 30;
        int BackUpLimit;
        bool ApproachPull;
        bool GotAspect;
        bool DotShot;
        long LastMarkGUID;
        double MinHappiness;
        bool FoodAllGone = false;
        bool SetTrap = false;
        int WalkBackTime = 0;
        int BackUpCount;
        bool NoPet;
        bool UseIntimidate;
        bool UseWrath;
        bool SpecialUsed;
        bool SeparateGroups;
        bool PetAttack;
        bool AvoidMelee;
        bool StopOnAmmo;
        bool TrapAdds;
        double MeleeDistance;
        bool UsedFirst;
        int AddHP = 0;
        GUnit Add = null;
        bool ChangeTarget = false;
        bool AddPending = false;
        bool FeedMacro;
        bool UseViper;
        double ViperMin;
        //Spelltimers
        GSpellTimer PetFeedTimer = new GSpellTimer(30 * 1000); // 30 seconds
        GSpellTimer NoFeed = new GSpellTimer(30 * 1000); // 30 seconds
        GSpellTimer RepeatShotTimer;
        GSpellTimer HuntersMark = new GSpellTimer(120 * 1000); // 2 mintues
        GSpellTimer SecondShotTimer = new GSpellTimer(15 * 1000); // 15 seconds
        GSpellTimer Trap = new GSpellTimer(60 * 1000); // 1 minute
        GSpellTimer FD = new GSpellTimer(360 * 1000); // 6 minutes
        GSpellTimer RevivePet = new GSpellTimer(10 * 1000); // 10 seconds
        GSpellTimer MendPetCast = new GSpellTimer(15 * 1000);//15 seconds

        const int VIPER_BUFF = 34074;
        #endregion

        #region GGameClass overrides
        public override string DisplayName
        {
            get { return "Hunter"; }
        }

        public override GConfigResult ShowConfiguration()
        {
            return Context.ShowStockConfigDialog(GPlayerClass.Hunter);
        }

        public override void CreateDefaultConfig()
        {
            Context.SetConfigValue("Hunter.PullDistance", "35", false);
            Context.SetConfigValue("Hunter.DotShot", "True", false);
            Context.SetConfigValue("Hunter.ApproachPull", "False", false);
            Context.SetConfigValue("Hunter.MinHappiness", "70", false);
            Context.SetConfigValue("Hunter.SetTrap", "False", false);
            Context.SetConfigValue("Hunter.UseIntimidate", "False", false);
            Context.SetConfigValue("Hunter.UseWrath", "False", false);
            Context.SetConfigValue("Hunter.PetAttack", "True", false);
            Context.SetConfigValue("Hunter.AvoidMelee", "True", false);
            Context.SetConfigValue("Hunter.SeparateGroups", "False", false);
            Context.SetConfigValue("Hunter.NoPet", "False", false);
            Context.SetConfigValue("Hunter.StopOnAmmo", "True", false);
            Context.SetConfigValue("Hunter.MeleeDistance", "4.80", false);
            Context.SetConfigValue("Hunter.TrapAdds", "False", false);
            Context.SetConfigValue("Hunter.RepeatShotMS", "7000", false);
            Context.SetConfigValue("Hunter.FeedMacro", "False", false);
            Context.SetConfigValue("Hunter.BackUpLimit", "1", false);
            Context.SetConfigValue("Hunter.UseViper", "True", false);
            Context.SetConfigValue("Hunter.ViperMin", "30", false);
        }

        public override void LoadConfig()
        {
            DotShot = Context.GetConfigBool("Hunter.DotShot");
            SetTrap = Context.GetConfigBool("Hunter.SetTrap");
            UseIntimidate = Context.GetConfigBool("Hunter.UseIntimidate");
            UseWrath = Context.GetConfigBool("Hunter.UseWrath");
            ApproachPull = Context.GetConfigBool("Hunter.ApproachPull");
            MinHappiness = Context.GetConfigDouble("Hunter.MinHappiness") / 100.0;
            SeparateGroups = Context.GetConfigBool("Hunter.SeparateGroups");
            PetAttack = Context.GetConfigBool("Hunter.PetAttack");
            AvoidMelee = Context.GetConfigBool("Hunter.AvoidMelee");
            NoPet = Context.GetConfigBool("Hunter.NoPet");
            StopOnAmmo = Context.GetConfigBool("Hunter.StopOnAmmo");
            MeleeDistance = Context.GetConfigDouble("Hunter.MeleeDistance");
            TrapAdds = Context.GetConfigBool("Hunter.TrapAdds");
            RepeatShotTimer = new GSpellTimer(Context.GetConfigInt("Hunter.RepeatShotMS"));
            FeedMacro = Context.GetConfigBool("Hunter.FeedMacro");
            BackUpLimit = Context.GetConfigInt("Hunter.BackUpLimit");
            UseViper = Context.GetConfigBool("Hunter.UseViper");
            ViperMin = Context.GetConfigInt("Hunter.ViperMin") / 100.0;
            GotAspect = true;
        }

        #endregion

        public override bool Rest()
        {
            if (!GotAspect)
            {
                GotAspect = true;
                Context.CastSpell("Hunter.Aspect");
            }

            if (!Me.HasLivePet && !NoPet)
            {
                Context.ReleaseSpinRun();
                Context.CastSpell("Hunter.CallPet");

                Thread.Sleep(1000);    //wait a second for pet to arrive

                if (!Me.HasLivePet)
                    Context.CastSpell("Hunter.RevivePet");
            }

            CheckPet();

            return base.Rest();
        }

        public bool ViperIsUp
        {
            get
            {
                return Me.HasBuff(VIPER_BUFF);
            }
        }

        #region KillTarget
        public override GCombatResult KillTarget(GUnit Target, bool IsAmbush)
        {
            if (Target.IsPlayer)
                return KillPlayer((GPlayer)Target);
            GMonster Monster = (GMonster)Target;
            GSpellTimer NoPull = new GSpellTimer(10000);
            double LastHealth;
            bool Separate = false;
            SpecialUsed = false;
            if (Context.IsManualKill)
                Target.Face();
            NoPull.Reset();
            ChangeTarget = false;
            Add = null;
            UsedFirst = false;
            #region Pulling
            Context.Log("Pulling");
            #region notAmbushed
            if (!IsAmbush) // normal pull conditions
            {
                // Consider going Viper if we're low on mana and most everything else is fine.
                if (UseViper && Me.Mana <= ViperMin && Me.Health > .50 && Me.HasLivePet && !ViperIsUp) 
                {
                    Context.Log("Mana a bit low, switching to Viper");
                    Context.CastSpell("Hunter.Viper");
                }

                BackUpCount = 0;
                Context.Log("No Ambush");

                if (SeparateGroups && Target.DistanceToSelf > 20.0)
                {
                    GUnit NextClosest = GObjectList.GetNearestHostile(Target.Location, Target.GUID, false);

                    if (NextClosest != null && NextClosest.Location.GetDistanceTo(Target.Location) < 20.0)
                    {
                        Context.Log("Target has a friend, attempting to separate");
                        Separate = true;
                    }
                }

                #region Check Ammo
                if (StopOnAmmo && !HasEnoughAmmo)
                    Context.HearthSoon(true);
                #endregion

                #region Set Trap for PULL - mob is far
                if (SetTrap && Target.DistanceToSelf > 17.0 && !Me.IsInCombat)
                {
                    Target.Approach(PullDistance - 7, false);
                    if (PetAttack && Me.HasLivePet)
                    {
                        UseTrap(Target, true, true);
                    }
                    else
                    {
                        UseTrap(Target, false, true);
                    }

                }
                #endregion

                Context.ReleaseSpinRun();
                MarkTarget(Target);

                if (Target.DistanceToSelf > PullDistance - 1)
                {
                    if (ApproachPull)
                    {
                        if (SetTrap)
                        {
                            WalkBackTime = (int)(Target.DistanceToSelf - PullDistance) * 420;
                        }
                        else
                        {
                            Target.Approach(PullDistance - 1);
                            UseRange(Target);
                        }
                    }
                    else
                    {
                        UseRange(Target);
                    }
                }
                else
                    if (Target.DistanceToSelf > Context.RangedDistance)
                        UseRange(Target);

                if (!Target.Refresh(true) && Me.HasLivePet)
                {
                    Context.SendKey("Common.PetFollow");
                }

                if ((!Separate) && (Target.DistanceToSelf >= PullDistance && Target.DistanceToSelf < (PullDistance - 10)))  // Back up a bit.
                {
                    Target.Face();
                    Context.SendKey("Common.Back");
                    Thread.Sleep(1200);
                    Context.ReleaseKey("Common.Back");
                }

                LastHealth = Target.Health;

                Target.Face();

                #region Seperate
                if (Separate)
                {
                    Context.CastSpell("Hunter.Shoot");
                    Target.WaitForApproach(PullDistance + 5.0, 5000);

                    //if (!Target.Refresh(true) || Target.IsDead)

                    if (PetAttack)
                        Context.SendKey("Common.PetAttack");

                    if (Target.DistanceToSelf < (PullDistance + 5.0))
                    {
                        GSpellTimer BackSafe = new GSpellTimer(3500);
                        BackSafe.Reset();

                        Context.SendKey("Common.Back");
                        Thread.Sleep(700);

                        while (!BackSafe.IsReady)
                        {
                            if (!Target.Refresh(true) || Target.IsDead)
                            {
                                Context.ReleaseKey("Common.Back");
                                continue;
                            }

                            DoPetSpecial(Target);

                            if (Target.DistanceToSelf > PullDistance + 1.0)
                            {
                                DoPetSpecial(Target);

                                if (!SpecialUsed)
                                    Thread.Sleep(1500 + (Context.RNG.Next() % 1500));

                                break;
                            }

                            Thread.Sleep(50);
                        }

                        Context.ReleaseKey("Common.Back");
                    }
                    else
                    {
                        //give up
                    }
                }
                #endregion
                                
            }
            #endregion

            #region ambushed
            else
            {
                if (PetAttack && Me.HasLivePet)
                {
                    Context.SendKey("Common.PetAttack");
                }
                if (AvoidMelee)
                {
                    UseRange(Target);
                }
                else
                {
                    UseMelee(Target);
                }

            }
            #endregion
            #endregion

            #region Combat Loop
            GSpellTimer Sanity = new GSpellTimer(5000);
            Sanity.Reset();
            LastHealth = Target.Health;
            while (true)
            {   //Combat On  
                if (Me.HasLivePet)
                {
                    CheckAdditional(Target);
                }

                if (Me.Mana > .90 && ViperIsUp)
                {
                    Context.Log("Mana is topped up, going back to old aspect");
                    Context.CastSpell("Hunter.Aspect");
                    Thread.Sleep(501);
                    continue;
                }

                Thread.Sleep(101);
                // Sanity check.                
                GCombatResult CommonResult = Context.CheckCommonCombatResult(Monster, IsAmbush);
                if (CommonResult != GCombatResult.Unknown && !AddPending || CommonResult != GCombatResult.Unknown && Me.IsDead)
                {
                    AddPending = false;
                    return CommonResult;
                }

                if (Me.HasLivePet)
                    DoPetSpecial(Target);

                Thread.Sleep(100);
                //Context.Log("Count: " + BackUpCount);
                #region Checks and Balances

                #region Additonal Mob combat check
                if (ChangeTarget && Add != null)
                {
                    #region ChangeTarget
                    if (!Target.IsDead)
                    {
                        if (AddHP > Add.HealthPoints)
                        {
                            AddHP = -1; ;//dont check again
                            Context.SendKey("Common.PetAttack");
                            AddPending = true;
                        }
                    }
                    else if (!Add.IsDead)
                    {
                        Add.SetAsTarget(false);//changed target
                        Context.SendKey("Common.PetAttack");
                        AddPending = false;
                        return GCombatResult.SuccessWithAdd;

                    }
                    else if (Add.IsDead)
                    {
                        ChangeTarget = false;
                        AddPending = false;
                        return GCombatResult.SuccessWithAdd;
                    }
                    #endregion
                }
                #endregion

                #region Combat Pet Check
                if (Me.IsInCombat && Me.HasLivePet && Me.Pet.TargetGUID == 0)//pet isn't fighting - fetch
                {
                    Context.SendKey("Common.PetAttack");
                    continue;
                }

                if (!Target.Refresh(true) && Me.HasLivePet)
                {
                    Context.SendKey("Common.PetFollow");
                    continue;
                }
                if (Target.Health < LastHealth)
                {
                    LastHealth = Target.Health;
                }
                CheckCombatHealPet();
                #endregion

                #endregion

                #region Self Health Check

                if (Me.IsDead)
                {
                    GotAspect = false;
                    continue;
                }
                if (((Me.Health < .35 && Target.Health > .35 && Me.isBeingTargeted) || Me.Health < .20 && Me.isBeingTargeted))
                {
                    Context.Log("Me.Health: " + Me.Health);
                    if (Interface.IsKeyReady("Hunter.Feign"))
                    {
                        Context.CastSpell("Hunter.Feign");
                        FD.Reset();
                        bool mobNear = false;
                        bool hasPet = Me.HasLivePet;
                        if (hasPet)
                        {
                            while (Me.Health < .60 && Me.Pet.IsInCombat && !FD.IsReady && !mobNear && !Me.IsDead || Me.Health < .90 && !FD.IsReady && mobNear && !Me.IsDead)
                            {//stay down
                                GUnit NextClosest = GObjectList.GetNearestHostile(Target.Location, Target.GUID, false);
                                if (NextClosest != null && NextClosest.Location.GetDistanceTo(Me.Location) < PullDistance)
                                {
                                    mobNear = true;
                                }
                                Thread.Sleep(10 * 1000); // 10 seconds then recheck
                            }
                            return GCombatResult.Retry;
                        }
                        else
                        {
                            while (Me.Health < .60 && !FD.IsReady && !mobNear && !Me.IsDead || Me.Health < .60 && !FD.IsReady && mobNear && !Me.IsDead)
                            {//stay down
                                GUnit NextClosest = GObjectList.GetNearestHostile(Target.Location, Target.GUID, false);
                                if (NextClosest != null && NextClosest.Location.GetDistanceTo(Me.Location) < PullDistance)
                                {
                                    mobNear = true;
                                }
                                Thread.Sleep(10 * 1000); // 10 seconds then recheck
                            }
                            return GCombatResult.Retry;
                        }

                    }
                    else if (Interface.IsKeyReady("Common.Potion") && Interface.IsKeyEnabled("Common.Potion"))
                    {
                        Context.CastSpell("Common.Potion");
                        continue;
                    }
                }
                #endregion

                Target.Face();

                CheckDeadZone(Target);
                //in Melee range - fight 
                if (Target.DistanceToSelf <= MeleeDistance)
                {
                    if (AvoidMelee && BackUpCount <= BackUpLimit && Me.HasLivePet)
                    {
                        UseRange(Target);
                        Context.Log("Use Range");
                    }
                    else
                    {
                        UseMelee(Target);
                        Context.Log("Use Melee");

                    }
                    continue;
                }
                CheckDeadZone(Target);
                // Out of Melee and within pull distance 
                if (Target.DistanceToSelf <= PullDistance && Target.DistanceToSelf > MeleeDistance)
                {
                    UseRange(Target);
                    continue;
                }
                CheckDeadZone(Target);
                // Far away, plug away! 
                if (Target.DistanceToSelf > PullDistance)
                {
                    UseRange(Target);
                    continue;
                }
            }
            #endregion
        }
        #endregion

        #region Hunter combat helpers

        GCombatResult KillPlayer(GPlayer Player)
        {
            //TODO: Fix to kill players
            Context.Log("Attacked by player, waiting to die off");

            GSpellTimer WaitTime = new GSpellTimer(60 * 1000);
            WaitTime.Wait();

            if (!Me.IsDead)
                Context.KillAction("PlayerLeftAlive", true);

            return GCombatResult.Died;
        }
        public void CheckDeadZone(GUnit Target)
        {
            #region Deadzone
            if (Target.DistanceToSelf > MeleeDistance && Target.DistanceToSelf < Context.RangedDistance)
            {
                if (AvoidMelee && BackUpCount <= BackUpLimit && Me.HasLivePet)
                {
                    MoveBack(Target, Context.RangedDistance);
                    UseRange(Target);
                }
                else
                {
                    UseMelee(Target);
                }
            }
            #endregion
        }
        public bool CheckAdditional(GUnit Target)
        {
            bool GotMobs = false;
            GUnit Extra = GObjectList.GetNearestAttacker(Target.GUID);
            if (Extra != null && Extra.Tag != null)
            {
                Thread.Sleep(2000);
                if (Extra.IsTargetingMe && Extra.IsInMeleeRange)//pulled aggro from my pet?
                {
                    Context.Log("Tag: " + Extra.Tag);
                    if (Interface.IsKeyReady("Hunter.Feign"))
                    {
                        Context.CastSpell("Hunter.Feign");
                        Thread.Sleep(6000);//wait 8 seconds for add to aggro back to pet...
                        Context.SendKey("Common.Forward");
                    }
                }
            }
            if (Extra != null && Extra != Add && Extra.Tag == null)
            {
                Add = Extra;
                Context.Log("Extra attacker: " + Add.ToString());
                if (TrapAdds && Add.IsTargetingMe && Add.IsInMeleeRange)
                {
                    string PressedKey = "";
                    Add.SetAsTarget(false); //change target to add
                    Add.Tag = "Touched"; //tag em as an add
                    Context.ReleaseSpinRun();
                    //Check Bearing 
                    if (GPlayerSelf.Me.Target.Bearing < (Math.PI / 2) && GPlayerSelf.Me.Target.Bearing > (-Math.PI / 2))
                    {
                        Context.PressKey("Common.Back");
                        PressedKey = "Common.Back";
                    }
                    else
                    {
                        Context.PressKey("Common.Forward");
                        PressedKey = "Common.Forward";
                    }
                    Thread.Sleep(1000);
                    if (Interface.IsKeyReady("Hunter.TrapAdd"))// set trap while moving 
                    {
                        Context.SendKey("Hunter.TrapAdd");
                    }
                    Thread.Sleep(1000);
                    Target.SetAsTarget(true);//changed target back
                    Context.ReleaseKey(PressedKey);
                    ChangeTarget = true;
                    AddHP = Add.HealthPoints;
                    GotMobs = true;

                }
                else
                {
                    Add.SetAsTarget(false); //change target to add                    
                    ChangeTarget = true;
                    AddHP = Add.HealthPoints;
                    Context.SendKey("Common.PetAttack");
                    Add.Tag = "PetTouched";
                    Target.SetAsTarget(true);//changed target back
                    GotMobs = true;
                }
            }

            return GotMobs;
        }

        public override void Disengage(GUnit Target)
        {
            base.Disengage(Target);
        }
        #endregion

        #region Pet Helpers

        bool CheckForPetPresence()
        {
            if (NoPet)
                return false;

            if (Me.HasLivePet)
                return true;

            // Pet not here, try to call it first:
            Context.CastSpell("Hunter.CallPet");

            GSpellTimer PetWait = new GSpellTimer(6000, false);
            while (!PetWait.IsReadySlow)
            {
                if (Me.HasLivePet)
                    return true;
            }

            // Pet never showed up.  Try to res it:
            Context.CastSpell("Hunter.RevivePet");
            while (!PetWait.IsReadySlow)
            {
                if (Me.HasLivePet)
                    return true;
            }

            // Pet never arrived, give up.
            return false;
        }

        void CheckPet()
        {
            CheckForPetPresence();

            if (Me.Pet == null)  // No pet.
                return;

            // Check health:
            if (Me.HasLivePet && Me.Pet.Health < .50 && Me.Pet.DistanceToSelf < 20 && Interface.IsKeyReady("Hunter.MendPet") && MendPetCast.IsReady)
            {
                Context.CastSpell("Hunter.MendPet");
                MendPetCast.Reset();
            }

            // Check food:
            if (NoFeed.IsReady)
            {
                if (FoodAllGone)
                {
                    Context.Log("Out of Food!");
                    return;
                }

                if (Me.Pet.Happiness < MinHappiness && PetFeedTimer.IsReady && !Me.Pet.IsDead)
                {
                    Thread.Sleep(2000);
                    PetFeedTimer.Reset();
                    FeedPet();
                }
            }

            return;
        }
        public override void OnResurrect()
        {
            GotAspect = false;

            //pet?
            if (!Me.HasLivePet && !NoPet)
            {
                Context.ReleaseSpinRun();
                Context.CastSpell("Hunter.CallPet");

                Thread.Sleep(1000);    //wait a second for pet to arrive

                if (!Me.HasLivePet)
                    Context.CastSpell("Hunter.RevivePet");
            }
        }


        public void FeedPet()
        {
            if (FeedMacro)
            {
                Context.CastSpell("Hunter.FeedMacro");
                Thread.Sleep(500);
            }
            else
            {
                GInterfaceObject Backpack = Context.Interface.GetByName("ContainerFrame1");

                if (Backpack == null)
                    Context.Log("No backpack UI object!");

                int i;

                if (!Backpack.IsVisible)
                {
                    Context.SendKey("Common.Backpack");
                    Thread.Sleep(500);
                }

                for (i = 16; i >= 1; i--)
                    if (TryPetFeed(i) || Me.IsDead)
                    {
                        if (Backpack.IsVisible)
                        {
                            Context.SendKey("Common.Backpack");
                            Thread.Sleep(500);
                        }

                        return;
                    }

                // Never got it going.
                Context.SendKey("Common.Escape");
                FoodAllGone = true;
            }
        }

        public bool TryPetFeed(int Slot)
        {
            string Target = "ContainerFrame1Item" + Slot;

            GInterfaceObject Food = Context.Interface.GetByName(Target);

            if (Food == null)
            {
                Context.Log("Couldn't find UI object: " + Target);
                return false;
            }

            Context.SendKey("Hunter.FeedPet");
            Thread.Sleep(500);

            Context.Log("Feeding pet from: " + Target);

            Food.ClickMouse(false);

            double StartHappiness = Me.Pet.Happiness;

            // Script the stuff off:

            GSpellTimer HappyCheck = new GSpellTimer(5000);
            HappyCheck.Reset();

            while (!HappyCheck.IsReady)
            {
                if (Me.Pet.Happiness > StartHappiness)
                {
                    NoFeed.Reset();
                    return true;
                }

                Thread.Sleep(500);
            }

            return false;
        }

        public void CheckCombatHealPet()
        {
            if (Me.Pet == null)
                return;

            if (Me.Pet.IsDead)      // Doh.
                return;

            if (Me.Pet.Health > .50)   // Doing fine.
                return;

            if (!Interface.IsKeyReady("Hunter.MendPet") || !MendPetCast.IsReady)
                return;

            if (Me.Pet.DistanceToSelf > 19.0)
                Me.Pet.Approach(19.0);

            Context.CastSpell("Hunter.MendPet");
            MendPetCast.Reset();
        }

        public void DoPetSpecial(GUnit Target)
        {
            if (Me.Pet == null || Target.Health < .25 || SpecialUsed)
                return;

            if (UseWrath && Interface.IsKeyReady("Hunter.Wrath"))
            {
                SpecialUsed = true;
                Context.CastSpell("Hunter.Wrath");
                return;
            }
            if (UseIntimidate && Interface.IsKeyReady("Hunter.Intimidation"))
            {
                SpecialUsed = true;
                Context.CastSpell("Hunter.Intimidation");
                return;
            }
        }
        #endregion

        #region Fight Club
        public void UseTrap(GUnit Target, bool SendPet, bool RangePull)
        {
            if (RangePull)
            {
                Target.Face();
                if (Interface.IsKeyReady("Hunter.Trap"))
                {
                    MarkTarget(Target);
                    Context.CastSpell("Hunter.Trap");
                }
                else
                {
                    Thread.Sleep(6000);
                    MarkTarget(Target);
                    Context.CastSpell("Hunter.Trap");
                }

                if (Me.IsInCombat && SendPet)
                {
                    Context.SendKey("Common.PetAttack");
                    UseRange(Target);
                }
                else
                {
                    Context.CastSpell("Hunter.Shoot");
                    MoveBack(Me, PullDistance);
                }

            }
            else
            {
                if (Interface.IsKeyReady("Hunter.Trap"))
                {
                    Context.SendKey("Hunter.Trap");
                }
            }
            if (SendPet && !Me.Pet.IsInCombat && Me.HasLivePet)
            {
                Thread.Sleep(500);
                Context.SendKey("Common.PetAttack");
            }
        }
        public void MoveBack(GUnit Target, double DistanceBack)
        {
            GContext.Main.Movement.BackAway(Target, DistanceBack);
            BackUpCount++;
        }

        public void UseRange(GUnit Target)
        {
            Target.Face();
            Context.ReleaseSpinRun();
            if (PetAttack && Me.HasLivePet && !Me.Pet.IsInCombat)
            {
                Context.SendKey("Common.PetAttack");
            }
            if (Target.DistanceToSelf < Context.RangedDistance)//called range but to close-back up 
            {
                MoveBack(Target, Context.RangedDistance);
                Context.ReleaseSpinRun();
            }
            if (Target.DistanceToSelf > PullDistance)
            {
                Context.Log("Too far move up.");
                GContext.Main.Movement.MoveToUnit(Target, PullDistance - 1, false);
            }
            if (!UsedFirst)
            {
                Thread.Sleep(1000);
                if (Interface.IsKeyReady("Hunter.FirstShot"))
                {
                    Context.SendKey("Hunter.FirstShot");
                    UsedFirst = true;
                }
            }
            if (DotShot)
            {
                Thread.Sleep(2000);
                if (Interface.IsKeyReady("Hunter.SecondShot") && SecondShotTimer.IsReady && !ViperIsUp)
                {
                    Context.CastSpell("Hunter.SecondShot");
                    SecondShotTimer.Reset();
                }
            }
            if (Interface.IsKeyReady("Hunter.RepeatShot") && RepeatShotTimer.IsReady && !ViperIsUp)
            {
                Context.CastSpell("Hunter.RepeatShot");
                RepeatShotTimer.Reset();
            }
            else if (!Interface.IsKeyFiring("Hunter.Shoot"))
            {
                Context.CastSpell("Hunter.Shoot");
            }
        }

        public void UseMelee(GUnit Target)
        {
            Target.Face();
            Target.Approach(MeleeDistance - 2, false);
            if (PetAttack && Me.HasLivePet && !Me.Pet.IsInCombat)
            {
                Context.SendKey("Common.PetAttack");
            }
            if (Interface.IsKeyReady("Hunter.RaptorStrike"))
            {
                Context.CastSpell("Hunter.RaptorStrike");
            }
            else
            {
                Thread.Sleep(6000); // wait 6 seconds
                UseMelee(Target); //loop    
            }
        }

        public void MarkTarget(GUnit Target)
        {
            if (Target.GUID != LastMarkGUID)
            {
                Context.CastSpell("Hunter.Mark");
                LastMarkGUID = Target.GUID;
                Target.Tag = "Pulled";
            }
        }

        public bool HasEnoughAmmo
        {
            get
            {
                return Me.AmmoCount >= MINIMUM_AMMO;
            }
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
                    case "Hunter.Aspect":     // Aspect of the Hawk, then Viper, then Monkey.
                        button = GShortcut.FindMatchingSpellGroup("0x336d 0x851a 0x336b");
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
