using CopperDevs.DearImGui;
using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Rendering;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Transformations;

namespace CopperDevs.Framework.Ui;

public class Button : UiElement
{
    [Seperator("Button Color Settings")]
    public Color BackgroundColor = Color.White;
    public Color HoverColor = Color.RayWhite;
    public Color ClickColor = Color.White;

    [Seperator("Button Tex Settings")]
    public string TextValue = "";
    public Color TextColor = Color.Black;
    public float FontSize = 48;

    [Seperator("Click Color Delay")]
    [Exposed] private float clickedColorTime = 0.025f;
    [Exposed] [ReadOnly] private float clickedColorTimer = 0;


    [HideInInspector] public Action ClickAction = null!;


    public override void DrawElement()
    {
        if (clickedColorTimer > 0)
            clickedColorTimer -= Time.DeltaTime;

        var insideButtonArea = MouseInsideButtonArea();
        var clicked = !(0 > clickedColorTimer);

        rlGraphics.DrawRectangleV(ScaledPosition, ScaledSize, clicked ? ClickColor : insideButtonArea ? HoverColor : BackgroundColor);
        UiDrawer.DrawText(TextValue, ScaledPosition, ScaledSize, TextColor, FontSize);

        if (Input.IsMouseButtonPressed(MouseButton.Left) && insideButtonArea)
        {
            ClickAction?.Invoke();
            clickedColorTimer = clickedColorTime;
        }
    }

    private bool MouseInsideButtonArea()
    {
        if ((CopperImGui.AnyElementHovered && Engine.Instance.DebugEnabled && !Engine.Instance.GameWindowHovered))
        {
            return false;
        }

        return rlCollision.CheckCollisionPointRec(Input.MousePosition, new Rectangle(ScaledPosition.X, ScaledPosition.Y, ScaledSize.X, ScaledSize.Y));
    }

    public Button() : base("Unnamed Button")
    {
    }

    public Button(string name) : base(name)
    {
    }
}