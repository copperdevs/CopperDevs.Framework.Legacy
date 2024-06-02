using CopperDevs.DearImGui;
using Raylib_CSharp.Collision;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Transformations;

namespace CopperDevs.Framework.Ui;

public class Button : UiElement
{
    public Color BackgroundColor = Color.White;
    public Action ClickAction = null!;

    public override void DrawElement()
    {
        rlGraphics.DrawRectangleV(ScaledPosition, ScaledSize, BackgroundColor);

        if (!MouseInsideButtonArea()) 
            return;
        
        rlGraphics.DrawCircleV(Input.MousePosition, 8, Color.White);
            
        if(rlInput.IsMouseButtonPressed(MouseButton.Left))
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