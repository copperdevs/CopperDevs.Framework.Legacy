using System.Text.RegularExpressions;

namespace CopperFramework.Utility;

public static partial class Util
{
    public static bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
    {
        return potentialDescendant.IsSubclassOf(potentialBase)
               || potentialDescendant == potentialBase;
    }
}