using CopperDevs.Core;

namespace CopperDevs.Framework.Common.Utility;

public class Singleton<T> : ISingleton where T : class, new()
{
    public static T Instance => GetInstance();
    private static T? instance;

    private static T GetInstance()
    {
        return instance ??= new T();
    }

    public void SetInstance(T newInstance)
    {
        if (instance is not null)
        {
            Log.Error($"Instance of type {nameof(T)} is already set.");
            return;
        }

        instance = newInstance;
    }
}