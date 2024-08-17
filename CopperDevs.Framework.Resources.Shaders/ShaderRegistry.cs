using CopperDevs.Framework.Resources.Core;

namespace CopperDevs.Framework.Resources.Shaders;

public class ShaderRegistry : ResourceRegistry<ShaderRegistry>
{
    public ShaderRegistry() : base(ResourceType.Shaders) => SetInstance(this);

    public ScreenShaders ScreenShaders = null!;

    protected override void LoadResources()
    {
        ScreenShaders = new ScreenShaders();
    }
}