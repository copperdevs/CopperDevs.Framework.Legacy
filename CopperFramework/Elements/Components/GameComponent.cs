using CopperFramework.Data;
using CopperPlatformer.Core.Scenes;

namespace CopperFramework.Elements.Components;

public abstract class GameComponent
{
    protected Transform Transform;
    protected internal Scene? ParentScene { get; internal set; }

    public virtual void Start()
    {
    }

    public virtual void Awake()
    {
    }

    public virtual void Update()
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