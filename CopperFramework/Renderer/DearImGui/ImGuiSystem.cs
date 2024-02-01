using CopperFramework.Components;
using CopperFramework.Renderer.DearImGui.OpenGl;
using CopperFramework.Systems;
using CopperFramework.Util;
using ImGuiNET;

namespace CopperFramework.Renderer.DearImGui;

public class ImGuiSystem : ISystem
{
    private CopperImGui<ImGuiRenderer> imGuiRenderer = null!;

    private readonly List<DearImGuiWindow> windows = LoadDearImGuiWindows();

    public SystemUpdateType GetUpdateType() => SystemUpdateType.Renderer;
    public int GetPriority() => 100;

    public void UpdateSystem()
    {
        imGuiRenderer.Begin();
        
        ImGui.DockSpaceOverViewport(ImGui.GetMainViewport(), ImGuiDockNodeFlags.AutoHideTabBar | ImGuiDockNodeFlags.PassthruCentralNode);
        
        foreach (var window in windows.Where(window => window.IsOpen))
        {
            window.PreRender();

            if (ImGui.Begin(window.GetWindowName(), ref window.IsOpen, window.GetWindowFlags()))
            {
                window.Render();
                ImGui.End();
            }

            window.PostRender();
        }

        foreach (var window in windows)
        {
            if (!ImGui.BeginMainMenuBar()) 
                continue;
            
            if (ImGui.BeginMenu("Windows"))
            {
                ImGui.MenuItem(window.GetWindowName(), null, ref window.IsOpen);
                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();
        }
        
        foreach (var component in ComponentRegistry.GameComponents.ToList())
        {
            component.UiUpdate();
        }

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

    private static List<DearImGuiWindow> LoadDearImGuiWindows()
    {
        var targetType = typeof(DearImGuiWindow);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => targetType.IsAssignableFrom(p)).ToList();

        types.Remove(typeof(DearImGuiWindow));

        foreach (var type in types)
            Log.Info($"Loading new {nameof(DearImGuiWindow)}. Name: {type.FullName}");

        return types.Select(type => (DearImGuiWindow)Activator.CreateInstance(type)!).ToList();
    }
}