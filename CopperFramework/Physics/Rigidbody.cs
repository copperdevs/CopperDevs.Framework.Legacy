using CopperDearImGui.Attributes;
using CopperFramework.Attributes;
using CopperFramework.Elements.Components;

namespace CopperFramework.Physics;

// references
    // https://code.tutsplus.com/how-to-create-a-custom-2d-physics-engine-oriented-rigid-bodies--gamedev-8032t

[Disabled]
public class Rigidbody : GameComponent
{
    private Collider targetCollider;
    [Exposed] private bool isStatic = false;
    

    public override void Start()
    {
        targetCollider = GetComponent<Collider>();
    }

    public override void FixedUpdate()
    {
        if (isStatic)
            return;
    }
}