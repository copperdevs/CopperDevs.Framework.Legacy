using CopperDevs.Core.Data;
using CopperDevs.DearImGui.Attributes;
using Raylib_CSharp.Windowing;

namespace CopperDevs.Framework.Data;

[HideInInspector]
public class EngineSettings
{
    public ConfigFlags WindowFlags = ConfigFlags.Msaa4XHint | ConfigFlags.VSyncHint | ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow | ConfigFlags.TransparentWindow;
    public Vector2Int WindowSize = new(650, 400);
    public int TargetFps = 60;
    public string WindowTitle = "Window";
    public bool DisableDevTools = true;
    public bool EnableDevToolsAtStart;
    public bool DwpApiCustomization = true;

    public static EngineSettings DefaultSettings => new()
    {
        WindowFlags = ConfigFlags.Msaa4XHint | ConfigFlags.VSyncHint | ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow | ConfigFlags.TransparentWindow,
        WindowSize = new Vector2Int(650, 400),
        TargetFps = 60,
        WindowTitle = "CopperFramework - Default",
        DisableDevTools = true,
        EnableDevToolsAtStart = false,
        DwpApiCustomization = true
    };

    public static EngineSettings UncappedFps => new()
    {
        WindowFlags = ConfigFlags.Msaa4XHint | ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow | ConfigFlags.TransparentWindow,
        WindowSize = new Vector2Int(650, 400),
        TargetFps = 10000,
        WindowTitle = "CopperFramework - Uncapped Fps",
        DisableDevTools = true,
        EnableDevToolsAtStart = false,
        DwpApiCustomization = true
    };

    public static EngineSettings Development => new()
    {
        WindowFlags = ConfigFlags.Msaa4XHint | ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow | ConfigFlags.TransparentWindow,
        WindowSize = new Vector2Int(650, 400),
        TargetFps = 10000,
        WindowTitle = "CopperFramework - Development",
        DisableDevTools = false,
        EnableDevToolsAtStart = true,
        DwpApiCustomization = true
    };
}