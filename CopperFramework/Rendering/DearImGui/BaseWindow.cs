namespace CopperFramework.Rendering.DearImGui;

public abstract class BaseWindow
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