using CopperFramework.Elements.Systems;

namespace CopperFramework.Util;

public class SystemSingleton<T> : ISingleton where T : class, ISystem, new()
{
    public static T Instance => GetInstance();
    private static T? instance;
    
    private static T GetInstance()
    {
        return instance ??= SystemManager.GetSystem<T>();
    }
}