namespace CopperFramework.Elements.Systems;

public class DebugSystem : BaseSystem<DebugSystem>
{
    public bool DebugEnabled { get; private set; }

    public override void LoadSystem()
    {
        DebugEnabled = Engine.Instance.Settings.EnableDevToolsAtStart;
    }

    public override void UpdateSystem()
    {
        if (Input.IsKeyPressed(KeyboardKey.F2))
            DebugEnabled = !DebugEnabled;
    }
}