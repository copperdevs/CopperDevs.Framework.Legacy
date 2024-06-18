using System.Reflection;

namespace CopperDevs.Framework.Utility;

public static class ResourceLoader
{
    private static readonly Dictionary<Assembly, List<string>> ResourcePaths = new();

    static ResourceLoader()
    {
        LoadResourcePaths();
    }

    private static void LoadResourcePaths()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            ResourcePaths.Add(assembly, assembly.GetManifestResourceNames().ToList());
    }

    public static byte[] LoadEmbeddedResourceBytes(string path)
    {
        using var s = GetAssemblyFromResourcePath(path).GetManifestResourceStream(path);
        using var ms = new MemoryStream();

        s?.CopyTo(ms);
        return ms.ToArray();
    }

    public static IEnumerable<string> GetAllResources()
    {
        var foundPaths = new List<string>();

        foreach (var paths in ResourcePaths.Values)
            foundPaths.AddRange(paths);

        return foundPaths;
    }

    public static string LoadTextResource(string path)
    {
        using var stream = GetAssemblyFromResourcePath(path).GetManifestResourceStream(path);
        using var reader = new StreamReader(stream!);
        return reader.ReadToEnd();
    }

    public static Assembly GetAssemblyFromResourcePath(string targetPath)
    {
        return ResourcePaths.Where(assembly => assembly.Value.Any(path => path == targetPath)).Select(assembly => assembly.Key).FirstOrDefault()!;
    }
}