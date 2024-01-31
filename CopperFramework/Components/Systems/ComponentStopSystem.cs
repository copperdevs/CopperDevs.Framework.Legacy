using CopperFramework.Systems;

namespace CopperFramework.Components.Systems;

public class ComponentStopSystem : ISystem
{
    public SystemUpdateType GetUpdateType() => SystemUpdateType.Close;

    public void UpdateSystem()
    {
        foreach (var component in ComponentRegistry.GameComponents.ToList())
        {
            component.Stop();
        }
    }

    public void LoadSystem()
    {
        
    }

    public void ShutdownSystem()
    {
        
    }
}