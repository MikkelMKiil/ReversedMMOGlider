using System.Runtime.CompilerServices;

#nullable disable

public class SolitaireMemoryOffsets
{
    public static int int_0 = 534600;
    public static int int_1 = 44;
    public static int int_2 = 68;
    public static int int_3 = 36;
    public static int int_4 = 192;
    public static int int_5 = 76;
    public static int int_6 = 92;
    public static int int_7 = 108;
    public static int int_8 = 72;
    public static int int_9 = 184;
    public static int int_10 = 196;
    public static int int_11 = 4;
    public static int int_12 = 13;
    public static int int_13 = 92;
    public static int int_14 = 36;
    public static int int_15 = 4;
    public static int int_16 = 8;
}

public class SolitaireGameState
{
    private bool bool_0;
    public CardStack gclass80_0;
    public CardStack gclass80_1;
    public CardStack[] gclass80_2;
    public CardStack[] gclass80_3;
    private readonly int int_0;

    public SolitaireGameState(int int_1)
    {
        int_0 = int_1;
        method_2();
    }

    [SpecialName]
    public bool method_0()
    {
        return bool_0;
    }

    [SpecialName]
    public void method_1(bool bool_1)
    {
        bool_0 = bool_1;
    }

    public void method_2()
    {
        gclass80_0 = new CardStack(GameMemoryAccess.ReadInt32(int_0 + SolitaireMemoryOffsets.int_8, "md30"), this);
        gclass80_1 = new CardStack(GameMemoryAccess.ReadInt32(int_0 + SolitaireMemoryOffsets.int_5, "md31"), this);
        gclass80_2 = new CardStack[4];
        var num1 = GameMemoryAccess.ReadInt32(int_0 + SolitaireMemoryOffsets.int_7, "md20");
        for (var index = 0; index < 4; ++index)
        {
            var int_3 = GameMemoryAccess.ReadInt32(num1 + index * 4, "md21");
            gclass80_2[index] = new CardStack(int_3, this);
        }

        gclass80_3 = new CardStack[7];
        var num2 = GameMemoryAccess.ReadInt32(int_0 + SolitaireMemoryOffsets.int_6, "md22");
        for (var index = 0; index < 7; ++index)
        {
            var int_3 = GameMemoryAccess.ReadInt32(num2 + index * 4, "md23");
            gclass80_3[index] = new CardStack(int_3, this);
        }
    }

    public void method_3()
    {
        Logger.LogMessage("SolitaireGame @ 0x" + int_0.ToString("x"));
        gclass80_0.method_1("Deck");
        gclass80_1.method_1("Draw");
        for (var index = 0; index < 4; ++index)
            gclass80_2[index].method_1("Suit" + index);
        for (var index = 0; index < 7; ++index)
            gclass80_3[index].method_1("InPlay" + index);
    }

    [SpecialName]
    public bool method_4()
    {
        foreach (var gclass80 in gclass80_2)
            if (gclass80.gclass7_0.Length != 13)
                return false;
        return true;
    }
}