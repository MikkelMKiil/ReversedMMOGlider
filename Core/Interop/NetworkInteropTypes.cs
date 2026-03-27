using System;
using System.Runtime.InteropServices;

#nullable disable

public struct FixedString16
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
    public string string_0;
}

public struct NetworkAdapterEntry
{
    public IntPtr intptr_0;
    public FixedString16 gstruct9_0;
    public FixedString16 gstruct9_1;
    public int int_0;
}