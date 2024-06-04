using System.Runtime.InteropServices;
using System.Text;
using CopperDevs.Core;
using CopperDevs.Core.Utility;
using CopperDevs.Framework.Utility;
using ImGuiNET;
using Raylib_CSharp.Images;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering.Gl;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Transformations;
using Raylib_CSharp.Windowing;

namespace CopperDevs.Framework.Rendering.DearImGui;

// ReSharper disable once InconsistentNaming
public static class rlImGui
{
    private static IntPtr imGuiContext = IntPtr.Zero;

    private static ImGuiMouseCursor currentMouseCursor = ImGuiMouseCursor.COUNT;
    private static Dictionary<ImGuiMouseCursor, MouseCursor> mouseCursorMap = new();
    private static rlTexture fontTexture;

    private static Dictionary<KeyboardKey, ImGuiKey> raylibKeyMap = new();

    internal static bool LastFrameFocused = false;

    internal static bool LastControlPressed = false;
    internal static bool LastShiftPressed = false;
    internal static bool LastAltPressed = false;
    internal static bool LastSuperPressed = false;

    // ReSharper disable once InconsistentNaming
    internal static bool rlImGuiIsControlDown()
    {
        return Input.IsKeyDown(KeyboardKey.RightControl) || Input.IsKeyDown(KeyboardKey.LeftControl);
    }

    // ReSharper disable once InconsistentNaming
    internal static bool rlImGuiIsShiftDown()
    {
        return Input.IsKeyDown(KeyboardKey.RightShift) || Input.IsKeyDown(KeyboardKey.LeftShift);
    }

    // ReSharper disable once InconsistentNaming
    internal static bool rlImGuiIsAltDown()
    {
        return Input.IsKeyDown(KeyboardKey.RightAlt) || Input.IsKeyDown(KeyboardKey.LeftAlt);
    }

    // ReSharper disable once InconsistentNaming
    internal static bool rlImGuiIsSuperDown()
    {
        return Input.IsKeyDown(KeyboardKey.RightSuper) || Input.IsKeyDown(KeyboardKey.LeftSuper);
    }
    // ReSharper disable once InconsistentNaming

    public delegate void SetupUserFontsCallback(ImGuiIOPtr imGuiIo);

    /// <summary>
    /// Callback for cases where the user wants to install additional fonts.
    /// </summary>
    public static SetupUserFontsCallback SetupUserFonts = null!;

    /// <summary>
    /// Sets up ImGui, loads fonts and themes
    /// </summary>
    /// <param name="darkTheme">when true(default) the dark theme is used, when false the light theme is used</param>
    /// <param name="enableDocking">when true(not default) docking support will be enabled</param>
    public static void Setup(bool darkTheme = true, bool enableDocking = false)
    {
        mouseCursorMap = new Dictionary<ImGuiMouseCursor, MouseCursor>();
        mouseCursorMap = new Dictionary<ImGuiMouseCursor, MouseCursor>();

        LastFrameFocused = rlWindow.IsFocused();
        LastControlPressed = false;
        LastShiftPressed = false;
        LastAltPressed = false;
        LastSuperPressed = false;

        fontTexture.Id = 0;

        BeginInitImGui();

        if (darkTheme)
            ImGui.StyleColorsDark();
        else
            ImGui.StyleColorsLight();

        if (enableDocking)
            ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.DockingEnable;

        EndInitImGui();
    }

    /// <summary>
    /// Custom initialization. Not needed if you call Setup. Only needed if you want to add custom setup code.
    /// must be followed by EndInitImGui
    /// </summary>
    public static void BeginInitImGui()
    {
        SetupKeymap();

        imGuiContext = ImGui.CreateContext();
    }

