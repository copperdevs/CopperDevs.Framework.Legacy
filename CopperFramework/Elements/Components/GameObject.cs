using CopperFramework.Rendering.DearImGui.Attributes;
using CopperFramework.Scenes;

namespace CopperFramework.Elements.Components;

public class GameObject : IEnumerable
{
    [HideInInspector] internal string GameObjectName = "New GameObject";
    [HideInInspector] internal Transform Transform = new();
    [HideInInspector] internal List<GameComponent> Components = new();
    [HideInInspector] protected internal Scene? Scene { get; internal set; }

    internal void UpdateComponents(Action<GameComponent> action)
    {
        Components.ForEach(action);
    }

    public void Add(GameComponent gameComponent)
    {
        gameComponent.Parent = this;
        Components.Add(gameComponent);
    }

    public void Remove(GameComponent gameComponent)
    {
        Components.Remove(gameComponent);
    }

    public IEnumerator GetEnumerator()
    {
        return Components.GetEnumerator();
    }
}