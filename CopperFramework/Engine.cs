using CopperCore;
using CopperFramework.Elements;
using CopperFramework.Elements.Systems;
using CopperFramework.Utility;

namespace CopperFramework;

public class Engine : Singleton<Engine>
{
    public static EngineWindow CurrentWindow => Instance.Window;
    
    public readonly EngineWindow Window;
    public readonly EngineSettings Settings;

    public Engine() : this(EngineSettings.DefaultSettings)
    {
    }

    public Engine(EngineSettings settings)
    {
        CopperLogger.Initialize();
        ConsoleUtil.Initialize();

        SetInstance(this);

        Settings = settings;
        Window = new EngineWindow(settings);
    }

    public void Run()
    {
        Start();

        while (!Raylib.WindowShouldClose())
            Update();

        Stop();
    }

    private void Start()
    {
        Window.Start();

        ElementManager.Initialize();
    }

    private void Update()
    {
        Window.Update(() =>
            {
                ElementManager.Update(ElementManager.ElementUpdateType.Update);
                ElementManager.Update(ElementManager.ElementUpdateType.Render);

                if (DebugSystem.Instance.DebugEnabled)
                    ElementManager.Update(ElementManager.ElementUpdateType.Debug);
            }, () =>
            {
                //
                ElementManager.Update(ElementManager.ElementUpdateType.UiRender);
            },
            () =>
            {
                //
                ElementManager.Update(ElementManager.ElementUpdateType.Fixed);
            });
    }

    private void Stop()
    {
        ElementManager.Shutdown();
        Window.Shutdown();
    }
}