    internal static void SetupKeymap()
    {
        if (raylibKeyMap.Count > 0)
            return;

        // build up a map of raylib keys to ImGuiKeys
        raylibKeyMap[KeyboardKey.Apostrophe] = ImGuiKey.Apostrophe;
        raylibKeyMap[KeyboardKey.Comma] = ImGuiKey.Comma;
        raylibKeyMap[KeyboardKey.Minus] = ImGuiKey.Minus;
        raylibKeyMap[KeyboardKey.Period] = ImGuiKey.Period;
        raylibKeyMap[KeyboardKey.Slash] = ImGuiKey.Slash;
        raylibKeyMap[KeyboardKey.Zero] = ImGuiKey._0;
        raylibKeyMap[KeyboardKey.One] = ImGuiKey._1;
        raylibKeyMap[KeyboardKey.Two] = ImGuiKey._2;
        raylibKeyMap[KeyboardKey.Three] = ImGuiKey._3;
        raylibKeyMap[KeyboardKey.Four] = ImGuiKey._4;
        raylibKeyMap[KeyboardKey.Five] = ImGuiKey._5;
        raylibKeyMap[KeyboardKey.Six] = ImGuiKey._6;
        raylibKeyMap[KeyboardKey.Seven] = ImGuiKey._7;
        raylibKeyMap[KeyboardKey.Eight] = ImGuiKey._8;
        raylibKeyMap[KeyboardKey.Nine] = ImGuiKey._9;
        raylibKeyMap[KeyboardKey.Semicolon] = ImGuiKey.Semicolon;
        raylibKeyMap[KeyboardKey.Equal] = ImGuiKey.Equal;
        raylibKeyMap[KeyboardKey.A] = ImGuiKey.A;
        raylibKeyMap[KeyboardKey.B] = ImGuiKey.B;
        raylibKeyMap[KeyboardKey.C] = ImGuiKey.C;
        raylibKeyMap[KeyboardKey.D] = ImGuiKey.D;
        raylibKeyMap[KeyboardKey.E] = ImGuiKey.E;
        raylibKeyMap[KeyboardKey.F] = ImGuiKey.F;
        raylibKeyMap[KeyboardKey.G] = ImGuiKey.G;
        raylibKeyMap[KeyboardKey.H] = ImGuiKey.H;
        raylibKeyMap[KeyboardKey.I] = ImGuiKey.I;
        raylibKeyMap[KeyboardKey.J] = ImGuiKey.J;
        raylibKeyMap[KeyboardKey.K] = ImGuiKey.K;
        raylibKeyMap[KeyboardKey.L] = ImGuiKey.L;
        raylibKeyMap[KeyboardKey.M] = ImGuiKey.M;
        raylibKeyMap[KeyboardKey.N] = ImGuiKey.N;
        raylibKeyMap[KeyboardKey.O] = ImGuiKey.O;
        raylibKeyMap[KeyboardKey.P] = ImGuiKey.P;
        raylibKeyMap[KeyboardKey.Q] = ImGuiKey.Q;
        raylibKeyMap[KeyboardKey.R] = ImGuiKey.R;
        raylibKeyMap[KeyboardKey.S] = ImGuiKey.S;
        raylibKeyMap[KeyboardKey.T] = ImGuiKey.T;
        raylibKeyMap[KeyboardKey.U] = ImGuiKey.U;
        raylibKeyMap[KeyboardKey.V] = ImGuiKey.V;
        raylibKeyMap[KeyboardKey.W] = ImGuiKey.W;
        raylibKeyMap[KeyboardKey.X] = ImGuiKey.X;
        raylibKeyMap[KeyboardKey.Y] = ImGuiKey.Y;
        raylibKeyMap[KeyboardKey.Z] = ImGuiKey.Z;
        raylibKeyMap[KeyboardKey.Space] = ImGuiKey.Space;
        raylibKeyMap[KeyboardKey.Escape] = ImGuiKey.Escape;
        raylibKeyMap[KeyboardKey.Enter] = ImGuiKey.Enter;
        raylibKeyMap[KeyboardKey.Tab] = ImGuiKey.Tab;
        raylibKeyMap[KeyboardKey.Backspace] = ImGuiKey.Backspace;
        raylibKeyMap[KeyboardKey.Insert] = ImGuiKey.Insert;
        raylibKeyMap[KeyboardKey.Delete] = ImGuiKey.Delete;
        raylibKeyMap[KeyboardKey.Right] = ImGuiKey.RightArrow;
        raylibKeyMap[KeyboardKey.Left] = ImGuiKey.LeftArrow;
        raylibKeyMap[KeyboardKey.Down] = ImGuiKey.DownArrow;
        raylibKeyMap[KeyboardKey.Up] = ImGuiKey.UpArrow;
        raylibKeyMap[KeyboardKey.PageUp] = ImGuiKey.PageUp;
        raylibKeyMap[KeyboardKey.PageDown] = ImGuiKey.PageDown;
        raylibKeyMap[KeyboardKey.Home] = ImGuiKey.Home;
        raylibKeyMap[KeyboardKey.End] = ImGuiKey.End;
        raylibKeyMap[KeyboardKey.CapsLock] = ImGuiKey.CapsLock;
        raylibKeyMap[KeyboardKey.ScrollLock] = ImGuiKey.ScrollLock;
        raylibKeyMap[KeyboardKey.NumLock] = ImGuiKey.NumLock;
        raylibKeyMap[KeyboardKey.PrintScreen] = ImGuiKey.PrintScreen;
        raylibKeyMap[KeyboardKey.Pause] = ImGuiKey.Pause;
        raylibKeyMap[KeyboardKey.F1] = ImGuiKey.F1;
        raylibKeyMap[KeyboardKey.F2] = ImGuiKey.F2;
        raylibKeyMap[KeyboardKey.F3] = ImGuiKey.F3;
        raylibKeyMap[KeyboardKey.F4] = ImGuiKey.F4;
        raylibKeyMap[KeyboardKey.F5] = ImGuiKey.F5;
        raylibKeyMap[KeyboardKey.F6] = ImGuiKey.F6;
        raylibKeyMap[KeyboardKey.F7] = ImGuiKey.F7;
        raylibKeyMap[KeyboardKey.F8] = ImGuiKey.F8;
        raylibKeyMap[KeyboardKey.F9] = ImGuiKey.F9;
        raylibKeyMap[KeyboardKey.F10] = ImGuiKey.F10;
        raylibKeyMap[KeyboardKey.F11] = ImGuiKey.F11;
        raylibKeyMap[KeyboardKey.F12] = ImGuiKey.F12;
        raylibKeyMap[KeyboardKey.LeftShift] = ImGuiKey.LeftShift;
        raylibKeyMap[KeyboardKey.LeftControl] = ImGuiKey.LeftCtrl;
        raylibKeyMap[KeyboardKey.LeftAlt] = ImGuiKey.LeftAlt;
        raylibKeyMap[KeyboardKey.LeftSuper] = ImGuiKey.LeftSuper;
        raylibKeyMap[KeyboardKey.RightShift] = ImGuiKey.RightShift;
        raylibKeyMap[KeyboardKey.RightControl] = ImGuiKey.RightCtrl;
        raylibKeyMap[KeyboardKey.RightAlt] = ImGuiKey.RightAlt;
        raylibKeyMap[KeyboardKey.RightSuper] = ImGuiKey.RightSuper;
        raylibKeyMap[KeyboardKey.KeyboardMenu] = ImGuiKey.Menu;
        raylibKeyMap[KeyboardKey.LeftBracket] = ImGuiKey.LeftBracket;
        raylibKeyMap[KeyboardKey.Backslash] = ImGuiKey.Backslash;
        raylibKeyMap[KeyboardKey.RightBracket] = ImGuiKey.RightBracket;
        raylibKeyMap[KeyboardKey.Grave] = ImGuiKey.GraveAccent;
        raylibKeyMap[KeyboardKey.Kp0] = ImGuiKey.Keypad0;
        raylibKeyMap[KeyboardKey.Kp1] = ImGuiKey.Keypad1;
        raylibKeyMap[KeyboardKey.Kp2] = ImGuiKey.Keypad2;
        raylibKeyMap[KeyboardKey.Kp3] = ImGuiKey.Keypad3;
        raylibKeyMap[KeyboardKey.Kp4] = ImGuiKey.Keypad4;
        raylibKeyMap[KeyboardKey.Kp5] = ImGuiKey.Keypad5;
        raylibKeyMap[KeyboardKey.Kp6] = ImGuiKey.Keypad6;
        raylibKeyMap[KeyboardKey.Kp7] = ImGuiKey.Keypad7;
        raylibKeyMap[KeyboardKey.Kp8] = ImGuiKey.Keypad8;
        raylibKeyMap[KeyboardKey.Kp9] = ImGuiKey.Keypad9;
        raylibKeyMap[KeyboardKey.KpDecimal] = ImGuiKey.KeypadDecimal;
        raylibKeyMap[KeyboardKey.KpDivide] = ImGuiKey.KeypadDivide;
        raylibKeyMap[KeyboardKey.KpMultiply] = ImGuiKey.KeypadMultiply;
        raylibKeyMap[KeyboardKey.KpSubtract] = ImGuiKey.KeypadSubtract;
        raylibKeyMap[KeyboardKey.KpAdd] = ImGuiKey.KeypadAdd;
        raylibKeyMap[KeyboardKey.KpEnter] = ImGuiKey.KeypadEnter;
        raylibKeyMap[KeyboardKey.KpEqual] = ImGuiKey.KeypadEqual;
    }

