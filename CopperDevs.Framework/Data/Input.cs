using CopperDevs.Core;
using CopperDevs.Core.Utility;
using ImGuiNET;
using nkast.Aether.Physics2D.Common;
using Raylib_CSharp.Interact;
using Vector2 = System.Numerics.Vector2;

namespace CopperDevs.Framework.Data;

public static class Input
{
    public static Vector2 RawMousePosition
    {
        get
        {
            return WindowsApi.IsWindows ? GetWindowsApiMousePos() : GetRaylibMousePos();

            Vector2 GetWindowsApiMousePos()
            {
                var windowInfo = WindowsApi.GetWindowPosition(rlWindow.GetHandle());
                return WindowsApi.GetMousePosition() - new Vector2(windowInfo.X+8, windowInfo.Y+31);
            }

            Vector2 GetRaylibMousePos()
            {
                return Engine.Instance.Camera.GetScreenToWorld(rlInput.GetMousePosition());
            }
        }
    }
    
    public static Vector2 MousePosition
    {
        get
        {
            var pos = RawMousePosition;

            if (!Engine.Instance.DebugEnabled) 
                return pos;
            
            pos = MathUtil.ReMap(pos, Engine.Instance.GameWindowPositionOne, Engine.Instance.GameWindowPositionTwo, Vector2.Zero, Engine.Instance.WindowSize);

            return pos;
        }
    }

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