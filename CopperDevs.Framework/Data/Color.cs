namespace CopperDevs.Framework.Data;

public struct Color
{
    public byte R { get; set; } = 255;
    public byte G { get; set; } = 255;
    public byte B { get; set; } = 255;
    public byte A { get; set; } = 255;


    public Color(byte r, byte g, byte b, byte a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public Color(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
        A = 255;
    }

    public Color(byte value)
    {
        R = value;
        G = value;
        B = value;
        A = value;
    }

    public Color() : this(255)
    {
    }

    public Color(Vector4 vector) : this((byte)vector.X, (byte)vector.Y, (byte)vector.Z, (byte)vector.W)
    {
    }

    public Color(Vector3 vector) : this((byte)vector.X, (byte)vector.Y, (byte)vector.Z)
    {
    }

    public int ToInt() => ((A & 0xff) << 24) | ((R & 0xff) << 16) | ((G & 0xff) << 8) | (B & 0xff);

    public static implicit operator Vector3(Color color) => new(color.R, color.G, color.B);

    public static implicit operator Vector4(Color color) => new(color.R, color.G, color.B, color.A);

    public static Color operator /(Color color, byte value) => new((byte)(color.R / value), (byte)(color.G / value), (byte)(color.B / value), (byte)(color.A / value));

    public static Color operator *(Color color, float value) => new((byte)(color.R * value), (byte)(color.G * value), (byte)(color.B * value), (byte)(color.A * value));

    public static implicit operator rlColor(Color color) => new(color.R, color.G, color.B, color.A);

    public static implicit operator Color(rlColor color) => new(color.R, color.G, color.B, color.A);

    public static implicit operator int(Color color) => color.ToInt();

    public static Color FromInt(int color)
    {
        return new Color
        {
            A = (byte)((color >> 24) & 0xFF),
            R = (byte)((color >> 16) & 0xFF),
            G = (byte)((color >> 8) & 0xFF),
            B = (byte)(color & 0xFF),
        };
    }

    public static Color LightGray => new(200, 200, 200, 255);
    public static Color Gray => new(130, 130, 130, 255);
    public static Color DarkGray => new(80, 80, 80, 255);
    public static Color Yellow => new(253, 249, 0, 255);
    public static Color Gold => new(255, 203, 0, 255);
    public static Color Orange => new(255, 161, 0, 255);
    public static Color Pink => new(255, 109, 194, 255);
    public static Color LightRed => new(240, 60, 72, 255);
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