using ImGuiNET;

namespace CopperFramework.Testing;

public static class Program
{
    public static void Main()
    {
        Framework.Load();
        Framework.OnUiRender += ImGui.ShowDemoWindow;
        Framework.Run();
    }
}