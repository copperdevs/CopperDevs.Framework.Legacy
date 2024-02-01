using System.Numerics;
using CopperFramework.Components;
using CopperFramework.Util;

namespace CopperFramework.Data;

// TODO: Make a high level camera component
public class Camera : GameComponent
{
    private static Vector3 GlobalPosition = new(0.0f, 0.0f, 3.0f);
    private static Vector3 GlobalForward = new(0.0f, 0.0f, -1.0f);
    private static Vector3 GlobalUp = Vector3.UnitY;
    private static float GlobalZoom = 45f;
    private static Vector2 GlobalClippingPlane = new(0.1f, 100f);

    public static Matrix4x4 ViewMatrix =>
        Matrix4x4.CreateLookAt(GlobalPosition, GlobalPosition + GlobalForward, GlobalUp);

    public static Matrix4x4 ProjectionMatrix => Matrix4x4.CreatePerspectiveFieldOfView(
        MathUtil.DegreesToRadians(GlobalZoom),
        (float)CopperWindow.silkWindow.Size.X / (float)CopperWindow.silkWindow.Size.Y,
        GlobalClippingPlane.X, GlobalClippingPlane.Y);

    public override void Update()
    {
        GlobalPosition = Transform.Position;
        GlobalUp = Transform.Up;
        GlobalForward = Transform.Forward;
    }
}