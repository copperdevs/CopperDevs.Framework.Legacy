using CopperDevs.DearImGui;
using CopperDevs.Framework.Components;
using Raylib_CSharp.Interact;

namespace TopDownShooter.Components;

public class EnemyManager : SingletonGameComponent<EnemyManager>
{
    [Seperator("Spawn Position")]
    [Exposed] public Vector2 EnemyTargetPosition;
    [Exposed] private Vector2 spawnRadius = new(1024, 2048);
    private List<Vector2> previousSpawnPositions = [];

    [Seperator("Spawn Time")]
    [Exposed] private bool spawnEnemies = true;
    [Exposed] private Vector2 spawnTimeRange = new(0.5f, 1.35f);
    [Exposed] [ReadOnly] private float currentTime;
    [Exposed] [ReadOnly] private float targetTime;

    public override void Update()
    {
        if (previousSpawnPositions.Count > 8)
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

        rlGraphics.DrawCircleLinesV(EnemyTargetPosition, spawnRadius.X, Color.Green);
        rlGraphics.DrawCircleLinesV(EnemyTargetPosition, spawnRadius.Y, Color.DarkGreen);

        rlGraphics.DrawCircleV(EnemyTargetPosition, 8, Color.Red);

        foreach (var position in previousSpawnPositions)
        {
            rlGraphics.DrawCircleV(position, 4, Color.Red);
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