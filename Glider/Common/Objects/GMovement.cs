// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GMovement
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Threading;

namespace Glider.Common.Objects
{
    public class GMovement
    {
        private const double MELEE_RANGE_TWEAK_MINIMUM = 3.0;
        private const double MELEE_HEADING_TWEAK_MINIMUM = 0.785;
        private const double AVOID_ADD_HEADING_TOLERANCE = 1.04;
        private static readonly GSpellTimer LastTweak = new GSpellTimer(2200);
        private readonly GSpellTimer AddBackup = new GSpellTimer(4000);

        public void TweakMelee(GUnit Target)
        {
            TweakMelee(Target, true);
        }

        public void TweakMelee(GUnit Target, bool IncludeCombatCheck)
        {
            if (!LastTweak.IsReady)
                return;
            var flag = false;
            if (IncludeCombatCheck && !GContext.Main.Me.IsMeleeing)
            {
                GClass37.smethod_0("Combat is not on, toggling");
                GContext.Main.SendKey("Common.ToggleCombat");
                Thread.Sleep(500);
                GContext.Main.Me.Refresh(true);
                LastTweak.Reset();
            }

            if (Target.DistanceToSelf < GContext.Main.MeleeDistance &&
                Target.DistanceToSelf > GContext.Main.MeleeDistance - 1.0 && Target.TicksSinceHealthDrop > 6000)
            {
                GClass37.smethod_0("Tweaking for mini-melee deadzone");
                GContext.Main.Movement.MoveToUnit(Target, GContext.Main.MeleeDistance - 1.4, false);
                LastTweak.Reset();
            }
            else if (Target.DistanceToSelf > GContext.Main.MeleeDistance)
            {
                GClass37.smethod_1("TweakMelee: too far away to tweak, ignoring");
            }
            else
            {
                if (Target.DistanceToSelf > 3.0 && Math.Abs(GContext.Main.Me.GetHeadingDelta(Target.Location)) < 0.785)
                    return;
                if (Math.Abs(GContext.Main.Me.GetHeadingDelta(Target.Location)) > 9.0 * Math.PI / 10.0)
                {
                    GClass37.smethod_0("Ahead of monster, backing up");
                    GContext.Main.PressKey("Common.Back");
                    var gspellTimer = new GSpellTimer(3000);
                    while (!gspellTimer.IsReady)
                        if (Math.Abs(GContext.Main.Me.GetHeadingDelta(Target.Location)) < Math.PI / 2.0 &&
                            Target.DistanceToSelf >= 3.0)
                        {
                            GContext.Main.ReleaseKey("Common.Back");
                            return;
                        }

                    GContext.Main.ReleaseKey("Common.Back");
                    LastTweak.Reset();
                }

                if (Target.DistanceToSelf < 2.0)
                {
                    GClass37.smethod_0("Backing up");
                    flag = true;
                    GContext.Main.PressKey("Common.Back");
                }

                if (Math.Abs(GContext.Main.Me.GetHeadingDelta(Target.Location)) >= 0.785 &&
                    Target.DistanceToSelf >= 1.8)
                    Target.StartSpinTowards();
                var gspellTimer1 = new GSpellTimer(1200);
                while (flag || GContext.Main.IsSpinning)
                {
                    GContext.Main.PulseSpin();
                    if (Target.DistanceToSelf > 3.0 && flag)
                    {
                        flag = false;
                        GContext.Main.ReleaseKey("Common.Back");
                    }

                    if (Math.Abs(GContext.Main.Me.GetHeadingDelta(Target.Location)) < 0.785 && GContext.Main.IsSpinning)
                        GContext.Main.ReleaseSpin();
                    Thread.Sleep(31);
                    Target.Refresh(true);
                    GContext.Main.Me.Refresh(true);
                }

                GContext.Main.ReleaseSpin();
                if (flag)
                    GContext.Main.ReleaseKey("Common.Back");
                LastTweak.Reset();
            }
        }

        public double CompareHeadings(double H1, double H2)
        {
            double num1;
            if (H1 < H2)
            {
                var num2 = H2 - H1;
                if (num2 > Math.PI)
                    num2 -= 2.0 * Math.PI;
                num1 = -num2;
            }
            else
            {
                num1 = H1 - H2;
                if (num1 > Math.PI)
                    num1 -= 2.0 * Math.PI;
            }

            return num1;
        }

