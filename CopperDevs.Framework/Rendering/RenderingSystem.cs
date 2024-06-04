using CopperDevs.Core.Utility;
using CopperDevs.Framework.Elements.Systems;
using CopperDevs.Framework.Utility;
using Raylib_CSharp.Interact;

namespace CopperDevs.Framework.Rendering;

public class RenderingSystem : BaseSystem<RenderingSystem>
{
    public Dictionary<Type, List<BaseRenderable>> LoadedRenderableItems { get; private set; } = new();

    public void RegisterRenderableItem<T>(T renderable) where T : BaseRenderable
    {
        if(LoadedRenderableItems.TryGetValue(typeof(T), out var value))
            value.Add(renderable);
        else
            LoadedRenderableItems.Add(typeof(T), new List<BaseRenderable> {renderable});
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
    
    public override void Start()
    {
        BaseRenderable.LoadQueuedItems();
        
        Shader.Load("Empty");
        Font.Load("Inter", ResourceLoader.LoadEmbeddedResourceBytes("CopperDevs.Framework.Resources.Fonts.Inter.static.Inter-Regular.ttf"));
        Font.Load();
    }

    public override void Update()
    {
        if (Input.IsKeyPressed(KeyboardKey.F3))
            Engine.CurrentWindow.ScreenShaderEnabled = !Engine.CurrentWindow.ScreenShaderEnabled;
    }

    public override void Stop()
    {
        foreach (var renderable in LoadedRenderableItems.Values.SelectMany(renderables => renderables).ToList())
        {
            renderable.UnLoadRenderable();
        }
    }
}