using System.IO.Pipes;
using CopperDevs.Core;
using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Components;
using nkast.Aether.Physics2D.Dynamics;

namespace CopperDevs.Framework.Physics;

public class Rigidbody : GameComponent
{
    [Exposed] public bool IsStatic;

    private Body body = null!;
    private Collider collider;

    public override void Start()
    {
        OnComponentValueChanged += ValueChanged;
        Transform.Updated += TransformChanged;

        collider = GetComponent<Collider>();
        
        body = ParentScene?.PhysicsWorld.CreateBody(new PhysicsVector2(Transform.Position.X, Transform.Position.Y), Transform.Rotation, IsStatic ? BodyType.Static : BodyType.Dynamic)!;

        switch (collider)
        {
            case BoxCollider boxCollider:
                var fixture = body.CreateRectangle(Transform.Scale, Transform.Scale, 1, PhysicsVector2.Zero);
                fixture.Restitution = 0;
                fixture.Friction = 0.5f;
                break;
        }
    }

    public override void Stop()
    {
        OnComponentValueChanged -= ValueChanged;
        Transform.Updated -= TransformChanged;

        ParentScene?.PhysicsWorld.Remove(body);
    }

    public override void FixedUpdate()
    {
        Transform.Position = new Vector2(body.Position.X, body.Position.Y);
        Transform.Rotation = body.Rotation;
    }

    private void ValueChanged()
    {
        body.BodyType = IsStatic ? BodyType.Static : BodyType.Dynamic;
    }

    private void TransformChanged()
    {
        body.AngularVelocity = 0;
        body.LinearVelocity = PhysicsVector2.Zero;

        body.Awake = true;
        
        body.SetTransform(new PhysicsVector2(Transform.Position.X, Transform.Position.Y), Transform.Rotation);
    }
}