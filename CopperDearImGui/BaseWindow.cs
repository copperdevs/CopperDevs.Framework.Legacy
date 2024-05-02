namespace CopperDearImGui;

public abstract class BaseWindow : IUpdatable
{
    public abstract string WindowName { get; protected internal set; }
    internal bool WindowOpen = false;

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void Stop()
    {
    }
}