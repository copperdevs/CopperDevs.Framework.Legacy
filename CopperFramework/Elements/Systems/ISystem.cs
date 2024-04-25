namespace CopperFramework.Elements.Systems;

internal interface ISystem
{
    public int GetPriority();
    public SystemUpdateType GetUpdateType();

    public void LoadSystem();
    public void UpdateSystem();
    public void ShutdownSystem();
}