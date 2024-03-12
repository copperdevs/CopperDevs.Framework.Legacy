using System.Numerics;
using CopperFramework.Elements.Systems;
using CopperFramework.Rendering.DearImGui;
using ImGuiNET;

namespace CopperPlatformer.Core.Rendering.DearImGui.Windows;

public class SystemViewerWindow : BaseWindow
{
    public override string WindowName { get; protected internal set; } = "System Viewer";
    private int currentSystemIndex = 0;

    public override void Update()
    {
        var systems = SystemManager.GetSystems();
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
        }, ImGuiChildFlags.Border, 0, ImGui.GetWindowWidth() * 0.25f);
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
            
            CopperImGui.RenderValues(system);
            
        }, ImGuiChildFlags.Border);
    }
}