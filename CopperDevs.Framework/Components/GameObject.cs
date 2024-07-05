using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Scenes;

namespace CopperDevs.Framework.Components;

public class GameObject : IEnumerable<GameComponent>
{
    [HideInInspector] internal string GameObjectName;
    [HideInInspector] internal Transform Transform = new();
    [HideInInspector] internal List<GameComponent> Components = [];
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
        // gameComponent.Sleep();
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