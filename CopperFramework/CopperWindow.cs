using System.Drawing;
using System.Numerics;
using CopperFramework.Info;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SilkWindow = Silk.NET.Windowing.IWindow;
using SilkOpenGlContext = Silk.NET.OpenGL.GL;
using SilkInputContext = Silk.NET.Input.IInputContext;

namespace CopperFramework;

public class CopperWindow : IDisposable
{
    public static Vector2D<int> windowSize => silkWindow.Size; 
    
    public static SilkWindow silkWindow { get; private set; }
    public SilkOpenGlContext gl { get; private set; } = null!;
    public SilkInputContext input { get; private set; } = null!;

    public Action OnLoad;
    public Action OnRender;
    public Action OnUpdate;
    public Action OnClose;

    public CopperWindow()
    {
        silkWindow = Window.Create(WindowOptions.Default);

        silkWindow.Load += () =>
        {
            gl = silkWindow.CreateOpenGL();
            input = silkWindow.CreateInput();
            OnLoad?.Invoke();
        };

        silkWindow.FramebufferResize += s => { gl?.Viewport(s); };

        silkWindow.Closing += () =>
        {
            input?.Dispose();
            gl?.Dispose();
            OnClose?.Invoke();
        };

        silkWindow.Update += delta =>
        {
            Time.DeltaTime = (float)delta;
            OnUpdate?.Invoke();
        };

        silkWindow.Render += delta =>
        {
            Time.DeltaTime = (float)delta;

            gl.ClearColor(Color.FromArgb(255, 245, 245, 245));
            gl.Enable(EnableCap.DepthTest);
            gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            OnRender?.Invoke();
        };
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        silkWindow.Dispose();
        gl.Dispose();
        input.Dispose();
    }

    public void Run() => silkWindow.Run();
}