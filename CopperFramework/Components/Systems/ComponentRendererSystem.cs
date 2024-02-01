using CopperFramework.Systems;

namespace CopperFramework.Components.Systems;

public class ComponentRendererSystem : ISystem
{
    public SystemUpdateType GetUpdateType() => SystemUpdateType.Renderer;
    public int GetPriority() => 0;

    public void UpdateSystem()
    {
        foreach (var component in ComponentRegistry.GameComponents.ToList())
        {
            component.Render();
        }
    }

    public void LoadSystem()
    {
        
    }

    public void ShutdownSystem()
    {
        
    }
}