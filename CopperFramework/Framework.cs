using CopperFramework.Renderer.DearImGui;
using CopperFramework.Renderer.DearImGui.OpenGl;

namespace CopperFramework;

public static class Framework
{
    private static CopperWindow window = null!;
    private static CopperImGui<ImGuiRenderer> imGuiRenderer = null!;
    
    public static Action? OnLoad;
    public static Action? OnRender;
    public static Action? OnUpdate;
    public static Action? OnClose;
    public static Action? OnUiRender;

    public static void Load()
    {
        window = new CopperWindow();
        imGuiRenderer = new CopperImGui<ImGuiRenderer>();
        
        window.OnLoad += () =>
        {
            imGuiRenderer.Setup(window);
            OnLoad?.Invoke();
        };

        window.OnUpdate += OnUpdate;
        
        window.OnRender += () =>
        {
            OnRender?.Invoke();
            
            imGuiRenderer.Begin();

            OnUiRender?.Invoke();

            imGuiRenderer.End();
        };

        window.OnClose += OnClose;
    }

    public static void Run()
    {
        window.Run();
        window.Dispose();
    }
}