namespace CopperDearImGui;

public interface ICopperImGuiRenderer
{
    public void Setup();
    public void Begin();
    public void End();
    public void Shutdown();
}