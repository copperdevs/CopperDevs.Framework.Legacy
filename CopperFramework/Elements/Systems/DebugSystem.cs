using CopperFramework.Utility;

namespace CopperFramework.Elements.Systems;

public class DebugSystem : SystemSingleton<DebugSystem>, ISystem
{
    public SystemUpdateType GetUpdateType() => SystemUpdateType.Update;
    public int GetPriority() => 0;

    public bool DebugEnabled { get; private set; }

    public void UpdateSystem()
    {
        if (Input.IsKeyPressed(KeyboardKey.F2))
            DebugEnabled = !DebugEnabled;
    }

    public void LoadSystem()
    {
        
    }

    public void ShutdownSystem()
    {
        
    }
}