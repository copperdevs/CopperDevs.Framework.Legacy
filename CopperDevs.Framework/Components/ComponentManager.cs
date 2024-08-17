using CopperDevs.Framework.Scenes;

namespace CopperDevs.Framework.Components;

internal static class ComponentManager
{
    internal static void UpdateActiveSceneComponents(ComponentUpdateType updateType) => UpdateSceneComponents(SceneManager.ActiveScene, updateType);

    internal static void UpdateSceneComponents(Scene targetScene, ComponentUpdateType updateType)
    {
        switch (updateType)
        {
            case ComponentUpdateType.Start:
                UpdateComponents(targetScene, false, component => component.Start()); // scene
                break;
            case ComponentUpdateType.Normal:
                UpdateComponents(targetScene, true, component => component.Update()); // engine
                break;
            case ComponentUpdateType.Debug:
                UpdateComponents(targetScene, false, component => component.DebugUpdate()); // engine
                break;
            case ComponentUpdateType.Ui:
                UpdateComponents(targetScene, false, component => component.UiUpdate()); // engine
                break;
            case ComponentUpdateType.Fixed:
                UpdateComponents(targetScene, true, component => component.FixedUpdate()); // engine
                break;
            case ComponentUpdateType.Stop:
                UpdateComponents(targetScene, false, component => component.Stop()); // scene
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(updateType), updateType, null);
        }
    }

    internal static void UpdateComponents(Scene targetScene, bool rlGlOffset, Action<GameComponent> component)
    {
        var gameObjects = targetScene.ToList();

        if (rlGlOffset)
        {
            foreach (var gameObject in gameObjects)
            {
                RlGl.PushMatrix();
                RlGl.TranslateF(gameObject.Transform.Position.X, gameObject.Transform.Position.Y, gameObject.Transform.Position.Z);
                RlGl.RotateF(gameObject.Transform.Rotation, 0, 0, -1);
                RlGl.ScaleF(gameObject.Transform.Scale.X, gameObject.Transform.Scale.Y, 0);

                gameObject.UpdateComponents(component);

                RlGl.PopMatrix();
            }
        }
        else
        {
            foreach (var gameObject in gameObjects)
                gameObject.UpdateComponents(component);
        }
    }

    internal enum ComponentUpdateType
    {
        Start,
        Normal,
        Debug,
        Ui,
        Fixed,
        Stop
    }
}