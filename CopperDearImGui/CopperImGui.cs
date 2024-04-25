using CopperDearImGui.ReflectionRenderers;
using ImGuiNET;

namespace CopperDearImGui;

public static class CopperImGui
{
    #region Values

    // render settings
    private static bool canRender;
    private static bool dockingEnabled;
    private static ICopperImGuiRenderer currentRenderer = null!;

    // windows
    private static List<BaseWindow> windows = new();

    // dearimgui windows
    public static bool ShowDearImGuiAboutWindow;
    public static bool ShowDearImGuiDemoWindow;
    public static bool ShowDearImGuiMetricsWindow;
    public static bool ShowDearImGuiDebugLogWindow;
    public static bool ShowDearImGuiIdStackToolWindow;

    // temps
    private static Vector2 tempVec = new();

    // actions
    public static Action? Rendered;

    #endregion

    #region DearImGui Data Layering

    public static bool AnyWindowHovered => ImGui.IsWindowHovered(ImGuiHoveredFlags.AnyWindow);
    public static bool AnyElementHovered => ImGui.GetIO().WantCaptureMouse;

    public static float CurrentWindowWidth => ImGui.GetWindowWidth();
    public static float CurrentWindowHeight => ImGui.GetWindowHeight();
    public static Vector2 CurrentWindowPosition => ImGui.GetWindowPos();
    public static Vector2 CurrentWindowSize => ImGui.GetWindowSize();
    public static ImGuiViewportPtr CurrentWindowViewport => ImGui.GetWindowViewport();
    public static float CurrentWindowDockId => ImGui.GetWindowDockID();

    public static ImFontPtr Font => ImGui.GetFont();
    public static double Time => ImGui.GetTime();

    #endregion

    #region CopperImGui Rendering

    public static void Setup<T>(bool isDockingEnabled = true) where T : ICopperImGuiRenderer, new()
    {
        currentRenderer = new T();

        currentRenderer.Setup();

        LoadConfig();
        LoadStyle();

        windows = LoadWindows();

        windows.ForEach(instance => instance.Start());

        canRender = true;
        dockingEnabled = isDockingEnabled;
    }

    public static void Render()
    {
        if (!canRender)
            return;

        currentRenderer.Begin();

        if (dockingEnabled)
            ImGui.DockSpaceOverViewport(ImGui.GetMainViewport(), ImGuiDockNodeFlags.PassthruCentralNode | ImGuiDockNodeFlags.AutoHideTabBar);

        RenderWindows();
        RenderBuiltInWindows();
        RenderPopups();

        Rendered?.Invoke();

        currentRenderer.End();
    }

    public static void Shutdown()
    {
        if (!canRender)
            return;

        currentRenderer.Shutdown();
        windows.ForEach(instance => instance.Stop());
    }

    private static void RenderBuiltInWindows()
    {
        if (ShowDearImGuiAboutWindow)
            ImGui.ShowAboutWindow(ref ShowDearImGuiAboutWindow);

        if (ShowDearImGuiDemoWindow)
            ImGui.ShowDemoWindow(ref ShowDearImGuiDemoWindow);

        if (ShowDearImGuiMetricsWindow)
            ImGui.ShowMetricsWindow(ref ShowDearImGuiMetricsWindow);

        if (ShowDearImGuiDebugLogWindow)
            ImGui.ShowDebugLogWindow(ref ShowDearImGuiDebugLogWindow);

        if (ShowDearImGuiIdStackToolWindow)
            ImGui.ShowIDStackToolWindow(ref ShowDearImGuiIdStackToolWindow);
    }

    #endregion

    #region Popup Managment

    private static readonly Dictionary<string, Action> RegisteredPopups = new();

    public static void RegisterPopup(string id, Action renderAction)
    {
        RegisteredPopups.TryAdd(id, renderAction);
    }

    public static void DeregisterPopup(string id)
    {
        RegisteredPopups.Remove(id);
    }

    public static void ShowPopup(string id)
    {
        ImGui.OpenPopup(id);
    }

    private static void RenderPopups()
    {
        foreach (var popup in RegisteredPopups.Where(popup => ImGui.BeginPopup(popup.Key)))
        {
            popup.Value?.Invoke();
            ImGui.EndPopup();
        }
    }
    
