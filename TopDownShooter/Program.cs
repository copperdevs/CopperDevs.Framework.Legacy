using CopperDevs.Framework.Scenes;
using Raylib_CSharp.Windowing;
using TopDownShooter.Components;

namespace TopDownShooter;

public static class Program
{
    public static void Main()
    {
        var engineSettings = new EngineSettings()
        {
            DisableDevTools = false,
            EnableDevToolsAtStart = true,
            WindowTitle = "Copper Framework - Top Down Shooter"
        };

        var engine = new Engine(engineSettings);
        engine.SetWindowColor(Color.Black);
        engine.SetWindowShader(Shader.IncludedShaders.Bloom);


        var mainMenu = new Scene("Main Menu", "main-menu")
        {
            new("Ui")
            {
                new UiRenderer(new UiScreen("main-menu-screen", "Main Menu Screen")
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
                        Size = new Vector2(.185f, .185f),
                        Position = new Vector2(.170f, .670f),
                        ClickAction = () => engine.ShouldRun = false,
                        TextValue = "Quit",
                        TextColor = Color.White,
                        FontSize = 220
                    },
                    new Button("Play Button")
                    {
                        BackgroundColor = Color.White,
                        HoverColor = Color.RayWhite,
                        Size = new Vector2(.185f, .185f),
                        Position = new Vector2(.075f, .435f),
                        ClickAction = () => SceneManager.LoadScene("game-scene"),
                        TextValue = "Play",
                        TextColor = Color.Black,
                        FontSize = 220
                    },
                    new Text("Title Text")
                    {
                        Position = new Vector2(.07f, .1f),
                        TextColor = Color.White,
                        TextValue = "coppers top down funny shooter game",
                        Size = new Vector2(0.6f, .1f),
                        FontSize = 96,
                    }
                })
            }
        };
        
        var gameScene = new Scene("Game Scene", "game-scene")
        {
            new("Player Object")
            {
                new PlayerController(),
                new PlayerWeapon()
            },
            new("Mouse Manager")
            {
                new MouseDrawer()
            }
        };

        var enemyTestingScene = new Scene("Enemy Testing", "enemy-testing")
        {
            new("Enemy Spawner")
            {
                new EnemyManager()
            }
        };

        engine.Run();
    }
}