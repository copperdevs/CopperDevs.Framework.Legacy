using Raylib_cs;

namespace CopperFramework.Data;

public class EngineSettings
{
    public ConfigFlags WindowFlags = ConfigFlags.Msaa4xHint | ConfigFlags.VSyncHint | ConfigFlags.ResizableWindow |
                                     ConfigFlags.AlwaysRunWindow;

    public Vector2Int WindowSize = new(650, 400);
    public int TargetFps = 60;
    public string WindowTitle = "Window";
}