using CopperDevs.Framework.Attributes;

namespace CopperDevs.Framework.Physics;

[Disabled]
public class CircleCollider : Collider
{
    public override void DebugUpdate()
    {
        Raylib.DrawCircleV(Vector2.Zero, 1, Color.Green);
    }
}