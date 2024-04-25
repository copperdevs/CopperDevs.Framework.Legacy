using CopperDearImGui;
using CopperDearImGui.Utility;

namespace CopperFramework.Rendering.DearImGui.Windows;

public class RenderingManagerWindow : BaseWindow
{
    public override string WindowName { get; protected set; } = "Rendering Manager";

    public override void Update()
    {
        CopperImGui.TabGroup("rendering_manager_window_tab_group",
            ("Shader Info", ShaderInfo),
            ("Window Render Texture", WindowRenderTexture));
    }

    private static void ShaderInfo()
    {
        CopperImGui.Checkbox("Engine Window Screen Shader Enabled", ref Engine.CurrentWindow.ScreenShaderEnabled);

        CopperImGui.CollapsingHeader("Loaded Shaders", () =>
        {
            using (new IndentScope())
            {
                foreach (var shader in RenderingManager.GetRenderableItems<Shader>())
                {
                    CopperImGui.CollapsingHeader(shader.Name, () =>
                    {
                        using (new IndentScope())
                        {
                            CopperImGui.Selectable($"Set to engine screen shader###{shader.Name}", () => Engine.CurrentWindow.SetScreenShader(shader));

                            CopperImGui.CollapsingHeader($"Vertex Shader Data###{shader.Name}", () =>
                            {
                                using (new IndentScope())
                                {
                                    CopperImGui.Text(shader.VertexShaderData);
                                }
                            });

                            CopperImGui.CollapsingHeader($"Fragment Shader Data###{shader.Name}", () =>
                            {
                                using (new IndentScope())
                                {
                                    CopperImGui.Text(shader.FragmentShaderData);
                                }
                            });
                        }
                    });
                }
            }
        });
    }

    private static void WindowRenderTexture()
    {
        var renderTexture = (object)Engine.CurrentWindow.RenderTexture;
        CopperImGui.GetFieldRenderer<RenderTexture2D>()?.ValueRenderer(ref renderTexture, 0);
    }
}