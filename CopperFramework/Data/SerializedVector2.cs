namespace CopperFramework.Data;

public class SerializedVector2
{
    public float X { get; set; }
    public float Y { get; set; }

    public SerializedVector2() : this(0, 0)
    {
        
    }

    public SerializedVector2(float value) : this(value, value)
    {
        
    }
    
    public SerializedVector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static implicit operator Vector2(SerializedVector2 serializedVector2) => new(serializedVector2.X, serializedVector2.Y);
    public static implicit operator SerializedVector2(Vector2 vector2) => new(vector2.X, vector2.Y);
}