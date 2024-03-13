using System.Reflection;
using CopperFramework.Rendering.DearImGui.Attributes;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class Vector2FieldRenderer : ImGuiReflection.IFieldRenderer
{
    public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        ImGuiReflection.currentRangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (ImGuiReflection.currentRangeAttribute is not null)
        {
            var value = (Vector2)(fieldInfo.GetValue(component) ?? Vector2.Zero);
            
            CopperImGui.SliderValue($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value, ImGuiReflection.currentRangeAttribute.Min, ImGuiReflection.currentRangeAttribute.Max, newValue =>
            {
                fieldInfo.SetValue(component, newValue);
            });
        }
        else
        {
            var value = (Vector2)(fieldInfo.GetValue(component) ?? Vector2.Zero);
            
            CopperImGui.DragValue($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value, newValue =>
            {
                fieldInfo.SetValue(component, newValue);
            });
        }
    }

    public void ValueRenderer(ref object value, int id)
    {
        var vectorValue = (Vector2)value;
        
        CopperImGui.DragValue($"{value.GetType().Name}##{id}", ref vectorValue);
        
        value = vectorValue;
    }
}