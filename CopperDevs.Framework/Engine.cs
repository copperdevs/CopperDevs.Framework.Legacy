using System.Diagnostics;
using System.Reflection;
using CopperDevs.Core;
using CopperDevs.Core.Data;
using CopperDevs.Core.Utility;
using CopperDevs.DearImGui;
using CopperDevs.Framework.Components;
using CopperDevs.Framework.Physics;
using CopperDevs.Framework.Rendering;
using CopperDevs.Framework.Rendering.DearImGui;
using CopperDevs.Framework.Rendering.DearImGui.ReflectionRenderers;
using CopperDevs.Framework.Scenes;
using CopperDevs.Framework.Ui;
using CopperDevs.Framework.Utility;
using ImGuiNET;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Transformations;

namespace CopperDevs.Framework;

public class Engine : Singleton<Engine>
{
    // settings
    public bool ShouldRun;
    public Action OnLoad = null!;

    // info
    private Stopwatch stopwatch = null!;
    private EngineSettings settings;
    public bool DebugEnabled;
    public Vector2Int WindowSize => new(rlWindow.GetScreenWidth(), rlWindow.GetScreenHeight());
    public bool GameWindowHovered = false;
    internal Vector2 GameWindowPositionOne { get; set; }
    internal Vector2 GameWindowPositionTwo { get; set; }

    // rendering
    public RenderingSystem? RenderingSystem;
    internal EngineCamera Camera;
    public rlRenderTexture GameRenderTexture { get; private set; }
    public Shader? ScreenShader { get; private set; }
    public bool ScreenShaderEnabled = true;
    public Color BackgroundColor { get; private set; } = Color.RayWhite;

    // fixed update
    private float fixedDeltaTime;
    private const float FixedFrameTime = 1f / 60;

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

        var handle = rlWindow.GetHandle();

        WindowsApi.OnWindowResize += OnWindowsApiWindowResize;

        WindowsApi.SetDwmImmersiveDarkMode(handle, true);
        WindowsApi.SetDwmSystemBackdropType(handle, WindowsApi.SystemBackdropType.Acrylic);
        WindowsApi.SetDwmWindowCornerPreference(handle, WindowsApi.WindowCornerPreference.Default);
        WindowsApi.RegisterWindow(handle);

        CopperImGui.RegisterFieldRenderer<Color, ColorFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<rlTexture, Texture2DFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<rlRenderTexture, RenderTexture2DFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<Transform, TransformFieldRenderer>();
        CopperImGui.RegisterFieldRenderer<UiScreen, UiScreenFieldRenderer>();

        CopperImGui.Setup<CopperRlImGui>();
        CopperImGui.Rendered += ImGuiRender;

        RenderingSystem ??= new RenderingSystem();
        RenderingSystem?.Start();

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

        if (DebugEnabled)
        {
            rlGraphics.ClearBackground(Color.DarkGray);
        }
        else
        {
            using (new ShaderScope(ScreenShader!, ScreenShader is not null && ScreenShaderEnabled))
                rlGraphics.DrawTextureRec(GameRenderTexture.Texture, new Rectangle(0, 0, GameRenderTexture.Texture.Width, -GameRenderTexture.Texture.Height), Vector2.Zero, Color.White);
        }

        UiRenderUpdate();

        rlGraphics.EndDrawing();
    }

    private void Stop()
    {
        Log.Performance($"Time elapsed during the runtime of the engine: {stopwatch.Elapsed}");

        CopperImGui.Rendered -= ImGuiRender;
        CopperImGui.Shutdown();

        RenderingSystem?.Stop();

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

        CopperImGui.Window("Game", () =>
        {
            rlImGui.ImageRenderTextureFit(GameRenderTexture, false);
            GameWindowHovered = ImGui.IsWindowHovered();

            var drawList = ImGui.GetWindowDrawList();
            GameWindowPositionOne = drawList.VtxBuffer[drawList.VtxBuffer.Size - 4].pos;
            GameWindowPositionTwo = drawList.VtxBuffer[drawList.VtxBuffer.Size - 2].pos;
        }, ImGuiWindowFlags.NoCollapse);

        CopperImGui.MenuBar(true, ("Windows", () =>
        {
            CopperImGui.MenuItem("ImGui About", ref CopperImGui.ShowDearImGuiAboutWindow);
            CopperImGui.MenuItem("ImGui Demo", ref CopperImGui.ShowDearImGuiDemoWindow);
            CopperImGui.MenuItem("ImGui Metrics", ref CopperImGui.ShowDearImGuiMetricsWindow);
            CopperImGui.MenuItem("ImGui Debug Log", ref CopperImGui.ShowDearImGuiDebugLogWindow);
            CopperImGui.MenuItem("ImGui Id Stack Tool", ref CopperImGui.ShowDearImGuiIdStackToolWindow);
        }));
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
        GameRenderTexture.Unload();
        GameRenderTexture = rlRenderTexture.Load(newSize.X, newSize.Y);
        Update();
    }

    public void SetBackgroundColor(Color color) => BackgroundColor = color;
    public void SetScreenShader(Shader shader) => ScreenShader = shader;
    public void SetScreenShader(Shader.IncludedShaders includedShader) => Shader.Load(includedShader);
}