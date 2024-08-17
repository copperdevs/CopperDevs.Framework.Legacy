using CopperDevs.Framework.Components;
using CopperDevs.Framework.Physics;
using CopperDevs.Framework.Scenes;

namespace CopperDevs.Framework.Testing;

public static class Program
{
    public static void Main()
    {
        var engine = new Engine(EngineSettings.Development);

        engine.OnLoad += EngineLoad;

        engine.Run();
    }

    public static void EngineLoad()
    {
        var emptyScene = new Scene("Empty");

        var uiTesting = new Scene("Ui")
        {
            new UiRenderer(new UiScreen("Testing Screen", "testing-screen")
            {
                new Box("Background")
                {
                    Position = new Vector2(0.005f),
                    Size = new Vector2(0.990f),
                    Color = Color.Black
                }
            })
        };

        var physicsTesting = new Scene("Physics")
        {
            new GameObject("Dynamic Rigidbody", new Transform { Scale = new Vector2(100), Position = new Vector2(0, -50) })
            {
                new BoxCollider(),
                new Rigidbody
                {
                    IsStatic = false
                },
                // new SquareComponent
                // {
                //     SquareColor = Color.Red
                // }
            },
            new GameObject("Ground Rigidbody", new Transform { Scale = new Vector2(200), Position = new Vector2(0, 400) })
            {
                new BoxCollider(),
                new Rigidbody
                {
                    IsStatic = true
                },
                // new SquareComponent
                // {
                //     SquareColor = Color.Black
                // }
            }
        };

        // emptyScene.Load();
        uiTesting.Load();
    }
}