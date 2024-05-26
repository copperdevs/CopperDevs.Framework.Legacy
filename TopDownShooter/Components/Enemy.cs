using CopperDevs.Core.Utility;
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
        Raylib.DrawCircle(0, 0, 1, Color.Blue);

        if (Transform.Distance(TargetPosition) < 32) 
            SceneManager.ActiveScene.Remove(Parent);
    }

    public override void FixedUpdate()
    {
        Transform.LookAt(TargetPosition);
        Transform.Position += Transform.Rotation.ToRotatedUnitVector() * 4;
    }

    public override void DebugUpdate()
    {
        Raylib.DrawLineV(Transform.Position.FlipY(), TargetPosition.FlipY(), Color.Green);
    }
}