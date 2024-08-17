namespace CopperDevs.Framework.Resources.Shaders;

public class ScreenShaders
{
    private static string GetPath(string name) => $"Screen.{name}.frag";

    public readonly string Bloom = ShaderRegistry.Instance.LoadTextResource(GetPath("Bloom"));
    public readonly string Blur = ShaderRegistry.Instance.LoadTextResource(GetPath("Blur"));
    public readonly string CrossStitching = ShaderRegistry.Instance.LoadTextResource(GetPath("CrossStitching"));
    public readonly string DreamVision = ShaderRegistry.Instance.LoadTextResource(GetPath("DreamVision"));
    public readonly string Fisheye = ShaderRegistry.Instance.LoadTextResource(GetPath("Fisheye"));
    public readonly string Grayscale = ShaderRegistry.Instance.LoadTextResource(GetPath("Grayscale"));
    public readonly string Pixelizer = ShaderRegistry.Instance.LoadTextResource(GetPath("Pixelizer"));
    public readonly string Posterization = ShaderRegistry.Instance.LoadTextResource(GetPath("Posterization"));
    public readonly string Predator = ShaderRegistry.Instance.LoadTextResource(GetPath("Predator"));
    public readonly string Scanlines = ShaderRegistry.Instance.LoadTextResource(GetPath("Scanlines"));
    public readonly string Sobel = ShaderRegistry.Instance.LoadTextResource(GetPath("Sobel"));
}