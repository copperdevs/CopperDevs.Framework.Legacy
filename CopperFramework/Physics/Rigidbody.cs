using CopperCore;
using CopperFramework.Elements.Components;
using CopperFramework.Rendering.DearImGui.Attributes;

namespace CopperFramework.Physics;

public class Rigidbody : GameComponent
{
    private bool isStatic;
    [HideInInspector] private Collider targetCollider;

    public override void Start()
    {
        targetCollider = GetComponent<Collider>();
        Log.Info($"{GetComponent<Collider>()} | {GetComponent<Collider>() is null}");
    }

    public override void FixedUpdate()
    {
        if (isStatic)
            return;
        
        Transform.Position.Y += PhysicsSystem.GetGravity();
    }
}