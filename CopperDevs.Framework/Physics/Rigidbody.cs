using Box2D.NetStandard.Collision.Shapes;
using Box2D.NetStandard.Dynamics.Bodies;
using CopperDevs.Core;
using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Attributes;
using CopperDevs.Framework.Elements.Components;

namespace CopperDevs.Framework.Physics;

// references
// https://code.tutsplus.com/how-to-create-a-custom-2d-physics-engine-oriented-rigid-bodies--gamedev-8032t

public class Rigidbody : GameComponent
{
    private Collider targetCollider;
    [Exposed] private bool isStatic = false;
    private bool addedToWorld;
    private Body body;


    public override void Start()
    {
        targetCollider = GetComponent<Collider>();
        AddToWorld();
    }

    public override void Awake()
    {
        AddToWorld();
    }

    public override void Sleep()
    {
        RemoveFromWorld();
    }

    public override void Stop()
    {
        RemoveFromWorld();
    }

    public override void FixedUpdate()
    {
        Transform.Rotation = body.GetAngle();
        Transform.Position = body.GetPosition();
    }

    private void AddToWorld()
    {
        if (addedToWorld)
            return;
        addedToWorld = true;

        body = ParentScene?.PhysicsWorld?.CreateBody(new BodyDef { type = BodyType.Dynamic })!;
        
        var shape = new PolygonShape();
        shape.SetAsBox(Transform.Scale/2,Transform.Scale/2, in Transform.Position, Transform.Rotation);

        body.CreateFixture(shape, 1);

        Transform.Updated += TransformUpdated;
        OnComponentValueChanged += ComponentUpdated;
    }

    private void RemoveFromWorld()
    {
        if (!addedToWorld)
            return;
        addedToWorld = false;
        
        ParentScene?.PhysicsWorld?.DestroyBody(body);
        
        Transform.Updated -= TransformUpdated;
        OnComponentValueChanged += ComponentUpdated;
    }

    private void TransformUpdated()
    {
        body.SetTransform(Transform.Position, Transform.Rotation);
    }

    private void ComponentUpdated()
    {
        Log.Debug("value changed");
    }
}