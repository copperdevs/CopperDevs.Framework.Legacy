using CopperDearImGui.Attributes;

namespace CopperDearImGui.ReflectionRenderers;

public class FloatFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        ImGuiReflection.CurrentRangeAttribute =
            (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (ImGuiReflection.CurrentRangeAttribute is not null)
        {
            var value = (float)(fieldInfo.GetValue(component) ?? 0);

            CopperImGui.SliderValue($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
                ImGuiReflection.CurrentRangeAttribute.Min, ImGuiReflection.CurrentRangeAttribute.Max,
                interactedValue => { fieldInfo.SetValue(component, interactedValue); });
        }
        else
        {
            var value = (float)(fieldInfo.GetValue(component) ?? 0);

            CopperImGui.DragValue($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
                newValue => { fieldInfo.SetValue(component, newValue); });
        }
    }

    public override void ValueRenderer(ref object value, int id)
    {
        var floatValue = (float)value;

        CopperImGui.DragValue($"{value.GetType().Name}{id}##{id}", ref floatValue);

        value = floatValue;
    }
}