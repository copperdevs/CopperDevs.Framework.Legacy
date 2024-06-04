namespace CopperDevs.Framework.Ui;

public class UiScreen : IEnumerable<UiElement>
{
    public string DisplayName;
    public string ScreenId;

    public readonly List<UiElement> UiElements = [];

    public UiScreen(string screenId, string displayName)
    {
        ScreenId = screenId;
        DisplayName = displayName;
    }

    public void Add(UiElement element)
    {
        UiElements.Add(element);
    }
    
    public IEnumerator<UiElement> GetEnumerator() => UiElements.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}