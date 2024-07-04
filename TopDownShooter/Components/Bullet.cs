using CopperDevs.Core.Data;
using CopperDevs.Framework.Scenes;

namespace TopDownShooter.Components;

public class Bullet : GameComponent
{
    public override void Update()
    {
        Transform.Position += (Transform.Rotation.ToRotatedUnitVector() * 2048) * Time.DeltaTime;
        rlGraphics.DrawCircleV(Vector2.Zero, 6, Color.RayWhite);

        Parallel.ForEach(ParentScene?.GetAllComponents<Enemy>()!, enemy =>
        {
            if (Vector2.Distance(Transform.Position, enemy.GetTransform().Position) <= 32)
                ParentScene?.Remove(enemy.GetParent());
        });
        SizeCheck(Engine.Instance.WindowSize);
    }

    private void SizeCheck(Vector2Int size)
    {
        if (Transform.Position.X > size.X || Transform.Position.X < 0 || Transform.Position.Y > size.Y || Transform.Position.Y < 0)
            SceneManager.ActiveScene.Remove(Parent);
    }
}