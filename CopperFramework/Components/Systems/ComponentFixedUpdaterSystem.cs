using CopperFramework.Info;
using CopperFramework.Systems;

namespace CopperFramework.Components.Systems;

public class ComponentFixedUpdaterSystem : ISystem
{
    private float fixedDeltaTime = 0;
    private const float FixedFrameTime = 0.02f;
    
    public SystemUpdateType GetUpdateType() => SystemUpdateType.Update;
    public int GetPriority() => 0;

    public void UpdateSystem()
    {
        fixedDeltaTime += Time.DeltaTime;

        if (!(fixedDeltaTime >= FixedFrameTime)) 
            return;
        
        fixedDeltaTime = 0;

        foreach (var component in ComponentRegistry.GameComponents.ToList())
        {
            component.FixedUpdate();
        }
    }

    public void LoadSystem()
    {
        
    }

    public void ShutdownSystem()
    {
        
    }
}