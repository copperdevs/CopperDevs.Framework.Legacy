using System.Diagnostics;
using CopperCore;
using CopperFramework.Elements;
using CopperFramework.Elements.Systems;
using CopperFramework.Utility;

namespace CopperFramework;

public class Engine : Singleton<Engine>
{
    public static EngineWindow CurrentWindow => Instance.window;

    private readonly EngineWindow window;
    internal readonly EngineSettings Settings;

    public bool ShouldRun;

    private readonly Stopwatch stopwatch;

    public Engine() : this(EngineSettings.DefaultSettings)
    {
    }

    public Engine(EngineSettings settings)
    {
        stopwatch = Stopwatch.StartNew();
        
        CopperLogger.Initialize();
        ConsoleUtil.Initialize();

        SetInstance(this);

        Settings = settings;
        window = new EngineWindow(settings);

        ShouldRun = true;
        
        Log.Info($"Time elapsed during engine creation: {stopwatch.Elapsed}");
    }

    public void Run()
    {
        Start();

        while (!Raylib.WindowShouldClose() && ShouldRun)
            Update();

        Stop();
    }

    private void Start()
    {
        window.Start();

        ElementManager.Initialize();
        
        Log.Info($"Time elapsed starting the engine: {stopwatch.Elapsed}");
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
        Log.Info($"Time elapsed during the runtime of the engine: {stopwatch.Elapsed}");
        ElementManager.Shutdown();
        window.Shutdown();
    }
}