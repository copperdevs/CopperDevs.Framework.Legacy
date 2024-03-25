using ImGuiNET;

namespace CopperFramework.Utility;

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