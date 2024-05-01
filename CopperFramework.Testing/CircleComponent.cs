
namespace CopperFramework.Testing;

public class CircleComponent : GameComponent
{
    [Exposed] private Color circleColor = Color.Red;
    
    public override void Update()
    {
        Raylib.DrawCircle(0, 0, 1, circleColor);
    }
}