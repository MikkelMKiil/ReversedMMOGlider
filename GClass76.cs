// Decompiled with JetBrains decompiler
// Type: GClass76
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.IO;

public class GClass76 : GInterface0
{
    private readonly string string_0;

    public GClass76(string string_1)
    {
        string_0 = string_1;
    }

    // Changed from explicit to implicit implementation
    public void imethod_2(string string_1)
    {
        var now = DateTime.Now;
        try
        {
            var streamWriter = File.AppendText(string_0);
            streamWriter.WriteLine(now.ToString("HH:mm:ss.ffff ") + string_1);
            streamWriter.Flush();
            streamWriter.Close();
        }
        catch (IOException ex)
        {
            Console.WriteLine("Unable to write to log: " + ex.Message);
        }
    }

    public void imethod_3(string string_1)
    {
        this.imethod_2("[Debug] " + string_1);
    }

    public void imethod_4()
    {
    }

    public void imethod_1()
    {
    }

    public void imethod_0()
    {
    }
}