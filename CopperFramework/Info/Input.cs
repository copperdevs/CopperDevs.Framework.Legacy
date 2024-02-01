using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Input.Extensions;

namespace CopperFramework.Info;

public static class Input
{
    public static IKeyboard? primaryKeyboard { get; internal set; }

    public static Action<IMouse, Vector2>? MouseMove;

    internal static readonly List<bool> IsMouseButtonDownStates = new(12)
    {
        false, false, false, false, false, false, false, false, false, false, false, false,
    };

    public static CursorMode CursorMode
    {
        set
        {
            CopperWindow.input.Mice.ToList().ForEach(mouse => mouse.Cursor.CursorMode = value);
        }
    }

    public static bool IsKeyPressed(Key key)
    {
        return primaryKeyboard is not null && primaryKeyboard.IsKeyPressed(key);
    }

    public static bool IsMouseButtonDown(MouseButton button)
    {
        return IsMouseButtonDownStates[(int)(button + 1)];
    }

    public static bool IsMouseButtonUp(MouseButton button)
    {
        return !IsMouseButtonDownStates[(int)(button + 1)];
    }
}