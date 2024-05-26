using CopperDevs.Framework.Rendering;

namespace CopperDevs.Framework.Ui;

public class Text : UiElement
{
    public string TextValue = "";
    public int FontSize = 16;
    public float TextSpacing = 8;
    public Color TextColor = Color.Black;
    
    public override void DrawElement()
    {
        Raylib.DrawTextEx(RenderingSystem.Instance.GetRenderableItems<Font>()[0], TextValue, ScaledPosition, FontSize, TextSpacing, TextColor);
    }

    public Text() : base("Unnamed Text")
    {
        
    }
    
    public Text(string name) : base(name)
    {
    }
}