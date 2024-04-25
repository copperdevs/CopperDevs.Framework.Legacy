using CopperDearImGui;
using CopperFramework.Elements.Systems;

namespace CopperFramework.UiCreator;

public class UiCreatorUiManagerSystem : BaseSystem<UiCreatorUiManagerSystem>
{
    public override SystemUpdateType GetUpdateType() => SystemUpdateType.UiRenderer;

    public override void UpdateSystem()
    {
        CopperImGui.MenuBar(null!, true,
            ("Menu", () =>
            {
                CopperImGui.MenuItem("New");
                CopperImGui.MenuItem("Open");
                CopperImGui.MenuItem("Save");
                CopperImGui.Separator();
                CopperImGui.MenuItem("Quit");
            }),
            ("Windows", () =>
            {
                CopperImGui.MenuItem("ImGui About", ref CopperImGui.ShowDearImGuiAboutWindow);
                CopperImGui.MenuItem("ImGui Demo", ref CopperImGui.ShowDearImGuiDemoWindow);
                CopperImGui.MenuItem("ImGui Metrics", ref CopperImGui.ShowDearImGuiMetricsWindow);
                CopperImGui.MenuItem("ImGui Debug Log", ref CopperImGui.ShowDearImGuiDebugLogWindow);
                CopperImGui.MenuItem("ImGui Id Stack Tool", ref CopperImGui.ShowDearImGuiIdStackToolWindow);
            }));
    }
}