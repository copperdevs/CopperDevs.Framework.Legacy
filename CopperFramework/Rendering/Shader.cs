using CopperCore;

namespace CopperFramework.Rendering;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class Shader
{
    private static readonly List<Shader> ShaderLoadQueue = [];
    private static bool requireQueue = true;

    private readonly string? vertexShaderData;
    private readonly string? fragmentShaderData;

    private rlShader rlShader;


    public Shader(string? newVertexShaderData, string? newFragmentShaderData)
    {
        vertexShaderData = newVertexShaderData;
        fragmentShaderData = newFragmentShaderData;

        if (requireQueue)
            ShaderLoadQueue.Add(this);
        else
            Load();
    }

    public static implicit operator rlShader(Shader shader) => shader.rlShader;


    #region Queue Methods

    internal static void LoadQueue()
    {
        requireQueue = false;

        foreach (var shader in ShaderLoadQueue)
            shader.Load();
    }

    private void Load()
    {
        Log.Info($"Loading Shader");
        rlShader = Raylib.LoadShaderFromMemory(vertexShaderData, fragmentShaderData);
    }

    #endregion
}