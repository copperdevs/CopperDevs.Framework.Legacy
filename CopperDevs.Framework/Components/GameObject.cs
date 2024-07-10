using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Scenes;

namespace CopperDevs.Framework.Components;

public class GameObject : IEnumerable<GameComponent>
{
    [HideInInspector] internal string GameObjectName;
    [HideInInspector] internal Transform Transform = new();
    [HideInInspector] internal readonly List<GameComponent> Components = [];
    [HideInInspector] internal Scene? Scene { get; set; }

    public GameObject()
    {
        GameObjectName = "New GameObject";
    }

    public GameObject(string name)
    {
        GameObjectName = name;
    }

    public GameObject(string name, Transform transform)
    {
        GameObjectName = name;
        Transform = transform;
    }

    internal void UpdateComponents(Action<GameComponent> action)
    {
        foreach (var component in Components.ToList())
        {
            component.Transform = Transform;
            action?.Invoke(component);

            if (Transform != component.Transform) 
                component.Transform.Updated?.Invoke();

            if (Transform.Position != component.Transform.Position) 
                component.Transform.PositionUpdated?.Invoke(component.Transform.Position);

            if ((int)(Transform.Scale * 100) != (int)(component.Transform.Scale * 100)) 
                component.Transform.ScaleUpdated?.Invoke(component.Transform.Scale);

            if ((int)(Transform.Rotation * 100) != (int)(component.Transform.Rotation * 100)) 
                component.Transform.RotationUpdated?.Invoke(component.Transform.Rotation);

            Transform = component.Transform;
        }
    }

    public void Add(GameComponent gameComponent)
    {
        gameComponent.Parent = this;
        Components.Add(gameComponent);
    }

    public void Remove(GameComponent gameComponent)
    {
        Components.Remove(gameComponent);
        gameComponent.Transform = Transform;
        gameComponent.Stop();
        Transform = gameComponent.Transform;
    }

    public T GetComponent<T>(bool addIfNotFound = true) where T : GameComponent
    {
        foreach (var component in Components)
        {
            if (ComponentRegistry.AbstractChildren.Any(abstractChild => component.GetType().IsSubclassOf(abstractChild.Key)))
                return (T)component;

            if (component.GetType() == typeof(T))
                return (T)component;
        }

        return addIfNotFound ? AddComponent<T>() : null!;
    }

    public T AddComponent<T>() where T : GameComponent
    {
        return (T)AddComponent(typeof(T));
    }

    public GameComponent AddComponent(Type type)
    {
        return ComponentRegistry.Instantiate(type, this);
    }

    public IEnumerator<GameComponent> GetEnumerator()
    {
        return Components.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool HasComponent<T>() where T : GameComponent
    {
        return Components.Any(component => component.GetType() == typeof(T));
    }

    internal void OnComponentValueChanged() => UpdateComponents(component => component.OnComponentValueChanged?.Invoke());
}