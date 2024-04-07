using System.Reflection;
using CopperFramework.Utility;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class ColorFieldRenderer : ImGuiReflection.FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (Color)(fieldInfo.GetValue(component) ?? new Color(0));

        CopperImGui.ColorEdit($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
            interactedValue => { fieldInfo.SetValue(component, interactedValue); });
    }

    public override void ValueRenderer(ref object value, int id)
    {
        var colorValue = (Color)value;

        CopperImGui.ColorEdit($"{value.GetType().Name.ToTitleCase()}##{id}", ref colorValue);

        value = colorValue;
    }
}