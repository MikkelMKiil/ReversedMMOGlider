// Decompiled with JetBrains decompiler
// Type: UIElement
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

public class UIElement
{
    private static float float_0 = 1f;
    private static float float_1 = 1f;
    private ArrayList arrayList_0 = new ArrayList();
    private bool bool_0;
    private float float_2;
    private float float_3;
    private float float_4;
    private float float_5;
    public int int_0;
    private List<UIElement> list_0;
    public string string_0;

    public UIElement(int int_1)
    {
        int_0 = int_1;
        bool_0 = true;
        method_1();
        if (string_0 == "UIParent" || string_0 == "GlueParent")
            smethod_1(float_4, float_3);
        list_0 = null;
    }

    [SpecialName]
    public bool method_0()
    {
        return bool_0;
    }

    public void method_1()
    {
        string_0 = "(no name)";
        var int_29 = GProcessMemoryManipulator.ReadInt32(int_0 + MemoryOffsetTable.Instance.GetIntOffset("UIName"), "onamer");
        if (int_29 != 0)
            string_0 = GProcessMemoryManipulator.ReadString(int_29, 200, "objectnamer");
        else
            bool_0 = false;
        var num = MemoryOffsetTable.Instance.GetIntOffset("UICoordBase");
        float_5 = GProcessMemoryManipulator.ReadFloat(int_0 + num, "uiobjbottom");
        float_2 = GProcessMemoryManipulator.ReadFloat(int_0 + num + 4, "uiobjleft");
        float_3 = GProcessMemoryManipulator.ReadFloat(int_0 + num + 8, "uiobjtop");
        float_4 = GProcessMemoryManipulator.ReadFloat(int_0 + num + 12, "uiobjright");
    }

    public string method_2()
    {
        return "L/R=" + float_2 + "/" + float_4 + ", T/B=" + float_3 + "/" + float_5 + ", " + ("SL/SR=" + method_11() +
            "/" + method_12() + ", ST/SB=" + method_13() + "/" + method_14());
    }

    [SpecialName]
    public string method_3()
    {
        var int_29 = GProcessMemoryManipulator.ReadInt32(int_0 + MemoryOffsetTable.Instance.GetIntOffset("UILabelText"), "labeltextptr");
        if (int_29 == 0 || (int_29 & 1) == 1)
        {
            var num1 = GProcessMemoryManipulator.ReadInt32(int_0, "GameClass");
            if (num1 == MemoryOffsetTable.Instance.GetIntOffset("UITypeLabel1") ||
                num1 == MemoryOffsetTable.Instance.GetIntOffset("UITypeLabel2"))
            {
                var num2 = GProcessMemoryManipulator.ReadInt32(int_0 + MemoryOffsetTable.Instance.GetIntOffset("UIFontString"), "fontstringptr");
                if (num2 != 0)
                    int_29 = GProcessMemoryManipulator.ReadInt32(num2 + MemoryOffsetTable.Instance.GetIntOffset("UILabelText"), "labeltextptr2");
            }
        }

        return int_29 != 0 && (int_29 & 1) != 1 ? GProcessMemoryManipulator.ReadStringInternal(int_29, 200, "labeltext1") : "(no text)";
    }

    private void method_4()
    {
        list_0 = new List<UIElement>();
        var num = GProcessMemoryManipulator.ReadInt32(int_0 + MemoryOffsetTable.Instance.GetIntOffset("UIChildren"), "childrenptr");
        if (num == 0)
            return;
        do
        {
            var int_1 = GProcessMemoryManipulator.ReadInt32(num + MemoryOffsetTable.Instance.GetIntOffset("UIChildStep"), "childstep");
            if ((int_1 & ushort.MaxValue) != 0)
            {
                list_0.Add(new UIElement(int_1));
                num = GProcessMemoryManipulator.ReadInt32(num + MemoryOffsetTable.Instance.GetIntOffset("UIChildNext"), "childnext");
            }
            else
            {
                goto label_1;
            }
        } while (num != 0);

        return;
    label_1:;
    }