    private static void SetupMouseCursors()
    {
        mouseCursorMap.Clear();
        mouseCursorMap[ImGuiMouseCursor.Arrow] = MouseCursor.Arrow;
        mouseCursorMap[ImGuiMouseCursor.TextInput] = MouseCursor.IBeam;
        mouseCursorMap[ImGuiMouseCursor.Hand] = MouseCursor.PointingHand;
        mouseCursorMap[ImGuiMouseCursor.ResizeAll] = MouseCursor.ResizeAll;
        mouseCursorMap[ImGuiMouseCursor.ResizeEW] = MouseCursor.ResizeEw;
        mouseCursorMap[ImGuiMouseCursor.ResizeNESW] = MouseCursor.ResizeNesw;
        mouseCursorMap[ImGuiMouseCursor.ResizeNS] = MouseCursor.ResizeNs;
        mouseCursorMap[ImGuiMouseCursor.ResizeNWSE] = MouseCursor.ResizeNwse;
        mouseCursorMap[ImGuiMouseCursor.NotAllowed] = MouseCursor.NotAllowed;
    }

    /// <summary>
    /// Forces the font texture atlas to be recomputed and re-cached
    /// </summary>
    public static unsafe void ReloadFonts()
    {
        ImGui.SetCurrentContext(imGuiContext);
        var io = ImGui.GetIO();

        io.Fonts.GetTexDataAsRGBA32(out byte* pixels, out var width, out var height, out _);

        var image = new Image
        {
            Data = (IntPtr)pixels,
            Width = width,
            Height = height,
            Mipmaps = 1,
            Format = PixelFormat.UncompressedR8G8B8A8,
        };

        if (fontTexture.IsReady())
            fontTexture.Unload();

        fontTexture = rlTexture.LoadFromImage(image);

        io.Fonts.SetTexID(new IntPtr(fontTexture.Id));
    }

