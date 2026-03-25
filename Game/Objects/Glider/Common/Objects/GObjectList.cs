// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GObjectList
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;

namespace Glider.Common.Objects
{
    public class GObjectList
    {
        private static readonly bool LogObjects = false;
        private static int FrameNumber;
        private static int LastUpdate;
        private static readonly SortedList<long, GObject> LastSnapshot = new SortedList<long, GObject>();

        public static void ClearCache()
        {
            lock (LastSnapshot)
            {
                LastUpdate = Environment.TickCount - 51;
                LastSnapshot.Clear();
            }
        }

        public static void SetCacheDirty()
        {
            lock (LastSnapshot)
            {
                LastUpdate = Environment.TickCount - 51;
            }
        }

        private static SortedList<long, GObject> GetAll()
        {
            return GetAll(false);
        }

        private static SortedList<long, GObject> GetAll(bool BypassTimer)
        {
            var flag = false;
            lock (LastSnapshot)
            {
                if (Environment.TickCount - LastUpdate < 50 && !BypassTimer)
                    return LastSnapshot;
                ++FrameNumber;
                var int5 = StartupClass.int_5;
                if (int5 == 0)
                {
                    Logger.LogMessage(MessageProvider.GetMessage(56));
                    StartupClass.smethod_27(true, "GetObjectsMainTableEmpty");
                    return null;
                }

                var BaseAddress = GetFirstObjectPointer(int5);
                var num1 = BaseAddress;
                var num = 0;
                var hashSet = new HashSet<int>();
                while (true)
                {
                    if (++num > 8192)
                    {
                        Logger.smethod_1("Object list traversal aborted: exceeded safety iteration cap");
                        break;
                    }
                    if (BaseAddress != 0 && !hashSet.Add(BaseAddress))
                    {
                        if (BaseAddress != num1)
                            Logger.smethod_1("Object list traversal aborted: detected cycle in object links");
                        break;
                    }
                    long guid;
                    do
                    {
                        if ((BaseAddress & 1) == 0 && BaseAddress != 0 && BaseAddress != 28)
                        {
                            guid = QuickGetGUID(BaseAddress);
                            if (!LastSnapshot.ContainsKey(guid))
                            {
                                GObject gobject;
                                if (guid == StartupClass.long_0)
                                {
                                    gobject = new GPlayerSelf(BaseAddress, FrameNumber);
                                    if (GContext.Main.Me == null)
                                        GContext.Main.Me = (GPlayerSelf)gobject;
                                }
                                else
                                {
                                    gobject = GObject.Create(BaseAddress, FrameNumber);
                                }

                                LastSnapshot.Add(guid, gobject);
                            }
                            else
                            {
                                goto label_7;
                            }
                        }
                        else
                        {
                            goto label_15;
                        }
                        BaseAddress = GameMemoryAccess.ReadInt32(BaseAddress + 60, "GameObjNext");
                    } while (!LogObjects);

                    goto label_13;
                label_7:
                    var existingObject = LastSnapshot[guid];
                    if (existingObject.BaseAddress != BaseAddress)
                    {
                        existingObject.BaseAddress = BaseAddress;
                        existingObject.StorageAddress = GameMemoryAccess.ReadInt32(BaseAddress + 8, "GameObjStorage");
                    }

                    existingObject.FrameNumber = FrameNumber;
                    continue;
                label_13:
                    Logger.smethod_1("+ Adding new object: " + LastSnapshot[guid]);
                }

                var num3 = MemoryOffsetTable.Instance.HasOffset("MainTableActivePlayer")
                    ? MemoryOffsetTable.Instance.GetIntOffset("MainTableActivePlayer")
                    : 24;
                if (int5 != 0 && num3 > 0)
                {
                    var num2 = GameMemoryAccess.ReadInt32(int5 + num3, "MainTableActivePlayerObj");
                    if (IsLikelyObjectPointer(num2))
                    {
                        var guid1 = QuickGetGUID(num2);
                        if (guid1 != 0L)
                        {
                            StartupClass.long_0 = guid1;
                            if (!LastSnapshot.ContainsKey(guid1) || !(LastSnapshot[guid1] is GPlayerSelf))
                            {
                                var gobject = new GPlayerSelf(num2, FrameNumber);
                                if (LastSnapshot.ContainsKey(guid1))
                                    LastSnapshot.Remove(guid1);
                                LastSnapshot.Add(guid1, gobject);
                            }
                            else
                            {
                                var activePlayer = LastSnapshot[guid1];
                                if (activePlayer.BaseAddress != num2)
                                {
                                    activePlayer.BaseAddress = num2;
                                    activePlayer.StorageAddress = GameMemoryAccess.ReadInt32(num2 + 8, "GameObjStorage");
                                }

                                activePlayer.FrameNumber = FrameNumber;
                            }

                            GContext.Main.Me = (GPlayerSelf)LastSnapshot[guid1];
                        }
                    }
                }

            label_15:
                for (var index = 0; index < LastSnapshot.Count; ++index)
                {
                    var gobject = LastSnapshot.Values[index];
                    if (gobject.FrameNumber != FrameNumber)
                    {
                        if (gobject is GPlayerSelf && gobject.GUID == StartupClass.long_0)
                        {
                            gobject.FrameNumber = FrameNumber;
                            continue;
                        }

                        if (gobject is GPlayerSelf || gobject.GUID == StartupClass.long_0)
                        {
                            GPlayerSelf.Me = null;
                            if (GContext.Main != null)
                                GContext.Main.Me = null;
                        }

                        if (LogObjects)
                            Logger.smethod_1("+ Culling old object: " + gobject);
                        gobject.Cull();
                        LastSnapshot.RemoveAt(index);
                        --index;
                    }
                }

                LastUpdate = Environment.TickCount;
                if (LastSnapshot.Count == 0)
                    flag = true;
            }

            if (flag)
                StartupClass.smethod_27(true, "ZeroCountObjects");
            return LastSnapshot;
        }

