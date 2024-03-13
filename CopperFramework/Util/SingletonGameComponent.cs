using CopperFramework.Elements.Components;
using CopperFramework.Scenes;

namespace CopperFramework.Util;

public class SingletonGameComponent<T> : GameComponent, ISingleton where T : GameComponent, new()
{
    public static T Instance => GetInstance();
    private static T? instance;

    private static T GetInstance()
    {
        if (SceneManager.ActiveScene.Contains(instance) && instance is not null)
            return instance;
        return instance ??= ComponentRegistry.GetFirstComponentOfType<T>();
    }
}