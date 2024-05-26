
namespace CopperDevs.Framework.Ui;

public class UiScreen : IEnumerable
{
    public string DisplayName;
    public string ScreenId;

    public List<UiElement> UiElements = [];

    public UiScreen(string screenId, string displayName)
    {
        ScreenId = screenId;
        DisplayName = displayName;
    }

    public void Register() => UiRendererSystem.Instance.RegisterUiScreen(this);
    public void Load() => UiRendererSystem.Instance.ChangeActiveScreen(this);

    public void Add(UiElement element)
    {
        UiElements.Add(element);
    }

    public IEnumerator GetEnumerator() => UiElements.GetEnumerator();
}