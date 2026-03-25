using Glider.Common.Objects;

// Compatibility extension methods for decompiled callsites that expect
// obfuscated method names on CameraRotator. These forward to the
// refactored CameraRotator implementation.
public static class CameraRotatorExtensions
{
    public static void method_1(this CameraRotator self)
    {
        self.Initialize();
    }

    public static void method_3(this CameraRotator self, bool force)
    {
        self.StopSpin(force);
    }

    public static void method_4(this CameraRotator self, double heading)
    {
        self.StartSpin(heading);
    }

    public static bool method_9(this CameraRotator self)
    {
        return self.IsActive();
    }

    public static void method_8(this CameraRotator self, bool fast)
    {
        self.PulseSpin(fast);
    }

    public static void method_16(this CameraRotator self, GGameCamera cam, float pitch)
    {
        self.SetCameraPitch(cam, pitch);
    }

    public static void method_7(this CameraRotator self)
    {
        self.ConsiderReleaseButton();
    }
}
