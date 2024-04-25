using CopperFramework.Data;
using CopperFramework.Rendering;

namespace CopperFramework.Testing;

public static class Program
{
    public static void Main()
    {
        var engine = new Engine(EngineSettings.UncappedFps);

        Shader.Load("Empty");
        
        Shader.Load(Shader.IncludedShaders.Bloom);
        Shader.Load(Shader.IncludedShaders.Blur);
        Shader.Load(Shader.IncludedShaders.CrossStitching);
        Shader.Load(Shader.IncludedShaders.DreamVision);
        Shader.Load(Shader.IncludedShaders.Fisheye);
        Shader.Load(Shader.IncludedShaders.Grayscale);
        Shader.Load(Shader.IncludedShaders.Pixelizer);
        Shader.Load(Shader.IncludedShaders.Posterization);
        Shader.Load(Shader.IncludedShaders.Predator);
        Shader.Load(Shader.IncludedShaders.Scanlines);
        Shader.Load(Shader.IncludedShaders.Sobel);

        engine.Run();
    }
}