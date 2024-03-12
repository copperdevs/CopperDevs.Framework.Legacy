using System.Reflection;
using CopperFramework.Data;
using CopperPlatformer.Core.Rendering.DearImGui;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class ColorFieldRenderer: ImGuiReflection.IFieldRenderer
{
    public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (Color)(fieldInfo.GetValue(component) ?? new Color(0));
        
        CopperImGui.ColorEdit($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value, interactedValue =>
        {
            fieldInfo.SetValue(component, interactedValue);
        });
    }

    public void ValueRenderer(ref object value, int id)
    {
        var colorValue = (Color)value;
        
        CopperImGui.ColorEdit($"{value.GetType().Name}##{id}", ref colorValue);

        value = colorValue;
    }
}