namespace CopperFramework.Rendering;

public static class RenderingManager
{
    public static Dictionary<Type, List<IRenderable>> LoadedRenderableItems { get; private set; } = new();

    public static void RegisterRenderableItem<T>(T renderable) where T : IRenderable
    {
        if(LoadedRenderableItems.ContainsKey(typeof(T)))
            LoadedRenderableItems[typeof(T)].Add(renderable);
        else
            LoadedRenderableItems.Add(typeof(T), new List<IRenderable> {renderable});
    }

    public static List<T> GetRenderableItems<T>() where T : IRenderable
    {
        return LoadedRenderableItems[typeof(T)].Cast<T>().ToList();
    }
}