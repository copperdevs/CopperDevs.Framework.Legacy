using System.Text.RegularExpressions;

namespace CopperFramework.Utility;

public static partial class Util
{
    public static bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
    {
        return potentialDescendant.IsSubclassOf(potentialBase)
               || potentialDescendant == potentialBase;
    }
    
    public static string ConvertToTitleCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;
        
        // Using regular expressions to split the string by camelCase
        var result = TitleCaseRegex().Replace(input, " $1");
        
        // Capitalizing the first character and lowercasing the rest
        result = char.ToUpper(result[0]) + result[1..].ToLower();
        return result;
    }

    [GeneratedRegex("(\\B[A-Z])")]
    private static partial Regex TitleCaseRegex();
}