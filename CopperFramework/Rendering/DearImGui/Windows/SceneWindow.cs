using CopperDearImGui;
using CopperFramework.Scenes;

namespace CopperFramework.Rendering.DearImGui.Windows;

public class SceneWindow : BaseWindow
{
    public override string WindowName { get; protected set; } = "Scene Manager";

    public override void Update()
    {
        var scenes = SceneManager.GetAllScenes();
        
        CopperImGui.Selectable("Add Scene", () =>
        {
            var scene = new Scene();
        });
        
        CopperImGui.Separator();

        foreach (var scene in scenes)
        {
            CopperImGui.Selectable(scene.Id.ToString(), scene.Id == SceneManager.ActiveScene.Id, () =>
            {
                SceneManager.LoadScene(scene);
                ComponentBrowserWindow.CurrentObjectBrowserTarget = null;
            });
        }
    }
}