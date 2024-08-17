namespace CopperDevs.Framework.Resources.Fonts;

public class Inter
{
    private static string GetPath(string name) => $"Inter.static.Inter-{name}.ttf";

    public readonly byte[] Black = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("Black"));
    
    public readonly byte[] Bold = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("Bold"));

    public readonly byte[] ExtraBold = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("ExtraBold"));
    
    public readonly byte[] ExtraLight = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("ExtraLight"));

    public readonly byte[] Light = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("Light"));
    
    public readonly byte[] Medium = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("Medium"));

    public readonly byte[] Regular = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("Regular"));
    
    public readonly byte[] SemiBold = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("SemiBold"));

    public readonly byte[] Thin = FontRegistry.Instance.LoadEmbeddedResourceBytes(GetPath("Thin"));
}