using CopperCore.Data;
using CopperDearImGui.Attributes;

namespace CopperFramework.Data;

public class EngineSettings
{
    [HideInInspector] public ConfigFlags WindowFlags = ConfigFlags.Msaa4xHint | ConfigFlags.VSyncHint | ConfigFlags.ResizableWindow |
                                     ConfigFlags.AlwaysRunWindow;

    public Vector2Int WindowSize = new(650, 400);
    [Range(-1, 1000)] public int TargetFps = 60;
    public string WindowTitle = "Window";
}