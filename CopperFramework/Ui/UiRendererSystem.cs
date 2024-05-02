using CopperDearImGui;
using CopperFramework.Elements.Systems;

namespace CopperFramework.Ui;

public class UiRendererSystem : BaseSystem<UiRendererSystem>
{
    private List<UiScreen> loadedUiScreens = new();
    private UiScreen? currentUiScreen = null!;

    private readonly List<Type> elementTypes = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(assembly => assembly.GetTypes())
        .Where(type => type.IsSubclassOf(typeof(UiElement))).ToList();

    public override SystemUpdateType GetUpdateType() => SystemUpdateType.Renderer;
    public override int GetPriority() => 1000;

    public override void Update()
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

    public override void Start()
    {
        CopperImGui.RegisterPopup("UiRenderSystemAddElementPopup", AddElementPopup);
    }

    public override void Stop()
    {
        CopperImGui.DeregisterPopup("UiRenderSystemAddElementPopup");
    }

    public override void UiUpdate()
    {
        if (currentUiScreen is null)
            return;
        
        CopperImGui.CollapsingHeader("Current Screen", () =>
        {
            for (var i = 0; i < currentUiScreen.UiElements.Count; i++)
            {
                var element = currentUiScreen.UiElements[i];
                
                CopperImGui.CollapsingHeader($"{element.Name}###{i}", () =>
                {
                    // ReSharper disable once AccessToModifiedClosure
                    CopperImGui.RenderObjectValues(ref element, i);
                });
            }
            
            CopperImGui.ForceRenderPopup("UiRenderSystemAddElementPopup");
            CopperImGui.Selectable("Add New Element", () => CopperImGui.ShowPopup("UiRenderSystemAddElementPopup"));
        });
    }

    private void AddElementPopup()
    {
        foreach (var type in elementTypes)
        {
            CopperImGui.Selectable(type.Name, () => currentUiScreen?.UiElements.Add(((UiElement)Activator.CreateInstance(type)!)!));
        }
    }
}