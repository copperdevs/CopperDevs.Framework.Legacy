using ImGuiNET;

namespace CopperFramework.Util;

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