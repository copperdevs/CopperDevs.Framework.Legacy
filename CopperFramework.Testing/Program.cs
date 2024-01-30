using CopperFramework.Renderer.DearImGui;
using CopperFramework.Renderer.DearImGui.OpenGl;
using ImGuiNET;

namespace CopperFramework.Testing;

public static class Program
{
    public static void Main()
    {
        using var window = new CopperWindow();
        var imGuiRenderer = new CopperImGui<ImGuiRenderer>();

        window.OnLoad += () =>
        {
            imGuiRenderer.Setup(window);
        };

        window.OnRender += () =>
        {
            imGuiRenderer.Begin();

            ImGui.ShowDemoWindow();

            imGuiRenderer.End();
        };

        window.Run();
    }
}