using ImGuiNET;

namespace CopperFramework.Util;

public class DisabledScope : Scope
{
    public DisabledScope()
    {
        ImGui.BeginDisabled();
    }
    
    protected override void CloseScope()
    {
        ImGui.EndDisabled();
    }
}