        private static GObject[] GetObjects(GObjectType ObjectType)
        {
            lock (LastSnapshot)
            {
                var all = GetAll();
                var gobjectList = new List<GObject>();
                if (all == null)
                    return null;
                foreach (var gobject in all.Values)
                    if (gobject.Type == ObjectType || ObjectType == GObjectType.Any)
                        gobjectList.Add(gobject);
                return gobjectList.ToArray();
            }
        }

        public static GObject[] GetObjects()
        {
            return GetObjects(GObjectType.Any);
        }

        public static GUnit[] GetUnits()
        {
            var objects = GetObjects(GObjectType.Any);
            var gunitList = new List<GUnit>();
            foreach (var gobject in objects)
                if (gobject.Type == GObjectType.Player || gobject.Type == GObjectType.Monster)
                    gunitList.Add((GUnit)gobject);
            return gunitList.ToArray();
        }

        public static GItem[] GetItems()
        {
            var objects = GetObjects(GObjectType.Any);
            var gitemList = new List<GItem>();
            foreach (var gobject in objects)
                if (gobject.Type == GObjectType.Item || gobject.Type == GObjectType.Container)
                    gitemList.Add((GItem)gobject);
            return gitemList.ToArray();
        }

        public static GMonster[] GetMonsters()
        {
            var objects = GetObjects(GObjectType.Monster);
            var destinationArray = new GMonster[objects.Length];
            Array.Copy(objects, destinationArray, objects.Length);
            return destinationArray;
        }

        public static GNode[] GetNodes()
        {
            var objects = GetObjects(GObjectType.Node);
            var destinationArray = new GNode[objects.Length];
            Array.Copy(objects, destinationArray, objects.Length);
            return destinationArray;
        }

        public static GPlayer[] GetPlayers()
        {
            var objects = GetObjects(GObjectType.Player);
            var destinationArray = new GPlayer[objects.Length];
            Array.Copy(objects, destinationArray, objects.Length);
            return destinationArray;
        }

        private static long QuickGetGUID(int BaseAddress)
        {
            return GameMemoryAccess.ReadInt64(BaseAddress + 48, "QuickGUID");
        }

        public static GUnit FindUnit(long GUID)
        {
            foreach (var unit in GetUnits())
                if (unit.GUID == GUID)
                    return unit;
            return null;
        }

        public static GUnit FindUnit(string DisplayName)
        {
            foreach (var unit in GetUnits())
                if (string.Compare(unit.Name, DisplayName, true) == 0)
                    return unit;
            return null;
        }

