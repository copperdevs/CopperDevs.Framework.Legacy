using CopperDevs.Framework.Resources.Shaders;

namespace CopperDevs.Framework.Rendering;

public enum IncludedShaders
{
    Bloom,
    Blur,
    CrossStitching,
    DreamVision,
    Fisheye,
    Grayscale,
    Pixelizer,
    Posterization,
    Predator,
    Scanlines,
    Sobel
}

public static class IncludedShadersExtension
{
    public static string GetShaderContentsFromRegistry(this IncludedShaders targetShader)
    {
        return targetShader switch
        {
            IncludedShaders.Bloom => ShaderRegistry.Instance.ScreenShaders.Bloom,
            IncludedShaders.Blur => ShaderRegistry.Instance.ScreenShaders.Blur,
            IncludedShaders.CrossStitching => ShaderRegistry.Instance.ScreenShaders.CrossStitching,
            IncludedShaders.DreamVision => ShaderRegistry.Instance.ScreenShaders.DreamVision,
            IncludedShaders.Fisheye => ShaderRegistry.Instance.ScreenShaders.Fisheye,
            IncludedShaders.Grayscale => ShaderRegistry.Instance.ScreenShaders.Grayscale,
            IncludedShaders.Pixelizer => ShaderRegistry.Instance.ScreenShaders.Pixelizer,
            IncludedShaders.Posterization => ShaderRegistry.Instance.ScreenShaders.Posterization,
            IncludedShaders.Predator => ShaderRegistry.Instance.ScreenShaders.Predator,
            IncludedShaders.Scanlines => ShaderRegistry.Instance.ScreenShaders.Scanlines,
            IncludedShaders.Sobel => ShaderRegistry.Instance.ScreenShaders.Sobel,
            _ => throw new ArgumentOutOfRangeException(nameof(targetShader), targetShader, null)
        };
    }
}