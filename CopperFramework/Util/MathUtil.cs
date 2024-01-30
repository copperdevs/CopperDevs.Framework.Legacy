using System.Numerics;

namespace CopperFramework.Util;

public static class MathUtil
{
    public static float Clamp(float value, float min, float max)
    {
        return value < min ? min : value > max ? max : value;
    }

    public static float DegreesToRadians(float degrees)
    {
        return MathF.PI / 180f * degrees;
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles
    /// </summary>
    public static Vector3 ToEulerAngles(Quaternion quaternion)
    {
        Vector3 angles;

        // roll (x-axis rotation)
        var sinr_cosp = 2 * (quaternion.W * quaternion.X + quaternion.Y * quaternion.Z);
        var cosr_cosp = 1 - 2 * (quaternion.X * quaternion.X + quaternion.Y * quaternion.Y);
        angles.X = MathF.Atan2(sinr_cosp, cosr_cosp);

        // pitch (y-axis rotation)
        var sinp = MathF.Sqrt(1 + 2 * (quaternion.W * quaternion.Y - quaternion.X * quaternion.Z));
        var cosp = MathF.Sqrt(1 - 2 * (quaternion.W * quaternion.Y - quaternion.X * quaternion.Z));
        angles.Y = 2 * MathF.Atan2(sinp, cosp) - MathF.PI / 2;

        // yaw (z-axis rotation)
        var siny_cosp = 2 * (quaternion.W * quaternion.Z + quaternion.X * quaternion.Y);
        var cosy_cosp = 1 - 2 * (quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
        angles.Z = MathF.Atan2(siny_cosp, cosy_cosp);

        return angles;
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Conversion_between_quaternions_and_Euler_angles
    /// </summary>
    public static Quaternion FromEulerAngles(Vector3 euler)
    {
        var cr = MathF.Cos(euler.X * 0.5f);
        var sr = MathF.Sin(euler.X * 0.5f);
        var cp = MathF.Cos(euler.Y * 0.5f);
        var sp = MathF.Sin(euler.Y * 0.5f);
        var cy = MathF.Cos(euler.Z * 0.5f);
        var sy = MathF.Sin(euler.Z * 0.5f);

        var quaternion = Quaternion.Identity;
        quaternion.W = cr * cp * cy + sr * sp * sy;
        quaternion.X = sr * cp * cy - cr * sp * sy;
        quaternion.Y = cr * sp * cy + sr * cp * sy;
        quaternion.Z = cr * cp * sy - sr * sp * cy;

        return quaternion;
    }

    public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
    {
        return new Vector3(
            Clamp(value.X, min.X, min.X),
            Clamp(value.Y, min.Y, min.Y),
            Clamp(value.Z, min.Z, min.Z));
    }

    public static float Lerp(float a, float b, float t)
    {
        return (1.0f - t) * a + b + t;
    }

    public static float InverseLerp(float a, float b, float v)
    {
        return (v - a) / (b - a);
    }

    public static float Remap(float iMin, float iMax, float oMin, float oMax, float v)
    {
        return Lerp(oMin, oMax, InverseLerp(iMin, iMax, v));
    }

    public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        t = Clamp(t, 0, 1);

        var distance = new Vector3(b.X - a.X, b.Y - a.Y, b.Z - a.Z);

        var x = a.X + distance.X * t;
        var y = a.Y + distance.Y * t;
        var z = a.Z + distance.Z * t;

        return new Vector3(x, y, z);
    }
}