using System.Reflection;
using CopperDearImGui;
using CopperDearImGui.ReflectionRenderers;
using CopperFramework.Ui;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class TextFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (Text)(fieldInfo.GetValue(component) ?? new Text());
        Renderer(ref value, id);
        fieldInfo.SetValue(component, value);
    }

    public override void ValueRenderer(ref object value, int id)
    {
        var castedValue = (Text)value;
        Renderer(ref castedValue, id);
        value = castedValue;
    }
    
    private void Renderer(ref Text text, int id)
    {
        var localText = text;
        CopperImGui.CollapsingHeader($"{text.Name} - Text###{id}", () =>
        {
            CopperImGui.Separator("Element Settings");
            CopperImGui.Text($"Name###{id}", ref localText.Name);
            CopperImGui.DragValue($"Position###{id}", ref localText.Position, 0.005f, 0, 1);
            CopperImGui.DragValue($"Size###{id}", ref localText.Size, 0.005f, 0, 1);
            
            CopperImGui.Separator("Text Settings");
            
            CopperImGui.Text($"Text Value###{id}", ref localText.TextValue);
            CopperImGui.DragValue($"Text Spacing###{id}",ref localText.TextSpacing);
            CopperImGui.DragValue($"Font Size###{id}",ref localText.FontSize);
            
            var tempColor = (Vector4)(localText.TextColor / 255);
            CopperImGui.ColorEdit($"Text Color###{id}", ref tempColor);
            localText.TextColor = new Color(tempColor * 255);
        });
        text = localText;
    }
}