        public double GetHeadingTo(GLocation Start, GLocation Target)
        {
            double num1 = Math.Abs(Start.X - Target.X);
            double num2 = Math.Abs(Start.Y - Target.Y);
            if (num1 < 1.0 && num2 < 1.0)
                return -1.0;
            var num3 = 0.0;
            if (Target.X >= (double)Start.X && Target.Y >= (double)Start.Y)
                num3 = Degrees(Math.Atan(num2 / num1));
            if (Target.X >= (double)Start.X && Target.Y <= (double)Start.Y)
                num3 = Degrees(Math.Atan(num2 / num1)) * -1.0 + 360.0;
            if (Target.X <= (double)Start.X && Target.Y <= (double)Start.Y)
                num3 = Degrees(Math.Atan(num2 / num1)) + 180.0;
            if (Target.X <= (double)Start.X && Target.Y >= (double)Start.Y)
                num3 = Degrees(Math.Atan(num2 / num1) * -1.0) + 180.0;
            return Math.PI / 180.0 * num3;
        }

        public void SetHeading(GLocation NewLocation)
        {
            SetHeading(GPlayerSelf.Me.Location.GetHeadingTo(NewLocation));
        }

        public void SetHeading(double NewHeading)
        {
            SetHeading(NewHeading, 0.16);
        }

        public void SetHeading(double NewHeading, double Tolerance)
        {
            var Delta = CompareHeadings(NewHeading, GContext.Main.Me.Heading);
            if (Math.Abs(Delta) < Tolerance)
                return;
            var SpinKey = Delta <= 0.0 ? "Common.RotateRight" : "Common.RotateLeft";
            if (GContext.Main.IsSpinning && !GContext.Main.MouseSpin && CheckTapSpin(Delta, SpinKey))
                return;
            GContext.Main.StartSpinTowards(NewHeading);
            var tickCount = Environment.TickCount;
            var millisecondsTimeout = GContext.Main.MouseSpin ? 10 : 20;
            do
            {
                GContext.Main.PulseSpin(false);
                GContext.Main.Me.Refresh(true);
                var num = CompareHeadings(NewHeading, GContext.Main.Me.Heading);
                if ((num >= 0.0 || Delta <= 0.0) && (num <= 0.0 || Delta >= 0.0) && Math.Abs(num) > Tolerance &&
                    GContext.Main.IsSpinning)
                    Thread.Sleep(millisecondsTimeout);
                else
                    goto label_8;
            } while (Environment.TickCount - tickCount <= 4000);

            GClass37.smethod_0(GClass30.smethod_1(310));
            label_8:
            GContext.Main.ReleaseSpin();
        }

        private static double Degrees(double radians)
        {
            return 180.0 / Math.PI * radians;
        }

        public bool MoveToUnit(GUnit Target, double Distance, bool LeaveRunning)
        {
            return MoveToUnit(Target, Distance, LeaveRunning, false);
        }

        public bool MoveToUnit(
            GUnit Target,
            double Distance,
            bool LeaveRunning,
            bool AvoidPossibleAdds)
        {
            return GContext.Main.MoveHelper != null
                ? GContext.Main.MoveHelper.MoveToUnit(Target, Distance, LeaveRunning, AvoidPossibleAdds)
                : BaseMoveToUnit(Target, Distance, LeaveRunning, AvoidPossibleAdds);
        }

