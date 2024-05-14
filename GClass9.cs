// Decompiled with JetBrains decompiler
// Type: GClass9
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using Glider.Common;
using Glider.Common.Objects;

public class GClass9
{
    private bool bool_0;
    private bool bool_1;
    private DateTime dateTime_0;
    private int int_0;
    private readonly List<GClass10> list_0;
    private readonly List<GClass10> list_1;
    private string string_0;
    private string string_1;
    private Thread thread_0;

    public GClass9()
    {
        bool_1 = false;
        thread_0 = null;
        bool_0 = false;
        string_0 = "";
        string_1 = "";
        list_0 = new List<GClass10>();
        list_1 = new List<GClass10>();
    }

    public void method_0(string string_2)
    {
        if (!bool_1)
            return;
        lock (this)
        {
            list_0.Add(new GClass10(string_2));
        }
    }

    public void method_1(string string_2)
    {
        if (!bool_1)
            return;
        lock (this)
        {
            list_1.Add(new GClass10(string_2));
        }
    }

    public void method_2()
    {
        method_4("Applying config, enabled = " + GClass61.gclass61_0.method_5("WebNotifyEnabled"));
        if (bool_1 && GClass61.gclass61_0.method_5("WebNotifyEnabled") &&
            GClass61.gclass61_0.method_2("WebNotifyURL") == string_0 &&
            GClass61.gclass61_0.method_2("WebNotifyCredentials") == string_1)
        {
            method_4("Config is same on started WebNotify, skipping apply step");
        }
        else
        {
            string_0 = GClass61.gclass61_0.method_2("WebNotifyURL");
            string_1 = GClass61.gclass61_0.method_2("WebNotifyCredentials");
            method_5();
            if (!GClass61.gclass61_0.method_5("WebNotifyEnabled") || bool_1)
                return;
            method_3();
        }
    }

    public void method_3()
    {
    }

    private void method_4(string string_2)
    {
        GClass37.smethod_0("WN: " + string_2);
    }

    public void method_5()
    {
        if (!bool_1)
            return;
        if (thread_0 != null)
        {
            method_4("Stopping background thread");
            bool_0 = true;
            thread_0.Interrupt();
            thread_0.Join();
            thread_0 = null;
        }

        bool_1 = false;
    }

    private void method_6()
    {
        method_4("Notify thread starting up");
        try
        {
            method_7();
        }
        catch (ThreadInterruptedException ex)
        {
        }
        catch (Exception ex)
        {
            if (bool_0)
                return;
            method_4("** Exception from WebNotifyInner: " + ex.Message + ex.StackTrace);
            thread_0 = null;
            bool_1 = false;
        }
    }

    private void method_7()
    {
        while (true)
        {
            do
            {
                Thread.Sleep(1000);
            } while ((DateTime.Now - dateTime_0).TotalSeconds < int_0);

            method_4("Touching remote server");
            try
            {
                method_8();
            }
            catch (Exception ex)
            {
                method_4("** Exception touching server: " + ex.Message + ex.StackTrace);
            }

            dateTime_0 = DateTime.Now;
        }
    }

    private void method_8()
    {
        XmlDocument xmlDocument;
        lock (this)
        {
            xmlDocument = method_10();
        }

        var w1 = new MemoryStream();
        var w2 = new XmlTextWriter(w1, Encoding.ASCII);
        w2.Formatting = Formatting.Indented;
        xmlDocument.WriteContentTo(w2);
        w2.Flush();
        w2.Close();
        var array = w1.ToArray();
        var str = Encoding.ASCII.GetString(array);
        if (GClass61.gclass61_0.method_5("LogNotifyQuery"))
            GClass37.smethod_1("xmlText:\r\n" + str);
        if (method_9(array))
            lock (this)
            {
                list_0.Clear();
                list_1.Clear();
            }

        int_0 = 5;
    }

