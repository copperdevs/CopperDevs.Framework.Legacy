using System.Text.Json;
using CopperFramework.Elements.Systems;
using CopperFramework.Ui;
using CopperFramework.Ui.Serialized;

namespace CopperFramework.UiCreator;

public class UICManagementSystem : BaseSystem<UICManagementSystem>
{
    public UiScreen UiScreen = new();
    
    internal static List<Type> ElementTypes { get; private set; } = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(assembly => assembly.GetTypes())
        .Where(type => type.IsSubclassOf(typeof(UiElement))).ToList();

    public override SystemUpdateType GetUpdateType() => SystemUpdateType.Renderer;

    public void LoadFile(string path)
    {
        // UiScreen = JsonSerializer.Deserialize<SerializedUiScreen>(File.ReadAllText(path))?.DeSerialize()!;
    }

    public void SaveCurrentFile(string path)
    {
        File.WriteAllText(path, UiScreen.ToJson());
    }

    public override void UpdateSystem()
    {
        foreach (var element in UiScreen.UiElements) 
            element.DrawElement();
    }
}