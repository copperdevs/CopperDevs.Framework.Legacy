using CopperDearImGui.ReflectionRenderers;

namespace CopperDearImGui;

public static partial class CopperImGui
{
    public static void RenderObjectValues(object component, int id = 0)
    {
        ImGuiReflection.RenderValues(component, id);
    }

    /// <summary>
    /// Uses registered field renderers to render values of a targetObject
    /// </summary>
    /// <param name="targetObject">Target object to render</param>
    /// <param name="id">Basically DearImGui rendering Id</param>
    /// <typeparam name="TTargetType">Type of the object to render</typeparam>
    public static void RenderObjectValues<TTargetType>(ref TTargetType targetObject, int id = 0)
    {
        var targetObjectCasted = (object)targetObject!;
        ImGuiReflection.GetImGuiRenderer<TTargetType>()?.ValueRenderer(ref targetObjectCasted, id);
        targetObject = (TTargetType)targetObjectCasted;
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