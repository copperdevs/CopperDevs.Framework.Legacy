using CopperCore;
using CopperFramework.Elements.Components;

namespace CopperFramework.Physics;

public class Rigidbody : GameComponent
{
    private bool isStatic;

    public override void FixedUpdate()
    {
        if (isStatic)
            return;
        
        Transform.Position.Y += PhysicsSystem.GetGravity();
    }
}