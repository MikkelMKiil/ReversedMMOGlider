#nullable disable
using System;
using System.IO;

public sealed class UnifiedLogger : ILogger
{
    public static readonly UnifiedLogger Default = new UnifiedLogger("Glider.log");

    private readonly object syncRoot = new object();
    private readonly string logFilePath;
    private StreamWriter writer;

    public string LogFilePath
    {
        get { return logFilePath; }
    }

    public UnifiedLogger(string string_0)
    {
        logFilePath = string_0;
    }

    public void imethod_0()
    {
    }

    public void imethod_1()
    {
    }

    public void imethod_2(string string_0)
    {
        AppendLine(FormatCompactEntry(DateTime.Now, string_0));
    }

    public void imethod_3(string string_0)
    {
        imethod_2("[Debug] " + string_0);
    }

    public void imethod_4()
    {
        CloseWriter();
    }

    public void Reset()
    {
        lock (syncRoot)
        {
            CloseWriter();
            try
            {
                if (File.Exists(logFilePath))
                    File.Delete(logFilePath);
                using (File.Create(logFilePath))
                {
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Unable to reset log file: " + ex.Message);
            }
        }
    }

    public void AppendLine(string string_0)
    {
        lock (syncRoot)
        {
            try
            {
                EnsureWriter();
                writer.WriteLine(string_0);
                writer.Flush();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Unable to write to log: " + ex.Message);
            }
        }
    }

    public static string FormatCompactEntry(DateTime dateTime_0, string string_0)
    {
        return dateTime_0.ToString("HH:mm:ss.ffff ") + string_0;
    }

    public static string FormatApplicationEntry(DateTime dateTime_0, string string_0, bool bool_0)
    {
        if (bool_0)
            return dateTime_0.ToLongTimeString() + " (" + dateTime_0.Millisecond + ") " + string_0;
        return dateTime_0.ToLongTimeString() + " " + string_0;
    }

    private void EnsureWriter()
    {
        if (writer != null)
            return;
        var string_0 = Path.GetDirectoryName(logFilePath);
        if (!string.IsNullOrEmpty(string_0))
            Directory.CreateDirectory(string_0);
        writer = new StreamWriter(new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read));
        writer.AutoFlush = true;
    }

    private void CloseWriter()
    {
        if (writer == null)
            return;
        try
        {
            writer.Flush();
            writer.Close();
        }
        catch
        {
        }
        finally
        {
            writer = null;
        }
    }
}