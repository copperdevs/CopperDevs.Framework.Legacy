using CopperCore;
using CopperFramework.Elements.Components;
using CopperFramework.Physics;
using CopperFramework.Rendering.DearImGui.Attributes;
using CopperFramework.Scenes;
using CopperFramework.Utility;
using ImGuiNET;

namespace CopperFramework.Rendering.DearImGui.Windows;

public class ComponentBrowserWindow : BaseWindow
{
    public override string WindowName { get; protected internal set; } = "Object Browser";

    internal static GameObject? CurrentObjectBrowserTarget = null!;
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
            CopperImGui.Selectable("Add Component", () => { ImGui.OpenPopup("ObjectBrowserNewGameObjectPopup"); });

            NewGameObjectPopup();

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
                CopperImGui.Selectable($"{gmName}###{i}", gameObject == CurrentObjectBrowserTarget,
                    () => { CurrentObjectBrowserTarget = gameObject; });
            }
        }, 0, ImGui.GetWindowWidth() * 0.25f);
    }

    private void InspectorWindow()
    {
        CopperImGui.Group("object_browser_inspector_window",
            () =>
            {
                AddComponentPopup();
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
                ImGuiReflection.GetImGuiRenderer<Transform>()?.ValueRenderer(ref transformValue, 100);
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

                            ImGuiReflection.RenderValues(component);
                        }
                    });
                }

                CopperImGui.Selectable("Add New Component", () => ImGui.OpenPopup("ObjectBrowserAddComponentPopup"));
            },
            ImGuiChildFlags.Border);
    }

    private void NewGameObjectPopup()
    {
        if (!ImGui.BeginPopup("ObjectBrowserNewGameObjectPopup"))
            return;

        CopperImGui.Selectable("Empty GameObject", () => { SceneManager.ActiveScene.Add(new GameObject()); });

        CopperImGui.Separator();


        foreach (var component in components.Where(component => !component.HasAttribute<HideInInspectorAttribute>()))
        {
            CopperImGui.Selectable(component.Name, () => SceneManager.ActiveScene.Add(new GameObject { Activator.CreateInstance(component) as GameComponent ?? null! }));
        }

        ImGui.EndPopup();
    }

    private void AddComponentPopup()
    {
        if (!ImGui.BeginPopup("ObjectBrowserAddComponentPopup"))
            return;

        foreach (var component in components.Where(component => !component.HasAttribute<HideInInspectorAttribute>()))
        {
            CopperImGui.Selectable(component.Name, () => CurrentObjectBrowserTarget?.Add(Activator.CreateInstance(component) as GameComponent ?? null!));
        }

        ImGui.EndPopup();
    }
}