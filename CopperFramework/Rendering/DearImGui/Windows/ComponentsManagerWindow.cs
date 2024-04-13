using CopperDearImGui;
using CopperDearImGui.Utility;
using CopperFramework.Elements.Components;

namespace CopperFramework.Rendering.DearImGui.Windows;

public class ComponentsManagerWindow : BaseWindow
{
    public override string WindowName { get; protected set; } = "Components Manager";

    public override void Update()
    {
        CopperImGui.TabGroup("components_manager_window_tab_group", 
            ("Abstract Children", AbstractChildTab),
            ("Components Viewer", ComponentsViewerTab));
    }

    private void AbstractChildTab()
    {
        foreach (var abstractChild in ComponentRegistry.AbstractChildren)
        {
            CopperImGui.CollapsingHeader($"{abstractChild.Key.FullName}", () =>
            {
                using (new IndentScope())
                {
                    foreach (var type in abstractChild.Value)
                    {
                        CopperImGui.Text(type.FullName);
                    }
                }
            });
        }
    }

    private void ComponentsViewerTab()
    {
        foreach (var type in ComponentRegistry.ComponentTypes)
        {
            CopperImGui.CollapsingHeader($"{type.Name}", () =>
            {
                using (new IndentScope())
                {
                    CopperImGui.Text(type.Namespace!, "Namespace");
                    CopperImGui.Text(type.FullName!, "Full Name");
                    
                    CopperImGui.Separator();
                    
                    CopperImGui.Text(type.Assembly, "Assembly");
                    CopperImGui.Text(type.Module, "Module");
                    
                    CopperImGui.Separator();
                    
                    CopperImGui.Text(type.BaseType?.Name!, "Base Type Name");
                    CopperImGui.Text(type.BaseType?.FullName!, "Base Type Full Name");
                    
                    CopperImGui.Separator();
                    
                    CopperImGui.Text(type.IsAbstract, "Is Abstract");
                    CopperImGui.Text(type.IsImport, "Is Import");
                    CopperImGui.Text(type.IsSealed, "Is Sealed");
                    CopperImGui.Text(type.IsSpecialName, "Is Special Name");
                }
            });
        }
    }
}