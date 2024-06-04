using System.Runtime.InteropServices;

namespace CopperDevs.Framework.Utility;

// https://github.com/LemonCaramel/Mica/blob/master/common/src/main/java/moe/caramel/mica/natives/DwmApi.java#L68
public static partial class DwmApi
{
    private static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    private static IntPtr WindowHandle => rlWindow.GetHandle();
    private static int IntSize => sizeof(int);

    [LibraryImport("dwmapi.dll")]
    private static partial void DwmSetWindowAttribute(IntPtr window, WindowAttribute dwAttribute, ref int pvAttribute, int cbAttribute);

    [LibraryImport("dwmapi.dll")]
    private static partial void DwmExtendFrameIntoClientArea(IntPtr window, ref Margins pMarInset);
    
    public static void SetWindowAttribute(WindowAttribute dwAttribute, int pvAttribute)
    {
        DwmSetWindowAttribute(WindowHandle, dwAttribute, ref pvAttribute, IntSize);
    }

    public static void SetImmersiveDarkMode(bool enableDarkMode)
    {
        if (IsWindows)
            SetWindowAttribute(WindowAttribute.UseImmersiveDarkMode, enableDarkMode.ToInt());
    }

    public static void SetSystemBackdropType(SystemBackdropType backdropType)
    {
        if (IsWindows)
            SetWindowAttribute(WindowAttribute.SystemBackdropType, (int)backdropType);
    }

    public static void SetWindowCornerPreference(WindowCornerPreference preference)
    {
        if (IsWindows)
            SetWindowAttribute(WindowAttribute.WindowCornerPreference, (int)preference);
    }

    public static void ExtendFrameIntoClientArea(Margins margins)
    {
        if (IsWindows)
            DwmExtendFrameIntoClientArea(WindowHandle, ref margins);
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

    [StructLayout(LayoutKind.Sequential)]
    public struct Margins
    {
        public int CxLeftWidth;
        public int CxRightWidth;
        public int CyTopHeight;
        public int CyBottomHeight;
    }
}