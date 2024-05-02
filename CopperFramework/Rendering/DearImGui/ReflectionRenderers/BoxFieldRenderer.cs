using System.Reflection;
using CopperDearImGui;
using CopperDearImGui.ReflectionRenderers;
using CopperFramework.Ui;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class BoxFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (Box)(fieldInfo.GetValue(component) ?? new Box());
        Renderer(ref value, id);
        fieldInfo.SetValue(component, value);
    }

    public override void ValueRenderer(ref object value, int id)
    {
        var castedValue = (Box)value;
        Renderer(ref castedValue, id);
        value = castedValue;
    }

    private void Renderer(ref Box box, int id)
    {
        var localBox = box;
        CopperImGui.CollapsingHeader($"{box.Name} - Button###{id}", () =>
        {
            CopperImGui.Separator("Element Settings");
            CopperImGui.Text($"Name###{id}", ref localBox.Name);
            CopperImGui.DragValue($"Position###{id}", ref localBox.Position, 0.005f, 0, 1);
            CopperImGui.DragValue($"Size###{id}", ref localBox.Size, 0.005f, 0, 1);
            
            CopperImGui.Separator("Button Settings");
            var tempColor = (Vector4)(localBox.Color / 255);
            CopperImGui.ColorEdit($"Color###{id}", ref tempColor);
            localBox.Color = new Color(tempColor * 255);
        });
        box = localBox;
    }
}