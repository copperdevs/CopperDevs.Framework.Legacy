using System.Reflection;
using CopperFramework.Utility;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class QuaternionFieldRenderer : ImGuiReflection.FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = ((Quaternion)(fieldInfo.GetValue(component) ?? Quaternion.Identity)).ToVector();

        CopperImGui.DragValue($"{value.GetType().Name.ToTitleCase()}##{id}", ref value,
            newValue => { fieldInfo.SetValue(component, newValue.ToQuaternion()); });
    }

    public override void ValueRenderer(ref object value, int id)
    {
        var vectorValue = ((Quaternion)value).ToVector();

        CopperImGui.DragValue($"{value.GetType().Name.ToTitleCase()}##{id}", ref vectorValue);

        value = vectorValue.ToQuaternion();
    }
}