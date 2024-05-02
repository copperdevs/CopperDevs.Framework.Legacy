using CopperCore;

namespace CopperFramework.Elements.Systems;

public abstract class BaseSystem<T> : ISystem, IUpdatable where T : BaseSystem<T>, new()
{
    public static T Instance => GetInstance();
    private static T? instance;

    public virtual SystemUpdateType GetUpdateType() => SystemUpdateType.Update;

    public virtual int GetPriority() => 0;

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void Stop()
    {
    }

    private static T GetInstance()
    {
        return instance ??= SystemManager.GetSystem<T>();
    }
}