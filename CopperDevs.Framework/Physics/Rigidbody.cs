using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Components;
using nkast.Aether.Physics2D.Dynamics;

namespace CopperDevs.Framework.Physics;

public class Rigidbody : GameComponent
{
    [Exposed] public bool IsStatic;

    private Body? body;
    private Collider? collider;
    private Fixture? fixture;

    public override void Start()
    {
        OnComponentValueChanged += ValueChanged;
        Transform.Updated += TransformChanged;

        collider = GetComponent<Collider>();

        body = ParentScene?.PhysicsWorld.CreateBody(new PhysicsVector2(Transform.Position.X, Transform.Position.Y), Transform.Rotation, IsStatic ? BodyType.Static : BodyType.Dynamic)!;

        CreateFixture();
    }

    public override void Stop()
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        OnComponentValueChanged -= ValueChanged;
        Transform.Updated -= TransformChanged;
#pragma warning restore CS8601 // Possible null reference assignment.

        ParentScene?.PhysicsWorld.Remove(body);
    }

    public override void FixedUpdate()
    {
        if (collider is null || body is null)
            return;

        Transform.Position = new Vector2(body.Position.X, body.Position.Y);
        Transform.Rotation = body.Rotation;
    }

    private void ValueChanged()
    {
        if (collider is null || body is null)
            return;

        body.BodyType = IsStatic ? BodyType.Static : BodyType.Dynamic;
    }

    private void TransformChanged()
    {
        if (collider is null || body is null)
            return;

        body.AngularVelocity = 0;
        body.LinearVelocity = PhysicsVector2.Zero;

        body.Awake = true;

        body.SetTransform(new PhysicsVector2(Transform.Position.X, Transform.Position.Y), Transform.Rotation);
        CreateFixture();
    }

    private void CreateFixture()
    {
        if (collider is null || body is null)
            return;

        if (fixture is not null && fixture.Body == body)
            body.Remove(fixture);


        switch (collider)
        {
            case BoxCollider boxCollider:
                fixture = body.CreateRectangle(Transform.Scale.X * 2, Transform.Scale.Y * 2, 1, PhysicsVector2.Zero);
                fixture.Restitution = 0;
                fixture.Friction = 0.5f;
                break;
        }
    }
}