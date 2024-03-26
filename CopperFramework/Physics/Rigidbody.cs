using CopperFramework.Elements.Components;

namespace CopperFramework.Physics;

public class Rigidbody : GameComponent
{
    public RigidbodyShape Shape = RigidbodyShape.Square;
    public RigidbodyType Type = RigidbodyType.Dynamic;

    public enum RigidbodyShape
    {
        Square,
        Circle
    }

    public enum RigidbodyType
    {
        Dynamic,
        Static
    }

    public override void FixedUpdate()
    {
        
    }
}