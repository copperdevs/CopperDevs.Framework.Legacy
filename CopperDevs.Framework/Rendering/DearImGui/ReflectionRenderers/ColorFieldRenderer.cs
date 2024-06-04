using System.Reflection;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui;
using CopperDevs.DearImGui.ReflectionRenderers;

namespace CopperDevs.Framework.Rendering.DearImGui.ReflectionRenderers;

public class ColorFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (Color)(fieldInfo.GetValue(component) ?? new Color(0));
        var vectorColor = (Vector4)(value / 255);

        CopperImGui.ColorEdit($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref vectorColor,
            interactedValue => { fieldInfo.SetValue(component, new Color(interactedValue * 255)); });
    }

    public override void ValueRenderer(ref object value, int id)
    {
        var colorValue = (Vector4)((Color)value / 255);

        CopperImGui.ColorEdit($"{value.GetType().Name.ToTitleCase()}##{id}", ref colorValue);

        value = colorValue * 255;
    }
}