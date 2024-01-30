namespace CopperFramework.Util;

public static class ResourceLoader
{
    /// <summary>
    /// Loads an byte array from a embedded resource
    /// </summary>
    /// <param name="path">Path of the resource</param>
    /// <typeparam name="T">Type inside of the assembly that contains the target resource</typeparam>
    /// <returns>The resource</returns>
    public static byte[] LoadEmbeddedResourceBytes(string path)
    {
        using var s = typeof(ResourceLoader).Assembly.GetManifestResourceStream(path);
        using var ms = new MemoryStream();
        
        s?.CopyTo(ms);
        return ms.ToArray();
    }
}