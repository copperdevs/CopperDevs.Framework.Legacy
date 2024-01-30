using System.Numerics;
using CopperFramework.Util;

namespace CopperFramework.ECS.Components;

// TODO: Make a high level camera component
public class Camera : Component
{
    private static Vector3 position = new(0.0f, 0.0f, 3.0f);
    private static Vector3 front = new(0.0f, 0.0f, -1.0f);
    private static Vector3 up = Vector3.UnitY;
    private static Vector3 direction = Vector3.Zero;
    private static float yaw = -90f;
    private static float pitch = 0f;
    private static float zoom = 45f;
    private static Vector2 ClippingPlane = new(0.1f, 100f);

    public static Matrix4x4 ViewMatrix => Matrix4x4.CreateLookAt(position, position + front, up);

    public static Matrix4x4 ProjectionMatrix => Matrix4x4.CreatePerspectiveFieldOfView(
        MathUtil.DegreesToRadians(zoom),
        (float)CopperWindow.silkWindow.Size.X / (float)CopperWindow.silkWindow.Size.Y,
        ClippingPlane.X, ClippingPlane.Y);
}