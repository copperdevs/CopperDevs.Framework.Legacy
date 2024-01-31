using CopperFramework.Data;
using CopperFramework.Renderer.DearImGui.Attributes;

namespace CopperFramework.Components;

public class GameComponent
{
    [HideInInspector] public GameObject Parent { get; protected internal set; } = null!;
    [HideInInspector] public Transform Transform => Parent.Transform;

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void Stop()
    {
    }
    
    public virtual void UiUpdate()
    {
    }

    public virtual void Render()
    {
        
    }
}