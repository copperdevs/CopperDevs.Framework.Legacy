using CopperDevs.Framework.Scenes;

namespace TopDownShooter.Components;

public class Bullet : GameComponent
{
    [Exposed] private float maxDistance = 2048;
    [Exposed] private Vector2 startPosition;

    public override void Start()
    {
        startPosition = Transform.Position;
    }

    public override void Update()
    {
        Transform.Position += (Transform.Rotation.ToRotatedUnitVector() * 2048) * Time.DeltaTime;
        rlGraphics.DrawCircleV(Vector2.Zero, 6, Color.RayWhite);

        if (Vector2.Distance(startPosition, Transform.Position) > maxDistance)
        {
            SceneManager.ActiveScene.Remove(Parent);
        }

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (var enemy in ParentScene?.GetAllComponents<Enemy>()!)
        {
            if (Vector2.Distance(Transform.Position, enemy.GetTransform().Position) <= 32)
                ParentScene?.Remove(enemy.GetParent());
        }
    }

    public void UpdateStartPosition(Vector2 newStartPos)
    {
        startPosition = newStartPos;
    }
}