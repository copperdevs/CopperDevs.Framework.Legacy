using CopperCore;
using CopperDearImGui.Attributes;
using CopperFramework.Scenes;

namespace CopperFramework.Elements.Components;

public sealed class GameObject : IEnumerable
{
    [HideInInspector] internal string GameObjectName;
    [HideInInspector] internal Transform Transform = new();
    [HideInInspector] internal List<GameComponent> Components = new();
    [HideInInspector] internal Scene? Scene { get; set; }

    public GameObject()
    {
        GameObjectName = "New GameObject";
    }

    public GameObject(string name)
    {
        GameObjectName = name;
    }

    internal void UpdateComponents(Action<GameComponent> action)
    {
        foreach (var component in Components)
            action?.Invoke(component);
    }

    public void Add(GameComponent gameComponent)
    {
        gameComponent.Parent = this;
        Components.Add(gameComponent);
        
        gameComponent.Transform = Transform;
        gameComponent.Start();
        Transform = gameComponent.Transform;
    }

    public void Remove(GameComponent gameComponent)
    {
        Components.Remove(gameComponent);
        gameComponent.Transform = Transform;
        gameComponent.Stop();
        gameComponent.Sleep();
        Transform = gameComponent.Transform;
    }

    public IEnumerator GetEnumerator()
    {
        return Components.GetEnumerator();
    }

    public T GetComponent<T>(bool addIfNotFound = true) where T : GameComponent
    {
        foreach (var component in Components)
        {
            if (ComponentRegistry.AbstractChildren.Any(abstractChild => component.GetType().IsSubclassOf(abstractChild.Key)))
            {
                return (T)component;
            }

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
}