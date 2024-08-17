namespace CopperDevs.Framework.Ui;

public class UiScreen(string displayName, string screenId) : IEnumerable<UiElement>
{
    public readonly string DisplayName = displayName;
    public readonly string ScreenId = screenId;

    public readonly List<UiElement> UiElements = [];

    public void Add(UiElement? element)
    {
        if (element is not null)
            UiElements.Add(element);
    }

    public IEnumerator<UiElement> GetEnumerator() => UiElements.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}