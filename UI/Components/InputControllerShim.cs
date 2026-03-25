using System;
using System.Threading;

// Shim to expose the small set of InputController APIs used across the codebase.
// Keeps existing InputController implementation untouched as requested.
public static class InputControllerShim
{
    // Move the cursor to normalized game coordinates (0..1)
    public static void MoveNormalized(double nx, double ny)
    {
        try
        {
            // Map normalized to screen using the game's WorldToScreen method if available
            // Fallback: use System.Windows.Forms.Cursor to set absolute position approximately
            System.Drawing.Point p = System.Windows.Forms.Cursor.Position;
            int x = (int)(nx * System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width);
            int y = (int)(ny * System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x, y);
        }
        catch (Exception ex)
        {
            Logger.LogMessage("InputControllerShim.MoveNormalized failed: " + ex.Message);
        }
    }

    // Click wrapper: true = left click? Keep signature compatible with callsites using boolean
    public static void Click(bool left)
    {
        try
        {
            var down = left ? 2 : 8; // left down/up mapping not important here, use SendMouseEvent via InputController if possible
            InputController.SendMouseEvent(left ? 2 : 8);
            Thread.Sleep(50);
            InputController.SendMouseEvent(left ? 4 : 16);
        }
        catch (Exception ex)
        {
            Logger.LogMessage("InputControllerShim.Click failed: " + ex.Message);
        }
    }

    // Expose cursor hook enable/disable
    public static void SetCursorHook(bool enable)
    {
        InputController.IsCursorHooked = enable;
    }

    // Back-compat: expose a simple text send used in some UI spots
    public static void SendText(string text)
    {
        try
        {
            foreach (var ch in text)
            {
                short vk = VkFromChar(ch);
                InputController.SendKey(vk, true);
                Thread.Sleep(20);
                InputController.SendKey(vk, false);
            }
        }
        catch (Exception ex)
        {
            Logger.LogMessage("InputControllerShim.SendText failed: " + ex.Message);
        }
    }

    private static short VkFromChar(char c)
    {
        try
        {
            return InputController.VkKeyScanSafe(c);
        }
        catch
        {
            return 0;
        }
    }
}
