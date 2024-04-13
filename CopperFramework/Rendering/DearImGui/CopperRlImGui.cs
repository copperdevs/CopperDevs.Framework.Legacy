using CopperDearImGui;

namespace CopperFramework.Rendering.DearImGui;

public class CopperRlCopperImGui : ICopperImGuiRenderer
{
    public void Setup()
    {
        rlImGui.Setup(true, true);
    }

    public void Begin()
    {
        rlImGui.Begin();
    }

    public void End()
    {
        rlImGui.End();
    }

    public void Shutdown()
    {
        rlImGui.Shutdown();
    }
}