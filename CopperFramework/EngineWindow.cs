using CopperCore;
using CopperFramework.Utility;
using static Raylib_cs.Raylib;

namespace CopperFramework;

public class EngineWindow : Singleton<EngineWindow>
{
    public EngineWindow() => SetInstance(this);

    private EngineSettings Settings => Engine.Instance.Settings;

    public EngineWindowCamera Camera;
    
    private static float fixedDeltaTime = 0;
    private const float FixedFrameTime = 0.02f;

    private RenderTexture2D renderTexture;
    
    public void Start()
    {
        SetConfigFlags(Settings.WindowFlags);
        InitWindow(Settings.WindowSize.X, Settings.WindowSize.Y, Settings.WindowTitle);
        InitAudioDevice();
        SetTargetFPS(Settings.TargetFps);

        Camera = new EngineWindowCamera
        {
            Position = Vector2.Zero,
            Rotation = 0,
            Zoom = 1
        };

        renderTexture = LoadRenderTexture(Settings.WindowSize.X, Settings.WindowSize.Y);
    }

    public void Update(Action cameraRenderUpdate, Action uiRenderUpdate, Action fixedUpdate)
    {
        fixedDeltaTime += Time.DeltaTime;
        if (fixedDeltaTime >= FixedFrameTime)
        {
            fixedDeltaTime = 0;
            fixedUpdate.Invoke();
        }
        
        if (IsWindowResized())
        {
            UnloadRenderTexture(renderTexture);
            renderTexture = LoadRenderTexture(GetScreenWidth(), GetScreenHeight());
        }
        
        BeginTextureMode(renderTexture);
        
        BeginDrawing();
        ClearBackground(Color.RayWhite);
        
        BeginMode2D(Camera);
        
        cameraRenderUpdate.Invoke();
        
        EndMode2D();
        
        EndTextureMode();
        
        DrawTextureRec(renderTexture.Texture, new Rectangle(0, 0, renderTexture.Texture.Width, -renderTexture.Texture.Height), Vector2.Zero, Color.White);
        
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
        private Camera2D camera2D;

        public EngineWindowCamera()
        {
            camera2D = new Camera2D()
            {
                Offset = Vector2.Zero,
                Zoom = 1,
                Rotation = 0,
                Target = Vector2.Zero
            };
        }

        public static implicit operator Camera2D(EngineWindowCamera camera) => camera.camera2D;

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