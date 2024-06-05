using System.Reflection;
using CopperDevs.Core.Data;
using CopperDevs.Core.Utility;
using CopperDevs.Framework.Rendering;
using CopperDevs.Framework.Utility;
using Raylib_CSharp.Transformations;
using static Raylib_CSharp.Windowing.Window;
using static Raylib_CSharp.Rendering.Graphics;

namespace CopperDevs.Framework;

public class EngineWindow(EngineSettings settings)
{
    public static Vector2Int Size => new(GetScreenWidth(), GetScreenHeight());

    public EngineCamera Camera;

    private static float fixedDeltaTime;
    public const float FixedFrameTime = 0.02f;

    public rlRenderTexture RenderTexture { get; private set; }
    public bool ScreenShaderEnabled = true;

    public Shader? ScreenShader { get; private set; }
    public Color BackgroundColor { get; private set; } = Color.RayWhite;

    public Action<Vector2Int> OnResize = null!;

    public void Start()
    {
        Raylib.SetConfigFlags(settings.WindowFlags);
        Init(settings.WindowSize.X, settings.WindowSize.Y, (string.IsNullOrWhiteSpace(settings.WindowTitle) ? Assembly.GetEntryAssembly()?.FullName : settings.WindowTitle)!);
        rlAudio.Init();
        Time.Fps = settings.TargetFps;

        Camera = new EngineCamera
        {
            Position = Vector2.Zero,
            Rotation = 0,
            Zoom = 1
        };

        RenderTexture = rlRenderTexture.Load(settings.WindowSize.X, settings.WindowSize.Y);

        if (!settings.DwpApiCustomization)
            return;

        WindowsApi.SetDwmImmersiveDarkMode(GetHandle(), true);
        WindowsApi.SetDwmSystemBackdropType(GetHandle(), WindowsApi.SystemBackdropType.Acrylic);
        WindowsApi.SetDwmWindowCornerPreference(GetHandle(), WindowsApi.WindowCornerPreference.Default);
        WindowsApi.RegisterWindow(GetHandle());
    }

    public void Update(Action cameraRenderUpdate, Action uiRenderUpdate, Action fixedUpdate)
    {
        fixedDeltaTime += Time.DeltaTime;
        if (fixedDeltaTime >= FixedFrameTime)
        {
            fixedDeltaTime = 0;
            fixedUpdate.Invoke();
        }

        if (IsResized())
        {
            RenderTexture.Unload();
            RenderTexture = rlRenderTexture.Load(Size.X, Size.Y);
            OnResize?.Invoke(Size);
        }

        BeginTextureMode(RenderTexture);

        BeginDrawing();
        ClearBackground(BackgroundColor);

        BeginMode2D(Camera);

        cameraRenderUpdate.Invoke();

        EndMode2D();

        EndTextureMode();

        using (new ShaderScope(ScreenShader!, (ScreenShader is not null) && ScreenShaderEnabled))
        {
            DrawTextureRec(RenderTexture.Texture, new Rectangle(0, 0, RenderTexture.Texture.Width, -RenderTexture.Texture.Height), Vector2.Zero, Color.White);
        }

        uiRenderUpdate.Invoke();

        EndDrawing();
    }

    public void Shutdown()
    {
        rlAudio.Close();
        Close();
    }

    public void SetScreenShader(Shader? shader) => ScreenShader = shader;
    public void SetWindowColor(Color color) => BackgroundColor = color;
}