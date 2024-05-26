using CopperDevs.Core.Utility;

namespace CopperDevs.DearImGui.ReflectionRenderers;

public class StringFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (string)(fieldInfo.GetValue(component) ?? false);


        CopperImGui.Text($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value,
            newValue => { fieldInfo.SetValue(component, newValue); });
    }

    public override void ValueRenderer(ref object value, int id)
    {
        var stringValue = (string)value;

        CopperImGui.Text($"{value.GetType().Name.ToTitleCase()}##{id}", ref stringValue);

        value = stringValue;
    }
}