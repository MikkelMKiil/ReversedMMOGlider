// Type: ScriptExecutor
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using Glider.Common;
using Glider.Common.Objects;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

public class ScriptExecutor
{
    private const int ScriptThreadJoinTimeoutMs = 5000;
    private readonly SortedList<string, GScript> Offsets;
    private Thread thread_0;

    public ScriptExecutor()
    {
        if (!Directory.Exists("DefaultScripts"))
            Directory.CreateDirectory("DefaultScripts");
        if (!Directory.Exists("Scripts"))
            Directory.CreateDirectory("Scripts");
        Offsets = new SortedList<string, GScript>();
    }

    public void method_0()
    {
        lock (this)
        {
            if (thread_0 == null)
                return;
            thread_0.Interrupt();
            if (!thread_0.Join(ScriptThreadJoinTimeoutMs))
            {
                Logger.LogMessage("ScriptHelper shutdown timed out waiting for running script thread");
                return;
            }

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
        Logger.smethod_1("ScriptHelper.Execute: " + string_0);
        if (StartupClass.CurrentGlideMode == GlideMode.None)
        {
            if (thread_0 != null && thread_0 == Thread.CurrentThread)
                method_4(string_0, bool_0);
            else
                lock (this)
                {
                    if (thread_0 != null)
                    {
                        Logger.LogMessage("Waiting for prior script to finish");
                        if (!thread_0.Join(ScriptThreadJoinTimeoutMs))
                        {
                            Logger.LogMessage("Prior script did not finish before timeout; skipping script launch");
                            return;
                        }
                    }

                    if (!StartupClass.IsGliderInitialized && ConfigManager.gclass61_0.method_5("BackgroundEnable") &&
                        StartupClass.AdditionalApplicationHandle != IntPtr.Zero && !StartupClass.IsAttached)
                    {
                        StartupClass.InitializeBackgroundModeIfNeeded();
                        if (StartupClass.IsGliderInitialized)
                            Logger.LogMessage("Setting up for background mode!");
                    }
                    else
                    {
                        StartupClass.BringGameToForeground();
                        Thread.Sleep(2000);
                    }

                    Logger.smethod_1("Firing up script on new thread");
                    thread_0 = new Thread(method_3);
                    thread_0.Start(new BoolStringOption
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
            var class1 = (BoolStringOption)object_0;
            method_4(class1.string_0, class1.bool_0);
        }
        catch (ThreadInterruptedException ex)
        {
            flag = false;
        }

        if (flag)
            thread_0 = null;
        // StartupClass.CameraController.ConsiderReleaseButton();
        // InputController.smethod_21(false);
        // InputController.smethod_21(false);
    }

    protected void method_4(string string_0, bool bool_0)
    {
        if (!Offsets.ContainsKey(string_0))
            method_5(string_0);
        try
        {
            if (Offsets[string_0] != null && !bool_0 && StartupClass.IsSomeConditionMet)
            {
                Offsets[string_0].Setup();
                Offsets[string_0].Execute();
            }
            else
            {
                Offsets[string_0 + "_Base"].Setup();
                Offsets[string_0 + "_Base"].Execute();
            }
        }
        catch (ThreadInterruptedException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            Logger.LogMessage("!! Unhandled exception in script for \"" + string_0 + "\": " + ex.Message + "\r\n" +
                               ex.StackTrace);
            Offsets.Remove(string_0);
            throw ex;
        }
    }

    private void method_5(string string_0)
    {
        if (StartupClass.IsSomeConditionMet)
        {
            var str1 = ConfigManager.gclass61_0.method_2("ScriptsFolder");
            if (GContext.Main.Profile != null && GContext.Main.Profile.ScriptOverride != null)
            {
                Logger.smethod_1("Using profile's script override folder");
                str1 = GContext.Main.Profile.ScriptOverride;
            }

            if (!str1.EndsWith("\\"))
                str1 += "\\";
            var str2 = str1 + string_0 + ".cs";
            Logger.smethod_1("Considering: \"" + str2 + "\"");
            if (File.Exists(str2))
            {
                Offsets[string_0] = method_11(str2);
            }
            else
            {
                Logger.smethod_1("No such file, skipping");
                Offsets[string_0] = null;
            }
        }
        else
        {
            Offsets[string_0] = null;
        }

        Offsets[string_0 + "_Base"] = method_10(string_0);
    }

    private void method_6(string string_0)
    {
        var path = "DefaultScripts\\" + string_0 + ".cs";
        var scriptText = method_9(string_0);
        if (scriptText == null || scriptText.Trim().Length == 0)
        {
            Logger.LogMessage("! Skipping default script export for \"" + string_0 + "\" (no source found).");
            return;
        }

        if (File.Exists(path))
            File.Delete(path);
        var streamWriter = new StreamWriter(path, false);
        streamWriter.Write(scriptText);
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
        Offsets.Clear();
    }

    private string method_9(string string_0)
    {
        var assembly = typeof(ScriptExecutor).Assembly;
        var exactNames = new[]
        {
            "GliderCommon.DefaultScripts." + string_0 + ".cs",
            "Glider.DefaultScripts." + string_0 + ".cs",
            "DefaultScripts." + string_0 + ".cs"
        };

        Stream manifestResourceStream = null;
        foreach (var name in exactNames)
        {
            manifestResourceStream = assembly.GetManifestResourceStream(name);
            if (manifestResourceStream != null)
                break;
        }

        if (manifestResourceStream == null)
        {
            var names = assembly.GetManifestResourceNames();
            for (var i = 0; i < names.Length; i++)
                if (names[i].EndsWith("DefaultScripts." + string_0 + ".cs", StringComparison.OrdinalIgnoreCase))
                {
                    manifestResourceStream = assembly.GetManifestResourceStream(names[i]);
                    if (manifestResourceStream != null)
                        break;
                }
        }

        if (manifestResourceStream != null)
        {
            var streamReader = new StreamReader(manifestResourceStream);
            var end = streamReader.ReadToEnd();
            streamReader.Close();
            manifestResourceStream.Close();
            return end;
        }

        var filePath = "DefaultScripts\\" + string_0 + ".cs";
        if (File.Exists(filePath))
            return File.ReadAllText(filePath);

        Logger.LogMessage("! Couldn't get default script: \"" + string_0 + "\" (resource + file lookup failed)");
        return null;
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
        if (string_0 == null || string_0.Trim().Length == 0)
        {
            Logger.LogMessage("Unable to compile script: script source is null or empty.");
            return null;
        }

        Logger.smethod_1("Compiling script, length = " + string_0.Length + " bytes");
        string string_1;
        var assembly_0 = method_13(string_0, out string_1);
        if (assembly_0 == null)
        {
            Logger.LogMessage("Unable to compile script:\r\n" + string_1);
            return null;
        }

        try
        {
            return method_14(assembly_0);
        }
        catch (Exception ex)
        {
            Logger.LogMessage("Unable to get script instance: " + ex.Message + "\r\n" + ex.StackTrace);
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
        compilerParameters.ReferencedAssemblies.Add(CodeCompiler.GetCompilerReferenceAssembly());
        CodeCompiler.smethod_1(string_0, compilerParameters);
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
        Logger.LogMessage("No GScripts in assembly (?!)");
        return null;
    }
}