    // ReSharper disable once InconsistentNaming
    unsafe internal static sbyte* rlImGuiGetClipText(IntPtr userData)
    {
        var bytes = Encoding.ASCII.GetBytes(rlWindow.GetClipboardText());

        fixed (byte* p = bytes)
            return (sbyte*)p;
    }

    // ReSharper disable once InconsistentNaming
    unsafe internal static void rlImGuiSetClipText(IntPtr userData, sbyte* text)
    {
        try
        {
            rlWindow.SetClipboardText(text->ToString());
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }

    private unsafe delegate sbyte* GetClipTextCallback(IntPtr userData);

    private unsafe delegate void SetClipTextCallback(IntPtr userData, sbyte* text);

    /// <summary>
    /// End Custom initialization. Not needed if you call Setup. Only needed if you want to add custom setup code.
    /// must be proceeded by BeginInitImGui
    /// </summary>
    public static void EndInitImGui()
    {
        SetupMouseCursors();

        ImGui.SetCurrentContext(imGuiContext);

        var fonts = ImGui.GetIO().Fonts;

        try
        {
            unsafe
            {
                fixed (byte* p = ResourceLoader.LoadEmbeddedResourceBytes("CopperDevs.Framework.Resources.Fonts.Inter.static.Inter-Regular.ttf"))
                    fonts.AddFontFromMemoryTTF((IntPtr)p, 14, 14);
            }
        }
        catch (Exception e)
        {
            Log.Error(e);
            fonts.AddFontDefault();
        }

        // remove this part if you don't want font awesome
        unsafe
        {
            var iconsConfig = ImGuiNative.ImFontConfig_ImFontConfig();
            iconsConfig->MergeMode = 1; // merge the glyph ranges into the default font
            iconsConfig->PixelSnapH = 1; // don't try to render on partial pixels
            iconsConfig->FontDataOwnedByAtlas = 0; // the font atlas does not own this font data

            iconsConfig->GlyphMaxAdvanceX = float.MaxValue;
            iconsConfig->RasterizerMultiply = 1.0f;
            iconsConfig->OversampleH = 2;
            iconsConfig->OversampleV = 1;

            var iconRanges = new ushort[3];
            iconRanges[0] = FontAwesome.IconMin;
            iconRanges[1] = FontAwesome.IconMax;
            iconRanges[2] = 0;

            fixed (ushort* range = &iconRanges[0])
            {
                // this unmanaged memory must remain allocated for the entire run of rlImgui
                FontAwesome.IconFontRanges = Marshal.AllocHGlobal(6);
                Buffer.MemoryCopy(range, FontAwesome.IconFontRanges.ToPointer(), 6, 6);
                iconsConfig->GlyphRanges = (ushort*)FontAwesome.IconFontRanges.ToPointer();

                var fontDataBuffer = Convert.FromBase64String(FontAwesome.IconFontData);

                fixed (byte* buffer = fontDataBuffer)
                {
                    ImGui.GetIO().Fonts.AddFontFromMemoryTTF(new IntPtr(buffer), fontDataBuffer.Length, 11, iconsConfig, FontAwesome.IconFontRanges);
                }
            }

            ImGuiNative.ImFontConfig_destroy(iconsConfig);
        }

        var io = ImGui.GetIO();

        SetupUserFonts?.Invoke(io);

        io.BackendFlags |= ImGuiBackendFlags.HasMouseCursors | ImGuiBackendFlags.HasSetMousePos | ImGuiBackendFlags.HasGamepad;

        io.MousePos.X = 0;
        io.MousePos.Y = 0;

        // copy/paste callbacks
        unsafe
        {
            GetClipTextCallback getClip = rlImGuiGetClipText;
            SetClipTextCallback setClip = rlImGuiSetClipText;

            io.SetClipboardTextFn = Marshal.GetFunctionPointerForDelegate(setClip);
            io.GetClipboardTextFn = Marshal.GetFunctionPointerForDelegate(getClip);
        }

        io.ClipboardUserData = IntPtr.Zero;
        ReloadFonts();
    }

    private static void SetMouseEvent(ImGuiIOPtr io, MouseButton rayMouse, ImGuiMouseButton imGuiMouse)
    {
        if (Input.IsMouseButtonPressed(rayMouse))
            io.AddMouseButtonEvent((int)imGuiMouse, true);
        else if (Input.IsMouseButtonReleased(rayMouse))
            io.AddMouseButtonEvent((int)imGuiMouse, false);
    }

    private static void NewFrame(float dt = -1)
    {
        var io = ImGui.GetIO();

        if (rlWindow.IsFullscreen())
        {
            var monitor = rlWindow.GetCurrentMonitor();
            io.DisplaySize = new Vector2(rlWindow.GetMonitorWidth(monitor), rlWindow.GetMonitorHeight(monitor));
        }
        else
        {
            io.DisplaySize = new Vector2(rlWindow.GetScreenWidth(), rlWindow.GetScreenHeight());
        }

        io.DisplayFramebufferScale = new Vector2(1, 1);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || rlWindow.IsState(ConfigFlags.HighDpiWindow))
            io.DisplayFramebufferScale = rlWindow.GetScaleDPI();

        io.DeltaTime = dt >= 0 ? dt : rlTime.GetFrameTime();

        if (io.WantSetMousePos)
        {
            rlInput.SetMousePosition((int)io.MousePos.X, (int)io.MousePos.Y);
        }
        else
        {
            io.AddMousePosEvent(rlInput.GetMouseX(), rlInput.GetMouseY());
        }

        SetMouseEvent(io, MouseButton.Left, ImGuiMouseButton.Left);
        SetMouseEvent(io, MouseButton.Right, ImGuiMouseButton.Right);
        SetMouseEvent(io, MouseButton.Middle, ImGuiMouseButton.Middle);
        SetMouseEvent(io, MouseButton.Forward, ImGuiMouseButton.Middle + 1);
        SetMouseEvent(io, MouseButton.Back, ImGuiMouseButton.Middle + 2);

        var wheelMove = rlInput.GetMouseWheelMoveV();
        io.AddMouseWheelEvent(wheelMove.X, wheelMove.Y);

        if ((io.ConfigFlags & ImGuiConfigFlags.NoMouseCursorChange) != 0)
            return;

        var imguiCursor = ImGui.GetMouseCursor();

        if (imguiCursor == currentMouseCursor && !io.MouseDrawCursor)
            return;

        currentMouseCursor = imguiCursor;

        if (io.MouseDrawCursor || imguiCursor == ImGuiMouseCursor.None)
        {
            rlInput.HideCursor();
        }
        else
        {
            rlInput.ShowCursor();

            if ((io.ConfigFlags & ImGuiConfigFlags.NoMouseCursorChange) == 0)
            {
                rlInput.SetMouseCursor(mouseCursorMap.GetValueOrDefault(imguiCursor, MouseCursor.Default));
            }
        }
    }

