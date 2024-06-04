using CopperDevs.DearImGui;
using Raylib_CSharp.Interact;

namespace CopperDevs.Framework.Elements.Systems;

public class DebugSystem : BaseSystem<DebugSystem>
{
    public bool DebugEnabled;

    public override void Start()
    {
        DebugEnabled = Engine.Instance.Settings.EnableDevToolsAtStart;
    }

    public override void Update()
    {
        if (Input.IsKeyPressed(KeyboardKey.F2))
            DebugEnabled = !DebugEnabled;
    }

    public override void UiUpdate()
    {
        CopperImGui.Checkbox("Debug Enabled", ref DebugEnabled);

    }
}