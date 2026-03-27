using System;

#nullable disable

internal class BoolStringOption
{
    public bool bool_0;
    public string string_0;
}

internal class IntStringEntry
{
    public int int_0;
    public string string_0;

    public IntStringEntry()
    {
        int_0 = 0;
        string_0 = null;
    }

    public override string ToString()
    {
        return string_0;
    }
}

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

public class LogEntry
{
    public DateTime dateTime_0;
    public string string_0;

    public LogEntry(string string_1)
    {
        dateTime_0 = DateTime.Now;
        string_0 = string_1;
    }
}

public class EmptyPlaceholder
{
}

public enum TriStateOption
{
    const_0,
    const_1,
    const_2
}

public enum SixStateOption
{
    const_0,
    const_1,
    const_2,
    const_3,
    const_4,
    const_5
}

public class GliderException : Exception
{
    public GliderException(string string_0)
        : base(string_0)
    {
    }
}