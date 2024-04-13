using CopperDearImGui;

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

    private void CameraTab()
    {
        var cameraPos = EngineWindow.Instance.Camera.Position;
        var cameraZoom = EngineWindow.Instance.Camera.Zoom;
        var cameraRot = EngineWindow.Instance.Camera.Rotation;
        
        CopperImGui.DragValue("Position", ref cameraPos);
        CopperImGui.DragValue("Zoom", ref cameraZoom);
        CopperImGui.DragValue("Rotation", ref cameraRot);
            
        EngineWindow.Instance.Camera.Position = cameraPos;
        EngineWindow.Instance.Camera.Zoom = cameraZoom;
        EngineWindow.Instance.Camera.Rotation = cameraRot;
    }

    private void PhysicsTab()
    {
        CopperImGui.DragValue("Fixed Delta Time", ref EngineWindow.FixedDeltaTime);
    }

    private void RenderingTab()
    {
    }

    private void SettingsTab()
    {
        // engine settings
            // flags
            // target fps
            // window title
    }
}