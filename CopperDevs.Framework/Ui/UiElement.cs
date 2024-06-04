using CopperDevs.Core.Utility;
using CopperDevs.DearImGui.Attributes;

namespace CopperDevs.Framework.Ui;

public abstract class UiElement
{
    public string Name;

    [Range(0, 1, TargetRangeType = RangeType.Drag, Speed = 0.005f)] public Vector2 Position;
    [Range(0, 1, TargetRangeType = RangeType.Drag, Speed = 0.005f)] public Vector2 Size;
    
    public Vector2 ScaledPosition => Position.Remap(Vector2.Zero, Vector2.One, Vector2.Zero, EngineWindow.Size);
    public Vector2 ScaledSize => Size.Remap(Vector2.Zero, Vector2.One, Vector2.Zero, EngineWindow.Size);

    public abstract void DrawElement();

    protected UiElement(string name)
    {
        Name = name;
    }
}