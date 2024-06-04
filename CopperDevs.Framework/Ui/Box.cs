namespace CopperDevs.Framework.Ui;

public class Box : UiElement
{
    public Color Color = Color.White;

    public Box() : base("Unnamed Box")
    {
    }

    public Box(string name) : base(name)
    {
    }

    public override void DrawElement()
    {
        rlGraphics.DrawRectangleV(ScaledPosition, ScaledSize, Color);
    }
}