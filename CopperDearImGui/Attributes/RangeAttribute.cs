namespace CopperDearImGui.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class RangeAttribute : Attribute
{
    public readonly float Min;
    public readonly float Max;

    public RangeAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }

    public RangeAttribute(float max) : this(0, max)
    {
    }
}