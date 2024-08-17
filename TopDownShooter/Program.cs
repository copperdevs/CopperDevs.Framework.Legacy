using CopperDevs.Framework.Components;
using CopperDevs.Framework.Rendering;
using CopperDevs.Framework.Scenes;
using Raylib_CSharp.Windowing;
using TopDownShooter.Components;

namespace TopDownShooter;

public static class Program
{
    public static void Main()
    {
        var engineSettings = new EngineSettings
        {
            DisableDevTools = false,
            EnableDevToolsAtStart = true,
            WindowTitle = "Copper Framework - Top Down Shooter",
            TargetFps = -1
        };

        var engine = new Engine(engineSettings);
        engine.SetBackgroundColor(Color.Black);
        engine.AddScreenShader(IncludedShaders.Bloom);


        var mainMenu = new Scene("Main Menu", "main-menu")
        {
            new GameObject("Ui")
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
                        TextColor = Color.Red,
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
            new EnemyManager(),
            new GameObject("Mouse Controller")
            {
                new MouseDrawer(),
                new ParticleSystem()
                {
                    MaxParticles = 300,
                    LifetimeRandomRange = new Vector2(0.010f, 0.115f),
                    SpeedRandomRange = new Vector2(130, 240),
                    SizeRandomRange = new Vector2(8, 14),
                    ParticleColors = [new Color(243, 243, 243, 50)]
                }
            },
            new GameObject("Player Object")
            {
                new PlayerController(),
                new PlayerWeapon()
            },
            new GameObject("Player Trail")
            {
                new PlayerPositionCopier(),
                new ParticleSystem()
                {
                    MaxParticles = 186,
                    LifetimeRandomRange = new Vector2(0.010f, 0.115f),
                    SpeedRandomRange = new Vector2(87, 237),
                    SizeRandomRange = new Vector2(16, 41),
                    ParticleColors = [new Color(328, 17, 17, 25), new Color(237, 108, 3, 16), new Color(216, 243, 7, 11)]
                }
            }
        };

        var enemyTestingScene = new Scene("Enemy Testing", "enemy-testing")
        {
            new EnemyManager()
        };

        // engine.SetWindowColor(new Color(0, 0, 0, 0));

        engine.Run();
    }
}