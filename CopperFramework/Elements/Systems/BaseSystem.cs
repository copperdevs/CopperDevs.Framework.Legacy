namespace CopperFramework.Elements.Systems;

public abstract class BaseSystem<T> : ISystem where T : BaseSystem<T>, new()
{
    public static T Instance => GetInstance();
    private static T? instance;

    public virtual SystemUpdateType GetUpdateType() => SystemUpdateType.Update;

    public virtual int GetPriority() => 0;

    public virtual void UpdateSystem()
    {
    }

    public virtual void LoadSystem()
    {
    }

    public virtual void ShutdownSystem()
    {
    }

    private static T GetInstance()
    {
        return instance ??= SystemManager.GetSystem<T>();
    }
}