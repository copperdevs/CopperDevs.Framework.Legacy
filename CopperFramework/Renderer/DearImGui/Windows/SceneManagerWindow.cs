using CopperFramework.Data;
using ImGuiNET;

namespace CopperFramework.Renderer.DearImGui.Windows;

public class SceneManagerWindow : DearImGuiWindow
{
    public override string GetWindowName() => "Scene Manager";

    public override void Render()
    {
        foreach (var scene in Scene.Scenes.Values)
        {
            if(ImGui.Selectable(scene.SceneDisplayName, scene == Scene.TargetScene))
                Scene.LoadScene(scene);
        }
    }
}