namespace CopperDevs.Framework.Utility;

public static class Util
{
    public static IList<T> Swap<T>(IList<T> list, int indexA, int indexB)
    {
        (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
        return list;
    }
}