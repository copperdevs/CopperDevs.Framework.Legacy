using ImGuiNET;

namespace CopperFramework.Renderer.DearImGui.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class SeperatorAttribute : Attribute
{
    private string? seperatorText;

    public SeperatorAttribute()
    {
        seperatorText = null;
    }

    public SeperatorAttribute(string text)
    {
        seperatorText = text;
    }

    internal void Render()
    {
        switch (seperatorText is null)
        {
            case true:
                ImGui.Separator();
                break;
            case false:
                ImGui.SeparatorText(seperatorText);
                break;
        }
    }
}