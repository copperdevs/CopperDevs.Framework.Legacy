using CopperDevs.DearImGui;
using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Rendering.DearImGui.Windows;
using CopperDevs.Framework.Scenes;

namespace CopperDevs.Framework.Elements.Components;

public abstract class GameComponent
{
    protected internal GameObject Parent = null!;

    [HideInInspector] protected internal Transform Transform;

    protected Scene? ParentScene => Parent.Scene;

    public ref GameObject GetParent() => ref Parent;
    public ref Transform GetTransform() => ref Parent.Transform;

    protected internal Action OnComponentValueChanged = null!;

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

    public virtual void Stop()
    {
    }
}