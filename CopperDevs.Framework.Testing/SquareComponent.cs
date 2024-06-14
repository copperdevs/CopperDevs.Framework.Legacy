namespace CopperDevs.Framework.Testing;

public class SquareComponent : GameComponent
{
    [Exposed] public Color SquareColor = Color.Red;

    public override void Update()
    {
        rlGraphics.DrawRectangle(-1, -1, 2, 2, SquareColor);
    }
}