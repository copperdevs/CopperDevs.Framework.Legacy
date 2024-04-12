namespace CopperFramework.Scenes;

public static class SceneManager
{
    private static readonly Dictionary<Guid, Scene> Scenes = new();

    public static Scene ActiveScene
    {
        get => GetCurrentScene();
        set => LoadScene(value);
    }
    
    private static Scene? currentScene;

    public static Scene GetCurrentScene()
    {
        if(currentScene is null && Scenes.Count >= 1) 
            currentScene = Scenes[Scenes.Keys.First()];
        currentScene ??= new Scene();
        return currentScene;
    }

    public static void LoadScene(Guid sceneId)
    {
        LoadScene(Scenes[sceneId]);
    }

    public static void LoadScene(Scene targetScene)
    {
        currentScene?.SceneObjects.ForEach(gameObject => gameObject.UpdateComponents(component => component.Sleep()));
        if (Scenes.ContainsKey(targetScene))
            currentScene = targetScene;
        targetScene?.SceneObjects.ForEach(gameObject => gameObject.UpdateComponents(component => component.Awake()));
    }

    internal static void RegisterScene(Scene targetScene)
    {
        Scenes.Add(targetScene.Id, targetScene);
    }

    public static List<Scene> GetAllScenes() => Scenes.Values.ToList();
}