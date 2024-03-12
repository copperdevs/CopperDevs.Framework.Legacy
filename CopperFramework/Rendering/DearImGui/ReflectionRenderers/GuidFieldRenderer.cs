using System.Reflection;
using CopperPlatformer.Core.Rendering.DearImGui;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class GuidFieldRenderer: ImGuiReflection.IFieldRenderer
{
    public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (Guid)(fieldInfo.GetValue(component) ?? new Guid());
        
        CopperImGui.Text(value, $"{fieldInfo.Name}##{fieldInfo.Name}{id}");
    }

    public void ValueRenderer(ref object value, int id)
    {
        CopperImGui.Text(value, $"{value.GetType().Name}##{id}");
    }
}