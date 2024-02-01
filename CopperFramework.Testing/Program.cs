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
                }
            };
        });
        Framework.Run();
    }
}