using CopperFramework.Elements.Components;
using Raylib_cs;
using Color = CopperFramework.Data.Color;

namespace CopperFramework.Testing;

public class SquareComponent : GameComponent
{
    private Color squareColor = Color.Red;

    public override void Update()
    {
        Raylib.DrawRectangle(-1, -1, 2, 2, squareColor);
    }
}