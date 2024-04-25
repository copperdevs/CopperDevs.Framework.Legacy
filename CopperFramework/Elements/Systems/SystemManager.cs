using CopperCore;

namespace CopperFramework.Elements.Systems;

public static class SystemManager
{
    private static List<ISystem> systems = new();
    private static readonly Dictionary<SystemUpdateType, List<Action>> SystemActions = new();

    internal static void Initialize()
    {
        systems = LoadSystems().OrderBy(s => s.GetPriority()).ToList();

        foreach (var system in systems)
        {
            system.LoadSystem();

            if (SystemActions.TryGetValue(system.GetUpdateType(), out var value))
                value.Add(system.UpdateSystem);
            else
                SystemActions.Add(system.GetUpdateType(), new List<Action> { system.UpdateSystem });
        }
    }

    internal static void Shutdown()
    {
        foreach (var system in systems)
        {
            system.ShutdownSystem();
        }
    }

    internal static void Update(SystemUpdateType type)
    {
        if (!SystemActions.TryGetValue(type, out var actions))
            return;

        foreach (var action in actions)
            action.Invoke();
    }

    private static IEnumerable<ISystem> LoadSystems()
    {
        var targetType = typeof(ISystem);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => targetType.IsAssignableFrom(p)).ToList();

        types.Remove(typeof(ISystem));
        types.Remove(typeof(BaseSystem<>));

        foreach (var type in types)
            Log.Info($"Loading new {nameof(ISystem)} | Name: {type.FullName}");

        return types.Select(type => (ISystem)Activator.CreateInstance(type)!).ToList();
    }

    internal static T GetSystem<T>() where T : ISystem
    {
        foreach (var system in systems.Where(system => system.GetType() == typeof(T)))
            return (T)system;
        return default!;
    }

    internal static List<ISystem> GetSystems()
    {
        return systems.ToList();
    }
}