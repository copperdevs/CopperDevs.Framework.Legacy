using CopperFramework.Rendering.DearImGui.Attributes;
using CopperFramework.Scenes;

namespace CopperFramework.Elements.Components;

public abstract class GameComponent
{
    protected internal GameObject Parent { get; internal set; }

    [HideInInspector] protected internal Transform Transform;

    protected internal Scene? ParentScene => Parent.Scene;

    public virtual void Start()
    {
    }

    public virtual void Awake()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void DebugUpdate()
    {
    }

    public virtual void UiUpdate()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void Sleep()
    {
    }

    public virtual void Stop()
    {
    }
}