namespace TopDownShooter;

public static class Program
{
    public static void Main()
    {
        var engineSettings = new EngineSettings()
        {
            DisableDevTools = false,
            EnableDevToolsAtStart = true,
            TargetFps = 144,
            WindowTitle = "Copper Framework - Top Down Shooter"
        };
        
        var engine = new Engine(engineSettings);
        
        engine.Run();
    }
}