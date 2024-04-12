using CopperFramework.Scenes;

namespace CopperFramework.Elements.Components;

public static class ComponentRegistry
{
    internal static List<GameObject> CurrentComponents
    {
        get => SceneManager.ActiveScene.SceneObjects;
        set => SceneManager.ActiveScene.SceneObjects = value;
    }
}