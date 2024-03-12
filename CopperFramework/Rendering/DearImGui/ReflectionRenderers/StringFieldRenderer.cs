using System.Reflection;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class StringFieldRenderer : ImGuiReflection.IFieldRenderer
{
    public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (string)(fieldInfo.GetValue(component) ?? false);


        CopperImGui.Text($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value, newValue => { fieldInfo.SetValue(component, newValue); });
    }

    public void ValueRenderer(ref object value, int id)
    {
        var stringValue = (string)value;

        CopperImGui.Text($"{value.GetType().Name}##{id}", ref stringValue);

        value = stringValue;
    }
}