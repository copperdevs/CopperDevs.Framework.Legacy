using CopperDearImGui;
using CopperDearImGui.Utility;
using CopperFramework.Ui;
using ImGuiNET;

namespace CopperFramework.UiCreator;

public class UICManagementWindow : BaseWindow
{
    public override string WindowName { get; protected set; } = "UIC Management";
    private UiElement? currentTargetElement;

    public override void Start()
    {
        CopperImGui.RegisterPopup("AddElementPopup", AddElementPopup);
    }

    public override void Stop()
    {
        CopperImGui.DeregisterPopup("AddElementPopup");
    }

    public override void Update()
    {
        ref var currentScreen = ref UICManagementSystem.Instance.UiScreen;

        CopperImGui.Text("Screen Display Name", ref currentScreen.DisplayName);
        CopperImGui.Text("Screen Id", ref currentScreen.ScreenId);
        
        CopperImGui.Separator();

        CopperImGui.HorizontalGroup(SelectorWindow, InspectorWindow);
    }

    private void SelectorWindow()
    {
        CopperImGui.Group("object_browser_objects_window", () =>
        {
            CopperImGui.ForceRenderPopup("AddElementPopup");

            CopperImGui.Selectable("Add Element", () => CopperImGui.ShowPopup("AddElementPopup"));

            CopperImGui.Selectable("Delete Element", () =>
            {
                if (currentTargetElement is null)
                    return;
                currentTargetElement = null;
            });

            CopperImGui.Separator();

            var list = UICManagementSystem.Instance.UiScreen.UiElements;
            for (var i = 0; i < list.Count; i++)
            {
                var element = list[i];

                var elementName = string.IsNullOrWhiteSpace(element.Name) ? "Unnamed Element" : element.Name;
                CopperImGui.Selectable($"{elementName}###{i}", element == currentTargetElement,
                    () => currentTargetElement = element);
            }
        }, 0, CopperImGui.CurrentWindowWidth * 0.25f);
    }

    private void InspectorWindow()
    {
        CopperImGui.Group("object_browser_inspector_window",
            () =>
            {
                if (currentTargetElement is null)
                    return;

                CopperImGui.RenderValues(currentTargetElement);
            },
            ImGuiChildFlags.Border);
    }

    private void AddElementPopup()
    {
        foreach (var elementType in UICManagementSystem.ElementTypes.Where(elementType =>
                     elementType != typeof(UiElement)))
        {
            CopperImGui.Selectable(elementType.Name,
                () => UICManagementSystem.Instance.UiScreen.UiElements.Add(
                    ((UiElement)Activator.CreateInstance(elementType)!)!));
        }
    }
}