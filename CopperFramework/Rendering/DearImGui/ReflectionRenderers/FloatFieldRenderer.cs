using System.Reflection;
using CopperFramework.Rendering.DearImGui.Attributes;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class FloatFieldRenderer : ImGuiReflection.IFieldRenderer
{
    public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        ImGuiReflection.currentRangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (ImGuiReflection.currentRangeAttribute is not null)
        {
            var value = (float)(fieldInfo.GetValue(component) ?? 0);

            CopperImGui.SliderValue($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value, ImGuiReflection.currentRangeAttribute.Min, ImGuiReflection.currentRangeAttribute.Max,
                interactedValue => { fieldInfo.SetValue(component, interactedValue); });
        }
        else
        {
            var value = (float)(fieldInfo.GetValue(component) ?? 0);

            CopperImGui.DragValue($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value, newValue => { fieldInfo.SetValue(component, newValue); });
        }
    }

    public void ValueRenderer(ref object value, int id)
    {
        var floatValue = (float)value;

        CopperImGui.DragValue($"{value.GetType().Name}{id}##{id}", ref floatValue);

        value = floatValue;
    }
}