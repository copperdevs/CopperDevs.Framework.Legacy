using CopperCore;

namespace CopperPlatformer.Core.Utility;

public class Singleton<T> : ISingleton where T : class, new()
{
    public static T Instance => GetInstance();
    private static T? instance;
    
    private static T GetInstance()
    {
        return instance ??= new T();
    }

    protected void SetInstance(T newInstance)
    {
        if (instance is not null)
        {
            Log.Error($"Instance of type {nameof(T)} is already set.");
            return;
        }
        instance = newInstance;
    }
}