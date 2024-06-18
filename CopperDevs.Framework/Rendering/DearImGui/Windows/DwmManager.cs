using CopperDevs.Core;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui;

namespace CopperDevs.Framework.Rendering.DearImGui.Windows;

public class DwmManager : BaseWindow
{
    public override string WindowName { get; protected set; } = "Dwm Manager";

    private static IntPtr WindowHandle => rlWindow.GetHandle();
    private Vector4 margins;

    public override void Update()
    {
        CopperImGui.Button("Log Handle", () => Log.Info(WindowHandle));
        
        CopperImGui.CollapsingHeader("Immersive Dark Mode", () =>
        {
            CopperImGui.Button("Enable", () => WindowsApi.SetDwmImmersiveDarkMode(WindowHandle, true));
            CopperImGui.Button("Disable", () => WindowsApi.SetDwmImmersiveDarkMode(WindowHandle, false));
        });

        CopperImGui.CollapsingHeader("System Backdrop Type", () =>
        {
            CopperImGui.Button("Auto", () => WindowsApi.SetDwmSystemBackdropType(WindowHandle, WindowsApi.SystemBackdropType.Auto));
            CopperImGui.Button("None", () => WindowsApi.SetDwmSystemBackdropType(WindowHandle, WindowsApi.SystemBackdropType.None));
            CopperImGui.Button("Mica", () => WindowsApi.SetDwmSystemBackdropType(WindowHandle, WindowsApi.SystemBackdropType.Mica));
            CopperImGui.Button("Acrylic", () => WindowsApi.SetDwmSystemBackdropType(WindowHandle, WindowsApi.SystemBackdropType.Acrylic));
            CopperImGui.Button("Tabbed", () => WindowsApi.SetDwmSystemBackdropType(WindowHandle, WindowsApi.SystemBackdropType.Tabbed));
        });

        CopperImGui.CollapsingHeader("Window Corner Preference", () =>
        {
            CopperImGui.Button("Default", () => WindowsApi.SetDwmWindowCornerPreference(WindowHandle, WindowsApi.WindowCornerPreference.Default));
            CopperImGui.Button("Do Not Round", () => WindowsApi.SetDwmWindowCornerPreference(WindowHandle, WindowsApi.WindowCornerPreference.DoNotRound));
            CopperImGui.Button("Round", () => WindowsApi.SetDwmWindowCornerPreference(WindowHandle, WindowsApi.WindowCornerPreference.Round));
            CopperImGui.Button("Round Small", () => WindowsApi.SetDwmWindowCornerPreference(WindowHandle, WindowsApi.WindowCornerPreference.RoundSmall));
        });

        CopperImGui.CollapsingHeader("Extend Frame Into Client Area", () =>
        {
            CopperImGui.DragValue("Margins", ref margins);

            CopperImGui.Button("Set Margins", () =>
            {
                WindowsApi.ExtendFrameIntoClientArea(WindowHandle, new WindowsApi.Margins()
                {
                    CxLeftWidth = (int)margins.X,
                    CxRightWidth = (int)margins.Y,
                    CyBottomHeight = (int)margins.Z,
                    CyTopHeight = (int)margins.W,
                });
            });
        });
    }
}