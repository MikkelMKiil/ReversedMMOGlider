// Decompiled with JetBrains decompiler
// Type: ScreenCapture
// Assembly: Glider, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: BE61069A-03D7-40D0-A422-37FF26A0373E
// Assembly location: C:\Users\kiilo\Desktop\WORK ON THSI\Glider_fix-cleaned.exe

#nullable disable
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public class ScreenCapture
{
    public static Bitmap smethod_0(IntPtr intptr_0)
    {
        var dc = WindowContextHelper.GetDC(intptr_0);
        var gstruct16_0 = new WindowContextHelper.GStruct16();
        WindowContextHelper.GetClientRect(intptr_0, ref gstruct16_0);
        var num1 = gstruct16_0.int_2 - gstruct16_0.int_0;
        var num2 = gstruct16_0.int_3 - gstruct16_0.int_1;
        var compatibleDc = GDIHelper.CreateCompatibleDC(dc);
        var compatibleBitmap = GDIHelper.CreateCompatibleBitmap(dc, num1, num2);
        var intptr_1 = GDIHelper.SelectObject(compatibleDc, compatibleBitmap);
        GDIHelper.BitBlt(compatibleDc, 0, 0, num1, num2, dc, 0, 0, 13369376);
        GDIHelper.SelectObject(compatibleDc, intptr_1);
        GDIHelper.DeleteDC(compatibleDc);
        WindowContextHelper.ReleaseDC(intptr_0, dc);
        var bitmap = Image.FromHbitmap(compatibleBitmap);
        GDIHelper.DeleteObject(compatibleBitmap);
        return bitmap;
    }

    public static ImageCodecInfo smethod_1(string string_0)
    {
        foreach (var imageEncoder in ImageCodecInfo.GetImageEncoders())
            if (imageEncoder.MimeType == string_0)
                return imageEncoder;
        return null;
    }

    public static Bitmap smethod_2(Image image_0, int int_0)
    {
        var num = int_0 / 100f;
        var width1 = image_0.Width;
        var height1 = image_0.Height;
        var width2 = (int)(width1 * (double)num);
        var height2 = (int)(height1 * (double)num);
        var bitmap = new Bitmap(width2, height2, PixelFormat.Format24bppRgb);
        bitmap.SetResolution(image_0.HorizontalResolution, image_0.VerticalResolution);
        var graphics = Graphics.FromImage(bitmap);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage(image_0, new Rectangle(0, 0, width2, height2), new Rectangle(0, 0, width1, height1),
            GraphicsUnit.Pixel);
        graphics.Dispose();
        return bitmap;
    }
}
