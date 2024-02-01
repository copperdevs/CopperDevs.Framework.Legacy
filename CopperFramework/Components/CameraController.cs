using System.Numerics;
using CopperFramework.Data;
using CopperFramework.Info;
using CopperFramework.Renderer.DearImGui.Attributes;
using CopperFramework.Util;
using Silk.NET.Input;

namespace CopperFramework.Components;

public class CameraController : GameComponent
{
    [Range(1, 5)] private float speed = 1.5f;
    [Range(0.1f, 5)] private float lookSensitivity = 0.75f;
    private Camera Camera => Parent.GetFirstComponentOfType<Camera>();

    public override void Start()
    {
        Input.MouseMove += OnMouseMove;
    }

    public override void Update()
    {
        var moveSpeed = speed * Time.DeltaTime;

        var cameraTransform = Camera.Transform;

        if (Input.IsKeyPressed(Key.W))
            cameraTransform.Position += moveSpeed * Camera.Transform.Forward;
        if (Input.IsKeyPressed(Key.S))
            cameraTransform.Position -= moveSpeed * Camera.Transform.Forward;
        if (Input.IsKeyPressed(Key.A))
            cameraTransform.Position -=
                Vector3.Normalize(Vector3.Cross(Camera.Transform.Forward, Camera.Transform.Up)) * moveSpeed;
        if (Input.IsKeyPressed(Key.D))
            cameraTransform.Position +=
                Vector3.Normalize(Vector3.Cross(Camera.Transform.Forward, Camera.Transform.Up)) * moveSpeed;
        if (Input.IsKeyPressed(Key.Space))

            cameraTransform.Position += moveSpeed * Vector3.UnitY;
        if (Input.IsKeyPressed(Key.ControlLeft))
            cameraTransform.Position -= moveSpeed * Vector3.UnitY;

        if (Input.IsKeyPressed(Key.Left))
            cameraTransform.Rotation.Y -= Time.DeltaTime * lookSensitivity;
        if (Input.IsKeyPressed(Key.Right))
            cameraTransform.Rotation.Y += Time.DeltaTime * lookSensitivity;
        
        if (Input.IsKeyPressed(Key.Down))
            cameraTransform.Rotation.X -= Time.DeltaTime * lookSensitivity;
        if (Input.IsKeyPressed(Key.Up))
            cameraTransform.Rotation.X += Time.DeltaTime * lookSensitivity;

        Camera.Transform = cameraTransform;
    }

    // TODO: Add looking around with the mouse?
    private void OnMouseMove(IMouse mouse, Vector2 position)
    {
        return;
    }
}