using CopperDevs.Core.Data;

namespace CopperDevs.Framework.Testing;

public class ReflectionTest : GameComponent
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

    public Vector2Int Vector2Int;
}