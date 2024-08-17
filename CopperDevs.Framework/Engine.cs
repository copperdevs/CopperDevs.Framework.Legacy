using System.Diagnostics;
using System.Reflection;
using CopperDevs.Core;
using CopperDevs.Core.Data;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui;
using CopperDevs.DearImGui.Renderer.Raylib;
using CopperDevs.Framework.Components;
using CopperDevs.Framework.Content;
using CopperDevs.Framework.Physics;
using CopperDevs.Framework.Rendering;
using CopperDevs.Framework.Rendering.DearImGui;
using CopperDevs.Framework.Rendering.DearImGui.ReflectionRenderers;
using CopperDevs.Framework.Scenes;
using CopperDevs.Framework.Ui;
using CopperDevs.Framework.Utility;
using ImGuiNET;
using Raylib_CSharp.Collision;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Transformations;
using ColorFieldRenderer = CopperDevs.Framework.Rendering.DearImGui.ReflectionRenderers.ColorFieldRenderer;

namespace CopperDevs.Framework;

public class Engine : Singleton<Engine>
{
    // settings
    public bool ShouldRun;
    public Action OnLoad = null!;

    // info
    private Stopwatch stopwatch = null!;
    private readonly EngineSettings settings;
    public bool DebugEnabled;
    public Vector2Int WindowSize => new(rlWindow.GetScreenWidth(), rlWindow.GetScreenHeight());
    public bool GameWindowHovered;
    internal Vector2 GameWindowPositionOne { get; set; }
    internal Vector2 GameWindowPositionTwo { get; set; }

    // rendering
    internal EngineCamera Camera;
    public rlRenderTexture GameRenderTexture { get; private set; }
    public rlRenderTexture ShaderRenderTexture { get; private set; }
    public bool ScreenShaderEnabled = true;
    public Color BackgroundColor { get; private set; } = Color.RayWhite;
    private Color editorBackgroundColor = new(0, 0, 0, 0);

    // fixed update
    private float fixedDeltaTime;
    private const float FixedFrameTime = 1f / 60;

    internal List<Shader> ScreenShaders = [];

    public Engine() : this(EngineSettings.DefaultSettings)
    {
    }

    public Engine(EngineSettings newSettings)
    {
        // variables
        SetInstance(this);
        settings = newSettings;

        // methods
        Initialize();
    }

    public void Run()
    {
        Start();

        while (!rlWindow.ShouldClose() && ShouldRun)
            Update();

        Stop();
    }

    private void Initialize()
    {
        stopwatch = Stopwatch.StartNew();

        CopperLogger.IncludeTimestamps = true;
        RaylibLogger.Initialize();

        ShouldRun = true;
        DebugEnabled = settings.EnableDevToolsAtStart;

        Log.Performance($"Time elapsed during engine creation: {stopwatch.Elapsed}");
    }

