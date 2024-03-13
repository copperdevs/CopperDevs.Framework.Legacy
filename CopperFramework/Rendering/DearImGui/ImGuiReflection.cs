using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using CopperCore;
using CopperFramework.Rendering.DearImGui.Attributes;
using CopperFramework.Rendering.DearImGui.ReflectionRenderers;
using CopperPlatformer.Core.Utility;

namespace CopperFramework.Rendering.DearImGui;

[SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
internal static class ImGuiReflection
{
    internal static RangeAttribute? currentRangeAttribute;
    internal static ReadOnlyAttribute? currentReadOnlyAttribute;
    internal static TooltipAttribute? currentTooltipAttribute;
    internal static HideInInspectorAttribute? currentHideInInspectorAttribute;
    internal static SpaceAttribute? currentSpaceAttribute;
    internal static SeperatorAttribute? currentSeperatorAttribute;

    internal static void RenderValues(object component, int id = 0)
    {
        var fields = component.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .ToList();
        foreach (var info in fields)
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
            {
                using (new DisabledScope())
                {
                    Render();
                }
            }
            else
            {
                Render();
            }

            currentTooltipAttribute = (TooltipAttribute)Attribute.GetCustomAttribute(info, typeof(TooltipAttribute))!;

            if (currentTooltipAttribute is null)
                continue;
            
            CopperImGui.Tooltip(currentTooltipAttribute.Message);
            
            continue;
            void Render()
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
                            CopperImGui.CollapsingHeader($"{info.Name}##{id + 1}", () =>
                            {
                                using (new IndentScope())
                                {
                                    var subComponent = info.GetValue(component);
                                    if (subComponent is not null)
                                    {
                                        RenderValues(subComponent, id + 1);
                                        info.SetValue(component, subComponent);
                                    }
                                }
                            });
                        }
                        catch (Exception e)
                        {
                            Log.Info(e);
                            CopperImGui.Text(info.FieldType.FullName!, "Unsupported editor value");
                        }
                    }
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

    internal static IFieldRenderer? GetImGuiRenderer<T>()
    {
        return ImGuiRenderers.ContainsKey(typeof(T)) ? ImGuiRenderers[typeof(T)] : null;
    }
    
    private static readonly Dictionary<Type, IFieldRenderer> ImGuiRenderers = new()
    {
        { typeof(float), new FloatFieldRenderer() },
        { typeof(int), new IntFieldRenderer() },
        { typeof(bool), new BoolFieldRenderer() },
        { typeof(string), new StringFieldRenderer() },
        { typeof(Vector2), new Vector2FieldRenderer() },
        { typeof(Vector3), new Vector3FieldRenderer() },
        { typeof(Vector4), new Vector4FieldRenderer() },
        { typeof(Quaternion), new QuaternionFieldRenderer() },
        { typeof(Guid), new GuidFieldRenderer() },
        { typeof(Transform), new TransformFieldRenderer() },
        { typeof(Color), new ColorFieldRenderer() },
        { typeof(Texture2D), new Texture2DFieldRenderer() }
    };

    internal interface IFieldRenderer
    {
        public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id);
        public void ValueRenderer(ref object value, int id);
    }

    private static void ListRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (IList)fieldInfo.GetValue(component)!;

        CopperImGui.CollapsingHeader($"{fieldInfo.Name}##{fieldInfo.Name}{id}", () =>
        {
            using (new IndentScope())
            {
                CopperImGui.HorizontalGroup(() =>
                {
                    CopperImGui.Text($"{value.Count} Items");
                }, () =>
                {
                    CopperImGui.Button($"+##{fieldInfo.Name}{id}", () => value.Add(value[^1]));   
                }, () =>
                {
                    CopperImGui.Button($"-##{fieldInfo.Name}{id}", () => value.RemoveAt(value.Count - 1));   
                });
                
                CopperImGui.Separator();
                
                for (var i = 0; i < value.Count; i++)
                {
                    var item = value[i];

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    var itemType = item.GetType();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                    if (ImGuiRenderers.TryGetValue(itemType, out var renderer))
                    {
                        renderer.ValueRenderer(ref item, int.Parse($"{i}{id}"));
                    }
                    else
                    {
                        try
                        {
                            CopperImGui.CollapsingHeader($"{item.GetType().Name}##{value.IndexOf(item)}", () =>
                            {
                                using (new IndentScope()) 
                                    RenderValues(item, value.IndexOf(item));
                            });
                        }
                        catch (Exception e)
                        {
                            Log.Info(e);
                        }
                    }

                    value[i] = item;
                }

            }
        });

        fieldInfo.SetValue(component, value);
    }
}