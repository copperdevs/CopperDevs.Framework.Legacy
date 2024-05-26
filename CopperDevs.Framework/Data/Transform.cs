namespace CopperDevs.Framework.Data;

public struct Transform
{
    public Vector2 Position = default;
    public float Rotation = 0;
    public float Scale = 1;

    public Transform()
    {
        Position = default;
        Rotation = 0;
        Scale = 1;
    }

    public void LookAt(Vector2 point)
    {
        Rotation = -MathF.Atan2(Position.Y - point.Y, Position.X - point.X) * (180 / MathF.PI) + 180;
    }

    public float Distance(Vector2 position)
    {
        return Vector2.Distance(Position, position);
    }
}