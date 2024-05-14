// Decompiled with JetBrains decompiler
// Type: GClass79
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Glider.Common.Objects;

public class GClass79
{
    private bool bool_0;
    public GClass36 gclass36_0;
    private GClass36 gclass36_1 = new GClass36(1000);
    public GUnit gunit_0;
    public int int_0;
    private readonly List<GClass77> list_0;
    private Socket socket_0;
    private Thread thread_0;

    public GClass79()
    {
        int_0 = GClass61.gclass61_0.method_3("ListenPort");
        thread_0 = null;
        socket_0 = null;
        bool_0 = false;
        list_0 = new List<GClass77>();
        gunit_0 = null;
        gclass36_0 = new GClass36(GClass61.gclass61_0.method_3("CaptureDelay"));
        gclass36_0.method_5();
    }

    public void method_0()
    {
        try
        {
            socket_0 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket_0.Bind(new IPEndPoint(IPAddress.Any, int_0));
            socket_0.Listen(5);
            thread_0 = new Thread(method_2);
            thread_0.Start();
        }
        catch (Exception ex)
        {
            Logger.LogMessage(MessageProvider.smethod_2(295, ex.Message));
            thread_0 = null;
        }
    }

    public void method_1()
    {
        bool_0 = true;
        if (thread_0 != null)
        {
            thread_0.Interrupt();
            thread_0 = null;
        }

        if (socket_0 != null)
        {
            socket_0.Close();
            socket_0 = null;
        }

        while (list_0.Count > 0)
            list_0[0].method_1();
    }

    private void method_2()
    {
        try
        {
            method_4();
        }
        catch (Exception ex)
        {
            if (bool_0)
                return;
            Logger.LogMessage(MessageProvider.smethod_2(296, ex.Message + ex.StackTrace));
        }
    }

    public void method_3(GClass77 gclass77_0)
    {
        lock (list_0)
        {
            list_0.Remove(gclass77_0);
        }
    }

    public void method_4()
    {
        while (true)
        {
            var socket_1 = socket_0.Accept();
            Logger.LogMessage(MessageProvider.smethod_2(297, socket_1.RemoteEndPoint.ToString()));
            lock (list_0)
            {
                var gclass77 = new GClass77(this, socket_1);
                list_0.Add(gclass77);
                gclass77.method_0();
            }

            Thread.Sleep(1000);
        }
    }

    public void method_5(int int_1, string string_0)
    {
        try
        {
            lock (list_0)
            {
                foreach (var gclass77 in list_0)
                    gclass77.method_5(int_1, string_0);
            }
        }
        catch (Exception ex)
        {
        }
    }
}