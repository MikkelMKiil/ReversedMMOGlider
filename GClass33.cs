// Decompiled with JetBrains decompiler
// Type: GClass33
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Collections.Generic;
using Glider.Common.Objects;

public class GClass33
{
    public List<GLocation> list_0;

    public GClass33(string string_0)
    {
        try
        {
            method_0(string_0);
        }
        catch (Exception ex)
        {
            Logger.LogMessage("* Unable to parse loot steps: " + ex.Message);
            method_0(GClass61.string_0);
        }
    }

    private void method_0(string string_0)
    {
        list_0 = new List<GLocation>();
        var num1 = 0;
        var startIndex1 = 0;
        var glocationList = new List<GLocation>();
        while (true)
        {
            var startIndex2 = string_0.IndexOf('(', startIndex1);
            if (startIndex2 != -1)
            {
                var num2 = string_0.IndexOf(')', startIndex2);
                if (num2 != -1)
                {
                    var num3 = string_0.IndexOf(' ', startIndex2);
                    if (num3 != -1)
                    {
                        try
                        {
                            var num4 = double.Parse(string_0.Substring(startIndex2 + 1, num3 - startIndex2 - 1));
                            var num5 = double.Parse(string_0.Substring(num3 + 1, num2 - num3 - 1));
                            list_0.Add(new GLocation(num4 / 100.0, num5 / 100.0));
                            var num6 = num4 + (num4 - 50.0);
                            var num7 = num5 + (num5 - 50.0);
                            glocationList.Add(new GLocation(num6 / 100.0, num7 / 100.0));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Couldn't parse step #" + num1);
                        }

                        startIndex1 = num2;
                        ++num1;
                    }
                    else
                    {
                        goto label_8;
                    }
                }
                else
                {
                    break;
                }
            }
            else
            {
                goto label_9;
            }
        }

        throw new Exception("Missing close paren near step #" + num1);
        label_8:
        throw new Exception("Missing space near step #" + num1);
        label_9:
        foreach (var glocation in glocationList)
            list_0.Add(glocation);
    }
}