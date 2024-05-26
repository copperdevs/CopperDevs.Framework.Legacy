namespace CopperDevs.Framework.Data;

public static class Time
{
    public static float GameTime => (float)Raylib.GetTime();
    public static float DeltaTime => Raylib.GetFrameTime();
    public static void Wait(float time) => Raylib.WaitTime(time);
}