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
    public enum GDecisionActionKind
    {
        None,
        EngageMonster,
        HarvestNode
    }

    public struct GDecisionAction
    {
        public GDecisionActionKind Kind;
        public GMonster Monster;
        public GNode Node;
    }

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
        private const int MinimumStableTraversalCount = 8;

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
            if (!IsVerboseMainLoopLoggingEnabled())
                return;

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

        private static bool IsVerboseMainLoopLoggingEnabled()
        {
            return ConfigManager.gclass61_0 != null && ConfigManager.gclass61_0.method_5("VerboseMainLoopLogging");
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
                            if (CanAcceptTruncatedTraversal(traversalObjectCount, priorSnapshotCount, newSnapshot.Count))
                            {
                                if (IsVerboseMainLoopLoggingEnabled())
                                    Logger.smethod_1("[WARN] Object list traversal detected cycle late in pass at 0x" + currentObjectAddress.ToString("x") + ", keeping partial snapshot");
                            }
                            else
                            {
                                Logger.LogMessage("[CRITICAL] Object list traversal aborted: detected cycle in object links at 0x" +
                                                currentObjectAddress.ToString("x"));
                                traversalComplete = false;
                            }
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
                            uint nextAddr;
                            uint rawNextAddr;
                            bool reachedListEnd;
                            if (!TryReadNextObjectPointer(currentObjectAddress, out nextAddr, out reachedListEnd, out rawNextAddr))
                            {
                                if (CanAcceptTruncatedTraversal(traversalObjectCount, priorSnapshotCount, newSnapshot.Count))
                                {
                                    Logger.smethod_1("[WARN] Skipped sentinel/invalid nodes: " + traversalSkippedCount + ", truncated pass due to implausible next pointer 0x" + rawNextAddr.ToString("x") + ", keeping partial snapshot");
                                    currentObjectAddress = 0U;
                                    break;
                                }

                                traversalComplete = false;
                                Logger.smethod_1("[CRITICAL] Skipped sentinel/invalid nodes: " + traversalSkippedCount + ", aborting due to implausible next pointer 0x" + rawNextAddr.ToString("x"));
                                break;
                            }

                            if (reachedListEnd)
                            {
                                currentObjectAddress = 0U;
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
                                if (gobject.Type == GObjectType.Monster && traversalObjectCount % 10 == 0 &&
                                    IsVerboseMainLoopLoggingEnabled())
                                    Logger.smethod_1("[TRACE] Added monster: 0x" + guid.ToString("x") + " (" + gobject.Name + ")");
                            }
                            else
                            {
                                goto label_7;
                            }
                        }
                        uint nextAddr2;
                        uint rawNextAddr2;
                        bool reachedListEnd2;
                        if (!TryReadNextObjectPointer(currentObjectAddress, out nextAddr2, out reachedListEnd2, out rawNextAddr2))
                        {
                            if (CanAcceptTruncatedTraversal(traversalObjectCount, priorSnapshotCount, newSnapshot.Count))
                            {
                                Logger.smethod_1("[WARN] Object list traversal truncated due to implausible next pointer 0x" + rawNextAddr2.ToString("x") + ", keeping partial snapshot");
                                currentObjectAddress = 0U;
                                break;
                            }

                            traversalComplete = false;
                            Logger.smethod_1("[CRITICAL] Object list traversal aborted: implausible next pointer 0x" + rawNextAddr2.ToString("x"));
                            break;
                        }

                        if (reachedListEnd2)
                        {
                            currentObjectAddress = 0U;
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
            return QuerySnapshot(BypassTimer,
                gobject => gobject.Type == ObjectType || ObjectType == GObjectType.Any,
                gobject => gobject);
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
            return QuerySnapshot(BypassTimer,
                gobject => gobject.Type == GObjectType.Player || gobject.Type == GObjectType.Monster,
                gobject => gobject as GUnit);
        }

        public static GItem[] GetItems()
        {
            return QuerySnapshot(false,
                gobject => gobject.Type == GObjectType.Item || gobject.Type == GObjectType.Container,
                gobject => gobject as GItem);
        }

        public static GMonster[] GetMonsters()
        {
            var destinationArray = QuerySnapshot(false,
                gobject => gobject.Type == GObjectType.Monster,
                gobject => gobject as GMonster);

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
            return QuerySnapshot(false,
                gobject => gobject.Type == GObjectType.Node,
                gobject => gobject as GNode);
        }

        public static GPlayer[] GetPlayers()
        {
            return QuerySnapshot(false,
                gobject => gobject.Type == GObjectType.Player,
                gobject => gobject as GPlayer);
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
                uint nextAddr3;
                uint rawNextAddr3;
                bool reachedListEnd3;
                if (!TryReadNextObjectPointer(currentObjectAddress, out nextAddr3, out reachedListEnd3, out rawNextAddr3))
                {
                    Logger.smethod_1("[CRITICAL] TryMaterializeUnitByGuid aborted: implausible next pointer 0x" + rawNextAddr3.ToString("x"));
                    break;
                }

                if (reachedListEnd3)
                    break;

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
            GUnit closestUnit = null;
            var closestDistance = 9999.0;
            foreach (var unit in GetUnits())
            {
                if (unit.TargetGUID != TargetGUID)
                    continue;

                if (unit.DistanceToSelf < closestDistance)
                {
                    closestDistance = unit.DistanceToSelf;
                    closestUnit = unit;
                }
            }

            return closestUnit;
        }

        public static GUnit FindUnitByTarget(long TargetGUID)
        {
            return FindUnitByTarget(unchecked((ulong)TargetGUID));
        }

        public static GObject FindObject(ulong GUID)
        {
            var all = GetAll();
            if (all == null)
                return null;

            GObject gobject;
            return all.TryGetValue(GUID, out gobject) ? gobject : null;
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
            return GetAttackers(true);
        }

        public static GItem[] GetEquippedItems()
        {
            return QuerySnapshot(false,
                gobject => gobject.Type == GObjectType.Item && ((GItem)gobject).IsEquipped,
                gobject => gobject as GItem);
        }

        public static GUnit[] GetAttackers(bool IncludePet)
        {
            var gunitList = new List<GUnit>();
            foreach (var unit in GetUnits())
            {
                if (IsAttacker(unit, IncludePet) && !unit.IsDead)
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
            var all = GetAll();
            return all != null && all.ContainsKey(GUID);
        }

        public static bool IsObjectPresent(long GUID)
        {
            return IsObjectPresent(unchecked((ulong)GUID));
        }

        public static GDecisionAction SelectNextAction(bool includeHarvest, int harvestRange)
        {
            var decision = new GDecisionAction
            {
                Kind = GDecisionActionKind.None,
                Monster = null,
                Node = null
            };

            GMonster targetMonster;
            if (TrySelectProfileTargetCandidate(out targetMonster))
            {
                decision.Kind = GDecisionActionKind.EngageMonster;
                decision.Monster = targetMonster;
                return decision;
            }

            if (!includeHarvest || harvestRange <= 0)
                return decision;

            GNode node;
            if (!TrySelectHarvestNodeCandidate(out node) || node.Location.DistanceToSelf > harvestRange)
                return decision;

            decision.Kind = GDecisionActionKind.HarvestNode;
            decision.Node = node;
            return decision;
        }

        private static bool TrySelectProfileTargetCandidate(out GMonster targetMonster)
        {
            targetMonster = null;
            if (StartupClass.ActiveProfile.IgnoreAttackers)
                return false;

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

                return false;
            }

            var closestDistance = 9999.0;
            GMonster skippedClosest = null;
            var skippedClosestDistance = 9999.0;
            var skippedCount = 0;
            foreach (var monster in monsters)
            {
                if (monster.IsValidProfileTarget)
                {
                    var monsterDistance = monster.DistanceToSelf;
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
                return false;

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
                targetMonster = null;
                return false;
            }

            return true;
        }

        private static bool TrySelectHarvestNodeCandidate(out GNode closestHarvestable)
        {
            closestHarvestable = null;
            var nodes = GetNodes();
            var closestDistance = 9999.0;
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
                        var distanceToNode = gnode.Location.DistanceToSelf;
                        if (distanceToNode < closestDistance)
                        {
                            closestHarvestable = gnode;
                            closestDistance = distanceToNode;
                        }
                    }
                }
            }

            return closestHarvestable != null;
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
                    uint nextAddr;
                    uint rawNextAddr;
                    bool reachedListEnd;
                    if (!TryReadNextObjectPointer(currentObjectAddress, out nextAddr, out reachedListEnd, out rawNextAddr))
                        break;
                    if (reachedListEnd)
                        break;
                    currentObjectAddress = nextAddr;
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
                uint nextAddr;
                uint rawNextAddr;
                bool reachedListEnd;
                if (!TryReadNextObjectPointer(currentObjectAddress, out nextAddr, out reachedListEnd, out rawNextAddr))
                    break;
                if (reachedListEnd)
                    break;
                currentObjectAddress = nextAddr;
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

        private static T[] QuerySnapshot<T>(bool bypassTimer, Func<GObject, bool> predicate, Func<GObject, T> map)
            where T : class
        {
            var all = GetAll(bypassTimer);
            if (all == null || all.Count == 0)
                return new T[0];

            var result = new List<T>(all.Count);
            foreach (var gobject in all.Values)
            {
                if (!predicate(gobject))
                    continue;

                var mapped = map(gobject);
                if (mapped != null)
                    result.Add(mapped);
            }

            return result.ToArray();
        }

        private static bool IsAttacker(GUnit unit, bool includePet)
        {
            if (unit == null || GContext.Main == null || GContext.Main.Me == null)
                return false;

            var player = GContext.Main.Me;
            if (unit.IsMonster && unit.TargetGUID == player.GUID)
                return true;

            if (includePet && player.PetGUID != 0UL && unit.IsMonster && unit.TargetGUID == player.PetGUID)
                return true;

            return unit.IsPlayer && unit.GUID == player.TargetGUID;
        }

        private static uint NormalizeObjectPointer(uint pointer)
        {
            return pointer & 4294967294U;
        }

        private static bool TryReadNextObjectPointer(uint currentObjectAddress, out uint nextObjectAddress, out bool reachedListEnd, out uint rawNextPointer)
        {
            if (!IsLikelyObjectPointer(currentObjectAddress) || GameObjNextOffset <= 0)
            {
                rawNextPointer = 0U;
                nextObjectAddress = 0U;
                reachedListEnd = false;
                return false;
            }

            var nextPointerAddress = currentObjectAddress + (uint)GameObjNextOffset;
            if (!GProcessMemoryManipulator.IsMemoryReadable(unchecked((int)nextPointerAddress)))
            {
                rawNextPointer = 0U;
                nextObjectAddress = 0U;
                reachedListEnd = false;
                return false;
            }

            rawNextPointer = GameMemoryAccess.ReadUInt32(currentObjectAddress + (uint)GameObjNextOffset, "GameObjNext");
            nextObjectAddress = NormalizeObjectPointer(rawNextPointer);
            reachedListEnd = nextObjectAddress == 0U || nextObjectAddress == 28U;
            return reachedListEnd || IsLikelyObjectPointer(nextObjectAddress);
        }

        private static bool CanAcceptTruncatedTraversal(int traversalObjectCount, int priorSnapshotCount, int candidateSnapshotCount)
        {
            if (traversalObjectCount < MinimumStableTraversalCount || candidateSnapshotCount < MinimumStableTraversalCount)
                return false;

            if (priorSnapshotCount <= 0)
                return true;

            return candidateSnapshotCount >= Math.Min(priorSnapshotCount, MinimumStableTraversalCount);
        }

        private static bool IsLikelyObjectPointer(uint pointer)
        {
            return (pointer & 1U) == 0U && pointer != 0U && pointer != 28U && pointer >= 65536U;
        }
    }
}

