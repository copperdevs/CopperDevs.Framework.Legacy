using CopperFramework.Rendering;

namespace CopperFramework.Ui;

public class Text : UiElement
{
    public Color TextColor = Color.Black;
    public string TextValue = "";
    public int FontSize = 16;
    public float TextSpacing = 8;

    
    public override void DrawElement()
    {
        Raylib.DrawTextEx(RenderingSystem.Instance.GetRenderableItems<Font>()[0], TextValue, ScaledPosition, FontSize, TextSpacing, TextColor);
    }

    public Text(string name) : base(name)
    {
    }
}