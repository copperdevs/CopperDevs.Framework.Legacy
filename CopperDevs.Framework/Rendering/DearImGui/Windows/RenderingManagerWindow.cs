using CopperDevs.Core;
using CopperDevs.DearImGui;
using CopperDevs.DearImGui.Attributes;
using CopperDevs.DearImGui.Utility;
using CopperDevs.Framework.Utility;
using ImGuiNET;

namespace CopperDevs.Framework.Rendering.DearImGui.Windows;

[Window("Rendering Manager", WindowOpen = true)]
public class RenderingManagerWindow : BaseWindow
{
    public override void WindowStart()
    {
        CopperImGui.RegisterPopup("BuiltInShaderPopup", BuiltInShaderPopup);
    }

    public override void WindowStop()
    {
        CopperImGui.DeregisterPopup("BuiltInShaderPopup");
    }

    public override void WindowUpdate()
    {
        CopperImGui.TabGroup("rendering_manager_window_tab_group",
            ("General Info", GeneralInfo),
            ("Shader Info", ShaderInfo),
            ("Window Render Texture", WindowRenderTexture),
            ("Font Info", FontInfo),
            ("Reflection Renderers Info", ImGuiReflectionRenderersInfo));
    }

    private static void GeneralInfo()
    {
        CopperImGui.Text(Time.Fps, "Fps");
    }

    private static void ShaderInfo()
    {
        CopperImGui.Checkbox("Engine Window Screen Shader Enabled", ref Engine.Instance.ScreenShaderEnabled);

        CopperImGui.CollapsingHeader("Engine Window Screen Shaders", () =>
        {
            var tempList = Engine.Instance.ScreenShaders.ToList();

            for (var index = 0; index < Engine.Instance.ScreenShaders.Count; index++)
            {
                var shader = Engine.Instance.ScreenShaders[index];

                CopperImGui.CollapsingHeader($"{shader.Name}", () =>
                {
                    using (new DisabledScope(index == 0))
                        CopperImGui.Button($"Move Up###u{shader.Name}{index}", () => tempList = Util.Swap(Engine.Instance.ScreenShaders, index, index - 1).ToList());

                    using (new DisabledScope(Engine.Instance.ScreenShaders.Count - 1 == index))
                        CopperImGui.Button($"Move Down###d{shader.Name}{index}", () => tempList = Util.Swap(Engine.Instance.ScreenShaders, index, index + 1).ToList());

                    CopperImGui.Button($"Remove###{shader.Name}{index}", () => tempList.RemoveAt(index));
                });
            }

            Engine.Instance.ScreenShaders = tempList;
        });

        CopperImGui.Separator();

        CopperImGui.ForceRenderPopup("BuiltInShaderPopup");
        CopperImGui.Button("Load Built In Shader", () => CopperImGui.ShowPopup("BuiltInShaderPopup"));

        CopperImGui.Separator();

        CopperImGui.CollapsingHeader("Loaded Shaders", () =>
        {
            var shaders = RenderingSystem.Instance.GetRenderableItems<Shader>();
            foreach (var shader in shaders)
            {
                CopperImGui.CollapsingHeader($"{shader.Name}###{shaders.IndexOf(shader)}", () =>
                {
                    CopperImGui.HorizontalGroup(() => { CopperImGui.Button($"Add to screen shaders###a{shader.Name}{shaders.IndexOf(shader)}", () => { Engine.Instance.AddScreenShader(shader); }); },
                        () => { CopperImGui.Button($"Remove from screen shaders###r{shader.Name}{shaders.IndexOf(shader)}", () => { Engine.Instance.RemoveScreenShader(shader); }); });

                    CopperImGui.Button($"Unload Shader###{shader.Name}{shaders.IndexOf(shader)}", () => { shader.UnLoadRenderable(); });

                    if (!string.IsNullOrEmpty(shader.VertexShaderData))
                        CopperImGui.CollapsingHeader($"Vertex Shader Data###vertex{shader.Name}", () => { CopperImGui.Text(shader.VertexShaderData); });

                    if (!string.IsNullOrEmpty(shader.FragmentShaderData))
                        CopperImGui.CollapsingHeader($"Fragment Shader Data###fragment{shader.Name}", () => { CopperImGui.Text(shader.FragmentShaderData); });
                });
            }
        });
    }

    private static void WindowRenderTexture()
    {
        CopperImGui.CollapsingHeader("Game Render Texture", () =>
        {
            var renderTexture = (object)Engine.Instance.GameRenderTexture;
            CopperImGui.RenderObjectValues(ref renderTexture);
        });

        CopperImGui.CollapsingHeader("Shader Render Texture", () =>
        {
            var renderTexture = (object)Engine.Instance.ShaderRenderTexture;
            CopperImGui.RenderObjectValues(ref renderTexture, 1);
        });
    }

    private static void FontInfo()
    {
        CopperImGui.CollapsingHeader("Loaded Fonts", () =>
        {
            var list = RenderingSystem.Instance.GetRenderableItems<Font>();
            for (var i = 0; i < list.Count; i++)
            {
                var font = list[i];
                CopperImGui.CollapsingHeader(font.Name, () =>
                {
                    CopperImGui.Text(font.BaseSize, "Base Size");
                    CopperImGui.Text(font.GlyphCount, "Glyph Count");
                    CopperImGui.Text(font.GlyphPadding, "Glyph Padding");

                    var targetObject = font.Texture;
                    CopperImGui.RenderObjectValues(ref targetObject, i);
                });
            }
        });
    }

    private static void BuiltInShaderPopup()
    {
        foreach (var includedShader in Enum.GetValues(typeof(IncludedShaders)))
        {
            CopperImGui.Selectable(includedShader, () => Shader.Load((IncludedShaders)includedShader));
        }
    }

    private static void ImGuiReflectionRenderersInfo()
    {
        foreach (var renderer in CopperImGui.GetAllImGuiRenderers())
        {
            CopperImGui.Text($"{renderer.Value}", $"{renderer.Key.Name}");
        }
    }
}