using System.Numerics;

namespace CopperCore.Utility;

public static class Extensions
{
    public static string ToTitleCase(this string target) => TextUtil.ConvertToTitleCase(target);

    public static Vector4 ToVector(this Quaternion quaternion) => new(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
    public static Quaternion ToQuaternion(this Vector4 vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    public static Quaternion FromEulerAngles(this Vector3 euler) => MathUtil.FromEulerAngles(euler);
    public static Vector3 ToEulerAngles(this Quaternion quaternion) => MathUtil.ToEulerAngles(quaternion);

    public static Vector3 WithX(this Vector3 vector, float value) => vector with { X = value };
    public static Vector3 WithY(this Vector3 vector, float value) => vector with { Y = value };
    public static Vector3 WithZ(this Vector3 vector, float value) => vector with { Z = value };
    public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max) => MathUtil.Clamp(value, min, max);


    public static Vector4 WithX(this Vector4 vector, float value) => vector with { X = value };
    public static Vector4 WithY(this Vector4 vector, float value) => vector with { Y = value };
    public static Vector4 WithZ(this Vector4 vector, float value) => vector with { Z = value };
    public static Vector4 WithW(this Vector4 vector, float value) => vector with { W = value };
}