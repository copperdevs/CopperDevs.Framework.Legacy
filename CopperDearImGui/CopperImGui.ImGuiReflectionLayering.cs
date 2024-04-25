using CopperDearImGui.ReflectionRenderers;

namespace CopperDearImGui;

public static partial class CopperImGui
{
    public static void RenderValues(object component, int id = 0)
    {
        ImGuiReflection.RenderValues(component);
    }

    public static FieldRenderer? GetFieldRenderer<T>()
    {
        return ImGuiReflection.GetImGuiRenderer<T>();
    }

    public static void RegisterFieldRenderer<TType, TRenderer>() where TRenderer : FieldRenderer, new()
    {
        ImGuiReflection.ImGuiRenderers.TryAdd(typeof(TType), new TRenderer());
    }
}