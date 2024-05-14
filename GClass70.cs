// Decompiled with JetBrains decompiler
// Type: GClass70
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class GClass70
{
    public bool bool_0;
    private readonly byte[] byte_0;
    public int int_0;
    protected string string_0;
    public string string_1;
    public string string_2;
    public string string_3;
    public string string_4;

    public GClass70(string string_5, byte[] byte_1)
    {
        string_0 = string_5;
        string_1 = "GET";
        string_2 = "Glider/1.01";
        string_4 =
            "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/pdf, text/xml, application/octet-stream";
        bool_0 = true;
        string_3 = "(no error)";
        int_0 = 15000;
        byte_0 = byte_1;
        if (byte_1 == null)
            return;
        string_1 = "POST";
    }

    public string method_0()
    {
        return Encoding.ASCII.GetString(method_1());
    }

    public byte[] method_1()
    {
        try
        {
            var socket_0 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket_0.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, int_0);
            var str1 = method_2();
            var str2 = method_3().Replace(' ', '+');
            IPEndPoint remoteEP = null;
            if (bool_0)
            {
                var hostEntry = Dns.GetHostEntry(str1);
                if (hostEntry == null)
                {
                    string_3 = "DNS failed for host";
                    return null;
                }

                foreach (var address in hostEntry.AddressList)
                    if (address.AddressFamily == AddressFamily.InterNetwork && !address.IsIPv6LinkLocal &&
                        !address.IsIPv6Multicast && !address.IsIPv6SiteLocal)
                    {
                        remoteEP = new IPEndPoint(address, 80);
                        break;
                    }
            }
            else
            {
                remoteEP = new IPEndPoint(IPAddress.Parse(str1), 80);
            }

            socket_0.Connect(remoteEP);
            var str3 = string_1 + " " + str2 + " HTTP/1.0\r\nHost: " + str1 + "\r\nUser-Agent: " + string_2 +
                       "\r\nAccept: */*\r\n";
            if (string_1 == "POST")
                str3 = str3 + "Content-Length: " + byte_0.Length + "\r\n";
            var s = str3 + "\r\n";
            socket_0.Send(Encoding.ASCII.GetBytes(s));
            if (string_1 == "POST")
                method_5(socket_0);
            var stringBuilder = new StringBuilder();
            var str4 = method_4(socket_0);
            var strArray = str4.Split(' ');
            if (strArray.Length != 3)
            {
                string_3 = "Bogus header back: " + str4;
                return null;
            }

            if (strArray[1] != "200")
            {
                string_3 = "Bogus server response code: " + strArray[1];
                return null;
            }

            var num1 = 0;
            string str5;
            do
            {
                str5 = method_4(socket_0);
                if (str5 != null)
                {
                    if (str5.StartsWith("Content-Length: "))
                    {
                        var startIndex = str5.IndexOf(' ') + 1;
                        num1 = int.Parse(str5.Substring(startIndex, str5.Length - startIndex));
                    }
                }
                else
                {
                    goto label_22;
                }
            } while (str5.Length != 0);

            goto label_23;
            label_22:
            string_3 = "Unexpected connection drop during header retrieval";
            return null;
            label_23:
            var memoryStream = new MemoryStream(num1 != 0 ? num1 : 4096);
            var num2 = 0;
            var buffer = new byte[4096];
            do
            {
                var count = socket_0.Receive(buffer);
                if (count != 0)
                {
                    memoryStream.Write(buffer, 0, count);
                    num2 += count;
                }
                else
                {
                    break;
                }
            } while (num1 == 0 || num2 != num1);

            socket_0.Close();
            return memoryStream.ToArray();
        }
        catch (Exception ex)
        {
            string_3 = "Exception in FireRequest: " + ex.Message + ex.StackTrace;
            return null;
        }
    }

    private string method_2()
    {
        var str = string_0.Substring(7);
        if (str.IndexOf('/') > -1)
            str = str.Substring(0, str.IndexOf('/'));
        return str;
    }

    private string method_3()
    {
        return string_0.Substring(method_2().Length + 7);
    }

    private string method_4(Socket socket_0)
    {
        var stringBuilder = new StringBuilder();
        var buffer = new byte[1];
        while (socket_0.Receive(buffer) != 0)
        {
            if (buffer[0] == 10)
                return stringBuilder.ToString();
            if (buffer[0] != 13)
                stringBuilder.Append((char)buffer[0]);
        }

        return null;
    }

    private void method_5(Socket socket_0)
    {
        int size;
        for (var offset = 0; offset < byte_0.Length; offset += size)
        {
            size = byte_0.Length - offset;
            if (size > 4096)
                size = 4096;
            socket_0.Send(byte_0, offset, size, SocketFlags.None);
        }
    }
}