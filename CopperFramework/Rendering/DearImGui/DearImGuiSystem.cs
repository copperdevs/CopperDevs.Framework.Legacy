using CopperDearImGui;
using CopperDearImGui.Utility;
using CopperFramework.Elements.Systems;
using CopperFramework.Rendering.DearImGui.ReflectionRenderers;
using CopperFramework.Utility;
using ImGuiNET;

namespace CopperFramework.Rendering.DearImGui;

public class DearImGuiSystem : SystemSingleton<DearImGuiSystem>, ISystem
{
    public SystemUpdateType GetUpdateType() => SystemUpdateType.UiRenderer;

    public int GetPriority() => 100;

    public void UpdateSystem()
    {
        if (DebugSystem.Instance.DebugEnabled)
            CopperImGui.Render();
    }

    public void LoadSystem()
    {
        CopperImGui.RegisterFieldRenderer<Color, ColorFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<Texture2D, Texture2DFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<RenderTexture2D, RenderTexture2DFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<Transform, TransformFieldRenderer>();
        
        CopperImGui.Setup<CopperRlCopperImGui>();
        CopperImGui.Rendered += RenderImGuiWindowsMenu;
    }

    public void ShutdownSystem()
    {
        CopperImGui.Rendered -= RenderImGuiWindowsMenu;
        CopperImGui.Shutdown();
    }

    private void RenderImGuiWindowsMenu()
    {
        if (!DebugSystem.Instance.DebugEnabled)
            return;
        
        CopperImGui.MenuBar(null!, true, ("Windows", () =>
            {
                CopperImGui.MenuItem("ImGui About", ref CopperImGui.ShowDearImGuiAboutWindow);
                CopperImGui.MenuItem("ImGui Demo", ref CopperImGui.ShowDearImGuiDemoWindow);
                CopperImGui.MenuItem("ImGui Metrics", ref CopperImGui.ShowDearImGuiMetricsWindow);
                CopperImGui.MenuItem("ImGui Debug Log", ref CopperImGui.ShowDearImGuiDebugLogWindow);
                CopperImGui.MenuItem("ImGui Id Stack Tool", ref CopperImGui.ShowDearImGuiIdStackToolWindow);
            }));
    }
}