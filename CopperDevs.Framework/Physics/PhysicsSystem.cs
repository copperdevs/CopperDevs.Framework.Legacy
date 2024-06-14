using CopperDevs.Framework.Elements.Systems;

namespace CopperDevs.Framework.Physics;

public class PhysicsSystem : BaseSystem<PhysicsSystem>
{
    public float Gravity = -9.81f;
    
    public override SystemUpdateType GetUpdateType() => SystemUpdateType.Fixed;

    public override void Update()
    {
        
    }

    public override void Start()
    {
        
    }

    public override void Stop()
    {
    }

    public static float GetGravity()
    {
        return Instance.Gravity;
    }
}