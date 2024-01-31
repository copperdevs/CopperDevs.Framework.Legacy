namespace CopperFramework.Systems;

public interface ISystem
{
    public SystemUpdateType GetUpdateType();
    public void UpdateSystem();

    public void LoadSystem();
    public void ShutdownSystem();
}