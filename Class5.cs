// Decompiled with JetBrains decompiler
// Type: Class5
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Class5
{
    private static ResourceManager resourceManager_0;
    private static CultureInfo cultureInfo_0;

    [SpecialName]
    internal static ResourceManager smethod_0()
    {
        // ISSUE: reference to a compiler-generated field
        if (ReferenceEquals(resourceManager_0, null))
            // ISSUE: reference to a compiler-generated field
            resourceManager_0 = new ResourceManager("Glider.Properties.Resources", typeof(Class5).Assembly);
        // ISSUE: reference to a compiler-generated field
        return resourceManager_0;
    }

    [SpecialName]
    internal static CultureInfo smethod_1()
    {
        return cultureInfo_0;
    }

    [SpecialName]
    internal static void smethod_2(CultureInfo cultureInfo_1)
    {
        cultureInfo_0 = cultureInfo_1;
    }
}