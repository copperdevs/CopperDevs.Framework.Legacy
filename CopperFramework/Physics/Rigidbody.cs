using CopperFramework.Elements.Components;

namespace CopperFramework.Physics;

public class Rigidbody : GameComponent
{
    public RigidbodyShape Shape = RigidbodyShape.Square;
    
    public enum RigidbodyShape
    {
        Square,
        Circle
    }
}