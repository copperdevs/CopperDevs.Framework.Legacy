namespace CopperDevs.Framework.Resources.Fonts;

public class Figtree
{
    private static string GetPath(string name) => $"Figtree.static.Figtree-{name}.ttf";

    public readonly byte[] Regular = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("Regular"));
    public readonly byte[] Italic = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("Italic"));

    public readonly byte[] Black = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("Black"));
    public readonly byte[] BlackItalic = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("BlackItalic"));

    public readonly byte[] Bold = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("Bold"));
    public readonly byte[] BoldItalic = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("BoldItalic"));

    public readonly byte[] ExtraBold = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("ExtraBold"));
    public readonly byte[] ExtraBoldItalic = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("ExtraBoldItalic"));

    public readonly byte[] Light = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("Light"));
    public readonly byte[] LightItalic = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("LightItalic"));

    public readonly byte[] Medium = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("Medium"));
    public readonly byte[] MediumItalic = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("MediumItalic"));

    public readonly byte[] SemiBold = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("SemiBold"));
    public readonly byte[] SemiBoldItalic = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("SemiBoldItalic"));
}