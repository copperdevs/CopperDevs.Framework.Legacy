using CopperDevs.Framework.Components;
using CopperDevs.Framework.Scenes;

namespace TopDownShooter.Components;

public class Enemy : GameComponent
{
    private Vector2 TargetPosition => EnemyManager.Instance.EnemyTargetPosition;

    public override void Start()
    {
        Transform.Scale = 16;
    }

    public override void Update()
    {
        Transform.LookAt(TargetPosition);
        Transform.Position += Transform.Rotation.ToRotatedUnitVector() * 768 * Time.DeltaTime;

        rlGraphics.DrawCircle(0, 0, 1, Color.Blue);

        if (Transform.Distance(TargetPosition) < 32)
            SceneManager.ActiveScene.Remove(Parent); }

    public override void DebugUpdate()
    {
        rlGraphics.DrawLineV(Transform.Position, TargetPosition, Color.Green);
    }
}