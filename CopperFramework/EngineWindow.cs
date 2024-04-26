using System.Reflection;
using CopperFramework.Utility;
using static Raylib_cs.Raylib;

namespace CopperFramework;

public class EngineWindow
{
    public static Vector2 Size => new Vector2(GetScreenWidth(), GetScreenHeight());
    
    public EngineWindow(EngineSettings settings)
    {
        this.settings = settings;
    }

    private readonly EngineSettings settings;

    public EngineWindowCamera Camera;
    
    internal static float FixedDeltaTime = 0;
    private const float FixedFrameTime = 0.02f;

    public RenderTexture2D RenderTexture { get; private set; }
    public bool ScreenShaderEnabled = true;
    public Shader? ScreenShader { get; private set; }
    
    public void SetScreenShader(Shader? shader) => ScreenShader = shader;

    public void Start()
    {
        SetConfigFlags(settings.WindowFlags);
        InitWindow(settings.WindowSize.X, settings.WindowSize.Y, string.IsNullOrWhiteSpace(settings.WindowTitle) ? Assembly.GetEntryAssembly()?.FullName : settings.WindowTitle);
        InitAudioDevice();
        SetTargetFPS(settings.TargetFps);

        Camera = new EngineWindowCamera
        {
            Position = Vector2.Zero,
            Rotation = 0,
            Zoom = 1
        };

        RenderTexture = LoadRenderTexture(settings.WindowSize.X, settings.WindowSize.Y);
        Shader.LoadQueue();
    }

    public void Update(Action cameraRenderUpdate, Action uiRenderUpdate, Action fixedUpdate)
    {
        FixedDeltaTime += Time.DeltaTime;
        if (FixedDeltaTime >= FixedFrameTime)
        {
            FixedDeltaTime = 0;
            fixedUpdate.Invoke();
        }
        
        if (IsWindowResized())
        {
            UnloadRenderTexture(RenderTexture);
            RenderTexture = LoadRenderTexture(GetScreenWidth(), GetScreenHeight());
        }
        
        BeginTextureMode(RenderTexture);
        
        BeginDrawing();
        ClearBackground(Color.RayWhite);
        
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
        CloseAudioDevice();
        CloseWindow();
    }

    public struct EngineWindowCamera
    {
        public EngineWindowCamera()
        {
            
        }
        
        private rlCamera2D camera2D = new()
        {
            Offset = Vector2.Zero,
            Zoom = 1,
            Rotation = 0,
            Target = Vector2.Zero
        };

        public static implicit operator rlCamera2D(EngineWindowCamera camera) => camera.camera2D;

        public Vector2 Position
        {
            get => camera2D.Target;
            set => camera2D.Target = value;
        }

        public float Zoom
        {
            get => camera2D.Zoom;
            set => camera2D.Zoom = value;
        }

        public float Rotation
        {
            get => camera2D.Rotation;
            set => camera2D.Rotation = value;
        }
    }
}