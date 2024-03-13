using System.Reflection;

namespace CopperFramework.Rendering.DearImGui.ReflectionRenderers;

public class Texture2DFieldRenderer: ImGuiReflection.IFieldRenderer
{
    public void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (Texture2D)(fieldInfo.GetValue(component) ?? new Texture2D());
        
        TextureRenderer($"{fieldInfo.Name}##{fieldInfo.Name}{id}", value);
    }

    public void ValueRenderer(ref object value, int id)
    {
        TextureRenderer($"{value.GetType().Name}##{id}", (Texture2D)value);
    }

    private static void TextureRenderer(string title, Texture2D textureValue)
    {
        CopperImGui.CollapsingHeader(title, () =>
        {
            CopperImGui.HorizontalGroup(() =>
            {
                rlImGui.ImageSize(textureValue, 64, 64);
            }, () =>
            {
                CopperImGui.Text($"Size: <{textureValue.Width},{textureValue.Height}> \nFormat: {textureValue.Format} \nOpenGL id: {textureValue.Id} \nMipmap level: {textureValue.Mipmaps}");
            });
        });
    }
}