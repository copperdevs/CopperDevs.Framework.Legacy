using CopperFramework.Rendering.DearImGui.Attributes;
using CopperFramework.Scenes;

namespace CopperFramework.Elements.Components;

public class GameObject : IEnumerable
{
    [HideInInspector] internal string GameObjectName;
    [HideInInspector] internal Transform Transform = new();
    [HideInInspector] internal List<GameComponent> Components = new();
    [HideInInspector] protected internal Scene? Scene { get; internal set; }

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
        gameComponent.Start();
    }

    public void Remove(GameComponent gameComponent)
    {
        Components.Remove(gameComponent);
        gameComponent.Stop();
        gameComponent.Sleep();
    }

    public IEnumerator GetEnumerator()
    {
        return Components.GetEnumerator();
    }
}