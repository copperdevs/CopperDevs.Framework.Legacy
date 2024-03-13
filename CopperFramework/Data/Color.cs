﻿namespace CopperFramework.Data;

public sealed class Color
{
    public float R;
    public float G;
    public float B;
    public float A;

    public Color(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public Color(float r, float g, float b)
    {
        R = r;
        G = g;
        B = b;
        A = 255;
    }

    public Color(float value)
    {
        R = value;
        G = value;
        B = value;
        A = value;
    }

    public Color() : this(255)
    {
    }

    public Color(Vector4 vector) : this(vector.X, vector.Y, vector.Z, vector.W)

    {
    }

    public Color(Vector3 vector) : this(vector.X, vector.Y, vector.Z)
    {
    }

    public static implicit operator Vector3(Color color)
    {
        return new Vector3(color.R, color.G, color.B);
    }

    public static implicit operator Vector4(Color color)
    {
        return new Vector4(color.R, color.G, color.B, color.A);
    }

    public static Color operator /(Color color, float value)
    {
        return new Color(color.R / value, color.G / value, color.B / value, color.A / value);
    }

    public static Color operator *(Color color, float value)
    {
        return new Color(color.R * value, color.G * value, color.B * value, color.A * value);
    }

    public static implicit operator rlColor(Color color)
    {
        return new rlColor((int)color.R, (int)color.G, (int)color.B, (int)color.A);
    }

    public static implicit operator Color(rlColor color)
    {
        return new Color(color.R, color.G, color.B, color.A);
    }


    public static Color LightGray => new(200, 200, 200, 255);
    public static Color Gray => new(130, 130, 130, 255);
    public static Color DarkGray => new(80, 80, 80, 255);
    public static Color Yellow => new(253, 249, 0, 255);
    public static Color Gold => new(255, 203, 0, 255);
    public static Color Orange => new(255, 161, 0, 255);
    public static Color Pink => new(255, 109, 194, 255);
    public static Color Red => new(230, 41, 55, 255);
    public static Color Maroon => new(190, 33, 55, 255);
    public static Color Green => new(0, 228, 48, 255);
    public static Color Lime => new(0, 158, 47, 255);
    public static Color DarkGreen => new(0, 117, 44, 255);
    public static Color SkyBlue => new(102, 191, 255, 255);
    public static Color Blue => new(0, 121, 241, 255);
    public static Color DarkBlue => new(0, 82, 172, 255);
    public static Color Purple => new(200, 122, 255, 255);
    public static Color Violet => new(135, 60, 190, 255);
    public static Color DarkPurple => new(112, 31, 126, 255);
    public static Color Beige => new(211, 176, 131, 255);
    public static Color Brown => new(127, 106, 79, 255);
    public static Color DarkBrown => new(76, 63, 47, 255);
    public static Color White => new(255, 255, 255, 255);
    public static Color Black => new(0, 0, 0, 255);
    public static Color Blank => new(0, 0, 0, 0);
    public static Color Magenta => new(255, 0, 255, 255);
    public static Color RayWhite => new(245, 245, 245, 255);
}