global using System.Collections;
global using System.Numerics;
global using CopperFramework.Data;
global using Raylib_cs;
global using Color = CopperFramework.Data.Color;
global using Random = CopperFramework.Utility.Random;
global using rlColor = Raylib_cs.Color;
global using Transform = CopperFramework.Data.Transform;
using CopperCore;
using CopperFramework.Elements;
using CopperFramework.Elements.Components;
using CopperFramework.Elements.Systems;
using CopperFramework.Utility;

namespace CopperFramework;

public class Engine : Singleton<Engine>
{
    private readonly EngineWindow window;
    public EngineSettings Settings { get; private set; }

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
            }, () => { ElementManager.Update(ElementManager.ElementUpdateType.UiRender); },
            () => { ComponentRegistry.CurrentComponents.ToList().ForEach(gameObject => gameObject.UpdateComponents(component => component.FixedUpdate())); });
    }

    private void Stop()
    {
        ElementManager.Shutdown();
        window.Shutdown();
    }
}