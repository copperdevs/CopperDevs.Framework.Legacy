using CopperDevs.Core.Utility;

namespace TopDownShooter.Components;

public class MouseDrawer : GameComponent
{
    [Exposed] private float smoothingTime = 8;
    [Exposed] private Color color = Color.White;

    public override void Start()
    {
        Transform.Scale = 8;
    }

    public override void Update()
    {
        Transform.Position = MathUtil.Lerp(Transform.Position, Input.MousePosition, Time.DeltaTime * smoothingTime);
        Raylib.DrawCircleV(Vector2.Zero, 1, color);
    }

    public override void DebugUpdate()
    {
        Raylib.DrawCircleV(Input.MousePosition.FlipY(), 8, Color.Red);
    }
}