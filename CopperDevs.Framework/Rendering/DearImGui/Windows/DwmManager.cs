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
            CopperImGui.Button("Enable", () => DwmApi.SetImmersiveDarkMode(true));
            CopperImGui.Button("Disable", () => DwmApi.SetImmersiveDarkMode(false));
        });
        
        CopperImGui.CollapsingHeader("System Backdrop Type", () =>
        {
            CopperImGui.Button("Auto", () => DwmApi.SetSystemBackdropType(DwmApi.SystemBackdropType.Auto));
            CopperImGui.Button("None", () => DwmApi.SetSystemBackdropType(DwmApi.SystemBackdropType.None));
            CopperImGui.Button("Mica", () => DwmApi.SetSystemBackdropType(DwmApi.SystemBackdropType.Mica));
            CopperImGui.Button("Acrylic", () => DwmApi.SetSystemBackdropType(DwmApi.SystemBackdropType.Acrylic));
            CopperImGui.Button("Tabbed", () => DwmApi.SetSystemBackdropType(DwmApi.SystemBackdropType.Tabbed));
        });
        
        CopperImGui.CollapsingHeader("Window Corner Preference", () =>
        {
            CopperImGui.Button("Default", () => DwmApi.SetWindowCornerPreference(DwmApi.WindowCornerPreference.Default));
            CopperImGui.Button("Do Not Round", () => DwmApi.SetWindowCornerPreference(DwmApi.WindowCornerPreference.DoNotRound));
            CopperImGui.Button("Round", () => DwmApi.SetWindowCornerPreference(DwmApi.WindowCornerPreference.Round));
            CopperImGui.Button("Round Small", () => DwmApi.SetWindowCornerPreference(DwmApi.WindowCornerPreference.RoundSmall));
        });
        
        CopperImGui.CollapsingHeader("Extend Frame Into Client Area", () =>
        {
            CopperImGui.DragValue("Margins", ref margins);
            
            CopperImGui.Button("Set Margins", () =>
            {
                DwmApi.ExtendFrameIntoClientArea(new DwmApi.Margins()
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