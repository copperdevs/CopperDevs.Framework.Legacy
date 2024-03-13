using System.Runtime.InteropServices;
using System.Text;
using CopperCore;

namespace CopperPlatformer.Core.Utility;

public static unsafe class ConsoleUtil
{
    [DllImport("msvcrt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    private static extern int vsprintf(StringBuilder buffer, string format, IntPtr args);

    [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern int _vscprintf(string format, IntPtr ptr);

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    private static void RayLibLog(int msgType, sbyte* text, sbyte* args)
    {
        var textStr = Marshal.PtrToStringUTF8((IntPtr)text) ?? "";

        var sb = new StringBuilder(_vscprintf(textStr, (IntPtr)args) + 1);
        vsprintf(sb, textStr, (IntPtr)args);

        var messageLog = sb.ToString();

        switch ((TraceLogLevel)msgType)
        {
            case TraceLogLevel.Info:
                Log.Info(messageLog);
                break;
            case TraceLogLevel.Error:
                Log.Error(messageLog);
                break;
            case TraceLogLevel.Warning:
                Log.Warning(messageLog);
                break;
            default:
                Log.Info(messageLog);
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