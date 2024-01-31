using CopperFramework.Components;
using CopperFramework.Util;

namespace CopperFramework.Testing;

public class LogComponent : GameComponent
{
    public override void Start()
    {
        Log.Info("Start");
    }

    public override void Update()
    {
        Log.Info("Update");
    }

    public override void FixedUpdate()
    {
        Log.Info("FixedUpdate");
    }

    public override void Stop()
    {
        Log.Info("Stop");
    }
    
    public override void UiUpdate()
    {
        Log.Info("UiUpdate");
    }

    public override void Render()
    {
        Log.Info("Render");
    }
}