﻿using CopperFramework.Elements.Systems;

namespace CopperFramework.Rendering;

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
    
    public override void LoadSystem()
    {
        BaseRenderable.LoadQueuedItems();
        
        Shader.Load("Empty");
        Font.Load();
    }

    public override void ShutdownSystem()
    {
        foreach (var renderable in LoadedRenderableItems.Values.SelectMany(renderables => renderables).ToList())
        {
            renderable.UnLoadRenderable();
        }
    }
}