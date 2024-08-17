using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Rendering;

namespace CopperDevs.Framework.Ui;

public class Text : UiElement
{
    public string TextValue = "";
    public Color TextColor = Color.Black;
    public float FontSize = 48;

    public override void DrawElement()
    {
        UiDrawer.DrawText(TextValue, ScaledPosition, ScaledSize, TextColor, FontSize);
    }

    public Text() : base("Unnamed Text")
    {
    }

    public Text(string name) : base(name)
    {
    }
}