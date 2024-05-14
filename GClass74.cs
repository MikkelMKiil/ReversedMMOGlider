// Decompiled with JetBrains decompiler
// Type: GClass74
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Glider.Common;
using Glider.Common.Objects;
using Microsoft.CSharp;

public class GClass74
{
    public static Assembly smethod_0(string string_0, out string string_1)
    {
        try
        {
            var path = "Classes\\" + string_0;
            if (File.Exists(path))
                return smethod_2(File.ReadAllText(path), out string_1);
            string_1 = "Source file does not exist";
            return null;
        }
        catch (Exception ex)
        {
            string_1 = "Exception from CompileHelper: " + ex.Message + ex.StackTrace;
            return null;
        }
    }

    public static void smethod_1(string string_0, CompilerParameters compilerParameters_0)
    {
        var str1 = string_0;
        var chArray = new char[1] { '\r' };
        foreach (var str2 in str1.Split(chArray))
            if (str2.Trim().StartsWith("//!Reference: "))
            {
                var num = str2.IndexOf(' ');
                var str3 = str2.Substring(num + 1).Trim();
                compilerParameters_0.ReferencedAssemblies.Add(str3);
            }
    }

    private static Assembly smethod_2(string string_0, out string string_1)
    {
        var flag = false;
        var stringBuilder = new StringBuilder();
        CodeDomProvider codeDomProvider = new CSharpCodeProvider();
        var compilerParameters = new CompilerParameters();
        compilerParameters.CompilerOptions = "/define:GCONFIGWINDOW";
        compilerParameters.GenerateExecutable = false;
        compilerParameters.GenerateInMemory = true;
        compilerParameters.WarningLevel = 4;
        compilerParameters.IncludeDebugInformation = true;
        compilerParameters.ReferencedAssemblies.Add("Grefs.dat");
        compilerParameters.ReferencedAssemblies.Add("System.dll");
        compilerParameters.ReferencedAssemblies.Add("System.Xml.dll");
        compilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
        smethod_1(string_0, compilerParameters);
        var compilerResults = codeDomProvider.CompileAssemblyFromSource(compilerParameters, string_0);
        var errors = compilerResults.Errors;
        if (errors.Count > 0)
            foreach (CompilerError compilerError in errors)
            {
                if (!compilerError.IsWarning)
                    flag = true;
                stringBuilder.Append("Line " + compilerError.Line + ", column " + compilerError.Column + ": " +
                                     compilerError.ErrorText + "\r\n");
            }

        if (stringBuilder.Length > 0)
            stringBuilder.Remove(stringBuilder.Length - 2, 2);
        string_1 = stringBuilder.ToString();
        return flag ? null : compilerResults.CompiledAssembly;
    }

    private static string smethod_3(string string_0)
    {
        var name = "GliderCommon.DefaultClasses." + string_0;
        var manifestResourceStream = Assembly.GetCallingAssembly().GetManifestResourceStream(name);
        if (manifestResourceStream == null)
        {
            Logger.LogMessage("! Couldn't get default class: \"" + name + "\"");
            return null;
        }

        var streamReader = new StreamReader(manifestResourceStream);
        var end = streamReader.ReadToEnd();
        streamReader.Close();
        manifestResourceStream.Close();
        return end;
    }

    public static void smethod_4()
    {
        var array = new string[StartupClass.ProfileMapping.Keys.Count];
        StartupClass.ProfileMapping.Keys.CopyTo(array, 0);
        foreach (var string_0 in array)
            smethod_5(string_0);
    }

