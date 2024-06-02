using System.Runtime.InteropServices;
using CopperDevs.Core;
using Raylib_CSharp.Logging;

namespace CopperDevs.Framework.Utility;

public static unsafe partial class ConsoleUtil
{
    private static bool RayLibLog(TraceLogLevel level, string message)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (level)
        {
            case TraceLogLevel.Info:
                Log.Info(message);
                break;
            case TraceLogLevel.Error:
                Log.Error(message);
                break;
            case TraceLogLevel.Warning:
                Log.Warning(message);
                break;
            default:
                Log.Info(message);
                break;
        }

        return true;
    }

    internal static void Initialize()
    {
        Logger.Init();
        Logger.Message += RayLibLog;
        Logger.SetTraceLogLevel(TraceLogLevel.All);
    }
    
    [LibraryImport("kernel32.dll")]
    private static partial IntPtr GetConsoleWindow();

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;

    public static bool IsConsoleActive
    {
        get => isShown;
        set
        {
            isShown = value;
            ShowWindow(GetConsoleWindow(), isShown ? SW_SHOW : SW_HIDE);
        }
    }
    private static bool isShown = false;

}