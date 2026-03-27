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
        private static readonly SortedList<ulong, GObject> LastSnapshot = new SortedList<ulong, GObject>();
        private static int _targetScanLastLogTick;
        private static string _targetScanLastLogState;

        // Debug tracking for object list population
        private static int _lastTraversalObjectCount;
        private static int _lastTraversalEnemyCount;
        private static int _lastTraversalPlayerCount;
        private static int _lastTraversalNodeCount;
        private static int _lastTraversalSkippedCount;
        private static int _lastLoggedTick;
        private const int LogIntervalMs = 5000; // Log every 5 seconds

        /// <summary>
        /// Gets the memory offset for the next object pointer in the linked-list traversal.
        /// Centralized from hardcoded 0x3C for maintainability and version management.
        /// </summary>
        private static int GameObjNextOffset => MemoryOffsetTable.Instance.GetIntOffset("GameObjNext");

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

        /// <summary>
        /// Logs object list population debug info at throttled intervals.
        /// </summary>
        private static void LogObjectListDebugInfo()
        {
            var currentTick = Environment.TickCount;
            if (currentTick - _lastLoggedTick < LogIntervalMs)
                return;

            _lastLoggedTick = currentTick;

            var enemies = 0;
            var players = 0;
            var nodes = 0;
            var other = 0;

            lock (LastSnapshot)
            {
                foreach (var obj in LastSnapshot.Values)
                {
                    if (obj.Type == GObjectType.Monster)
                        enemies++;
                    else if (obj.Type == GObjectType.Player)
                        players++;
                    else if (obj.Type == GObjectType.Node)
                        nodes++;
                    else
                        other++;
                }
            }

            Logger.LogMessage("[DEBUG_OBJLIST] Total=" + LastSnapshot.Count + 
                            ", Enemies=" + enemies + 
                            ", Players=" + players + 
                            ", Nodes=" + nodes + 
                            ", Other=" + other);
        }

        private static SortedList<ulong, GObject> GetAll()
        {
            return GetAll(false);
        }

        private static SortedList<ulong, GObject> GetAll(bool BypassTimer)
        {
            var snapshotIsEmpty = false;
            var traversalObjectCount = 0;
            var traversalSkippedCount = 0;
            var priorSnapshotCount = 0;
            SortedList<ulong, GObject> newSnapshot = null;

            lock (LastSnapshot)
            {
                priorSnapshotCount = LastSnapshot.Count;
                if (Environment.TickCount - LastUpdate < 50 && !BypassTimer)
                    return LastSnapshot;
                ++FrameNumber;

                newSnapshot = new SortedList<ulong, GObject>(priorSnapshotCount > 0 ? priorSnapshotCount : 16);
                var objectManagerBase = StartupClass.ResolvedMainTableAddress;
                if (objectManagerBase == 0)
                {
                    Logger.LogMessage(MessageProvider.GetMessage(56));
                    StartupClass.StopGlide(true, "GetObjectsMainTableEmpty");
                    return null;
                }

                uint currentObjectAddress = unchecked((uint)GetFirstObjectPointer(objectManagerBase));
                var firstObjectAddress = currentObjectAddress;
                var iterationCount = 0;
                var visitedAddresses = new HashSet<uint>();
                var traversalComplete = true;

                if (currentObjectAddress == 0)
                {
                    Logger.LogMessage("[CRITICAL] GetAll: FirstObjectPointer returned 0");
                    traversalComplete = false;
                }

                while (true)
                {
                    if (++iterationCount > 8192)
                    {
                        Logger.LogMessage("[CRITICAL] Object list traversal aborted: exceeded safety iteration cap");
                        traversalComplete = false;
                        break;
                    }
                    if (currentObjectAddress != 0 && !visitedAddresses.Add(currentObjectAddress))
                    {
                        if (currentObjectAddress != firstObjectAddress)
                        {
                            Logger.LogMessage("[CRITICAL] Object list traversal aborted: detected cycle in object links at 0x" + 
                                            currentObjectAddress.ToString("x"));
                            traversalComplete = false;
                        }
                        break;
                    }
                    ulong guid = 0UL;
                    do
                    {
                        if (currentObjectAddress == 0)
                            goto label_15;

                        // Some builds include sentinel/invalid nodes in the linked list. These must be skipped,
                        // not treated as end-of-list, or the snapshot will be incomplete/empty.
                    if (currentObjectAddress == 28U || (currentObjectAddress & 1U) != 0U)
                        {
                            ++traversalSkippedCount;
                            // Read next pointer as uint and validate before advancing to avoid ReadBytesInternal errors
                            var nextAddr = GameMemoryAccess.ReadUInt32(currentObjectAddress + (uint)GameObjNextOffset, "GameObjNext");
                            if (!IsLikelyObjectPointer(nextAddr))
                            {
                                // Abort traversal if the next pointer is implausible
                                traversalComplete = false;
                                Logger.smethod_1("[TRACE] Skipped sentinel/invalid nodes: " + traversalSkippedCount + ", aborting due to implausible next pointer 0x" + nextAddr.ToString("x"));
                                break;
                            }

                            currentObjectAddress = nextAddr;

                            // Log sentinel skips occasionally
                            if (traversalSkippedCount % 100 == 0)
                                Logger.smethod_1("[TRACE] Skipped sentinel/invalid nodes: " + traversalSkippedCount);
                            continue;
                        }

                        if ((currentObjectAddress & 1U) == 0U)
                        {
                            guid = QuickGetGUID(currentObjectAddress);
                            if (!newSnapshot.ContainsKey(guid))
                            {
                                ++traversalObjectCount;
                                GObject gobject;
                                if (guid == StartupClass.CurrentPlayerGuid)
                                {
                                    gobject = new GPlayerSelf(currentObjectAddress, FrameNumber);
                                    if (GContext.Main.Me == null)
                                        GContext.Main.Me = (GPlayerSelf)gobject;
                                }
                                else
                                {
                                    gobject = GObject.Create(currentObjectAddress, FrameNumber);
                                }

                                newSnapshot.Add(guid, gobject);

                                // Log enemy additions
                                if (gobject.Type == GObjectType.Monster && traversalObjectCount % 10 == 0)
                                    Logger.smethod_1("[TRACE] Added monster: 0x" + guid.ToString("x") + " (" + gobject.Name + ")");
                            }
                            else
                            {
                                goto label_7;
                            }
                        }
                        // Read next pointer as uint and validate before advancing to avoid ReadBytesInternal errors
                        var nextAddr2 = GameMemoryAccess.ReadUInt32(currentObjectAddress + (uint)GameObjNextOffset, "GameObjNext");
                        if (!IsLikelyObjectPointer(nextAddr2))
                        {
                            traversalComplete = false;
                            Logger.smethod_1("[CRITICAL] Object list traversal aborted: implausible next pointer 0x" + nextAddr2.ToString("x"));
                            break;
                        }

                        currentObjectAddress = nextAddr2;
                    } while (!LogObjects);

                    goto label_13;
                label_7:
                    var existingObject = newSnapshot[guid];
                    if (existingObject.BaseAddress != currentObjectAddress)
                    {
                        existingObject.BaseAddress = unchecked((uint)currentObjectAddress);
                        existingObject.StorageAddress = unchecked((uint)GameMemoryAccess.ReadInt32(currentObjectAddress + 8, "GameObjStorage"));
                    }

                    existingObject.FrameNumber = FrameNumber;
                    continue;
                label_13:
                    Logger.smethod_1("+ Adding new object: " + newSnapshot[guid]);
                }

                var activePlayerOffset = MemoryOffsetTable.Instance.HasOffset("MainTableActivePlayer")
                    ? MemoryOffsetTable.Instance.GetIntOffset("MainTableActivePlayer")
                    : 24;
                if (objectManagerBase != 0 && activePlayerOffset > 0)
                {
                    var activePlayerObjectAddress = GameMemoryAccess.ReadInt32(objectManagerBase + activePlayerOffset, "MainTableActivePlayerObj");
                    if (IsLikelyObjectPointer(activePlayerObjectAddress))
                    {
                        var activePlayerGuid = QuickGetGUID(unchecked((uint)activePlayerObjectAddress));
                        if (activePlayerGuid != 0UL)
                        {
                            StartupClass.CurrentPlayerGuid = activePlayerGuid;
                            if (!newSnapshot.ContainsKey(activePlayerGuid) || !(newSnapshot[activePlayerGuid] is GPlayerSelf))
                            {
                                var gobject = new GPlayerSelf(unchecked((uint)activePlayerObjectAddress), FrameNumber);
                                if (newSnapshot.ContainsKey(activePlayerGuid))
                                    newSnapshot.Remove(activePlayerGuid);
                                newSnapshot.Add(activePlayerGuid, gobject);
                            }
                            else
                            {
                                var activePlayer = newSnapshot[activePlayerGuid];
                                if (activePlayer.BaseAddress != activePlayerObjectAddress)
                                {
                                    activePlayer.BaseAddress = unchecked((uint)activePlayerObjectAddress);
                                    activePlayer.StorageAddress = unchecked((uint)GameMemoryAccess.ReadInt32(unchecked((uint)activePlayerObjectAddress) + 8, "GameObjStorage"));
                                }

                                activePlayer.FrameNumber = FrameNumber;
                            }

                            GContext.Main.Me = (GPlayerSelf)newSnapshot[activePlayerGuid];
                        }
                    }
                }

            label_15:
                // If the traversal produced an implausibly tiny snapshot compared to the previous tick,
                // assume the OM links were unstable for this pass and preserve the prior snapshot.
                // This prevents a single transient read failure from collapsing targeting to "2 objects".
                if (traversalComplete && priorSnapshotCount >= 50 && newSnapshot.Count <= 10)
                {
                    traversalComplete = false;
                    Logger.smethod_1("Object snapshot marked incomplete: tiny result (" + newSnapshot.Count + ") vs prior (" + priorSnapshotCount + ")");
                }

                if (traversalComplete)
                {
                    // Cull old objects from the previous snapshot before swapping.
                    // This preserves prior entities when traversal is incomplete.
                    ulong playerTargetGuid = 0UL;
                    if (GContext.Main != null && GContext.Main.Me != null)
                        playerTargetGuid = GContext.Main.Me.TargetGUID;

                    for (var index = 0; index < LastSnapshot.Count; ++index)
                    {
                        var gobject = LastSnapshot.Values[index];
                        if (gobject.FrameNumber != FrameNumber)
                        {
                            if (unchecked(FrameNumber - gobject.FrameNumber) <= 1)
                                continue;

                            if (playerTargetGuid != 0UL && gobject.GUID == playerTargetGuid)
                            {
                                gobject.FrameNumber = FrameNumber;
                                continue;
                            }

                            if (gobject is GPlayerSelf && gobject.GUID == StartupClass.CurrentPlayerGuid)
                            {
                                gobject.FrameNumber = FrameNumber;
                                continue;
                            }

                            if (gobject is GPlayerSelf || gobject.GUID == StartupClass.CurrentPlayerGuid)
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

                    LastSnapshot.Clear();
                    for (var index = 0; index < newSnapshot.Count; ++index)
                        LastSnapshot.Add(newSnapshot.Keys[index], newSnapshot.Values[index]);
                }
                else
                {
                    Logger.smethod_1("Object snapshot cull skipped due to incomplete traversal; preserving prior entities for next tick");
                }

                LastUpdate = Environment.TickCount;
                if (traversalComplete && newSnapshot.Count == 0)
                    snapshotIsEmpty = true;

                // Store metrics for debug logging
                _lastTraversalObjectCount = traversalObjectCount;
                _lastTraversalSkippedCount = traversalSkippedCount;
            }

            if (snapshotIsEmpty)
                StartupClass.StopGlide(true, "ZeroCountObjects");

            // Log periodic summary
            LogObjectListDebugInfo();

            return LastSnapshot;
        }

        private static GObject[] GetObjects(GObjectType ObjectType)
        {
            return GetObjects(ObjectType, false);
        }

        private static GObject[] GetObjects(GObjectType ObjectType, bool BypassTimer)
        {
            lock (LastSnapshot)
            {
                var all = GetAll(BypassTimer);
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
            return GetUnits(false);
        }

        private static GUnit[] GetUnits(bool BypassTimer)
        {
            var objects = GetObjects(GObjectType.Any, BypassTimer);
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

            // Debug log if monster count drops unexpectedly
            if (destinationArray.Length > 0 && Environment.TickCount - _lastLoggedTick < LogIntervalMs)
            {
                var validMonsters = 0;
                foreach (var monster in destinationArray)
                {
                    if (monster != null && monster.IsValid)
                        validMonsters++;
                }

                if (validMonsters < destinationArray.Length)
                    Logger.smethod_1("[TRACE] Monsters: Total=" + destinationArray.Length + 
                                   ", Valid=" + validMonsters + 
                                   ", Invalid=" + (destinationArray.Length - validMonsters));
            }

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

        private static ulong QuickGetGUID(uint BaseAddress)
        {
            return GameMemoryAccess.ReadObjectGuid(BaseAddress);
        }

        public static GUnit ResolveUnitByGuid(ulong GUID)
        {
            if (GUID == 0UL)
                return null;

            GUnit resolvedUnit;
            if (TryResolveUnitByGuidFromSnapshot(GUID, false, out resolvedUnit))
                return resolvedUnit;
            if (TryResolveUnitByGuidFromSnapshot(GUID, true, out resolvedUnit))
                return resolvedUnit;
            if (TryMaterializeUnitByGuid(GUID, out resolvedUnit))
                return resolvedUnit;

            Logger.LogMessage("[CRITICAL] ResolveUnitByGuid failed for GUID=0x" + GUID.ToString("x") +
                              ". Target exists in player memory but is missing from object snapshot.");

            return null;
        }

        /// <summary>
        /// Resolves the current target for a player from a single GUID snapshot.
        /// </summary>
        public static GUnit ResolveCurrentTarget(GPlayerSelf player, out ulong targetGuid)
        {
            targetGuid = 0UL;
            if (player == null)
                return null;

            targetGuid = player.TargetGUID;
            return ResolveUnitByGuid(targetGuid);
        }

        public static GUnit FindUnit(ulong GUID)
        {
            return ResolveUnitByGuid(GUID);
        }

        private static bool TryResolveUnitByGuidFromSnapshot(ulong guid, bool bypassTimer, out GUnit unit)
        {
            unit = null;

            var snapshot = GetAll(bypassTimer);
            if (snapshot == null || !snapshot.ContainsKey(guid))
                return false;

            var obj = snapshot[guid];
            unit = obj as GUnit;
            if (unit != null)
                return true;

            var refreshed = GObject.Create(obj.BaseAddress, FrameNumber);
            snapshot[guid] = refreshed;
            unit = refreshed as GUnit;
            if (unit != null)
                return true;

            Logger.LogMessage("[CRITICAL] ResolveUnitByGuid found GUID=0x" + guid.ToString("x") +
                              " but object is non-unit. Type=" + refreshed.Type + ", Class=" + refreshed.GetType().Name +
                              ", Base=0x" + refreshed.BaseAddress.ToString("x") +
                              ", Storage=0x" + refreshed.StorageAddress.ToString("x"));
            return false;
        }

        private static bool TryMaterializeUnitByGuid(ulong guid, out GUnit unit)
        {
            unit = null;

            var objectManagerBase = StartupClass.ResolvedMainTableAddress;
            if (objectManagerBase == 0)
                return false;

            var currentObjectAddress = GetFirstObjectPointer(objectManagerBase);
            var firstObjectAddress = currentObjectAddress;
            var iterationCount = 0;
            var visitedAddresses = new HashSet<uint>();
            var checkCount = 0;
            var readFailures = 0;

            while ((currentObjectAddress & 1U) == 0U && currentObjectAddress != 0U && currentObjectAddress != 28U)
            {
                if (++iterationCount > 8192)
                {
                    Logger.LogMessage("[CRITICAL] TryMaterializeUnitByGuid: Iteration cap hit for GUID=0x" + guid.ToString("x"));
                    break;
                }

                if (!visitedAddresses.Add(currentObjectAddress))
                {
                    if (currentObjectAddress != firstObjectAddress)
                        Logger.smethod_1("ResolveUnitByGuid materialize traversal detected cycle");
                    break;
                }

                ++checkCount;
                var storageGuid = GameMemoryAccess.ReadObjectGuid(currentObjectAddress);
                if (storageGuid == 0UL)
                    ++readFailures;

                var legacyGuid = GameMemoryAccess.ReadInt64(currentObjectAddress + 48, "GameObjGUID.Legacy");
                if (storageGuid == guid || legacyGuid == guid)
                {
                    var obj = GObject.Create((uint)currentObjectAddress, FrameNumber);
                    lock (LastSnapshot)
                    {
                        if (LastSnapshot.ContainsKey(obj.GUID))
                            LastSnapshot[obj.GUID] = obj;
                        else
                            LastSnapshot.Add(obj.GUID, obj);
                    }

                    unit = obj as GUnit;
                    if (unit != null)
                    {
                        if (obj.GUID != guid)
                            Logger.LogMessage("[CRITICAL] ResolveUnitByGuid GUID mismatch while materializing. Requested=0x" + guid.ToString("x") +
                                              ", Materialized=0x" + obj.GUID.ToString("x") +
                                              ", StorageGUID=0x" + storageGuid.ToString("x") +
                                              ", LegacyGUID=0x" + legacyGuid.ToString("x") +
                                              ", Base=0x" + obj.BaseAddress.ToString("x") +
                                              ", Storage=0x" + obj.StorageAddress.ToString("x"));
                        return true;
                    }

                    Logger.LogMessage("[CRITICAL] ResolveUnitByGuid matched raw object but materialized non-unit. Requested=0x" + guid.ToString("x") +
                                      ", MaterializedType=" + obj.Type +
                                      ", Class=" + obj.GetType().Name +
                                      ", StorageGUID=0x" + storageGuid.ToString("x") +
                                      ", LegacyGUID=0x" + legacyGuid.ToString("x") +
                                      ", Base=0x" + obj.BaseAddress.ToString("x") +
                                      ", Storage=0x" + obj.StorageAddress.ToString("x"));
                    return false;
                }

                // Ensure we read the next pointer using uint arithmetic and validate it
                var nextAddr3 = GameMemoryAccess.ReadUInt32(currentObjectAddress + (uint)GameObjNextOffset, "GameObjNext");
                if (!IsLikelyObjectPointer(nextAddr3))
                {
                    Logger.smethod_1("[CRITICAL] TryMaterializeUnitByGuid aborted: implausible next pointer 0x" + nextAddr3.ToString("x"));
                    break;
                }

                currentObjectAddress = nextAddr3;
            }

            // Log if we had read failures during traversal
            if (readFailures > 0)
                Logger.smethod_1("[TRACE] TryMaterializeUnitByGuid: GUID=0x" + guid.ToString("x") + 
                               " not found. Checked=" + checkCount + 
                               ", ReadFailures=" + readFailures);

            return false;
        }

        public static GUnit FindUnit(long GUID)
        {
            return FindUnit(unchecked((ulong)GUID));
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

        public static GUnit FindUnitByTarget(ulong TargetGUID)
        {
            var units = GetUnits();
            var gunitList = new List<GUnit>();
            foreach (var gunit in units)
                if (gunit.TargetGUID == TargetGUID)
                    gunitList.Add(gunit);
            return gunitList.Count == 0 ? null : (GUnit)GetClosest(gunitList.ToArray());
        }

        public static GUnit FindUnitByTarget(long TargetGUID)
        {
            return FindUnitByTarget(unchecked((ulong)TargetGUID));
        }

        public static GObject FindObject(ulong GUID)
        {
            foreach (var gobject in GetObjects())
                if (gobject.GUID == GUID)
                    return gobject;
            return null;
        }

        public static GObject FindObject(long GUID)
        {
            return FindObject(unchecked((ulong)GUID));
        }

        public static GMonster GetNearestHostile()
        {
            return GetNearestHostile(GContext.Main.Me.Location, 0UL, false);
        }

        public static GMonster GetNearestHostile(
            GLocation Location,
            ulong ExcludeGUID,
            bool IncludeInjured)
        {
            var monsters = GetMonsters();
            GMonster nearestHostile = null;
            var closestDistance = 9999.0;
            foreach (var gmonster in monsters)
                if ((IncludeInjured || gmonster.Health == 1.0) && gmonster.IsValid && !gmonster.IsTrivial &&
                    gmonster.GUID != ExcludeGUID &&
                    (gmonster.Reaction == GReaction.Hostile || gmonster.Reaction == GReaction.Unknown) &&
                    gmonster.Location.GetDistanceTo(Location) < closestDistance &&
                    Math.Abs(gmonster.Location.Z - Location.Z) < 12.0)
                {
                    closestDistance = gmonster.Location.GetDistanceTo(Location);
                    nearestHostile = gmonster;
                }

            return nearestHostile;
        }

        public static GMonster GetNearestHostile(GLocation Location, long ExcludeGUID, bool IncludeInjured)
        {
            return GetNearestHostile(Location, unchecked((ulong)ExcludeGUID), IncludeInjured);
        }

        public static bool CheckForAttackers()
        {
            var attackers = GetAttackers(false);
            var closestDistance = 9999.0;
            foreach (var gunit in attackers)
                if (gunit.DistanceToSelf < closestDistance && !gunit.IsDead)
                    return true;
            return false;
        }

        public static GUnit GetNearestAttacker(ulong ExcludeGUID)
        {
            var attackers = GetAttackers(true);
            GUnit nearestAttacker = null;
            var closestDistance = 9999.0;
            foreach (var gunit in attackers)
                if (gunit.DistanceToSelf < closestDistance && gunit.GUID != ExcludeGUID && !gunit.IsDead)
                {
                    closestDistance = gunit.DistanceToSelf;
                    nearestAttacker = gunit;
                }

            return nearestAttacker;
        }

        public static GUnit GetNearestAttacker(long ExcludeGUID)
        {
            return GetNearestAttacker(unchecked((ulong)ExcludeGUID));
        }

        public static GUnit[] GetAttackers()
        {
            var gunitList = new List<GUnit>();
            foreach (var unit in GetUnits())
            {
                var flag = false;
                if (unit.IsMonster && unit.TargetGUID == GContext.Main.Me.GUID)
                    flag = true;
                if (GContext.Main.Me.PetGUID != 0UL && unit.IsMonster && unit.TargetGUID == GContext.Main.Me.PetGUID)
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
                if (IncludePet && GContext.Main.Me.PetGUID != 0UL && unit.IsMonster &&
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
            var objectCount = 0;
            var unitCount = 0;
            lock (LastSnapshot)
            {
                Logger.smethod_1("-- GObjectList.DumpDebug invoked, LastUpdate = " + LastUpdate + ", Current = " +
                                   Environment.TickCount);
                var lastSnapshot = LastSnapshot;
                foreach (var key in lastSnapshot.Keys)
                {
                    ++objectCount;
                    Logger.smethod_1(key.ToString("x16") + " --> " + lastSnapshot[key]);
                }

                Logger.smethod_1("-- Object dump done, hits: " + objectCount + ", unit hits: " + unitCount);
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
                        Math.Abs(gmonster.Location.Z - GContext.Main.Me.Location.Z) < 10.0 && gmonster.TargetGUID == 0UL)
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
            var closestDistance = 99999.0;
            GObject closest = null;
            foreach (var gobject in Objects)
                if (gobject.Location.GetDistanceTo(Location) < closestDistance)
                {
                    closestDistance = gobject.Location.GetDistanceTo(Location);
                    closest = gobject;
                }

            return closest;
        }

        public static GMonster GetClosestNeutralMonster()
        {
            var monsters = GetMonsters();
            var closestDistance = 99999.0;
            GMonster closestNeutralMonster = null;
            foreach (var gmonster in monsters)
                if (gmonster.Reaction == GReaction.Neutral && gmonster.Level > 5 &&
                    Math.Abs(gmonster.Location.Z - GContext.Main.Me.Location.Z) < 10.0 && gmonster.Health == 1.0 &&
                    gmonster.DistanceToSelf < closestDistance)
                {
                    closestDistance = gmonster.DistanceToSelf;
                    closestNeutralMonster = gmonster;
                }

            return closestNeutralMonster;
        }

        public static bool IsObjectPresent(ulong GUID)
        {
            return GetAll().ContainsKey(GUID);
        }

        public static bool IsObjectPresent(long GUID)
        {
            return IsObjectPresent(unchecked((ulong)GUID));
        }

        public static GMonster GetNextProfileTarget()
        {
            if (StartupClass.ActiveProfile.IgnoreAttackers)
                return null;
            var monsters = GetMonsters();
            if (monsters == null || monsters.Length == 0)
            {
                var emptyState = "nomonsters";
                var nowTick = Environment.TickCount;
                if (_targetScanLastLogState != emptyState || nowTick - _targetScanLastLogTick > 2500)
                {
                    _targetScanLastLogState = emptyState;
                    _targetScanLastLogTick = nowTick;
                    Logger.LogMessage("[Loop] skipping target scan: no monsters found in current object snapshot.");
                }

                return null;
            }
            var closestDistance = 9999.0;
            GMonster targetMonster = null;
            GMonster skippedClosest = null;
            var skippedClosestDistance = 9999.0;
            var skippedCount = 0;
            foreach (var monster in monsters)
                if (monster.IsValidProfileTarget)
                {
                    double monsterDistance = monster.DistanceToSelf;
                    if (monsterDistance < closestDistance)
                    {
                        closestDistance = monsterDistance;
                        targetMonster = monster;
                    }
                }
                else
                {
                    ++skippedCount;
                    var skipDistance = monster.DistanceToSelf;
                    if (skipDistance < skippedClosestDistance)
                    {
                        skippedClosestDistance = skipDistance;
                        skippedClosest = monster;
                    }
                }

            if (targetMonster == null && ConfigManager.gclass61_0.method_5("LogMonsterChecks"))
                Logger.smethod_1("FindClosestToMe is returning null, nobody worth killing right now");
            if (targetMonster == null && skippedClosest != null)
            {
                var state = "skip:" + skippedClosest.SkipReason + ":" + skippedCount;
                var nowTick = Environment.TickCount;
                if (_targetScanLastLogState != state || nowTick - _targetScanLastLogTick > 2500)
                {
                    _targetScanLastLogState = state;
                    _targetScanLastLogTick = nowTick;
                    Logger.LogMessage("[Loop] skipping target candidates: count=" + skippedCount +
                                      ", closest=\"" + skippedClosest.Name + "\"" +
                                      ", guid=0x" + skippedClosest.GUID.ToString("x") +
                                      ", dist=" + Math.Round(skippedClosest.DistanceToSelf, 2) +
                                      ", faction=" + skippedClosest.FactionID +
                                      ", hp=" + skippedClosest.HealthPoints + "/" + skippedClosest.HealthMax +
                                      ", target=0x" + skippedClosest.TargetGUID.ToString("x") +
                                      ", reason=\"" + skippedClosest.SkipReason + "\"");
                }
            }
            if (targetMonster == null)
                return null;
            var extraPullDistance = ConfigManager.gclass61_0.method_3("ExtraPull");
            var withinPull = targetMonster.DistanceToSelf <= (double)(StartupClass.CurrentGameClass.PullDistance + extraPullDistance);
            var withinProfile = StartupClass.ActiveProfile.Wander ||
                                StartupClass.ActiveProfile.GetDistanceTo(targetMonster.Location) <=
                                StartupClass.CurrentGameClass.PullDistance + extraPullDistance;
            if (!withinPull || !withinProfile)
            {
                var state = "envelope:" + targetMonster.GUID.ToString("x");
                var nowTick = Environment.TickCount;
                if (_targetScanLastLogState != state || nowTick - _targetScanLastLogTick > 2500)
                {
                    _targetScanLastLogState = state;
                    _targetScanLastLogTick = nowTick;
                    Logger.LogMessage("[Loop] skipping target \"" + targetMonster.Name + "\"" +
                                      ", guid=0x" + targetMonster.GUID.ToString("x") +
                                      ", dist=" + Math.Round(targetMonster.DistanceToSelf, 2) +
                                      ", pullMax=" + (StartupClass.CurrentGameClass.PullDistance + extraPullDistance) +
                                      ", profileDist=" + Math.Round(StartupClass.ActiveProfile.GetDistanceTo(targetMonster.Location), 2) +
                                      ", profileMax=" + (StartupClass.CurrentGameClass.PullDistance + extraPullDistance) +
                                      ", reason=\"outside pull/profile envelope\"");
                }
                return null;
            }

            return targetMonster;
        }

        public static GNode GetClosestHarvestable()
        {
            var nodes = GetNodes();
            var closestDistance = 9999.0;
            GNode closestHarvestable = null;
            foreach (var gnode in nodes)
            {
                var canHarvest = true;
                if (!StartupClass.RuntimeProfileCache.ContainsKey(gnode.GUID) &&
                    Math.Abs(gnode.Location.Z - GPlayerSelf.Me.Location.Z) <= 10.0)
                {
                    if (gnode.IsFlower && GPlayerSelf.Me.HasHerbalism)
                        canHarvest = false;
                    if (gnode.IsMineral && GPlayerSelf.Me.HasMining)
                        canHarvest = false;
                    if (gnode.IsTreasure)
                        canHarvest = false;
                    if ((!canHarvest || ConfigManager.gclass61_0.method_5("PickupJunk")) && !IsHarvestBanned(gnode.Name))
                    {
                        double distanceToNode = gnode.Location.DistanceToSelf;
                        if (distanceToNode < closestDistance)
                        {
                            closestHarvestable = gnode;
                            closestDistance = distanceToNode;
                        }
                    }
                }
            }

            return closestHarvestable;
        }

        private static bool IsHarvestBanned(string ObjectName)
        {
            if (StartupClass.ActiveCombatController.string_1 == null)
                return false;
            foreach (var str in StartupClass.ActiveCombatController.string_1)
                if (ObjectName.ToLower().IndexOf(str.ToLower()) > -1)
                    return true;
            return false;
        }

        public static int StealthCountGameObjects(ulong SeekPlayerID)
        {
            var int5 = StartupClass.ResolvedMainTableAddress;
            var foundObjectCount = 0;
            var playerFound = false;
            var traversalIterations = 0;
            var visitedAddresses = new HashSet<uint>();
            if (int5 == 0)
                return 0;
            var currentObjectAddress = GetFirstObjectPointer(int5);
            var firstObjectAddress = currentObjectAddress;
            while (true)
            {
                if (++traversalIterations > 8192)
                {
                    Logger.smethod_1("Attach probe note: stealth object traversal hit safety cap");
                    break;
                }
                if (currentObjectAddress != 0 && !visitedAddresses.Add(currentObjectAddress))
                {
                    if (currentObjectAddress != firstObjectAddress)
                        Logger.smethod_1("Attach probe note: stealth traversal detected cycle");
                    break;
                }
                if ((currentObjectAddress & 1) == 0 && currentObjectAddress != 0 && currentObjectAddress != 28)
                {
                    if (GameMemoryAccess.ReadObjectGuid(currentObjectAddress) == SeekPlayerID)
                    {
                        Logger.smethod_1("Found myself in object list (0x" + SeekPlayerID.ToString("x") + ")");
                        playerFound = true;
                    }

                    ++foundObjectCount;
                    currentObjectAddress = GameMemoryAccess.ReadUInt32(currentObjectAddress + (uint)GameObjNextOffset, "GameObjNext");
                }
                else
                {
                    break;
                }
            }

            if (foundObjectCount > 0)
                Logger.smethod_1("Stealth object count: " + foundObjectCount + ", hitme = " + playerFound);
            return !playerFound ? 0 : foundObjectCount;
        }

        public static bool TryGetLikelyPlayerGuid(out ulong guid_0)
        {
            guid_0 = 0UL;
            var int5 = StartupClass.ResolvedMainTableAddress;
            if (int5 == 0)
                return false;
            var currentObjectAddress = GetFirstObjectPointer(int5);
            var firstObjectAddress = currentObjectAddress;
            var traversalIterations = 0;
            var visitedAddresses = new HashSet<uint>();
            while (true)
            {
                if (++traversalIterations > 8192)
                    break;
                if (currentObjectAddress != 0 && !visitedAddresses.Add(currentObjectAddress))
                {
                    if (currentObjectAddress != firstObjectAddress)
                        Logger.smethod_1("Attach probe note: player GUID traversal detected cycle");
                    break;
                }
                if ((currentObjectAddress & 1) != 0 || currentObjectAddress == 0 || currentObjectAddress == 28)
                    break;
                if (GameMemoryAccess.ReadInt32(currentObjectAddress + 20, "QuickType") == 4)
                {
                    var playerGuid = GameMemoryAccess.ReadObjectGuid(currentObjectAddress);
                    if (playerGuid != 0UL)
                    {
                        guid_0 = playerGuid;
                        return true;
                    }
                }
                currentObjectAddress = GameMemoryAccess.ReadUInt32(currentObjectAddress + (uint)GameObjNextOffset, "GameObjNext");
            }

            return false;
        }

        private static uint GetFirstObjectPointer(int int_0)
        {
            var initialOffset = MemoryOffsetTable.Instance.GetIntOffset("InitialOffset");
            var firstObjectPointer = GameMemoryAccess.ReadInt32(int_0 + initialOffset, "GameObjFirst");
            return IsLikelyObjectPointer(firstObjectPointer) ? unchecked((uint)firstObjectPointer) : 0U;
        }

        private static bool IsLikelyObjectPointer(int int_0)
        {
            var pointer = unchecked((uint)int_0);
            return IsLikelyObjectPointer(pointer);
        }

        private static bool IsLikelyObjectPointer(uint pointer)
        {
            return (pointer & 1U) == 0U && pointer != 0U && pointer != 28U && pointer >= 65536U;
        }
    }
}

