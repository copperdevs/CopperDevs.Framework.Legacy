namespace CopperFramework.Testing;

public class SquareComponent : GameComponent
{
    [Exposed] private Color squareColor = Color.Red;

    public override void Update()
    {
        Raylib.DrawRectangle(-1, -1, 2, 2, squareColor);
    }
}