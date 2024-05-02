using System.Runtime.InteropServices;
using System.Text;
using CopperCore;

namespace CopperFramework.Utility;

public static unsafe class ConsoleUtil
{
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    private static void RayLibLog(int msgType, sbyte* text, sbyte* args)
    {
        var message = Logging.GetLogMessage(new IntPtr(text), new IntPtr(args));

        switch ((TraceLogLevel)msgType)
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
    }

    internal static void Initialize()
    {
        Raylib.SetTraceLogCallback(&RayLibLog);
        Raylib.SetTraceLogLevel(TraceLogLevel.All);
    }
    
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

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