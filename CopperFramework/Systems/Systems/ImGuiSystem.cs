using CopperFramework.Renderer.DearImGui;
using CopperFramework.Renderer.DearImGui.OpenGl;
using ImGuiNET;

namespace CopperFramework.Systems.Systems;

public class ImGuiSystem : ISystem
{
    private CopperImGui<ImGuiRenderer> imGuiRenderer = null!;
    
    public SystemUpdateType GetUpdateType() => SystemUpdateType.Renderer;

    public void UpdateSystem()
    {
        imGuiRenderer.Begin();

        ImGui.ShowDemoWindow();
        
        imGuiRenderer.End();
    }

    public void LoadSystem()
    {
        imGuiRenderer = new CopperImGui<ImGuiRenderer>();
        imGuiRenderer.Setup(Framework.Window);
    }

    public void ShutdownSystem()
    {
        imGuiRenderer.Dispose();
    }
}