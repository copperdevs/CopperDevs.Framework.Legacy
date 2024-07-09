namespace CopperDevs.Framework.Physics;

public class BoxCollider : Collider
{
    private Color color = Color.Green;
    
    public override void Update()
    {
        rlGraphics.DrawRectangle(-1, -1, 2, 2, Color.Green);
    }
}