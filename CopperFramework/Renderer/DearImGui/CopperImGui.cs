namespace CopperFramework.Renderer.DearImGui;

public class CopperImGui<TRenderer> : IDisposable where TRenderer : IImGuiRenderer, new()
{
    private TRenderer currentRenderer = new TRenderer();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        
        if (currentRenderer is IDisposable currentRendererDisposable)
            currentRendererDisposable.Dispose();
    }
}