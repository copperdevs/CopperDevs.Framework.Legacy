using CopperFramework.Systems;

namespace CopperFramework.Components.Systems;

public class ComponentUpdaterSystem : ISystem
{
    public SystemUpdateType GetUpdateType() => SystemUpdateType.Update;

    public void UpdateSystem()
    {
        foreach (var component in ComponentRegistry.GameComponents.ToList())
        {
            component.Update();
        }
    }

    public void LoadSystem()
    {
        
    }

    public void ShutdownSystem()
    {
        
    }
}