namespace CopperFramework.Components;

public static class ComponentRegistry
{
    internal static readonly List<GameObject> GameObjects = new();
    internal static readonly List<GameComponent> GameComponents = new();
    
    public static void RegisterObject(GameObject gameObject)
    {
        GameObjects.Add(gameObject);
    }
    
    public static void RegisterObject(GameComponent gameComponent)
    {
        GameComponents.Add(gameComponent);
    }
}