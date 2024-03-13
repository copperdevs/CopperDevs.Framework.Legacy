namespace CopperFramework.Data;

public static class Input
{
    public static Vector2 MousePosition => Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), EngineWindow.Instance.Camera);

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
            var value = Raylib.IsKeyDown(key) ? 1 : 0;
            return float.IsNaN(value) ? 0 : value;
        }
    }
    
    public static bool IsKeyDown(KeyboardKey key)
    {
        return Raylib.IsKeyDown(key);
    }
    
    public static bool IsKeyPressed(KeyboardKey key)
    {
        return Raylib.IsKeyPressed(key);
    }
    
    
    public static bool IsMouseButtonDown(MouseButton key)
    {
        return Raylib.IsMouseButtonDown(key);
    }
}