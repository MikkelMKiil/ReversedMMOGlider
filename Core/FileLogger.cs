// Decompiled with JetBrains decompiler
// Type: FileLogger
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
public class FileLogger : ILogger
{
    private readonly UnifiedLogger unifiedLogger_0;

    public FileLogger(string string_1)
    {
        unifiedLogger_0 = string_1 == UnifiedLogger.Default.LogFilePath ? UnifiedLogger.Default : new UnifiedLogger(string_1);
    }

    // Changed from explicit to implicit implementation
    public void imethod_2(string string_1)
    {
        unifiedLogger_0.AppendLine(UnifiedLogger.FormatCompactEntry(DateTime.Now, string_1));
    }

    public void imethod_3(string string_1)
    {
        unifiedLogger_0.AppendLine(UnifiedLogger.FormatCompactEntry(DateTime.Now, "[Debug] " + string_1));
    }

    public void imethod_4()
    {
        unifiedLogger_0.imethod_4();
    }

    public void imethod_1()
    {
    }

    public void imethod_0()
    {
    }
}
