namespace CopperFramework.Physics;

public class BoxCollider : Collider
{
    public override void Update()
    {
    }

    public override void DebugUpdate()
    {
        Raylib.DrawRectangle(-1, -1, 2, 2, Color.Green);
    }
}