using CopperCore;
using CopperDearImGui;
using CopperDearImGui.Attributes;
using CopperFramework.Rendering.DearImGui.Windows;
using CopperFramework.Scenes;

namespace CopperFramework.Elements.Components;

public abstract class GameComponent : IUpdatable
{
    protected internal GameObject Parent = null!;

    [HideInInspector] protected internal Transform Transform;

    protected internal Scene? ParentScene => Parent.Scene;

    public ref GameObject GetParent() => ref Parent;
    public ref Transform GetTransform() => ref Parent.Transform;

    public T GetComponent<T>(bool addIfNotFound = true) where T : GameComponent
    {
        return Parent.GetComponent<T>(addIfNotFound)!;
    }
    
    public T AddComponent<T>() where T : GameComponent
    {
        return Parent.AddComponent<T>();
    }

    public bool IsCurrentInspectionTarget()
    {
        return CopperImGui.GetWindow<ComponentsManagerWindow>()?.CurrentObjectBrowserTarget == Parent;
    }
    
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