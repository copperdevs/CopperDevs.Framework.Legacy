using CopperDevs.DearImGui;
using CopperDevs.Framework.Utility;

namespace CopperDevs.Framework.Rendering.DearImGui.Windows;

public class DwmManager : BaseWindow
{
    public override string WindowName { get; protected set; } = "Dwm Manager";

    private Vector4 margins;

    public override void Update()
    {
        CopperImGui.CollapsingHeader("Immersive Dark Mode", () =>
        {
            CopperImGui.Button("Enable", () => WindowsApi.SetDwmImmersiveDarkMode(true));
            CopperImGui.Button("Disable", () => WindowsApi.SetDwmImmersiveDarkMode(false));
        });
        
        CopperImGui.CollapsingHeader("System Backdrop Type", () =>
        {
            CopperImGui.Button("Auto", () => WindowsApi.SetDwmSystemBackdropType(WindowsApi.SystemBackdropType.Auto));
            CopperImGui.Button("None", () => WindowsApi.SetDwmSystemBackdropType(WindowsApi.SystemBackdropType.None));
            CopperImGui.Button("Mica", () => WindowsApi.SetDwmSystemBackdropType(WindowsApi.SystemBackdropType.Mica));
            CopperImGui.Button("Acrylic", () => WindowsApi.SetDwmSystemBackdropType(WindowsApi.SystemBackdropType.Acrylic));
            CopperImGui.Button("Tabbed", () => WindowsApi.SetDwmSystemBackdropType(WindowsApi.SystemBackdropType.Tabbed));
        });
        
        CopperImGui.CollapsingHeader("Window Corner Preference", () =>
        {
            CopperImGui.Button("Default", () => WindowsApi.SetDwmWindowCornerPreference(WindowsApi.WindowCornerPreference.Default));
            CopperImGui.Button("Do Not Round", () => WindowsApi.SetDwmWindowCornerPreference(WindowsApi.WindowCornerPreference.DoNotRound));
            CopperImGui.Button("Round", () => WindowsApi.SetDwmWindowCornerPreference(WindowsApi.WindowCornerPreference.Round));
            CopperImGui.Button("Round Small", () => WindowsApi.SetDwmWindowCornerPreference(WindowsApi.WindowCornerPreference.RoundSmall));
        });
        
        CopperImGui.CollapsingHeader("Extend Frame Into Client Area", () =>
        {
            CopperImGui.DragValue("Margins", ref margins);
            
            CopperImGui.Button("Set Margins", () =>
            {
                WindowsApi.ExtendFrameIntoClientArea(new WindowsApi.Margins()
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