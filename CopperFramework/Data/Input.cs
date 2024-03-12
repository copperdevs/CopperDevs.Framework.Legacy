using System.Numerics;
using CopperPlatformer.Core;
using Raylib_cs;

namespace CopperFramework.Data;

public static class Input
{
    public static Vector2 MousePosition => Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), EngineWindow.Instance.Camera);

    public static float IsKeyDown(KeyboardKey upKey, KeyboardKey downKey)
    {
        // first one only: 1
        // second one only: -1
        // both: 0
        var value = IsKeyDown(upKey) + -IsKeyDown(downKey);
        return float.IsNaN(value) ? 0 : value;
    }

    public static float IsKeyDown(KeyboardKey key)
    {
        // down: 1
        // up: 0
        var value = Raylib.IsKeyDown(key) ? 1 : 0;
        return float.IsNaN(value) ? 0 : value;
    }
}