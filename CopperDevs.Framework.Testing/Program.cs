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
    }

    public static void EngineLoad(Engine engine)
    {
        var emptyScene = new Scene("Empty");

        var physicsTesting = new Scene("Physics")
        {
            new("Dynamic Rigidbody", new Transform { Scale = 100, Position = new Vector2(0, -50) })
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
            new("Ground Rigidbody", new Transform { Scale = 200, Position = new Vector2(0, 400) })
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

        emptyScene.Load();
    }
}