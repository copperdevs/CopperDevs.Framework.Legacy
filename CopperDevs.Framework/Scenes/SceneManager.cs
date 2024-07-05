using CopperDevs.Framework.Components;
using Force.DeepCloner;

namespace CopperDevs.Framework.Scenes;

public static class SceneManager
{
    private static readonly Dictionary<string, Scene> Scenes = new();

    public static Scene ActiveScene
    {
        get => GetCurrentScene();
        set => LoadScene(value);
    }

    private static Scene? currentScene;

    public static Scene GetCurrentScene()
    {
        if (currentScene is null && Scenes.Count >= 1)
            currentScene = Scenes[Scenes.Keys.First()];
        currentScene ??= [];
        return currentScene;
    }

    public static void LoadScene(string sceneId, bool clone = true)
    {
        LoadScene(Scenes[sceneId], clone);
    }

    public static void LoadScene(Scene targetScene, bool clone = true)
    {
        if (currentScene is not null)
            ComponentManager.UpdateSceneComponents(currentScene, ComponentManager.ComponentUpdateType.Stop);

        if (!Scenes.ContainsKey(targetScene))
            return;
        currentScene = clone ? targetScene.DeepClone() : targetScene;
        ComponentManager.UpdateSceneComponents(currentScene, ComponentManager.ComponentUpdateType.Start);
    }

    internal static void RegisterScene(Scene targetScene)
    {
        Scenes.Add(targetScene.Id, targetScene);
    }

    public static List<Scene> GetAllScenes() => Scenes.Values.ToList();
}