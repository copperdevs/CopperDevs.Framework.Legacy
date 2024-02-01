using System.Numerics;
using CopperFramework.Components;
using CopperFramework.Data;
using ImGuiNET;

namespace CopperFramework.Renderer.DearImGui.Windows;

public class ObjectBrowser : DearImGuiWindow
{
    public override string GetWindowName() => "Object Browser";

    internal static GameObject? CurrentObjectBrowserTarget = null!;
    

    public override void Render()
    {
        ObjectBrowserSection();

        if (CurrentObjectBrowserTarget is null)
            return;

        ImGui.SameLine();

        ImGui.BeginGroup();
        ImGui.BeginChild("object_browser_inspector_window", new Vector2(0, 0), true);

        ObjectInspectorSection();

        ImGui.EndChild();
        ImGui.EndGroup();
    }

    private void ObjectBrowserSection()
    {
        if (ImGui.BeginChild("object_browser_objects_window", new Vector2(ImGui.GetWindowWidth() * 0.25f, 0),
                true))
        {
            var list = Scene.TargetScene.ToList();
            for (var i = 0; i < list.Count; i++)
            {
                var gameObject = list[i];

                if (ImGui.Selectable($"{gameObject.DisplayName}###{i}{gameObject.GetType().FullName}", gameObject == CurrentObjectBrowserTarget))
                    CurrentObjectBrowserTarget = gameObject;
            }

            ImGui.EndChild();
        }
    }

    private void ObjectInspectorSection()
    {
        if (CurrentObjectBrowserTarget is not null)
        {
            var transform = (object)CurrentObjectBrowserTarget.Transform;
            ImGuiReflection.ImGuiRenderers[typeof(Transform)].ValueRenderer(ref transform, CurrentObjectBrowserTarget.Count()^2);
            CurrentObjectBrowserTarget.Transform = (Transform)transform;
            
            var components = CurrentObjectBrowserTarget.ToList();
            for (var i = 0; i < components.Count; i++)
            {
                var component = components[i];
                
                if (ImGui.CollapsingHeader($"{component.GetType().Name}##{i}{CurrentObjectBrowserTarget.DisplayName}"))
                {
                    using (new Indent())
                    {
                        ImGuiReflection.RenderValues(component, i);
                    }
                }
            }
        }
    }
}