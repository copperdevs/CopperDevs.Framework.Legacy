using CopperDevs.Core.Utility;
using Raylib_CSharp.Interact;

namespace CopperDevs.Framework.Data;

public static class Input
{
    public static Vector2 MousePosition => Engine.CurrentWindow.Camera.GetScreenToWorld(rlInput.GetMousePosition());

    public static float IsKeyDown(KeyboardKey upKey, KeyboardKey downKey)
    {
        // first one only: 1
        // second one only: -1
        // both: 0
        var value = KeyDown(upKey) + -KeyDown(downKey);
        return float.IsNaN(value) ? 0 : value;

        float KeyDown(KeyboardKey key)
        {
            // down: 1
            // up: 0
            var keyDownValue = rlInput.IsKeyDown(key) ? 1 : 0;
            return float.IsNaN(keyDownValue) ? 0 : keyDownValue;
        }
    }

    public static bool IsKeyDown(KeyboardKey key) => rlInput.IsKeyDown(key);

    public static bool IsKeyPressed(KeyboardKey key) => rlInput.IsKeyPressed(key);

    public static bool IsMouseButtonDown(MouseButton key) => rlInput.IsMouseButtonDown(key);

    public static bool IsMouseButtonPressed(MouseButton button) => rlInput.IsMouseButtonPressed(button);

    public static bool IsMouseButtonReleased(MouseButton button) => rlInput.IsMouseButtonReleased(button);
}