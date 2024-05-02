using CopperCore;
using CopperFramework.Elements.Components;
using CopperFramework.Elements.Systems;
using CopperFramework.Utility;

namespace CopperFramework.Physics;

public class PhysicsSystem : BaseSystem<PhysicsSystem>
{
    public float Gravity = -9.81f;

    public override SystemUpdateType GetUpdateType() => SystemUpdateType.Fixed;

    public override void Update()
    {
    }

    public override void Start()
    {
        ComponentRegistry.RegisterAbstractSubclass<Collider, BoxCollider>();
        ComponentRegistry.RegisterAbstractSubclass<Collider, CircleCollider>();
    }

    public override void Stop()
    {
    }

    public static float GetGravity()
    {
        return Instance.Gravity;
    }
}