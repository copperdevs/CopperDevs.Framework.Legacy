using CopperDevs.DearImGui.Attributes;
using CopperDevs.Framework.Components;

namespace CopperDevs.Framework.Ui;

public class UiRenderer : GameComponent
{
    [Exposed] private UiScreen? screen;

    public UiRenderer(UiScreen targetScreen)
    {
        screen = targetScreen;
    }
    
    public UiRenderer()
    {
        var guid = Guid.NewGuid().ToString();
        screen = new UiScreen(guid, guid);
    }

    public override void UiUpdate()
    {
        if (screen is null)
            return;
        
        foreach (var uiElement in screen)
        {
            uiElement.DrawElement();
        }
    }
}