        public static GUnit FindUnit(string DisplayName, bool IsNPC)
        {
            if (IsNPC)
                foreach (var unit in GetUnits())
                    if (unit.Name.ToLower().StartsWith(DisplayName.ToLower()))
                        return unit;
            return null;
        }

        public static GUnit FindUnitByTarget(long TargetGUID)
        {
            var units = GetUnits();
            var gunitList = new List<GUnit>();
            foreach (var gunit in units)
                if (gunit.TargetGUID == TargetGUID)
                    gunitList.Add(gunit);
            return gunitList.Count == 0 ? null : (GUnit)GetClosest(gunitList.ToArray());
        }

        public static GObject FindObject(long GUID)
        {
            foreach (var gobject in GetObjects())
                if (gobject.GUID == GUID)
                    return gobject;
            return null;
        }

        public static GMonster GetNearestHostile()
        {
            return GetNearestHostile(GContext.Main.Me.Location, 0L, false);
        }

        public static GMonster GetNearestHostile(
            GLocation Location,
            long ExcludeGUID,
            bool IncludeInjured)
        {
            var monsters = GetMonsters();
            GMonster nearestHostile = null;
            var num = 9999.0;
            foreach (var gmonster in monsters)
                if ((IncludeInjured || gmonster.Health == 1.0) && gmonster.IsValid && !gmonster.IsTrivial &&
                    gmonster.GUID != ExcludeGUID &&
                    (gmonster.Reaction == GReaction.Hostile || gmonster.Reaction == GReaction.Unknown) &&
                    gmonster.Location.GetDistanceTo(Location) < num &&
                    Math.Abs(gmonster.Location.Z - Location.Z) < 12.0)
                {
                    num = gmonster.Location.GetDistanceTo(Location);
                    nearestHostile = gmonster;
                }

            return nearestHostile;
        }

        public static bool CheckForAttackers()
        {
            var attackers = GetAttackers(false);
            var num = 9999.0;
            foreach (var gunit in attackers)
                if (gunit.DistanceToSelf < num && !gunit.IsDead)
                    return true;
            return false;
        }

        public static GUnit GetNearestAttacker(long ExcludeGUID)
        {
            var attackers = GetAttackers(true);
            GUnit nearestAttacker = null;
            var num = 9999.0;
            foreach (var gunit in attackers)
                if (gunit.DistanceToSelf < num && gunit.GUID != ExcludeGUID && !gunit.IsDead)
                {
                    num = gunit.DistanceToSelf;
                    nearestAttacker = gunit;
                }

            return nearestAttacker;
        }

        public static GUnit[] GetAttackers()
        {
            var gunitList = new List<GUnit>();
            foreach (var unit in GetUnits())
            {
                var flag = false;
                if (unit.IsMonster && unit.TargetGUID == GContext.Main.Me.GUID)
                    flag = true;
                if (GContext.Main.Me.PetGUID != 0L && unit.IsMonster && unit.TargetGUID == GContext.Main.Me.PetGUID)
                    flag = true;
                if (unit.IsPlayer && unit.GUID == GContext.Main.Me.TargetGUID)
                    flag = true;
                if (flag && !unit.IsDead)
                    gunitList.Add(unit);
            }

            return gunitList.ToArray();
        }

        public static GItem[] GetEquippedItems()
        {
            var gitemList = new List<GItem>();
            foreach (var gobject in GetObjects())
                if (gobject.Type == GObjectType.Item)
                {
                    var gitem = (GItem)gobject;
                    if (gitem.IsEquipped)
                        gitemList.Add(gitem);
                }

            return gitemList.ToArray();
        }

        public static GUnit[] GetAttackers(bool IncludePet)
        {
            var gunitList = new List<GUnit>();
            foreach (var unit in GetUnits())
            {
                var flag = false;
                if (unit.IsMonster && unit.TargetGUID == GContext.Main.Me.GUID)
                    flag = true;
                if (IncludePet && GContext.Main.Me.PetGUID != 0L && unit.IsMonster &&
                    unit.TargetGUID == GContext.Main.Me.PetGUID)
                    flag = true;
                if (unit.IsPlayer && unit.GUID == GContext.Main.Me.TargetGUID)
                    flag = true;
                if (flag && !unit.IsDead)
                    gunitList.Add(unit);
            }

            return gunitList.ToArray();
        }