    private static void FrameEvents()
    {
        var io = ImGui.GetIO();

        var focused = rlWindow.IsFocused();
        if (focused != LastFrameFocused)
            io.AddFocusEvent(focused);
        LastFrameFocused = focused;


        // handle the modifier key events so that shortcuts work
        var ctrlDown = rlImGuiIsControlDown();
        if (ctrlDown != LastControlPressed)
            io.AddKeyEvent(ImGuiKey.ModCtrl, ctrlDown);
        LastControlPressed = ctrlDown;

        var shiftDown = rlImGuiIsShiftDown();
        if (shiftDown != LastShiftPressed)
            io.AddKeyEvent(ImGuiKey.ModShift, shiftDown);
        LastShiftPressed = shiftDown;

        var altDown = rlImGuiIsAltDown();
        if (altDown != LastAltPressed)
            io.AddKeyEvent(ImGuiKey.ModAlt, altDown);
        LastAltPressed = altDown;

        var superDown = rlImGuiIsSuperDown();
        if (superDown != LastSuperPressed)
            io.AddKeyEvent(ImGuiKey.ModSuper, superDown);
        LastSuperPressed = superDown;

        // get the pressed keys, they are in event order
        var keyId = rlInput.GetKeyPressed();
        while (keyId != 0)
        {
            var key = (KeyboardKey)keyId;
            if (raylibKeyMap.TryGetValue(key, out var value))
                io.AddKeyEvent(value, true);
            keyId = rlInput.GetKeyPressed();
        }

        // look for any keys that were down last frame and see if they were down and are released
        foreach (var keyItr in raylibKeyMap.Where(keyItr => rlInput.IsKeyReleased(keyItr.Key)))
        {
            io.AddKeyEvent(keyItr.Value, false);
        }

        // add the text input in order
        var pressed = rlInput.GetCharPressed();
        while (pressed != 0)
        {
            io.AddInputCharacter((uint)pressed);
            pressed = rlInput.GetCharPressed();
        }

        // gamepads
        if ((io.ConfigFlags & ImGuiConfigFlags.NavEnableGamepad) == 0 || !rlInput.IsGamepadAvailable(0))
            return;

        HandleGamepadButtonEvent(io, GamepadButton.LeftFaceUp, ImGuiKey.GamepadDpadUp);
        HandleGamepadButtonEvent(io, GamepadButton.LeftFaceRight, ImGuiKey.GamepadDpadRight);
        HandleGamepadButtonEvent(io, GamepadButton.LeftFaceDown, ImGuiKey.GamepadDpadDown);
        HandleGamepadButtonEvent(io, GamepadButton.LeftFaceLeft, ImGuiKey.GamepadDpadLeft);

        HandleGamepadButtonEvent(io, GamepadButton.RightFaceUp, ImGuiKey.GamepadFaceUp);
        HandleGamepadButtonEvent(io, GamepadButton.RightFaceRight, ImGuiKey.GamepadFaceLeft);
        HandleGamepadButtonEvent(io, GamepadButton.RightFaceDown, ImGuiKey.GamepadFaceDown);
        HandleGamepadButtonEvent(io, GamepadButton.RightFaceLeft, ImGuiKey.GamepadFaceRight);

        HandleGamepadButtonEvent(io, GamepadButton.LeftTrigger1, ImGuiKey.GamepadL1);
        HandleGamepadButtonEvent(io, GamepadButton.LeftTrigger2, ImGuiKey.GamepadL2);
        HandleGamepadButtonEvent(io, GamepadButton.RightTrigger1, ImGuiKey.GamepadR1);
        HandleGamepadButtonEvent(io, GamepadButton.RightTrigger2, ImGuiKey.GamepadR2);
        HandleGamepadButtonEvent(io, GamepadButton.LeftThumb, ImGuiKey.GamepadL3);
        HandleGamepadButtonEvent(io, GamepadButton.RightThumb, ImGuiKey.GamepadR3);

        HandleGamepadButtonEvent(io, GamepadButton.MiddleLeft, ImGuiKey.GamepadStart);
        HandleGamepadButtonEvent(io, GamepadButton.MiddleRight, ImGuiKey.GamepadBack);

        // left stick
        HandleGamepadStickEvent(io, GamepadAxis.LeftX, ImGuiKey.GamepadLStickLeft, ImGuiKey.GamepadLStickRight);
        HandleGamepadStickEvent(io, GamepadAxis.LeftY, ImGuiKey.GamepadLStickUp, ImGuiKey.GamepadLStickDown);

        // right stick
        HandleGamepadStickEvent(io, GamepadAxis.RightX, ImGuiKey.GamepadRStickLeft, ImGuiKey.GamepadRStickRight);
        HandleGamepadStickEvent(io, GamepadAxis.RightY, ImGuiKey.GamepadRStickUp, ImGuiKey.GamepadRStickDown);
    }


