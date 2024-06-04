namespace CopperDevs.Framework.Data;

public static class Time
{
    public static float GameTime => (float)rlTime.GetTime();
    public static float DeltaTime => rlTime.GetFrameTime();
    public static void Wait(float time) => rlTime.WaitTime(time);

    public static int Fps
    {
        get => rlTime.GetFPS();
        set => rlTime.SetTargetFPS(value);
    }

    public static void Invoke(Action method, float delay)
    {
        Task.Delay(new TimeSpan(0, 0, 0, 0, (int)(delay * 1000))).ContinueWith(_ => { method.Invoke(); });
    }

    public static void Invoke(Action method, TimeSpan delay)
    {
        Task.Delay(delay).ContinueWith(_ => { method.Invoke(); });
    }
}