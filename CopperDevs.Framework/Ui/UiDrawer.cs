using CopperDevs.Framework.Rendering;

namespace CopperDevs.Framework.Ui;

public static class UiDrawer
{
    public static void DrawText(string textValue, Vector2 scaledPosition, Vector2 scaledSize, float textScale, rlColor textColor)
    {
        rlGraphics.DrawTextEx(
            RenderingSystem.Instance.GetRenderableItems<Font>()[0],
            textValue, 
            scaledPosition,
            (((scaledSize.X / textValue.Length) + scaledSize.X / 240) * 1.75f) * textScale,
            scaledSize.X / 240,
            textColor);
    }
}