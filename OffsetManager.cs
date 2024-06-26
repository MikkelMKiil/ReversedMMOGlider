﻿// Decompiled with JetBrains decompiler
// Type: GClass43
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Collections;

public class OffsetManager
{
    private readonly int initialOffset;
    private readonly SortedList OffsetList;
    private string descriptorName;

    public OffsetManager(string descriptorName, int initialOffset)
    {
        this.descriptorName = descriptorName;
        this.initialOffset = initialOffset;
        OffsetList = new SortedList();
        PopulateOffsetList();
    }

    public void PopulateOffsetList()
    {
        var currentOffset = initialOffset;
        var descriptorCount = 0;
        while (true)
        {
            var descriptorStringPtr = GProcessMemoryManipulator.smethod_11(currentOffset, "DescriptorStringPtr");
            if ((descriptorStringPtr & 1073741824) != 1073741824)
            {
                var descriptorKey = GProcessMemoryManipulator.smethod_9(descriptorStringPtr, 64, "DescriptorString");
                switch (descriptorKey)
                {
                    case null:
                        goto NullDescriptorKey;
                    case "OBJECT_FIELD_GUID":
                        if (descriptorCount > 2)
                            goto ExceededDescriptorCount;
                        break;
                }

                if (!OffsetList.ContainsKey(descriptorKey))
                    OffsetList.Add(descriptorKey, descriptorCount * 4);
                currentOffset += 20;
                ++descriptorCount;
            }
            else
            {
                goto ExitLoop;
            }
        }

        NullDescriptorKey:
        return;
        ExceededDescriptorCount:
        return;
        ExitLoop: ;
    }

    public int GetOffsetValue(string descriptorKey)
    {
        return OffsetList.ContainsKey(descriptorKey) ? (int)OffsetList[descriptorKey] : 0;
    }
}