using System.Reflection;
using CopperFramework.Util;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class TransformFieldRenderer : ImGuiReflection.IFieldRenderer
    {
        public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
        {
            var value = (Transform)(fieldInfo.GetValue(component) ?? 0);

            TransformRenderer($"{fieldInfo.Name}##{fieldInfo.Name}{id}", ref value, id);
            
            fieldInfo.SetValue(component, value);
        }

        public void ValueRenderer(ref object value, int id)
        {
            var transformValue = (Transform)value;

            TransformRenderer($"{value.GetType().Name}##{id}", ref transformValue, id);

            value = transformValue;
        }

        private static void TransformRenderer(string title, ref Transform transform, int id)
        {
            var value = transform;
            
            CopperImGui.CollapsingHeader(title, () =>
            {
                using (new IndentScope())
                {
                    var position = value.Position;
                    var scale = value.Scale;
                    var rotation = value.Rotation;
                    
                    CopperImGui.DragValue($"Position##{id}", ref position);
                    CopperImGui.DragValue($"Scale##{id}", ref scale);
                    CopperImGui.DragValue($"Rotation##{id}", ref rotation);

                    value.Position = position;
                    value.Scale = scale;
                    value.Rotation = rotation;
                }
            });

            transform = value;
        }
    }
