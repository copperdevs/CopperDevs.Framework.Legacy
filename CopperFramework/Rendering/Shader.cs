using CopperCore;
using CopperFramework.Utility;

namespace CopperFramework.Rendering;

public sealed class Shader : BaseRenderable
{
    public readonly string Name;
    public readonly string? VertexShaderData;
    public readonly string? FragmentShaderData;

    private rlShader rlShader;

    public enum IncludedShaders
    {
        Bloom,
        Blur,
        CrossStitching,
        DreamVision,
        Fisheye,
        Grayscale,
        // ReSharper disable once IdentifierTypo
        Pixelizer,
        Posterization,
        Predator,
        // ReSharper disable once IdentifierTypo
        Scanlines,
        Sobel
    }


    public static Shader Load(string shaderName, string? newVertexShaderData = null,
        string? newFragmentShaderData = null)
    {
        return new Shader(shaderName, newVertexShaderData, newFragmentShaderData);
    }

    public static Shader Load(IncludedShaders includedShaders)
    {
        return Load(includedShaders.ToString(), null,
            ResourceLoader.LoadTextResource($"CopperFramework.Resources.Shaders.{includedShaders.ToString()}.frag"));
    }


    public Shader(string shaderName, string? newVertexShaderData = null, string? newFragmentShaderData = null)
    {
        Name = shaderName;
        VertexShaderData = newVertexShaderData;
        FragmentShaderData = newFragmentShaderData;
        
        BaseLoad(this);
    }

    public override void LoadRenderable()
    {
        rlShader = Raylib.LoadShaderFromMemory(VertexShaderData, FragmentShaderData);
        RenderingSystem.Instance.RegisterRenderableItem(this);
    }

    public override void UnLoadRenderable()
    {
        RenderingSystem.Instance.DeregisterRenderableItem(this);
        Raylib.UnloadShader(rlShader);
    }

    public static implicit operator rlShader(Shader shader) => shader.rlShader;
}