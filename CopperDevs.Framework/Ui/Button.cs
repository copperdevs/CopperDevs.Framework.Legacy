using CopperDevs.DearImGui;
using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Rendering;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Transformations;

namespace CopperDevs.Framework.Ui;

public class Button : UiElement
{
    public Color BackgroundColor = Color.White;
    public Color HoverColor = Color.RayWhite;
    public string TextValue = "";
    public Color TextColor = Color.Black;
    public float FontSize = 48;
    [HideInInspector] public Action ClickAction = null!;

    public override void DrawElement()
    {
        var insideButtonArea = MouseInsideButtonArea();
        
        rlGraphics.DrawRectangleV(ScaledPosition, ScaledSize, insideButtonArea ? HoverColor : BackgroundColor);   
        UiDrawer.DrawText(TextValue, ScaledPosition, ScaledSize, TextColor, FontSize);

        if (Input.IsMouseButtonPressed(MouseButton.Left) && insideButtonArea)
            ClickAction?.Invoke();
    }

    private bool MouseInsideButtonArea()
    {
        return !CopperImGui.AnyElementHovered && rlCollision.CheckCollisionPointRec(Input.MousePosition, new Rectangle(ScaledPosition.X, ScaledPosition.Y, ScaledSize.X, ScaledSize.Y));
    }

    public Button() : base("Unnamed Button")
    {
    }

    public Button(string name) : base(name)
    {
    }
}