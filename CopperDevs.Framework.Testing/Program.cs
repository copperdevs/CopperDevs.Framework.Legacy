using CopperDevs.Framework.Physics;
using CopperDevs.Framework.Scenes;

namespace CopperDevs.Framework.Testing;

public static class Program
{
    public static void Main()
    {
        var engine = new Engine(EngineSettings.Development);
        
        engine.OnLoad += () => EngineLoad(engine);
        
        engine.Run();

        // var engine = new OldEngine(EngineSettings.Development);
        // engine.OnLoad += () => EngineLoad(engine);
        // engine.Run();
    }

    public static void EngineLoad(Engine engine)
    {
        var emptyScene = new Scene("Empty");
            
        var starterScene = new Scene("Ui Testing")
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

        var physicsTesting = new Scene("Physics")
        {
            new("Dynamic Rigidbody", new Transform { Scale = 100, Position = new Vector2(0, -50) })
            {
                new Rigidbody
                {
                    isStatic = false
                },
                new SquareComponent
                {
                    SquareColor = Color.Red
                }
            },
            new("Ground Rigidbody", new Transform { Scale = 50, Position = new Vector2(0, 400) })
            {
                new Rigidbody
                {
                    isStatic = true
                },
                new SquareComponent
                {
                    SquareColor = Color.Black
                }
            }
        };

        emptyScene.Load();
    }
}