// Decompiled with JetBrains decompiler
// Type: GClass43
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections;

public class OffsetManager
{
    private readonly string descriptorName;
    private readonly SortedList OffsetList;

    public OffsetManager(string descriptorName, int initialOffset)
    {
        this.descriptorName = descriptorName;
        OffsetList = new SortedList();
    }

    /// <summary>
    /// Populates the offset list using static WoW 3.3.5a descriptor data.
    /// Replaces the old dynamic descriptor scanning that is no longer functional.
    /// </summary>
    public void PopulateOffsetList()
    {
        WoW335aDescriptors.PopulateOffsetManager(this, descriptorName);
        Logger.smethod_1("OffsetManager: Loaded " + OffsetList.Count + " static descriptors for '" + descriptorName + "'");
    }

    /// <summary>
    /// Adds an offset entry to the descriptor table.
    /// </summary>
    public void AddOffset(string key, int byteOffset)
    {
        if (!OffsetList.ContainsKey(key))
            OffsetList.Add(key, byteOffset);
    }

    public int GetOffsetValue(string descriptorKey)
    {
        return OffsetList.ContainsKey(descriptorKey) ? (int)OffsetList[descriptorKey] : 0;
    }
}
