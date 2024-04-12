using CopperFramework.Elements.Components;
using CopperFramework.Utility;

namespace CopperFramework.Testing;

public class TextReplacer : GameComponent
{
    private string input = "";
    private string output = "";

    public override void Update()
    {
        output = input.ToTitleCase();
    }
}