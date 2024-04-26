namespace CopperFramework.Ui.Serialized;

public class SerializedText
{
    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    
    public SerializedText(Text text)
    {
        Name = text.Name;
        Position = text.Position;
        Size = text.Size;
    }
}