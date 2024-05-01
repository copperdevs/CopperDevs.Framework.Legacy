using CopperDearImGui;

namespace CopperFramework.Rendering.DearImGui.Windows;

public class CameraManagerWindow : BaseWindow
{
    public override string WindowName { get; protected set; } = "Engine Settings";

    public override void Update()
    {
        EngineCameraInfo();
    }

    private static void EngineCameraInfo()
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

    private static void Something()
    {
    }
}