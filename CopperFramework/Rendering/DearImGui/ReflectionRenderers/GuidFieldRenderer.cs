using System.Reflection;
using CopperFramework.Utility;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class GuidFieldRenderer : ImGuiReflection.FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (Guid)(fieldInfo.GetValue(component) ?? new Guid());

        CopperImGui.Text(value, $"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}");
    }

    public override void ValueRenderer(ref object value, int id)
    {
        CopperImGui.Text(value, $"{value.GetType().Name.ToTitleCase()}##{id}");
    }
}