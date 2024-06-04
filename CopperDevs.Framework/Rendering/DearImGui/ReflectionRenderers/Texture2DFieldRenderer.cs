using System.Reflection;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui;
using CopperDevs.DearImGui.ReflectionRenderers;
using Raylib_CSharp.Textures;

namespace CopperDevs.Framework.Rendering.DearImGui.ReflectionRenderers;

public class Texture2DFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id)
    {
        var value = (rlTexture)(fieldInfo.GetValue(component) ?? new rlTexture());

        TextureRenderer($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", value);
    }

    public override void ValueRenderer(ref object value, int id)
    {
        TextureRenderer($"{value.GetType().Name.ToTitleCase()}##{id}", (rlTexture)value);
    }

    private static void TextureRenderer(string title, rlTexture textureValue)
    {
        CopperImGui.CollapsingHeader(title,
            () =>
            {
                CopperImGui.HorizontalGroup(() => { rlImGui.ImageSize(textureValue, 64, 64); },
                    () =>
                    {
                        CopperImGui.Text(
                            $"Size: <{textureValue.Width},{textureValue.Height}> \nFormat: {textureValue.Format} \nOpenGL id: {textureValue.Id} \nMipmap level: {textureValue.Mipmaps}");
                    });
            });
    }
}