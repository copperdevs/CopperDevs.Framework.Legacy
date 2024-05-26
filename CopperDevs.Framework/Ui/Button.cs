using CopperDevs.DearImGui;

namespace CopperDevs.Framework.Ui;

public class Button : UiElement
{
    public Color BackgroundColor = Color.White;
    public Action ClickAction = null!;

    public override void DrawElement()
    {
        Raylib.DrawRectangleV(ScaledPosition, ScaledSize, BackgroundColor);

        if (!MouseInsideButtonArea()) 
            return;
        
        Raylib.DrawCircleV(Input.MousePosition, 8, Color.White);
            
        if(Raylib.IsMouseButtonPressed(MouseButton.Left))
            ClickAction?.Invoke();
    }

    private bool MouseInsideButtonArea()
    {
        if (CopperImGui.AnyElementHovered)
            return false;

        return Raylib.CheckCollisionPointRec(Input.MousePosition, new Rectangle(ScaledPosition, ScaledSize));
    }

    public Button() : base("Unnamed Button")
    {
        
    }
    
    public Button(string name) : base(name)
    {
    }
}