namespace CopperDevs.Framework.Testing;

public class CircleComponent : GameComponent
{
    [Exposed] private Color circleColor = Color.Red;

    public override void Update()
    {
        rlGraphics.DrawCircle(0, 0, 1, circleColor);
    }
}