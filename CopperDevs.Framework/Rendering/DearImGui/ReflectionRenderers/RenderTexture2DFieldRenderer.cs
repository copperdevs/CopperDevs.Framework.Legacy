using System.Reflection;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui;
using CopperDevs.DearImGui.ReflectionRenderers;
using Raylib_CSharp.Textures;

namespace CopperDevs.Framework.Rendering.DearImGui.ReflectionRenderers;

public class RenderTexture2DFieldRenderer : FieldRenderer
{
    public override void ReflectionRenderer(FieldInfo fieldInfo, object component, int id, Action valueChanged = null!)
    {
        var value = (rlRenderTexture)(fieldInfo.GetValue(component) ?? new rlRenderTexture());

        TextureRenderer($"{fieldInfo.Name.ToTitleCase()}##{fieldInfo.Name}{id}", value);
    }

    public override void ValueRenderer(ref object value, int id, Action valueChanged = null!)
    {
        TextureRenderer($"{value.GetType().Name.ToTitleCase()}##{id}", (rlRenderTexture)value);
    }

    private static void TextureRenderer(string title, rlRenderTexture textureValue)
    {
        CopperImGui.CollapsingHeader(title, () =>
        {
            CopperImGui.CollapsingHeader("Texture Info", () =>
            {
                CopperImGui.Text(textureValue.Id, "Render texture id");
                CopperImGui.Text(textureValue.Texture.Format, "Format");
                CopperImGui.Text($"{textureValue.Texture.Width},{textureValue.Texture.Height}", "Size");
                CopperImGui.Text(textureValue.Texture.Id, "OpenGL id");
                CopperImGui.Text(textureValue.Texture.Mipmaps, "Mipmap level");
            });

            CopperImGui.CollapsingHeader("Texture", () => { rlImGui.ImageFit(textureValue.Texture); });
        });
    }
}