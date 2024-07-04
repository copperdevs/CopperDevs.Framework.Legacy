namespace CopperDevs.Framework.Physics;

public class BoxCollider : Collider
{
    public override void Update()
    {
        // rlGraphics.DrawRectangle(-1, -1, 2, 2, Color.Green);
        // rlGraphics.DrawRectangle(0, 0, 1, 1, Color.Green);
        rlGraphics.DrawRectangleV(new Vector2(-.5f), new Vector2(1f), Color.Green);
    }
}