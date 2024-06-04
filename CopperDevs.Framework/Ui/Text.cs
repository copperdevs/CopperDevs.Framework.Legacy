using CopperDevs.DearImGui.Attributes;

namespace CopperDevs.Framework.Ui;

public class Text : UiElement
{
    public string TextValue = "";
    public Color TextColor = Color.Black;
    public Color BackgroundColor = Color.Black;
    [Range(0, 1)] public float TextScale = 1;

    public override void DrawElement()
    {
        rlGraphics.DrawRectangleV(ScaledPosition, ScaledSize, BackgroundColor);
        UiDrawer.DrawText(TextValue, ScaledPosition, ScaledSize, TextScale, TextColor);
    }

    public Text() : base("Unnamed Text")
    {
    }

    public Text(string name) : base(name)
    {
    }
}