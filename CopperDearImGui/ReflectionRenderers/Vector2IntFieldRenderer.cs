using CopperDearImGui.Attributes;

namespace CopperDearImGui.ReflectionRenderers;

public class Vector2IntFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        ImGuiReflection.CurrentRangeAttribute =
            (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (ImGuiReflection.CurrentRangeAttribute is not null)
        {
            var value = (Vector2Int)(fieldInfo.GetValue(component) ?? Vector2Int.Zero);

            CopperImGui.SliderValue($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
                (int)ImGuiReflection.CurrentRangeAttribute.Min, (int)ImGuiReflection.CurrentRangeAttribute.Max,
                newValue => { fieldInfo.SetValue(component, newValue); });
        }
        else
        {
            var value = (Vector2Int)(fieldInfo.GetValue(component) ?? Vector2Int.Zero);

            CopperImGui.DragValue($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
                newValue => { fieldInfo.SetValue(component, newValue); });
        }
    }

    public override void ValueRenderer(ref object value, int id)
    {
        var vectorValue = (Vector2Int)value;

        CopperImGui.DragValue($"{value.GetType().Name.ToTitleCase()}##{id}", ref vectorValue);

        value = vectorValue;
    }
}