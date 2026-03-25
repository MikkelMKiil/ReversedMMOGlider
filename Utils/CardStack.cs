// Decompiled with JetBrains decompiler
// Type: CardStack
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System.Runtime.CompilerServices;

public class CardStack
{
    public SolitaireGameState gclass34_0;
    public PlayingCard[] gclass7_0;
    private readonly int int_0;
    private int int_1;
    private int int_2;

    public CardStack(int int_3, SolitaireGameState gclass34_1)
    {
        int_0 = int_3;
        gclass34_0 = gclass34_1;
        method_0();
    }

    public void method_0()
    {
        var length = GameMemoryAccess.ReadInt32(int_0 + SolitaireMemoryOffsets.int_9, "md10");
        gclass7_0 = new PlayingCard[length];
        for (var index = 0; index < length; ++index)
        {
            var int_5 = GameMemoryAccess.ReadInt32(GameMemoryAccess.ReadInt32(int_0 + SolitaireMemoryOffsets.int_10, "md11") + 4 * index, "md12");
            gclass7_0[index] = new PlayingCard(int_5, this);
        }

        int_1 = 0;
        int_2 = 0;
        var num = GameMemoryAccess.ReadInt32(int_0 + SolitaireMemoryOffsets.int_13, "md13");
        int_1 = GameMemoryAccess.ReadInt32(num + SolitaireMemoryOffsets.int_15, "md14");
        int_2 = GameMemoryAccess.ReadInt32(num + SolitaireMemoryOffsets.int_16, "md15");
    }

    public void method_1(string string_0)
    {
        Logger.LogMessage("CardStack \"" + string_0 + "\" @ 0x" + int_0.ToString("x") + ": depth=" + gclass7_0.Length +
                           " cards");
        for (var index = 0; index < gclass7_0.Length; ++index)
            Logger.LogMessage(index + ": " + gclass7_0[index]);
    }

    public void method_2()
    {
        InputController.smethod_18(int_1 / 800.0 + 0.045, int_2 / 500.0 + 0.025);
    }

    public void method_3()
    {
        method_2();
        InputController.smethod_23(false);
    }

    public PlayingCard method_4()
    {
        return gclass7_0.Length == 0 ? null : gclass7_0[gclass7_0.Length - 1];
    }

    public void method_5()
    {
        gclass34_0.method_1(true);
    }

    [SpecialName]
    public int method_6()
    {
        var num = 0;
        foreach (var gclass7 in gclass7_0)
            if (!gclass7.bool_0)
                ++num;
        return num;
    }

    [SpecialName]
    public int method_7()
    {
        var num = 0;
        foreach (var gclass7 in gclass7_0)
            if (gclass7.bool_0)
                ++num;
        return num;
    }

    [SpecialName]
    public PlayingCard method_8()
    {
        if (gclass7_0.Length == 0)
            return null;
        PlayingCard gclass7 = null;
        for (var index = gclass7_0.Length - 1; index >= 0 && gclass7_0[index].bool_0; --index)
            gclass7 = gclass7_0[index];
        return gclass7;
    }
}
