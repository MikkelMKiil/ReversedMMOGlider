// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GGameClass
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Threading;

namespace Glider.Common.Objects
{
    public abstract class GGameClass
    {
        private int _combatStartedTime;
        private string _sourceFileName;
        protected GContext Context;
        protected GInterfaceHelper Interface;
        protected GPlayerSelf Me;
        protected GMovement Movement;

        public GGameClass()
        {
            Context = GContext.Main;
            Movement = GContext.Main.Movement;
            Interface = GContext.Main.Interface;
            Me = Context.Me;
            _sourceFileName = null;
            _combatStartedTime = 0;
            CombatStartLocation = null;
        }

        public int TicksSinceCombatStart => Environment.TickCount - _combatStartedTime;

        public GLocation CombatStartLocation { get; private set; }

        public abstract string DisplayName { get; }

        public string SourceFileName
        {
            get => _sourceFileName;
            set
            {
                if (_sourceFileName != null)
                    return;
                _sourceFileName = value;
            }
        }

        public virtual bool IsSelectable => true;

        public virtual bool CanDrink => true;

        public virtual string PowerLabel => "Mana";

        public virtual string PowerValue => string.Format("{0} ({1}%)", Me.ManaPoints, (int)(Me.Mana * 100.0));

        public virtual int PullDistance => Context.GetConfigInt(Me.PlayerClass + ".PullDistance");

        public virtual bool CanStealth => false;

        public virtual bool ShouldBuyFood => true;

        public virtual bool ShouldBuyWater => true;

        public override string ToString()
        {
            return DisplayName;
        }

        public void StartCombat()
        {
            _combatStartedTime = Environment.TickCount;
            CombatStartLocation = Me.Location;
        }

        public virtual void OnAttach()
        {
            Me = Context.Me;
        }

        public virtual void Startup()
        {
        }

        public virtual void Shutdown()
        {
        }

        public virtual GConfigResult ShowConfiguration()
        {
            return GConfigResult.NotSupported;
        }

        public virtual void CreateDefaultConfig()
        {
        }

        public virtual void LoadConfig()
        {
        }

        public virtual bool ShouldRest()
        {
            if (Me.IsDead)
            {
                Logger.smethod_1("I'm dead, can't rest");
                return false;
            }

            return Me.Health < Context.RestHealth || (Me.Mana < Context.RestMana && CanDrink);
        }

        public virtual bool Rest()
        {
            var flag1 = false;
            var flag2 = false;
            if (Me.TargetGUID != 0L && Me.Target != null && !Me.Target.IsDead)
                return true;
            CheckBandageApply(false);
            if (Me.TargetGUID != 0L && Me.Target != null && !Me.Target.IsDead)
            {
                Logger.smethod_1("Got a target, can't rest after bandaging");
                return true;
            }

            if (Me.IsDead)
            {
                Logger.smethod_1("I'm dead, can't rest");
                return false;
            }

            if (GContext.Main.IsHostileNear(Me.Location))
            {
                Logger.LogMessage(MessageProvider.GetMessage(22));
                return false;
            }

            if (!ShouldRest())
                return false;
            if (Me.Health < Context.RestHealth)
                flag1 = true;
            if (Me.Mana < Context.RestMana && CanDrink)
                flag2 = true;
            if (flag1 && CanDrink && Me.Mana < Context.RestMana + 0.1)
                flag2 = true;
            if (flag2 && Me.Health < Context.RestHealth + 0.1)
                flag1 = true;
            if (!flag1 && !flag2)
                return false;
            LeaveForm();
            Me.WaitForCombat();
            if (flag1)
                Context.CastSpell("Common.Eat");
            if (flag2)
                Context.CastSpell("Common.Drink");
            StartupClass.CurrentGameClass.EnterStealth(false);
            var gspellTimer = new GSpellTimer(27000);
            var flag3 = !flag1;
            var flag4 = !flag2;
            while (!gspellTimer.IsReadySlow && (!flag4 || !flag3))
                if (!Me.IsUnderAttack)
                {
                    if (Me.Health >= 0.99)
                        flag3 = true;
                    if (Me.Mana >= 0.99)
                        flag4 = true;
                }
                else
                {
                    Logger.smethod_1("Got a target while waiting for food/drink effects!");
                    Context.SendKey("Common.Sit");
                    return true;
                }

            Context.SendKey("Common.Back");
            return false;
        }

        public bool CheckBandageApply(bool InCombat)
        {
            if ((Me.TargetGUID != 0L && !InCombat) || !(GClass61.gclass61_0.method_2("UseBandages") == "True") ||
                Me.HasWellKnownBuff("Bandaged"))
                return false;
            if (Me.Health * 100.0 < GClass61.gclass61_0.method_3("BandageHealth"))
            {
                Logger.LogMessage(MessageProvider.GetMessage(27));
                var actionInventory = Interface.GetActionInventory("Common.ApplyBandage");
                Logger.smethod_1(MessageProvider.smethod_2(28, actionInventory));
                if (actionInventory > 0)
                {
                    Thread.Sleep(200);
                    Context.CastSpell("Common.ApplyBandage", true, true);
                    Thread.Sleep(500);
                    Context.CastSpell("Common.TargetSelf", false, false);
                    if (InCombat)
                        Context.SendKey("Common.TargetLastHostile");
                    else
                        Context.ClearTarget();
                }
            }

            return true;
        }

        public virtual void TargetAcquired(GUnit Target)
        {
        }

        public virtual void ApproachingTarget(GUnit Target)
        {
        }

        public virtual bool CheckPartyHeal(GUnit OriginalTarget)
        {
            return false;
        }

        public virtual bool CheckPartyBuffs()
        {
            return false;
        }

        public virtual void OnStartGlide()
        {
        }

        public virtual void OnStopGlide()
        {
        }

        public virtual GCombatResult KillTarget(GUnit Target, bool IsAmbush)
        {
            return GCombatResult.RunAway;
        }

        public virtual void OnResurrect()
        {
        }

        public virtual void RunningAction()
        {
        }

        public virtual void LeaveForm()
        {
        }

        public virtual void EnterStealth(bool OverrideConfig)
        {
        }

        public virtual void Disengage(GUnit Target)
        {
            if (!Me.IsMeleeing)
                return;
            Context.SendKey("Common.ToggleCombat");
        }

        public virtual void ResetBuffs()
        {
        }

        public virtual void Patrol()
        {
        }

        public virtual void UpdateKeys(GKey[] UpdatableKeys)
        {
        }
    }
}