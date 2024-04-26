using System.Runtime.CompilerServices;
using System.Text.Json;

namespace CopperFramework.Ui.Serialized;

public class SerializedUiScreen
{
    public string DisplayName { get; set; }
    public string ScreenId { get; set; }

    public List<SerializedBox> SerializedBoxes { get; set; } = new();
    public List<SerializedButton> SerializedButtons { get; set; } = new();
    public List<SerializedText> SerializedTexts { get; set; } = new();

    public SerializedUiScreen(UiScreen uiScreen)
    {
        DisplayName = uiScreen.DisplayName;
        ScreenId = uiScreen.ScreenId;

        foreach (var element in uiScreen.UiElements)
        {
            switch (element)
            {
                case Box box:
                    SerializedBoxes.Add(new SerializedBox(box));
                    break;
                case Text text:
                    SerializedTexts.Add(new SerializedText(text));
                    break;
                case Button button:
                    SerializedButtons.Add(new SerializedButton(button));
                    break;
            }
        }
    }

    public string ToJson() => JsonSerializer.Serialize(this);
}