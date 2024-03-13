using SystemRandom = System.Random;

namespace CopperFramework.Util;

public static class Random
{
    private static readonly SystemRandom SystemRandom = new(new Guid().GetHashCode());

    public static float Range(float min, float max)
    {
        if ((int)(MathF.Round(min * 1000) / 1000) == (int)(MathF.Round(max * 1000) / 1000))
            return (min + max) / 2;

        var lowerLimit = MathF.Min(min, max);
        var upperLimit = MathF.Max(min, max);

        return SystemRandom.NextSingle() * (upperLimit - lowerLimit) + lowerLimit;
    }

    public static int Range(int min, int max)
    {
        if (min == max)
            return (min + max) / 2;

        var lowerLimit = Math.Min(min, max);
        var upperLimit = Math.Max(min, max);

        return SystemRandom.Next(lowerLimit, upperLimit);
    }

    public static float Range(float max)
    {
        return Range(0, max);
    }

    public static int Range(int max)
    {
        return Range(0, max);
    }

    public static float Range(Vector2 valueRange)
    {
        return Range(valueRange.X, valueRange.Y);
    }

    public static T Item<T>(List<T> items)
    {
        return items[SystemRandom.Next(items.Count - 1)];
    }
}