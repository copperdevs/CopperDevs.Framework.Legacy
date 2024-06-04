using System.Runtime.InteropServices;

namespace CopperDevs.Core.Utility;

public static partial class WindowsApi
{
    private static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    private static int IntSize => sizeof(int);

    [LibraryImport("dwmapi.dll")]
    private static partial void DwmSetWindowAttribute(IntPtr window, WindowAttribute dwAttribute, ref int pvAttribute, int cbAttribute);

    [LibraryImport("dwmapi.dll")]
    private static partial void DwmExtendFrameIntoClientArea(IntPtr window, ref Margins pMarInset);

    [LibraryImport("kernel32.dll")]
    private static partial IntPtr GetConsoleWindow();

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [LibraryImport("user32.dll")]
    private static partial IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    public static void SetDwmWindowAttribute(IntPtr windowHandle, WindowAttribute dwAttribute, int pvAttribute)
    {
        DwmSetWindowAttribute(windowHandle, dwAttribute, ref pvAttribute, IntSize);
    }

    public static void SetDwmImmersiveDarkMode(IntPtr windowHandle, bool enableDarkMode)
    {
        if (IsWindows)
            SetDwmWindowAttribute(windowHandle, WindowAttribute.UseImmersiveDarkMode, enableDarkMode.ToInt());
    }

    public static void SetDwmSystemBackdropType(IntPtr windowHandle, SystemBackdropType backdropType)
    {
        if (IsWindows)
            SetDwmWindowAttribute(windowHandle, WindowAttribute.SystemBackdropType, (int)backdropType);
    }

    public static void SetDwmWindowCornerPreference(IntPtr windowHandle, WindowCornerPreference preference)
    {
        if (IsWindows)
            SetDwmWindowAttribute(windowHandle, WindowAttribute.WindowCornerPreference, (int)preference);
    }

    public static void ExtendFrameIntoClientArea(IntPtr windowHandle, Margins margins)
    {
        if (IsWindows)
            DwmExtendFrameIntoClientArea(windowHandle, ref margins);
    }

    public static IntPtr GetConsoleWindowPointer()
    {
        unsafe
        {
            return IsWindows ? GetConsoleWindow() : new IntPtr(null);
        }
    }

    public static void SetWindowState(IntPtr targetWindow, WindowState state)
    {
        ShowWindow(targetWindow, (int)state);
    }

    public static void SetWindowParent(IntPtr child, IntPtr parent)
    {
        SetParent(child, parent);
    }

    public enum WindowAttribute
    {
        UseImmersiveDarkMode = 20,
        WindowCornerPreference = 33,
        SystemBackdropType = 38
    }

    public enum SystemBackdropType
    {
        Auto,
        None,
        Mica,
        Acrylic,
        Tabbed
    }

    public enum WindowCornerPreference
    {
        Default,
        DoNotRound,
        Round,
        RoundSmall,
    }

    public enum WindowState
    {
        Hide,
        ShowNormal,
        ShowMinimized,
        ShowMaximized,
        ShowNoActivate,
        Show,
        Minimize,
        ShowMinNoActive,
        ShowNa,
        Restore,
        ShowDefault,
        ForceMinimize
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Margins
    {
        public int CxLeftWidth;
        public int CxRightWidth;
        public int CyTopHeight;
        public int CyBottomHeight;
    }
}