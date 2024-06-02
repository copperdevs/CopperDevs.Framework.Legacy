namespace CopperDevs.Framework.Data;

public static class Time
{
    public static float GameTime => (float)rlTime.GetTime();
    public static float DeltaTime => rlTime.GetFrameTime();
    public static void Wait(float time) => rlTime.WaitTime(time);
}