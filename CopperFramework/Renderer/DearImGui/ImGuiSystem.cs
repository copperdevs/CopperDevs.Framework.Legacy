using CopperFramework.Renderer.DearImGui;
using CopperFramework.Renderer.DearImGui.OpenGl;
using CopperFramework.Util;
using ImGuiNET;

namespace CopperFramework.Systems.Systems;

public class ImGuiSystem : ISystem
{
    private CopperImGui<ImGuiRenderer> imGuiRenderer = null!;

    private readonly List<DearImGuiWindow> windows = LoadDearImGuiWindows();

    public SystemUpdateType GetUpdateType() => SystemUpdateType.Renderer;

    public void UpdateSystem()
    {
        imGuiRenderer.Begin();

        ImGui.ShowDemoWindow();

        foreach (var window in windows)
        {
            window.PreRender();

            if (ImGui.Begin(window.GetWindowName(), ref window.IsOpen, window.GetWindowFlags()))
            {
                window.Render();
                ImGui.End();
            }

            window.PostRender();
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