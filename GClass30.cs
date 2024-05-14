// Decompiled with JetBrains decompiler
// Type: GClass30
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;

public class GClass30
{
    public static GClass30 gclass30_0;
    private static readonly Regex regex_0 = new Regex("^\\d+$");
    private readonly SortedList sortedList_0;
    private readonly SortedList sortedList_1;
    private readonly SortedList sortedList_2;
    private readonly SortedList sortedList_3;
    public string string_0;
    private string[] string_1;
    private string[] string_2;
    private string string_3 = "(none)";
    private string string_4;
    private readonly string string_5;

    public GClass30(string string_6)
    {
        string_4 = string_6;
        string_0 = Thread.CurrentThread.CurrentCulture.Name;
        GClass37.smethod_1("Culture: " + string_0);
        if (GClass61.gclass61_0.method_2("ForceCulture") != null)
        {
            string_0 = GClass61.gclass61_0.method_2("ForceCulture");
            GClass37.smethod_1("Overriding with ForceCulture: " + string_0);
        }

        var str1 = string_0 + ".xml";
        sortedList_0 = new SortedList();
        sortedList_1 = new SortedList();
        sortedList_2 = new SortedList();
        sortedList_3 = new SortedList();
        if (File.Exists(string_6 + str1))
        {
            string_1 = method_0(string_6 + str1, sortedList_0, sortedList_2);
            string_5 = str1;
        }
        else
        {
            GClass37.smethod_0("No string filename: " + str1);
            var str2 = string_0.Substring(0, string_0.IndexOf("-"));
            var files = Directory.GetFiles(string_6, str2 + "*.xml");
            if (files != null && files.Length > 0)
            {
                GClass37.smethod_0("Using alternate strings: " + files[0]);
                string_5 = files[0];
                string_1 = method_0(files[0], sortedList_0, sortedList_2);
            }
            else
            {
                GClass37.smethod_0("No other language files matching " + str2 + "*.xml, falling back to en-US");
            }
        }

        if (string_5 != null && string_5.ToLower().IndexOf("en-us.xml") != -1)
            return;
        string_2 = method_0(string_6 + "en-US.xml", sortedList_1, sortedList_3);
    }

    public static void smethod_0(string string_6)
    {
        try
        {
            gclass30_0 = new GClass30(string_6);
            StartupClass.numbers_string = smethod_1(2);
        }
        catch (Exception ex)
        {
            GClass37.smethod_1("** Exception reading strings: " + ex.Message);
            GClass37.smethod_1("** Last string: " + gclass30_0.string_3);
        }
    }

    private string[] method_0(string string_6, SortedList sortedList_4, SortedList sortedList_5)
    {
        GClass37.smethod_0("Loading: " + string_6);
        var arrayList = new ArrayList();
        try
        {
            XmlReader xmlReader = new XmlTextReader(string_6);
            while (xmlReader.Read())
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "S")
                {
                    var attribute1 = xmlReader.GetAttribute("id");
                    var attribute2 = xmlReader.GetAttribute("text");
                    if (attribute1 != null && attribute1.Length > 0)
                        string_3 = attribute1;
                    if (attribute1 == null)
                        throw new Exception("Missing id attribute!");
                    if (attribute2 == null)
                        throw new Exception("Missing text attribute!");
                    if (smethod_8(attribute1))
                    {
                        var num = int.Parse(attribute1);
                        if (num != arrayList.Count + 1)
                            GClass37.smethod_0("ID #" + num + " is out of order, should be #" + (arrayList.Count + 1));
                        arrayList.Add(attribute2);
                        sortedList_5.Add(attribute1, attribute2);
                    }
                    else
                    {
                        sortedList_4.Add(attribute1, attribute2);
                    }
                }

            xmlReader.Close();
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("** Exception loading strings from " + string_6 + ": " + ex.Message + ex.StackTrace);
            GClass37.smethod_0("** Last string: " + string_3);
        }

        return (string[])arrayList.ToArray(typeof(string));
    }

    public static string smethod_1(int int_0)
    {
        var key = int_0.ToString();
        return !gclass30_0.sortedList_2.ContainsKey(key)
            ? !gclass30_0.sortedList_3.ContainsKey(key)
                ? "(bogus string #" + int_0 + ")"
                : (string)gclass30_0.sortedList_3[key]
            : (string)gclass30_0.sortedList_2[key];
    }

    public static string smethod_2(int int_0, params object[] object_0)
    {
        return string.Format(smethod_1(int_0), object_0);
    }

    public static void smethod_3(Form form_0, string string_6)
    {
        GClass37.smethod_1("Localizing form: " + string_6);
        var str = smethod_4(string_6);
        if (str != null)
            form_0.Text = str;
        gclass30_0.method_1(form_0.Controls, string_6);
    }

    private void method_1(Control.ControlCollection controlCollection_0, string string_6)
    {
        foreach (Control control_0 in (ArrangedElementCollection)controlCollection_0)
        {
            var string_6_1 = smethod_4(string_6 + "." + control_0.Name);
            if (string_6_1 != null)
                method_2(control_0, string_6_1);
            method_1(control_0.Controls, string_6);
            if (control_0.GetType() == typeof(TabControl))
                foreach (TabPage tabPage in ((TabControl)control_0).TabPages)
                {
                    var str = smethod_4(string_6 + "." + tabPage.Name);
                    if (str != null)
                        tabPage.Text = str;
                    method_1(tabPage.Controls, string_6);
                }
        }
    }

    private void method_2(Control control_0, string string_6)
    {
        var type = control_0.GetType();
        if (type == typeof(Label))
            control_0.Text = string_6;
        else if (type == typeof(Button))
            control_0.Text = string_6;
        else if (type == typeof(RadioButton))
            control_0.Text = string_6;
        else if (type == typeof(CheckBox))
            control_0.Text = string_6;
        else if (type == typeof(GroupBox))
            control_0.Text = string_6;
        else if (type == typeof(TabPage))
            control_0.Text = string_6;
        else
            GClass37.smethod_1("! Couldn't localize: " + control_0.Name);
    }

    public static string smethod_4(string string_6)
    {
        string str = null;
        if (gclass30_0.sortedList_0.ContainsKey(string_6))
            str = (string)gclass30_0.sortedList_0[string_6];
        if (str == null && gclass30_0.sortedList_1 != null && gclass30_0.sortedList_1.ContainsKey(string_6))
            str = (string)gclass30_0.sortedList_1[string_6];
        return str;
    }

    public static bool smethod_5(string string_6)
    {
        return (gclass30_0.sortedList_1 != null && gclass30_0.sortedList_1.ContainsKey(string_6)) ||
               gclass30_0.sortedList_0.ContainsKey(string_6);
    }

    public static string smethod_6(string string_6, params object[] object_0)
    {
        return string.Format(smethod_4(string_6), object_0);
    }

    public static void smethod_7(ComboBox comboBox_0, string string_6)
    {
        var num = 0;
        while (true)
        {
            var str = smethod_4(string_6 + num);
            if (str != null)
            {
                comboBox_0.Items.Add(str);
                ++num;
            }
            else
            {
                break;
            }
        }
    }

    public static bool smethod_8(string string_6)
    {
        return regex_0.Match(string_6).Success;
    }
}