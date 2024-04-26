using CopperCore.Utility;

namespace CopperFramework.Utility;

public static class Extensions
{
    public static string ToFancyString(this IEnumerable<byte> array) => array.Aggregate("", (current, item) => current + $"<{item}>,");

    public static string CapitalizeFirstLetter(this string message)
    {
        return message.Length switch
        {
            0 => "",
            1 => char.ToUpper(message[0]).ToString(),
            _ => char.ToUpper(message[0]) + message[1..]
        };
    }

    public static Vector2 Remap(this Vector2 input, Vector2 inputMin, Vector2 inputMax, Vector2 outputMin, Vector2 outputMax) =>
        MathUtil.ReMap(input, inputMin, inputMax, outputMin, outputMax);

    public static Matrix4x4 ToRowMajor(this Matrix4x4 columnMatrix)
    {
        return new Matrix4x4(
            columnMatrix.M11, columnMatrix.M21, columnMatrix.M31, columnMatrix.M41,
            columnMatrix.M12, columnMatrix.M22, columnMatrix.M32, columnMatrix.M42,
            columnMatrix.M13, columnMatrix.M23, columnMatrix.M33, columnMatrix.M43,
            columnMatrix.M14, columnMatrix.M24, columnMatrix.M34, columnMatrix.M44
        );
    }

    // Extension method to convert a row-major Matrix4x4 to a column-major Matrix4x4
    public static Matrix4x4 ToColumnMajor(this Matrix4x4 rowMatrix)
    {
        return new Matrix4x4(
            rowMatrix.M11, rowMatrix.M12, rowMatrix.M13, rowMatrix.M14,
            rowMatrix.M21, rowMatrix.M22, rowMatrix.M23, rowMatrix.M24,
            rowMatrix.M31, rowMatrix.M32, rowMatrix.M33, rowMatrix.M34,
            rowMatrix.M41, rowMatrix.M42, rowMatrix.M43, rowMatrix.M44
        );
    }

    public static int EnumToInt<T>(this T value) where T : Enum
    {
        return (int)(object)value;
    }

    
    public static bool HasAttribute<T>(this object value) where T : Attribute
    {
        return value.GetType().IsDefined(typeof(T), false);
    }
    
    public static bool HasAttribute<T>(this Type value) where T : Attribute
    {
        return value.IsDefined(typeof(T), false);
    }

    public static bool IsSameOrSubclass(this Type potentialDescendant, Type potentialBase)
    {
        return Util.IsSameOrSubclass(potentialBase, potentialDescendant);
    }
}