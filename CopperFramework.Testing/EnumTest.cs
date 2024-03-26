using CopperFramework.Elements.Components;

namespace CopperFramework.Testing;

public class EnumTest : GameComponent
{
    public MyEnum CurrentMyEnum = MyEnum.One;

    // out of sight out of mind
    // public List<MyEnum> ManyMyEnums = new()
    // {
        // MyEnum.One,
        // MyEnum.Two,
        // MyEnum.Three
    // };

    public enum MyEnum
    {
        One,
        Two,
        Three
    }
}