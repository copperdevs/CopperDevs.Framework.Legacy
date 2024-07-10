using CopperDevs.Core;
using CopperDevs.DearImGui.Attributes;

namespace CopperDevs.Framework.Components;

public class Camera : GameComponent
{
    private static readonly RegisteredCameras Cameras = new();

    [Exposed] private int priority = 10;

    public override void Start()
    {
        Transform.PositionUpdated += TransformPositionUpdated;
        Transform.RotationUpdated += TransformRotationUpdated;

        Cameras.Add(this);
    }


    public override void Stop()
    {
        Cameras.Remove(this);

#pragma warning disable CS8601 // Possible null reference assignment.
        Transform.PositionUpdated -= TransformPositionUpdated;
        Transform.RotationUpdated -= TransformRotationUpdated;
#pragma warning restore CS8601 // Possible null reference assignment.
    }

    private void TransformPositionUpdated(Vector2 value)
    {
        if (Cameras.HighestPriorityCamera == this)
            Engine.Instance.Camera.Position = value;
    }

    private void TransformRotationUpdated(float value)
    {
        if (Cameras.HighestPriorityCamera == this)
            Engine.Instance.Camera.Rotation = value;
    }

    private class RegisteredCameras
    {
        private List<Camera?> registeredCameras = [];

        public Camera? HighestPriorityCamera => registeredCameras[^1]!;

        public void Add(Camera camera)
        {
            if (!registeredCameras.Contains(camera))
                registeredCameras.Add(camera);

            Sort();
        }

        public void Remove(Camera camera)
        {
            registeredCameras.Remove(camera);
            Sort();
        }

        private void Sort()
        {
            var previousCamera = HighestPriorityCamera;

            registeredCameras.RemoveAll(item => item is null);
            registeredCameras = registeredCameras.OrderBy(camera => camera?.priority).ToList();

            if (previousCamera is null || HighestPriorityCamera is null || previousCamera == HighestPriorityCamera)
                return;

            HighestPriorityCamera.TransformPositionUpdated(HighestPriorityCamera.Transform.Position);
            HighestPriorityCamera.TransformRotationUpdated(HighestPriorityCamera.Transform.Rotation);
        }
    }
}