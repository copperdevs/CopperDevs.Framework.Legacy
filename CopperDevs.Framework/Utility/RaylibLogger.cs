using CopperDevs.Core;
using Raylib_CSharp.Logging;

namespace CopperDevs.Framework.Utility;

public static class RaylibLogger
{
    public static void Initialize()
    {
        Logger.Init();
        Logger.Message += RayLibLog;
        Logger.SetTraceLogLevel(TraceLogLevel.All);
    }

    private static bool RayLibLog(TraceLogLevel level, string message)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (level)
        {
            case TraceLogLevel.Info:
                Log.Info($"[RAYLIB] {message}");
                break;
            case TraceLogLevel.Error:
                Log.Error($"[RAYLIB] {message}");
                break;
            case TraceLogLevel.Warning:
                Log.Warning($"[RAYLIB] {message}");
                break;
            default:
                Log.Info($"[RAYLIB] {message}");
                break;
        }

        return true;
    }
}