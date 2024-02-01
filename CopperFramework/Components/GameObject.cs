using System.Collections;
using CopperFramework.Data;
using CopperFramework.Renderer.DearImGui.Attributes;

namespace CopperFramework.Components;

public sealed class GameObject : IEnumerable<GameComponent>
{
    [HideInInspector] public Transform Transform { get; internal set; } = new();
    [HideInInspector] internal string DisplayName;

    [HideInInspector] internal readonly List<GameComponent> Components = new();

    public GameObject(string displayName)
    {
        DisplayName = displayName;
        ComponentRegistry.RegisterObject(this);
    }

    public void Add(GameComponent gameComponent)
    {
        Components.Add(gameComponent);
        gameComponent.Parent = this;
        gameComponent.Start();
        ComponentRegistry.RegisterObject(gameComponent);
    }

    public IEnumerator<GameComponent> GetEnumerator()
    {
        return Components.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public T[] GetComponentsOfType<T>() where T : GameComponent
    {
        return Components.Where(component => component.GetType() == typeof(T)).Cast<T>().ToArray();
    }

    public T GetFirstComponentOfType<T>() where T : GameComponent
    {
        return GetComponentsOfType<T>()[0];
    }
}