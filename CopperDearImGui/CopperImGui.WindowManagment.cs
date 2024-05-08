using ImGuiNET;

namespace CopperDearImGui;

public static partial class CopperImGui
{
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

    public static T? GetWindow<T>() where T : BaseWindow
    {
        return windows.Where(window => window.GetType() == typeof(T)).Cast<T>().FirstOrDefault();
    }
}