    private static void HandleGamepadButtonEvent(ImGuiIOPtr io, GamepadButton button, ImGuiKey key)
    {
        if (rlInput.IsGamepadButtonPressed(0, button))
            io.AddKeyEvent(key, true);
        else if (rlInput.IsGamepadButtonReleased(0, button))
            io.AddKeyEvent(key, false);
    }

    private static void HandleGamepadStickEvent(ImGuiIOPtr io, GamepadAxis axis, ImGuiKey negKey, ImGuiKey posKey)
    {
        const float deadZone = 0.20f;

        var axisValue = rlInput.GetGamepadAxisMovement(0, axis);

        io.AddKeyAnalogEvent(negKey, axisValue < -deadZone, axisValue < -deadZone ? -axisValue : 0);
        io.AddKeyAnalogEvent(posKey, axisValue > deadZone, axisValue > deadZone ? axisValue : 0);
    }

    /// <summary>
    /// Starts a new ImGui Frame
    /// </summary>
    /// <param name="dt">optional delta time, any value below 0 will use raylib GetFrameTime</param>
    public static void Begin(float dt = -1)
    {
        ImGui.SetCurrentContext(imGuiContext);

        NewFrame(dt);
        FrameEvents();
        ImGui.NewFrame();
    }

    private static void EnableScissor(float x, float y, float width, float height)
    {
        RlGl.EnableScissorTest();
        var io = ImGui.GetIO();

        var scale = new Vector2(1.0f, 1.0f);
        if (rlWindow.IsState(ConfigFlags.HighDpiWindow) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            scale = io.DisplayFramebufferScale;

        RlGl.Scissor((int)(x * scale.X),
            (int)((io.DisplaySize.Y - (int)(y + height)) * scale.Y),
            (int)(width * scale.X),
            (int)(height * scale.Y));
    }

    private static void TriangleVert(ImDrawVertPtr idxVert)
    {
        var color = ImGui.ColorConvertU32ToFloat4(idxVert.col);

        RlGl.Color4F(color.X, color.Y, color.Z, color.W);
        RlGl.TexCoord2F(idxVert.uv.X, idxVert.uv.Y);
        RlGl.Vertex2F(idxVert.pos.X, idxVert.pos.Y);
    }

    private static void RenderTriangles(uint count, uint indexStart, ImVector<ushort> indexBuffer, ImPtrVector<ImDrawVertPtr> vertBuffer, IntPtr texturePtr)
    {
        if (count < 3)
            return;

        uint textureId = 0;
        if (texturePtr != IntPtr.Zero)
            textureId = (uint)texturePtr.ToInt32();

        RlGl.Begin(DrawMode.Triangles);
        RlGl.SetTexture(textureId);

        for (var i = 0; i <= (count - 3); i += 3)
        {
            if (RlGl.CheckRenderBatchLimit(3))
            {
                RlGl.Begin(DrawMode.Triangles);
                RlGl.SetTexture(textureId);
            }

            var indexA = indexBuffer[(int)indexStart + i];
            var indexB = indexBuffer[(int)indexStart + i + 1];
            var indexC = indexBuffer[(int)indexStart + i + 2];

            var vertexA = vertBuffer[indexA];
            var vertexB = vertBuffer[indexB];
            var vertexC = vertBuffer[indexC];

            TriangleVert(vertexA);
            TriangleVert(vertexB);
            TriangleVert(vertexC);
        }

        RlGl.End();
    }

    private delegate void Callback(ImDrawListPtr list, ImDrawCmdPtr cmd);

    private static void RenderData()
    {
        RlGl.DrawRenderBatchActive();
        RlGl.DisableBackfaceCulling();

        var data = ImGui.GetDrawData();

        for (var l = 0; l < data.CmdListsCount; l++)
        {
            var commandList = data.CmdLists[l];

            for (var cmdIndex = 0; cmdIndex < commandList.CmdBuffer.Size; cmdIndex++)
            {
                var cmd = commandList.CmdBuffer[cmdIndex];

                EnableScissor(cmd.ClipRect.X - data.DisplayPos.X, cmd.ClipRect.Y - data.DisplayPos.Y, cmd.ClipRect.Z - (cmd.ClipRect.X - data.DisplayPos.X),
                    cmd.ClipRect.W - (cmd.ClipRect.Y - data.DisplayPos.Y));
                if (cmd.UserCallback != IntPtr.Zero)
                {
                    var cb = Marshal.GetDelegateForFunctionPointer<Callback>(cmd.UserCallback);
                    cb(commandList, cmd);
                    continue;
                }

                RenderTriangles(cmd.ElemCount, cmd.IdxOffset, commandList.IdxBuffer, commandList.VtxBuffer, cmd.TextureId);

                RlGl.DrawRenderBatchActive();
            }
        }

        RlGl.SetTexture(0);
        RlGl.DisableScissorTest();
        RlGl.EnableBackfaceCulling();
    }

    /// <summary>
    /// Ends an ImGui frame and submits all ImGui drawing to raylib for processing.
    /// </summary>
    public static void End()
    {
        ImGui.SetCurrentContext(imGuiContext);
        ImGui.Render();
        RenderData();
    }

    /// <summary>
    /// Cleanup ImGui and unload font atlas
    /// </summary>
    public static void Shutdown()
    {
        fontTexture.Unload();
        ImGui.DestroyContext();

        // remove this if you don't want font awesome support
        {
            if (FontAwesome.IconFontRanges != IntPtr.Zero)
                Marshal.FreeHGlobal(FontAwesome.IconFontRanges);

            FontAwesome.IconFontRanges = IntPtr.Zero;
        }
    }

    /// <summary>
    /// Draw a texture as an image in an ImGui Context
    /// Uses the current ImGui Cursor position and the full texture size.
    /// </summary>
    /// <param name="image">The raylib texture to draw</param>
    public static void Image(rlTexture image)
    {
        ImGui.Image(new IntPtr(image.Id), new Vector2(image.Width, image.Height));
    }

    /// <summary>
    /// Draw a texture as an image in an ImGui Context at a specific size
    /// Uses the current ImGui Cursor position and the specified width and height
    /// The image will be scaled up or down to fit as needed
    /// </summary>
    /// <param name="image">The raylib texture to draw</param>
    /// <param name="width">The width of the drawn image</param>
    /// <param name="height">The height of the drawn image</param>
    public static void ImageSize(rlTexture image, int width, int height)
    {
        ImGui.Image(new IntPtr(image.Id), new Vector2(width, height));
    }

    /// <summary>
    /// Draw a texture as an image in an ImGui Context at a specific size
    /// Uses the current ImGui Cursor position and the specified size
    /// The image will be scaled up or down to fit as needed
    /// </summary>
    /// <param name="image">The raylib texture to draw</param>
    /// <param name="size">The size of drawn image</param>
    public static void ImageSize(rlTexture image, Vector2 size)
    {
        ImGui.Image(new IntPtr(image.Id), size);
    }

    /// <summary>
    /// Draw a portion texture as an image in an ImGui Context at a defined size
    /// Uses the current ImGui Cursor position and the specified size
    /// The image will be scaled up or down to fit as needed
    /// </summary>
    /// <param name="image">The raylib texture to draw</param>
    /// <param name="destWidth">The width of the drawn image</param>
    /// <param name="destHeight">The height of the drawn image</param>
    /// <param name="sourceRect">The portion of the texture to draw as an image. Negative values for the width and height will flip the image</param>
    public static void ImageRect(rlTexture image, int destWidth, int destHeight, Rectangle sourceRect)
    {
        var uv0 = new Vector2();
        var uv1 = new Vector2();

        if (sourceRect.Width < 0)
        {
            uv0.X = -(sourceRect.X / image.Width);
            uv1.X = uv0.X - Math.Abs(sourceRect.Width) / image.Width;
        }
        else
        {
            uv0.X = sourceRect.X / image.Width;
            uv1.X = uv0.X + sourceRect.Width / image.Width;
        }

        if (sourceRect.Height < 0)
        {
            uv0.Y = -(sourceRect.Y / image.Height);
            uv1.Y = (uv0.Y - Math.Abs(sourceRect.Height) / image.Height);
        }
        else
        {
            uv0.Y = sourceRect.Y / image.Height;
            uv1.Y = uv0.Y + sourceRect.Height / image.Height;
        }

        ImGui.Image(new IntPtr(image.Id), new Vector2(destWidth, destHeight), uv0, uv1);
    }

    /// <summary>
    /// Draws a render texture as an image an ImGui Context, automatically flipping the Y axis so it will show correctly on screen
    /// </summary>
    /// <param name="image">The render texture to draw</param>
    public static void ImageRenderTexture(rlRenderTexture image)
    {
        ImageRect(image.Texture, image.Texture.Width, image.Texture.Height, new Rectangle(0, 0, image.Texture.Width, -image.Texture.Height));
    }

    public static void ImageFit(rlTexture image, bool center = true)
    {
        var area = ImGui.GetContentRegionAvail();

        var scale = area.X / image.Width;

        var y = image.Height * scale;
        if (y > area.Y)
        {
            scale = area.Y / image.Height;
        }

        var sizeX = (int)(image.Width * scale);
        var sizeY = (int)(image.Height * scale);

        if (center)
        {
            ImGui.SetCursorPosX(0);
            // ReSharper disable once PossibleLossOfFraction
            ImGui.SetCursorPosX(area.X / 2 - sizeX / 2);
            // ReSharper disable once PossibleLossOfFraction
            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + (area.Y / 2 - sizeY / 2));
        }

        ImageRect(image, sizeX, sizeY, new Rectangle(0, 0, (image.Width), -(image.Height)));
    }

