using CopperDevs.DearImGui;
using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Scenes;

namespace CopperDevs.Framework.Rendering.DearImGui.Windows;

[Window("Scene Manager", WindowOpen = true)]
public class SceneManagerWindow : BaseWindow
{
    private bool shouldClone = true;

    public override void WindowUpdate()
    {
        var scenes = SceneManager.GetAllScenes();

        CopperImGui.Checkbox("Should clone on load", ref shouldClone);

        CopperImGui.Separator();

        CopperImGui.Selectable("Add Scene", () =>
        {
            var scene = new Scene();
        });

        CopperImGui.Separator();

        foreach (var scene in scenes)
        {
            CopperImGui.Selectable(
                scene.DisplayName == scene.Id.ToString() ? scene.Id.ToString() : $"{scene.DisplayName} ({scene.Id})",
                scene.Id == SceneManager.ActiveScene.Id, () =>
                {
                    SceneManager.LoadScene(scene, shouldClone);
                    CopperImGui.GetWindow<ComponentsManagerWindow>()!.CurrentObjectBrowserTarget = null;
                });
        }
    }
}