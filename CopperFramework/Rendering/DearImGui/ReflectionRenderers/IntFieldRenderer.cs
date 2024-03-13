using System.Reflection;
using CopperFramework.Rendering.DearImGui.Attributes;
using CopperPlatformer.Core.Rendering.DearImGui;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class IntFieldRenderer: ImGuiReflection.IFieldRenderer
{
    public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        ImGuiReflection.currentRangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (ImGuiReflection.currentRangeAttribute is not null)
        {
            var value = (int)(fieldInfo.GetValue(component) ?? 0);
            
            CopperImGui.SliderValue($"{value.GetType().Name}##{id}", ref value, (int)ImGuiReflection.currentRangeAttribute.Min, (int)ImGuiReflection.currentRangeAttribute.Max, newValue =>
            {
                fieldInfo.SetValue(component, newValue);
            });
        }
        else
        {
            var value = (int)(fieldInfo.GetValue(component) ?? 0);
            
            CopperImGui.DragValue($"{value.GetType().Name}##{id}", ref value, newValue =>
            {
                fieldInfo.SetValue(component, newValue);
            });
        }
    }

    public void ValueRenderer(ref object value, int id)
    {
        var intValue = (int)value;

        CopperImGui.DragValue($"{value.GetType().Name}##{id}", ref intValue);
        
        value = intValue;
    }
}