    /// <summary>
    /// Draws a render texture as an image to the current ImGui Context, flipping the Y axis so it will show correctly on the screen
    /// The texture will be scaled to fit the content are available, centered if desired
    /// </summary>
    /// <param name="image">The render texture to draw</param>
    /// <param name="center">When true the texture will be centered in the content area. When false the image will be left and top justified</param>
    public static void ImageRenderTextureFit(RenderTexture2D image, bool center = true)
    {
        Vector2 area = ImGui.GetContentRegionAvail();

        var scale = area.X / image.Texture.Width;

        var y = image.Texture.Height * scale;
        if (y > area.Y)
        {
            scale = area.Y / image.Texture.Height;
        }

        var sizeX = (int)(image.Texture.Width * scale);
        var sizeY = (int)(image.Texture.Height * scale);

        if (center)
        {
            ImGui.SetCursorPosX(0);
            // ReSharper disable once PossibleLossOfFraction
            ImGui.SetCursorPosX(area.X / 2 - sizeX / 2);
            // ReSharper disable once PossibleLossOfFraction
            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + (area.Y / 2 - sizeY / 2));
        }

        ImageRect(image.Texture, sizeX, sizeY, new Rectangle(0, 0, (image.Texture.Width), -(image.Texture.Height)));
    }

    /// <summary>
    /// Draws a texture as an image button in an ImGui context. Uses the current ImGui cursor position and the full size of the texture
    /// </summary>
    /// <param name="name">The display name and ImGui ID for the button</param>
    /// <param name="image">The texture to draw</param>
    /// <returns>True if the button was clicked</returns>
    public static bool ImageButton(string name, rlTexture image)
    {
        return ImageButtonSize(name, image, new Vector2(image.Width, image.Height));
    }

    /// <summary>
    /// Draws a texture as an image button in an ImGui context. Uses the current ImGui cursor position and the specified size.
    /// </summary>
    /// <param name="name">The display name and ImGui ID for the button</param>
    /// <param name="image">The texture to draw</param>
    /// <param name="size">The size of the button</param>
    /// <returns>True if the button was clicked</returns>
    public static bool ImageButtonSize(string name, rlTexture image, Vector2 size)
    {
        return ImGui.ImageButton(name, new IntPtr(image.Id), size);
    }
}