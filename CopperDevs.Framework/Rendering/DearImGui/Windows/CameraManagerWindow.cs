using CopperDevs.DearImGui;
using CopperDevs.DearImGui.Attributes;

namespace CopperDevs.Framework.Rendering.DearImGui.Windows;

[Window("Camera Manager", WindowOpen = false)]
public class CameraManagerWindow : BaseWindow
{
    public override void WindowUpdate()
    {
        EngineCameraInfo();
    }

    private static void EngineCameraInfo()
    {
        var engineCamera = Engine.Instance.Camera;
        
        var cameraPos = engineCamera.Position;
        var cameraZoom = engineCamera.Zoom;
        var cameraRot = engineCamera.Rotation;

        CopperImGui.DragValue("Position", ref cameraPos);
        CopperImGui.DragValue("Zoom", ref cameraZoom);
        CopperImGui.DragValue("Rotation", ref cameraRot);

        engineCamera.Position = cameraPos;
        engineCamera.Zoom = cameraZoom;
        engineCamera.Rotation = cameraRot;
    }
}