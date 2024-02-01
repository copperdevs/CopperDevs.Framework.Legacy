using CopperFramework.Renderer;
using CopperFramework.Systems;
using CopperFramework.Util;

namespace CopperFramework;

public static class Framework
{
    internal static CopperWindow Window = null!;

    internal static Shader Shader = null!;

    public static void Load() => Load(() => { });

    public static void Load(Action loadAction)
    {
        Window = new CopperWindow();

        Window.OnLoad += () =>
        {
            SystemManager.Initialize();
            SystemManager.Update(SystemUpdateType.Load);

            Shader = new Shader(CopperWindow.gl,
                ResourceLoader.LoadTextResource("CopperFramework.Resources.Shaders.shader.vert"),
                ResourceLoader.LoadTextResource("CopperFramework.Resources.Shaders.shader.frag"));

            loadAction.Invoke();
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