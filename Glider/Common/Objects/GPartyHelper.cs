// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GPartyHelper
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;

namespace Glider.Common.Objects
{
    public class GPartyHelper
    {
        private readonly SortedList<long, PartyBuffBucket> PartyBuckets;

        public GPartyHelper()
        {
            PartyBuckets = new SortedList<long, PartyBuffBucket>();
        }

        public GPartyMode Mode
        {
            get
            {
                switch (StartupClass.gclass54_0.genum7_0)
                {
                    case GEnum7.const_0:
                        return GPartyMode.Solo;
                    case GEnum7.const_1:
                        return GPartyMode.Leader;
                    case GEnum7.const_2:
                        return GPartyMode.Follower;
                    default:
                        throw new Exception("unknown partymode!");
                }
            }
        }

        public GHealDisposition HealMode
        {
            get
            {
                if (StartupClass.glideMode_0 != GlideMode.Auto || StartupClass.gclass54_0.genum7_0 == GEnum7.const_0)
                    return GHealDisposition.Never;
                switch (GClass61.gclass61_0.method_2("PartyHealMode"))
                {
                    case "Dedicated":
                        return GHealDisposition.Dedicated;
                    case "Normal":
                        return GHealDisposition.Normal;
                    case "Never":
                        return GHealDisposition.Never;
                    default:
                        return GHealDisposition.Normal;
                }
            }
        }

        public bool HealParty => HealMode != GHealDisposition.Never && GClass54.gclass54_0.genum7_0 != GEnum7.const_0;

        public long[] GetPartyMembers()
        {
            var longList = new List<long>();
            var int_29 = GClass18.gclass18_0.method_4("PartyMembers");
            var num1 = 0;
            while (num1 < 4)
            {
                var num2 = GProcessMemoryManipulator.smethod_12(int_29, "PartyMember" + num1);
                if (num2 != 0L)
                    longList.Add(num2);
                ++num1;
                int_29 += 8;
            }

            return longList.ToArray();
        }

        public GPlayer[] GetPartyMemberObjects()
        {
            var gplayerList = new List<GPlayer>();
            foreach (var partyMember in GetPartyMembers())
            {
                var unit = GObjectList.FindUnit(partyMember);
                if (unit != null)
                    gplayerList.Add((GPlayer)unit);
            }

            return gplayerList.ToArray();
        }

        public void TargetPartyMember(GPlayer Target)
        {
            var partyMembers = GetPartyMembers();
            var index = 0;
            while (index < partyMembers.Length && partyMembers[index] != Target.GUID)
                ++index;
            if (index == partyMembers.Length)
                Logger.LogMessage("!! Never found party member to target: " + Target);
            else
                GClass55.smethod_9((short)(113 + (short)index));
        }

        private PartyBuffBucket GetBuffsForPlayer(long GUID)
        {
            if (!PartyBuckets.ContainsKey(GUID))
            {
                var partyBuffBucket = new PartyBuffBucket(GUID);
                PartyBuckets.Add(GUID, partyBuffBucket);
            }

            return PartyBuckets[GUID];
        }

        public bool IsBuffReady(GPlayer Member, string SpellName, int BuffDurationSeconds)
        {
            var buffsForPlayer = GetBuffsForPlayer(Member.GUID);
            if (!buffsForPlayer.Buffs.ContainsKey(SpellName))
            {
                buffsForPlayer.Buffs.Add(SpellName, new PartyBuff(SpellName, BuffDurationSeconds * 1000));
                if (GClass61.gclass61_0.method_5("ResetBuffs"))
                    buffsForPlayer.Buffs[SpellName].Timer.ForceReady();
            }

            return buffsForPlayer.Buffs[SpellName].Timer.IsReady;
        }

        public bool BuffPartyMember(
            GPlayer Member,
            string SpellName,
            int BuffDurationSeconds,
            bool AlwaysCast)
        {
            if (!AlwaysCast && !IsBuffReady(Member, SpellName, BuffDurationSeconds))
                return false;
            CastOnMember(Member, SpellName, GPlayerSelf.Me.Target);
            var buffsForPlayer = GetBuffsForPlayer(Member.GUID);
            if (!buffsForPlayer.Buffs.ContainsKey(SpellName))
                buffsForPlayer.Buffs.Add(SpellName, new PartyBuff(SpellName, BuffDurationSeconds * 1000));
            buffsForPlayer.Buffs[SpellName].Timer.Reset();
            return true;
        }

        public void CastOnMember(GPlayer Member, string SpellName, GUnit OriginalTarget)
        {
            Logger.LogMessage("Casting \"" + SpellName + "\" on party member \"" + Member.Name + "\"");
            if (Member.DistanceToSelf > 30.0 && Member.DistanceToSelf < 200.0)
                Member.Approach(30.0);
            if (Member.DistanceToSelf > 30.0)
                return;
            StartupClass.CurrentGameClass.LeaveForm();
            TargetPartyMember(Member);
            GContext.Main.CastSpell(SpellName);
            if (OriginalTarget != null)
                OriginalTarget.SetAsTarget(true);
            else
                GContext.Main.ClearTarget();
        }

        public bool BuffParty(string SpellName, int BuffDurationSeconds, GPlayerClass MatchingClass)
        {
            foreach (var partyMember in GetPartyMembers())
            {
                var unit = GObjectList.FindUnit(partyMember);
                if (unit != null && unit.DistanceToSelf <= 30.0 &&
                    (MatchingClass == GPlayerClass.Unknown || MatchingClass == ((GPlayer)unit).PlayerClass) &&
                    BuffPartyMember((GPlayer)unit, SpellName, BuffDurationSeconds, false))
                    return true;
            }

            return false;
        }
    }
}