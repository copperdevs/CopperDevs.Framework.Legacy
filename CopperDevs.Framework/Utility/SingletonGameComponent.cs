using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Common.Utility;
using CopperDevs.Framework.Components;
using CopperDevs.Framework.Scenes;

namespace CopperDevs.Framework.Utility;

[HideInInspector]
public class SingletonGameComponent<T> : GameComponent, ISingleton where T : GameComponent
{
    public static T Instance => SceneManager.ActiveScene.FindFirstObjectByType<T>();
}