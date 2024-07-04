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