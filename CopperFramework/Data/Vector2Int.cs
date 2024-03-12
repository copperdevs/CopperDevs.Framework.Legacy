﻿using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace CopperFramework.Data;

public struct Vector2Int : IEquatable<Vector2Int>, IFormattable
{
    public int X;
    public int Y;

    public static Vector2Int Zero => default;
    public static Vector2Int One => new(1);
    public static Vector2Int UnitX => new(1, 0);
    public static Vector2Int UnitY => new(0, 1);

    public Vector2Int(int value) : this(value, value)
    {
    }

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Vector2Int other)
    {
        return X.Equals(other.X) && Y.Equals(other.Y);
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
        return obj is Vector2Int vector2Int && Equals(vector2Int);
    }

    public static bool operator ==(Vector2Int left, Vector2Int right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Vector2Int left, Vector2Int right)
    {
        return !(left == right);
    }

    public static Vector2Int operator +(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(left.X + right.X, left.Y + right.Y);
    }

    public static Vector2Int operator /(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(left.X / right.X, left.Y / right.Y);
    }

    public static Vector2Int operator /(Vector2Int value1, int value2)
    {
        return value1 / new Vector2Int(value2);
    }
    
    public static Vector2Int operator *(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(left.X * right.X,  left.Y * right.Y);
    }
    
    public static Vector2Int operator *(Vector2Int left, int right)
    {
        return left * new Vector2Int(right);
    }
    
    public static Vector2Int operator *(int left, Vector2Int right)
    {
        return right * left;
    }
    
    public static Vector2Int operator -(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(left.X - right.X, left.Y - right.Y);
    }

    public static implicit operator Vector2(Vector2Int value)
    {
        return new Vector2(value.X, value.Y);
    }

    public static implicit operator Vector2Int(Vector2 value)
    {
        return new Vector2Int((int)value.X, (int)value.Y);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}