    private void Start()
    {
        Raylib.SetConfigFlags(settings.WindowFlags);
        rlWindow.Init(settings.WindowSize.X, settings.WindowSize.Y, (string.IsNullOrWhiteSpace(settings.WindowTitle) ? Assembly.GetEntryAssembly()?.FullName : settings.WindowTitle)!);
        rlAudio.Init();
        Time.Fps = settings.TargetFps;

        Camera = new EngineCamera
        {
            Position = Vector2.Zero,
            Rotation = 0,
            Zoom = 1
        };

        GameRenderTexture = rlRenderTexture.Load(settings.WindowSize.X, settings.WindowSize.Y);
        ShaderRenderTexture = rlRenderTexture.Load(settings.WindowSize.X, settings.WindowSize.Y);

        var handle = rlWindow.GetHandle();

        WindowsApi.OnWindowResize += OnWindowsApiWindowResize;

        WindowsApi.SetDwmImmersiveDarkMode(handle, true);
        WindowsApi.SetDwmSystemBackdropType(handle, WindowsApi.SystemBackdropType.Acrylic);
        WindowsApi.SetDwmWindowCornerPreference(handle, WindowsApi.WindowCornerPreference.Default);
        WindowsApi.RegisterWindow(handle);

        CopperImGui.RegisterFieldRenderer<Color, ColorFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<Transform, TransformFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<UiScreen, UiScreenFieldRenderer>();

        RlImGuiRenderer.SetupUserFonts += ptr =>
        {
            try
            {
                unsafe
                {
                    fixed (byte* p = ResourceLoader.LoadEmbeddedResourceBytes("CopperDevs.Framework.Resources.Fonts.Inter.static.Inter-Regular.ttf"))
                        ptr.AddFontFromMemoryTTF((IntPtr)p, 14, 14);

                    fixed (byte* p = ResourceLoader.LoadEmbeddedResourceBytes("CopperDevs.Framework.Resources.Fonts.Figtree.static.Figtree-Regular.ttf"))
                        ptr.AddFontFromMemoryTTF((IntPtr)p, 14, 14);
                }
            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        };
        CopperImGui.Setup<RlImGuiRenderer>(true, true);
        CopperImGui.Rendered += ImGuiRender;

        ContentRegistry.Start();

        ComponentRegistry.RegisterAbstractSubclass<Collider, BoxCollider>();

        OnLoad?.Invoke();

        Log.Performance($"Time elapsed starting the engine: {stopwatch.Elapsed}");
    }

    private void Update()
    {
        if (Input.IsKeyPressed(KeyboardKey.F2))
            DebugEnabled = !DebugEnabled;

        if (Input.IsKeyPressed(KeyboardKey.F3))
            ScreenShaderEnabled = !ScreenShaderEnabled;

        FixedUpdateCheck();

        rlGraphics.BeginTextureMode(GameRenderTexture);

        rlGraphics.BeginDrawing();
        rlGraphics.ClearBackground(BackgroundColor);

        rlGraphics.BeginMode2D(Camera);

        CameraRenderUpdate();

        rlGraphics.EndMode2D();

        if (DebugEnabled)
            ComponentManager.UpdateActiveSceneComponents(ComponentManager.ComponentUpdateType.Ui);

        rlGraphics.EndTextureMode();

        if (ScreenShaders.Count > 0)
        {
            for (var index = 0; index < ScreenShaders.Count; index++)
            {
                var shader = ScreenShaders[index];

                rlGraphics.BeginTextureMode(ShaderRenderTexture);

                rlGraphics.BeginShaderMode(shader);

                var targetTexture = index == 0 ? GameRenderTexture.Texture : ShaderRenderTexture.Texture;
                rlGraphics.DrawTextureRec(targetTexture, new Rectangle(0, 0, targetTexture.Width, -targetTexture.Height), Vector2.Zero, Color.White);

                rlGraphics.EndShaderMode();

                rlGraphics.EndTextureMode();
            }
        }
        else
        {
            rlGraphics.BeginTextureMode(ShaderRenderTexture);
            rlGraphics.DrawTextureRec(GameRenderTexture.Texture, new Rectangle(0, 0, GameRenderTexture.Texture.Width, -GameRenderTexture.Texture.Height), Vector2.Zero, Color.White);
            rlGraphics.EndTextureMode();
        }

        if (DebugEnabled)
            rlGraphics.ClearBackground(editorBackgroundColor);
        else
            rlGraphics.DrawTextureRec(ShaderRenderTexture.Texture, new Rectangle(0, 0, ShaderRenderTexture.Texture.Width, -ShaderRenderTexture.Texture.Height), Vector2.Zero, Color.White);

        UiRenderUpdate();

        rlGraphics.EndDrawing();
    }

    private void Stop()
    {
        Log.Performance($"Time elapsed during the runtime of the engine: {stopwatch.Elapsed}");

        CopperImGui.Rendered -= ImGuiRender;
        CopperImGui.Shutdown();

        ContentRegistry.Stop();

        rlAudio.Close();
        rlWindow.Close();
    }

    private void CameraRenderUpdate()
    {
        ComponentManager.UpdateActiveSceneComponents(ComponentManager.ComponentUpdateType.Normal);

        if (DebugEnabled)
            ComponentManager.UpdateActiveSceneComponents(ComponentManager.ComponentUpdateType.Debug);
    }

    private void UiRenderUpdate()
    {
        if (DebugEnabled)
            CopperImGui.Render();
        else
            ComponentManager.UpdateActiveSceneComponents(ComponentManager.ComponentUpdateType.Ui);
    }

    private void ImGuiRender()
    {
        if (!DebugEnabled)
            return;

        EngineWindows.RenderGameWindow();
        EngineWindows.RenderMenuBar();
    }

    private void FixedUpdateCheck()
    {
        fixedDeltaTime += Time.DeltaTime;

        if (!(fixedDeltaTime >= FixedFrameTime))
            return;

        fixedDeltaTime = 0;
        FixedUpdate();
    }

    private void FixedUpdate()
    {
        SceneManager.ActiveScene.PhysicsWorld.Step(FixedFrameTime);
        ComponentManager.UpdateActiveSceneComponents(ComponentManager.ComponentUpdateType.Fixed);
    }

    private void OnWindowsApiWindowResize(Vector2Int newSize)
    {
        RaylibLogger.HideLogs = true;

        GameRenderTexture.Unload();
        GameRenderTexture = rlRenderTexture.Load(newSize.X, newSize.Y);

        ShaderRenderTexture.Unload();
        ShaderRenderTexture = rlRenderTexture.Load(newSize.X, newSize.Y);

        RaylibLogger.HideLogs = false;

        Log.Debug($"Reloading render textures. New size: <{newSize.X},{newSize.Y}>");

        Update();
    }

    public void SetBackgroundColor(Color color) => BackgroundColor = color;
    public void AddScreenShader(Shader.IncludedShaders includedShader) => AddScreenShader(Shader.Load(includedShader));
    public void AddScreenShader(Shader targetShader) => ScreenShaders.Add(targetShader);
    public void RemoveScreenShader(Shader targetShader) => ScreenShaders.Remove(targetShader);
}