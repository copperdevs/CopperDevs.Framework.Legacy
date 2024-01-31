using CopperFramework.Renderer.DearImGui;
using CopperFramework.Renderer.DearImGui.OpenGl;
using CopperFramework.Systems;

namespace CopperFramework;

public static class Framework
{
    internal static CopperWindow Window = null!;

    public static void Load()
    {
        Window = new CopperWindow();

        Window.OnLoad += () =>
        {
            SystemManager.Initialize();
            SystemManager.Update(SystemUpdateType.Load);
        };

        Window.OnUpdate += () => { SystemManager.Update(SystemUpdateType.Update); };

        Window.OnRender += () => { SystemManager.Update(SystemUpdateType.Renderer); };

        Window.OnClose += () =>
        {
            SystemManager.Shutdown();
            SystemManager.Update(SystemUpdateType.Close);
        };
    }

    public static void Run()
    {
        Window.Run();
        Window.Dispose();
    }
}