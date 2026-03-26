// Decompiled with JetBrains decompiler
// Type: GClass37
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
public class Logger
{
    private static ILogger GetLogger()
    {
        if (StartupClass.ginterface0_0 == null)
            StartupClass.ginterface0_0 = UnifiedLogger.Default;
        return StartupClass.ginterface0_0;
    }

    public static void LogMessage(string string_0)
    {
        GetLogger().imethod_2(string_0);
    }

    public static void LogDebug(string string_0)
    {
        GetLogger().imethod_3(string_0);
    }

    public static void smethod_1(string string_0)
    {
        LogDebug(string_0);
    }
}
