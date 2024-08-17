using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using CopperDevs.Core.Data;

namespace CopperDevs.Framework.Data;

public struct TransformVector3(float x, float y, float z)
{
    public float X = x;
    public float Y = y;
    public float Z = z;

    public static TransformVector3 Zero => default;
    public static TransformVector3 One => new(1);

    public TransformVector3(float value) : this(value, value, value)
    {
    }

    public TransformVector3(Vector2 xy) : this(xy.X, xy.Y, 0)
    {
    }

    public bool Equals(TransformVector3 other)
    {
        return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
    }

    public readonly override string ToString()
    {
        return ToString("G", CultureInfo.CurrentCulture);
    }

    public readonly string ToString([StringSyntax(StringSyntaxAttribute.NumericFormat)] string? format)
    {
        return ToString(format, CultureInfo.CurrentCulture);
    }

    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        var separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
        return $"<{X.ToString(format, formatProvider)}{separator} {Y.ToString(format, formatProvider)}>";
    }

    public override bool Equals(object? obj)
    {
        return obj is TransformVector3 vector3 && Equals(vector3);
    }

    public static bool operator ==(TransformVector3 left, TransformVector3 right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(TransformVector3 left, TransformVector3 right)
    {
        return !(left == right);
    }

    public static TransformVector3 operator +(TransformVector3 left, TransformVector3 right)
    {
        return new TransformVector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }

    public static TransformVector3 operator /(TransformVector3 left, TransformVector3 right)
    {
        return new TransformVector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
    }

    public static TransformVector3 operator /(TransformVector3 value1, float value2)
    {
        return value1 / new TransformVector3(value2);
    }

    public static TransformVector3 operator *(TransformVector3 left, TransformVector3 right)
    {
        return new TransformVector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
    }

    public static TransformVector3 operator *(TransformVector3 left, float right)
    {
        return left * new TransformVector3(right);
    }

    public static TransformVector3 operator *(float left, TransformVector3 right)
    {
        return right * left;
    }

    public static TransformVector3 operator -(TransformVector3 left, TransformVector3 right)
    {
        return new TransformVector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
    }

    public static TransformVector3 operator +(TransformVector3 left, Vector2 right)
    {
        return new TransformVector3(left.X + right.X, left.Y + right.Y, left.Z);
    }

    public static TransformVector3 operator -(TransformVector3 left, Vector2 right)
    {
        return new TransformVector3(left.X - right.X, left.Y - right.Y, left.Z);
    }

    public static TransformVector3 operator /(TransformVector3 left, Vector2 right)
    {
        return new TransformVector3(left.X / right.X, left.Y / right.Y, left.Z);
    }

    public static TransformVector3 operator *(TransformVector3 left, Vector2 right)
    {
        return new TransformVector3(left.X * right.X, left.Y * right.Y, left.Z);
    }

    public static implicit operator TransformVector3(Vector3Int value)
    {
        return new TransformVector3(value.X, value.Y, value.Z);
    }

    public static implicit operator Vector3Int(TransformVector3 value)
    {
        return new Vector3Int((int)value.X, (int)value.Y, (int)value.Z);
    }

    public static implicit operator TransformVector3(Vector3 value)
    {
        return new TransformVector3(value.X, value.Y, value.Z);
    }

    public static implicit operator Vector3(TransformVector3 value)
    {
        return new Vector3((int)value.X, (int)value.Y, (int)value.Z);
    }

    public static implicit operator Vector2(TransformVector3 value)
    {
        return new Vector2(value.X, value.Y);
    }

    public static implicit operator Vector2Int(TransformVector3 value)
    {
        return new Vector2Int((int)value.X, (int)value.Y);
    }

    public static implicit operator TransformVector3(Vector2 value)
    {
        return new TransformVector3(value.X, value.Y, 0);
    }

    public static implicit operator TransformVector3(Vector2Int value)
    {
        return new TransformVector3(value.X, value.Y, 0);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }
}