    public static void smethod_5(string string_0)
    {
        Logger.smethod_1("Unloading custom class: \"" + string_0 + "\"");
        if (!StartupClass.ProfileMapping.ContainsKey(string_0))
        {
            Logger.LogMessage("Can't unload \"" + string_0 + "\", it isn't loaded!");
        }
        else
        {
            try
            {
                if (smethod_15())
                    if (StartupClass.ProfileMapping[string_0].object_0 != null)
                        if (!StartupClass.ProfileMapping[string_0].bool_0)
                        {
                            StartupClass.ProfileMapping[string_0].method_0();
                            if (StartupClass.ProfileMapping[string_0].genum1_0 == GEnum1.const_1)
                                ((GGameClass)StartupClass.ProfileMapping[string_0].object_0).Shutdown();
                        }
            }
            catch (Exception ex)
            {
                Logger.LogMessage("** Exception shutting down \"" + string_0 + "\": " + ex.Message + ex.StackTrace);
            }

            StartupClass.ProfileMapping.Remove(string_0);
        }
    }

    private static string smethod_6(string string_0)
    {
        var strArray = File.ReadAllLines("Classes\\" + string_0);
        string str1 = null;
        foreach (var str2 in strArray)
            if (str2.Trim().StartsWith("//!Class: "))
            {
                var num = str2.IndexOf(' ');
                str1 = str2.Substring(num + 1).Trim();
                Logger.smethod_1("Override class to instantiate: \"" + str1 + "\"");
            }

        return str1;
    }

    public static GGameClass smethod_7(
        string string_0,
        Assembly assembly_0,
        bool bool_0,
        bool bool_1)
    {
        var exportedTypes = assembly_0.GetExportedTypes();
        Type type1 = null;
        string str = null;
        if (bool_1)
            str = smethod_6(string_0);
        if (str == null)
        {
            foreach (var type2 in exportedTypes)
                if (type2.IsSubclassOf(typeof(GGameClass)))
                {
                    type1 = type2;
                    str = type1.FullName;
                    Logger.smethod_1("Guessed class name: \"" + str + "\"");
                    break;
                }

            if (type1 == null)
            {
                Logger.LogMessage("Never found any good classes in: \"" + string_0 + "\"");
                return null;
            }
        }
        else
        {
            type1 = assembly_0.GetType(str);
        }

        try
        {
            var instance = (GGameClass)assembly_0.CreateInstance(str);
            var flag = false;
            instance.SourceFileName = string_0;
            instance.Startup();
            instance.CreateDefaultConfig();
            instance.LoadConfig();
            var method = type1.GetMethod("Patrol");
            if (method != null && method.DeclaringType != typeof(GGameClass))
            {
                Logger.LogMessage("This class contains an override for Patrol, will use it when gliding (oh no!)");
                flag = true;
            }

            if (bool_0)
                StartupClass.ProfileMapping.Add(string_0, new GClass22(instance.DisplayName)
                {
                    string_1 = string_0,
                    object_0 = instance,
                    genum1_0 = GEnum1.const_1,
                    bool_0 = false,
                    bool_1 = flag
                });
            return instance;
        }
        catch (Exception ex)
        {
            Logger.LogMessage("** Exception initializing: " + ex.Message + ex.StackTrace);
            return null;
        }
    }

