using System.Reflection;
using CopperFramework.Utility;
using ImGuiNET;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class EnumFieldRenderer : ImGuiReflection.FieldRenderer
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

        CopperImGui.HorizontalGroup(() =>
        {
            CopperImGui.Text(title);
        }, () =>
        {
            CopperImGui.Button($"{value}###{Enum.GetUnderlyingType(type)}{type}{id}", () =>
            {
                renderEnumPopup = true;
                ImGui.OpenPopup("enum_field_renderer_select_popup");
                currentEnumValues = enumValues;
            });
        });

        var componentValue = component;
        
        if (renderEnumPopup)
        {
            ImGui.BeginPopup("enum_field_renderer_select_popup");

            foreach (var enumValue in currentEnumValues)
            {
                CopperImGui.Selectable($"{enumValue}", () =>
                {
                    renderEnumPopup = false;
                    componentValue = enumValue;
                });
            }
            
            ImGui.EndPopup();
        }

        component = componentValue;
    }

    private Array currentEnumValues = null!;
    private bool renderEnumPopup;
}