        public bool BaseMoveToUnit(
            GUnit Target,
            double Distance,
            bool LeaveRunning,
            bool AvoidPossibleAdds)
        {
            if (Target == null)
                throw new Exception("MoveToUnit.Target = null, bailing out");
            var flag1 = false;
            var num1 = 0;
            var gspellTimer1 = new GSpellTimer(1500);
            var gspellTimer2 = new GSpellTimer(24000);
            var location = GContext.Main.Me.Location;
            Target.Face(Distance > 20.0 ? Math.PI / 18.0 : Math.PI / 36.0);
            if (Target.DistanceToSelf <= Distance)
            {
                if (!LeaveRunning)
                    GContext.Main.ReleaseSpinRun();
                return true;
            }

            GContext.Main.StartRun();
            while (!gspellTimer2.IsReady && Target.DistanceToSelf > Distance && Target.IsValid)
            {
                if (AvoidPossibleAdds)
                {
                    var likelyAdds = GObjectList.GetLikelyAdds();
                    if (likelyAdds.Length > 0 && likelyAdds[0].DistanceToSelf <
                        (double)(GContext.Main.GetConfigInt("LootSafeDistance") + 5))
                    {
                        GContext.Main.Log("Stopping approach, too likely to aggro: " + likelyAdds[0]);
                        GContext.Main.ReleaseSpinRun();
                        return false;
                    }
                }

                var flag2 = false;
                if (Math.Abs(CompareHeadings(GContext.Main.Me.GetHeadingTo(Target), GContext.Main.Me.Heading)) > 1.0)
                {
                    GClass37.smethod_1(GClass30.smethod_1(18));
                    flag2 = true;
                }

                if (flag2)
                    GContext.Main.ReleaseRun();
                Target.Face(Distance > 20.0 ? 0.5 : 0.3);
                if (flag2)
                    GContext.Main.StartRun();
                if (gspellTimer1.IsReady)
                {
                    if (GContext.Main.Me.Location.GetDistanceTo(location) < 3.0)
                    {
                        ++num1;
                        if (!flag1)
                        {
                            flag1 = true;
                            var StrafeKey = StartupClass.random_0.Next() % 2 == 0
                                ? "Common.StrafeLeft"
                                : "Common.StrafeRight";
                            if (!StrafeTilUnstuck(StrafeKey))
                                StrafeTilUnstuck(StrafeKey == "Common.StrafeLeft"
                                    ? "Common.StrafeRight"
                                    : "Common.StrafeLeft");
                        }
                        else
                        {
                            ++num1;
                            GClass37.smethod_0(GClass30.smethod_1(19));
                            GContext.Main.ReleaseRun();
                            var num2 = StartupClass.random_0.Next() % 2 != 0
                                ? GPlayerSelf.Me.Heading + Math.PI / 2.0
                                : GPlayerSelf.Me.Heading - Math.PI / 2.0;
                            if (num2 > 2.0 * Math.PI)
                            {
                                var num3 = num2 - 2.0 * Math.PI;
                            }

                            Target.Face(Distance > 20.0 ? 0.5 : 0.3);
                            GContext.Main.StartRun();
                            Thread.Sleep(1000 + StartupClass.random_0.Next() % 1500);
                            flag1 = false;
                        }
                    }

                    location = GContext.Main.Me.Location;
                    gspellTimer1.Reset();
                }

                StartupClass.smethod_39(107);
                if (num1 >= 4)
                {
                    GClass37.smethod_0("Stuck too many times in MoveToMonster, giving up");
                    break;
                }
            }

            if (!LeaveRunning)
            {
                GContext.Main.ReleaseRun();
                Thread.Sleep(212);
            }

            return Target.DistanceToSelf <= Distance;
        }

        public void BasePatrolTowards(object DestOrUnit)
        {
            var Target = DestOrUnit.GetType() != typeof(GLocation)
                ? ((GObject)DestOrUnit).Location
                : (GLocation)DestOrUnit;
            double headingTo = GPlayerSelf.Me.Location.GetHeadingTo(Target);
            var Delta = GContext.Main.Movement.CompareHeadings(headingTo, GPlayerSelf.Me.Heading);
            var SpinKey = Delta > 0.0 ? "Common.RotateLeft" : "Common.RotateRight";
            if (Math.Abs(Delta) >= 2.0 && !GContext.Main.MouseSpin)
                GContext.Main.ReleaseRun();
            else
                GContext.Main.StartRun();
            if (Math.Abs(Delta) < Math.PI / 18.0)
            {
                GClass37.smethod_1("Heading close enough, releasing spin");
                GContext.Main.ReleaseSpin();
            }
            else
            {
                if (!GContext.Main.MouseSpin && GContext.Main.Movement.CheckTapSpin(Delta, SpinKey))
                    return;
                GContext.Main.StartSpinTowards(headingTo);
            }
        }

        public bool MoveToLocation(GLocation Target)
        {
            return MoveToLocation(Target, GContext.Main.MeleeDistance, false);
        }

        public bool MoveToLocation(GLocation Target, double Distance, bool LeaveRunning)
        {
            return GContext.Main.MoveHelper != null
                ? GContext.Main.MoveHelper.MoveToLocation(Target, Distance, LeaveRunning)
                : BaseMoveToLocation(Target, Distance, LeaveRunning);
        }

