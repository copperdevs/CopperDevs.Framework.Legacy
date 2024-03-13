using CopperFramework.Scenes;

namespace CopperFramework.Elements.Components;

public static class ComponentRegistry
{
    internal static List<GameComponent> CurrentComponents
    {
        get => SceneManager.ActiveScene.SceneComponents;
        set => SceneManager.ActiveScene.SceneComponents = value;
    }

    internal static bool CheckIfCurrentComponentsContainsType<T>() where T : class
    {
        return CurrentComponents.Any(component => component.GetType() == typeof(T));
    }
    
    internal static bool CheckIfCurrentComponentsContainsType<T>(T type) where T : class
    {
        return CurrentComponents.Any(component => component.GetType() == type.GetType());
    }
    internal static T GetFirstComponentOfType<T>() where T: GameComponent, new()
    {
        foreach (var component in CurrentComponents.Where(component => component.GetType() == typeof(T)))
        {
            return (T)component;
        }

        var newComponent = new T();
        SceneManager.ActiveScene.Add(newComponent);
        return newComponent;
    }
}