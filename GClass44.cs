// Decompiled with JetBrains decompiler
// Type: GClass44
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using Glider.Common.Objects;

public class GClass44
{
    public SortedList<int, GClass45> sortedList_0;
    public SortedList<int, string> sortedList_1;

    public GClass44()
    {
        sortedList_0 = new SortedList<int, GClass45>();
        sortedList_1 = new SortedList<int, string>();
        method_0();
    }

    public void method_0()
    {
        var str = "Debuffs.xml";
        if (!File.Exists(str))
            return;
        try
        {
            var num1 = 0;
            var num2 = 0;
            var xmlTextReader = new XmlTextReader(str);
            while (xmlTextReader.Read())
                if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name == "Debuff")
                {
                    var gclass45 = new GClass45();
                    while (xmlTextReader.MoveToNextAttribute())
                        switch (xmlTextReader.Name.ToUpper())
                        {
                            case "ID":
                                gclass45.int_0 = int.Parse(xmlTextReader.Value, NumberStyles.HexNumber);
                                continue;
                            case "DESCRIPTION":
                                gclass45.string_0 = xmlTextReader.Value;
                                continue;
                            case "TYPE":
                                gclass45.genum4_0 = method_2(xmlTextReader.Value);
                                continue;
                            case "COMBAT":
                                if (xmlTextReader.Value.ToLower() == "true")
                                {
                                    gclass45.bool_0 = true;
                                }

                                continue;
                            default:
                                continue;
                        }

                    if (!sortedList_0.ContainsKey(gclass45.int_0))
                    {
                        sortedList_0.Add(gclass45.int_0, gclass45);
                        ++num1;
                    }
                    else
                    {
                        ++num2;
                    }
                }

            xmlTextReader.Close();
            GClass37.smethod_0(GClass30.smethod_2(797, num1, num2));
        }
        catch (Exception ex)
        {
            GClass37.smethod_0(GClass30.smethod_2(799, ex.Message, ex.StackTrace));
        }
    }

    public void method_1(int int_0, string string_0, GEnum4 genum4_0)
    {
        if (sortedList_0.ContainsKey(int_0))
            return;
        sortedList_0.Add(int_0, new GClass45(int_0, genum4_0, string_0, false));
    }

    protected GEnum4 method_2(string string_0)
    {
        switch (string_0.ToUpper())
        {
            case "MAGIC":
                return GEnum4.const_1;
            case "POISON":
                return GEnum4.const_2;
            case "CURSE":
                return GEnum4.const_3;
            case "DISEASE":
                return GEnum4.const_4;
            default:
                return GEnum4.const_0;
        }
    }

    protected string method_3(GEnum4 genum4_0)
    {
        switch (genum4_0)
        {
            case GEnum4.const_1:
                return "Magic";
            case GEnum4.const_2:
                return "Poison";
            case GEnum4.const_3:
                return "Curse";
            case GEnum4.const_4:
                return "Disease";
            default:
                return "Unknown";
        }
    }

    public void method_4()
    {
        if (sortedList_0.Keys.Count == 0)
            return;
        var filename = "Debuffs.xml";
        try
        {
            var xmlTextWriter = new XmlTextWriter(filename, Encoding.ASCII);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("Debuffs");
            foreach (var gclass45 in sortedList_0.Values)
            {
                xmlTextWriter.WriteStartElement("Debuff");
                xmlTextWriter.WriteAttributeString("ID", gclass45.int_0.ToString("x"));
                xmlTextWriter.WriteAttributeString("Type", method_3(gclass45.genum4_0));
                xmlTextWriter.WriteAttributeString("Description", gclass45.string_0);
                xmlTextWriter.WriteEndElement();
            }

            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndDocument();
            xmlTextWriter.Flush();
            xmlTextWriter.Close();
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("* Exception saving new debuffs: " + ex.Message + ex.StackTrace);
        }
    }

    public bool method_5(int int_0, GEnum4 genum4_0, bool bool_0)
    {
        if (!sortedList_0.ContainsKey(int_0))
            return false;
        var flag = sortedList_0[int_0].genum4_0 == genum4_0;
        if (bool_0 && flag)
            flag = sortedList_0[int_0].bool_0;
        return flag;
    }

    public bool method_6(int int_0)
    {
        return sortedList_0.ContainsKey(int_0);
    }

    public bool method_7(GEnum4 genum4_0, string string_0, bool bool_0)
    {
        if (!GClass61.gclass61_0.method_5("RemoveDebuffs"))
            return false;
        foreach (var gbuff in GPlayerSelf.Me.GetBuffSnapshot())
            if (method_5(gbuff.SpellID, genum4_0, bool_0))
            {
                GContext.Main.CastSpell(string_0);
                return true;
            }

        return false;
    }

    public void method_8()
    {
        if (!StartupClass.bool_13)
            return;
        foreach (var gbuff in GPlayerSelf.Me.GetBuffSnapshot())
            if (gbuff.SpellID > 0 && !sortedList_0.ContainsKey(gbuff.SpellID) &&
                !sortedList_1.ContainsKey(gbuff.SpellID) && gbuff.SpellName != null && gbuff.SpellName.Length > 0)
            {
                GClass37.smethod_1(GClass30.smethod_2(800, gbuff.SpellID.ToString("x"), gbuff.SpellName));
                sortedList_1.Add(gbuff.SpellID, gbuff.SpellName);
            }
    }

    [SpecialName]
    public int method_9()
    {
        return sortedList_0.Keys.Count;
    }

    public void method_10()
    {
        if (sortedList_1.Keys.Count == 0)
            return;
        try
        {
            var xmlTextWriter = new XmlTextWriter("NewDebuffs.xml", Encoding.ASCII);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("Debuffs");
            foreach (var key in sortedList_1.Keys)
            {
                xmlTextWriter.WriteStartElement("Debuff");
                xmlTextWriter.WriteAttributeString("ID", key.ToString("x"));
                xmlTextWriter.WriteAttributeString("Description", sortedList_1[key]);
                xmlTextWriter.WriteEndElement();
            }

            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndDocument();
            xmlTextWriter.Flush();
            xmlTextWriter.Close();
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("* Exception saving new debuffs: " + ex.Message + ex.StackTrace);
        }
    }

    public void method_11()
    {
        try
        {
            foreach (var gbuff in GPlayerSelf.Me.GetBuffSnapshot())
                GClass37.smethod_0(GClass30.smethod_2(798, gbuff.SpellID.ToString("x"), gbuff.SpellName));
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("** Exception logging debuffs: " + ex.Message + ex.StackTrace);
        }
    }
}