        public bool BaseMoveToLocation(GLocation Target, double Distance, bool LeaveRunning)
        {
            var flag1 = false;
            var gspellTimer1 = new GSpellTimer(1500);
            var gspellTimer2 = new GSpellTimer(12000);
            var location = GContext.Main.Me.Location;
            SetHeading(GPlayerSelf.Me.Location.GetHeadingTo(Target), Distance > 20.0 ? Math.PI / 18.0 : Math.PI / 36.0);
            if (Target.DistanceToSelf <= Distance)
                return true;
            GContext.Main.StartRun();
            while (!gspellTimer2.IsReady && Target.DistanceToSelf > Distance)
            {
                var flag2 = false;
                if (Math.Abs(CompareHeadings(GPlayerSelf.Me.Location.GetHeadingTo(Target), GContext.Main.Me.Heading)) >
                    1.0)
                {
                    GClass37.smethod_1(GClass30.smethod_1(18));
                    flag2 = true;
                }

                if (flag2)
                    GContext.Main.ReleaseRun();
                SetHeading(GPlayerSelf.Me.Location.GetHeadingTo(Target), Distance > 20.0 ? 0.5 : 0.3);
                if (flag2)
                    GContext.Main.StartRun();
                if (gspellTimer1.IsReady)
                {
                    if (GContext.Main.Me.Location.GetDistanceTo(location) < 3.0)
                    {
                        if (!flag1)
                        {
                            flag1 = true;
                            var StrafeKey = StartupClass.random_0.Next() % 2 == 0
                                ? "Common.StrafeLeft"
                                : "Common.StrafeRight";
                            if (!StrafeTilUnstuck(StrafeKey))
                                StrafeTilUnstuck(StrafeKey == "Common.StrafeLeft"
                                    ? "Common.StrafeRight"
                                    : "Common.StrafeLeft");
                        }
                        else
                        {
                            GClass37.smethod_0("Still stuck, trying a different angle");
                            GContext.Main.ReleaseRun();
                            GContext.Main.PressKey("Common.Back");
                            Thread.Sleep(600);
                            GContext.Main.ReleaseKey("Common.Back");
                            var NewHeading = StartupClass.random_0.Next() % 2 != 0
                                ? GPlayerSelf.Me.Heading + Math.PI / 2.0
                                : GPlayerSelf.Me.Heading - Math.PI / 2.0;
                            if (NewHeading > 2.0 * Math.PI)
                                NewHeading -= 2.0 * Math.PI;
                            if (NewHeading < 0.0)
                                NewHeading += 2.0 * Math.PI;
                            GClass37.smethod_1("Current: " + GPlayerSelf.Me.Heading + ", new: " + NewHeading);
                            SetHeading(NewHeading, Distance > 20.0 ? 0.5 : 0.3);
                            GContext.Main.StartRun();
                            Thread.Sleep(1000 + StartupClass.random_0.Next() % 1500);
                            flag1 = false;
                        }
                    }

                    location = GContext.Main.Me.Location;
                    gspellTimer1.Reset();
                }

                StartupClass.smethod_39(50);
            }

            if (!LeaveRunning)
                GContext.Main.ReleaseRun();
            return Target.DistanceToSelf <= Distance;
        }

        private bool StrafeTilUnstuck(string StrafeKey)
        {
            var flag = false;
            var gspellTimer = new GSpellTimer(2000);
            var location = GContext.Main.Me.Location;
            GContext.Main.PressKey(StrafeKey);
            Thread.Sleep(100);
            GContext.Main.SendKey("Common.Jump");
            while (!gspellTimer.IsReady)
            {
                Thread.Sleep(500);
                if (GContext.Main.Me.Location.GetDistanceTo(location) <= 3.0)
                {
                    location = GContext.Main.Me.Location;
                }
                else
                {
                    flag = true;
                    break;
                }
            }

            GContext.Main.ReleaseKey(StrafeKey);
            return flag;
        }

        public void LookConfused()
        {
            for (var index = 0; index < 5; ++index)
            {
                SetHeading(StartupClass.random_0.NextDouble() * 6.14);
                StartupClass.gclass73_0.method_34(1500, 5000);
                if (StartupClass.random_0.Next() % 3 == 0)
                {
                    GClass42.gclass42_0.method_0("Common.Jump");
                    StartupClass.gclass73_0.method_34(1500, 3000);
                }
            }
        }

        public void BackAway(GUnit Target, double RequestedDistance)
        {
            var location = Target.Location;
            var gspellTimer = new GSpellTimer(4000, false);
            Target.Face();
            GContext.Main.PressKey("Common.Back");
            while (!gspellTimer.IsReadySlow)
                if (Target.Location.GetDistanceTo(location) <= 6.5)
                {
                    if (Target.DistanceToSelf >= RequestedDistance)
                        break;
                }
                else
                {
                    GContext.Main.Debug("Monster moved too much while backing away: " + Target);
                    break;
                }

            GContext.Main.ReleaseKey("Common.Back");
        }

        public double AdjustHeading(double StartHeading, double DeltaRads)
        {
            var num = StartHeading + DeltaRads;
            if (num < 0.0)
                num += 2.0 * Math.PI;
            if (num >= 2.0 * Math.PI)
                num -= 2.0 * Math.PI;
            return num;
        }

