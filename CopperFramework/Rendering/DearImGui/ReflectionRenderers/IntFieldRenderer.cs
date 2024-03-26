using System.Reflection;
using CopperFramework.Rendering.DearImGui.Attributes;
using CopperFramework.Utility;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class IntFieldRenderer : ImGuiReflection.FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        ImGuiReflection.CurrentRangeAttribute =
            (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (ImGuiReflection.CurrentRangeAttribute is not null)
        {
            var value = (int)(fieldInfo.GetValue(component) ?? 0);

            CopperImGui.SliderValue($"{value.GetType().Name.ToTitleCase()}##{id}", ref value,
                (int)ImGuiReflection.CurrentRangeAttribute.Min, (int)ImGuiReflection.CurrentRangeAttribute.Max,
                newValue => { fieldInfo.SetValue(component, newValue); });
        }
        else
        {
            var value = (int)(fieldInfo.GetValue(component) ?? 0);

            CopperImGui.DragValue($"{value.GetType().Name.ToTitleCase()}##{id}", ref value,
                newValue => { fieldInfo.SetValue(component, newValue); });
        }
    }

    public override void ValueRenderer(ref object value, int id)
    {
        var intValue = (int)value;

        CopperImGui.DragValue($"{value.GetType().Name}##{id}", ref intValue);

        value = intValue;
    }
}