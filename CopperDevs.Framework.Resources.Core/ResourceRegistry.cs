using CopperDevs.Framework.Common.Utility;

namespace CopperDevs.Framework.Resources.Core;

public abstract class ResourceRegistry<T>(ResourceType resourceType) : ISingleton where T : class, new()
{
    public static T Instance => Singleton<T>.Instance;

    private readonly Singleton<T> resourceRegistrySingleton = new();
    
    private ResourceLoader resourceLoader = null!;


    protected ResourceRegistry() : this(ResourceType.None)
    {
    }

    protected abstract void LoadResources();

    protected void SetInstance(T newInstance)
    {
        resourceRegistrySingleton.SetInstance(newInstance);
        resourceLoader = new ResourceLoader(newInstance.GetType().Assembly, $"CopperDevs.Framework.Resources.{resourceType.ToString()}.Resources");
        LoadResources();
    }

    public byte[] LoadEmbeddedResourceBytes(string path)
    {
        return resourceLoader.LoadEmbeddedResourceBytes(path);
    }

    public string LoadTextResource(string path)
    {
        return resourceLoader.LoadTextResource(path);
    }
}