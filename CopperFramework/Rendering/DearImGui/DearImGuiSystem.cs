using CopperFramework.Elements.Systems;
using CopperFramework.Utility;
using ImGuiNET;

namespace CopperFramework.Rendering.DearImGui;

public class DearImGuiSystem : SystemSingleton<DearImGuiSystem>, ISystem
{
    private static readonly List<BaseWindow> Windows = new();

    public SystemUpdateType GetUpdateType() => SystemUpdateType.UiRenderer;

    public int GetPriority() => 100;

    private bool showImGuiDemo;
    private bool showIdStackTool;
    private bool showImGuiLog;

    internal void RenderImGuiWindowsMenu()
    {
        if (!DebugSystem.Instance.DebugEnabled)
            return;

        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("Windows"))
            {
                ImGui.MenuItem("ImGui Demo", null, ref showImGuiDemo);
                ImGui.MenuItem("ImGui Id Stack Tool", null, ref showIdStackTool);
                ImGui.MenuItem("ImGui Debug Log", null, ref showImGuiLog);
                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();
        }

        if (showImGuiDemo)
            ImGui.ShowDemoWindow(ref showImGuiDemo);

        if (showIdStackTool)
            ImGui.ShowIDStackToolWindow(ref showIdStackTool);
        
        if(showImGuiLog)
            ImGui.ShowDebugLogWindow(ref showImGuiLog);
    }

    public void UpdateSystem()
    {
        if (DebugSystem.Instance.DebugEnabled)
            CopperImGui.Render();
    }

    public void LoadSystem()
    {
        CopperImGui.Setup<CopperRlImGui>();
    }

    public void ShutdownSystem()
    {
        CopperImGui.Shutdown();
    }


    private class CopperRlImGui : CopperImGui.IImGuiRenderer
    {
        public void Setup()
        {
            rlImGui.Setup(true, true);
        }

        public void Begin()
        {
            rlImGui.Begin();
        }

        public void End()
        {
            rlImGui.End();
        }

        public void Shutdown()
        {
            rlImGui.Shutdown();
        }
    }
}