    public static void ForceRenderPopup(string id)
    {
        if (!RegisteredPopups.TryGetValue(id, out var popup)) 
            return;

        if (!ImGui.BeginPopup(id)) 
            return;
        
        popup?.Invoke();
        ImGui.EndPopup();
    }

    #endregion

    #region Window Managment

    private static List<BaseWindow> LoadWindows()
    {
        var targetType = typeof(BaseWindow);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => targetType.IsAssignableFrom(p)).ToList();

        types.Remove(typeof(BaseWindow));

        foreach (var type in types)
            Log.Info($"Loading new {nameof(BaseWindow)} | Name: {type.FullName}");

        return types.Select(type => (BaseWindow)Activator.CreateInstance(type)!).ToList();
    }

    private static void RenderWindows()
    {
        windows.ForEach(window =>
        {
            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("Windows"))
                {
                    ImGui.MenuItem(window.WindowName, null, ref window.WindowOpen);
                    ImGui.EndMenu();
                }

                ImGui.EndMainMenuBar();
            }

            if (!window.WindowOpen)
                return;

            if (ImGui.Begin(window.WindowName, ref window.WindowOpen))
            {
                window.Update();
                ImGui.End();
            }
        });
    }

    #endregion

    #region ImGuiReflection Layering

    public static void RenderValues(object component, int id = 0)
    {
        ImGuiReflection.RenderValues(component);
    }

    public static FieldRenderer? GetFieldRenderer<T>()
    {
        return ImGuiReflection.GetImGuiRenderer<T>();
    }

    public static void RegisterFieldRenderer<TType, TRenderer>() where TRenderer : FieldRenderer, new()
    {
        ImGuiReflection.ImGuiRenderers.TryAdd(typeof(TType), new TRenderer());
    }

    #endregion

    #region DearImGui Theming

    private static void LoadConfig()
    {
        ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.DockingEnable;
        ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.ViewportsEnable;
        ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard;
        ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.NavEnableGamepad;

        ImGui.GetIO().ConfigWindowsMoveFromTitleBarOnly = false;

        ImGui.GetStyle().WindowRounding = 5;
        ImGui.GetStyle().ChildRounding = 5;
        ImGui.GetStyle().FrameRounding = 5;
        ImGui.GetStyle().PopupRounding = 5;
        ImGui.GetStyle().ScrollbarRounding = 5;
        ImGui.GetStyle().GrabRounding = 5;
        ImGui.GetStyle().TabRounding = 5;

        ImGui.GetStyle().TabBorderSize = 1;

        ImGui.GetStyle().WindowTitleAlign = new Vector2(0.5f);
        ImGui.GetStyle().SeparatorTextAlign = new Vector2(0.5f);
        ImGui.GetStyle().SeparatorTextPadding = new Vector2(20, 5);
    }

    private static void LoadStyle()
    {
        var colors = ImGui.GetStyle().Colors;
        colors[(int)ImGuiCol.WindowBg] = new Vector4(0.1f, 0.105f, 0.11f, 1.0f);

        // Headers
        colors[(int)ImGuiCol.Header] = new Vector4(0.2f, 0.205f, 0.21f, 1.0f);
        colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.3f, 0.305f, 0.31f, 1.0f);
        colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);

        // Buttons
        colors[(int)ImGuiCol.Button] = new Vector4(0.2f, 0.205f, 0.21f, 1.0f);
        colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.3f, 0.305f, 0.31f, 1.0f);
        colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);

        // Frame BG
        colors[(int)ImGuiCol.FrameBg] = new Vector4(0.2f, 0.205f, 0.21f, 1.0f);
        colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.3f, 0.305f, 0.31f, 1.0f);
        colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);

        // Tabs
        colors[(int)ImGuiCol.Tab] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
        colors[(int)ImGuiCol.TabHovered] = new Vector4(0.38f, 0.3805f, 0.381f, 1.0f);
        colors[(int)ImGuiCol.TabActive] = new Vector4(0.28f, 0.2805f, 0.281f, 1.0f);
        colors[(int)ImGuiCol.TabUnfocused] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
        colors[(int)ImGuiCol.TabUnfocusedActive] = new Vector4(0.2f, 0.205f, 0.21f, 1.0f);

        // Title
        colors[(int)ImGuiCol.TitleBg] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
        colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
        colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.15f, 0.1505f, 0.151f, 1.0f);
    }

    #endregion

    #region DearImGui Layering

    public static void Separator()
    {
        if (canRender)
            ImGui.SeparatorText("");
    }

    public static void Separator(string text)
    {
        if (canRender)
            ImGui.SeparatorText(text);
    }

    public static void Space()
    {
        if (canRender)
            ImGui.Dummy(tempVec with { Y = 20 });
    }

    public static void Space(float amount)
    {
        if (canRender)
            ImGui.Dummy(tempVec with { Y = amount });
    }

    public static void HorizontalGroup(params Action[] items)
    {
        if (!canRender)
            return;

        foreach (var action in items)
        {
            action.Invoke();
            ImGui.SameLine();
        }

        ImGui.Dummy(tempVec with { X = 0, Y = 0 });
    }

    public static void Group(string id, Action group, float height = 0, float width = 0)
    {
        if (canRender)
            Group(id, group, ImGuiChildFlags.None, height, width);
    }

    public static void Group(string id, Action group, ImGuiChildFlags flags, float height = 0, float width = 0)
    {
        if (!canRender)
            return;
        if (!ImGui.BeginChild(id, tempVec with { X = width, Y = height }, flags))
            return;

        group.Invoke();
        ImGui.EndChild();
    }

    public static void Selectable(string text, Action? clickEvent = null)
    {
        if (canRender)
            Selectable(text, false, clickEvent);
    }

    public static void Selectable(string text, bool enabled, Action? clickEvent = null)
    {
        if (!canRender)
            return;
        if (ImGui.Selectable(text, enabled))
            clickEvent?.Invoke();
    }

    public static void Text(object value, string title)
    {
        if (canRender)
            ImGui.LabelText(title, $"{value}");
    }

    public static void Text(object? value)
    {
        if (canRender)
            ImGui.Text($"{value}");
    }

    public static bool DragValue(string name, Matrix4x4 matrix4X4)
    {
        if (!canRender)
            return false;

        var matrix = matrix4X4;
        return DragValue(name, ref matrix);
    }

    public static bool DragValue(string name, ref Matrix4x4 matrix)
    {
        if (!canRender)
            return false;

        var interacted = false;

        if (ImGui.CollapsingHeader(name))
        {
            ImGui.Indent();

            interacted =
                DragMatrix4X4Row($"Row One##{name}",
                    ref matrix.M11, ref matrix.M12, ref matrix.M13, ref matrix.M14) ||
                DragMatrix4X4Row($"Row Two##{name}",
                    ref matrix.M21, ref matrix.M22, ref matrix.M23, ref matrix.M24) ||
                DragMatrix4X4Row($"Row Three##{name}",
                    ref matrix.M31, ref matrix.M32, ref matrix.M33, ref matrix.M34) ||
                DragMatrix4X4Row($"Row Four##{name}",
                    ref matrix.M41, ref matrix.M42, ref matrix.M43, ref matrix.M44);

            ImGui.Unindent();
        }

        return interacted;
    }

    private static bool DragMatrix4X4Row(string rowName,
        ref float itemOne, ref float itemTwo, ref float itemThree, ref float itemFour)
    {
        if (!canRender)
            return false;

        var interacted = false;
        var row = new Vector4(itemOne, itemTwo, itemThree, itemFour);

        if (ImGui.DragFloat4(rowName, ref row))
        {
            interacted = true;
            itemOne = row.X;
            itemTwo = row.Y;
            itemThree = row.Z;
            itemFour = row.W;
        }

        return interacted;
    }

    public static void Tooltip(string message)
    {
        if (!canRender)
            return;

        if (!ImGui.BeginItemTooltip())
            return;

        ImGui.PushTextWrapPos(ImGui.GetFontSize() * 35.0f);

        ImGui.TextUnformatted(message);

        ImGui.PopTextWrapPos();

        ImGui.EndTooltip();
    }

    public static void CollapsingHeader(string name, Action group)
    {
        if (!canRender)
            return;

        if (ImGui.CollapsingHeader(name))
        {
            group.Invoke();
        }
    }

    public static void Button(string name, Action? clickEvent = null)
    {
        if (!canRender)
            return;

        if (ImGui.Button(name))
            clickEvent?.Invoke();
    }

    public static void Button(string name, float width, float height = 0, Action? clickEvent = null)
    {
        if (!canRender)
            return;

        if (ImGui.Button(name, tempVec with { X = width, Y = height }))
            clickEvent?.Invoke();
    }

    public static void Checkbox(string name, ref bool currentValue, Action<bool>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.Checkbox(name, ref currentValue))
            interacted?.Invoke(currentValue);
    }

    public static void ColorEdit(string name, ref Vector4 color, Action<Vector4>? interacted = null)
    {
        if (!canRender)
            return;

        var vectorColor = color;

        if (!ImGui.ColorEdit4(name, ref vectorColor))
            return;

        interacted?.Invoke(vectorColor);
    }

    public static void DragValue(string name, ref float value, Action<float>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragFloat(name, ref value))
            interacted?.Invoke(value);
    }

    public static void SliderValue(string name, ref float value, float min, float max,
        Action<float>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.SliderFloat(name, ref value, min, max))
            interacted?.Invoke(value);
    }

    public static void DragValue(string name, ref Vector2 value, Action<Vector2>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragFloat2(name, ref value))
            interacted?.Invoke(value);
    }

    public static void SliderValue(string name, ref Vector2 value, float min, float max,
        Action<Vector2>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.SliderFloat2(name, ref value, min, max))
            interacted?.Invoke(value);
    }


    public static void DragValue(string name, ref Vector2Int value, Action<Vector2Int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragInt2(name, ref value.X))
            interacted?.Invoke(value);
    }

    public static void SliderValue(string name, ref Vector2Int value, int min, int max, Action<Vector2Int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.SliderInt2(name, ref value.X, min, max))
            interacted?.Invoke(value);
    }

    public static void DragValue(string name, ref Vector3 value, Action<Vector3>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragFloat3(name, ref value))
            interacted?.Invoke(value);
    }

    public static void SliderValue(string name, ref Vector3 value, float min, float max,
        Action<Vector3>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.SliderFloat3(name, ref value, min, max))
            interacted?.Invoke(value);
    }

    public static void DragValue(string name, ref Vector4 value, Action<Vector4>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragFloat4(name, ref value))
            interacted?.Invoke(value);
    }

    public static void SliderValue(string name, ref Vector4 value, float min, float max,
        Action<Vector4>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.SliderFloat4(name, ref value, min, max))
            interacted?.Invoke(value);
    }

    public static void DragValue(string name, ref int value, Action<int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.DragInt(name, ref value))
            interacted?.Invoke(value);
    }

    public static void SliderValue(string name, ref int value, int min, int max, Action<int>? interacted = null!)
    {
        if (!canRender)
            return;

        if (ImGui.SliderInt(name, ref value, min, max))
            interacted?.Invoke(value);
    }


    public static void Text(string name, ref string value, Action<string>? interacted = null!, uint maxLength = 64)
    {
        if (!canRender)
            return;

        if (ImGui.InputText(name, ref value, maxLength))
            interacted?.Invoke(value);
    }

    public static void TabGroup(string id, params (string, Action)[] tabs)
    {
        if (!canRender)
            return;

        if (!ImGui.BeginTabBar(id, ImGuiTabBarFlags.Reorderable))
            return;

        for (var i = 0; i < tabs.Length; i++)
        {
            var (tabTitle, tabAction) = tabs[i];

            if (!ImGui.BeginTabItem($"{tabTitle}###{id}{i}"))
                continue;

            tabAction?.Invoke();
            ImGui.EndTabItem();
        }

        ImGui.EndTabBar();
    }

    public static bool MenuItem(string text, ref bool enabled)
    {
        return canRender && ImGui.MenuItem(text, null, ref enabled);
    }

    public static void MenuBar(params (string, Action)[] subMenus)
    {
        MenuBar(null!, false, subMenus);
    }

    public static void MenuBar(Action action = null!, bool isMainMenuBar = false, params (string, Action)[] subMenus)
    {
        if (isMainMenuBar ? !ImGui.BeginMainMenuBar() : !ImGui.BeginMenuBar())
            return;

        foreach (var subMenu in subMenus)
        {
            if (!ImGui.BeginMenu(subMenu.Item1))
                continue;

            subMenu.Item2?.Invoke();
            ImGui.EndMenu();
        }

        action?.Invoke();

        if (isMainMenuBar)
            ImGui.EndMainMenuBar();
        else
            ImGui.EndMenuBar();
    }

    #endregion
}