    private static void smethod_8(string string_0)
    {
        var str = smethod_3(string_0);
        if (str == null)
            return;
        var path = "DefaultClasses\\" + string_0;
        if (File.Exists(path))
            File.Delete(path);
        var streamWriter = new StreamWriter(path, false);
        streamWriter.Write(str);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public static void smethod_9(GClass22 gclass22_0)
    {
        var string_0 = gclass22_0.string_1.Substring(0, gclass22_0.string_1.IndexOf(' '));
        Logger.LogMessage("Compiling internal: \"" + string_0 + "\"");
        string string_1;
        var assembly_0 =
            smethod_2(
                smethod_3(string_0) ?? throw new Exception("Can't get default class source: \"" + string_0 + "\""),
                out string_1);
        if (assembly_0 == null)
            Logger.LogMessage("!! Exception compiling internal class \"" + string_0 + "\": " + string_1);
        else
            try
            {
                var ggameClass = smethod_7("(Internal) " + string_0, assembly_0, false, false);
                if (ggameClass == null)
                    return;
                Logger.LogMessage("Compiled internal class: \"" + string_0 + "\", displayname = \"" +
                                   ggameClass.DisplayName + "\"");
                gclass22_0.object_0 = ggameClass;
            }
            catch (Exception ex)
            {
                Logger.LogMessage("!! Exception instantiating internal class \"" + string_0 + "\": " + ex.Message +
                                   ex.StackTrace);
            }
    }

    public static void smethod_10()
    {
        smethod_11("Paladin.cs (internal)", "Paladin");
        smethod_11("Mage.cs (internal)", "Mage");
        smethod_11("Priest.cs (internal)", "Priest");
        smethod_11("Druid.cs (internal)", "Druid");
        smethod_11("Shaman.cs (internal)", "Shaman");
        smethod_11("Rogue.cs (internal)", "Rogue");
        smethod_11("Warlock.cs (internal)", "Warlock");
        smethod_11("Warrior.cs (internal)", "Warrior");
        smethod_11("Hunter.cs (internal)", "Hunter");
        smethod_11("Deathknight.cs (internal)", "Deathknight");
    }

    private static void smethod_11(string string_0, string string_1)
    {
        var gclass22 = new GClass22(MessageProvider.smethod_4("Common.Class" + string_1));
        gclass22.bool_0 = true;
        gclass22.string_1 = string_0;
        gclass22.genum1_0 = GEnum1.const_1;
        Logger.LogMessage("Adding class stub: \"" + string_1 + "\" on source file \"" + string_0 + "\"");
        StartupClass.ProfileMapping.Add(string_0, gclass22);
    }

    public static void smethod_12()
    {
        if (!Directory.Exists("DefaultClasses"))
            Directory.CreateDirectory("DefaultClasses");
        smethod_8("Druid.cs");
        smethod_8("Mage.cs");
        smethod_8("Paladin.cs");
        smethod_8("Priest.cs");
        smethod_8("Rogue.cs");
        smethod_8("Shaman.cs");
        smethod_8("Warrior.cs");
        smethod_8("Hunter.cs");
        smethod_8("Warlock.cs");
        smethod_8("Deathknight.cs");
    }

    public static bool smethod_13(string string_0, out string string_1)
    {
        string_1 = "";
        if (StartupClass.ProfileMapping.ContainsKey(string_0))
            return true;
        var assembly_0 = smethod_0(string_0, out string_1);
        if (assembly_0 == null)
        {
            Logger.LogMessage("Compile failed on \"" + string_0 + "\": " + string_1);
            return false;
        }

        if (string_1.Length > 0)
            Logger.LogMessage("Compile successful with warnings on \"" + string_0 + "\": " + string_1);
        else
            Logger.LogMessage("Compile successful on \"" + string_0 + "\"");
        var ggameClass = smethod_7(string_0, assembly_0, true, true);
        if (ggameClass != null && StartupClass.bool_13)
            ggameClass.OnAttach();
        return ggameClass != null;
    }

    public static void smethod_14()
    {
        var strArray = GClass61.gclass61_0.method_10("CustomClasses");
        var stringList = new List<string>();
        if (strArray == null)
            return;
        foreach (var string_0 in strArray)
            if (!smethod_13(string_0, out var _))
                stringList.Add(string_0);
        foreach (var string_4 in stringList)
            GClass61.gclass61_0.method_13("CustomClasses", string_4);
    }

    [SpecialName]
    private static bool smethod_15()
    {
        return StartupClass.ApplicationStartupMode == AppMode.Invisible || StartupClass.ApplicationStartupMode == AppMode.Normal;
    }

    public static GClass22 smethod_16(GGameClass ggameClass_0)
    {
        foreach (var gclass22 in StartupClass.ProfileMapping.Values)
            if (gclass22.object_0 == ggameClass_0)
                return gclass22;
        return null;
    }
}