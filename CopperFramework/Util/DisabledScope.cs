using ImGuiNET;

namespace CopperPlatformer.Core.Utility;

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