using CopperDevs.Core.Utility;

namespace CopperDevs.Framework.Utility;

public class ShaderScope : Scope
{
    private readonly bool condition;

    public ShaderScope(Shader shader, bool condition)
    {
        this.condition = condition;
        
        if(condition)
            rlGraphics.BeginShaderMode(shader);
    }
    
    protected override void CloseScope()
    {
        if(condition)
            rlGraphics.EndShaderMode();
    }
}