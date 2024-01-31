using System.Collections;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using CopperFramework.Data;
using CopperFramework.Renderer.DearImGui.Attributes;
using CopperFramework.Util;
using ImGuiNET;

namespace CopperFramework.Renderer.DearImGui;

internal static class ImGuiReflection
{
    private static RangeAttribute? currentRangeAttribute;
    private static ReadOnlyAttribute? currentReadOnlyAttribute;
    private static TooltipAttribute? currentTooltipAttribute;
    private static HideInInspectorAttribute? currentHideInInspectorAttribute;
    private static SpaceAttribute? currentSpaceAttribute;
    private static SeperatorAttribute? currentSeperatorAttribute;
    
    internal static void RenderValues(object component, int id = 0)
    {
        var fields = component.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .ToList();
        foreach (var info in fields.Where(info => info.FieldType != typeof(Transform)))
        {
            SpaceAttributeRenderer(info);
            SeperatorAttributeRenderer(info);

            currentHideInInspectorAttribute =
                (HideInInspectorAttribute?)Attribute.GetCustomAttribute(info, typeof(HideInInspectorAttribute))!;

            if (currentHideInInspectorAttribute is not null)
                continue;

            currentReadOnlyAttribute =
                (ReadOnlyAttribute?)Attribute.GetCustomAttribute(info, typeof(ReadOnlyAttribute))!;

            if (currentReadOnlyAttribute is not null)
                ImGui.BeginDisabled();

            {
                var isList = info.FieldType is { IsGenericType: true } &&
                             info.FieldType.GetGenericTypeDefinition() == typeof(List<>);

                if (isList)
                {
                    ListRenderer(info, component, id);
                }
                else
                {
                    if (ImGuiRenderers.TryGetValue(info.FieldType, out var renderer))
                        renderer.ReflectionRenderer(info, component, id);
                    else
                    {
                        try
                        {
                            if (ImGui.CollapsingHeader($"{info.Name}##{id+1}"))
                            {
                                ImGui.Indent();
                                var subComponent = info.GetValue(component);
                                if (subComponent is not null)
                                {
                                    RenderValues(subComponent, id+1);
                                    info.SetValue(component, subComponent);
                                }
                                ImGui.Unindent();
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Info(e);
                            ImGui.LabelText("Unsupported editor value", info.FieldType.FullName);
                        }
                    }
                }
            }

            if (currentReadOnlyAttribute is not null)
                ImGui.EndDisabled();

            currentTooltipAttribute = (TooltipAttribute)Attribute.GetCustomAttribute(info, typeof(TooltipAttribute))!;

            if (currentTooltipAttribute is not null)
            {
                if (ImGui.BeginItemTooltip())
                {
                    ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35.0f);
                    ImGui.TextUnformatted(currentTooltipAttribute.Message);
                    ImGui.PopTextWrapPos();
                    ImGui.EndTooltip();
                }
            }
        }
    }

    private static void SpaceAttributeRenderer(FieldInfo info)
    {
        currentSpaceAttribute = (SpaceAttribute?)Attribute.GetCustomAttribute(info, typeof(SpaceAttribute))!;
        if (currentSpaceAttribute is not null) currentSpaceAttribute.Render();
    }

    private static void SeperatorAttributeRenderer(FieldInfo info)
    {
        currentSeperatorAttribute =
            (SeperatorAttribute?)Attribute.GetCustomAttribute(info, typeof(SeperatorAttribute))!;
        if (currentSeperatorAttribute is not null) currentSeperatorAttribute.Render();
    }

    internal static readonly Dictionary<Type, IFieldRenderer> ImGuiRenderers = new()
    {
        { typeof(float), new FloatFieldRenderer() },
        { typeof(int), new IntFieldRenderer() },
        { typeof(bool), new BoolFieldRenderer() },
        { typeof(Vector2), new Vector2FieldRenderer() },
        { typeof(Vector3), new Vector3FieldRenderer() },
        { typeof(Vector4), new Vector4FieldRenderer() },
        { typeof(Quaternion), new QuaternionFieldRenderer() },
        { typeof(Guid), new GuidFieldRenderer() },
        { typeof(Transform), new TransformFieldRenderer() },
        { typeof(Color), new ColorFieldRenderer() },
    };

