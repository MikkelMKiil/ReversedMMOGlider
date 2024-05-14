// Decompiled with JetBrains decompiler
// Type: GClass47
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
using System.Threading;
using Glider.Common;
using Glider.Common.Objects;
using Microsoft.CSharp;

public class GClass47
{
    private readonly SortedList<string, GScript> sortedList_0;
    private Thread thread_0;

    public GClass47()
    {
        if (!Directory.Exists("DefaultScripts"))
            Directory.CreateDirectory("DefaultScripts");
        if (!Directory.Exists("Scripts"))
            Directory.CreateDirectory("Scripts");
        sortedList_0 = new SortedList<string, GScript>();
    }

    public void method_0()
    {
        lock (this)
        {
            if (thread_0 == null)
                return;
            thread_0.Interrupt();
            thread_0.Join();
            thread_0 = null;
        }
    }

    [SpecialName]
    public bool method_1()
    {
        return thread_0 != null;
    }

    public void method_2(string string_0, bool bool_0)
    {
        GClass37.smethod_1("ScriptHelper.Execute: " + string_0);
        if (StartupClass.glideMode_0 == GlideMode.None)
        {
            if (thread_0 != null && thread_0 == Thread.CurrentThread)
                method_4(string_0, bool_0);
            else
                lock (this)
                {
                    if (thread_0 != null)
                    {
                        GClass37.smethod_0("Waiting for prior script to finish");
                        thread_0.Join();
                    }

                    if (!StartupClass.bool_11 && GClass61.gclass61_0.method_5("BackgroundEnable") &&
                        StartupClass.gclass71_0 != null && StartupClass.intptr_1 != IntPtr.Zero && !StartupClass.bool_2)
                    {
                        StartupClass.intptr_0 = GProcessMemoryManipulator.smethod_27(StartupClass.int_3);
                        if (StartupClass.intptr_0 == IntPtr.Zero)
                        {
                            GClass37.smethod_0("No game window, no background mode!");
                        }
                        else
                        {
                            StartupClass.gclass71_0.method_34(StartupClass.int_3, StartupClass.intptr_0);
                            StartupClass.bool_11 = true;
                            GClass37.smethod_0("Setting up for background mode!");
                        }
                    }
                    else
                    {
                        StartupClass.smethod_22();
                        Thread.Sleep(2000);
                    }

                    GClass37.smethod_1("Firing up script on new thread");
                    if (StartupClass.gclass71_0 != null)
                        StartupClass.gclass71_0.method_33(true);
                    thread_0 = new Thread(method_3);
                    thread_0.Start(new Class1
                    {
                        bool_0 = bool_0,
                        string_0 = string_0
                    });
                }
        }
        else
        {
            method_4(string_0, bool_0);
        }
    }

    protected void method_3(object object_0)
    {
        var flag = true;
        try
        {
            var class1 = (Class1)object_0;
            method_4(class1.string_0, class1.bool_0);
        }
        catch (ThreadInterruptedException ex)
        {
            flag = false;
        }

        if (flag)
            thread_0 = null;
        StartupClass.gclass68_0.method_7();
        GClass55.smethod_21(false);
        if (StartupClass.gclass71_0 == null)
            return;
        StartupClass.gclass71_0.method_33(false);
    }

    protected void method_4(string string_0, bool bool_0)
    {
        if (!sortedList_0.ContainsKey(string_0))
            method_5(string_0);
        try
        {
            if (sortedList_0[string_0] != null && !bool_0 && StartupClass.bool_12)
            {
                sortedList_0[string_0].Setup();
                sortedList_0[string_0].Execute();
            }
            else
            {
                sortedList_0[string_0 + "_Base"].Setup();
                sortedList_0[string_0 + "_Base"].Execute();
            }
        }
        catch (ThreadInterruptedException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("!! Unhandled exception in script for \"" + string_0 + "\": " + ex.Message + "\r\n" +
                               ex.StackTrace);
            sortedList_0.Remove(string_0);
            throw ex;
        }
    }

