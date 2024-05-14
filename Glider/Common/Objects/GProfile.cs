// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GProfile
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Glider.Common.Objects
{
    public class GProfile
    {
        private const double BREADCRUMB_PLACE_YARDS = 18.3;
        private const double BREADCRUMB_START_YARDS = 27.6;
        private const double WANDER_SHORT_YARDS = 99.0;
        private const double SKIP2_DISTANCE = 25.0;
        private const double SKIP_DISTANCE = 25.0;
        public bool AllowShortCircuit;
        public string[] AvoidList;
        public bool Beach;
        public List<long> Blacklist;
        public bool BlacklistOn;
        public Stack<GLocation> Breadcrumbs;
        public int CurrentIndex;
        public int[] Factions;
        public bool Fishing;
        public List<GLocation> GhostWaypoints;
        public bool IgnoreAttackers;
        public int LureMinutes;
        public int MaxLevel;
        public int MinLevel;
        public bool NaturalRun;
        public bool OneShot;
        public bool OneShotHit;
        public int OneShotStepCheck;
        public bool ReverseWaypoints;
        public bool Reversible;
        public bool RunFromAvoids;
        public bool SkipWaypoints;
        public bool UseBreadcrumbs;
        public string VendorAR;
        public string VendorFW;
        public string VendorRepair;
        public List<GLocation> VendorWaypoints;
        public bool Wander;
        public List<GLocation> Waypoints;

        public GProfile()
        {
            CurrentIndex = 0;
            MinLevel = 0;
            MaxLevel = 0;
            LureMinutes = 0;
            Factions = null;
            Waypoints = new List<GLocation>();
            GhostWaypoints = new List<GLocation>();
            VendorWaypoints = new List<GLocation>();
            Blacklist = new List<long>();
            BlacklistOn = false;
            Fishing = false;
            NaturalRun = true;
            SkipWaypoints = false;
            ReverseWaypoints = false;
            Beach = false;
            Wander = false;
            AvoidList = null;
            RunFromAvoids = false;
            OneShot = false;
            OneShotHit = false;
            OneShotStepCheck = 0;
            IgnoreAttackers = false;
            UseBreadcrumbs = false;
            AllowShortCircuit = true;
            Breadcrumbs = new Stack<GLocation>();
            VendorAR = null;
            VendorFW = null;
            VendorRepair = null;
            Name = "(no profile loaded yet)";
        }

        public string Name { get; private set; }

        public GLocation CurrentWaypoint => Breadcrumbs.Count > 0 ? Breadcrumbs.Peek() : Waypoints[CurrentIndex];

        public string ScriptOverride => null;

        public bool IsVendorEnabled => VendorWaypoints.Count > 0 && VendorAR != null && VendorFW != null;

        public bool AllCoordsHaveZ()
        {
            foreach (var waypoint in Waypoints)
                if (!waypoint.HasZ)
                    return false;
            foreach (var ghostWaypoint in GhostWaypoints)
                if (!ghostWaypoint.HasZ)
                    return false;
            foreach (var vendorWaypoint in VendorWaypoints)
                if (!vendorWaypoint.HasZ)
                    return false;
            return true;
        }

        public void Save(string OutputFile)
        {
            Name = OutputFile;
            GClass37.smethod_0(GClass30.smethod_2(768, OutputFile));
            var xmlDocument_0 = new XmlDocument();
            xmlDocument_0.AppendChild(xmlDocument_0.CreateXmlDeclaration("1.0", null, null));
            xmlDocument_0.AppendChild(xmlDocument_0.CreateElement("GlideProfile"));
            AddToOutput(xmlDocument_0, "MinLevel", MinLevel.ToString());
            AddToOutput(xmlDocument_0, "MaxLevel", MaxLevel.ToString());
            AddToOutput(xmlDocument_0, "Factions", GetFactionsAsString());
            AddToOutput(xmlDocument_0, "LureMinutes", LureMinutes.ToString());
            if (AllowShortCircuit)
                AddToOutput(xmlDocument_0, "AllowShortCircuit", "True");
            if (UseBreadcrumbs)
                AddToOutput(xmlDocument_0, "UseBreadcrumbs", "True");
            if (Reversible)
                AddToOutput(xmlDocument_0, "Reversible", "True");
            if (Fishing)
                AddToOutput(xmlDocument_0, "Fishing", "True");
            if (Beach)
                AddToOutput(xmlDocument_0, "Beach", "True");
            if (BlacklistOn)
                AddToOutput(xmlDocument_0, "BlacklistOn", "True");
            if (SkipWaypoints)
                AddToOutput(xmlDocument_0, "SkipWaypoints", "True");
            if (ReverseWaypoints)
                AddToOutput(xmlDocument_0, "ReverseWaypoints", "True");
            if (Wander)
                AddToOutput(xmlDocument_0, "Wander", "True");
            if (RunFromAvoids)
                AddToOutput(xmlDocument_0, "RunFromAvoids", "True");
            if (OneShot)
                AddToOutput(xmlDocument_0, "OneShot", "True");
            if (IgnoreAttackers)
                AddToOutput(xmlDocument_0, "IgnoreAttackers", "True");
            if (VendorFW != null)
                AddToOutput(xmlDocument_0, "VendorFW", VendorFW);
            if (VendorAR != null)
                AddToOutput(xmlDocument_0, "VendorAR", VendorAR);
            if (VendorRepair != null)
                AddToOutput(xmlDocument_0, "VendorRepair", VendorRepair);
            AddToOutput(xmlDocument_0, "NaturalRun", NaturalRun.ToString());
            if (AvoidList != null)
                foreach (var avoid in AvoidList)
                    AddToOutput(xmlDocument_0, "Avoid", avoid);
            foreach (var waypoint in Waypoints)
                AddToOutput(xmlDocument_0, "Waypoint", waypoint.ToString3D());
            foreach (var ghostWaypoint in GhostWaypoints)
                AddToOutput(xmlDocument_0, "GhostWaypoint", ghostWaypoint.ToString3D());
            foreach (var vendorWaypoint in VendorWaypoints)
                AddToOutput(xmlDocument_0, "VendorWaypoint", vendorWaypoint.ToString3D());
            xmlDocument_0.Save(OutputFile);
        }

        public bool Load(string InputFile)
        {
            try
            {
                Waypoints.Clear();
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(InputFile);
                Reversible = xmlDocument.SelectSingleNode("/GlideProfile/Reversible") != null;
                var xmlNode1 = xmlDocument.SelectSingleNode("/GlideProfile/LureMinutes");
                if (xmlNode1 != null)
                    LureMinutes = int.Parse(xmlNode1.InnerText);
                Fishing = xmlDocument.SelectSingleNode("/GlideProfile/Fishing") != null;
                BlacklistOn = xmlDocument.SelectSingleNode("/GlideProfile/BlacklistOn") != null;
                ReverseWaypoints = xmlDocument.SelectSingleNode("/GlideProfile/ReverseWaypoints") != null;
                SkipWaypoints = xmlDocument.SelectSingleNode("/GlideProfile/SkipWaypoints") != null;
                Beach = xmlDocument.SelectSingleNode("/GlideProfile/Beach") != null;
                Wander = xmlDocument.SelectSingleNode("/GlideProfile/Wander") != null;
                RunFromAvoids = xmlDocument.SelectSingleNode("/GlideProfile/RunFromAvoids") != null;
                OneShot = xmlDocument.SelectSingleNode("/GlideProfile/OneShot") != null;
                IgnoreAttackers = xmlDocument.SelectSingleNode("/GlideProfile/IgnoreAttackers") != null;
                UseBreadcrumbs = xmlDocument.SelectSingleNode("/GlideProfile/UseBreadcrumbs") != null;
                AllowShortCircuit = xmlDocument.SelectSingleNode("/GlideProfile/AllowShortCircuit") != null;
                var xmlNode2 = xmlDocument.SelectSingleNode("/GlideProfile/VendorFW");
                if (xmlNode2 != null)
                    VendorFW = xmlNode2.InnerText.Trim();
                var xmlNode3 = xmlDocument.SelectSingleNode("/GlideProfile/VendorAR");
                if (xmlNode3 != null)
                    VendorAR = xmlNode3.InnerText.Trim();
                var xmlNode4 = xmlDocument.SelectSingleNode("/GlideProfile/VendorRepair");
                if (xmlNode4 != null)
                    VendorRepair = xmlNode4.InnerText.Trim();
                var xmlNode5 = xmlDocument.SelectSingleNode("/GlideProfile/NaturalRun");
                if (xmlNode5 != null)
                    NaturalRun = xmlNode5.InnerText == "True";
                MinLevel = int.Parse(xmlDocument.SelectSingleNode("/GlideProfile/MinLevel").InnerText);
                MaxLevel = int.Parse(xmlDocument.SelectSingleNode("/GlideProfile/MaxLevel").InnerText);
                SetFactionsFromString(xmlDocument.SelectSingleNode("/GlideProfile/Factions").InnerText);
                foreach (XmlNode selectNode in xmlDocument.SelectNodes("/GlideProfile/Waypoint"))
                    Waypoints.Add(new GLocation(selectNode.InnerText));
                foreach (XmlNode selectNode in xmlDocument.SelectNodes("/GlideProfile/GhostWaypoint"))
                    GhostWaypoints.Add(new GLocation(selectNode.InnerText));
                foreach (XmlNode selectNode in xmlDocument.SelectNodes("/GlideProfile/VendorWaypoint"))
                    VendorWaypoints.Add(new GLocation(selectNode.InnerText));
                var arrayList = new ArrayList();
                foreach (XmlNode selectNode in xmlDocument.SelectNodes("/GlideProfile/Avoid"))
                    arrayList.Add(selectNode.InnerText);
                AvoidList = arrayList.Count <= 0 ? null : (string[])arrayList.ToArray(typeof(string));
                Select();
                Name = InputFile;
                return true;
            }
            catch (Exception ex)
            {
                GClass37.smethod_0(GClass30.smethod_2(64, ex.Message));
                return false;
            }
        }

        public void Select()
        {
            StartupClass.int_6 = 1;
            if (ReverseWaypoints)
                StartupClass.int_6 = -1;
            StartupClass.sortedList_2.Clear();
        }

        public bool IsBlacklisted(long GUID)
        {
            foreach (var num in Blacklist)
                if (GUID == num)
                    return true;
            return false;
        }

        public void ForceBlacklist(long GUID)
        {
            if (GPlayerSelf.Me.TargetGUID == GUID)
                GContext.Main.ClearTarget();
            GClass37.smethod_0(GClass30.smethod_2(648, GUID.ToString("x")));
            Blacklist.Add(GUID);
        }

        public void AddToBlacklist(long GUID)
        {
            if (GPlayerSelf.Me.TargetGUID == GUID)
                GContext.Main.ClearTarget();
            if (!BlacklistOn)
                return;
            GClass37.smethod_0(GClass30.smethod_2(648, GUID.ToString("x")));
            Blacklist.Add(GUID);
        }

        private void AddToOutput(XmlDocument xmlDocument_0, string KeyName, string Value)
        {
            var element = xmlDocument_0.CreateElement(KeyName);
            element.InnerText = Value;
            xmlDocument_0.DocumentElement.AppendChild(element);
        }

        public void SetFactionsFromString(string What)
        {
            if (What.Length == 0)
            {
                Factions = null;
            }
            else
            {
                What = What.Trim();
                var strArray = What.Split(' ');
                Factions = new int[strArray.Length];
                for (var index = 0; index < strArray.Length; ++index)
                    Factions[index] = int.Parse(strArray[index]);
            }
        }

        public string GetFactionsAsString()
        {
            if (Factions == null)
                return "";
            var stringBuilder = new StringBuilder();
            foreach (var faction in Factions)
                stringBuilder.Append(faction + " ");
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }

        public void AddFaction(int TheFaction)
        {
            if (Factions != null)
            {
                var intList = new List<int>();
                foreach (var faction in Factions)
                    intList.Add(faction);
                intList.Add(TheFaction);
                Factions = intList.ToArray();
            }
            else
            {
                Factions = new int[1];
                Factions[0] = TheFaction;
            }
        }

        public void RemoveFaction(int TheFaction)
        {
            var intList = new List<int>();
            foreach (var faction in Factions)
                if (faction != TheFaction)
                    intList.Add(faction);
            if (intList.Count == 0)
                Factions = null;
            else
                Factions = intList.ToArray();
        }

        public GLocation GetWaypointDelta(GLocation StartingFrom, int Delta)
        {
            var index1 = 0;
            while (index1 < Waypoints.Count && Waypoints[index1] != StartingFrom)
                ++index1;
            var index2 = index1 + Delta;
            if (index2 == Waypoints.Count)
                index2 = 0;
            if (index2 < 0)
                index2 = Waypoints.Count - 1;
            return Waypoints[index2];
        }

        public bool CheckFaction(int FactionToFind)
        {
            return CheckFaction(FactionToFind, false);
        }

        public bool CheckFaction(int FactionToFind, bool ForceCheck)
        {
            if (Factions == null)
                return !ForceCheck;
            foreach (var faction in Factions)
                if (faction == FactionToFind)
                    return true;
            return false;
        }

        public double GetDistanceTo(GLocation glocation_0)
        {
            var closestWaypoint = FindClosestWaypoint(glocation_0);
            return closestWaypoint == null ? 0.0 : closestWaypoint.GetDistanceTo(glocation_0);
        }

        public GLocation FindClosestWaypoint(GLocation glocation_0)
        {
            if (Waypoints.Count < 1)
                return null;
            var num = 99998.0;
            GLocation closestWaypoint = null;
            for (var index = 0; index < Waypoints.Count; ++index)
            {
                double distanceTo = Waypoints[index].GetDistanceTo(glocation_0);
                if (distanceTo < num)
                {
                    closestWaypoint = Waypoints[index];
                    num = distanceTo;
                }
            }

            return closestWaypoint;
        }

        public GLocation FindClosestVendorWaypoint(GLocation glocation_0)
        {
            if (VendorWaypoints.Count < 1)
                return null;
            var num = 99998.0;
            GLocation closestVendorWaypoint = null;
            for (var index = 0; index < VendorWaypoints.Count; ++index)
            {
                double distanceTo = VendorWaypoints[index].GetDistanceTo(glocation_0);
                if (distanceTo < num)
                {
                    closestVendorWaypoint = VendorWaypoints[index];
                    num = distanceTo;
                }
            }

            return closestVendorWaypoint;
        }

        protected double GetHeightAH(GLocation A, GLocation B, GLocation C)
        {
            double distanceTo1 = A.GetDistanceTo(B);
            double distanceTo2 = A.GetDistanceTo(C);
            double num1 = B.GetDistanceTo(C);
            if (num1 == 0.0)
                num1 = 0.001;
            var num2 = (distanceTo1 + distanceTo2 + num1) / 2.0;
            return 2.0 * Math.Sqrt(num2 * (num2 - distanceTo1) * (num2 - distanceTo2) * (num2 - num1)) / num1;
        }

        public void PlaceBreadcrumb()
        {
            if (!UseBreadcrumbs)
                return;
            var index1 = 0;
            if (Wander && Breadcrumbs.Count == 0 && CurrentWaypoint.GetDistanceTo(GPlayerSelf.Me.Location) > 99.0)
            {
                var closestWaypointIndex = GetClosestWaypointIndex(GPlayerSelf.Me.Location);
                if (closestWaypointIndex == CurrentIndex)
                    return;
                GClass37.smethod_1("## Way off course, re-syncing to closest waypoint (current wp is #" + CurrentIndex +
                                   ", distance = " + CurrentWaypoint.GetDistanceTo(GPlayerSelf.Me.Location) +
                                   " yards)");
                CurrentIndex = closestWaypointIndex;
                GClass37.smethod_1("## New is #" + CurrentIndex + ", distance = " +
                                   CurrentWaypoint.GetDistanceTo(GPlayerSelf.Me.Location) + " yards)");
            }
            else
            {
                double distanceTo;
                double num1;
                if (Breadcrumbs.Count > 0)
                {
                    var array = Breadcrumbs.ToArray();
                    var num2 = 99999.0;
                    GLocation glocation = null;
                    var num3 = 0;
                    for (var index2 = 0; index2 < array.Length; ++index2)
                        if (array[index2].GetDistanceTo(GPlayerSelf.Me.Location) < num2)
                        {
                            num2 = array[index2].GetDistanceTo(GPlayerSelf.Me.Location);
                            glocation = array[index2];
                            num3 = index2;
                        }

                    if (num2 < 18.3 && num3 < Breadcrumbs.Count - 1)
                    {
                        GClass37.smethod_1("## Looped back, tearing down breadcrumb list to #" + (num3 + 1));
                        while (Breadcrumbs.Peek() != glocation)
                            Breadcrumbs.Pop();
                        return;
                    }

                    distanceTo = Breadcrumbs.Peek().GetDistanceTo(GPlayerSelf.Me.Location);
                    num1 = 18.3;
                    GClass37.smethod_1("## Distance to last breadcrumb: " + distanceTo + " yards");
                }
                else
                {
                    index1 = GetClosestWaypointIndex(GPlayerSelf.Me.Location);
                    num1 = 27.6;
                    distanceTo = Waypoints[index1].GetDistanceTo(GPlayerSelf.Me.Location);
                    GClass37.smethod_1("## Distance to profile: " + distanceTo + " yards");
                }

                if (distanceTo < num1)
                    return;
                if (Breadcrumbs.Count == 0)
                {
                    GClass37.smethod_1("## Starting breadcrumbs, syncing profile to closest waypoint");
                    CurrentIndex = index1;
                }

                Breadcrumbs.Push(GPlayerSelf.Me.Location);
                GClass37.smethod_0("Placing breadcrumb #" + Breadcrumbs.Count);
            }
        }

        public void ClearBreadcrumbs()
        {
            Breadcrumbs.Clear();
        }

        private int GetLastGhostIndex(int StartIndex)
        {
            while (StartIndex < GhostWaypoints.Count - 1 &&
                   GhostWaypoints[StartIndex + 1].GetDistanceTo(GhostWaypoints[StartIndex]) <= 25.0)
                ++StartIndex;
            return StartIndex;
        }

        public Queue<GLocation> CreateVendorPath(GLocation Starting)
        {
            var vendorPath = new Queue<GLocation>();
            var flag = Starting == null;
            foreach (var vendorWaypoint in VendorWaypoints)
                if (flag)
                    vendorPath.Enqueue(vendorWaypoint);
                else if (vendorWaypoint == Starting)
                    flag = true;
            return vendorPath;
        }

        public Queue<GLocation> CreateGhostwalkPath(GLocation Corpse)
        {
            var ghostwalkPath = new Queue<GLocation>();
            var ghostWaypointIndex = GetClosestGhostWaypointIndex(GPlayerSelf.Me.Location);
            var lastGhostIndex = GetLastGhostIndex(ghostWaypointIndex);
            var closestWaypointIndex = GetClosestWaypointIndex(GPlayerSelf.Me.Location);
            GClass37.smethod_1("## Creating ghost walk path (GhostIndex=" + ghostWaypointIndex + ", LastGhostIndex=" +
                               lastGhostIndex + ", Me.Location=" + GPlayerSelf.Me.Location + ")");
            int index1;
            if (GPlayerSelf.Me.Location.GetDistanceTo(Waypoints[closestWaypointIndex]) >
                (double)GPlayerSelf.Me.Location.GetDistanceTo(GhostWaypoints[ghostWaypointIndex]))
            {
                for (var index2 = ghostWaypointIndex; index2 <= lastGhostIndex; ++index2)
                {
                    GClass37.smethod_1("## Queueing gwp #" + index2 + ": " + GhostWaypoints[index2]);
                    ghostwalkPath.Enqueue(GhostWaypoints[index2]);
                }

                index1 = GetClosestWaypointIndex(GhostWaypoints[lastGhostIndex]);
            }
            else
            {
                GClass37.smethod_1("## Real waypoint is closer than ghost, skipping GWP's");
                index1 = closestWaypointIndex;
            }

            int num1;
            if (Breadcrumbs.Count == 0)
            {
                num1 = GetClosestWaypointIndex(Corpse);
                GClass37.smethod_1("## No breadcrumbs, last waypoint index = " + num1);
            }
            else
            {
                num1 = CurrentIndex;
                GClass37.smethod_1("## Breadcrumbs present, last waypoint index = " + num1);
            }

            int num2;
            if (Reversible)
            {
                num2 = index1 <= num1 ? 1 : -1;
            }
            else
            {
                var num3 = lastGhostIndex - num1;
                var num4 = num1 - lastGhostIndex;
                if (num3 < 0)
                    num3 += Waypoints.Count;
                if (num4 < 0)
                    num4 += Waypoints.Count;
                GClass37.smethod_1("## LGI: " + lastGhostIndex + ", LWI: " + num1 + ", PSteps: " + num3 + ", NSteps: " +
                                   num4);
                num2 = num3 >= num4 ? 1 : -1;
            }

            GClass37.smethod_1("## CurrentWPI: " + index1 + ", LastWPI: " + num1 + ", Vector: " + num2);
            while (index1 != num1)
            {
                GClass37.smethod_1("## Queuing rwp #" + index1 + ": " + Waypoints[index1]);
                ghostwalkPath.Enqueue(Waypoints[index1]);
                index1 += num2;
                if (index1 == Waypoints.Count)
                    index1 = 0;
                if (index1 < 0)
                    index1 = Waypoints.Count - 1;
            }

            if (Breadcrumbs.Count > 0)
            {
                var array = Breadcrumbs.ToArray();
                for (var index3 = 0; index3 < array.Length; ++index3)
                {
                    GClass37.smethod_1("## Queuing bwp #" + (index3 + 1) + ": " + array[index3]);
                    ghostwalkPath.Enqueue(array[index3]);
                }
            }

            return ghostwalkPath;
        }

        private GLocation GetClosestWaypoint(GLocation Source)
        {
            return Waypoints[GetClosestWaypointIndex(Source)];
        }

        public string GetWaypointDescription(int WPIndex)
        {
            double distanceTo = GPlayerSelf.Me.Location.GetDistanceTo(Waypoints[WPIndex]);
            double headingTo = GPlayerSelf.Me.Location.GetHeadingTo(Waypoints[WPIndex]);
            var num = GContext.Main.Movement.CompareHeadings(GPlayerSelf.Me.Heading, headingTo);
            string str;
            if (num < 0.0)
                str = GClass30.smethod_2(664, Math.Round(Math.Abs(num) / Math.PI * 180.0, 1));
            else
                str = GClass30.smethod_2(665, Math.Round(num / Math.PI * 180.0, 1));
            return GClass30.smethod_2(666, WPIndex, str, Math.Round(distanceTo, 2));
        }

        public string[] GetWaypointNotes()
        {
            var closestWaypointIndex = GetClosestWaypointIndex(GPlayerSelf.Me.Location);
            var WPIndex1 = closestWaypointIndex + StartupClass.int_6;
            var WPIndex2 = closestWaypointIndex - StartupClass.int_6;
            if (WPIndex1 < 0)
                WPIndex1 = Waypoints.Count - 1;
            if (WPIndex1 == Waypoints.Count)
                WPIndex1 = 0;
            if (WPIndex2 < 0)
                WPIndex2 = Waypoints.Count - 1;
            if (WPIndex2 == Waypoints.Count)
                WPIndex2 = 0;
            return new string[3]
            {
                GetWaypointDescription(WPIndex2),
                GetWaypointDescription(closestWaypointIndex),
                GetWaypointDescription(WPIndex1)
            };
        }

        private int GetClosestWaypointIndex(GLocation Source)
        {
            var num = 99999.0;
            var closestWaypointIndex = -1;
            for (var index = 0; index < Waypoints.Count; ++index)
            {
                double distanceTo = Waypoints[index].GetDistanceTo(Source);
                if (distanceTo < num)
                {
                    num = distanceTo;
                    closestWaypointIndex = index;
                }
            }

            if (closestWaypointIndex == -1)
            {
                GClass37.smethod_0("Could not find closest waypoint to " + Source + "!  Checked " + Waypoints.Count +
                                   " waypoints!  Adding temp waypoint.");
                Waypoints.Add(GPlayerSelf.Me.Location);
                closestWaypointIndex = 0;
            }

            return closestWaypointIndex;
        }

        private int GetClosestGhostWaypointIndex(GLocation Source)
        {
            var num = 99999.0;
            var ghostWaypointIndex = 0;
            for (var index = 0; index < GhostWaypoints.Count; ++index)
            {
                double distanceTo = GhostWaypoints[index].GetDistanceTo(Source);
                if (distanceTo < num)
                {
                    num = distanceTo;
                    ghostWaypointIndex = index;
                }
            }

            return ghostWaypointIndex;
        }

        public GLocation BeginProfile(GLocation Source)
        {
            GClass37.smethod_1("## BeginProfile invoked");
            var closestWaypointIndex = GetClosestWaypointIndex(Source);
            var L = Waypoints[closestWaypointIndex];
            CurrentIndex = closestWaypointIndex;
            if (Breadcrumbs.Count > 0)
            {
                if (GPlayerSelf.Me.Location.GetDistanceTo(Breadcrumbs.Peek()) <
                    (double)GPlayerSelf.Me.Location.GetDistanceTo(L))
                {
                    GClass37.smethod_1("## Closest spot is a breadcrumb, taking that instead");
                    L = Breadcrumbs.Peek();
                }
                else
                {
                    GClass37.smethod_1("## Got crumbs, but they're too far away, wiping trail");
                    ClearBreadcrumbs();
                }
            }
            else
            {
                GClass37.smethod_1("## No breadcrumbs to start profile");
            }

            return L;
        }

        public string DebugCurrentWaypoint()
        {
            return Breadcrumbs.Count > 0
                ? "bwp #" + Breadcrumbs.Count + ": " + Breadcrumbs.Peek()
                : "rwp #" + CurrentIndex + ": " + Waypoints[CurrentIndex];
        }

        public void ConsumeCurrentWaypoint()
        {
            GClass37.smethod_1("## ConsumeCurrentWaypoint invoked");
            if (Breadcrumbs.Count > 0)
            {
                if (AllowShortCircuit &&
                    GetClosestWaypoint(GPlayerSelf.Me.Location).GetDistanceTo(GPlayerSelf.Me.Location) < 27.6)
                {
                    GClass37.smethod_1("## Short-circuiting back onto regular waypoints");
                    ClearBreadcrumbs();
                    CurrentIndex = GetClosestWaypointIndex(GPlayerSelf.Me.Location);
                }
                else
                {
                    Breadcrumbs.Pop();
                    if (Breadcrumbs.Count > 0)
                    {
                        GClass37.smethod_1("## On crumbs, next waypoint is bwp #" + Breadcrumbs.Count + ": " +
                                           Breadcrumbs.Peek());
                    }
                    else
                    {
                        CurrentIndex = GetClosestWaypointIndex(GPlayerSelf.Me.Location);
                        GClass37.smethod_1("## Off crumbs, next waypoint is rwp #" + CurrentIndex + ": " +
                                           Waypoints[CurrentIndex]);
                    }
                }
            }
            else
            {
                CurrentIndex += StartupClass.int_6;
                if (CurrentIndex < 0 || CurrentIndex >= Waypoints.Count)
                {
                    GClass48.smethod_2();
                    OneShotHit = OneShot && OneShotStepCheck > 4;
                }

                if (CurrentIndex < 0)
                {
                    if (Reversible)
                    {
                        GClass37.smethod_0(GClass30.smethod_1(67));
                        CurrentIndex = 1;
                        StartupClass.int_6 = 1;
                    }
                    else
                    {
                        CurrentIndex = Waypoints.Count - 1;
                        GClass37.smethod_0(GClass30.smethod_1(68));
                    }
                }

                if (CurrentIndex >= Waypoints.Count)
                {
                    if (Reversible)
                    {
                        GClass37.smethod_0(GClass30.smethod_1(65));
                        CurrentIndex = Waypoints.Count - 2;
                        StartupClass.int_6 = -1;
                    }
                    else
                    {
                        GClass37.smethod_0(GClass30.smethod_1(66));
                        CurrentIndex = 0;
                    }
                }

                GClass37.smethod_1("## Next waypoint is rwp #" + CurrentIndex + ": " + Waypoints[CurrentIndex]);
            }
        }

        private int PeekNextWaypointIndex(int StartIndex)
        {
            StartIndex += StartupClass.int_6;
            if (StartIndex < 0)
                StartIndex = !Reversible ? Waypoints.Count - 1 : 1;
            if (StartIndex == Waypoints.Count)
                StartIndex = !Reversible ? 0 : Waypoints.Count - 2;
            return StartIndex;
        }

        public void ConsiderWaypointSkip()
        {
            if (!SkipWaypoints)
                return;
            CurrentIndex = GetClosestWaypointIndex(GPlayerSelf.Me.Location);
            ConsumeCurrentWaypoint();
        }

        public void SetPreviousWaypoint()
        {
            if (Breadcrumbs.Count > 0)
                Breadcrumbs.Pop();
            if (Breadcrumbs.Count != 0)
                return;
            StartupClass.int_6 *= -1;
            ConsumeCurrentWaypoint();
            StartupClass.int_6 *= -1;
        }
    }
}