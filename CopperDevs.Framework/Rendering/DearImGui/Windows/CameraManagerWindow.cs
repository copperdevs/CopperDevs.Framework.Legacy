using CopperDevs.DearImGui;

namespace CopperDevs.Framework.Rendering.DearImGui.Windows;

public class CameraManagerWindow : BaseWindow
{
    public override string WindowName { get; protected set; } = "Camera Manager";

    public override void Update()
    {
        EngineCameraInfo();
    }

    private static void EngineCameraInfo()
    {
        var cameraPos = OldEngine.CurrentWindow.Camera.Position;
        var cameraZoom = OldEngine.CurrentWindow.Camera.Zoom;
        var cameraRot = OldEngine.CurrentWindow.Camera.Rotation;

        CopperImGui.DragValue("Position", ref cameraPos);
        CopperImGui.DragValue("Zoom", ref cameraZoom);
        CopperImGui.DragValue("Rotation", ref cameraRot);

        OldEngine.CurrentWindow.Camera.Position = cameraPos;
        OldEngine.CurrentWindow.Camera.Zoom = cameraZoom;
        OldEngine.CurrentWindow.Camera.Rotation = cameraRot;
    }
}