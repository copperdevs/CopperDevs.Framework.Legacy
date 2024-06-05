using CopperDevs.Framework.Scenes;

namespace CopperDevs.Framework.Testing;

public static class Program
{
    public static void Main()
    {
        var engine = new Engine(EngineSettings.Development);

        var starterScene = new Scene("Main")
        {
            new("Ui")
            {
                new UiRenderer(new UiScreen("testing-screen", "Testing Screen")
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
                        HoverColor = Color.LightRed,
                        Size = new Vector2(.1f, .1f),
                        Position = new Vector2(.185f, .180f),
                        ClickAction = () => engine.ShouldRun = false,
                        TextValue = "Quit",
                        TextColor = Color.White,
                        FontSize = 220
                    },
                    new Text("Title Text")
                    {
                        Position = new Vector2(.07f, .1f),
                        TextColor = Color.White,
                        TextValue = "The quick brown fox jumps over the lazy dog",
                        Size = new Vector2(0.6f, .1f)
                    }
                })
            }
        };

        engine.Run();
    }
}