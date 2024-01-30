namespace CopperFramework.Renderer.DearImGui;

public interface IImGuiRenderer : IDisposable
{
    public void Setup();
    public void Shutdown();
    public void Begin();
    public void End();
}