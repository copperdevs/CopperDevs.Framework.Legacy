using CopperDevs.Framework.Utility;

namespace CopperDevs.Framework.Rendering;

public class RenderingSystem : Singleton<RenderingSystem>
{
    public Dictionary<Type, List<BaseRenderable>> LoadedRenderableItems { get; private set; } = new();

    public void RegisterRenderableItem<T>(T renderable) where T : BaseRenderable
    {
        if (LoadedRenderableItems.TryGetValue(typeof(T), out var value))
            value.Add(renderable);
        else
            LoadedRenderableItems.Add(typeof(T), [renderable]);
    }

    public List<T> GetRenderableItems<T>() where T : BaseRenderable
    {
        return LoadedRenderableItems[typeof(T)].Cast<T>().ToList();
    }

    public void DeregisterRenderableItem<T>(T renderable) where T : BaseRenderable
    {
        var targetList = LoadedRenderableItems[typeof(T)];
        targetList.Remove(renderable);
        LoadedRenderableItems[typeof(T)] = targetList;
    }

    internal void Start()
    {
        BaseRenderable.LoadQueuedItems();

        Shader.Load("Empty");

        Font.Load("Inter", ResourceLoader.LoadEmbeddedResourceBytes("CopperDevs.Framework.Resources.Fonts.Inter.static.Inter-Regular.ttf"));
        Font.Load();
    }

    internal void Stop()
    {
        foreach (var renderable in LoadedRenderableItems.Values.SelectMany(renderables => renderables).ToList()) 
            renderable.UnLoadRenderable();
    }
}