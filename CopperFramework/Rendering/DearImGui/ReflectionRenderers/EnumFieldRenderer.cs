using System.Reflection;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class EnumFieldRenderer : ImGuiReflection.FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        CopperImGui.Text("enum");
    }

    public override void ValueRenderer(ref object value, int id)
    {
        CopperImGui.Text("enum");
    }
}