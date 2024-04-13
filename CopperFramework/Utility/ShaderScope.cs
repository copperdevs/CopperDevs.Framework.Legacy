using CopperCore.Utility;

namespace CopperFramework.Utility;

public class ShaderScope : Scope
{
    private readonly bool condition;

    public ShaderScope(Shader shader, bool condition)
    {
        this.condition = condition;
        
        if(condition)
            Raylib.BeginShaderMode(shader);
    }
    
    protected override void CloseScope()
    {
        if(condition)
            Raylib.EndShaderMode();
    }
}