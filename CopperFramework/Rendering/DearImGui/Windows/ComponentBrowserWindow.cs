using CopperFramework.Elements.Components;
using CopperPlatformer.Core.Scenes;
using CopperPlatformer.Core.Utility;
using ImGuiNET;

namespace CopperFramework.Rendering.DearImGui.Windows;

public class ComponentBrowserWindow : BaseWindow
{
    public override string WindowName { get; protected internal set; } = "Object Browser";

    internal static GameComponent? CurrentObjectBrowserTarget = null!;
    private List<Type> components = new();

    public override void Start()
    {
        components = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(GameComponent))).ToList();
    }

    public override void Update()
    {
        CopperImGui.HorizontalGroup(SelectorWindow, InspectorWindow);
    }

    private void SelectorWindow()
    {
        CopperImGui.Group("object_browser_objects_window", () =>
        {
            CopperImGui.Selectable("Add Component", () => { ImGui.OpenPopup("ObjectBrowserNewComponentPopup"); });
            
            NewComponentPopup();

            CopperImGui.Selectable("Delete Component", () =>
            {
                if (CurrentObjectBrowserTarget is null)
                    return;
                SceneManager.ActiveScene.Remove(CurrentObjectBrowserTarget);
                CurrentObjectBrowserTarget = null;
            });

            CopperImGui.Separator();

            var list = ComponentRegistry.CurrentComponents.ToList();
            for (var i = 0; i < list.Count; i++)
            {
                var gameObject = list[i];
                
                CopperImGui.Selectable($"{gameObject.GetType().Name}###{i}", gameObject == CurrentObjectBrowserTarget,
                    () => { CurrentObjectBrowserTarget = gameObject; });
            }
        }, 0, ImGui.GetWindowWidth() * 0.25f);
    }

    private void InspectorWindow()
    {
        CopperImGui.Group("object_browser_inspector_window",
            () =>
            {
                if (CurrentObjectBrowserTarget is null) 
                    return;

                var transformValue = (object)CurrentObjectBrowserTarget.Transform;
                ImGuiReflection.GetImGuiRenderer<Transform>()?.ValueRenderer(ref transformValue, 100);
                CurrentObjectBrowserTarget.Transform = (Transform)transformValue;
                
                ImGuiReflection.RenderValues(CurrentObjectBrowserTarget);
            },
            ImGuiChildFlags.Border);
    }

    private void NewComponentPopup()
    {
        if (ImGui.BeginPopup("ObjectBrowserNewComponentPopup"))
        {
            foreach (var component in components)
            {
                if (component == typeof(SingletonGameComponent<>))
                    continue;

                var canAddSingleton = component.BaseType!.IsAssignableTo(typeof(ISingleton)) &&
                                      ComponentRegistry.CurrentComponents.Any(registryComponent =>
                                          registryComponent.GetType() == component);

                if (canAddSingleton) ImGui.BeginDisabled();

                if (ImGui.Selectable(component.Name))
                {
                    SceneManager.ActiveScene.Add(Activator.CreateInstance(component) as GameComponent ?? null!);
                }

                if (canAddSingleton) ImGui.EndDisabled();
            }

            ImGui.EndPopup();
        }
    }
}