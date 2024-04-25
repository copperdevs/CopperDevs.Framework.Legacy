using CopperFramework.Data;

namespace CopperFramework.UiCreator;

public static class Program
{
    public static void Main()
    {
        var engine = new Engine(EngineSettings.Development);
        
        
        
        engine.Run();
    }
}