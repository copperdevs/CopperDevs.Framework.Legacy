using CopperDevs.Core.Utility;
using CopperDevs.DearImGui;
using CopperDevs.DearImGui.Attributes;
using CopperDevs.DearImGui.Enums;
using CopperDevs.DearImGui.Utility;
using CopperDevs.Framework.Components;
using CopperDevs.Framework.Scenes;
using ImGuiNET;

namespace CopperDevs.Framework.Rendering.DearImGui.Windows;

[Window("Components Manager", WindowOpen = true)]
public class ComponentsManagerWindow : BaseWindow
{
    public GameObject? CurrentObjectBrowserTarget;

    public override void WindowStart()
    {
        CopperImGui.RegisterPopup("ObjectBrowserNewGameObjectPopup", NewGameObjectPopup);
        CopperImGui.RegisterPopup("ObjectBrowserAddComponentPopup", AddComponentPopup);
    }

    public override void WindowStop()
    {
        CopperImGui.DeregisterPopup("ObjectBrowserNewGameObjectPopup");
        CopperImGui.DeregisterPopup("ObjectBrowserAddComponentPopup");
    }

    public override void WindowUpdate()
    {
        CopperImGui.TabGroup("components_manager_window_tab_group",
            ("Scene Components Browser", SceneComponentsBrowser),
            ("Abstract Children", AbstractChildTab),
            ("Components Viewer", ComponentsViewerTab));
    }

    private void SceneComponentsBrowser()
    {
        CopperImGui.HorizontalGroup(SelectorWindow, InspectorWindow);
        return;

        void SelectorWindow()
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

                    var gmName = string.IsNullOrWhiteSpace(gameObject.GameObjectName)
                        ? "Unnamed GameObject"
                        : gameObject.GameObjectName;
                    CopperImGui.Selectable($"{gmName}###{i}", gameObject == CurrentObjectBrowserTarget,
                        () => CurrentObjectBrowserTarget = gameObject);
                }
            }, 0, CopperImGui.CurrentWindowWidth * 0.25f);
        }

        void InspectorWindow()
        {
            CopperImGui.Group("object_browser_inspector_window",
                () =>
                {
                    CopperImGui.ForceRenderPopup("ObjectBrowserAddComponentPopup");
                    if (CurrentObjectBrowserTarget is null)
                        return;

                    CopperImGui.CollapsingHeader($"GameObject Settings###{CurrentObjectBrowserTarget}",
                        () => { CopperImGui.Text("Name", ref CurrentObjectBrowserTarget.GameObjectName); });

                    CopperImGui.RenderObjectValues(ref CurrentObjectBrowserTarget.Transform, 100);

                    for (var i = 0; i < CurrentObjectBrowserTarget.Components.Count; i++)
                    {
                        var component = CurrentObjectBrowserTarget.Components[i];

                        CopperImGui.CollapsingHeader($"{component.GetType().Name.ToTitleCase()}###{i}{component.GetHashCode()}",
                            () =>
                            {
                                // ReSharper disable once AccessToModifiedClosure
                                CopperImGui.Selectable($"Remove Component###{i}", () => CurrentObjectBrowserTarget.Remove(component));

                                ImGui.Unindent();
                                CopperImGui.Separator("Component Settings");
                                ImGui.Indent();

                                CopperImGui.RenderObjectValues(component, component.GetHashCode(), RenderingType.Exposed, CurrentObjectBrowserTarget.OnComponentValueChanged);
                            });
                    }

                    CopperImGui.Selectable("Add New Component",
                        () => CopperImGui.ShowPopup("ObjectBrowserAddComponentPopup"));
                },
                ChildFlags.Border);
        }
    }

    private void AbstractChildTab()
    {
        foreach (var abstractChild in ComponentRegistry.AbstractChildren)
        {
            CopperImGui.CollapsingHeader($"{abstractChild.Key.FullName}", () =>
            {
                using (new IndentScope())
                {
                    foreach (var type in abstractChild.Value)
                    {
                        CopperImGui.Text(type.FullName);
                    }
                }
            });
        }
    }

    private void ComponentsViewerTab()
    {
        foreach (var type in ComponentRegistry.ComponentTypes)
        {
            CopperImGui.CollapsingHeader($"{type.Name}", () =>
            {
                using (new IndentScope())
                {
                    CopperImGui.Text(type.Namespace!, "Namespace");
                    CopperImGui.Text(type.FullName!, "Full Name");

                    CopperImGui.Separator();

                    CopperImGui.Text(type.Assembly, "Assembly");
                    CopperImGui.Text(type.Module, "Module");

                    CopperImGui.Separator();

                    CopperImGui.Text(type.BaseType?.Name!, "Base Type Name");
                    CopperImGui.Text(type.BaseType?.FullName!, "Base Type Full Name");

                    CopperImGui.Separator();

                    CopperImGui.Text(type.IsAbstract, "Is Abstract");
                    CopperImGui.Text(type.IsImport, "Is Import");
                    CopperImGui.Text(type.IsSealed, "Is Sealed");
                    CopperImGui.Text(type.IsSpecialName, "Is Special Name");
                }
            });
        }
    }

    private static void NewGameObjectPopup()
    {
        CopperImGui.Selectable("Empty GameObject", () => { SceneManager.ActiveScene.Add([]); });

        CopperImGui.Separator();

        foreach (var componentType in ComponentRegistry.ComponentTypes.Where(component =>
                     !component.HasAttribute<HideInInspectorAttribute>()))
            CopperImGui.Selectable(componentType.Name, () => ComponentRegistry.Instantiate(componentType));
    }

    private void AddComponentPopup()
    {
        foreach (var componentType in ComponentRegistry.ComponentTypes.Where(component =>
                     !component.HasAttribute<HideInInspectorAttribute>()))
        {
            CopperImGui.Selectable(componentType.Name, () => CurrentObjectBrowserTarget?.AddComponent(componentType));
        }
    }
}