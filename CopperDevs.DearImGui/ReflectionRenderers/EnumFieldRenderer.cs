// BUG: Fix Lists of enums
// BUG: Multiple enums breaks 

using CopperDevs.Core;
using CopperDevs.Core.Data;
using CopperDevs.Core.Utility;

namespace CopperDevs.DearImGui.ReflectionRenderers;

public class EnumFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var enumValue = fieldInfo.GetValue(component)!;

        RenderEnum(fieldInfo.FieldType, ref enumValue, id, fieldInfo.Name.ToTitleCase());

        fieldInfo.SetValue(component, enumValue);
    }

    public override void ValueRenderer(ref object value, int id)
    {
        RenderEnum(value.GetType(), ref value, id, value.GetType().Name.ToTitleCase());
    }

    private void RenderEnum(Type type, ref object component, int id, string title)
    {
        var enumValues = Enum.GetValues(type);
        var value = enumValues.GetValue((int)Convert.ChangeType(component, Enum.GetUnderlyingType(type)))!;

        var enumRange = new Vector2Int(0, enumValues.Length);

        var currentValueIndex = 0;
        
        for (var i = 0; i < enumValues.Length; i++)
        {
            var enumValue = enumValues.GetValue(i);

            if (enumValue?.GetHashCode() == value.GetHashCode())
                currentValueIndex = i;
        }
        
        CopperImGui.HorizontalGroup(() =>
        {
            CopperImGui.Text(title);
        }, () =>
        {
            CopperImGui.Button($"-###{Enum.GetUnderlyingType(type)}{type}{id}", () =>
            {
                Log.Info($"{currentValueIndex - 1} {MathUtil.Clamp(currentValueIndex - 1, enumRange)}");
            });
        }, () =>
        {
            CopperImGui.Button($"{value}###{Enum.GetUnderlyingType(type)}{type}{id}");
        }, () =>
        {
            CopperImGui.Button($"+###{Enum.GetUnderlyingType(type)}{type}{id}", () =>
            {
                Log.Info($"{currentValueIndex + 1} {MathUtil.Clamp(currentValueIndex + 1, enumRange)}");
            });
        });
    }
}