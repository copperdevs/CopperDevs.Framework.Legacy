using CopperDevs.Core.Utility;
using CopperDevs.DearImGui;

namespace TopDownShooter.Components;

public class EnemyManager : SingletonGameComponent<EnemyManager>
{
    [Seperator("Spawn Position")]
    [Exposed] public Vector2 EnemyTargetPosition;
    [Exposed] private Vector2 spawnRadius = new(1024, 2048);
    private List<Vector2> previousSpawnPositions = new();

    [Seperator("Spawn Time")]
    [Exposed] private bool spawnEnemies = true;
    [Exposed] private Vector2 spawnTimeRange = new(0.5f, 1.35f);
    [Exposed] [ReadOnly] private float currentTime;
    [Exposed] [ReadOnly] private float targetTime;

    public override void Update()
    {
        if(previousSpawnPositions.Count > 8)
            previousSpawnPositions.RemoveAt(0);
        
        currentTime = Time.GameTime;

        if (!(currentTime >= targetTime))
            return;

        targetTime += Random.Range(spawnTimeRange);

        if (!spawnEnemies)
            return;

        var enemy = ComponentRegistry.Instantiate<Enemy>();
        ref var enemyTransform = ref enemy.GetTransform();
        enemyTransform.Position = GetRandomSpawnPosition();
    }

    public override void DebugUpdate()
    {
        if (!IsCurrentInspectionTarget())
            return;

        Raylib.DrawCircleLinesV(EnemyTargetPosition.FlipY(), spawnRadius.X, Color.Green);
        Raylib.DrawCircleLinesV(EnemyTargetPosition.FlipY(), spawnRadius.Y, Color.DarkGreen);

        Raylib.DrawCircleV(EnemyTargetPosition.FlipY(), 8, Color.Red);

        foreach (var position in previousSpawnPositions)
        {
            Raylib.DrawCircleV(position.FlipY(), 4, Color.Red);
        }

        if (!CopperImGui.AnyElementHovered && Input.IsMouseButtonDown(MouseButton.Left))
            EnemyTargetPosition = Input.MousePosition;
    }

    private Vector2 GetRandomSpawnPosition()
    {
        var value = Random.PointInAnnulus(EnemyTargetPosition, spawnRadius);
        previousSpawnPositions.Add(value);
        return value;
    }
}