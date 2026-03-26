using Glider.Common.Objects;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

public class CameraRotator // Original: CameraRotator
{
    // Constants (Original: double_0, double_1, double_2)
    private const double HeadingTolerance = 0.15; // double_0
    private const double MaxPixelsPerPulse = 120.0; // double_1
    private const double MinRadianDelta = 0.087266462599716474; // double_2

    // Windows Message Constants
    private const uint WM_MOUSEMOVE = 0x0200;
    private const uint MK_LBUTTON = 0x0001;
    private const uint MK_RBUTTON = 0x0002;

    // State Flags
    private bool IsRightButtonDown; // Original: bool_0
    private bool IsSpinStopping;    // Original: bool_1
    private bool IsSpinActive;      // Original: bool_2

    private double PixelsPerRadian = 255.0; // Original: double_3 (byte.MaxValue)
    private double TargetHeading;           // Original: double_4

    private GSpellTimer ActionTimer;        // Original: gspellTimer_0
    private int RetryCount;                 // Original: int_0
    private int HeadingMemoryAddress;       // Original: int_1

    // Virtual Cursor Tracking for Background Panning
    private int VirtualCursorX;             // Original: int_2
    private int VirtualCursorY;             // Original: int_3

    // P/Invoke for Background Mouse Moves
    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

    private void Log(string message) // Original: method_0
    {
        Logger.smethod_1(message);
    }

    public void Initialize() // Original: method_1
    {
        ActionTimer = new GSpellTimer(ConfigManager.gclass61_0.method_3("MouseHoldMS"), true);
        IsRightButtonDown = false;
    }

    [SpecialName]
    public bool IsIdle() // Original: method_2
    {
        lock (this)
        {
            return !IsRightButtonDown;
        }
    }

    public void StopSpin(bool force) // Original: method_3
    {
        lock (this)
        {
            if (IsSpinActive)
            {
                Log("Stopping mouse spin, force = " + force);
                IsSpinStopping = true;
                IsSpinActive = false;
                InputController.IsCursorHooked = false; // Original: InputController.smethod_21(false)
            }

            ReleaseRightButton(force); // Original: method_6
        }
    }

    public void StartSpin(double targetHeading) // Original: method_4
    {
        lock (this)
        {
            if (IsSpinActive)
            {
                Log("New heading received by mousespin helper, updating field");
                TargetHeading = targetHeading;
            }
            else
            {
                if (GContext.Main.Me.IsSitting)
                {
                    GContext.Main.Log("Mouse spin invoked while sitting, standing up first!");
                    GContext.Main.SendKey("Common.Sit");
                    Thread.Sleep(1211);
                }

                Log("Starting mouse spin, new heading = " + targetHeading + ", my heading = " + GPlayerSelf.Me.Heading);
                HeadingMemoryAddress = GContext.Main.Me.GetHeadingAddress();

                CenterVirtualCursor(); // Original: method_11 / method_12

                IsSpinActive = true;
                IsSpinStopping = false;
                TargetHeading = targetHeading;
                PushRightButton(); // Original: InitializeComponent
            }
        }
    }

    private void PushRightButton() // Original: InitializeComponent
    {
        lock (this)
        {
            if (!IsRightButtonDown)
            {
                IsRightButtonDown = true;
                Log("Pushing down right button");
                InputController.SendMouseEvent(8); // Original: smethod_24(true) -> Right Down
                ActionTimer.Reset();
                Thread.Sleep(199);
            }
            else
            {
                Log("Skipping button push, already down");
            }
        }
    }

    private void ReleaseRightButton(bool waitOutTimer) // Original: method_6
    {
        lock (this)
        {
            var interrupted = false;
            if (!IsRightButtonDown)
                return;

            if (waitOutTimer)
            {
                Log("Waiting out timer on force call");
                interrupted = ActionTimer.WaitNoInterrupt();
            }

            if (ActionTimer.IsReady)
            {
                Log("Releasing right button");
                IsRightButtonDown = false;
                InputController.SendMouseEvent(16); // Original: smethod_25(true) -> Right Up
            }

            if (interrupted)
                throw new ThreadInterruptedException();
        }
    }

    public void ConsiderReleaseButton() // Original: method_7
    {
        lock (this)
        {
            if (!IsRightButtonDown || !IsSpinStopping || !ActionTimer.IsReady)
                return;

            ReleaseRightButton(false);
            Log("Releasing cursor in ConsiderReleaseButton");
            InputController.IsCursorHooked = false; // Original: smethod_21(false)
        }
    }

