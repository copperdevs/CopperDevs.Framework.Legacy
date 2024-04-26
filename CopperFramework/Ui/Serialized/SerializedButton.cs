namespace CopperFramework.Ui.Serialized;

public class SerializedButton
{
    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    
    public SerializedButton(Button button)
    {
        Name = button.Name;
        Position = button.Position;
        Size = button.Size;
    }
}