using CopperDearImGui;
using CopperFramework.Elements.Systems;
using CopperFramework.Ui;

namespace CopperFramework.UiCreator;

public class UICImGuiSystem : BaseSystem<UICImGuiSystem>
{
    public override SystemUpdateType GetUpdateType() => SystemUpdateType.Renderer;

    public override void LoadSystem()
    {
        CopperImGui.PreRendered += RenderDearImGui;
    }
    public override void ShutdownSystem()
    {
        CopperImGui.PreRendered += RenderDearImGui;
    }

    public override void UpdateSystem()
    {
    }
    
    private void RenderDearImGui()
    {
        CopperImGui.MenuBar(null!, true,
            ("Menu", () =>
            {
                CopperImGui.MenuItem("New", () => UICManagementSystem.Instance.UiScreen = new UiScreen());
                CopperImGui.MenuItem("Open");
                CopperImGui.MenuItem("Save", () =>
                {
                    Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/Screens/");
                    var json = UICManagementSystem.Instance.UiScreen.ToJson();
                    File.WriteAllText($"{Directory.GetCurrentDirectory()}/Screens/{UICManagementSystem.Instance.UiScreen.ScreenId}.json", json);
                });
                CopperImGui.Separator();
                CopperImGui.MenuItem("Quit", () => Engine.Instance.ShouldRun = false);
            }));
    }
}