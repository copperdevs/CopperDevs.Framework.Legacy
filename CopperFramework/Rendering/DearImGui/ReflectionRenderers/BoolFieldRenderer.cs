using System.Reflection;
using CopperFramework.Utility;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class BoolFieldRenderer : ImGuiReflection.FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (bool)(fieldInfo.GetValue(component) ?? false);

        CopperImGui.Checkbox($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value, interacted =>
        {
            fieldInfo.SetValue(component, interacted);
        });
    }

    public override void ValueRenderer(ref object value, int id)
    {
        var boolValue = (bool)value;
        
        CopperImGui.Checkbox($"{value.GetType().Name.ToTitleCase()}##{id}", ref boolValue);
        
        value = boolValue;
    }
}