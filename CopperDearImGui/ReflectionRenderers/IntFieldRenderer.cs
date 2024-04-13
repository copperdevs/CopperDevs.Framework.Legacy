using CopperDearImGui.Attributes;

namespace CopperDearImGui.ReflectionRenderers;

public class IntFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var rangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (rangeAttribute is not null)
        {
            var value = (int)(fieldInfo.GetValue(component) ?? 0);

            CopperImGui.SliderValue($"{value.GetType().Name.ToTitleCase()}##{id}", ref value,
                (int)rangeAttribute.Min, (int)rangeAttribute.Max,
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