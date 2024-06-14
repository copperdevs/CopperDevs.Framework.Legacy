using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Elements.Components;

namespace CopperDevs.Framework.Physics;

public class Rigidbody : GameComponent
{
    [Exposed] public bool isStatic;
    [Exposed] [ReadOnly] private Vector2 velocity;

    public override void Update()
    {
        if (!isStatic)
        {
            velocity.Y += PhysicsSystem.Instance.Gravity * Time.DeltaTime;
            Transform.Position += velocity;
        }
        else
        {
            velocity = Vector2.Zero;
        }
    }
}