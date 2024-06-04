using System.Reflection;
using CopperDevs.Core.Data;
using CopperDevs.Framework.Rendering;
using CopperDevs.Framework.Utility;

using Raylib_CSharp.Transformations;

using static Raylib_CSharp.Windowing.Window;
using static Raylib_CSharp.Rendering.Graphics;

namespace CopperDevs.Framework;

public class EngineWindow
{
    public static Vector2Int Size => new(rlWindow.GetScreenWidth(), rlWindow.GetScreenHeight());
    
    public EngineWindow(EngineSettings settings)
    {
        this.settings = settings;
    }

    private readonly EngineSettings settings;

    public EngineCamera Camera;
    
    internal static float FixedDeltaTime = 0;
    private const float FixedFrameTime = 0.02f;

    public rlRenderTexture RenderTexture { get; private set; }
    public bool ScreenShaderEnabled = true;
    
    public Shader? ScreenShader { get; private set; }
    public Color BackgroundColor { get; private set; } = Color.RayWhite;
    
    public void SetScreenShader(Shader? shader) => ScreenShader = shader;
    public void SetWindowColor(Color color) => BackgroundColor = color;

    public void Start()
    {
        Raylib.SetConfigFlags(settings.WindowFlags);
        Init(settings.WindowSize.X, settings.WindowSize.Y, (string.IsNullOrWhiteSpace(settings.WindowTitle) ? Assembly.GetEntryAssembly()?.FullName : settings.WindowTitle)!);
        rlAudio.Init();
        rlTime.SetTargetFPS(settings.TargetFps);

        Camera = new EngineCamera
        {
            Position = Vector2.Zero,
            Rotation = 0,
            Zoom = 1
        };

        RenderTexture = rlRenderTexture.Load(settings.WindowSize.X, settings.WindowSize.Y);

        if (settings.DwpApiCustomization)
        {
            WindowsApi.SetDwmImmersiveDarkMode(true);
            WindowsApi.SetDwmSystemBackdropType(WindowsApi.SystemBackdropType.Acrylic);
            WindowsApi.SetDwmWindowCornerPreference(WindowsApi.WindowCornerPreference.Default);
        }
    }

    public void Update(Action cameraRenderUpdate, Action uiRenderUpdate, Action fixedUpdate)
    {
        FixedDeltaTime += Time.DeltaTime;
        if (FixedDeltaTime >= FixedFrameTime)
        {
            FixedDeltaTime = 0;
            fixedUpdate.Invoke();
        }
        
        if (IsResized())
        {
            RenderTexture.Unload();
            RenderTexture = rlRenderTexture.Load(Size.X, Size.Y);
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
}