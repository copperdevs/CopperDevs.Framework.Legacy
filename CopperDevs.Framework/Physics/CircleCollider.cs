using CopperDevs.Framework.Attributes;
using Raylib_CSharp.Rendering;

namespace CopperDevs.Framework.Physics;

[Disabled]
public class CircleCollider : Collider
{
    public override void DebugUpdate()
    {
        rlGraphics.DrawCircleV(Vector2.Zero, 1, Color.Green);
    }
}