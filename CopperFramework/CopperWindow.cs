using System.Drawing;
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
    public SilkWindow silkWindow { get; }
    public SilkOpenGlContext gl { get; private set; } = null!;
    public SilkInputContext input { get; private set; } = null!;

    public Action OnLoad;
    public Action OnRender;
    public Action OnUpdate;
    public Action OnClose;

    public CopperWindow()
    {
        var options = WindowOptions.Default;
        options.Title = "CopperWindow";
        options.Size = new Vector2D<int>(650, 450);
        silkWindow = Window.Create(options);

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

            gl.ClearColor(Color.FromArgb(255, (int)(.45f * 255), (int)(.55f * 255), (int)(.60f * 255)));
            gl.Clear((uint)ClearBufferMask.ColorBufferBit);

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