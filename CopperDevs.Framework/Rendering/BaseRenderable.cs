namespace CopperDevs.Framework.Rendering;

public abstract class BaseRenderable
{
    private static readonly List<BaseRenderable> RenderableLoadQueue = [];
    private static bool requireQueue = true;

    protected void BaseLoad(BaseRenderable renderable)
    {
        if (requireQueue)
            RenderableLoadQueue.Add(renderable);
        else
            LoadRenderable();
    }


    public abstract void LoadRenderable();
    public abstract void UnLoadRenderable();
    
    internal static void LoadQueuedItems()
    {
        if (!requireQueue)
            return;

        requireQueue = false;

        foreach (var renderable in RenderableLoadQueue)
        {
            renderable.LoadRenderable();
        }
    }
}