    private void method_5(string string_0)
    {
        if (StartupClass.bool_12)
        {
            var str1 = GClass61.gclass61_0.method_2("ScriptsFolder");
            if (GContext.Main.Profile != null && GContext.Main.Profile.ScriptOverride != null)
            {
                GClass37.smethod_1("Using profile's script override folder");
                str1 = GContext.Main.Profile.ScriptOverride;
            }

            if (!str1.EndsWith("\\"))
                str1 += "\\";
            var str2 = str1 + string_0 + ".cs";
            GClass37.smethod_1("Considering: \"" + str2 + "\"");
            if (File.Exists(str2))
            {
                sortedList_0[string_0] = method_11(str2);
            }
            else
            {
                GClass37.smethod_1("No such file, skipping");
                sortedList_0[string_0] = null;
            }
        }
        else
        {
            sortedList_0[string_0] = null;
        }

        sortedList_0[string_0 + "_Base"] = method_10(string_0);
    }

    private void method_6(string string_0)
    {
        var path = "DefaultScripts\\" + string_0 + ".cs";
        if (File.Exists(path))
            File.Delete(path);
        var streamWriter = new StreamWriter(path, false);
        streamWriter.Write(method_9(string_0));
        streamWriter.Flush();
        streamWriter.Close();
    }

    public void method_7()
    {
        method_6("OnGliderStart");
        method_6("OnHearth");
        method_6("OnGameFirstSeen");
        method_6("DoAutoLog");
    }

    public void method_8()
    {
        sortedList_0.Clear();
    }

    private string method_9(string string_0)
    {
        var name = "GliderCommon.DefaultScripts." + string_0 + ".cs";
        var manifestResourceStream = Assembly.GetCallingAssembly().GetManifestResourceStream(name);
        if (manifestResourceStream == null)
        {
            GClass37.smethod_0("! Couldn't get default script: \"" + name + "\"");
            return null;
        }

        var streamReader = new StreamReader(manifestResourceStream);
        var end = streamReader.ReadToEnd();
        streamReader.Close();
        manifestResourceStream.Close();
        return end;
    }

    private GScript method_10(string string_0)
    {
        return method_12(method_9(string_0));
    }

    private GScript method_11(string string_0)
    {
        return method_12(File.ReadAllText(string_0));
    }

    private GScript method_12(string string_0)
    {
        GClass37.smethod_1("Compiling script, length = " + string_0.Length + " bytes");
        string string_1;
        var assembly_0 = method_13(string_0, out string_1);
        if (assembly_0 == null)
        {
            GClass37.smethod_0("Unable to compile script:\r\n" + string_1);
            return null;
        }

        try
        {
            return method_14(assembly_0);
        }
        catch (Exception ex)
        {
            GClass37.smethod_0("Unable to get script instance: " + ex.Message + "\r\n" + ex.StackTrace);
        }

        return null;
    }

    private Assembly method_13(string string_0, out string string_1)
    {
        var flag = false;
        var stringBuilder = new StringBuilder();
        CodeDomProvider codeDomProvider = new CSharpCodeProvider();
        var compilerParameters = new CompilerParameters();
        compilerParameters.GenerateExecutable = false;
        compilerParameters.GenerateInMemory = true;
        compilerParameters.WarningLevel = 4;
        compilerParameters.IncludeDebugInformation = true;
        compilerParameters.ReferencedAssemblies.Add("Grefs.dat");
        GClass74.smethod_1(string_0, compilerParameters);
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

    private GScript method_14(Assembly assembly_0)
    {
        foreach (var exportedType in assembly_0.GetExportedTypes())
            if (exportedType.IsSubclassOf(typeof(GScript)))
                return (GScript)assembly_0.CreateInstance(exportedType.FullName);
        GClass37.smethod_0("No GScripts in assembly (?!)");
        return null;
    }
}