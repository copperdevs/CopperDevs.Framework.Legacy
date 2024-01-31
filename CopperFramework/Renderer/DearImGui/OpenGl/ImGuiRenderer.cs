using CopperFramework.Info;

namespace CopperFramework.Renderer.DearImGui.OpenGl;

public class ImGuiRenderer : IImGuiRenderer
{
    private ImGuiController controller = null!;

    public void Setup(CopperWindow window)
    {
        controller = new ImGuiController(
            CopperWindow.gl,
            CopperWindow.silkWindow,
            CopperWindow.input
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