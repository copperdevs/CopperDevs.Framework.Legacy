using System.Numerics;
using ImGuiNET;

namespace CopperFramework.Renderer.DearImGui.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class SpaceAttribute : Attribute
{
    private Vector2 spacing;

    public SpaceAttribute()
    {
        spacing = new Vector2(0, 20);
    }

    public SpaceAttribute(float space)
    {
        spacing = new Vector2(0, space);
    }

    internal void Render()
    {
        ImGui.Dummy(spacing);
    }
}