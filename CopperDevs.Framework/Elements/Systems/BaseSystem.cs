using CopperDevs.DearImGui;

namespace CopperDevs.Framework.Elements.Systems;

public abstract class BaseSystem<T> : ISystem where T : BaseSystem<T>, new()
{
    public static T Instance => GetInstance();
    private static T? instance;

    public Action? CustomSystemUi { get; set; } = null!;

    public virtual SystemUpdateType GetUpdateType() => SystemUpdateType.Update;
    public virtual int GetPriority() => 0;

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void UiUpdate()
    {
        CopperImGui.RenderObjectValues(this);
    }

    public virtual void Stop()
    {
    }


    private static T GetInstance()
    {
        return instance ??= SystemManager.GetSystem<T>();
    }
}