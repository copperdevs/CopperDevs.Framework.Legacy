using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Elements.Components;

namespace CopperDevs.Framework.Physics;

public class Rigidbody : GameComponent
{
    [Exposed] public bool isStatic;
    [Exposed] [ReadOnly] private Vector2 velocity;

    public override void Start()
    {
        
    }

    public override void Stop()
    {
        
    }

    public override void Update()
    {
        if (!isStatic)
        {
            velocity.Y += -9.81f * Time.DeltaTime;
            Transform.Position += velocity;
        }
        else
        {
            velocity = Vector2.Zero;
        }
    }
}