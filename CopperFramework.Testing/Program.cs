using System.Numerics;
using CopperFramework.Components;
using CopperFramework.Data;
using CopperFramework.Renderer;

namespace CopperFramework.Testing;

public static class Program
{
    public static void Main()
    {
        Framework.Load(() =>
        {
            var scene = new Scene("Test Scene")
            {
                new GameObject("Object One")
                {
                    new Model("Resources/Textures/silk.png", "Resources/Models/Cube.obj")
                },
                new GameObject("Camera")
                {
                    // Transform = { Position = new Vector3(0, 0.85f, 3), Rotation = new Vector3(-200f, 0, 180f)}
                    new Camera(),
                    new CameraController()
                },
                new GameObject("Plane")
                {
                    new Model("Resources/Textures/silk.png", "Resources/Models/Plane.obj")
                },
            };
        });
        Framework.Run();
    }
}