    private static bool smethod_0(List<UIElement> list_1, int int_1)
    {
        foreach (var gclass8 in list_1)
            if (gclass8.int_0 == int_1)
                return true;
        return false;
    }

    private void method_5()
    {
        list_0 = new List<UIElement>();
        var num1 = GProcessMemoryManipulator.ReadInt32(int_0 + MemoryOffsetTable.Instance.GetIntOffset("UIChildren"), "uichild1");
        var num2 = 150;
        if (num1 != 0)
            do
            {
                var int_1 = GProcessMemoryManipulator.ReadInt32(num1 + MemoryOffsetTable.Instance.GetIntOffset("UIChildStep"), "uichild2");
                if ((int_1 & ushort.MaxValue) != 0)
                {
                    if ((int_1 & 1) == 0)
                    {
                        if (!smethod_0(list_0, int_1))
                        {
                            var gclass8 = new UIElement(int_1);
                            if (gclass8.method_0())
                                list_0.Add(gclass8);
                            else
                                break;
                        }
                        else
                        {
                            break;
                        }
                    }

                    num1 = GProcessMemoryManipulator.ReadInt32(num1 + MemoryOffsetTable.Instance.GetIntOffset("UIChildNext"), "uichild3");
                    if (num1 != 0)
                        --num2;
                    else
                        break;
                }
                else
                {
                    break;
                }
            } while (num2 != 0);

        var int_1_1 = GProcessMemoryManipulator.ReadInt32(int_0 + MemoryOffsetTable.Instance.GetIntOffset("UIChildrenOneShot"), "uichild1s");
        if (int_1_1 == 0 || (int_1_1 & 1) != 0 || smethod_0(list_0, int_1_1))
            return;
        var gclass8_1 = new UIElement(int_1_1);
        if (!gclass8_1.method_0())
            return;
        list_0.Add(gclass8_1);
    }

    public UIElement method_6(string string_1)
    {
        if (list_0 == null)
            method_5();
        foreach (var gclass8 in list_0)
            if (gclass8.string_0.ToLower() == string_1.ToLower())
                return gclass8;
        return null;
    }

    public UIElement[] method_7()
    {
        var gclass8List = new List<UIElement>();
        if (list_0 == null)
            method_5();
        foreach (var gclass8 in list_0)
            gclass8List.Add(gclass8);
        return gclass8List.ToArray();
    }

    public void method_8()
    {
        Logger.smethod_1("Dumping children of \"" + string_0 + "\"");
        var gclass8Array = method_9();
        if (gclass8Array != null && gclass8Array.Length != 0)
        {
            foreach (var gclass8 in gclass8Array)
                if (gclass8.string_0 != "(no name)")
                    Logger.smethod_1("- UIObject @ 0x" + gclass8.int_0.ToString("x8") + ", Name=\"" +
                                       gclass8.string_0 + "\", Visible=" + gclass8.method_10() + ", Label=\"" +
                                       gclass8.method_3() + "\"");
        }
        else
        {
            Logger.smethod_1("No children!");
        }
    }

    [SpecialName]
    public UIElement[] method_9()
    {
        if (list_0 == null)
            method_5();
        return list_0 == null ? null : list_0.ToArray();
    }

    [SpecialName]
    public bool method_10()
    {
        return GProcessMemoryManipulator.ReadInt32(int_0 + MemoryOffsetTable.Instance.GetIntOffset("UIMenuVisible"), "tfvisible") != 0;
    }

    [SpecialName]
    public double method_11()
    {
        return float_2 / (double)float_0;
    }

    [SpecialName]
    public double method_12()
    {
        return float_4 / (double)float_0;
    }

    [SpecialName]
    public double method_13()
    {
        return 1.0 - float_3 / (double)float_1;
    }

    [SpecialName]
    public double method_14()
    {
        return 1.0 - float_5 / (double)float_1;
    }

