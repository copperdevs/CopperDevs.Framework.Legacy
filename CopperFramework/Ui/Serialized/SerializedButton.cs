namespace CopperFramework.Ui.Serialized;

public class SerializedButton
{
    public string Name { get; set; }
    public SerializedVector2 Position { get; set; }
    public SerializedVector2 Size { get; set; }
    
    public SerializedButton(Button button)
    {
        Name = button.Name;
        Position = button.Position;
        Size = button.Size;
    }
}