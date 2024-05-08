using CopperDearImGui.Attributes;
using CopperFramework.Elements.Components;
using CopperFramework.Scenes;

namespace CopperFramework.Utility;

[HideInInspector]
public class SingletonGameComponent<T> : GameComponent, ISingleton where T : GameComponent
{
    public static T Instance => SceneManager.ActiveScene.FindFirstObjectByType<T>();
}