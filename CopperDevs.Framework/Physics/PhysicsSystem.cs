using CopperDevs.Framework.Elements.Components;
using CopperDevs.Framework.Elements.Systems;
using CopperDevs.Framework.Scenes;

namespace CopperDevs.Framework.Physics;

public class PhysicsSystem : BaseSystem<PhysicsSystem>
{
    public float Gravity = -9.81f;
    
    public int VelocityIterations = 6;
    public int PositionIterations = 2;

    public override SystemUpdateType GetUpdateType() => SystemUpdateType.Fixed;

    public override void Update()
    {
        SceneManager.ActiveScene.PhysicsWorld.Step(EngineWindow.FixedFrameTime, VelocityIterations, PositionIterations);
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