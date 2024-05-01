using CopperFramework.Elements.Systems;

namespace CopperFramework.Ui;

public class UiRendererSystem : BaseSystem<UiRendererSystem>
{
    private List<UiScreen> loadedUiScreens = new();
    private UiScreen? currentUiScreen = null!;

    public override SystemUpdateType GetUpdateType() => SystemUpdateType.Renderer;
    public override int GetPriority() => 1000;

    public override void UpdateSystem()
    {
        if (currentUiScreen is null)
            return;

        foreach (var uiElement in currentUiScreen.UiElements)
            uiElement.DrawElement();
    }
    
    public void RegisterUiScreen(UiScreen screen)
    {
        if (loadedUiScreens.Contains(screen))
            return;
        loadedUiScreens.Add(screen);
    }

    public void ChangeActiveScreen(UiScreen? targetScreen = null)
    {
        currentUiScreen = targetScreen;
    }
}