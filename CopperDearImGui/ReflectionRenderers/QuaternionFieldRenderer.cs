namespace CopperDearImGui.ReflectionRenderers;

public class QuaternionFieldRenderer : FieldRenderer
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