using System.Reflection;
using CopperDearImGui;
using CopperDearImGui.ReflectionRenderers;
using CopperFramework.Ui;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class ButtonFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (Button)(fieldInfo.GetValue(component) ?? new Button());
        Renderer(ref value, id);
        fieldInfo.SetValue(component, value);
    }

    public override void ValueRenderer(ref object value, int id)
    {
        var castedValue = (Button)value;
        Renderer(ref castedValue, id);
        value = castedValue;
    }

    private void Renderer(ref Button button, int id)
    {
        var localButton = button;
        CopperImGui.CollapsingHeader($"{button.Name} - Button###{id}", () =>
        {
            CopperImGui.Separator("Element Settings");
            CopperImGui.Text($"Name###{id}", ref localButton.Name);
            CopperImGui.DragValue($"Position###{id}", ref localButton.Position, 0.005f, 0, 1);
            CopperImGui.DragValue($"Size###{id}", ref localButton.Size, 0.005f, 0, 1);
            
            CopperImGui.Separator("Button Settings");
            var tempColor = (Vector4)(localButton.BackgroundColor / 255);
            CopperImGui.ColorEdit($"Background Color###{id}", ref tempColor);
            localButton.BackgroundColor = new Color(tempColor * 255);
        });
        button = localButton;
    }
}