// Decompiled with JetBrains decompiler
// Type: TimedStringEntry
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
public class TimedStringEntry
{
    public GameTimer gclass36_0;
    public string string_0;

    public TimedStringEntry(string string_1, int int_0)
    {
        string_0 = string_1;
        gclass36_0 = new GameTimer(int_0);
        gclass36_0.method_4();
    }
}
