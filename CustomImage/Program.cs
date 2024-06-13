using CopperDevs.Core.Data;
using CopperDevs.Framework.Scenes;
using Raylib_CSharp.Windowing;

namespace CustomImage;

public static class Program
{
    public static void Main(string[] args)
    {
        var image = ImageData.LoadFromFile(args.Length is 0 ? "" : args[0]);

        var imageSize = image.GetImageSize();

        var engineSettings = new EngineSettings
        {
            WindowFlags = ConfigFlags.Msaa4XHint | ConfigFlags.VSyncHint | ConfigFlags.AlwaysRunWindow | ConfigFlags.TransparentWindow,
            DisableDevTools = false,
            EnableDevToolsAtStart = false,
            WindowTitle = "Copper Image",
            TargetFps = -1,
            WindowSize = new Vector2Int(200)
        };

        var engine = new Engine(engineSettings);

        var renderingScene = new Scene("Rendering Testing")
        {
            new("Image Renderer", new Transform { Scale = 100, Position = new Vector2(100) })
            {
                new ImageRenderer
                {
                    Data = image
                }
            }
        };

        renderingScene.Load();


        engine.Run();
    }
}