using CopperDearImGui;
using CopperFramework.Scenes;

namespace CopperFramework.Rendering.DearImGui.Windows;

public class SceneWindow : BaseWindow
{
    public override string WindowName { get; protected set; } = "Scene Manager";

    private bool shouldClone = true;
    
    public override void Update()
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
            CopperImGui.Selectable(scene.DisplayName == scene.Id.ToString() ? scene.Id.ToString() : $"{scene.DisplayName} ({scene.Id})", scene.Id == SceneManager.ActiveScene.Id, () =>
            {
                SceneManager.LoadScene(scene, shouldClone);
                ComponentsManagerWindow.CurrentObjectBrowserTarget = null;
            });
        }
    }
}