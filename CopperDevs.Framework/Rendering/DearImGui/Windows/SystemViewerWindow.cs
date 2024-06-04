using CopperDevs.DearImGui;
using CopperDevs.Framework.Elements.Systems;
using ImGuiNET;

namespace CopperDevs.Framework.Rendering.DearImGui.Windows;

public class SystemViewerWindow : BaseWindow
{
    public override string WindowName { get; protected set; } = "System Viewer";
    private int currentSystemIndex;

    public override void Update()
    {
        SystemManager.GetSystems();
        CopperImGui.HorizontalGroup(SelectorWindow, InspectorWindow);
    }

    private void SelectorWindow()
    {
        CopperImGui.Group("system_viewer_selector_window", () =>
        {
            var systems = SystemManager.GetSystems();

            for (var i = 0; i < systems.Count; i++)
            {
                var localI = i;
                CopperImGui.Selectable($"{systems[i].GetType().Name}", i == currentSystemIndex,
                    () => { currentSystemIndex = localI; });
            }
        }, ImGuiChildFlags.Border, 0, CopperImGui.CurrentWindowWidth * 0.25f);
    }

    private void InspectorWindow()
    {
        CopperImGui.Group("system_viewer_inspector_window", () =>
        {
            var system = SystemManager.GetSystems()[currentSystemIndex];

            CopperImGui.Separator("System Info");

            CopperImGui.Text(system.GetType().Name, "Name");
            CopperImGui.Text(system.GetType().FullName!, "Full Name");
            CopperImGui.Text(system.GetUpdateType().ToString(), "System Type");
            CopperImGui.Text(system.GetPriority().ToString(), "System Priority");

            CopperImGui.Separator();

            system.UiUpdate();
        }, ImGuiChildFlags.Border);
    }
}