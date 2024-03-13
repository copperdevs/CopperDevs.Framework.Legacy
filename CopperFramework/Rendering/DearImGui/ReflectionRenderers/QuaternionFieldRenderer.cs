using System.Reflection;
using CopperPlatformer.Core.Rendering.DearImGui;
using CopperPlatformer.Core.Utility;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class QuaternionFieldRenderer: ImGuiReflection.IFieldRenderer
{
    public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = ((Quaternion)(fieldInfo.GetValue(component) ?? Quaternion.Identity)).ToVector();
        
        CopperImGui.DragValue($"{value.GetType().Name}##{id}", ref value, newValue =>
        {
            fieldInfo.SetValue(component, newValue.ToQuaternion());
        });
    }

    public void ValueRenderer(ref object value, int id)
    {
        var vectorValue = ((Quaternion)value).ToVector();
        
        CopperImGui.DragValue($"{value.GetType().Name}##{id}", ref vectorValue);
        
        value = vectorValue.ToQuaternion();
    }
}