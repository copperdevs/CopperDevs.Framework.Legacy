using CopperFramework.Info;
using Silk.NET.Windowing;

namespace CopperFramework.Renderer.DearImGui.OpenGl;

public class ImGuiRenderer : IImGuiRenderer
{
    private ImGuiController controller;

    public void Setup(CopperWindow window)
    {
        controller = new ImGuiController(
            window.gl,
            CopperWindow.silkWindow,
            window.input
        );
    }

    public void Shutdown()
    {
    }

    public void Begin()
    {
        controller.Update(Time.DeltaTime);
    }

    public void End()
    {
        controller.Render();
    }

    public void Dispose()
    {
        controller.Dispose();
    }
}