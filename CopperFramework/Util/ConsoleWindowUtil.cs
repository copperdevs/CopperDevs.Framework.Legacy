using System.Runtime.InteropServices;

namespace CopperFramework.Util;

public static class ConsoleWindowUtil
{
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SwHide = 0;
    private const int SwShow = 5;

    private static bool windowShown = false;

    public static void ShowConsoleWindow()
    {
#if !DEBUG
        ShowWindow(GetConsoleWindow(), SW_SHOW);
#endif
    }

    public static void HideConsoleWindow()
    {
#if !DEBUG
        ShowWindow(GetConsoleWindow(), SW_HIDE);
#endif
    }

    public static void ToggleConsoleWindow()
    {
#if !DEBUG
        ShowWindow(GetConsoleWindow(), windowShown ? SW_HIDE : SW_SHOW);
        windowShown = !windowShown;
#endif
    }
}