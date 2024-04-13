using CopperCore;
using CopperFramework.Elements.Components;
using CopperFramework.Elements.Systems;
using CopperFramework.Utility;

namespace CopperFramework.Physics;

public class PhysicsSystem : SystemSingleton<PhysicsSystem>, ISystem
{
    private float gravity = -9.81f;
    
    public SystemUpdateType GetUpdateType()
    {
        return SystemUpdateType.Fixed;
    }

    public int GetPriority()
    {
        return 0;
    }

    public void UpdateSystem()
    {
    }

    public void LoadSystem()
    {
        ComponentRegistry.RegisterAbstractSubclass<Collider, BoxCollider>();
        ComponentRegistry.RegisterAbstractSubclass<Collider, CircleCollider>();
    }

    public void ShutdownSystem()
    {
    }

    public static float GetGravity()
    {
        return -9.81f;
        return Instance.gravity;
    }
}