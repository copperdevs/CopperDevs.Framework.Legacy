using CopperDevs.DearImGui;
using CopperDevs.DearImGui.Renderer.Raylib;
using ImGuiNET;

namespace CopperDevs.Framework.Rendering.DearImGui;

internal static class EngineWindows
{
    private static Engine Engine => Engine.Instance;

    internal static void RenderGameWindow() => CopperImGui.Window("Game", GameWindow, ImGuiWindowFlags.NoCollapse);

    private static void GameWindow()
    {
        RlImGuiRenderer.RenderTextureFit(Engine.GameRenderTexture, false);
        Engine.GameWindowHovered = ImGui.IsWindowHovered();

        var drawList = ImGui.GetWindowDrawList();
        Engine.GameWindowPositionOne = drawList.VtxBuffer[drawList.VtxBuffer.Size - 4].pos;
        Engine.GameWindowPositionTwo = drawList.VtxBuffer[drawList.VtxBuffer.Size - 2].pos;
    }

    internal static void RenderMenuBar() => CopperImGui.MenuBar(true, ("Windows", MenuBar));

    private static void MenuBar()
    {
        CopperImGui.MenuItem("ImGui About", ref CopperImGui.ShowDearImGuiAboutWindow);
        CopperImGui.MenuItem("ImGui Demo", ref CopperImGui.ShowDearImGuiDemoWindow);
        CopperImGui.MenuItem("ImGui Metrics", ref CopperImGui.ShowDearImGuiMetricsWindow);
        CopperImGui.MenuItem("ImGui Debug Log", ref CopperImGui.ShowDearImGuiDebugLogWindow);
        CopperImGui.MenuItem("ImGui Id Stack Tool", ref CopperImGui.ShowDearImGuiIdStackToolWindow);
    }
}