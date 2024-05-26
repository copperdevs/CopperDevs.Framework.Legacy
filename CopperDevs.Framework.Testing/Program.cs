using CopperDevs.Framework;

namespace CopperDevs.Framework.Testing;

public static class Program
{
    public static void Main()
    {
        var engine = new Engine(EngineSettings.Development);

        var uiScreen = new UiScreen("testing-screen", "Testing Screen")
        {
            new Box("Background")
            {
                Size = new Vector2(.98f, .975f),
                Position = new Vector2(0.01f),
                Color = Color.Black
            },
            new Button("Quit Button")
            {
                BackgroundColor = Color.Red,
                Size = new Vector2(.2f, .1f),
                Position = new Vector2(.4f, .65f),
                ClickAction = () => engine.ShouldRun = false
            },
            new Text("Title Text")
            {
                Position = new Vector2(.07f, .1f),
                TextColor = Color.White,
                TextValue = "The quick brown fox jumps over the lazy dog",
                FontSize = 48
            }
        };

        engine.OnLoad += () =>
        {
            uiScreen.Register();
            uiScreen.Load();
        };

        engine.Run();
    }
}