    private bool method_9(byte[] byte_0)
    {
        method_4("Posting XML");
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(GClass61.gclass61_0.method_2("WebNotifyURL"));
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept =
                "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/pdf, text/xml, application/octet-stream";
            httpWebRequest.UserAgent = "Glider/1.0";
            httpWebRequest.ContentLength = byte_0.Length;
            httpWebRequest.GetRequestStream().Write(byte_0, 0, byte_0.Length);
            var response = httpWebRequest.GetResponse();
            var streamReader = new StreamReader(response.GetResponseStream());
            var end = streamReader.ReadToEnd();
            if (GClass61.gclass61_0.method_5("LogNotifyResponse"))
                GClass37.smethod_1("Response from post:\r\n" + end);
            method_4("Response received from post: " + end.Length + " bytes");
            streamReader.Close();
            response.Close();
            method_4("Sent!");
            return true;
        }
        catch (Exception ex)
        {
            method_4("* Exception posting XML: " + ex.Message + ex.StackTrace);
            return false;
        }
    }

    private XmlDocument method_10()
    {
        var xmlDocument = new XmlDocument();
        var element1 = xmlDocument.CreateElement("WebGliderQuery");
        xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", null, null));
        xmlDocument.AppendChild(element1);
        method_12(element1, "CustomerID", GClass61.gclass61_0.method_2("WebNotifyCredentials"));
        method_12(element1, "QueryID", GClass61.gclass61_0.method_2("WebNotifyCredentials") + "-" + Guid.NewGuid());
        method_12(element1, "Profile", StartupClass.string_5 == null ? "(no profile)" : StartupClass.string_5);
        method_12(element1, "Gliding", StartupClass.glideMode_0 == GlideMode.Auto ? "1" : "0");
        method_12(element1, "GliderVersion", "1.8.0");
        method_12(element1, "GliderSubVersion", "Release");
        if (StartupClass.bool_13)
        {
            var element2 = xmlDocument.CreateElement("Player");
            method_12(element2, "Name", GPlayerSelf.Me.Name);
            method_12(element2, "Realm", GProcessMemoryManipulator.smethod_32());
            method_12(element2, "Race", ((int)GPlayerSelf.Me.PlayerRace).ToString());
            method_12(element2, "Class", ((int)GPlayerSelf.Me.PlayerClass).ToString());
            method_12(element2, "Level", GPlayerSelf.Me.Level.ToString());
            method_12(element2, "Experience", GPlayerSelf.Me.Experience.ToString());
            method_12(element2, "NextLevelExperience", GPlayerSelf.Me.NextLevelExperience.ToString());
            method_12(element2, "Health", GPlayerSelf.Me.Health.ToString());
            method_12(element2, "HealthMax", GPlayerSelf.Me.HealthMax.ToString());
            method_12(element2, "SecondAttr", GPlayerSelf.Me.Power2.ToString());
            method_12(element2, "SecondAttrMax", GPlayerSelf.Me.Power2Max.ToString());
            method_12(element2, "KLD", StartupClass.int_7 + "/" + StartupClass.int_8 + "/" + StartupClass.int_9);
            method_12(element2, "XPHour", StartupClass.smethod_29().ToString());
            method_12(element2, "Location: ", GPlayerSelf.Me.Location.ToString());
            method_12(element2, "Heading: ", GPlayerSelf.Me.Heading.ToString());
            method_12(element2, "WorldMap: ",
                GProcessMemoryManipulator.smethod_9(GClass18.gclass18_0.method_4("WorldMap"), 100, "WorldMap"));
            element1.AppendChild(element2);
            var target = GPlayerSelf.Me.Target;
            if (target != null)
            {
                var element3 = xmlDocument.CreateElement("Target");
                method_12(element3, "Name", target.Name);
                method_12(element3, "Health", target.Health.ToString());
                method_12(element3, "Level", target.Level.ToString());
                method_12(element3, "Location: ", target.Location.ToString());
                method_12(element3, "Heading: ", target.Heading.ToString());
                element1.AppendChild(element3);
            }

            var element4 = xmlDocument.CreateElement("PlayersNearby");
            foreach (var gclass21 in GClass21.smethod_1())
            {
                var element5 = xmlDocument.CreateElement("Player");
                method_13(element5, "Name", gclass21.gplayer_0.Name);
                method_13(element5, "Race", ((int)gclass21.gplayer_0.PlayerRace).ToString());
                method_13(element5, "Class", ((int)gclass21.gplayer_0.PlayerClass).ToString());
                method_13(element5, "Level", gclass21.gplayer_0.Level.ToString());
                method_13(element5, "Seconds", (DateTime.Now - gclass21.dateTime_0).TotalSeconds.ToString());
                method_13(element5, "Location", gclass21.gplayer_0.Location.ToString());
                method_13(element5, "Heading", gclass21.gplayer_0.Heading.ToString());
                element4.AppendChild(element5);
            }

            element1.AppendChild(element4);
        }

        method_11(element1, list_0, "GliderLog");
        method_11(element1, list_1, "ChatLog");
        return xmlDocument;
    }

    private void method_11(XmlElement xmlElement_0, List<GClass10> list_2, string string_2)
    {
        var element1 = xmlElement_0.OwnerDocument.CreateElement(string_2);
        xmlElement_0.AppendChild(element1);
        foreach (var gclass10 in list_2)
        {
            var element2 = xmlElement_0.OwnerDocument.CreateElement("LogEntry");
            method_13(element2, "Timestamp", gclass10.dateTime_0.ToString());
            element2.InnerText = gclass10.string_0;
            element1.AppendChild(element2);
        }
    }

    private XmlElement method_12(XmlElement xmlElement_0, string string_2, string string_3)
    {
        var element = xmlElement_0.OwnerDocument.CreateElement(string_2);
        element.InnerText = string_3;
        xmlElement_0.AppendChild(element);
        return element;
    }

    private void method_13(XmlElement xmlElement_0, string string_2, string string_3)
    {
        var attribute = xmlElement_0.OwnerDocument.CreateAttribute(string_2);
        attribute.Value = string_3;
        xmlElement_0.Attributes.Append(attribute);
    }
}