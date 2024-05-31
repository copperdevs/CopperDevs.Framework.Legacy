﻿using System.Numerics;

namespace CopperDevs.Core.Utility;

public static class Extensions
{
    public static string ToTitleCase(this string target) => TextUtil.ConvertToTitleCase(target);
    public static Vector4 ToVector(this Quaternion quaternion) => new(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
    public static Quaternion ToQuaternion(this Vector4 vector) => new(vector.X, vector.Y, vector.Z, vector.W);
    public static Quaternion FromEulerAngles(this Vector3 euler) => MathUtil.FromEulerAngles(euler);
    public static Vector3 ToEulerAngles(this Quaternion quaternion) => MathUtil.ToEulerAngles(quaternion);
    public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max) => MathUtil.Clamp(value, min, max);
    public static Vector2 ToRotatedUnitVector(this float value) => MathUtil.CreateRotatedUnitVector(value);
    public static Vector2 WithX(this Vector2 vector, float value) => vector with { X = value };
    public static Vector2 WithY(this Vector2 vector, float value) => vector with { Y = value };
    public static Vector3 WithX(this Vector3 vector, float value) => vector with { X = value };
    public static Vector3 WithY(this Vector3 vector, float value) => vector with { Y = value };
    public static Vector3 WithZ(this Vector3 vector, float value) => vector with { Z = value };
    public static Vector4 WithX(this Vector4 vector, float value) => vector with { X = value };
    public static Vector4 WithY(this Vector4 vector, float value) => vector with { Y = value };
    public static Vector4 WithZ(this Vector4 vector, float value) => vector with { Z = value };
    public static Vector4 WithW(this Vector4 vector, float value) => vector with { W = value };
    public static Vector2 FlipX(this Vector2 vector) => vector with { X = -vector.X };
    public static Vector2 FlipY(this Vector2 vector) => vector with { Y = -vector.Y };
    public static Vector3 FlipX(this Vector3 vector) => vector with { X = -vector.X };
    public static Vector3 FlipY(this Vector3 vector) => vector with { Y = -vector.Y };
    public static Vector3 FlipZ(this Vector3 vector) => vector with { Z = -vector.Z };
    public static Vector4 FlipX(this Vector4 vector) => vector with { X = -vector.X };
    public static Vector4 FlipY(this Vector4 vector) => vector with { Y = -vector.Y };
    public static Vector4 FlipZ(this Vector4 vector) => vector with { Z = -vector.Z };
    public static Vector4 FlipW(this Vector4 vector) => vector with { W = -vector.W };
    public static Vector3 ToVector3(this Vector2 vector, float z = 0) => new(vector.X, vector.Y, z);
    public static Vector4 ToVector4(this Vector2 vector, float z = 0, float w = 0) => new(vector.X, vector.Y, z, w);
    public static Vector4 ToVector4(this Vector3 vector, float w = 0) => new(vector.X, vector.Y, vector.Z, w);
    public static Vector2 ToVector2<T>(this T value) where T : INumber<T> => new((float)Convert.ChangeType(value, typeof(float)));
    public static Vector3 ToVector3<T>(this T value) where T : INumber<T> => new((float)Convert.ChangeType(value, typeof(float)));
    public static Vector4 ToVector4<T>(this T value) where T : INumber<T> => new((float)Convert.ChangeType(value, typeof(float)));
}