    internal interface IFieldRenderer
    {
        public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id);
        public void ValueRenderer(ref object value, int id);
    }

    private static void ListRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (IList)fieldInfo.GetValue(component)!;

        if (ImGui.CollapsingHeader($"{fieldInfo.Name}##{fieldInfo.Name}{id}"))
        {
            ImGui.Indent();

            ImGui.Text($"{value.Count} Items");
            
            ImGui.SameLine();

            if (ImGui.Button($"+##{fieldInfo.Name}{id}"))
                value.Add(value[^1]);
            
            ImGui.SameLine();
            
            if (ImGui.Button($"-##{fieldInfo.Name}{id}"))
                value.RemoveAt(value.Count-1);

            ImGui.Separator();

            for (var i = 0; i < value.Count; i++)
            {
                var item = value[i];

                var itemType = item.GetType();

                if (ImGuiRenderers.TryGetValue(itemType, out var renderer))
                {
                    renderer.ValueRenderer(ref item, int.Parse($"{i}{id}"));
                }
                else
                {
                    try
                    {
                        if (ImGui.CollapsingHeader($"{item.GetType().Name}##{value.IndexOf(item)}"))
                        {
                            ImGui.Indent();
                            RenderValues(item, value.IndexOf(item));
                            ImGui.Unindent();
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Info(e);
                    }
                }

                value[i] = item;
            }

            ImGui.Unindent();
        }

        fieldInfo.SetValue(component, value);
    }


    private class FloatFieldRenderer : IFieldRenderer
    {
        public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
        {
            currentRangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;

            if (currentRangeAttribute is not null)
            {
                var value = (float)(fieldInfo.GetValue(component) ?? 0);
                if (ImGui.SliderFloat($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value, currentRangeAttribute.Min,
                        currentRangeAttribute.Max))
                    fieldInfo.SetValue(component, value);
            }
            else
            {
                var value = (float)(fieldInfo.GetValue(component) ?? 0);
                if (ImGui.DragFloat($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value))
                    fieldInfo.SetValue(component, value);
            }
        }

        public void ValueRenderer(ref object value, int id)
        {
            var floatValue = (float)value;
            if (ImGui.DragFloat($"{value.GetType().Name}{id}##{id}", ref floatValue))
                value = floatValue;
        }
    }

    private class IntFieldRenderer : IFieldRenderer
    {
        public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
        {
            currentRangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;

            if (currentRangeAttribute is not null)
            {
                var value = (int)(fieldInfo.GetValue(component) ?? 0);
                if (ImGui.SliderInt($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value,
                        (int)currentRangeAttribute.Min,
                        (int)currentRangeAttribute.Max))
                    fieldInfo.SetValue(component, value);
            }
            else
            {
                var value = (int)(fieldInfo.GetValue(component) ?? 0);
                if (ImGui.DragInt($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value))
                    fieldInfo.SetValue(component, value);
            }
        }

        public void ValueRenderer(ref object value, int id)
        {
            var intValue = (int)value;
            if (ImGui.DragInt($"{value.GetType().Name}##{id}", ref intValue))
                value = intValue;
        }
    }

    private class BoolFieldRenderer : IFieldRenderer
    {
        public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
        {
            var value = (bool)(fieldInfo.GetValue(component) ?? false);
            if (ImGui.Checkbox($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value))
                fieldInfo.SetValue(component, value);
        }

        public void ValueRenderer(ref object value, int id)
        {
            var boolValue = (bool)value;
            if (ImGui.Checkbox($"{value.GetType().Name}##{id}", ref boolValue))
                value = boolValue;
        }
    }

    private class Vector2FieldRenderer : IFieldRenderer
    {
        public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
        {
            currentRangeAttribute = (RangeAttribute?)Attribute.GetCustomAttribute(fieldInfo, typeof(RangeAttribute))!;
            if (currentRangeAttribute is not null)
            {
                var value = (Vector2)(fieldInfo.GetValue(component) ?? Vector2.Zero);
                if (ImGui.SliderFloat2($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value,
                        (int)currentRangeAttribute.Min,
                        (int)currentRangeAttribute.Max))
                    fieldInfo.SetValue(component, value);
            }
            else
            {
                var value = (Vector2)(fieldInfo.GetValue(component) ?? Vector2.Zero);
                if (ImGui.DragFloat2($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value))
                    fieldInfo.SetValue(component, value);
            }
        }

        public void ValueRenderer(ref object value, int id)
        {
            var vectorValue = (Vector2)value;
            if (ImGui.DragFloat2($"{value.GetType().Name}##{id}", ref vectorValue))
                value = vectorValue;
        }
    }

    private class Vector3FieldRenderer : IFieldRenderer
    {
        public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
        {
            var value = (Vector3)(fieldInfo.GetValue(component) ?? Vector3.Zero);
            if (ImGui.DragFloat3($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value))
                fieldInfo.SetValue(component, value);
        }

        public void ValueRenderer(ref object value, int id)
        {
            var vectorValue = (Vector3)value;
            if (ImGui.DragFloat3($"{value.GetType().Name}##{id}", ref vectorValue))
                value = vectorValue;
        }
    }

    private class Vector4FieldRenderer : IFieldRenderer
    {
        public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
        {
            var value = (Vector4)(fieldInfo.GetValue(component) ?? Vector4.Zero);
            if (ImGui.DragFloat4($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value))
                fieldInfo.SetValue(component, value);
        }

        public void ValueRenderer(ref object value, int id)
        {
            var vectorValue = (Vector4)value;
            if (ImGui.DragFloat4($"{value.GetType().Name}##{id}", ref vectorValue))
                value = vectorValue;
        }
    }

    private class QuaternionFieldRenderer : IFieldRenderer
    {
        public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
        {
            var value = ((Quaternion)(fieldInfo.GetValue(component) ?? Quaternion.Identity)).ToVector();
            if (ImGui.DragFloat4($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value))
            {
                var result = value.ToQuaternion();
                fieldInfo.SetValue(component, result);
            }
        }

        public void ValueRenderer(ref object value, int id)
        {
            var vectorValue = ((Quaternion)value).ToVector();
            if (ImGui.DragFloat4($"{value.GetType().Name}##{id}", ref vectorValue))
                value = vectorValue.ToQuaternion();
        }
    }

    private class GuidFieldRenderer : IFieldRenderer
    {
        public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
        {
            var value = (Guid)(fieldInfo.GetValue(component) ?? new Guid());
            ImGui.LabelText($"{fieldInfo.Name}##{fieldInfo.Name}{id}", value.ToString());
        }

        public void ValueRenderer(ref object value, int id)
        {
            ImGui.LabelText($"{value.GetType().Name}##{id}", ((Guid)value).ToString());
        }
    }

    private class TransformFieldRenderer : IFieldRenderer
    {
        public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
        {
            var value = (Transform)(fieldInfo.GetValue(component) ?? 0);

            if (ImGui.CollapsingHeader($"{fieldInfo.Name}##{fieldInfo.Name}{id}"))
            {
                ImGui.Indent();

                var position = value.Position;
                if (ImGui.DragFloat3($"Position##{fieldInfo.Name}{id}", ref position, 0.1f))
                {
                    value.Position = position;
                    fieldInfo.SetValue(component, value);
                }

                var scale = value.Scale;
                if (ImGui.DragFloat3($"Scale##{fieldInfo.Name}{id}", ref scale, 0.1f))
                {
                    value.Scale = scale;
                    fieldInfo.SetValue(component, value);
                }

                var rotation = value.Rotation.ToVector();
                if (ImGui.DragFloat4($"Rotation##{fieldInfo.Name}{id}", ref rotation, 0.1f))
                {
                    value.Rotation = rotation.ToQuaternion();
                    fieldInfo.SetValue(component, value);
                }

                ImGui.Unindent();
            }
        }

        public void ValueRenderer(ref object value, int id)
        {
            var transformValue = (Transform)value;

            if (ImGui.CollapsingHeader($"{value.GetType().Name}##{id}"))
            {
                ImGui.Indent();

                var position = transformValue.Position;
                if (ImGui.DragFloat3($"Position##{id}", ref position, 0.1f))
                {
                    transformValue.Position = position;
                    value = transformValue;
                }

                var scale = transformValue.Scale;
                if (ImGui.DragFloat3($"Scale##{id}", ref scale, 0.1f))
                {
                    transformValue.Scale = scale;
                    value = transformValue;
                }

                var rotation = transformValue.Rotation.ToVector();
                if (ImGui.DragFloat4($"Rotation##{id}", ref rotation, 0.1f))
                {
                    transformValue.Rotation = rotation.ToQuaternion();
                    value = transformValue;
                }

                ImGui.Unindent();
            }
        }
    }

    private class ColorFieldRenderer : IFieldRenderer
    {
        public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
        {
            var value = (Color)(fieldInfo.GetValue(component) ?? new Color(0));
            var color = value / 255;
            Vector4 vecColor = color;
            if (ImGui.ColorEdit4($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref vecColor))
            {
                fieldInfo.SetValue(component, new Color(vecColor * 255));
            }
        }

        public void ValueRenderer(ref object value, int id)
        {
            var colorValue = (Vector4)(((Color)value) / 255);
            if (ImGui.ColorEdit4($"{value.GetType().Name}##{id}", ref colorValue))
            {
                value = new Color(colorValue * 255);
            }
        }
    }
}