using CopperDearImGui;
using CopperDearImGui.Attributes;
using CopperDearImGui.Utility;
using CopperFramework.Elements.Components;
using CopperFramework.Scenes;
using CopperFramework.Utility;
using ImGuiNET;

namespace CopperFramework.Rendering.DearImGui.Windows;

public class ComponentBrowserWindow : BaseWindow
{
    public override string WindowName { get; protected set; } = "Object Browser";

    internal static GameObject? CurrentObjectBrowserTarget = null!;

    public override void Start()
    {
        CopperImGui.RegisterPopup("ObjectBrowserNewGameObjectPopup", NewGameObjectPopup);
        CopperImGui.RegisterPopup("ObjectBrowserAddComponentPopup", AddComponentPopup);
    }

    public override void Stop()
    {
        CopperImGui.DeregisterPopup("ObjectBrowserNewGameObjectPopup");
        CopperImGui.DeregisterPopup("ObjectBrowserAddComponentPopup");
    }

    public override void Update()
    {
        CopperImGui.HorizontalGroup(SelectorWindow, InspectorWindow);
    }

    private static void SelectorWindow()
    {
        CopperImGui.Group("object_browser_objects_window", () =>
        {
            CopperImGui.ForceRenderPopup("ObjectBrowserNewGameObjectPopup");
            
            CopperImGui.Selectable("Add Component", () => CopperImGui.ShowPopup("ObjectBrowserNewGameObjectPopup"));

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

                var gmName = string.IsNullOrWhiteSpace(gameObject.GameObjectName) ? "Unnamed GameObject" : gameObject.GameObjectName;
                CopperImGui.Selectable($"{gmName}###{i}", gameObject == CurrentObjectBrowserTarget, () => CurrentObjectBrowserTarget = gameObject);
            }
        }, 0, CopperImGui.CurrentWindowWidth * 0.25f);
    }

    private static void InspectorWindow()
    {
        CopperImGui.Group("object_browser_inspector_window",
            () =>
            {
                CopperImGui.ForceRenderPopup("ObjectBrowserAddComponentPopup");
                if (CurrentObjectBrowserTarget is null)
                    return;

                CopperImGui.CollapsingHeader($"GameObject Settings###{CurrentObjectBrowserTarget}", () =>
                {
                    using (new IndentScope())
                    {
                        CopperImGui.Text("Name", ref CurrentObjectBrowserTarget.GameObjectName);
                    }
                });

                var transformValue = (object)CurrentObjectBrowserTarget.Transform;
                CopperImGui.GetFieldRenderer<Transform>()?.ValueRenderer(ref transformValue, 100);
                CurrentObjectBrowserTarget.Transform = (Transform)transformValue;

                for (var i = 0; i < CurrentObjectBrowserTarget.Components.Count; i++)
                {
                    var component = CurrentObjectBrowserTarget.Components[i];

                    CopperImGui.CollapsingHeader($"{component.GetType().FullName}###{i}{component.GetHashCode()}", () =>
                    {
                        using (new IndentScope())
                        {
                            // ReSharper disable once AccessToModifiedClosure
                            CopperImGui.Selectable($"Remove Component###{i}", () => CurrentObjectBrowserTarget.Remove(component));
                            CopperImGui.Separator("Component Settings");

                            CopperImGui.RenderValues(component, component.GetHashCode());
                        }
                    });
                }

                CopperImGui.Selectable("Add New Component", () => CopperImGui.ShowPopup("ObjectBrowserAddComponentPopup"));
            },
            ImGuiChildFlags.Border);
    }

    private static void NewGameObjectPopup()
    {
        CopperImGui.Selectable("Empty GameObject", () => { SceneManager.ActiveScene.Add(new GameObject()); });

        CopperImGui.Separator();

        foreach (var componentType in ComponentRegistry.ComponentTypes.Where(component => !component.HasAttribute<HideInInspectorAttribute>()))
            CopperImGui.Selectable(componentType.Name, () => ComponentRegistry.Instantiate(componentType));
    }

    private static void AddComponentPopup()
    {
        foreach (var componentType in ComponentRegistry.ComponentTypes.Where(component => !component.HasAttribute<HideInInspectorAttribute>()))
        {
            CopperImGui.Selectable(componentType.Name, () => CurrentObjectBrowserTarget?.AddComponent(componentType));
        }
    }
}