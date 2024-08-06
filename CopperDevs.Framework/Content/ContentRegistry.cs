using CopperDevs.Framework.Rendering;

namespace CopperDevs.Framework.Content;

public static class ContentRegistry
{
    // sub systems
    private static RenderingSystem renderingSystem = null!;

    internal static void Start()
    {
        renderingSystem = new RenderingSystem();
        renderingSystem.Start();
    }

    internal static void Stop()
    {
        renderingSystem.Stop();
    }
}