    public bool PulseSpin(bool isPrecision) // Original: method_8
    {
        lock (this)
        {
            double currentHeading = GameMemoryAccess.ReadFloat(HeadingMemoryAddress, "Quickheading");
            var headingDelta = GContext.Main.Movement.CompareHeadings(currentHeading, TargetHeading);

            if (Math.Abs(headingDelta) < HeadingTolerance)
            {
                StopSpin(false);
                return true;
            }

            var pixelDelta = CalculateRadianDelta(Math.Abs(headingDelta), isPrecision) * PixelsPerRadian; // Original: method_10
            if (pixelDelta > MaxPixelsPerPulse)
                pixelDelta = MaxPixelsPerPulse;

            if (headingDelta < 0.0)
                pixelDelta *= -1.0;

            var movementTimer = new GSpellTimer(2000, false);
            Log("Pulsing cursor: " + pixelDelta + " pixels");

            // Move mouse horizontally to adjust heading
            MoveVirtualMouseRelative((int)pixelDelta, 0, true); // Original: method_13

            var newHeading = 0.0;
            while (!movementTimer.IsReady)
            {
                newHeading = GameMemoryAccess.ReadFloat(HeadingMemoryAddress, "QuickHeadingCheck");
                if (newHeading == currentHeading)
                    Thread.Sleep(6);
                else
                    break;
            }

            if (newHeading == currentHeading)
            {
                ++RetryCount;
                if (RetryCount == 3)
                    throw new Exception("never able to change heading in mouse spin!");

                Log("Restarting spin, heading did not change");
                StopSpin(false);
                StartSpin(TargetHeading);
                return PulseSpin(isPrecision);
            }

            RetryCount = 0;
            var scalingAdjustment = 1.0 / Math.Abs(GContext.Main.Movement.CompareHeadings(newHeading, currentHeading) / pixelDelta);
            if (scalingAdjustment > 50.0)
                PixelsPerRadian = scalingAdjustment;

            var remainingDelta = GContext.Main.Movement.CompareHeadings(newHeading, TargetHeading);
            if ((remainingDelta <= 0.0 || headingDelta >= 0.0) && (remainingDelta >= 0.0 || headingDelta <= 0.0))
            {
                if (Math.Abs(remainingDelta) >= HeadingTolerance)
                    return false;
            }

            StopSpin(false);
            return true;
        }
    }

    [SpecialName]
    public bool IsActive() // Original: method_9
    {
        return IsSpinActive;
    }

    private double CalculateRadianDelta(double delta, bool isPrecision) // Original: method_10
    {
        double adjustedDelta = delta;
        if (delta > Math.PI / 2.0)
            adjustedDelta = Math.PI / 4.0;
        else if (delta > Math.PI / 4.0)
            adjustedDelta = Math.PI / 8.0;

        var baseRadian = Math.PI / 16.0;
        if (isPrecision && adjustedDelta > Math.PI / 16.0)
            adjustedDelta = baseRadian * 0.66;
        else if (!isPrecision)
            adjustedDelta = baseRadian * 0.44;

        return adjustedDelta;
    }

    private void CenterVirtualCursor() // Original: method_11 & method_12
    {
        Log("Centering virtual cursor for background spin.");
        // By relying strictly on the game's internal resolution mapping rather than Desktop.
        GameMemoryAccess.WorldToScreen(0.5, 0.5, out int centerX, out int centerY);
        VirtualCursorX = centerX;
        VirtualCursorY = centerY;

        SetVirtualMousePosition(VirtualCursorX, VirtualCursorY, false);
        Thread.Sleep(201);
    }

    private void MoveVirtualMouseRelative(int dx, int dy, bool isRightClick) // Original: method_13
    {
        VirtualCursorX += dx;
        VirtualCursorY += dy;
        SetVirtualMousePosition(VirtualCursorX, VirtualCursorY, isRightClick);
    }

    private void SetVirtualMousePosition(int x, int y, bool isRightClick) // Original: method_14
    {
        Log("SetVirtualMousePosition: " + x + "," + y);
        VirtualCursorX = x;
        VirtualCursorY = y;

        if (StartupClass.IsGliderInitialized && StartupClass.MainApplicationHandle != IntPtr.Zero)
        {
            uint wParam = isRightClick ? MK_RBUTTON : MK_LBUTTON;
            uint lParam = (uint)((y << 16) | (x & 0xFFFF));
            SendMessage(StartupClass.MainApplicationHandle, WM_MOUSEMOVE, wParam, lParam);
        }
    }

    public void SetCameraPitch(GGameCamera camera, float targetPitch) // Original: method_16
    {
        var startingPitchDelta = camera.Pitch - (double)targetPitch;
        CenterVirtualCursor();

        var actionTimer = new GSpellTimer(ConfigManager.gclass61_0.method_3("MouseHoldMS"), false);
        InputController.SendMouseEvent(2); // Original: smethod_24(false) -> Left Down
        Thread.Sleep(122);

        while (!actionTimer.IsReady)
        {
            var currentPitch = camera.Pitch;
            var currentPitchDelta = currentPitch - (double)targetPitch;

            if ((currentPitchDelta >= 0.0 || startingPitchDelta <= 0.0) &&
                (currentPitchDelta <= 0.0 || startingPitchDelta >= 0.0) &&
                Math.Abs(currentPitchDelta) >= Math.PI / 36.0)
            {
                Log("Delta on pulse: " + currentPitchDelta + ", oldpitch: " + currentPitch);
                var pixelDelta = CalculateRadianDelta(Math.Abs(currentPitchDelta), false) * PixelsPerRadian;

                if (pixelDelta > MaxPixelsPerPulse)
                    pixelDelta = MaxPixelsPerPulse;

                if (currentPitchDelta > 0.0)
                    pixelDelta *= -1.0;

                Log("Pulsing cursor: " + pixelDelta);

                // Move mouse vertically to adjust pitch (Left Click Down)
                MoveVirtualMouseRelative(0, (int)pixelDelta, false); // Original: method_13(0, (int)int_5)

                var verificationTimer = new GSpellTimer(1000, false);
                while (!verificationTimer.IsReady && camera.Pitch == (double)currentPitch)
                    Thread.Sleep(9);

                if (verificationTimer.IsReady)
                    throw new Exception("Camera pitch never changed in SetCameraPitch!");
            }
            else
            {
                break;
            }
        }

        actionTimer.Wait();
        InputController.SendMouseEvent(4); // Original: smethod_25(false) -> Left Up
        Logger.LogMessage("SetCameraPitch all done!");
    }
}