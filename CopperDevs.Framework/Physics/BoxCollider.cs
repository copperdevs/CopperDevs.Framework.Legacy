using CopperDevs.Framework.Attributes;
using Raylib_CSharp.Rendering;

namespace CopperDevs.Framework.Physics;

[Disabled]
public class BoxCollider : Collider
{
    public override void Update()
    {
    }

    public override void DebugUpdate()
    {
        rlGraphics.DrawRectangle(-1, -1, 2, 2, Color.Green);
    }
}