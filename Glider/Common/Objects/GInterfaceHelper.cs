// Decompiled with JetBrains decompiler
// Type: Glider.Common.Objects.GInterfaceHelper
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.Threading;

namespace Glider.Common.Objects
{
    public class GInterfaceHelper
    {
        private readonly SortedList<string, GInterfaceObject> Cache;
        private int FailSpam;

        public GInterfaceHelper()
        {
            Cache = new SortedList<string, GInterfaceObject>();
        }

        public bool IsPreWorldVisible
        {
            get
            {
                var num1 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("UIGlue1"), "uig1");
                var num2 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("UIGlue2"), "uig2");
                var int_29_1 = num1 + GClass18.gclass18_0.method_4("UIGlueStep");
                var int_29_2 = num2 + GClass18.gclass18_0.method_4("UIGlueStep");
                var num3 = GProcessMemoryManipulator.smethod_11(int_29_1, "uigm1");
                var num4 = GProcessMemoryManipulator.smethod_11(int_29_2, "uigm2");
                return num3 == num4 && num3 != 0;
            }
        }

        public int CursorType => GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4(nameof(CursorType)), "cursortype");

        public GCursorItemTypes CursorItemType =>
            (GCursorItemTypes)GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4(nameof(CursorItemType)),
                "cursoritemtype");

        public void OnAttach()
        {
            lock (Cache)
            {
                Cache.Clear();
            }
        }

        public GInterfaceObject GetByName(string ObjectName)
        {
            lock (Cache)
            {
                if (Cache.ContainsKey(ObjectName))
                    return Cache[ObjectName];
                var InnerObject = GClass8.smethod_2(ObjectName);
                if (InnerObject == null)
                    return null;
                var byName = new GInterfaceObject(InnerObject);
                Cache.Add(ObjectName, byName);
                return byName;
            }
        }

        public GInterfaceObject GetByNamePreWorld(string ObjectName)
        {
            var InnerObject = GClass8.smethod_3(ObjectName);
            return InnerObject == null ? null : new GInterfaceObject(InnerObject);
        }

        public static string[] GetAllInterfaceObjectNames()
        {
            return GClass8.smethod_5();
        }

        public GInterfaceObject GetByKeyName(string KeyName)
        {
            try
            {
                var gkey = GClass42.gclass42_0.sortedList_0[KeyName];
                if (gkey.BarState == GBarState.Indifferent)
                    throw new Exception("BarState indifferent for that key, can only look up Combat/Rest");
                var num = StartupClass.numbers_string.IndexOf(gkey.CharCode);
                var ObjectName = "BonusActionButton" + (num + 1);
                var byName = GetByName(ObjectName);
                if (byName == null || !byName.IsVisible)
                {
                    ObjectName = "ActionButton" + (num + 1);
                    byName = GetByName(ObjectName);
                }

                return byName != null
                    ? byName
                    : throw new Exception("No such UI object in game: \"" + ObjectName + "\"");
            }
            catch (ThreadInterruptedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ++FailSpam;
                if (FailSpam < 5)
                    GContext.Main.Log("* GInterfaceHelper.GetByKeyName failed on \"" + KeyName + "\", exception: " +
                                      ex.Message);
                return null;
            }
        }

        public int GetActionInventory(string KeyName)
        {
            var gkey = GClass42.gclass42_0.sortedList_0[KeyName];
            gkey.FilloutKey();
            GClass37.smethod_1(GClass30.smethod_2(627, gkey.KeyName, gkey.CharCode));
            var num1 = GClass18.gclass18_0.method_4("BarBase");
            var num2 = GClass18.gclass18_0.method_4("BarSize");
            var num3 = 0;
            var num4 = StartupClass.numbers_string.IndexOf(gkey.CharCode);
            if (num4 == -1)
                return -1;
            int int_29;
            try
            {
                switch (gkey.BarState)
                {
                    case GBarState.Combat:
                        num3 = int.Parse(new string(GClass42.gclass42_0.sortedList_0["Common.BarCombat"].CharCode, 1));
                        GClass37.smethod_1(GClass30.smethod_2(3, num3));
                        break;
                    case GBarState.Rest:
                        num3 = int.Parse(new string(GClass42.gclass42_0.sortedList_0["Common.BarRest"].CharCode, 1));
                        GClass37.smethod_1(GClass30.smethod_2(4, num3));
                        break;
                    case GBarState.Bar1:
                        num3 = 1;
                        break;
                    case GBarState.Bar2:
                        num3 = 2;
                        break;
                    case GBarState.Bar3:
                        num3 = 3;
                        break;
                    case GBarState.Bar4:
                        num3 = 4;
                        break;
                    case GBarState.Bar5:
                        num3 = 5;
                        break;
                    case GBarState.Bar6:
                        num3 = 6;
                        break;
                }

                int_29 = num1 + (num3 - 1) * num2 + 4 * num4;
            }
            catch (ThreadInterruptedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return -1;
            }

            return GProcessMemoryManipulator.smethod_11(int_29, "GetActionInv" + num4);
        }

        public void WaitForReady(string KeyName)
        {
            var gspellTimer = new GSpellTimer(9000);
            do
            {
                ;
            } while (!gspellTimer.IsReadySlow && !IsKeyReady(KeyName));

            if (gspellTimer.IsReady)
                GContext.Main.Log("* Gave up waiting for cooldown on \"" + KeyName + "\"");
            Thread.Sleep(67);
        }

        public bool IsKeyFiring(string KeyName)
        {
            GKey gkey;
            try
            {
                gkey = GClass42.gclass42_0.sortedList_0[KeyName];
            }
            catch (KeyNotFoundException ex)
            {
                GClass37.smethod_0("!! IsKeyFiring failed, missing key: \"" + KeyName + "\"");
                return false;
            }

            gkey.FilloutKey();
            var visibleInterfaceObject = GClass42.gclass42_0.sortedList_0[KeyName].FindVisibleInterfaceObject();
            return visibleInterfaceObject != null && GetByName(visibleInterfaceObject).IsFiring;
        }

        public bool IsKeyReady(string KeyName)
        {
            var gkey = GClass42.gclass42_0.sortedList_0[KeyName];
            if (gkey == null)
            {
                GClass37.smethod_0("!! IsKeyReady failed, missing key: \"" + KeyName + "\"");
                return false;
            }

            gkey.FilloutKey();
            return StartupClass.gclass63_0.method_15(gkey.LiveSIM);
        }

        public static void DumpUIDebug(bool RequireVisible)
        {
            var int_1 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("UIParent"), "DumpUI_UIParent");
            var num = 0;
            do
            {
                var int_29 = GProcessMemoryManipulator.smethod_11(int_1 + GClass18.gclass18_0.method_4("UIName"), "uinamedumpui");
                if (int_29 != 0)
                    goto label_2;
                label_1:
                int_1 = GProcessMemoryManipulator.smethod_11(int_1 + GClass18.gclass18_0.method_4("UINext"), "CurrentAddress");
                continue;
                label_2:
                var str = GProcessMemoryManipulator.smethod_9(int_29, 200, "objectnamedumpui");
                var gclass8 = new GClass8(int_1);
                if (gclass8.method_10() || !RequireVisible)
                {
                    ++num;
                    GClass37.smethod_1("Object @ 0x" + int_1.ToString("x8") + ": \"" + str + "\", visible=" +
                                       gclass8.method_10() + ", label=\"" + gclass8.method_3() + "\"");
                    gclass8.method_8();
                    GClass37.smethod_1("");
                }

                goto label_1;
            } while (int_1 != 0);

            GClass37.smethod_0("DumpUIDebug done, dumped " + num + " named objects");
        }

        public bool IsKeyPopulated(string KeyName)
        {
            try
            {
                GClass42.gclass42_0.sortedList_0[KeyName].FilloutKey(true);
                return !GClass42.gclass42_0.sortedList_0[KeyName].IsUndefined;
            }
            catch (KeyNotFoundException ex)
            {
                GClass37.smethod_0("!! IsKeyPopulated failed, missing key: \"" + KeyName + "\"");
                return false;
            }
        }

        public bool IsKeyEnabled(string KeyName)
        {
            GKey gkey;
            try
            {
                gkey = GClass42.gclass42_0.sortedList_0[KeyName];
            }
            catch (KeyNotFoundException ex)
            {
                GClass37.smethod_0("!! IsKeyEnabled failed, missing key: \"" + KeyName + "\"");
                return false;
            }

            gkey.FilloutKey();
            var visibleInterfaceObject = GClass42.gclass42_0.sortedList_0[KeyName].FindVisibleInterfaceObject();
            if (visibleInterfaceObject == null)
            {
                gkey.EnsureBar();
                Thread.Sleep(100);
                visibleInterfaceObject = GClass42.gclass42_0.sortedList_0[KeyName].FindVisibleInterfaceObject();
                if (visibleInterfaceObject == null)
                    return false;
            }

            return GetByName(visibleInterfaceObject).IsEnabled;
        }

        public void SendString(string What)
        {
            GClass55.smethod_29(What);
        }

        public void SendLine(string What)
        {
            GClass55.smethod_28(What);
        }

        public static void DumpPreWorldUIDebug(bool RequireVisible)
        {
            var num1 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("UIGlue1"), "uig1");
            var num2 = GProcessMemoryManipulator.smethod_11(GClass18.gclass18_0.method_4("UIGlue2"), "uig2");
            var int_29_1 = num1 + GClass18.gclass18_0.method_4("UIGlueStep");
            var int_29_2 = num2 + GClass18.gclass18_0.method_4("UIGlueStep");
            var num3 = GProcessMemoryManipulator.smethod_11(int_29_1, "uigm1");
            var num4 = GProcessMemoryManipulator.smethod_11(int_29_2, "uigm2");
            var int_1 = num3;
            if (num3 != 0 && num3 == num4)
            {
                var num5 = 0;
                do
                {
                    var int_29_3 = GProcessMemoryManipulator.smethod_11(int_1 + GClass18.gclass18_0.method_4("UIName"), "uinamedumpui");
                    if (int_29_3 != 0)
                        goto label_3;
                    label_2:
                    int_1 = GProcessMemoryManipulator.smethod_11(int_1 + GClass18.gclass18_0.method_4("UINext"), "CurrentAddress");
                    continue;
                    label_3:
                    var str = GProcessMemoryManipulator.smethod_9(int_29_3, 200, "objectnamedumpui");
                    var gclass8 = new GClass8(int_1);
                    if (gclass8.method_10() || !RequireVisible)
                    {
                        ++num5;
                        GClass37.smethod_1("Object @ 0x" + int_1.ToString("x8") + ": \"" + str + "\", visible=" +
                                           gclass8.method_10());
                        gclass8.method_8();
                        GClass37.smethod_1("");
                    }

                    goto label_2;
                } while (int_1 != 0);

                GClass37.smethod_0("DumpUIPreWorldDebug done, dumped " + num5 + " named objects");
            }
            else
            {
                GClass37.smethod_0("Can't do DumpPreWorldUIDebug, no pre-world interface visible!");
            }
        }

        public void UpdateKeys()
        {
            GClass42.gclass42_0.method_23();
        }

        public void UnFillAllKeys()
        {
            GClass42.gclass42_0.method_3();
        }
    }
}