using System.Reflection;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui;
using CopperDevs.DearImGui.ReflectionRenderers;

namespace CopperDevs.Framework.Rendering.DearImGui.ReflectionRenderers;

public class TransformFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var value = (Transform)(fieldInfo.GetValue(component) ?? 0);

        TransformRenderer($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref value, id, valueChanged);

        fieldInfo.SetValue(component, value);
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        var transformValue = (Transform)value;

        TransformRenderer($"{value.GetType().Name.ToTitleCase()}##{id}", ref transformValue, id, valueChanged);

        value = transformValue;
    }

    private static void TransformRenderer(string title, ref Transform transform, int id, Action valueChanged = null!)
    {
        var value = transform;

        CopperImGui.CollapsingHeader(title, () =>
        {
            var position = value.Position;
            var scale = value.Scale;
            var rotation = value.Rotation;

            CopperImGui.DragValue($"Position##{id}", ref position, _ =>
            {
                TransformUpdated(value);
                TransformPositionUpdated(value);
                valueChanged?.Invoke();
            });
            CopperImGui.DragValue($"Scale##{id}", ref scale, _ =>
            {
                TransformUpdated(value);
                TransformScaleUpdated(value);
                valueChanged?.Invoke();
            });
            CopperImGui.DragValue($"Rotation##{id}", ref rotation, _ =>
            {
                TransformUpdated(value);
                TransformRotationUpdated(value);
                valueChanged?.Invoke();
            });

            value.Position = position;
            value.Scale = scale;
            value.Rotation = rotation;
        });

        transform = value;
    }

    private static void TransformUpdated(Transform transform)
    {
        transform.Updated?.Invoke();
    }

    private static void TransformPositionUpdated(Transform transform)
    {
        transform.PositionUpdated?.Invoke(transform.Position);
    }

    private static void TransformRotationUpdated(Transform transform)
    {
        transform.RotationUpdated?.Invoke(transform.Rotation);
    }

    private static void TransformScaleUpdated(Transform transform)
    {
        transform.ScaleUpdated?.Invoke(transform.Scale);
    }
}