using System.Reflection;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class BoolFieldRenderer : ImGuiReflection.IFieldRenderer
{
    public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (bool)(fieldInfo.GetValue(component) ?? false);

        CopperImGui.Checkbox($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value, interacted =>
        {
            fieldInfo.SetValue(component, interacted);
        });
    }

    public void ValueRenderer(ref object value, int id)
    {
        var boolValue = (bool)value;
        
        CopperImGui.Checkbox($"{value.GetType().Name}##{id}", ref boolValue);
        
        value = boolValue;
    }
}