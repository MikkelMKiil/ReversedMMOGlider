// Decompiled with JetBrains decompiler
// Type: MemoryOffsetTable
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections;

/// <summary>
/// Stores and manages memory offsets for WoW client interaction.
/// Offsets can be stored as either integer addresses or string identifiers.
/// </summary>
public class MemoryOffsetTable
{
    /// <summary>
    /// Singleton instance of the MemoryOffsetTable.
    /// </summary>
    public static MemoryOffsetTable Instance;

    /// <summary>
    /// Internal storage for offsets. Keys are offset names, values can be int or string.
    /// </summary>
    public SortedList Offsets;
    private readonly SortedList missingOffsetsLogged;

    public MemoryOffsetTable()
    {
        Instance = this;
        Offsets = new SortedList();
        missingOffsetsLogged = new SortedList();
    }

    /// <summary>
    /// Clears all stored offsets.
    /// </summary>
    public void Clear()
    {
        Offsets.Clear();
        missingOffsetsLogged.Clear();
    }

    /// <summary>
    /// Adds a string-based offset to the table.
    /// </summary>
    /// <param name="offsetName">The name/key of the offset</param>
    /// <param name="offsetValue">The string value (e.g., buff ID)</param>
    public void AddStringOffset(string offsetName, string offsetValue)
    {
        Offsets.Add(offsetName, offsetValue);
    }

    /// <summary>
    /// Adds an integer-based offset to the table.
    /// </summary>
    /// <param name="offsetName">The name/key of the offset</param>
    /// <param name="offsetAddress">The memory address as integer</param>
    public void AddIntOffset(string offsetName, int offsetAddress)
    {
        Offsets.Add(offsetName, offsetAddress);
    }

    /// <summary>
    /// Retrieves a string offset by name.
    /// </summary>
    /// <param name="offsetName">The name of the offset to retrieve</param>
    /// <returns>The string value, or empty string if not found</returns>
    public string GetStringOffset(string offsetName)
    {
        if (Offsets.ContainsKey(offsetName))
            return (string)Offsets[offsetName];
        LogMissingOffsetOnce(offsetName);
        return "";
    }

    /// <summary>
    /// Retrieves an integer offset by name.
    /// </summary>
    /// <param name="offsetName">The name of the offset to retrieve</param>
    /// <returns>The integer address, or 0 if not found</returns>
    public int GetIntOffset(string offsetName)
    {
        if (Offsets.ContainsKey(offsetName))
            return (int)Offsets[offsetName];
        LogMissingOffsetOnce(offsetName);
        return 0;
    }

    /// <summary>
    /// Checks if an offset exists in the table.
    /// </summary>
    /// <param name="offsetName">The name of the offset to check</param>
    /// <returns>True if the offset exists, false otherwise</returns>
    public bool HasOffset(string offsetName)
    {
        return Offsets.ContainsKey(offsetName);
    }

    private void LogMissingOffsetOnce(string offsetName)
    {
        if (missingOffsetsLogged.ContainsKey(offsetName))
            return;
        missingOffsetsLogged.Add(offsetName, true);
        Logger.LogMessage(MessageProvider.smethod_2(314, offsetName));
    }
}