    public void method_15()
    {
        StartupClass.cameraRotator.method_3(true);
        InputController.smethod_18(method_11() + (method_12() - method_11()) / 2.0,
            method_13() + (method_14() - method_13()) / 2.0);
    }

    public static void smethod_1(float float_6, float float_7)
    {
        float_0 = float_6;
        float_1 = float_7;
    }

    public static UIElement smethod_2(string string_1)
    {
        var int_1 = GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("UIParent"), "uiparent");
        if (int_1 != 0)
            return smethod_4(int_1, string_1);
        Logger.smethod_1("! UIParent points to nowhere, can't find \"" + string_1 + "\"");
        return null;
    }

    public static UIElement smethod_3(string string_1)
    {
        var num1 = GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("UIGlue1"), "uig1");
        var num2 = GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("UIGlue2"), "uig2");
        var int_29_1 = num1 + MemoryOffsetTable.Instance.GetIntOffset("UIGlueStep");
        var int_29_2 = num2 + MemoryOffsetTable.Instance.GetIntOffset("UIGlueStep");
        var int_1 = GProcessMemoryManipulator.ReadInt32(int_29_1, "uigm1");
        var num3 = GProcessMemoryManipulator.ReadInt32(int_29_2, "uigm2");
        return int_1 == num3 && int_1 != 0 ? smethod_4(int_1, string_1) : null;
    }

    private static UIElement smethod_4(int int_1, string string_1)
    {
        do
        {
            var int_29 = GProcessMemoryManipulator.ReadInt32(int_1 + MemoryOffsetTable.Instance.GetIntOffset("UIName"), "oname2");
            if (int_29 != 0)
                goto label_2;
        label_1:
            int_1 = GProcessMemoryManipulator.ReadInt32(int_1 + MemoryOffsetTable.Instance.GetIntOffset("UINext"), "onextnb");
            continue;
        label_2:
            var str = GProcessMemoryManipulator.ReadString(int_29, 200, "objectnamenb");
            switch (str)
            {
                case "(read failed)":
                    goto label_5;
                case "GlueParent":
                    var gclass8 = new UIElement(int_1);
                    break;
            }

            if (str == string_1)
                goto label_6;
            goto label_1;
        } while (int_1 != 0);

        goto label_7;
    label_5:
        Logger.smethod_1("Invalid object in list while searching, giving up");
        return null;
    label_6:
        return new UIElement(int_1);
    label_7:
        Logger.smethod_1(MessageProvider.smethod_2(820, string_1));
        return null;
    }

    public static string[] smethod_5()
    {
        var num1 = GProcessMemoryManipulator.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("UIParent"), "uiparent");
        if (num1 == 0)
        {
            Logger.smethod_1("! UIParent points to nowhere, can't find dump list of object names");
            return null;
        }

        var stringList = new List<string>();
        var num2 = num1;
        do
        {
            var int_29 = GProcessMemoryManipulator.ReadInt32(num2 + MemoryOffsetTable.Instance.GetIntOffset("UIName"), "oname3");
            if (int_29 != 0)
                goto label_4;
        label_3:
            num2 = GProcessMemoryManipulator.ReadInt32(num2 + MemoryOffsetTable.Instance.GetIntOffset("UINext"), "onextga");
            continue;
        label_4:
            var str = GProcessMemoryManipulator.ReadString(int_29, 200, "objectnamega");
            stringList.Add(str);
            goto label_3;
        } while (num2 != 0);

        return stringList.ToArray();
    }

    public void method_16(bool bool_1)
    {
        method_15();
        Thread.smethod_39(300);
        InputController.smethod_23(bool_1);
    }

    public void method_17(bool bool_1)
    {
        method_15();
        Thread.smethod_39(300);
        InputController.smethod_24(bool_1);
    }

    public void method_18(bool bool_1)
    {
        method_15();
        Thread.smethod_39(300);
        InputController.smethod_25(bool_1);
    }
}