using System.Reflection;

namespace CopperDevs.Framework.Resources.Core;

public class ResourceLoader(Assembly targetAssembly, string rootPath)
{
    public byte[] LoadEmbeddedResourceBytes(string path)
    {
        using var s = targetAssembly.GetManifestResourceStream($"{rootPath}.{path}");
        using var ms = new MemoryStream();

        s?.CopyTo(ms);
        return ms.ToArray();
    }

    public string LoadTextResource(string path)
    {
        using var stream = targetAssembly.GetManifestResourceStream($"{rootPath}.{path}");
        using var reader = new StreamReader(stream!);
        return reader.ReadToEnd();
    }
}