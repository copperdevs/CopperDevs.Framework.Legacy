using CopperFramework.Ui.Serialized;

namespace CopperFramework.Ui;

public class UiScreen
{
    public string DisplayName = "Untitled Ui Screen";
    public string ScreenId = "untitled-ui-screen";

    public List<UiElement> UiElements = new();

    public string ToJson()
    {
        return new SerializedUiScreen(this).ToJson();
    }
}