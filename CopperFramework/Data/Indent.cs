using ImGuiNET;

namespace CopperFramework.Data;

public class Indent : IDisposable
{
    public Indent()
    {
        ImGui.Indent();
    }

    ~Indent()
    {
        ReleaseUnmanagedResources();
    }

    private void ReleaseUnmanagedResources()
    {
        ImGui.Unindent();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}