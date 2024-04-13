using CopperCore;
using CopperDearImGui.Attributes;
using CopperFramework.Elements.Components;

namespace CopperFramework.Physics;

public class Rigidbody : GameComponent
{
    private bool isStatic;
    [HideInInspector] private Collider targetCollider;

    public override void Start()
    {
        targetCollider = GetComponent<Collider>();
    }

    public override void FixedUpdate()
    {
        if (isStatic)
            return;
        
        Transform.Position.Y += PhysicsSystem.GetGravity();
    }
}