using CopperDevs.Framework.Elements.Components;
using CopperDevs.Framework.Elements.Systems;

namespace CopperDevs.Framework.Elements;

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
                UpdateComponents(true, component => component.Update());
                break;
            case ElementUpdateType.Close:
                SystemManager.Update(SystemUpdateType.Close);
                UpdateComponents(false, component => component.Sleep());
                UpdateComponents(false, component => component.Stop());
                break;

            case ElementUpdateType.UiRender:
                SystemManager.Update(SystemUpdateType.UiRenderer);
                UpdateComponents(false, component => component.UiUpdate());
                break;

            case ElementUpdateType.Debug:
                UpdateComponents(false, component => component.DebugUpdate());
                break;

            case ElementUpdateType.Fixed:
                SystemManager.Update(SystemUpdateType.Fixed);
                UpdateComponents(true, component => component.FixedUpdate());
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(updateType), updateType, null);
        }
    }

    private static void UpdateComponents(bool rlGlOffset, Action<GameComponent> updateAction)
    {
        var components = ComponentRegistry.CurrentComponents.ToList();

        if (rlGlOffset)
        {
            foreach (var gameObject in components)
            {
                RlGl.PushMatrix();
                RlGl.TranslateF(gameObject.Transform.Position.X, gameObject.Transform.Position.Y, 0);
                RlGl.RotateF(gameObject.Transform.Rotation, 0, 0, -1);
                RlGl.ScaleF(gameObject.Transform.Scale, gameObject.Transform.Scale, 0);

                gameObject.UpdateComponents(component =>
                {
                    component.Transform = gameObject.Transform;
                    updateAction?.Invoke(component);
                    gameObject.Transform = component.Transform;
                });

                RlGl.PopMatrix();
            }
        }
        else
        {
            foreach (var gameObject in components)
            {
                gameObject.UpdateComponents(component =>
                {
                    component.Transform = gameObject.Transform;
                    updateAction?.Invoke(component);
                    gameObject.Transform = component.Transform;
                });
            }
        }
    }

    internal enum ElementUpdateType
    {
        Load,
        Update,
        Render,
        UiRender,
        Close,
        Debug,
        Fixed
    }
}