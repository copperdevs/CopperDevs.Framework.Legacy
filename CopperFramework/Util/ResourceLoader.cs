namespace CopperFramework.Util;

public static class ResourceLoader
{
    public static byte[] LoadEmbeddedResourceBytes(string path)
    {
        using var s = typeof(ResourceLoader).Assembly.GetManifestResourceStream(path);
        using var ms = new MemoryStream();

        s?.CopyTo(ms);
        return ms.ToArray();
    }

    public static string[] GetResources()
    {
        return typeof(ResourceLoader).Assembly.GetManifestResourceNames();
    }
}