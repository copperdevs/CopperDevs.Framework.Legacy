using CopperDevs.Core.Data;
using CopperDevs.Framework.Components;

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

    public List<float> FloatList = [0, 1, 2];

    public List<int> IntList = [0, 1, 2];

    public List<Vector2> Vector2List = [Vector2.Zero, Vector2.One, Vector2.One * 2];
}