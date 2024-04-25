using CopperDearImGui.Attributes;

namespace CopperFramework.Ui;

public class UiElement
{
    public string Name = "Untitled UiElement";
    [Range(0, 1)] public Vector2 Position;
    [Range(0, 1)] public Vector2 Size;
}