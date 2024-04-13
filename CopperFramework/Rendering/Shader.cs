using CopperCore;

namespace CopperFramework.Rendering;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class Shader : IRenderable
{
    private static readonly List<Shader> ShaderLoadQueue = [];
    private static bool requireQueue = true;

    public readonly string Name;
    public readonly string? VertexShaderData;
    public readonly string? FragmentShaderData;

    private rlShader rlShader;


    public static Shader Load(string shaderName, string? newVertexShaderData = null, string? newFragmentShaderData = null)
    {
        return new Shader(shaderName, newVertexShaderData, newFragmentShaderData);
    }

    public Shader(string shaderName, string? newVertexShaderData = null, string? newFragmentShaderData = null)
    {
        Name = shaderName;
        VertexShaderData = newVertexShaderData;
        FragmentShaderData = newFragmentShaderData;

        if (requireQueue)
            ShaderLoadQueue.Add(this);
        else
            LoadShader();
    }

    public static implicit operator rlShader(Shader shader) => shader.rlShader;


    #region Queue Methods

    internal static void LoadQueue()
    {
        requireQueue = false;

        foreach (var shader in ShaderLoadQueue)
            shader.LoadShader();
    }

    private void LoadShader()
    {
        Log.Info($"Loading Shader");
        rlShader = Raylib.LoadShaderFromMemory(VertexShaderData, FragmentShaderData);
        
        RenderingManager.RegisterRenderableItem(this);
    }

    #endregion
}