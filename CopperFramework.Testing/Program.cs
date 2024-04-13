using CopperCore;
using CopperFramework.Data;
using CopperFramework.Rendering;
using CopperFramework.Utility;

namespace CopperFramework.Testing;

public static class Program
{
    public static void Main()
    {
        var engine = new Engine(EngineSettings.UncappedFps);
        
        var shader = new Shader(null, ResourceLoader.LoadTextResource("CopperFramework.Resources.Shaders.Bloom.frag"));
        engine.Window.SetScreenShader(shader);
        
        engine.Run();
    }
}