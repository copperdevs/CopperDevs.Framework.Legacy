namespace CopperFramework.Ui;

public class Box : UiElement
{
    public Color Color = Color.White;
    
    public override void DrawElement()
    {
        Raylib.DrawRectangleV(ScaledPosition, ScaledSize, Color);
    }
}