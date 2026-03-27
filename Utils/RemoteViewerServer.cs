// Decompiled with JetBrains decompiler
// Type: RemoteViewerServer
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common.Objects;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class RemoteViewerServer
{
    private bool isStopping;
    public GameTimer gclass36_0;
    public GUnit gunit_0;
    public int int_0;
    private readonly List<RemoteViewerClient> clients;
    private Socket listenerSocket;
    private Thread listenerThread;

    public RemoteViewerServer()
    {
        int_0 = ConfigManager.gclass61_0.method_3("ListenPort");
        listenerThread = null;
        listenerSocket = null;
        isStopping = false;
        clients = new List<RemoteViewerClient>();
        gunit_0 = null;
        gclass36_0 = new GameTimer(ConfigManager.gclass61_0.method_3("CaptureDelay"));
        gclass36_0.method_5();
    }

    public void method_0()
    {
        try
        {
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerSocket.Bind(new IPEndPoint(IPAddress.Any, int_0));
            listenerSocket.Listen(5);
            listenerThread = new Thread(method_2);
            listenerThread.Start();
        }
        catch (Exception ex)
        {
            Logger.LogMessage(MessageProvider.smethod_2(295, ex.Message));
            listenerThread = null;
        }
    }

    public void method_1()
    {
        isStopping = true;
        if (listenerThread != null)
        {
            listenerThread.Interrupt();
            listenerThread = null;
        }

        if (listenerSocket != null)
        {
            listenerSocket.Close();
            listenerSocket = null;
        }

        while (clients.Count > 0)
            clients[0].method_1();
    }

    private void method_2()
    {
        try
        {
            method_4();
        }
        catch (Exception ex)
        {
            if (isStopping)
                return;
            Logger.LogMessage(MessageProvider.smethod_2(296, ex.Message + ex.StackTrace));
        }
    }

    public void method_3(RemoteViewerClient client)
    {
        lock (clients)
        {
            clients.Remove(client);
        }
    }

    public void method_4()
    {
        while (true)
        {
            var clientSocket = listenerSocket.Accept();
            Logger.LogMessage(MessageProvider.smethod_2(297, clientSocket.RemoteEndPoint.ToString()));
            lock (clients)
            {
                var client = new RemoteViewerClient(this, clientSocket);
                clients.Add(client);
                client.method_0();
            }

            Thread.Sleep(1000);
        }
    }

    public void method_5(int messageChannel, string message)
    {
        try
        {
            lock (clients)
            {
                foreach (var client in clients)
                    client.method_5(messageChannel, message);
            }
        }
        catch (Exception ex)
        {
            Logger.LogMessage("Remote viewer broadcast failed: " + ex.Message);
        }
    }
}
