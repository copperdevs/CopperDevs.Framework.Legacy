using System.Reflection;
using CopperDevs.Core;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui;
using CopperDevs.DearImGui.ReflectionRenderers;
using ImGuiNET;

namespace CopperDevs.Framework.Rendering.DearImGui.ReflectionRenderers;

public class ColorFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var value = (Color)(fieldInfo.GetValue(component) ?? new Color(0));
        var vectorColor = (Vector4)(value / 255);

        CopperImGui.ColorEdit($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", ref vectorColor,
            interactedValue =>
            {
                fieldInfo.SetValue(component, new Color(interactedValue * 255));
                valueChanged?.Invoke();
            });
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        var colorValue = new Vector4(((Color)value).R, ((Color)value).G, ((Color)value).B, ((Color)value).A) / 255;

        Log.Info(colorValue);

        var interacted = ColorEdit($"{value.GetType().Name.ToTitleCase()}###{id}", ref colorValue, _ =>
        {
            valueChanged?.Invoke();
            Log.Debug($"post interact value: {colorValue}");
        });

        Log.Info(colorValue);
        Log.Info(interacted);

        value = new Color(colorValue * 255);
    }

    private static bool ColorEdit(string name, ref Vector4 color, Action<Vector4>? interacted = null)
    {
        if (!ImGui.ColorEdit4(name, ref color))
            return false;

        interacted?.Invoke(color);
        return true;
    }
}