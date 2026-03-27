#nullable disable

using Glider.Common.Objects;
using System;

internal static class CameraProjection
{
    // WotLK 3.3.5a camera block layout confirmed from wowdev.wiki/Camera.
    private const int CameraPositionOffset = 0x8;
    private const int CameraViewMatrixOffset = 0x14;
    private const int CameraFovOffset = 0x40;
    private const double DegreesToRadians = Math.PI / 180.0;

    internal static bool TryProject(
        GLocation location,
        double zOffset,
        out double relativeX,
        out double relativeY,
        out int screenX,
        out int screenY,
        out GProcessMemoryManipulator.GStruct22 viewport,
        out string failureReason)
    {
        relativeX = 0.0;
        relativeY = 0.0;
        screenX = 0;
        screenY = 0;
        viewport = default(GProcessMemoryManipulator.GStruct22);
        failureReason = null;

        Matrix3 viewMatrix;
        Vector3 cameraPosition;
        float fieldOfView;

        if (!TryCaptureCamera(out viewMatrix, out cameraPosition, out fieldOfView, out viewport, out failureReason))
            return false;

        var worldPoint = Vector3.FromLocation(location);
        worldPoint.float_2 += (float)zOffset;

        var relative = Vector3.Subtract(worldPoint, cameraPosition);
        if (Vector3.Dot(relative, viewMatrix.GetRow(0)) < 0.0)
        {
            failureReason = "dot product behind camera";
            return false;
        }

        viewMatrix.InvertInPlace();
        var cameraSpace = viewMatrix.TransformVector(relative);
        var projected = new Vector3(-cameraSpace.float_1, -cameraSpace.float_2, cameraSpace.float_0);
        if (projected.float_2 <= 0.0)
        {
            failureReason = "camera Z <= 0 (" + projected.float_2 + ")";
            return false;
        }

        var halfWidth = viewport.method_1() / 2f;
        var halfHeight = viewport.method_0() / 2f;
        var verticalFieldOfView = fieldOfView * DegreesToRadians;
        var aspectRatio = viewport.method_1() / (double)viewport.method_0();
        var horizontalFieldOfView = 2.0 * Math.Atan(Math.Tan(verticalFieldOfView / 2.0) * aspectRatio);
        var horizontalScale = halfWidth / (float)Math.Tan(horizontalFieldOfView / 2.0);
        var verticalScale = halfHeight / (float)Math.Tan(verticalFieldOfView / 2.0);

        screenX = (int)(projected.float_0 * (double)horizontalScale / projected.float_2 + halfWidth);
        screenY = (int)(projected.float_1 * (double)verticalScale / projected.float_2 + halfHeight);

        var absoluteX = viewport.int_0 + screenX;
        var absoluteY = viewport.int_1 + screenY;
        if (!viewport.method_5(absoluteX, absoluteY))
        {
            failureReason = "projected click outside window, pixel=(" + absoluteX + "," + absoluteY + "), window=(" +
                            viewport.int_0 + "," + viewport.int_1 + "," + viewport.method_1() + "," +
                            viewport.method_0() + ")";
            return false;
        }

        relativeX = screenX / (double)viewport.method_1();
        relativeY = screenY / (double)viewport.method_0();
        return true;
    }

    internal static bool TryCaptureCamera(
        out Matrix3 viewMatrix,
        out Vector3 cameraPosition,
        out float fieldOfView,
        out GProcessMemoryManipulator.GStruct22 viewport,
        out string failureReason)
    {
        viewMatrix = null;
        cameraPosition = null;
        fieldOfView = 0.0f;
        viewport = default(GProcessMemoryManipulator.GStruct22);
        failureReason = null;

        var cameraBase = GameMemoryAccess.ReadInt32(MemoryOffsetTable.Instance.GetIntOffset("CameraBase"), "camerabase");
        if (cameraBase == 0)
        {
            failureReason = "camera base is unavailable";
            return false;
        }

        var cameraSub = GameMemoryAccess.ReadInt32(cameraBase + MemoryOffsetTable.Instance.GetIntOffset("CameraOff1"), "camerasub");
        if (cameraSub == 0)
        {
            failureReason = "camera substructure is unavailable";
            return false;
        }

        viewMatrix = new Matrix3();
        viewMatrix.LoadFromAddress(cameraSub + CameraViewMatrixOffset);

        cameraPosition = new Vector3();
        cameraPosition.LoadFromAddress(cameraSub + CameraPositionOffset);

        fieldOfView = GameMemoryAccess.ReadFloat(cameraSub + CameraFovOffset, "camerafov");
        if (fieldOfView <= 0.0f || float.IsNaN(fieldOfView) || float.IsInfinity(fieldOfView))
        {
            failureReason = "camera FOV is invalid";
            return false;
        }

        viewport = GameMemoryAccess.GetCursorPosition();
        if (viewport.method_1() <= 0 || viewport.method_0() <= 0)
        {
            failureReason = "viewport is empty";
            return false;
        }

        return true;
    }
}