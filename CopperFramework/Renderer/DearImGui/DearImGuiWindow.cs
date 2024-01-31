using ImGuiNET;

namespace CopperFramework.Renderer.DearImGui;

public abstract class DearImGuiWindow
{
    public virtual string GetWindowName() => "Unnamed Window";
    public virtual ImGuiWindowFlags GetWindowFlags() => ImGuiWindowFlags.None;

    public virtual void PreRender()
    {
    }

    public abstract void Render();

    public virtual void PostRender()
    {
    }

    protected internal bool IsOpen;
}