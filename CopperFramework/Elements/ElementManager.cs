using CopperFramework.Elements.Components;
using CopperFramework.Elements.Systems;

namespace CopperFramework.Elements;

internal static class ElementManager
{
    internal static void Initialize()
    {
        Update(ElementUpdateType.Load);
        SystemManager.Initialize();
    }

    internal static void Shutdown()
    {
        Update(ElementUpdateType.Close);
        SystemManager.Shutdown();
    }

    internal static void Update(ElementUpdateType updateType)
    {
        switch (updateType)
        {
            case ElementUpdateType.Load:
                SystemManager.Update(SystemUpdateType.Load);
                break;

            case ElementUpdateType.Update:
                SystemManager.Update(SystemUpdateType.Update);
                break;

            case ElementUpdateType.Render:
                SystemManager.Update(SystemUpdateType.Renderer);
                ComponentRegistry.CurrentComponents.ToList().ForEach(gameObject =>
                {
                    Rlgl.PushMatrix();
                    Rlgl.Translatef(gameObject.Transform.Position.X, gameObject.Transform.Position.Y, 0);
                    Rlgl.Rotatef(gameObject.Transform.Rotation, 0, 0, -1);
                    Rlgl.Scalef(gameObject.Transform.Scale, gameObject.Transform.Scale, 0);
                    gameObject.UpdateComponents(component => component.Update());
                    Rlgl.PopMatrix();
                });
                break;
            case ElementUpdateType.Close:
                SystemManager.Update(SystemUpdateType.Close);
                ComponentRegistry.CurrentComponents.ToList().ForEach(gameObject => gameObject.UpdateComponents(component => component.Sleep()));
                ComponentRegistry.CurrentComponents.ToList().ForEach(gameObject => gameObject.UpdateComponents(component => component.Stop()));
                break;

            case ElementUpdateType.UiRender:
                SystemManager.Update(SystemUpdateType.UiRenderer);
                ComponentRegistry.CurrentComponents.ToList().ForEach(gameObject => gameObject.UpdateComponents(component => component.UiUpdate()));
                break;

            case ElementUpdateType.Debug:
                ComponentRegistry.CurrentComponents.ToList().ForEach(gameObject =>
                {
                    Rlgl.PushMatrix();
                    Rlgl.Translatef(gameObject.Transform.Position.X, gameObject.Transform.Position.Y, 0);
                    Rlgl.Rotatef(gameObject.Transform.Rotation, 0, 0, -1);
                    Rlgl.Scalef(gameObject.Transform.Scale, gameObject.Transform.Scale, 0);
                    gameObject.UpdateComponents(component => component.DebugUpdate());
                    Rlgl.PopMatrix();
                });
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(updateType), updateType, null);
        }
    }

    internal enum ElementUpdateType
    {
        Load,
        Update,
        Render,
        UiRender,
        Close,
        Debug
    }
}