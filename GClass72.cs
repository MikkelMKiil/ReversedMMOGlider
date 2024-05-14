// Decompiled with JetBrains decompiler
// Type: GClass72
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections.Generic;
using System.Xml;
using Glider.Common.Objects;

public class GClass72
{
    public bool bool_0;
    public bool bool_1;
    public bool bool_2;
    public GProfile gprofile_0;
    private readonly List<GClass51> list_0;
    public string string_0;
    public string string_1;
    public string string_2;

    public GClass72()
    {
        list_0 = new List<GClass51>();
        bool_2 = true;
    }

    public string[] method_0(int int_0)
    {
        return new string[2]
        {
            int_0.ToString(),
            StartupClass.smethod_42(string_0)
        };
    }

    public void method_1(XmlElement xmlElement_0)
    {
        var element1 = xmlElement_0.OwnerDocument.CreateElement("Step");
        xmlElement_0.AppendChild(element1);
        method_3(element1, "ProfileName", string_0);
        method_3(element1, "Mount", bool_1.ToString());
        method_3(element1, "UseFlight", bool_0.ToString());
        method_3(element1, "Startable", bool_2.ToString());
        if (bool_0)
        {
            method_3(element1, "Flightmaster", string_1);
            method_3(element1, "Destination", string_2);
        }

        foreach (var gclass51 in list_0)
        {
            var element2 = xmlElement_0.OwnerDocument.CreateElement("Condition");
            method_2(element2, "ConditionType", ((int)gclass51.genum6_0).ToString());
            method_2(element2, "Parameter", gclass51.int_0.ToString());
            method_2(element2, "NextStep", gclass51.int_1.ToString());
            element1.AppendChild(element2);
        }
    }

    private void method_2(XmlElement xmlElement_0, string string_3, string string_4)
    {
        var attribute = xmlElement_0.OwnerDocument.CreateAttribute(string_3);
        attribute.Value = string_4;
        xmlElement_0.Attributes.Append(attribute);
    }

    private void method_3(XmlElement xmlElement_0, string string_3, string string_4)
    {
        var element = xmlElement_0.OwnerDocument.CreateElement(string_3);
        element.InnerText = string_4;
        xmlElement_0.AppendChild(element);
    }

    public void method_4(XmlNode xmlNode_0)
    {
        string_0 = xmlNode_0.SelectSingleNode("ProfileName").InnerText.Trim();
        bool_1 = xmlNode_0.SelectSingleNode("Mount").InnerText == "True";
        bool_0 = xmlNode_0.SelectSingleNode("UseFlight").InnerText == "True";
        if (bool_0)
        {
            string_1 = xmlNode_0.SelectSingleNode("Flightmaster").InnerText.Trim();
            string_2 = xmlNode_0.SelectSingleNode("Destination").InnerText.Trim();
        }

        var xmlNode = xmlNode_0.SelectSingleNode("Startable");
        if (xmlNode != null)
            bool_2 = xmlNode.InnerText.Trim() == "True";
        foreach (XmlNode selectNode in xmlNode_0.SelectNodes("Condition"))
            method_7(new GClass51((GEnum6)int.Parse(selectNode.Attributes["ConditionType"].Value),
                int.Parse(selectNode.Attributes["Parameter"].Value),
                int.Parse(selectNode.Attributes["NextStep"].Value)));
    }

    public void method_5(GEnum6 genum6_0)
    {
        var gclass51 = method_6(genum6_0);
        if (gclass51 == null)
            return;
        list_0.Remove(gclass51);
    }

    public GClass51 method_6(GEnum6 genum6_0)
    {
        for (var index = 0; index < list_0.Count; ++index)
            if (list_0[index].genum6_0 == genum6_0)
                return list_0[index];
        return null;
    }

    public void method_7(GClass51 gclass51_0)
    {
        method_5(gclass51_0.genum6_0);
        list_0.Add(gclass51_0);
    }

    public void method_8(int int_0)
    {
        var index = 0;
        while (index < list_0.Count)
            if (list_0[index].int_1 == int_0)
                list_0.RemoveAt(index);
            else
                ++index;
        foreach (var gclass51 in list_0)
            if (gclass51.int_1 > int_0)
                --gclass51.int_1;
    }

    public void method_9(int int_0, int int_1)
    {
        foreach (var gclass51 in list_0)
            if (gclass51.int_1 == int_0)
                gclass51.int_1 = int_1;
            else if (gclass51.int_1 == int_1)
                gclass51.int_1 = int_0;
    }

    public void method_10()
    {
        gprofile_0 = new GProfile();
        if (!gprofile_0.Load(string_0))
        {
            GClass37.smethod_0("Can't load profile: \"" + gprofile_0 + "\", giving up!");
            StartupClass.smethod_27(false, "LoadProfileFail");
        }
        else
        {
            GClass37.smethod_1("Loaded profile: " + string_0);
        }
    }
}