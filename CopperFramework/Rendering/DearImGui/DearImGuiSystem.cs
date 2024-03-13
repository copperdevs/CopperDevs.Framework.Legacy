using CopperFramework.Elements.Systems;

namespace CopperFramework.Rendering.DearImGui;

public class DearImGuiSystem : ISystem
{
    private static readonly List<BaseWindow> Windows = new();

    public SystemUpdateType GetUpdateType() => SystemUpdateType.UiRenderer;

    public int GetPriority() => 100;

    public void UpdateSystem()
    {
        if (DebugSystem.Instance.DebugEnabled)
            CopperImGui.Render();
    }

    public void LoadSystem()
    {
        CopperImGui.Setup<CopperRlImGui>();
    }

    public void ShutdownSystem()
    {
        CopperImGui.Shutdown();
    }


    private class CopperRlImGui : CopperImGui.IImGuiRenderer
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
}