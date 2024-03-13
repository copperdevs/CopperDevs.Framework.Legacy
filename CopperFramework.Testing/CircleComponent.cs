using CopperFramework.Elements.Components;
using Raylib_cs;
using Color = CopperFramework.Data.Color;

namespace CopperFramework.Testing;

public class CircleComponent : GameComponent
{
    private Color circleColor = Color.Red;
    
    public override void Update()
    {
        Raylib.DrawCircle((int)Transform.Position.X, (int)Transform.Position.Y, Transform.Scale, circleColor);
    }
}