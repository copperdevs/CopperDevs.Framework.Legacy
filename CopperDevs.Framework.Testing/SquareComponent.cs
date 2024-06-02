namespace CopperDevs.Framework.Testing;

public class SquareComponent : GameComponent
{
    [Exposed] private Color squareColor = Color.Red;

    public override void Update()
    {
        rlGraphics.DrawRectangle(-1, -1, 2, 2, squareColor);
    }
}