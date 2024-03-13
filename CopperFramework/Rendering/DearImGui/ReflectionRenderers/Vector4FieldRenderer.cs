using System.Reflection;
using CopperFramework.Rendering.DearImGui.Attributes;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class Vector4FieldRenderer: ImGuiReflection.IFieldRenderer
{
    public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        ImGuiReflection.currentRangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (ImGuiReflection.currentRangeAttribute is not null)
        {
            var value = (Vector4)(fieldInfo.GetValue(component) ?? Vector4.Zero);
            
            CopperImGui.SliderValue($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value, ImGuiReflection.currentRangeAttribute.Min, ImGuiReflection.currentRangeAttribute.Max, newValue =>
            {
                fieldInfo.SetValue(component, newValue);
            });
        }
        else
        {
            var value = (Vector4)(fieldInfo.GetValue(component) ?? Vector4.Zero);
            
            CopperImGui.DragValue($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value, newValue =>
            {
                fieldInfo.SetValue(component, newValue);
            });
        }
    }

    public void ValueRenderer(ref object value, int id)
    {
        var vectorValue = (Vector4)value;
        
        CopperImGui.DragValue($"{value.GetType().Name}##{id}", ref vectorValue);
        
        value = vectorValue;
    }
}