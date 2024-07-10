namespace CopperDevs.Framework.Rendering;

internal struct EngineCamera
{
    private rlCamera2D camera2D = new()
    {
        Offset = Vector2.Zero,
        Zoom = 1,
        Rotation = 0,
        Target = Vector2.Zero
    };

    public Vector2 Position
    {
        get => camera2D.Target;
        set => camera2D.Target = value;
    }

    public float Zoom
    {
        get => camera2D.Zoom;
        set => camera2D.Zoom = value;
    }

    public float Rotation
    {
        get => camera2D.Rotation;
        set => camera2D.Rotation = value;
    }

    public EngineCamera()
    {
    }

    public Vector2 GetScreenToWorld(Vector2 position)
    {
        return camera2D.GetScreenToWorld(position);
    }

    public Vector2 GetWorldToScreen(Vector2 position)
    {
        return camera2D.GetWorldToScreen(position);
    }

    public Matrix4x4 GetMatrix() => camera2D.GetMatrix();


    public static implicit operator rlCamera2D(EngineCamera camera) => camera.camera2D;
}