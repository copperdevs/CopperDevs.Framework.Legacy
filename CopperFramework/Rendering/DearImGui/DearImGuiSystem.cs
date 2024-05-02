using CopperDearImGui;
using CopperFramework.Elements.Systems;
using CopperFramework.Rendering.DearImGui.ReflectionRenderers;

namespace CopperFramework.Rendering.DearImGui;

public class DearImGuiSystem : BaseSystem<DearImGuiSystem>
{
    public override SystemUpdateType GetUpdateType() => SystemUpdateType.UiRenderer;

    public override int GetPriority() => 100;

    public override void Update()
    {
        if (DebugSystem.Instance.DebugEnabled && !Engine.Instance.Settings.DisableDevTools)
            CopperImGui.Render();
    }

    public override void Start()
    {
        CopperImGui.RegisterFieldRenderer<Color, ColorFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<Texture2D, Texture2DFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<RenderTexture2D, RenderTexture2DFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<Transform, TransformFieldRenderer>();

        CopperImGui.Setup<CopperRlImGui>();
        CopperImGui.Rendered += RenderImGuiWindowsMenu;
    }

    public override void Stop()
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