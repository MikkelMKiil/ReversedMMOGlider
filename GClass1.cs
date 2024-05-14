// Decompiled with JetBrains decompiler
// Type: GClass1
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
public class GClass1
{
    public static byte[] smethod_0()
    {
        var gclass56 = new GDataEncryptionManager(5);
        gclass56.PrepareDataStream();
        gclass56.SendAndReceiveData();
        return gclass56.ReadBytesFromDecryptedStream();
    }

    public static byte[] smethod_1(int int_0)
    {
        var gclass56 = new GDataEncryptionManager(9);
        gclass56.PrepareDataStream();
        gclass56.WriteIntToStream(int_0);
        gclass56.SendAndReceiveData();
        return gclass56.ReadBytesFromDecryptedStream();
    }
}