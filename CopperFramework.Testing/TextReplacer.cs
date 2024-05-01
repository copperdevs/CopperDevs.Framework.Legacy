
namespace CopperFramework.Testing;

public class TextReplacer : GameComponent
{
    [Exposed] private string input = "";
    [Exposed] private string output = "";

    public override void Update()
    {
        output = input.ToTitleCase();
    }
}