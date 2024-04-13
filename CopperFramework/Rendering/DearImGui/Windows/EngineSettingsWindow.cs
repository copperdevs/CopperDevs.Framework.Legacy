using CopperDearImGui;
using CopperDearImGui.Utility;

namespace CopperFramework.Rendering.DearImGui.Windows;

public class EngineSettingsWindow : BaseWindow
{
    public override string WindowName { get; protected set; } = "Engine Settings";

    public override void Update()
    {
        CopperImGui.TabGroup("engine_settings_window_tab_bar",
            ("Camera", CameraTab),
            ("Physics", PhysicsTab),
            ("Rendering", RenderingTab),
            ("Settings", SettingsTab));
    }

    private static void CameraTab()
    {
        var cameraPos = Engine.CurrentWindow.Camera.Position;
        var cameraZoom = Engine.CurrentWindow.Camera.Zoom;
        var cameraRot = Engine.CurrentWindow.Camera.Rotation;

        CopperImGui.DragValue("Position", ref cameraPos);
        CopperImGui.DragValue("Zoom", ref cameraZoom);
        CopperImGui.DragValue("Rotation", ref cameraRot);

        Engine.CurrentWindow.Camera.Position = cameraPos;
        Engine.CurrentWindow.Camera.Zoom = cameraZoom;
        Engine.CurrentWindow.Camera.Rotation = cameraRot;
    }

    private static void PhysicsTab()
    {
        CopperImGui.DragValue("Fixed Delta Time", ref EngineWindow.FixedDeltaTime);
    }

    private static void RenderingTab()
    {
        CopperImGui.Checkbox("Engine Window Screen Shader Enabled", ref Engine.CurrentWindow.ScreenShaderEnabled);
        
        CopperImGui.CollapsingHeader("Window Render Texture", () =>
        {
            using (new IndentScope())
            {
                var renderTexture = (object)Engine.CurrentWindow.RenderTexture;
                CopperImGui.GetFieldRenderer<RenderTexture2D>()?.ValueRenderer(ref renderTexture, 0);
            }
        });
    }

    private static void SettingsTab()
    {
        // engine settings
        // flags
        // target fps
        // window title
    }
}