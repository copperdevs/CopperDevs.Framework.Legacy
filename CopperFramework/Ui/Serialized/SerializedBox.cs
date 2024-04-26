namespace CopperFramework.Ui.Serialized;

public class SerializedBox
{
    public string Name { get; set; }
    public SerializedVector2 Position { get; set; }
    public SerializedVector2 Size { get; set; }
    public Color Color { get; set; }
    
    public SerializedBox(Box box)
    {
        Name = box.Name;
        Position = box.Position;
        Size = box.Size;
        Color = box.Color;
    }
}