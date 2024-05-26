namespace CopperDevs.Framework.Elements.Systems;

public class DebugSystem : BaseSystem<DebugSystem>
{
    public bool DebugEnabled { get; private set; }

    public override void Start()
    {
        DebugEnabled = Engine.Instance.Settings.EnableDevToolsAtStart;
    }

    public override void Update()
    {
        if (Input.IsKeyPressed(KeyboardKey.F2))
            DebugEnabled = !DebugEnabled;
    }
}