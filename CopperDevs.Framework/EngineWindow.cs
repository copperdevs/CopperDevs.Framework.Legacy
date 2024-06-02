using System.Reflection;
using CopperDevs.Core.Data;
using CopperDevs.Framework.Utility;
using Raylib_CSharp.Audio;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Transformations;

namespace CopperDevs.Framework;

public class EngineWindow
{
    public static Vector2Int Size => new(rlWindow.GetScreenWidth(), rlWindow.GetScreenHeight());
    
    public EngineWindow(EngineSettings settings)
    {
        this.settings = settings;
    }

    private readonly EngineSettings settings;

    public EngineWindowCamera Camera;
    
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
        rlWindow.Init(settings.WindowSize.X, settings.WindowSize.Y, (string.IsNullOrWhiteSpace(settings.WindowTitle) ? Assembly.GetEntryAssembly()?.FullName : settings.WindowTitle)!);
        rlAudio.Init();
        rlTime.SetTargetFPS(settings.TargetFps);

        Camera = new EngineWindowCamera
        {
            Position = Vector2.Zero,
            Rotation = 0,
            Zoom = 1
        };

        RenderTexture = rlRenderTexture.Load(settings.WindowSize.X, settings.WindowSize.Y);
    }

    public void Update(Action cameraRenderUpdate, Action uiRenderUpdate, Action fixedUpdate)
    {
        FixedDeltaTime += Time.DeltaTime;
        if (FixedDeltaTime >= FixedFrameTime)
        {
            FixedDeltaTime = 0;
            fixedUpdate.Invoke();
        }
        
        if (rlWindow.IsResized())
        {
            RenderTexture.Unload();
            RenderTexture = rlRenderTexture.Load(Size.X, Size.Y);
        }
        
        rlGraphics.BeginTextureMode(RenderTexture);
        
        rlGraphics.BeginDrawing();
        rlGraphics.ClearBackground(BackgroundColor);
        
        rlGraphics.BeginMode2D(Camera);
        
        cameraRenderUpdate.Invoke();
        
        rlGraphics.EndMode2D();
        
        rlGraphics.EndTextureMode();
        
        using (new ShaderScope(ScreenShader!, (ScreenShader is not null) && ScreenShaderEnabled))
        {
            rlGraphics.DrawTextureRec(RenderTexture.Texture, new Rectangle(0, 0, RenderTexture.Texture.Width, -RenderTexture.Texture.Height), Vector2.Zero, Color.White);
        }
        
        uiRenderUpdate.Invoke();
        
        rlGraphics.EndDrawing();
    }

    public void Shutdown()
    {
        rlAudio.Close();
        rlWindow.Close();
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
        
        public Vector2 GetScreenToWorld(Vector2 position)
        {
            return camera2D.GetScreenToWorld(position);
        }

        public Vector2 GetWorldToScreen(Vector2 position)
        {
            return camera2D.GetWorldToScreen(position);
        }

        public Matrix4x4 GetMatrix() => camera2D.GetMatrix();
    }
}