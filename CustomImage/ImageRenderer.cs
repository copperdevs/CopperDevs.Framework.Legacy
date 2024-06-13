using CopperDevs.Core.Data;
using CopperDevs.Framework.Elements.Components;

namespace CustomImage;

public class ImageRenderer : GameComponent
{
    public ImageData Data;

    public override void Update()
    {
        foreach (var pixel in Data.Pixels)
        {
            DrawPixel(pixel.Position, pixel.Color);
        }
    }

    private void DrawPixel(Vector2Int position, Color color)
    {
        rlGraphics.DrawRectangle(-2 + position.X, -2 + position.Y, 1, 1, color);
    }
}