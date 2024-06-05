using CopperDevs.Framework.Rendering;

namespace CopperDevs.Framework.Ui;

public static class UiDrawer
{
    public static void DrawText(string textValue, Vector2 scaledPosition, Vector2 scaledSize, rlColor textColor, float fontSize)
    {
        float scaleFactor = (float)rlWindow.GetScreenWidth() / 1920;

        int scaledFontSize = (int)(fontSize * scaleFactor);
        
        rlGraphics.DrawTextEx(
            RenderingSystem.Instance.GetRenderableItems<Font>()[0],
            textValue,
            scaledPosition,
            scaledFontSize,
            scaledSize.X / 240,
            textColor);
    }
}