        public static void DumpDebug()
        {
            var num1 = 0;
            var num2 = 0;
            lock (LastSnapshot)
            {
                Logger.smethod_1("-- GObjectList.DumpDebug invoked, LastUpdate = " + LastUpdate + ", Current = " +
                                   Environment.TickCount);
                var lastSnapshot = LastSnapshot;
                foreach (var key in lastSnapshot.Keys)
                {
                    ++num1;
                    Logger.smethod_1(key.ToString("x16") + " --> " + lastSnapshot[key]);
                }

                Logger.smethod_1("-- Object dump done, hits: " + num1 + ", unit hits: " + num2);
            }
        }

        public static GUnit[] GetLikelyAdds()
        {
            var gunitList = new List<GUnit>();
            foreach (var unit in GetUnits())
                if (unit.IsMonster)
                {
                    var gmonster = (GMonster)unit;
                    if (gmonster.Reaction == GReaction.Hostile && gmonster.Health == 1.0 &&
                        Math.Abs(gmonster.Location.Z - GContext.Main.Me.Location.Z) < 10.0 && gmonster.TargetGUID == 0L)
                        gunitList.Add(gmonster);
                }

            return gunitList.ToArray();
        }

        public static GObject GetClosest(GObject[] Objects)
        {
            return GetClosest(Objects, GContext.Main.Me.Location);
        }

        public static GObject GetClosest(GObject[] Objects, GLocation Location)
        {
            if (Objects == null)
                return null;
            var num = 99999.0;
            GObject closest = null;
            foreach (var gobject in Objects)
                if (gobject.Location.GetDistanceTo(Location) < num)
                {
                    num = gobject.Location.GetDistanceTo(Location);
                    closest = gobject;
                }

            return closest;
        }

        public static GMonster GetClosestNeutralMonster()
        {
            var monsters = GetMonsters();
            var num = 99999.0;
            GMonster closestNeutralMonster = null;
            foreach (var gmonster in monsters)
                if (gmonster.Reaction == GReaction.Neutral && gmonster.Level > 5 &&
                    Math.Abs(gmonster.Location.Z - GContext.Main.Me.Location.Z) < 10.0 && gmonster.Health == 1.0 &&
                    gmonster.DistanceToSelf < num)
                {
                    num = gmonster.DistanceToSelf;
                    closestNeutralMonster = gmonster;
                }

            return closestNeutralMonster;
        }

        public static bool IsObjectPresent(long GUID)
        {
            return GetAll().ContainsKey(GUID);
        }

        public static GMonster GetNextProfileTarget()
        {
            if (StartupClass.gprofile_0.IgnoreAttackers)
                return null;
            var num1 = 9999.0;
            GMonster gmonster = null;
            foreach (var monster in GetMonsters())
                if (monster.IsValidProfileTarget)
                {
                    double distanceToSelf = monster.DistanceToSelf;
                    if (distanceToSelf < num1)
                    {
                        num1 = distanceToSelf;
                        gmonster = monster;
                    }
                }

            if (gmonster == null && ConfigManager.gclass61_0.method_5("LogMonsterChecks"))
                Logger.smethod_1("FindClosestToMe is returning null, nobody worth killing right now");
            if (gmonster == null)
                return null;
            var num2 = ConfigManager.gclass61_0.method_3("ExtraPull");
            return gmonster.DistanceToSelf <= (double)(StartupClass.CurrentGameClass.PullDistance + num2) &&
                   (StartupClass.gprofile_0.Wander || StartupClass.gprofile_0.GetDistanceTo(gmonster.Location) <=
                       StartupClass.CurrentGameClass.PullDistance + num2)
                ? gmonster
                : null;
        }

