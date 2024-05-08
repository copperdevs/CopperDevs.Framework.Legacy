using CopperFramework.Scenes;
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
            TargetFps = 144,
            WindowTitle = "Copper Framework - Top Down Shooter"
        };
        
        var engine = new Engine(engineSettings);
        engine.SetWindowColor(Color.Black);
        engine.SetWindowShader(Shader.IncludedShaders.Bloom);

        var gameScene = new Scene("Game Scene")
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

        var enemyTestingScene = new Scene()
        {
            new("Enemy Spawner")
            {
                new EnemyManager()
            }
        };
        
        enemyTestingScene.Load();
        
        engine.Run();
    }
}