using CopperDearImGui.Attributes;

namespace CopperDearImGui.ReflectionRenderers;

public class Vector3FieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        ImGuiReflection.CurrentRangeAttribute =
            (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (ImGuiReflection.CurrentRangeAttribute is not null)
        {
            var value = (Vector3)(fieldInfo.GetValue(component) ?? Vector3.Zero);

            CopperImGui.SliderValue($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
                ImGuiReflection.CurrentRangeAttribute.Min, ImGuiReflection.CurrentRangeAttribute.Max,
                newValue => { fieldInfo.SetValue(component, newValue); });
        }
        else
        {
            var value = (Vector3)(fieldInfo.GetValue(component) ?? Vector3.Zero);

            CopperImGui.DragValue($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
                newValue => { fieldInfo.SetValue(component, newValue); });
        }
    }

    public override void ValueRenderer(ref object value, int id)
    {
        var vectorValue = (Vector3)value;

        CopperImGui.DragValue($"{value.GetType().Name}##{id}", ref vectorValue);

        value = vectorValue;
    }
}