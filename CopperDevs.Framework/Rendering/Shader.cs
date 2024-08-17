namespace CopperDevs.Framework.Rendering;

public sealed class Shader : BaseRenderable
{
    public readonly string Name;
    public readonly string? VertexShaderData;
    public readonly string? FragmentShaderData;

    private rlShader rlShader;


    public static Shader Load(string shaderName, string? newVertexShaderData = null, string? newFragmentShaderData = null)
    {
        return new Shader(shaderName, newVertexShaderData, newFragmentShaderData);
    }

    public static Shader Load(IncludedShaders includedShaders)
    {
        return Load(includedShaders.ToString(), null, includedShaders.GetShaderContentsFromRegistry());
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
        rlShader = rlShader.LoadFromMemory(VertexShaderData!, FragmentShaderData!);

        if (!ListContainsShader(RenderingSystem.Instance.GetRenderableItems<Shader>()))
            RenderingSystem.Instance.RegisterRenderableItem(this);
    }

    public override void UnLoadRenderable()
    {
        RenderingSystem.Instance.DeregisterRenderableItem(this);
        rlShader.Unload();
    }

    private bool ListContainsShader(List<Shader> shaders)
    {
        if (shaders.Contains(this))
            return true;

        foreach (var shader in shaders)
        {
            if (!string.IsNullOrEmpty(shader.VertexShaderData))
            {
                if (shader.VertexShaderData == VertexShaderData)
                    return true;
            }

            if (!string.IsNullOrEmpty(shader.FragmentShaderData))
            {
                if (shader.FragmentShaderData == FragmentShaderData)
                    return true;
            }
        }

        return false;
    }

    public static implicit operator rlShader(Shader shader) => shader.rlShader;
}