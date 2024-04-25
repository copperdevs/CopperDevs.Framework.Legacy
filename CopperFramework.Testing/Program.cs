using CopperFramework.Data;
using CopperFramework.Rendering;

namespace CopperFramework.Testing;

public static class Program
{
    public static void Main()
    {
        var engine = new Engine(EngineSettings.UncappedFps);

        Shader.Load(Shader.IncludedShaders.Bloom);

        engine.Run();
    }
}