        public static GNode GetClosestHarvestable()
        {
            var nodes = GetNodes();
            var num = 9999.0;
            GNode closestHarvestable = null;
            foreach (var gnode in nodes)
            {
                var flag = true;
                if (!StartupClass.sortedList_2.ContainsKey(gnode.GUID) &&
                    Math.Abs(gnode.Location.Z - GPlayerSelf.Me.Location.Z) <= 10.0)
                {
                    if (gnode.IsFlower && GPlayerSelf.Me.HasHerbalism)
                        flag = false;
                    if (gnode.IsMineral && GPlayerSelf.Me.HasMining)
                        flag = false;
                    if (gnode.IsTreasure)
                        flag = false;
                    if ((!flag || ConfigManager.gclass61_0.method_5("PickupJunk")) && !IsHarvestBanned(gnode.Name))
                    {
                        double distanceToSelf = gnode.Location.DistanceToSelf;
                        if (distanceToSelf < num)
                        {
                            closestHarvestable = gnode;
                            num = distanceToSelf;
                        }
                    }
                }
            }

            return closestHarvestable;
        }

        private static bool IsHarvestBanned(string ObjectName)
        {
            if (StartupClass.gclass73_0.string_1 == null)
                return false;
            foreach (var str in StartupClass.gclass73_0.string_1)
                if (ObjectName.ToLower().IndexOf(str.ToLower()) > -1)
                    return true;
            return false;
        }

        public static int StealthCountGameObjects(long SeekPlayerID)
        {
            var int5 = StartupClass.int_5;
            var num1 = 0;
            var flag = false;
            var num2 = 0;
            var hashSet = new HashSet<int>();
            if (int5 == 0)
                return 0;
            var num3 = GetFirstObjectPointer(int5);
            var num4 = num3;
            while (true)
            {
                if (++num2 > 8192)
                {
                    Logger.smethod_1("Attach probe note: stealth object traversal hit safety cap");
                    break;
                }
                if (num3 != 0 && !hashSet.Add(num3))
                {
                    if (num3 != num4)
                        Logger.smethod_1("Attach probe note: stealth traversal detected cycle");
                    break;
                }
                if ((num3 & 1) == 0 && num3 != 0 && num3 != 28)
                {
                    if (GameMemoryAccess.ReadInt64(num3 + 48, "GameObjGUID") == SeekPlayerID)
                    {
                        Logger.smethod_1("Found myself in object list (0x" + SeekPlayerID.ToString("x") + ")");
                        flag = true;
                    }

                    ++num1;
                    num3 = GameMemoryAccess.ReadInt32(num3 + 60, "GameObjNext");
                }
                else
                {
                    break;
                }
            }

            if (num1 > 0)
                Logger.smethod_1("Stealth object count: " + num1 + ", hitme = " + flag);
            return !flag ? 0 : num1;
        }

        public static bool TryGetLikelyPlayerGuid(out long guid_0)
        {
            guid_0 = 0L;
            var int5 = StartupClass.int_5;
            if (int5 == 0)
                return false;
            var num = GetFirstObjectPointer(int5);
            var num2 = num;
            var num1 = 0;
            var hashSet = new HashSet<int>();
            while (true)
            {
                if (++num1 > 8192)
                    break;
                if (num != 0 && !hashSet.Add(num))
                {
                    if (num != num2)
                        Logger.smethod_1("Attach probe note: player GUID traversal detected cycle");
                    break;
                }
                if ((num & 1) != 0 || num == 0 || num == 28)
                    break;
                if (GameMemoryAccess.ReadInt32(num + 20, "QuickType") == 4)
                {
                    var int64 = GameMemoryAccess.ReadInt64(num + 48, "GameObjGUID");
                    if (int64 != 0L)
                    {
                        guid_0 = int64;
                        return true;
                    }
                }
                num = GameMemoryAccess.ReadInt32(num + 60, "GameObjNext");
            }

            return false;
        }

        private static int GetFirstObjectPointer(int int_0)
        {
            var initialOffset = MemoryOffsetTable.Instance.GetIntOffset("InitialOffset");
            var firstObjectPointer = GameMemoryAccess.ReadInt32(int_0 + initialOffset, "GameObjFirst");
            return IsLikelyObjectPointer(firstObjectPointer) ? firstObjectPointer : 0;
        }

        private static bool IsLikelyObjectPointer(int int_0)
        {
            return (int_0 & 1) == 0 && int_0 != 0 && int_0 != 28 && int_0 >= 65536;
        }
    }
}
