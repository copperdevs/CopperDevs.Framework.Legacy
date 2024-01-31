using ImGuiNET;

namespace CopperFramework.Renderer.DearImGui.Windows;

public class TestWindow : DearImGuiWindow
{
    public override void Render()
    {
        ImGui.Text($"test");
    }
}