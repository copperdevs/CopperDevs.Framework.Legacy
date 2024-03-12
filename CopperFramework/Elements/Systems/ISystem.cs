namespace CopperFramework.Elements.Systems;

public interface ISystem
{
    public SystemUpdateType GetUpdateType();
    public int GetPriority();
    
    public void UpdateSystem();

    public void LoadSystem();
    public void ShutdownSystem();
}