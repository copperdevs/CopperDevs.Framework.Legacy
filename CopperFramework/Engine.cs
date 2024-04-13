using CopperCore;
using CopperFramework.Elements;
using CopperFramework.Elements.Systems;
using CopperFramework.Utility;

namespace CopperFramework;

public class Engine : Singleton<Engine>
{
    private readonly EngineWindow window;
    public EngineSettings Settings;

    public Engine() : this(new EngineSettings())
    {
    }

    public Engine(EngineSettings settings)
    {
        CopperLogger.Initialize();
        ConsoleUtil.Initialize();

        SetInstance(this);

        Settings = settings;
        window = new EngineWindow();
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
        window.Start();

        ElementManager.Initialize();
    }

    private void Update()
    {
        window.Update(() =>
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
        window.Shutdown();
    }
}