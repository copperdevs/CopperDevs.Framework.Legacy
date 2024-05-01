using CopperFramework.Attributes;

namespace CopperFramework.Physics;

[Disabled]
public class CircleCollider : Collider
{
    public override void DebugUpdate()
    {
        Raylib.DrawCircleV(Vector2.Zero, 1, Color.Green);
    }
}