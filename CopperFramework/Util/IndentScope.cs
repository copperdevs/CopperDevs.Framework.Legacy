using ImGuiNET;

namespace CopperPlatformer.Core.Utility;

public class IndentScope : Scope
{
    public IndentScope()
    {
        ImGui.Indent();
    }
    
    protected override void CloseScope()
    {
        ImGui.Unindent();
    }
}