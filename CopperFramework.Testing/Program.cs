using CopperFramework.Data;
using CopperFramework.Rendering;
using CopperFramework.Utility;

namespace CopperFramework.Testing;

public static class Program
{
    public static void Main()
    {
        var engine = new Engine(EngineSettings.UncappedFps);

        Shader.Load("Bloom Screen Shader", null, ResourceLoader.LoadTextResource("CopperFramework.Resources.Shaders.Bloom.frag"));

        engine.Run();
    }
}