        public bool CheckTapSpin(double Delta, string SpinKey)
        {
            var int_14 = 0;
            if (Math.Abs(Delta) < 0.25)
                int_14 = (int)(Delta * GClass55.int_19);
            if (int_14 <= 0)
                return false;
            var heading = GPlayerSelf.Me.Heading;
            GClass42.gclass42_0.method_1(SpinKey);
            StartupClass.smethod_39(int_14);
            GClass42.gclass42_0.method_2(SpinKey);
            GPlayerSelf.Me.Refresh(true);
            var num = CompareHeadings(heading, GPlayerSelf.Me.Heading);
            if (Math.Abs(num) > 0.02)
            {
                if ((num < 0.0 && Delta > 0.0) || (num > 0.0 && Delta < 0.0))
                    GClass55.int_19 -= 15;
                else
                    GClass55.int_19 += 15;
            }

            return true;
        }

        public void TweakSpell(GUnit Target)
        {
            if (!LastTweak.IsReady)
                return;
            var flag = false;
            if (Target.DistanceToSelf > GContext.Main.MeleeDistance)
            {
                GClass37.smethod_1("TweakSpell: too far away to tweak, ignoring");
            }
            else
            {
                if (Target.DistanceToSelf > 3.0 && Math.Abs(GContext.Main.Me.GetHeadingDelta(Target.Location)) < 0.785)
                    return;
                if (Math.Abs(GContext.Main.Me.GetHeadingDelta(Target.Location)) > 9.0 * Math.PI / 10.0)
                {
                    GClass37.smethod_0("Ahead of monster, backing up");
                    GContext.Main.PressKey("Common.Back");
                    var gspellTimer = new GSpellTimer(3000);
                    while (!gspellTimer.IsReady)
                        if (Math.Abs(GContext.Main.Me.GetHeadingDelta(Target.Location)) < Math.PI / 2.0 &&
                            Target.DistanceToSelf >= 3.0)
                        {
                            GContext.Main.ReleaseKey("Common.Back");
                            return;
                        }

                    GContext.Main.ReleaseKey("Common.Back");
                    LastTweak.Reset();
                }

                if (Target.DistanceToSelf < 2.0)
                {
                    GClass37.smethod_0("Backing up");
                    flag = true;
                    GContext.Main.PressKey("Common.Back");
                }

                if (Math.Abs(GContext.Main.Me.GetHeadingDelta(Target.Location)) >= 0.785 &&
                    Target.DistanceToSelf >= 1.8)
                    Target.StartSpinTowards();
                var gspellTimer1 = new GSpellTimer(1200);
                while (flag || GContext.Main.IsSpinning)
                {
                    GContext.Main.PulseSpin();
                    if (Target.DistanceToSelf > 3.0 && flag)
                    {
                        flag = false;
                        GContext.Main.ReleaseKey("Common.Back");
                    }

                    if (Math.Abs(GContext.Main.Me.GetHeadingDelta(Target.Location)) < 0.785 && GContext.Main.IsSpinning)
                        GContext.Main.ReleaseSpin();
                    Thread.Sleep(31);
                    Target.Refresh(true);
                    GContext.Main.Me.Refresh(true);
                }

                GContext.Main.ReleaseSpin();
                if (flag)
                    GContext.Main.ReleaseKey("Common.Back");
                LastTweak.Reset();
            }
        }

        public void ConsiderAvoidAdds(int AvoidAddDistance)
        {
            var likelyAdds = GObjectList.GetLikelyAdds();
            if (likelyAdds.Length == 0 || !AddBackup.IsReady)
                return;
            var closest = (GUnit)GObjectList.GetClosest(likelyAdds);
            if (closest.DistanceToSelf >= (double)AvoidAddDistance || !closest.IsApproaching ||
                Math.Abs(closest.Bearing) >= 1.04)
                return;
            GContext.Main.Log("Possible add: \"" + closest.Name + "\" (distance = " + closest.DistanceToSelf +
                              ", bearing = " + closest.Bearing + "), backing up combat");
            AddBackup.Reset();
            var gspellTimer = new GSpellTimer(3000);
            GContext.Main.PressKey("Common.Back");
            closest.StartSpinTowards();
            while (!gspellTimer.IsReadySlow)
            {
                GContext.Main.PulseSpin();
                if (Math.Abs(closest.Bearing) < Math.PI / 10.0)
                    GContext.Main.ReleaseSpin();
                if (closest.DistanceToSelf > AvoidAddDistance + 6.0)
                    break;
            }

            GContext.Main.ReleaseSpin();
            GContext.Main.ReleaseKey("Common.Back");
            if (gspellTimer.IsReady)
                GContext.Main.Log("Backed up for max time, stopping");
            Thread.Sleep(601);
            AddBackup.Reset();
        }
    }
}